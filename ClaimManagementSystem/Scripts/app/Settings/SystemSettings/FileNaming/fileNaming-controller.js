(function (applicationBaseUrl) {
    "use strict";
    var FileNamingController = function ($scope, $routeParams, $timeout, fileNamingService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.fileNameCode = {};

        $scope.GetAllFileNameCodes = function () {
            $scope.fileNamingGridOptions = {
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
                            return fileNamingService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "Location",
                    field: "locationName",
                    width: "50%",
                    template: "<a href='\\#/viewFileNamingCode/{{dataItem.fileNameId}}' title='View Details' class='k-link'>{{dataItem.locationName}}</a>"
                },
                {
                    title: "Prefix",
                    field: "fileNumberPrefix",
                    width: "30%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    template: "<a href='\\#/editFileNamingCode/{{dataItem.fileNameId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteFileNameCode(#=fileNameId#)'></a>",
                    width: "20%"
                }]
            };
        };

        $scope.InitializeFileNameCode = function () {
            $scope.fileNameCode = new fileNamingService();
        };

        $scope.ResetAddFileNameCode = function () {
            $scope.InitializeFileNameCode();

            $scope.fileNameCodeSettingForm.$setPristine();
            $scope.fileNameCodeSettingForm.$setUntouched();
            $scope.fileNameCodeSettingForm.$setValidity();

            var validator = $("#fileNameCodeSettingForm").kendoValidator().data("kendoValidator");
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

        $scope.AddFileNameCode = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                fileNamingService.addFileNameCode(
                    null,
                    $scope.fileNameCode,
                    function (response) {
                        if (!response.hasError && response.fileNameId > 0) {
                            $scope.InitializeFileNameCode();

                            $scope.message = "File naming details have been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save file naming details. Please try again.";
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
                $scope.message = "Please provide valid file naming details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteFileNameCode = function (fileNameId) {
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this file naming details?')) {
                fileNamingService.deleteFileNameCode({ id: fileNameId }, { id: fileNameId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected file naming details have been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.fileNamingGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected file naming details. Please try again.";
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

        $scope.GetAllFileNameCodes();
    };

    var ViewEditFileNamingController = function ($scope, $routeParams, $timeout, fileNamingService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.fileNameCode = {}; // fileNameCode
        debugger;
        $scope.LoadFileNameCode = function () {
            debugger;
            var fileNameCode = fileNamingService.getFileNameCode({ id: $routeParams.fileNameId }, function (response) {
                $scope.fileNameCode = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetEditFileCode = function () {
            $scope.LoadFileNameCode();

            $scope.editFileNameCodeForm.$setPristine();
            $scope.editFileNameCodeForm.$setUntouched();
            $scope.editFileNameCodeForm.$setValidity();

            var validator = $("#editFileNameCodeForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateFileNameCode = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                fileNamingService.updateFileNameCode(
                    null,
                    $scope.fileNameCode,
                    function (response) {
                        if (!response.hasError && response.fileNameId > 0) {
                            $scope.message = "File naming details have been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save file naming details. Please try again.";
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
                $scope.message = "Please provide valid file naming details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadFileNameCode();
    };

    angular.module("ClaimsManagementModule")
            .controller("FileNamingController", ["$scope", "$routeParams", "$timeout", "FileNamingService", FileNamingController])
            .controller("ViewEditFileNamingController", ["$scope", "$routeParams", "$timeout", "FileNamingService", ViewEditFileNamingController]);
}(window.appURL));