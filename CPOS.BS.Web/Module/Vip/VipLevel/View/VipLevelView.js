var tagsData = [], tagsStr = "";
function InitView() {
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipLevelStore"),
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
            store: Ext.getStore("vipLevelStore"),
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
        
//        {
//            text: '操作',
//            width: 50,
//            sortable: true,
//            dataIndex: 'VipCardGradeID',
//            align: 'left',
//            //hidden: __getHidden("delete"),
//            renderer: function (value, p, record) {
//                var str = "";
//                var d = record.data;
//                return str;
//            }
//        }
        //, 
        {
            text: '等级名称',
            width: 110,
            sortable: true,
            dataIndex: 'VipCardGradeName',
            align: 'left'
        }
//        , {
//            text: '是否扩展到会员',
//            width: 110,
//            sortable: true,
//            dataIndex: 'IsExpandVip',
//            align: 'left'
//        }
//        , {
//            text: '累计充值',
//            width: 110,
//            sortable: true,
//            dataIndex: 'AddUpAmount',
//            align: 'left'
//        }
//        , {
//            text: '充值优惠',
//            width: 110,
//            sortable: true,
//            dataIndex: 'PreferentialAmount',
//            align: 'left'
//        }

//        , {
//            text: '消费优惠',
//            width: 110,
//            sortable: true,
//            dataIndex: 'SalesPreferentiaAmount',
//            align: 'left'
//        }, {
//            text: '积分倍数',
//            width: 110,
//            sortable: true,
//            dataIndex: 'IntegralMultiples',
//            align: 'left'
//        }
        , {
            text: '成为会员',
            width: 190,
            sortable: true,
            dataIndex: 'BeVip',
            align: 'left'
        }
        , {
            text: '描述',
            width: 380,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }, {
            text: '会员数量',
            width: 110,
            sortable: true,
            dataIndex: 'VipCardGradeID',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VipCardGradeID + "')\">" + d.VipLevelCount + "</a>";
                return str;
            }
        }
        ]
    });
}