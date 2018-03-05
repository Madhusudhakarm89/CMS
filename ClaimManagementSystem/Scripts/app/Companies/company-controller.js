(function () {
    "use strict";
    var ListAllCompaniesController = function ($scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService) {
        $scope.validationMessage = "";
        $scope.validationClass = "";
        $scope.isSuccess = null;

        $scope.list = [];
        var countries = countriesService.query(function (response) {
            $scope.lists = new kendo.data.DataSource({

                data: response
            });
        })

        //var contacts = contactsService.query(function (response) {
        //    $scope.contactNames = new kendo.data.DataSource({
        //        data: response

        //    });
        //});
        //var SelectedContactId;
        //$scope.contactOptions = {

        //    dataTextField: 'contactName', dataValueField: "ContactId",
        //    select: function (e) {
        //        var dataItem = this.dataItem(e.item.index());
        //        SelectedContactId = dataItem.contactId;

        //    },
        //    dataSource: contacts

        //}

        $scope.contactOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Contact",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return contactsService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        var users = usersService.query(function (response) {
            $scope.adjusterLists = new kendo.data.DataSource({
                data: response
            });

        })

        var states = statesService.query(function (response) {
            $scope.AnotherList = new kendo.data.DataSource({
                data: response
            });

        })


        $scope.Initialize = function () {
            //$scope.CompanyName = '';
            //$scope.ParentCompanyName = '';
            //$scope.Type = '';
            //$scope.DefaultAdjuster = '';
            //$scope.ContactEmailId = '';
            //$scope.Phone = '';
            //$scope.City = '';
            //$scope.CountryId = '';
            //$scope.ProvinceId = '';
            //$scope.Postal = '';
            //$scope.Street = '';
            //$scope.IsActive = '';
            //$scope.KeyContact = '';
            //$scope.AlternatePhone = '';
            //$scope.Fax = '';
            //$scope.Extension = '';
            //$scope.Unit = '';
            $scope.company = new companiesService();
        }
        $scope.gridOptions = null;
        $scope.GetAllCompanies = function () {

            //var companies = companiesService.query(function (response) {
            $scope.companiesGridOptions = {
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
                            return companiesService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
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
                                "<a href='\\#/editCompany/{{dataItem.companyId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                                "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteCompany(#=companyId#)'></a>",
                    width: "12%"
                }]
            };
            //});
        }

        $scope.UploadCompany = function (companyId) {

            $("#window").kendoWindow({
                width: 300,
                height: 150,
                title: "Upload Files",
                visible: false,
                modal: true

            }).data("kendoWindow");

            $("#window").removeClass("hidden");
            var win = $("#window").data("kendoWindow");
            win.center().open();
            $("input[type='file']").kendoUpload({
                async: {
                    saveUrl: "../Base/SaveCompanyFiles",
                    removeUrl: "../Base/RemoveCompanyFile",
                    autoUpload: true

                },

                upload: function (e) {

                    e.data = { companyId: companyId };
                }
                //,
                //success: onSuccess,
                //complete: onComplete

            });

        }


        $scope.onSelect = function (e) {
        }

        function onSuccess(e) {

        }
        function onComplete() {
            //var win = window.parent.$("#window").data("kendoWindow");
            //win.close();

        }

        $scope.Initialize();
        $scope.validate = function (event) {
            event.preventDefault();
            if ($scope.validator.validate()) {
                companiesService.addCompany(
                    null,
                    $scope.company,
                    function (response) {
                        $scope.Initialize();

                        if (response.companyId > 0) {
                            $scope.validationMessage = "Company details have been saved successfully.";
                            $scope.validationClass = "valid";
                            $scope.isSuccess = true;
                        }
                        else {
                            $scope.validationMessage = "Unable to save company details. Please try again.";
                            $scope.validationClass = "invalid";
                            $scope.isSuccess = false;
                        }
                    },
                    function (err) {
                        $scope.validationMessage = "Unable to proces your request at this time. Please try again later.";
                        $scope.validationClass = "invalid";
                        $scope.isSuccess = true;
                    });
            }
            else {
                $scope.validationMessage = "Please provide valid company details.";
                $scope.validationClass = "invalid";
                $scope.isSuccess = false;
            }

            $timeout(function () {
                if ($scope.isSuccess != null) {
                    $scope.validationMessage = "";
                    $scope.validationClass = "";
                    $scope.isSuccess = null;
                }
            }, 10000);
        }

        $scope.DeleteCompany = function (companyId) {
            $scope.validationMessage = "";
            $scope.validationClass = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this company?')) {
                companiesService.deleteCompany({ id: companyId }, { id: companyId }, function (deleted) {
                    if (deleted) {
                        $scope.validationMessage = "Selected company has been successfully deleted.";
                        $scope.validationClass = "valid";
                        $scope.isSuccess = true;

                        $scope.companyGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        $scope.validationMessage = "Unable to delete the selected company. Please try again.";
                        $scope.validationClass = "invalid";
                        $scope.isSuccess = true;
                    }
                },
                function (err) {
                    $scope.validationMessage = "Unable to proces your request. Please try again.";
                    $scope.validationClass = "invalid";
                    $scope.isSuccess = true;
                });
            }

            $timeout(function () {
                if ($scope.isSuccess != null) {
                    $scope.validationMessage = "";
                    $scope.validationClass = "";
                    $scope.isSuccess = null;
                }
            }, 10000);
        }

        $scope.ResetCompany = function () {
            $scope.Initialize();

            $scope.CompanyForm.$setPristine();
            $scope.CompanyForm.$setUntouched();
            $scope.CompanyForm.$setValidity();

            var validator = $("#CompanyForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.validationMessage = "";
            $scope.validationClass = "";
            $scope.isSuccess = null;
        };

        $scope.GetAllCompanies();
    }

    var ViewEditCompanyController = function ($scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService, companydocumentsService) {
        $scope.validationMessage = "";
        $scope.validationClass = "";
        $scope.isSuccess;

        var countries = countriesService.query(function (response) {
            $scope.lists = new kendo.data.DataSource({

                data: response
            });
        })

        //var contacts = contactsService.query(function (response) {
        //    $scope.contactNames = new kendo.data.DataSource({
        //        data: response

        //    });
        //});
        //var SelectedContactId;
        //$scope.contactOptions = {

        //    dataTextField: 'contactName', dataValueField: "ContactId",
        //    select: function (e) {
        //        var dataItem = this.dataItem(e.item.index());
        //        SelectedContactId = dataItem.contactId;

        //    },
        //    dataSource: contacts

        //}

        $scope.contactOptions = {
            dataTextField: 'contactName',
            dataValueField: "contactId",
            placeholder: "Select Contact",
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        return contactsService.query(function (response) {
                            options.success(response);
                        });
                    }
                }
            })
        };

        var users = usersService.query(function (response) {
            $scope.adjusterLists = new kendo.data.DataSource({
                data: response
            });

        })

        var states = statesService.query(function (response) {
            $scope.AnotherList = new kendo.data.DataSource({
                data: response
            });

        })

        $scope.LoadCompany = function () {
            var company = companiesService.get({ id: $routeParams.companyId }, function (response) {
                $scope.company = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetCompany = function () {
            $scope.LoadCompany();

            $scope.companyForm.$setPristine();
            $scope.companyForm.$setUntouched();
            $scope.companyForm.$setValidity();

            var validator = $("#companyForm").kendoValidator().data("kendoValidator");
            if (!$.isEmptyObject(validator)) {
                validator.hideMessages();

                var invalidElements = angular.element(document.getElementsByClassName("k-invalid"));
                if (!$.isEmptyObject(invalidElements)) {
                    invalidElements.removeClass("k-invalid");
                }
            }

            $scope.validationMessage = "";
            $scope.validationClass = "";
            $scope.isSuccess = null;
        };

        $scope.UpdateCompany = function (event) {
            event.preventDefault();
            if ($scope.validator.validate()) {

                companiesService.updateCompany(
                    { id: $routeParams.contactId },
                    $scope.company,
                    function (response) {
                        $scope.company = response;

                        $scope.validationMessage = "Company details have been updated successfully.";
                        $scope.validationClass = "valid";
                        $scope.isSuccess = true;
                    },
                    function (err) {
                        $scope.validationMessage = "Unable to update company details. Please try again.";
                        $scope.validationClass = "invalid";
                        $scope.isSuccess = false;
                    });
            }
            else {
                $scope.validationMessage = "Please provide valid company details.";
                $scope.validationClass = "invalid";
                $scope.isSuccess = false;
            }

            $timeout(function () {
                if ($scope.isSuccess != null) {
                    $scope.validationMessage = "";
                    $scope.validationClass = "";
                    $scope.isSuccess = null;
                }
            }, 10000);
        };

        $scope.LoadCompany();

        $scope.gridDocumentsOptions = null;
        $scope.GetAllCompaniesDocuments = function () {
            $scope.downloadCompaniesDocumentsGridOptions = {
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

                            return companydocumentsService.query({ companyId: $routeParams.companyId }, function (response) {

                                $scope.gridDocumentsOptions = options;
                                options.success(response);
                            });
                        }
                    }
                }),
                columns: [

                {

                    title: "File Name",
                    field: "fileName",
                    template: "<a title='Click to download' ng-click='DownloadCompanyDocument(dataItem.documentId,dataItem.fileName)'  class='k-link'>{{dataItem.fileName}}</a>",
                    width: "15%"
                }

                ]
            };
        }


        $scope.DownloadCompanyDocument = function (documentId, fileName) {
            var downloadPath = "/api/AccountDocumentMapping/GetCompanyDocuments?documentId=" + documentId + "&fileName=" + fileName;
            $scope.downloadFile(downloadPath);
        }

        $scope.downloadFile = function (downloadPath) {
            window.open(downloadPath, '_blank', '');
        }


        $scope.fileUpload = function () {
            $("#files").kendoUpload({
                async: {
                    saveUrl: "../Base/SaveCompanyFiles",
                    removeUrl: "../Base/RemoveCompanyFile"
                },
                upload: onUpload,
                complete: onComplete
            });
        }


        function onUpload(e) {
            e.data = {
                companyId: $routeParams.companyId
            };
        }

        function onComplete(e) {
            $scope.downloadEditCompanyDocumentsGrid.dataSource.transport.read($scope.gridDocumentsOptions);
        }
        $scope.fileUpload();
        $scope.GetAllCompaniesDocuments();
    };


    angular.module("ClaimsManagementModule")
            .controller("CompaniesController", ["$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", ListAllCompaniesController])
            .controller("ViewEditCompanyController", ["$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", "CompanyDocumentsService", ViewEditCompanyController]);
}());