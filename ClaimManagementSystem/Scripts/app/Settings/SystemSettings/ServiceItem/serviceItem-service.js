(function (applicationBaseUrl) {
    "use strict";
    var serviceItemServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/ServiceItem/:id",
                        { id: "@serviceItemId" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ServiceItem/GetAllServiceItems',
                                isArray: true
                            },
                            'getServiceItem': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ServiceItem/Find/:id'
                            },
                            'findServiceItem': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ServiceItem/Find/',
                                isArray: true
                            },
                            'addServiceItem': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ServiceItem/Save'
                            },
                            'updateServiceItem': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ServiceItem/Update'
                            },
                            'deleteServiceItem': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/ServiceItem/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ServiceItemService", ["$resource", serviceItemServiceFactory]);
}(window.appURL));