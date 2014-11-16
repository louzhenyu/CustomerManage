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
            fieldLabel: "标题",
            id: "txtTitle",
            name: "Title",
            jitSize: 'small'
        },
        {
            xtype: "jitbizzforumtype",
            fieldLabel: "类别",
            id: "txtForumType",
            name: "ForumTypeId",
            jitSize: 'small'
        },
        {
            xtype: "jitbizzcourse",
            fieldLabel: "课程",
            id: "txtCourse",
            name: "CourseId",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "城市",
            id: "txtCity",
            name: "City",
            jitSize: 'small'
        },
        {
            xtype: "jitdatefield",
            fieldLabel: "开始日期",
            id: "txtBeginTime",
            name: "BeginTime",
            jitSize: 'small',
            hidden: true
        }
        ]
    });
    
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
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
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
        ,{
            xtype: "jitbutton",
            text: "重置",
            margin: '0 0 10 14',
            handler: fnReset
        }
        ]
    });


    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 367,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("eventsStore"),
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
            text: '序号',
            width: 80,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        },
        {
            text: '操作',
            width: 80,
            sortable: true,
            dataIndex: 'ForumId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '标题',
            width: 250,
            sortable: true,
            dataIndex: 'Title',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.ForumId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '城市',
            width: 100,
            sortable: true,
            dataIndex: 'City',
            align: 'left'
        },
        {
            text: '开始日期',
            width: 130,
            sortable: true,
            dataIndex: 'BeginTime',
            //format: 'Y-m-d',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        },
        {
            text: '课程',
            width: 250,
            sortable: true,
            dataIndex: 'CourseName',
            align: 'left'
        }
        ]
    });
}