Ext.define('Jit.form.field.Date', {
    extend: 'Ext.form.field.Date'
    , alias: 'widget.jitdatefield'
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , labelWidth: 73
            , height: 22
            , width: 183
            , editable: false
            , format: "Y-m-d"
        }

        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    jitGetValue: function () {
        var timeText = this.getValue();
        if (timeText != null) {
            return Ext.Date.format(timeText, 'Y-m-d');
        }
        return "";
    },
    jitSetValue: function (value) {
        if (value != null && value != "") {
            //兼容IE8并且替换2013/12/12为2013-12-12
            if (typeof (value) != 'object' && value.constructor != Date) {
                if (value.toString().indexOf("/") > -1) {
                    value = value.replace(/\//g, "-");
                }
                this.setValue(Ext.Date.format(Ext.Date.parse(value, 'c'), 'Y-m-d'));
            } else {
                this.setValue(Ext.Date.format(value, 'Y-m-d'));
            }
        } else {
            this.setValue("");
        }
    },
    jitGetValueText: function () {
        var timeText = this.getValue();
        if (timeText != null) {
            return Ext.Date.format(timeText, 'Y-m-d');
        }
        return "";
    }
});