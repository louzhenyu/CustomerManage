function InitVE() {
    Ext.define("SalesOrderDetailItemViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'order_detail_id',
            type: 'string'
        },
        {
            name: 'order_id',
            type: 'string'
        },
        {
            name: 'ref_order_detail_id',
            type: 'string'
        },
        {
            name: 'sku_id',
            type: 'string'
        },
        {
            name: 'unit_id',
            type: 'string'
        },
        {
            name: 'enter_qty',
            type: 'string'
        },
        {
            name: 'order_qty',
            type: 'string'
        },
        {
            name: 'enter_price',
            type: 'string'
        },
        {
            name: 'std_price',
            type: 'string'
        },
        {
            name: 'order_discount_rate',
            type: 'string'
        },
        {
            name: 'discount_rate',
            type: 'string'
        },
        {
            name: 'retail_price',
            type: 'string'
        },
        {
            name: 'retail_amount',
            type: 'string'
        },
        {
            name: 'enter_amount',
            type: 'string'
        },
        {
            name: 'receive_points',
            type: 'string'
        },
        {
            name: 'pay_points',
            type: 'string'
        },
        {
            name: 'remark',
            type: 'string'
        },
        {
            name: 'order_detail_status',
            type: 'string'
        },
        {
            name: 'display_index',
            type: 'string'
        },
        {
            name: 'create_time',
            type: 'string'
        },
        {
            name: 'create_user_id',
            type: 'string'
        },
        {
            name: 'modify_time',
            type: 'string'
        },
        {
            name: 'modify_user_id',
            type: 'string'
        },
        {
            name: 'if_flag',
            type: 'string'
        },
        {
            name: 'pos_order_code',
            type: 'string'
        },
        {
            name: 'order_status',
            type: 'string'
        },
        {
            name: 'prop_1_detail_name',
            type: 'string'
        },
        {
            name: 'prop_2_detail_name',
            type: 'string'
        },
        {
            name: 'prop_3_detail_name',
            type: 'string'
        },
        {
            name: 'prop_4_detail_name',
            type: 'string'
        },
        {
            name: 'prop_5_detail_name',
            type: 'string'
        },
        {
            name: 'display_name',
            type: 'string'
        },
        {
            name: 'item_code',
            type: 'string'
        },
        {
            name: 'item_name',
            type: 'string'
        }]
    });
}