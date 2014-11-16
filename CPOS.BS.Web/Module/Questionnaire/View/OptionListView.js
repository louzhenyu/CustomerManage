function InitView() {

    
    
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("OptionListStore"),
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
            store: Ext.getStore("OptionListStore"),
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
            dataIndex: 'OptionID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '排序',
            width: 100,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        },
        {
            text: '文字描述',
            width: 140,
            sortable: true,
            dataIndex: 'OptionsText',
            align: 'left'
        },
        {
            text: '是否选中',
            width: 100,
            sortable: true,
            dataIndex: 'IsSelect',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                if (value == "1") str = "是";
                else if (value == "0") str = "否";
                return str;
            }
        },
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