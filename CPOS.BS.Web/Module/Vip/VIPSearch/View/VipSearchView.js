var tagsData = [], tagsStr = "";
function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: 'column',
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        width:815,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "会员",
            id: "txtVipName",
            name: "VipInfo",
            jitSize: 'small',
            //labelWidth: 63,
            hidden:false
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "手机",
            id: "txtPhone",
            name: "Phone",
            jitSize: 'small',
            hidden: false
        }
        ,{
            xtype: "jitbizunitselecttree",
            fieldLabel: "最后消费门店",
            id: "txtUnitName",
            name: "UnitId",
            jitSize: 'small'
            , hidden: false
        }
        ,{
            xtype: "jitbizvipsource",
            fieldLabel: "来源",
            id: "txtVipSource",
            name: "VipSourceId",
            jitSize: 'small'
            , hidden: false
            //, width: 200
        }
        ,{
            xtype: "jitbizstatus",
            fieldLabel: "会员状态",
            id: "txtStatus",
            name: "Status",
            dataType: "vip_status",
            hidden: true,
            jitSize: 'small'
        }
        ,{
            xtype: "jitbizviplevel",
            fieldLabel: "会员等级",
            id: "txtVipLevel",
            name: "VipLevel",
            hidden: true,
            jitSize: 'small'
        }

        ,{
            xtype: 'panel',
            id: 'txtRegistrationTime',
           // colspan: 2,
            // layout: 'hbox',
            layout: 'column',
            border: 0,
            bodyBorder: false,
            width:400,
          //  bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "注册时间",
                    id: "txtRegistrationDateBegin",
                    name: "RegistrationDateBegin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtRegistrationDateEnd",
                    name: "RegistrationDateEnd",
                    jitSize: 'small',
                    width: 100
                }
            ]
         }
         
        ,{
            xtype: 'panel',
            id: 'txtIntegration',
           // colspan: 2,
            // layout: 'hbox',
            layout: 'column',
            border: 0,
            bodyBorder: false,
            width:406,
          //  bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            items: [
                {
                    xtype: "jittextfield",
                    fieldLabel: "积分",
                    id: "txtIntegrationBegin",
                    name: "IntegrationBegin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jittextfield",
                    fieldLabel: "",
                    id: "txtIntegrationEnd",
                    name: "IntegrationEnd",
                    jitSize: 'small',
                    width: 100
                }
            ]
         }
        //,{
        //    xtype: 'panel',
        //    id: 'txtIntegration',
        //    colspan: 2,
        //    layout: 'hbox',
        //    border: 0,
        //    bodyBorder: false,
        //    bodyStyle: 'background:#F1F2F5;',
        //    hidden: true,
        //    items: [
        //        {
        //            xtype: "jittextfield",
        //            fieldLabel: "积分",
        //            id: "txtIntegrationBegin",
        //            name: "IntegrationBegin",
        //            jitSize: 'small'
        //        },
        //        {
        //            xtype: "label",
        //            text: "至"
        //        },
        //        {
        //            xtype: "jittextfield",
        //            fieldLabel: "",
        //            id: "txtIntegrationEnd",
        //            name: "IntegrationEnd",
        //            jitSize: 'small',
        //            width: 100
        //        }
        //    ]
        //    }
        ,{
            xtype: 'panel',
            id: 'txtRecentlySalesDate',
           // colspan: 2,
            // layout: 'hbox',
            layout: 'column',
            border: 0,
            bodyBorder: false,
            width:400,
          //  bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "最近消费时间",
                    id: "txtRecentlySalesDateBegin",
                    name: "RecentlySalesDateBegin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtRecentlySalesDateEnd",
                    name: "RecentlySalesDateEnd",
                    jitSize: 'small',
                    width: 100
                }
            ]
         }
        //,{
        //    xtype: 'panel',
        //    id: 'txtRecentlySalesDate',
        //    colspan: 2,
        //    layout: 'hbox',
        //    border: 0,
        //    bodyBorder: false,
        //    bodyStyle: 'background:#F1F2F5;',
        //    hidden: true,
        //    items: [
        //        {
        //            xtype: "jitdatefield",
        //            fieldLabel: "最近消费时间",
        //            id: "txtRecentlySalesDateBegin",
        //            name: "RecentlySalesDateBegin",
        //            jitSize: 'small'
        //        },
        //        {
        //            xtype: "label",
        //            text: "至"
        //        },
        //        {
        //            xtype: "jitdatefield",
        //            fieldLabel: "",
        //            id: "txtRecentlySalesDateEnd",
        //            name: "RecentlySalesDateEnd",
        //            jitSize: 'small',
        //            width: 100
        //        }
        //    ]
        //  }
        
            
        ,{
            xtype: "jitbizunitselecttree",
            fieldLabel: "会籍店",
            id: "txtMembershipShop",
            name: "MembershipShop",
            hidden: true,
            jitSize: 'small'
        }
            , {
                xtype: "jitbiztags",
                fieldLabel: "标签",
                id: "txtTags",
                name: "txtTags",
                hidden: true,
                jitSize: 'small'
            }, {
                xtype: "jitbiztagsgroup",
                fieldLabel: "组合关系",
                id: "txtTagsGroup",
                name: "txtTagsGroup",
                hidden: true,
                jitSize: 'small'
            }
            , {
                xtype: "jitbutton",
                id:"btnAddGroup",
                text: "获取",
                hidden: true,
                margin: '0 0 10 14',
                handler: fnAddGroup
            }
//            , {
//                xtype: "jitdivfield",
//                fieldLabel: "已选",
//                id: "txtAddedTags",
//                name: "txtAddedTags",
//                hidden: true,
//                jitSize: 'big'
//            }

        ]

    });
    
    // btn_panel
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
        //width: 200,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
            
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
        }
        , {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
        }
        ]

    });

    Ext.create('Jit.button.Button', {
        text: "清 除",
        renderTo: "btnCancel"
        , handler: fnCancel
    });
    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "span_create",
    //    //hidden: __getHidden("create"),
    //    handler: fnCreate
    //});

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipSearchStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("vipSearchStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'VIPID',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        {
            text: '会员号码',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VIPID + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '会员名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VIPID + "')\">" + value + "</a>";
                return str;
            }
        }, {
            text: '会籍店',
            width: 110,
            sortable: true,
            dataIndex: 'MembershipShop',
            align: 'left'
        }
        ,{
            text: '手机号',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        }
        , {
            text: '关注时间',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }, {
            text: '最后操作时间',
            width: 130,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ,{
            text: '最近消费时间',
            width: 140,
            sortable: true,
            dataIndex: 'RecentlySalesTime',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ,{
            text: '最近消费门店',
            width: 110,
            sortable: true,
            dataIndex: 'TencentMBlog',
            align: 'left'
        }
        ,{
            text: '会员标签',
            width: 140,
            sortable: true,
            dataIndex: 'VipTagsCount',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                p.tdAttr = 'data-qtip="' + d.VipTagsLong + '"';
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VIPID + "', 4)\">" + value + "个</a>";
                //str += "<font color=\"blue\" onclick=\"\">" + value + "个</font>";
                return str;
            }
        }
        
        ,{
            text: '有效积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
        }, {
            text: '来源',
            width: 110,
            sortable: true,
            dataIndex: 'VipSourceName',
            align: 'left'
        }
        ,{
            text: '会员等级',
            width: 110,
            sortable: true,
            dataIndex: 'VipLevelDesc',
            align: 'left'
        }
        ,{
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'StatusDesc',
            align: 'left'
        }
        
        ]
    });
}