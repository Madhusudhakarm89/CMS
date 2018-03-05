(function (applicationBaseUrl) {
    "use strict";
    var countriesServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Country/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Country/GetAllCountries',
                                isArray: true
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Country/Find/:id'
                            },
                            'addCountry': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Country/Create/:id',
                              
                            },
                            'deleteCountry': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Country/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("CountriesService", ["$resource", countriesServiceFactory]);
}(window.appURL));