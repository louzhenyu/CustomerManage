
// MenuSelectTree 业务控件
Ext.define('Jit.Biz.MenuSelectTree', {
    alias: 'widget.jitbizmenuselecttree',
    constructor: function (args) {
        var instance = '';
        
        if (args.parentId == null) {
            args = Ext.applyIf(args, { parentId: "" });
        }
        if (args.reg_app_id == null) {
            var app = Ext.getCmp("txtAppSys");
            var app_id = "";
            if (app != null) {
                app_id = app.jitGetValue();
                if (app_id == undefined) {
                    app_id = "";
                }
            }
            args = Ext.applyIf(args, { reg_app_id: app_id });
        }
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/MenuSelectTreeHandler.ashx?method=get_menu_tree&parent_id=" + args.parentId + "&reg_app_id=" + args.reg_app_id,
            method: 'GET',
            async: false,
            success: function (response) {
                defaultConfig = {
                    url: '/Framework/Javascript/Biz/Handler/MenuSelectTreeHandler.ashx?method=get_menu_tree&parent_id=' + args.parentId + '&reg_app_id=' + args.reg_app_id   //树的数据从后台加载
                    , multiSelect: false                 //树是否为多选
                    , rootText: ''                  //树的根节点的文本
                    , rootID: ''                      //树的根节点的值
                    , isSelectLeafOnly: false           //只能选择树的叶子节点
                    , isRootVisible: false
                    , emptyText: '--请选择--'
                    , isAddPleaseSelectItem: true
                    , pleaseSelectText: '--请选择--'
                    , pickerCfg: {
                        minHeight: 230
                        , maxHeight: 230
                    }
                }
                args = Ext.applyIf(args, defaultConfig);

                instance = Ext.create('Jit.form.field.ComboTree', args);
            }
        });
        return instance;
    }
})
