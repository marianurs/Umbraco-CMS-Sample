"use strict";

//* VARIABLES *//
var screenSm = 768;
var screenMd = 1024;
var screenLg = 1200;

var fontsizeScale = 100;
var fontsizeMin = 12;

var screeXsMax = screenSm - 1;
var screeSmMax = screenMd - 1;
var screeMdMax = screenLg - 1;
var _stationList = [];

var devicewidth = window.outerWidth;
var tabletwidth = 1024;
// var desktopwidth = 1024;

var parentElem;
var src_d, src_new, JPGelem, JPGelemThis;

var $navbarMainMenuContainer = $("#bs-example-navbar-collapse-1").parents(".navbar");
var $navbarMainMenuHeader = $navbarMainMenuContainer.find(".navbar-header");
var cookieValue, contrastMemoryValue, cookieCont, cookieElem;

cookieCont = $(".hpm-js-alertcont-cookies");
cookieElem = $(".hpm-js-alert-cookies");



$(document).ready(function() {

    $('.js-font--up').click(fontScaleUp);
    $('.js-font--down').click(fontScaleDown);

    pickers();
    $('.js-top').click(slideToTop);
    //* Akcept cookies *//
    cookieValue = $.cookie("AcceptCookies");
    if (cookieValue == 1) { // 1 - acepted by user,
        cookieRemove();
    } else {
        cookieShow();
    }
    if (document.getElementById('routemaps')) {

        setTimeout(function() {
            initGoogle();
        }, 10)
    }

    $(".hpm-js-alert-cookies button").on('click', function() {
        ///$.cookie("AcceptCookies", 0); // for test AcceptCookies = 0 
        $.cookie("AcceptCookies", 1, {
            expires: 30,
            path: '/'
        });
        cookieRemove();
    });
    showpassword();
    listHeightFix();
    filterAjaxForm();
    selectStylish();
    getSvg();
    expandNavbarHeader();

    //* change fixed-menu on mobile/desktop on resize *//
    $(window).resize(function() {
        if (window.outerWidth > tabletwidth) {
            addFixedMenuClass();
        } else {
            removeFixedMenuClass();
        }
        listHeightFix();
    });


    // do on desktop only (window.size > tabletwidth ) 
    if (devicewidth >= tabletwidth) {
        /* tooltip */
        //footerSticky();
        $('[data-toggle="tooltip"]').tooltip();

        //* change fixed-menu on desktop *//
        addFixedMenuClass();

        //* change logo when element menu click *//
        $('#bs-example-navbar-collapse-1 .dropdown').on('shown.bs.dropdown', function() {
            collapseNavbarHeader();
        })
        $('#bs-example-navbar-collapse-1 .dropdown').on('hidden.bs.dropdown', function() {
            expandNavbarHeader();
        })

        //* change logo and wcag-bar when scroll page *//

        $(".top-bar").headroom();

        //* add special class to sumbenu with category *//
        $(".hpm-js-subnav-list>li>div").parent("li").addClass("subnav-list-subnav");
    }
    // do on mobile 
    else {
        //* main menu on mobile isn't fixed! *//
        removeFixedMenuClass();
    }

    /* WCAG - Contrast */
    contrastMemoryValue = $.cookie("contrast");
    $(".js-font--contrast").on('click', function() {
        if ($("body").hasClass("hpm-change-contrast")) {
            $.removeCookie('contrast', {
                path: '/'
            });
            $("body").removeClass("hpm-change-contrast");
        } else {
            $("body").addClass("hpm-change-contrast");
            $.cookie("contrast", 1, {
                expires: 30,
                path: '/'
            });
        }
        if(googlemod&&googleservice&&googleservice.map){
            googlemod.swapcolor();
        }
    });
    //* save contrast in cookie *//
    if (contrastMemoryValue == 1) { // 1 - acepted by user, 
        $("body").addClass("hpm-change-contrast");
    } else {
        $("body").removeClass("hpm-change-contrast");
    }
    /* WCAG - Font size change */
    $(".hpm-js-wcag-biggest-text").on('click', function() {
        var zoom = $("body").css("zoom");
        //console.log(zoom);
        zoom = parseFloat(zoom);
        zoom = zoom + 0.25;
        var newzoom = zoom;
        if (newzoom >= 1.5) {
            newzoom = 1.5;
        }
        $("body").css("zoom", newzoom);
    });
    $(".hpm-js-wcag-smaller-text").on('click', function() {
        var zoom = $("body").css("zoom");
        zoom = parseFloat(zoom);
        zoom = zoom - 0.25;
        var newzoom = zoom;
        if (newzoom <= 1) {
            newzoom = 1;
        }
        $("body").css("zoom", newzoom);
    });

    setAdrertisngOfSalesDateTimePickers();

    slider();

    $('.hpm-js-erase-cookie').click(function() {
        var cookieName = $(this).attr('data-cookie');
        $.removeCookie(cookieName, {
            path: '/'
        });
    });
    $('.editbutton:not(.agreements)').on('click', function() {

        $(".inputsorfields:not(.agreements)").addClass("shown")
    })
    $('.editbutton.agreements').on('click', function() {

        $(".inputsorfields.agreements").toggleClass("shown")
    })
    if ($('.toprow')[0]) {
        $('.toprow').on('click', function(e) {
            $(this).parent().toggleClass('open')
        })
    }
    $('.closeover').on('click', function(e) {
        e.preventDefault();
        setCookie('overlay-closed', 'true', 10)
        $(".hpm-overlay").hide(200)
    })

    function onblur1() {
        alert("Now!")
    }

    $('.closeall').on('click', function(e) {

        setCookie('overlay-closed', 'true', 10)
        $(".hpm-overlay").hide(200)
    })
    $('.closeall .container').on('click', function(e) {
        e.stopPropagation();
    })

    $('.closealert').on('click', function(e) {
        e.preventDefault();

        $("#topalert").hide(200)
        clearInterval(scroller);
    })
    $(document).on({
        'DOMNodeInserted': function() {
            $('.pac-item, .pac-item span', this).addClass('needsclick');
        }
    }, '.pac-container');
    // var scroller;
    // if ($("#topalert")) {
    //     var temp1 = $("#topalert .h1container h1").width();
    //     var temp2 = $("#topalert .h1container").width();
    //     var temp3 = 100;
    //     // alert(temp1+"   "+temp2)
    //     if (temp1 > temp2) {
    //         scroller = setInterval(function() {
    //             $("#topalert .h1container h1").animate({
    //                 "margin-left": "-350%"
    //             }, 17000, "linear", function() {
    //                 $("#topalert .h1container h1").css("margin-left", "105%")
    //             });


    //         }, 17000);
    //     }

    // }


    getFormCookie();
    checkFBLogin();
    if ($(".showonmap")) {
        $(".showonmap").on("click", function(event) {
            event.preventDefault();
            //console.log("now")
            $("#routemaps").parents(".hpm-search").toggleClass("open")
        })
    }
    if ($('#popupinfo')) {
        var temp = '<div type="button" class="close hpm-btn-close-modal-desktop"aria-label="Close"><span aria-hidden="true">×</span></div>'
        $(temp).prependTo("#popupinfo");
        $('#popupinfo').prependTo(".dropdown:nth-of-type(6)")
        $('#popupinfo').show();
        // setTimeout(function() {
        $('#popupinfo div').on("click", function() {
            $('#popupinfo').hide(200)
        });
        // }, 3000)
    }
    // if ($('.hideaftermoment')) {
    //     setTimeout(function() {
    //         $('.hideaftermoment').hide(200);
    //     }, 3000)
    // }

});
$(window).load(function() {
    if ($('#map-svg').length > 0) {
        svgPZ();
    }

});
//* FUNCTIONS *//
function svgPZ() {
    var svgElement = document.querySelector('#map-svg')
    var options = {
        panEnabled: true,
        controlIconsEnabled: false,
        zoomEnabled: true,
        dblClickZoomEnabled: true,
        mouseWheelZoomEnabled: false,
        preventMouseEventsDefault: true,
        zoomScaleSensitivity: 0.5,
        minZoom: 1,
        maxZoom: 10,
        fit: true,
        contain: false,
        center: true,
        refreshRate: 'auto'
    }
    var panZoomMap = svgPanZoom(svgElement, options);
    $('.hpm-wiremap__map__button--zoom-in').click(function() {
        panZoomMap.zoomIn();

        panZoomMap.center();
    });
    $('.hpm-wiremap__map__button--zoom-out').click(function() {
        panZoomMap.zoomOut();

        panZoomMap.center();
    })

}

