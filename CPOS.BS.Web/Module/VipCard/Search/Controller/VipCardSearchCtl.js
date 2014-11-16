var VipCardID = null;
var VipID = null;
var UnitID = null;
var ChangeBeforeGradeID = null;
var VipCardStatusId = null;
var tabs3;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VipCardSearchHandler.ashx?mid=");
    var pSearchVipCardCode = getStr(getUrlParam("VipCardCode"));
    var pSearchVipName = getStr(getUrlParam("VipName"));
    var pSearchCarCode = getStr(getUrlParam("CarCode"));

    fnSearch(0, pSearchVipCardCode, pSearchVipName, pSearchCarCode);
});

fnSearch = function(index, pSearchVipCardCode, pSearchVipName, pSearchCarCode) {
    if (typeof pSearchVipCardCode == "string") 
        Ext.getCmp("txtSearchVipCardCode").setValue(pSearchVipCardCode);
    if (typeof pSearchVipName == "string") 
        Ext.getCmp("txtSearchVipName").setValue(pSearchVipName);
    if (typeof pSearchCarCode == "string") 
        Ext.getCmp("txtSearchCarCode").setValue(pSearchCarCode);

    var SearchVipCardCode = Ext.getCmp("txtSearchVipCardCode").getValue();
    var SearchVipName = Ext.getCmp("txtSearchVipName").getValue();
    var SearchCarCode = Ext.getCmp("txtSearchCarCode").getValue();

    if (SearchVipCardCode == "" && SearchVipName == "" && SearchCarCode == "") {
        //alert("请输入查询条件");
        return;
    }
    
    Ext.getCmp("btnSearch").setText("查询中...");
    Ext.getCmp("btnSearch").disable(false);
    if (index != undefined && index != null) Ext.getCmp("tabs3").setActiveTab(index);
    Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=search_vip",
            params: { VipCardNumber: SearchVipCardCode, 
                VipName: SearchVipName, 
                CarNumber: SearchCarCode },
            method: 'POST',
            //sync: true,
            //async: false,
            success: function (response) {
                var d = Ext.decode(response.responseText);
                //alert(response.responseText);
                if (d.VipCardData == null || d.VipCardData.VipCardID == null || d.VipCardData.VipCardID == "" ||
                    d.VipData == null || d.VipData.VIPID == null || d.VipData.VIPID == "")
                {
                    alert("未查找到匹配的会员卡信息");
                    Ext.getCmp("btnSearch").setText("查询");
                    Ext.getCmp("btnSearch").enable(true);
                    return;
                }
                VipCardID = d.VipCardData.VipCardID;
                VipID = d.VipData.VIPID;
                UnitID = d.VipCardData.UnitID;
                ChangeBeforeGradeID = d.VipCardData.VipCardGradeID;
                VipCardStatusId = d.VipCardData.VipCardStatusId;

                get("txtVipCardCode").innerHTML = getStr(d.VipCardData.VipCardCode);
                get("txtVipName").innerHTML = getStr(d.VipData.VipName);
                get("txtPhone").innerHTML = getStr(d.VipData.Phone);
                get("txtBeginDate").innerHTML = getStr(d.VipCardData.BeginDate);
                get("txtEndDate").innerHTML = getStr(d.VipCardData.EndDate);
                get("txtUnitName").innerHTML = getStr(d.VipCardData.UnitName);
                get("txtBalanceAmount").innerHTML = getStr(d.VipCardData.BalanceAmount);
                get("txtIntegration").innerHTML = getStr(d.VipData.Integration);
                get("txtVipCardGrade").innerHTML = getStr(d.VipCardData.VipCardGradeName);
                get("txtVipCardStatus").innerHTML = getStr(d.VipCardData.VipStatusName);
                get("txtLastSalesTime").innerHTML = getStr(d.VipCardData.LastSalesTime);
                get("txtPurchaseTotalCount").innerHTML = getStr(d.VipCardData.PurchaseTotalCount);
                get("txtSalesTotalAmount").innerHTML = getStr(d.VipCardData.SalesTotalAmount);
                get("txtPurchaseTotalAmount").innerHTML = getStr(d.VipCardData.PurchaseTotalAmount);

                fnLoadVipCardSales();
                fnLoadVipCardRechargeRecord();
                fnLoadVipCardGradeChangeLog();
                fnLoadVipCardStatusChangeLog();
                fnLoadVipExpand();

                switch (d.VipCardData.VipCardStatusCode) {
                    case "001":
                        Ext.getCmp("btnOp1").enable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").enable(true);
                        Ext.getCmp("btnOp4").disable(true);
                        Ext.getCmp("btnOp5").enable(true);
                        Ext.getCmp("btnOp6").enable(true);
                        Ext.getCmp("btnOp7").enable(true);
                        Ext.getCmp("btnOp8").enable(true);
                        break;
                    case "002":
                        Ext.getCmp("btnOp1").disable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").disable(true);
                        Ext.getCmp("btnOp4").disable(true);
                        Ext.getCmp("btnOp5").disable(true);
                        Ext.getCmp("btnOp6").disable(true);
                        Ext.getCmp("btnOp7").disable(true);
                        Ext.getCmp("btnOp8").disable(true);
                        break;
                    case "003":
                        Ext.getCmp("btnOp1").disable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").disable(true);
                        Ext.getCmp("btnOp4").enable(true);
                        Ext.getCmp("btnOp5").disable(true);
                        Ext.getCmp("btnOp6").enable(true);
                        Ext.getCmp("btnOp7").disable(true);
                        Ext.getCmp("btnOp8").disable(true);
                        break;
                    case "004":
                        Ext.getCmp("btnOp1").disable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").disable(true);
                        Ext.getCmp("btnOp4").disable(true);
                        Ext.getCmp("btnOp5").disable(true);
                        Ext.getCmp("btnOp6").enable(true);
                        Ext.getCmp("btnOp7").disable(true);
                        Ext.getCmp("btnOp8").disable(true);
                        break;
                    case "005":
                        Ext.getCmp("btnOp1").disable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").disable(true);
                        Ext.getCmp("btnOp4").disable(true);
                        Ext.getCmp("btnOp5").disable(true);
                        Ext.getCmp("btnOp6").disable(true);
                        Ext.getCmp("btnOp7").disable(true);
                        Ext.getCmp("btnOp8").disable(true);
                        break;
                    case "006":
                        Ext.getCmp("btnOp1").disable(true);
                        Ext.getCmp("btnOp2").disable(true);
                        Ext.getCmp("btnOp3").disable(true);
                        Ext.getCmp("btnOp4").enable(true);
                        Ext.getCmp("btnOp5").disable(true);
                        Ext.getCmp("btnOp6").disable(true);
                        Ext.getCmp("btnOp7").disable(true);
                        Ext.getCmp("btnOp8").disable(true);
                        break;
                }

                Ext.getCmp("btnSearch").setText("查询");
                Ext.getCmp("btnSearch").enable(true);
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
                Ext.getCmp("btnSearch").setText("查询");
            }
        });
    //Ext.getCmp("btnSearch").setText("查询");
}
fnReset = function() {
    Ext.getCmp("txtSearchVipCardCode").setValue("");
    Ext.getCmp("txtSearchVipName").setValue("");
    Ext.getCmp("txtSearchCarCode").setValue("");
}

