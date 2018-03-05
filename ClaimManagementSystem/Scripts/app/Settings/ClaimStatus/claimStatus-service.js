(function (applicationBaseUrl) {
    "use strict";
    var claimStatusServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/ClaimStatus/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimStatus/GetAllClaimStatus',
                                isArray: true
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimStatus/Find/:id'
                            },
                            'addClaimStatus': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ClaimStatus/Create',

                            },
                            'updateClaimStatus': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ClaimStatus/Update',

                            },
                            'deleteClaimStatus': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ClaimStatus/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ClaimStatusService", ["$resource", claimStatusServiceFactory]);
}(window.appURL));