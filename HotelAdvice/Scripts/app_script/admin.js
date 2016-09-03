$(document).ready(function () {

    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
        $("#main_icon").toggleClass('glyphicon-chevron-right').toggleClass('glyphicon-chevron-left');
    });

    if (localStorage.getItem("msg")) {
        $("#alert_success").html(localStorage.getItem("msg"));
        $("#div_alert").fadeIn(500);
        localStorage.clear();
    }

    $("#close_alert").click(function () {
        $("#div_alert").fadeOut(500);
        return false;
    });


    function showModal() {
        var url = $(this).data('url');

        $.get(url, function (data) {
            $("#MyModal").html(data).find('.modal').modal('show');
        });
    }
    $("#btn_add_new").click(showModal);
    $(".actionButton").click(showModal);

   


    /*******************Hotel Region***********************/
    /*******************City Region***********************/


    
});


var Success_AjaxReturn = function (result) {
    
    if (result.msg) {
        localStorage.setItem("msg", result.msg)
        location.reload();
    }
}

