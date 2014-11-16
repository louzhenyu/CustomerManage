function InitView() {
    
    //operator area
    Ext.create('Jit.button.Button', {
        text: "清空",
        renderTo: "btnReset",
        handler: fnReset
    });
    Ext.create('Jit.button.Button', {
        text: "下一步",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "上一步",
        renderTo: "btnPre",
        handler: fnPre
    });

    Ext.create('Jit.button.Button', {
        text: "目标人群选取",
        renderTo: "btnImport",
        handler: fnImport
    });
    
    var fileUpload = Ext.create('Ext.form.field.File', {
        renderTo: 'fileUpload',
        width: 200,
        hideLabel: true
    });
    Ext.create('Ext.button.Button', {
        text: '上传',
        renderTo: 'btnUpload',
        handler: function(){
        }
    });

    // gridStore list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("MarketPersonStore"),
        id: "gridMarketPerson",
        renderTo: "gridMarketPerson",
        columnLines: true,
        height: 419,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("MarketPersonStore"),
            pageSize: 1000//JITPage.PageSize.getValue()
        }),
        //listeners: {
        //    render: function (p) {
        //        p.setLoading({
        //            store: p.getStore()
        //        }).hide();
        //    }
        //},
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'MarketPersonID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '卡号',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '等级',
            width: 110,
            sortable: true,
            dataIndex: 'VipLevel',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '姓名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
            ,flex: true
        }
       
        ,{
            text: '微信',
            width: 110,
            sortable: true,
            dataIndex: 'WeiXin',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
            ,flex: true
        }, {
            text: '最新更新时间',
            width: 110,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            align: 'left'
            , flex: true
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ,{
            text: '购买金额',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseAmount',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '购买频次',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseCount',
            align: 'left'
            ,flex: true
        }
        ]
    });

}