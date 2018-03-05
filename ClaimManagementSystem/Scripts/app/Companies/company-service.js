(function (applicationBaseUrl) {
    "use strict";
    var companiesServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Company/:id",
                        { id: "@companyId" },
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
                            'getCompanyDocuments': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/AccountDocumentMapping/GetDocuments/:id'
                            },
                            'addCompany': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Company/Create/:id'
                            },
                            'updateCompany': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Company/Update/:id'                              
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