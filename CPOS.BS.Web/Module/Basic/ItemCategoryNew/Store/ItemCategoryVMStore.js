function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "itemCategoryStore",
        model: "ItemCategoryViewEntity",
        proxy: {
            type: 'ajax'
        },
        reader: {
            type:'json'
        }
    });
    Ext.create('Ext.data.TreeStore', {
        proxy: {
            type: 'ajax'
            , url: 'Handler/ItemCategoryTreeHandler.ashx'
        }
        , listeners: {
            beforeload: {
                fn: function (store, operation, options) {
                    operation.params.multiSelect = false;
                    operation.params.isSelectLeafOnly = false;    //告诉后台是否为只选择叶子节点
                }
            }
        }
        , autoLoad:false
        , storeId: 'itemCategoryTreeStore'//这个是用于右边的树面板的。
    });
}