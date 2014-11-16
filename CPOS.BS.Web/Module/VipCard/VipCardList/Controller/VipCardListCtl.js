Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VipCardListHandler.ashx?mid=" + __mid);

    fnSearch();
});



function fnSearch() {
    //debugger;
    Ext.getStore("VipCardStore").proxy.url = "Handler/VipCardListHandler.ashx?mid=&method=Search";
    Ext.getStore("VipCardStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("VipCardStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        //, sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
    };

    Ext.getStore("VipCardStore").load();

}

function fnModify(VipCardCode) {
    //alert(id);
    location.href = "/module/VipCard/Search/VipCardSearch.aspx?mid=8F56D25631F542AC8E7593229E68DFF8&VipCardCode=" + VipCardCode;
}

function fnReset() {
    Ext.getCmp("txtVipCardNo").setValue("");
    Ext.getCmp("txtVipName").setValue("");
    Ext.getCmp("txtVipCardGradeID").setValue("");
    Ext.getCmp("txtVipCardStatus").setValue("");
}

//延期
function fnDelay() {
    //alert('延期');
    var data = {};
    data.vips = [];

    var d = Ext.getCmp('gridView').getSelectionModel().getSelection();
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            //            data.vips.push(d[i].data);
            alert(d[i].data.VipStatusName);
        }
    } else {
        alert('没有选择延期会员卡信息');
    }

    //parent.fnSetSelectData(data.vips);
}
//升降级
function fnRelegation() {
    alert('升降级');
}
//激活
function fnActivation() {
    //alert('激活');
    var ids = "";
    var StatusIDNext = 1;
    var bSelect = true;
    var d = Ext.getCmp('gridView').getSelectionModel().getSelection();
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            if (d[i].data.VipCardStatusId == 6 || d[i].data.VipCardStatusId == 3) {
                if (ids == "") { ids = d[i].data.VipCardID; }
                else { ids = ids + "," + d[i].data.VipCardID; }
            } else {
                bSelect = false;
                alert('只有激活状态或者休眠状态的会员卡才能激活.');
                break;
            }
        }
        if (ids == "" || bSelect == false) {
            //alert('没有选择会员卡');
        } else {
            SetVipCardStatus(ids, StatusIDNext);
        };
    }
}
//冻结
function fnFreeze() {
    //    alert('冻结');
    var ids = "";
    var StatusIDNext = 6;
    var bSelect = true;
    var d = Ext.getCmp('gridView').getSelectionModel().getSelection();
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            if (d[i].data.VipCardStatusId == 1) {
                if (ids == "") {ids = d[i].data.VipCardID; }
                else { ids = ids + "," + d[i].data.VipCardID; }
            } else {
                bSelect = false;
                alert('只有激活状态的会员卡才能冻结.');
                break;
            }
        }
        if (ids == "" || bSelect==false) {
            //alert('没有选择会员卡');
        } else {
            SetVipCardStatus(ids, StatusIDNext);
        };
    }

   

}
//休眠
function fnDormancy() {
    //alert('休眠');
    var ids = "";
    var StatusIDNext = 3;
    var bSelect = true;
    var d = Ext.getCmp('gridView').getSelectionModel().getSelection();
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            if (d[i].data.VipCardStatusId == 1) {
                if (ids == "") { ids = d[i].data.VipCardID; }
                else { ids = ids + "," + d[i].data.VipCardID; }
            } else {
                bSelect = false;
                alert('只有激活状态的会员卡才能休眠.');
                break;
            }
        }
        if (ids == "" || bSelect == false) {
            //alert('没有选择会员卡');
        } else {
            SetVipCardStatus(ids, StatusIDNext);
        };
    }
}

function SetVipCardStatus(ids, StatusIDNext) {
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        async: false,
        url: 'Handler/VipCardListHandler.ashx?method=vipCardUpdateStatus&StatusIDNext=' + StatusIDNext + '&ids=' + ids,
        params: {
            //"template": Ext.encode(template)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("修改会员卡状态失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("修改会员卡状态成功");
                flag = true;
                Ext.getStore("VipCardStore").load();
            }
        },
        failure: function (result) {
            showError("修改会员卡状态失败：" + result.responseText);
        }
    });
}
