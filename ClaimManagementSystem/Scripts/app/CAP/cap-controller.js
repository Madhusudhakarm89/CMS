(function (applicationBaseUrl) {
    "use strict";
    var CAPController = function ($scope, $routeParams, $timeout, claimService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};

       

        

        $scope.GetAllClaims = function () {
            $scope.myClaimsGridOptions = {
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
                            return claimService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
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
                    template: "<a href='\\#/claimdetails/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.claimNo}}</a>",
                    width: "8%"
                },
                {
                    title: "Claimant",
                    field: "claimantName",
                    width: "12%"
                },
                {
                    title: "Customer",
                    field: "companyName",
                    width: "12%"
                },
                {
                    title: "Contact",
                    field: "contactName",
                    width: "12%"
                },
                {
                    title: "Adjuster",
                    field: "adjusterName",
                    width: "12%"
                },
                {
                    title: "Received",
                    field: "receivedDate",
                    width: "8%"
                },
                {
                    title: "Loss Date",
                    field: "lossDate",
                    width: "8%"
                },
                {
                    title: "Status",
                    field: "status",
                    width: "8%"
                }]
            };
        }

        $scope.GetAllClaims();
    };

    var ViewCAPController = function ($scope, $routeParams, $timeout, claimService) {
      
        $scope.LoadClaim = function () {

            var claim = claimService.getClaim({ id: $routeParams.claimId }, function (response) {
                $scope.claim = response;
            });

            window.scrollTo(0, 0);
        };

        $scope.LoadClaim();
    };

    angular.module("ClaimsManagementModule")
    .controller("CAPController", ["$scope", "$routeParams", "$timeout", "ClaimService", CAPController])
    .controller("ViewCAPController", ["$scope", "$routeParams", "$timeout", "ClaimService", ViewCAPController]);
}(window.appURL));