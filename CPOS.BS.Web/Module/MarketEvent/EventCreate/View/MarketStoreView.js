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

    Ext.create('Jit.button.Button', {
        text: '门店选取',
        renderTo: 'btnSelectStore',
        handler: function(){
            document.getElementById('mapArea').style.display='block';
            document.getElementById('mapAreaClose').style.display='block';
        }
    });

    // gridStore list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("MarketStoreStore"),
        id: "gridStore",
        renderTo: "gridStore",
        columnLines: true,
        height: 459,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("MarketStoreStore"),
            pageSize: JITPage.PageSize.getValue()
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
            dataIndex: 'MarketStoreID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        ,{
            text: '门店号',
            width: 110,
            sortable: true,
            dataIndex: 'StoreCode',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '门店名',
            width: 110,
            sortable: true,
            dataIndex: 'StoreName',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '商区',
            width: 110,
            sortable: true,
            dataIndex: 'BusinessDistrict',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '地址',
            width: 110,
            sortable: true,
            dataIndex: 'Address',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '会员数',
            width: 110,
            sortable: true,
            dataIndex: 'MembersCount',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '年销售额',
            width: 110,
            sortable: true,
            dataIndex: 'SalesYear',
            align: 'left'
            ,flex: true
        }
        ,{
            text: '开业时间',
            width: 110,
            sortable: true,
            dataIndex: 'Opened',
            align: 'left'
            ,flex: true
        }
        ]
    });

}