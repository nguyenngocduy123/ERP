//jQuery extention -----------------------------
(function ($) {
    $.fn.onChangeSetValue = function (options) {
        var settings = $.extend({
            selector: null,
            valueIs: 'value'
        }, options);
        var $this = this;

        if ($this[0] == undefined)
            return;

        if ($(settings.selector).val() == '') {
            $(settings.selector).val($this.find('option:selected').text());
        }

        $this.change(function () {
            if (settings.selector != null) {
                if (settings.valueIs == 'text')
                    $(settings.selector).val($this.find('option:selected').text());
                else
                    $(settings.selector).val($this.val());
            }
        });
    };
}(jQuery));

(function ($) {
    $.fn.numberOnly = function () {

        var $this = this;

        if ($this[0] == undefined)
            return;

        var selectors = $this.selector;
        var elements = document.querySelectorAll(selectors);
        for (var i = 0; i < elements.length; i++) {
            init(elements[i]);
        }
    };

    function init(elem) {
        var $this = $(elem)

        $this.keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $this.keyup(function (event) {
            var s = $(this).val();
            while (s.charAt(0) === '0') {
                s = s.substr(1);
            }

            $(this).val(s.replace(/\D/g, ""));//.replace(/\B(?=(\d{3})+(?!\d))/g, "."));
            $this.val(s.replace(/\D/g, "")).trigger('change');
        });

        $this.blur(function () {
            var str = $(this).val();
            if (str == '') {
                str = '0';
                $this.val(str).trigger('change');
                $(this).val(str);
            }
        });

        //$this.keyup(function (event) {
        //    if (event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 39) {
        //        this.value = this.value.replace(/[^0-9\.]/g, ''); // number only
        //    }
        //});
        //$this.change(function (event) {
        //    this.value = this.value.replace(/[^0-9\.]/g, ''); // number only
        //});
    };
}(jQuery));

//number Format
(function ($) {
    $.fn.numberFormat = function (locationAppend) {

        var $this = this;

        if ($this[0] == undefined)
            return;

        //var selectors = $this.selector;
        //var elements = document.querySelectorAll(selectors);
        for (var i = 0; i < $this.length; i++) {
            initFormat($this[i], locationAppend);
        }
    };

    function initFormat(elem, locationAppend) {
        var $this = $(elem);
        var $input = $this.clone().attr('id', 'mask-' + $this.attr('id')).addClass('mask-format-currency mask-format-input-' + $this.attr('role'));//$('<input type="text" id="mask-' + $this.attr('id') + '" value="' + $this.val().replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&,') + '" class="' + $this.attr('class') + '" />');
        $this.attr("tabindex", "-1");
        $input.removeAttr('data-val');
        $input.removeAttr('data-val-number');
        $input.removeAttr('name');
        $input.removeAttr('role');
        $input.val($this.val().replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&.'));

        if ($this.parent().find('.mask-format-currency').length != 0) {
            $this.parent().find('.mask-format-currency').remove();
        }

        if (locationAppend == 'after')
            $this.after($input);
        else
            $this.before($input);

        if ($input.width() == 0) {
            var style = 'opacity: 1; height: inherit; width: 100%; margin: 0px; padding: 5px 4px 6px; border: 1px solid #b5b5b5;';
            $input.attr('style', style);
        }

        var style = 'display: none; height: 0px !important; width: 0px !important; margin: 0px !important; padding:0px !important; border: 0px !important;';
        $this.attr('style', style);

        $input.keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $input.keyup(function (event) {
            var s = $(this).val();
            while (s.charAt(0) === '0') {
                s = s.substr(1);
            }

            $(this).val(s.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, "."));
            $this.val(s.replace(/\D/g, "")).trigger('change');
        });

        $input.blur(function () {
            var str = $(this).val();
            if (str == '') {
                str = '0';
                $this.val(str).trigger('change');
                $(this).val(str);
            }
        });
    };
}(jQuery));

