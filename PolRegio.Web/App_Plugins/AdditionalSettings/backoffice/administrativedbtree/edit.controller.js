angular.module("umbraco").controller("PolRegio.AdministrativeEditController",
    function ($scope, $routeParams, administrativeDbResource, notificationsService, navigationService) {
        $scope.loaded = false;

        if ($routeParams.id == -1) {
            $scope.item = {};
            $scope.item.isenabled = true;
            $scope.loaded = true;
        }
        else {
            administrativeDbResource.getById($routeParams.id).then(function (response) {
                $scope.item = response.data;
                $scope.loaded = true;
            });
        }

        $scope.save = function (item) {
            if ($routeParams.id == -1) {
                item.dictionarykey = "region_" + item.name;
            }

            administrativeDbResource.save(item).then(function (response) {
                $scope.item = response.data;
                $scope.serviceForm.$dirty = false;
                navigationService.syncTree({ tree: 'administrativedbtree', path: [-1, -1], forceReload: true });
                notificationsService.success("Success", item.name + " has been saved");
            });
        };

        $scope.showEditDictionary = function () {
            return $routeParams.id == -1;
        }

        $scope.showStaticDictionary = function () {
            return $routeParams.id != -1;
        }
        
    });