
function findPromotion($detail_item_id) {
    var type = $detail_item_id.hasClass('type_service') ? "service" : "product";
    var categoryCode = $detail_item_id.closest('tr').find('.detail_item_category_type').val();
    categoryCode = categoryCode == '' ? null : categoryCode;
    var productId = $detail_item_id.val();
    var quantity = $detail_item_id.closest('tr').find('.detail_item_qty').val();
    quantity = parseInt(quantity);

    //1: ưu tiên cho sản phẩm
    var promotion_product = promotion.productList.filter(function (obj) {
        return obj.ProductId == productId && obj.QuantityFor >= quantity;
    });

    //promotion_product = promotion_product.sort(function (a, b) {
    //    if (a.PercentValue > b.PercentValue && (a.QuantityFor > b.QuantityFor || a.QuantityFor == b.QuantityFor)) {
    //        return 1;
    //    } else {
    //        if (a.PercentValue == b.PercentValue && (a.QuantityFor > b.QuantityFor || a.QuantityFor == b.QuantityFor)) {
    //            return 0;
    //        else
    //            return -1;
    //    }
    //});
    //console.log('promotion_product', promotion_product);
    if (promotion_product.length > 0) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_product[0].PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_product[0].PromotionId);
        $detail_item_id.closest('tr').find('.detail_item_promotion_detail_id').val(promotion_product[0].Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_product[0].PercentValue);

        var promotionItem = promotion.promotionList.find(function (obj) {
            return obj.Id == promotion_product[0].PromotionId;
        });
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotionItem != undefined ? promotionItem.Name : "");

        return;
    }

    //2: xét đến danh mục: tất cả sản phẩm (hàm find chỉ trả về phần tử đầu tiên tìm đc)
    var promotion_product_category = promotion.productCategoryList.find(function (obj) {
        return obj.CategoryCode == categoryCode;
    });
    //console.log('promotion_product_category', promotion_product_category);
    if (promotion_product_category != undefined && type == promotion_product_category.Type) {
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
};