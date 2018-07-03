angular.module("umbraco").controller("PolRegio.RegionEditController",
    function ($scope, $routeParams, regionDbResource, notificationsService, navigationService) {
        $scope.loaded = false;

        if ($routeParams.id == -1) {
            $scope.item = {};
            $scope.item.isenabled = true;
            $scope.loaded = true;
        }
        else {
            regionDbResource.getById($routeParams.id).then(function (response) {
                $scope.item = response.data;
                $scope.loaded = true;
            });
        }

        $scope.save = function (item) {
            if ($routeParams.id == -1) {
                item.dictionarykey = "region_" + item.name;
            }

            regionDbResource.save(item).then(function (response) {
                $scope.item = response.data;
                $scope.serviceForm.$dirty = false;
                navigationService.syncTree({ tree: 'regiondbtree', path: [-1, -1], forceReload: true });
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