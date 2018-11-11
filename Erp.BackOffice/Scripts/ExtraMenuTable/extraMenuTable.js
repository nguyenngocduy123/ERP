/*
<div class="extra-menu-table" data-table=".grid-table">
    <div class="navbar-buttons navbar-header pull-right" role="navigation">
        <ul class="nav ace-nav">
            <li class="grey"><a class="btn btn-sm sticky-header-table"><i class="fa fa-tags"></i></a></li>
            <li class="purple"><a class="btn btn-sm search-table"><i class="fa fa-search"></i></a></li>
            <li class="light-blue dropdown-modal">
                <a data-toggle="dropdown1" href="javascript:;" class="dropdown-toggle" aria-expanded="true">
                    <i class="fa fa-eye dropdown-toggle-icon"></i> <i class="ace-icon fa fa-caret-down dropdown-toggle-icon"></i>
                </a>
                <ul class="dropdown-menu-right dropdown-menu dropdown-caret dropdown-close column-show"></ul>
            </li>
        </ul>
    </div>
</div>
*/

$(document).ready(function () {

    var $table = $($('.extra-menu-table').closest('.extra-menu-table').data('table'));

    var $column_show = $('.extra-menu-table .column-show');
    var json_column_show = [];
    if ($column_show.length != 0) {
        
        let column_table = $table.find('thead tr th').filter(function () {
            return $(this).text().trim() != '';
        });
        for (let index = 0; index < column_table.length; index++) {
            if (column_table[index].innerText != '') {
                json_column_show.push({
                    selector: ':nth-child(' + (index + 1) + ')',
                    label: column_table[index].innerText
                });
                var $li = $('<li><label><input type="checkbox" checked value="' + json_column_show[index].selector + '" class="ace" /><span class="lbl label-show-column">' + json_column_show[index].label + '</span></label></li>');
                $column_show.append($li);
            }
        }

        $('body').on('change', '.column-show input' ,function () {
            console.log(this);
            var selector = $(this).val();
            console.log(selector);
            var $td_selector = $table.find('tr td' + selector).toggle();

            $table.find('tr th' + selector).toggle();
        });

        var $column_show_parent = $column_show.parent();
        $column_show_parent.find('.dropdown-toggle').click(function () {
            $column_show_parent.toggleClass('open');

            if (window.innerHeight <= 540) {
                $column_show.css({ width: '320px', right: 'auto', left: ((window.outerWidth - 320) / 2) + 'px', top: ((window.innerHeight - $column_show.outerHeight(true)) / 2) + 'px' });
            }
        });

        $('body').click(function (event) {
            console.log(event.target);
            if ($(event.target).closest(".dropdown-modal").length == 0 || $(event.target).hasClass('dropdown-modal')) {
                if ($column_show_parent.hasClass('open')) {
                    $column_show_parent.removeClass('open');
                    $column_show.attr('style', '');
                }
            }
        });
    }
    

    $('.extra-menu-table .sticky-header-table').click(function () {
        $(this).toggleClass('active');

        if ($table.hasClass('extra-menu-table-destination') == false) {

            var $tr_search = $table.find('tr.tr-search');
            if ($tr_search.length > 0) {
                $tr_search = $tr_search.clone();
                $table.find('tr.tr-search').remove();
                $table.find('tr.label-guide-search').remove();
            }

            //var total_th_has_width = 0;
            //var arr_th_none_width = [];
            $table.find('thead tr:first-child th').each(function (index, th) {
                var match_width = [];
                if ($(th).attr('style') != undefined) {
                    match_width = $(th).attr('style').toString().match(/width:\d+(px|%)/g);
                    var match_width_str = match_width != null ? match_width[0].replace('width:', '') : '';
                    //độ rộng tính chung về px 
                    //if (match_width.indexOf('%') != -1) {
                    //    width = (parseFloat(match_width.replace('%','')) * $table.width()) / 100;
                    //}
                }
                var width = $(th).width();//parseFloat(match_width.replace('px', ''));
                $(th).attr('oldwidth', match_width_str).width(width + 'px');
                //total_th_has_width += width;
                $table.find('tbody tr').each(function (index2, tr) {
                    $(tr).find('td:nth-child(' + (index + 1) + ')').width(width + 'px');
                });
            });

            if ($tr_search.length > 0) {
                $table.find('thead tr:first-child th').each(function (index, th) {
                    $tr_search.find('th:nth-child(' + (index + 1) + ')').width($(th).outerWidth(true) + 'px');
                });
                $table.find('thead').append($tr_search);
            }
        }

        $table.toggleClass('extra-menu-fixed-table-header');

    });

    $('.extra-menu-table .search-table').click(function () {
        var $table = $($(this).closest('.extra-menu-table').data('table'));
        $(this).toggleClass('active');

        if ($table.find('tr.tr-search').length > 0) {
            $table.find('tr.tr-search').remove();
            $table.find('tr.label-guide-search').remove();
            return;
        }

        var $tr_search = $table.find('thead tr:first-child').clone().addClass('tr-search');
        $tr_search.find('th').each(function (index, th) {
            $(th).width($(th).outerWidth(true)).html('<input type="text" value="" class="input-search" style="width:100%" />');
        });

        var label_guide_search = $('<tr class="label-guide-search"><th colspan="' + $tr_search.find('th').length + '"><p>Sử dụng: [&gt;, &gt;=, &lt;, &lt;=, =] để tìm kiếm giá trị số, ví dụ: &gt;= 7, = 9,... </p></th></tr>');
        $table.find('thead').append($tr_search).append(label_guide_search);

        $table.toggleClass('extra-menu-search-table');
    });

    $('body').on('keyup', '.extra-menu-search-table .input-search', function (e) {
        var $this = $(this);
        var caret = $this.caret();
        if (e.keyCode == 37 && caret.begin == 0) {
            var $prev = $this.closest('th').prev('th');
            if ($prev.length != 0) {
                $prev.find('input').focus();
            }
        }

        if (e.keyCode == 39 && caret.end == $this.val().length) {
            var $next = $this.closest('th').next('th');
            if ($next.length != 0) {
                $next.find('input').focus();
            }
        }
    });

    $('body').on('keyup', '.extra-menu-search-table .input-search', function () {
        var $this = $(this);
        var first_word = $.trim($this.val()) != '' ? $.trim($this.val())[0] : '';
        var relational_operator = '>,>=,<,<=,=';
        if (relational_operator.indexOf(first_word) != -1 && first_word != '') {
            //nếu input chỉ mới nhập các kí tự so sánh thì return về 1 để vẫn cho hiện dòng dữ liệu
            if ($.trim($this.val()).length == 1)
                return;
        }

        searchByColumn($this);
    });
});