function triggereditbuttons() {
    $('.editbutton:not(.agreements)').on('click', function() {

        $(".inputsorfields:not(.agreements)").addClass("shown")
    })
    $('.editbutton.agreements').on('click', function() {

        $(".inputsorfields.agreements").toggleClass("shown")
    })
}

function bipFormScroll() {
    if ($('.input-validation-error').length > 0) {
		if($('.input-validation-error:first-of-type')[0]){
        var top = $('.input-validation-error:first-of-type').offset().top - $('.navbar-collapse').height();

        $("html, body").animate({
            scrollTop: top
        });
		}
    }
}

function showpassword() {
    $("#showpassword").on("click", function() {
            var obj = document.getElementById('UserPassword')
            var obj2 = document.getElementById('UserConfirmPassword')
            if (obj) {
                if (obj.type == "text") {
                    obj.type = "password";
                    if (obj2) {
                        obj2.type = "password"
                    };
                } else {
                    if (obj2) {
                        obj2.type = "text"
                    };
                    obj.type = "text";
                }
            }
            var obj3 = document.getElementById('Password')
            var obj4 = document.getElementById('PasswordConfirm');
            if (obj3) {
                if (obj3.type == "text") {
                    obj3.type = "password";
                    if (obj4) {
                        obj4.type = "password"
                    };
                } else {
                    if (obj3) {
                        obj3.type = "text"
                    };
                    obj4.type = "text";
                }

            }
        })
        // $("#").keydown(function(event){
        //     console.log(event)
        //     if(event.key!="d"){
        //         event.preventDefault();
        //     }
        // })
    if (document.getElementById("UserPhone")) {
        document.getElementById("UserPhone").addEventListener("keypress", function(evt) {

            var allowed = [45, 40, 41, 44, 43]

            if ((evt.which < 48 || evt.which > 57) && allowed.indexOf(evt.which) == -1) {
                evt.preventDefault();
            }
        });
    }

    $('.hpm-reg option').mousedown(function(e) {
        e.preventDefault();
        //console.log($(this))
        $(this).prop('selected', !$(this).prop('selected'));
        return false;
    });
}

function clearForm(cl, reg) {
    //console.log("reg " + reg);
    if (cl == "True") {
        if (!reg) {
            $('.hpm-bip__form__element [type="text"]').val("");
            $('.hpm-bip__form__element textarea').val("");
            $('.hpm-bip__form__element__radio [type="radio"]').prop("checked", false).removeAttr('checked');
            $('.hpm-bip__form__element__checkbox [type="checkbox"]').prop("checked", false).removeAttr('checked');
            grecaptcha.reset();
        } else {
            $('.hpm-reg__form__element [type="text"]').val("");
            $('.hpm-reg__form__element textarea').val("");
            $('.hpm-reg__form__element__radio [type="radio"]').prop("checked", false).removeAttr('checked');
            $('.hpm-reg__form__element__checkbox [type="checkbox"]').prop("checked", false).removeAttr('checked');
        }
        $("html, body").animate({
            scrollTop: "0"
        });
    } else {

        $("html, body").animate({
            scrollTop: "0"
        });
    }
}

function cookieShow() {
    cookieCont.show();
    cookieElem.show();
}

function cookieRemove() {
    cookieCont.remove();
    cookieElem.remove();
}

function slider() {
    $(".bxslider").after('<ul class="bxslider-pager2"></ul>');
    //* get imgsrc to table, without ext. *//
    var imgList = [];
    var i = 0;
    // var displayPager = true;
    $(".bxslider li img").each(function() {
        var slidesrc, slidesrcBeforeConvert, slidesrcConvert;
        slidesrc = $(this).attr("src");
        slidesrcBeforeConvert = slidesrc;
        slidesrcConvert = slidesrcBeforeConvert.replace(/\?(.*)/i, "")
        imgList[i] = slidesrcConvert;
        $('.bxslider-pager2').append('<li data-slideIndex="' + i + '"><a href="#"><img src="' + slidesrcConvert + '?width=117px&height=96px&mode=crop" /></a></li>');
        i++;
    });
    var realSlider = $("ul.bxslider").bxSlider({
        speed: 1000,
        pager: false,
        nextText: '',
        prevText: '',
        infiniteLoop: true,
        onSlideBefore: function($slideElement, oldIndex, newIndex) {
            changeRealThumb(realThumbSlider, newIndex);
            // $("ul.bxslider-pager2 li.active").removeClass("active");
            // $slideElement.addClass("active");
        }
    });
    var realThumbSlider = $("ul.bxslider-pager2").bxSlider({
        minSlides: 3,
        maxSlides: 3,
        slideWidth: 120,
        slideMargin: 10,
        moveSlides: 1,
        pager: false,
        speed: 1000,
        infiniteLoop: false,
        nextText: '<span></span>',
        prevText: '<span></span>',
        hideControlOnEnd: true,
        onSlideBefore: function($slideElement, oldIndex, newIndex) {
            // $("ul.bxslider-pager2 li.active").removeClass("active");
            // $slideElement.addClass("active");
        }
    });
    linkRealSliders(realSlider, realThumbSlider);
    jQuery('.bxslider-pager2').parents(".bx-wrapper").addClass("hpm-bxslider-miniatures");
    if ($(".bxslider-pager2 li").length < 4) {
        $(".hpm-bxslider-miniatures .bx-next").hide();
    }
    $(".hpm-bxslider-miniatures .bx-prev").addClass("disabled");

    if (imgList.length == 1) {
        // displayPager = false;
        // $("ul.bxslider-pager2").hide();
        $(".bx-wrapper.hpm-bxslider-miniatures").remove();
    };

}

