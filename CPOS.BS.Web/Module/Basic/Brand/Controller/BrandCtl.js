Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();
    //页面异步加载（__mid:菜单ID<必须>）
    JITPage.HandlerUrl.setValue("Handler/BrandHandler.ashx?mid=" + __mid);
    fnSearch();
});

/*
创建编辑品牌信息
*/
function fnCreate() {
    window.location = "BrandEdit.aspx?mid=" + __mid;
}

/*
查询品牌信息
返回品牌信息
*/
function fnSearch() {//store：数据源，用于存储各种数据表格
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=search";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        //读取form中searchPanel面板中所有控件的值
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();
}

/*
删除品牌信息
@pBrandID    品牌的ID
返回品牌信息
*/
function fnDelete() {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "BrandID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "BrandID" })
        },
        handler: function () {
            Ext.getStore("brandStore").load();
        }
    });
}

function fnColumnUpdate(value, p, r) {
    if (__getHidden("update")) {
        //无权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
    else {
        //有权限
        return "<a style='color:blue;' href=\"BrandEdit.aspx?mid=" + __mid + "&id=" + r.data.BrandID + "\">" + value + "</a>";
    }
}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnValidateBrandLevel(__bLevel) {
    if (__bLevel == "2")
        return "二级品牌";
    else if (__bLevel == "1")
        return "一级品牌";
    else if (__bLevel == "3")
        return "三级品牌";
    else if (__bLevel == "4")
        return "四级品牌";
    return "";
}
function fnCheckIsOwnerValue(value) {
    if (value == "0")
        return "否";
    else if (value == "1")
        return "是";
    return "";
}