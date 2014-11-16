/*Jermyn 2013-04-02 */
function InitVE() {
    Ext.define("StockBalanceEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'stock_balance_id', type: 'string' }
            , { name: 'unit_id', type: 'string' }
            , { name: 'warehouse_id', type: 'string' }
            , { name: 'sku_id', type: 'string' }
            , { name: 'begin_qty', type: 'string' }
            , { name: 'in_qty', type: 'string' }
            , { name: 'out_qty', type: 'string' }
            , { name: 'adjust_in_qty', type: 'string' }
            , { name: 'adjust_out_qty', type: 'string' }
            , { name: 'reserver_qty', type: 'string' }
            , { name: 'on_way_qty', type: 'string' }
            , { name: 'end_qty', type: 'string' }
            , { name: 'price', type: 'string' }
            , { name: 'item_label_type_id', type: 'string' }
            , { name: 'status', type: 'string' }
            , { name: 'create_time', type: 'string' }
            , { name: 'create_user_id', type: 'string' }
            , { name: 'modify_time', type: 'string' }
            , { name: 'modify_user_id', type: 'string' }
            , { name: 'item_label_type_code', type: 'string' }
            , { name: 'item_label_type_name', type: 'string' }
            , { name: 'item_code', type: 'string' }
            , { name: 'item_name', type: 'string' }
            , { name: 'unit_name', type: 'string' }
            , { name: 'warehouse_name', type: 'string' }
            , { name: 'prop_1_detail_name', type: 'string' }
            , { name: 'prop_2_detail_name', type: 'string' }
            , { name: 'prop_3_detail_name', type: 'string' }
            , { name: 'prop_4_detail_name', type: 'string' }
            , { name: 'prop_5_detail_name', type: 'string' }
            , { name: 'row_no', type: 'string' }
            , { name: 'icount', type: 'string' }
           
            ]
    });

}