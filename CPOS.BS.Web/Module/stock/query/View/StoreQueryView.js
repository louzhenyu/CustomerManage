/*Jermyn 2013-04-02*/
function InitView() {
    //    debugger;
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 3
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "门店",
            id: "txtUnit",
            name: "unit_id",
            jitSize: 'small'
        },{
            xtype: "jitbizwarehouse",
            fieldLabel: "仓库",
            id: "txtWarehouse",
            name: "warehouse_id",
            jitSize: 'small'
            , parent_id: "txtUnit"
        },{
            xtype: 'jitbizskuselect'
            , fieldLabel: "商品"
            , width: 380
            ,nameId: 'txtItemName'
            , jitSize: 'small'
           
        },
        {
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch
        }
//        ,
//        {
//            xtype: "jitbutton",
//            text: "重置",
//            margin: '0 0 10 14',
//            handler: fnReset
//        }
//        ,{
//            xtype: "jitbutton",
//            text: "导出",
//            margin: '0 0 10 14',
//            handler: fnDownload
//        }
        ]

    });

    var z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("storeQueryStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("storeQueryStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '门店',
            width: 110,
            sortable: true,
            dataIndex: 'unit_name',
            align: 'left'
        }, {
            text: '仓库',
            width: 110,
            sortable: true,
            dataIndex: 'warehouse_name',
            align: 'left'
        }, 
        {
            text: '商品代码',
            width: 110,
            sortable: true,
            dataIndex: 'item_code',
            align: 'left'
        }, 
        {
            text: '商品名称',
            width: 220,
            sortable: true,
            dataIndex: 'item_name',
            align: 'left'
        },
        { id: "g_sp_1", dataIndex: 'prop_1_detail_name', width: 110, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_1_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_1 == "1" ? false : true
        },
        { id: "g_sp_2", dataIndex: 'prop_2_detail_name', width: 110, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_2_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_2 == "1" ? false : true
        },
        { id: "g_sp_3", dataIndex: 'prop_3_detail_name', width: 110, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_3_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_3 == "1" ? false : true
        },
        { id: "g_sp_4", dataIndex: 'prop_4_detail_name', width: 110, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_4_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_4 == "1" ? false : true
        },
        { id: "g_sp_5", dataIndex: 'prop_5_detail_name', width: 110, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_5_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_5 == "1" ? false : true
        }, 
        {
            text: '期初数',
            width: 50,
            sortable: true,
            dataIndex: 'begin_qty',
            align: 'left'
        }, 
        {
            text: '入库数',
            width: 50,
            sortable: true,
            dataIndex: 'in_qty',
            align: 'left'
        }, 
        {
            text: '出库数',
            width: 50,
            sortable: true,
            dataIndex: 'out_qty',
            align: 'left'
        },
        {
            text: '调整入数',
            width: 50,
            sortable: true,
            dataIndex: 'adjust_in_qty',
            align: 'left'
        },
        {
            text: '调整出数',
            width: 50,
            sortable: true,
            dataIndex: 'adjust_out_qty',
            align: 'left'
        },
        {
            text: '期末数',
            width: 50,
            sortable: true,
            dataIndex: 'end_qty',
            align: 'left'
        }
        ]
    });
}