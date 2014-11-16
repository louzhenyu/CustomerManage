﻿Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.PageSize.setValue(15); //默认15
    JITPage.HandlerUrl.setValue("Handler/ActivityMediaHandler.ashx?mid=" + __mid);

    fnSearch();
});

/*
*添加拜访任务
*/
function fnCreate() {
    window.location = "ActivityMediaedit.aspx?mid=" + __mid + "&btncode=create";
}

/*
*删除拜访任务
*@pActivityMediaID    任务的ID
*/
function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ActivityMediaID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            id: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ActivityMediaID" })
        },
        handler: function () {
            Ext.getStore("ActivityMediaStore").load();
        }
    });
}

function fnSearch() {

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=search&method=GetActivityMediaList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        MediaTitle: Ext.JSON.encode(Ext.getCmp("MediaTitle").getValue())
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
        return "<a style='color:blue;' href=\"ActivityMediaEdit.aspx?mid=" + __mid + "&btncode=update&id=" + r.data.ActivityMediaID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"ActivityMediaEdit.aspx?mid=" + __mid + "&btncode=search&id=" + r.data.ActivityMediaID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
    return "";
}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}