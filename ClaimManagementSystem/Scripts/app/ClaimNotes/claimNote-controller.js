(function () {
    "use strict";
    var ClaimNotesController = function ($scope, $routeParams, $timeout, usersService, claimService, claimNotesService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};
        $scope.claimNote = {};

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

        $scope.GetAllClaimNotes = function () {
            $scope.claimNotesGridOptions = {
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
                            return claimNotesService.query({ claimId: $routeParams.claimId }, function (response) {
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
                    title: "Note / Task",
                    field: "title",
                    template: "<a href='\\#/viewClaimNote/{{dataItem.noteId}}' title='View Claim Note / Task Detail' class='k-link'>{{dataItem.title}}</a>",
                    width: "40%"
                },
                {
                    title: "Assigned To",
                    field: "assignedTo",
                    template: "<a href='javascript:void(0);' title='View Adjuster Details' class='k-link'>{{dataItem.assignedTo}}</a>",
                    width: "20%"
                },
                {
                    title: "Created Date",
                    field: "createdDate",
                    width: "14%"
                },
                {
                    title: "Due Date",
                    field: "taskEndDate",
                    width: "14%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "noteId",
                    template: "<a href='\\#/editClaimNote/{{dataItem.noteId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteClaimNote(#=noteId#)'></a>",
                    width: "12%"
                }]
            };
        }


        $scope.InitializeClaimNote = function () {
            $scope.claimNote = new claimNotesService();
            var claim = claimService.getClaim({ id: $routeParams.claimId }, function (response) {
                $scope.claimNote.claimId = response.claimId;
                $scope.claimNote.claimNo = response.fileNo;
                $scope.claim = response;
            });
        };

        $scope.ResetClaimNote = function () {
            $scope.InitializeClaimNote();

            $scope.claimNoteForm.$setPristine();
            $scope.claimNoteForm.$setUntouched();
            $scope.claimNoteForm.$setValidity();

            var validator = $("#claimNoteForm").kendoValidator().data("kendoValidator");
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

        $scope.AddClaimNote = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                if (angular.isObject($scope.claimNote.assignedToUser) && angular.isArray($scope.claimNote.assignedToUser)) {
                    $scope.claimNote.assignedToUser = $scope.claimNote.assignedToUser[0];
                }

                claimNotesService.addClaimNote(
                    null,
                    $scope.claimNote,
                    function (response) {
                        if (!response.hasError && response.noteId > 0) {
                            $scope.InitializeClaimNote();

                            $scope.message = "Claim note / task has been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== null && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save claim note / task. Please try again.";
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
                $scope.message = "Please provide valid claim details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteClaimNote = function (claimNoteId) { // Delete a claim
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this claim note / task?')) {
                claimNotesService.deleteClaimNote({ id: claimNoteId }, { id: claimNoteId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected claim note / task has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.claimNotesGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected claim. Please try again.";
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

        $scope.InitializeClaimNote();
        $scope.GetAllClaimNotes();
    };

    var ViewEditClaimNotesController = function ($scope, $routeParams, $timeout, usersService, claimService, claimNotesService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};
        $scope.claimNote = {};

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

        $scope.LoadClaimNote = function () {
            var claimNote = claimNotesService.getClaimNote({ id: $routeParams.claimNoteId }, function (response) {
                $scope.claimNote = response;

                var claim = claimService.getClaim({ id: response.claimId }, function (response) {
                    $scope.claim = response;
                });
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetClaimNote = function () {
            $scope.LoadClaimNote();

            $scope.claimNoteForm.$setPristine();
            $scope.claimNoteForm.$setUntouched();
            $scope.claimNoteForm.$setValidity();

            var validator = $("#claimNoteForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateClaimNote = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                if (angular.isObject($scope.claimNote.assignedToUser) && angular.isArray($scope.claimNote.assignedToUser)) {
                    $scope.claimNote.assignedToUser = $scope.claimNote.assignedToUser[0];
                }

                claimNotesService.updateClaimNote(
                    null,
                    $scope.claimNote,
                    function (response) {
                        if (!response.hasError && response.noteId > 0) {
                            $scope.message = "Claim note / task has been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save claim note / task. Please try again.";
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
                $scope.message = "Please provide valid claim details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadClaimNote();
    };

    angular.module("ClaimsManagementModule")
            .controller("ClaimNotesController", ["$scope", "$routeParams", "$timeout", "UsersService", "ClaimService", "ClaimNotesService", ClaimNotesController])
            .controller("ViewEditClaimNotesController", ["$scope", "$routeParams", "$timeout", "UsersService", "ClaimService", "ClaimNotesService", ViewEditClaimNotesController]);
}());