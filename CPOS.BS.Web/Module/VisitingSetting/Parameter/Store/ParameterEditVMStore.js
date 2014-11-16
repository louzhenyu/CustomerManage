function InitParameterEditStore() {

//gridview
    new Ext.data.Store({
        storeId: "optionsStore",
        pageSize: 15,
        model: "OptionsEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });

    new Ext.data.Store({
        storeId: "parameterStore",
        model: "VisitingParameterEntity"
    });

    new Ext.create('Ext.data.Store', {
        storeId: "scaleStore",
        fields:  [{name:'name',type:"string"}, {name:'value',type:"int"}],
        data: [
        { "name": "1", "value": 1 },
        { "name": "2", "value": 2 },
        { "name": "3", "value": 3 }
    ]
    });

    new Ext.data.Store({
        storeId: "unitStore",
        model: "UnitEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}