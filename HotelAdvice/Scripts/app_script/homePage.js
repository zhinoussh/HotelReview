

var modalIsOpen = false;

$('#modal_login').on('shown.bs.modal', function (e) { modalIsOpen = true; })
$('#modal_login').on('hidden.bs.modal', function (e) { modalIsOpen = false; })

var show_login = function () {
    $.get("/Account/Login", function (data) {

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
        else
            $("#modal_container").html(result);
}

var SuccessRegister = function (result,e)
{
    if (result.url) {
        window.location = result.url;
    }
    else
        $("#modal_container").html(result);
}
var SuccessLogOf = function (result) {
    if (result.url) {
        window.location = result.url;
    }

}

//$(document).ready(function () {
//    var scroll_start = 0;
//    var startchange = $('#searchbar');
//    var offset = startchange.offset();
//    $(document).scroll(function () {
//        scroll_start = $(this).scrollTop();
//        if (scroll_start > offset.top) {
//            $('#header').removeClass()
//            $('#header').slideToggle(500,'swing');
//        }
//        else {
//          //  $('#searchbar').addClass("navbar navbar-default navbar-fixed-top");
//        }
//    });
//});