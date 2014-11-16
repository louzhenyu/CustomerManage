function InitView() {

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
        items: [{
            xtype: "jittextfield",
            fieldLabel: "会员卡号",
            id: "txtVipCardNo",
            name: "VipCardCode",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "会员姓名",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizvipcardgrade"
            , fieldLabel: "卡等级"
            , id: "txtVipCardGradeID"
            , name: "VipCardGradeID"
            , jitSize: 'small'
            , orderType: 'VipCardGradeID'
        }
        ,{
            xtype: "jitbizvipcardstatus"
            , fieldLabel: "卡状态"
            ,id: "txtVipCardStatus"
            ,name: "VipCardStatusId"
            ,jitSize: 'small'
            ,orderType: 'VipCardStatusId'
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
        //width: 200,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }, {
            xtype: "jitbutton",
            text: "重置",
            margin: '0 0 10 14',
            handler: fnReset
                , jitIsHighlight: false
                , jitIsDefaultCSS: true
        }
        ]

    });

    //operator area
//    Ext.create('Jit.button.Button', {
//        text: "延期",
//        renderTo: "span_create",
//        handler: fnDelay
//    });

//    Ext.create('Jit.button.Button', {
//        text: "升降级",
//        renderTo: "span_create",
//        handler: fnRelegation
//    });

    Ext.create('Jit.button.Button', {
        text: "激活",
        renderTo: "span_create",
        handler: fnActivation
    });

    Ext.create('Jit.button.Button', {
        text: "冻结",
        renderTo: "span_create",
        handler: fnFreeze
    });

//    Ext.create('Jit.button.Button', {
//        text: "休眠",
//        renderTo: "span_create",
//        handler: fnDormancy
//    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipCardStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 450,
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
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'VipCardID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnModify('" + d.VipCardCode + "')\">查看";
                return str;
            }
        }
        , {
            text: '会员卡号',
            width: 120,
            sortable: true,
            dataIndex: 'VipCardCode',
            align: 'left'
        },
        {
            text: '会员名',
            width: 120,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        },
        {
            text: '会籍店',
            width: 120,
            sortable: true,
            dataIndex: 'UnitName',
            align: 'left'
        },
        {
            text: '卡等级',
            width: 100,
            sortable: true,
            dataIndex: 'VipCardGradeName',
            align: 'left'
        }, {
            text: '卡状态',
            width: 100,
            sortable: true,
            dataIndex: 'VipStatusName',
            align: 'left'
        }, {
            text: '开卡时间',
            width: 100,
            sortable: true,
            dataIndex: 'BeginDate',
            align: 'left'
        }, {
            text: '有效期',
            width: 100,
            sortable: true,
            dataIndex: 'EndDate',
            align: 'left'
        }, {
            text: '账户余额',
            width: 100,
            sortable: true,
            dataIndex: 'BalanceAmount',
            align: 'left'
        }
        ]
    });
}
