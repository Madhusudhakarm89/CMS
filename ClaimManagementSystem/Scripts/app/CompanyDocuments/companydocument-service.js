(function (applicationBaseUrl) {
    "use strict";
    var companydocumentsServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/AccountDocumentMapping/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/AccountDocumentMapping/GetDocuments',
                                isArray: true
                            },
                            'GetCompanyDocuments': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/AccountDocumentMapping/GetCompanyDocuments/:id'
                            },
                            'addCountry': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/AccountDocumentMapping/Create/:id',
                              
                            },
                            'deleteCountry': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/AccountDocumentMapping/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("CompanyDocumentsService", ["$resource", companydocumentsServiceFactory]);
}(window.appURL));