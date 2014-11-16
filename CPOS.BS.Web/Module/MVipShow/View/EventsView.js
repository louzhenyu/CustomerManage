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
            fieldLabel: "会员名",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "心得",
            id: "txtExperience",
            name: "Experience",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "商品名称",
            id: "txtItemName",
            name: "ItemName",
            jitSize: 'small'
        },
        {
            xtype: "jitdatefield",
            fieldLabel: "发布时间",
            id: "txtPublishDate",
            name: "BeginTime",
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
        ,{
            xtype: "jitbutton",
            text: "重置",
            margin: '0 0 10 14',
            handler: fnReset
        }
        ]
    });

    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "span_create",
    //    handler: fnCreate
    //    , jitIsHighlight: true
    //    , jitIsDefaultCSS: true
    //});

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsStore"),
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
            store: Ext.getStore("eventsStore"),
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
        //{
        //    text: '序号',
        //    width: 80,
        //    sortable: true,
        //    dataIndex: 'DisplayIndex',
        //    align: 'left'
        //},
        {
            text: '操作',
            width: 150,
            sortable: true,
            dataIndex: 'VipShowId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                if (d.IsCheck != "1") {
                    str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VipShowId + "')\">编辑</a>&nbsp;&nbsp;&nbsp;";
                    if (d.IsDelete != "1") {
                        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">停用</a>";
                    } else {
                        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '0')\">启用</a>";
                    }
                    str += "&nbsp;&nbsp;&nbsp;";
                }
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPraise('" + value + "', '" + d.VipId + "')\">赞</a>";
                if (d.IsCheck != "1") {
                    str += "&nbsp;&nbsp;&nbsp;<a class=\"z_op_link\" href=\"#\" onclick=\"fnPass('" + value + "', '1')\">通过</a>";
                } else {
                    str += "&nbsp;&nbsp;&nbsp;<a class=\"z_op_link\" href=\"#\" onclick=\"fnPass('" + value + "', '0')\">不通过</a>";
                }
                return str;
            }
        },
        {
            text: '会员名',
            width: 130,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VipShowId + "')\">" + value + "</a>";
                return value;
            }
        },
        {
            text: '提交时间',
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
        },
        {
            text: '商品名称',
            width: 130,
            sortable: true,
            dataIndex: 'ItemName',
            align: 'left'
        },
        {
            text: '赞的数量',
            width: 100,
            sortable: true,
            dataIndex: 'PraiseCount',
            align: 'left'
        },
        {
            text: '心得',
            width: 300,
            sortable: true,
            dataIndex: 'Experience',
            flex:true,
            align: 'left'
        },
        {
            text: '图片',
            width: 100,
            sortable: true,
            dataIndex: 'ImageCount',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewImage('" + d.VipShowId + "')\">" + value + "张</a>";
                return str;
            }
        },
        {
            text: '审核状态',
            width: 100,
            sortable: true,
            dataIndex: 'IsCheck',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                var str = d.IsCheck == "1" ? "通过" : "未通过";
                return str;
            }
        }
        ]
    });
}