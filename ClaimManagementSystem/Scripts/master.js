var protocol = window.location.protocol;
var hostName = window.location.host;
var appURL = "";
var activeArea = "";
var activeController = "";
var activePageURL = "";
var activeUserRole = "";
var reportIdentifierId = "";
var PageNavigationUrl = {};

var HttpRequestMethod = {
    GET: "GET",
    POST: "POST"
};

var HttpRequestDataType = {
    HTML: "HTML",
    JSON: "JSON"
};

var HttpRequestContentType = {
    PLAIN: "text/plain",
    HTML: "text/html",
    JSON: "application/json",
    TEXT_JSON: "text/json"
};



SetApplicationBaseURL = function (hostURL, areaName, controllerName, actionName, userRole, reportIdentifier) {

    appURL = hostURL;
    if (reportIdentifier != undefined) {
        reportIdentifierId = reportIdentifier.toString();
    }

    if (appURL.length > 0 && appURL.substr(appURL.length - 1, 1) === "/")
        appURL = appURL.substr(0, appURL.length - 1);

    activeArea = areaName;
    activeController = controllerName;

    if (areaName != null && areaName !== "") {
        activePageURL = appURL + "/" + activeArea + "/" + activeController + "/" + actionName;
    }
    else {
        activePageURL = appURL + "/" + activeController + "/" + actionName;
    }
    activeUserRole = userRole;

    IntilizePageUrl();

};

// intilizePageUrl: For getting the absolute URL
IntilizePageUrl = function () {

    PageNavigationUrl = {
        Exception: {
            LogException: appURL + '/' + (activeArea != "" ? activeArea + '/' : '') + activeController + '/LogAjaxException'
        },
        Account: {
            Login: appURL + '/Account/Login',
            ForgotPassword: appURL + '/Account/ForgotPassword',
            ResetPassword: appURL + '/Account/ResetPassword',
            ChangePassword: appURL + '/Account/ChangePassword'
        },
        Home: appURL + '/Home/Index',
        CAP:appURL+'/CAP/Index',
        PipelineAnalyser: {
            CompetitionRadar: appURL + "/TherapeuticArea/PipelineAnalyzer/Index/" + reportIdentifierId,
            CompetitionRadarGetDataForBullseye: appURL + "/TherapeuticArea/PipelineAnalyzer/GetDataForBullsEyeChart/" + reportIdentifierId,
            DownloadBullseyeChart: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadBullseyeChart",
            DownloadHeatMap: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadHeatMapChart",
            DrugLaunchTimeline: appURL + "/TherapeuticArea/PipelineAnalyzer/GetDrugLaunchTimeLines/" + reportIdentifierId,
            CreateDrugLaunchTimeline: appURL + "/TherapeuticArea/PipelineAnalyzer/CreateBymDrugLaunchTimeLines",
            DownloadDrugLaunchTimeline: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadDrugLaunchTimeLineChart",
            DownloadPoD: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadPhaseOfDevelopmentChart",
            DownloadLoT: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadLineOfTherapyChart",
            DownloadPartnershipChart: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadPartnershipChart",
            DownloadDrugClassChart: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadDrugClassChart",
            DownloadRoA: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadRoAChart",
            DownloadPhasebyDrugClass: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadPhasebyDrugClassChart",
            DownloadPhasebyLoT: appURL + "/TherapeuticArea/PipelineAnalyzer/DownloadPhasebyLoTChart",
            UpgradeServiceViewModelData: appURL + "/TherapeuticArea/PipelineAnalyzer/UpgradeService/" + reportIdentifierId,
            UpgradeServiceViewModelPostData: appURL + "/TherapeuticArea/PipelineAnalyzer/UpgradeService"

        },
        ProductPositioningProfiler: {
            BrandProfilers: appURL + '/TherapeuticArea/ProductPositioningProfiler/GetBrandProfilerData?IndicationId=' + reportIdentifierId,
            PositioningStrategy: appURL + '/TherapeuticArea/ProductPositioningProfiler/RedirectPositioningStrategy?encryptedIndicationId=' + reportIdentifierId,
            LabelInformation: appURL + '/TherapeuticArea/ProductPositioningProfiler/RedirectLabelInformation?encryptedIndicationId=' + reportIdentifierId,
            GetClinicalTrialInformation: appURL + '/TherapeuticArea//ProductPositioningProfiler/GetClinicalTrialInformation',
            GetClinicalTrialFullInformation: appURL + '/TherapeuticArea//ProductPositioningProfiler/GetClinicalTrialFullInformation',
            GetLabelFilter: appURL + '/TherapeuticArea//ProductPositioningProfiler/GetLabelFilter',
            GetPositioningMatrix: appURL + '/TherapeuticArea/ProductPositioningProfiler/GetPositioningStrategymatrix/' + reportIdentifierId,
            GetCommunicationStrategy: appURL + '/TherapeuticArea/ProductPositioningProfiler/GetCommunicationAdvantageGrid/' + reportIdentifierId,
            GetCompetitiveAdvantage: appURL + '/TherapeuticArea/ProductPositioningProfiler/GetCompetitiveAdvantageGrid/' + reportIdentifierId,
            GetRelativeStrategyPosition: appURL + '/TherapeuticArea/ProductPositioningProfiler/GetRelativeStrategicPosition/' + reportIdentifierId,
            DownloadChart: appURL + '/TherapeuticArea/ProductPositioningProfiler/DownloadLabelsChart/',
            UpgradeServiceViewModelData: appURL + "/TherapeuticArea/ProductPositioningProfiler/GetUpgradeServiceViewModelData/" + reportIdentifierId,
            UpgradeServiceViewModelPostData: appURL + "/TherapeuticArea/ProductPositioningProfiler/UpgradeService"
        }
    }
}


