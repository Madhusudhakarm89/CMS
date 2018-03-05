(function (applicationBaseUrl) {
    "use strict";
    var TaxSettingController = function ($scope, $routeParams, $timeout, countriesService, statesService, taxSettingService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.taxSetting = {};

        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });

        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.GetAllTaxSettings = function () {
            $scope.taxSettingGridOptions = {
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
                            return taxSettingService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Country",
                    field: "countryName",
                    width: "20%"
                },
                {
                    title: "State",
                    field: "stateName",
                    width: "15%"
                },
                {
                    title: "Tax Rate",
                    field: "taxRate",
                    width: "20%",
                    template: ""
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editTaxSetting/{{dataItem.id}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteTaxSetting(#=id#)'></a>",
                    width: "12%"
                }]
            };
        };

        $scope.InitializeTaxSetting = function () {
            $scope.taxSetting = new taxSettingService();
        };

        $scope.ResetAddTaxSetting = function () {
            $scope.InitializeTaxSetting();

            $scope.taxSettingForm.$setPristine();
            $scope.taxSettingForm.$setUntouched();
            $scope.taxSettingForm.$setValidity();

            var validator = $("#taxSettingForm").kendoValidator().data("kendoValidator");
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

        $scope.AddTaxSetting = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                taxSettingService.addTaxSetting(
                    null,
                    $scope.taxSetting,
                    function (response) {
                        if (!response.hasError && response.id > 0) {
                            $scope.InitializeTaxSetting();

                            $scope.message = "Tax details have been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save tax details. Please try again.";
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
                $scope.message = "Please provide valid Tax details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteTaxSetting = function (id) { // Delete a taxt
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this tax setting?')) {
                taxSettingService.deleteTaxSetting({ id: id }, { id: id }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected tax setting has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.taxSettingGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected tax setting. Please try again.";
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

        $scope.GetAllTaxSettings();
    };

    var ViewEditTaxSettingController = function ($scope, $routeParams, $timeout,  countriesService, statesService,  taxSettingService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.taxSetting = {};

             
        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });

        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.LoadTaxSetting = function () {
            var taxSetting = taxSettingService.getTaxSetting({ id: $routeParams.id }, function (response) {
                $scope.taxSetting = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetTaxSetting = function () {
            $scope.LoadTaxSetting();

            $scope.editTaxSettingForm.$setPristine();
            $scope.editTaxSettingForm.$setUntouched();
            $scope.editTaxSettingForm.$setValidity();

            var validator = $("#editTaxSettingForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateTaxSetting = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                taxSettingService.updateTaxSetting(
                    null,
                    $scope.taxSetting,
                    function (response) {
                        if (!response.hasError && response.id > 0) {
                            $scope.message = "Tax details have been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save tax details. Please try again.";
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
                $scope.message = "Please provide valid tax details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadTaxSetting();
    };

    angular.module("ClaimsManagementModule")
            .controller("TaxSettingController", ["$scope", "$routeParams", "$timeout", "CountriesService", "StatesService", "TaxSettingService", TaxSettingController])
            .controller("ViewEditTaxSettingController", ["$scope", "$routeParams", "$timeout", "CountriesService", "StatesService", "TaxSettingService", ViewEditTaxSettingController]);
}(window.appURL));