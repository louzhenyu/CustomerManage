Ext.define("ContorlAppSysEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Def_App_Id",
        type: "string"
    }, {
        name: "value",
        mapping:"Def_App_Id",
        type: "string"
    }, {
        name: "text",
        mapping:"Def_App_Name",
        type: "string"
    }]
});

// AppSys 业务控件
Ext.define('jit.biz.AppSys', {
    alias: 'widget.jitbizappsys',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlAppSysEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    //if (args.isDefault != null && args.isDefault) {
                        store.insert(0, {
                            "name": args.Name,
                            "value": '',
                            "text": "--请选择--"
                        });
                    //}
                }
            }
        }); 
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "get_app_sys_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/AppSysHandler.ashx?method=" + args.dataType;
        store.load({
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        args.listeners = {
            select: function(combo, record, index) {
                if (args.selectFn != undefined) args.selectFn();
            }
        };
        
        var result= Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function() {
            store.load({
                params: {  }
            });
        };

        return result;
  }
})