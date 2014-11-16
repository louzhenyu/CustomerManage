function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "名称/卡号",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        }
        //,{
        //    xtype: "jittextfield",
        //    fieldLabel: "手机",
        //    id: "txtTel",
        //    name: "Phone",
        //    //hidden: true,
        //    jitSize: 'small'
        //}
        //,{
        //    xtype: "jitbizstatus",
        //    fieldLabel: "状态",
        //    id: "txtStatus",
        //    name: "vip_status",
        //    hidden: true,
        //    jitSize: 'small'
        //}
//        ,{
//            xtype: "jitbutton",
//            text: "查询",
//            //hidden: __getHidden("search"),
//            margin: '0 0 10 14',
//            handler: fnSearch
//        }
        ]

    });
    
    // btn_panel
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
        //width: 200,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        }
        //, {
        //    xtype: "jitbutton",
        //    id: "btnMoreSearchView",
        //    text: "高级查询",
        //    margin: '0 0 10 14',
        //    handler: fnMoreSearchView
        //}
        ]

    });

    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "span_create",
    //    //hidden: __getHidden("create"),
    //    handler: fnCreate
    //});

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("vipStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'VIPID',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                //if (d.order_status == "1") {
                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
                //}
                return str;
            }
        }
        ,{
            text: '卡号',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
        }
        ,{
            text: '等级',
            width: 110,
            sortable: true,
            dataIndex: 'VipLevel',
            align: 'left'
        }
        ,{
            text: '姓名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        }
        ,{
            text: '性别',
            width: 110,
            sortable: true,
            dataIndex: 'GenderInfo',
            align: 'left'
        }
        ,{
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        }
        ,{
            text: '电邮',
            width: 110,
            sortable: true,
            dataIndex: 'Email',
            align: 'left'
        }
        ,{
            text: '微信',
            width: 110,
            sortable: true,
            dataIndex: 'WeiXin',
            align: 'left'
        }
        ,{
            text: '积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
        }
        ,{
            text: '购买金额',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseAmount',
            align: 'left'
        }
        ,{
            text: '购买频次',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseCount',
            align: 'left'
        }
        ,{
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            //format: 'Y-m-d',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ]
    });
}