// Navigation Active Class Funcion
HighlightNavigationMenu = function (route, defaultArea, defaultController, defaultAction) {
    //var hrefElement = $("#AuthUserNavigationMenu li").find("a[href = '" + route + "']");

    $(".Navigation li a").each(function (index, elem) {
        var href = $(this).attr("href");
        
        if (href === "" || href === "/") {
            if (defaultArea != null && defaultArea !== "") {
                href = appURL + "/" + defaultArea + "/" + defaultController + "/" + defaultAction;
            }
            else {
                href = appURL + "/" + defaultController + "/" + defaultAction;
            }
        }
        else if (href.indexOf(appURL) < 0) {
            href = appURL + href;
        }

        if (href.toLowerCase() === route.toLowerCase()) {
            var parentListElement = $(this).parent();
            if (parentListElement != undefined) {
                $(parentListElement).addClass("act");
            }
        }
    });
};

ShowPageLoader = function () {
    $("#loaderDiv").show();
};

HidePageLoader = function () {
    $('#loaderDiv').hide("slow");
};

ProcessAjaxRequest = function (targerUrl, requestMethod, dataType, contentType, inputData, failureMessageContainer) {
    var responseData = {};

    $.ajax({
        url: targerUrl,
        async: false,
        cache: false,
        type: requestMethod,
        dataType: dataType,
        contentType: contentType,
        data: inputData,
        beforeSend: function () {
            ShowPageLoader();
        },
        success: function (data, status) {
            responseData = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (failureMessageContainer != null && $(failureMessageContainer).length > 0) {
                $.DisplayFailureMessage(jqXHR, textStatus, errorThrown, failureMessageContainer);
            }
            else {
                $.redirect(PageNavigationUrl.Exception.LogException, { ajaxErrorMessage: errorThrown }, HttpRequestMethod.POST);
            }
        },
        complete: function () {
            HidePageLoader();
        }
    });

    return responseData;
};

//function AddRemoveClassOnSelection() {

//    if ($('.inp label').length > 0) {
//        $('.inp label>input[type=checkbox]').each(function () {
//            if ($(this).prop('checked') === true) {
//                $(this).parent().addClass("Pink BGGreyLight bold");
//            } else {
//                $(this).parent().removeClass("Pink BGGreyLight bold");
//            }
//        });
//    }
//};

