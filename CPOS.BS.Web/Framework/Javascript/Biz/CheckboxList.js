
Ext.define('Jit.biz.CheckboxList', {
    alias: 'widget.jitbizcheckboxlist'
    , constructor: function (cfg) {
        if (cfg.items != null && cfg.items.length > 0) {
            var panelConfigs = {};
            panelConfigs.renderTo = cfg.renderTo;
            panelConfigs.width = cfg.width;
            panelConfigs.height = cfg.height;
            panelConfigs.layout = {
                type: 'table'
                , columns: 3
            };
            panelConfigs.items = new Array();
            //            if (cfg.data != null) {
            //                cfg.data.each(function (item) {
            //                    panelConfigs.items.push({ xtype: 'checkbox'
            //                                            , id: item.get('id')
            //                                            , boxLabel: item.get('text')
            //                                            , checked: item.get('checked')
            //                                            , width: 100
            //                                            , labelPad: 5
            //                                            , margin: '5 5 5 5' });
            //                });
            //            }
            for (var i = 0; i < cfg.items.length; i++) {
                var item = cfg.items[i];

                panelConfigs.items.push({ xtype: 'checkbox'
                                        , id: item.id
                                        , boxLabel: item.text
                                        , checked: item.checked
                                        , inputValue: item.value
                                        , width: 100
                                        , itemid: item.value
                                        , labelPad: 5
                                        , margin: '5px 5px 5px 5px'
                });
            }

            var result = Ext.create('Ext.panel.Panel', panelConfigs);
           
            return result;
        } else {
            return Ext.create('Ext.panel.Panel', cfg);
        }
    }
});