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
            fieldLabel: "相册标题",
            id: "txtTitle",
            name: "Title",
            jitSize: 'small'
        },
        {
            xtype: "jitbizalbumtype",
            fieldLabel: "相册类型",
            id: "txtAlbumType",
            name: "AlbumType",
            dataType: "AlbumType",
            jitSize: 'small'
        },
        {
            xtype: "jitbizalbummoduletype",
            fieldLabel: "模块类型",
            id: "txtAlbumModuleType",
            name: "AlbumModuleType",
            dataType: "AlbumModuleType",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "模块标题",
            id: "txtModuleTypeName",
            name: "ModuleTypeName",
            jitSize: 'small'
        }]
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
            handler: fnSearch,
            jitIsHighlight: true,
            jitIsDefaultCSS: true
        },
        {
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
        handler: fnCreate,
        jitIsHighlight: true,
        jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("AlbumStore"),
        id: "gridView",
        renderTo: "gridView",
        columnLines: true,
        height: 387,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("AlbumStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'AlbumId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                if (d.Type == "1") {
                    str += "&nbsp;&nbsp;<a class=\"z_op_link\" href=\"#\" onclick=\"fnViewImage('" + value + "')\">相片管理</a>";
                }
                return str;
            }
        },
        {
            text: '相册标题',
            width: 230,
            sortable: true,
            dataIndex: 'Title',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.AlbumId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '相册类型',
            width: 100,
            sortable: true,
            dataIndex: 'TypeName',
            align: 'left'
        },
        {
            text: '序号',
            width: 80,
            sortable: true,
            dataIndex: 'SortOrder',
            align: 'left'
        },
        {
            text: '发布时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        },
        {
            text: '模块类型',
            width: 100,
            sortable: true,
            dataIndex: 'ModuleTypeName',
            align: 'left'
        },
        {
            text: '模块标题',
            width: 230,
            sortable: true,
            dataIndex: 'ModuleName',
            align: 'left'
        }
        ]
    });
}