//calculating Filter width
var myWidth

var myWidth2
var ParentWidth
//storing left position
var leftPosition

function calmyWidth() {
    if (myWidth < 115) {
        leftPosition = ParentWidth - $(".FilterClick").height()
    }
    else {
        leftPosition = (ParentWidth + 5) - (myWidth2 / 2)
    }
}

$(window).load(function () {
    myWidth = $(".FilterClick:visible").width() + 20

    myWidth2 = $(".FilterClick:visible").width()
    ParentWidth = $(".FilterRelative:visible").width()
    calmyWidth()

    if ($(".FilterClick").length > 0) {
        calmyWidth()
        $(".FilterClick").width($("FilterClick").width()).css({ "left": leftPosition })
    }
})



// Function that will execute on window's scroll
$(window).scroll(function () {

    filterPositiononScroll();

    compareBoxscroll();
});

// filterPositiononScroll - For moving up/down of filters acoording to scroll
var isMobile = (/Android|iPhone|iPad|iPod|BlackBerry|IEMobile|Windows Phone/i.test(navigator.userAgent)) ? true : false;
function filterPositiononScroll() {

    winScroll = $(window).scrollTop()
    myPos = 100

    if (isMobile == false) {
        if (winScroll > myPos) {
            $(".FilterRelative").stop().animate({
                marginTop: winScroll - 180
            }, "slow");
        } else {
            $(".FilterRelative").stop().animate({
                marginTop: 0
            });
        }
    }
    else {
        if ($(".FilterRelative").offset().left < 0) {
            if (winScroll > myPos) {
                $(".FilterRelative").stop().animate({
                    marginTop: winScroll - 180
                }, "slow");
            } else {
                $(".FilterRelative").stop().animate({
                    marginTop: 0
                });
            }
        }
    }
}

//For scrolling of compare box 
function compareBoxscroll() {

    if ($('#LabelBox').length > 0) {

        var window_top = $(window).scrollTop();
        var div_top = $('#stickON').offset().top;

        if (window_top > div_top) {
            $('#LabelBox').addClass('stick')
        } else {
            $('#LabelBox').removeClass('stick')
        }
    }
}