// sincronizza sliders realizzazioni
function linkRealSliders(bigS, thumbS) {
    $("ul.bxslider-pager2").on("click", "a", function(event) {
        event.preventDefault();
        var newIndex = $(this).parent().attr("data-slideIndex");
        bigS.goToSlide(newIndex);
    });
}

//slider!=$thumbSlider. slider is the realslider
function changeRealThumb(slider, newIndex) {
    var $thumbS = $(".bxslider-pager2");
    $thumbS.find('.active').removeClass("active");
    $thumbS.find('li[data-slideIndex="' + newIndex + '"]').addClass("active");
    if (slider.getSlideCount() - newIndex >= 3) slider.goToSlide(newIndex);
    else slider.goToSlide(slider.getSlideCount() - 3);

}

// colapse logo menu (whithout triangle)
function collapseNavbarHeader() {
    // $navbarMainMenuHeader.removeClass("navbar-header--main");
    $navbarMainMenuContainer.removeClass("navbar-header--maincont");
}

// expanded logo menu (with triangle)
function expandNavbarHeader() {
    // $navbarMainMenuHeader.addClass("navbar-header--main");
    $navbarMainMenuContainer.addClass("navbar-header--maincont");
}

// fix mainmenu
function addFixedMenuClass() {
    //$("#bs-example-navbar-collapse-1").parents(".navbar").addClass("navbar-fixed-top");
}

// mainmenu not fix
function removeFixedMenuClass() {
    // $("#bs-example-navbar-collapse-1").parents(".navbar").removeClass("navbar-fixed-top");
}

/*filter for News site*/
function filterAjaxForm() {
    // $('.js-submit').change(function() {
    //     $(this).parents('form').submit();
    // });
    $('.js-submit').change(function() {
        var temp = $(this);
        setTimeout(function() {
            setFormCookie(temp.parents('form')[0].id)
            temp.parents('form').submit();
        }, 0);
    });
}

