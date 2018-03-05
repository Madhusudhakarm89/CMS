(function (applicationBaseUrl) {
    "use strict";
    var ClaimController = function ($rootScope, $scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService, typeOfLossService, claimStatusService, claimService) {
        $rootScope.showProgressLoader = false;

        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};
        

        var users = usersService.query(function (response) {
            $scope.Users = new kendo.data.DataSource({
                data: response
            });
        });

        var claimStatus = claimStatusService.query(function (response) {
            $scope.ClaimStatusOptions = new kendo.data.DataSource({
                data: response
            });
        });

        var LossTypeOptions = typeOfLossService.query(function (response) {
            $scope.LossTypeOptions = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.managerOptions = {
            dataTextField: 'fullName',
            dataValueField: "userId",
            placeholder: "Select Manager",
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

        $scope.adjusterOptions = {
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

        $scope.companyOptions = {
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

        $scope.contactOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Contact",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        var contactSearchFilter = [];
                        var searchOption1 = new contactsService();
                        var searchOption2 = new contactsService();
                        var searchOption3 = new contactsService();

                        if (angular.isObject($scope.claim.company)) {
                            if (angular.isArray($scope.claim.company) && $scope.claim.company[0].companyId > 0)
                                searchOption1.companyId = $scope.claim.company[0].companyId;
                            else if (!angular.isArray($scope.claim.company) && $scope.claim.company.companyId > 0)
                                searchOption1.companyId = $scope.claim.company.companyId;

                        }
                        //searchOption1.contactTypeId = 1;
                        //searchOption2.contactTypeId = 3;
                        //searchOption3.contactTypeId = 3;

                        contactSearchFilter.push(searchOption1);
                        //contactSearchFilter.push(searchOption2);
                        //contactSearchFilter.push(searchOption3);

                        return contactsService.findContacts(
                            null,
                            contactSearchFilter,
                            function (response) {
                                options.success(response);
                            });
                    }
                }
            })
        };

        $scope.claimantOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Claimant",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        var contactSearchFilter = [];
                        var contact = new contactsService();
                        contact.contactTypeId = 2;

                        contactSearchFilter.push(contact);
                        return contactsService.findContacts(
                            null,
                            contactSearchFilter,
                            function (response) {
                                options.success(response);
                            });
                    }
                }
            })
        };

        $scope.GetAllClaims = function () {
            $rootScope.showProgressLoader = true;
            $scope.claimsGridOptions = {
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
                                $rootScope.showProgressLoader = false;
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "File #",
                    field: "fileNo",
                    template: "<a href='\\#/viewClaim/{{dataItem.claimId}}' title='View Claim Detail' class='k-link'>{{dataItem.fileNo}}</a>",
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
                    field: "adjusterName",
                    template: "<a href='\\#/viewProfile/{{dataItem.adjusterId}}' title='View Adjuster Details' class='k-link'>{{dataItem.adjusterName}}</a>",
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
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "claimId",
                    template: "<a href='\\#/claimNotes/{{dataItem.claimId}}' data-toggle='tooltip' title='Notes' class='glyphicon glyphicon-file grey'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/timelogs/{{dataItem.claimId}}' title='Time Logs' class='glyphicon glyphicon-time grey'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/invoices/{{dataItem.claimId}}' title='Invoices' class='glyphicon glyphicon-list-alt grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Send Mail' class='glyphicon glyphicon-envelope grey'></a>&nbsp;&nbsp;" +
                                "<a href='\\#/editClaim/{{dataItem.claimId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteClaim(#=claimId#)'></a>",
                    width: "12%"
                }]
            };
        }


        $scope.InitializeClaim = function () {
            $rootScope.showProgressLoader = true;
            $scope.claim = new claimService();

            $rootScope.showProgressLoader = false;
        };

        $scope.ResetClaim = function () {
            $scope.InitializeClaim();

            $scope.claimForm.$setPristine();
            $scope.claimForm.$setUntouched();
            $scope.claimForm.$setValidity();

            var validator = $("#claimForm").kendoValidator().data("kendoValidator");
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

        $scope.AddClaim = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $rootScope.showProgressLoader = true;

                if (angular.isObject($scope.claim.company) && angular.isArray($scope.claim.company)) {
                    $scope.claim.company = $scope.claim.company[0];
                }

                LossTypeOptions.forEach(function (element) {
                    if (element.lossTypeId == $scope.claim.lossType) {
                        $scope.claim.lossType = element.lossTypeName;
                    }
                });

                if (angular.isObject($scope.claim.contact) && angular.isArray($scope.claim.contact)) {
                    $scope.claim.contact = $scope.claim.contact[0];
                }

                if (angular.isObject($scope.claim.claimant) && angular.isArray($scope.claim.claimant)) {
                    $scope.claim.claimant = $scope.claim.claimant[0];
                }

                if (angular.isObject($scope.claim.manager) && angular.isArray($scope.claim.manager)) {
                    $scope.claim.manager = $scope.claim.manager[0];
                }

                if (angular.isObject($scope.claim.adjuster) && angular.isArray($scope.claim.adjuster)) {
                    $scope.claim.adjuster = $scope.claim.adjuster[0];
                }

                claimService.addClaim(
                    null,
                    $scope.claim,
                    function (response) {
                        if (!response.hasError && response.claimId > 0) {
                            $scope.InitializeClaim();

                            $scope.message = "Claim details have been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== null && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save claim details. Please try again.";
                            }

                            $scope.isSuccess = false;
                        }
                        $rootScope.showProgressLoader = false;

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
                        $rootScope.showProgressLoader = false;

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

        $scope.DeleteClaim = function (claimId) { // Delete a claim
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this claim record?')) {
                $rootScope.showProgressLoader = true;

                claimService.deleteClaim({ id: claimId }, { id: claimId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected claim has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.claimsGrid.dataSource.transport.read($scope.gridOptions);
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
                    $rootScope.showProgressLoader = false;

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
                    $rootScope.showProgressLoader = false;

                    $timeout(function () {
                        if ($scope.isSuccess != null) {
                            $scope.message = "";
                            $scope.isSuccess = null;
                        }
                    }, 30000);
                });
            }
        };

        $scope.GetAllClaims();
    };

    var ViewEditClaimController = function ($rootScope, $scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService, claimNotesService, timeLogService, typeOfLossService, claimStatusService, claimService) {
        $scope.AllowedFileType = {
            Image: "Image",
            Document: "Document"
        };
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.claim = {};


        var users = usersService.query(function (response) {
            $scope.Users = new kendo.data.DataSource({
                data: response
            });
        });

        var claimStatus = claimStatusService.query(function (response) {
            $scope.ClaimStatusOptions = new kendo.data.DataSource({
                data: response
            });
        });

        var LossTypeOptions = typeOfLossService.query(function (response) {
            $scope.LossTypeOptions = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.managerOptions = {
            dataTextField: 'fullName',
            dataValueField: "userId",
            placeholder: "Select Manager",
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

        $scope.adjusterOptions = {
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

        $scope.companyOptions = {
            dataTextField: 'companyName',
            dataValueField: "companyId",
            placeholder: "Select Company",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return companiesService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        $scope.contactOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Contact",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        var contactSearchFilter = [];
                        var searchOption1 = new contactsService();
                        var searchOption2 = new contactsService();
                        var searchOption3 = new contactsService();

                        if (angular.isObject($scope.claim.company)) {
                            if (angular.isArray($scope.claim.company) && $scope.claim.company[0].companyId > 0)
                                searchOption1.companyId = $scope.claim.company[0].companyId;
                            else if (!angular.isArray($scope.claim.company) && $scope.claim.company.companyId > 0)
                                searchOption1.companyId = $scope.claim.company.companyId;

                        }
                        searchOption1.contactTypeId = 1;
                        //searchOption2.contactTypeId = 3;
                        //searchOption3.contactTypeId = 3;

                        contactSearchFilter.push(searchOption1);
                        contactSearchFilter.push(searchOption2);
                        contactSearchFilter.push(searchOption3);

                        return contactsService.findContacts(
                            null,
                            contactSearchFilter,
                            function (response) {
                                options.success(response);
                            });
                    }
                }
            })
        };

        $scope.claimantOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Claimant",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        var contactSearchFilter = [];
                        var contact = new contactsService();
                        contact.contactTypeId = 2;

                        contactSearchFilter.push(contact);
                        return contactsService.findContacts(
                            null,
                            contactSearchFilter,
                            function (response) {
                                options.success(response);
                            });
                    }
                }
            })
        };

        $scope.imageUploadOptions = {
            multiple: true,
            async: {
                saveUrl: applicationBaseUrl + '/Home/UploadClaimFiles',
                saveVerb: 'POST',
                removeUrl: applicationBaseUrl + '/Home/RemoveClaimFiles',
                removeVerb: 'DELETE',
                autoUpload: true,
                batch: true
            },
            validation: {
                allowedExtensions: ['.bmp', '.jpg', '.jpeg', '.png', '.tiff'],
                maxFileSize: 4194304
            },
            error: HandleUploadError,
            upload: onUploadImage
            //success: onSuccess
        };

        $scope.documentUploadOptions = {
            multiple: true,
            async: {
                saveUrl: applicationBaseUrl + '/Home/UploadClaimFiles',
                saveVerb: 'POST',
                removeUrl: applicationBaseUrl + '/Home/RemoveClaimFiles',
                removeVerb: 'DELETE',
                autoUpload: true,
                batch: true
            },
            validation: {
                allowedExtensions: ['.txt', '.rtf', '.csv', '.doc', '.docx', '.xls', '.xlsx', '.ppt', '.pptx', '.pdf'],
                maxFileSize: 4194304
            },
            error: HandleUploadError,
            upload: onUploadDocument
            //success: onSuccess
        };

        function onUploadDocument(e) {
            e.data = { claimId: $scope.claim.claimId, fileType: $scope.AllowedFileType.Document };
        }

        function onUploadImage(e) {
            e.data = { claimId: $scope.claim.claimId, fileType: $scope.AllowedFileType.Image };
        }

        function HandleUploadError(e) {
            console.log(e);
            $rootScope.showProgressLoader = false;
        }

        $scope.GetAllClaimNotes = function (claimId) {
            $rootScope.showProgressLoader = true;

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
                            return claimNotesService.query({ claimId: claimId }, function (response) {
                                $scope.gridOptionsForClaimNotes = options;
                                options.success(response);
                                $rootScope.showProgressLoader = false;
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
                    template: "<a href='\\#/viewProfile/{{dataItem.AssignedToUser.userId}}' title='View Details' class='k-link'>{{dataItem.assignedTo}}</a>",
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

        $scope.GetAllClaimTimelogs = function (claimId) {
            $rootScope.showProgressLoader = true;

            $scope.claimTimelogsGridOptions = {
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
                            return timeLogService.query({ claimId: claimId }, function (response) {
                                $scope.gridOptionsForClaimTimelogs = options;
                                options.success(response);
                                $rootScope.showProgressLoader = false;
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
        }

        $scope.GetAllClaimImages = function (claimId) {
            $rootScope.showProgressLoader = true;

            $scope.claimImagesGridOptions = {
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
                            return claimService.getDocuments({ claimId: claimId, fileType: $scope.AllowedFileType.Image }, { claimId: claimId, fileType: $scope.AllowedFileType.Image }, function (response) {
                                $scope.gridOptionsForClaimImages = options;
                                options.success(response);
                                $rootScope.showProgressLoader = false;
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "File Name",
                    field: "fileName",
                    template: "{{dataItem.fileDisplayName}}",
                    width: "40%"
                },
                {
                    title: "Uploaded By",
                    field: "uploadedBy",
                    template: "{{dataItem.uploadedBy}}",
                    width: "20%"
                },
                {
                    title: "Upload Date",
                    field: "uploadedOn",
                    width: "20%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "documentId",
                    template: "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteDocument(#=documentId#, true)'></a>",
                    width: "10%"
                }]
            };
        }

        $scope.GetAllClaimDocuments = function (claimId) {
            $rootScope.showProgressLoader = true;

            $scope.claimDocumentsGridOptions = {
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
                            return claimService.getDocuments({ claimId: claimId, fileType: $scope.AllowedFileType.Document }, { claimId: claimId, fileType: $scope.AllowedFileType.Document }, function (response) {
                                $scope.gridOptionsForClaimDocuments = options;
                                options.success(response);
                                $rootScope.showProgressLoader = false;
                            });
                        }
                    }
                }),
                columns: [{
                    headerAttributes: { "ng-non-bindable": true },
                    title: "File Name",
                    field: "fileName",
                    template: "{{dataItem.fileDisplayName}}",
                    width: "40%"
                },
                {
                    title: "Uploaded By",
                    field: "uploadedBy",
                    template: "{{dataItem.uploadedBy}}",
                    width: "20%"
                },
                {
                    title: "Upload Date",
                    field: "uploadedOn",
                    width: "20%"
                },
                {
                    title: "Action",
                    headerAttributes: { style: "text-align: center" },
                    attributes: { class: "text-center" },
                    field: "documentId",
                    template: "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteDocument(#=documentId#, false)'></a>",
                    width: "10%"
                }]
            };
        }

        $scope.LoadClaim = function () {
            $rootScope.showProgressLoader = true;

            var claim = claimService.getClaim({ id: $routeParams.claimId }, function (response) {
                $scope.claim = response;
                $rootScope.showProgressLoader = false;

                $scope.GetAllClaimNotes($scope.claim.claimId);
                $scope.GetAllClaimTimelogs($scope.claim.claimId);
                $scope.GetAllClaimImages($scope.claim.claimId);
                $scope.GetAllClaimDocuments($scope.claim.claimId);
            });

            window.scrollTo(0, 0);
        };

        $scope.ResetClaim = function () {
            $scope.LoadClaim();

            $scope.claimForm.$setPristine();
            $scope.claimForm.$setUntouched();
            $scope.claimForm.$setValidity();

            var validator = $("#claimForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateClaim = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                $rootScope.showProgressLoader = true;

                if (angular.isObject($scope.claim.company) && angular.isArray($scope.claim.company)) {
                    $scope.claim.company = $scope.claim.company[0];
                }

                LossTypeOptions.forEach(function (element) {
                    if (element.lossTypeId == $scope.claim.lossType) {
                        $scope.claim.lossType = element.lossTypeName;
                    }
                });


                if (angular.isObject($scope.claim.contact) && angular.isArray($scope.claim.contact)) {
                    $scope.claim.contact = $scope.claim.contact[0];
                }

                if (angular.isObject($scope.claim.claimant) && angular.isArray($scope.claim.claimant)) {
                    $scope.claim.claimant = $scope.claim.claimant[0];
                }

                if (angular.isObject($scope.claim.manager) && angular.isArray($scope.claim.manager)) {
                    $scope.claim.manager = $scope.claim.manager[0];
                }

                if (angular.isObject($scope.claim.adjuster) && angular.isArray($scope.claim.adjuster)) {
                    $scope.claim.adjuster = $scope.claim.adjuster[0];
                }

                claimService.updateClaim(
                    null,
                    $scope.claim,
                    function (response) {
                        if (!response.hasError && response.contactId > 0) {
                            $scope.message = "Claim details have been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save claim details. Please try again.";
                            }

                            $scope.isSuccess = false;
                        }
                        $rootScope.showProgressLoader = false;

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
                        $rootScope.showProgressLoader = false;

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

        $scope.DeleteClaimNote = function (claimNoteId) { // Delete a claim note
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this claim note / task?')) {
                $rootScope.showProgressLoader = true;

                claimNotesService.deleteClaimNote({ id: claimNoteId }, { id: claimNoteId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected claim note / task has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.claimNotesGrid.dataSource.transport.read($scope.gridOptionsForClaimNotes);
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
                    $rootScope.showProgressLoader = false;

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
                    $rootScope.showProgressLoader = false;

                    $timeout(function () {
                        if ($scope.isSuccess != null) {
                            $scope.message = "";
                            $scope.isSuccess = null;
                        }
                    }, 30000);
                });
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

                        $scope.claimTimelogsGrid.dataSource.transport.read($scope.gridOptionsForClaimTimelogs);
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

        $scope.DeleteDocument = function (documentId, isImageFile) { // Delete a claim image
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this file?')) {
                $rootScope.showProgressLoader = true;

                claimService.deleteDocument({ id: documentId }, { id: documentId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected file has been successfully deleted.";
                        $scope.isSuccess = true;

                        if (isImageFile) {
                            $scope.associatedPhotosForClaimGrid.dataSource.transport.read($scope.gridOptionsForClaimImages);
                        }
                        else {
                            $scope.associatedDocumentsForClaimGrid.dataSource.transport.read($scope.gridOptionsForClaimDocuments);
                        }
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected file. Please try again.";
                        }

                        $scope.isSuccess = false;
                    }
                    $rootScope.showProgressLoader = false;

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
                    $rootScope.showProgressLoader = false;

                    $timeout(function () {
                        if ($scope.isSuccess != null) {
                            $scope.message = "";
                            $scope.isSuccess = null;
                        }
                    }, 30000);
                });
            }
        };

        $scope.LoadClaim();
    };

    angular.module("ClaimsManagementModule")
            .controller("ClaimController", ["$rootScope", "$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", "TypeOfLossService", "ClaimStatusService", "ClaimService", ClaimController])
            .controller("ViewEditClaimController", ["$rootScope", "$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", "ClaimNotesService", "TimeLogService", "TypeOfLossService", "ClaimStatusService", "ClaimService", ViewEditClaimController]);
}(window.appURL));