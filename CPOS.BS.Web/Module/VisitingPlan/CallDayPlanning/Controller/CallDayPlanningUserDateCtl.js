var btncode = JITMethod.getUrlParam("btncode");

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();


    InitCDPEditVE();
    InitCDPEditStore();
    InitCDPEditView();
    Ext.getCmp("cdpEditPanel_ClientUserID").selectText.addListener({
        'change': function () {
            Ext.getCmp("cdpEditPanel_StoreList").ajaxPath = "/Module/BasicData/Store/Handler/StoreSelectByClientUser.ashx?pUserArray=" + Ext.getCmp("cdpEditPanel_ClientUserID").jitGetValue();

            Ext.getCmp("cdpEditPanel_DistributorList").ajaxPath = "/Module/BasicData/Store/Handler/StoreSelectByClientUser.ashx?pUserArray=" + Ext.getCmp("cdpEditPanel_ClientUserID").jitGetValue();
        }
    });

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/CallDayPlanningHandler.ashx?mid=" + __mid);

    btncode == "search" ? Ext.getCmp("btnCreate").hide() : Ext.getCmp("btnCreate").show();

    //fnSearch();

    Ext.getCmp("ClientStructureID").jitSetValue(JITMethod.getUrlParam("ClientStructureID"));
    Ext.getCmp("ClientPositionID").jitSetValue(JITMethod.getUrlParam("ClientPositionID"));
    Ext.getCmp("ClientUserID").jitSetValue(JITMethod.getUrlParam("ClientUserID"));
    var date = JITMethod.getUrlParam("CallDate");


    Ext.getCmp("CallDate").setRawValue(date.split("-")[0] + "-" + date.split("-")[1]);
});

function fnCreate() {
    Ext.getCmp('cdpEditPanel').getForm().reset();
    Ext.getCmp("cdpEditPanel_ClientUserID").jitSetValue(Ext.getCmp("ClientUserID").jitGetValue());

    Ext.getCmp("cdpEditWin").show();

}

//@date 可为空
function fnSearch() {
    window.location = "CallDayPlanningUserDate.aspx" + fnGetSearchCondition();
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("ClientStructureID").jitSetValue(JITMethod.getUrlParam("ClientStructureID"));
    Ext.getCmp("ClientPositionID").jitSetValue(JITMethod.getUrlParam("ClientPositionID"));
    Ext.getCmp("ClientUserID").jitSetValue(JITMethod.getUrlParam("ClientUserID"));
    var date = JITMethod.getUrlParam("CallDate");
    Ext.getCmp("CallDate").jitSetValue(date.split("-")[0] + "-" + date.split("-")[1]);
}

//date 查看的某一天
function fnViewUserDetail(date) {
    window.location = "UserDateDetailTab.aspx?mid=" + __mid + "&btncode=" + btncode + "&ClientUserID=" + JITMethod.getUrlParam("ClientUserID") + "&CallDate=" + date;
}

function fnNextMonth() {
    //设置时间
    var date = JITMethod.getUrlParam("CallDate");
    var nowdate = new Date(Date.parse(date.split("-")[0] + "/" + date.split("-")[1] + "/01"));
    nowdate.setMonth(nowdate.getMonth() + 1);

    //拼接参数跳转
    var y = nowdate.getFullYear();
    var m = (nowdate.getMonth() + 1) >= 10 ? (nowdate.getMonth() + 1) : "0" + (nowdate.getMonth() + 1);

    window.location = 'CallDayPlanningUserDate.aspx' + fnGetSearchCondition(y + "-" + m);
}

function fnPreMonth() {
    //设置时间
    var date = JITMethod.getUrlParam("CallDate");
    var nowdate = new Date(Date.parse(date.split("-")[0] + "/" + date.split("-")[1] + "/01"));
    nowdate.setMonth(nowdate.getMonth() - 1);

    //拼接参数跳转
    var y = nowdate.getFullYear();
    var m = (nowdate.getMonth() + 1) >= 10 ? (nowdate.getMonth() + 1) : "0" + (nowdate.getMonth() + 1);

    window.location = 'CallDayPlanningUserDate.aspx' + fnGetSearchCondition(y + "-" + m);
}


function fnGetSearchCondition(cd) {
    var params = "?mid=" + __mid
    + "&btncode=" + btncode
        + "&ClientStructureID=" + Ext.getCmp("ClientStructureID").jitGetValue()
        + "&ClientPositionID=" + (Ext.getCmp("ClientPositionID").jitGetValue() == undefined ? "" : Ext.getCmp("ClientPositionID").jitGetValue())
        + "&ClientUserID=" + Ext.getCmp("ClientUserID").jitGetValue()

    if (cd == null || cd == undefined) {
        params += "&CallDate=" + Ext.getCmp("CallDate").getRawValue();
    }
    else {
        params += "&CallDate=" + cd;
    }
    return params;
}