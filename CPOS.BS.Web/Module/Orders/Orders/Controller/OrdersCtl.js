var id, btncode, status, ordersNo, linkMan, linkTel, userId,VipName;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/OrdersHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnSearch() {
    var status = get("hStatus").value; //记录当前的栏目
    fnSearchOrder(status);
}

/*查询方法*/
function fnSearchOrder(status) {
    get("hStatus").value = status;
    for (var i = 0; i < 600; i += 100) {
        get('tab' + i).style.background = "#E6E4E1";
    }
    get('tab' + status).style.background = "#F3F3F6";

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        pOrdersNo: Ext.getCmp("txtOrdersNo").jitGetValue(),
        pStartDate: Ext.getCmp("dtStartDate").jitGetValue(),
        pEndDate: Ext.getCmp("dtEndDate").jitGetValue(),
        pStoreName: Ext.getCmp("txtStoreName").jitGetValue(),
        pItemName: Ext.getCmp("txtRoomType").jitGetValue(),
        pUserName: Ext.getCmp("txtGuestName").jitGetValue(),
        pStatus: status
    };
    Ext.getCmp("pageBar").moveFirst();
    Ext.getCmp("pageBar").store.load({
        callback: function () {
            fnGetOrdersCount();
        }
    });
}

function fnGetOrdersCount() {
    Ext.Ajax.request({
        method: 'post',
        sync: true,
        url: 'Handler/OrdersHandler.ashx?method=GetOrdersStatusCount',
        params: {
            pOrdersNo: Ext.getCmp("txtOrdersNo").jitGetValue(),
            pStartDate: Ext.getCmp("dtStartDate").jitGetValue(),
            pEndDate: Ext.getCmp("dtEndDate").jitGetValue(),
            pStoreName: Ext.getCmp("txtStoreName").jitGetValue(),
            pItemName: Ext.getCmp("txtRoomType").jitGetValue(),
            pUserName: Ext.getCmp("txtGuestName").jitGetValue(),
            pStatus: status
        },
        success: function (result) {
            var jdata = Ext.decode(result.responseText);
            get("txtNum0").innerHTML = jdata.allCount;
            get("txtNum1").innerHTML = jdata.approveCount;
            get("txtNum2").innerHTML = jdata.checkCount;
            get("txtNum3").innerHTML = jdata.completeCount;
            get("txtNum4").innerHTML = jdata.cancelCount;
            get("txtNum5").innerHTML = jdata.notAuditCount;
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}

/*编辑备注列渲染*/
function fnColumnRemark(value, p, r) {
    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnEditRemark('" + r.data.OrdersID + "','" + r.data.Remark + "','update');\">" + (value == "" ? "..." : value) + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnEditRemark('" + r.data.OrdersID + "','" + r.data.Remark + "','search');\">" + (value == "" ? "..." : value) + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}

/*提交订单备注*/
function fnSubmitRemark() {
    var form = Ext.getCmp('editRemarkPanel').getForm();

    if (Ext.getCmp("txtRemark").getValue() == null || Ext.getCmp("txtRemark").getValue() == "") {
        Ext.Msg.show({
            title: '警告',
            msg: "'备注'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("txtRemark").getValue().length > 500) {
        Ext.Msg.show({
            title: '警告',
            msg: '备注不能大于500个字符',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    if (!form.isValid()) {
        return false;
    }

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    var btn = this;
    btn.setDisabled(true);

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&method=EditOrderRemark",
        waitMsg: JITPage.Msg.GetData,
        params: {
            id: id,
            Remark: Ext.getCmp("txtRemark").getValue()
        },
        success: function (fp, o) {
            myMask.hide();
            btn.setDisabled(false);
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("editRemarkWin").hide();
                        fnSearchOrder(get("hStatus").value);
                    }
                });
            }
            else {
                myMask.hide();
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            myMask.hide();
            btn.setDisabled(false);
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

/*编辑订单备注*/
function fnEditRemark(pid, val, bcode) {
    id = pid;
    Ext.getCmp("txtRemark").jitSetValue(val);
    Ext.getCmp("editRemarkWin").show();
}

/*编辑列渲染*/
function fnColumnUpdate(value, p, r) {
    if (!__getHidden("update")) {
        //修改权限
        return fnOrdersStatus(r.data.OrdersID, 'update', r.data.OrdersStatus, r.data.PayType);
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return fnOrdersStatus(r.data.OrdersID, 'update', r.data.OrdersStatus, r.data.PayType);
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\"></a>";
    }
}

/*订单状态判断*/
function fnOrdersStatus(pid, bcode, pStatus, pPayType) {
    id = pid;
    btncode = bcode;

    var html = "";
    switch (pStatus) {
        //待审核订单                                                   
        case "100":
            Ext.getCmp("approveWin").setTitle("订单详情 -- 审核");
            html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnView('" + pid + "','" + pStatus + "','" + pPayType + "','" + bcode + "');\">处理</a>";
            break;
        //待入住订单                                                 
        case "200":
            Ext.getCmp("approveWin").setTitle("订单详情 -- 入住");
            html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnView('" + pid + "','" + pStatus + "','" + pPayType + "','" + bcode + "');\">处理</a>";
            break;
        default:
            Ext.getCmp("approveWin").setTitle("订单详情 -- 查看");
            html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnView('" + pid + "','" + pStatus + "','" + pPayType + "','" + bcode + "');\">查看</a>";
            break;
    }
    return html;
}

/*查看订单明细*/
function fnView(pid, pStatus, pPayType, bcode) {

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    id = pid;
    btncode = bcode;
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    status = parseInt(pStatus) + 100;

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetOrdersDetail",
        params: { "id": id ,"isHotel":"1"},
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            if (jdata.orderDetail.length > 0) {
                var res = jdata.orderDetail[0];
                ordersNo = res.OrdersNo;
                linkMan = res.GuestName;
                VipName = res.VipName;                
                linkTel = res.LinkTel;
                userId = res.VipID;
                Ext.getCmp("txt_OrdersNo").jitSetValue(res.OrdersNo);
                Ext.getCmp("txt_GuestName").jitSetValue(res.GuestName);
                Ext.getCmp("txt_LinkTel").jitSetValue(res.LinkTel);
                Ext.getCmp("txt_OrdersDate").jitSetValue(res.StartDate + ' 至 ' + res.EndDate + ' 共' + res.QTY + '晚');
                Ext.getCmp("txt_RoomCount").jitSetValue(res.RoomCount + '间');
                Ext.getCmp("txt_hdRemark").jitSetValue(res.Remark);
                Ext.getCmp("txt_RoomTypeName").jitSetValue(res.StoreName + ' - ' + res.RoomTypeName);
                Ext.getCmp("txt_Payment").jitSetValue('到店付款，实付 RMB ' + res.Amount + '元');

                Ext.getCmp("txt_couponAmount").jitSetValue('RMB ' + res.couponAmount + '元');
                Ext.getCmp("txt_integral").jitSetValue('RMB ' + res.integral + '元');
                Ext.getCmp("txt_vipEndAmount").jitSetValue('RMB ' + res.vipEndAmount + '元');
                Ext.getCmp("txt_totalamount").jitSetValue('应付 RMB ' + res.totalamount + '元');
            }

            if (jdata.orderStatus.length > 0) {
                var html = '', res = jdata.orderStatus;
                html += jdata.orderStatus[0].OrderDate + ' 客户提交订单' + '<br/><br/>';
                for (var i = 1; i < jdata.orderStatus.length; i++) {
                    html += res[i].OperaterTime + fnGetOrderStatus(res[i].OrderStatus) + '<br/><br/>';
                }
                Ext.getCmp("txt_OperationDetail").jitSetValue(html);
            }
            myMask.hide();

            Ext.getCmp("approveWin").show();

            fnChangeBtnByStatus(pStatus);

        },
        failure: function (response) {
            myMask.hide();
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

/*根据状态值判断订单操作流水*/
function fnGetOrderStatus(val) {
    var html = '';
    switch (val.toString()) {
        case "200":
            html = ' 管理员审核通过 ';
            break;
        case "300":
            html = ' 管理员完成订单 ';
            break;
        case "400":
            html = ' 取消订单 ';
            break;
        case "500":
            html = ' 管理员审核不通过 ';
        case "800":
            html = ' 客户取消订单 ';
            break;

    }
    return html;
}
function fnStatusDesc(val) {

    var html = '';
    switch (val.toString()) {
        case "200":
            html = ' 订单已确认 ';
            break;
        case "300":
            html = ' 已完成 ';
            break;
        case "400":
            html = ' 已取消 ';
            break;
        case "500":
            html = ' 审核不通过 ';
            break;
    }
    return html;

}
/*根据订单状态控制按钮隐藏 显示*/
function fnChangeBtnByStatus(val) {
    switch (val.toString()) {
        //待审核                                                     
        case "100":
            Ext.getCmp("btnSave").show();
            Ext.getCmp("btnCancel").show();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
        //待入住                       
        case "200":
            Ext.getCmp("btnSave").hide();
            Ext.getCmp("btnCancel").hide();
            Ext.getCmp("btnComplete").show();
            Ext.getCmp("btnCancelOrder").show();
            break;
        //审核完成                                                     
        case "300":
            Ext.getCmp("btnSave").hide();
            Ext.getCmp("btnCancel").hide();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
        //取消订单                                                      
        case "400":
            Ext.getCmp("btnSave").hide();
            Ext.getCmp("btnCancel").hide();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
        //审核不通过                                                      
        case "500":
            Ext.getCmp("btnSave").hide();
            Ext.getCmp("btnCancel").hide();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
        case "800":
            Ext.getCmp("btnSave").hide();
            Ext.getCmp("btnCancel").hide();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
        default:
            Ext.getCmp("btnCancel").show();
            Ext.getCmp("btnClose").hide();
            Ext.getCmp("btnComplete").hide();
            Ext.getCmp("btnCancelOrder").hide();
            break;
    }
}

/*订单_审核通过*/
function fnAuditOrders() {

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    var btn = this;
    btn.setDisabled(true);

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=OrderApprove",
        params: {
            pOrdersID: id,
            pOrdersType: 1,
            pOrdersStatus: status,
            pOrderDesc: fnStatusDesc(status),
            pCheckResult: "",
            pRemark: ""
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            //提示错误信息
            if (jdata.msg != null) {
                myMask.hide();

                //fnSendMsg(1, ordersNo, linkMan, linkTel, userId);

                btn.setDisabled(false);
                Ext.Msg.show({
                    title: "提示",
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("approveWin").hide();
                        fnSearchOrder(get("hStatus").value);
                    }
                });
            }
        },
        failure: function () {
            myMask.hide();
            btn.setDisabled(false);
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

/*显示不通过窗口*/
function fnShowNotAuditWin() {
    Ext.getCmp("notAuditPanel").getForm().reset();
    Ext.getCmp("notAuditWin").show();
}

/*订单_审核不通过*/
function fnNotAudit() {
    var form = Ext.getCmp('notAuditPanel').getForm();

    if (Ext.getCmp("rdReason").getValue().rb == "3" && (Ext.getCmp("txt_Remark").getValue() == null || Ext.getCmp("txt_Remark").getValue() == "")) {
        Ext.Msg.show({
            title: '警告',
            msg: "'备注'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("txt_Remark").getValue().length > 500) {
        Ext.Msg.show({
            title: '警告',
            msg: '备注不能大于500个字符',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    if (!form.isValid()) {
        return false;
    }

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    var btn = this;
    btn.setDisabled(true);

    var remark = '';
    if (Ext.getCmp("rdReason").getValue().rb == 3) {
        remark = Ext.getCmp("txt_Remark").jitGetValue();
    } else if (Ext.getCmp("rdReason").getValue().rb == 2) {
        remark = $('#rb2-boxLabelEl').html();
    } else if (Ext.getCmp("rdReason").getValue().rb == 1) {
        remark = $('#rb1-boxLabelEl').html();
    }

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=OrderApprove",
        params: {
            pOrdersID: id,
            pOrdersType: 2,
            pOrdersStatus: 500,
            pOrderDesc: fnStatusDesc(500),
            pCheckResult: Ext.getCmp("rdReason").getValue().rb,
            pRemark: remark
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            //提示错误信息
            if (jdata.msg != null) {
                myMask.hide();

                fnSendMsg(2, ordersNo, VipName, linkTel, userId);

                btn.setDisabled(false);
                Ext.Msg.show({
                    title: "提示",
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("notAuditWin").hide();
                        Ext.getCmp("approveWin").hide();
                        fnSearchOrder(get("hStatus").value);
                    }
                });
            }
        },
        failure: function () {
            myMask.hide();
            btn.setDisabled(false);
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

/*订单_完成*/
function fnComplete() {

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    var btn = this;
    btn.setDisabled(true);

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Complete",
        params: {
            id: id,
            status: status
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            //提示错误信息
            if (jdata.msg != null) {
                myMask.hide();
                btn.setDisabled(false);
                Ext.Msg.show({
                    title: "提示",
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("approveWin").hide();
                        fnSearchOrder(get("hStatus").value);
                    }
                });
            }
        },
        failure: function () {
            myMask.hide();
            btn.setDisabled(false);
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

/*订单打印*/
function fnPrint() {
    var url = "/Module/Orders/Orders/PrintOrders.aspx?pOrdersID=" + id;
    window.open(url, "打印订单", "height=800,width=1000,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no");
}

/*列表付款方式 默认显示 到店付款*/
function fnShowPayment(value, p, r) {
    return "到店付款";
}

/*取消订单*/
function fnCancelOrders() {

    var btn = this;
    btn.setDisabled(true);

    Ext.Msg.confirm("请确认", "确认取消此订单吗？", function (button) {
        if (button == "yes") {

            var myMask_info = JITPage.Msg.GetData;
            var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
            myMask.show();

            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=OrderApprove",
                params: {
                    pOrdersID: id,
                    pOrdersType: 2,
                    pOrdersStatus: 400,
                    pOrderDesc: fnStatusDesc(400),
                    pCheckResult: "",
                    pRemark: ""
                },
                method: 'post',
                success: function (response) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    //提示错误信息
                    if (jdata.msg != null) {
                        myMask.hide();
                        btn.setDisabled(false);
                        Ext.Msg.show({
                            title: "提示",
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: function () {
                                Ext.getCmp("approveWin").hide();
                                fnSearchOrder(get("hStatus").value);
                            }
                        });
                    }
                },
                failure: function () {
                    myMask.hide();
                    btn.setDisabled(false);
                    Ext.Msg.alert("提示", "操作失败");
                }
            });
        }
    });
}

//导出订单
function fnExport() {
    var type = get("hStatus").value;

    //确定是否导出当前数据
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {
            //console.log(OrdersViewColumns);
            //导出当前数据
            var myMask_info = JITPage.Msg.GetData;
            var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
            myMask.show();

            Ext.Ajax.request(
                {
                    url: JITPage.HandlerUrl.getValue() + "&method=Export",
                    params: {
                        Columns: Ext.JSON.encode(OrdersViewColumns),
                        pOrdersNo: Ext.getCmp("txtOrdersNo").jitGetValue(),
                        pStartDate: Ext.getCmp("dtStartDate").jitGetValue(),
                        pEndDate: Ext.getCmp("dtEndDate").jitGetValue(),
                        pStoreName: Ext.getCmp("txtStoreName").jitGetValue(),
                        pItemName: Ext.getCmp("txtRoomType").jitGetValue(),
                        pUserName: Ext.getCmp("txtGuestName").jitGetValue(),
                        pStatus: type
                    },
                    method: 'POST',
                    success: function (res) {
                        var obj = res.responseText;
                        window.location.href = obj;
                        myMask.hide();
                    },
                    failure: function () {
                        Ext.Msg.alert("提示", "导出失败!");
                        myMask.hide();
                    }
                });
        }
    });

}

function fnSendMsg(pType, pOrderNo, pName, pTel, pUserID) {
    if (pType == 1) {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=sendMsg",
            params: {
                'csPipelineId': 1,
                'isCS': '1',
                'messageContent': '亲爱的' + pName + '您好，感谢您选择花间堂！您的订单' + pOrderNo + '已确认，期待您的入住。您可以进入个人中心页面随时关注订单状态，如有疑问请致电4000 767 123。',
                'userid': pUserID,
                'sign': ''
            },
            method: 'post',
            success: function (response) {
                var jdata = Ext.JSON.decode(response.responseText);
                //提示错误信息
                //                if (jdata.msg != null) {
                //                    Ext.Msg.show({
                //                        title: "提示",
                //                        msg: jdata.msg,
                //                        buttons: Ext.Msg.OK,
                //                        icon: Ext.Msg.INFO
                //                    });
                //                }
            },
            failure: function () {
                //Ext.Msg.alert("提示", "操作失败");
            }
        });
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=sendMsg",
            params: {
                'csPipelineId': 2,
                'isCS': '1',
                'messageContent': '亲爱的' + pName + '您好，感谢您选择花间堂！您的订单' + pOrderNo + '已确认，期待您的入住。您可以进入个人中心页面随时关注订单状态，如有疑问请致电4000 767 123。',
                'userid': pUserID,
                'sign': '花间堂',
                'mobile': pTel
            },
            method: 'post',
            success: function (response) {
                var jdata = Ext.JSON.decode(response.responseText);
                //提示错误信息
                //                if (jdata.msg != null) {
                //                    Ext.Msg.show({
                //                        title: "提示",
                //                        msg: jdata.msg,
                //                        buttons: Ext.Msg.OK,
                //                        icon: Ext.Msg.INFO
                //                    });
                //                }
            },
            failure: function () {
                //Ext.Msg.alert("提示", "操作失败");
            }
        });
    } else {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=sendMsg",
            params: {
                'csPipelineId': 1,
                'isCS': '1',
                'messageContent': '亲爱的' + pName + '您好，感谢您选择花间堂！很抱歉，您的订单' + pOrderNo + '，由于满房暂时无法确认，如果房间有空出，我们会及时联系您。如有疑问请致电4000 767 123。',
                'userid': pUserID,
                'sign': ''
            },
            method: 'post',
            success: function (response) {
                var jdata = Ext.JSON.decode(response.responseText);
                //提示错误信息
                //                if (jdata.msg != null) {
                //                    Ext.Msg.show({
                //                        title: "提示",
                //                        msg: jdata.msg,
                //                        buttons: Ext.Msg.OK,
                //                        icon: Ext.Msg.INFO
                //                    });
                //                }
            },
            failure: function () {
                //Ext.Msg.alert("提示", "操作失败");
            }
        });
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=sendMsg",
            params: {
                'csPipelineId': 2,
                'isCS': '1',
                'messageContent': '亲爱的' + pName + '您好，感谢您选择花间堂！很抱歉，您的订单' + pOrderNo + '，由于满房暂时无法确认，如果房间有空出，我们会及时联系您。如有疑问请致电4000 767 123。',
                'userid': pUserID,
                'sign': '花间堂',
                'mobile': pTel
            },
            method: 'post',
            success: function (response) {
                var jdata = Ext.JSON.decode(response.responseText);
                //                //提示错误信息
                //                if (jdata.msg != null) {
                //                    Ext.Msg.show({
                //                        title: "提示",
                //                        msg: jdata.msg,
                //                        buttons: Ext.Msg.OK,
                //                        icon: Ext.Msg.INFO
                //                    });
                //                }
            },
            failure: function () {
                //Ext.Msg.alert("提示", "操作失败");
            }
        });
    }
}