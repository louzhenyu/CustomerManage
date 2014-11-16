Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/TaskDataHandler.ashx?mid=" + __mid);

    var d = new Date();

    //    if (JITMethod.getUrlParam("sClientStructureID") != null
    //    && JITMethod.getUrlParam("sClientStructureID") != "null") {
    //        Ext.getCmp("ClientStructureID").jitSetValue(JITMethod.getUrlParam("sClientStructureID"));
    //    }
    //    if (JITMethod.getUrlParam("sClientPositionID") != null
    //    && JITMethod.getUrlParam("sClientPositionID") != "null") {
    //        Ext.getCmp("ClientPositionID").jitSetValue(JITMethod.getUrlParam("sClientPositionID"));
    //    }
    //    if (JITMethod.getUrlParam("sClientUserName") != null
    //    && JITMethod.getUrlParam("sClientUserName") != "null") {
    //        Ext.getCmp("ClientUserName").jitSetValue(unescape(JITMethod.getUrlParam("sClientUserName")));
    //    }
    if (JITMethod.getUrlParam("sExecutionTime") != null) {
        Ext.getCmp("ExecutionTime").setValue(JITMethod.getDate(JITMethod.getUrlParam("sExecutionTime")));
    }
    else {
        Ext.getCmp("ExecutionTime").setValue(new Date());
    }

//    Ext.getCmp("pageBar").store.addListener({
//        'load': function (obj, record, successful) {
//            alert(record);
//        }
//    });

    fnSearch();

    //    var ismove = false;
    //    if (JITMethod.getUrlParam("sPageIndex") != null) {
    //        Ext.getCmp("pageBar").addListener({
    //            'afterlayout': function () {
    //                if (!ismove) {
    //                    ismove = true;
    //                    Ext.getCmp("pageBar").moveLast();
    //                }
    //            }
    //        });
    //    }
});

function fnSearch() {
    if (Ext.getCmp("ExecutionTime").getValue() == null) {
        Ext.Msg.alert("提示", "请选择执行日期");
        return;
    }

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetTaskDataList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        //ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
        ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
        ClientUserName: Ext.getCmp("ClientUserName").jitGetValue(),
        ExecutionTime: Ext.getCmp("ExecutionTime").getRawValue()
    };
    Ext.getCmp("pageBar").moveFirst();


    
    //Ext.getCmp("gridView").setLoading();
//    Ext.Ajax.request({
//        url: JITPage.HandlerUrl.getValue() + "&btncode=search&method=GetTaskDataList&r="+Math.random(),
//        params: {
//            ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
//            ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
//            ClientUserName: Ext.getCmp("ClientUserName").jitGetValue(),
//            ExecutionTime: Ext.getCmp("ExecutionTime").getRawValue(),
//            limit:1,
//            page:1
//        },
//        method: 'post',
//        success: function (response) {
//            //alert(Ext.getCmp("ExecutionTime").getRawValue());
//            var jdata = Ext.JSON.decode(response.responseText);

//            //加载列表
////            Ext.getCmp("pageBar").store.removeAll();
////            Ext.getCmp("pageBar").store.add(jdata.topics);
//            summary.VisitingTaskPlanCountSummary = jdata.summary[0].VisitingTaskPlanCountSummary;
//            summary.VisitingTaskExecutionCountSummary = jdata.summary[0].VisitingTaskExecutionCountSummary;
//            summary.WorkingHoursTotalSummary = jdata.summary[0].WorkingHoursTotalSummary;
//            summary.WorkingHoursIndoorSummary = jdata.summary[0].WorkingHoursIndoorSummary;
//            summary.WorkingHoursJourneyTimeSummary = jdata.summary[0].WorkingHoursJourneyTimeSummary;
//            summary.EffectiveVisitSummary = jdata.summary[0].EffectiveVisitSummary;

//            //Ext.getCmp("gridView").reconfigure(Ext.getCmp("pageBar").store);
//            //Ext.getCmp("gridView").setLoading().hide();
//        },
//        failure: function () {
//            //Ext.Msg.alert("提示", "获取数据失败");
//        }
//    });
}

function fnReset() {
    //Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("ClientStructureID").jitSetValue("");
    Ext.getCmp("ClientPositionID").jitSetValue("");
    Ext.getCmp("ClientUserName").jitSetValue("");
    Ext.getCmp("ExecutionTime").setValue(new Date());
}

function fnView() {

    //   参数传入
    window.location = "TaskDataView_List.aspx?mid=" + __mid
    + "&ClientStructureID=" + JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ClientStructureID" })[0]
    + "&ClientPositionID=" + JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ClientPositionID" })[0]
    + "&ClientUserID=" + JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ClientUserID" })[0]
    + "&ClientUserName=" + escape(JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ClientUserName" })[0])
    + "&sClientStructureID=" + Ext.getCmp("ClientStructureID").jitGetValue()
    + "&sClientPositionID=" + Ext.getCmp("ClientPositionID").jitGetValue()
    + "&sClientUserName=" + escape(Ext.getCmp("ClientUserName").jitGetValue())
    + "&sExecutionTime=" + Ext.getCmp("ExecutionTime").getRawValue()
    + "&sPageIndex=" + Ext.getCmp("pageBar").store.currentPage;
}

function fnColumnUpdate(value, p, r) {
    if (__getHidden("search")) {
        //无权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
    else {
        //有权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnView()\">" + value + "</a>";
    }
}

function fnColumnMustDo(value, p, r) {
    return (value == 1 ? "√" : "")
}

function fnColumnTime(value, p, r) {
    var res = "";
    var min = parseInt(value);
    if (!isNaN(min)) {
        if (min >= 60) {
            res = Math.floor(min / 60) + "小时" + (min % 60) + "分钟";
        }
        else if (min < 0) {
            res = "0分钟";
        }
        else {
            res = min + "分钟";
        }
    }
    else {
        res = value;
    }
    return res;
}

function fnColumnColor(value, metaData, r) {

    if (r.data.FirstInTime == null) {
        return "";
    }
    var vint = parseInt(value);
    if (vint <= 80) {
        metaData.style = "background-color: Red;color:White;font-weight:bold;";
    }
    else if (vint > 80 && vint < 100) {
        metaData.style = "background-color: Orange";
    }
    else if (vint == 100) {

    }
    return value;
}