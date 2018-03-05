(function (applicationBaseUrl) {
    "use strict";
    var timelogServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/TimeLog/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TimeLog/GetTimeLogs',
                                isArray: true
                            },
                            'getTimeLog': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TimeLog/Find/:id'
                            },
                            'findTimeLogs': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TimeLog/Find'
                            },
                            'addTimeLog': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TimeLog/Save'
                            },
                            'updateTimeLog': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TimeLog/Update'
                            },
                            'deleteTimeLog': {
                                method: 'DELETE',
                                url: applicationBaseUrl + '/api/TimeLog/Delete/:id'
                            },
                             'updateBilledTimeLog': {
                                 method: 'POST',
                            url: applicationBaseUrl + '/api/TimeLog/IsBilledUpdate'
    }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("TimeLogService", ["$resource", timelogServiceFactory]);
}(window.appURL));