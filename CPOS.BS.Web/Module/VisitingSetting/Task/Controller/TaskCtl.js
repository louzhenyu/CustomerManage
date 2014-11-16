Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);//默认15
    JITPage.HandlerUrl.setValue("Handler/TaskHandler.ashx?mid=" + __mid);

    fnSearch();
});

/*
*添加拜访任务
*/
function fnCreate() {
    window.location = "taskedit.aspx?mid=" + __mid + "&btncode=create";
}

/*
*删除拜访任务
*@pVisitingTaskID    任务的ID
*/
function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingTaskID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            id: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingTaskID" })
        },
        handler: function () {
            Ext.getStore("taskStore").load();
        }
    });
}

/*
*查询拜访任务列表
*@pClientPositionID     职位的ID(可选)
*/
function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=search&method=GetTaskList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: "",
        ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue()
    };
    Ext.getCmp("pageBar").moveFirst();
}

/*
*页面查询重置方法
*/
function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
}

/*
*
*/
function fnColumnUpdate(value, p, r) {

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"TaskEdit.aspx?mid=" + __mid + "&btncode=update&id=" + r.data.VisitingTaskID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"TaskEdit.aspx?mid=" + __mid + "&btncode=search&id=" + r.data.VisitingTaskID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }

}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}