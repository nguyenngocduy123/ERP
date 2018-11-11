/*********** khai báo biến ***************/
var city = $('#ShipCityId');
var districts = $('#ShipDistrictId');
var ward = $('#ShipWardId');
var promotion = {};

/*********** function ***************/
function loadContact(customerId) {
    //$('#ContactId').html('');
    //$.getJSON('/Contact/GetContactListByCustomerId', { customerId: customerId }, function (res) {
    //    for (var i in res) {
    //        var option = '<option value="' + res[i].Id + '" data-city="' + res[i].CityId + '" data-district="' + res[i].DistrictId + '" data-ward="' + res[i].WardId + '" data-address="/' + res[i].Address + '/" data-phone="' + res[i].Phone + '">' + res[i].LastName + ' ' + res[i].FirstName + '</option>';
    //        $('#ContactId').append($(option));
    //    }

    //    if (res.length == 0)
    //        $('#ContactId').html('<option value="">KH này không có liên hệ</option>');

    //    $('#ContactId').trigger("chosen:updated");
    //    $('#ContactId').trigger('change');
    //});

    //$.getJSON('/CustomerDiscount/GetDiscountLast', {
    //    customerId: customerId
    //}, function (res) {
    //    //console.log(res);
    //    var percent = res.ValuePercent == null ? 0 : parseInt(res.ValuePercent);

    //    $('#Discount').val(percent).trigger('change');
    //    $('#CustomerDiscountId').val(res.Id);
    //});
}

function searchProductByBarCodeContain(barcode) {
    barcode = barcode.toLowerCase();

    var $optionList = $("#productSelectList").find('option');

    var arrResulft = [];
    for (var i = 0; i < $optionList.length; i++) {
        var data_code = $($optionList[i]).data('code') != undefined ? $($optionList[i]).data('code').toString().toLowerCase() : undefined;
        if (barcode.indexOf(data_code) != -1)
            arrResulft.push($($optionList[i]).attr('value'));

        if (arrResulft.length == 1) {
            return arrResulft[0];
        }
    }

    return arrResulft[0];
}

function tinh_thanh_tien_moi_dong(id) {
    var input_price = $('#DetailList_' + id + '__Price');
    var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

    //Số lượng
    var input_qty = $('tr#product_item_' + id).find('.detail_item_qty');
    var qty = 1;
    if (input_qty.val() == '') {
        input_qty.val(1);
    } else {
        qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
    }
    //Thành tiền
    var total = parseFloat(price) * qty;
    var totalAmount = total;
    if ($('#setting_hiden').val() == "false") {
        //Chiết khấu
        var input_fixed_discount = $('tr#product_item_' + id).find('.detail_item_fixed_discount');
        var input_discount = $('tr#product_item_' + id).find('.detail_item_discount');
        var discount = 0;
        var fixed_discount = 0;
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(input_discount.val());
        }
        if (input_fixed_discount.val() == '') {
            input_fixed_discount.val(0);
        } else {
            fixed_discount = parseInt(input_fixed_discount.val());
        }

        var discountAmount = discount * total / 100;
        var fixeddiscountAmount = fixed_discount * total / 100;

        $('tr#product_item_' + id).find('.detail_item_discount_amount').val(numeral(discountAmount).format('0,0'));
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_amount').val(numeral(fixeddiscountAmount).format('0,0'));
    }
    $('tr#product_item_' + id).find('.detail_item_total').text(numeral(totalAmount).format('0,0'));
}