//number Float Format
(function ($) {
    $.fn.numberFloatFormat = function (separator) {
        separator = separator == undefined ? '.' : separator; // console.log(separator);

        var $this = this;
        if ($this[0] == undefined)
            return;

        var $input = $this.clone().attr('id', 'mask-' + $this.attr('id'));//$('<input type="text" id="mask-' + $this.attr('id') + '" value="' + $this.val().replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&,') + '" class="' + $this.attr('class') + '" />');
        $input.removeAttr('data-val');
        $input.removeAttr('data-val-number');
        $input.removeAttr('name');
        $this.after($input);
        $this.css({ 'opacity': 0, 'height': 0, 'width': 0, 'margin': 0, 'padding': 0 });

        if (separator == ',')
            $input.val($this.val().replace('.', ','));
        else
            $input.val($this.val().replace(',', '.'));


        //var numberOnlyRegex = /[^0-9\.]/g; //number only : /\D+/g
        var numberOnlyRegex = new RegExp("[^0-9\\" + separator + "]", "g"); //console.log(numberOnlyRegex);

        $input.keydown(function (event) {
            if (event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 39) {
                this.value = this.value.replace(numberOnlyRegex, ''); // number only with separator (,|.)
                $input.trigger('change');
                //var valueInput = this.value.replace(numberOnlyRegex, ''); // number only with separator (,|.)
                //if ((valueInput.match(/(\,|\.)/g) || []).length > 1) {
                //    //console.log(valueInput.match(/(\,|\.)/g));
                //    valueInput = valueInput.slice(0, -1);
                //}
                ////console.log('valueInput', valueInput);
                //this.value = valueInput;
                //var str = valueInput; //.replace(/\,/g, '.').replace(/\D+\./g, '');
            }
        });
        $input.change(function (event) {
            var valueInput = this.value.replace(numberOnlyRegex, ''); // number only with separator (,|.)
            if ((valueInput.match(/(\,|\.)/g) || []).length > 1) {
                //console.log(valueInput.match(/(\,|\.)/g));
                valueInput = valueInput.slice(0, -1);
            }
            //console.log('valueInput', valueInput);
            this.value = valueInput;
            var str = valueInput; //.replace(/\,/g, '.').replace(/\D+\./g, '');

            $this.val(str).trigger('change');
        });
    };

}(jQuery));

//remove Data Attr
(function ($) {
    $.fn.removeDataAttr = function (data_name) {
        data_name = data_name.replace(/\s/g, '');
        var arrDataName = data_name.split(',');

        var $this = this;
        if ($this[0] == undefined)
            return;

        var selectors = $this.selector;
        var elements = $this;

        for (var i = 0; i < elements.length; i++) {
            //console.log(elements[i].dataset);
            if (elements[i].dataset != undefined) {
                for (var j = 0; j < arrDataName.length ; j++) {
                    var dataLabel = getDataLabelLookLikeJavascript(arrDataName[j]);
                    //console.log(dataLabel);
                    //console.log(elements[i].dataset[dataLabel]);
                    if (elements[i].dataset[dataLabel] != undefined)
                        delete elements[i].dataset[dataLabel];
                }
            }
        }
    };

    $.fn.addDataAttr = function (data_name, data_value) {
        var $this = this;
        var selectors = $this.selector;
        var elements = document.querySelectorAll(selectors);
        for (var i = 0; i < elements.length; i++) {
            var dataLabel = getDataLabelLookLikeJavascript(data_name);
            elements[i].dataset[dataLabel] = data_value;
        }
    };

    function getDataLabelLookLikeJavascript(dataLabel) {
        if (dataLabel.indexOf('-')) {
            var arrDataLabel = dataLabel.split('-');
            dataLabel = arrDataLabel[0];
            for (var i = 1; i < arrDataLabel.length; i++) {
                dataLabel += Capitalize(arrDataLabel[i]);
            }
            return dataLabel;
        }
        return dataLabel;
    };

    function Capitalize(str) {
        str = str.toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });
        return str;
    };

}(jQuery));

