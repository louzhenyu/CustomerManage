Ext.define('Jit.form.field.ComboBox', {
    extend: 'Ext.form.ComboBox',
    alias: 'widget.jitcombobox',
    allSelector: false,
    jitAllText: 'All',
    /*构造函数*/
    constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            multiSelect: false
            , labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , queryMode: 'local'
            , margin: '0 10 10 10'
            , editable: false
            , width: 183
            , labelWidth: 73
            , height: 22
            , matchFieldWidth: false
            , listConfig: {
                minWidth: 100
                , maxWidth: 300
                , cls: 'ComboBox'
            }
        }


        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        if (cfg.width != null && cfg.width != "") {
            cfg.listConfig = {
                minWidth: cfg.width - (cfg.labelWidth + cfg.labelPad)
                , maxWidth: 300
                , cls: 'ComboBox'
            }
        }
        //判断是否可以CheckBox多选
        if (cfg.multiSelect) {
            cfg.addAllSelector = true;
        } else {
            cfg.addAllSelector = false;
        }
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
    },
    createPicker: function () {
        var me = this,
           picker,
           menuCls = Ext.baseCSSPrefix + 'menu',
           opts = Ext.apply({
               pickerField: me,
               selModel: {
                   mode: me.multiSelect ? 'SIMPLE' : 'SINGLE'
               },
               floating: true,
               hidden: true,
               ownerCt: me.ownerCt,
               cls: me.el.up('.' + menuCls) ? menuCls : '',
               store: me.store,
               displayField: me.displayField,
               focusOnToFront: false,
               pageSize: me.pageSize,
               tpl: me.multiSelect ?
            [
                '<ul><tpl for=".">',
                    '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item"><span class="x-combo-checker">&nbsp;</span> {' + me.displayField + '}</li>',
                '</tpl></ul>'
            ] : ''
           }, me.listConfig, me.defaultListConfig);

        picker = me.picker = Ext.create('Ext.view.BoundList', opts);
        if (me.pageSize) {
            picker.pagingToolbar.on('beforechange', me.onPageChange, me);
        }

        me.mon(picker, {
            itemclick: me.onItemClick,
            refresh: me.onListRefresh,
            scope: me
        });

        me.mon(picker.getSelectionModel(), {
            'beforeselect': me.onBeforeSelect,
            'beforedeselect': me.onBeforeDeselect,
            'selectionchange': me.onListSelectionChange,
            scope: me
        });

        return picker;
    },
    getSubmitValue: function () {
        return this.getValue();
    },
    expand: function () {
        var me = this,
           bodyEl, picker, collapseIf;

        if (me.rendered && !me.isExpanded && !me.isDestroyed) {
            bodyEl = me.bodyEl;
            picker = me.getPicker();
            collapseIf = me.collapseIf;
            //显示
            picker.show();
            me.isExpanded = true;
            me.alignPicker();
            bodyEl.addCls(me.openCls);

            if (me.addAllSelector == true) {
                if (picker.getEl().getHeight() == 300) {
                    picker.getEl().setHeight(330)
                }
            }

            if (me.addAllSelector == true && me.allSelector == false) {
                me.allSelector = picker.getEl().insertHtml('afterBegin', '<div class="x-boundlist-item" role="option"><span class="x-combo-checker">&nbsp;</span> ' + me.jitAllText + '</div>', true);
                me.allSelector.on('click', function (e) {
                    if (me.allSelector.hasCls('x-boundlist-selected')) {
                        me.allSelector.removeCls('x-boundlist-selected');
                        me.setValue('');
                        me.fireEvent('select', me, []);
                    }
                    else {
                        var records = [];
                        me.store.each(function (record) {
                            records.push(record);
                        });
                        me.allSelector.addCls('x-boundlist-selected');
                        me.select(records);
                        me.fireEvent('select', me, records);
                    }
                });
            }
            // 监听 
            me.mon(Ext.getDoc(), {
                mousewheel: collapseIf,
                mousedown: collapseIf,
                scope: me
            });
            Ext.EventManager.onWindowResize(me.alignPicker, me);
            me.fireEvent('expand', me);
            me.onExpand();
        }
    }
    , onListSelectionChange: function (list, selectedRecords) {
        var me = this;

        if (!me.ignoreSelection && me.isExpanded) {
            if (!me.multiSelect) {
                Ext.defer(me.collapse, 1, me);
            }
            me.setValue(selectedRecords, false);
            if (selectedRecords.length > 0) {
                me.fireEvent('select', me, selectedRecords);
            }
            me.inputEl.focus();
        }

        if (me.addAllSelector == true && me.allSelector != false) {
            if (selectedRecords.length == me.store.getCount()) me.allSelector.addCls('x-boundlist-selected');
            else me.allSelector.removeCls('x-boundlist-selected');
        }
    }
}); 