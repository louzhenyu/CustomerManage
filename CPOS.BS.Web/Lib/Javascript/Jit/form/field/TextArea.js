Ext.define('Jit.form.field.TextArea', {
    extend: 'Ext.form.field.TextArea'
    , alias: 'widget.jittextarea'
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '10'
            , width: 482
            , labelWidth: 72
            , height: 84           
            , grow: true
            , matchFieldWidth: false
            , componentCls: 'TextArea'
        };
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