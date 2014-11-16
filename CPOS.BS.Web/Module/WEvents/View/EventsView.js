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
             xtype: "jitbizleventstype",
             fieldLabel: "活动类型",
             id: "cmbEventTypeID",
             name: "EventTypeID",
             isDefault: true,
             jitSize: 'small'
         }, {
             xtype: "jittextfield",
             fieldLabel: "活动标题",
             id: "txtTitle",
             name: "Title",
             jitSize: 'small'
         }, {
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
             }, {
                 xtype: "label",
                 text: "至"
             }, {
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
        items: [{
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }, {
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

    var cellEditing = Ext.create('Ext.grid.plugin.CellEditing', {
        clicksToEdit: 1,
        listeners: {
            "edit": function (a, b, c) {
                var value = b.value;
                var originalValue = b.originalValue;
                b.record.data[b.field] = originalValue;
                Ext.getStore("eventsStore").sort({ property: "isdelete", direction: 'ASC' });
            }
        }

    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsStore"),
        plugins: [cellEditing],
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
        }, {
            text: '活动类型',
            width: 100,
            align: 'left',
            dataIndex: 'EventTypeName'
        }, {
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
        }, {
            text: '城市',
            width: 80,
            sortable: true,
            dataIndex: 'CityID',
            align: 'left'
        }, {
            text: '联系人',
            width: 100,
            sortable: true,
            dataIndex: 'Content',
            align: 'left'
        }, {
            text: '状态',
            width: 60,
            sortable: true,
            dataIndex: 'EventStatus',
            align: 'left',
            renderer: function (value, p, record) {
                var status = '无效';
                switch (value) {
                    case 1:
                        status = '有效';
                        break;
                    case 10:
                        status = '未开始';
                        break;
                    case 20:
                        status = '运行中';
                        break;
                    case 30:
                        status = '暂停';
                        break;
                    case 40:
                        status = '结束';
                        break;
                }
                return status;
            }
        }, {
            text: '电话',
            width: 120,
            sortable: true,
            dataIndex: 'PhoneNumber',
            align: 'left'
        }, {
            text: '报名人员',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (record.data.CustomerId == "a2573925f3b94a32aca8cac77baf6d33" || record.data.CustomerId == "75a232c2cf064b45b1b6393823d2431e" || record.data.CustomerId == "376f4455b43647fd8bda39a3bb39eb73" || record.data.CustomerId == "1c6a39e4a9e54fecb508abfa5cda9eaa" || record.data.CustomerId == "56319f7e9c74424dba95b8e96d2487bc") {
                    str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewUserList_New('" + d.EventID + "')\">" + d.AppliesCount + "人</a>";
                } else {
                    str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewUserList2('" + d.EventID + "')\">" + d.AppliesCount + "人</a>";
                }
                return str;
            }
        }, {
            text: '奖品',
            width: 80,
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
        }, {
            text: '轮次',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewRoundList('" + d.EventID + "')\">" +
                    d.RoundCount + "个</a>";
                return str;
            }
        }, {
            text: '抽奖日志',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewLotteryLogList('" + d.EventID + "')\">查看</a>";
                return str;
            }
        }, {
            text: '中奖人员',
            width: 80,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnViewPrizesWinnerList('" + d.EventID + "')\">查看</a>";
                return str;
            }
        }, {
            text: '活动Id',
            width: 140,
            sortable: true,
            dataIndex: 'EventID',
            align: 'left',
            field: { xtype: 'textfield' }
        }, {
            xtype: "jitcolumn",
            jitDataType: "datetimenotss",
            text: '起始时间',
            width: 120,
            sortable: true,
            dataIndex: 'BeginTime',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: "datetimenotss",
            text: '结束时间',
            width: 120,
            sortable: true,
            dataIndex: 'EndTime',
            align: 'left'
        }]
    });

    /*选择终端窗口*/
    Ext.create('Jit.window.Window', {
        id: 'userListWin',
        title: '会员列表',
        width: 800,
        height: 562,
        modal: true,
        border: 0,
        closeAction: 'hide',
        html: '<iframe id="frame" name="frame" frameborder="no" height="100%" width="100%" marginheight="0" marginwidth="0" scrolling="auto"  src=""/>'
    });
}