function tinh_tong_tien() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var selector = '.detailList tr';
    $(selector).each(function (index, elem) {
        if ($(elem).find('.detail_item_total').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total').text()));
            $("#TongThanhTien").text(numeral(total).format('0,0'));
        }

        if ($(elem).find('.detail_item_qty').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty').val().replace(/\-./g, ''));
            $("#TongSoLuong").text(currencyFormat(total1));
        }
        if ($('#setting_hiden').val() == "false") {
            //tong chiet khau co dinh
            if ($(elem).find('.detail_item_fixed_discount_amount').val() != '') { // la số thì mới tính

                var ck = removeComma($(elem).find('.detail_item_fixed_discount_amount').val());
                ckcd += parseFloat(ck);
                //console.log(ckcd);
                $('#mask-FixedDiscount').val(numeral(ckcd).format('0,0'));
                $('#FixedDiscount').val(numeral(ckcd).format('0,0'));
            }
            //tong chiet khau dot xuat
            if ($(elem).find('.detail_item_discount_amount').val() != '') { // la số thì mới tính
                var ck2 = removeComma($(elem).find('.detail_item_discount_amount').val());
                ckdx += parseFloat(ck2);
                $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
                $('#IrregularDiscount').val(numeral(ckdx).format('0,0'));
                //console.log(ckdx);
            }
        }
        if (index == $(selector).length - 1) {
            $('#mask-TotalAmount').val(numeral(total).format('0,0'));
            $('#TotalAmount').val(numeral(total).format('0,0'));

            var total_ckdx = ckdx;
            //   console.log("ckcd"+ckcd);
            //$('#mask-IrregularDiscount').val(total_ckdx).trigger("change");
            //$('#IrregularDiscount').val(total_ckdx).trigger("change");
            //$('#mask-FixedDiscount').val(Math.round(ckcd)).trigger("change");
            //$('#FixedDiscount').val(Math.round(ckcd)).trigger("change");

            //Tính tiền sau thế
            var vat = parseInt($("#TaxFee").val());
            var TongTienSauVAT = total + (vat * (total / 100));

            $("#TongTienSauVAT").val(numeral(TongTienSauVAT).format('0,0'));
            $('#mask-TongTienSauVAT').val(numeral(TongTienSauVAT).format('0,0'));
        }
    });
}

function findPromotion($detail_item_id) {
    var categoryCode = $detail_item_id.closest('tr').find('.detail_item_category_type').val();
    var productId = $detail_item_id.val();
    var quantity = $detail_item_id.closest('tr').find('.detail_item_qty').val();
    quantity = parseInt(quantity);

    //1: ưu tiên cho sản phẩm
    var promotion_product = promotion.productList.filter(function (obj) {
        return obj.ProductId == productId && obj.QuantityFor >= quantity;
    });

    //console.log('promotion_product', promotion_product);
    if (promotion_product.length > 0) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_product[0].PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_product[0].PromotionId);
        $detail_item_id.closest('tr').find('.detail_item_promotion_detail_id').val(promotion_product[0].Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_product[0].PercentValue);

        var promotionItem = promotion.promotionList.find(function (obj) {
            return obj.Id == promotion_product_category.PromotionId;
        });
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotionItem != undefined ? promotionItem.Name : "");

        return;
    }

    //2: xét đến danh mục: tất cả sản phẩm (hàm find chỉ trả về phần tử đầu tiên tìm đc)
    var promotion_product_category = promotion.productCategoryList.find(function (obj) {
        return obj.ProductCategoryCode == categoryCode;
    });
    //console.log('promotion_product_category', promotion_product_category);
    if (promotion_product_category != undefined) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_product_category.PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_product_category.PromotionId);
        $detail_item_id.closest('tr').find('.detail_item_promotion_detail_id').val(promotion_product_category.Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_product_category.PercentValue);

        var promotionItem = promotion.promotionList.find(function (obj) {
            return obj.Id == promotion_product_category.PromotionId;
        });
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotionItem != undefined ? promotionItem.Name : "");

        return;
    }

    //3: xét đến cho tất cả sản phẩm
    var promotion_all = promotion.promotionList.find(function (obj) {
        return obj.IsAllProduct == true;
    });

    //console.log('promotion_all', promotion_all);
    if (promotion_all != undefined) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_all.PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotion_all.Name);

        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_all.Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_all.PercentValue);
        return;
    }

    //nếu không có thì mặc định là 0
    $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text('0%');
    return;
}

function checkChosenProductOnTable() {
    var flag = true;
    $('#detailList select.detail_item_id').each(function (index, elem) {
        if ($(elem).val() == '') {
            var message = $(elem).data('val-required') != undefined ? $(elem).data('val-required') : 'Chưa chọn sản phẩm!';
            $(elem).next('span').text(message);
            flag = false;
        }
    });
    return flag;
}

