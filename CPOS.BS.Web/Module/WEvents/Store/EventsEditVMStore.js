function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "eventsEditStore",
        model: "EventsViewEntity",
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
    new Ext.data.Store({
        storeId: "drawMethodStore",
        model: "DrawMethodEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'data'
            }
        }
    });

  


    new Ext.data.Store({
        storeId: "Module1Store",
        model: "ModuleEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'data'
            }
        }
    });
    new Ext.data.Store({
        storeId: "Module2Store",
        model: "ModuleEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'data'
            }
        }
    });

    new Ext.data.Store({
        storeId: "PersonCountStore",
        model: "ModelEntity",
        data: [
                { Id: '2', Name: '每天一次' },
                { Id: '1', Name: '仅一次' },
                { Id: '-1', Name: '无' }
        ]
    });

    Ext.create('Ext.data.Store', {
        model: 'ModelEntity',
        storeId: "ModelStore",
        data: [
                { Id: '1', Name: '文本' },
                { Id: '2', Name: '图文' }
               ]
    });
    Ext.create('Ext.data.Store', {
        model: 'FlagEntity',
        storeId: "FlagStore",
        data: [
                { Id: '1', Name: '是否需要注册' },
                { Id: '2', Name: '是否需要签到' },
                { Id: '3', Name: '是否需要身份验证' },
                { Id: '4', Name: '是否判断在现场' },
               ]
    });
}