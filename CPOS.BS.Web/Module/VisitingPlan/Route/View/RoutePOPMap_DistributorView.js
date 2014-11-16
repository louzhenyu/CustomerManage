function InitView() {
    Ext.create('Jit.button.Button', {
        renderTo: 'tab2_span_set',
        imgName: 'route',
        isImgFirst: true,
        handler: fnRouteAdjust
    });

    Ext.create('Jit.button.Button', {
        renderTo: 'tab2_span_save',
        imgName: 'save',
        id: "btnSave",
        handler: fnSave
    });

    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel', {
        jitSize: "big",
        height: 430,
        pageSize: 300,
        pageIndex: 1,
        CheckMode: 'MULTI',
        KeyName: 'MappingID', //这个是判断是否选中的数据
        KeyName: "DistributorID", //这个值是添加到数据库中数据
        BtnCode: btncode,
        CorrelationValue: id,
        ajaxPath: JITPage.HandlerUrl.getValue(),
        isHaveCheckMode: false,
        gridCallBack: fnInitMap
    });

    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel', {
        margin: '10 0 0 0',
        layout: 'column',
        border: 0,
        width: 910,
        grid: gridStoreList,
        BtnCode: btncode,
        renderTo: 'dvSearch',
        ajaxPath: JITPage.HandlerUrl.getValue()
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("storeStore"),
        id: "gridView",
        border: 0,
        columnLines: true,
        columns: [{
            text: '顺序',
            width: 40,
            sortable: true,
            dataIndex: 'Sequence',
            align: 'left'
        }, {
            text: '门店名',
            width: 155,
            sortable: true,
            dataIndex: 'Distributor',
            align: 'left'
        }],
        tbar: [{
            xtype: 'button',
            text: "上升",
            height: 22,
            margin: '0 0 0 10',
            width: 40,
            handler: fnUpUpdate
        }, {
            xtype: 'button',
            text: "下降",
            height: 22,
            margin: '0 0 0 10',
            width: 40,
            handler: fnDownUpdate
        }, { xtype: 'button',
            text: "删除",
            height: 22,
            margin: '0 0 0 10',
            width: 40,
            handler: fnDeleteUpdate
        }],
        height: 500,
        stripeRows: true,
        width: "100%",
        renderTo: "dvGrid",
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        selModel: {
            mode: 'MULTI'
        },
        viewConfig: {
            plugins: {
                ptype: 'gridviewdragdrop',
                dragGroup: 'firstGridDDGroup1',
                dropGroup: 'firstGridDDGroup1'
            },
            listeners: {
                drop: function (node, data, dropRec, dropPosition) {
                    var dropOn = dropRec ? ' ' + dropPosition + ' '
									+ dropRec.get('name') : ' on empty view';
                    renderLine();
                }
            }
        }
    });
}