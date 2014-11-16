function InitView() {

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("QuestionListStore"),
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
            store: Ext.getStore("QuestionListStore"),
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
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'QuestionID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '试题描述',
            width: 200,
            sortable: true,
            dataIndex: 'QuestionDesc',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.QuestionID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '试题类型',
            width: 140,
            sortable: true,
            dataIndex: 'QuestionTypeDesc',
            align: 'left'
        },
        {
            text: '试题答案',
            width: 140,
            sortable: true,
            dataIndex: 'QuestionValue',
            align: 'left'
        },
        {
            text: '问题选项数量',
            width: 100,
            sortable: true,
            dataIndex: 'OptionsCount',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnOptionListView('" + d.QuestionID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '最少选中项',
            width: 100,
            sortable: true,
            dataIndex: 'MinSelected',
            align: 'left'
        },
        {
            text: '最多选中项',
            width: 100,
            sortable: true,
            dataIndex: 'MaxSelected',
            align: 'left'
        },
        {
            text: '问题答案数量',
            width: 100,
            sortable: true,
            dataIndex: 'QuestionValueCount',
            align: 'left'
        },
        {
            text: '是否必填项',
            width: 100,
            sortable: true,
            dataIndex: 'IsRequired',
            align: 'left'
        },
        {
            text: '试题是否开放',
            width: 100,
            sortable: true,
            dataIndex: 'IsOpen',
            align: 'left'
        },
        {
            text: '是否公共的问题',
            width: 100,
            sortable: true,
            dataIndex: 'IsSaveOutEvent',
            align: 'left'
        },
        {
            text: 'cookie存储名字',
            width: 100,
            sortable: true,
            dataIndex: 'CookieName',
            align: 'left'
        },
        //{
        //    text: '排序',
        //    width: 100,
        //    sortable: true,
        //    dataIndex: 'DisplayIndex',
        //    align: 'left'
        //},
        {
            text: '创建时间',
            width: 140,
            sortable: true,
            dataIndex: 'CreateTime',
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
    
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [
        //{
        //    xtype: "jitbutton",
        //    id: "btnSave",
        //    text: "保存",
        //    formBind: true,
        //    disabled: true,
        //    hidden: false
        //    //handler: fnSave
        //    , jitIsHighlight: true
        //    , jitIsDefaultCSS: true
        //},
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });

}