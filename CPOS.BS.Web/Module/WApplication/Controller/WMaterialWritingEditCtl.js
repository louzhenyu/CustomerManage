var K;
var htmlEditor;

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

    var Id = getUrlParam("Id");
    if (Id != "null" && Id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WMaterialWriting_by_id",
            params: { Id: Id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtContent").jitSetValue(getStr(d.Content));

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
    CloseWin('WMaterialWritingEdit');
}

function fnSave() {
    var flag;
    var item = {};
    item.WritingId = getUrlParam("Id");
    item.ModelId = getUrlParam("ModelId");
    item.Content = Ext.getCmp("txtContent").jitGetValue();
    item.ApplicationId = getUrlParam("ApplicationId");

    if (item.Content == null || item.Content == "") {
        showError("请填写文本");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WMaterialWriting_save&Id=' + item.WritingId, 
        params: {
            "item": Ext.encode(item)
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
    CloseWin('WMaterialWritingEdit');
}

