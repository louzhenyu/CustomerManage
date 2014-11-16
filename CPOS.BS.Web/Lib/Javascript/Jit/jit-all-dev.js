Ext.define('Jit.app.Controller', {
    extend: 'Ext.app.Controller'
    , config: {
        /*
        @view   当前的视图
        */
        view: null
        /*
        @menuID 当前页面的菜单ID
        */
        ,menuID:null
        /*
        @actions 当前view所支持的所有actions
        */
        , actions: null
    }
    , constructor: function (cfg) {
        //定义默认配置项
        var defaultConfig = {
            view: null
            , menuID: null
            , actions: null
        };
        //合并配置项
        Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    , init: function (application) {
        //将application的参数拷贝到controller中
        Ext.applyIf(this, application.params || {});
        //检查是否配置了参数
        if (this.ajaxHandlerPath == null) {
            Ext.Error.raise("在application中必须配置ajaxHandlerPath.");
        }
        if (this.menuID == null) {
            Ext.Error.raise("在application中必须配置menuID.");
        }
    }
    /*
    工具方法 - 根据键获取多语言资源
    @key    多语言资源项的键
    返回多语言资源项的值
    */
    , getResource: function (key) {
        if (key != undefined) {
            for (var i = 0; i < this.languageResources.length; i++) {
                if (this.languageResources[i].key == key) {
                    return this.languageResources[i].value;
                }
            }
            return 'ERROR';
        }
    }
    /*
    工具方法 - 根据键获取多语言资源
    @view   视图
    @action 操作
    返回ajax请求的url
    */
    , getAjaxUrl: function (view, action) {
        //处理view&action
        var current = {
            view: view
            , action: action
        };
        current = Ext.applyIf(current, {
            view: this.view
            , action: this.action
        });
        //拼接查询字符串
        var url = this.ajaxHandlerPath + '?';
        url += '&menu=' + (this.menu == null ? '' : this.menu);
        url += '&view=' + (current.view == null ? '' : current.view.toString());
        url += '&action=' + (current.action == null ? '' : current.action.toString());
        //
        return url;
    }
});
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
/*
内存分页的Store
*/
Ext.define('Jit.data.PagingMemoryStore', {
    extend: 'Ext.data.Store'
    , alias: 'widget.jitPagingMemoryStore'
    /*
    构造函数
    */
    , constructor: function (config) {
        config = config || {};
        this.callParent([config]);
    }
    /*
    覆写filterBy方法，以实现从包含所有数据的数据集中获取当页数据
    */
    , filterBy: function (fn, scope) {
        var me = this;
        //从proxy中获取所有的数据进行筛选
        var allDatas = Ext.create('Ext.util.MixedCollection', false, function (record) { return record.internalId; });
        allDatas.addAll(me.proxy.getReader().readRecords(me.proxy.data).records);
        var filteredDatas = allDatas.filterBy(fn, scope || me);
        filteredDatas.sort(me.sorters.items);
        //获取当页数据
        var pageSize = me.pageSize;
        var totalRowCount = filteredDatas.length || 0;
        var totalPageCount = Math.ceil(totalRowCount / pageSize);

        if (me.currentPage > totalPageCount && me.currentPage != 1) {
            me.currentPage = totalPageCount
        }
        var currentPageIndex = me.currentPage - 1;

        var start = currentPageIndex * pageSize;
        var limit = (currentPageIndex + 1) * pageSize;
        var pagedData = new Array();
        for (var i = start; i < filteredDatas.length && i < limit; i++) {
            pagedData.push(filteredDatas.items[i]);
        }

        var data = Ext.create('Ext.util.MixedCollection', false, function (record) { return record.internalId; });
        data.addAll(pagedData);
        me.snapshot = me.snapshot || data;
        me.data = data;

        //重新设置store的一些属性以便于分页控件重新计算
        me.totalCount = totalRowCount;
        me.fireEvent('datachanged', me);
    }
});
Ext.define('Jit.form.Label', {
    extend: 'Ext.form.Label'
    , alias: 'widget.jitlabel'
    , config: {
        /*
        @size 可选的值有small,big
        */
        jitSize: null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , margin: '0 10 10 10'          
            , height: 22
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 183;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 233;
                    }
                    break;
            }
        }      
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    jitSetValue: function (value) {
        this.setText(value);
    }
});
Ext.define('Jit.form.field.Checkbox', {
    extend: 'Ext.form.field.Checkbox'
    , alias: 'widget.jitcheckbox'
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , width: 183
            , labelWidth: 73
            , height: 22
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
Ext.define('Jit.form.field.ComboTree', {
    extend: 'Ext.form.field.Picker'
    , alias: 'widget.jitcombotree'
    /*自定义属性*/
    , config: {
        /*单选or多选*/
        multiSelect: false
        /*获取数据的url*/
        , url: null
        /*是否显示根节点*/
        , isRootVisible: false
        /*根节点的文本*/
        , rootText: 'root'
        /*根节点的ID*/
        , rootID: 'root'
        /*是否只能选择叶子节点*/
        , isSelectLeafOnly: false
        /*初始选中的项,该参数为一个数组，数组中的每个元素都包含id和text属性*/
        , initSelectedItems: null
        /*是否添加请选择项*/
        , isAddPleaseSelectItem: false
        /*请选择项的文本*/
        , pleaseSelectText: '--请选择--'
        /*请选择项的ID*/
        , pleaseSelectID: -2
        /*选择事件*/
        , onSelect: null
        /*树picker的配置项*/
        , pickerCfg: {
            Height: 300
            , maxHeight: 300
        }
        /*当前面板内已经选中的项*/
        , selectedValues: new Array()
        /*初始值,初始值可能不在当前面板内*/
        , initValues: new Array()
        /*是否已经创建picker*/
        , hasCreatedPicker: false
    }
    /*私有字段*/
//    /*当前面板内已经选中的项*/
//    , selectedValues: new Array()
//    /*初始值,初始值可能不在当前面板内*/
//    , initValues: new Array()
//    /*是否已经创建picker*/
//    , hasCreatedPicker: false
    /*构造函数*/
    , constructor: function (cfg) {
        var defaultConfig = {
            editable: false
            , labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , width: 183
            , labelWidth: 73
            , height: 22
            , matchFieldWidth: false
        };
        //处理初始值
        if (cfg.initSelectedItems != null) {
            if (!Ext.isArray(cfg.initSelectedItems)) {
                Ext.Error.raise("配置项initSelectedItems的值必须是一个数组,且数组内的每个元素都包含id和text属性.");
            }
            this.initValues = new Array();
            var text = '';
            var isFirstItem = true;
            for (var i = 0; i < cfg.initSelectedItems.length; i++) {
                var item = cfg.initSelectedItems[i];
                if (item.id != null && item.text != null) {
                    this.initValues.push({ id: item.id, text: item.text });
                    if (isFirstItem) {
                        text += item.text;
                        isFirstItem = false;
                    } else {
                        text += ',' + item.text;
                    }
                }
            }

            defaultConfig.value = text;
        }
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    /*
    @private
    重写createPicker方法,由创建boundlist改为创建树面板
    */
    , createPicker: function () {
        var me = this;
        me.selectedValues = new Array();
        Ext.log('call createPicker method.');
        //创建数据集
        me.store = Ext.create('Ext.data.TreeStore', {
            proxy: {
                type: 'ajax'
                , url: this.url
            }
            , root: {
                expanded: true
                , text: this.rootText
                , id: this.rootID
                , checked: (this.multiSelect && this.isSelectLeafOnly == false) ? false : null
            }
            , listeners: {
                load: {
                    /*
                    加载数据
                    @store      当前的TreeStore实例
                    @node       需要加载数据的节点
                    @records    从后台获取的数据记录
                    @isSuccess  是否成功
                    @options    调用者传递过来的可选参数项
                    */
                    fn: function (store, node, records, isSuccess, options) {
                        Ext.log('fire load event.');
                        if (!isSuccess) {
                            Ext.Error.raise("获取树节点数据失败.");
                        } else {
                            //如果设置了初始值，则一旦加载了初始值后，就不在保留为初始值
                            var picker = me.getPicker();
                            if (me.initValues != null && me.initValues.length > 0 && records != null && records.length > 0) {
                                for (var i = 0; i < me.initValues.length; i++) {
                                    var val = me.initValues[i].id;
                                    for (var j = 0; j < records.length; j++) {
                                        var record = records[j];
                                        if (val == record.get('id')) {//当前节点
                                            //移到选中项中
                                            me.selectedValues.push(me.initValues[i]);
                                            me.initValues.splice(i, 1);
                                            i--;
                                            //如果是单选，设置选中样式
                                            if (me.multiSelect == false) {
                                                picker.getSelectionModel().select(record);
                                            }
                                        } else {//其子节点
                                            var child = record.findChild('id', val, true);
                                            if (child != null) {
                                                //移到选中项中
                                                me.selectedValues.push(me.initValues[i]);
                                                me.initValues.splice(i, 1);
                                                i--;
                                                //如果是单选，设置选中样式
                                                if (me.multiSelect == false) {
                                                    picker.getSelectionModel().select(record);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                , beforeload: {
                    /*
                    加载数据之前
                    @store      当前的树节点store
                    @operation  操作
                    @options    向后传递的可选参数
                    */
                    fn: function (store, operation, options) {
                        Ext.log('fire beforeload event.');
                        operation.params.multiSelect = me.multiSelect;  //告诉后台是否为多选
                        operation.params.isSelectLeafOnly = me.isSelectLeafOnly;    //告诉后台是否为只选择叶子节点
                        //向后台发送初始值
                        if (me.initValues != null && me.initValues.length > 0) {
                            operation.params.initValues = Ext.JSON.encode(me.initValues);    //告诉后台初始值
                        }
                        //向后发发送是否配置了请选择项
                        if (me.isAddPleaseSelectItem) {
                            operation.params.isAddPleaseSelectItem = me.isAddPleaseSelectItem;
                            operation.params.pleaseSelectText = me.pleaseSelectText;
                            operation.params.pleaseSelectID = me.pleaseSelectID;
                        }
                    }
                }
            }
        });
        //
        var defaultTreeCfg = {
            width: 200
            , minHeight: 300
            , autoScroll: true
            , floating: true
        };
        var treeCfg = Ext.applyIf(me.pickerCfg || {}, defaultTreeCfg);
        treeCfg.store = me.store;
        treeCfg.multiSelect = me.multiSelect;
        treeCfg.rootVisible = me.isRootVisible;
        if (me.isAddPleaseSelectItem) {
            //如果需要添加 --请选择-- 项,则采用自定义的tree column
            treeCfg.hideHeaders = true;
            treeCfg.columns = [{
                xtype: 'jittreecolumn',
                text: 'Name',
                width: Ext.isIE6 ? null : 10000,
                dataIndex: 'text'
            }];
        }
        //创建树面板
        var tree = Ext.create('Ext.tree.Panel', treeCfg);
        if (me.isAddPleaseSelectItem) {
            tree.addCls(Ext.baseCSSPrefix + 'autowidth-table');
        }
        this.hasCreatedPicker = true;
        //挂载事件处理
        if (Ext.isIE7 || Ext.isIE8) {
            tree.on('checkchange'
                , function (record, checked, options) {
                    me.choice(record, false);
                }, this);
        }
        tree.on('itemclick', function (combotreeView, record, item, index, e, options) {
            me.choice(record, true);
        }, this);
        //
        return tree;
    }
    /*
    @public
    获取已经选中的值
    @return     选中值的数组,如果为单选则数组中只有一个元素，否则会有多个
    */
    , getValues: function () {
        var values = new Array();
        //将仍然没有加载到当前面板的初始值加入值数组
        if (this.initValues != null && this.initValues.length > 0) {
            values = values.concat(Ext.clone(this.initValues));
        }
        if (this.selectedValues != null) {
            values = values.concat(Ext.clone(this.selectedValues));
        }
        return values;
    }
    /*
    @public
    设置值
    @pSelectedItems     选中的项,参数类型为数组，数组中的每个元素都是一个包含id和text属性的对象。
    id值是必须的，text值为可选。但是，如果选中的项不在当前树节点内，如果text为空
    则显示的选中值文本会不对。
    @pIsAppend          是否添加,参数类型为bool.如果值为true,则保留现有的值,否则首先清除现有的值.
    默认为false.
    */
    , setValues: function (pSelectedItems, pIsAppend) {
        Ext.log('call setValues method.');
        //参数处理
        if (pSelectedItems == null)
            pSelectedItems = new Array();
        if (this.multiSelect == false && pSelectedItems.length > 1) {//单选时不能设置多值
            Ext.Error.raise('单选时不能设置多值.');
        }
        var items = new Array();
        for (var i = 0; i < pSelectedItems.length; i++) {
            var item = pSelectedItems[i];
            if (item.text == null) {
                item.text = "";
            }
            if (item.id == null) {
                Ext.Error.raise('pSelectedItems数组中的每个元素都必须包含id属性.');
            }
            items.push({ id: item.id, text: item.text });
        }
        //Ext.data.TreeStore一旦创建后，会自动向后台发送请求，获取节点数据
        //因此，为了避免对initValues的并发访问,分2种情况进行处理
        if (this.hasCreatedPicker == false) {//如果未调用过createPicker方法
            if (this.initValues == null) {
                this.initValues = new Array();
            }
            if (!pIsAppend) {
                this.initValues = items;
            } else {
                if (items != null && items.length > 0) {
                    for (var i = 0; i < items.length; i++) {
                        var isRepeat = false;
                        for (var j = 0; j < this.initValues.length; j++) {
                            if (items[i].id == this.initValues[j].id) {
                                isRepeat = true;
                                break;
                            }
                        }
                        if (!isRepeat) {
                            this.initValues.push(items[i]);
                        }
                    }
                }
            }
        } else {//调用过createPicker方法,默认认为数据已经加载完毕
            var picker = this.getPicker();
            var store = picker.store;
            //清除现有的选中项
            if (!pIsAppend) {
                var checkedItems = picker.getChecked();
                if (checkedItems != null) {
                    for (var i = 0; i < checkedItems.length; i++) {
                        checkedItems[i].set('checked', false);
                    }
                }
                //清除值
                this.selectedValues = new Array();
                this.initValues = new Array();
            }
            //处理选中值
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                //
                var node = store.getNodeById(item.id);
                if (node != null) {
                    //在当前树中发现指定的节点
                    //如果节点的选中状态为未选中,则设置选中状态为选中
                    if (this.multiSelect == true) {
                        if (!node.get('checked')) {
                            node.set('checked', true);
                        }
                    } else {
                        picker.getSelectionModel().select(node);
                    }
                    this.selectedValues.push({ id: node.get('id'), text: node.get('text') });
                } else {
                    //在当前树中未发现指定的节点，组件将该节点当作是初始选中项看待
                    this.initValues.push({ id: item.id, text: item.text });
                }
            }
        }
        //更新当前的文本
        this.refreshText();
    },
    jitGetValue: function () {
        var selectedValues = this.getValues();
        var ids = new Array();
        if (selectedValues != null) {
            for (var i = 0; i < selectedValues.length; i++) {
                var itemID = selectedValues[i]["id"];
                if (itemID != this.pleaseSelectID)
                    ids.push(itemID);
            }
        }
        return ids.join(',');
    },
    /*
    根据id值
    */
    jitGetByID: function (pID) {
        var store = this.store;
        var item = null;
        if (store != null) {
            var root = store.getRootNode();
            if (root != null) {
                var node = root.findChild('id', pID, true);
                if (node != null) {
                    item = { "id": node.data.id, "text": node.data.text };
                }
            }
        }
        return item;
    },
    jitSetValueText: function (value, valueText) {
        if (Ext.isArray(value)) {
            this.setValues(value, false);
        } else {
            var initValues = new Array();
            if (value != null && value != "") {
                var values = value.toString().split(",");
                var valueTexts = valueText.toString().split(",");
                for (var i = 0; i < values.length; i++) {
                    if (values[i] != "") {
                        initValues.push({ id: values[i], text: valueTexts[i] });
                    }
                }
            }
            this.setValues(initValues, false);
        }
    },
    jitSetValue: function (value) {
        this.setValues(value, false);
    }
    /*
    @private
    工具方法 - 根据选中项刷新文本
    */
    , refreshText: function () {
        var text = '';
        var isFirstNode = true;
        if (this.selectedValues != null && this.selectedValues.length > 0) {
            for (var i = 0; i < this.selectedValues.length; i++) {
                if (isFirstNode) {
                    text = this.selectedValues[i].text;
                    isFirstNode = false;
                } else {
                    text += ',' + this.selectedValues[i].text;
                }
            }
        }
        if (this.initValues != null && this.initValues.length > 0) {
            for (var i = 0; i < this.initValues.length; i++) {
                if (isFirstNode) {
                    text = this.initValues[i].text;
                    isFirstNode = false;
                } else {
                    text += ',' + this.initValues[i].text;
                }
            }
        }
        //设置文本
        this.setValue(text);
    }
    /*
    @private
    工具方法 - 选中处理
    @combotreeView   当前的控件的view
    @record     选中项的数据
    @item       选中项的HTML
    @index      选中项的索引
    @e          原始的事件对象
    @options    从前面传递过来的可选参数
    */
    , choice: function (record, type) {
        var picker = this.getPicker();
        //
        if (this.isSelectLeafOnly && record.get('leaf') == false) {
            return;
        }
        var defaultSelectValue = this.jitGetValue();
        if (this.multiSelect) {//多选模式
            //复写 picker.getView().onCheckChange(record),不调用'checkchange'事件
            if (type) {
                var checked = record.get('checked');
                if (Ext.isBoolean(checked)) {
                    checked = !checked;
                    record.set('checked', checked);
                }
            }
            //
            var selectedItems = picker.getChecked();
            var selectedValues = new Array();
            if (selectedItems != null) {
                for (var i = 0; i < selectedItems.length; i++) {
                    var item = selectedItems[i];
                    selectedValues.push({ id: item.get('id'), text: item.get('text') });
                }
            }
            this.selectedValues = selectedValues;
        } else {//单选模式 
            this.initValues = new Array();
            this.selectedValues = new Array();
            this.selectedValues.push({ id: record.get('id'), text: record.get('text') });

            this.collapse();
        }
        //刷新文本
        this.refreshText();
        if (this.onSelect != null) {
            if (defaultSelectValue != this.jitGetValue()) {
                this.onSelect();
            }
        }
    }
});
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
Ext.define('Jit.form.field.Display', {
    extend: 'Ext.form.field.Display'
    , alias: 'widget.jitdisplayfield'
    , config: {
        /*
        @size 可选的值有small,big
        */
        jitSize: null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , labelWidth: 73
            , cls: 'Displayfield'
            , height: 22
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 183;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 233;
                    }
                    break;
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
Ext.define('Jit.form.field.Radio', {
    extend: 'Ext.form.field.Radio',
    alias: ['widget.jitradiofield', 'widget.jitradio'],

    constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
			, style: 'font-color:#333333;font-size:12px'
            , width: 183
            , labelWidth: 73
            , height: 22  
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



Ext.define('Jit.form.field.Text', {
    extend: 'Ext.form.field.Text'
    , alias: 'widget.jittextfield'
    , config: {
        /*
        @size 可选的值有small,big
        */
        jitSize: null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , labelWidth: 73
            , height: 22
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 183;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 233;
                    }
                    break;
            }
        }
       jitGetValue:
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
Ext.define('Jit.form.field.Time', {
    extend: 'Ext.form.field.Time'
    , alias: 'widget.jittimefield'
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
Ext.define('Jit.grid.column.Column', {
    extend: 'Ext.grid.column.Column'
    , alias: 'widget.jitcolumn'
    , config: {
        /*@jitDataType jit的数据类型*/
        jitDataType: null
        /*@jitTimespanFormatter 时间的格式化*/
        , jitTimespanFormatter: null
    }
    , statics: {
        /*@jitYesText 当jitDataType=Boolean时，值为true的文本*/
        jitYesText: '是'
        /*@jitNoText 当jitDataType=Boolean时，值为false的文本*/
        , jitNoText: '否'
        /*@jitTimespanFormatter 时间跨度的格式字符串*/
        , jitTimespanFormatter: {
            dayText: '天'
            , hourText: '小时'
            , minuteText: '分'
            , secondText: '秒'
        }
    }
    /*@constructor 构造函数*/
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            jitDataType: 'int'
            , sortable: true
            , hideable: false
        };
        //处理配置项      
        var cfg = Ext.applyIf(cfg, defaultConfig);
        var dataType = cfg.jitDataType.toLowerCase();
        switch (dataType) {
            case 'int': // 整数，数值靠右对齐
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderInt;
                }
                break;
            case 'decimal': //定点小数，数值靠右对齐，小数点后只保留两位
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDecimal;
                }
                break;
            case 'boolean': //布尔，如果是true则为是，为false则为否，否则为空。
                {
                    cfg.align = 'center';
                    cfg.renderer = this.renderBoolean;
                }
                break;
            case 'string': //字符串，文本靠左对齐。
                {
                    cfg.align = 'left';
                }
                break;
            case 'date': //日期，数据类型为Date。格式化为yyyy-MM-dd，即4位年+2位月+2位日
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDate;
                }
                break;
            case 'datetime': //日期时间，数据类型为Date。格式化为 yyyy-MM-dd HH:mi:ss，即4位年+2位月+2位日+2位24小时制的小时+2位分+2位秒。
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDateTime;
                }
                break;
            case 'datetimenotss': //日期时间，数据类型为Date。格式化为 yyyy-MM-dd HH:mi
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDateTimeNotSS;
                }
                break;
            case 'monthdayhourminute':
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderMonthDayHourMinute;
                }
                break;
            case 'time': //时间，数据类型为Date。格式化为HH:mi:ss，即2位24小时制的小时+2位分+2位秒。
                {
                    cfg.align = 'left';
                    cfg.renderer = this.renderTime;
                }
                break;
            case 'timespan':
                {
                    /* 时间跨度，数据为int，值为时间跨度的秒数。按 xxx天xxx小时xxx分xxx秒的方式格式化，最后只保留2节。
                    例如
                    	1天14小时
                    	1小时14分
                    	58分38秒
                    	44秒
                    */
                    cfg.align = 'left';
                    cfg.renderer = this.renderTimespan;
                }
                break;
            case 'coordinate': //地图列
                {
                    cfg.align = 'center';
                    var me = this;
                    cfg.renderer = function (val, metaData, record, rowIndex, colIndex, store, view) {
                        return me.renderCoordinate(val, metaData, record, rowIndex, colIndex, store, view, cfg.getMapTitle);
                    }
                }
                break;
            case 'photo': //图片列
                {
                    cfg.align = 'center';
                    cfg.renderer = this.renderPhoto;
                }
                break;
            case 'tips':
                {
                    cfg.align = 'left';
                    cfg.renderer = this.renderTips;
                }
                break;
            case 'percent': //百分比列
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderPercent;
                    if (!cfg.accuracy) {//默认百分值不带小数位
                        cfg.accuracy = 0;
                    }
                }
                break;
            case 'customize': //自定义,不做任何事情
                break;
            default:
                Ext.Error.raise("无效的jitDataType的值:" + dataType + '.');
                break;
        }
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    /*
    呈现整数
    */
    , renderInt: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null || val == "") {
            return "0";
        }
        else {
            var a = val.toString();
            var fuhao = a.indexOf("-")
            if (fuhao > -1) {
                a = a.substring(1);
            }
            var b = "";
            if (a.length > 3) {
                var sum = a.length;
                var c = a.substring(sum);
                var k = sum % 3;
                var j = parseInt(sum / 3);
                b = b + a.substring(0, k);
                for (var i = 0; i < j; i++) {
                    var one = (k + 3 * i);
                    var two = k + 3 * (i + 1);
                    var d = ",";

                    b = b + d + a.substring(one, two);
                }
                b = b + c;
            }
            else { b = a; }
            if (k == 0) {
                b = b.substring(1);
            }
            if (fuhao > -1) {
                b = "-" + b;
            }
            return b;
        }
    }
    /*
    呈现定点小数
    */
    , renderDecimal: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null || val == "") {
            return "0.00";
        }
        else {
            var a = parseFloat(val).toFixed(2).toString();
            var fuhao = a.indexOf("-")
            if (fuhao > -1) {
                a = a.substring(1);
            }
            var b = "";
            if (a.length > 3) {
                var sum = a.lastIndexOf('.');
                if (sum < 1) {
                    sum = a.length;
                }
                var c = a.substring(sum);
                var k = sum % 3;
                var j = parseInt(sum / 3);
                b = b + a.substring(0, k);
                for (var i = 0; i < j; i++) {
                    var one = (k + 3 * i);
                    var two = k + 3 * (i + 1);
                    var d = ",";
                    b = b + d + a.substring(one, two);
                }
                b = b + c;
            }
            else { b = a; }
            if (k == 0) {
                b = b.substring(1);
            }
            if (fuhao > -1) {
                b = "-" + b;
            }
            return b;
        }
    }
    /*
    呈现布尔值
    */
    , renderBoolean: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (val)
                return Jit.grid.column.Column.jitYesText;
            else
                return Jit.grid.column.Column.jitNoText;
        }
    }
    /*
    呈现日期
    */
    , renderDate: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'Y-m-d');
        }
    }
    /*
    呈现日期时间
    */
    , renderDateTime: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'Y-m-d H:i:s');
        }
    }
    /*
    呈现日期时间
    */
    , renderDateTimeNotSS: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'Y-m-d H:i');
        }
    }
    /*
    呈现月日时分 06-04 18：08 
    */
    , renderMonthDayHourMinute: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'm-d H:i');
        }
    }
    /*
    呈现时间
    */
    , renderTime: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'H:i:s');
        }
    }
    /*
    呈现时间跨度
    */
    , renderTimespan: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (val > 3600) {
                return parseInt(val / 3600) + Jit.grid.column.Column.jitTimespanFormatter.hourText + parseInt((val - parseInt(val / 3600) * 3600) / 60) + Jit.grid.column.Column.jitTimespanFormatter.minuteText + (val - parseInt(val / 60) * 60) + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            } else if (val > 60) {
                return parseInt(val / 60) + Jit.grid.column.Column.jitTimespanFormatter.minuteText + (val - parseInt(val / 60) * 60) + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            } else {
                return val + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            }
        }
    }
    /*
    呈现地图
    */
   , renderCoordinate: function (val, metaData, record, rowIndex, colIndex, store, view, getMapTitle) {
       if (val != null && val != "") {
           try {
               var title = null;
               if (getMapTitle && Ext.isFunction(getMapTitle)) {
                   title = getMapTitle(val, record);
               }
               var Values = val.split(",");
               var Lng = 0;
               var Lat = 0;
               var inGPSType = 0;
               if (Values.length > 1) {
                   Lng = Values[0];
                   Lat = Values[1];
               }
               if (Values.length == 3) {
                   inGPSType = Values[2];
               }
               //  var Id = this.columns[colIndex].getId();
               //Ext.getCmp(\"" + Id + "\").mapShow(" + Lng + "," + Lat + "," + inGPSType + ",\"" + title + "\")
               return "<img src='/Lib/Image/icon_world.gif' style='cursor:pointer' onclick='___fnMapShow(" + Lng + "," + Lat + "," + inGPSType + ",\"" + title + "\")' /> ";
           } catch (e) {
               return "<img src='/Lib/Image/icon_noworld.jpg' /> ";
           }
       }
       return "<img src='/Lib/Image/icon_noworld.jpg' /> ";
   }
    /*
    呈现照片
    */
    , renderPhoto: function (val, metaData, record, rowIndex, colIndex, store, view) {
        var photoValue = "";
        var pClientUserID = 0;
        if (val != null && val != "") {
            try {
                if (val != null && val != "") {
                    var value = eval(val);
                    return "<img src='/Lib/Image/image.png' style='cursor:pointer' onclick='___fnPhotoShow(\"" + encodeURIComponent(Ext.encode(value)) + "\")' /> ";
                }

            } catch (e) {
                return "<img src='/Lib/Image/noimage.png' /> ";
            }
        }
        return "<img src='/Lib/Image/noimage.png' /> ";
    }
    /*
    呈现带有tips的字符串单元格
    */
    , renderTips: function (val, metaData, record, rowIndex, colIndex, store, view) {
        metaData.tdAttr = 'data-qtip="' + val + '"';
        return val;
    }
    /*
    呈现百分比
    */
    , renderPercent: function (val, metaData, record, rowIndex, colIndex, store, view) {
        var html = '';
        var col = this.headerCt.getGridColumns()[colIndex];
        if (val != null) {
            html = (parseFloat(val) * 100).toFixed(col.accuracy).toString() + "%";
        } else {
            html = '-';
        }
        return html;
    }
});


