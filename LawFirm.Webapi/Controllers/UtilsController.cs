﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LawFirm.Webapi.DTO;

namespace LawFirm.Webapi.Controllers
{
    public class UtilsController : ApiController
    {
        [HttpPost]
        [Route("Utils/UploadImages/")]
        public async Task<HttpResponseMessage> UploadImages()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var folderName = HttpContext.Current.Request.Form["FolderName"];
                var oldProp = HttpContext.Current.Request.Form["OldProp"];

                var provider = GetMultipartProvider(folderName);
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                var originalFileName = GetDeserializedFileName(result.FileData.First());
                string extension = originalFileName.Split('.')[1];
                var newName = Guid.NewGuid().ToString() +'.'+ extension;


                var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

                string targetFile = Path.Combine(uploadedFileInfo.DirectoryName, newName);

                if(oldProp != "null")
                {
                    var url = new Uri(oldProp);
                    oldProp = '~' + url.LocalPath;
                    oldProp = HttpContext.Current.Server.MapPath(oldProp);
                    if (File.Exists(oldProp))
                        File.Delete(oldProp);
                }
                
                uploadedFileInfo.MoveTo(targetFile);
                var response = Request.CreateResponse(HttpStatusCode.OK, "/images/"+folderName+'/'+newName);

                return response;
                
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new ObjectContent(ex.GetType(), ex, new JsonMediaTypeFormatter()) };
            }
        }

        private MultipartFormDataStreamProvider GetMultipartProvider(string foldername)
        {
            var uploadFolder = HttpContext.Current.Server.MapPath("~/images/"+foldername);

            if (Directory.Exists(uploadFolder) == false) Directory.CreateDirectory(uploadFolder);

            return new MultipartFormDataStreamProvider(uploadFolder);
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        private string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}
