function InitVE() {
    Ext.define("InoutOrderEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'customer_id', type: 'string' },
            { name: 'order_id', type: 'string' },
            { name: 'order_no', type: 'string' },
            { name: 'order_type_id', type: 'string' },
            { name: 'order_reason_id', type: 'string' },
            { name: 'red_flag', type: 'string' },
            { name: 'ref_order_id', type: 'string' },
            { name: 'ref_order_no', type: 'string' },
            { name: 'order_date', type: 'string' },
            { name: 'request_date', type: 'string' },
            { name: 'complete_date', type: 'string' },
            { name: 'create_unit_id', type: 'string' },
            { name: 'unit_id', type: 'string' },
            { name: 'related_unit_id', type: 'string' },
            { name: 'ref_unit_id', type: 'string' },
            { name: 'total_amount', type: 'string' },
            { name: 'discount_rate', type: 'string' },
            { name: 'actual_amount', type: 'string' },
            { name: 'receive_points', type: 'string' },
            { name: 'pay_points', type: 'string' },
            { name: 'pay_id', type: 'string' },
            { name: 'payment_name', type: 'string' },
            { name: 'print_times', type: 'string' },
            { name: 'carrier_id', type: 'string' },
            { name: 'carrier_name', type: 'string' },
            { name: 'remark', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'status_desc', type: 'string' },
            { name: 'create_time', type: 'string' },
            { name: 'create_user_id', type: 'string' },
            { name: 'approve_time', type: 'string' },
            { name: 'approve_user_id', type: 'string' },
            { name: 'send_user_id', type: 'string' },
            { name: 'send_time', type: 'string' },
            { name: 'accpect_user_id', type: 'string' },
            { name: 'accpect_time', type: 'string' },
            { name: 'modify_user_id', type: 'string' },
            { name: 'modify_time', type: 'string' },
            { name: 'total_qty', type: 'string' },
            { name: 'total_retail', type: 'string' },
            { name: 'sales_unit_id', type: 'string' },
            { name: 'purchase_unit_id', type: 'string' },
            { name: 'if_flag', type: 'string' },
            { name: 'sales_unit_name', type: 'string' },
            { name: 'purchase_unit_name', type: 'string' }
            , { name: 'purchase_warehouse_id', type: 'string' }
            , { name: 'warehouse_id', type: 'string' }
            , { name: 'warehouse_name', type: 'string' }
            , { name: 'pos_id', type: 'string' }
            , { name: 'shift_id', type: 'string' }
            , { name: 'sales_user', type: 'string' }
            , { name: 'keep_the_change', type: 'string' }
            , { name: 'wiping_zero', type: 'string' }
            , { name: 'vip_no', type: 'string' }
            , { name: 'vip_name', type: 'string' }
            , { name: 'purchase_warehouse_name', type: 'string' }
            , { name: 'sales_warehouse_name', type: 'string' }
            , { name: 'order_reason_name', type: 'string' }
            ,{ name: 'Field1', type: 'string' }
            ,{ name: 'Field2', type: 'string' }
            ,{ name: 'Field3', type: 'string' }
            ,{ name: 'Field4', type: 'string' }
            ,{ name: 'Field5', type: 'string' }
            ,{ name: 'Field6', type: 'string' }
            ,{ name: 'Field7', type: 'string' }
            ,{ name: 'Field8', type: 'string' }
            ,{ name: 'Field9', type: 'string' }
            ,{ name: 'Field10', type: 'string' }
            ,{ name: 'Field12', type: 'string' }
            ,{ name: 'Field13', type: 'string' }
            ,{ name: 'Field14', type: 'string' }
            ,{ name: 'Field15', type: 'string' }
            ,{ name: 'Field16', type: 'string' }
            ,{ name: 'Field17', type: 'string' }
            ,{ name: 'Field18', type: 'string' }
            ,{ name: 'Field19', type: 'string' }
            ,{ name: 'Field20', type: 'string' }
            ,{ name: 'DefrayTypeName', type: 'string' }
            ,{ name: 'DeliveryName', type: 'string' }
            , { name: 'data_from_name', type: 'string' }
            , { name: 'linkMan', type: 'string' }
            , { name: 'linkTel', type: 'string' }
            , { name: 'address', type: 'string' }
            ,{ name: 'StatusCount1', type: 'string' }
            ,{ name: 'StatusCount2', type: 'string' }
            ,{ name: 'StatusCount3', type: 'string' }
            , { name: 'StatusCount0', type: 'string' }
                     , { name: 'IntegralBack', type: 'decimal' }
            , { name: 'AmountBack', type: 'decimal' }
            ]
    });

}