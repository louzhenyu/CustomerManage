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
            name: "prop_name",
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
            xtype: "jitbizpropdomain",
            fieldLabel: "属性域",
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
        store: Ext.getStore("PropStore"),
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
            store: Ext.getStore("PropStore"),
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
            dataIndex: 'Prop_Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '名称',
            width: 200,
            sortable: true,
            dataIndex: 'Prop_Name',
            align: 'left',
            flex: true
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Prop_Id + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '代码',
            width: 110,
            sortable: true,
            dataIndex: 'Prop_Code',
            align: 'left'
        }
        , {
            text: '属性类型',
            width: 110,
            sortable: true,
            dataIndex: 'Prop_Type_Name',
            align: 'left'
        }
        , {
            text: '输入类型',
            width: 140,
            sortable: true,
            dataIndex: 'Prop_Input_Flag',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                switch (value) {
                    case "label": str = "文字"; break;
                    case "text": str = "输入文本"; break;
                    case "textarea": str = "输入文本(多行)"; break;
                    case "textnumber": str = "输入文本(数字)"; break;
                    case "select": str = "下拉选项"; break;
                    case "select-date-(yyyy-MM-dd)": str = "日期选项(yyyy-MM-dd)"; break;
                    case "select-date-(yyyy-MM)": str = "日期选项(yyyy-MM)"; break;
                    case "htmltextarea": str = "富文本"; break;
                    case "fileupload": str = "图片上传"; break;
                    case "keyvalue": str = "键值对"; break;
                }
                return str;
            }
        }
        , {
            text: '序号',
            width: 80,
            sortable: true,
            dataIndex: 'Display_Index',
            align: 'left'
        }
        , {
            text: '最后修改时间',
            width: 130,
            sortable: true,
            dataIndex: 'Modify_Time',
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
        store: Ext.getStore("PropParentStore"),
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
                return record.data.prop_name;
            }
        }
        ],
        listeners: {
            'itemclick' : function(view, re){
                get("tree_selected").value = re.data.ID;
                get("tree_prop_type").value = re.data.prop_type;
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