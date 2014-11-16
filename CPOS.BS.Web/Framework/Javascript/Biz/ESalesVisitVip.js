Ext.define("ContorlESalesVisitVipEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"VipId",
        type: "string"
    }, {
        name: "value",
        mapping:"VipId",
        type: "string"
    }, {
        name: "text",
        mapping:"VipName",
        type: "string"
    }]
});

// ZCourse 业务控件
Ext.define('jit.biz.ESalesVisitVip', {
    alias: 'widget.jitbizesalesvisitvip',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlESalesVisitVipEntity",
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
                    if (!args.multiSelect)
                        store.insert(0, {
                            "name": args.Name,
                            "value": '',
                            "text": "--请选择--"
                        });
                    //}
                }
                //expand: function(combo, record, index) {
                //     try {
                //         store.load({
                //            params: {  }
                //         });
                //     }
                //     catch (ex) {
                //         Ext.MessageBox.alert("错误", "错误:" + ex);
                //     }
                //}
            }
        }); 
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "get_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/ESalesVisitVipHandler.ashx?method=" + args.dataType;
        store.load({
            //limit: 10,
            //page: 0
            params: {  }
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);
        
        //args.listeners = {
        //    expand: function(combo, record, index) {
        //        Ext.getCmp(args.id).jitSetValue("");
        //        try {
        //            store.load({
        //                params: {  }
        //            });
        //        }
        //        catch (ex) {
        //            Ext.MessageBox.alert("错误", "错误:" + ex);
        //        }
        //    }
        //};

        var result= Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function(fn) {
            store.load({
                params: {  }
                ,callback: function(r, options, success) {
                    //alert("123");
                    if (fn != undefined && fn != null) {
                        fn();
                    }
                }
            });
        };
        result.setDefaultValue = function(defValue) {
            store.load({
                params: { }
                ,callback: function(r, options, success) {
                    for (var i = 0; i < r.length; i++) {
                        var rawValue = r[i].data.id;
                        if (rawValue == defValue) {
                            result.setValue(rawValue);
                        }
                    }
                }
            });
        };

        return result;
  }
})