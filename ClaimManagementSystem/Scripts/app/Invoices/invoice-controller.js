(function (applicationBaseUrl) {
    "use strict";
    var InvoiceController = function ($http, $rootScope, $scope, $routeParams, $filter, $timeout, claimService, companiesService, usersService, serviceItemService, timelogUnitService, timeLogService, invoiceService) {
        $scope.invoice = {};
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};
        $scope.timelogs = {};
        $scope.timelogs.claimId = 0;
        $scope.timelogs.timeLogId = 0;
        $scope.timelogs.isBilled = false;

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

        $scope.ValidClaimId = function () {
            if ($routeParams.claimId != undefined)
            return $routeParams.claimId.split('_')[0];
        };

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

        $scope.claimOptions = {
            placeholder: "Select Claim Number",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return claimService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            }),
            select: onClaimSelectionChange,
            dataTextField: 'claimNo',
            dataValueField: "claimId"
        };

        $scope.InitializeInvoiceDetails = function (claimData) {
            $scope.invoice.invoiceNumber = claimData.fileNo;
            $scope.invoice.claimantId = claimData.claimantId;
            $scope.invoice.claimNumber = claimData.claimNo;
            $scope.invoice.claimantName = claimData.claimantName;
            $scope.invoice.policyNumber = claimData.policyNo;
            $scope.invoice.lossType = claimData.lossType;
            $scope.invoice.companyId = claimData.companyId;
            $scope.invoice.companyName = claimData.companyName;
        };

        function onClaimSelectionChange(e) {
            var claimData = this.dataItem(e.item.index());
           
            if (claimData) {
                $scope.claim = claimData;
                $('#generateInvoicesGrid').data('kendoGrid').dataSource.read();
                $('#generateInvoicesGrid').data('kendoGrid').refresh();
                $scope.InitializeInvoiceDetails(claimData);
                $scope.invoiceTimelogGrid.dataSource.transport.read($scope.gridOptions);
                //$scope.GetTimeLogsForClaim($scope.claim.claimId);
                
            }
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

        $scope.GetTimeLogsForClaim = function (claimId) {

            $scope.invoiceTimelogGridOptions = {
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
                            return timeLogService.query({ claimId: $scope.claim.claimId ? $scope.claim.claimId : 0 }, function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true, style: "text-align: center" },
                    attributes: { style: "text-align: center" },
                    title: "Select",
                    field: "timeLogId",
                    template: "<input type='checkbox' name='ServiceItem' class='checkbox' />",
                    width: "5%",
                },
                {
                    title: "Service Name",
                    field: "serviceItemName",
                    width: "15%"
                },
                {
                    title: "Service Rate",
                    field: "serviceRate",
                    width: "15%"
                },
                {
                    title: "Quantity",
                    field: "quantity",
                    width: "15%"
                },
                {
                    title: "Unit",
                    field: "unit",
                    width: "18%"
                },
                {
                    title: "Comments",
                    field: "comments",
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
                    title: "Total",
                    headerAttributes: { style: "text-align: center" },
                    field: "totalAmount",
                    template: "<input name='serviceItemTotal' type='text' class='k-textbox text-right' disabled='true' required value='{{dataItem.totalAmount}}' />",
                    width: "14%"
                }, {
                    headerAttributes: { "ng-non-bindable": true, style: "text-align: center" },
                    attributes: { style: "text-align: center" },
                    title: "Is Billed",
                    field: "isBilled",
                    template: "<input type='checkbox' name='ServiceItemCheckBox' class='checkbox'  />",
                    width: "5%"
                },


                ],
                dataBound: function () {
                    var dataView = this.dataSource.view();
                    for (var i = 0; i < dataView.length; i++) {
                        var uid = dataView[i].uid;
                        if (dataView[i].isBilled == true) {
                            $("#invoiceTimelogGrid table tbody").find("tr[data-uid=" + uid + "]").find("input[name*='ServiceItemCheckBox']")[0].checked = true;
                        }
                        $("#invoiceTimelogGrid table tbody").find("tr[data-uid=" + uid + "]").find("input[name*='ServiceItemCheckBox']").attr('disabled', 'disabled').val(0);
                    }
                    $(".checkbox").bind("change", function (e) {
                        $(e.target).closest("tr").toggleClass("k-state-selected");

                        var row = $(e.target).closest("tr");
                        var grid = $("#invoiceTimelogGrid").data("kendoGrid"),
                        dataItem = grid.dataItem(row);

                        if ($(e.target).prop('checked') == true) {
                            var serviceTotalAmount = Number(dataItem.quantity) * Number(dataItem.serviceRate);
                            $(e.target).closest("tr").find("input[name*='serviceItemTotal']").removeAttr('disabled').val(serviceTotalAmount);
                            dataItem.totalAmount = serviceTotalAmount;
                        }
                        else {
                            $(e.target).closest("tr").find("input[name*='serviceItemTotal']").attr('disabled', 'disabled').val(0);
                        }

                        $scope.SelectRow(e.target, dataItem);
                    });
                }
            };
        }

        $scope.LoadGetGeneratedInvoices = function () {
            $rootScope.showProgressLoader = true;
            $scope.generateInvoicesGridOptions = {
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
                            if (!($routeParams.claimId != undefined && $routeParams.claimId.split('_')[0])) {
                                return invoiceService.getGeneratredInvoices({ id: $scope.claim.claimId ? $scope.claim.claimId : 0, claimNo: $scope.claim.claimNo ? $scope.claim.claimNo : "" }, function (response) {
                                    $scope.generateInvoicesGridOptions = options;
                                    options.success(response);
                                });
                            }
                            else
                            {
                                return invoiceService.getGeneratredInvoices({ id: $routeParams.claimId.split('_')[0] ? $routeParams.claimId.split('_')[0] : 0, claimNo: $routeParams.claimId.split('_')[1] ? $routeParams.claimId.split('_')[1] : "" }, function (response) {
                                    $scope.generateInvoicesGridOptions = options;
                                        options.success(response);
                                });
                            }
                        }
                    }
                }),
                columns: [{
                    title: "PDF File Link",
                    field: "fileName",
                    width: "50%",
                    template: "<a href='{{dataItem.fileLocation}}' target='_blank' title='View Claimant Details' class='k-link'>{{dataItem.fileName}}</a>"
                },
                {
                    title: "Created By",
                    field: "createdBy",
                    width: "15%"
                },
                {
                    title: "Created Date",
                    field: "createdOn",
                    width: "15%"
                }
                
                ]               
            };
        }

        $scope.GetAllInvoices = function () {
            $scope.invoiceGridOptions = {
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
                            if (!$routeParams.claimId) {
                                return invoiceService.query(function (response) {
                                    $scope.gridOptions = options;
                                    options.success(response);
                                });
                            }
                            else {
                                return invoiceService.getInvoicesByClaim({ claimId: $routeParams.claimId.split('_')[0] }, function (response) {
                                    $scope.gridOptions = options;
                                    var claim = claimService.getClaim({ id: $routeParams.claimId.split('_')[0] }, function (response) {
                                        $scope.claim = response;
                                    });
                                    options.success(response);
                                });
                            }
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Invoice #",
                    field: "invoiceNumber",
                    template: "<a href='\\#/viewInvoice/{{dataItem.invoiceId}}' title='View Invoice Details' class='k-link'>{{dataItem.invoiceNumber}}</a>",
                    width: "8%",
                },
                {
                    title: "Claim #",
                    field: "claimNumber",
                    template: "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Details' class='k-link'>{{dataItem.claimNumber}}</a>",
                    width: "8%"
                },
                {
                    title: "Company",
                    field: "companyName",
                    filterable: {
                        cell: {
                            operator: "contains"
                        }
                    },
                    template: "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Company Details' class='k-link'>{{dataItem.companyName}}</a>",
                    width: "14%"
                },
                {
                    title: "Claimant",
                    field: "claimantName",
                    filterable: {
                        cell: {
                            operator: "contains"
                        }
                    },
                    template: "<a href='\\#/viewContact/{{dataItem.claimantId}}' title='View Claimant Details' class='k-link'>{{dataItem.claimantName}}</a>",
                    width: "14%"
                },
                {
                    title: "Adjuster",
                    field: "adjusterName",
                    filterable: {
                        cell: {
                            operator: "contains"
                        }
                    },
                    width: "14%"
                },
                {
                    title: "Invoice Date",
                    field: "invoiceDate",
                    width: "10%"
                },
                {
                    title: "Due Date",
                    field: "dueDate",
                    width: "10%"
                },
                {
                    title: "Total Amount",
                    field: "invoiceTotal",
                    format: "{0:c}",
                    width: "10%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editInvoice/{{dataItem.invoiceId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Print' class='glyphicon glyphicon-print grey' ng-click='PrintInvoice(#=invoiceId#)'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Download' class='glyphicon glyphicon-download-alt grey' ng-click='DownloadInvoice(#=invoiceId#)'></a>",
                    width: "12%"
                }]
            };
        };

        $scope.SelectRow = function (elem, gridDataItem) {
            if (!$.isEmptyObject($scope.invoice)) {
                if (elem.checked) {
                    if ($scope.invoice.timeLogs.length == 0) {
                        $scope.invoice.timeLogs = [];
                    }

                    var selectedTimelog = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId == gridDataItem.timeLogId; });
                    if (selectedTimelog) {
                        if (selectedTimelog.length <= 0) {
                            $scope.invoice.timeLogs.push(gridDataItem);
                        }
                    }
                }
                else {
                    $scope.invoice.timeLogs = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId != gridDataItem.timeLogId; });
                }

                var totalInvoiceAmount = 0.0;
                angular.forEach($scope.invoice.timeLogs, function (timelog) {
                    totalInvoiceAmount += timelog.totalAmount;
                });

                $scope.invoice.invoiceTotal = totalInvoiceAmount;
                $scope.$apply();
            }
        };

        $scope.InitializeInvoice = function () {
            $scope.invoice = new invoiceService();
            $scope.invoice.timeLogs = [];
            $scope.invoice.invoiceTotal = 0.0;

            var invoiceDate = new Date();
            $scope.invoice.invoiceDate = $filter("date")(invoiceDate, "MM/dd/yyyy");
            $scope.invoice.dueDate = $filter("date")(invoiceDate.setDate(invoiceDate.getDate() + 14), "MM/dd/yyyy");

            if ($routeParams.claimId != undefined && $routeParams.claimId.split('_')[0]) {
                var claim = claimService.getClaim({ id: $routeParams.claimId.split('_')[0] }, function (response) {
                    $scope.claim = response;
                    $scope.InitializeInvoiceDetails(response);
                    $scope.GetTimeLogsForClaim($scope.claim.claimId);
                });
            }
            else {
                $scope.claim = {};
                $scope.GetTimeLogsForClaim(0);
            }
        };

        $scope.ResetAddInvoice = function () {
            $scope.InitializeInvoice();

            $scope.invoiceForm.$setPristine();
            $scope.invoiceForm.$setUntouched();
            $scope.invoiceForm.$setValidity();

            var validator = $("#invoiceForm").kendoValidator().data("kendoValidator");
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


     
        $scope.GenerateInvoice = function (event) {
            //$rootScope.showProgressLoader = true;
            $rootScope.showProgressLoader = true;
            applicationBaseUrl = window.parent.location.href.split('Home')[0];
            $http.post(applicationBaseUrl + "Home/GenerateIncoicePdf", { ClaimId: $scope.claim.claimId, InvoiceId: $scope.invoice.invoiceId,TimelogId:$scope.timelogs.timelogId }).success(function (data) {
                if (data == "True") {
                    $('#generateInvoicesGrid').data('kendoGrid').dataSource.read();
                    $('#generateInvoicesGrid').data('kendoGrid').refresh();
                            $scope.message = "PDF has been generate successfully.";
                            $scope.isSuccess = true;
                            $rootScope.showProgressLoader = false;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to generate invoice pdf. Please try again.";
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

        };

        $scope.AddInvoice = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $scope.invoice.claimId = $scope.claim.claimId;
                $scope.invoice.adjusterId = $scope.claim.adjusterId;
                $scope.invoice.claimNumber = $scope.claim.claimNo;
                invoiceService.addInvoice(
                    null,
                    $scope.invoice,
                    function (response) {
                        if (!response.hasError && response.invoiceId > 0) {
                            $scope.InitializeInvoice();
                            $scope.timelogs.claimId = response.claimId;
                            $scope.timelogs.timelogId = response.timeLogs[0].timeLogId;
                            $scope.timelogs.isBilled = true;
                            $scope.invoice.invoiceId = response.invoiceId;
                            $scope.claim.claimNo = response.claimNumber;
                            timeLogService.updateBilledTimeLog(
                            null,
                            $scope.timelogs,
                            function (response) {
                                if (!response.hasError && response.timeLogId > 0) {
                                    //$scope.InitializeInvoice();
                                    $scope.claim.claimId = response.claimId;
                                    $scope.timelogs.timelogId = response.timeLogId;
                                    $("#GenerateInvoice").show();
                                    $scope.message = "An invoice has been saved successfully.";
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

                            // ENd


                           
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save invoice details. Please try again.";
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
                $scope.message = "Please provide valid invoice details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteInvoice = function (invoiceId) { // Delete an invoice
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this invoice?')) {
                invoiceService.deleteInvoice({ id: invoiceId }, { id: invoiceId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected invoice has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.invoiceGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected invoice. Please try again.";
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

        $scope.InitializeInvoice();
        $scope.GetAllInvoices();
        $scope.LoadGetGeneratedInvoices();
    };

    var ViewEditInvoiceController = function ($http,$rootScope, $scope, $routeParams, $filter, $timeout, claimService, companiesService, usersService, serviceItemService, timelogUnitService, timeLogService, invoiceService) {
        $scope.invoice = {};
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

        $scope.GetTimeLogsForClaim = function () {

            $scope.invoiceTimelogGridOptions = {
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
                            return timeLogService.query({ claimId: $scope.invoice.claimId ? $scope.invoice.claimId : 0 }, function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true, style: "text-align: center" },
                    attributes: { style: "text-align: center" },
                    title: "Select",
                    field: "timeLogId",
                    template: "<input type='checkbox' name='ServiceItem' class='checkbox' />",
                    width: "5%",
                },
                {
                    title: "Service Name",
                    field: "serviceItemName",
                    width: "15%"
                },
                {
                    title: "Service Rate",
                    field: "serviceRate",
                    width: "15%"
                },
                {
                    title: "Quantity",
                    field: "quantity",
                    width: "15%"
                },
                {
                    title: "Unit",
                    field: "unit",
                    width: "18%"
                },
                {
                    title: "Comments",
                    field: "comments",
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
                    title: "Total",
                    headerAttributes: { style: "text-align: center" },
                    field: "totalAmount",
                    template: "<input name='serviceItemTotal' type='text' class='k-textbox text-right' disabled='true' required value='{{dataItem.totalAmount}}' />",
                    width: "14%"
                }],
                dataBound: function (e) {
                    var view = this.dataSource.view();
                    for (var i = 0; i < view.length; i++) {
                        var row = this.tbody.find("tr[data-uid='" + view[i].uid + "']");
                        var checkbox = $(row).find("input[type*='checkbox']"),
                            grid = $("#invoiceTimelogGrid").data("kendoGrid"),
                            dataItem = grid.dataItem(row);

                        if (checkbox) {
                            if (checkbox.length > 0) {
                                $(checkbox).prop('checked', $scope.IsInvoiceRaised(checkbox, dataItem, dataItem.timeLogId));
                            }
                        }
                    }
                    $scope.SelectRow(event.target, dataItem);

                    $(".checkbox").bind("change", function (event) {
                        $(event.target).closest("tr").toggleClass("k-state-selected");

                        var row = $(event.target).closest("tr");
                        var grid = $("#invoiceTimelogGrid").data("kendoGrid"),
                        dataItem = grid.dataItem(row);

                        if ($(event.target).prop('checked') == true) {
                            var invoiceTimeLog = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId == dataItem.timeLogId; });
                            if (invoiceTimeLog) {
                                if (invoiceTimeLog.length > 0) {
                                    dataItem.totalAmount = invoiceTimeLog[0].totalAmount;
                                }
                                else {
                                    var serviceTotalAmount = Number(dataItem.quantity) * Number(dataItem.serviceRate);
                                    dataItem.totalAmount = serviceTotalAmount;
                                }

                                $(event.target).closest("tr").find("input[name*='serviceItemTotal']").removeAttr('disabled');//.val(serviceTotalAmount);
                            }
                        }
                        else {
                            $(event.target).closest("tr").find("input[name*='serviceItemTotal']").attr('disabled', 'disabled').val(0);
                        }

                        $scope.SelectRow(event.target, dataItem);
                    });
                }
            };
        }

        $scope.GetTimeLogsForInvoice = function () {

            $scope.invoiceRaisedTimelogGridOptions = {
                selectable: false,
                scrollable: false,
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                dataSource: $scope.invoice.timeLogs,
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Service Name",
                    field: "serviceItemName",
                    width: "15%"
                },
                {
                    title: "Service Rate",
                    field: "serviceRate",
                    width: "15%"
                },
                {
                    title: "Quantity",
                    field: "quantity",
                    width: "15%"
                },
                {
                    title: "Unit",
                    field: "unit",
                    width: "18%"
                },
                {
                    title: "Comments",
                    field: "comments",
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
                    headerAttributes: { style: "text-align: center" },
                    attribute: { class: "text-center" },
                    title: "Total",
                    field: "totalAmount",
                    width: "14%"
                }]
            };
        }

        $scope.IsInvoiceRaised = function (elem, dataItem, timelogId) {
            var invoiceTimeLog = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId == timelogId; });

            if (invoiceTimeLog) {
                if (invoiceTimeLog.length > 0) {
                    $(elem).closest("tr").toggleClass("k-state-selected");
                    $(elem).closest("tr").find("input[name*='serviceItemTotal']").removeAttr('disabled');
                    dataItem.totalAmount = invoiceTimeLog[0].totalAmount;

                    return true;
                }
            }

            return false;
        }

        $scope.SelectRow = function (elem, gridDataItem) {
            if (!$.isEmptyObject($scope.invoice)) {
                if (elem.checked) {
                    if ($scope.invoice.timeLogs.length == 0) {
                        $scope.invoice.timeLogs = [];
                    }

                    var selectedTimelog = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId == gridDataItem.timeLogId; });
                    if (selectedTimelog) {
                        if (selectedTimelog.length <= 0) {
                            $scope.invoice.timeLogs.push(gridDataItem);
                        }
                    }
                }
                else {
                    $scope.invoice.timeLogs = $filter('filter')($scope.invoice.timeLogs, function (timelog) { return timelog.timeLogId != gridDataItem.timeLogId; });
                }

                var totalInvoiceAmount = 0.0;
                angular.forEach($scope.invoice.timeLogs, function (timelog) {
                    totalInvoiceAmount += timelog.totalAmount;
                });

                $scope.invoice.invoiceTotal = totalInvoiceAmount;
                $scope.$apply();
            }
        };

        $scope.LoadInvoice = function () {
            var invoice = invoiceService.getInvoice({ invoiceId: $routeParams.invoiceId }, function (response) {
                $scope.invoice = response;
                $scope.GetTimeLogsForClaim();
                $scope.GetTimeLogsForInvoice();
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetInvoice = function () {
            $scope.LoadInvoice();

            $scope.editInvoiceForm.$setPristine();
            $scope.editInvoiceForm.$setUntouched();
            $scope.editInvoiceForm.$setValidity();

            var validator = $("#editInvoiceForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateInvoice = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {

                invoiceService.updateInvoice(
                    null,
                    $scope.invoice,
                    function (response) {
                        if (!response.hasError && response.invoiceId > 0) {
                            $scope.message = "Invoice has been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save invoice. Please try again.";
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
                $scope.message = "Please provide valid invoice details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadInvoice();
    };

    angular.module("ClaimsManagementModule")
            .controller("InvoiceController", ["$http", "$rootScope", "$scope", "$routeParams", "$filter", "$timeout", "ClaimService", "CompaniesService", "UsersService", "ServiceItemService", "TimelogUnitService", "TimeLogService", "InvoiceService", InvoiceController])
            .controller("ViewEditInvoiceController", ["$http", "$rootScope", "$scope", "$routeParams", "$filter", "$timeout", "ClaimService", "CompaniesService", "UsersService", "ServiceItemService", "TimelogUnitService", "TimeLogService", "InvoiceService", ViewEditInvoiceController]);

}());