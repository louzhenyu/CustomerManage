function InitVE() {
    Ext.define("WarehouseViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "warehouse_id", type: 'string' }, 
            { name: "wh_code", type: 'string' }, 
            { name: "wh_name", type: 'string' }, 
            { name: "wh_name_en", type: 'string' }, 
            { name: "wh_address", type: 'string' }, 
            { name: "wh_contacter", type: 'string' }, 
            { name: "wh_tel", type: 'string' }, 
            { name: "wh_fax", type: 'string' }, 
            { name: "wh_status", type: 'string' }, 
            { name: "wh_remark", type: 'string' }, 
            { name: "create_user_id", type: 'string' }, 
            { name: "create_user_name", type: 'string' }, 
            { name: "create_time", type: 'string' }, 
            { name: "modify_user_id", type: 'string' }, 
            { name: "modify_user_name", type: 'string' }, 
            { name: "modify_time", type: 'string' }, 
            { name: "sys_modify_stamp", type: 'string' }, 
            { name: "is_default", type: 'string' }, 
            { name: "wh_status_desc", type: 'string' }, 
            { name: "is_default_desc", type: 'string' }, 
            { name: "unit_id", type: 'string' }, 
            { name: "unit_code", type: 'string' }, 
            { name: "unit_name", type: 'string' }, 
            { name: "unit_name_short", type: 'string' }
            ]
    });

}