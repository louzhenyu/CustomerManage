function InitVE() {
    Ext.define("CcOrderDetailItemViewEntity", {
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
            name: 'order_no',
            type: 'string'
        },
        {
            name: 'warehouse_id',
            type: 'string'
        },
        {
            name: 'sku_id',
            type: 'string'
        },
        {
            name: 'end_qty',
            type: 'string'
        },
        {
            name: 'order_qty',
            type: 'string'
        },
        {
            name: 'remark',
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
            name: 'create_user_name',
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
            name: 'modify_user_name',
            type: 'string'
        },
        {
            name: 'if_flag',
            type: 'string'
        },
        {
            name: 'difference_qty',
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