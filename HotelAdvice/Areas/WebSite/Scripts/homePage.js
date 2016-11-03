﻿$(document).ready(function () {

    Set_Rating_Plugins();

    Set_Score_Sliders();
   

    $("#search_destination").autocomplete({
        source: function (request, response) {
            var list_array = new Array();
            $.ajax({
                url: "/WebSite/Home/SearchList",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {                   
                    response($.map(data, function (item) {
                        return { label: item.CName, value: item.CID };
                    }))
                    
                }

            });
        }
        , select: function (event, ui) {
            event.preventDefault();
            $("#txt_dest_name").val(ui.item.label);
        }
        , focus: function (event, ui) {
            event.preventDefault();
            $("#txt_dest_name").val(ui.item.label);
        },
        messages: {
            noResults: "", results: function (resultsCount) { }
        }
    });

    String.prototype.replaceAt = function (index, char) {
        return this.substr(0, index) + "<span class='highlight_search'>" + char + "</span>";
    }

    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        this.term = this.term.toLowerCase();
        var resultStr = item.label.toLowerCase();
        var highlight = "";
        var rest = "";
        while (resultStr.indexOf(this.term) != -1) {
            var index = resultStr.indexOf(this.term);
            highlight = highlight + item.label.replaceAt(index, item.label.slice(index, index + this.term.length));
            resultStr = resultStr.substr(index + this.term.length);
            rest = item.label.substr(index + this.term.length);
        }
        return $("<li></li>").data("item.autocomplete", item).append("<a>" + highlight + rest + "</a>").appendTo(ul);


    };


    //photo slider inside hotel details
    $('#myCarousel').carousel({
        interval: 5000
    });

    //Handles the carousel thumbnails
    $('[id^=carousel-selector-]').click(function () {
        var id = this.id.substr(this.id.lastIndexOf("-") + 1);
        var id = parseInt(id);
        $('#myCarousel').carousel(id);
    });

    //change open and close accordion panel styles
    function toggleChevron(e) {
        
        $(e.target)
            .prev('.panel-heading')
            .find("i.indicator")
            .toggleClass('glyphicon-chevron-down glyphicon-chevron-up');
      
        if($(e.target).is( ":visible" ))
            $(e.target).parent().removeClass("panel-default").addClass("panel-info");
        else
            $(e.target).parent().removeClass("panel-info").addClass("panel-default");


    }
    $('#accordion').on('hidden.bs.collapse', toggleChevron);
    $('#accordion').on('shown.bs.collapse', toggleChevron);


    //set alert user Action

    $("#close_alert").click(function () {
        $("#div_alert").slideUp(600);
        return false;
    });
    
    $("#close_alert_review").click(function () {
        $("#review_alert").slideUp(500);
        return false;
    });

    
    $("#btn_open_review").click(function () {
        $("#pnl_write_review").slideDown(600);
        return false;
    });

    $("#btn_close_review").click(function () {
        $("#pnl_write_review").slideUp(500);
        return false;
    });

    if (localStorage.getItem("msg")) {
        if (localStorage.getItem("msg") == "success_add_review") {
            $("#table_review").html(result.partial);
            $("#pnl_write_review").slideUp(600);
            $("#review_alert").slideDown(500);
            scroll_to_top();
        }
        else
            set_alert_user_action(localStorage.getItem("msg"));

        localStorage.clear();
    }
   
});

$(document).ajaxComplete(function () {

    Set_Rating_Plugins();
});

function Set_Rating_Plugins() {
    $('.HotelStars').rating({
        step: 1,
        size: 's',
        displayOnly: true,
        hoverOnClear: false,
        theme: 'krajee-fa'
  , 'showClear': false
  , 'showCaption': false
    });


    $('.GuestRating').rating({
        step: 0.5,
        size: 's',
        displayOnly: true,
        hoverOnClear: false,
        theme: 'krajee-fa'
    , 'showClear': false
    , 'showCaption': false,
        filledStar: '<i class="fa fa-circle"></i>',
        emptyStar: '<i class="fa fa-circle-thin"></i>'
    });

    $('.GuestRating-compare').rating({
        step: 0.5,
        size: 's',
        displayOnly: true,
        hoverOnClear: false,
        theme: 'krajee-fa'
      , 'showClear': false
      , 'showCaption': false,
        filledStar: '<i class="fa fa-circle fa-circle-yellow"></i>',
        emptyStar: '<i class="fa fa-circle-thin fa-circle-thin-yellow"></i>'
    });

    

    $('.GuestRating-compared-hotel').rating({
        step: 0.5,
        size: 's',
        displayOnly: true,
        hoverOnClear: false,
        theme: 'krajee-fa'
         , 'showClear': false
         , 'showCaption': false,
        filledStar: '<i class="fa fa-circle fa-circle-red"></i>',
        emptyStar: '<i class="fa fa-circle-thin fa-circle-thin-red"></i>'
    });
    $('.YourRating').rating({
        step: 1,
        size: 'xs',
        hoverOnClear: false,
        theme: 'krajee-fa',
        'showCaption': false,
        clearButton: 'remove <i class="fa fa-eraser"></i>',
        filledStar: '<i class="fa fa-check-circle fa-yourrating"></i>',
        emptyStar: '<i class="fa fa-circle-thin fa-yourrating"></i>'
    });

    $('.YourRating-review').rating({
        step: 1,
        size: 'xs',
        hoverOnClear: false,
        theme: 'krajee-fa',
        'showCaption': false,
        clearButton: 'remove <i class="fa fa-eraser"></i>',
        filledStar: '<i class="fa fa-check-circle fa-yourrating-review"></i>',
        emptyStar: '<i class="fa fa-circle-thin fa-yourrating-review"></i>'
    });
}

