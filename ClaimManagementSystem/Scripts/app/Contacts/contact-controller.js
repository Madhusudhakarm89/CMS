(function (applicationBaseUrl) {
    "use strict";
    var ContactController = function ($scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.contact = {};

        var users = usersService.query(function (response) {
            $scope.Users = new kendo.data.DataSource({
                data: response
            });
        });

        //var contactTypes = companiesService.query(function (response) {
        //    $scope.Companies = new kendo.data.DataSource({
        //        data: response
        //    });

        //});

        $scope.ContactTypes = new kendo.data.DataSource({
            data: [{
                contactTypeId: 3,
                contactTypeName: "Insurer"
            }, {
                contactTypeId: 1,
                contactTypeName: "Broker"
            }, {
                contactTypeId: 2,
                contactTypeName: "Claimant"
            }, {
                contactTypeId: 4,
                contactTypeName: "Other"
            }]
        });

        var companies = companiesService.query(function (response) {

            $scope.Companies = new kendo.data.DataSource({
                data: response
            });

        });

        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });

        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.GetAllContacts = function () {
            //var contacts = contactsService.query(function (response) {
            $scope.contactsGridOptions = {
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
                            return contactsService.query(function (response) {
                                $scope.gridOptions = options;
                                options.success(response);
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
                              "<a href='\\#/editContact/{{dataItem.contactId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                              "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteContact(#=contactId#)'></a>",
                    width: "12%"
                }]
            };
            //});
        };

        $scope.InitializeContact = function () {
            $scope.contact = new contactsService();
        };

        $scope.ResetAddContact = function () {
            $scope.InitializeContact();

            $scope.contactForm.$setPristine();
            $scope.contactForm.$setUntouched();
            $scope.contactForm.$setValidity();

            var validator = $("#contactForm").kendoValidator().data("kendoValidator");
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

        $scope.AddContact = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                contactsService.addContact(
                    null,
                    $scope.contact,
                    function (response) {
                        if (!response.hasError && response.contactId > 0) {
                            $scope.InitializeContact();

                            $scope.message = "Contact details have been saved successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save contact details. Please try again.";
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
                $scope.message = "Please provide valid contact details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.DeleteContact = function (contactId) { // Delete a contact
            $scope.message = "";
            $scope.isSuccess = null;

            if (confirm('Are you sure to delete this contact?')) {
                contactsService.deleteContact({ id: contactId }, { id: contactId }, function (deleted) {
                    if (deleted) {
                        $scope.message = "Selected contact has been successfully deleted.";
                        $scope.isSuccess = true;

                        $scope.contactGrid.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                            $scope.message = response.errorMessage;
                        }
                        else {
                            $scope.message = "Unable to delete the selected contact. Please try again.";
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

        $scope.GetAllContacts();
    };

    var ViewEditContactController = function ($scope, $routeParams, $timeout, companiesService, countriesService, statesService, usersService, contactsService) {
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.contact = {};

        var users = usersService.query(function (response) {
            $scope.Users = new kendo.data.DataSource({
                data: response
            });
        });

        //var contactTypes = companiesService.query(function (response) {
        //    $scope.Companies = new kendo.data.DataSource({
        //        data: response
        //    });

        //});

        $scope.ContactTypes = new kendo.data.DataSource({
            data: [{
                contactTypeId: 1,
                contactTypeName: "Insurer"
            },
            {
                contactTypeId: 2,
                contactTypeName: "Broker"
            }, {
                contactTypeId: 3,
                contactTypeName: "Claimant"
            }, {
                contactTypeId: 4,
                contactTypeName: "Other"
            }]
        });

        var companies = companiesService.query(function (response) {

            $scope.Companies = new kendo.data.DataSource({
                data: response
            });

        });

        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });

        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.LoadContact = function () {
            var contact = contactsService.getContact({ id: $routeParams.contactId }, function (response) {
                $scope.contact = response;
            });
            window.scrollTo(0, 0);
        };

        $scope.ResetContact = function () {
            $scope.LoadContact();

            $scope.contactForm.$setPristine();
            $scope.contactForm.$setUntouched();
            $scope.contactForm.$setValidity();

            var validator = $("#contactForm").kendoValidator().data("kendoValidator");
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

        $scope.UpdateContact = function (event) {
            event.preventDefault();

            if ($scope.validator.validate()) {
                contactsService.updateContact(
                    null,
                    $scope.contact,
                    function (response) {
                        if (!response.hasError && response.contactId > 0) {
                            $scope.message = "Contact details have been updated successfully.";
                            $scope.isSuccess = true;
                        }
                        else {
                            if (!angular.isUndefined(response.errorMessage) && response.errorMessage !== "") {
                                $scope.message = response.errorMessage;
                            }
                            else {
                                $scope.message = "Unable to save contact details. Please try again.";
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
                $scope.message = "Please provide valid contact details.";
                $scope.isSuccess = false;

                $timeout(function () {
                    if ($scope.isSuccess != null) {
                        $scope.message = "";
                        $scope.isSuccess = null;
                    }
                }, 30000);
            }
        };

        $scope.LoadContact();
    };

    angular.module("ClaimsManagementModule")
            .controller("ContactController", ["$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", ContactController])
            .controller("ViewEditContactController", ["$scope", "$routeParams", "$timeout", "CompaniesService", "CountriesService", "StatesService", "UsersService", "ContactsService", ViewEditContactController]);
}(window.appURL));