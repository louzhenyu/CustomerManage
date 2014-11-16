var K;
var htmlEditor;
var K2;
var htmlEditor2;

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
    JITPage.HandlerUrl.setValue("Handler/WEventsHandler.ashx?mid=");

    //htmlEditor.html('');
    
    var RoundId = new String(JITMethod.getUrlParam("RoundId"));
    if (RoundId != "null" && RoundId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_round_by_id",
            params: {
                RoundId: RoundId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtRound").setDefaultValue(getStr(d.Round));
                Ext.getCmp("txtStatus").setDefaultValue(getStr(d.RoundStatus));
                
                //fnLoadPrizes();

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }
                fnLoadPrizes();
});

fnLoadPrizes = function () {
    var store = Ext.getStore("RoundPrizesStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_round_prizes_list_query&EventId=" +
        getUrlParam("EventID") + "&RoundId=" + getUrlParam("RoundId");
    store.pageSize = 1000;
    store.proxy.extraParams = {
        start: 0, limit: 0
    };
 
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()); });
}
function fnClose() {
    CloseWin('RoundEdit');
}

function fnSave() {
    var events = {};

    events.EventID = getUrlParam("EventID");
    events.RoundId = getUrlParam("RoundId");
    events.Round = Ext.getCmp("txtRound").jitGetValue();
    events.RoundDesc = Ext.getCmp("txtRound").rawValue;
    events.RoundStatus = Ext.getCmp("txtStatus").jitGetValue();

    if (events.Round == null || events.Round == "") {
        showError("必须输入轮次");
        return;
    }
    if (events.RoundStatus == null || events.RoundStatus == "") {
        showError("必须选择启用状态");
        return;
    }

    events.Prizes = [];
    var grid = Ext.getCmp("grid");
    if (grid.store.data.map != null) {
        for (var tmpItem in grid.store.data.map) {
            var objData = grid.store.data.map[tmpItem].data;
            var objItem = {};
            objItem.PrizesID = objData.PrizesID;
            objItem.PrizesCount = getFloat(objData.PrizesCount);
            objItem.WinnerCount = getFloat(objData.WinnerCount);

            if (objItem.PrizesCount < objItem.WinnerCount) {
                alert("奖品数量不能小于已中奖数量");
                return;
            }
            events.Prizes.push(objItem);
        }
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/WEventsHandler.ashx?method=round_save&RoundId=' + events.RoundId + "&EventID=" + events.EventID,
        params: {
            "events": Ext.encode(events)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch(getUrlParam("EventID"));
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    if (flag) fnClose();
}

