/// <reference path="D:\Git_repos\HotelReview\HotelAdvice\Scripts/jquery-3.1.0.intellisense.js" />


$(document).ready(function () {

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
                        return { label: item };
                    }))
                    
                }

            });
        }
        , select: function (event, ui) {
            event.preventDefault();
            $("#search_destination").val(ui.item.value);
        }
        , focus: function (event, ui) {
            event.preventDefault();
            $("#search_destination").val(ui.item.value);
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
            .toggleClass('glyphicon-chevron-up glyphicon-chevron-down');
      
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
        $("#table_review").fadeOut(500);
        
        return false;
    });

    $("#btn_close_review").click(function () {
        $("#pnl_write_review").slideUp(500);
        $("#table_review").fadeIn(500);

        return false;
    });

    if (localStorage.getItem("msg")) {
        if (localStorage.getItem("msg") == "success_add_review") {
            $("#table_review").fadeIn(500);
            $("#pnl_write_review").slideUp(600);
            $("#review_alert").slideDown(500);
            scroll_to_top();
        }
        else
            set_alert_user_action(localStorage.getItem("msg"));

        localStorage.clear();
    }

    $('#CityDescriptionModal').on('shown.bs.modal', function (e) {
        var description = $(e.relatedTarget).data('desc');
        var cityName =  $(e.relatedTarget).data('name');
        $(this).find('#CityTitle').html('<b>' + cityName + '</b>');
        $(this).find('#CityAttractions').html("<p class='text-justify'>" + description + "</p>");

        $('#CityDescriptionModal').removeClass('fadeOutUp').addClass('fadeInDown');
    });

    $('#CityDescriptionModal').on('hide.bs.modal', function (e) {
        if ($('#CityDescriptionModal').hasClass('fadeInDown')) {
            e.preventDefault();
            $('#CityDescriptionModal').removeClass('fadeInDown').addClass('fadeOutUp');
            setTimeout("$('#CityDescriptionModal').modal('hide')", 500);
        }
    });
   
});

$(document).on("click", "#btn_edit_review", showEditModalReview);
$(document).on("click", "#btn_delete_review", showDeleteModalReview);

function showEditModalReview() {
    var url = $(this).data('url');

    $.get(url, function (data) {

        $("#modal_review").html(data).find("#pnl_write_review").fadeIn(100);
        $("#modal_review_main").modal('show');
        //reparseform();
    });
}

function showDeleteModalReview() {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $("#DeleteModalReview").html(data).find(".modal_main").modal('show');
    });
}

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
            //replace partial view
            if (result.partial) {
                $("#tab_content_review").html(result.partial);
                set_alert_user_action(result.msg);
                $("#modal_review_main").modal('hide');
            }
            else {
                localStorage.setItem("msg", result.msg);
                location.reload();
            }
             
        }
    }

}

var Success_DeleteReview = function (result) {
    set_alert_user_action("success_delete_review");
    $("#DeleteModalReview").find(".modal_main").modal('hide');

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

var SuccessAjax_AdvancedSearch = function (result) {
  
    var hotelName = result.searchcriteria.Hotel_Name;
    if (hotelName == null)
        hotelName = '';
    var city = result.searchcriteria.selected_city;
    var Star1 = result.searchcriteria.Star1;
    var Star2 = result.searchcriteria.Star2;
    var Star3 = result.searchcriteria.Star3;
    var Star4 = result.searchcriteria.Star4;
    var Star5 = result.searchcriteria.Star5;
    var score = result.searchcriteria.Guest_Rating;
    var center = result.searchcriteria.distance_city_center;
    var airport = result.searchcriteria.distance_airport;

    var amenity_list = result.searchcriteria.lst_amenity;
    var selected_amenities='';
    for (var i = 0; i < amenity_list.length; i++) {
        if (amenity_list[i].hotel_selected)
            selected_amenities += (amenity_list[i].AmenityID + ',');
    }

    if (selected_amenities != '')
        selected_amenities=selected_amenities.slice(0, -1);

    var url = window.location.pathname.split("/");
    var actionName = url[2];
    if (window.location.pathname == '/' || actionName == 'Home') {
        var search_url = "/Website/SearchHotel/ShowSearchResult?HotelName=" + hotelName + "&cityId=" + city + "&score=" + score + "&center=" + center + "&airport=" + airport
            + "&Star1=" + Star1 + "&Star2=" + Star2 + "&Star3=" + Star3 + "&Star4=" + Star4 + "&Star5=" + Star5 + "&amenity=" + selected_amenities;
        location.href = search_url;
    }
    else if (actionName == 'SearchHotel') {
        $.ajax({
            url: "/WebSite/SearchHotel/ShowSearchResult",
            type: "get",
            dataType: "json",
            data: {
                "HotelName": hotelName, "cityId": city, "score": score, "center": center, "airport": airport
                , "Star1": Star1, "Star2": Star2, "Star3": Star3, "Star4": Star4, "Star5": Star5, "amenity": selected_amenities
            },
            success: function (data) {
                if (data.partial) {
                    alert('here');
                    $("#table_container").html = data.partial;
                }
            }

        });
    }
    

}

var Success_ajax_HotelSearch=function(result)
{
    if (result.partial)
        $("#table_container").html = result.partial;
}
var Success_Ajax_SearchBar = function (result) {
    var search_url = "/Website/SearchHotel/ShowSearchResult?destination_name=" +result.destination_name;
    location.href = search_url;
}
var Success_paging_Results_Home = function (result) {
    scroll_to_top('homePage');
}

var scroll_to_top = function (p) {
    if(p==null)
        $('html, body').animate({ scrollTop: 0 }, 600);
    else if (p == 'homePage')
        $('html, body').animate({ scrollTop: $("#tab_homePage").offset().top-50 }, 600);
}

var set_alert_user_action = function (msg) {
    if (msg == "add_favorite_success") {

        $("#alert_user_action").html("Hotel added to your wish list");
        $("#div_alert").removeClass("alert-danger").removeClass("alert-warning").addClass("alert-success");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    }
    else if (msg == "favorite_already_exist") {

        $("#alert_user_action").html("Hotel removed from your wish list");
        $("#div_alert").removeClass("alert-success").removeClass("alert-warning").addClass("alert-danger");
        $("#icon_alert").removeClass("fa-check").addClass("fa-times-circle");
    }
    else if (msg == "rating_success") {

        $("#alert_user_action").html("Thank you! Your rating saved.");
        $("#div_alert").removeClass("alert-success").removeClass("alert-danger").addClass("alert-warning");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    }
    else if (msg == "success_edit_review") {

        $("#alert_user_action").html("Thank you! Your review saved.");
        $("#div_alert").removeClass("alert-danger").removeClass("alert-warning").addClass("alert-success");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    } 
    else if (msg == "success_delete_review") {

        $("#alert_user_action").html("Your review removed.");
        $("#div_alert").removeClass("alert-success").removeClass("alert-warning").addClass("alert-danger");
        $("#icon_alert").removeClass("fa-times-circle").addClass("fa-check");
    }

    scroll_to_top();

    $("#div_alert").slideDown(500);
}

function getQueryStringByName(name, url) {
    if (!url) {
        url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}