//edit Inline Input
(function ($) {
    $.fn.editInlineInput = function (options) {
        var settings = $.extend({
            id: null,
            url: '',
            disabled: true,
            dataId: null
        }, options);
        var $this = this;

        if ($this[0] == undefined)
            return;

        for (var i = 0; i < $this.length; i++) {
            initControl($this[i], settings);
        }
    };

    function initControl(elem, settings) {
        var $control = $('<span class="edit-inline-control">' +
            '<span class="control-edit open btn btn-info btn-sm"><i class="ace-icon fa fa-edit"></i></span>' +
            '<span class="control-save btn btn-success btn-sm"><i class="ace-icon fa fa-save"></i></span>' +
            '<span class="control-cancel btn btn-grey btn-sm"><i class="ace-icon fa fa-times"></i></span>' +
            '</span>');
        var $elem = $(elem);

        var inputName = $elem.attr('name');
        var id = settings.dataId != null ? $elem.data(settings.dataId) : settings.id;

        $elem.after($control);
        $control = $elem.next('span.edit-inline-control');

        $control.find('.control-edit').click(function () {
            if (settings.disabled == true)
                $elem.removeAttr('disabled');

            $control.find('span').addClass('open');
            $control.find('.control-edit').removeClass('open');
        });

        $control.find('.control-cancel, .control-save').click(function () {
            $control.find('span').removeClass('open');
            $control.find('.control-edit').addClass('open');

            if (settings.disabled == true)
                $elem.attr('disabled', 'disabled');
        });

        $control.find('.control-save').click(function () {
            $.post(settings.url, { id: id, fieldName: inputName, value: $elem.val() }, function (res) {
                console.log(res);
                if (res.status == 'success') {
                    alertPopup('Lưu thành công!', '', 'success');
                } else {
                    alertPopup('Lưu không thành công!', 'Xin hãy kiểm tra lại.', 'error');
                }
            });
        });
    };

}(jQuery));

//edit inline on table

(function ($) {
    $.fn.editInlineInputTable = function (options) {
        var settings = $.extend({
            id: null,
            url: '',
            disabled: true,
            dataId: null
        }, options);
        var $this = this;

        if ($this[0] == undefined)
            return;

        for (var i = 0; i < $this.length; i++) {
            initControl($this[i], settings);
        }
    };

    function initControl(elem, settings) {
        var $elem = $(elem);

        var $control = $('<span class="edit-inline-control">' +
            '<span class="control-edit open btn btn-info btn-sm"><i class="ace-icon fa fa-edit"></i></span>' +
            '<span class="control-save btn btn-success btn-sm"><i class="ace-icon fa fa-save"></i></span>' +
            '<span class="control-cancel btn btn-grey btn-sm"><i class="ace-icon fa fa-times"></i></span>' +
            '</span>');

        var $inputControl;
        if ($elem.data('dropdown') == undefined) {
            $inputControl = $('<input class="edit-inline-input-control" style="display:none" />');
        } else {
            $inputControl = $('<select class="edit-inline-input-control" style="display:none" />');
            var dataValue = $elem.data('dropdown').toString().replace(/\//g, '');
            var arr_data = dataValue.split('|');
            for (var i in arr_data) {
                var option = arr_data[i].split('#');
                if (option.length == 2 && option[1] != '' && option[1] != undefined) {
                    $inputControl.append($('<option value="' + option[0] + '">' + option[1] + '</option>'));
                }
            }
        }
        //đặt giá trị cho input theo giá trị truyền vào
        $inputControl.val($elem.data('value'));

        var inputName = $elem.attr('name');
        var id = settings.dataId != null ? $elem.data(settings.dataId) : settings.id;

        var $container = $('<div class="container-edit-inline" />').append($inputControl).append($control);
        $elem.parent().append($container);
        $elem.prependTo($container);

        $control = $container.find('span.edit-inline-control');

        $control.find('.control-edit').click(function () {
            $container.addClass('active-edit');
            $elem.hide();
            $inputControl.show();
            if (settings.disabled == true)
                $elem.removeAttr('disabled');

            $control.find('span').addClass('open');
            $control.find('.control-edit').removeClass('open');
        });

        $control.find('.control-cancel, .control-save').click(function () {
            $container.removeClass('active-edit');
            $inputControl.hide();
            $elem.show();
            $control.find('span').removeClass('open');
            $control.find('.control-edit').addClass('open');

            if (settings.disabled == true)
                $elem.attr('disabled', 'disabled');
        });

        $control.find('.control-save').click(function () {

            if ($inputControl.is('select')) {
                $elem.text($inputControl.find('option:selected').text());
            } else {
                $elem.text($inputControl.val());
            }

            $.post(settings.url, { id: id, fieldName: inputName, value: $elem.val() }, function (res) {
                console.log(res);
                if (res.status == 'success') {
                    alertPopup('Lưu thành công!', '', 'success');
                } else {
                    alertPopup('Lưu không thành công!', 'Xin hãy kiểm tra lại.', 'error');
                }
            });
        });
    };

}(jQuery));

// end jQuery Extention ------------------------------------

$(document).ready(function () {
    $(document).on("change", ".numberinput3", function () {
        var old_value = $(this).val();
        
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0.00'));
        }
    });
    $(document).on("change", ".numberinput2", function () {
        var old_value = $(this).val();

        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0'));
        }
    });
    $(document).on("change", ".numberinput1", function () {
        var old_value = $(this).val();

        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0'));
        }
    });
    $(document).on("change", ".maxpercent", function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal >= 100)
        {
            $(this).val(100);
        }
    });
    $(document).on("blur", ".numberinput1", function () {
        var old_value = $(this).val();

        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0'));
        }
    });
    $(document).on("keydown", ".numberinput1", function () {
        var numberOnlyRegex = new RegExp("[^0-9]", "g");
        if (event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 39) {
            this.value = this.value.replace(numberOnlyRegex, ''); // number only with separator (,|.)
            $(this).val($(this).val().replace(numberOnlyRegex, ''));
        }

    });
  
});
function LoadNumberInput() {
    $(".numberinput3").each(function () {
        var old_value = $(this).val();
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0.00'));
        }
    });
    $(".numberinput2").each(function () {
        var old_value = $(this).val();
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0'));
        }
    });
    $(".numberinput1").each(function () {
        var old_value = $(this).val();
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal == 0) {
            if (old_value == "") {
                $(this).val("");
            }
        }
        else {
            $(this).val(ralVal.format('0,0'));
        }
    });
};

