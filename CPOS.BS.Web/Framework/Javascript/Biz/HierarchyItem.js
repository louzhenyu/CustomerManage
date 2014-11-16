Ext.define("ContorlHierarchyItemEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "HierarchyItemID",
        type: "string"
    }, {
        name: "ItemName",
        type: "string"
    }]
});

//HierarchyItem 层系控件
Ext.define('Jit.biz.HierarchyItem', {
    alias: 'widget.jitbizhierarchyitem',
    constructor: function (args) {
        var instance = '';
        var argsConfig = { parentID: "", isTree: true, emptyText: '--请选择--' };
        args = Ext.applyIf(args, argsConfig);
        if (args.isTree) {
            defaultConfig = {
                url: '/Framework/Javascript/Biz/Handler/HierarchyItemHandler.ashx?method=GetHierarchyItemByParentID&IsTree=success&ParentID=' + args.parentID + "&HierarchID=" + args.HierarchID   //树的数据从后台加载
                        , multiSelect: false                 //树是否为多选
                        , rootText: '层系'                  //树的根节点的文本
                        , rootID: '-1'                      //树的根节点的值
                        , isSelectLeafOnly: false           //只能选择树的叶子节点
            }
            args = Ext.applyIf(args, defaultConfig);
            instance = Ext.create('Jit.form.field.ComboTree', args);
            instance.jitSetValue = function (value) {
                try {
                    if (value != null && value.toString() != "") {
                        var values = "'" + value.toString().replace(/,/g, "','") + "'";
                        Ext.Ajax.request({
                            url: "/Framework/Javascript/Biz/Handler/HierarchyItemHandler.ashx?method=GetHierarchyItemByID&HierarchyItemID="
					        + values,
                            method: 'get',
                            success: function (response) {
                                if (response.responseText != "") {
                                    instance.jitSetValueText(value.toString().toLowerCase(), response.responseText);
                                }
                            }
                        });
                    } else {
                        instance.jitSetValueText("", "");
                    }
                } catch (e) {

                }
            }
        } else {
            var store = new Ext.data.Store({
                storeId: args.storeId,
                pageSize: args.pageSize || 15,
                model: "ContorlHierarchyItemEntity",
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
                                "HierarchyItemID": "",
                                "ItemName": "--请选择--"
                            });
                        }
                    }
                }
            });
            store.proxy.url = "/Framework/Javascript/Biz/Handler/HierarchyItemHandler.ashx?method=GetHierarchyItemByParentID&IsTree=false&ParentID=" + args.parentID + "&HierarchID=" + args.HierarchID;
            store.load({
                limit: 1,
                page: 1
            });
            defaultConfig = {
                store: store
                        , valueField: 'HierarchyItemID'
                        , displayField: 'ItemName'
            };
            args = Ext.applyIf(args, defaultConfig);
            instance = Ext.create('Jit.form.field.ComboBox', args);
        }
        return instance;
    }
})