//hàm gọi lại từ form tạo mới khách hàng
//function ClosePopupAndDoSomethings(optionSelect) {
//    ClosePopup(false);
//    $("#CustomerId").val($(optionSelect).val()).triggerHandler('change');
//    $("#CustomerId_DisplayText").val($(optionSelect).text()).triggerHandler('change');
//}

//Chọn sản phẩm vào đơn hàng
//function selectItem(id, lo, expirydate) {
//    if (id != undefined) {

//        for (var i in list) {
//            var obj = list[i];
//            if (obj.Id == id && obj.Lo == lo && obj.ExpiryDate == expirydate) {
//                var OrderNo = $('.detailList tr').length;
//                var ProductId = obj.Id;
//                var ProductName = obj.Name;
//                var Unit = obj.Unit;
//                var Quantity = 1;
//                var Price = obj.PriceOutbound;
//                var ProductType = obj.Type;
//                var ProductCode = obj.Code;
//                var QuantityInventory = obj.InventoryQuantity;
//                var LoCode = obj.LoCode;
//                var ExpiryDate = obj.ExpiryDate;
//                var formdata = {
//                    OrderNo: OrderNo,
//                    ProductId: ProductId,
//                    ProductName: ProductName,
//                    Unit: Unit,
//                    Quantity: Quantity,
//                    Price: Price,
//                    ProductType: ProductType,
//                    ProductCode: ProductCode,
//                    QuantityInventory: QuantityInventory,
//                    LoCode: LoCode,
//                    ExpiryDate: ExpiryDate

//                };

//                //Thêm dòng mới
//                ClickEventHandler(true, "/ProductInvoice/LoadProductItemNT", ".detailList", formdata, function () {

//                    $('#ProductItemCount').val($('#listOrderDetail .detailList tr').length);
//                    tinh_tong_tien();
//                    $.mask.definitions['~'] = '[+-]';
//                    $('.input-mask-date').mask('99/99/9999');

//                    $('#DetailList_' + OrderNo + '__Quantity').numberOnly();
//                    $('#DetailList_' + OrderNo + '__Price').numberFormat();
//                    $('#DetailList_' + OrderNo + '__FixedDiscount').numberOnly();
//                    $('#DetailList_' + OrderNo + '__IrregularDiscount').numberOnly();
//                    $('#DetailList_' + OrderNo + '__FixedDiscountAmount1').numberFormat();
//                    $('#DetailList_' + OrderNo + '__IrregularDiscountAmount1').numberFormat();

//                    //if ($('#DetailList_' + OrderNo + '__FixedDiscount').val() > 0) {
//                    //    $('#DetailList_' + OrderNo + '__FixedDiscount').val();

//                    //}
//                    //else {
//                    //    $('#DetailList_' + OrderNo + '__FixedDiscount').val($("#InputFixedDiscount").val()).trigger("change");
//                    //}

//                    //if ($('#DetailList_' + OrderNo + '__IrregularDiscount').val() > 0) {
//                    //    $('#DetailList_' + OrderNo + '__IrregularDiscount').val();

//                    //}
//                    //else {
//                    //    $('#DetailList_' + OrderNo + '__IrregularDiscount').val($("#InputDiscount").val()).trigger("change");
//                    //}

//                    $("#DetailList_" + OrderNo + "__Quantity").focus().select();


//                });
//                break;
//            }
//        }
//    }

