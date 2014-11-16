function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 5
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "名称",
            id: "txtBrandName",
            name: "BrandName",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "代码",
            id: "txtBrandCode",
            name: "BrandCode",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        }]

    });
    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("BrandDetailStore"),
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
            store: Ext.getStore("BrandDetailStore"),
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
            dataIndex: 'BrandId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '0')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '名称',
            width: 150,
            sortable: true,
            dataIndex: 'BrandName',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.BrandId + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '代码',
            width: 150,
            sortable: true,
            dataIndex: 'BrandCode',
            align: 'left'
        }
        ,{
            text: '描述',
            width: 340,
            sortable: true,
            dataIndex: 'BrandDesc',
            align: 'left'
        }
        //,{
        //    text: '品牌LOGO',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'BrandLogoURL',
        //    align: 'left'
        //}
        ,{
            text: '联系电话',
            width: 110,
            sortable: true,
            dataIndex: 'Tel',
            align: 'left'
        }
        ,{
            text: '排序',
            width: 110,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        //,{
        //    text: '状态',
        //    width: 110,
        //    sortable: true,
        //    dataIndex: 'Status',
        //    align: 'left',
        //    renderer: function (value, p, record) {
        //        if (value == "-1") return "停用";
        //        else if (value == "1") return "正常";
        //        return "";
        //    }
        //}
        ]
    });
}