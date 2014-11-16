Ext.onReady(function () {
    //加载需要的文件
    //var myMask_info = "loading...";
    //var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VipHandler.ashx?mid=");

    fnLoadVip();
});

fnLoadVip = function() {
     get("tabProp").style.display = "none";
     get("tabProp2").style.display = "none";
     get("tabProp3").style.display = "none";
     get("tabProp4").style.display = "none";
     get("tabProp5").style.display = "none";

    var vip_id = getUrlParam("vip_id");
    if (vip_id != "null" && vip_id != "") {

        var type = getUrlParam("type");
        //type = "5";
        if (type == "2") {
            get("tabProp2").style.display = "";
            fnPosOrderList();
        } else if (type == "3") {
            get("tabProp3").style.display = "";
            fnNextLevelUserList();
        } else if (type == "4") {
            get("tabProp4").style.display = "";
            fnCollectionPropertyList();
        } else if (type == "5") {
            get("tabProp5").style.display = "";
            fnLoadVipTags();
        } else {
            get("tabProp").style.display = "";
            fnLoadVipIntegralDetail();
        }
        
    }
}

fnLoadVipIntegralDetail = function() {
    var store = Ext.getStore("vipIntegralDetailStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipIntegralDetail&vip_id=" + getUrlParam("vip_id");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnPosOrderList = function() {
    var store = Ext.getStore("PosOrderListStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPosOrderList&vip_id=" + 
            getUrlParam("vip_id");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnNextLevelUserList = function() {
    var store = Ext.getStore("NextLevelUserListStore"); //GetNextLevelUserList
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetIntegralByVip&vip_id=" + 
            getUrlParam("vip_id");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load();
}
fnCollectionPropertyList = function () {
    var store = Ext.getStore("CollectionPropertyStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetCollectionPropertyList&vip_id=" +
            getUrlParam("vip_id");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0
    };
    store.load();
}
fnLoadVipTags = function() {
    var store = Ext.getStore("vipTagsStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetVipTags&vip_id=" + 
            getUrlParam("vip_id");
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

function fnDeleteVipTags(id) {
    if (!confirm("确认删除会员所具备的标签吗？")) {
        return;
    }
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/VipHandler.ashx?method=tags_delete',
        params: {
            "ids": id
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                flag = false;
            } else {
                flag = true;
                fnLoadVipTags();
            }
        },
        failure: function (result) {
            showError("删除数据失败：" + result.responseText);
        }
    });
}
function fnTagsAdd() {
    var tagsId = Ext.getCmp("txtTags").jitGetValue();
    if (tagsId == null || tagsId.length == 0) {
        alert("标签不能为空");
        return;
    }

    var item = {};
    item.TagsId = tagsId;
    item.VipId = getUrlParam("vip_id");
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/VipHandler.ashx?method=tags_save',
        params: {
            "item": Ext.encode(item)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                fnLoadVipTags();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}
function fnChangeIntegralAdd() {
    var changeIntegral = Ext.getCmp("txtChangeIntegral").jitGetValue();
    var changeIntegralRemark = Ext.getCmp("txtChangeIntegralRemark").jitGetValue();
    if (changeIntegral == null || changeIntegral.length == 0 || changeIntegral == 0) {
        alert("积分不能为空或0");
        return;
    }
    if (changeIntegralRemark == null || changeIntegralRemark.length == 0) {
        alert("变动原因不能为空");
        return;
    }
    if (!confirm("请确定是否变更积分"+changeIntegral+"?")) {
        return;
    }

    var item = {};
    item.VIPID = getUrlParam("vip_id");
    item.Integral = changeIntegral;
    item.IntegralSourceID = "11";
    item.Remark = changeIntegralRemark;
    item.IsAdd = 1;
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/VipHandler.ashx?method=changeIntegral_save',
        params: {
            "item": Ext.encode(item)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                fnLoadVip();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

