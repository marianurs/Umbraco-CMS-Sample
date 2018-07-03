angular.module("umbraco").controller("PolRegio.ArticleTypeEditController",
    function ($scope, $routeParams, articleTypeResource, notificationsService, navigationService) {
        $scope.loaded = false;

        if ($routeParams.id == -1) {
            $scope.item = {};
            $scope.item.isenabled = true;
            $scope.loaded = true;
        }
        else {
            articleTypeResource.getById($routeParams.id).then(function (response) {
                $scope.item = response.data;
                $scope.loaded = true;
            });
        }

        $scope.save = function (item) {
            if ($routeParams.id == -1) {
                item.dictionarykey = "articleType_" + item.name;
            }

            articleTypeResource.save(item).then(function (response) {
                $scope.item = response.data;
                $scope.serviceForm.$dirty = false;
                navigationService.syncTree({ tree: 'articletypedbtree', path: [-1, -1], forceReload: true });
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