var id, btncode;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitEditView();

    JITPage.HandlerUrl.setValue("Handler/OrdersEditHandler.ashx?mid=");

    fnSearch();
});

function fnSearch() {

    var order_id = new String(JITMethod.getUrlParam("order_id"));
    var op = new String(JITMethod.getUrlParam("op"));
    var st = new String(JITMethod.getUrlParam("st"));
    var pay = new String(JITMethod.getUrlParam("pay"));


    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetOrdersDetail",
            params: { id: order_id },
            method: 'post',
            success: function (response) {
                var jdata = Ext.decode(response.responseText).orderDetail[0];
                debugger;
                Ext.getCmp("txtOrderNo").setValue(jdata.OrderNo);
                Ext.getCmp("txt_order_date").jitSetValue(jdata.OrderDate);
                Ext.getCmp("txt_orderStatus").jitSetValue(jdata.Field10);
                Ext.getCmp("txt_model").jitSetValue(jdata.Field8);
                Ext.getCmp("txt_SerCode").jitSetValue(jdata.Field9);
                Ext.getCmp("txt_Price").jitSetValue(jdata.Field17);
                Ext.getCmp("txt_BuyWay").jitSetValue(jdata.Field11);
                Ext.getCmp("txt_BuyDate").jitSetValue(jdata.Field16);
                Ext.getCmp("txt_UserName").jitSetValue(jdata.Field12);
                Ext.getCmp("txt_Phone").jitSetValue(jdata.Field13);
                Ext.getCmp("txt_Email").jitSetValue(jdata.Field14);
                Ext.getCmp("txt_GetWay").jitSetValue(jdata.Field15);
                Ext.getCmp("txt_vipName").jitSetValue(jdata.VipName);

                Ext.getCmp("txt_school").jitSetValue(jdata.Field18);
                Ext.getCmp("txt_specialt").jitSetValue(jdata.Field19);
                Ext.getCmp("txt_intent").jitSetValue(jdata.Field20);

                if (jdata.Field7 != 100) {
                    Ext.getCmp("btn_1").hide();
                } else {
                    Ext.getCmp("btn_1").show();
                }

                var imgHtml = "";
                imgHtml += (jdata.Field1 == null || jdata.Field1 == "") ? "" : "<a href='#'><img src='" + jdata.Field1.toString() + "' alt=''></a>";
                imgHtml += (jdata.Field2 == null || jdata.Field2 == "") ? "" : "<a href='#'><img src='" + jdata.Field2.toString() + "' alt=''></a>";
                imgHtml += (jdata.Field3 == null || jdata.Field3 == "") ? "" : "<a href='#'><img src='" + jdata.Field3.toString() + "' alt=''></a>";
                imgHtml += (jdata.Field4 == null || jdata.Field4 == "") ? "" : "<a href='#'><img src='" + jdata.Field4.toString() + "' alt=''></a>";
                imgHtml += (jdata.Field5 == null || jdata.Field5 == "") ? "" : "<a href='#'><img src='" + jdata.Field5.toString() + "' alt=''></a>";
                imgHtml += (jdata.Field6 == null || jdata.Field6 == "") ? "" : "<a href='#'><img src='" + jdata.Field6.toString() + "' alt=''></a>";

                $(".a_b_aa").html(imgHtml);
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
}


//审核
function fnbtn_1() {
    fnbtnChange("1");
}


//按钮对应功能的隐藏显示
function fnbtnChange(btnIndex) {

    if (btnIndex == "1") {
        //审核
        optIndex = "1";
    } 

    Ext.getCmp("operationPanel").getForm().reset();
    Ext.getCmp("operationWin").show();

}

function fnCheckStatusChange() {

    if (Ext.getCmp("CheckStatus").jitGetValue() == "2") {
        Ext.getCmp("CheckResult").show();
    } else {
        Ext.getCmp("CheckResult").hide();
    }

}

function fnOperationSubmit() {
    var form = Ext.getCmp('operationPanel').getForm();
    if (!form.isValid()) {
        return false;
    }

    form.submit({
        url: "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?mid=&method=UpdateStatus",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            order_id: new String(JITMethod.getUrlParam("order_id")),
            optIndex: optIndex,
            PicUrl: ""
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        parent.location.reload();
                    }
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
