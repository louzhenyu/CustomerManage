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
        /*选择事件*/
        , onSelect: null
        /*树picker的配置项*/
        , pickerCfg: {
            Height: 300
            , maxHeight: 300
        }

    }
    /*私有字段*/
    /*当前面板内已经选中的项*/
    , selectedValues: new Array()
    /*初始值,初始值可能不在当前面板内*/
    , initValues: new Array()
    /*是否已经创建picker*/
    , hasCreatedPicker: false
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
        //创建树面板
        var tree = Ext.create('Ext.tree.Panel', treeCfg);
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
                Ext.Error.raise('pSelectedItems数组中的每个元素都必须包含id和text属性.');
            }
            items.push({ id: item.id, text: item.text });
        }
        //Ext.data.TreeStore一旦创建后，会自动向后台发送请求，获取节点数据
        //因此，为了避免对initValues的并发访问,分2种情况进行处理
        if (this.hasCreatedPicker == false) {//如果未调用过createPicker方法
            this.initValues = items;
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
                ids.push(selectedValues[i]["id"]);
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
            //            if (type) {
            //                picker.getView().onCheckChange(record); //点击选项时将勾选框勾选上
            //            }

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