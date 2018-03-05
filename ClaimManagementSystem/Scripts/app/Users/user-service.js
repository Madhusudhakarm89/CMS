(function (applicationBaseUrl) {
   
    "use strict";
    var usersServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/User/:id",
                        { id: "@id" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/User/GetAllUsers',
                                isArray: true
                            },
                            'get': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/User/Find/:id'
                            },
                            'sendEmail': {
                                method: 'POST',
                                url: applicationBaseUrl + '/Account/SendEmail/:id'
                            },
                            'addUser': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/User/Create/:id',
                              
                            },
                            'updateUser': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/User/Update/:id'
                            },
                            'deleteUser': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/User/Delete/:id'
                            },
                            'getContactTypes': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ContactType/GetAllContactTypes',
                                isArray: true
                            },
                            'getProfiles': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/UserProfile/GetAllProfiles',
                                isArray: true
                            },
                            'findProfile': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/UserProfile/Find/:id',
                            },
                            'getDocuments': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/User/GetAspNetUserDocuments',
                                isArray: true
                            },
                            'deleteDocument': {
                            method: 'POST',
                            url: applicationBaseUrl + '/api/User/DeleteAspNetUserDocument/:id'
                            },
                            'getImage': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/User/GetAspNetUserImage'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("UsersService", ["$resource", usersServiceFactory]);
}(window.appURL));