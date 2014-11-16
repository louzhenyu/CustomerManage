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
            xtype: "jittextfield",
            fieldLabel: "名称",
            id: "txtName",
            name: "Name",
            jitSize: 'small'
        }
        //{
        //    xtype: "jittextfield",
        //    fieldLabel: "菜单KEY值",
        //    id: "txtKey",
        //    name: "Key",
        //    jitSize: 'small'
        //},
        //{
        //    xtype: "jittextfield",
        //    fieldLabel: "类型",
        //    id: "txtType",
        //    name: "Type",
        //    jitSize: 'small'
        //}
        ]
    });
    
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel2',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'span_panel2',
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
                    fnAppSearch();
                }
            }
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
            handler: function() {
                fnSearch(get("tree_selected").value);
            }
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
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
        //{
        //    xtype: "jitbutton",
        //    text: "查询",
        //    margin: '0 0 10 14',
        //    handler: function() {
        //        fnAppSearch();
        //    }
        //    , jitIsHighlight: true
        //    , jitIsDefaultCSS: true
        //}
        {
            xtype: "jitbutton",
            text: "发布",
            margin: '0 0 10 14',
            handler: function() {
                fnAppSave();
            }
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
        store: Ext.getStore("wMenuStore"),
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
            store: Ext.getStore("wMenuStore"),
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
            width: 76,
            sortable: true,
            dataIndex: 'ID',
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
            text: '菜单名称',
            width: 200,
            sortable: true,
            dataIndex: 'Name',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.ID + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '模板名称',
            width: 110,
            sortable: true,
            dataIndex: 'ModelName',
            align: 'left'
        }
        ,{
            text: '模板类型',
            width: 110,
            sortable: true,
            dataIndex: 'MaterialTypeName',
            align: 'left'
        }
        ,{
            text: '序号',
            width: 110,
            sortable: true,
            dataIndex: 'DisplayColumn',
            align: 'left'
        }
        ,{
            text: '最后修改时间',
            width: 130,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            //format: 'Y-m-d',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        //,{
        //    text: '创建人',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'CreateByName',
        //    align: 'left'
        //}
        ]
    });

    // tree
    var store = Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true,
            children: [
                { text: "detention", leaf: true },
                { text: "homework", expanded: true, children: [
                    { text: "book report", leaf: true },
                    { text: "alegrbra", leaf: true}
                ] },
                { text: "buy lottery tickets", leaf: true }
            ]
        }
    });
    
    var pTree = new Ext.create('Ext.tree.TreePanel', {
        height: 448,
        width: '100%',
        store: Ext.getStore("WMenuParentStore"),
        id: 'span_tree1',
        renderTo: "span_tree",
        autoScroll: true,
        containerScroll: true,
        rootVisible: false,
        hideHeaders: true,
        columns: [
        {
            xtype: 'treecolumn',
            text: '',
            width: 188,
            dataIndex: 'ID',
            renderer: function (value, p, record) {
                return record.data.Name;
            }
        }
        ],
        listeners: {
            'itemclick' : function(view, re){  
                get("tree_selected").value = re.data.ID;
                fnSearch(re.data.ID);
            },
            beforeload : function() {  
               Ext.getCmp("span_tree1").body.mask('Loading...');
            },  
            load : function() {  
               Ext.getCmp("span_tree1").body.unmask();
            }
        }
    });

}