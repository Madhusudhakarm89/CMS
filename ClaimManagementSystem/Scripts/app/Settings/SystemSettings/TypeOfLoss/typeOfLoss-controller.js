(function (applicationBaseUrl) {
    "use strict";
    var TypeOfLossController = function ($scope, $routeParams, $timeout, typeOfLossService) {
        debugger;
        $scope.message = "";
        $scope.isSuccess = null;
        debugger;
        $scope.LossTypeService = {};

        $scope.GetAllLossTypes = function () {
            $scope.lossTypeGridOptions = {
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
                            return typeOfLossService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Name",
                    field: "lossTypeName",
                    width: "8%",
                    template: "<a href='\\#/viewLossType/{{dataItem.lossTypeId}}' title='View Details' class='k-link'>{{dataItem.lossTypeName}}</a>"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editLossType/{{dataItem.lossTypeId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteLossType(#=lossTypeId#)'></a>",
                    width: "12%"
                }
                ]
            };
            //});
        };

        $scope.InitializeLossType = function () {
            $scope.LossTypeService = new typeOfLossService();
        };

        $scope.AddLossType = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                typeOfLossService.addLossType(
                    null,
                    $scope.typeOfLoss,
                    function (response) {
                        if (!response.hasError && response.lossTypeId > 0) {
                            $scope.InitializeLossType();

                            $scope.message = "Loss type has been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save loss type. Please try again.";
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
                $scope.message = "Please provide valid loss Type details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };


        $scope.DeleteLossType = function (lossTypeId) { // Delete a contact
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this loss type?')) {
                typeOfLossService.deleteLossType({ id: lossTypeId }, { id: lossTypeId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected loss type has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.lossTypeGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected loss type. Please try again.";
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
        $scope.GetAllLossTypes();
    };

    var ViewEditLossTypeController = function ($scope, $routeParams, $timeout, typeOfLossService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.typeOfLoss = {};

        
        $scope.LoadLossType = function () {
            var typeOfLoss = typeOfLossService.getLossType({ id: $routeParams.lossTypeId }, function (response) {
                $scope.typeOfLoss = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetLossType = function () {
            $scope.LoadLossType();

            $scope.editLossTypeForm.$setPristine();
            $scope.editLossTypeForm.$setUntouched();
            $scope.editLossTypeForm.$setValidity();

            var validator = $("#editLossTypeForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateLossType = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                typeOfLossService.updateLossType(
                    null,
                    $scope.typeOfLoss,
                    function (response) {
                        if (!response.hasError && response.lossTypeId > 0) {
                            $scope.message = "Loss type has been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save loss type. Please try again.";
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
                $scope.message = "Please provide valid loss type details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadLossType();
    };

    angular.module("ClaimsManagementModule")
            .controller("TypeOfLossController", ["$scope", "$routeParams", "$timeout", "TypeOfLossService", TypeOfLossController])
    .controller("ViewEditLossTypeController", ["$scope", "$routeParams", "$timeout",  "TypeOfLossService", ViewEditLossTypeController]);
}(window.appURL));