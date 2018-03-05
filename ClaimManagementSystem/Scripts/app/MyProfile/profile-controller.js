(function () {
    "use strict";


    var EditProfileController = function ($scope, $routeParams, usersService, companiesService, countriesService, statesService) {

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


        $scope.UserTypes = new kendo.data.DataSource({
            data: [{
                userTypeId: 1,
                userTypeName: "Insurer"
            },
            {
                userTypeId: 2,
                userTypeName: "Broker"
            }, {
                userTypeId: 3,
                userTypeName: "Claimant"
            }, {
                userTypeId: 4,
                userTypeName: "Adjuster"
            }]
        });

        $scope.userProfiles = new kendo.data.DataSource({
            data: [{
                ProfileTypeId: 1,
                ProfileType: "Standard User"
            },
            {
                ProfileTypeId: 2,
                ProfileType: "System Administrator"
            }]
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
            if ($routeParams.userId) {
                var user = usersService.findProfile({ id: $routeParams.userId }, function (response) {
                    $scope.user = response;
                });
            }
            else {
                var users = usersService.getProfile(function (response) {
                    $scope.user = response;
                });
            }
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

        $scope.UpdateUser = function (event) {
            event.preventDefault();
            if ($scope.validator.validate()) {
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



        $scope.LoadUser();
    };


    angular.module("ClaimsManagementModule")
            .controller("EditProfileController", ["$scope", "$routeParams", "ProfileService", "UsersService", "CompaniesService", "CountriesService", "StatesService", EditProfileController]);
}());