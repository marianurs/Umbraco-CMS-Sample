angular.module("umbraco").controller("FrontUsersManager.FrontUsersManagerEditController",
    function ($scope, $routeParams, FrontUsersManagerResource, notificationsService, navigationService) {

        $scope.loaded = false;

        if ($routeParams.id == -1) {
            $scope.node = {};
            $scope.loaded = true;
        }
        else {
            Promise.all([
                FrontUsersManagerResource.getById($routeParams.id),
                FrontUsersManagerResource.getRegions()

            ]).then(function (responses) {
                $scope.regions = responses[1].data; // save regions info
                $scope.node = responses[0].data; // save user data
                $scope.node.SelectedRegions = $scope.node.SelectedRegions.map(function (i) { return parseInt(i); });



                $scope.loaded = true;
            });
        }

        $scope.update = function () {
            FrontUsersManagerResource.update($routeParams.id, $scope.node).then(function (response) {
                $scope.contentForm.$dirty = false;
                navigationService.syncTree({ tree: 'frontusermanage', path: [-1, -1], forceReload: true });
                notificationsService.success("Zaktualizowano użytkowania");
            });
        };

        $scope.delete = function () {
            FrontUsersManagerResource.delete($routeParams.id).then(function (response) {
                if (response.data === "true") {
                    $scope.contentForm.$dirty = false;
                    navigationService.syncTree({ tree: 'frontusermanage', path: [-1, -1], forceReload: true });
                    notificationsService.success("Usunięto użytkownika");
                    window.location.hash = "/users/frontusermanage/index/1";
                }
            });
        };

    });