function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: 'column',
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        width: 815,
        items: [
        {
            xtype: "jitbizleventselecttree",
            fieldLabel: "活动标题",
            id: "txtEvent",
            name: "Event",
            dataType: "ParentEvent",
            jitSize: 'small'
        }
        ,
        {
            xtype: "jittextfield",
            fieldLabel: "姓名",
            id: "txtName",
            name: "VipName",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "手机号码",
            id: "txtPhone",
            name: "Phone",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizyesnostatus",
            fieldLabel: "是否已注册",
            id: "txtRegister",
            name: "Register",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizyesnostatus",
            fieldLabel: "是否已签到",
            id: "txtSign",
            name: "Sign",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizyesnostatus",
            fieldLabel: "是否可抽奖",
            id: "txtLottery",
            name: "Lottery",
            jitSize: 'small',
            hidden: true
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
        //, {
        //    xtype: "jitbutton",
        //    id: "btnMoreSearchView",
        //    text: "高级查询",
        //    margin: '0 0 10 14',
        //    handler: fnMoreSearchView
        //}
        ]
    });

    //operator area
    Ext.create('Ext.form.Panel', {
        id: 'operation_panel',
        layout: {
            type: 'table',
            columns: 6
        },
        renderTo: 'span_operation',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "添加",
            handler: fnCreate
			, imgName: 'create'
			, isImgFirst: true
			, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
        }
		, {
		    xtype: "jitbutton",
		    text: "删除",
		    handler: fnDelete
		    //, imgName: 'create'
		    //, isImgFirst: true
			, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
		}
		, {
		    xtype: "jitbutton",
		    text: "取消注册",
		    handler: UnRegister
		    //, imgName: 'create'
		    //, isImgFirst: true
			, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
            , hidden: true
		}
		, {
		    xtype: "jitbutton",
		    text: "生成二维码",
		    handler: fnGenerateQR
		    //, imgName: 'create'
		    //, isImgFirst: true
		    //, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
            , hidden: true
		}
        , {
            xtype: "jitbutton",
            text: "微信推送",
            handler: fnPushWeixin
            //, imgName: 'create'
            //, isImgFirst: true
			, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
        }
        , {
            xtype: "jitbutton",
            text: "屏蔽抽奖",
            handler: fnForbidLottery
            //, imgName: 'create'
            //, isImgFirst: true
            //, hidden: __getHidden("create")
			, jitIsHighlight: true
			, jitIsDefaultCSS: true
            , hidden: true
        }
        ]
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventEventVipStore"),
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
            store: Ext.getStore("eventEventVipStore"),
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
        //    text: '操作',
        //    width: 80,
        //    sortable: true,
        //    dataIndex: 'EventVipId',
        //    align: 'left',
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        if (d.IsSigned == 0)
        //            str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 1)\">停用</a>";
        //        else
        //            str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 0)\">启用</a>";
        //        return str;
        //    }
        //},
        {
        text: '人员编号',
        width: 100,
        sortable: true,
        dataIndex: 'VipId',
        hidden: true,
        align: 'left'

    }
        ,
        {
            text: '姓名',
            width: 100,
            sortable: true,
            dataIndex: 'UserName',
            align: 'left'
            , renderer: function (value, p, record) {
                debugger;
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.SignUpID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '所属公司',
            width: 100,
            sortable: true,
            dataIndex: 'VipCompany',
            align: 'left'
        },
        {
            text: '职位',
            width: 100,
            sortable: true,
            dataIndex: 'VipPost',
            align: 'left'
        },
        {
            text: '联系电话',
            width: 100,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        },
        {
            text: '电子邮箱',
            width: 100,
            sortable: true,
            dataIndex: 'Email',
            align: 'left'
        },
        {
            text: '是否注册',
            width: 100,
            sortable: true,
            dataIndex: 'IsRegistered',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (value == true)
                    str = "是";
                else
                    str = "否";
                return str;
            }
        },
        {
            text: '是否签到',
            width: 100,
            sortable: true,
            dataIndex: 'IsSigned',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (value == true)
                    str = "是";
                else
                    str = "否";
                return str;
            }
        }
        , {
            text: '是否可抽奖',
            width: 100,
            sortable: true,
            dataIndex: 'CanLottery',
            hidden: true,
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (value == true)
                    str = "是";
                else
                    str = "否";
                return str;
            }
        }
        ]
});
}