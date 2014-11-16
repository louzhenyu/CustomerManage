function InitView() {

    //operator area
    Ext.create('Jit.button.Button', {
        text: "确认",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    // gridStore list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("VipStore"),
        id: "gridVipSearch",
        renderTo: "gridVipSearch",
        columnLines: true,
        height: 389,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("VipStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '卡号',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
        }
        , {
            text: '等级',
            width: 110,
            sortable: true,
            dataIndex: 'VipLevel',
            align: 'left'
        }
        , {
            text: '姓名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        }
        , {
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        }
        , {
            text: '电邮',
            width: 110,
            sortable: true,
            dataIndex: 'Email',
            align: 'left'
        }
        , {
            text: '微信',
            width: 110,
            sortable: true,
            dataIndex: 'WeiXin',
            align: 'left'
        }
        , {
            text: '积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
        }
        , {
            text: '购买金额',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseAmount',
            align: 'left'
        }
        , {
            text: '购买频次',
            width: 110,
            sortable: true,
            dataIndex: 'PurchaseCount',
            align: 'left'
        }
        , {
            text: '性别',
            width: 110,
            sortable: true,
            dataIndex: 'GenderInfo',
            align: 'left'
        }
        ]
    });

}