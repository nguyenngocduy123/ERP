
//<link href="http://aspnet-skins.telerikstatic.com/ajaxz/2017.1.118/ComboBoxLite.css" type="text/css" rel="stylesheet" class="Telerik_stylesheet" />
//<link href="http://aspnet-skins.telerikstatic.com/ajaxz/2017.1.118/SilkLite/ComboBox.Silk.css" type="text/css" rel="stylesheet" class="Telerik_stylesheet" />
//<script src="http://aspnet-scripts.telerikstatic.com/ajaxz/2017.1.118/ComboBox/RadComboBoxScripts.js" type="text/javascript"></script>

// rad combo box jquery plugin v1.0
(function ($) {
    $.fn.radComboBox = function (options) {
        var settings = $.extend({
            colTitle: '',
            colValue: '.col1',
            colSize: '',
            colSizeDefault: '',
            colHide: '',
            colClass: '',
            colImage: '',
            width: '',
            height: '',
            listHeight: '',
            boxSearch: false,
            colSearch: '',
            cols: 0,
            top_pos: 0,
            left_pos: 0,
            select_box_height: 0,
            customFunction: function () {
                //console.log('default function');
            }
        }, options);

        settings.colImage = ',' + settings.colImage + ',';
        settings.colSearch = ',' + settings.colSearch + ',';

        var $this = $(this);

        if ($this[0] == undefined)
            return;

        var selectors = $this.selector;
        var elements = $this;//document.querySelectorAll(selectors);
        for (var i = 0; i < elements.length; i++) {
            initComboBox($(elements[i]), settings, i + 1);
        }
    };

    function getSettingElement($this, settings) {
        var select_box_height = $this.outerHeight(true);
        var top_pos = $this.offset().top;
        var left_pos = $this.offset().left;

        settings.top_pos = top_pos;
        settings.left_pos = left_pos;
        settings.select_box_height = select_box_height;
        settings.width = settings.width != '' && settings.width != null ? settings.width : $this.outerWidth(true);
        settings.width += 40;
        settings.height = settings.height != '' && settings.height != null ? settings.height : 270;
        settings.listHeight = settings.height - 65 - (settings.boxSearch == true ? 55 : 0);
        settings.colSizeDefault = 100 / (settings.colTitle.split(',').length - (settings.colHide == '' ? 0 : settings.colHide.split(',').length)) + '%';

        return settings;
    };

    function initComboBox($this, settings, rcb_id) {
        $list_option = $this.find('option:not([value=""])');

        //lấy các thông số cài đặt từ select box này
        settings = getSettingElement($this, settings);

        //if ($this.parent().find('.rcbSlide').length != 0) {
        //    $this.parent().find('.rcbSlide').remove();
        //}

        if ($this.hasClass('control-rcb') == false) {
            $this.addClass('control-rcb');
            $this.attr('data-rcb', 'rcb_' + ($('.rcbSlide').length + 1));
        }

        $rcbSlide = initTable(settings, $this);
        //thêm vào ngay sau select box
        if ($('body .rcbContainer').length == 0) {
            $('body').prepend($('<div class="size-narrow rcbContainer"></div>'));
        }

        $('body .rcbContainer').append($rcbSlide);
        $rcbSlide.insertAfter($this);

        //Khi click textbox sẽ hiển thị dropdown
        var $txtSearch = $rcbSlide.find('.rcb-input-search');

        $txtSearch.on("focus", function (e) {
            $("#rcb_" + rcb_id + "_RadComboBox").show();

        });

        $txtSearch.on("keydown", function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        //sự kiện khi bấm ra ngoài thì ẩn bảng        
        $(window).click(function () {
            $("#rcb_" + rcb_id + "_RadComboBox").hide();
            $("#rcb_" + rcb_id + "_RadComboBox").find('tr.rcbItem').removeClass('hideItem');
        });

        $('.rcbSlide').bind('click', function (event) {
            event.stopPropagation();
            $("#rcb_" + rcb_id + "_RadComboBox").find('tr.rcbItem').removeClass('hideItem');
            $("#rcb_" + rcb_id + "_RadComboBox").find('tr.rcbItem').removeClass('selected');
        });

        $this.bind('click', function (event) {
            event.stopPropagation();
            $("#rcb_" + rcb_id + "_RadComboBox").find('tr.rcbItem').removeClass('hideItem');
            $("#rcb_" + rcb_id + "_RadComboBox").find('tr.rcbItem').removeClass('selected');
        });

        //gọi hàm từ cài đặt ban đầu truyền vào
        settings.customFunction.call();

        //khởi tạo các sự kiện của bảng
        initEvent(settings, $this, $rcbSlide, rcb_id);

    };

    function initTable(settings, $elemSelect) {

        var $list_option = $elemSelect.find('option:not([value=""])').clone();

        var $rcbSlide = $('<div class="rcbSlide" id="' + $elemSelect.attr('data-rcb') + '_rcbSlide"></div>');

        var offsetTop_rcbSlide = ($(document).height() - settings.top_pos) > settings.height ? (settings.top_pos) : (settings.top_pos - settings.height);
        $rcbSlide.css({
            "z-index": 1,
            "display": "block",
            //"height": settings.height + "px",
            //"width": "500px",
            "top": "0px",
            "left": "0px",
            "overflow": "hidden"
        });

        var $RadComboBoxDropDown = $('<div id="' + $elemSelect.attr('data-rcb') + '_RadComboBox" class="RadComboBoxDropDown RadComboBoxDropDown_Silk" style="width: ' + settings.width + 'px; display: none; top: 0px; visibility: visible; transition: none;"></table>');
        var $rcbHeader = $('<div class="rcbHeader"></div>');
        var search_panel = "";

        if (settings.colTitle != '') {
            var arr_colTitle = settings.colTitle.trim().split(',');
            settings.cols = arr_colTitle.length;

            //nếu có truyền độ rộng của từng cột thì lấy ứng với index của cột đó, nếu không có thì lấy độ rộng mặc định 
            var arr_col_size = settings.colSize == '' ? [] : settings.colSize.split(',');

            var ul_header = '<table width=100%><tr>';
            search_panel = '<div class="rcbSearch">';
            for (var index = 1; index <= arr_colTitle.length; index++) {
                var css_col_w = (arr_col_size[index - 1] != '' && arr_col_size[index - 1] != '0px' ? 'width:' + arr_col_size[index - 1] : '');
                var title = arr_colTitle[index - 1];
                var class_col_hide = (settings.colHide.indexOf(index.toString()) != -1 ? " hideItem" : "");
                var class_col_search_hide = (settings.colImage.indexOf(index.toString()) != -1 ? " hideItem" : "");
                var class_col_main_search = (settings.colSearch.indexOf(index.toString()) != -1 ? " txtSearch" : "");

                ul_header += '<td class="colHeader col' + index + class_col_hide + '" style="' + css_col_w + '">' + title + '</td>';
                //if (class_col_hide == '' && class_col_search_hide == '') {
                if (settings.colSearch.indexOf(index.toString()) != -1) {
                    search_panel += '<input data-index="' + index + '" class="rcb-input-search ' + class_col_main_search + '" style="width: ' + settings.width + 'px" type="text" placeholder="Tìm kiếm..." autocomplete="off"/>';
                }
            }
            ul_header += '</tr></table>';
            search_panel += '</div>';
            $rcbHeader.append($(ul_header));

            if (settings.boxSearch == true) {
                //$rcbHeader.append($(ul_search));
                //$rcbHeader.append($('<p class="label-guide-search">Sử dụng: [&gt;, &gt;=, &lt;, &lt;=, =] để tìm kiếm giá trị số, ví dụ: &gt;= 7, = 9,... </p>'));
            }
        }

        $RadComboBoxDropDown.append($rcbHeader);

        var $rcbScroll = $(' <div class="rcbScroll rcbWidth" style="height: ' + settings.listHeight + 'px;"></div>');
        //tạo danh sách các lựa chọn
        var $ul_rcbList = initOrUpdateListOption(settings, $elemSelect, $list_option);
        $rcbScroll.append($ul_rcbList);

        $RadComboBoxDropDown.append($rcbScroll);

        //var $rcbFooter = $('<div class="rcbFooter"></div>');
        //$RadComboBoxDropDown.append($rcbFooter);

        //Bắt đầu add từng phần tử vào
        //Đầu tiên là search box
        $rcbSlide.append($(search_panel));

        $rcbSlide.append($RadComboBoxDropDown);

        updateCountItemFooter($rcbSlide, $list_option);
        var $rcb = $("<div class='rcb'></div>");
        $rcb.append($rcbSlide);
        return $rcb;
    };

    function initOrUpdateListOption(settings, $elemSelect, $list_option) {
        //list option
        var $ul_rcbList = $('<table class="rcbList"></table>');
        for (var iOpt = 0; iOpt < $list_option.length; iOpt++) {
            var elemOpt = $list_option[iOpt];
            var arr_value_for_col = $(elemOpt).data('value') != undefined ? $(elemOpt).data('value').split('|') : [];

            //nếu có truyền độ rộng của từng cột thì lấy ứng với index của cột đó, nếu không có thì lấy độ rộng mặc định 
            var arr_col_size = settings.colSize == '' ? [] : settings.colSize.split(',');
            var arr_col_class = settings.colClass == '' ? [] : settings.colClass.split(',');

            var $li_rcbItem = $('<tr class="rcbItem rcbTemplate ' + ($(elemOpt).attr('value') == $elemSelect.val() ? 'selected ' : '') + ($(elemOpt).is(':disabled') == true ? 'disabled ' : '') + ($(elemOpt).css('display') == 'none' ? 'hideItem ' : '') + '"></tr>');
            var row_item = '';
            for (var numCol = 1; numCol <= settings.cols; numCol++) {
                var css_col_w = (arr_col_size[numCol - 1] != '' && arr_col_size[numCol - 1] != '0px' ? 'width:' + arr_col_size[numCol - 1] : '');
                var class_col_hide = (settings.colHide.indexOf(numCol.toString()) != -1 ? " hideItem" : "");
                var value = settings.colImage.indexOf("," + numCol + ",") != -1 ? '<img src="' + arr_value_for_col[numCol - 1].trim() + '" />' : arr_value_for_col[numCol - 1].trim();

                row_item += '<td class="rcbCol colValue ' + (arr_col_class[numCol - 1] || "") + ' col' + numCol + class_col_hide + '" style="' + css_col_w + '; ' + (settings.colImage.indexOf("," + numCol + ",") == -1 ? 'padding:0px 5px' : '') + '">'
                    + value + '</td>';
            }
            row_item += '';

            $li_rcbItem.append($(row_item));
            $ul_rcbList.append($li_rcbItem);
        };

        return $ul_rcbList;
    };

    function updateCountItemFooter($rcbSlide, $list_option) {
        //var $list_option_display = $list_option.filter(function () { return $(this).css("display") != "none" });
        //$rcbSlide.find('.rcbFooter').text('Tổng cộng ' + $list_option_display.length + ' dòng');
    };

    function initEvent(settings, $elemSelect, $rcbSlide, rcb_id) {

        //sự kiện khi chọn dòng 
        $rcbSlide.find('.rcbItem').click(function () {
            if ($(this).hasClass('disabled') == false) {
                $rcbSlide.find('.rcbItem').removeClass('selected');
                $(this).addClass('selected');
                var value_select = $(this).find('.colValue').eq(settings.colValue - 1).text();
                $elemSelect.val(value_select).trigger('change');
                //$rcbSlide.removeClass('open').css({ "z-index": 6000, "display": "block", "height": "0px", "width": "460px", "top": (settings.top_pos + settings.select_box_height) + "px", "left": settings.left_pos + "px", "overflow": "hidden" });
                //$rcbSlide.removeClass('open').hide();
                $("#rcb_" + rcb_id + "_RadComboBox").hide();
                $("#rcb_" + rcb_id + "_rcbSlide .rcbSearch input").val('');
            }
        });

        $('.rcbHoverImage').remove();
        var $imgHover = $('<div class="rcbHoverImage" ></div>').appendTo('body');
        $rcbSlide.find('.rcbItem img').mouseover(function (e) {
            var img = $(this).clone()[0];
            $imgHover.html(img.outerHTML);
        });

        $rcbSlide.find('.rcbItem img').mouseout(function (e) {
            $('.rcbHoverImage').html('');
        });

        $('.rcbItem img').mousemove(function (e) {
            //console.log(e.pageY);
            $(".rcbHoverImage").css({ 'top': e.pageY > 200 ? e.pageY - 200 : e.pageY, 'left': e.pageX + 20 });
        });

        //sự kiện tìm kiếm
        $rcbSlide.find('.rcb-input-search').val('');
        $rcbSlide.find('.rcb-input-search').keyup(function () {
            var $list_option = $rcbSlide.find('.rcbList .rcbItem');

            // Toán tử số học (Arithmetic operators), Toán tử quan hệ (Relational operator), Toán tử logic (Logical operator), Toán tử điều kiện (Condition operator), Các toán tử tăng, giảm (Increment and decrement operator), Toán tử gán (Assignment operator)
            var relational_operator = '>,>=,<,<=,=';
            var isSearchNumber = false;

            var content_search = $(this).val().trim();

            //lấy từ đầu tiên (tính bằng khoảng trắng) trong nội dung tìm kiếm, xem có các toán tử so sánh hay không để tìm kiếm cho giá trị số
            var first_word = content_search.split(' ')[0].trim().replace(/\d/g, '');
            if (relational_operator.indexOf(first_word) != -1) {
                isSearchNumber = true;

                // nếu tìm kiếm cho số thì bỏ đi các toán tử so sánh, chỉ giữ lại số
                content_search = content_search.replace(/\D/g, '');
            }

            content_search = ref_convertVNtoEN(content_search)

            if (content_search != '') {
                var index = $(this).data("index");

                $list_option.addClass('hideItem');
                for (var i = 0; i < $list_option.length; i++) {
                    var $col_find = $($list_option[i]).find('td').eq(index);

                    var content_col = ref_convertVNtoEN($col_find.text());
                    console.log($col_find);
                    if (content_col.indexOf(content_search) != -1) {
                        $col_find.closest('tr.rcbItem').removeClass('hideItem');
                    }
                }
            } else {
                $list_option.removeClass('hideItem');
            }

            updateCountItemFooter($rcbSlide, $list_option);
        });

    };

    function relationalOperator(number1, number2, operator) {
        var flag = false;
        switch (operator) {
            case '>':
                flag = number1 > number2;
                break;
            case '>=':
                flag = number1 >= number2;
                break;
            case '<':
                flag = number1 < number2;
                break;
            case '<=':
                flag = number1 <= number2;
                break;
            default:
                flag = number1 == number2;
                break;
        }
        return flag;
    };

})(jQuery);

function ref_convertVNtoEN(str) {
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