$(document).ready(function () {

    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
        $("#main_icon").toggleClass('glyphicon-chevron-right').toggleClass('glyphicon-chevron-left');
    });

    if (localStorage.getItem("msg")) {
        $("#alert_success").html(localStorage.getItem("msg"));
        $("#div_alert").slideDown(500);
        localStorage.clear();
    }

    $("#close_alert").click(function () {
        $("#div_alert").slideUp(500);
        return false;
    });

    $("#btn_add_new").on('click',showModal);
    $(".actionButton").on('click', showModal);

    /*******************Hotel Region***********************/
    /*******************City Region***********************/

    
});

function showModal() {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $("#MyModal").html(data).find('.modal').modal('show');
    });
}
var Success_AjaxReturn = function (result) {
    
    if (result.msg) {
        localStorage.setItem("msg", result.msg)
        location.reload();
    }
}

