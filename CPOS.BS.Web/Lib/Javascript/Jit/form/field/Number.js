Ext.define('Jit.form.field.Number', {
    extend: 'Ext.form.field.Number'
    , alias: 'widget.jitnumberfield'
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , labelWidth: 73
            , height: 22
            , width:183
            , allowDecimals: false
            , decimalPrecision: 0
            , maxValue:999999999
            , minValue:0
        };
        //判断是否可以输入小数
        if (cfg.allowDecimals) {
            defaultConfig.allowDecimals = cfg.allowDecimals;
            if (cfg.decimalPrecision) {
                defaultConfig.decimalPrecision = cfg.decimalPrecision
            } else {
                defaultConfig.decimalPrecision = 2;
            }
        }
        
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    jitGetValue: function () {
        return this.getValue();
    },
    jitSetValue: function (value) {
        this.setValue(value);
    }
});