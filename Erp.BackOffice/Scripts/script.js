//Khai báo biến toàn cục
var d = new Date();
var currentMonth = d.getMonth() + 1;
var currentQuarter = 1;
switch(currentMonth)
{
    case 1:
    case 2:
    case 3:
        currentQuarter = 1;
        break;
    case 4:
    case 5:
    case 6:
        currentQuarter = 2;
        break;
    case 7:
    case 8:
    case 9:
        currentQuarter = 3;
        break;
    case 10:
    case 11:
    case 12:
        currentQuarter = 4;
        break;
}

//Cái gì ready cho zô đây nha
$(document).ready(function () {
    $('.numberVNI').each(function () {
        $(this).val($(this).val().replace(',', '.'))
    });

    $('footer, .main-content-popup').dblclick(function () {
        $('.profiler-results').remove();
    });

    //Xử lý cái nút thu nhỏ sidebar
    var sidebar_collapse_class = localStorage.getItem('sidebar-collapse');
    var $sidebar = $('#sidebar');
    var $logo_small = $('#navbar-container .logo-small');
    var $logo_big = $('#navbar-container .logo-big');

    if (sidebar_collapse_class == 'menu-min') {
        $sidebar.addClass('menu-min');
        $logo_small.toggle();
        $logo_big.toggle();
        $('#sidebar-collapse i').attr('class', $('#sidebar-collapse i').data('icon2'))
    }

    $('#sidebar-collapse').click(function () {
        if ($sidebar.hasClass('menu-min') == false) {
            localStorage.setItem('sidebar-collapse', 'menu-min');
            $logo_small.show();
            $logo_big.hide();
        } else {
            localStorage.removeItem('sidebar-collapse');
            sidebar_collapse = localStorage.getItem('sidebar-collapse');
            $logo_small.hide();
            $logo_big.show();
        }
    });

    setTimeout(function () {
        $('#sidebar > .nav > li.active').each(function (index, li) {
            var li_child = $(li).find('li');
            //console.log(li_child);
            if (li_child.length != 0) {
                $(li).addClass('open');
                var pathName = location.pathname;
                var a = li_child.find('a[href="' + pathName + '"]');
                if (a.length != 0)
                    a.parent('li').addClass('active');
            }
        });
    }, 1000);

    /*********************************************/
    //Khi load popup lên xong thì Hide Loading
    window.onload = function () {
        if (window.parent != null)
            window.parent.HideLoading();
    }

    $('#myModal').on('hidden', function () {
        enableScroll();
    });

    $('form').not('.no_show_loading').submit(function () {
        ShowLoading();
        if ($(this).valid()) {
        }
        else {
            HideLoading();
        }
    });

    //Cài đặt định dạng cho các input là kiểu số
    $('.input-price').priceFormat({
        centsSeparator: ',',
        thousandsSeparator: '.',
        prefix: '',
        suffix: '',
        clearPrefix: true,
        clearSuffix: true,
        //allowNegative: true,
        //limit: 2,
        centsLimit: 0
    });

    $('.input-price').focus(function () {
        $(this).select();
    });

    $(".input-float").numberFloatFormat();

    //Đặt lại độ rộng cho .edit-view label/control
    function arrangeLayout() {
        setTimeout(function () {
            $(".edit-view .control-group").each(function () {
                var label = $(this).find(".control-label");
                var value = $(this).find(".control-value");

                if ($(this).width() > 0) {
                    value.width($(this).width() - label.width() - 40);
                }
            })
        }, 10);
    }

    $(window).resize(function () {
        arrangeLayout();
    });

    $("ul.nav-tabs li a").click(function () {
        arrangeLayout();
    });
});

/******************************************************************************/

var previewFileImage = function (file, display) {
    var input = file.target;

    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        var output = document.querySelector(display);
        output.src = dataURL;
    };
    reader.readAsDataURL(input.files[0]);
};

// method khởi tạo các input theo type của phần đặc tính động (vì load lên theo ajax)
function initDataTypeInput($multi_field) {
    $($multi_field).each(function (index, elem) {
        switch ($(elem).attr('type')) {
            case 'date':
                $(elem).attr('type', 'text');
                //https://bootstrap-datepicker.readthedocs.io/en/latest/options.html#defaultviewdate
                moment();
                $(elem).datepicker({
                    //startDate: new Date(),
                    format: 'dd-mm-yyyy',
                });
                break;
            case 'datetime':
                $(elem).attr('type', 'text');
                //https://eonasdan.github.io/bootstrap-datetimepicker/
                moment();
                $(elem).dateptimeicker({
                    defaultDate: new Date(),
                    format: 'H:mm DD-MM-YYYY',
                    ignoreReadonly: true,
                });
                break;
        }
    });
};

// Xóa các thông báo yêu cầu chọn hoặc nhập
function resetLableRequired($form) {
    $form = $($form);
    $form.find('select[data-val="true"], input[data-val="true"]').each(function (index, elem) {
        $(elem).change(function () {
            $(elem).next('span').text('');
        });
    });
};

