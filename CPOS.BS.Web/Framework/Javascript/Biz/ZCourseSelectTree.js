
// ZCourseSelectTree 业务控件
Ext.define('Jit.Biz.ZCourseSelectTree', {
    alias: 'widget.jitbizzcourseselecttree',
    constructor: function (args) {
        var instance = '';
        if (args.parentId == null) {
            args = Ext.applyIf(args, { parentId: "" });
        }
        args.ApplicationId = getUrlParam("ApplicationId");
        if (args.ApplicationId == undefined || args.ApplicationId == null) {
            args.ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
        }
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/ZCourseSelectTreeHandler.ashx?method=&parent_id=" + args.parentId,
            method: 'GET', 
            async: false,
            success: function (response) {
                defaultConfig = {
                    url: '/Framework/Javascript/Biz/Handler/ZCourseSelectTreeHandler.ashx?method=&parent_id=' + args.parentId
                        + "&ApplicationId=" + args.ApplicationId
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
                        ,listeners : {
                            select: function(combo, record, index) {
                                if (args.fn != null) args.fn(record);
                            }
                        }
                    }
                }
                args = Ext.applyIf(args, defaultConfig);

                instance = Ext.create('Jit.form.field.ComboTree', args);
            }
        });
        return instance;
    }
})
