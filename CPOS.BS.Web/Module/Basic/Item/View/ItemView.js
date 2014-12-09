function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: "column",
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "商品代码",
            id: "txtItemCode",
            name: "item_code",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "商品名称",
            id: "txtItemName",
            name: "item_name",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizitemcategoryselecttree",
            fieldLabel: "商品分类",
            id: "txtItemCategory",
            name: "item_category_id",
           
            jitSize: 'small'
                }
// , {
//     xtype: 'jitcombotree',
//     id: 'txtItemCategory',
//     fieldLabel: '商品分类',  //取的是一个树数据
//     emptyText: '--请选择--',
//     multiSelect: false,
//     isAddPleaseSelectItem: true,
//     pleaseSelectText: '--请选择--',
//     isSelectLeafOnly: true, //设置只能选择叶子结点。
//     pickCfg: {
//         minHeight: 100,
//         maxHeight: 120,
//         width: 500
//     }, url: '../ItemCategoryNew/Handler/ItemCategoryTreeHandler.ashx?Status=1'  //获取数据
// }
        , {
            xtype: "jitbizstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "item_status",
            jitSize: 'small'
            , defaultValue: '1'
        }
        , {
            xtype: "jitbizyesnostatus",
            fieldLabel: "积分商品",
            id: "txtCanRedeem",
            name: "item_can_redeem",
            dataType: "yn",
            jitSize: 'small'
            , hidden: true
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }
        , {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
        }
        ]
    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
    });

    Ext.create('Jit.button.Button', {
        text: "启用",
        renderTo: "span_enable",
        handler: function () { fnEnable(1) }
    });

    Ext.create('Jit.button.Button', {
        text: "停用",
        renderTo: "span_disable",
        handler: function () { fnEnable(-1) }
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("itemStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        viewConfig: {
            enableTextSelection: true
        }
        , selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        viewConfig: {
            getRowClass: fnRowRenderer
        },
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("itemStore"),
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
        //            {
        //            text: '操作',
        //            width: 110,
        //            sortable: true,
        //            dataIndex: 'Item_Id',
        //            align: 'left',
        //            //hidden: __getHidden("delete"),
        //            renderer: function (value, p, record) {
        //                var str = "";
        //                var d = record.data;
        //                if (d.Status != "-1") {
        //                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '-1')\">停用</a>";
        //                } else {
        //                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">启用</a>";
        //                }
        //                //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
        ////                if (d.order_status == "1") {
        ////                    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        ////                    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        ////                }
        //                return str;
        //            }
        //        },

        {
        text: '商品类型',
        width: 110,
        sortable: true,
        dataIndex: 'Item_Category_Name',
        align: 'left'
    }
        , {
            text: '商品代码',
            width: 110,
            sortable: true,
            dataIndex: 'Item_Code',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Item_Id + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '商品名称',
            width: 190,
            sortable: true,
            dataIndex: 'Item_Name',
            align: 'left',
            renderer: function (value, p, record) {
                var jdata = record.data;
                //                if (jdata.IsPause == "true" || record.data.IsPause == "") {
                //                     return "<span style=color:red>" + value + "</span>";
                //                   // metaData.style = "background-color: red";
                //                }
                //                if (jdata.IsItemCategory == "true") {
                //                    return "<span style=color:darkviolet>" + value + "</span>";
                //                }
                return value;
            }
        }
        , {
            text: '商品简称',
            width: 110,
            sortable: true,
            dataIndex: 'Item_Name_Short',
            align: 'left'
        }
        , {
            text: '商品助记码',
            width: 110,
            sortable: true,
            dataIndex: 'Pyzjm',
            align: 'left'
        }
        , {
            text: '状态',
            width: 60,
            sortable: true,
            dataIndex: 'Status',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "-1") return "停用";
                else if (value == "1") return "正常";
                return "";
            }
        },
        {
            text: '是否过期',
            width: 60,
            sortable: true,
            dataIndex: 'IsPause',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "false") return "否";
                else if (value == "true") return "是";
                return "否";
            }

        },
        {
            text: '商品标识',
            width: 250,
            sortable: true,
            dataIndex: 'Item_Id',
            align: 'left'
        }
        ]
});
}