Ext.define("ContorlCarModelsEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping: "CarModelsID",
        type: "string"
    }, {
        name: "value",
        mapping: "CarModelsID",
        type: "string"
    }, {
        name: "text",
        mapping: "CarModelsName",
        type: "string"
    }]
});

// CarModels 业务控件
Ext.define('jit.biz.CarModels', {
    alias: 'widget.jitbizcarmodels',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlCarModelsEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    store.insert(0, {
                        "name": args.Name,
                        "value": '',
                        "text": "--请选择--"
                    });
                },
                expand: function (combo, record, index) {
                    try {
                        store.load({
                            params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
                        });
                    }
                    catch (ex) {
                        Ext.MessageBox.alert("错误", "错误:" + ex);
                    }
                }
            }
        });
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "CarModels";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/CarModelsHandler.ashx?method=" + args.dataType;
        store.load({
            //limit: 10,
            //page: 0,
            params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        args.listeners = {
            expand: function (combo, record, index) {
                //alert(Ext.getCmp(args.parent_id).jitGetValue());
                try {
                    store.load({
                        params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
                    });
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        };
        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function () {
            store.load({
                params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
            });
        };

        return result;
    }
})