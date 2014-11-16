//Ext.onReady(function () {
//    //加载需要的文件
//    InitVE();
//    InitStore();
//    InitView();

//    //页面加载
//    //JITPage.PageSize.setValue(15);
//    JITPage.HandlerUrl.setValue("Handler/ParameterHandler.ashx?mid=" + __mid);

//   
//});

function fnPOPTypeChange() {
//    alert(this.getValue());
    var type= parseInt(this.getValue());
    if (type == 1) {
        Ext.getCmp("cdpEditPanel_StoreList").show();
        Ext.getCmp("cdpEditPanel_DistributorList").hide();
    }
    else if (type == 2) {
        Ext.getCmp("cdpEditPanel_StoreList").hide();
        Ext.getCmp("cdpEditPanel_DistributorList").show();
    }
}

function fnSubmit() {
    var form = Ext.getCmp('cdpEditPanel').getForm();
    if (!form.isValid()) {
        return false;
    }

    if(Ext.getCmp("cdpEditPanel_ClientUserID").jitGetValue()==undefined
        ||Ext.getCmp("cdpEditPanel_ClientUserID").jitGetValue()==null
        ||Ext.getCmp("cdpEditPanel_ClientUserID").jitGetValue()=="")
    {
         Ext.Msg.show({
                    title: '提示',
                    msg: "请选择人员",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
    return false;
    }

    var type = parseInt(Ext.getCmp("cdpEditPanel_POPType").jitGetValue());
    if (type == 1) {
        if (Ext.getCmp("cdpEditPanel_StoreList").jitGetValue() == undefined
        || Ext.getCmp("cdpEditPanel_StoreList").jitGetValue() == null
        || Ext.getCmp("cdpEditPanel_StoreList").jitGetValue() == "") {
            Ext.Msg.show({
                title: '提示',
                msg: "请选择终端",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }
    else if (type == 2) {
        if (Ext.getCmp("cdpEditPanel_DistributorList").jitGetValue() == undefined
        || Ext.getCmp("cdpEditPanel_DistributorList").jitGetValue() == null
        || Ext.getCmp("cdpEditPanel_DistributorList").jitGetValue() == "") {
            Ext.Msg.show({
                title: '提示',
                msg: "请选择终端",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=update&method=EditCDP",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            ClientUserID: Ext.getCmp("cdpEditPanel_ClientUserID").getValue(),
            CallDate: Ext.getCmp("cdpEditPanel_CallDate").getValue(),
            POPType: Ext.getCmp("cdpEditPanel_POPType").jitGetValue(),
            StoreList: Ext.getCmp("cdpEditPanel_StoreList").jitGetValue(),
            DistributorList: Ext.getCmp("cdpEditPanel_DistributorList").jitGetValue(),
            Remark: Ext.getCmp("cdpEditPanel_Remark").getValue()
        },
        success: function (fp, o) {

            Ext.getCmp("cdpEditWin").hide();

            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                window.location = window.location;

                //                var focus = 1;
                //                parent.tab2State = false;
                //                parent.tab3State = false;
                //                parent.document.getElementById("tab1").setAttribute("src", "RouteEdit.aspx" + "?mid=" + __mid + "&id=" + o.result.id + "&focus=" + focus + "&btncode=" + btncode);

            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: "编辑失败",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnCancel() {
    Ext.getCmp("cdpEditWin").hide();
}