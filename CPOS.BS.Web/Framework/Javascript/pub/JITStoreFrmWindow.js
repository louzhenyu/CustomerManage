//Jit.biz.DynamicEditForm
Ext.define('Jit.window.JITStoreFrmWindow', {
    extend: 'Jit.window.Window',
    alias: 'widget.JITStoreFrmWindow',
    config: {
        showPannel: null,
        btnPannel: null,
        BtnCode: null,
        action: null,
        IsInit: null,
        ajaxPath: null,
        KeyValue: null,
        grid: null,
        id: null,
        clientDistributorID: null,
        tableName: null,
        btnSave: null
    }, constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //显示业务控件数据
        //自己的配置项处理
        //   
        var isclientDistributorID = false;
        if (cfg.clientDistributorID == null || cfg.clientDistributorID == 0)
            isclientDistributorID = false;
        else
            isclientDistributorID = true;
        if (cfg.BtnCode != 'delete')
            Ext.Ajax.request({
                url: cfg.ajaxPath,
                params: {
                    btncode: cfg.BtnCode,
                    method: 'EditView'
                },
                method: 'POST',
                async: false,
                callback: function (options, success, response) {
                    //获取数据
                    var json = Ext.JSON.decode(response.responseText);

                    cfg.btnSave = Ext.create('Jit.button.Button', {
                        text: "保存",
                        componentCls: 'jitbutton',
                        handler: function () { me.fnSubmit(me); }
                    });
                    if (cfg.BtnCode == "search") cfg.btnSave.setDisabled(true);
                    var btnClose = Ext.create('Jit.button.Button', {
                        text: "关闭",
                        componentCls: 'jitbutton',
                        handler: function () { me.close(); }
                    });

                    var lshowitem = new Array();
                    for (var i = 0; i < json.length; i++) {
                        var strIsMustDo = '';
                        if (json[i].IsMustDo == 1) strIsMustDo = '<font color=red>*</font>'
                        switch (json[i].ControlType) {
                            case 1:
                                lshowitem.push({
                                    xtype: 'jittextfield',
                                    jitSize: 'big',
                                    width: 300,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 2: //整型文本
                                lshowitem.push({
                                    xtype: 'jitnumberfield',
                                    jitSize: 'big',
                                    width: 300,
                                    allowDecimals: false,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 3: //数字文本
                                lshowitem.push({
                                    xtype: 'jitnumberfield',
                                    allowDecimals: true,
                                    jitSize: 'big',
                                    width: 300,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 4: //日期
                                lshowitem.push({
                                    xtype: 'jitdatefield',
                                    jitSize: 'big',
                                    width: 300,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 6: //自定义下拉
                                lshowitem.push({
                                    xtype: 'jitbizoptions',
                                    jitSize: 'big',
                                    width: 300,
                                    OptionName: json[i].CorrelationValue,
                                    isDefault: true,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 7: //自定义层系
                                lshowitem.push({
                                    xtype: 'jitbizhierarchyitem',
                                    jitSize: 'big',
                                    width: 300,
                                    HierarchID: json[i].CorrelationValue,
                                    isDefault: true,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 27: //省
                                lshowitem.push({
                                    xtype: 'jitbizprovince',
                                    jitSize: 'small',
                                    width: 300,
                                    HierarchID: json[i].CorrelationValue,
                                    isDefault: true,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    id: cfg.id + '_' + json[i].ControlName,
                                    storeId: cfg.id + '_' + json[i].ControlName + '_store',
                                    CityID: cfg.id + '_CityID',
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 28: //市
                                lshowitem.push({
                                    xtype: 'jitbizcity',
                                    jitSize: 'small',
                                    isDefault: true,
                                    width: 300,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    id: cfg.id + '_' + json[i].ControlName,
                                    storeId: cfg.id + '_' + json[i].ControlName + '_store',
                                    DistrictID: cfg.id + '_' + 'DistrictID',
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                //设定市初始化状态
                                break;
                            case 29: //县
                                lshowitem.push({
                                    xtype: 'jitbizdistrict',
                                    jitSize: 'small',
                                    width: 300,
                                    storeId: cfg.id + '_' + json[i].ControlName + '_store',
                                    alertLabel: json[i].fieldLabel,
                                    isDefault: true,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    id: cfg.id + '_' + json[i].ControlName,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 30: //地图
                                lshowitem.push({
                                    xtype: 'jitbizmapselect',
                                    text: '选择',
                                    width: 300,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    id: cfg.id + '_' + json[i].ControlName,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                            case 203: //门店选择
                                lshowitem.push({ xtype: 'jitbizunitselecttree',
                                    jitSize: 'big',
                                    width: 260,
                                    isSelectLeafOnly: false,
                                    multiSelect: json[i].IsMoreRegard == 1 ? true : false,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat
                                });
                                break;
                        }
                    }
                    var showHeight = 34;
                    var f = parseInt(json.length / 2) + json.length % 2;
                    if (f > 15) f = 15;
                    showHeight = showHeight * f;
                    me.showPannel = Ext.create('Ext.panel.Panel', {
                        items: lshowitem,
                        bodyStyle: 'background:#F1F2F5;padding-top:5px;',
                        layout: 'column',
                        height: showHeight,
                        border: 0,
                        autoScroll: true
                    });

                    defaultConfig.height = showHeight + 80;

                    defaultConfig.width = 680;
                    defaultConfig.bodyStyle = 'background:#F1F2F5;';
                    defaultConfig.items = [me.showPannel];
                    defaultConfig.buttons = [cfg.btnSave, btnClose];
                }
            });
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    //提交数据
    fnSubmit: function (me) {
        if (me.fnCheck(me) == false) {
            return false;
        }
        var l = new Array();
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var c = me.showPannel.items.items[i];
            if (c.jitGetValue(c) != null && c.jitGetValue(c) != "") {
                var o = Object();
                o.ControlType = c.ControlType;
                o.ControlValue = c.jitGetValue();
                o.ControlName = c.ControlName;
                o.CorrelationValue = c.CorrelationValue;
                l.push(o);
            }
        }
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "提交数据...." });
        myMask.show();
        Ext.Ajax.request({
            url: me.ajaxPath,
            params: {
                method: me.action,
                btncode: me.BtnCode,
                pEditValue: Ext.JSON.encode(l),
                pKeyValue: me.KeyValue
            },
            method: 'POST',
            callback: function (options, success, response) {
                myMask.hide();
                if (success == true) {
                    Ext.Msg.alert("提示", '保存成功');
                    me.close();
                    if (me.action == "create")
                        me.grid.pagebar.moveFirst();
                    else
                        me.grid.pagebar.doRefresh();
                }
                else {
                    Ext.Msg.alert("提示", '保存失败');
                }
                me.close();
            }
        });
    },
    fnDelete: function (me) {
        Ext.MessageBox.confirm('提示信息', '确认删除?', function deldbconfig(btn) {
            if (btn == 'yes')
                Ext.Ajax.request({
                    url: me.ajaxPath,
                    params: {
                        btncode: me.BtnCode,
                        method: "delete",
                        pKeyValue: me.KeyValue
                    },
                    method: 'POST',
                    callback: function (options, success, response) {
                        if (success == true) {
                            Ext.Msg.alert("提示", '删除成功');
                            me.grid.pagebar.doRefresh();
                        }
                        else {
                            Ext.Msg.alert("提示", '删除失败');

                        }
                    }
                });
        });
    },
    fnShowAdd: function (me) {
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            o.jitSetValue("");
        }
        me.action = "create";
        me.btnSave.setDisabled(false);
        me.KeyValue = "";
        me.show();
    },
    fnShowUpdate: function (me) {
        if (me.action == "search") {
            me.btnSave.setDisabled(true);
        }
        else {
            me.btnSave.setDisabled(false);
        }
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "获取数据...." });
        myMask.show();
        Ext.Ajax.request({
            url: me.ajaxPath, params: {
                btncode: me.action,
                method: "EditViewData",
                pKeyValue: me.KeyValue
            },
            method: 'POST',
            callback: function (options, success, response) {
                var json = Ext.JSON.decode(response.responseText);
                for (var i = 0; i < json.length; i++) {
                    var c = me.fnGetCol(me, json[i].ControlName);
                    if (parseInt(c.ControlType) == 203) {
                        c.jitSetValue([{ "id": json[i].ControlValue.split('|')[0], "text": json[i].ControlValue.split('|')[1]}]);
                    } else {
                        c.jitSetValue(json[i].ControlValue);
                    }
                }
                me.show();
                myMask.hide();
            }
        });
    },
    fnGetCol: function (me, pName) {
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            if (o.ControlName == pName) {
                return o;
            }
        }
    },
    fnCheck: function (me) {
        //检查必填字段
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            if (o.IsMustDo == 1)
                if (o.jitGetValue(me) == "") {
                    Ext.Msg.alert("提示", o.alertLabel + '不能为空');
                    return false;
                }
        }
        // 检查单个重复
        var IsRepeatArray = new Array();
        var isCheck = true;
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            if (o.IsRepeat == 1) {
                var c = new Object();
                c.ControlType = o.ControlType;
                c.ControlValue = o.jitGetValue();
                c.ControlName = o.ControlName;
                c.CorrelationValue = o.CorrelationValue;
                IsRepeatArray = new Array();
                IsRepeatArray.push(c);
                var p = Ext.JSON.encode(IsRepeatArray);
                Ext.Ajax.request({
                    url: me.ajaxPath,
                    params: {
                        btncode: me.action,
                        method: "CheckIsRepeat",
                        pKeyValue: me.KeyValue,
                        pSearch: Ext.JSON.encode(IsRepeatArray)
                    },
                    method: 'POST',
                    async: false,
                    callback: function (options, success, response) {
                        var RowCount = parseInt(response.responseText);
                        if (RowCount > 0) {
                            Ext.Msg.alert("提示", o.alertLabel + '数据中有重复值');
                            isCheck = false;
                        }
                    }
                });
            }
        }
        if (isCheck == false) return false;

        //检查多个重复
        IsRepeatArray = new Array(); //多个重复
        for (var i = 0; i < me.showPannel.items.items.length; i++) {
            var o = me.showPannel.items.items[i];
            if (o.IsRepeat == 2) {
                var c = new Object();
                c.ControlType = o.ControlType;
                c.ControlValue = o.jitGetValue(c);
                c.ControlName = o.ControlName;
                c.CorrelationValue = o.CorrelationValue;
                c.alertLabel = o.alertLabel;
                IsRepeatArray.push(c);
            }
        }
        if (IsRepeatArray.length > 0) {
            Ext.Ajax.request({
                url: me.ajaxPath,
                params: {
                    btncode: me.action,
                    method: "CheckIsRepeat",
                    pKeyValue: me.KeyValue,
                    pSearch: Ext.JSON.encode(IsRepeatArray)
                },
                method: 'POST',
                async: false,
                callback: function (options, success, response) {
                    var RowCount = parseIn(response.responseText);
                    if (RowCount > 0) {
                        var msg = '';
                        for (var i = 0; i < IsRepeatArray.length; i++) {
                            msg = msg + '[' + IsRepeatArray[i].alertLabel + ']';
                        }
                        Ext.Msg.alert("提示", msg + '数据有重复值');
                        isCheck = false;
                    }
                }
            });
        }
        if (isCheck == false) return false;
        return true;
    }
});