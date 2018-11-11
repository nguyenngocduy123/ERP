
(function ($) {
    $.fn.tableFixedColumn = function (options) {
        var settings = $.extend({
            leftTableWidth: '20', //%
            rightTableWidth: '80', //%
            leftTableColumnWidth: [20, 80], //%
            rightTableColumnWidth: [20, 70], //px
            columnHeight: 35, //px
            numberColumnFixedLeft: 2,
            numberColumnFixedTop: 1,
            contentHeight: '500px'
        }, options);
        var $this = this;

        if ($this[0] == undefined)
            return;

        if ($this.find('table').length == 0)
            return;

        //var selectors = $this.selector;
        //var elements = document.querySelectorAll(selectors);
        for (var i = 0; i < $this.length; i++) {
            init($this[i], settings);
        }
    };

    function init(elem, settings) {
        $this = $(elem);
        $this.addClass('table-fixed-column-container');
        $this.find('th').addClass('nowrap');

        var tbodyContentHeight = 0;
        var leftSideTableWidth = $this.width() * parseFloat(settings.leftTableWidth.replace('%', '')) / 100;
        var rightSideTableWidth = $this.width() * parseFloat(settings.rightTableWidth.replace('%', '')) / 100;
        leftSideTableWidth = Math.round(leftSideTableWidth) - 2;
        console.log('leftSideTableWidth', leftSideTableWidth);
        rightSideTableWidth = Math.round(rightSideTableWidth) - 1;
        console.log('rightSideTableWidth', rightSideTableWidth);

        var contentLeftThead = '';
        var contentLeftTbody = '';
        var selectorChildLeftThead = '';
        var selectorChildLeftTbody = '';

        //tạo bộ chọn các ô cần lấy cho bảng bên trái
        for (var i = 1; i <= settings.numberColumnFixedLeft; i++) {
            selectorChildLeftThead += 'th:nth-child(' + i + ')' + (i != settings.numberColumnFixedLeft ? ',' : '');
            selectorChildLeftTbody += 'td:nth-child(' + i + ')' + (i != settings.numberColumnFixedLeft ? ',' : '');
            contentLeftThead += $this.find('thead tr th:nth-child(' + i + ')')[0].outerHTML;
        }

        //xóa đi tất cả các ô tiêu đề đã được tách qua bảng bên trái
        $this.find('thead th').filter(selectorChildLeftThead).remove();

        contentLeftThead = '<thead><tr>' + contentLeftThead + '</tr></thead>';

        //tách các ô dữ liệu của tbody cần lấy theo bộ chọn đã tạo ở trên cho bảng bên trái
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

        //tạo nội dung HTML cần hiển thị
        var $tableContainer = $('<div class="table-fixed-main-content"><div class="table-fixed-top-content"></div><div class="table-fixed-middle-content"><div class="scrollbar-middle-content"><div class="scrollbar-middle"></div></div><div style="width:' + settings.leftTableWidth + '" class="left-table"><table class="table">' + contentLeftThead + contentLeftTbody + '</table></div><div style="width:' + settings.rightTableWidth + '; left:' + settings.leftTableWidth + '" class="right-table">' + $this.find('table')[0].outerHTML + '</div></div></div>');

        //copy các class CSS của bảng bên phải ( là bảng ban đầu cho bảng bên trái);
        $tableContainer.find('.left-table table').attr('class', $tableContainer.find('.right-table table').attr('class'));

        // cài đặt chiều rộng cho các ô của bảng bên trái
        $tableContainer.find('.left-table table th').each(function (index, th) {
            var widthPercent = settings.leftTableColumnWidth[index] != undefined ? settings.leftTableColumnWidth[index] : settings.leftTableColumnWidth[settings.leftTableColumnWidth.length - 1];
            var width = Math.round(leftSideTableWidth * widthPercent / 100) - 1;
            $(th).width(width + 'px');

            $tableContainer.find('.left-table table tbody td:nth-child(' + (index + 1) + ')').width(width + 'px');
        });

        // cài đặt chiều rộng cho các ô của bảng bên phải
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
        //cài đặt chiều rộng cho bảng và các dòng bên phải 
        $tableContainer.find('.right-table table, .right-table table thead tr, .right-table table tbody tr').width(rightTableWidth + 'px');


        //xử việc việc có bao nhiêu dòng header không cuộn
        for (var i = 2; i <= settings.numberColumnFixedTop; i++) {
            //lấy các dòng theo thứ tự từ trên xuống của phần tbody của cả 2 bên (bắt đầu từ i-1)
            var $tr_left_tbody = $tableContainer.find('.left-table tbody tr:nth-child(' + (i - 1) + ')');
            var $tr_right_tbody = $tableContainer.find('.right-table tbody tr:nth-child(' + (i - 1) + ')');

            var tr_left_thead = $tr_left_tbody.clone()[0].outerHTML.replace(/<td/g, '<th').replace(/td>/g, 'th>');
            $tableContainer.find('.left-table thead').append($(tr_left_thead));
            $tr_left_tbody.remove();

            var tr_right_thead = $tr_right_tbody.clone()[0].outerHTML.replace(/<td/g, '<th').replace(/td>/g, 'th>');
            $tableContainer.find('.right-table thead').append($(tr_right_thead));
            $tr_right_tbody.remove();
        }


        //cài đặt chiều cao cho tất cả các ô trong hai bảng
        var tdHeight = settings.columnHeight.toString().replace(/\D/g, '');
        tdHeight = parseFloat(tdHeight);
        $tableContainer.find('table td').css({ 'height': tdHeight + 'px', 'max-height': tdHeight + 'px' });

        tbodyContentHeight = settings.columnHeight * $tableContainer.find('.right-table table tbody tr').length;

        //nếu chiều cao của tất cả các dòng nhỏ hơn chiều cao cài đặt thì lấy chiều cao của tất cả các dòng
        var num_contentHeight = parseFloat(settings.contentHeight.replace(/\D/g,''));
        settings.contentHeight = num_contentHeight > tbodyContentHeight ? tbodyContentHeight + 'px' : settings.contentHeight;

        //đặt chiều cao cho tbody của bảng bên trái và bên phải để cuộn
        $tableContainer.find('.left-table table tbody, .right-table table tbody').height(settings.contentHeight);

        
        //đưa nội dung HTML vào container
        $this.html($tableContainer);

        //xử lý chiều cao cho các dòng header không cuộn
        for (var i = 2; i <= settings.numberColumnFixedTop; i++) {
            //lấy các dòng theo thứ tự từ trên xuống của phần tbody của cả 2 bên (bắt đầu từ i-1)
            var $tr_left_thead = $tableContainer.find('.left-table thead tr:nth-child(' + i + ')');
            var $tr_right_thead = $tableContainer.find('.right-table thead tr:nth-child(' + i + ')');
            var th_height_max = $tr_left_thead.height();
            var th_height_right = $tr_right_thead.height();

            if (th_height_right > th_height_max)
                th_height_max = th_height_right;

            $tr_left_thead.find('th').css('height', th_height_max);
            $tr_right_thead.find('th').css('height', th_height_max);
        }

        //cài đặt độ cao cho container
        setHeightContainer($this);

        //cài đặt chiều cao cho thanh scrollbar
        $tableContainer.find('.scrollbar-middle-content').css({ 'height': (parseFloat(settings.contentHeight.replace(/\D/g,'')) + 19), 'top': $this.find('table thead').outerHeight(true) + 'px' });


        $tableContainer.find('.scrollbar-middle-content .scrollbar-middle').height(tbodyContentHeight + 'px');

        //cài đặt sự kiện scroll của thanh cuộn dọc phần nội dung
        $this.find('.scrollbar-middle-content').on('scroll', function () {
            $tableContainer.find('.left-table table tbody, .right-table table tbody').scrollTop($(this).scrollTop());
        });

        $this.find('.right-table table tbody').on('scroll', function () {
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
                biggestHeight = $(this).outerHeight(true);
            }
        });

        // Set the container height
        $elem.height(biggestHeight);
        $elem.find('.table-fixed-middle-content').height(biggestHeight - 2);
    }
}(jQuery));