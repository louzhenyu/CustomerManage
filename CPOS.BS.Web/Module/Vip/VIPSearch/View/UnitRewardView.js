var tagsData = [], tagsStr = "";
function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: 'column',
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        width:815,
        items: [
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "门店",
            id: "txtUnitName",
            name: "UnitId",
            jitSize: 'small'
            , hidden: false
        }
        //,{
        //    xtype: "jittextfield",
        //    fieldLabel: "会员名",
        //    id: "txtVipName",
        //    name: "VipInfo",
        //    jitSize: 'small',
        //    //labelWidth: 63,
        //    hidden:false
        //}
        ,{
            xtype: "jitbizsysintegralsource",
            fieldLabel: "奖励类型",
            id: "txtSysIntegralSource",
            name: "SysIntegralSourceId",
            jitSize: 'small'
            ,typeCode: 3
            , hidden: false
            ,multiSelect: true 
            //, width: 200
        }

        ,{
            xtype: 'panel',
            id: 'txtDate',
           // colspan: 2,
            // layout: 'hbox',
            layout: 'column',
            border: 0,
            bodyBorder: false,
            width:400,
          //  bodyStyle: 'background:#F1F2F5;',
            hidden: false,
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "日期",
                    id: "txtDateBegin",
                    name: "DateBegin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtDateEnd",
                    name: "DateEnd",
                    jitSize: 'small',
                    width: 100
                }
            ]
         }
         

        ]

    });
    
    // btn_panel
    //Ext.create('Ext.form.Panel', {
    //    id: 'btn_panel',
    //    layout: {
    //        type: 'table',
    //        columns: 4
    //    },
    //    renderTo: 'btn_panel',
    //    padding: '10 0 0 0',
    //    bodyStyle: 'background:#F1F2F5;',
    //    border: 0,
    //    //width: 200,
    //    height: 42,
    //    items: [
        
    //     {
    //        xtype: "jitbutton",
    //        id: "btnMoreSearchView",
    //        text: "更多",
    //        margin: '0 0 10 14',
    //        handler: fnMoreSearchView
    //    }
    //    ]

    //});
    
    // btn_panel2
    Ext.create('Ext.form.Panel', {
        id: 'btn_panel2',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel2',
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
        }
        , {
            xtype: "jitbutton",
            id: "btnCancel",
            text: "重置",
            margin: '0 0 10 14',
            handler: fnCancel
        }
        ]

    });

    //Ext.create('Jit.button.Button', {
    //    text: "清 除",
    //    renderTo: "btnCancel"
    //    , handler: fnCancel
    //});
    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "span_create",
    //    //hidden: __getHidden("create"),
    //    handler: fnCreate
    //});

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("UnitRewardStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("UnitRewardStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'VIPID',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        
        {
            text: '门店',
            width: 210,
            sortable: true,
            dataIndex: 'MembershipShop',
            align: 'left'
        }
        ,{
            text: '门店积分',
            width: 110,
            sortable: true,
            dataIndex: 'SearchIntegral',
            align: 'right'
        }
        ,{
            text: '会员招募数',
            width: 110,
            sortable: true,
            dataIndex: 'UnitCount',
            align: 'right',
            renderer: fnRenderVipColumn
        }
        ,{
            text: '导购员人数',
            width: 110,
            sortable: true,
            dataIndex: 'VipCount',
            align: 'right'
        }
        ,{
            text: '销售金额',
            width: 110,
            sortable: true,
            dataIndex: 'UnitSalesAmount',
            align: 'right'
        }
        
        ]
    });
}