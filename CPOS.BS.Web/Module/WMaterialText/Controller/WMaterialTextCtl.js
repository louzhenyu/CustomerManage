Ext.onReady(function () {

    InitVE();
    InitStore();
    InitEditView();
    JITPage.HandlerUrl.setValue("Handler/WMaterialTextHandler.ashx?mid=" + window.mid);
    fnSearch();
    fnWmaterialType();

});

function fnWmaterialType() {

    Ext.getStore("TypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetMaterialTextType";
    var Store = Ext.getStore("TypeStore");
    Store.load();
}

function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    //Ext.getCmp("pageBar").moveFirst();

    Ext.getCmp("pageBar").store.load(function (options) {
        if (options == null || options.length == 0) {
            $(".x-grid-header-row").after('<tr><td></td><td colspan="5" class="" style="height: 40px;line-height: 40px;padding-left: 25px;">没有符合条件的查询记录</td></tr>');
        }
    });
}

function fnCreate() {
    var mid = JITMethod.getUrlParam("mid");
    var url = "../WeiXin/ImageContent.aspx?type=add" + "&mid=" + window.mid;;
    window.open(url, "_top");

}
function fnEdit(id) {
    var mid = JITMethod.getUrlParam("mid");
    var url = "../WeiXin/ImageContent.aspx?type=edit&imageContentId=" + id + "&mid=" + window.mid;
    window.open(url, "_top");
}

function fnDelete(id) {
    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=Delete",
            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&method=Delete",
                params: {
                    textID: id
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success && jdata.success != "false") {
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


function setImg() {
    if ($('a[rel=fancybox_group]').length == 0) {
        return;
    }
    $('a[rel=fancybox_group]').fancybox({
        openEffect: 'none',
        closeEffect: 'none',
        prevEffect: 'none',
        nextEffect: 'none',
        closeBtn: true
    });

}