function searchByColumn($input_element) {

    var $tr_search = $input_element.closest('tr');
    var arr_input_has_value = $tr_search.find('input').filter(function () {
        return $.trim(this.value) != '';
    }).map(function (index_in_result, elem) {
        var index_parent = $tr_search.find('th').index($(elem).parent('th'));
        return { index_col: index_parent, value: elem.value, elem: elem };
    }).get();


    var $list_row = $input_element.closest('table').find('tbody tr');
    $list_row.addClass('hideItem');
    for (var i = 0; i < $list_row.length; i++) {
        //console.log('begin row');
        var flag_col = 0;
        var num_col = 0;
        while (num_col < arr_input_has_value.length) {
            var $col_find = $($list_row[i]).find('td').eq(arr_input_has_value[num_col].index_col);
            var content_col = convertVNtoEN($col_find.text());
            flag_col += compareResult(content_col, arr_input_has_value[num_col].value, arr_input_has_value[num_col].elem);
            num_col++;
        }

        if (flag_col == arr_input_has_value.length)
            $($list_row[i]).removeClass('hideItem');

        //console.log('end row');
    }// kết thúc duyệt qua tât cả các dòng

};

function compareResult(content_col, content_search, input_element) {

    // Toán tử số học (Arithmetic operators), Toán tử quan hệ (Relational operator), Toán tử logic (Logical operator), Toán tử điều kiện (Condition operator), Các toán tử tăng, giảm (Increment and decrement operator), Toán tử gán (Assignment operator)
    var relational_operator = '>,>=,<,<=,=';
    var isSearchNumber = false;

    //lấy từ đầu tiên (tính bằng khoảng trắng) trong nội dung tìm kiếm, xem có các toán tử so sánh hay không để tìm kiếm cho giá trị số
    var first_word = content_search.split(' ')[0].trim().replace(/\d|\./g, '');
    if (relational_operator.indexOf(first_word) != -1 && first_word != '') {
        isSearchNumber = true;

        // nếu tìm kiếm cho số thì bỏ đi các toán tử so sánh, chỉ giữ lại số
        content_search = content_search.replace(/\D/g, '');

        var number_format = content_search.replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&.');
        $(input_element).val(first_word + number_format);
    }

    content_search = convertVNtoEN(content_search);

    var flag = 0;
    if (isSearchNumber == false) { // tìm kiếm cho chữ
        //console.log('string', 'content_search:' + content_search + ', content_col:' + content_col);
        if (content_col.indexOf(content_search) != -1) {
            flag = 1; //console.log('tim thay');
        }
    } else { // tìm kiếm cho số
        var content_col = content_col.replace(/\D/g, ''); //console.log('number', 'content_search:' + content_search + ', content_col:' + content_col);
        if (isNaN(content_col) == false) { // nếu nội dung của cột tìm kiếm phải là số thì mới kiểm tra
            if (relationalOperator(parseFloat(content_col), parseFloat(content_search), first_word)) {
                flag = 1; //console.log('tim thay');
            }
        }
    }
    return flag;
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