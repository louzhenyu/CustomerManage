Ext.define('Jit.button.Button', {
    extend: 'Ext.button.Button'
    , alias: 'widget.jitbutton'
    , config: {
        /*
        @size   尺寸分为small,big两种
        */
        jitSize: 'small'
        /*
        @isHighlight    是否为高亮
        */
        , jitIsHighlight: false
        , jitIsDefaultCSS: false
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            height: 25,
            margin: '0 0 0 10'
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
            , jitIsHighlight: false
            , jitIsDefaultCSS: false
        });

        if (cfg.jitIsDefaultCSS != null && cfg.jitIsDefaultCSS) {
            if (cfg.text != null && cfg.text != "") {
                cfg.buttonText = cfg.text.toString();
            }
            cfg.padding = '0 0 0 0';
            if (cfg.disabled != null && cfg.disabled) {
                if (cfg.jitSize.toString().toLowerCase() == "big")
                    cfg.cls = 'buttondisabled_big';
                else
                    cfg.cls = 'buttondisabled';
                cfg.text = '<font style="color:#000000;font-weight:bold">' + cfg.buttonText + '</font>';
            } else {
                if (cfg.jitIsHighlight) {
                    if (cfg.jitSize.toString().toLowerCase() == "big")
                        cfg.cls = 'buttonhighlight_big';
                    else
                        cfg.cls = 'buttonhighlight';
                    cfg.text = '<font style="color:#ffffff;font-weight:bold">' + cfg.buttonText + '</font>';
                } else {
                    if (cfg.jitSize.toString().toLowerCase() == "big")
                        cfg.cls = 'buttoncurrent_big';
                    else
                        cfg.cls = 'buttoncurrent'; 
                    cfg.text = '<font style="color:#484947;font-weight:bold">' + cfg.buttonText + '</font>';
                }
            } 
            if (cfg.jitSize.toString().toLowerCase() == "big")
                cfg.disabledCls = 'buttondisabled_big';
            else
                cfg.disabledCls = 'buttondisabled';
        }
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 80;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 100;
                    }
                    break;
            }
        }
        cfg.setDisabled = function (disabled) {
            if (cfg.jitIsDefaultCSS != null && cfg.jitIsDefaultCSS) {
                if (disabled) {
                    this.setText('<font style="color:#000000;font-weight:bold">' + cfg.buttonText + '</font>');
                    this.removeCls(cfg.cls);
                } else {
                    if (cfg.jitIsHighlight != null && cfg.jitIsHighlight) {
                        if (cfg.jitSize.toString().toLowerCase() == "big")
                            cfg.cls = 'buttonhighlight_big';
                        else
                            cfg.cls = 'buttonhighlight';
                        this.setText('<font style="color:#ffffff;font-weight:bold">' + cfg.buttonText + '</font>');
                    } else {
                        if (cfg.jitSize.toString().toLowerCase() == "big")
                            cfg.cls = 'buttoncurrent_big';
                        else
                            cfg.cls = 'buttoncurrent'; 
                        this.setText('<font style="color:#484947;font-weight:bold">' + cfg.buttonText + '</font>');
                    }
                    this.addCls(cfg.cls);
                }
            }
            return this[disabled ? 'disable' : 'enable']();
        }
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
});