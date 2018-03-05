(function (applicationBaseUrl) {
    "use strict";
    var fileNamingServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/FileNaming/:id",
                        { id: "@serviceItemId" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/FileNaming/GetAllFileNamingCode',
                                isArray: true
                            },
                            'getFileNameCode': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/FileNaming/Find/:id'
                            },
                            'findFileNameCodes': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/FileNaming/Find/',
                                isArray: true
                            },
                            'addFileNameCode': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/FileNaming/Save'
                            },
                            'updateFileNameCode': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/FileNaming/Update'
                            },
                            'deleteFileNameCode': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/FileNaming/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("FileNamingService", ["$resource", fileNamingServiceFactory]);
}(window.appURL));