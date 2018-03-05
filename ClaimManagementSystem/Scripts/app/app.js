
var cliamsManagementAppModule =
         angular.module("ClaimsManagementModule", ["ngRoute", "ngResource", "ngAnimate", "ngSanitize", "kendo.directives"])
                .config(['$routeProvider',
                    function ($routeProvider) {
                        $routeProvider
                            .when
                            ("/home", {
                                templateUrl: appURL + "/Scripts/app/Home/Views/Home.html",
                                controller: "HomeController"
                            })
                            .when("/claims", {
                                templateUrl: appURL + '/Scripts/app/Claims/Views/AllClaims.html',
                                controller: "ClaimController"
                            })
                            .when("/newClaim", {
                                templateUrl: appURL + '/Scripts/app/Claims/Views/NewClaim.html',
                                controller: "ClaimController"
                            })
                            .when("/editClaim/:claimId", {
                                templateUrl: appURL + '/Scripts/app/Claims/Views/EditClaim.html',
                                controller: "ViewEditClaimController"
                            })
                            .when("/viewClaim/:claimId", {
                                templateUrl: appURL + '/Scripts/app/Claims/Views/ViewClaim.html',
                                controller: "ViewEditClaimController"
                            })
                            .when("/claimNotes/:claimId", {
                                templateUrl: appURL + '/Scripts/app/ClaimNotes/Views/AllClaimNotes.html',
                                controller: "ClaimNotesController"
                            })
                            .when("/newClaimNote/:claimId", {
                                templateUrl: appURL + '/Scripts/app/ClaimNotes/Views/NewClaimNote.html',
                                controller: "ClaimNotesController"
                            })
                            .when("/editClaimNote/:claimNoteId", {
                                templateUrl: appURL + '/Scripts/app/ClaimNotes/Views/EditClaimNote.html',
                                controller: "ViewEditClaimNotesController"
                            })
                            .when("/viewClaimNote/:claimNoteId", {
                                templateUrl: appURL + '/Scripts/app/ClaimNotes/Views/ViewClaimNote.html',
                                controller: "ViewEditClaimNotesController"
                            })
                            .when("/timelogs/:claimId", {
                                templateUrl: appURL + '/Scripts/app/TimeLogs/Views/AllLogs.html',
                                controller: "TimeLogController"
                            })
                            .when("/newTimelog/:claimId", {
                                templateUrl: appURL + '/Scripts/app/TimeLogs/Views/NewTimelog.html',
                                controller: "TimeLogController"
                            })
                            .when("/editTimelog/:timelogId", {
                                templateUrl: appURL + '/Scripts/app/TimeLogs/Views/EditTimelog.html',
                                controller: "ViewEditTimeLogController"
                            })
                            .when("/viewTimelog/:timelogId", {
                                templateUrl: appURL + '/Scripts/app/TimeLogs/Views/ViewTimelog.html',
                                controller: "ViewEditTimeLogController"
                            })
                            .when("/companies", {
                                templateUrl: appURL + '/Scripts/app/Companies/Views/AllCompanies.html',
                                controller: "CompaniesController"
                            })
                            .when("/newCompany", {
                                templateUrl: appURL + '/Scripts/app/Companies/Views/NewCompany.html',
                                controller: "CompaniesController"
                            })
                            .when("/globalSearch/:searchKeyword", {
                                templateUrl: appURL + '/Scripts/app/GlobalSearch/Views/GlobalSearch.html',
                                controller: "GlobalSearchController"
                            })
                            .when("/editCompany/:companyId", {
                                templateUrl: appURL + '/Scripts/app/Companies/Views/EditCompany.html',
                                controller: "ViewEditCompanyController"
                            })
                            .when("/viewCompany/:companyId", {
                                templateUrl: appURL + '/Scripts/app/Companies/Views/viewCompany.html',
                                controller: "ViewEditCompanyController"
                            })
                            .when("/contacts", {
                                templateUrl: appURL + '/Scripts/app/Contacts/Views/AllContacts.html',
                                controller: "ContactController"
                            })
                            .when("/newContact", {
                                templateUrl: appURL + '/Scripts/app/Contacts/Views/NewContact.html',
                                controller: "ContactController"
                            })
                            .when("/editContact/:contactId", {
                                templateUrl: appURL + '/Scripts/app/Contacts/Views/EditContact.html',
                                controller: "ViewEditContactController"
                            })
                            .when("/viewContact/:contactId", {
                                templateUrl: appURL + '/Scripts/app/Contacts/Views/ViewContact.html',
                                controller: "ViewEditContactController"
                            })
                            .when("/invoices", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/AllInvoices.html',
                                controller: "InvoiceController"
                            })
                            .when("/invoices/:claimId", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/AllInvoices.html',
                                controller: "InvoiceController"
                            })
                            .when("/newInvoice", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/NewInvoice.html',
                                controller: "InvoiceController"
                            })
                            .when("/newInvoice/:claimId", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/NewInvoice.html',
                                controller: "InvoiceController"
                            })
                            .when("/editInvoice/:invoiceId", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/EditInvoice.html',
                                controller: "ViewEditInvoiceController"
                            })
                            .when("/viewInvoice/:invoiceId", {
                                templateUrl: appURL + '/Scripts/app/Invoices/Views/ViewInvoice.html',
                                controller: "ViewEditInvoiceController"
                            })
                            .when("/settings", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/FileNaming/Views/AllFileNameCodes.html',
                                controller: "FileNamingController"
                            })
                          
                            .when("/serviceItems", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/ServiceItem/Views/AllServiceItems.html',
                                controller: "ServiceItemController"
                            })
                            .when("/newServiceItem", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/ServiceItem/Views/NewServiceItem.html',
                                controller: "ServiceItemController"
                            })
                            .when("/editServiceItem/:serviceItemId", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/ServiceItem/Views/EditServiceItem.html',
                                controller: "ViewEditServiceItemController"
                            })
                            .when("/viewServiceItem/:serviceItemId", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/ServiceItem/Views/ViewServiceItem.html',
                                controller: "ViewEditServiceItemController"
                            })
                            .when("/taxSettings", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TaxSetting/Views/AllTaxSetting.html',
                                controller: "TaxSettingController"
                            })
                            .when("/newTaxSetting", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TaxSetting/Views/NewTaxSetting.html',
                                controller: "TaxSettingController"
                            })
                            .when("/editTaxSetting/:id", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TaxSetting/Views/EditTaxSetting.html',
                                controller: "ViewEditTaxSettingController"
                            })
                            .when("/myprofile", {
                                templateUrl: appURL + '/Scripts/app/myprofile/Views/viewProfile.html',
                                controller: "EditUserController"
                            })
                            .when("/editProfile/:userId", {
                                templateUrl: appURL + '/Scripts/app/myprofile/Views/EditProfile.html',
                                controller: "EditUserController"
                            })
                            .when("/viewProfile/:userId" , {
                                templateUrl: appURL + '/Scripts/app/myprofile/Views/viewProfile.html',
                                controller: "EditUserController"
                            })
                            .when("/users", {
                                templateUrl: appURL + '/Scripts/app/users/Views/AllUsers.html',
                                controller: "UserController"
                            })
                            .when("/newUser", {
                                templateUrl: appURL + '/Scripts/app/users/Views/NewUser.html',
                                controller: "UserController"
                            })
                            .when("/editUser/:userId", {
                                templateUrl: appURL + '/Scripts/app/users/Views/EditUser.html',
                                controller: "EditUserController"
                            })
                            .when("/myclaims", {
                                templateUrl: appURL + '/Scripts/app/cap/Views/AllCAPClaims.html',
                                controller: "CAPController"
                            })
                            .when("/cap", {
                                templateUrl: appURL + '/Scripts/app/cap/Views/AllCAPClaims.html',
                                controller: "CAPController"
                            })
                            .when
                            ("/claimdetails/:claimId", {
                                templateUrl: appURL + "/Scripts/app/cap/Views/ViewCAPClaim.html",
                                controller: "ViewCAPController"
                            })
                            .when
                            ("/systemalerts", {
                                templateUrl: appURL + "/Scripts/app/Settings/SystemSettings/SystemAlerts/Views/AllSystemAlerts.html",
                                controller: "SystemAlertsController"
                            })
                            .when
                            ("/newsystemalerts", {
                                templateUrl: appURL + "/Scripts/app/Settings/SystemSettings/SystemAlerts/Views/NewSystemAlerts.html",
                                controller: "SystemAlertsController"
                            })
                            .when
                            ("/editsystemalerts/:alertId", {
                                templateUrl: appURL + "/Scripts/app/Settings/SystemSettings/SystemAlerts/Views/EditSystemAlerts.html",
                                controller: "EditSystemAlertsController"
                            })
                            // [Start]: Losstype Module : Venkatesh Chittepu 
                             .when("/lossTypes", {
                                 templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TypeOfLoss/Views/AllLossType.html',
                                 controller: "TypeOfLossController"
                             })
                            .when("/newLossType", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TypeOfLoss/Views/NewLossType.html',
                                controller: "TypeOfLossController"
                            })
                            .when("/editLossType/:lossTypeId", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TypeOfLoss/Views/EditLossType.html',
                                controller: "ViewEditLossTypeController"
                            })
                            .when("/viewLossType/:lossTypeId", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/TypeOfLoss/Views/ViewLossType.html',
                                controller: "ViewEditLossTypeController"
                            })
                            // [End]: Losstype Module : Venkatesh Chittepu
                              .when("/fileNaming", {
                                  templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/FileNaming/Views/AllFileNameCodes.html',
                                  controller: "FileNamingController"
                              })
                             .when("/newFileNameCode", {
                                 templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/FileNaming/Views/NewFileNameCode.html',
                                 controller: "FileNamingController"
                             })
                                 .when("/editFileNamingCode/:fileNameId", {
                                     templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/FileNaming/Views/EditFileNameCode.html',
                                     controller: "ViewEditFileNamingController"
                                 })
                            .when("/viewFileNamingCode/:fileNameId", {
                                templateUrl: appURL + '/Scripts/app/Settings/SystemSettings/FileNaming/Views/ViewFileNameCode.html',
                                controller: "ViewEditFileNamingController"
                            })

                           
