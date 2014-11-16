
// PropSelectTree 业务控件
Ext.define('Jit.Biz.PropSelectTree', {
    alias: 'widget.jitbizpropselecttree',
    constructor: function (args) {
        var instance = '';
        if (args.parentId == null) {
            args = Ext.applyIf(args, { parentId: "" });
        }
        if (args.domain == undefined || args.domain == null || args.domain.length == 0) {
            args.domain = getUrlParam("domain");
        }
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/PropSelectTreeHandler.ashx?method=&parent_id=" + args.parentId + "&domain=" + args.domain,
            method: 'GET',
            async: false,
            success: function (response) {
                defaultConfig = {
                    url: '/Framework/Javascript/Biz/Handler/PropSelectTreeHandler.ashx?method=&parent_id=' + args.parentId + "&domain=" + args.domain
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
                        , listeners: {
                            select: function (combo, record, index) {
                                
                                if (args.fn != null) args.fn(record);
                                if (args.id == "txtParentId" && args.renderTo == "txtParentId") {
                                    fnCreateIndex(record.data);
                                }
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
