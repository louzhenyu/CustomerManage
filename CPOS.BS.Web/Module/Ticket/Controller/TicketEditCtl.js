
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
    JITPage.HandlerUrl.setValue("Handler/TicketHandler.ashx?mid=");


    var TicketID = new String(JITMethod.getUrlParam("TicketID"));
    if (TicketID != "null" && TicketID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_ticket_by_id",
            params: {
                TicketID: TicketID
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("TicketName").jitSetValue(getStr(d.TicketName));
                Ext.getCmp("EventID").jitSetValue([{ "id": d.EventID, "text": d.Title}]);
                Ext.getCmp("TicketPrice").jitSetValue(getStr(d.TicketPrice));
                Ext.getCmp("TicketNum").jitSetValue(getStr(d.TicketNum));
                Ext.getCmp("TicketSort").jitSetValue(getStr(d.TicketSort));
                Ext.getCmp("TicketRemark").jitSetValue(getStr(d.TicketRemark));

                
                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }
});

function fnClose() {
    CloseWin('TicketEdit');
}

function fnSave() {
    var ticket = {};

    ticket.TicketID = getUrlParam("TicketID"); 
    if (ticket.TicketID == undefined || ticket.TicketID == null || ticket.TicketID.length == 0) ticket.TicketID = newGuid();

    ticket.TicketName = Ext.getCmp("TicketName").jitGetValue();
    ticket.EventID = Ext.getCmp("EventID").jitGetValue();
    ticket.TicketPrice = Ext.getCmp("TicketPrice").jitGetValue();
    ticket.TicketNum = Ext.getCmp("TicketNum").jitGetValue();
    ticket.TicketSort = Ext.getCmp("TicketSort").jitGetValue();
    ticket.TicketRemark = Ext.getCmp("TicketRemark").jitGetValue();


    if (ticket.TicketName == null || ticket.TicketName == "") {
        showError("必须输入票务名称");
        return;
    }

    if (ticket.EventID == null || ticket.EventID == "") {
        showError("必须选择活动");
        return;
    }


    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/TicketHandler.ashx?method=ticket_save&TicketID=' + ticket.TicketID,
        params: {
            "ticket": Ext.encode(ticket)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {

                Ext.Msg.show({
                    title: "提示",
                    msg: "保存数据成功",
                    icon: Ext.Msg.INFO,
                    buttons: Ext.Msg.OK,
                    minWidth: 260,
                    modal: true,
                    fn: function () {
                        parent.fnSearch();
                        fnClose();
                    }
                });

            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

