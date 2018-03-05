/// <reference path="timelogUnit-service.js" />
(function (applicationBaseUrl) {
    "use strict";
    var TimelogUnitServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/TimelogUnit/:id",
                        { id: "@unitId" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TimelogUnit/GetAllUnits',
                                isArray: true
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("TimelogUnitService", ["$resource", TimelogUnitServiceFactory]);
}(window.appURL));