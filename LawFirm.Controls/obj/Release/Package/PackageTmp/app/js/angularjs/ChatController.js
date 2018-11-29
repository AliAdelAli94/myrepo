﻿
app.controller("chatController", function ($scope, $rootScope, signalR, lawfirmService) {

    var msgContent = document.getElementById("msgContent");
    msgContent.addEventListener("keydown", function (e) {
        if (e.keyCode === 13) {
            $scope.sendMessage();
        }
    });


    $scope.client = {};
    $scope.messages = [];
    $scope.startChat = function () {
        lawfirmService.GetNumberOfOnlineUsers(function(response)
        {
            if(parseInt(response.data) > 0)
            {
                if ($scope.client) {
                    if ($scope.client.username && $scope.client.email) {

                        signalR.startHub();
                        signalR.startChat($scope.client.username, $scope.client.email);
                    }
                }
            }

        }, function () { })
    };

    signalR.recieveMessage(function (msg, fromCID) {

        console.log("new Message : ", msg);

        $scope.messages.push({ content: msg, dir: 0 });
        $scope.$apply();

    });


    signalR.getAdminData(function (cid) {

        $scope.adminConnectionID = cid;
        console.log("admin id : " + $scope.adminConnectionID);

    });


    $scope.sendMessage = function (e) {
        if ($scope.adminConnectionID)
        {
            $scope.messages.push({ content: $scope.msgContent, dir: 1 })
            signalR.sendMessage($scope.msgContent, $scope.adminConnectionID);
            $scope.msgContent = "";
            $scope.$apply();
        }
        else
        {
            $scope.messages.push({ content: $scope.msgContent, dir: 1 })
            signalR.startChat($scope.client.username, $scope.client.email);
            signalR.sendMessageBroadCast($scope.msgContent);
            $scope.msgContent = "";
            $scope.$apply();
        }
        
    };


});


