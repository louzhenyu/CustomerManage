Ext.define("ContorlBrandEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "BrandID",
        type: "int"
    }, {
        name: "BrandName",
        type: "string"
    }]
});

//Brand 渠道控件
Ext.define('Jit.Biz.Brand', {
    alias: 'widget.jitbizbrand',
    constructor: function (args) {
        var instance = '';
        if (args.ParentID == null) {
            args = Ext.applyIf(args, { ParentID: "" });
        }
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/BrandHandler.ashx?method=GetBrandTree&ParentID="
					+ args.ParentID,
            method: 'get',
            async: false,
            success: function (response) {
                if (response.responseText != "" && response.responseText == "success") {
                    defaultConfig = {
                        url: '/Framework/Javascript/Biz/Handler/BrandHandler.ashx?method=GetBrandByParentID&IsTree=success&ParentID=' + args.ParentID   //树的数据从后台加载
                        , multiSelect: false                 //树是否为多选
                        , rootText: '品牌'                  //树的根节点的文本
                        , rootID: '-1'                      //树的根节点的值
                        , isSelectLeafOnly: false           //只能选择树的叶子节点
                    }
                    args = Ext.applyIf(args, defaultConfig);
                    instance = Ext.create('Jit.form.field.ComboTree', args);
                } else {
                    var store = new Ext.data.Store({
                        storeId: args.storeId,
                        pageSize: args.pageSize || 15,
                        model: "ContorlBrandEntity",
                        proxy: {
                            type: 'ajax',
                            reader: {
                                type: 'json'
                            }
                        },
                        listeners: {
                            load: function (store) {
                                if (args.isDefault != null && args.isDefault) {
                                    store.insert(0, {
                                        "BrandID": 0,
                                        "BrandName": "--请选择--"
                                    });
                                }
                            }
                        }
                    }); 
                    store.proxy.url = "/Framework/Javascript/Biz/Handler/BrandHandler.ashx?method=GetBrandByParentID&IsTree=false&ParentID=" + args.ParentID;
                    store.load({
                        limit: 1,
                        page: 1
                    });
                    defaultConfig = {
                        store: store
                        , valueField: 'BrandID'
                        , displayField: 'BrandName'
                    };
                    args = Ext.applyIf(args, defaultConfig);
                    instance = Ext.create('Jit.form.field.ComboBox', args);
                }
            }
        });
     
        return instance;
    }
})

