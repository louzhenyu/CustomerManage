Ext.define('Jit.form.field.SelectTextBox', {
    extend: 'Ext.panel.Panel'
    , alias: 'widget.JITSelectTextBox'
    , config: {
        fieldLabel: null,
        Value: null,
        Text: null,
        width: null
        /*
        @size 可选的值有small,big
        */
        ,jitSize: 'small'
    }
    , constructor: function (cfg) {
        if (cfg.jitSize == null)
            cfg.jitSize = 'small';
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        cfg.width = 183;
                    }
                    break;
                case 'big':
                    {
                        cfg.width = 233;
                    }
                    break;
            }
        }
        var defaultConfig = {
            margin: '0 10 10 10',
            height: 22,
            layout: 'column',
            border: 0
        };
        var me = this;
        //添加自定义事件
        this.addEvents({
            "onSelect": true
        });
        //自己的配置项处理
        //创建文本框
        me.selectText = Ext.create('Jit.form.field.Text',
        {
            fieldLabel: cfg.fieldLabel,
            height: 22,
            margin: '0 0 0 0'
             , labelWidth: 53
         , labelPad: 10
         , labelAlign: 'left'
            , width: cfg.width - 17
        });
        //创建按钮
        me.selectBtn = Ext.create('Jit.button.Button',
         {
             margin: '0 0 0 0'
                , height: 22
                , width: 17
                , border: 0
                , name: 'selectBtn'
                , cls: 'selecthighlight'
               , handler: function () { //引发自定义事件                       
                   me.fireEvent("onSelect", me);
               }
         });

        me.selectText.addListener('focus', function () {
            //引发自定义事件                       
            me.fireEvent("onSelect", me);
        });

        defaultConfig.items = [me.selectText, me.selectBtn];
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    , jitSetText: function (text) {
        var me = this;
        me.Text = text;
        me.selectText.setValue(me.Text);
    }
    , jitSetValue: function (Value) {
        var me = this;
        me.Value = Value;
    }
    , jitGetText: function () {
        var me = this;
        return me.Text;
    }
    , jitGetValue: function () {
        var me = this;
        return me.Value;
    }

}
);


