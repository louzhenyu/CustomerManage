function InitView() {
    /*活动类型查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "eventsType",
            name: "Title",
            fieldLabel: "活动类型",
            jitSize: 'small'
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
            hidden: __getHidden("search")
            , handler: fnSearch
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
    /*添加*/
    Ext.create('Jit.button.Button', {
        imgName: 'create',
        isImgFirst: true,
        margin: '0 0 0 10',
        renderTo: 'dvWork',
        handler: function () {
            fnAddEditView();
        }
    });
    /*活动类型列表*/
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore('EventsTypeStore'),
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
            dataIndex: 'EventTypeID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
         {
             text: '活动类型名称',
             width: 120,
             sortable: true,
             dataIndex: 'Title',
             jitDataType: "tips",
             align: 'left',
             renderer: function (value, p, record) {
                 var str = "";
                 var d = record.data;
                 str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.EventTypeID + "')\">" + value + "</a>";
                 return str;
             }
         },
        {
            text: '分组编号',
            width: 160,
            sortable: true,
            dataIndex: 'GroupNo'
        },
        {
            text: '备注',
            width: 160,
            sortable: true,
            dataIndex: 'Remark'
        },
        {
            text: '最后修改人',
            width: 120,
            sortable: true,
            dataIndex: 'LastUpdateBy',
            hidden: true
        },
        { xtype: "jitcolumn",
            text: '最后修改时间',
            width: 140,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            jitDataType: "datetimenotss"
        }],
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("EventsTypeStore"),
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
    /*修改界面*/

    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 0,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>分类名称",
            name: 'Title',
            jitSize: 'small',
            id: 'txtTitle'
        }
        , {
            xtype: "jitnumberfield",
            fieldLabel: "<font color='red'>*</font>分组编号",
            name: "GroupNo",
            id: "txtGroupNo",
            value: "1"
        }, {
            xtype: 'label',
            html: "<div><font color=red>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp  分组编号用于微信、APP上分组显示不同活动分类列表</font></div>"
        } ,{
            xtype: "jittextfield",
            fieldLabel: "备注",
            name: "Remark",
            id: "txtRemark"
        }]
    });
    /*创建弹出界面*/
    Ext.create('Jit.window.Window', {
        jitSize: "large",
        title: '活动分类管理',
        id: "editWin",
        items: [Ext.getCmp("editPanel")],
        border: 1,
        buttons: ['->', {
            xtype: "jitbutton",
            text: "保&nbsp;&nbsp;存",
            id: 'btnSave',
            jitIsHighlight: true,
            jitIsDefaultCSS: true
           , handler: fnSave
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
