(function (applicationBaseUrl) {
    "use strict";
    var globalSearchServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/GlobalSearch/:id",
                        { id: "@searchKeyword" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/GlobalSearch/GetGlobalSearchResult/:id',
                                isArray: false
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("GlobalSearchService", ["$resource", globalSearchServiceFactory]);
}(window.appURL));