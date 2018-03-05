(function (applicationBaseUrl) {
    "use strict";
    var HomeServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/ClaimNotes/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'GetClaimNotesAssignedToMe': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimNotes/GetClaimNotesAssignedToMe',
                                isArray: true
                            },
                            'GetClaimNotesAssignedByMe': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimNotes/GetClaimNotesAssignedByMe',
                                isArray: true
                            },
                            'GetOverdueTasks': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ClaimNotes/GetOverdueTasks',
                                isArray: true
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("HomeService", ["$resource", HomeServiceFactory]);
}(window.appURL));