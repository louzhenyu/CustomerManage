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