___fnMapShow = function (pLng, pLat, pA, pTitle) {
    if (pTitle == null || pTitle == "" || pTitle == "null") {
        pTitle = "Map";
    }
    Ext.create('Jit.window.MapWindow', {
        id: '__columnMapID',
        title: pTitle,
        jitPoint: {
            pointID: '0',            //整数，唯一标识，必须
            lng: pLng,               // 浮点数，商店GPS坐标的经度，必须，范围0-180.
            lat: pLat,               // 浮点数，商店GPS坐标的纬度，必须，范围0-90             
            isEditable: false,       // 是否可编辑      
            inGPSType: pA,
            mapScale: 15           //地图级别
        }
    });
    Ext.getCmp("__columnMapID").show();
}

/*照片查看*/
___fnPhotoShow = function (value) {
    Ext.create('Jit.window.PhotoWindow', {
        id: '__columnPhotoID',
        title: '照片查看',
        value: value
    });
    Ext.getCmp("__columnPhotoID").show();
}
/*
动态面板
*/
Ext.define('Jit.panel.DynamicPanel', {
    extend: 'Ext.panel.Panel'
    , alias: ['widget.jitdynamicpanel','widget.jitdpanel']
    , config: {
        /*
        获取动态控件配置的url
        $required
        */
        url:null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            closeable: true
            , closeAction: 'hide'
            , modal: 'true'
            , resizable: false
        };
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
});
//CheckboxModel基础控件
Ext.define('Jit.selection.CheckboxModel', {
    extend: 'Ext.selection.CheckboxModel',
    alias: 'widget.jitcheckboxmodel',
    allSelectorStatus: 0,   //定义初始值，当前状态0为默认，1为全选，2为全不选
    includeList: new Array(),   //定义包含值
    excludeList: new Array(),   //定义排除值
    initChecked: new Array(),   //定义中间值
    defaultList: new Array(),   //定义默认值
    rowSelect: true,            //定义是否能点击行选择
    config: {
        idProperty: '',  // 定义添加到数据库中的关联ID字段
        idSelect: ''     // 定义判断是否选择的ID字段
    },
    constructor: function (cfg) {
        var defaultConfig = {};
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    /*
    点击头部的checkbox 事件，这个进行判断状态是默认，还是全选或者全不选
    重写的方法，并且为一些参数设置为初始化状态
    */
    onHeaderClick: function (headerCt, header, e) {
        if (header.isCheckerHd) {
            e.stopEvent();
            var me = this,
                        isChecked = header.el.hasCls(Ext.baseCSSPrefix + 'grid-hd-checker-on');
            me.preventFocus = true;
            if (isChecked) {
                //修改为全不选状态
                this.allSelectorStatus = 2;
                this.excludeList = new Array();
                this.initChecked = new Array();
                this.defaultList = new Array();
                Ext.Msg.show({
                    title: '提示',
                    msg: '您的操作将清空所有数据',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        me.deselectAll();
                    }
                });
            } else {
                //修改为全选状态
                this.allSelectorStatus = 1;
                this.includeList = new Array();
                this.initChecked = new Array();
                this.defaultList = new Array();
                Ext.Msg.show({
                    title: '提示',
                    msg: '您的操作将选择所有数据',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        me.selectAll();
                    }
                });   
            }
            me.updateHeaderState();
            //if (me.selected.getCount() == 0) {
            //         me.updateHeaderState();
            //}
            delete me.preventFocus;
        }
    },
    /*
    重写点击行的事件，禁用了选择单条的功能
    在点击的时候，为数组添加或者删除一个数据
    */
    onRowMouseDown: function (view, record, item, index, e) {
        view.el.focus();
        var me = this,
                checker = e.getTarget('.' + Ext.baseCSSPrefix + 'grid-row-checker'),
                mode;
        var valueID = record.get(me.idProperty);

        if (me.checkOnly && checker == null) {
            return;
        }
        if (checker || this.rowSelect) {
            if (me.allSelectorStatus == 0) {
                if (me.isSelected(record)) {
                    this.includeList = this.jitDeleteArray(me.includeList, valueID);
                } else {
                    me.includeList = this.jitAddArray(me.includeList, valueID);
                }
            } else if (me.allSelectorStatus == 1) {
                if (me.isSelected(record)) {
                    me.excludeList = this.jitAddArray(me.excludeList, valueID);
                } else {
                    me.excludeList = this.jitDeleteArray(me.excludeList, valueID);
                }
            } else if (me.allSelectorStatus == 2) {
                if (me.isSelected(record)) {
                    me.includeList = this.jitDeleteArray(me.includeList, valueID);
                } else {
                    me.includeList = this.jitAddArray(me.includeList, valueID);
                }
            }
        }

        if (!me.allowRightMouseSelection(e)) {
            return;
        }
        if (me.checkOnly && !checker) {
            return;
        } 
        if (checker || this.rowSelect) {
            mode = me.getSelectionMode();
            if (mode !== 'SINGLE') {
                me.setSelectionMode('SIMPLE');
            }
            me.selectWithEvent(record, e);
            me.setSelectionMode(mode);
        }
    },
    onSelectChange: function () {
        var me = this;
        me.callParent(arguments);
        me.updateHeaderState();
    },
    /*
    重写了修改头部checkbox的事件
    */
    updateHeaderState: function () {
        //判断原始属性allSelectorStatus的值是否是全选
        var hdSelectStatus = this.allSelectorStatus == 1; // this.selected.getCount() === this.store.getCount();
        this.toggleUiHeader(hdSelectStatus);
    },
    /*
    自定义获取数据的方法，返回数组对象
    */
    jitGetValue: function () {
        var values = {};
        values.allSelectorStatus = this.allSelectorStatus;
        values.includeList = this.includeList;
        values.excludeList = this.excludeList;
        values.defaultList = this.defaultList;
        return values;
    },
    /*
    自定义清空所有的值方法
    */
    jitClearValue: function () {
        this.allSelectorStatus = 0;
        this.includeList = new Array();
        this.excludeList = new Array();
        this.initChecked = new Array();
        this.defaultList = new Array();
    }
    /*
    初始化的时候设置参数
    传入pGridView的Id    
    */
    , jitSetValue: function () {
        var me = this;
        var pGirdStore = me.getStore();
        var length = pGirdStore.data.items.length;
        if (this.allSelectorStatus == 0) {
            /*
            状态为默认状态
            */
            if (pGirdStore.data != null) {
                if (this.initChecked.length == 0) {
                    for (var i = 0; i < length; i++) {
                        var pStoreData = pGirdStore.data.items[i].data[this.idSelect]
                        if (pStoreData != null && pStoreData != "0" && pStoreData != "") {
                            this.initChecked = this.jitAddArray(this.initChecked, pGirdStore.data.items[i].data[this.idProperty]);
                        }
                    }
                    var defaultCount = this.defaultList.length;
                    if (this.defaultList.length == 0) {
                        this.defaultList = this.initChecked.toString().split(",");
                    } else {
                        this.defaultList = this.jitAddArrayByArray(this.defaultList, this.initChecked.toString().split(","));
                    }
                    if (defaultCount != this.defaultList.length) {
                        if (this.includeList.length == 0) {
                            this.includeList = this.initChecked.toString().split(",");
                        } else {
                            this.includeList = this.jitAddArrayByArray(this.includeList, this.initChecked.toString().split(","));
                        }
                    }
                    this.initChecked = new Array();
                }
                for (var i = 0; i < length; i++) {
                    var pStoreData = pGirdStore.data.items[i].data[this.idProperty]
                    if (pStoreData != null && pStoreData != "0" && pStoreData != "") {
                        if (this.includeList.length > 0) {
                            for (var j = 0; j < this.includeList.length; j++) {
                                if (this.includeList[j] == pStoreData) {
                                    me.select(i, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        } else if (this.allSelectorStatus == 1) {
            /*
            状态为全选状态
            */
            me.selectAll();
            for (var i = 0; i < length; i++) {
                var pStoreData = pGirdStore.data.items[i].data[this.idProperty]
                if (pStoreData != null && pStoreData != "0" && pStoreData != "") {
                    if (this.excludeList.length > 0) {
                        for (var j = 0; j < this.excludeList.length; j++) {
                            if (this.excludeList[j] == pStoreData) {
                                me.deselect(i, true);
                                break;
                            }
                        }
                    }
                }
            }
            //excludeList: new Array()
        } else if (this.allSelectorStatus == 2) {
            /*
            状态为全不选状态
            */
            me.deselectAll();
            for (var i = 0; i < length; i++) {
                var pStoreData = pGirdStore.data.items[i].data[this.idProperty]
                if (pStoreData != null && pStoreData != "0" && pStoreData != "") {
                    if (this.includeList.length > 0) {
                        for (var j = 0; j < this.includeList.length; j++) {
                            if (this.includeList[j] == pStoreData) {
                                me.select(i, true);
                                break;
                            }
                        }
                    }
                }
            }
        }
    },
    /*
    添加一个值到一个数组对象
    返回新的数组对象
    */
    jitAddArray: function (pCalueChecked, pValue) {
        var isContain = true;
        if (pCalueChecked.length > 0) {
            for (var j = 0; j < pCalueChecked.length; j++) {
                if (pCalueChecked[j] == pValue) {
                    isContain = false;
                    break;
                }
            }
        }
        if (isContain) {
            pCalueChecked.push(pValue);
        }
        return pCalueChecked;
    },
    /*
    添加一个新的数组对象到另一个数组中，去除重复
    返回新的数组对象
    */
    jitAddArrayByArray: function (pValueChecked, pValuesChecked) {
        var isContain = true;
        if (pValueChecked.length > 0) {
            for (var j = 0; j < pValuesChecked.length; j++) {
                isContain = true;
                for (var i = 0; i < pValueChecked.length; i++) {
                    if (pValuesChecked[j] == pValueChecked[i]) {
                        isContain = false;
                        break;
                    }
                }
                if (isContain) {
                    pValueChecked.push(pValuesChecked[j]);
                }
            }
        }
        return pValueChecked;
    }
    ,
    /*
    删除一个数组对象中的一个数据
    返回一个新的数组对象
    */
    jitDeleteArray: function (pValueChecked, pValue) {
        var isContain = false;
        var j = 0;
        if (pValueChecked.length > 0) {
            for (j = 0; j < pValueChecked.length; j++) {
                if (pValueChecked[j] == pValue) {
                    isContain = true;
                    break;
                }
            }
        }
        if (isContain) {
            pValueChecked.splice(j, 1);
        }
        return pValueChecked;
    }
})

/*
自定义实现树节点的呈现,以在下拉树控件中支持:
1.增加请选择项
*/
Ext.define('Jit.tree.Column', {
    extend: 'Ext.grid.column.Column',
    alias: 'widget.jittreecolumn',
    tdCls: Ext.baseCSSPrefix + 'grid-cell-treecolumn',

    initComponent: function () {
        var origRenderer = this.renderer || this.defaultRenderer,
            origScope = this.scope || window;

        this.renderer = function (value, metaData, record, rowIdx, colIdx, store, view) {
            var buf = [],
                format = Ext.String.format,
                depth = record.getDepth(),
                treePrefix = Ext.baseCSSPrefix + 'tree-',
                elbowPrefix = treePrefix + 'elbow-',
                expanderCls = treePrefix + 'expander',
                imgText = '<img src="{1}" class="{0}" />',
                checkboxText = '<input type="button" role="checkbox" class="{0}" {1} />',
                formattedValue = origRenderer.apply(origScope, arguments),
                href = record.get('href'),
                target = record.get('hrefTarget'),
                cls = record.get('cls');
            var isPleaseSelectItem = false;
            if (record.get('id') == "-2") {
                isPleaseSelectItem = true;
            }
            if (isPleaseSelectItem) {
                formattedValue = "&nbsp;" + formattedValue;
            }

            while (record) {
                if (isPleaseSelectItem == false) {
                    if (!record.isRoot() || (record.isRoot() && view.rootVisible)) {
                        if (record.getDepth() === depth) {
                            buf.unshift(format(imgText,
                                treePrefix + 'icon ' +
                                treePrefix + 'icon' + (record.get('icon') ? '-inline ' : (record.isLeaf() ? '-leaf ' : '-parent ')) +
                                (record.get('iconCls') || ''),
                                record.get('icon') || Ext.BLANK_IMAGE_URL
                            ));
                            if (record.get('checked') !== null) {
                                buf.unshift(format(
                                    checkboxText,
                                    (treePrefix + 'checkbox') + (record.get('checked') ? ' ' + treePrefix + 'checkbox-checked' : ''),
                                    record.get('checked') ? 'aria-checked="true"' : ''
                                ));
                                if (record.get('checked')) {
                                    metaData.tdCls += (' ' + treePrefix + 'checked');
                                }
                            }
                            if (record.isLast()) {
                                if (record.isExpandable()) {
                                    buf.unshift(format(imgText, (elbowPrefix + 'end-plus ' + expanderCls), Ext.BLANK_IMAGE_URL));
                                } else {
                                    buf.unshift(format(imgText, (elbowPrefix + 'end'), Ext.BLANK_IMAGE_URL));
                                }

                            } else {
                                if (record.isExpandable()) {
                                    buf.unshift(format(imgText, (elbowPrefix + 'plus ' + expanderCls), Ext.BLANK_IMAGE_URL));
                                } else {
                                    buf.unshift(format(imgText, (treePrefix + 'elbow'), Ext.BLANK_IMAGE_URL));
                                }
                            }
                        } else {
                            if (record.isLast() || record.getDepth() === 0) {
                                buf.unshift(format(imgText, (elbowPrefix + 'empty'), Ext.BLANK_IMAGE_URL));
                            } else if (record.getDepth() !== 0) {
                                buf.unshift(format(imgText, (elbowPrefix + 'line'), Ext.BLANK_IMAGE_URL));
                            }
                        }
                    }
                }
                record = record.parentNode;
            }
            if (href) {
                buf.push('<a href="', href, '" target="', target, '">', formattedValue, '</a>');
            } else {
                buf.push(formattedValue);
            }
            if (cls) {
                metaData.tdCls += ' ' + cls;
            }
            return buf.join('');
        };
        this.callParent(arguments);
    },

    defaultRenderer: function (value) {
        return value;
    }
});
//地图基础控件
Ext.define('Jit.window.MapWindow', {
    alias: 'widget.jitmapwindow',
    config: {
        jitPoint: null
    },
    constructor: function (args) {
        if (args.id == null || args.id == "") {
            args.id = "__MapID";
        }
        var defaultConfig = {
            jitSize: "custom",
            title: 'Map',
            width: 700,
            height: 450,
            constrain: true,
            modal: true,
            resizable: true,
            inGPSType: 0,
            html: '<iframe  height="100%"  marginheight="0" marginwidth="0" scrolling="no"  frameborder="no"  id="' + args.id + 'frmFlashMap"  width="100%" src="/Lib/MapFlash/index.html?config=mapconfig.xml&MapWindowID=' + args.id + '"></iframe>'
        }
        var defaultPoint = {
            pointID: '0',           //[StoreID] 整数，唯一标识，必须
            lng: '0',               //[Lng] 浮点数，商店GPS坐标的经度，必须，范围0-180.
            lat: '0',               //[Lat] 浮点数，商店GPS坐标的纬度，必须，范围0-90
            icon: 'g.png',          //[Icon] 图片样式，g.png绿色点（gs.png选择后），b.png蓝色点（bs.png选择后），o.png橙色点（os.png选择后），必须
            isEditable: false,      //[IsEdit] 是否可拖拽
            insideText: '',         //[LabelID] 图片上放的文字
            pointTitle: '',         //[LabelContent] 图片边上文字
            pointInfoHeight: '0',   //[PopInfoHeight] 弹出框长度
            pointInfoWidth: '0',    //[PopInfoWidth] 弹出框宽度
            tips: '',               //[Tips] 字符串，鼠标悬停到点上时显示的文本信息
            pointInfo: new Array(),  //[PopInfo] 门店信息,必须 {"title":"客户名称","value":"坂田医院","type":"1" }type=1为文本、type=2为图片、type=3为按钮,type=4 为iframe
            mapScale: 0              //地图等级
        };
        args.jitPoint = Ext.applyIf(args.jitPoint || {}, defaultPoint);
        args = Ext.applyIf(args, defaultConfig);
        if (args.id != null && args.id != "") {
            if (Ext.getCmp(args.id) != null) {
                Ext.getCmp(args.id).destroy();
            }
            var instance = Ext.create('Jit.window.Window', args);
            /*
            获取地图对象
            */
            instance.getFlashMapObject = function () {
                var frame = window.frames[args.id + "frmFlashMap"];
                return frame;
            }

            /*
            Flash地图是否加载完毕
            */
            instance._map_InitMap = function () {
                //var frame = instance.getFlashMapObject();
                //frame.index._map_SetMapScale(this.jitPoint.mapScale);
                this._map_LoadMap();
            }

            instance._map_LoadMap = function () {

                if (this.jitPoint.lng > 0 && this.jitPoint.lat > 0) {
                    if (this.jitPoint.inGPSType == 1) {
                        var xy = instance._map_XYGpsChange(this.jitPoint.lng, this.jitPoint.lat);
                        this.jitPoint.lng = xy.split(',')[0];
                        this.jitPoint.lat = xy.split(',')[1];
                    }
                    var frame = instance.getFlashMapObject();
                    if (frame != null && frame.index != null) {
                        frame.index._map_RemoveStores("");
                        frame.index._map_AddStores(instance.jitGetPoint(), true);
                        var p = 0;
                        if (this.jitPoint.mapScale != null && this.jitPoint.mapScale > 0) {
                            p = this.jitPoint.mapScale;
                        }
                        if (p == 0) {
                            p = frame.index._map_GetMapScale();
                            if (p <= 4) {
                                p = 15;
                            }
                        }
                        if (p == 0) {
                            frame.index._map_MoveToStore(this.jitPoint.pointID);
                        } else {
                            frame.index._map_MoveToStoreByScale(this.jitPoint.pointID, p)
                        }
                    }
                }
            }
            /*
            地图单击事件,重新调用            
            */
            instance._map_OnClick = function (pLng, pLat) {
                if (this.jitPoint.isEditable) {
                    this.jitPoint.lng = pLng;
                    this.jitPoint.lat = pLat;
                    this._map_LoadMap();
                }
            }
            /*
            地图修改事件，影响
            */
            instance._map_Update = function () {
                if (args.handler != null) {
                    args.handler(this.jitPoint);
                }
            }

            /*
            设置数据  数据格式 {"Lat":123.32,"Lng":32.12,"Type":1}
            */
            instance.jitSetValue = function (pValue) {
                if (pValue != null && pValue != "") {
                    var Values = pValue.split(",");
                    if (Values.length > 1) {
                        if (this.jitPoint.isEditable) {
                            this.jitPoint.lng = Values[0];
                            this.jitPoint.lat = Values[1];
                            this._map_LoadMap();
                        }
                    }
                } else {
                    this.jitPoint.lng = "";
                    this.jitPoint.lat = "";
                    this._map_LoadMap();
                }
            }

            /*
            获取数据
            */
            instance.jitGetValue = function () {
                return this.jitPoint;
            }
            /*
            要素编辑事件
            */
            instance._map_Graphic_MoveEdit = function (pObj) {
                if (this.jitPoint.isEditable) {
                    this.jitPoint.lng = pObj.Lng;
                    this.jitPoint.lat = pObj.Lat;
                    this._map_LoadMap();
                }
            }

            /*
            Flash地图清除点
            */
            instance._map_RemoveStores = function () {
                var frame = instance.getFlashMapObject();
                if (frame != null && frame.index != null && frame.index._map_RemoveStores != null) {
                    frame.index._map_RemoveStores("");
                    frame._map_RemoveTitle();
                    this.jitPoint.lng = "";
                    this.jitPoint.lat = "";
                    this._map_LoadMap();
                }
            }

            /*
            Flash地图百度转换谷歌坐标
            */
            instance._map_XYChange = function (lng, lat) {
                var frame = instance.getFlashMapObject();
                var xy = frame.index._map_XYChange(lng, lat, 1);
                return xy;
            }
            /*gps 转google*/
            instance._map_XYGpsChange = function (lng, lat) {

                var frame = instance.getFlashMapObject();
                var xy = frame.index._map_XYChange(lng, lat, 3);
                return xy;
            }
            /*
            Flash地图清空查询数据
            */
            instance._map_RemoveTitle = function (lng, lat) {
                var frame = instance.getFlashMapObject();
                frame.index._map_RemoveTitle();
            }

            /*Flash地图所需要的Json 字符串,
            @return string类型的Json
            */
            instance.jitGetPoint = function () {
                var Point = new Object();
                Point.StoreID = this.jitPoint.pointID;
                Point.Lng = this.jitPoint.lng;
                Point.Lat = this.jitPoint.lat;
                Point.Icon = this.jitPoint.icon;
                Point.IsAssigned = "true";  //默认为true
                Point.IsEdit = this.jitPoint.isEditable;
                Point.LabelID = this.jitPoint.insideText;
                Point.LabelContent = this.jitPoint.pointTitle;
                if (this.jitPoint.pointInfoHeight > 0) {
                    Point.PopInfoHeight = this.jitPoint.pointInfoHeight;
                }
                if (this.jitPoint.pointInfoWidth > 0) {
                    Point.PopInfoWidth = this.jitPoint.pointInfoWidth;
                }
                Point.Tips = this.jitPoint.tips;
                if (this.jitPoint.pointInfo != null && this.jitPoint.pointInfo.toString() != "") {
                    Point.PopInfo = [this.jitPoint.pointInfo];
                }
                var PointArray = new Array();
                PointArray.push(Point);
                var PointJson = Ext.JSON.encode(PointArray);
                return PointJson;
            }
            return instance;
        } else {
            return null;
        }
    }
});
function __fnGetImg(m, type, id, value) {

    pObjectValue = eval(decodeURIComponent(value));
    var n = parseInt(document.getElementById("__sp_" + id).innerHTML) - 1;

    if (type == 1) {
        n = n - 1;
    }
    else {
        n = n + 1;
    }
    if (n >= m) {
        n = 0;
    }
    if (n < 0) {
        n = m - 1;
    }
    document.getElementById("__img" + id).src = '/File/MobileDevices/Photo/' + __clientid + '/' + pObjectValue[n].ClientUserID + '/'
             + pObjectValue[n].FileName;
    document.getElementById("__sp_" + id).innerHTML = (n + 1);
}
Ext.define('Jit.window.PhotoWindow', {
    alias: 'widget.jitphotowindow',

    constructor: function (args) {
        var me = this;
        var defaultConfig = {
            id: '__PhotoWindowID'   //panel 的id 默认为mapSelect          
            , renderTo: null //panel的renderTo          
            , photoTitle: '照片查看'
            , pClientID: 0
            , pClientUserID: 0
            , pObjectValue: ''
        }
        args = Ext.applyIf(args, defaultConfig);
        if (__clientid != null) {
            args.pClientID = __clientid;
        }
        if (args.value != null && args.value != '') {
            me.pObjectValue = eval(decodeURIComponent(args.value));
        }
        //照片窗体控件
        //照片panel
        me.photoPanelImg = Ext.create('Ext.panel.Panel', {
            width: 490,
            height: 325,
            columnWidth: 1,
            html: "<div style='width:488px;height:295px; text-align:center;padding-top:5px'>" +
            "<img id='__img" + args.id + "' style='max-width:480px;max-height:280px' src='" +
             "/File/MobileDevices/Photo/" + args.pClientID + "/" + me.pObjectValue[0].ClientUserID + "/"
             + me.pObjectValue[0].FileName + "'></div><div  style='width:488px;height:20px; text-align:center;'>" +
             "<a href='javascript:void(0)' onclick='__fnGetImg(" + me.pObjectValue.length + ",1,\"" + args.id + "\",\"" + args.value + "\")' >上一张</a>  <span id='__sp_" + args.id + "'>1</span>/" + me.pObjectValue.length + "  <a href='javascript:void(0)' onclick='__fnGetImg(" + me.pObjectValue.length + ",2,\"" + args.id + "\",\"" + args.value + "\")'>下一张</a></div>",
            layout: 'column',
            border: 0
        });

        if (Ext.getCmp(args.id) != null) {
            Ext.getCmp(args.id).destroy();
        }
        //上传图片的window
        var instance = Ext.create('Jit.window.Window', {
            id: args.id,
            title: args.photoTitle,
            items: [me.photoPanelImg],
            width: 500,
            height: 345,
            jitSize: "custom",
            constrain: true,
            modal: true
        });
    }
});
Ext.define('Jit.window.Window', {
    extend: 'Ext.window.Window'
    , alias: 'widget.jitwindow'
    , config: {
        /*
        @size   尺寸有small,big,large
        */
        jitSize: 'small'
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            closeable: true
            , closeAction: 'hide'
            , modal: 'true'
            , resizable: false
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var jitSize = cfg.jitSize.toString().toLowerCase();
            switch (jitSize) {
                case 'small':
                    {
                        defaultConfig.width = 300;
                        defaultConfig.height = 150;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 680;
                        defaultConfig.height = 250;
                    }
                    break;
                case 'large':
                    {
                        defaultConfig.width = 900;
                        defaultConfig.height = 400;
                    }
                    break;
            }
        }
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
});