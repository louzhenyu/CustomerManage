Ext.define("ContorlSkuSelectEntity", {
    extend: "Ext.data.Model",
    fields: [
        {name:"id", mapping:"sku_id"}, 
        {name:"value", mapping:"sku_id"},
        {name:"text", mapping:"item_name"},
        {name:"sku_id", mapping:"sku_id"}, 
        {name:"item_name", mapping:"item_name"},
        {name:"item_id", mapping:"item_id"}, 
        {name:"item_code", mapping:"item_code"},
        {name:"prop_1_detail_id", mapping:"prop_1_detail_id"}, 
        {name:"prop_1_detail_code", mapping:"prop_1_detail_code"}, 
        {name:"prop_1_detail_name", mapping:"prop_1_detail_name"}, 
        {name:"prop_2_detail_id", mapping:"prop_2_detail_id"}, 
        {name:"prop_2_detail_code", mapping:"prop_2_detail_code"}, 
        {name:"prop_2_detail_name", mapping:"prop_2_detail_name"}, 
        {name:"prop_3_detail_id", mapping:"prop_3_detail_id"}, 
        {name:"prop_3_detail_code", mapping:"prop_3_detail_code"}, 
        {name:"prop_3_detail_name", mapping:"prop_3_detail_name"}, 
        {name:"prop_4_detail_id", mapping:"prop_4_detail_id"}, 
        {name:"prop_4_detail_code", mapping:"prop_4_detail_code"}, 
        {name:"prop_4_detail_name", mapping:"prop_4_detail_name"}, 
        {name:"prop_5_detail_id", mapping:"prop_5_detail_id"}, 
        {name:"prop_5_detail_code", mapping:"prop_5_detail_code"}, 
        {name:"prop_5_detail_name", mapping:"prop_5_detail_name"}, 
        {name:"prop_1_id", mapping:"prop_1_id"}, 
        {name:"prop_1_code", mapping:"prop_1_code"}, 
        {name:"prop_1_name", mapping:"prop_1_name"}, 
        {name:"prop_2_id", mapping:"prop_2_id"}, 
        {name:"prop_2_code", mapping:"prop_2_code"}, 
        {name:"prop_2_name", mapping:"prop_2_name"}, 
        {name:"prop_3_id", mapping:"prop_3_id"}, 
        {name:"prop_3_code", mapping:"prop_3_code"}, 
        {name:"prop_3_name", mapping:"prop_3_name"}, 
        {name:"prop_4_id", mapping:"prop_4_id"}, 
        {name:"prop_4_code", mapping:"prop_4_code"}, 
        {name:"prop_4_name", mapping:"prop_4_name"}, 
        {name:"prop_5_id", mapping:"prop_5_id"}, 
        {name:"prop_5_code", mapping:"prop_5_code"}, 
        {name:"prop_5_name", mapping:"prop_5_name"}, 
        {name:"barcode", mapping:"barcode"}, 
        {name:"status", mapping:"status"}, 
        {name:"display_name", mapping:"display_name"}, 
        {name:"create_time", mapping:"create_time"}, 
        {name:"create_user_id", mapping:"create_user_id"}, 
        {name:"modify_time", mapping:"modify_time"}, 
        {name:"modify_user_id", mapping:"modify_user_id"}
    ]
});

// SkuSelect 业务控件
Ext.define('jit.biz.SkuSelect', {
    alias: 'widget.jitbizskuselect',
    constructor: function (args) {
        
        var _ctrl, _store;
        
        _store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlSkuSelectEntity",
            proxy: new Ext.data.HttpProxy({
                type: 'ajax',
                url: "/Framework/Javascript/Biz/Handler/SkuSelectHandler.ashx?method=sku",
                headers: { 'Content-type': 'application/json' },
                reader: {
                    type: 'json',
                    root: 'data'
                }
            })
        });
        var defaultConfig = {
            multiSelect: false
            , labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
//            , queryMode: 'local'
            , margin: '0 10 10 10'
            //, editable: false
            , width: 183
            , labelWidth: 73
            , height: 22
            , matchFieldWidth: false
            , listConfig: {
//                minWidth: 102
//                , maxWidth: 300
//                , cls: 'ComboBox'
                loadingText: '正在加载数据...'
                , emptyText: '未查找到数据'
                , getInnerTpl: function() {
                    return '<div>{display_name}</div>';
                }
            }
            , store: _store
            , valueField: args.valueKey != undefined && args.valueKey != null ? args.valueKey : 'id'
            , displayField: 'display_name'
            , minChars: 1
            , typeAhead: false
            , hideTrigger: true
            , anchor: '100%'
            , listeners:{
                select: function(combo, record, index) {
                     try {
                         var d = record[0].data;
                         sku_selected_data = d;

                         var hctrl = Ext.getCmp(args.hideId);
                         if (hctrl != null) {
                             hctrl.setValue(this.value);
                         }

                         var tbItemName = Ext.getCmp(args.nameId);
                         if (tbItemName != null) tbItemName.setValue(d.text);
                        
                         if (typeof args.fnCallback == "function") {
                            args.fnCallback(d);
                         }
                         setSkuPropsDisplay(d, args);
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

