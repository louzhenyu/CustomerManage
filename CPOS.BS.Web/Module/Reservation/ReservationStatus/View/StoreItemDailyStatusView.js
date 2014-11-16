function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 5
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [{
            labelWidth: "65px",
            width: 160,
            xtype: "jitcombobox",
            fieldLabel: "门店",
            id: "cmbStoreID",
            name: "cmbStoreID",
            emptyText: "--请选择--",
            valueField: 'unit_id',
            displayField: 'unit_name',
            store: Ext.getStore("unitStore"),
            listeners: {
                change: function () {
                    fnLoadTypeCmb();
                }
            }
        }, {
            labelWidth: "65px",
            width: 160,
            xtype: "jitcombobox",
            fieldLabel: "房型",
            id: "cmbHouseType",
            name: "cmbHouseType",
            emptyText: "--请选择--",
            valueField: 'sku_id',
            displayField: 'item_name',
            store: Ext.getStore("houseTypeStore")
        }, {
            labelWidth: "65px",
            width: 160,
            xtype: "jitdatefield",
            id: "txtStartTime",
            fieldLabel: '开始日期',
            jitSize: 'small',
            format: 'Y-m-d',
            value: new Date()
        }, {
            labelWidth: "65px",
            width: 165,
            xtype: "jitdatefield",
            id: "txtEndTime",
            fieldLabel: '结束日期',
            jitSize: 'small',
            format: 'Y-m-d',
            vtype: "enddate",
            begindateField: "txtStartTime",
            value: Ext.Date.add(new Date(), Ext.Date.MONTH, +1)
        }, {
            labelWidth: "65px",
            width: 165,
            xtype: "jitcombobox",
            fieldLabel: "星期",
            id: 'cmbWeek',
            name: "cmbWeek",
            multiSelect: true,
            emptyText: "--请选择--",
            valueField: 'value',
            displayField: 'name',
            store: Ext.getStore("weekStore")
        }]
    });

    // btn_panel
    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 5
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        //width: 200,
        height: 42,
        items: [{
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        }]

    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel_batchupdate',
        renderTo: 'span_BatchUpdate',
        bodyStyle: 'background:#F1F2F5;',
        layout: {
            type: 'table',
            columns: 4
        },
        border: 0,
        items: [{
            labelWidth: "80px",
            width: 180,
            xtype: "jitradiofield"
            , id: "radio1"
            , name: 'HouseStates'
            , boxLabel: '满房'
            , width: 60
            , listeners: {
                'focus': {
                    fn: function () {
                        fnBatchAmount(0, 100);
                    }
                }
            }
        }, {
            xtype: "jitradiofield"
            , id: "radio2"
            , name: 'HouseStates'
            , boxLabel: '非满房'
            , width: 60
            , listeners: {
                'focus': {
                    fn: function () {
                        fnBatchAmount(100, 100);
                    }
                }
            }
        }, {
            xtype: "jittextfield",
            fieldLabel: "可用库存",
            id: "txtStockAmount",
            name: "StockAmount",
            jitSize: 'small',
            value: 100,
            style:"display:none"
        }, {
            xtype: "jitbutton",
            text: "批量修改",
            handler: fnBatchUpdate
        }]
    });
}