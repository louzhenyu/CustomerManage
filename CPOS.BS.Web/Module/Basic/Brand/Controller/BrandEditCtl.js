Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/BrandHandler.ashx?mid=" + __mid);

    var btncode = "";
    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        btncode = "update";
    }
    else {
        id = "";
        btncode = "create";
    }
    Ext.getStore("brandStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode;
    Ext.getStore("brandStore").load();

    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=update&method=GetBrandByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                var jdata = eval(response.responseText);
                //加载form
                Ext.getStore("brandStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("brandStore").first());
                //修改时设置上级品牌的值
                if (jdata[0].ParentID != null || jdata[0].ParentID != "")
                    var __sParentID = jdata[0].ParentID;
                Ext.getCmp("cmbSingleSelect").jitSetValue(__sParentID);

                var ctl_txtSelectedItems = Ext.getCmp('cmbSingleSelect');
                var ctl_cmtMultiSelectionSaleAreaSelectLeafOnlyAndInit = Ext.getCmp('cmbSingleSelect');
                var selectedItems = Ext.JSON.decode(ctl_txtSelectedItems.getValue());
                ctl_cmtMultiSelectionSaleAreaSelectLeafOnlyAndInit.jitSetValue(selectedItems, false);

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

function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }
    
    var btncode = "";
    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        btncode = "update";
    }
    else {
        id = "";
        btncode = "create";
    }
    //获取上级品牌ID
    var selectedValues = Ext.getCmp('cmbSingleSelect').jitGetValue();
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Edit",
        waitMsg: '提交数据中...',
        params: {
            id: id,
            //表单之外的参数传值方法(此参数不可以与BrandEditView.js中的上级品牌中的name值相同，否则将被覆盖掉。)
            ParentID: selectedValues
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
            //userStore.load();
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnCancel() {
    window.location = "Brand.aspx?mid=" + __mid;
}

function fnControlTypeChange() {
    switch (parseInt(this.getValue())) {
        case 1:
            Ext.getCmp("cmbSingleSelect").hide();
            return;
        case 0:
            Ext.getCmp("cmbSingleSelect").show();
            return;
    }
}