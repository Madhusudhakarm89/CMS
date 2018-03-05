(function (applicationBaseUrl) {

    "use strict";
    var systemAlertsServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/User/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/SystemAlerts/GetAllSystemAlerts',
                                isArray: true
                            },
                          
                            'addSystemAlerts': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/SystemAlerts/Create/:id'
                            },
                            'getSystemAlerts': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/SystemAlerts/GetSystemAlerts',
                                isArray: true
                            },
                            'updateSystemAlerts': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/SystemAlerts/Update'
                            },
                            'deleteSystemAlerts': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/SystemAlerts/Delete/:id'
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/SystemAlerts/Find/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("SystemAlertsService", ["$resource", systemAlertsServiceFactory]);
}(window.appURL));