//    $("#productName").val("").focus();
//    $("#product_search_control .result").html("");
//}
/*********** page load ***************/
$(function () {
    $("#categorySelectList").hide();
    LoadNumberInput();
    //init rcb chọn sản phẩm
    $('#productSelectList').radComboBox({
        colTitle: 'ID, Hình, Tên sản phẩm',
        colValue: 1,
        colImage: 2,
        colHide: '1',
        colSize: '0px,50px,',
        colClass: ',,',
        //width: 600,
        height: 300,
        boxSearch: true,
        colSearch: 2
    });

    //$('#TotalAmount').numberFormat();
    //$('.detail_item_price').numberFormat('before');
    $('#PaymentViewModel_Amount').val('0').numberFormat();
    $("#ProductItemCount").attr("readonly", "true");

    //Hiển thị giá và tính thành tiền khi chọn sản phẩm
    $('#productSelectList').on('change', function () {
        var $this = $(this);
        var selected = $this.find("option:selected");

        if (selected.val() == '' || $('#product_item_' + selected.val()).length > 0)
            return;

        var OrderNo = $('.detailList tr').length;
        var ProductId = selected.data("product-id");
        var ProductName = selected.text();
        var Unit = selected.data("unit");
        var Quantity = 1;
        var Price = selected.data("price");
        var ProductType = selected.data("product-type");
        var ProductCode = selected.data("code");
        var QuantityInventory = selected.data("quantity-inventory");
        var DrugStore = $("#BranchId").find("option:selected").val();
        var LoCode = selected.data("lo-code");
        var ExpiryDate = selected.data("expiry-date");
        var formdata = {
            OrderNo: OrderNo,
            ProductId: ProductId,
            ProductName: ProductName,
            Unit: Unit,
            Quantity: Quantity,
            Price: Price,
            ProductCode: ProductCode,
            ProductType: ProductType,
            QuantityInventory: QuantityInventory,
            LoCode: LoCode,
            ExpiryDate: ExpiryDate,
            DrugStore: DrugStore
        };

        //Thêm dòng mới
        ClickEventHandler(true, "/ProductInvoice/LoadProductItemNT", ".detailList", formdata, function () {
            $('#ProductItemCount').val($('#listOrderDetail .detailList tr').length);
            $.mask.definitions['~'] = '[+-]';
            $('.input-mask-date').mask('99/99/9999');
            LoadNumberInput();
            var _product_id = $("#DetailList_" + OrderNo + "__ProductId").val();
            var _LoCode = $("#DetailList_" + OrderNo + "__LoCode").val();
            var _ExpiryDate = $("#DetailList_" + OrderNo + "__ExpiryDate").val();
            var _quantity_inventory = $("#DetailList_" + OrderNo + "__Quantity").data("quantity-inventory");
            var _qty = $("#DetailList_" + OrderNo + "__Quantity").val();
            var selector = '.detailList tr';
            var quantity_used = 0;
            $(selector).each(function (index, elem) {
                if (index != OrderNo) {
                    var product_id = $("#DetailList_" + index + "__ProductId").val();
                    var LoCode = $("#DetailList_" + index + "__LoCode").val();
                    var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                    var Quantity = $("#DetailList_" + index + "__Quantity").val();
                    if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                        quantity_used += parseInt(removeComma(Quantity));
                    }
                }
            });
            var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
            var _quantity = parseInt(removeComma(_qty));
            $("#status").text("");
            if (_quantity > inventory_qty) {
                $("#DetailList_" + OrderNo + "__Quantity").val(inventory_qty);
                $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
            }
            tinh_thanh_tien_moi_dong(OrderNo);
            tinh_tong_tien();
        });
    });

    //Khởi tạo giá trị khi load lên
    //Load thông tin cho trang Edit
    if ($("#CustomerId").val() != '') {
        loadContact($("#CustomerId").val());
    }

    //$('.detail_item_qty').numberOnly();
    //$('.detail_item_price').numberFormat();
    //$('.detail_item_discount').numberOnly();
    //$('.detail_item_fixed_discount').numberOnly();
    //$('#InputDiscount').numberOnly();
    //$('#TotalAmount').numberFormat();
    $('#TaxFee').numberOnly();
    //$('#TongTienSauVAT').numberFormat();
    //$('#FixedDiscount').numberFormat();
    //$('#IrregularDiscount').numberFormat();
    tinh_tong_tien();

    $("#productCode").focus();

    //Event
    var timeout = null;
    $("#productName").keyup(function (e) {
        if (e.which == 13 || e.which == 37 || e.which == 38 || e.which == 39 || e.which == 40)
            return;

        var searchText = $(this).val().trim();

        //if you already have a timout, clear it
        if (timeout) { clearTimeout(timeout); }

        //start new time, to perform ajax stuff in 500ms
        timeout = setTimeout(function () {
            $("#product_search_control .result").html("");
            // Toán tử số học (Arithmetic operators), Toán tử quan hệ (Relational operator), Toán tử logic (Logical operator), Toán tử điều kiện (Condition operator), Các toán tử tăng, giảm (Increment and decrement operator), Toán tử gán (Assignment operator)
            var relational_operator = '>,>=,<,<=,=';
            var isSearchNumber = false;

            //lấy từ đầu tiên (tính bằng khoảng trắng) trong nội dung tìm kiếm, xem có các toán tử so sánh hay không để tìm kiếm cho giá trị số
            var first_word = searchText.split(' ')[0].trim().replace(/\d/g, '');
            if (relational_operator.indexOf(first_word) != -1) {
                isSearchNumber = true;

                // nếu tìm kiếm cho số thì bỏ đi các toán tử so sánh, chỉ giữ lại số
                searchText = searchText.replace(/\D/g, '');
            }

            searchText = convertVNtoEN(searchText);

            if (searchText != '') {
                var result = [];
                var numberOfItem = 0;
                for (var i in list) {
                    var obj = list[i];
                    if (numberOfItem <= 10) {
                        //Xét theo từng danh mục chọn
                        var checkCategory = false;
                        switch ($("#categorySelectList option:selected").val()) {
                            case "0": //1. Tất cả
                                checkCategory = true;
                                break;
                            case "1": //2. Sản phẩm
                                if (obj.Type == 'product')
                                    checkCategory = true;
                                break;
                            case "2": //3. Dịch vụ
                                if (obj.Type == 'service')
                                    checkCategory = true;
                                break;
                            default: //4. Danh mục cụ thể
                                if (obj.CategoryCode == $("#categorySelectList option:selected").val())
                                    checkCategory = true;
                                break;
                        }

                        if (checkCategory && convertVNtoEN(obj.Name).indexOf(searchText) != -1) {
                            result.push(obj);
                            numberOfItem++;
                        }
                    }
                }

                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var typeCss = result[i].Type == "product" ? "blue" : "orange";
                        var item = "<a class='item " + typeCss + "' data-id='" + result[i].Id + "' data-lo='" + result[i].LoCode + "' data-expiry-date='" + result[i].ExpiryDate + "'onclick='selectItem(" + result[i].Id + "," + result[i].LoCode + "," + result[i].ExpiryDate + ")'>" + result[i].Code + "-" + result[i].Name + " (Lô:" + result[i].LoCode + "-HSD:" + result[i].ExpiryDate + ")" + "</a>";
                        $("#product_search_control .result").append(item);
                    }

                    $("#product_search_control .result .item:first").addClass("selected");
                    $("#product_search_control .result").show();
                }
            }

        }, 300);
    });

    $("#productName").keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });
    $("#productName").click(function () {
        $("#product_search_control .result").html("");
        //for (i = 0; i < list.length; i++) {
        //        var typeCss = list[i].Type == "product" ? "blue" : "orange";
        //        var item = "<a class='item " + typeCss + "' data-id='" + list[i].Id + "' onclick='selectItem(" + list[i].Id + ")'>" + list[i].Name + " (" + list[i].Code + ")" + "</a>";
        //        $("#product_search_control .result").append(item);
        //    }
        //    $("#product_search_control .result .item:first").addClass("selected");
        //    $("#product_search_control .result").show();
        var result = [];
        var numberOfItem = 0;
        for (var i in list) {
            var obj = list[i];
            if (numberOfItem <= 10) {
                //Xét theo từng danh mục chọn
                var checkCategory = false;
                switch ($("#categorySelectList option:selected").val()) {
                    case "0": //1. Tất cả
                        checkCategory = true;
                        break;
                    case "1": //2. Sản phẩm
                        if (obj.Type == 'product')
                            checkCategory = true;
                        break;
                    case "2": //3. Dịch vụ
                        if (obj.Type == 'service')
                            checkCategory = true;
                        break;
                    default: //4. Danh mục cụ thể
                        if (obj.CategoryCode == $("#categorySelectList option:selected").val())
                            checkCategory = true;
                        break;
                }

                if (checkCategory) {
                    result.push(obj);
                }
            }
        }

        if (result.length > 0) {
            for (i = 0; i < result.length; i++) {
                var typeCss = result[i].Type == "product" ? "blue" : "orange";
                var item = "<a class='item " + typeCss + "' data-id='" + result[i].Id + "' data-lo='" + result[i].LoCode + "' data-expiry-date='" + result[i].ExpiryDate + "' onclick='selectItem(" + result[i].Id + "," + result[i].LoCode + "," + result[i].ExpiryDate + ")'>" + result[i].Code + "-" + result[i].Name + " (Lô:" + result[i].LoCode + "-HSD:" + result[i].ExpiryDate + ")" + "</a>";
                $("#product_search_control .result").append(item);
            }

            $("#product_search_control .result .item:first").addClass("selected");
            $("#product_search_control .result").show();
        }
    });
    $("#TaxFee").keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });

    $("#productName").keydown(function (e) {
        if (e.which == 37 || e.which == 39) {
            e.preventDefault();
        }

        if (e.which == 40) {
            e.preventDefault();
            var selectedItem = $("#product_search_control .result .item.selected");
            if (!selectedItem.is(':last-child')) {
                selectedItem.removeClass("selected").next().addClass("selected");
            }
        }
        else if (e.which == 38) {
            e.preventDefault();
            var selectedItem = $("#product_search_control .result .item.selected");
            if (!selectedItem.is(':first-child')) {
                selectedItem.removeClass("selected").prev().addClass("selected");
            }
        }
        else if (e.which == 13) {
            var selectedItem = $("#product_search_control .result .item.selected");
            var id = selectedItem.data("id");
            selectItem(id);
        }
    });

    //Sự kiện khi bấm ra ngoài thì ẩn khung tìm kiếm
    $(window).click(function () {
        $("#product_search_control .result").hide();
        $("#productName").val("");
        $("#product_search_control .result").html("");

        $("#popup_archive").removeClass("collapse in");
    });

    $('#productName').bind('click', function (event) {
        event.stopPropagation();
        $("#product_search_control .result").show();
    });

    $('#popup_archive').bind('click', function (event) {
        event.stopPropagation();
    });

    //khi nhập barcode
    $('#productCode').change(function () {
        var $this = $(this);
        if ($this.val() != '') {

            var barcode = $this.val();

            //đặt lại giá trị rỗng
            $this.val('').focus();

            var valueSearch = searchProductByBarCodeContain(barcode);
            if (valueSearch == undefined) {
                alert('Không tìm thấy sản phẩm với mã code trên!');
                return;
            }

            $('#productSelectList').val(valueSearch).trigger("change");
        }
    });

    //xóa sản phẩm
    $('#listOrderDetail').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#ProductItemCount').val('');
            $('#TongSoLuong').text('');
            $('#TongThanhTien').text('');
        }

        tinh_tong_tien();

        $('.detailList tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);

            $(tr).find('.detail-locode input').attr('name', 'DetailList[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail-locode input').attr('name', 'DetailList[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');
            //   $(tr).find('.detail_item_check').attr('name', 'DetailList[' + index + '].CheckPromotion').attr('id', 'DetailList_' + index + '__CheckPromotion');
            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount1');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount1');
        });
    });

    //change
    $('#CustomerId').change(function () {
        loadContact($(this).val());
    });



    $('#ContactId').change(function () {
        var $this = $(this);
        var $option = $this.find('option:selected');

        if ($option.val() != '') {

            $('#ShipName').val($option.text());
            $('#CustomerPhone').val($option.data('phone'));
            if ($option.data('address') != '/null/') {
                $('#ShipAddress').val($option.data('address').toString().replace(/\//g, ''));
            }
            else {
                $('#ShipAddress').val('');
            }
            city.val($option.data('city'));
            city.trigger("chosen:updated");
            city.trigger('change');

            setTimeout(function () { });

        } else {
            $('#ShipName, #CustomerPhone, #ShipAddress, #ShipCityId').val('').trigger('change');
            city.trigger("chosen:updated");
        }
    });

    //Khi thay đổi số lượng
    $('#listOrderDetail').on('change', '.detail_item_qty', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        $("#status").text("");
        if (_quantity > inventory_qty) {
            $("#DetailList_" + id + "__Quantity").val(inventory_qty);
            $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        }

        //tính tổng cộng
        tinh_thanh_tien_moi_dong(id);
        tinh_tong_tien();
    });

    //Khi thay đổi giá
    $('#listOrderDetail').on('change', '.detail_item_price', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong(id);
        tinh_tong_tien();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail').on('change', '.detail_item_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        //tính tổng cộng
        tinh_thanh_tien_moi_dong(id);
        tinh_tong_tien();
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail').on('change', '.detail_item_fixed_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        //tính tổng cộng
        tinh_thanh_tien_moi_dong(id);
        tinh_tong_tien();
    });
    //Khi thay đổi khuyến mãi hay k
    //$('#listOrderDetail').on('change', '.detail_item_check', function () {
    //    var $this = $(this);
    //    var id = $this.closest('tr').data('id');
    //    if ($('tr#product_item_' + id).find('.detail_item_check').is(':checked')) {
    //        $('tr#product_item_' + id).find('.detail_item_price').val(0);
    //        var q = $('tr#product_item_' + id).find('.detail_item_price').val();
    //        $('tr#product_item_' + id).find('.detail_item_check').val("True");
    //    }
    //    else {
    //        var q = $('tr#product_item_' + id).find('.pricetest').val().replace(/\./g, '');
    //        $('tr#product_item_' + id).find('.detail_item_price').val(q).trigger('change');
    //        $("#mask-DetailList_" + id + "__Price").val(currencyFormat(q));
    //        $('tr#product_item_' + id).find('.detail_item_check').val("False");
    //    }
    //    tinh_thanh_tien_moi_dong(id);
    //    tinh_tong_tien();
    //});

    //Khi thay đổi mức chiết khấu chung
    $('#InputDiscount').change(function () {
        if ($(this).val() != '') {
            $(".detail_item_discount").val($(this).val()).trigger("change");
        }
    });
    $('#InputFixedDiscount').change(function () {
        if ($(this).val() != '') {
            $(".detail_item_fixed_discount").val($(this).val()).trigger("change");
        }
    });
    //VAT thay đổi
    $('#TaxFee').change(function () {
        if ($(this).val() != '') {
            if ($(this).val() > 100) {
                $(this).val(100);
            }

            var total = removeComma($("#TotalAmount").val());
            var vat = parseInt($(this).val());

            var TongTienSauVAT = parseFloat(total) + (vat * (parseFloat(total) / 100));
            //console.log(total, vat, TongTienSauVAT);
            $("#TongTienSauVAT").val(numeral(TongTienSauVAT).format('0,0.00'));
            $('#mask-TongTienSauVAT').val(numeral(TongTienSauVAT).format('0,0.00'));
        }
    });

    //keyress
    $('#InputDiscount').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            if ($(this).val() != '') {
                $(".detail_item_discount").val($(this).val()).trigger("change");
            }
        }
    });
    $('#InputFixedDiscount').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            if ($(this).val() != '') {
                $(".detail_item_fixed_discount").val($(this).val()).trigger("change");
            }
        }
    });
    $('#listOrderDetail').on('keypress', '.detail_item_price', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail').on('keypress', '.detail_item_qty', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail').on('keypress', '.detail_item_discount', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $("#productCode").focus();
        }
    });

    $('#productCode').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $('#productCode').trigger('change');
        }
    });

    $(window).keydown(function (e) {
        if (e.which == 120 || e.which == 13) {
            e.preventDefault();
            $("#btnShowPayment").click();
        } else
            if (e.which == 119) {
                e.preventDefault();
                OpenPopup('/Customer/Index?IsPopup=true&jsCallback=selectItem_CustomerId', 'Tìm kiếm dữ liệu', 800, 600);
            }
            else if (e.which == 113) {
                e.preventDefault();
                $("#categorySelectList").trigger('chosen:activate');
            }
            else
                if (e.which == 114) {
                    e.preventDefault();
                    $("#productCode").focus();
                }
                else if (e.which == 115) {
                    e.preventDefault();
                    $("#productName").focus();
                }
    });

    //focus
    $('#InputDiscount').focus(function () {
        $(this).select();
    });
    $('#InputFixedDiscount').focus(function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_fixed_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_price', function () {
        $(this).select();
    });

    $('#listOrderDetail').on('focus', '.detail_item_qty', function () {
        $(this).select();
    });

    $('#TaxFee').focus(function () {
        $(this).select();
    });

    //Load quận huyện => xã phường
    var url = '/api/BackOfficeServiceAPI/FetchLocation';

    city.change(function () {
        districts.attr("disabled", "disabled");
        ward.attr("disabled", "disabled");

        var id = $(this).val(); // Use $(this) so you don't traverse the DOM again
        $.getJSON(url, { parentId: id }, function (response) {
            districts.empty();
            $(document.createElement('option'))
                    .attr('value', '')
                    .text('- Quận/Huyện -')
                    .appendTo(districts);

            ward.empty();
            $(document.createElement('option'))
                    .attr('value', '')
                    .text('- Xã/Phường -')
                    .appendTo(ward);

            $(response).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Id)
                    .text(capitalizeFirstAllWords(this.Name.toLowerCase()))
                    //.text(capitalizeFirstAllWords(this.Name.toLowerCase().replace('huyện', '').replace('quận', '')))
                    .appendTo(districts);
            });

            districts.removeAttr("disabled");
            ward.removeAttr("disabled");

            //var $option = $('#ContactId').find('option:selected');
            //districts.val($option.data('district'));

            districts.trigger("chosen:updated");
            districts.trigger('change');
        });
    });

    districts.change(function () {
        ward.attr("disabled", "disabled");

        var id = $(this).val(); // Use $(this) so you don't traverse the DOM again
        $.getJSON(url, { parentId: id }, function (response) {
            ward.empty();
            $(document.createElement('option'))
                    .attr('value', '')
                    .text('- Xã/Phường -')
                    .appendTo(ward);

            $(response).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Id)
                    .text(capitalizeFirstAllWords(this.Name.toLowerCase()))
                    .appendTo(ward);
            });

            ward.removeAttr("disabled");

            //var $option = $('#ContactId').find('option:selected');
            //ward.val($option.data('ward'));
            ward.trigger("chosen:updated");
        });
    });

    //Cho cái đếm tổng cộng nó readonly
    $("#ProductItemCount").attr("readonly", "true");

    //Thu tiền
    //$('#ReceiptViewModel_Amount').numberFormat();

    $('#AmountRemain').val('0');

    $('#ReceiptViewModel_Amount').focus(function () {
        $(this).select();
    });

    $('#ReceiptViewModel_Amount').blur(function () {
        //$("#TongTienSauVAT").val(numeral(Math.round(TongTienSauVAT)).format('0,0.00'));
        //$('#mask-TongTienSauVAT').val(numeral(Math.round(TongTienSauVAT)).format('0,0.00'));
        var totalAmount = parseFloat(removeComma($("#TongTienSauVAT").val()));
        var amount = parseFloat(removeComma($('#ReceiptViewModel_Amount').val()));
        if (amount < totalAmount) {
            $('.NextPaymentDate-container').show();
            $('#AmountRemain').val(numeral(totalAmount - amount).format('0,0'));
        }
        else
            $('.NextPaymentDate-container').hide();
    });

    $("#btnShowPayment").click(function () {
        $('#ReceiptViewModel_Amount').val($("#TongTienSauVAT").val());
        $('#mask-ReceiptViewModel_Amount').val($("#TongTienSauVAT").val());
        $('#mask-ReceiptViewModel_Amount').focus();
    });
});