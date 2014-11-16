function InitVE() {
    Ext.define("CcOrderViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'order_id',
            type: 'string'
        },
        {
            name: 'order_no',
            type: 'string'
        },
        {
            name: 'order_type_id',
            type: 'string'
        },
        {
            name: 'order_reason_id',
            type: 'string'
        },
        {
            name: 'ref_order_id',
            type: 'string'
        },
        {
            name: 'ref_order_no',
            type: 'string'
        },
        {
            name: 'order_date',
            type: 'string'
        },
        {
            name: 'request_date',
            type: 'string'
        },
        {
            name: 'complete_date',
            type: 'string'
        },
        {
            name: 'unit_id',
            type: 'string'
        },
        {
            name: 'pos_id',
            type: 'string'
        },
        {
            name: 'warehouse_id',
            type: 'string'
        },
        {
            name: 'remark',
            type: 'string'
        },
        {
            name: 'status',
            type: 'string'
        },
        {
            name: 'status_desc',
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
            name: 'data_from_id',
            type: 'string'
        },
        {
            name: 'send_user_id',
            type: 'string'
        },
        {
            name: 'send_time',
            type: 'string'
        },
        {
            name: 'approve_user_id',
            type: 'string'
        },
        {
            name: 'approve_time',
            type: 'string'
        },
        {
            name: 'accept_user_id',
            type: 'string'
        },
        {
            name: 'accept_time',
            type: 'string'
        },
        {
            name: 'if_flag',
            type: 'string'
        },
        {
            name: 'unit_name',
            type: 'string'
        },
        {
            name: 'warehouse_name',
            type: 'string'
        },
        {
            name: 'total_qty',
            type: 'string'
        }]
    });
}