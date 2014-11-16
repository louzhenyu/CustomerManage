function InitView() {
    
    Ext.create('jit.biz.WMaterialType', {
        id: "txtMaterialTypeId",
        text: "",
        renderTo: "txtMaterialTypeId",
        dataType: "get_list",
        listeners: {
            change: function (store) { 
                fnSearch();
            }
        },
        width: 100
    });

    //operator area
    Ext.create('Jit.button.Button', {
        id: 'span_create',
        text: "添加素材",
        renderTo: "span_create",
        handler: fnCreate
        ,hidden: true
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "确定",
        renderTo: "btnSave",
        //hidden: __getHidden("create"),
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "关闭",
        renderTo: "btnClose",
        //hidden: __getHidden("create"),
        handler: fnClose
    });
    
    Ext.create('Jit.form.field.TextArea', {
        id: "DivGridView1",
        text: "",
        renderTo: "DivGridView1",
        margin: "0 0 0 0",
        width: '100%',
        height: 250
    });
    
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem2Store"),
        id: "gridView2",
        renderTo: "DivGridView2",
        columnLines: true,
        height: 250,
        width: '100%',
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem2Store"),
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
            dataIndex: 'ImageId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete2('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '名称',
            width: 200,
            sortable: true,
            dataIndex: 'ImageName',
            align: 'left'
            ,flex: true
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView2('" + d.ImageId + "')\">" + value + "</a>";
                return str;
            }
        }
        //,{
        //    text: '文本内容',
        //    width: 330,
        //    sortable: true,
        //    dataIndex: 'Text',
        //    align: 'left'
        //    ,renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        str = value;
        //        //str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView2('" + d.TextId + "')\">" + value + "</a>";
        //        return str;
        //    }
        //}
        ]
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem3Store"),
        id: "gridView3",
        renderTo: "DivGridView3",
        columnLines: true,
        height: 320,
        width: '100%',
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar3",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem3Store"),
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
            dataIndex: 'TextId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete3('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '标题',
            width: 200,
            sortable: true,
            dataIndex: 'Title',
            align: 'left'
            ,flex:true
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView3('" + d.TextId + "')\">" + value + "</a>";
                return str;
            }
        }
        //,{
        //    text: '文本内容',
        //    width: 330,
        //    sortable: true,
        //    dataIndex: 'Text',
        //    align: 'left'
        //    ,renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        str = value;
        //        //str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView3('" + d.TextId + "')\">" + value + "</a>";
        //        return str;
        //    }
        //}
        ]
    });
    
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem4Store"),
        id: "gridView4",
        renderTo: "DivGridView4",
        columnLines: true,
        height: 250,
        width: '100%',
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar4",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem4Store"),
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
            dataIndex: 'VoiceId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete4('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '名称',
            width: 200,
            sortable: true,
            dataIndex: 'VoiceName',
            align: 'left'
            ,flex: true
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView4('" + d.VoiceId + "')\">" + value + "</a>";
                return str;
            }
        }
        //,{
        //    text: '文本内容',
        //    width: 330,
        //    sortable: true,
        //    dataIndex: 'Text',
        //    align: 'left'
        //    ,renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        str = value;
        //        //str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView4('" + d.VoiceId + "')\">" + value + "</a>";
        //        return str;
        //    }
        //}
        ]
    });
    
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("WMenuAddItem5Store"),
        id: "gridView5",
        renderTo: "DivGridView5",
        columnLines: true,
        height: 250,
        width: '100%',
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar5",
            defaultType: 'button',
            store: Ext.getStore("WMenuAddItem5Store"),
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
            dataIndex: 'TextId',
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
            text: '标题',
            width: 200,
            sortable: true,
            dataIndex: 'Title',
            align: 'left'
            ,flex: true
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //str = value;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView5('" + d.TextId + "')\">" + value + "</a>";
                return str;
            }
        }
        //,{
        //    text: '文本内容',
        //    width: 330,
        //    sortable: true,
        //    dataIndex: 'Text',
        //    align: 'left'
        //    ,renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        str = value;
        //        //str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.TextId + "')\">" + value + "</a>";
        //        return str;
        //    }
        //}
        ]
    });

}