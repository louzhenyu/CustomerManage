Ext.onReady(function () {

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/VisitingObjectHandler.ashx?mid=" + __mid);

    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    fnSearch();
});

function fnCreate() {
    fnShowWindow({ id: null, btncode: 'create' });
}

function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
}

function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingObjectID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingObjectID" })
        },
        handler: function () {
            Ext.getStore("list_objectStore").load();
        }
    });
}

function fnColumnUpdate(value, p, r) {

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnShowWindow({id:'" + r.data.VisitingObjectID + "',btncode:'update'});\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnShowWindow({id:'" + r.data.VisitingObjectID + "',btncode:'search'});\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}


function fnRenderStatus(value, p, r) {
    return (value == 1 ? "启用" : "未启用")
}


var tabObject = {
    height: "500px",
    width: "900px", //如果修改了这里的宽度，请修改 VisitingObjectView.js objectEditWin window 的宽度（多加10px）
    tab1url: "VisitingObjectEdit.aspx"
};

var tab2State = false;
function fnShowWindow(obj) {

    tab2State = false;

    Ext.getCmp("tabs").setActiveTab(0);

    var parameters = "?mid=" + __mid + "&btncode=" + obj.btncode + "&r=" + Math.random();
    if (obj.id != null && obj.id != "") {
        //修改
        parameters += "&id=" + obj.id;
        Ext.getCmp("tabs").items.items[1].setDisabled(true);
    }
    else {
        //新增
        Ext.getCmp("tabs").items.items[1].setDisabled(true);
    }
    document.getElementById("tab1").setAttribute("src", tabObject.tab1url + parameters);

    for (var i = 1; i <= 2; i++) {
        document.getElementById("tab" + i).style.height = tabObject.height;
        document.getElementById("tab" + i).style.width = tabObject.width;
        //document.getElementById("tab" + i).style.display = "block";
    }

    Ext.getCmp("objectEditWin").show();
}