$(function () {

    $.ajaxSetup({
        cache: false,
        async: false
    });

    //User agent test for Setting Click function
    var userAgent = window.navigator.userAgent.toLowerCase(),
    ios = /iphone|ipod|ipad/.test(userAgent);

    if (ios) {
        $(".quickLink").click(function (event) {
            $(this).children("ul").toggle()
            event.stopPropagation();
        })

        $(".infoTip").click(function (event) {
            $(this).children(".iThover").toggle()
            event.stopPropagation();
        })
        $('html,body,.container').click(function () {
            if ($(".infoTip").children(".iThover").is(":visible")) {
                $(".infoTip").children(".iThover").hide()
            }
            if ($(".quickLink").children("ul").is(":visible")) {
                $(".quickLink").children("ul").hide()
            }

        });
    }

    $(".flyClick").click(function () {

        alert('hello');
        $(this).closest("label").toggleClass("BGPink White")
        //console.log("clicked")

        imgURL = $(this).closest(".row").find("img").attr("src")


        Brand = imgURL.substring(imgURL.lastIndexOf('/') + 1);


        if ($(this).closest(".rowBrand").find("input").is(":checked")) {

            //animation for fly
            flyLeftPos = $(this).closest(".rbBlock").position().left
            flyTopPos = $(this).closest(".rbBlock").position().top



            $(this).closest(".rbBlock").clone().addClass("fly").css({ "left": flyLeftPos, "top": flyTopPos }).appendTo($(this).closest(".rowBrand")).animate({
                top: -150, left: 0
            },

                function () {
                    $(this).closest(".container").find("#LabelBox").find("img").attr("src", imgURL)
                    if (Brand == "Tecfidera-logo.png" || Brand == "Gilenya-logo.png" || Brand == "gsk-logo.png") {
                        $(this).closest(".container").find("#LabelBox").find(".BrandSelect").find("i.glyphicon").removeClass("glyphicon-remove-sign red").addClass("glyphicon-ok-sign green")
                    }
                    else {
                        $(this).closest(".container").find("#LabelBox").find(".BrandSelect").find("i.glyphicon").removeClass("glyphicon-ok-sign green").addClass("glyphicon-remove-sign red")
                    }
                    $(this).animate({
                        opacity: 0
                    }, function () {
                        $(this).closest("body").find(".fly").remove()
                    })
                })
        }
        else {
            $("#LabelBox").hide()
            $(this).closest("body").find(".fly").remove()
            $(this).closest("label").toggleClass("BGPink White")
        }



    });

    $.fn.clearErrors = function () {
        $(this).each(function () {
            $(this).trigger('reset.unobtrusiveValidation');
        });
    };

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $.fn.showRemainingCharacter = function (options) {
        $(this).each(function (index, elem) {
            $(elem).after("<div id=spnCharacterCount" + $(elem)[0].id + " class='size12'></div>");
            
            if (elem.hasAttribute("showlengthmessage") && elem.attributes.showlengthmessage.value.toString().toLowerCase() === "true")
                countCharacters(elem);
            $(elem).on("change keyup input paste", function () {
                if (elem.hasAttribute("showlengthmessage") && elem.attributes.showlengthmessage.value.toString().toLowerCase() === "true")
                    countCharacters(elem);
            });
            //$(elem).on("blur", function () {
            //    $(elem).next("span[id=spnCharacterCount" + $(elem)[0].id + "]").each(function () {
            //        $(this).attr("style", "display:none;");
            //        $(this)[0].innerText = "";
            //    });
            //});
        });

        //display characters left for max length elements - start
        function countCharacters(control) {
            $(control).next("div[id=spnCharacterCount" + control.id + "]").each(function () {
                var maxLength = control.maxLength;//$(control).attr("showlengthmessage"); //maxLength;
                var txtLength = control.value.length;
                $(this)[0].innerText = "Character(s) left: " + (parseInt(maxLength) - txtLength);
            });
        }
        //display characters left for max length elements - end
    }



    //Remove tags
    //if ($(".removeME").length > 0) {
    //    $(".removeME").click(function() {
    //        $(this).fadeOut(300).remove();
    //    });
    //}

    //Filter Show Hide
    if ($(".FilterClick").length > 0) {
        $(".FilterClick").click(function () {

            calmyWidth()

            if ($(this).parent().offset().left < 0) {
                $(this).animate({ left: 0, top: -29, width: ParentWidth }, 150).css({
                    "transform": "rotate(0)"
                })
                $(this).parent().animate({
                    left: 0
                }, 150)
                $(this).children().removeClass("glyphicon-plus-sign").addClass("glyphicon-minus-sign")
            }
            else {
                $(this).animate({ left: leftPosition, top: 50, width: myWidth }, 150).css({
                    "transform": "rotate(90deg)"
                })
                $(this).parent().animate({
                    left: -ParentWidth
                }, 150)
                $(this).children().removeClass("glyphicon-minus-sign").addClass("glyphicon-plus-sign")
            }
        })
    }



    $('html').click(function () {
        ParentWidth = $(".FilterRelative:visible").width()
        calmyWidth()

        $(".FilterClick:visible").animate({ left: leftPosition, top: 50, width: myWidth }, 150).css({
            "transform": "rotate(90deg)"
        })
        $(".FilterClick:visible").closest(".FilterRelative:visible").animate({
            left: -ParentWidth
        }, 150)
        $(".FilterClick:visible").children().removeClass("glyphicon-minus-sign").addClass("glyphicon-plus-sign")
    });


    $('.FilterRelative').click(function (event) {
        event.stopPropagation();
    });

    // custome slider for launch timeline
    //if ($('.span2').length > 0) {
    //    $('.span2').slider()
    //}

    //My Report Filterd view
    if ($('.ShowHide').length > 0) {
        $('.ShowHide').click(function () {
            $('body').find('.table1').slideUp()
            $('body').find('.table2').slideDown()
            $('html').click()
        })
    }

    if ($('.ReverseHS').length > 0) {
        $('.ReverseHS').click(function () {
            $('body').find('.table1').slideDown()
            $('body').find('.table2').slideUp()
            $('html').click()
        })
    }

    //Remove Table row
    if ($('.RemoveRow').length > 0) {
        $('.RemoveRow').click(function () {
            if ($(this).parent().is("td")) {
                $(this).closest("tr").remove()
            }
            else {
                $(this).closest("label").remove()
            }
        })
    }

    //Molecule Click Launch Timeline
    if ($('.CheckBox').length > 0) {
        $('.CheckBox').click(function () {
            var myText = $(this).parent().text()
            if ($(this).closest(".FilterMain").find("td:contains(" + myText.trim() + ")").closest("tr").is(':visible')) {
                $(this).closest(".FilterMain").find("td:contains(" + myText.trim() + ")").closest("tr").hide()
            }
            else {
                $(this).closest(".FilterMain").find("td:contains(" + myText.trim() + ")").closest("tr").show()
            }
        })
    }

    //Brand Selection
    if ($('.AddCompare').length > 0) {
        $('.AddCompare').click(function () {
            alert('hello');
            myName = $(this).val()
            $("#" + myName + "").toggle()

            if ($(this).closest(".rowBrand").find("input").is(":checked")) {
                $("#CompareBox").slideDown()
            }
            else {
                $("#CompareBox").slideUp()
            }
        })
    }

    if ($('.removeBrand').length > 0) {
        $('.removeBrand').click(function () {
            $(this).closest(".BrandSelect").hide()

            myid = $(this).closest(".BrandSelect").attr("id")
            $(this).closest("body").find(".rowBrand").find("input[value=" + myid + "]").prop('checked', false);

            console.log(myid)

            if ($(this).closest("body").find(".rowBrand").find("input").is(":checked")) {
                $("#CompareBox").show()
            }
            else {
                $("#CompareBox").hide()
            }
        })
    }




    //Basic Information Accordien - for comparision as per v8
    if ($('.BAccordHeading').length > 0) {
        $('.BAClick').closest(".mainBasicAccord").click(function () {
            $(this).find(".BAccordContent").slideToggle()
            if ($(this).find(".mainBasicAccord").find("a").children().hasClass("glyphicon-triangle-bottom")) {
                $(this).find(".mainBasicAccord").find("a").children().removeClass("glyphicon-triangle-bottom").addClass("glyphicon-triangle-top")
            }
            else {
                $(this).find(".mainBasicAccord").find("a").children().removeClass("glyphicon-triangle-top").addClass("glyphicon-triangle-bottom")
            }
        })
    }

    //accordHeading click as per V8 - for monographic 
    if ($('.accordHeading').length > 0) {
        $('.accoClick').closest(".accordHeading").click(function () {
            $(this).closest(".accordion").find(".accordContent").slideToggle()
            $(this).closest(".accordion").find(".accordHeading").toggleClass("accordToggle")
            if ($(this).find('.accoClick').children().hasClass("glyphicon-triangle-bottom")) {
                $(this).find('.accoClick').children().removeClass("glyphicon-triangle-bottom").addClass("glyphicon-triangle-top")
            }
            else {
                $(this).find('.accoClick').children().removeClass("glyphicon-triangle-top").addClass("glyphicon-triangle-bottom")
            }
        })
    }



    $('body').on("click", "input[class*='moredetails']", function () {
        $("body").find(".TrialDetail").slideDown("slow")
        $('html,body').animate({
            scrollTop: $("body").find(".TrialDetail:visible").offset().top
        }, 'slow');
    })


    $("body").on("click", "input[class*='hidedetails']", function () {

        $("body").find(".TrialDetail").slideUp("slow")
        $('html,body').animate({
            scrollTop: $("body").find(".CTI:visible").offset().top
        }, 'slow');
    })

    //datepicker
    if ($('.datepicker').length > 0) {
        $('.datepicker').datepicker().on('changeDate', function (ev) {
            $('.datepicker').datepicker('hide');
        });
    }

    //custome Class for selection
    if ($('.inp label').length > 0) {
        $('.inp label>input[type=checkbox]').click(function () {
            $(this).parent().toggleClass("Pink BGGreyLight bold")
        })
    }

    if ($('.bulleye').length > 0) {
        $('.bulleye').click(function () {
            if ($("#Phase").is(":checked") && $("#Launched").is(":checked") && $("#MoA").is(":not(:checked)") && $("#LoT").val() !== "2L") {
                $(".table2").show()
                $(".table1, .table3, .table4").hide()
            }
            else if ($("#Phase").is(":checked") && $("#Launched").is(":checked") && $("#MoA").is(":not(:checked)") && $("#LoT").val() == "2L") {
                $(".table3").show()
                $(".table1, .table2, .table4").hide()
            }
            else if ($("#Phase").is(":checked") && $("#Launched").is(":checked") && $("#MoA").is(":checked") && $("#LoT").val() == "2L") {
                $(".table4").show()
                $(".table1, .table3, .table2").hide()
            }
            $(".FilterClick").animate({ left: 402, top: 50, width: 230 }, 150).css({ "transform": "rotate(90deg)" }).removeClass("FClose")
            $(".FilterRelative").animate({
                left: -500
            }, 150)
            $(".FilterRelative").find(".FilterClick").children().removeClass("glyphicon-minus-sign").addClass("glyphicon-plus-sign")
        })
    }

    //Login Slider
    if ($('.carousel').length > 0) {
        $('.carousel').carousel({
            interval: 5000, //changes the speed
            pause: "false"
        })
    }

    if ($('.insight').length > 0) {
        $('.insight').click(function () {
            $('html,body').animate({
                scrollTop: $(".insightDetail").offset().top
            }, 'slow');
        })
    }

    //tooltip
    if ($('[data-toggle="tooltip"]').length > 0) {
        $('[data-toggle="tooltip"]').tooltip()
    }

    //PPP comparision Sticky
    if ($("body").find('#stickyOn').length > 0) {
        $(window).scroll(function () {
            var window_top = $(window).scrollTop();
            var div_top = $('#stickyOn').offset().top;
            if ($(".accordContent").is(":visible")) {
                if (window_top > div_top) {
                    $('#Sticky').addClass('stick').fadeIn();
                } else {
                    $('#Sticky').removeClass('stick').hide();
                }
            }
        });
    }

    //My folder show hide top content
    if ($(".HomeClick").length > 0) {
        $(".HomeClick").click(function () {
            if ($(".HomeTop").is(":visible")) {
                $(".HomeTop").slideUp()
                $(this).animate({ top: 0 })
                $(this).children().removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down")
            }
            else {
                $(".HomeTop").slideDown()
                $(this).animate({ top: -90 })
                $(this).children().removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up")
            }

        })
    }

    // My folder 
    if ($(".mFcols").length > 0) {
        $(".mFcols").click(function () {
            $('html,body').animate({ scrollTop: $(".MyFolderMain").offset().top }, 'slow');

            myId = $(this).attr("name")

            $(this).closest("body").find(".ng-scope").find(".MFMrHead").removeClass("act")
            $(this).closest("body").find(".ng-scope").find(".MFMrContent").slideUp()
            $(this).closest("body").find(".ng-scope").find("#" + myId).find(".MFMrHead").addClass("act")
            $(this).closest("body").find(".ng-scope").find("#" + myId).find(".MFMrContent").slideDown()

        })
    }

    // My folder 
    $('body').on("click", ".MFMrHead", function () {

        if ($(this).closest(".MFMrow").find(".MFMrContent").is(":visible")) {
            $(this).closest(".MFMrow").find(".MFMrContent").slideUp()
            $(this).removeClass("act")
        }
        else {
            $(this).closest(".MFMrow").find(".MFMrContent").slideDown()
            $(this).addClass("act")
        }

    })
});


