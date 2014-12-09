
// ItemCategorySelectTree 业务控件
Ext.define('Jit.Biz.ItemCategorySelectTree', {
    alias: 'widget.jitbizitemcategoryselecttree',
    constructor: function (args) {
        var instance = '';
        if (args.parentId == null) {
            args = Ext.applyIf(args, { parentId: null });
        }

        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/ItemCategorySelectTreeHandler.ashx?method=item_category&parent_id=" + args.parentId,
            method: 'GET',
            async: false,
            success: function (response) {

                var rootID = '';
                var rootText = '';
                if (args.setRootAsDefault == true) {
                    //root
                    var category = Ext.JSON.decode(response.responseText)[0];
                    rootID = category.id;
                    rootText = category.text;

                }
             //   alert("s");
                //是否只能选择叶子节点。
                var isSelectLeafOnly = args.isSelectLeafOnly;
                if (!isSelectLeafOnly) isSelectLeafOnly = false;
                defaultConfig = {
                    url: '/Framework/Javascript/Biz/Handler/ItemCategorySelectTreeHandler.ashx?method=item_category&parent_id=' + args.parentId   //树的数据从后台加载
                    ,
                    multiSelect: false                 //树是否为多选
                    , rootText: rootText                  //树的根节点的文本
                    , rootID: rootID                      //树的根节点的值
                    , isSelectLeafOnly: isSelectLeafOnly           //只能选择树的叶子节点
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
