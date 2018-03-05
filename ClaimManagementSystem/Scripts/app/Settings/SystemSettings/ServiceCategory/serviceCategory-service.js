(function (applicationBaseUrl) {
    "use strict";
    var serviceCategoryServiceFactory = function ($resource) {
        return $resource(applicationBaseUrl + "/api/ServiceCategory/:id",
                        { id: "@serviceCategoryId" },
                        {
                            stripTrailingSlashes: true,
                            'query': {
                                method: 'GET',
                                url: applicationBaseUrl + '/api/ServiceCategory/GetAllServiceCategories',
                                isArray: true
                            }//,
                            //'getServiceCategory': {
                            //    method: 'GET',
                            //    url: applicationBaseUrl + '/api/ServiceCategory/Find/:id'
                            //},
                            //'findServiceCategory': {
                            //    method: 'POST',
                            //    url: applicationBaseUrl + '/api/ServiceCategory/Find/',
                            //    isArray: true
                            //},
                            //'addServiceCategory': {
                            //    method: 'POST',
                            //    url: applicationBaseUrl + '/api/ServiceCategory/Save'
                            //},
                            //'updateServiceCategory': {
                            //    method: 'POST',
                            //    url: applicationBaseUrl + '/api/ServiceCategory/Update'
                            //},
                            //'deleteServiceCategory': {
                            //    method: 'DELETE',
                            //    url: applicationBaseUrl + '/api/ServiceCategory/Delete/:id'
                            //}
                        });
    };

    angular.module("ClaimsManagementModule")
            .factory("ServiceCategoryService", ["$resource", serviceCategoryServiceFactory]);
}(window.appURL));