angular.module("umbraco.resources")
    .factory("FrontUsersManagerResource", function ($http) {
        return {
            getById: function (id) {
                return $http({
                    url: "backoffice/FrontUsersManager/FrontUsersApi/GetUser/" + id,
                    method: 'GET',
                    transformResponse: [
                        function(data) {
                            data = data.substring(data.indexOf("\n") + 1);
                            return JSON.parse(data);
                        }
                    ]
                });
            },
            getByQuery: function (query) {
                return $http.get("backoffice/FrontUsersManager/FrontUsersApi/SearchUsers?query="+query);
            },
            update: function (id,user) {
                return $http.post("backoffice/FrontUsersManager/FrontUsersApi/Update/" + id, user);
            },
            delete: function (id) {
                return $http.post("backoffice/FrontUsersManager/FrontUsersApi/Delete/" + id);
            },
            getRegions: function () {
                return $http.get("backoffice/AdditionalSettings/RegionDBApi/GetAll/");
            }
        };
    });