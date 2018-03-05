(function (applicationBaseUrl) {
    "use strict";
    var typeOfLossServiceFactory = function ($resource) {
        debugger;
        return $resource(applicationBaseUrl + "/api/TypeOfLoss/:id",
                        { id: "@lossTypeId" },
                        {
                            
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TypeOfLoss/GetAllTypeOfLoss',
                                isArray: true
                            },
                            'addLossType': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TypeOfLoss/Save'
                            },
                            'getLossType': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/TypeOfLoss/Find/:id'
                            },
                            'findLossType': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TypeOfLoss/Find/',
                                isArray: true
                            },
                         
                            'updateLossType': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TypeOfLoss/Update'
                            },
                            'deleteLossType': {
                                method: 'POST',
                                url: applicationBaseUrl + '/api/TypeOfLoss/Delete/:id'
                            }
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("TypeOfLossService", ["$resource", typeOfLossServiceFactory]);
}(window.appURL));