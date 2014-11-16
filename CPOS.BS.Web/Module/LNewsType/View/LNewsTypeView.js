function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            fieldLabel: "类型名称",
            id: "txtNewsTypeName",
            name: "NewsTypeName",
            jitSize: 'small',
            isDefault: true,
            id: 'serchObjectType'
        },
        //                {
        //                    xtype: "jitcombobox",
        //                    fieldLabel: "父类型",
        //                    emptyText: '--请选择--',
        //                    store: Ext.getStore("searchPartentTypeStore"),
        //                    valueField: "NewsTypeId",
        //                    displayField: "NewsTypeName",
        //                    name: "ParentTypeId",
        //                    id: "serchParentTypeId"
        //                }
                 {

                 xtype: "jitbizlnewstypeselecttree",
                 fieldLabel: "父类型",
                 name: "ParentTypeId",
                 id: "serchParentTypeId"
             }
        ],
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
            handler: function () { fnSearch(Ext.getCmp("serchParentTypeId").selectedValues[0].id) }
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
        store: Ext.getStore('LNewsTypeStore'),
        id: 'gridView',
        renderTo: 'DivGridView',
        columnLines: true,
        height: 420,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("LNewsTypeStore"),
            pageSize: JITPage.PageSize.getValue()     //定义pageSize
        }),
        columns: [
        {
            text: '操作',
            width: 40,
            sortable: true,
            dataIndex: 'NewsTypeId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";

                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";

                return str;
            }
        },
        {
            text: '类型名称',
            width: 160,
            sortable: true,
            dataIndex: 'NewsTypeName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.NewsTypeId + "')\">" + value + "</a>";
                return str;
            }

        },
         {
             text: '级别',
             width: 80,
             sortable: true,
             dataIndex: 'TypeLevel',
             align: 'right'

         }, {
             text: '父类型名称',
             width: 160,
             sortable: true,
             dataIndex: 'ParentTypeName',
             align: 'left'
         },
         {
             text: '创建时间',
             xtype: 'jitcolumn',
             jitDataType: 'Date',
             width: 140,
             sortable: true,
             dataIndex: 'CreateTime',
             align: 'left'
         }],
        listeners: {
            render: function (p) {
                p.setLoading({                    //执行了什么？分页事件
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
        items: [
        //        {
        //            xtype: "jitcombobox",
        //            fieldLabel: "父类型",
        //            emptyText: '--请选择--',
        //            store: Ext.getStore("PartentTypeStore"),
        //            valueField: "NewsTypeId",
        //            displayField: "NewsTypeName",
        //            name: "ParentTypeId",
        //            id: "winParentTypeId"
        //        },
         {
         xtype: "jitbizlnewstypeselecttree",
         fieldLabel: "父类型",
         name: "ParentTypeId",
         id: "winParentTypeId"
        },
         {
             xtype: "jittextfield",
             fieldLabel: "<font color='red'>*</font>类型名称",
             name: "NewsTypeName",
             id: "txt_NewsTypeName",
             readOnly: false,
             allowBlank: true
         }
         ,
         {
             xtype: "jittextfield",
             fieldLabel: "频道编号",
             name: "ChannelCode",
             id: "txt_ChannelCode",
             readOnly: false,
             allowBlank: true
             ,value:1    //默认值是1
         }

        ]
    });

    // //创建弹出窗体
    Ext.create('Jit.window.Window', {
        jitSize: "large",
        title: '添加资讯类型',
        id: "editWin",
        items: [Ext.getCmp("editPanel")],    //用了上面定义的一个panel
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
   