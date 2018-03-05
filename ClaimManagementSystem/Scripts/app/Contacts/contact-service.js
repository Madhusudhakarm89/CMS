(function (applicationBaseUrl) {
    "use strict";
    var contactsServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/Contact/:id",
                        { id: "@contactId" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Contact/GetContacts',
                                isArray: true
                            },
                            'getContact': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/Contact/Find/:id'
                            },
                            'findContacts': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Contact/Find/',
                                isArray: true
                            },
                            'addContact': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Contact/Save'
                            },
                            'updateContact': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/Contact/Update'
                            },
                            'deleteContact': {
                                method: 'DELETE',
                                url: applicationBaseUrl + '/api/Contact/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ContactsService", ["$resource", contactsServiceFactory]);
}(window.appURL));