Ext.define("ContorlRoleEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Role_Id",
        type: "string"
    }, {
        name: "value",
        mapping:"Role_Id",
        type: "string"
    }, {
        name: "text",
        mapping:"Role_Name",
        type: "string"
    }]
});

// Role 业务控件
Ext.define('jit.biz.Role', {
    alias: 'widget.jitbizrole',
    constructor: function(args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlRoleEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function(store) {
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
        if (args.orderType == undefined || args.orderType == null) {
            args.orderType = "get_role_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/RoleHandler.ashx?method=" + args.orderType;
        store.load({
            
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store,
            valueField: 'value',
            displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        args.listeners = {
            expand: function(combo, record, index) {
                //alert(Ext.getCmp(args.parent_id).jitGetValue());
                try {
                    store.load({
                        params: { app_sys_id: Ext.getCmp(args.parentId).jitGetValue() }
                    });
                } catch(ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        };

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.setDefaultValue = function(defValue) {
            store.load({
                params: { app_sys_id: Ext.getCmp(args.parentId).jitGetValue() },
                callback: function(r, options, success) {
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
});
// Role 业务控件
Ext.define('jit.biz.RolePosition', {
    alias: 'widget.jitbizroleposition',
    constructor: function(args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlRoleEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function(store) {
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
        if (args.orderType == undefined || args.orderType == null) {
            args.orderType = "get_role_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/RoleHandler.ashx?method=" + args.orderType;
        store.load({
            
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store,
            valueField: 'value',
            displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        args.listeners = {
            expand: function(combo, record, index) {
                //alert(Ext.getCmp(args.parent_id).jitGetValue());
                try {
                    store.load({
                        params: { app_sys_id: '7C7CC257927D44BD8CF4F9CD5AC5BDCD' }
                    });
                } catch(ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        };

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.setDefaultValue = function(defValue) {
            store.load({
                params: { app_sys_id: Ext.getCmp(args.parentId).jitGetValue() },
                callback: function(r, options, success) {
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
});

