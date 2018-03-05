(function (applicationBaseUrl) {
    
    "use strict";
    var statesServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Province/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Province/GetAllProvinces',
                                isArray: true
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Province/Find/:id'
                            },
                            'addState': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Province/Create/:id',
                              
                            },
                           
                            'deleteState': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Province/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("StatesService", ["$resource", statesServiceFactory]);
}(window.appURL));