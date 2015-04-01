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
            fieldLabel: "微信账号名称",
            id: "txtWeiXinName",
            name: "WeiXinName",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "平台唯一码",
            id: "txtWeiXinID",
            name: "WeiXinID",
            jitSize: 'small'
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
        ]
    });


    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });


    Ext.create('Jit.button.Button', {
        text: "批量导入微信公众号会员",
        renderTo: "Bulkimport",
	hidden: true, 
        //hidden: __getHidden("create"),
        handler: fnImport
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });


    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("wApplicationStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        viewConfig: {
            enableTextSelection: true
        },
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("wApplicationStore"),
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
            width: 110,
            sortable: true,
            dataIndex: 'ApplicationId',
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
            text: '微信账号名称',
            width: 200,
            sortable: true,
            dataIndex: 'WeiXinName',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.ApplicationId + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '微信账号唯一码',
            width: 200,
            sortable: true,
            dataIndex: 'WeiXinID',
            align: 'left'
        }
        , {
            text: '公众号主标识',
            width: 250,
            sortable: true,
            dataIndex: 'ApplicationId',
            align: 'left'
        }
        , {
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
        , {
            text: '创建人',
            width: 110,
            sortable: true,
            dataIndex: 'CreateByName',
            align: 'left'
        }
         , {
             text: '清空Session',
             width: 110,
             dataIndex: 'ApplicationId',
             align: 'left', renderer: function (value, p, record) {
                 var str = "";
                 var d = record.data;
                 //                 str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.ApplicationId + "')\">" + value + "</a>";
                 str = "<div> <input type=button id=remove  value=清空 style=width:50px onclick=fnRemoveSession('" + d.ApplicationId + "') /></div>"
                 return str;
             }
         }
        ]
    });
}