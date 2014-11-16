Ext.define("ContorlMarketBrandEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"BrandID",
        type: "string"
    }, {
        name: "value",
        mapping:"BrandID",
        type: "string"
    }, {
        name: "text",
        mapping:"BrandName",
        type: "string"
    }]
});

// MarketBrand 业务控件
Ext.define('jit.biz.MarketBrand', {
    alias: 'widget.jitbizmarketbrand',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlMarketBrandEntity",
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
            args.dataType = "MarketBrand";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/MarketBrandHandler.ashx?method=" + args.dataType;
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
                params: {  }
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