//deffered function
function deffredHTMLtwoCanvas(element) {

    var def = $.Deferred();
    var imgSrc = '';


    html2canvas($(element), {

        onrendered: $.when(function (renderedCanvas) {

            alert('def hello first');
            imgSrc = renderedCanvas.toDataURL("image/png");

            //imageTorenderOnCanvas.src = renderedCanvas.toDataURL("image/png");
            //return def.promise();
        }).then(function () {

            alert('done second');
            def.resolve(imgSrc);

        })
    })

    //if (imgSrc != '') {

    //    def.resolve(imgSrc);
    //}
    //else {

    //    def.reject('Error occurred');
    //}

    return def.promise();
}


function getImage(element, imageTorenderOnCanvas, count) {

    var def = $.Deferred();
    var convertedImage;

    html2canvas($(element), {

        onrendered: $.when(function (canvas) {
            //var ctx = canvas.getContext("2d");
            //ctx.imageSmoothingEnabled = false;
            convertedImage = $when(canvas.toDataURL('image/jpg')).done(

                function () {

                    alert('onrendered');
                    imageTorenderOnCanvas.src = convertedImage;
                });

            // imageTorenderOnCanvas.onload = function () {

            //}

        }).done(function () {

            def.resolve(imageTorenderOnCanvas);
        })

    });

    return def.promise();
}


