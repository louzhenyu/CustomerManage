function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "eventsRoundListStore",
        model: "RoundViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: {
                read: 'POST'
            }
        }
    });
    Ext.create('Ext.data.Store', {
        storeId: "RoundPrizesStore",
        model: "RoundPrizesViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: {
                read: 'POST'
            }
        }
    });
    Ext.create('Ext.data.Store', {
        model: 'StatusEntity',
        storeId: "StatusStore",
        data: [
                { Id: '', Name: '--请选择---' },
                { Id: '10', Name: '未开始' },
                { Id: '20', Name: '运行中' },
                { Id: '30', Name: '暂停' },
                { Id: '40', Name: '结束' }
               ]
    });
}