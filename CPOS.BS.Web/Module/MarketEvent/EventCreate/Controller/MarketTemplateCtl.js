Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

var typeData = [];
var templateData = [];
var templateId = "";

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/EventHandler.ashx?mid=");

    fnLoad();
});

fnCheckAmount = function () {
    if (Ext.getCmp('chkAmount1').getValue()) {
        Ext.getCmp('txtAmount1').setDisabled(false);
        Ext.getCmp('txtAmount2').setDisabled(true);
    } else {
        Ext.getCmp('txtAmount1').setDisabled(true);
        Ext.getCmp('txtAmount2').setDisabled(false);
    }
};

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "RoleEdit",
        title: "角色",
        url: "RoleEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnLoad = function () {
    var MarketEventID = getUrlParam("MarketEventID");

    if (MarketEventID != "null" && MarketEventID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_event_by_id",
            params: { MarketEventID: MarketEventID },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                //templateId = d.TemplateID;

                //Ext.getCmp("tbTemplateContent").setValue(Ext.util.Format.htmlDecode(d.TemplateContent));
                Ext.getCmp("tbTemplateContent").setValue(d.TemplateContent);
                Ext.getCmp("tbTemplateContentSMS").setValue(d.TemplateContentSMS);
                Ext.getCmp("txtSendType").setDefaultValue(d.SendTypeId);

                fnLoadType();
                fnPersonCount();
                fnLoadSend();
                $(".wrap,.header").css("width", $(".wrap>table").eq(0).width());
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
}

fnLoadType = function () {
    Ext.Ajax.request({
        method: 'GET',
        url: '/Framework/Javascript/Biz/Handler/EventTemplateType.ashx',
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                fnCreateType(d.data);
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showInfo("错误：" + result.responseText);
        }
    });
}
fnLoadTemplateList = function (type) {
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/MarketEvent/EventCreate/Handler/EventHandler.ashx?method=get_template_list',
        params: { type: type },
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.topics != null) {
                fnCreateTemplateList(d.topics);
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showInfo("错误：" + result.responseText);
        }
    });
}
fnCreateType = function (data) {
    typeData = data;
    var pnlType = get("pnlType");
    var str = "";
    for (var i = 0; i < data.length; i++) {
        typeData[i].id = newGuid();
        var val = data[i].Description;
        var s = "<div id=\"market_type_item" + typeData[i].id +
            "\" class=\"z_market_type_item\" onclick=\"fnCreateTemplate('" + val + "', this)\">" + val + "</div>";
        str += s;
    }
    pnlType.innerHTML = str;
}
fnCreateTemplateList = function (data) {
    templateData = data;
    var pnlTemplate = get("pnlTemplate");
    var str = "";
    for (var i = 0; i < data.length; i++) {
        var val = data[i].TemplateID;
        var focus = "";
        if (data[i].TemplateID == templateId) focus = "z_market_temp_item2";
        var s = "<div id=\"market_temp_item" + templateData[i].TemplateID +
            "\" class=\"z_market_temp_item " + focus +
            "\" onclick=\"fnSelectTemplate('" + templateData[i].TemplateID + "', this)\">" + data[i].TemplateDesc + "</div>";
        str += s;
    }
    pnlTemplate.innerHTML = str;
}
fnCreateTemplate = function (type, c) {
    fnResetTypeStyle();
    var p = $(c);
    p.addClass("z_market_type_item2");

    fnLoadTemplateList(type);
}
fnResetTypeStyle = function () { 
    for (var i = 0; i < typeData.length; i++) {
        var c = get("market_type_item" + typeData[i].id);
        var p = $(c);
        p.removeClass("z_market_type_item2");
    }
}
fnSelectTemplate = function (tempId, c) {
    fnResetTemplateStyle();
    var p = $(c);
    p.addClass("z_market_temp_item2");
    templateId = tempId;
    fnLoadTemplate(tempId);
}
fnResetTemplateStyle = function () {
    for (var i = 0; i < templateData.length; i++) {
        var c = get("market_temp_item" + templateData[i].TemplateID);
        var p = $(c);
        p.removeClass("z_market_temp_item2");
    }
}
fnLoadTemplate = function (id) {
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/MarketEvent/EventCreate/Handler/EventHandler.ashx?method=get_template_by_id',
        params: { TemplateID: id },
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText).data;
            if (d != null) {
                var tbTemplateContent = Ext.getCmp("tbTemplateContent");
                var tbTemplateContentSMS = Ext.getCmp("tbTemplateContentSMS");
                if (!get("chkTemplateContent").disabled) {
                    tbTemplateContent.setValue(d.TemplateContent);
                }
                if (!get("chkTemplateContentSMS").disabled) {
                    tbTemplateContentSMS.setValue(d.TemplateContentSMS);
                }
                if (!get("chkTemplateContentAPP").disabled) {
                    tbTemplateContentAPP.setValue(d.TemplateContentAPP);
                }
            } else {
                //showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showInfo("错误：" + result.responseText);
        }
    });
}

