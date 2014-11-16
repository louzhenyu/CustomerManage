//针对按钮特殊处理
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
        if (cfg.text != null && cfg.text != "") {
            cfg.buttonText = cfg.text.toString();
        }
        if (cfg.imgName != null) {
            cfg.jitIsDefaultCSS = true;
            var icons = {
                'search': '查&nbsp;&nbsp;询',
                'reset': '重&nbsp;&nbsp;置',
                'create': '添&nbsp;&nbsp;加',
                'appoint': '指&nbsp;&nbsp;派',
                'approve': '审&nbsp;&nbsp;批',
                'back': '返&nbsp;&nbsp;回',
                'cancel': '关&nbsp;&nbsp;闭',
                'clear': '清除选择',
                'createsku': '添加产品',
                'delete': '删&nbsp;&nbsp;除',
                'ok': '确&nbsp;&nbsp;定',
                'resetroute': '重新配线',
                'route': '优化路线',
                'save': '保&nbsp;&nbsp;存',
                'searchposition': '搜索位置',
                'stockback': '备&nbsp;货&nbsp;中',
                'stockfinsh': '备货完成',
                'update': '修&nbsp;&nbsp;改',
                'open': '',
                'close': ''
            }
            cfg.buttonText = icons[cfg.imgName];
        }
        if (cfg.isImgFirst != null) {
            cfg.jitIsHighlight = cfg.isImgFirst;
        }
        if (cfg.jitIsDefaultCSS != null && cfg.jitIsDefaultCSS) {
            cfg.padding = '0 0 0 0';
            cfg.border = 0;
            cfg.height = 25;
            if (cfg.disabled != null && cfg.disabled) {
                cfg.cls = 'buttondisabled';
                cfg.text = '<font style="color:#000000;font-weight:bold">' + cfg.buttonText + '</font>';
            } else {
                if (cfg.jitIsHighlight) {
                    cfg.cls = 'buttonhighlight';
                    cfg.text = '<font style="color:#ffffff;font-weight:bold">' + cfg.buttonText + '</font>';
                } else {
                    cfg.cls = 'buttoncurrent';
                    cfg.text = '<font style="color:#484947;font-weight:bold">' + cfg.buttonText + '</font>';
                }
            }
            cfg.disabledCls = 'buttondisabled';
        }
        if (cfg.imgName != null && cfg.imgName == "select") {
            cfg.cls = 'selecthighlight';
            cfg.text = "";
            cfg.buttonText = "";
            cfg.height = 22;
            cfg.border = 0;
            cfg.padding = '0 0 0 0';
        }
        if (cfg.imgName != null && cfg.imgName == "open") {
            cfg.cls = 'opentwo';
            cfg.text = "&nbsp;&nbsp;&nbsp;&nbsp;更&nbsp;&nbsp;多</font>";
            cfg.buttonText = "";
            cfg.height = 22;
            cfg.border = 0;
            cfg.padding = '0 0 0 0';
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
                        cfg.cls = 'buttonhighlight';
                        this.setText('<font style="color:#ffffff;font-weight:bold">' + cfg.buttonText + '</font>');
                    } else {
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
