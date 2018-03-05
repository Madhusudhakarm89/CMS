(function () {
    "use strict";
    var TimeLogController = function ($rootSscope, $scope, $routeParams, $timeout, claimService, companiesService, usersService, serviceItemService, timelogUnitService, timeLogService) {
        $scope.timelog = {};
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};

        var serviceItems = serviceItemService.query(function (response) {
            $scope.ServiceItemOptions = new kendo.data.DataSource({
                data: response
            });
        });

        var timelogUnits = timelogUnitService.query(function (response) {
            $scope.TimelogUnitOptions = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.AdjusterOptions = {
            dataTextField: 'fullName',
            dataValueField: "userId",
            placeholder: "Select Adjuster",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return usersService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        $scope.CompanyOptions = {
            placeholder: "Select Company",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return companiesService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            }),
            dataTextField: 'companyName',
            dataValueField: "companyId"
        };

        $scope.GetAllTimeLogs = function () {
            $scope.timelogGridOptions = {
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
                            return timeLogService.query({ claimId: $routeParams.claimId }, function (response) {
                                $scope.gridOptions = options;
                                var claim = claimService.getClaim({ id: $routeParams.claimId }, function (response) {
                                    $scope.claim = response;
                                });
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Service Item",
                    field: "serviceItemName",
                    template: "<a href='\\#/viewServiceItem/{{dataItem.serviceItemId}}' title='View Service Item' class='k-link'>{{dataItem.serviceItemName}}</a>",
                    width: "15%",
                },
                {
                    title: "Quantity",
                    field: "quantity",
                    width: "15%"
                },
                {
                    title: "Task Date",
                    field: "taskDate",
                    width: "18%"
                },
                {
                    title: "Logged Date",
                    field: "loggedOn",
                    width: "18%"
                },
                {
                    title: "Adjuster",
                    field: "Name",
                    filterable: {
                        cell: {
                            operator: "contains"
                        }
                    },
                    template: "<a href='\\#/viewProfile/{{dataItem.adjusterId}}' title='View Adjuster Details' class='k-link'>{{dataItem.adjusterName}}</a>",
                    width: "20%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editTimelog/{{dataItem.timeLogId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteTimeLog(#=timeLogId#)'></a>",
                    width: "14%"
                }]
            };
        };

        $scope.InitializeTimeLog = function () {
            $scope.timelog = new timeLogService();
            $scope.timelog.taskDate = new Date();

            var claim = claimService.getClaim({ id: $routeParams.claimId }, function (response) {
                $scope.claim = response;
            });
        };

        $scope.ResetAddTimelog = function () {
            $scope.InitializeTimeLog();

            $scope.timelogForm.$setPristine();
            $scope.timelogForm.$setUntouched();
            $scope.timelogForm.$setValidity();

            var validator = $("#timelogForm").kendoValidator().data("kendoValidator");
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

        $scope.AddTimelog = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $scope.timelog.claimId = $scope.claim.claimId;
                $scope.timelog.adjuster = {
                    userId: $scope.claim.adjusterId
                };

                timeLogService.addTimeLog(
                    null,
                    $scope.timelog,
                    function (response) {
                        if (!response.hasError && response.timeLogId > 0) {
                            $scope.InitializeTimeLog();

                            $scope.message = "Timelog details have been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save timelog details. Please try again.";
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
                $scope.message = "Please provide valid timelog details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteTimeLog = function (timelogId) { // Delete a timelog
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this timelog entry?')) {
                timeLogService.deleteTimeLog({ id: timelogId }, { id: timelogId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected timelog has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.timelogGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected timelog. Please try again.";
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

        $scope.InitializeTimeLog();
        $scope.GetAllTimeLogs();
    };

    var ViewEditTimeLogController = function ($rootSscope, $scope, $routeParams, $timeout, claimService, companiesService, usersService, serviceItemService, timelogUnitService, timeLogService) {
        $scope.timelog = {};
        $scope.message = "";
        $scope.isSuccess = null;

        var serviceItems = serviceItemService.query(function (response) {
            $scope.ServiceItemOptions = new kendo.data.DataSource({
                data: response
            });
        });

        var timelogUnits = timelogUnitService.query(function (response) {
            $scope.TimelogUnitOptions = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.AdjusterOptions = {
            dataTextField: 'fullName',
            dataValueField: "userId",
            placeholder: "Select Adjuster",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return usersService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        $scope.CompanyOptions = {
            placeholder: "Select Company",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return companiesService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            }),
            dataTextField: 'companyName',
            dataValueField: "companyId"
        };

        $scope.userOptions = {
            dataTextField: 'fullName',
            dataValueField: "userId",
            placeholder: "Select Assignee",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return usersService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        $scope.LoadTimeLog = function () {
            var timelog = timeLogService.getTimeLog({ timeLogId: $routeParams.timelogId }, function (response) {
                $scope.timelog = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetTimeLog = function () {
            $scope.LoadTimeLog();

            $scope.editTimelogForm.$setPristine();
            $scope.editTimelogForm.$setUntouched();
            $scope.editTimelogForm.$setValidity();

            var validator = $("#editTimelogForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateTimeLog = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {

                timeLogService.updateTimeLog(
                    null,
                    $scope.timelog,
                    function (response) {
                        if (!response.hasError && response.timeLogId > 0) {
                            $scope.message = "Timelog has been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save timelog. Please try again.";
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
                $scope.message = "Please provide valid timelog details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadTimeLog();
    };

    angular.module("ClaimsManagementModule")
            .controller("TimeLogController", ["$rootScope", "$scope", "$routeParams", "$timeout", "ClaimService", "CompaniesService", "UsersService", "ServiceItemService", "TimelogUnitService", "TimeLogService", TimeLogController])
            .controller("ViewEditTimeLogController", ["$rootScope", "$scope", "$routeParams", "$timeout", "ClaimService", "CompaniesService", "UsersService", "ServiceItemService", "TimelogUnitService", "TimeLogService", ViewEditTimeLogController]);
}());