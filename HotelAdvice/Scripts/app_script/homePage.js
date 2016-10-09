$(document).ready(function () {

    Set_Rating_Plugins();

    $("#slider_guest_review").bootstrapSlider({
        min: 0, max: 5, value:[0,5],step:0.5, focus: true
    });

    $("#txt_dest_name").autocomplete({
        source: function (request, response) {
            var list_array = new Array();
            $.ajax({
                url: "/Home/SearchList",
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
        size: 'xs',
        //displayOnly: true,
        hoverOnClear: false,
        theme: 'krajee-fa'
    , 'showClear': false
    , 'showCaption': false,
        filledStar: '<i class="fa fa-check-circle"></i>',
        emptyStar: '<i class="fa fa-circle-thin"></i>'
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
   
    if (returnUrl == null)
        returnUrl = window.location.pathname;
    
    $.get("/Account/Login?returnUrl=" + returnUrl, function (data) {

        $("#modal_container").html(data);
        if (!modalIsOpen)
            $("#modal_login").modal('show').fadeToggle(500);


    });
}

var show_signUp = function () {
    $.get("/Account/Register", function (data) {

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
var SuccessAjax_AddFavorit = function (result) {
    //login required
    if (result.msg=="login_required") {
        show_login(window.location.href);
    }
       //else if()
}
$(".dropdown-menu-sort li a").click(function () {

    var dropdown = $(this).parents(".btn-group").find('.selection');

    if ($(this).text() == 'No Matter')
        dropdown.text('Stars');
    else
        dropdown.html($(this).html());
   // $(this).parents(".btn-group").find('.selection').val($(this).text());

});


