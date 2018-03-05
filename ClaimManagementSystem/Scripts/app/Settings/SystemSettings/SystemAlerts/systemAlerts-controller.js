(function (applicationBaseUrl) {
    "use strict";

    var SystemAlertsController = function ($scope, $routeParams, $timeout, systemAlertsService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.systemAlerts = {};

        $scope.createdDate = Date();
        $scope.ResetCreateSystemAlerts = function () {

            $scope.Initialize();
            $scope.systemAlertsForm.$setPristine();
            $scope.systemAlertsForm.$setUntouched();
            $scope.systemAlertsForm.$setValidity();

            var validator = $("#systemAlertsForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.validationMessage = "";
            $scope.isSuccess = null;
        };

      
        $scope.Initialize = function () {
            $scope.systemAlerts = new systemAlertsService();
        };
        
        $scope.AddSystemAlerts = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                systemAlertsService.addSystemAlerts(
                    null,
                    $scope.systemAlerts,
                    function (response) {
                        if (response.alertId > 0) {
                            $scope.Initialize();

                            $scope.validationMessage = "System alerts have been saved successfully.";
                            $scope.validationClass = "valid";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.validationMessage = response.errorMessage;
                            }
                            else {
                                $scope.validationMessage = "Unable to save system alerts. Please try again.";
                            }
                            $scope.validationClass = "invalid";
                            $scope.isSuccess = false;
                        }

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.validationMessage = "";
                                $scope.isSuccess = null;
                                $scope.validationClass = "";
                            }
                        }, 30000);
                    },
                    function (err) {
                        $scope.message = "Unable to proces your request. Please try again.";
                        $scope.isSuccess = false;

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.validationMessage = "";
                                $scope.isSuccess = null;
                                $scope.validationClass = "";
                            }
                        }, 30000);
                    });

            } else {
                $scope.message = "Please provide valid Tax details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.validationMessage = "";
                        $scope.isSuccess = null;
                        $scope.validationClass = "";
                    }
                }, 30000);
            }
        };

        $scope.DeleteSystemAlerts = function (alertId) {
            if (confirm('Are you sure to de-active this record?')) {
                systemAlertsService.deleteSystemAlerts({ id: alertId }, function (deleted) {
                    if (deleted) {
                        $scope.LoadSysemAlerts();
                        alert("The selected record has been successfully de-activate.");
                        $scope.systemAlertsGridOptions.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        alert("Unable to proces your request at this time. Please try again later.");
                    }
                });
            }
        }

        $scope.LoadSysemAlerts = function () {
            $scope.systemAlertsGridOptions = {
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
                            return systemAlertsService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Subject",
                    field: "title",
                    width: "15%"
                },
                {
                    title: "Message",
                    field: "description",
                    width: "35%"
                },
                {
                    title: "CreatedBy",
                    field: "alertBy",
                    width: "10%"
                },
                   {
                       title: "Created Date",
                       field: "createdOn",
                       width: "5%"
                   },
                   {
                       title: "Modified Date",
                       field: "lastModifiedOn",
                       width: "5%"
                   },
                   {
                       title: "Status",
                       field: "status",
                       width: "5%"
                   },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editsystemalerts/{{dataItem.alertId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteSystemAlerts(\"#=alertId#\")'></a>",
                    width: "5%"
                }]
            };
        }

        $scope.LoadSysemAlerts();
    };

    var EditSystemAlertsController = function ($scope, $routeParams, $timeout, systemAlertsService) {

        $scope.validationMessage = "";
        $scope.isSuccess = null;
        $scope.systemAlerts = {};

        $scope.Initialize = function () {
            $scope.systemAlerts = new systemAlertsService();
        };

        $scope.ResetCreateSystemAlerts = function () {

            $scope.Initialize();
            $scope.systemAlertsForm.$setPristine();
            $scope.systemAlertsForm.$setUntouched();
            $scope.systemAlertsForm.$setValidity();

            var validator = $("#systemAlertsEditForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.validationMessage = "";
            $scope.isSuccess = null;
        };

        $scope.LoadSysemAlert = function () {
            var systemAlerts = systemAlertsService.get({ id: $routeParams.alertId }, function (response) {
                $scope.systemAlerts = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.UpdateSystemAlerts = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                systemAlertsService.updateSystemAlerts(
                    null,
                    $scope.systemAlerts,
                    function (response) {
                        if (response.alertId > 0) {
                            $scope.Initialize();

                            $scope.validationMessage = "System alerts have been updated successfully.";
                            $scope.validationClass = "valid";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.validationMessage = response.errorMessage;
                            }
                            else {
                                $scope.validationMessage = "Unable to update system alerts. Please try again.";
                            }
                            $scope.validationClass = "invalid";
                            $scope.isSuccess = false;
                        }

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.validationMessage = "";
                                $scope.isSuccess = null;
                                $scope.validationClass = "";
                            }
                        }, 30000);
                    },
                    function (err) {
                        $scope.message = "Unable to proces your request. Please try again.";
                        $scope.isSuccess = false;

                        $timeout(function () {
                            if ($scope.isSuccess != null) {
                                $scope.validationMessage = "";
                                $scope.isSuccess = null;
                                $scope.validationClass = "";
                            }
                        }, 30000);
                    });

            } else {
                $scope.message = "Please provide valid system alert details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.validationMessage = "";
                        $scope.isSuccess = null;
                        $scope.validationClass = "";
                    }
                }, 30000);
            }
        };

    $scope.LoadSysemAlert();
};

angular.module("ClaimsManagementModule")
        .controller("SystemAlertsController", ["$scope", "$routeParams", "$timeout", "SystemAlertsService", SystemAlertsController])
        .controller("EditSystemAlertsController", ["$scope", "$routeParams", "$timeout", "SystemAlertsService", EditSystemAlertsController]);
}(window.appURL));