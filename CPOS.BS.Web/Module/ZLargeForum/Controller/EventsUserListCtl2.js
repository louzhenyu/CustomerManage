
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");

    fnSearch();

    myMask.hide();
});

function fnClose() {
    CloseWin('EventsUserList');
}

var loadOp = false;
function fnSearch() {
    var eventId = getUrlParam("EventID");
    var pnlUserList = document.getElementById("pnlUserList");
    var pnlOptions = document.getElementById("pnlOptions");
    var pnlSearch = document.getElementById("pnlSearch");
    pnlUserList.innerHTML = "加载数据中...";

    var searchOptionValue = getSearchOptionValue();
    //alert(searchOptionValue);pnlSearch

    var order_no = "";
    Ext.Ajax.request({
        method: 'GET',
        url: 'Handler/EventsHandler.ashx?method=events_user_list_query2',
        params: { EventId: eventId, SearchOptionValue: searchOptionValue },
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.projectName == "EMBA") {
                pnlOptions.style.display = "none";
                pnlSearch.style.display = "none";
            }
            else {
                pnlOptions.style.display = "";
                pnlSearch.style.display = "";
                if (!loadOp) {
                    pnlOptions.innerHTML = d.options;
                    loadOp = true;
                }
            }
            pnlUserList.innerHTML = d.html;
        },
        failure: function (result) {
            pnlUserList.innerHTML = "数据加载错误：" + result.responseText;
        }
    });
    return order_no;
}
function getSearchOptionValue() {
    var str = "";
    var list = document.getElementsByTagName("input");
    if (list != null && list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            if (list[i].name == "op" && list[i].value != "") {
                str += "and " + list[i].getAttribute("cookie_name") + " like ''%" + list[i].value + "%'' ";
            }
        }
    }
    var list2 = document.getElementsByTagName("select");
    if (list2 != null && list2.length > 0) {
        for (var i = 0; i < list2.length; i++) {
            if (list2[i].name == "op" && list2[i].value != "") {
                str += "and " + list2[i].getAttribute("cookie_name") + "=''" + list2[i].value + "'' ";
            }
        }
    }
    return str;
}

