Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/TagsHandler.ashx?mid=");
    
    var TagsId = getUrlParam("TagsId");
    if (TagsId != "null" && TagsId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_tags_by_id",
            params: { TagsId: TagsId },
            method: 'POST',
            success: function (response) {
                var storeId = "tagsEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtTagsName").jitSetValue(getStr(d.TagsName));
                Ext.getCmp("txtTypeId").jitSetValue(getStr(d.TypeId));
                Ext.getCmp("txtStatusId").jitSetValue(getStr(d.StatusId));
                Ext.getCmp("txtTagsDesc").jitSetValue(getStr(d.TagsDesc));
                Ext.getCmp("txtTagsFormula").jitSetValue(getStr(d.TagsFormula));

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

fnLoadRole = function() {
    var store = Ext.getStore("tagsEditRoleStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_tags_role_info_by_tags_id&TagsId=" + 
            getUrlParam("TagsId"),
        params: { start: 0, limit: 0 }
    });
}

function fnClose() {
    CloseWin('TagsEdit');
}

function fnSave() {
    var flag;
    var tags = {};
    tags.TagsId = getUrlParam("TagsId");
    tags.TagsName = Ext.getCmp("txtTagsName").jitGetValue();
    tags.TypeId = Ext.getCmp("txtTypeId").jitGetValue();
    tags.StatusId = Ext.getCmp("txtStatusId").jitGetValue();
    tags.TagsDesc = Ext.getCmp("txtTagsDesc").jitGetValue();
    tags.TagsFormula = Ext.getCmp("txtTagsFormula").jitGetValue();

    if (tags.TagsName == null || tags.TagsName == "") {
        showError("请填写标签名称");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Tags/Handler/TagsHandler.ashx?method=tags_save&TagsId=' + tags.TagsId, 
        params: {
            "tags": Ext.encode(tags)
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
    CloseWin('TagsEdit');
}

