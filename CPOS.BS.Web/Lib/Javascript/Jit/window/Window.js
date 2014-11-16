Ext.define('Jit.window.Window', {
    extend: 'Ext.window.Window'
    , alias: 'widget.jitwindow'
    , config: {
        /*
        @size   尺寸有small,big,large
        */
        jitSize: 'small'
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            closeable: true
            , closeAction: 'hide'
            , modal: 'true'
            , resizable: false
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var jitSize = cfg.jitSize.toString().toLowerCase();
            switch (jitSize) {
                case 'small':
                    {
                        defaultConfig.width = 300;
                        defaultConfig.height = 150;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 680;
                        defaultConfig.height = 250;
                    }
                    break;
                case 'large':
                    {
                        defaultConfig.width = 900;
                        defaultConfig.height = 400;
                    }
                    break;
            }
        }
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
});