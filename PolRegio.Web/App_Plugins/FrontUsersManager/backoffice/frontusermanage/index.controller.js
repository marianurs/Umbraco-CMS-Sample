angular.module("umbraco").controller("FrontUsersManager.FrontUsersManagerIndexController",
    function ($scope, $routeParams, FrontUsersManagerResource, notificationsService, navigationService) {

        $scope.loaded = false;

        if ($routeParams.id == -1) {
            $scope.node = {};
            $scope.loaded = true;
        }
        else {
            $scope.node = {
                query: "",
                users : []
            };

            $scope.loaded = true;
        }


        $scope.search = function () {
            if ($scope.node.query.length < 3) {
                $scope.node.users = [];
                return;
            }
            FrontUsersManagerResource.getByQuery($scope.node.query).then(function (response) {
                $scope.node.users = response.data;
            });
        };
    });