function InitView() {
    btnAdd = Ext.create('Jit.button.Button', {
        renderTo: 'dvWork',
        imgName: 'save',
        id: "btnSave",
        isImgFirst: true,
        handler: fnSave
    });

    cellEditing = Ext.create('Ext.grid.plugin.CellEditing', {
        clicksToEdit: 1,
        listeners: {
            "beforeedit": function (a, b, c) {
                if (checkStore.getById(b.record.data.StoreID) != null && checkStore.getById(b.record.data.StoreID).data.IsDelete == 0)
                    return true;
                else
                    return false;
            },
            "edit": function (a, b, c) {
                var r = checkStore.getById(b.record.data.StoreID);
                var r1 = null;
                for (var i = 0; i < gridStoreList.getStore().data.items.length; i++) {
                    if (b.record.data.StoreID == gridStoreList.getStore().data.items[i].data.StoreID) {
                        r1 = gridStoreList.getStore().data.items[i];
                    }
                }
                if (r != null && r1 != null) {
                    r.data.Sequence = b.record.data.Sequence;
                    r1.data.Sequence = b.record.data.Sequence;
                }
                // checkStore.insert(0, b.record.data);
                else
                    return false;

            }
        }
    });

    var otherColumns = new Array();
    otherColumns.push({
        text: '顺序',
        width: 50,
        sortable: false,
        dataIndex: "Sequence",
        editor: {
            allowBlank: false,
            vtype: "number"
        }
    });

    Ext.define("SelModel", {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'MappingID', type: 'string' },
            { name: 'StoreID', type: 'string' },
            { name: 'Sequence', type: 'int' },
            { name: 'IsDelete', type: 'int' }
        ],
        idProperty: 'StoreID'
    });

    checkStore = Ext.create("Ext.data.Store", { model: 'SelModel' });

    selModelOrder = Ext.create('Ext.selection.CheckboxModel',
                                {
                                    singleSelect: false, checkOnly: true,
                                    listeners: {
                                        'deselect': function (a, b, c) {
                                            var r = checkStore.getById(b.data.StoreID);
                                            //checkStore.remove(r);
                                            r.data.IsDelete = 1;
                                            //r.data.Sequence = 0;
                                        },
                                        'select': function (a, b, c) {
                                            if (a.checkOnly == true) {
                                                if (checkStore.getById(b.data.StoreID) == null) {
                                                    checkStore.insert(0, b.data);
                                                } else {
                                                    if (checkStore.getById(b.data.StoreID).data.IsDelete = 1) {
                                                        checkStore.getById(b.data.StoreID).data.IsDelete = 0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                });

    // 数据操作
    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel',
                    {
                        jitSize: "big",
                        renderTo: 'dvGrid',
                        height: 430,
                        pageSize: 15,
                        pageIndex: 1,
                        CheckMode: 'MULTI',
                        KeyName: 'MappingID', //这个是判断是否选中的数据
                        KeyName: "StoreID", //这个值是添加到数据库中数据
                        BtnCode: btncode,
                        CorrelationValue: id,
                        ajaxPath: JITPage.HandlerUrl.getValue(),
                        isHaveCheckMode: false,
                        otherGridColumns: otherColumns,
                        plugins: [cellEditing],
                        selModel: selModelOrder,
                        gridCallBack: fnSearch

                    });

    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel',
{
    margin: '10 0 0 0',
    layout: 'column',
    border: 0,
    width: 910,
    grid: gridStoreList,
    BtnCode: btncode,
    renderTo: 'dvSearch',
    ajaxPath: JITPage.HandlerUrl.getValue()
});
}