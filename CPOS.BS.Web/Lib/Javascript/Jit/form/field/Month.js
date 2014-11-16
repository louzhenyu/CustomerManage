Ext.define('Jit.form.field.Month', {
    extend: 'Ext.form.field.Picker'
    , alias: 'widget.jitmonthfield'
    , triggerCls: Ext.baseCSSPrefix + 'form-date-trigger'
    , matchFieldWidth: false
    /*值格式不正确的错误信息*/
    , valueFormatErrorMessage:'value值必须为合法的日期对象或格式正确的日期字符串.'
    /*内部的日期值*/
    , innerDateValue: null
    /*构造函数*/
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
            , format: "Y-m"
        }
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //设置内部的日期值
        if (cfg.value) {
            if (Ext.isDate(cfg.value)) {
                //do nothing
            } else if (Ext.isString(cfg.value)) {
                var temp = Ext.Date.parse(cfg.value, cfg.format);
                if (temp != null)
                    cfg.value = temp;
                else
                    Ext.Error.raise(this.valueFormatErrorMessage);
            } else {
                Ext.Error.raise(this.valueFormatErrorMessage);
            }
        } else {
            cfg.value = new Date();
        }
        this.innerDateValue = cfg.value;
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    /*初始化控件*/
    , initComponent: function () {
        var me = this;
        me.disabledDatesRE = null;
        me.callParent();
    }
    /*
    将文本框中的输入内容转换为日期值
    */
    , rawToValue: function (rawValue) {
        return Ext.Date.parse(rawValue, this.format);
    }
    /*
    格式化日期并显示在文本框中
    */
    , valueToRaw: function (value) {
        return Ext.Date.dateFormat(value, this.format);
    }
    /*创建Picker对象*/
    , createPicker: function () {
        var me = this;
        //默认的配置项
        var defaultPickerCfg = {
            pickerField: me
            , renderTo: me.renderTo
            , ownerCt: me.ownerCt
            , floating: true
            , shadow: false
            , focusOnShow: true
            , listeners: {
                scope: me
                , cancelclick: me.onCancelClick
                , okclick: me.onOKClick
                , yeardblclick: me.onOKClick
                , monthdblclick: me.onOKClick
            }
        };
        //合并配置项
        var cfg = Ext.applyIf(me.pickerCfg || {}, defaultPickerCfg);
        //创建Picker
        return Ext.create('Ext.picker.Month', cfg);
    }
    /*
    控件展开
    */
    , onExpand: function () {
        this.picker.setValue(this.innerDateValue);
    }
    /*
    工具函数 - 用于处理Picker的OK按钮点击后的事件
    @pPicker    当前的Picker对象
    @pValue     用户选中的值数组
    */
    , onOKClick: function (pPicker, pValue) {
        var me = this,
            month = pValue[0],
            year = pValue[1],
            date = new Date(year, month, 1);
        me.setValue(date);
        me.innerDateValue = date;
        pPicker.hide();
    }
    /*
    工具函数 - 用于处理Picker的Cancel按钮点击后的事件
    */
    , onCancelClick: function () {
        this.picker.hide();
    }
    /*
    JIT标准的获取值方法
    */
    , jitGetValue: function () {
        var val = this.getValue();
        if (val != null) {
            return Ext.Date.format(val, this.format);
        }
        return "";
    }
    /*
    JIT标准的设置值方法
    */
    , jitSetValue: function (value) {
        if (value) {
            if(Ext.isDate(value)){
                this.setValue(value);
                this.innerDateValue = value;
            }else if(Ext.isString(value)){
                var temp = Ext.Date.parse(value, this.format);
                if (temp != null){
                    this.setValue(temp);
                    this.innerDateValue = temp;
                }
                else{
                    Ext.Error.raise(this.valueFormatErrorMessage);
                }
            }else{
                Ext.Error.raise(this.valueFormatErrorMessage);
            }
        } else {
            this.setValue(null);
            this.innerDateValue = null;
        }
    }
});