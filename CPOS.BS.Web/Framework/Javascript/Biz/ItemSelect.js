Ext.define("ContorlItemSelectEntity", {
    extend: "Ext.data.Model",
    fields: [
        {name:"id", mapping:"Item_Id"}, 
        {name:"value", mapping:"Item_Id"},
        {name:"text", mapping:"Item_Name"},
        {name:"sku_id", mapping:"Item_Id"}, 
        {name:"item_name", mapping:"Item_Name"},
        {name:"item_id", mapping:"Item_Id"}, 
        {name:"item_code", mapping:"Item_Code"},
        {name:"display_name", mapping:"Item_Name"}
    ]
});

// ItemSelect 业务控件
Ext.define('jit.biz.ItemSelect', {
    alias: 'widget.jitbizitemselect',
    constructor: function (args) {
        
        var _ctrl, _store;
        
        _store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlItemSelectEntity",
            proxy: new Ext.data.HttpProxy({
                type: 'ajax',
                url: "/Framework/Javascript/Biz/Handler/ItemSelectHandler.ashx?method=sku",
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
                         //setSkuPropsDisplay(d, args);
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

