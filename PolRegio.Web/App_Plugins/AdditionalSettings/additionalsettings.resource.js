angular.module("umbraco.resources")
.factory("articleTypeResource", function ($http) {
    return {
        getById: function (id) {
            return $http.get("backoffice/AdditionalSettings/ArticleTypeDBApi/GetById?id=" + id);
        },
        save: function (serviceitem) {
            return $http.post("backoffice/AdditionalSettings/ArticleTypeDBApi/PostSave", angular.toJson(serviceitem));
        }
    };
});
angular.module("umbraco.resources")
.factory("regionDbResource", function ($http) {
    return {
        getById: function (id) {
            return $http.get("backoffice/AdditionalSettings/RegionDBApi/GetById?id=" + id);
        },
        save: function (item) {
            return $http.post("backoffice/AdditionalSettings/RegionDBApi/PostSave", angular.toJson(item));
        }
    };
});
angular.module("umbraco.resources")
.factory("administrativeDbResource", function ($http) {
    return {
        getById: function (id) {
            return $http.get("backoffice/AdditionalSettings/AdministrativeDBApi/GetById?id=" + id);
        },
        save: function (item) {
            return $http.post("backoffice/AdditionalSettings/AdministrativeDBApi/PostSave", angular.toJson(item));
        }
    };
});