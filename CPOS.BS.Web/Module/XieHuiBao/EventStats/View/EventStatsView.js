function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitbizoptions",
            fieldLabel: "类型",
            OptionName: 'EventsStat',
            name: 'serchObjectType',
            jitSize: 'small',
            isDefault: true,
            id: 'serchObjectType',
            listeners: {
                "select": function () {
                    var objecttype = Ext.getCmp("serchObjectType").getValue();
                    if (objecttype == 0 || objecttype == null) {
                        Ext.getStore("serchStore").removeAll();
                        Ext.getCmp("serchTitle").setValue("");
                    }
                    else {
                        // var objecttype = Ext.getCmp("serchObjectType").getValue();
                        Ext.getStore("serchStore").removeAll();
                        Ext.getCmp("serchTitle").setValue("");
                        //var objecttype = Ext.getCmp("serchObjectType").getValue();
                        Ext.getStore("serchStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetOptionID&objecttype=" + objecttype;
                        Ext.getStore("serchStore").load();
                    }
                }
            }
        },
        {
            xtype: "jitcombobox",
            fieldLabel: "标题",
            emptyText: '--请选择--',
            store: Ext.getStore("serchStore"),
            valueField: "NewsID",
            displayField: "Title",
            name: "serchTitle",
            id: "serchTitle",
            jitSize: 'small',
            isDefault: true
        }],
        renderTo: 'search_form_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    /*查询按钮区域*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        items: [{
            xtype: "jitbutton",
            height: 22,
            isImgFirst: true,
            text: "查询",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("search"),
            handler: fnSearch
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "重置",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }],
        renderTo: 'search_button_panel',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    //添加
    Ext.create('Jit.button.Button', {
        imgName: 'create',
        isImgFirst: true,
        margin: '0 0 0 10',
        renderTo: 'dvWork',
        handler: function () {
            fnAddEditView();
        }
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore('EventStatsStore'),
        id: 'gridView',
        renderTo: 'DivGridView',
        columnLines: true,
        height: 420,
        stripeRows: true,
        width: "100%",
        columns: [
        {
            text: '操作',
            width: 40,
            sortable: true,
            dataIndex: 'EventStatsID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";

                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";

                return str;
            }
        }, {
            text: '排序',
            width: 80,
            sortable: true,
            dataIndex: 'Sequence',
            align: 'left'
        }, 
        {
            text: '类型',
            width: 80,
            sortable: true,
            dataIndex: 'Type_Name',
            align: 'left'
        },
         {
             text: '标题',
             width: 200,
             sortable: true,
             dataIndex: 'Title',
             jitDataType: "tips",
             align: 'left',
             renderer: function (value, p, record) {
                 var str = "";
                 var d = record.data;
                 str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.EventStatsID + "')\">" + value + "</a>";
                 return str;
             }
         },
        {
            text: '浏览数量',
            width: 80,
            sortable: true,
            dataIndex: 'BrowseNum',
            xtype: 'jitcolumn',
            jitDataType: "int"
        },
        {
            text: '点赞数量',
            width: 80,
            sortable: true,
            dataIndex: 'PraiseNum',
            xtype: 'jitcolumn',
            jitDataType: "int"
        },
        {
            text: '收藏数量',
            width: 80,
            sortable: true,
            dataIndex: 'BookMarkNum',
            xtype: 'jitcolumn',
            jitDataType: "int"
        }, {
            xtype: 'jitcolumn',
            jitDataType: "int",
            text: '分享',
            width: 80,
            sortable: true,
            dataIndex: 'ShareNum',
            align: 'left'
        }],
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("EventStatsStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });



    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 0,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        items: [{
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>类型",
            OptionName: 'EventsStat',
            name: 'ObjectType',
            jitSize: 'small',
            isDefault: true,
            id: 'ObjectType',
            listeners: {
                "select": function () {
                    var objecttype = Ext.getCmp("ObjectType").getValue();
                    if (objecttype == 0 || objecttype == null) {
                        Ext.getStore("titleStore").removeAll();
                        Ext.getCmp("title").setValue("");
                    }
                    else {
                        Ext.getStore("titleStore").removeAll();
                        Ext.getCmp("title").setValue("");
                        var objecttype = Ext.getCmp("ObjectType").getValue();
                        Ext.getStore("titleStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetOptionID&objecttype=" + objecttype;
                        Ext.getStore("titleStore").load();
                    }

                }
            }
        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>标题",
            emptyText: '--请选择--',
            store: Ext.getStore("titleStore"),
            valueField: "NewsID",
            displayField: "Title",
            name: "title",
            id: "title"
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "排序",
            name: "sequence",
            value:1000,
            id: "txt_sequence",
            readOnly: false,
            allowBlank: true
        }]
    });

    //创建弹出窗体
    Ext.create('Jit.window.Window', {
        jitSize: "large",
        title: '添加最受关注',
        id: "editWin",
        items: [Ext.getCmp("editPanel")],
        border: 1,
        buttons: ['->', {
            xtype: "jitbutton",
            text: "保&nbsp;&nbsp;存",
            id: 'btnSave',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: fnSave
        }, {
            xtype: "jitbutton",
            text: "关&nbsp;&nbsp;闭",
            id: 'btnClose',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                Ext.getCmp("editWin").hide();
            }
        }],
        closeAction: 'hide'
    });
}
   