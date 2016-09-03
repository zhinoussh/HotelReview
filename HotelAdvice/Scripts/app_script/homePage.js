$(document).ready(function () {


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

    $("#btn_logoff").on('click', function () {
        $.get("/Account/LogOff", function (result) {
              window.location =result.url;
        })
    });
});


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

//var SuccessLogOf = function (result) {
//    if (result.url) {
//        window.location = result.url;
//    }

//}

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