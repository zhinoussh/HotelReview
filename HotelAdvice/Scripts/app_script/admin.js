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
   // $(document).on('click', '#btn_add_new', showModal);

    /*******************Hotel Region***********************/

    /*******************City Region***********************/
 
   
});

function showModal() {
    var url = $(this).data('url');

    $.get(url, function (data) {
        $("#MyModal").html(data).find('.modal_main').modal('show');
        reparseform();

        //only do for add hotel dialog
        if (url = '/Hotel/ADD_New_Hotel') {
            SetUp_AddHotel();
            Set_Restaurants_tag();
        }
    });
}

var reparseform = function () {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");

};
var Success_AjaxReturn = function (result) {
    
    if (result.msg) {
        localStorage.setItem("msg", result.msg)
        location.reload();
    }
}

function SetUp_AddHotel() {
   
    $('#checkin').timepicker({
        template: false,
        showInputs: false,
        minuteStep: 1
    });
    $('#checkout').timepicker({
        template: false,
        showInputs: false,
        minuteStep: 1
    });

    $('.HotelStars').rating({
        step: 1,
        size: 's',
        hoverOnClear: false,
        theme: 'krajee-fa'
        ,'showClear': false
        ,'showCaption': false
    });

    $("#image_hotel").fileinput({
        overwriteInitial: true,
        maxFileSize: 1024,
        maxFileCount: 1,
        showClose: false,
        showCaption: false,
        browseLabel: 'Choose Image',
        browseClass: "btn btn-sm btn-success",
        removeClass: "btn btn-sm btn-danger",
        removeLabel: 'Delete Image',
        removeTitle: 'Delete Image',
        browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>',
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        elErrorContainer: '#kv-avatar-errors-1',
        msgErrorClass: 'alert alert-block alert-danger',
        defaultPreviewContent: '<img src="/images/empty.gif" alt="Hotel Image" style="width:220px">',
        layoutTemplates: { main2: '{preview}  {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "gif"],
        browseOnZoneClick: true,
        showZoom: false

    });
}

function Set_Restaurants_tag()
{
    var Restnames = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/Hotel/Get_Restaurants',
            
            prepare: function (query, settings) {
                settings.type = "POST";
                settings.contentType = "application/json; charset=UTF-8";
                settings.data = JSON.stringify({ "Prefix": query });
                return settings;
            },
            filter: function (list) {
                return $.map(list, function (object) {
                    return object.RestName;
                });
            }
        }
    });

    Restnames.initialize();

    $('#txt_restaurant').tagsinput({
        typeaheadjs: {
            name: 'Restnames',
            source: Restnames.ttAdapter(),
            limit: 10        
        },
        freeInput: true
    });
}


