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
            xtype: "jitbizleventselecttree",
            fieldLabel: "上一级活动",
            id: "txtParentEvent",
            name: "ParentEvent",
            dataType: "ParentEvent",
            jitSize: 'small'
        },   
        {
            xtype: "jittextfield",
            fieldLabel: "活动标题",
            id: "txtTitle",
            name: "Title",
            jitSize: 'small'
        },
        {
            xtype: 'panel',
            colspan: 2,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            width: 400,
            id: 'txtPublishDate',
            items: [{
                xtype: "jitdatefield",
                fieldLabel: "活动时间",
                id: "txtPublishDateBegin",
                name: "DateBegin",
                jitSize: 'small'
            },
            {
                xtype: "label",
                text: "至"
            },
            {
                xtype: "jitdatefield",
                fieldLabel: "",
                id: "txtPublishDateEnd",
                name: "DateEnd",
                jitSize: 'small',
                width: 100
            }]
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
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

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
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },       
        {
            text: '活动标题',
            width: 240,
            sortable: true,
            dataIndex: 'Title',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.EventID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '城市',
            width: 80,
            sortable: true,
            dataIndex: 'CityID',
            align: 'left'
        },
        {
            text: '起始时间',
            width: 150,
            sortable: true,
            dataIndex: 'BeginTime',
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
            text: '结束时间',
            width: 150,
            sortable: true,
            dataIndex: 'EndTime',
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
            text: '联系人',
            width: 100,
            sortable: true,
            dataIndex: 'Content',
            align: 'left'
        },
        {
            text: '电话',
            width: 120,
            sortable: true,
            dataIndex: 'PhoneNumber',
            align: 'left'
        }
        , {
            text: '设为首页',
            width: 120,
            sortable: true,
            dataIndex: 'IsDefault',
            align: 'left',
            renderer: fnFormartShow
        },
        {
            text: '设为置顶',
            width: 120,
            sortable: true,
            dataIndex: 'IsTop',
            align: 'left',
            renderer: fnFormartShow
        }, {
            text: '报名人员',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewUserList2('" + d.EventID + "')\">" +
                    d.EventCount + "人</a>";
                return str;
            }
        }
        ,{
            text: '奖品',
            width: 80,
            hidden: true,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewPrizesList('" + d.EventID + "')\">" + 
                    d.PrizesCount + "个</a>";
                return str;
            }
        }
        ,{
            text: '轮次',
            width: 80,
            sortable: true,
            hidden: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewRoundList('" + d.EventID + "')\">" + 
                    d.RoundCount + "个</a>";
                return str;
            }
        }
        ,{
            text: '抽奖日志',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
              hidden:true,
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewLotteryLogList('" + d.EventID + "')\">查看</a>";
                return str;
            }
        }
        ,{
            text: '中奖人员',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            hidden: true,
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewPrizesWinnerList('" + d.EventID + "')\">查看</a>";
                return str;
            }
        }
        ]
    });
}