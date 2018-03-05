(function (applicationBaseUrl) {
    "use strict";
    var taxSettingServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/TaxSetting/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TaxSetting/GetAllTaxSettings',
                                isArray: true
                            },
                            'getTaxSetting': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TaxSetting/Find/:id'
                            },
                            'findTaxSetting': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TaxSetting/Find/',
                                isArray: true
                            },
                            'addTaxSetting': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TaxSetting/Save'
                            },
                            'updateTaxSetting': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TaxSetting/Update'
                            },
                            'deleteTaxSetting': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TaxSetting/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("TaxSettingService", ["$resource", taxSettingServiceFactory]);
}(window.appURL));