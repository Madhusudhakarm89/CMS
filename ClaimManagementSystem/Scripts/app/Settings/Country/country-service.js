(function (applicationBaseUrl) {
    "use strict";
    var companiesServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Company/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Company/GetCompanies',
                                isArray: true
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Company/Find/:id'
                            },
                            'addCompany': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Company/Create/:id',
                              
                            },
                            'deleteCompany': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Company/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("CompaniesService", ["$resource", companiesServiceFactory]);
}(window.appURL));