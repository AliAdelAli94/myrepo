angular
    .module('altairApp')
    .controller('faqs_ctrl', [
        '$scope',
        '$rootScope',
        'dashService',
        '$stateParams',
        '$filter',
function ($scope, $rootScope, dashService, $stateParams, $filter) {


    $scope.pageSize = 10;

    $scope.filter_pageSize = ['5', '10', '15'];

    GetAllFaqData();

    EditFaq = function () {

        dashService.EditFaq($scope.currenFaq, function (response) {
            console.log(response);
        },
    function (response) { });

    };


    AddFaq = function () {

        dashService.AddFaqDashboard($scope.currenFaq, function (response) {
            console.log(response);
        },
    function (response) { });

    };

    $scope.DeleteFaq = function (id) {
        dashService.DeleteFaqDashboard(id, function (response) {

            GetAllFaqData();

        },
          function (response) { });
    };


    Array.prototype.contains = function (obj) {
        var i = this.length;
        while (i--) {
            if (this[i] === obj) {
                return true;
            }
        }
        return false;
    }


    function GetAllFaqData() {

        dashService.GetAllFaq(
            function (response) {
                debugger;
                $scope.faqs = response.data;

                if ($stateParams != null) {
                    if ($stateParams.id != null) {
                        $scope.action = "edit";
                        $scope.currenFaq = $filter('filter')($scope.faqs, { id: parseInt($stateParams.id) }, true)[0];
                    }
                    else
                    {
                        $scope.action = "insert";
                    }
                }
            },
            function (response) { });
    };


    $scope.Submit = function (item) {

        if($scope.action == "edit")
        {
            EditFaq(item);
        }
        else if ($scope.action == "insert")
        {
            AddFaq(item);
        }
    };


}]);
