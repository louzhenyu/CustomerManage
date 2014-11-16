
Ext.define('Jit.button.CollapseButton', {
    extend: 'Ext.button.Button'
    , alias: 'widget.JitCollapseButton'
    , config: {
        //默认是否为展开状态
        expanded: false
        , handler: null
        /*
        @size   尺寸分为small,big两种
        */
         , jitSize: 'small'
    }
        , constructor: function (cfg) {

            var me = this;
            //定义默认配置
            var defaultConfig = {
                height: 25,
                margin: '0 0 0 10'
                , border: 0
                 , jitSize: 'small'
            };
            //自己的配置项处理
            //            var cfg = Ext.applyIf(cfg, { });
            //合并配置项
            cfg = Ext.applyIf(cfg, defaultConfig);
            if (cfg.jitSize) {
                var size = cfg.jitSize.toString().toLowerCase();
                switch (size) {
                    case 'small':
                        {
                            cfg.width = 80;
                        }
                        break;
                    case 'big':
                        {
                            cfg.width = 100;
                        }
                        break;
                }
            }
            //初始化配置项
            this.initConfig(cfg);
            //调用父类进行初始化
            this.callParent(arguments);

            me.addListener('click', function () {
                if (cfg.readOnly == false || cfg.readOnly == null) {
                    //切换按钮外观
                    this.fnSwitchCollapse();
                }
            });
            //呈现默认状态
            this.expanded = !this.expanded;
            this.fnSwitchCollapse();
        },
    fnSwitchCollapse: function () {
        if (this.expanded) {
            this.setText("&nbsp;&nbsp;&nbsp;&nbsp;展&nbsp;&nbsp;开");
            this.removeCls("arrowup");
            this.addCls("arrowdown");
            this.show = false;
            this.expanded = false;
        } else {
            this.setText("&nbsp;&nbsp;&nbsp;&nbsp;收&nbsp;&nbsp;起");
            this.removeCls("arrowdown");
            this.addCls("arrowup");
            this.show = true;
            this.expanded = true;
        }
    }
});