function checkIfShowLoadMoreButton() {
    var _boxesCount = $('#allBoxesCount').val();
    var _displayedBoxesCount = $('.hpm-js-boxes-container > .js-list-item').length;
    if (_boxesCount > _displayedBoxesCount) {
        $('.hpm-js-load-more').show();
    } else {
        $('.hpm-js-load-more').hide();
    }
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
if (!getCookie("overlay-closed")) {

    $(".hpm-overlay").show();
}

function getFormCookie() {
    var x = document.cookie.split(",");
    //You can add here getting cookie and updating form accordingly

    if ($("#boxesForm select#SelectedRegionId").length != 0) {
        var temp = getCookie("boxesForm");
        if (temp) {
            temp = temp.split("/");
            $("#boxesForm select#SelectedRegionId").val(temp[0]);
            $("#boxesForm #NewsTypeFilter_0__IsChecked:checked").val(temp[1]);
            $("#boxesForm #NewsTypeFilter_1__IsChecked:checked").val(temp[2]);
            $("#boxesForm #NewsTypeFilter_2__IsChecked:checked").val(temp[3]);
            //
            //        console.log(temp)
            //        console.log($("#boxesForm select#SelectedRegionId").val()) 
            setTimeout(function() {
                $("#boxesForm").submit();
            }, 0);
        }
    }
}

function setFormCookie(name) {
    //You can add another form cookie containing field values
    //console.log(name)
    if (name == "boxesForm") {
        var region = $("#" + name + " select#SelectedRegionId")[0].value;
        var filter1 = $("#" + name + " #NewsTypeFilter_0__IsChecked:checked").val();
        var filter2 = $("#" + name + " #NewsTypeFilter_1__IsChecked:checked").val();
        var filter3 = $("#" + name + " #NewsTypeFilter_2__IsChecked:checked").val();
        document.cookie = "boxesForm=" + region + "/" + filter1 + "/" + filter2 + "/" + filter3;
    }

}

/*fix height for tablets and desktops*/
function listHeightFix() {
    $('.hpm-list').find('.hpm-list--content').height('auto');
    if ($(window).width() >= screenSm) {
        $('.hpm-list').each(function() {
            var _self = this;
            var listHeaderHeight = 0;
            var listContainerHeight = 0;

            $(_self).find('.hpm-list--content').each(function() {
                if ($(this).height() > listContainerHeight) listContainerHeight = $(this).height();
            })
            $(_self).find('.hpm-list--content').height(listContainerHeight);

        });
    }
}

// on data-picker on adrertisng-of-sales
function setAdrertisngOfSalesDateTimePickers() {
    $('.hpm-list--adw-of-sales--filters .js-datepicker').datetimepicker({
        locale: _currentCulture,
        format: 'YYYY-MM-DD',
        ignoreReadonly: true,
        minDate: 0,
        showClear: true
    });
}

function pickers() {
    $('.nolpad .js-datepicker').datetimepicker({
        locale: _currentCulture,
        format: 'YYYY-MM-DD',
        ignoreReadonly: true,
		defaultDate: moment(),
        widgetPositioning: {
            horizontal: 'left',
            vertical: 'top'
        }
    });
    $('.nolpad .js-timepicker').datetimepicker({
        locale: _currentCulture,
        format: 'LT',
        ignoreReadonly: true,
		defaultDate: moment(),
        widgetPositioning: {
            horizontal: 'right',
            vertical: 'top'
        }
    });

    $('#mapControl .js-datepicker').datetimepicker({
        locale: _currentCulture,
        format: 'YYYY-MM-DD',
        ignoreReadonly: true,
		defaultDate: moment(),
        widgetPositioning: {
            horizontal: 'auto',
            vertical: 'bottom'
        }
    });
    $('#mapControl .js-timepicker').datetimepicker({
        locale: _currentCulture,
        format: 'LT',
        ignoreReadonly: true,
		defaultDate: moment(),
        widgetPositioning: {
            horizontal: 'right',
            vertical: 'bottom'
        },
        showClose: true,
        icons: {
            close: 'closeText'
        }
    });

}

function searchTrains() {
    pickers();
    $('.js-stations').autocomplete({
        minChars: 3,
        lookup: _stationList
    });
}

function loadMoreNews() {
    loaderShow();
    var _selectedTypeIds = [];

    $(".js-checkboxes-filter input:checked").each(function() {
        _selectedTypeIds.push($(this).prev().val());
    });

    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioNews/LoadMoreArticleBoxes",
        data: {
            selectedRegionId: $('#SelectedRegionId').val(),
            selectedTypeIds: _selectedTypeIds,
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function loadStations() {
    $.ajax({
        type: "GET",
        url: "/umbraco/Surface/PolRegioISS/GetStations",
        success: function(result) {
            _stationList = JSON.parse(result);
            searchTrains();
        }
    });
}

function getSvg() {
    $('img.img-svg').each(function() {
        var $img = jQuery(this);
        var imgID = $img.attr('id');
        var imgClass = $img.attr('class');
        var imgURL = $img.attr('src');

        jQuery.get(imgURL, function(data) {

            var $svg = jQuery(data).find('svg');

            if (typeof imgID !== 'undefined') {
                $svg = $svg.attr('id', imgID);
            }

            if (typeof imgClass !== 'undefined') {
                $svg = $svg.attr('class', imgClass + ' replaced-svg');
            }

            $svg = $svg.removeAttr('xmlns:a');

            if (!$svg.attr('viewBox') && $svg.attr('height') && $svg.attr('width')) {
                $svg.attr('viewBox', '0 0 ' + $svg.attr('height') + ' ' + $svg.attr('width'))
            }

            $img.replaceWith($svg);

        }, 'xml');
    });
}

function loaderShow() {
    $('#page-loader').show();
}

function loaderHide() {
    setTimeout(function() {
        $('#page-loader').fadeOut();
    }, 500);
}

function loadMoreArticles() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioArticle/LoadMoreArticleBoxes",
        data: {
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function loadMoreRegionalArticles() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioArticle/LoadMoreArticleBoxes",
        data: {
            selectedRegionId: $('#SelectedRegionId').val(),
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function selectStylish() {
    if (devicewidth > screenSm) {
        $('select.js-stylish').selectpicker({
            size: 6
        });
    }
}

function loadMoreAdvertisingOfSales() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioAdvertisingOfSales/LoadMoreAdvertisingOfSalesBoxes",
        data: {
            selectedAdministrativeId: $('#CurrentSearchSelectedAdministrativeId').val(),
            selectedStatusId: $('#CurrentSearchSelectedStatusId').val(),
            startDate: $('#CurrentSearchStartDate').val(),
            endDate: $('#CurrentSearchEndDate').val(),
            name: $('#CurrentSearchName').val(),
            number: $('#CurrentSearchNumber').val(),
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function loadMoreNotices() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioContractNotices/LoadMoreNoticeBoxes",
        data: {
            selectedAdministrativeId: $('#CurrentSearchSelectedAdministrativeId').val(),
            selectedStatusId: $('#CurrentSearchSelectedStatusId').val(),
            selectedLawActId: $('#CurrentSearchSelectedLawActId').val(),
            selectedTypeOfContractId: $('#CurrentSearchSelectedTypeOfContractId').val(),
            startDate: $('#CurrentSearchStartDate').val(),
            endDate: $('#CurrentSearchEndDate').val(),
            name: $('#CurrentSearchName').val(),
            number: $('#CurrentSearchNumber').val(),
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function loadMoreTicketOffices() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioTicketOffices/LoadMoreTicketOfficesBoxes",
        data: {
            officeName: $('#hpm-search--name').val(),
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function loadMoreSearch() {
    loaderShow();
    $.ajax({
        type: "POST",
        url: "/umbraco/Surface/PolRegioSearch/LoadMoreSearchResult",
        data: {
            name: $('#hpm-search--input').val(),
            number: $('#hpm-search--number').val(),
            skipCount: $('.hpm-js-boxes-container > .js-list-item').length,
            currentCulture: _currentCulture,
            displayCount: $('#DisplayCount').val(),
            currentPageId: _currentPageId
        },
        success: function(result) {
            $('.hpm-js-boxes-container').append(result);
            checkIfShowLoadMoreButton();
            listHeightFix();
            loaderHide();
        }
    });
}

function slideToTop(e) {
    $("html, body").animate({
        scrollTop: 0
    }, "slow");
    return false;
}

function footerSticky() {
    var winHeight = $(window).outerHeight();
    var footerHeight = $('footer').outerHeight();
    var bodyHeight = $('body').outerHeight();
    if (bodyHeight < winHeight && !$('footer').hasClass('footerSticky')) {
        $('footer').addClass('footerSticky');
    } else {
        if ((bodyHeight + footerHeight) < winHeight) {
            $('footer').addClass('footerSticky');
        } else {
            $('footer').removeClass('footerSticky');
        }
    }
}

function getSocialData() {
    var temp = JSON.parse(sessionStorage.getItem('userData'));
    //console.log(temp)
    window.doc = temp;

    if (temp.w3) {
        $("#UserEmail").val(temp.w3.U3)
        $("#UserName").val(temp.w3.ofa)
        $("#UserSurname").val(temp.w3.wea)
        $("#UserEmail").val(temp.w3.U3)
        $("#Type").val("google")
        $("#AccessToken").val(temp.Zi.access_token)
        $("#IdToken").val(temp.w3.Eea)
    } else {
        $("#UserEmail").val(temp.email)
        $("#UserName").val(temp.first_name)
        $("#UserSurname").val(temp.last_name)
        $("#Type").val("facebook")
        $("#IdToken").val(temp.id)
    }
}

function registerfinished() {
    if ($("#Response").html()) {
        var temp = JSON.parse($("#Response").html());
        var temp2 = $("#hpm-reg").data("redirect")
            // console.log(temp2)
        if (temp && temp.IsError == false) {
            //Add other language version here
            window.location = temp2;
            // setTimeout(function() {
            //     window.location = temp2;
            // }, 4500)

        }
    } else {
        var temp2 = $("#hpm-reg").data("redirect")
        window.location = temp2;
        // setTimeout(function() {
        //     window.location = temp2;
        // }, 4500)
    }
}

function checksocial(googleUser) {
    var temploc = window.location.pathname.split("/")[1];
    var http = new XMLHttpRequest();
    var url = "/" + document.documentElement.lang + "/konto/logowanie-media-spolecznosciowe";
    var params = "UserEmail=" + googleUser.getBasicProfile().getEmail() + "&accessToken=" + googleUser.Zi.access_token + "&idToken=" + googleUser.w3.Eea + "&type=google";

    http.open("POST", url, true);

    http.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    http.onreadystatechange = function() {
        var temp = JSON.parse(http.responseText)
        var temp2 = $("#hpm-reg").data("redirectlogged")
        if (!temp2) {
            var temp2 = $("#hpm-reg").data("redirect")
        }
        if (http.readyState == 4 && http.status == 200) {
            if (!temp.LoggedIn) {
                var temp4 = JSON.stringify(googleUser)
                sessionStorage.setItem('userData', temp4);
                window.location = "/" + temploc + "/konto/rejestracja/media-spolecznosciowe"
            } else {
                window.location = temp2
            }
        }
    };
    http.send(params);
}

function checkfacebook(fbdata) {
    var temploc = window.location.pathname.split("/")[1];
    //console.log(fbdata)
    var http = new XMLHttpRequest();
    var url = "/" + document.documentElement.lang + "/konto/logowanie-media-spolecznosciowe";
    var params = "UserEmail=" + fbdata.email + "&idToken=" + fbdata.id + "&type=facebook";

    http.open("POST", url, true);

    http.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    http.onreadystatechange = function() {
        var temp = JSON.parse(http.responseText)
        var temp2 = $("#hpm-reg").data("redirect")
        //console.log(temp);
        window.temp = temp;
        if (http.readyState == 4 && http.status == 200) {
            if (!temp.LoggedIn) {
                var temp4 = JSON.stringify(fbdata)
                sessionStorage.setItem('userData', temp4);
                window.location = "/" + temploc + "/konto/rejestracja/media-spolecznosciowe"
            } else {
                window.location = temp2
            }
        }
    };
    http.send(params);

}

function fontScaleUp() {
    if (fontsizeScale == 125) fontsizeScale = 150;
    else if (fontsizeScale == 100) fontsizeScale = 125;
    $('html').css("fontSize", (fontsizeMin * (fontsizeScale / 100)));
    listHeightFix();
}

function fontScaleDown() {
    if (fontsizeScale == 125) fontsizeScale = 100;
    else if (fontsizeScale == 150) fontsizeScale = 125;
    $('html').css("fontSize", (fontsizeMin * (fontsizeScale / 100)));
    listHeightFix();
}


function checkFBLogin() {

    if ($(".facebook-login").length > 0) {
        /*   FB.getLoginStatus(function(response) {
        	   alert("Already logged")
        	//statusChangeCallback(response);
        	});*/
        $(".facebook-login").on("click", function(e) {
            e.preventDefault();
            FB.login(function(response) {
                var temp = response;
                if (response.status === 'connected') {
                    FB.api('/me', {
                        fields: 'first_name,last_name,email'
                    }, function(response) {


                        // console.log(response);

                        checkfacebook(response)

                    });

                } else {
                    // The person is not logged into this app or we are unable to tell. 
                }
            }, {
                scope: 'public_profile,email'
            });
        })
    }
}

function launchsocial() {
    checkFBLogin();

    var googleUser = {};
    var startApp = function() {
        gapi.load('auth2', function() {
            // Retrieve the singleton for the GoogleAuth library and set up the client.
            auth2 = gapi.auth2.init({
                client_id: '542825678396-kdkeo4m4cfpe35dt4mgjhjgns6lbjv9n.apps.googleusercontent.com',
                cookiepolicy: 'single_host_origin',
                // Request scopes in addition to 'profile' and 'email'
                //scope: 'additional_scope'
            });
            attachSignin(document.getElementById('gplus'));
        });
    };

    function attachSignin(element) {
        //  console.log(element.id);
        auth2.attachClickHandler(element, {},
            function(googleUser) {
                //    console.log(googleUser)
                checksocial(googleUser)

            },
            function(error) {
                // alert(JSON.stringify(error, undefined, 2));
            });
    }

    setTimeout(function() {
        startApp();
    }, 10)
}
var accountaction = {
    "show": function() {
        $(".overlay").show();
        $("#hpm-section-bipform-container").addClass("smaller")
    },
    "hide": function() {
        $(".overlay").hide();
        $("#hpm-section-bipform-container").removeClass("smaller")
    },
    "execute": function() {
        var temploc = window.location.pathname.split("/")[1];
        var http = new XMLHttpRequest();
        var url = "/" + document.documentElement.lang + "/konto/usun";
        http.open("POST", url, true);
        http.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        http.onreadystatechange = function(response) {
           // console.log(response);

            if (http.readyState == 4 && http.status == 200) {

                window.location = "/" + temploc;
            }
        }
        http.send();
    }
}


//Google maps
function initGoogle() {
    var cb = "&callback=initMap";
    if (!(typeof google === 'object' && typeof google.maps === 'object')) {
        (function(d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s);
            js.id = id;
            js.src = '//maps.googleapis.com/maps/api/js?key=AIzaSyCaNmYhuem7EqTchsNbL9NzFfDRPjpqm68&language='+document.documentElement.lang+'&libraries=places' + cb;
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'google-apis-maps'));
    } else {
        initMap();
    }
}
var googlemod;

function initMap() {
    googlemod = googlemodule();
    googlemod.initMap();
    //googlemod.initMap();

}
var googleservice = {
    marker: {},
    removes: {},
    stationMarkers: []
};
googleservice.showstations = function() {

    for (var i = 0; i < _stationList.length; i++) {
        if (_stationList[i].latitude && _stationList[i].longitude) {
            googleservice.addMarker({
                lat: _stationList[i].latitude,
                lng: _stationList[i].longitude
            }, _stationList[i].value)
        }
    }
    googleservice.addMarkerCluster(googleservice.map, googleservice.stationMarkers)

}
googleservice.setstart = function(val) {
    document.getElementById('fromLocation').value = val;
    $('.toprow').parent().addClass('open')

}
googleservice.setend = function(val) {
    document.getElementById('toLocation').value = val;
    $('.toprow').parent().addClass('open')
}

googleservice.addMarker = function(location, title) {
    var marker = new google.maps.Marker({
        position: location,
        map: null,
        title: title
    });
    var start = "\"googleservice.setstart('" + title + "')\"";
    var end = "\"googleservice.setend('" + title + "')\"";
	if(document.documentElement.lang=="pl"){
		    var contentString = "<div class='Station'><div class='Main'>" + title + "</div><div class='addStart' onClick=" + start + ">Ustaw jako punkt początkowy</div><div class='addEnd' onClick=" + end + ">Ustaw jako punkt końcowy</div></div>"

		
	}else{
		    var contentString = "<div class='Station'><div class='Main'>" + title + "</div><div class='addStart' onClick=" + start + ">Set as start station</div><div class='addEnd' onClick=" + end + ">Set as end station</div></div>"

	}
  
	var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    marker.setVisible(false)
    marker.addListener('click', function() {
        infowindow.open(googleservice.map, marker);

    });
    // googleservice.stationMarkers[i].setVisible(true);
    googleservice.stationMarkers.push(marker);

};
googleservice.addMarkerCluster = function(map, markers) {
    var clusterStyles = [{
        textColor: 'white',
        url: '/images/markers/m1.png',
        height: 53,
        width: 52
    }, {
        textColor: 'white',
        url: '/images/markers/m2.png',
        height: 56,
        width: 55
    }, {
        textColor: 'white',
        url: '/images/markers/m3.png',
        height: 66,
        width: 65
    }];
    var mcOptions = {
        gridSize: 50,
        styles: clusterStyles,
        maxZoom: 15,
        ignoreHidden: true
    };
    googleservice.markerCluster = new MarkerClusterer(map, markers, mcOptions);
}

function setMapOnAll(map) {


    //googleservice.stationMarkers[i].setMap(map);
    if (map == null) {
        for (var i = 0; i < googleservice.stationMarkers.length; i++) {
            googleservice.stationMarkers[i].setVisible(false);

        }
        // googleservice.markerCluster.clearMarkers();
        googleservice.markerCluster.clearMarkers();
    } else {
       // console.log("now")
        for (var i = 0; i < googleservice.stationMarkers.length; i++) {
            googleservice.stationMarkers[i].setVisible(true);
        }
        var clusterStyles = [{
            textColor: 'white',
            url: '/images/markers/m1.png',
            height: 53,
            width: 52
        }, {
            textColor: 'white',
            url: '/images/markers/m2.png',
            height: 56,
            width: 55
        }, {
            textColor: 'white',
            url: '/images/markers/m3.png',
            height: 66,
            width: 65
        }];
        var mcOptions = {
            gridSize: 50,
            styles: clusterStyles,
            maxZoom: 15,
            ignoreHidden: true
        };
        googleservice.markerCluster = new MarkerClusterer(googleservice.map, googleservice.stationMarkers, mcOptions);

    }

}
googleservice.showhidestations = function() {
    if (!document.getElementById('showhidestations').checked) {
        setMapOnAll(googleservice.map);
    } else {
        setMapOnAll(null);
    }

}

function googlemodule() {
    var poland = {
            lat: 52.3842839,
            lng: 20.671074
        },
        card = document.getElementById('mapControl'),
        directionsText = document.getElementById('directions'),
        savemap = document.getElementById('savemap'),
        getLocations = document.getElementById('getLocation'),
        directionsTextInner = document.getElementById('inner-directions');

    var initMap = function() {

        googleservice.map = new google.maps.Map(document.getElementById('map'), {
            zoom: 6,
            center: poland,
            mapTypeControl: false,
            streetViewControl: false,
            fullscreenControl: false

        });
        googleservice.styledMapType = new google.maps.StyledMapType(
                [{
                    "featureType": "all",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "saturation": 36
                    }, {
                        "color": "#000000"
                    }, {
                        "lightness": 40
                    }]
                }, {
                    "featureType": "all",
                    "elementType": "labels.text.stroke",
                    "stylers": [{
                        "visibility": "on"
                    }, {
                        "color": "#000000"
                    }, {
                        "lightness": 16
                    }]
                }, {
                    "featureType": "all",
                    "elementType": "labels.icon",
                    "stylers": [{
                        "visibility": "off"
                    }]
                }, {
                    "featureType": "administrative",
                    "elementType": "geometry.fill",
                    "stylers": [{
                        "lightness": 20
                    }]
                }, {
                    "featureType": "administrative",
                    "elementType": "geometry.stroke",
                    "stylers": [{
                        "color": "#000000"
                    }, {
                        "lightness": 17
                    }, {
                        "weight": 1.2
                    }]
                }, {
                    "featureType": "administrative.province",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#e3b141"
                    }]
                }, {
                    "featureType": "administrative.locality",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#e0a64b"
                    }]
                }, {
                    "featureType": "administrative.locality",
                    "elementType": "labels.text.stroke",
                    "stylers": [{
                        "color": "#0e0d0a"
                    }]
                }, {
                    "featureType": "administrative.neighborhood",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#d1b995"
                    }]
                }, {
                    "featureType": "landscape",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#000000"
                    }, {
                        "lightness": 20
                    }]
                }, {
                    "featureType": "poi",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#000000"
                    }, {
                        "lightness": 21
                    }]
                }, {
                    "featureType": "road",
                    "elementType": "labels.text.stroke",
                    "stylers": [{
                        "color": "#12120f"
                    }]
                }, {
                    "featureType": "road.highway",
                    "elementType": "geometry.fill",
                    "stylers": [{
                        "lightness": "-77"
                    }, {
                        "gamma": "4.48"
                    }, {
                        "saturation": "24"
                    }, {
                        "weight": "0.65"
                    }]
                }, {
                    "featureType": "road.highway",
                    "elementType": "geometry.stroke",
                    "stylers": [{
                        "lightness": 29
                    }, {
                        "weight": 0.2
                    }]
                }, {
                    "featureType": "road.highway.controlled_access",
                    "elementType": "geometry.fill",
                    "stylers": [{
                        "color": "#f6b044"
                    }]
                }, {
                    "featureType": "road.arterial",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#4f4e49"
                    }, {
                        "weight": "0.36"
                    }]
                }, {
                    "featureType": "road.arterial",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#c4ac87"
                    }]
                }, {
                    "featureType": "road.arterial",
                    "elementType": "labels.text.stroke",
                    "stylers": [{
                        "color": "#262307"
                    }]
                }, {
                    "featureType": "road.local",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#a4875a"
                    }, {
                        "lightness": 16
                    }, {
                        "weight": "0.16"
                    }]
                }, {
                    "featureType": "road.local",
                    "elementType": "labels.text.fill",
                    "stylers": [{
                        "color": "#deb483"
                    }]
                }, {
                    "featureType": "transit",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#000000"
                    }, {
                        "lightness": 19
                    }]
                }, {
                    "featureType": "water",
                    "elementType": "geometry",
                    "stylers": [{
                        "color": "#0f252e"
                    }, {
                        "lightness": 17
                    }]
                }, {
                    "featureType": "water",
                    "elementType": "geometry.fill",
                    "stylers": [{
                        "color": "#080808"
                    }, {
                        "gamma": "3.14"
                    }, {
                        "weight": "1.07"
                    }]
                }]
            ,
            {name: 'Styled Map'});
            // googleservice.map.KmlLayer({
            //     suppressInfoWindows:true
            // })
        googleservice.stepDisplay = new google.maps.InfoWindow({
            disableAutoPan: true

        });
        googleservice.directionsService = new google.maps.DirectionsService;
        googleservice.directionsDisplay = new google.maps.DirectionsRenderer({
            map: googleservice.map,
            infoWindow: googleservice.stepDisplay,
            preserveViewport: true
        });



        googleservice.map.controls[google.maps.ControlPosition.RIGHT_TOP].push(card);
        googleservice.map.controls[google.maps.ControlPosition.RIGHT_TOP].push(directionsText);
        googleservice.map.controls[google.maps.ControlPosition.TOP_RIGHT].push(savemap);
        // googleservice.map.controls[google.maps.ControlPosition.TOP_CENTER].push(getLocations);
        googleservice.map.mapTypes.set('styled_map', googleservice.styledMapType);

        googleservice.map.setMapTypeId('roadmap');
        googlemod.swapcolor();
        googlemod.checkcookie();
        $('#fromLocation').autocomplete({
            minChars: 3,
            lookup: _stationList
        });
        $('#toLocation').autocomplete({
            minChars: 3,
            lookup: _stationList
        });
        // googlemod.initAutocoplete("fromLocation")
        // googlemod.initAutocoplete("toLocation")
        googleservice.showstations()
        setMapOnAll(null)
    }

    // var saveSearch = function() {

    //     if (document.getElementById('savingsearch').checked) {
    //         var firstDay = new Date();
    //         var nextWeek = new Date(firstDay.getTime() + 7 * 24 * 60 * 60 * 1000);
    //         var date = document.getElementById('departuredate').value,
    //             time = document.getElementById('departuretime').value,
    //             arrive = document.getElementById('arrivedepart').checked,
    //             origin = encodeURI(document.getElementById('fromLocation').value),
    //             destination = encodeURI(document.getElementById('toLocation').value);
    //         setCookie('routesearch', origin + "/" + destination + "/" + arrive + "/" + date + "/" + time, nextWeek);
    //     } else {
    //         console.log(document.getElementById('savingsearch').checked)
    //         setCookie('routesearch', '', 'Thu, 01 Jan 1970 00:00:00 UTC');
    //     }
    // }

    var calculateAndDisplayRoute = function() {

        //googleservice.stepDisplay
        //googleservice.map
        var date = document.getElementById('departuredate').value;
        var time = document.getElementById('departuretime').value;
        var arrive = document.getElementById('arrivedepart').checked;
        if (googlemod && googleservice.directionsDisplay) {
            googleservice.directionsDisplay.setPanel(directionsTextInner);
            //googlemod.saveSearch();
            var TransitOptions = {};
            var tempdate;
            if (date) {
                tempdate = new Date(date)

            } else {
                tempdate = new Date()
            }
            if (arrive) {
                TransitOptions.arrivalTime = tempdate;
            } else {
                TransitOptions.departureTime = tempdate;
            }
            if (time) {
                if (arrive) {

                    TransitOptions.arrivalTime.setHours(time.split(":")[0]);
                    TransitOptions.arrivalTime.setMinutes(time.split(":")[1])
                } else {
                    TransitOptions.departureTime.setHours(time.split(":")[0]);
                    TransitOptions.departureTime.setMinutes(time.split(":")[1])
                }
            }



            if (document.getElementById('fromLocation').value && document.getElementById('toLocation').value) {
                googleservice.directionsService.route({
                    origin: document.getElementById('fromLocation').value,
                    destination: document.getElementById('toLocation').value,
                    travelMode: 'TRANSIT',
                    provideRouteAlternatives: true,
                    //  drivingOptions:{
                    //     trafficModel: "optimistic",
                    //     departureTime: TransitOptions.departureTime
                    // },
                    transitOptions: TransitOptions
                }, function(response, status) {
                    // Route the directions and pass the response to a function to create
                    // markers for each step.
                    

                   
                    // temp.style.right = "auto";
                    if (status === 'OK') {
						var temp = document.getElementById('directions');
                    document.getElementById("chosenFrom").innerHTML = document.getElementById('fromLocation').value
                    document.getElementById("chosenTo").innerHTML = document.getElementById('toLocation').value
						 document.getElementById('notfilled').style.display = "none";
                    document.getElementById('filled').style.display = "block";
					document.getElementById('noresults').style.display = "none";
                    temp.style.display = "block";
                        // document.getElementById('warnings-panel').innerHTML =
                        //     '<b>' + response.routes[0].warnings + '</b>';
                        //console.log(response)

                        window.szlaki = response;
                        for (var j = 0; j < response.routes.length; j++) {
                            for (var i = 0; i < response.routes[0].legs[0].steps.length; i++) {
                                //  console.log(response.routes[0].legs[0].steps[i])
                                if (response.routes[j].legs[0].steps[i] && response.routes[j].legs[0].steps[i].transit && response.routes[j].legs[0].steps[i].transit.line && response.routes[j].legs[0].steps[i].transit.line.agencies[0].name == "POLREGIO") {
                                    //console.log(response.routes[j].legs[0].steps[i].transit)
                                    var temp = encodeURI(response.routes[j].legs[0].steps[i].transit.arrival_stop.name),
                                        temp2 = encodeURI(response.routes[j].legs[0].steps[i].transit.departure_stop.name),
                                        temp3 = formatDate(response.routes[j].legs[0].steps[i].transit.departure_time.value),
                                        temp4 = response.routes[j].legs[0].steps[i].transit.departure_time.text;
                                    response.routes[j].legs[0].steps[i].transit.line.agencies.push({
                                        name: "KUP BILET",
                                        url: "/pl/?search=true&from=" + temp2 + "&to=" + temp + "&date=" + temp3 + "&time=" + temp4
                                    })
                                } //https://www.polregio.pl
                            } //http://polregio-bbc.nowellon2.hypermedia.pl
                        }

                        function rescale(bound, mapID, elementId, elementposition) {
                            var mapwidth = document.getElementById(mapID).offsetWidth,
                                elementwidth = document.getElementById(elementId).offsetWidth,
                                mapheight = document.getElementById(mapID).offsetHeight,
                                elementheight = document.getElementById(elementId).offsetHeight,
                                ratio = elementwidth / mapwidth,
                                ratio2 = elementheight / mapheight;
                            //console.log(ratio + "  " + ratio2)
                                //Checking if your element is taking more of width, or height of the map

							
                            if (elementposition == "right") {
                                bound.b.f = bound.b.b + (Math.abs(Math.abs(bound.b.f) - bound.b.b) / (1 - ratio))
                            }
                            if (elementposition == "left") {
                                bound.b.b = bound.b.f - (Math.abs(Math.abs(bound.b.f) - bound.b.b) / (1 - ratio))
                            }
							


                            return bound
                        }
                        googleservice.directionsDisplay.setDirections(response);
						if(document.getElementById("map").offsetWidth>600){
                        googleservice.map.fitBounds(rescale(response.routes[0].bounds, "map", "mapControl", "right"))
						}else{
                            googleservice.map.fitBounds(response.routes[0].bounds);
                        }
                        setTimeout(function() {
                            $('#directions .next').on('click', function(e) {
                                if (response.routes.length > 0) {
                                    var tempd = new Date();
                                    for (var i = 0; i < response.routes.length; i++) {
                                        if (tempd < response.routes[i].legs[0].departure_time.value) {
                                            tempd = response.routes[i].legs[0].departure_time.value;
                                        };
                                    }
                                    var temp = tempd;
                                    temp.setTime(temp.getTime() + (60 * 60 * 1000));
                                    var n = temp.getHours();
                                    var m = temp.getMinutes();
                                    n = n + 1;
                                    n = "0" + n;
                                    n = n.substr(-2);
                                    m = "0" + m;
                                    m = m.substr(-2);
                                    var temp = n + ":" + m;


                                    var month = tempd.getUTCMonth() + 1; //months from 1-12
                                    month = "0" + month;
                                    month = month.substr(-2);
                                    var day = tempd.getUTCDate();
                                    day = "0" + day;
                                    day = day.substr(-2);
                                    var year = tempd.getUTCFullYear();

                                    var newdate = year + "-" + month + "-" + day;
                                    document.getElementById('departuredate').value = newdate;
                                    document.getElementById('departuretime').value = temp;
                                    googlemod.calculateAndDisplayRoute();

                                } else if (!time) {
                                    var d = new Date();
                                    var n = d.getHours();
                                    var m = d.getMinutes();
                                    n = n + 1;
                                    n = "0" + n;
                                    n = n.substr(-2);
                                    m = "0" + m;
                                    m = m.substr(-2);
                                    var temp = n + ":" + m;
                                    document.getElementById('departuretime').value = temp;
                                    googlemod.calculateAndDisplayRoute();

                                } else {
                                    var temp = parseInt(time.split(":")[0]);
                                    temp = temp + 1;
                                    temp = "0" + temp;
                                    temp = temp.substr(-2);
                                    document.getElementById('departuretime').value = temp + ":" + time.split(":")[1];
                                    googlemod.calculateAndDisplayRoute();
                                }

                            })
                            $("#directions a").on('click', function(e) {
                                if ($(this).attr('href').indexOf('search=true') != -1) {
                                    e.preventDefault();
                                    var temp = $(this).attr('href').split("&");
                                    document.getElementById('hpm-search--startstation').value = decodeURI(temp[1].split("=")[1]);
                                    document.getElementById('hpm-search-endstation').value = decodeURI(temp[2].split("=")[1]);
                                    document.getElementById('hpm-search--date').value = temp[3].split("=")[1];
                                    document.getElementById('hpm-search--time').value = temp[4].split("=")[1];
                                    $("#routemaps").parents(".hpm-search").removeClass("open")
                                        // window.location.search=$(this).attr('href').split("?")[1]
                                    var tempurl = window.location.pathname + "?" + $(this).attr('href').split("?")[1]
                                    window.history.pushState("fddfdf", "Szukaj", tempurl);
                                    slideToTop();
                                }
                            })
                        }, 10);
                        // showSteps(response, markerArray, stepDisplay, map);
                    } else {
                        document.getElementById('noresults').style.display = "block";
                    }
                });
            }
        }
    }
    backtosearch = function(e) {
        e.preventDefault();
        // document.getElementById('mapControl').style.display='block'
        document.getElementById('notfilled').style.display = "block";
        document.getElementById('filled').style.display = "none";
        document.getElementById('directions').style.display = 'none'


    }
    showSteps = function(directionResult, markerArray, stepDisplay, map) {
        // For each step, place a marker, and add the text to the marker's infowindow.
        // Also attach the marker to an array so we can keep track of it and remove it
        // when calculating new routes.
        var myRoute = directionResult.routes[0].legs[0];
        for (var i = 0; i < myRoute.steps.length; i++) {
            var marker = markerArray[i] = markerArray[i] || new google.maps.Marker;
            marker.setMap(map);
            marker.setPosition(myRoute.steps[i].start_location);
            attachInstructionText(
                stepDisplay, marker, myRoute.steps[i].instructions, map);
        }
    }

    var initAutocoplete = function(id) {

        var options = {
            componentRestrictions: {
                country: "pl"
            }
        };
        var input = document.getElementById(id);
        var autocomplete = new google.maps.places.Autocomplete(input, options);
        var autocompletelsr = autocomplete.addListener('place_changed', function() {
            if (!googleservice.marker[id]) {
                googleservice.marker[id] = new google.maps.Marker({
                    map: googleservice.map
                });
            }
            googleservice.marker[id].setVisible(false);
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                // User entered the name of a Place that was not suggested and
                // pressed the Enter key, or the Place Details request failed.
                window.alert("No details available for input: '" + place.name + "'");
                return;
            }

            // If the place has a geometry, then present it on a map.
            if (place.geometry.viewport) {
                googleservice.map.fitBounds(place.geometry.viewport);
            } else {
                googleservice.map.setCenter(place.geometry.location);
                googleservice.map.setZoom(17); // Why 17? Because it looks good.
            }
            googleservice.marker[id].setPosition(place.geometry.location);
            googleservice.marker[id].setVisible(true);
            googleservice.removes[id] = function() {
                google.maps.event.removeListener(autocomplete);
                google.maps.event.clearInstanceListeners(autocompletelsr);
            }
        });


    }
    var changeautocomplete = function() {
        //console.log(document.getElementById('autocompletetype').checked)
        if (document.getElementById('autocompletetype').checked) {
            $('#fromLocation').autocomplete({
                minChars: 3,
                lookup: _stationList
            });
            $('#toLocation').autocomplete({
                minChars: 3,
                lookup: _stationList
            });
            googleservice.removes['fromLocation']
            googleservice.removes['toLocation']
            $(".pac-container").remove();
        } else {
            $('#fromLocation').autocomplete("dispose");
            $('#toLocation').autocomplete("dispose");
            googlemod.initAutocoplete("fromLocation")
            googlemod.initAutocoplete("toLocation")
        }
    }
    var saveurl = function() {
        var date = document.getElementById('departuredate').value,
            time = document.getElementById('departuretime').value,
            arrive = document.getElementById('arrivedepart').checked,
            origin = encodeURI(document.getElementById('fromLocation').value),
            destination = encodeURI(document.getElementById('toLocation').value),
            element = document.getElementById('saveurl'),
            button = document.getElementById('saveurlbutton');
        element.value = window.location.origin + window.location.pathname + "?routesearch=" + origin + "/" + destination + "/" + arrive + "/" + date + "/" + time;
        element.style.display = 'block';
        button.style.display = 'none';
        element.focus();
        element.select();
    }
    var checkcookie = function() {
        var temp = getCookie('routesearch');
        temp = temp.split("/");
        var link = getParameterByName('routesearch');
        var search = getParameterByName('search');
        if (temp.length == 5 && !link) {
            if (temp[4]) document.getElementById('departuredate').value = temp[3];
            if (temp[3]) document.getElementById('departuretime').value = temp[4];
            $("#routemaps").parents(".hpm-search").addClass("open")
            if (temp[2] == "true") document.getElementById('arrivedepart').checked = true;
            if (temp[0]) document.getElementById('fromLocation').value = decodeURI(temp[0]);
            if (temp[1]) document.getElementById('toLocation').value = decodeURI(temp[1]);

            document.getElementById('savingsearch').checked = true;
        }
        //console.log(link)
        if (link) link = link.split('/');
        if (link && link.length > 0) {
            $("#routemaps").parents(".hpm-search").addClass("open")
            if (link[4]) document.getElementById('departuredate').value = link[3];
            if (link[3]) document.getElementById('departuretime').value = link[4];
            if (link[2] == "true") document.getElementById('arrivedepart').checked = true;
            if (link[0]) document.getElementById('fromLocation').value = decodeURI(link[0]);
            if (link[1]) document.getElementById('toLocation').value = decodeURI(link[1]);
        }
        if (search) {
            if (getParameterByName('from')) document.getElementById('hpm-search--startstation').value = decodeURI(getParameterByName('from'));
            if (getParameterByName('to')) document.getElementById('hpm-search-endstation').value = decodeURI(getParameterByName('to'));
            if (getParameterByName('date')) document.getElementById('hpm-search--date').value = getParameterByName('date');
            if (getParameterByName('time')) document.getElementById('hpm-search--time').value = getParameterByName('time');
        }

    }

    var swapmap = function() {
        var temp1 = document.getElementById('fromLocation').value,
            temp2 = document.getElementById('toLocation').value;
        document.getElementById('fromLocation').value = temp2;
        document.getElementById('toLocation').value = temp1;
    }
    var swapcolor=function(){
        if ($("body").hasClass("hpm-change-contrast")) {

            googleservice.map.setMapTypeId('styled_map');
        }else{
            googleservice.map.setMapTypeId('roadmap');
        }
    }
    return {
        initMap: initMap,
        //saveSearch: saveSearch,
        calculateAndDisplayRoute: calculateAndDisplayRoute,
        showSteps: showSteps,
        initAutocoplete: initAutocoplete,
        changeautocomplete: changeautocomplete,
        saveurl: saveurl,
        checkcookie: checkcookie,
        swapmap: swapmap,
        swapcolor: swapcolor
    }
}



function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showonmap);
    } else {
        //x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showonmap(position) {
    var center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude
    }
    googleservice.map.setZoom(10)
    googleservice.map.setCenter(center)

}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}



// google.maps.event.addDomListener(window, 'load', initGoogle);