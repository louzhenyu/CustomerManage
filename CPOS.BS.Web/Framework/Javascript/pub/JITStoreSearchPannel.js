//Jit.biz.DynamicSearchPanel
Ext.define('Jit.panel.JITStoreSearchPannel', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.JITStoreSearchPannel',
    config: {
        showPannel: null,
        btnPannel: null,
        grid: null,
        ajaxPath: '/Module/BasicData/Store/Handler/StoreHandler.ashx',
        btnSearch: null,
        id: null,
        BtnCode: null,
        btnMoreSearch: null
    }, constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //显示业务控件数据
        //自己的配置项处理
        Ext.Ajax.request({
            url: cfg.ajaxPath,
            params: {
                btncode: cfg.BtnCode,
                method: 'QueryView'
            },
            method: 'POST',
            async: false,
            callback: function (options, success, response) {
                //获取数据
                var json = Ext.JSON.decode(response.responseText);//返回的查询条件的信息
                me.btnSearch = Ext.create('Jit.button.Button', {
                    text: '查询',
                    jitIsHighlight: false,
                    jitIsDefaultCSS: true,
                    margin: '0 0 0 0',
                    isImgFirst: true,
                    handler: function () { me.fnSearch(me); }//查询操作
                });
                me.btnReset = Ext.create('Jit.button.Button', {
                    margin: '0 0 0 0',
                    text: '重置',
                    jitIsHighlight: false,
                    jitIsDefaultCSS: true,
                    handler: function () { me.fnReset(me); }//重置操作
                });
                var btnhidden = true;
                if (json.length > 4) btnhidden = false;
                cfg.btnMoreSearch = Ext.create('Jit.button.Button', {
                    text: "更多",
                    imgName: 'open',
                    jitIsHighlight: false,
                    jitIsDefaultCSS: true,
                    margin: '10 0 0 0',
                    show: true,
                    hidden: btnhidden,
                    handler: function () {
                        me.fnMoreSearchView(me, cfg.btnMoreSearch);//展示更多查询条件
                    }
                });
                var hidden = false;
                var lshowitem = new Array();//创建一个数组
                var hideni = 0;
                for (var i = 0; i < json.length; i++) {
                    if (i > 3) hidden = true;         //从第四个开始隐藏
                    switch (json[i].ControlType) {
                        case 1:
                            lshowitem.push({
                                xtype: 'jittextfield',
                                jitSize: 'small',
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,//关联性
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        case 2: //整型文本
                            lshowitem.push({
                                xtype: 'jitnumberfield',
                                jitSize: 'small',
                                allowDecimals: false,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden

                            });
                            break;
                        case 3: //数字文本
                            lshowitem.push({
                                xtype: 'jitnumberfield',
                                allowDecimals: true,
                                jitSize: 'small',
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden

                            });
                            break;
                        case 4: //日期
                            lshowitem.push({
                                xtype: 'jitdatefield',
                                jitSize: 'small',
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        case 6: //自定义下拉
                            lshowitem.push({
                                xtype: 'jitbizoptions',
                                jitSize: 'small',
                                OptionName: json[i].CorrelationValue,
                                isDefault: false,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                multiSelect: true,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        case 7: //城市
                            lshowitem.push({
                                xtype: 'jitbizcityselecttree',
                                jitSize: 'small',
                                isSelectLeafOnly: false,
                                isDefault: true,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        case 201: //等级
                            lshowitem.push({
                                xtype: 'jitbizviplevel',
                                jitSize: 'small',
                                isDefault: true,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden,
                                multiSelect: true
                            });
                            break;
                        case 202: //来源
                            lshowitem.push({
                                xtype: 'jitbizvipsource',
                                multiSelect: true,
                                jitSize: 'small',
                                isDefault: false,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        case 203: //门店选择
                            lshowitem.push({
                                xtype: 'jitbizunitselecttree',
                                jitSize: 'small',
                                isDefault: false,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: true,
                                multiSelect: false,
                                hidden: hidden
                            });
                            break;
                        case 204: //标签
                            lshowitem.push({
                                xtype: 'jitbiztags',
                                jitSize: 'small',
                                isDefault: false,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: true,
                                multiSelect: true,
                                hidden: hidden
                            });
                            break;
                        case 301:
                            lshowitem.push({
                                xtype: 'jitbizoptions',
                                jitSize: 'small',
                                OptionName: json[i].CorrelationValue,
                                isDefault: true,
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                multiSelect: false,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                        default:
                            lshowitem.push({
                                xtype: 'jittextfield',
                                jitSize: 'small',
                                fieldLabel: json[i].fieldLabel,
                                ControlName: json[i].ControlName,
                                ControlType: json[i].ControlType,
                                id: cfg.id + '_' + json[i].ControlName,
                                CorrelationValue: json[i].CorrelationValue,
                                IsMoreRegard: json[i].IsMoreRegard,
                                hidden: hidden
                            });
                            break;
                    }
                }
                me.showPannel = Ext.create('Ext.panel.Panel', {
                    items: lshowitem,//把上面含有控件信息的数组设为showPannel的一个属性
                    margin: '10 0 0 0',
                    layout: 'column',
                    width: 815,
                    border: 0
                });
                me.btnPannel = Ext.create('Ext.panel.Panel', {
                    items: [me.btnSearch, {
                        width: 100,
                        cls: 'panel_searchLeft',
                        items: [me.btnReset],
                        margin: '0 0 0 0',
                        layout: 'column',
                        height: 25,
                        border: 0
                    }],
                    margin: '0 0 10 0',
                    cls: 'panel_searchLeft',
                    layout: 'column',
                    width: 815,
                    height: 25,
                    border: 0
                });
                defaultConfig.items = [me.showPannel, cfg.btnMoreSearch, me.btnPannel];
            }
        });
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    fnSearch: function (me) {
      
        //var n=new Array();
        //n.push(11);
        //n.push(22);
        //n.push(33);
        //console.log(n.length);
        //console.log(n.items.length);

        var l = new Array();
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var c = me.showPannel.items.items[i];
            //判断要不要把状态给特殊化一下，因为状态里取消是0
           // alert(c.fieldLabel + c.jitGetValue());//看看标签
            if (c.jitGetValue() != null && c.jitGetValue() != "") {//这里把0给过滤掉了 && c.jitGetValue() != "0",如果影响其他，就只对状态使用放开0的过滤
                var o = new Object();
                var searchValue = '';
                o.ControlType = c.ControlType;
                o.ControlValue = me.fnGetSearchValue(c.jitGetValue());
                o.ControlName = c.ControlName;
                o.CorrelationValue = c.CorrelationValue;
                o.IsMoreRegard = c.IsMoreRegard;
                l.push(o);
            }
        }
        me.grid.searchConditions = Ext.JSON.encode(l)//在这里给searchConditions赋值
        if (me.grid.CheckStore != null) me.grid.CheckStore.removeAll();
        if (me.grid.selModel.jitClearValue != undefined) me.grid.selModel.jitClearValue();
        me.grid.pagebar.moveFirst();
    },
    fnGetSearchValue: function (value) {
        if (Object.prototype.toString.call(value) == '[object Array]') {
            var a = '';
            for (var i = 0; i < value.length; i++) {
                if (i == 0) {
                    a = value[i].toString();
                }
                else {
                    a = a + ',' + value[i].toString();
                }
            }
            return a;
        }
        else
            return value;
    },
    fnReset: function (me) {
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var c = me.showPannel.items.items[i];
            if (me.showPannel.items.items[i].ControlType == 7) {
                c.jitSetValue([{ id: "", text: ""}]);
            }
            else {
                c.jitSetValue("");
            }
        }
    },
    jitSetValue: function (json) {
        var me = this;
        me.btnMoreSearch.show = true;
        me.fnMoreSearchView(me, me.btnMoreSearch);//还要根据btnMoreSearch的设置显示与否来决定下面添加的条件的值得数量
        for (var i = 0; i < json.length; i++) {
            var c = me.fnGetCol(me, json[i].ControlName);
            c.jitSetValue(json[i].ControlValue);
        }
    },
    fnGetCol: function (me, pName) {
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            if (o.ControlName == pName) {//根据控件名获取控件
                return o;
            }
        }
    },
    fnMoreSearchView: function (me, btn, b) {
        if (btn.show == true) {
            for (var i = 0; i < me.showPannel.items.items.length; i++) {
                var c = me.showPannel.items.items[i];
                c.setVisible(true);
            }
            btn.setText('收起');
            btn.removeCls("opentwo");
            btn.addCls("closetwo");
            btn.show = false;
        }
        else {
            for (var i = 4; i < me.showPannel.items.items.length; i++) {//从第四个开始隐藏
                var c = me.showPannel.items.items[i];
                c.setVisible(false);
                c.jitSetValue("");
            }
            btn.setText('更多');
            btn.removeCls("closetwo");
            btn.addCls("opentwo");
            btn.show = true;
        }
    }
});