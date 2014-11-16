var id, btncode;//定义全局变量
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/LNewsTypeHander.ashx?mid=" + __mid);


    fnSearch("");

});

function fnSearch(lnewstypeid) {
    debugger;
    
    Ext.getStore("searchPartentTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPartentNewsType";
    Ext.getStore("searchPartentTypeStore").load();//这句话向后台请求数据了吗？

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetLNewsTypeList";
    //Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        typeid: lnewstypeid
    };
    Ext.getCmp("pageBar").moveFirst();
    // Ext.getStore("newsStore").load();
}

//添加窗体
function fnAddEditView() {
    id = null;
    Ext.getCmp("winParentTypeId").jitSetValue("");
    Ext.getStore("PartentTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPartentNewsType";
    Ext.getStore("PartentTypeStore").load();
    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();
}

//查看订单详细数据
function fnView(newstypeID) {
    debugger;
    id = newstypeID;
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("txt_NewsTypeName").jitSetValue("");
    // Ext.getCmp("winParentTypeId").jitSetValue("");
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetNewsTypeDetail",
        params: { newstTypeID: id },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            Ext.getCmp("txt_NewsTypeName").jitSetValue(d.NewsTypeName);
            Ext.getCmp("txt_ChannelCode").jitSetValue(d.ChannelCode);

            Ext.getStore("PartentTypeStore").removeAll();
            Ext.getStore("PartentTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetPartentNewsType";
            Ext.getStore("PartentTypeStore").load();
            debugger;
            //设置父类型的值
            Ext.getCmp("winParentTypeId").jitSetValue([{ "id": d.ParentTypeId, "text": d.ParentTypeName}]);

        }
    });
    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();
    myMask.hide();
}

//删除操作
function fnDelete(id) {



    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=DelLNewsTypeByID";
            Ext.Ajax.request({
                url: handlerUrl,
                params: {
                    newstTypeID: id
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: fnSearch()
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });

}

function fnSave() {
    var NewsTypeName = Ext.getCmp("txt_NewsTypeName").jitGetValue();
    if (NewsTypeName == null || NewsTypeName == "") {
        Ext.Msg.alert("提示", "请输入类型名称");
        return;
    }


    var ChannelCode = Ext.getCmp("txt_ChannelCode").jitGetValue();
    if (ChannelCode == null || ChannelCode == "") {
        Ext.Msg.alert("提示", "请输入频道编号");
        return;
    }
    if (isNaN(ChannelCode))
    {
        Ext.Msg.alert("提示", "频道编号必须是数字");
        return;
    }

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();
    var parenttypeId = "";
    if (Ext.getCmp("winParentTypeId").selectedValues != null && Ext.getCmp("winParentTypeId").selectedValues.length > 0) {
        parenttypeId = Ext.getCmp("winParentTypeId").selectedValues[0].id;//获取下拉框的值。
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=LNewsTypeSave",
        params: {
            form: Ext.JSON.encode(Ext.getCmp("editPanel").getValues()),
            parenttypeId: parenttypeId,
            newstypeid: id   //id
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            if (jdata.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        id = null;
                        myMask.hide();
                        Ext.getCmp("editWin").hide();
                        fnSearch();

                    }
                });
            }
            else {

                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            myMask.hide();
        }


    });
}