function mainDeffred(canvas, chartContainer) {

    var d = $.Deferred();
    var count = 1;

    var yCoordinateToDrawImage = 0;
    var context = $(canvas)[0].getContext('2d');
    var canvasforTempImage = document.createElement('canvas');


    $(chartContainer).children().each(function (index, element) {


        var contextforTempImage; // = canvasforTempImage.getContext('2d');
        var imageTorenderOnCanvas = new Image();
        var htmlImageData;


        if ($(element).find("svg").length > 0) {
            var svgElement = $(element).find("span[class*='fusioncharts-container']");

            $(svgElement).find("svg").each(function () {
                $(this).removeAttr("xmlns");
            });

            var svgContent = svgElement.html().trim();

            if (!$(svgContent)[0].hasAttribute("xmlns")) {
                $(svgElement).find("svg").each(function () {
                    $(this).attr("xmlns", "http://www.w3.org/2000/svg");
                });

                svgContent = svgElement.html().trim();
            }

            canvg(canvasforTempImage, svgContent);
            imageTorenderOnCanvas.src = canvasforTempImage.toDataURL("image/png");

            context.drawImage(imageTorenderOnCanvas, 0, yCoordinateToDrawImage);
            yCoordinateToDrawImage += element.clientHeight;
        }
        else {


            var _deffun = getImage(element, imageTorenderOnCanvas, count);
            _deffun.done(function (image) {

                context.drawImage(imageTorenderOnCanvas, 0, yCoordinateToDrawImage);
                yCoordinateToDrawImage += element.clientHeight;
                count += 1;

                if (count == 3) {

                    d.resolve('done');
                }
            });
        }

    });


    return d.promise();

}

