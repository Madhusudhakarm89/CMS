﻿(function () {
    "use strict";

    var ListAllCountriesController = function ($scope, $routeParams, countriesService) {
        //$scope.companiesGridOptions = {};


        $scope.GetAllCountries = function () {
            var countries = countriesService.query(function (response) {
                $scope.countriesOptions = {
                    dataSource: response,
                    dataTextField: "CountryName",
                    dataValueField: "CountryId"
                };
            })
        };



        //$scope.GetAllCompanies = function () {
        //    var companies = companiesService.query(function (response) {
        //        $scope.companiesGridOptions = {
        //            sortable: true,
        //            pageSize: 1,
        //            selectable: false,
        //            dataSource: response,
        //            columns: [{
        //                headerAttributes: { "ng-non-bindable": true },
        //                title: "Account Name",
        //                field: "accountName",
        //                width: "20%"
        //            },
        //            {
        //                title: "Company Name",
        //                field: "companyName",
        //                width: "20%"
        //            },
        //            {
        //                title: "Type",
        //                field: "type",
        //                width: "15%"
        //            },
        //            {
        //                title: "Owner",
        //                field: "owner",
        //                width: "18%"
        //            },
        //            {
        //                title: "Email",
        //                field: "contactEmailId",
        //                width: "15%"
        //            },
        //            {
        //                title: "Action",
        //                headerAttributes: { style: "text-align: center" },
        //                attributes: { class: "text-center" },
        //                field: "companyId",
        //                template: "<a href='javascript:void(0);' title='Edit' class='glyphicon glyphicon-edit grey' ng-click='EditCompany(#=companyId#)'></a>&nbsp;&nbsp;" +
        //                            "<a href='javascript:void(0);' title='Delete' class='glyphicon glyphicon-remove grey' ng-click='DeleteCompany(#=companyId#)'></a>",
        //                width: "12%"
        //            }]
        //        };
        //    });
        //}

        //$scope.CompanyName = '';
        //$scope.ParentCompanyName = '';
        //$scope.Type = '1';
        //$scope.Owner = '';
        //$scope.ContactEmailId = '';
        //$scope.Phone = '';
        //$scope.City = '';
        //$scope.CountryId = '';
        //$scope.ProvinceId = '';
        //$scope.Postal = '';
        //$scope.Street = '';



        //$scope.save = function () {
        //    var vm = new Object();
        //    vm.CompanyName = $scope.CompanyName;
        //    vm.ParentCompanyName = $scope.ParentCompanyName;
        //    vm.Type = $scope.Type;
        //    vm.Owner = $scope.Owner;
        //    vm.ContactEmailId = $scope.ContactEmailId;
        //    vm.Phone = $scope.Phone;
        //    vm.City = $scope.City;
        //    vm.CountryId = $scope.CountryId;
        //    vm.ProvinceId = $scope.ProvinceId;
        //    vm.Postal = $scope.Postal;
        //    vm.Street = $scope.Street;

        //    companiesService.addCompany(function (response) {

        //    }, vm);
        //};

        //$scope.DeleteCompany = function (companyId) {
        //    if (confirm('Are you sure to delete this record?')) {
        //        companiesService.deleteCompany({ id: companyId }, function (deleted) {
        //            if (deleted) {
        //                alert("The selected record has been successfully deleted.");
        //                $scope.GetAllCompanies();
        //            }
        //            else {
        //                alert("Unable to proces your request at this time. Please try again later.");
        //            }
        //        });
        //    }
        //}

        $scope.GetAllCountries();
    }

    angular.module("ClaimsManagementModule")
            .controller("CountriesController", ["$scope", "$routeParams", "CountriesService", ListAllCountriesController]);
}());