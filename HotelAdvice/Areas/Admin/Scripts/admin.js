/// <reference path="D:\Git_repos\HotelReview\HotelAdvice\Scripts/jquery-3.1.0.intellisense.js" />


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

    $("#btn_add_new").on('click', showAddModal);
  
    //SetUp_AddImages
    $("#inputimages").fileinput({
        uploadAsync: false,
        uploadUrl: '/Admin/Hotel/AddImage/',
        maxFilePreviewSize: 10240,
        dropZoneEnabled: false,
        uploadExtraData: { hotel_ID: $('#hd_hotel_id').val() }


    }).on('filebatchuploadsuccess', function (event, data) {
        //var form = data.form, files = data.files, extra = data.extra,
        //    response = data.response, reader = data.reader;
        localStorage.setItem("msg", "File uploaded successfully.");
        location.reload();
    });
   
});

$(document).on("click", ".editButton", showAddModal);
$(document).on("click", ".actionButton", showModal);

$(document).on("click", ".editAmenityButton", function () {

    $("#amenity_id").val($(this).data('id'));
    $("#amenity_name").val($(this).data('name'));
    
});

function showAddModal() {
    
    var url = $(this).data('url');
    var url_parts = url.split('/');
    var controller=url_parts[2];
    
    $.get(url, function (data) {
        $("#MyModal").html(data).find('.modal_main').modal('show');
        reparseform();
        if (controller == 'City')
            SetUp_AddCity();
        else if (controller == 'Hotel')
            SetUp_AddHotel();        
    });
}

function showModal() {

    var url = $(this).data('url');

    $.get(url, function (data) {
        $("#MyModal").html(data).find('.modal_main').modal('show');
        reparseform();
       
    });
}

var reparseform = function () {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");

};

var Success_AjaxReturn = function (result) {
    if (result.msg) {

        localStorage.setItem("msg", result.msg);
        // location.reload();
        location.href = result.ctrl + "/Index?page=" + result.cur_pg + "&filter=" + result.filter;
    }

    
}


var Success_AjaxReturn_deleteImage = function (result) {

    if (result.msg) {
        localStorage.setItem("msg", result.msg);
        location.reload();
        
    }

}

function SetUp_AddCity() {
    var img_path = $('#img_city').val();
    $("#image_city").fileinput({
        overwriteInitial: true,
        cache: false,
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
        defaultPreviewContent: "<img src='" + img_path + "' alt='City Image' style='width:220px; height:220px'>",
        layoutTemplates: { main2: '{preview}  {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "gif"],
        browseOnZoneClick: true,
        showZoom: false
        , deleteUrl: 'http://Admin/City/DeleteMainImage/'

    });

}


function SetUp_AddHotel() {
    var img_path = $('#img_hotel').val();
    $("#image_hotel").fileinput({
        overwriteInitial: true,
        cache:false,
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
        defaultPreviewContent: "<img src='" + img_path + "' alt='Hotel Image' style='width:220px; height:220px'>",
        layoutTemplates: { main2: '{preview}  {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "gif"],
        browseOnZoneClick: true,
        showZoom: false
        , deleteUrl: 'http://Admin/Hotel/DeleteMainImage/'

    });

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

    Set_Restaurants_tag();
    Set_Rooms_tag();
    Set_Sightseeing_tag();
}


function Set_Restaurants_tag()
{
    var Restnames = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/Admin/Hotel/Get_Restaurants',
            
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

function Set_Rooms_tag() {
    var roomTypes = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/Admin/Hotel/Get_Rooms',

            prepare: function (query, settings) {
                settings.type = "POST";
                settings.contentType = "application/json; charset=UTF-8";
                settings.data = JSON.stringify({ "Prefix": query });
                return settings;
            },
            filter: function (list) {
                return $.map(list, function (object) {
                    return object.RoomType;
                });
            }
        }
    });

    roomTypes.initialize();

    $('#txt_room').tagsinput({
        typeaheadjs: {
            name: 'roomTypes',
            source: roomTypes.ttAdapter(),
            limit: 10
        },
        freeInput: true
    });
}

function Set_Sightseeing_tag() {

    var sightseeings = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/Admin/Hotel/Get_SightSeeing',

            prepare: function (query, settings) {
                settings.type = "POST";
                settings.contentType = "application/json; charset=UTF-8";
                settings.data = JSON.stringify({ "Prefix": query });
                return settings;
            },
            filter: function (list) {
                return $.map(list, function (object) {
                    return object.SightSeeing;
                });
            }
        }
    });

    sightseeings.initialize();

    $('#txt_sightseeing').tagsinput({
        typeaheadjs: {
            name: 'sightseeings',
            source: sightseeings.ttAdapter(),
            limit: 10
        },
        freeInput: true
    });
}

function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

function add_hotel_click()
{
    if ($("#frm_add_hotel").valid()) {
        var formData = new FormData($('#frm_add_hotel')[0]);
        var action = $("#frm_add_hotel").attr("action");

        $.ajax({
            type: "POST",
            url: action,
            data: addRequestVerificationToken(formData),
            contentType: false,
            processData: false,
            success: function (data) {
                Success_AjaxReturn(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing
                alert(errorThrown);
            }
        });
    }
            
}

function add_city_click() {
    if ($("#frm_add_city").valid()) {
        var formData = new FormData($('#frm_add_city')[0]);
        var action = $("#frm_add_city").attr("action");

        $.ajax({
            type: "POST",
            url: action,
            data: addRequestVerificationToken(formData),
            contentType: false,
            processData: false,
            success: function (data) {
                Success_AjaxReturn(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //do your own thing
                alert(errorThrown);
            }
        });
    }

}