var htmltoimage = function (chartContainer, containerHeight, containerWidth) {

    var canvas = $("#svg-canvas");
    var context = $(canvas)[0].getContext('2d');

    $(canvas).attr("width", containerWidth || $(chartContainer)[0].clientWidth);
    $(canvas).attr("height", containerHeight || $(chartContainer)[0].clientHeight);


    var canvasforTempImage = document.createElement('canvas');
    var yCoordinateToDrawImage = 0;

    $(chartContainer).children().each(function (index, element) {

        var imageTorenderOnCanvas = new Image();
        var svgElement;

        if ($(element).find("svg").length > 0) {

            if ($(element).find("span[class*='fusioncharts-container']").length > 0) {
                svgElement = $(element).find("span[class*='fusioncharts-container']").length > 0
                                   ? $(element).find("span[class*='fusioncharts-container']")
                                   : $(element).parent().find("span[class*='fusioncharts-container']");
            }
            else {
                svgElement = $(element);
            }

            $(svgElement).find("svg").each(function () {
                $(this).removeAttr("xmlns");
            });


            var svgContent = svgElement.html().trim();

            if (!$(svgContent)[0].hasAttribute("xmlns")) {
                $(svgElement).find("svg").each(function () {
                    $(this).attr("xmlns", "http://www.w3.org/2000/svg");
                });

                svgContent = svgElement.html().trim();
            }

            canvg(canvasforTempImage, svgContent);
            imageTorenderOnCanvas.src = canvasforTempImage.toDataURL("image/png");

            context.drawImage(imageTorenderOnCanvas, 0, yCoordinateToDrawImage);
            yCoordinateToDrawImage += element.clientHeight;
        }
        else {
            html2canvas($(element), {
                onrendered: function (canvas) {
                    imageTorenderOnCanvas.src = canvas.toDataURL('image/png');

                    context.drawImage(imageTorenderOnCanvas, 0, yCoordinateToDrawImage);
                    yCoordinateToDrawImage += element.clientHeight;
                }
            });
        }
    });

    return canvas[0].toDataURL('image/png');
};

