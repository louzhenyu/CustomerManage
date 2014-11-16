function InitStore() {

    new Ext.data.Store({
        storeId: "parameterStore",
        model: "VisitingParameterViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                id: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}

var pagedData = new Array();
function InitStoreMemory(records) { 
    for (var i = 0; i < records.length; i++) {
        pagedData.push(records[i].data);
    }
    Ext.create('Jit.data.PagingMemoryStore', {
        model: 'VisitingParameterViewEntity',
        storeId: "parameterStoreMemory",
        remoteSort: true,
        pageSize: JITPage.PageSize.getValue(),
        data: pagedData,
        proxy: {
            type: 'pagingmemory'
        }
    });
}