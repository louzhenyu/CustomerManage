var tabState = {
    tab1: true,
    tab2: false,
    tab3: false,
    tab4: false,
    tab5: false,
    tabUrl: "/Module/Vip/VipSearch/VipEdit.aspx",
    keyValue: ""
}

Ext.define('Jit.window.JITVipFrmWindow', {//Jit.biz.DynamicEditForm
    extend: 'Jit.window.Window',
    alias: 'widget.JITVipFrmWindow',
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
    },
    constructor: function (cfg) {
        var defaultConfig = {};
        var me = this;
        //显示业务控件数据
        //自己的配置项处理
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
                    var lshowitem = new Array();
                    for (var i = 0; i < json.length; i++) {
                        var strIsMustDo = '';
                        if (json[i].IsMustDo == 1) strIsMustDo = '<font color=red>*</font>'
                        switch (json[i].ControlType) {
                            case 41:
                            default:
                            case 1: //文本
                                lshowitem.push({
                                    xtype: 'jittextfield',
                                    jitSize: 'big',
                                    readOnly: json[i].IsRead == 1 ? true : false,
                                    width: 260,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat,
                                    MaxLength: json[i].MaxLength,
                                    MinLength: json[i].MinLength
                                });
                                break;
                            case 2: //整型文本
                                lshowitem.push({
                                    xtype: 'jitnumberfield',
                                    jitSize: 'big',
                                    width: 260,
                                    allowDecimals: false,
                                    readOnly: json[i].IsRead == 1 ? true : false,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat,
                                    MaxLength: json[i].MaxLength,
                                    MinLength: json[i].MinLength
                                });
                                break;
                            case 3: //数字文本
                                lshowitem.push({
                                    xtype: 'jitnumberfield',
                                    allowDecimals: true,
                                    readOnly: json[i].IsRead == 1 ? true : false,
                                    jitSize: 'big',
                                    width: 260,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat,
                                    MaxLength: json[i].MaxLength,
                                    MinLength: json[i].MinLength
                                });
                                break;
                            case 4: //日期
                                lshowitem.push({
                                    xtype: 'jitdatefield',
                                    jitSize: 'big',
                                    readOnly: json[i].IsRead == 1 ? true : false,
                                    id: '__Time_' + json[i].ControlName,
                                    width: 260,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat,
                                    editable: true
                                });
                                break;
                            case 6: //自定义下拉
                                lshowitem.push({
                                    xtype: 'jitbizoptions',
                                    jitSize: 'big',
                                    width: 260,
                                    readOnly: json[i].IsRead == 1 ? true : false,
                                    OptionName: json[i].CorrelationValue,
                                    fieldLabel: strIsMustDo + json[i].fieldLabel,
                                    alertLabel: json[i].fieldLabel,
                                    ControlName: json[i].ControlName,
                                    ControlType: json[i].ControlType,
                                    CorrelationValue: json[i].CorrelationValue,
                                    IsMustDo: json[i].IsMustDo,
                                    IsRepeat: json[i].IsRepeat,
                                    isDefault: json[i].IsMoreRegard == 1 ? false : true,
                                    multiSelect: json[i].IsMoreRegard == 1 ? true : false
                                });
                                break;
                            case 7: //城市
                                lshowitem.push({
                                    xtype: 'jitbizcityselecttree',
                                    jitSize: 'big',
                                    width: 260,
                                    readOnly: json[i].IsRead == 1 ? true : false,
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
                            case 30: //地图
                                lshowitem.push({ xtype: 'jitbizmapselect',
                                    text: '选择',
                                    width: 260,
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

                    me.showPannel = Ext.create('Jit.biz.WindowPanel', {
                        id: 'WindowPanelID',
                        width: 900, //必须值宽
                        fieldItems: lshowitem, //文本框，下拉的对象集合
                        buttonSave: function () { me.fnSubmit(me); },   //保存方法
                        buttonHidden: false, //保存操作按钮是否隐藏
                        fieldCount: 3  //显示数量（文本框控件）
                    });
                    var tab = Ext.widget('tabpanel', {
                        width: "100%",
                        height: "100%",
                        activeTab: 0,
                        items: [{
                            title: '会员积分变更记录', id: "tabpanel1", contentEl: "tab1", height: 500, border: 0,
                            listeners: {
                                load: {
                                    element: 'el',
                                    fn: function () {
                                    }
                                },
                                render: function () {
                                },
                                activate: function () {
                                    document.getElementById("tab1").src = tabState.tabUrl + "?vip_id=" + tabState.keyValue + "&type=1";
                                }
                            }
                        }, {
                            title: '门店消费记录',
                            id: "tabpanel2",
                            contentEl: "tab2",
                            height: 500,
                            border: 0,
                            listeners: {
                                load: {
                                    element: 'el',
                                    fn: function () {
                                    }
                                },
                                render: function () {
                                },
                                activate: function () {
                                    document.getElementById("tab2").src = tabState.tabUrl + "?vip_id=" + tabState.keyValue + "&type=2";
                                }
                            }
                        }, {
                            title: '下线清单积分',
                            id: "tabpanel3",
                            contentEl: "tab3",
                            height: 500,
                            border: 0,
                            listeners: {
                                load: {
                                    element: 'el',
                                    fn: function () {
                                    }
                                },
                                render: function () {
                                },
                                activate: function () {
                                    document.getElementById("tab3").src = tabState.tabUrl + "?vip_id=" + tabState.keyValue + "&type=3";
                                }
                            }
                        }, {
                            title: '会员标签',
                            id: "tabpanel5",
                            contentEl: "tab5",
                            height: 500,
                            border: 0,
                            listeners: {
                                load: {
                                    element: 'el',
                                    fn: function () {
                                    }
                                },
                                render: function () {
                                },
                                activate: function () {
                                    document.getElementById("tab5").src = tabState.tabUrl + "?vip_id=" + tabState.keyValue + "&type=5";
                                }
                            }
                        }]
                    });
                    //TabPanel
                    Ext.create('Ext.form.Panel', {
                        id: 'TabPannel',
                        items: tab,
                        margin: '0 0 0 0',
                        height: 347,
                        width: 900,
                        style: 'border-top:1px solid #C2C3C8;background:#FFFFFF',
                        layout: 'column',
                        border: 0
                    });
                    //分隔的panel
                    Ext.create('Ext.panel.Panel', {
                        id: 'Separator',
                        margin: '0 0 0 0',
                        padding: '10 0 0 0',
                        style: 'background:#FFFFFF',
                        height: 10,
                        width: 900,
                        layout: 'column',
                        border: 0
                    });
                    //整体的panel
                    Ext.create('Ext.panel.Panel', {
                        width: 900,
                        overflowY: 'auto',
                        id: 'PannelContainer',
                        items: [Ext.getCmp("WindowPanelID"), Ext.getCmp("Separator"), Ext.getCmp("TabPannel")],
                        style: 'border:1px solid #C2C3C8;background:#ffffff',
                        layout: 'column',
                        height: 467,
                        border: 0
                    });
                    Ext.create('Jit.window.Window', {
                        id: "PannelWindow",
                        title: '编辑',
                        width: 910,
                        height: 540,
                        buttons: ['->', {
                            xtype: "jitbutton",
                            text: "关&nbsp;&nbsp;闭",
                            jitIsHighlight: false,
                            jitIsDefaultCSS: true,
                            margin: '10 10 0 0',
                            handler: function () {
                                Ext.getCmp("PannelWindow").close();
                            }
                        }],
                        items: [Ext.getCmp("PannelContainer")],
                        border: 0
                    });
                }
            });
        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },  //提交数据
    fnSubmit: function (me) {
        if (me.fnCheck(me) == false) {
            return false;
        }
        var l = new Array();
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var c = me.showPannel.items.items[0].items.items[i];
            if (c.jitGetValue(c) != null && c.jitGetValue(c) != "") {
                var o = Object();
                o.ControlType = c.ControlType;
                o.ControlValue = me.fnGetSearchValue(c.jitGetValue());
                o.ControlName = c.ControlName;
                o.CorrelationValue = c.CorrelationValue;
                l.push(o);
            }
        }
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: '系统处理中...' });
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
                    console.log(response.responseText);
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: (jdata.msg == "" ? '保存成功' : jdata.msg),
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO
                        });
                        Ext.getCmp("PannelWindow").close();
                        if (me.action == "create")
                            me.grid.pagebar.moveFirst();
                        else
                            me.grid.pagebar.doRefresh();
                    }
                    else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: (jdata.msg == "" ? '保存失败' : jdata.msg),
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
                else {
                    Ext.Msg.show({
                        title: '错误',
                        msg: '保存失败',
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.ERROR
                    });
                }
            }
        });
    },
    //删除数据
    fnDelete: function (me) {
        Ext.MessageBox.confirm('提示', '确认删除？', function deldbconfig(btn) {
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
                            var jdata = Ext.JSON.decode(response.responseText);
                            if (jdata.success) {
                                Ext.Msg.show({
                                    title: '提示',
                                    msg: '删除成功',
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO
                                });
                                me.grid.pagebar.doRefresh();
                            }
                            else {
                                Ext.Msg.show({
                                    title: '提示',
                                    msg: '删除失败',
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.ERROR
                                });
                            }
                        }
                        else {
                            Ext.Msg.show({
                                title: '提示',
                                msg: '删除失败',
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    }
                });
        });
    },
    //导出数据-tiansheng.zhu
    fnExport: function (me) {
        //导出条件
        var sme = me.pnlSearch;
        var l = new Array();
        if (sme != null && sme.showPannel != null) {
            try {
                for (var i = 0; i < sme.showPannel.items.items.length; i++) {
                    var c = sme.showPannel.items.items[0].items.items[i];
                    if (c.jitGetValue() != null && c.jitGetValue() != "" && c.jitGetValue() != "0") {
                        var o = new Object();
                        o.ControlType = c.ControlType;
                        o.ControlValue = c.jitGetValue();
                        o.ControlName = c.ControlName;
                        o.CorrelationValue = c.CorrelationValue;
                        l.push(o);
                    }
                }
            } catch (e) {
                l = new Array();
            }
        }
        var search = Ext.JSON.encode(l);
        //确定是否导出当前数据
        Ext.MessageBox.confirm('提示', '确定导出当前数据?', function deldbconfig(btn) {
            if (btn == 'yes') {
                //导出当前数据
                window.open(me.ajaxPath + "&btncode=" + me.BtnCode + "&method=export&pSearch=" + search);
            }
        });
    },
    //显示添加页面
    fnShowAdd: function (me) {
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            o.jitSetValue("");
            //如果是自动生成编号
            if (me.showPannel.items.items[0].items.items[i].ControlType == 33) {
                o.jitRefreshValue(); //自动生成
            } else if (me.showPannel.items.items[0].items.items[i].ControlType == 28) {
                o.jitGetCityValue(null);
            }
        }
        me.action = "create";
        me.KeyValue = "";
        Ext.getCmp("PannelWindow").show();
    },
    //显示修改页面
    fnShowUpdate: function (me) {
        me.action = "update";
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: '系统处理中...' });
        myMask.show();
        Ext.Ajax.request({
            url: me.ajaxPath,
            params: {
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
                myMask.hide();
            }
        });
    },
    //根据名称获取控件
    fnGetCol: function (me, pName) {
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            if (o.ControlName == pName) {
                return o;
            }
        }
    },
    fnContainSpecial: function (value) {

    },
    fnGetStrLen: function (str) {
        var realLength = 0, len = str.length, charCode = -1;
        for (var i = 0; i < len; i++) {
            charCode = str.charCodeAt(i);
            if (charCode >= 0 && charCode <= 128) realLength += 1;
            else realLength += 2;
        }
        return realLength;
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
    //检查表单数据正确性
    fnCheck: function (me) {
        //检查必填字段
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            if (o.IsMustDo == 1)
                if (o.jitGetValue(me) == null || o.jitGetValue(me) == "") {
                    Ext.Msg.alert('提示', o.alertLabel + '不能为空');
                    return false;
                }
        }
        //检查数据长度或者数字大小限制
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            switch (o.ControlType) {
                case 1:
                    if (o.jitGetValue() != "" && o.jitGetValue() != null) {
                        if (o.MinLength != null) {
                            if (me.fnGetStrLen(o.jitGetValue()) < o.MinLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据长度小于位最小值设定');
                                return false;
                            }
                        }
                        if (o.MaxLength != null) {
                            if (me.fnGetStrLen(o.jitGetValue()) > o.MaxLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据长度大于位最大值设定');
                                return false;
                            }
                        }
                    }
                    break;
                case 2:
                    if (o.jitGetValue() != "" && o.jitGetValue() != null) {
                        if (o.MinLength != null) {
                            if (parseInt(o.jitGetValue()) < o.MinLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据小于最小值设定');
                                return false;
                            }
                        }
                        if (o.MaxLength != null) {
                            if (parseInt(o.jitGetValue()) > o.MaxLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据大于最大值设定');
                                return false;
                            }
                        }
                    }
                    break;
                case 3:
                    if (o.jitGetValue() != "" && o.jitGetValue() != null) {
                        if (o.MinLength != null) {
                            if (parseInt(o.jitGetValue()) < o.MinLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据小于最小值设定');
                                return false;
                            }
                        }
                        if (o.MaxLength != null) {
                            if (parseInt(o.jitGetValue()) > o.MaxLength) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据小于最大值设定');
                                return false;
                            }
                        }
                    }
                    break;
            }
        }
        //数据类型判断
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            if (o.jitGetValue() != "" && o.jitGetValue() != null) {
                switch (o.ControlType) {
                    case 2:
                        if (isNaN(o.jitGetValue())) {
                            Ext.Msg.alert('提示', o.alertLabel + '不是正确的数字类型');
                            return false;
                        }
                        break;
                    case 3:
                        if (isNaN(o.jitGetValue())) {
                            Ext.Msg.alert('提示', o.alertLabel + '不是正确的数字类型');
                            return false;
                        }
                        break;
                }
            }
        }
        // 检查单个重复
        var IsRepeatArray = new Array();
        var isCheck = true;
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
            if (o.IsRepeat == 1) {
                var c = new Object();
                c.ControlType = o.ControlType;
                c.ControlValue = o.jitGetValue();
                c.ControlName = o.ControlName;
                c.CorrelationValue = o.CorrelationValue;
                if (c.ControlValue == "")//为空的不作处理
                {
                    continue;
                }
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
                            if (c.ControlType == 33) {
                                Ext.Msg.alert('提示', o.alertLabel + '数据中有重复值,请重新提交');
                                o.jitRefreshValue(); //重新生成
                            } else {
                                Ext.Msg.alert('提示', o.alertLabel + '数据中有重复值');
                            }
                            isCheck = false;
                        }
                    }
                });
            }
        }
        if (isCheck == false) return false;
        //检查多个重复
        IsRepeatArray = new Array(); //多个重复
        for (var i = 0; i < me.showPannel.items.items[0].items.items.length; i++) {
            var o = me.showPannel.items.items[0].items.items[i];
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
                    var RowCount = parseInt(response.responseText);
                    if (RowCount > 0) {
                        var msg = '';
                        for (var i = 0; i < IsRepeatArray.length; i++) {
                            msg = msg + '[' + IsRepeatArray[i].alertLabel + ']';
                        }
                        Ext.Msg.alert('提示', msg + '数据有重复值');
                        isCheck = false;
                    }
                }
            });
        }
        if (isCheck == false) return false;
        return true;
    }
});