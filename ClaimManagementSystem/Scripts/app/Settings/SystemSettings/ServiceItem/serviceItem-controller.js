(function (applicationBaseUrl) {
    "use strict";
    var ServiceItemController = function ($scope, $routeParams, $timeout, serviceCategoryService, serviceItemService) {
        $scope.message = "";
        $scope.isSuccess = null;
        debugger;
        $scope.serviceItem = {};

        //var users = usersService.query(function (response) {
        //    $scope.Users = new kendo.data.DataSource({
        //        data: response
        //    });
        //});

        var serviceCategories = serviceCategoryService.query(function (response) {
            $scope.ServiceCategoryList = new kendo.data.DataSource({
                data: response
            });
        });

        
        $scope.GetAllServiceItems = function () {
            $scope.serviceItemGridOptions = {
                selectable: false,
                scrollable: false,
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                dataSource: new kendo.data.DataSource({
                    pageSize: 10,
                    transport: {
                        read: function (options) {
                            return serviceItemService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Name",
                    field: "serviceItemName",
                    width: "18%",
                    template: "<a href='\\#/viewServiceItem/{{dataItem.serviceItemId}}' title='View Details' class='k-link'>{{dataItem.serviceItemName}}</a>"
                },
                {
                    title: "Category",
                    field: "serviceCategoryName",
                    width: "12%"
                },
                {
                    title: "Is Hourly",
                    field: "isHourBased",
                    width: "12%",
                    template: "#if(isHourBased == true){#YES#}else{#NO#}#"
                },
                {
                    title: "Default Quantity",
                    field: "defaultQuantity",
                    width: "16%"
                },
                {
                    title: "Default Fee",
                    field: "defaultFee",
                    width: "15%"
                },
                {
                    title: "Minimum Fee",
                    field: "minimumFee",
                    width: "15%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editServiceItem/{{dataItem.serviceItemId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteServiceItem(#=serviceItemId#)'></a>",
                    width: "12%"
                }]
            };
            //});
        };

        $scope.InitializeServiceItem = function () {
            $scope.serviceItem = new serviceItemService();
        };

        $scope.ResetAddServiceItem = function () {
            $scope.InitializeServiceItem();

            $scope.serviceItemForm.$setPristine();
            $scope.serviceItemForm.$setUntouched();
            $scope.serviceItemForm.$setValidity();

            var validator = $("#serviceItemForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.message = "";
            $scope.isSuccess = null;
        };

        $scope.AddServiceItem = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                serviceItemService.addServiceItem(
                    null,
                    $scope.serviceItem,
                    function (response) {
                        if (!response.hasError && response.serviceItemId > 0) {
                            $scope.InitializeServiceItem();

                            $scope.message = "Service item has been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save service item. Please try again.";
                            }

                            $scope.isSuccess = false;
                        }

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.message = "";
                                $scope.isSuccess = null;
                            }
                        }, 30000);
                    },
                    function (err) {
                        $scope.message = "Unable to proces your request. Please try again.";
                        $scope.isSuccess = false;

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.message = "";
                                $scope.isSuccess = null;
                            }
                        }, 30000);
                    });

            } else {
                $scope.message = "Please provide valid service item details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteServiceItem = function (serviceItemId) { // Delete a contact
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this service item?')) {
                serviceItemService.deleteServiceItem({ id: serviceItemId }, { id: serviceItemId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected service item has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.serviceItemGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected service item. Please try again.";
                        }

                        $scope.isSuccess = false;
                    }

                    $timeout(function () {
                        if ($scope.isSuccess != null) {
                            $scope.message = "";
                            $scope.isSuccess = null;
                        }
                    }, 30000);
                },
                function (err) {
                    $scope.message = "Unable to proces your request. Please try again.";
                    $scope.isSuccess = false;

                    $timeout(function () {
                        if ($scope.isSuccess != null) {
                            $scope.message = "";
                            $scope.isSuccess = null;
                        }
                    }, 30000);
                });
            }
        };

        $scope.GetAllServiceItems();
    };

    var ViewEditServiceItemController = function ($scope, $routeParams, $timeout, serviceCategoryService, serviceItemService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.serviceItem = {};

        //var users = usersService.query(function (response) {
        //    $scope.Users = new kendo.data.DataSource({
        //        data: response
        //    });
        //});


        var serviceCategories = serviceCategoryService.query(function (response) {
            $scope.ServiceCategoryList = new kendo.data.DataSource({
                data: response
            });
        });

        
        $scope.LoadServiceItem = function () {
            var serviceItem = serviceItemService.getServiceItem({ id: $routeParams.serviceItemId }, function (response) {
                $scope.serviceItem = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetServiceItem = function () {
            $scope.LoadServiceItem();

            $scope.editServiceItemForm.$setPristine();
            $scope.editServiceItemForm.$setUntouched();
            $scope.editServiceItemForm.$setValidity();

            var validator = $("#editServiceItemForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.message = "";
            $scope.isSuccess = null;
        };

        $scope.UpdateServiceItem = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                serviceItemService.updateServiceItem(
                    null,
                    $scope.serviceItem,
                    function (response) {
                        if (!response.hasError && response.serviceItemId > 0) {
                            $scope.message = "Service item has been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save service item. Please try again.";
                            }

                            $scope.isSuccess = false;
                        }

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.message = "";
                                $scope.isSuccess = null;
                            }
                        }, 30000);
                    },
                    function (err) {
                        $scope.message = "Unable to proces your request. Please try again.";
                        $scope.isSuccess = false;

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.message = "";
                                $scope.isSuccess = null;
                            }
                        }, 30000);
                    });

            }
            else {
                $scope.message = "Please provide valid service item details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadServiceItem();
    };

    angular.module("ClaimsManagementModule")
            .controller("ServiceItemController", ["$scope", "$routeParams", "$timeout", "ServiceCategoryService", "ServiceItemService", ServiceItemController])
            .controller("ViewEditServiceItemController", ["$scope", "$routeParams", "$timeout", "ServiceCategoryService", "ServiceItemService", ViewEditServiceItemController]);
}(window.appURL));