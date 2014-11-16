function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
//        layout: {
//            type: 'table',
//            columns: 4
//                },
//        renderTo: 'span_panel',
//        padding: '10 0 0 0',
//        bodyStyle: 'background:#F1F2F5;',
//        border: 0,
        items: [{
            xtype: "jitbiznewstype",
            fieldLabel: "新闻类型",
            id: "txtNewsType",
            name: "NewsType",
            dataType: "NewsType",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "新闻标题",
            id: "txtNewsTitle",
            name: "NewsTitle",
            jitSize: 'small'
        },
        {
            xtype: 'panel',
            colspan: 2,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            width: 400,
            id: 'txtPublishTime',
            items: [{
                xtype: "jitdatefield",
                fieldLabel: "发布时间",
                id: "txtPublishTimeBegin",
                name: "PublishTimeBegin",
                jitSize: 'small'
            },
            {
                xtype: "label",
                text: "至"
            },
            {
                xtype: "jitdatefield",
                fieldLabel: "",
                id: "txtPublishTimeEnd",
                name: "PublishTimeEnd",
                jitSize: 'small',
                width: 100
            }]
        },{
            width: '100%',
            items: [{
                xtype: "jitbutton",
                text: "查询",
                margin: '0 0 10 14',
                handler: fnSearch
                , jitIsHighlight: true
                , jitIsDefaultCSS: true
            }, {
                xtype: "jitbutton",
                text: "重置",
                margin: '0 0 10 14',
                handler: fnSearch
                , jitIsHighlight: false
                , jitIsDefaultCSS: true
            }],
            margin: '0 0 10 0',
            layout: 'column',
            border: 0
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });


    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        ,imgName: 'create'
        ,isImgFirst: true
        ,hidden: __getHidden("create")
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });


    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("newsStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 387,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("newsStore"),
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
            dataIndex: 'NewsId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '发布时间',
            width: 100,
            sortable: true,
            dataIndex: 'StrPublishTime',
            format: 'Y-m-d',
            align: 'left'
        },
        {
            text: '新闻标题',
            width: 200,
            sortable: true,
            dataIndex: 'NewsTitle',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.NewsId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '新闻子标题',
            width: 150,
            sortable: true,
            dataIndex: 'NewsSubTitle',
            align: 'left'
        },
        {
            text: '新闻类型',
            width: 120,
            sortable: true,
            dataIndex: 'NewsTypeName',
            align: 'left'
        },
        {
            text: '内容链接',
            width: 150,
            sortable: true,
            dataIndex: 'ContentUrl',
            align: 'left'
        }]
    });
}