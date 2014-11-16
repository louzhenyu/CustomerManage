var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");
var urlparams = window.location.search;

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);//默认15
    JITPage.HandlerUrl.setValue("Handler/TaskHandler.ashx?mid=" + __mid);

    btncode == "search" ? Ext.getCmp("btnCreate").hide() : Ext.getCmp("btnCreate").show();

    if (id != null && id != "") {
        fnSearch(id);
    };

    var poptype = JITMethod.getUrlParam("poptype");
    var popurl = "";
    switch (poptype.toString()) {
        case "1":
            popurl = 'TaskEdit_StoreSelect.aspx';
            break;
        case "2":
            popurl = 'TaskEdit_DistributorSelect.aspx';
            break;
    }
    document.getElementById("a1").setAttribute("href", popurl + urlparams);

    //by zhongbao.xiao 2013.5.24 用户返回拜访任务列表页
    $("#navi").click(function () {
        window.location = "task.aspx?mid=" + __mid;
    });
});

function fnCreate() {
    if (id != null && id != "") {
        fnShowWindow({ tid: id, id: null });
    }
    else {
        Ext.Msg.show({
            title: '提示',
            msg: "请先提交基本信息",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
    }
}

function fnCancel() { 
    
}

function fnDelete() {
    
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingTaskStepID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode+"&method=DeleteStep",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingTaskStepID" })
        },
        handler: function () {
            Ext.getStore("stepStore").load();
        }
    });
}

function fnSearch(id) {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetStepByTID";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: "",
        id: id
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnUpdate(tid, id) {
    fnShowWindow({ tid: tid, id: id });
}


var tabObject = {
    height: "500px",
    width: "950px",//如果修改了这里的宽度，请修改 TaskEdit_StepView.js stepEditWin  window 的宽度（多加10px）
    tab1url: "../Step/StepEdit.aspx"
};

var tab2State = false;
var tab3State = false;
function fnShowWindow(obj) {
//    Ext.getCmp("tabs").removeAll();
//    Ext.getCmp("tabs").add([{
//        title: '基本信息',
//        contentEl: "tab1"
//    }, {
//        title: '拜访对象',
//        contentEl: "tab2"
//    }, {

//        title: '采集参数',
//        contentEl: "tab3"
    //    }]);
    
    tab2State = false;
    tab3State = false;

    Ext.getCmp("tabs").setActiveTab(0);
    
    var parameters = "?mid=" + __mid + "&btncode=" + btncode+"&r="+Math.random();
    if (obj.id != null && obj.id != "") {
        //修改

        parameters += "&tid=" + obj.tid + "&id=" + obj.id ;
        Ext.getCmp("tabs").items.items[1].setDisabled(true);
        Ext.getCmp("tabs").items.items[2].setDisabled(true);
    }
    else {
        //新增
        parameters += "&tid=" + obj.tid;
        Ext.getCmp("tabs").items.items[1].setDisabled(true);
        Ext.getCmp("tabs").items.items[2].setDisabled(true);
    }
    document.getElementById("tab1").setAttribute("src", tabObject.tab1url + parameters);

    for (var i = 1; i <= 3; i++) {
        document.getElementById("tab" + i).style.height = tabObject.height;
        document.getElementById("tab" + i).style.width = tabObject.width;
        //document.getElementById("tab" + i).style.display = "block";
    }
    
    Ext.getCmp("stepEditWin").show();
}

var renderer = {
    Delete: function (value, p, r) {
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
    },
    Update: function (value, p, r) {

        if (!__getHidden("update")) {
            //修改权限
            return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VisitingTaskID + "','" + r.data.VisitingTaskStepID + "')\">" + value + "</a>";
        }
        else if (__getHidden("update") && !__getHidden("search")) {
            //查询权限
            return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VisitingTaskID + "','" + r.data.VisitingTaskStepID + "')\">" + value + "</a>";
        }
        else if (__getHidden("update") && __getHidden("search")) {
            //无修改、查询(update,search)权限
            return "<a href=\"javascript:;\">" + value + "</a>";
        }
    }
};


function fnColumnMust(v) {
    var html = "-";
    if (v.toString() == 1) {
        html = "√";
    }
    return html;
}

/*
*更改弹出窗体的Title 2013-05-16 by zhongbao.xiao
*/
function fnSetWinTitle(value) {
    switch (value) {
        case "0":
            Ext.getCmp("stepEditWin").setTitle("拜访步骤-基本信息");
            break;
        case "1":
            Ext.getCmp("stepEditWin").setTitle("拜访步骤-拜访对象");
            break;
        case "2":
            Ext.getCmp("stepEditWin").setTitle("拜访步骤-采集参数");
            break;
        default:
            Ext.getCmp("stepEditWin").setTitle("拜访步骤-基本信息");
            break;
    }
}