fnLoadVipCardSales = function() {
    //get("gridVipCardSales").innerHTML = "";
    if (VipCardID == null || VipCardID == "") return;
    var store = Ext.getStore("VipCardSalesStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipCardSales&VipCardID=" + VipCardID;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnLoadVipCardRechargeRecord = function() {
    if (VipCardID == null || VipCardID == "") return;
    var store = Ext.getStore("VipCardRechargeRecordStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipCardRechargeRecord&VipCardID=" + VipCardID;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnLoadVipCardGradeChangeLog = function() {
    if (VipCardID == null || VipCardID == "") return;
    var store = Ext.getStore("VipCardGradeChangeLogStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipCardGradeChangeLog&VipCardID=" + VipCardID;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnLoadVipCardStatusChangeLog = function() {
    if (VipCardID == null || VipCardID == "") return;
    var store = Ext.getStore("VipCardStatusChangeLogStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipCardStatusChangeLog&VipCardID=" + VipCardID;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnLoadVipExpand = function() {
    if (VipID == null || VipID == "") return;
    var store = Ext.getStore("VipExpandStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipExpand&VipID=" + VipID;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}

function fnClose() {
    CloseWin('vipEdit');
}

function fnSave() {
    var flag;
    var vip = {};
        
    var hdPurchaseUnitCtrl = Ext.getCmp("txtParentUnit");
    var tbVipCodeCtrl = Ext.getCmp("txtVipCode");
    var tbVipNameCtrl = Ext.getCmp("txtVipName");
    var tbVipEnglishCtrl = Ext.getCmp("txtVipEnglish");
    var tbVipAddressCtrl = Ext.getCmp("txtAddress");
    var tbVipContacterCtrl = Ext.getCmp("txtVipContacter");
    var tbVipTelCtrl = Ext.getCmp("txtVipTel");
    var tbVipFaxCtrl = Ext.getCmp("txtVipFax");
    var hdIsDefaultVipCtrl = Ext.getCmp("txtIsDefaultVip");
    var hdVipStatusCtrl = Ext.getCmp("txtVipStatus");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");

    vip.vip_id = getUrlParam("vip_id");
    vip.unit_id = hdPurchaseUnitCtrl.jitGetValue();
    vip.wh_code = tbVipCodeCtrl.jitGetValue();
    vip.wh_name = tbVipNameCtrl.jitGetValue();
    vip.wh_name_en = tbVipEnglishCtrl.jitGetValue();
    vip.wh_address = tbVipAddressCtrl.jitGetValue();
    vip.wh_contacter = tbVipContacterCtrl.jitGetValue();
    vip.wh_tel = tbVipTelCtrl.jitGetValue();
    vip.wh_fax = tbVipFaxCtrl.jitGetValue();
    vip.is_default = hdIsDefaultVipCtrl.jitGetValue();
    vip.wh_status = hdVipStatusCtrl.jitGetValue();
    vip.wh_remark = tbRemarkCtrl.jitGetValue();

    if (vip.unit_id == null || vip.unit_id == "") {
        showError("请选择所属单位");
        return;
    }
    if (vip.wh_code == null || vip.wh_code == "") {
        showError("请填写仓库编码");
        return;
    }
    if (vip.wh_name == null || vip.wh_name == "") {
        showError("请填写仓库名称");
        return;
    }
    if (vip.wh_tel == null || vip.wh_tel == "") {
        showError("请填写电话");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Vip/Handler/VipHandler.ashx?method=vip_save&vip_id=' + vip.vip_id, 
        params: {
            "vip": Ext.encode(vip)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('vipEdit');
}

function fnVipCardDisable() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardDisable",
        title: "卡注销",
        url: "VipCardDisable.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}
function fnVipCardFozen() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardFozen",
        title: "卡冻结",
        url: "VipCardFozen.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}
function fnVipCardSleep() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardSleep",
        title: "卡休眠",
        url: "VipCardSleep.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}
function fnVipCardRecharge() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardRecharge",
        title: "卡充值",
        url: "VipCardRecharge.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}
function fnVipCardExtension() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardExtension",
        title: "卡延期",
        url: "VipCardExtension.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}
function fnVipCardChangeLevel() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardChangeLevel",
        title: "卡升降级",
        url: "VipCardChangeLevel.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID + 
            "&VipCardStatusId=" + VipCardStatusId +
            "&ChangeBeforeGradeID=" + ChangeBeforeGradeID
    });
	win.show();
}
function fnVipCardActive() {
    var flag;
    var vip = {};
    vip.VipCardID = VipCardID;
    vip.VipID = VipID;
    vip.UnitID = UnitID;
    vip.StatusIDNow = VipCardStatusId;
    vip.StatusIDNext = "1";
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/VipCard/Search/Handler/VipCardSearchHandler.ashx?method=SaveVipCardActive&VipCardId=' + vip.VipCardID, 
        params: {
            "vip": Ext.encode(vip)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}
function fnVipCardReportLoss() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 300,
        id: "VipCardReportLoss",
        title: "卡挂失",
        url: "VipCardReportLoss.aspx?VipCardID=" + VipCardID + "&VipID=" + VipID + "&UnitID=" + UnitID +
            "&VipCardStatusId=" + VipCardStatusId
    });
	win.show();
}

fnCheckChangeLevel = function() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetVipCardInfo",
        params: { VipCardID: VipCardID },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.VipCardStatusCode != "001") {
                alert("只有卡状态是激活的情况下，才能做卡的升降级操作。");
                return false;
            }
            fnVipCardChangeLevel();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}