function removeComma(val) {
    var result = "";
    val = val.replace(/\-/g, '');
    if (val === undefined) return val;
    if (val.indexOf(".") > 0) {
        var temp = val.split('.');
        for (var i = 0; i < temp.length; i++) {
            result += temp[i].toString();
        }
    }
    else {
        result = val;
    }
   // result = result.replace(',', '.');
    console.log(result);
    return result;
};

function removeCommaSubmit(val) {
    var result = "";
    if (val === undefined) return val;
    if (val.indexOf(".") > 0) {
        var temp = val.split('.');
        for (var i = 0; i < temp.length; i++) {
            result += temp[i].toString();
        }
    }
    else {
        result = val;
    }
    //   console.log(result);
    return result;
};
function round(n, dec) {
    n = parseFloat(n);
    if (!isNaN(n)) {
        if (!dec)
            var dec = 0;
        var factor = Math.pow(10, dec);
        return Math.floor(n * factor + ((n * factor * 10) % 10 >= 5 ? 1 : 0)) / factor;

    }
    else {
        return n;
    }
};

function formatNumber(val,seperator,every,precision) {
    val = parseFloat(val).toFixed(precision);
    val += '';
    var arr = val.split('.', 2);
    var i = parseInt(arr[0]);
    if (isNaN(i))
        return '';
    i = Math.abs(i);
    var n = new String(i);
    var d = arr.length > 1 ? ',' + arr[1] : '';
    var a = [];
    var nn;
    while (n.length > every)
    {
        nn = n.substr(n.length - every);
        a.unshift(nn);
        n = n.substr(0, n.length - every);
    }
    if (n.length > 0)
    {
        a.unshift(n);
    }
    n = a.join(seperator);
    n = n + d;
    n = n.replace("-.", "");
    n = n.replace("-", "");
    if (val.indexOf("-") != -1)
    {
        n = "-" + n;
    }
    return n;
};

function formatNumberText(value, precision) {
    value = value.toString();
    if (precision == "0")
    {
        if ($.trim(value) == "" || value.indexOf("..") > -1) {

            return formatNumber(0, ",", 3, 0);
        } else {
            return formatNumber(removeComma(value), ",", 3, 0);
        }
    }
    if (precision == "2")
    {
        if ($.trim(value) == "" || value.indexOf("..") > -1) {
            return formatNumber(0, ".", 3, 2);
        }
        else {
            return formatNumber(removeComma(value), ",", 3, 2);
        }
    }
};

function checkForEnter(event) {
    var lfound = false;
    if (event.keyCode == 13) {
        var obj = this;
        $(".enter").each(function () {
            if (this == obj) {
                lfound = true;
            }
            else {
                if (lfound) {
                    $(this).focus();
                    $(this).select();
                    event.preventDefault();
                    return false;
                }
            }

        });
    }
};

function ClearFormatBeforeSubmit(formElement) {
    $(formElement).find("input[class*='numberinput']").each(function () {
        $(this).val(removeCommaSubmit($(this).val()));
    });

};