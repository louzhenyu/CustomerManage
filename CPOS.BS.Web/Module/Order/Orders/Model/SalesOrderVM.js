﻿function InitVE() {
    Ext.define("SalesOrderViewEntity", {
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
            name: 'order_reason_type_id',
            type: 'string'
        },
        {
            name: 'red_flag',
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
            name: 'create_unit_id',
            type: 'string'
        },
        {
            name: 'unit_id',
            type: 'string'
        },
        {
            name: 'related_unit_id',
            type: 'string'
        },
        {
            name: 'ref_unit_id',
            type: 'string'
        },
        {
            name: 'total_amount',
            type: 'string'
        },
        {
            name: 'discount_rate',
            type: 'string'
        },
        {
            name: 'actual_amount',
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
            name: 'pay_id',
            type: 'string'
        },
        {
            name: 'print_times',
            type: 'string'
        },
        {
            name: 'carrier_id',
            type: 'string'
        },
        {
            name: 'remark',
            type: 'string'
        },
        {
            name: 'order_status',
            type: 'string'
        },
        {
            name: 'order_status_desc',
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
            name: 'approve_time',
            type: 'string'
        },
        {
            name: 'approve_user_id',
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
            name: 'accpect_user_id',
            type: 'string'
        },
        {
            name: 'accpect_time',
            type: 'string'
        },
        {
            name: 'modify_user_id',
            type: 'string'
        },
        {
            name: 'modify_time',
            type: 'string'
        },
        {
            name: 'total_qty',
            type: 'string'
        },
        {
            name: 'total_retail',
            type: 'string'
        },
        {
            name: 'sales_unit_id',
            type: 'string'
        },
        {
            name: 'purchase_unit_id',
            type: 'string'
        },
        {
            name: 'if_flag',
            type: 'string'
        },
        {
            name: 'sales_unit_name',
            type: 'string'
        },
        {
            name: 'purchase_unit_name',
            type: 'string'
        }]
    });
}