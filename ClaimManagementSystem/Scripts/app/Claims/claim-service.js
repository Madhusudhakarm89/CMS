(function (applicationBaseUrl) {
    "use strict";
    var claimServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Claims/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Claims/GetClaims',
                                isArray: true
                            },
                            'getClaim': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Claims/Find/:id'
                            },
                            'getDocuments': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Claims/GetClaimDocuments',
                                isArray: true
                            },
                            'addClaim': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Claims/Save'
                            },
                            'updateClaim': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Claims/Update'
                            },
                            'deleteClaim': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Claims/Delete/:id'
                            },
                            'deleteDocument': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Claims/DeleteClaimDocument/:id'
                            },
                            'GetAllClaimsDueToday': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Claims/GetAllClaimsDueToday',
                                isArray: true
                            },
                            'GetAllClaimsDueIn7Days': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Claims/GetAllClaimsDueIn7Days',
                                isArray: true
                            },
                            'GetOverdueClaims': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Claims/GetOverdueClaims',
                                isArray: true
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ClaimService", ["$resource", claimServiceFactory]);
}(window.appURL));