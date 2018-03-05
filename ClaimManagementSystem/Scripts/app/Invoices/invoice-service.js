(function (applicationBaseUrl) {
    "use strict";
    var invoiceServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Invoice/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Invoice/GetAllInvoice',
                                isArray: true
                            },
                            'getInvoicesByClaim': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Invoice/GetAllInvoiceByClaim/:id',
                                isArray: true
                            },
                            'getInvoice': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Invoice/Find/:id'
                            },
                            'findInvoices': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Invoice/Find'
                            },
                            'addInvoice': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Invoice/Save'
                            },
                            'updateInvoice': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Invoice/Update'
                            },
                            'deleteInvoice': {
                                method: 'DELETE',
                                url: applicationBaseUrl + '/api/Invoice/Delete/:id'
                            },
                            'GenerateInvoice': {
                                method: 'POST',
                                url: applicationBaseUrl + '/Account/GenerateIncoicePdf/:id',id: '@id'
                            },
                            'getGeneratredInvoices': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Invoice/GetGeneratedInvoices/:id',
                                isArray: true
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("InvoiceService", ["$resource", invoiceServiceFactory]);
}(window.appURL));