(function (applicationBaseUrl) {
    "use strict";
    var claimNoteServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/ClaimNotes/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimNotes/GetClaimNotes',
                                isArray: true
                            },
                            'getClaimNote': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimNotes/Find/:id'
                            },
                            'addClaimNote': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ClaimNotes/Save'
                            },
                            'updateClaimNote': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ClaimNotes/Update'
                            },
                            'deleteClaimNote': {
                                method: 'DELETE',
                                url: applicationBaseUrl + '/api/ClaimNotes/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ClaimNotesService", ["$resource", claimNoteServiceFactory]);
}(window.appURL));