Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");
    
    var WKeywordReplyId = getUrlParam("WKeywordReplyId");
    if (WKeywordReplyId != "null" && WKeywordReplyId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WKeywordReply_by_id",
            params: { WKeywordReplyId: WKeywordReplyId },
            method: 'POST',
            success: function (response) {
                var storeId = "WKeywordReplyEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtKeyword").jitSetValue(getStr(d.Keyword));
                //Ext.getCmp("txtApplicationId").setDefaultValue(getStr(d.ApplicationId));
                //Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                
                Ext.getCmp("txtApplicationId").fnLoad(function(){
                    Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));
                    Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                });

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        myMask.hide();
    }
    
});

function fnClose() {
    CloseWin('WKeywordReplyEdit');
}

function fnSave() {
    var flag;
    var WKeywordReply = {};
    WKeywordReply.ReplyId = getUrlParam("WKeywordReplyId");
    WKeywordReply.Keyword = Ext.getCmp("txtKeyword").jitGetValue();
    WKeywordReply.ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    WKeywordReply.ModelId = Ext.getCmp("txtModelId").jitGetValue();

    if (WKeywordReply.Keyword == null || WKeywordReply.Keyword == "") {
        showError("请填写关键字");
        return;
    }
    if (WKeywordReply.ApplicationId == null || WKeywordReply.ApplicationId == "") {
        showError("请填写微信账号");
        return;
    }
    if (WKeywordReply.ModelId == null || WKeywordReply.ModelId == "") {
        showError("请填写模板");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WKeywordReply_save&WKeywordReplyId=' + WKeywordReply.ReplyId, 
        params: {
            "item": Ext.encode(WKeywordReply)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('WKeywordReplyEdit');
}