//kiểm tra đã chọn hoặc nhập hay chưa đối với các trường thêm động
function checkFieldLiveRequired($form) {
    $form = $($form);
    var flag = true;
    $form.find('select[data-val="true"], input[data-val="true"]').filter(':not(input[type="hidden"])').each(function (index, elem) {
        if ($(elem).next('span').length > 0) {
            $(elem).next('span').text('');
        }
        else {
            $(elem).after($('<span class="text-danger"></span>'));
        }

        if ($(elem).val() == '') {
            var message = $(elem).data('val-required') != undefined ? $(elem).data('val-required') : 'Hãy nhập/chọn cho ô này!';
            $(elem).next('span').text(message);
            $(elem).next('span').parent().find('input').first().focus();
            flag = false;
            console.log($(elem));
        } else {
            switch ('') {
                case 'range':
                    break;
            }
        }
    });
    return flag;
};


//ẩn thẻ div bao quanh tên và ô nhập giá trị của thuộc tính
function hideGroupRootFromInput($input) {
    $($input).closest('.form-group').hide();
};
//hiện thẻ div bao quanh tên và ô nhập giá trị của thuộc tính
function showGroupRootFromInput($input) {
    $($input).closest('.form-group').show();
};

// method for custom validate MVC
function onErrorValid(error, inputElement) {  // 'this' is the form element
    var container = $(this).find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),
        replace = $.parseJSON(container.attr("data-valmsg-replace")) !== false;

    container.removeClass("field-validation-valid").addClass("field-validation-error");
    error.data("unobtrusiveContainer", container);

    if (replace) {
        container.empty();
        error.removeClass("input-validation-error").appendTo(container);
    }
    else {
        error.hide();
    }
};

function onSuccessValid(error) {  // 'this' is the form element
    var container = error.data("unobtrusiveContainer"),
        replace = $.parseJSON(container.attr("data-valmsg-replace"));

    if (container) {
        container.addClass("field-validation-valid").removeClass("field-validation-error");
        error.removeData("unobtrusiveContainer");

        if (replace) {
            container.empty();
        }
    }
};
// end method for custom validate MVC


// left: 37, up: 38, right: 39, down: 40,
// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

function disableScroll() {
    if (window.addEventListener) // older FF
        window.addEventListener('DOMMouseScroll', preventDefault, false);
    window.onwheel = preventDefault; // modern standard
    window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
    window.ontouchmove = preventDefault; // mobile
    document.onkeydown = preventDefaultForScrollKeys;
}

function enableScroll() {
    if (window.removeEventListener)
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.onmousewheel = document.onmousewheel = null;
    window.onwheel = null;
    window.ontouchmove = null;
    document.onkeydown = null;
}

function OpenPopup(url, title, w, h, size) {
    if (url != '') {
        if (w == "0") {
            $("#myModal").addClass("modal-full");
        }
        else {
            $("#myModal").removeClass("modal-full");
        }

        $("#myModal .modal-title").text(title);
        $("#myModal .modal-body .iframe-container").html("<iframe src=\"" + url + "\"></iframe>");
        if (h == "100%") {
            //$("#myModal .modal-body").height($(window).height() - $("#myModal .modal-header").height() - 100);
            //$("#myModal .modal-body iframe").height("100%");
            $("#myModal .modal-body").height("100%");
        }
        else
            $("#myModal .modal-body").height(h);

        $('#myModal').modal('show');
        $("#myModal .modal-body .img-loading-wrap").show();
    }
}

function ClosePopup(bReload) {
    $("#myModal .modal-body .iframe-container").html("");
    $('#myModal').modal('hide');

    if (bReload)
        location.reload(true);
}

function ClosePopupAndReloadPage() {
    ShowLoading();
    ClosePopup(true);
}

function ShowLoading() {
    $(".img-loading-wrap").show();
}

function HideLoading() {
    $(".img-loading-wrap").hide();
}

//------------------------------------------------------------------------------------

function capitalizeFirstAllWords(str) {
    var pieces = str.split(" ");
    for (var i = 0; i < pieces.length; i++) {
        var j = pieces[i].charAt(0).toUpperCase();
        pieces[i] = j + pieces[i].substr(1);
    }
    return pieces.join(" ");
}

function handleNewItemClick(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).append(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
function ClickEventHandler(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).append(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}

function currencyFormat(value) {
    value = value.toString().replace(/\D+\-/g, ''); // number only
    return value.replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&.');
}

function alertPopup(title, text, type) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: title,
        // (string | mandatory) the text inside the notification
        text: text,
        class_name: 'gritter-' + type + ' gritter-light' //gritter-dark
    });
};

function scrollToTopPosition(number) {
    $('html, body').animate({ scrollTop: number }, 500);
};


function getListByProperty(arr, propertyName, propertyValue) {
    var objects = arr.filter(function (obj) {
        if (obj[propertyName] !== undefined && obj[propertyName] !== null) {
            return (obj[propertyName].toString() == propertyValue.toString());
        } else {
            return null;
        }
    });

    return objects;
}

function getSelectedText() {
    if (window.getSelection) {
        return window.getSelection().toString();
    } else if (document.selection) {
        return document.selection.createRange().text;
    }
    return '';
};

function convertCSharpDateToJavaScriptDate(stringDate) {
    var src = "/Date(1302589032000+0400)/";
    //Remove all non-numeric (except the plus)
    stringDate = stringDate.replace(/[^0-9 +]/g, '');
    //Create date
    var myDate = new Date(parseInt(stringDate));
    return myDate;
};

function convertVNtoEN(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
    str = str.replace(/-+-/g, "-");
    str = str.replace(/^\-+|\-+$/g, "");

    return str;
};

function ClickEventHandlerHTML(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).html(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
