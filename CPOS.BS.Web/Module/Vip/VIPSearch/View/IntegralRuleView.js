var tagsData = [], tagsStr = "";
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
            xtype: "jitbizsysintegralsource",
            fieldLabel: "积分变动行为",
            id: "txtVipSource",
            name: "IntegralSourceID",
            jitSize: 'small'
        }
//        ,{
//            xtype: "jittextfield",
//            fieldLabel: "积分公式",
//            id: "txtIntegral",
//            name: "Integral",
//            jitSize: 'small'
//        }
        ,{
            xtype: 'panel',
            id: 'txtTime',
            colspan: 2,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "有效日期",
                    id: "txtBeginDate",
                    name: "BeginDate",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtEndDate",
                    name: "EndDate",
                    jitSize: 'small',
                    width: 100
                }
            ]
         }

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
        store: Ext.getStore("IntegralRuleStore"),
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
            store: Ext.getStore("IntegralRuleStore"),
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
//            width: 50,
//            sortable: true,
//            dataIndex: 'IntegralRuleID',
//            align: 'left',
//            //hidden: __getHidden("delete"),
//            renderer: function (value, p, record) {
//                var str = "";
//                var d = record.data;
//                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
//                //if (d.order_status == "1") {
//                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
//                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
//                //}
//                return str;
//            }
//        }
//        ,
        {
            text: '积分变动行为',
            width: 150,
            sortable: true,
            dataIndex: 'IntegralSourceName',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.IntegralRuleID + "')\">" + value + "</a>";
                return value;
            }
        }
//        ,{
//            text: '积分公式',
//            width: 210,
//            sortable: true,
//            dataIndex: 'Integral',
//            align: 'left'
//            ,renderer: function (value, p, record) {
//                var str = "";
//                var d = record.data;
//                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.IntegralRuleID + "')\">" + value + "</a>";
//                return str;
//            }
//        }
        , {
            text: '积分说明',
            width: 130,
            sortable: true,
            dataIndex: 'IntegralDesc',
            flex: true,
            align: 'left'
        }
        , {
            text: '类型',
            width: 50,
            sortable: true,
            dataIndex: 'TypeCodeDesc',
            flex: true,
            align: 'left'
        }
        , {
            text: '有效起始日期',
            width: 130,
            sortable: true,
            dataIndex: 'BeginDate',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str.substring(0, 10);
            }
        }
        ,{
            text: '有效截止日期',
            width: 130,
            sortable: true,
            dataIndex: 'EndDate',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str.substring(0, 10);
            }
        }, {
            text: '创建时间',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        
        ]
    });

    
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

}