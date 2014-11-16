Ext.define("ContorlVipCardExtensionYearEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Id",
        type: "string"
    }, {
        name: "value",
        mapping:"Id",
        type: "string"
    }, {
        name: "text",
        mapping:"Description",
        type: "string"
    }]
});

// OrderStatus 业务控件
Ext.define('jit.biz.VipCardExtensionYear', {
    alias: 'widget.jitbizvipcardextensionyear',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlVipCardExtensionYearEntity",
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
                },
                select: function() {
                    alert("123");
                }
            }
        }); 
        
        store.proxy.url = "/Framework/Javascript/Biz/Handler/VipCardExtensionYearHandler.ashx?method=" + args.orderType;
        store.load({
            //limit: 10,
            //page: 0
        });
        //defaultConfig = {
        //    store: store
        //    , valueField: 'value'
        //    , displayField: 'text'
        //};

        var defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
            , listeners:{
                select: function(combo, record, index) {
                     try {
                         var d = record[0].data;
                         if (typeof args.fnCallback == "function") {
                            args.fnCallback(d);
                         }
                     }
                     catch (ex) {
                         Ext.MessageBox.alert("错误", "错误:" + ex);
                     }
                }
            }
        }
        args = Ext.applyIf(args, defaultConfig);
        _ctrl = Ext.create('Ext.form.field.ComboBox', args);
        return _ctrl;
  }
})