.otherwise({
    resolve: {
        "check": function ($location) {
            var location = $location.$$absUrl;
            if (location.split('/')[3] == "Cap") {
                $location.path("/cap");
            } else {
                $location.path("/home");
            }
        }
    }
});

                        // $routeProvider.html5Mode(true);
                    }
                ]);

cliamsManagementAppModule.controller("NavigationController", ["$rootScope", "$scope", "$location", "$http", function ($rootScope, $scope, $location, $http) {
    $scope.isActiveMenuItem = function (url, exactMatch) {
        if (exactMatch) {
            return $location.path() === url;
        }
        else {
            var path = $location.path().indexOf('/', 1) >= 0 ? $location.path().substr(0, $location.path().indexOf('/', 1)) : $location.path();
            switch (path.toUpperCase()) {
                case "/EDITCLAIM":
                case "/VIEWCLAIM":
                case "/CLAIMNOTES":
                case "/NEWCLAIMNOTE":
                case "/EDITCLAIMNOTE":
                case "/VIEWCLAIMNOTE":
                case "/TIMELOGS":
                case "/NEWTIMELOG":
                case "/VIEWTIMELOG":
                case "/EDITTIMELOG":
                    url = url === "/claims" ? path : url;
                    break;
                case "/NEWCOMPANY":
                case "/EDITCOMPANY":
                case "/VIEWCOMPANY":
                    url = url === "/companies" ? path : url;
                    break;
                case "/NEWCONTACT":
                case "/VIEWCONTACT":
                case "/EDITCONTACT":
                    url = url === "/contacts" ? path : url;
                    break;
                case "/NEWINVOICE":
                case "/VIEWINVOICE":
                case "/EDITINVOICE":
                    url = url === "/invoices" ? path : url;
                    break;
                case "/MYPROFILE":
                case "/EDITPROFILE":
                case "/VIEWPROFILE":
                case "/SETTINGS":
                case "/FILENAMING":
                    // [Start]: Losstype Module : Venkatesh Chittepu 
                case "/NEWFILENAMECODE":
                case "/EDITFILENAMINGCODE": 
                case "/VIEWFILENAMINGCODE":
                    // [END]: Losstype Module : Venkatesh Chittepu 
                case "/SERVICEITEMS":
                case "/NEWSERVICEITEM":
                case "/EDITSERVICEITEM":
                case "/VIEWSERVICEITEM":
                    // [Start]: Losstype Module : Venkatesh Chittepu 
                case "/LOSSTYPES":
                case "/NEWLOSSTYPE":
                case "/EDITLOSSTYPE":
                case "/VIEWLOSSTYPE":
                    // [END]: Losstype Module : Venkatesh Chittepu 
                case "/TAXSETTINGS":
                case "/NEWTAXSETTING":
                case "/EDITTAXSETTING":
                case "/EDITSYSTEMALERTS":
                case "/USERS":
                    url = url === "/settings" ? path : url;
                    break;
                case "/systemalerts":
                    url = url === "/settings" ? "/AllSystemAlerts" : url;
                    break;
                case "/MYCLAIMS":
                    url = url === "/myclaims" ? "/myclaims" : url;
                    break;
                case "/CAP":
                    url = url === "/myclaims" ? "/myclaims" : url;
                    break;
                case "/CLAIMDETAILS":
                    url = url === "/claimdetails" ? "/claimdetails" : url;
                    break;
            }

            //$rootScope.showProgressLoader = false;
            return $location.path().indexOf(url) == 0;
        }
    }
}]);

cliamsManagementAppModule.run(['$rootScope', '$window', function ($root, $window) {
    $root.$on('$routeChangeStart', function (e, curr, prev) {
        if (curr.$$route && curr.$$route.resolve) {
            //$window.scrollTo(0, 100);

            // Show a loading message until promises aren't resolved
            //$root.showProgressLoader = false;
        }
    });
    $root.$on('$routeChangeSuccess', function (e, curr, prev) {
        // Hide loading messagev
        //$root.showProgressLoader = false;

        if (angular.isDefined($root.$$childHead) && angular.isDefined($root.$$childHead.searchKeyword)) {
            $root.$$childHead.searchKeyword = "";
        }
    });
}]);
