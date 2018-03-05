
(function () {
    "use strict";
    var HomeController = function ($scope, $routeParams, $timeout, claimNotesService, claimService, systemAlertsService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};
        $scope.claimNote = {};

        $scope.GetAllClaimNotesAssignedToMe = function () {
            $scope.tasksAssignedToMeGridOptions = {
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
                            return claimNotesService.GetClaimNotesAssignedToMe(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [
                    {
                        headerAttributes: { "ng-non-bindable": true },
                        title: "Task Name",
                        field: "title",
                        template:
                            "<a href='\\#/viewClaimNote/{{dataItem.noteId}}' title='View Claim Note / Task Detail' class='k-link'>{{dataItem.title}}</a>",
                        width: "15%"
                    },
                    {
                        title: "Created By",
                        field: "createdBy",
                        width: "15%"
                    },
                    {
                        title: "Created Date",
                        field: "createdDate",
                        width: "10%"
                    },
                    {
                        title: "Due Date",
                        field: "dueDate",
                        width: "10%"
                    }
                ],
                dataBound: function () {
                    var dataView = this.dataSource.view();
                    for (var i = 0; i < dataView.length; i++) {
                        if (dataView[i].isDuteDatetask == "1") {
                            var uid = dataView[i].uid;
                            $("#tasksAssignedToMeGrid table tbody").find("tr[data-uid=" + uid + "]").addClass("dueDateCustomClass");
                        }
                    }
                }
            };
        }

        $scope.GetAllClaimNotesAssignedByMe = function () {
            $scope.tasksAssignedByMeGridOptions = {
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
                            return claimNotesService.GetClaimNotesAssignedByMe(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
               
                columns: [
                    {
                        headerAttributes: { "ng-non-bindable": true },
                        title: "Task Name",
                        field: "title",
                        template:
                            "<a href='\\#/viewClaimNote/{{dataItem.noteId}}' title='View Claim Note / Task Detail' class='k-link'>{{dataItem.title}}</a>",
                        width: "20%"
                    },
                    {
                        title: "Assigned To",
                        field: "assignedTo",
                        width: "20%"
                    },
                    {
                        title: "Created Date",
                        field: "createdDate",
                        width: "14%"
                    },
                    {
                        title: "Due Date",
                        field: "dueDate",
                        width: "14%"
                    }
                ],
                dataBound: function () {
                    var dataView = this.dataSource.view();
                    for (var i = 0; i < dataView.length; i++) {
                        if (dataView[i].isDuteDatetask == "1") {
                            var uid = dataView[i].uid;
                            $("#tasksAssignedByMeGrid table tbody").find("tr[data-uid=" + uid + "]").addClass("dueDateCustomClass");
                        }
                    }
                }
            };
        }

        $scope.GetAllClaimsDueToday = function () {
            $scope.dueTodayGridOptions = {
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
                            return claimService.GetAllClaimsDueToday(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                
                columns: [
                    {
                        title: "Claim #",
                        field: "claimNo",
                        template:
                            "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                        width: "8%"
                    },
                    {
                        title: "Customer",
                        field: "companyName",
                        template:
                            "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Customer Detail' class='k-link'>{{dataItem.companyName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Contact",
                        field: "contactName",
                        template:
                            "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Contact Detail' class='k-link'>{{dataItem.contactName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Open Date",
                        field: "receivedDate",
                        width: "8%"
                    },
                    {
                        title: "Due Date",
                        field: "dueDate",
                        width: "8%"
                    }
                ]
            };
        }
        $scope.GetAllClaimsDueIn7Days = function () {
            $scope.due7daysGridOptions = {
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
                            return claimService.GetAllClaimsDueIn7Days(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [
                    {
                        title: "Claim #",
                        field: "claimNo",
                        template:
                            "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                        width: "8%"
                    },
                    {
                        title: "Customer",
                        field: "companyName",
                        template:
                            "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Customer Detail' class='k-link'>{{dataItem.companyName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Contact",
                        field: "contactName",
                        template:
                            "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Contact Detail' class='k-link'>{{dataItem.contactName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Open Date",
                        field: "receivedDate",
                        width: "8%"
                    },
                    {
                        title: "Due Date",
                        field: "dueDate",
                        width: "8%"
                    }
                ]
            };
        }

        $scope.BindOverdueClaims = function () {
            $scope.overdueClaimsGridOptions = {
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
                            return claimService.GetOverdueClaims(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [
                    {
                        title: "Claim #",
                        field: "claimNo",
                        template:
                            "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                        width: "8%"
                    },
                    {
                        title: "Customer",
                        field: "companyName",
                        template:
                            "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Customer Detail' class='k-link'>{{dataItem.companyName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Contact",
                        field: "contactName",
                        template:
                            "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Contact Detail' class='k-link'>{{dataItem.contactName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Open Date",
                        field: "receivedDate",
                        width: "8%"
                    },
                    {
                        title: "Due Date",
                        field: "dueDate",
                        width: "8%"
                    }
                ]
            };
        }

        $scope.BindOverdueTasks = function () {
            $scope.overdueTasksGridOptions = {
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
                            return claimNotesService.GetOverdueTasks(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [
                    {
                        title: "Claim #",
                        field: "claimNo",
                        template:
                            "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                        width: "8%"
                    },
                    {
                        title: "Customer",
                        field: "companyName",
                        template:
                            "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Customer Detail' class='k-link'>{{dataItem.companyName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Contact",
                        field: "contactName",
                        template:
                            "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Contact Detail' class='k-link'>{{dataItem.contactName}}</a>",
                        width: "12%"
                    },
                    {
                        title: "Received",
                        field: "createdDate ",
                        width: "8%"
                    },
                    {
                        title: "Due Date",
                        field: "taskEndDate ",
                        width: "8%"
                    }
                ]
            };
        }

        $scope.BindOpenClaims = function () {
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url:appURL+"/api/claims/GetOpenClaims",
                        dataType: "json"
                    }
                }
            });
            dataSource.read();
            $("#chartOpenClaims").kendoChart({
                title: {
                   text: "Open Claims",
                    font: "30px Arial,Helvetica,sans-serif; display:inline-block;"
                },
                legend: {
                    //position: "bottom"
                    visible:false
                },
                dataSource: {
                    data: dataSource._data[0].claimses
                },
                series: [{
                    type: "pie",
                    field: "count",
                    startAngle: 150,
                    categoryField: "contactName",
                    explodeField: "explode"
                }],
                seriesColors: dataSource._data[0].color,
                tooltip: {
                    visible: true,
                    template: "${ category } - ${ value }"
                },
                seriesDefaults: {
                    labels: {
                        visible: true,
                        background: "transparent",
                        template: "${ category } - ${ value }"
                    }
                },
                dataBound: function () {
                    if (this.dataSource.transport.data.length < 1)
                    {
                        $('#chartOpenClaims').data('kendoChart').options.title.text = "Currently, there are no open claims";
                    }
                }
            });
           
        }

        $scope.BindAverageDaysOpen = function () {
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: appURL+"/api/claims/GetAverageDaysOpen",
                        dataType: "json"
                    }
                }
            });
            dataSource.read();
            $("#chartAverageDaysOpen").kendoChart({
                autoBind: false,
                title: {
                    text: "Average Days Open",
                    font: "30px Arial,Helvetica,sans-serif; display:inline-block;"
                },
                dataSource: dataSource._data[0].claimses,
                series: [
                 {
                     data: dataSource._data[0].count
                 }
                ],
                categoryAxis: {
                    labels: {
                        rotation: -40
                    },
                    majorGridLines: {
                        visible: false
                    },
                    categories: dataSource._data[0].contactNames
                },
                
                tooltip: {
                    visible: true,
                    format: "N0"
                }
            });
            dataSource.read();
        }
        $scope.BindSystemAlerts=function()
        {
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return systemAlertsService.getSystemAlerts(function (response) {
                            options.success(response);
                        });
                    }
                },
                schema: {
                    parse: function (response) {
                        var output = [];
                        $.each(response, function (idx, elem) {
                            elem.index = idx;
                            output.push(elem);
                        });
                        return output;
                    }
                }
            });

            var viewModel = new kendo.data.ObservableObject({
               // Id: "System Alerts",
                Associative: dataSource
            });

            kendo.bind('#systemAlerts', viewModel);
        }


        $scope.GetAllClaimNotesAssignedToMe();
        $scope.GetAllClaimNotesAssignedByMe();
        $scope.GetAllClaimsDueToday();
        $scope.GetAllClaimsDueIn7Days();
        $scope.BindAverageDaysOpen();
        $scope.BindOpenClaims();
        $scope.BindOverdueTasks();
        $scope.BindOverdueClaims();
        $scope.BindSystemAlerts();

    };


    angular.module("ClaimsManagementModule")
            .controller("HomeController", ["$scope", "$routeParams", "$timeout", "HomeService", "ClaimService", "SystemAlertsService", HomeController]);
}());