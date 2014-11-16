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
            fieldLabel: "标签名称",
            id: "txtTagsName",
            name: "TagsName",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbiztagstype",
            fieldLabel: "标签分类",
            id: "txtTagsType",
            name: "TypeId",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbiztagsstatus",
            fieldLabel: "标签状态",
            id: "txtTagsStatus",
            name: "StatusId",
            jitSize: 'small'
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
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("tagsStore"),
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
            store: Ext.getStore("tagsStore"),
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
            width: 60,
            sortable: true,
            dataIndex: 'TagsId',
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
            text: '标签名称',
            width: 200,
            sortable: true,
            dataIndex: 'TagsName',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.TagsId + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '标签描述',
            width: 200,
            sortable: true,
            dataIndex: 'TagsDesc',
            align: 'left'
        }
        ,{
            text: '标签分类',
            width: 100,
            sortable: true,
            dataIndex: 'TypeName',
            align: 'left'
        }
        ,{
            text: '标签状态',
            width: 100,
            sortable: true,
            dataIndex: 'StatusName',
            align: 'left' 
        }
        ,{
            text: '会员数量',
            width: 100,
            sortable: true,
            dataIndex: 'VipCount',
            align: 'right'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnVipView('" + d.TagsId + "','" + d.TagsName + "')\">" + value + "</a>";
                return str;
            }
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