function Set_Score_Sliders()
{
    $("#slider_guest_review").bootstrapSlider({
        min: 0, max: 5, value: [0, 5], step: 0.5, focus: true
    });

    $(".slider_score").bootstrapSlider({
        min: 0, max: 5, step: 0.1, focus: true, enabled: false
    });

    $("#slider_Cleanliness").on("slide", function (slideEvt) {
        $("#slider_Cleanliness_value").text(slideEvt.value);
    });

    $("#slider_Comfort").on("slide", function (slideEvt) {
        $("#slider_Comfort_value").text(slideEvt.value);
    });

    $("#slider_Location").on("slide", function (slideEvt) {
        $("#slider_Location_value").text(slideEvt.value);
    });

    $("#slider_Facilities").on("slide", function (slideEvt) {
        $("#slider_Facilities_value").text(slideEvt.value);
    });

    $("#slider_Staff").on("slide", function (slideEvt) {
        $("#slider_Staff_value").text(slideEvt.value);
    });

    $("#slider_money").on("slide", function (slideEvt) {
        $("#slider_money_value").text(slideEvt.value);
    });
}
$(window).on('load', function () {
    var returnUrl = $("#hd_return_url").val();
    if (returnUrl != null && returnUrl != '')
        show_login(returnUrl);
});

$(document).on('keypress', '#txt_username', Close_LoginAlert);
$(document).on('keypress', '#txt_password', Close_LoginAlert); 

function Close_LoginAlert() {
    $('#login-alert').fadeOut(500);
}

function checkbox_Checked(n)
{
    if ($("#checkbox_star"+n).is(":checked"))
        $("#hd_star" + n).val('True');
    else
        $("#hd_star" + n).val('False');
}
function chk_amenity_changed(n) {
    if ($("#chk_amenity" + n).is(":checked"))
        $("#hd_amenity" + n).val('True');
    else
        $("#hd_amenity" + n).val('False');
}


var modalIsOpen = false;

$('#modal_login').on('shown.bs.modal', function (e) { modalIsOpen = true; })
$('#modal_login').on('hidden.bs.modal', function (e) { modalIsOpen = false; })


var show_login = function (returnUrl) {

    if (returnUrl == null) {
        returnUrl = window.location.pathname+window.location.search;
    }
    
    $.get("/Account/Account/Login?returnUrl=" + returnUrl, function (data) {

        $("#modal_container").html(data);
        if (!modalIsOpen)
            $("#modal_login").modal('show').fadeToggle(500);


    });
}

var show_signUp = function () {
    $.get("/Account/Account/Register", function (data) {

        $("#modal_container").html(data)
        if (!modalIsOpen)
            $("#modal_login").modal('show').fadeIn(500);
    });
}

var SuccessLogin = function (result) {
    if (result.url)
        window.location.href = result.url;
    else {
        $("#modal_container").html(result);
        $('#login-alert').fadeIn(500);
        
    }
   
   // if (result.fail=="true")
       // $('#login-alert').fadeIn(500);
   
}



var SuccessRegister = function (result,e)
{
    if (result.url) {
        window.location = result.url;
    }
    else
        $("#modal_container").html(result);
}

var Success_ADDReview = function (result) {
    
    if (result.msg) {
        //login required
        if (result.msg == "login_required") {
            show_login(window.location.href);
        }
        else {
            localStorage.setItem("msg", result.msg);
            location.reload();
             
        }
    }

}

var SuccessAjax_AddFavorit = function (result) {
    
    if (result.msg) {
        //login required
        if (result.msg == "login_required") {
            show_login(window.location.href);
        }
        else {
            //replace partial view
            if (result.partial) {
                $("#table_container").html(result.partial);

                set_alert_user_action(result.msg);
            }
            else {
                localStorage.setItem("msg", result.msg);
                location.reload();
            }

        }
    }
        
}

var SuccessAjax_AddRating = function (result) {

    if (result.msg) {
        //login required
        if (result.msg == "login_required") {
            show_login(window.location.href);
        }
        else {
            //replace partial view
            if (result.partial) {
                $("#tab_content_rating").html(result.partial);

                set_alert_user_action(result.msg);
            }
            else {
                localStorage.setItem("msg", result.msg);
                location.reload();
            }

        }
    }
}

var Success_paging_Results = function (result) {
   scroll_to_top();
}

var scroll_to_top = function () {
    $('html, body').animate({ scrollTop: 0 }, 600);
}

var set_alert_user_action = function (msg) {
    if (msg == "add_favorite_success") {

        $("#alert_user_action").html("Hotel added to your wish list");
        $("#div_alert").removeClass("alert-danger").addClass("alert-success");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    }
    else if (msg == "favorite_already_exist") {

        $("#alert_user_action").html("Hotel removed from your wish list");
        $("#div_alert").removeClass("alert-success").addClass("alert-danger");
        $("#icon_alert").removeClass("fa-check").addClass("fa-times-circle");
    }
    else if (msg == "rating_success") {

        $("#alert_user_action").html("Thank you! Your rating saved.");
        $("#div_alert").removeClass("alert-warning").addClass("alert-success");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    }

    scroll_to_top();

    $("#div_alert").slideDown(500);
}