var drawImageOnCanvas = function (element) {
    var promise = $.Deferred();

    var canvasforTempImage = document.createElement('canvas');
    var imageTorenderOnCanvas = new Image();
    var svgElement;

    html2canvas($(element), {
        async: false,
        onrendered: function (canvas) {
            if ($(element).find("svg").length > 0) {

                if ($(element).find("span[class*='fusioncharts-container']").length > 0) {
                    svgElement = $(element).find("span[class*='fusioncharts-container']").length > 0
                                       ? $(element).find("span[class*='fusioncharts-container']")
                                       : $(element).parent().find("span[class*='fusioncharts-container']");
                }
                else {
                    svgElement = $(element);
                }

                $(svgElement).find("svg").each(function () {
                    $(this).removeAttr("xmlns");
                });


                var svgContent = svgElement.html().trim();

                if (!$(svgContent)[0].hasAttribute("xmlns")) {
                    $(svgElement).find("svg").each(function () {
                        $(this).attr("xmlns", "http://www.w3.org/2000/svg");
                    });

                    svgContent = svgElement.html().trim();
                }

                canvg(canvasforTempImage, svgContent);
                imageTorenderOnCanvas.onload = function () {

                    // pass the loaded image along with the promise
                    promise.resolve(imageTorenderOnCanvas);
                }
                imageTorenderOnCanvas.src = canvasforTempImage.toDataURL("image/png");
            }
            else {

                imageTorenderOnCanvas.onload = function () {

                    // pass the loaded image along with the promise
                    promise.resolve(imageTorenderOnCanvas);
                }
                imageTorenderOnCanvas.src = canvas.toDataURL('image/png');
            }
        }
    });

    return promise;
};


var drawSvgOnCanvas = function (element) {

    var imageCanvas = document.createElement('canvas');
    var svgElement;

    if ($(element).find("span[class*='fusioncharts-container']").length > 0) {
        svgElement = $(element).find("span[class*='fusioncharts-container']").length > 0
                           ? $(element).find("span[class*='fusioncharts-container']")
                           : $(element).parent().find("span[class*='fusioncharts-container']");
    }
    else {
        svgElement = $(element);
    }

    $(svgElement).find("svg").each(function () {
        $(this).removeAttr("xmlns");
    });

    var svgContent = svgElement.html().trim();
    if (!$(svgContent)[0].hasAttribute("xmlns")) {
        $(svgElement).find("svg").each(function () {
            $(this).attr("xmlns", "http://www.w3.org/2000/svg");
        });

        svgContent = svgElement.html().trim();
    }

    canvg(imageCanvas, svgContent);
    return imageCanvas;
};
