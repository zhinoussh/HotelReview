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

    /*******************City Region***********************/

    $("#btn_add_newCity").click(function () {
        var url = "/City/ADD_New_City/";
        $.get(url, function (data)
        {
            $("#CityModal").html(data).find('.modal').modal('show');
        });
    });

    $(".fullshowItem").click(function () {

        var cityId = $(this).data('id');

        $.get("/City/CityDescription", { city_id: cityId }, function (data)
        {
            $("#CityModal").html(data).find('.modal').modal('show');
        });
    });

    $(".editItem").click(function () {

        var cityId = $(this).data('id');

        $.get("/City/ADD_New_City/"+cityId, function (data) {
            $("#CityModal").html(data).find('.modal').modal('show');
        });
    });
    

    $(".deleteItem").click(function () {

        var cityId = $(this).data('id');

        $.get("/City/Delete_City/" + cityId, function (data) {
            $("#CityModal").html(data).find('.modal').modal('show');
        });
    });
    /*******************Hotel Region***********************/


    
});


var Success_AjaxReturn = function (result) {
    
    if (result.msg) {
        localStorage.setItem("msg", result.msg)
        location.reload();
    }
}