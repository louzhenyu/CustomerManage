/*
动态面板
*/
Ext.define('Jit.panel.DynamicPanel', {
    extend: 'Ext.panel.Panel'
    , alias: ['widget.jitdynamicpanel','widget.jitdpanel']
    , config: {
        /*
        获取动态控件配置的url
        $required
        */
        url:null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            closeable: true
            , closeAction: 'hide'
            , modal: 'true'
            , resizable: false
        };
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
});