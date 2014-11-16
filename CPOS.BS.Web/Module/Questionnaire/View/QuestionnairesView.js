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
        //{
        //    xtype: "jitbizcity",
        //    fieldLabel: "城市",
        //    id: "txtCityId",
        //    name: "CityId",
        //    dataType: "City",
        //    jitSize: 'small'
        //},
        {
            xtype: "jittextfield",
            fieldLabel: "问券名称",
            id: "txtQuestionnaireName",
            name: "QuestionnaireName",
            jitSize: 'small'
        }
        //{
        //    xtype: 'panel',
        //    colspan: 2,
        //    layout: 'hbox',
        //    border: 0,
        //    bodyBorder: false,
        //    bodyStyle: 'background:#F1F2F5;',
        //    width: 400,
        //    id: 'txtPublishDate',
        //    items: [{
        //        xtype: "jitdatefield",
        //        fieldLabel: "时间",
        //        id: "txtPublishDateBegin",
        //        name: "DateBegin",
        //        jitSize: 'small'
        //    },
        //    {
        //        xtype: "label",
        //        text: "至"
        //    },
        //    {
        //        xtype: "jitdatefield",
        //        fieldLabel: "",
        //        id: "txtPublishDateEnd",
        //        name: "DateEnd",
        //        jitSize: 'small',
        //        width: 100
        //    }]
        //}
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
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("QuestionnairesStore"),
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
            store: Ext.getStore("QuestionnairesStore"),
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
            dataIndex: 'QuestionnaireID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '问券序号',
            width: 100,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        },
        {
            text: '问券名称',
            width: 240,
            sortable: true,
            dataIndex: 'QuestionnaireName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.QuestionnaireID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '问券描述',
            width: 240,
            sortable: true,
            dataIndex: 'QuestionnaireDesc',
            align: 'left'
        },
        {
            text: '问题数量',
            width: 240,
            sortable: true,
            dataIndex: 'QuestionCount',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnQuestionListView('" + d.QuestionnaireID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
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
        ]
    });
}