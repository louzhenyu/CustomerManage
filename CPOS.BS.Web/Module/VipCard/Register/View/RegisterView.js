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
        border: 0,
        items: [{
            xtype: "jittextfield",
            fieldLabel: "会员姓名",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "联系电话",
            id: "txtPhone",
            name: "Phone",
            jitSize: 'small'
        }]
    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        border: 0,
        height: 42,
        items: [{
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch,
            jitIsHighlight: true,
            jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "清空",
            margin: '0 0 10 14',
            handler: fnReset,
            jitIsHighlight: false,
            jitIsDefaultCSS: true
        }]
    });

    Ext.create('Jit.button.Button', {
        text: "新增会员卡",
        renderTo: "btnAddVipCard",
        handler: fnAddVipCard
        , jitIsHighlight: false
        , jitIsDefaultCSS: true
    });

    Ext.create('Jit.button.Button', {
        text: "新增车信息",
        renderTo: "btnAddVipExpand",
        handler: fnAddVipExpand
        , jitIsHighlight: false
        , jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 157,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("VipCardStore"),
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
//        {
//            text: '操作',
//            width: 80,
//            sortable: true,
//            dataIndex: 'VipCardID',
//            align: 'left',
//            renderer: function (value, p, record) {
//                var str = "";
//                var d = record.data;
//                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnVipCardView('" + d.VipCardID + "')\">编辑&nbsp;&nbsp;</a>";
//                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnVipCardDelete('" + d.VipCardID + "')\">删除</a>";
//                return str;
//            }
//        },
        {
            text: '会员卡号',
            width: 120,
            sortable: true,
            dataIndex: 'VipCardCode',
            align: 'left'
        },
        {
            text: '会籍店',
            width: 150,
            sortable: true,
            dataIndex: 'UnitName',
            align: 'left'
        },
        {
            text: '卡等级',
            width: 120,
            sortable: true,
            dataIndex: 'VipCardGradeName',
            align: 'left'
        },
        {
            text: '卡状态',
            width: 120,
            sortable: true,
            dataIndex: 'VipStatusName',
            align: 'left'
        },
        {
            text: '卡类型',
            width: 120,
            sortable: true,
            dataIndex: 'VipCardTypeName',
            align: 'left'
        },
        {
            text: '开卡时间',
            width: 120,
            sortable: true,
            dataIndex: 'BeginDate',
            align: 'left'
        },
        {
            text: '有效期',
            width: 120,
            sortable: true,
            dataIndex: 'EndDate',
            align: 'left'
        },
        {
            text: '账户余额',
            width: 120,
            sortable: true,
            dataIndex: 'BalanceAmount',
            align: 'left'
        }
        ]
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipExpandStore"),
        id: "gridView2",
        renderTo: "DivGridView2",
        columnLines: true,
        height: 187,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
            defaultType: 'button',
            store: Ext.getStore("VipExpandStore"),
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
            width: 80,
            sortable: true,
            dataIndex: 'VipExpandID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnVipExpandView('" + d.VipExpandID + "')\">编辑&nbsp;&nbsp;</a>";
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnVipExpandDelete('" + d.VipExpandID + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '车牌号',
            width: 120,
            sortable: true,
            dataIndex: 'LicensePlateNo',
            align: 'left'
        },
        {
            text: '车品牌',
            width: 120,
            sortable: true,
            dataIndex: 'CarBrandName',
            align: 'left'
        },
        {
            text: '车型',
            width: 150,
            sortable: true,
            dataIndex: 'CarModelsName',
            align: 'left'
        },
        {
            text: '车架号',
            width: 150,
            sortable: true,
            dataIndex: 'ChassisNumber',
            align: 'left'
        },
        {
            text: '车厢形式',
            width: 120,
            sortable: true,
            dataIndex: 'CompartmentsForm',
            align: 'left'
        },
        {
            text: '购买时间',
            width: 120,
            sortable: true,
            dataIndex: 'PurchaseTime',
            align: 'left'
        },
        {
            text: '备注',
            width: 180,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }
        ]
    });
}