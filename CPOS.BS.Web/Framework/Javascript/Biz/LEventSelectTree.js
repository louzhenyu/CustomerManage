
// LEventSelectTree 业务控件
Ext.define('Jit.Biz.LEventSelectTree', {
    alias: 'widget.jitbizleventselecttree',
    constructor: function (args) {
        var instance = '';
        if (args.parentId == null) {
            args = Ext.applyIf(args, { parentId: "" });
        }
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/LEventSelectTreeHandler.ashx?method=get_levent_tree&parent_id=" + args.parentId,
            method: 'GET',
            async: false,
            success: function (response) {
                defaultConfig = {
                    url: '/Framework/Javascript/Biz/Handler/LEventSelectTreeHandler.ashx?method=get_levent_tree&parent_id=' + args.parentId   //树的数据从后台加载
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
