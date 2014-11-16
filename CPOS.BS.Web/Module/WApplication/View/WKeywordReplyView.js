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
        items: [
        {
            xtype: "jitbizwapplicationinterface",
            fieldLabel: "微信账号",
            id: "txtApplicationId",
            name: "ApplicationId",
            jitSize: 'small'
            ,listeners: {
                select: function (store) {
                    Ext.getCmp("txtModelId").setDefaultValue("");
                }
            }
        }
        ,{
            xtype: "jitbizwmodel",
            fieldLabel: "模板",
            id: "txtModelId",
            name: "ModelId",
            jitSize: 'small',
            c: true,
            parent_id: "txtApplicationId"
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "关键字",
            id: "txtKeyword",
            name: "Keyword",
            jitSize: 'small'
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
        ]
    });


    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WKeywordReplyStore"),
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
            store: Ext.getStore("WKeywordReplyStore"),
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
            dataIndex: 'ReplyId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '微信账号',
            width: 150,
            sortable: true,
            dataIndex: 'WeiXinName',
            align: 'left'
        }
        ,{
            text: '关键字',
            width: 200,
            sortable: true,
            dataIndex: 'Keyword',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.ReplyId + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '模块',
            width: 100,
            sortable: true,
            dataIndex: 'ModelName',
            align: 'left'
        }
        ,{
            text: '模板类别',
            width: 100,
            sortable: true,
            dataIndex: 'MaterialTypeName',
            align: 'left'
        }
        ,{
            text: '创建时间',
            width: 130,
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
        ,{
            text: '创建人',
            width: 110,
            sortable: true,
            dataIndex: 'CreateByName',
            align: 'left'
        }
        ]
    });
}