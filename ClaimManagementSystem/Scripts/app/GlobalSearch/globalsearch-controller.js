(function () {
    "use strict";

    var ListAllGlobalSearchController = function ($scope, $routeParams, globalsearchService) {

        $scope.searchKeyword = $scope.$parent.searchKeyword;

        $scope.GetAllCompanies_GlobalSearch = function () {
            
            $scope.companiesGridOptions_GlobalSearch = {
                sortable: true,
                pageable: {
                    refresh: true,
                    pageSizes: true
                },
                scrollable: false,
                selectable: false,
                dataSource: new kendo.data.DataSource({
                    pageSize: 10,
                    transport: {
                        read: function (options) {
                            return globalsearchService.query({ id: $routeParams.searchKeyword }, function (response) {
                                if (!angular.isArray(response.companyViewModel))
                                {
                                    response.companyViewModel = [];
                                }

                                $scope.gridOptions = options;
                                options.success(response.companyViewModel);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Company Name",
                    field: "companyName",
                    template: "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Details' class='k-link'>{{dataItem.companyName}} </a>",
                    width: "25%"
                },
                {
                    title: "Type",
                    field: "type",
                    width: "15%"
                },
                {
                    title: "Email",
                    field: "contactEmailId",
                    template: "<a href='mailto:{{dataItem.contactEmailId}}' title='Click to email' class='k-link'>{{dataItem.contactEmailId}}</a>",
                    width: "15%"
                },
                {
                    title: "Phone",
                    field: "phone",
                    template: "<a href='tel:{{dataItem.phone}}' title='Click to call' class='k-link'>{{dataItem.phone}}</a>",
                    width: "20%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "companyId",

                    template: "<a href='javascript:void(0);' title='Upload Files' class='glyphicon glyphicon-duplicate grey' ng-click='UploadCompany(#=companyId#)'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/newContact' title='Add Contact' class='glyphicon glyphicon-user grey'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/editCompany/{{dataItem.companyId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>",
                    width: "12%"
                }]
            };
        }


        $scope.GetAllClaims_GlobalSearch = function () {
            //var claims = claimService.query(function (response) {
            $scope.claimGridOptions_GlobalSearch = {
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
                            
                            return globalsearchService.query({ id: $routeParams.searchKeyword }, function (response) {
                                if (!angular.isArray(response.claimViewModel)) {
                                    response.claimViewModel = [];
                                }

                                $scope.gridOptions = options;
                                options.success(response.claimViewModel);
                                
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "File #",
                    field: "fileNo",
                    width: "8%"
                },
                {
                    title: "Claim #",
                    field: "claimNo",
                    template: "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                    width: "8%"
                },
                {
                    title: "Claimant",
                    field: "claimName",
                    template: "<a href='\\#/viewContact/{{dataItem.claimantId}}' title='View Claimant Detail' class='k-link'>{{dataItem.claimantName}}</a>",
                    width: "12%"
                },
                {
                    title: "Customer",
                    field: "companyName",
                    template: "<a href='\\#/viewCompany/{{dataItem.companyId}}' title='View Customer Detail' class='k-link'>{{dataItem.companyName}}</a>",
                    width: "12%"
                },
                {
                    title: "Contact",
                    field: "contactName",
                    template: "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Contact Detail' class='k-link'>{{dataItem.contactName}}</a>",
                    width: "12%"
                },
                {
                    title: "Adjuster",
                    field: "adjuster",
                    width: "12%"
                },
                {
                    title: "Received",
                    field: "receivedDate",
                    width: "8%"
                },
                {
                    title: "Due Date",
                    template: "&nbsp;",
                    width: "8%"
                },
                {
                    title: "Status",
                    field: "claimStatus",
                    width: "8%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "claimId",
                    template: "<a href='javascript:void(0);' title='Notes' class='glyphicon glyphicon-file grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Time Logs' class='glyphicon glyphicon-time grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Invoices' class='glyphicon glyphicon-list-alt grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Send Mail' class='glyphicon glyphicon-envelope grey'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/editClaim/{{dataItem.claimId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>",
                    width: "12%"
                }]
            };
            //});
        }

        $scope.GetAllContacts_GlobalSearch = function () {
            //var contacts = contactsService.query(function (response) {
            $scope.contactsGridOptions_GlobalSearch = {
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
                            return globalsearchService.query({ id: $routeParams.searchKeyword }, function (response) {
                                if (!angular.isArray(response.contactViewModel)) {
                                    response.contactViewModel = [];
                                }

                                $scope.gridOptions = options;
                                options.success(response.contactViewModel);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Contact Name",
                    field: "contactName",
                    width: "20%",
                    template: "<a href='\\#/viewContact/{{dataItem.contactId}}' title='View Details' class='k-link'>{{dataItem.contactName}}</a>"
                },
                {
                    title: "Company Name",
                    field: "companyName",
                    width: "20%"
                },
                {
                    title: "Contact Type",
                    field: "contactTypeName",
                    width: "15%"
                },
                {
                    title: "Email Id",
                    field: "email",
                    template: "<a href='mailto:{{dataItem.email}}' title='Click to email' class='k-link'>{{dataItem.email}}</a>",
                    width: "18%"
                },
                {
                    title: "Contact No",
                    field: "phone",
                    template: "<a href='tel:{{dataItem.phone}}' title='Click to call' class='k-link'>{{dataItem.phone}}</a>",
                    width: "15%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='javascript:void(0);' title='Upload Files' class='glyphicon glyphicon-duplicate grey'></a>&nbsp;&nbsp;" +
                              "<a href='\\#/editContact/{{dataItem.contactId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>",
                    width: "12%"
                }]
            };
            //});
        };

        //$scope.GetAllCompanies_GlobalSearch();
        $scope.searchKeyword = angular.isString($routeParams.searchKeyword) ? $routeParams.searchKeyword : "";
        $scope.GetAllClaims_GlobalSearch();
        $scope.GetAllContacts_GlobalSearch();
        $scope.GetAllCompanies_GlobalSearch();
        
        $scope.globalSearch = function () {
            window.location = "#/globalSearch/" + $scope.searchKeyword;
        }

    }

    angular.module("ClaimsManagementModule")
            .controller("GlobalSearchController", ["$scope", "$routeParams", "GlobalSearchService", ListAllGlobalSearchController]);
}());