(function (applicationBaseUrl) {
    "use strict";

    var ListAllUsersController = function ($http,$scope, $routeParams, usersService, companiesService, countriesService, statesService, claimService) {
        $scope.companiesGridOptions = {};

        var companies = companiesService.query(function (response) {

            $scope.Companies = new kendo.data.DataSource({
                data: response
            });

        });
        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });
        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });
        var userTypes = usersService.getContactTypes(function (response) {
            $scope.userTypes = new kendo.data.DataSource({
                data: response
            });
        });


        var userProfiles = usersService.getProfiles(function (response) {
            $scope.userProfiles = new kendo.data.DataSource({
                data: response
            });
        });
        $scope.createdDate = Date();

        //$scope.UserTypes = new kendo.data.DataSource({
        //    data: [{
        //        userTypeId: 1,
        //        userTypeName: "Insurer"
        //    },
        //    {
        //        userTypeId: 2,
        //        userTypeName: "Broker"
        //    }, {
        //        userTypeId: 3,
        //        userTypeName: "Claimant"
        //    }, {
        //        userTypeId: 4,
        //        userTypeName: "Adjuster"
        //    }]
        //});

        //$scope.userProfiles = new kendo.data.DataSource({
        //    data: [{
        //        ProfileTypeId: 1,
        //        ProfileType: "Standard User"
        //    },
        //    {
        //        ProfileTypeId: 2,
        //        ProfileType: "System Administrator"
        //    }]
        //});
        $scope.userStatus = new kendo.data.DataSource({
            data: [{
                statusId: 1,
                statusName: "Active"

            },
            {
                statusId: 0,
                statusName: "Inactive"
            }]
        });

        $scope.GetAllUsers = function () {
            var users = usersService.query(function (response) {
                $scope.UserOptions = {
                    dataSource: response,
                    dataTextField: "userId",
                    dataValueField: "userId"
                };
            })
        };



        $scope.LoadUsers = function () {
            $scope.usersGridOptions = {
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
                                return usersService.query(function (response) {
                                    $scope.gridOptions = options;
                                    options.success(response);
                                });
                            }
                        }
                    }),
                    columns: [{
                        headerAttributes: { "ng-non-bindable": true },
                        title: "First Name",
                        field: "firstName",
                        width: "15%"
                    },
                    {
                        title: "Last Name",
                        field: "lastName",
                        width: "15%"
                    },
                    {
                        title: "Email",
                        field: "email",
                        width: "15%"
                    },
                       {
                           title: "Created Date",
                           field: "createdDate",
                           width: "10%"
                       },
                       {
                           title: "User Profile",
                           field: "userTypeName",
                           width: "15%"
                       },
                     {
                         title: "Department",
                         field: "department",
                         width: "10%"
                     },
                      
                       {
                           title: "Status",
                           field: "status",
                           width: "10%"
                       },
                    {
                        title: "Action",
                        headerAttributes: { style: "text-align: center" },
                        attributes: { class: "text-center" },
                        template: "<a href='\\#/editUser/{{dataItem.userId}}' title='Edit' class='glyphicon glyphicon-edit grey'></a>&nbsp;&nbsp;" +
                                    "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteUser(\"#=userId#\")'></a>",
                        width: "12%"
                    }]
                };
        }

        

        // $scope.GetAllUsers();
        $scope.Initialize = function () {
            $scope.user = new usersService();
        }
        $scope.DeleteUser = function (userId) {
            if (confirm('Are you sure to de-active this record?')) {
                usersService.deleteUser({ id: userId }, function (deleted) {
                    if (deleted) {
                        $scope.LoadUsers();
                        alert("The selected record has been successfully de-activate.");
                        $scope.usersGridOptions.dataSource.transport.read($scope.gridOptions);
                    }
                    else {
                        alert("Unable to proces your request at this time. Please try again later.");
                    }
                });
            }
        }

        
        $scope.ResetAddUser = function () {
            $scope.Initialize();

            $scope.userForm.$setPristine();
            $scope.userForm.$setUntouched();
            $scope.userForm.$setValidity();

            var validator = $("#userForm").kendoValidator().data("kendoValidator");
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

        $scope.AddUser = function (event) {
            event.preventDefault();
            if ($scope.validator.validate()) {
                //if (angular.isObject($scope.userProfiles)) {
                //    $scope.user.profileType = $scope.userProfiles.transport.data[$scope.user.profileTypeId-1].name;
                //}
                //if (angular.isObject($scope.userTypes)) {
                //    $scope.user.userTypeName = $scope.userTypes.transport.data[$scope.user.userTypeId-1].contactTypeName;
                //}
                //if (angular.isObject($scope.Companies)) {
                //    $scope.user.companyName = $scope.Companies.transport.data[$scope.user.companyId-1].companyName;
                //}
                
                userProfiles.forEach(function (element) {
                    if (element.id == $scope.user.profileTypeId) {
                        $scope.user.profileType = element.name;
                    }
                });
                userTypes.forEach(function (element) {
                    if (element.contactTypeId == $scope.user.userTypeId) {
                        $scope.user.userTypeName = element.contactTypeName;
                    }
                });
                companies.forEach(function (element) {
                    if (element.companyId == $scope.user.companyId) {
                        $scope.user.companyName = element.companyName;
                    }
                });
                usersService.addUser(
                    null,
                    $scope.user,
                    function (response) {
                       // $scope.Initialize();
                        if (response.errors != null && response.errors.length > 0) {
                            $scope.validationMessage = response.errors[0];
                           // $scope.validationMessage = "Unable to save user details. Please try again.";
                            $scope.validationClass = "invalid";
                            $scope.isSuccess = false;
                        }
                        else
                        {
                            if (response.id.length > 0) {
                                applicationBaseUrl = window.parent.location.href.split('Home')[0];
                                $http.post(applicationBaseUrl + "Account/SendEmail", { id: response.id }).success(function (data) {
                                    if (data == "True") {
                                        $scope.validationMessage = "User details have been saved successfully.";
                                        $scope.validationClass = "valid";
                                        $scope.isSuccess = true;
                                    }
                                    else {
                                        $scope.validationMessage = "User details have been saved successfully. But while sending mail problem occured. Please contact administrator";
                                        $scope.validationClass = "valid";
                                        $scope.isSuccess = true;
                                    }
                                }).error(function (error) {
                                    $scope.validationMessage = "Unable to proces your request at this time. Please try again later.";
                                    $scope.validationClass = "invalid";
                                    $scope.isSuccess = true;
                                });
                            }
                            else {
                                $scope.validationMessage = "Unable to save user details. Please try again.";
                                $scope.validationClass = "invalid";
                                $scope.isSuccess = false;
                            }
                        }
                    },
                    function (err) {
                        $scope.validationMessage = "Unable to proces your request at this time. Please try again later.";
                        $scope.validationClass = "invalid";
                        $scope.isSuccess = true;
                    });
            }
            else {
                $scope.validationMessage = "Please provide valid user details.";
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

        $scope.LoadUsers();
    }

    var EditUserController = function ($http,$scope, $routeParams, usersService, companiesService, countriesService, statesService, claimService) {

        $scope.AllowedFileType = {
            Image: "Image",
            Document: "Document"
        };
        $scope.message = "";
        $scope.isSuccess = null;
        $scope.imagesrc = "";
        $scope.statusMsg = "";

        var companies = companiesService.query(function (response) {

            $scope.Companies = new kendo.data.DataSource({
                data: response
            });

        });
        var countries = countriesService.query(function (response) {
            $scope.Countries = new kendo.data.DataSource({
                data: response
            });
        });
        var states = statesService.query(function (response) {
            $scope.States = new kendo.data.DataSource({
                data: response
            });
        });


        var userTypes = usersService.getContactTypes(function (response) {
            $scope.userTypes = new kendo.data.DataSource({
                data: response
            });
        });


        var userProfiles = usersService.getProfiles(function (response) {
            $scope.userProfiles = new kendo.data.DataSource({
                data: response
            });
        });

        $scope.userStatus = new kendo.data.DataSource({
            data: [{
                statusId: 1,
                statusName: "Active"

            },
            {
                statusId: 0,
                statusName: "Inactive"
            }]
        });

        $scope.GetAllUsers = function () {
            var users = usersService.query(function (response) {
                $scope.UserOptions = {
                    dataSource: response,
                    dataTextField: "userId",
                    dataValueField: "userId"
                };
            })
        };


        $scope.LoadUser = function () {
            var user = usersService.get({ id: $routeParams.userId }, function (response) {
                $scope.user = response;
                userProfiles.forEach(function (element) {
                    if(element.name==response.profileType)
                    {
                        $scope.user.profileTypeId=element.id;
                    }
                });
                userTypes.forEach(function (element) {
                    if(element.contactTypeName==response.userTypeName)
                    {
                        $scope.user.userTypeId = element.contactTypeId;
                    }
                });
                companies.forEach(function (element) {
                    if(element.companyName==response.companyName)
                    {
                        $scope.user.companyId = element.companyId;
                    }
                });
                if(response.statusId==1)
                {
                   $scope.user.statusMsg="Active"
                }
                else
                { $scope.user.statusMsg = "Inactive" }
            });
            window.scrollTo(0, 0);
        };

        $scope.Initialize = function () {
            $scope.user = new usersService();
        }

        $scope.ResetUpdateUser = function () {

            $scope.Initialize();
            $scope.userForm.$setPristine();
            $scope.userForm.$setUntouched();
            $scope.userForm.$setValidity();

            var validator = $("#userForm").kendoValidator().data("kendoValidator");
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
        applicationBaseUrl = window.parent.location.href.split('Home')[0];
        $scope.imageUploadOptions = {
            multiple: true,
            async: {
                saveUrl: applicationBaseUrl + 'Home/UploadProfileImages',
                saveVerb: 'POST',
                removeUrl: applicationBaseUrl + 'Home/RemoveProfileImages',
                removeVerb: 'POST',
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

        function onUploadImage(e) {
            e.data = { userId: $routeParams.userId, fileType: $scope.AllowedFileType.Image };
        }

        function HandleUploadError(e) {
             console.log(e);
        }

      
        $scope.UpdateUser = function (event) {
            event.preventDefault();
            if ($scope.validator.validate()) {
                userProfiles.forEach(function (element) {
                    if (element.id == $scope.user.profileTypeId) {
                        $scope.user.profileType = element.name;
                    }
                });
                userTypes.forEach(function (element) {
                    if (element.contactTypeId == $scope.user.userTypeId) {
                        $scope.user.userTypeName = element.contactTypeName;
                    }
                });
                companies.forEach(function (element) {
                    if (element.companyId == $scope.user.companyId) {
                        $scope.user.companyName = element.companyName;
                    }
                });
                

                usersService.updateUser(
                    null,
                    $scope.user,
                    function (response) {
                        // $scope.Initialize();

                        if (response.userId.length > 0) {
                            $scope.validationMessage = "User details have been updated successfully.";
                            $scope.validationClass = "valid";
                            $scope.isSuccess = true;
                        }
                        else {
                            $scope.validationMessage = "Unable to update user details. Please try again.";
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
                $scope.validationMessage = "Please provide valid user details.";
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

        var userImage = usersService.getImage(function (response) {
            $scope.userImage = new kendo.data.DataSource({
                data: response,
                imagesrc: response.fileLocation + "\\" + response.fileName,
                
            });
            $("#imageId").attr('src', $scope.userImage.transport.data.fileLocation+"/"+ $scope.userImage.transport.data.fileName);
        });

       

        $scope.LoadUser();
    };


    angular.module("ClaimsManagementModule")
            .controller("UserController", ["$http", "$scope", "$routeParams", "UsersService", "CompaniesService", "CountriesService", "StatesService", "ClaimService", ListAllUsersController])
            .controller("EditUserController", ["$http","$scope", "$routeParams", "UsersService", "CompaniesService", "CountriesService", "StatesService", "ClaimService", EditUserController]);
}());