fnLoadSend = function () {
    var MarketEventID = getUrlParam("MarketEventID");
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/MarketEvent/EventCreate/Handler/EventHandler.ashx?method=get_unit_property',
        params: { MarketEventID: MarketEventID },
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText).data;
            var counts = Ext.decode(result.responseText).topics.split(',');
            get("chkTemplateContent").disabled = "disabled";
            get("txtTemplateContent").disabled = "disabled";
            get("txtTemplateContent").innerHTML = "微信（暂不可用）";
            get("txtTemplateContent").style.color = "#808080";
            Ext.getCmp("tbTemplateContent").setDisabled(true);
            get("chkTemplateContentSMS").disabled = "disabled";
            get("txtTemplateContentSMS").disabled = "disabled";
            get("txtTemplateContentSMS").innerHTML = "短信（暂不可用）";
            get("txtTemplateContentSMS").style.color = "#808080";
            Ext.getCmp("tbTemplateContentSMS").setDisabled(true);
            get("chkTemplateContentAPP").disabled = "disabled";
            get("txtTemplateContentAPP").disabled = "disabled";
            get("txtTemplateContentAPP").innerHTML = "APP（暂不可用）";
            get("txtTemplateContentAPP").style.color = "#808080";
            Ext.getCmp("tbTemplateContentAPP").setDisabled(true);

            if (d != null) {
                if (d.IsWeixinPush == 1) {
                    get("chkTemplateContent").disabled = "";
                    get("txtTemplateContent").disabled = "";
                    get("txtTemplateContent").innerHTML = "微信（" + counts[0] + "人）";
                    get("txtTemplateContent").style.color = "#000";
                    Ext.getCmp("tbTemplateContent").setDisabled(false);
                }
                if (d.IsSMSPush == 1) {
                    get("chkTemplateContentSMS").disabled = "";
                    get("txtTemplateContentSMS").disabled = "";
                    get("txtTemplateContentSMS").innerHTML = "短信（" + counts[1] + "人）";
                    get("txtTemplateContentSMS").style.color = "#000";
                    Ext.getCmp("tbTemplateContentSMS").setDisabled(false);
                }
                if (d.IsAPPPush == 1) {
                    get("chkTemplateContentAPP").disabled = "";
                    get("txtTemplateContentAPP").disabled = "";
                    get("txtTemplateContentAPP").innerHTML = "APP（" + counts[2] + "人）";
                    get("txtTemplateContentAPP").style.color = "#000";
                    Ext.getCmp("tbTemplateContentAPP").setDisabled(false);
                }
            }
        },
        failure: function (result) {
            showInfo("错误：" + result.responseText);
        }
    });
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "RoleEdit",
        title: "角色",
        url: "RoleEdit.aspx?role_id=" + id
    });
    win.show();
}
function fnDelete(id) {
    var grid = Ext.getCmp("gridWave");
    var ids = JITPage.getSelected({ gridView: grid, id: "WaveBandID" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择记录");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("WaveBandID", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
}
fnReset = function () {
    templateId = "";
    Ext.getCmp("tbTemplateContent").setValue("");
    Ext.getCmp("tbTemplateContentSMS").setValue("");
    Ext.getCmp("tbTemplateContentAPP").setValue("");
}
var pnlImport = get("pnlImport");
fnImport = function () {
    pnlImport = get("pnlImport");
    if (pnlImport.style.display != "") {
        pnlImport.style.display = "";
    } else {
        pnlImport.style.display = "none";
    }
}
fnOpenImport = function (url, text) {
    pnlImport.style.display = "none";
    url += "?MarketEventID=" + getUrlParam("MarketEventID");
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "RoleEdit",
        title: text,
        url: url
    });
    win.show();
}
fnSave = function (breakUrl) {
    var _grid = Ext.getStore("MarketStoreStore");
    var flag = false;

    var data = {};
    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;
    data.TemplateID = templateId;
    //data.TemplateContent = Ext.util.Format.htmlEncode(Ext.getCmp("tbTemplateContent").getValue());
    data.TemplateContent = Ext.getCmp("tbTemplateContent").getValue();
    data.TemplateContentSMS = Ext.getCmp("tbTemplateContentSMS").getValue();
    data.TemplateContentAPP = Ext.getCmp("tbTemplateContentAPP").getValue();
    data.SendTypeId = Ext.getCmp("txtSendType").getValue();

    if (data.SendTypeId == null || data.SendTypeId == "") {
        showError("请选择发送方式");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventHandler.ashx?method=event_template_save&MarketEventID=' + MarketEventID,
        params: { "data": Ext.encode(data) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;

            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (breakUrl == true) return;
    if (flag) location.href = "../EventList/EventList.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A";
}

fnSendMsg = function () {

    if (!confirm("确定启动并发送消息?")) return;

    fnSave(true);

    var data = {};
    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;
    data.EventStatus = 2;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventHandler.ashx?method=event_send&MarketEventID=' + MarketEventID,
        params: { "data": Ext.encode(data), 
            chk: get("chkTemplateContent").checked,
            chkSMS: get("chkTemplateContentSMS").checked,
            chkAPP: get("chkTemplateContentAPP").checked
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("数据发送成功");
                flag = true;
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) location.href = "../EventList/EventList.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A";
}

fnPre = function () {
    fnSave();
    location.href = "MarketPerson.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}

fnPersonCount = function () {
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/MarketEvent/EventList/Handler/EventListHandler.ashx?method=eventPerson_query',
        params: { eventId: getUrlParam("MarketEventID") },
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.msg != null) {
                Ext.getCmp("btnStart").setText("立即启动发送" + d.msg + "人");
            }
        },
        failure: function (result) {
            showInfo("错误：" + result.responseText);
        }
    });
}
