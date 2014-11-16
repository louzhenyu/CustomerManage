Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

//页面加载
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/RegisterHandler.ashx?mid=" + __mid);

    fnReset();
});

//查询
function fnSearch() {
    var vipName = Ext.getCmp("txtVipName").getValue();
    var phone = Ext.getCmp("txtPhone").getValue();

    if ((vipName == null || vipName == "") && (phone == null || phone == "")) {
        showError("请输入查询条件");
        return;
    }
    else {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_vip_list",
            params: {
                vipName: vipName,
                phone: phone
            },
            method: 'post',
            success: function (response) {
                var totalCount = Ext.decode(response.responseText).totalCount;
                if (totalCount > 0) {
                    if (totalCount == 1) {
                        var d = Ext.decode(response.responseText).topics;

                        //页面赋值
                        fnSetSelectData(d);
                    }
                    else {
                        //会员选择小窗口
                        fnOpenImport(vipName, phone);
                    }
                }
                else {
                    Ext.Msg.show({
                        title: '提示',
                        msg: "没有查询到相关的会员信息",
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.INFO
                    });
                }
            },
            failure: function () {
                showError("提示", "获取参数数据失败");
            }
        });
    }
}

//清空
function fnReset() {
    //会员查询
    Ext.getCmp("txtVipName").setValue("");
    Ext.getCmp("txtPhone").setValue("");
    //会员基本信息
    get("labVipName").innerHTML = "";
    get("labPhone").innerHTML = "";
    get("labGender").innerHTML = "";
    get("labBirthday").innerHTML = "";
    get("labEmail").innerHTML = "";
    get("labQq").innerHTML = "";
    get("labSinaMBlog").innerHTML = "";
    get("labTencentMBlog").innerHTML = "";
    get("labRegistrationTime").innerHTML = "";
    get("labVipSourceId").innerHTML = "";
    get("labIntegration").innerHTML = "";
    get("labVIPID").innerHTML = "";

    //加载会员卡信息
    fnLoadVipCard();

    //加载车信息
    fnLoadVipExpand()
}

//会员选择小窗口
fnOpenImport = function (vipName, phone) {
    var url = "VipSearch.aspx?vipName=" + encodeURIComponent(vipName) + "&phone=" + encodeURIComponent(phone);
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 480,
        id: "VipSearch",
        title: '会员查询',
        url: url
    });
    win.show();
}

//选择会员后页面赋值
fnSetSelectData = function (data) {
    if (data != null && data.length > 0) {
        get("labVipName").innerHTML = getStr(data[0].VipName);
        get("labPhone").innerHTML = getStr(data[0].Phone);
        get("labGender").innerHTML = getStr(data[0].GenderInfo);
        get("labBirthday").innerHTML = getStr(data[0].Birthday);
        get("labEmail").innerHTML = getStr(data[0].Email);
        get("labQq").innerHTML = getStr(data[0].Qq);
        get("labSinaMBlog").innerHTML = getStr(data[0].SinaMBlog);
        get("labTencentMBlog").innerHTML = getStr(data[0].TencentMBlog);

        if (data[0].RegistrationTime == undefined || data[0].RegistrationTime == null) {
            get("labRegistrationTime").innerHTML = getStr(data[0].RegistrationTime);
        }
        else {
            get("labRegistrationTime").innerHTML = data[0].RegistrationTime.substr(0, 10);
        }
        
        get("labVipSourceId").innerHTML = getStr(data[0].VipSourceName);
        get("labIntegration").innerHTML = getStr(data[0].Integration);
        get("labVIPID").innerHTML = getStr(data[0].VIPID);

        //加载会员卡信息
        fnLoadVipCard();

        //加载车信息
        fnLoadVipExpand()
    }
}

//新增会员卡
function fnAddVipCard() {
    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") {
        alert("请先查询会员信息");
        return;
    }

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipCardEdit",
        title: "会员卡信息",
        url: "VipCardEdit.aspx?mid=" + __mid + "&op=new&VipID=" + vipId
    });

    win.show();
}

//编辑会员卡
function fnVipCardView(id) {
    if (id == undefined || id == null) id = "";

    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") {
        alert("请先查询会员信息");
        return;
    }

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipCardEdit",
        title: "会员卡信息",
        url: "VipCardEdit.aspx?VipCardID=" + id + "&VipID=" + vipId
    });

    win.show();
}

//删除会员卡
function fnVipCardDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "VipCardID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=delete_vipcard",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "VipCardID"
            })
        },
        handler: function () {
            fnLoadVipCard();
        }
    });
}

//加载会员卡信息
function fnLoadVipCard() {
    var store = Ext.getStore("VipCardStore");
    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") vipId = "vipId";
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_vipcard_by_vipid&vipId=" + vipId;
    store.pageSize = JITPage.PageSize.getValue();

    store.load();
}

//新增车信息
function fnAddVipExpand() {
    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") {
        alert("请先查询会员信息");
        return;
    }

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipExpandEdit",
        title: "车信息",
        url: "VipExpandEdit.aspx?mid=" + __mid + "&op=new&VipID=" + vipId
    });

    win.show();
}

//编辑车信息
function fnVipExpandView(id) {
    if (id == undefined || id == null) id = "";

    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") {
        alert("请先查询会员信息");
        return;
    }

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipExpandEdit",
        title: "车信息",
        url: "VipExpandEdit.aspx?VipExpandID=" + id + "&VipID=" + vipId
    });

    win.show();
}

//删除车信息
function fnVipExpandDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView2"),
            id: "VipExpandID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=delete_vipexpand",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView2"),
                id: "VipExpandID"
            })
        },
        handler: function () {
            fnLoadVipExpand();
        }
    });
}

//加载车信息
function fnLoadVipExpand() {
    var store = Ext.getStore("VipExpandStore");
    var vipId = get("labVIPID").innerHTML;
    if (vipId == null || vipId == "") vipId = "vipId";
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_vipexpand_by_vipid&vipId=" + vipId;
    store.pageSize = JITPage.PageSize.getValue();

    store.load();
}
