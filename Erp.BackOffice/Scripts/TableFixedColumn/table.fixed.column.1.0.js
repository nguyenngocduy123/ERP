

(function ($) {
    $.fn.tableFixedColumn = function (options) {
        var settings = $.extend({
            leftTableWidth: '0px',
            rightTableWidth: '0px',
            leftTableColumnWidth: [20, 80],
            rightTableColumnWidth: [20, 70],
            columnHeight: 35,
            numberColumnFixedLeft: 2,
            numberColumnFixedTop: 1,
            contentHeight: '500px'
        }, options);
        var $this = this;

        if ($this[0] == undefined)
            return;

        if ($this.find('table').length == 0)
            return;

        var selectors = $this.selector;
        var elements = document.querySelectorAll(selectors);
        for (var i = 0; i < elements.length; i++) {
            init(elements[i], settings);
        }
    };

    function init(elem, settings) {
        $this = $(elem);
        $this.addClass('table-fixed-column-container');

        var leftTableWidth = $this.width() * parseFloat(settings.leftTableWidth.replace('%', '')) / 100;
        var rightTableWidth = $this.width() * parseFloat(settings.rightTableWidth.replace('%', '')) / 100;

        var contentLeftThead = '';
        var contentLeftTbody = '';
        var selectorChildLeftThead = '';
        var selectorChildLeftTbody = '';
        for (var i = 1; i <= settings.numberColumnFixedLeft; i++) {
            selectorChildLeftThead += 'th:nth-child(' + i + ')' + (i != settings.numberColumnFixedLeft ? ',' : '');
            selectorChildLeftTbody += 'td:nth-child(' + i + ')' + (i != settings.numberColumnFixedLeft ? ',' : '');
            contentLeftThead += $this.find('thead tr th:nth-child(' + i + ')')[0].outerHTML;
        }
        //xóa đi tất cả các ô tiêu đề đã được tách qua bảng bên trái
        $this.find('thead th').filter(selectorChildLeftThead).remove();

        contentLeftThead = '<thead><tr>' + contentLeftThead + '</tr></thead>';

        $this.find('tbody tr').each(function (index, tr) {
            var tdChild = $(tr).find(selectorChildLeftTbody);

            contentLeftTbody += '<tr>';
            for (var index_td = 0 ; index_td < tdChild.length; index_td++) {
                contentLeftTbody += tdChild[index_td].outerHTML;
            }
            contentLeftTbody += '</tr>';
            //xóa đi tất cả các ô nội dung đã được tách qua bảng bên trái
            tdChild.remove();
        });
        contentLeftTbody = '<tbody>' + contentLeftTbody + '</tbody>';

        var $tableContainer = $('<table class="table"><tbody><tr><td class="table-fixed-main-content"><div class="table-fixed-top-content"></div><div class="table-fixed-middle-content"><div style="width:' + settings.leftTableWidth + '" class="left-table"><table class="table">' + contentLeftThead + contentLeftTbody + '</table></div><div style="width:' + settings.rightTableWidth + '; left:' + settings.leftTableWidth + '" class="right-table">' + $this.find('table')[0].outerHTML + '</div></div></td></tr></tbody></table>');

        $tableContainer.find('.left-table table th').each(function (index, th) {
            var widthPercent = settings.leftTableColumnWidth[index] != undefined ? settings.leftTableColumnWidth[index] : settings.leftTableColumnWidth[settings.leftTableColumnWidth.length - 1];
            var width = leftTableWidth * widthPercent / 100;
            $(th).width(width + 'px');

            $tableContainer.find('.left-table table tbody td:nth-child(' + (index + 1) + ')').width(width + 'px');
        });

        var rightTableWidth = 0;
        $tableContainer.find('.right-table table th').each(function (index, th) {
            var width = settings.rightTableColumnWidth[index] != undefined ? settings.rightTableColumnWidth[index] : settings.rightTableColumnWidth[settings.rightTableColumnWidth.length - 1];
            $(th).width(width + 'px');
            rightTableWidth += $(th).outerWidth(true);

            $tableContainer.find('.right-table table tbody tr td:nth-child(' + (index + 1) + ')').width(width + 'px');

            if (th.nextElementSibling == null) {//tức là phần từ cuối thì đặt lại độ rộng của tất cả các td cuối của mỗi dòng (trừ đi 20px của thanh scroll bar)
                $tableContainer.find('.right-table table tbody tr td:last-child').width((width - 20) + 'px');
            }
        });
        $tableContainer.find('.right-table table, .right-table table thead tr, .right-table table tbody tr').width(rightTableWidth + 'px');

        $tableContainer.find('table td').each(function (index, td) {
            var height = settings.columnHeight.toString().replace(/\D/g, '');
            height = parseFloat(height);

            $(td).height(height + 'px');
        });

        //đặt chiều cao cho tbody của bảng bên trái và bên phải để cuộn
        $tableContainer.find('.left-table table tbody, .right-table table tbody').height(settings.contentHeight);

        $this.html($tableContainer);

        setHeightContainer($this);

        $tableContainer.find('.right-table table tbody').on('scroll', function () {
            $tableContainer.find('.left-table table tbody').scrollTop($(this).scrollTop());
        });
    };

    function setHeightContainer($elem) {
        var biggestHeight = 0;
        // Loop through elements children to find & set the biggest height
        $elem.find("table").each(function () {
            // If this elements height is bigger than the biggestHeight
            if ($(this).height() > biggestHeight) {
                // Set the biggestHeight to this Height
                biggestHeight = $(this).height();
            }
        });

        // Set the container height
        $elem.height(biggestHeight);
        $elem.find('.table-fixed-middle-content').height(biggestHeight);
    }
}(jQuery));