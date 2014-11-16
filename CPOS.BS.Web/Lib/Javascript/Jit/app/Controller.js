Ext.define('Jit.app.Controller', {
    extend: 'Ext.app.Controller'
    , config: {
        /*
        @view   当前的视图
        */
        view: null
        /*
        @menuID 当前页面的菜单ID
        */
        ,menuID:null
        /*
        @actions 当前view所支持的所有actions
        */
        , actions: null
    }
    , constructor: function (cfg) {
        //定义默认配置项
        var defaultConfig = {
            view: null
            , menuID: null
            , actions: null
        };
        //合并配置项
        Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    , init: function (application) {
        //将application的参数拷贝到controller中
        Ext.applyIf(this, application.params || {});
        //检查是否配置了参数
        if (this.ajaxHandlerPath == null) {
            Ext.Error.raise("在application中必须配置ajaxHandlerPath.");
        }
        if (this.menuID == null) {
            Ext.Error.raise("在application中必须配置menuID.");
        }
    }
    /*
    工具方法 - 根据键获取多语言资源
    @key    多语言资源项的键
    返回多语言资源项的值
    */
    , getResource: function (key) {
        if (key != undefined) {
            for (var i = 0; i < this.languageResources.length; i++) {
                if (this.languageResources[i].key == key) {
                    return this.languageResources[i].value;
                }
            }
            return 'ERROR';
        }
    }
    /*
    工具方法 - 根据键获取多语言资源
    @view   视图
    @action 操作
    返回ajax请求的url
    */
    , getAjaxUrl: function (view, action) {
        //处理view&action
        var current = {
            view: view
            , action: action
        };
        current = Ext.applyIf(current, {
            view: this.view
            , action: this.action
        });
        //拼接查询字符串
        var url = this.ajaxHandlerPath + '?';
        url += '&menu=' + (this.menu == null ? '' : this.menu);
        url += '&view=' + (current.view == null ? '' : current.view.toString());
        url += '&action=' + (current.action == null ? '' : current.action.toString());
        //
        return url;
    }
});