function InitVE() {
    Ext.define("InoutOrderDetailItemViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'order_detail_id', type: 'string' },
            { name: 'sku_id', type: 'string' },
            { name: 'item_code', type: 'string' },
            { name: 'item_name', type: 'string' },
            { name: 'enter_price', type: 'string' },
            { name: 'std_price', type: 'string' },
            { name: 'discount_rate', type: 'string' },
            { name: 'retail_price', type: 'string' },
            { name: 'remark', type: 'string' },
            { name: 'enter_qty', type: 'string' },
            { name: 'order_qty', type: 'string' },
            { name: 'enter_amount', type: 'string' },
            { name: 'retail_amount', type: 'string' },
            { name: 'prop_1_detail_name', type: 'string' },
            { name: 'prop_2_detail_name', type: 'string' },
            { name: 'prop_3_detail_name', type: 'string' },
            { name: 'prop_4_detail_name', type: 'string' },
            { name: 'prop_5_detail_name', type: 'string' },
            { name: 'display_name', type: 'string' }
            ]
    });

}