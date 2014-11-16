var myMask_info = "loading...";
var nextStatus;   //下级状态(jifeng.cao)
var ary_items = [];  //按钮集合(jifeng.cao)
Ext.QuickTips.init();
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    fnInitVE();
    fnInitStore();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/Inout3Handler.ashx?mid=");

    fnInitPageContent();
});

//初始化显示内容
function fnInitPageContent() {

    var order_id = new String(JITMethod.getUrlParam("order_id"));
    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetOrderStatusList",
            params: { order_id: order_id },
            sync: true,
            method: 'post',
            success: function (result, request) {
                var d = Ext.decode(result.responseText);
                if (!d.success) {
                    //alert("操作失败：" + d.msg);
                } else {
                    ary_items = eval(d.data);

                    InitView();
                    Ext.getCmp("btn_2").hide();
                    fnGetInoutInfoById();
                    fnGetVipInfo();
                    fnLoadItems();
                }
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取数据失败");
            }
        });
    }

}

function fnGetInoutInfoById() {
    var order_id = new String(JITMethod.getUrlParam("order_id"));
    var op = new String(JITMethod.getUrlParam("op"));
    var st = new String(JITMethod.getUrlParam("st"));
    var pay = new String(JITMethod.getUrlParam("pay"));

    //判断是否支付(jifeng.cao)
    if (pay == "1") {
        Ext.getCmp("btn_2").hide();
    } else {
        Ext.getCmp("btn_2").show();
    }
    //对应订单状态的操作权限(jifeng.cao)
    if (st != "") {
        fnOptChange(st);
    } else {
        fnOptChange(op);
    }
    fnSearch();

    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetInoutInfoById_lj",
            params: { order_id: order_id },
            method: 'post',
            success: function (response) {
                var storeId = "salesOutOrderEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;//解码
                console.log(response.responseText);   //打出来了，看看是什么
        
                Ext.getCmp("txtRemark").setValue(getStr(d.remark));
                fnStatusSetting(d.Field7)
                Ext.getCmp("txtOrderNo").setValue(getStr(d.order_no));
                //Ext.getCmp("txtOrderDate").setValue(getStr(d.order_date));


                Ext.getCmp("txtSalesUnitId").jitSetValue([{ "id": d.sales_unit_id, "text": d.sales_unit_name}]);
                Ext.getCmp("txtPurchaseUnitName").setValue(getStr(d.purchase_unit_name));

                //支付信息模块
             
                Ext.getCmp("tbKeepTheChange").setValue(getStr(d.keep_the_change)); //找零
                Ext.getCmp("tbWipingZero").setValue(getStr(d.wiping_zero)); //抹零
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name)); //收银员
                Ext.getCmp("tbSYCreateTime").setValue(getStr(d.create_time)); //收银时间

                //第一行
                //支付方式**
                Ext.getCmp("txtPayment_name").setValue(getStr(d.payment_name));
                Ext.getCmp("txtinvoice").setValue(getStr(d.Field19));//发票抬头
                Ext.getCmp("tbDiscountRate").setValue(getStr(d.discount_rate)); //折扣
                Ext.getCmp("txtTotalRetail").setValue(getStr(d.total_retail));//应付金额
                Ext.getCmp("txtTotalAmount").setValue(getStr(d.actual_amount));//现在用的是应付金额

                //第二行

                Ext.getCmp("txtFieldDeliveryAmount").setValue(getStr(d.DeliveryAmount));//配送费，还在配送信息里
                var sWhole = "";
                sWhole += "商品总额(￥" + (d.total_amount - d.DeliveryAmount )+ ")";
                if (d.DeliveryAmount!= 0)
                {
                sWhole += "+配送费(￥" + d.DeliveryAmount + ")";
                }
                if (d.pay_pointsAmount != 0)
                {
                sWhole += "-积分折扣(￥" + d.pay_pointsAmount + ")";
                }
                if (d.couponAmount != 0)
                {
                sWhole += "-优惠券折扣(￥" + d.couponAmount + ")";
                }
                if (d.vipEndAmount != 0)
                {
                sWhole += "-余额支付(￥" + d.vipEndAmount + ")";
                }
                Ext.getCmp("lab_wholeOrder").setValue(sWhole);



                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name));
                Ext.getCmp("txtCreateTime").jitSetValue(getStr(d.create_time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.modify_user_name));
                Ext.getCmp("txtModifyTime").jitSetValue(getStr(d.modify_time));



                var lblStatus = getStr(d.Field10);
                if (d.Field7 == "900") {
                    var model900 = Ext.getStore("inoutStatusStore").getById(900);
                    if (model900 != null) {
                        if (getStr(model900.get("CheckResultName")) != "") {
                            lblStatus += "-" + getStr(model900.get("CheckResultName"));
                        }
                    }
                }
                Ext.getCmp("lab_Status").setValue(lblStatus);


                Ext.getCmp("txtVipName").setValue(getStr(d.vip_name));
                Ext.getCmp("txtVipPhone").setValue(getStr(d.vipPhone));
                //Ext.getCmp("txtDefrayTypeName").setValue(getStr(d.DefrayTypeName));


                var pay_str = "";
                var model = Ext.getStore("inoutStatusStore").getById(10000);
                if (model != null) {
                    if (getStr(model.get("PayMethodName")) != "") {
                        if (getStr(model.get("PicUrl")) != "") {
                            pay_str += '<a rel="fancybox_group0" href="' + getStr(model.get("PicUrl")) + '" title="拍摄时间：' + getStr(model.get("LastUpdateTimeFormat")) + '"><img src="/Lib/Image/image.png" /></a>';
                        }
                    }
                }
                //                pay_str += (getStr(d.DefrayTypeName) == "" ? "在线支付" : getStr(d.DefrayTypeName));

                var isGet = false; //判断是后台收款还是前端付款
                if (model != null) {
                    if (getStr(model.get("PayMethodName")) != "") {
                        isGet = true;
                    }
                }

                if (isGet) {
                    //判断是否支付
                    if (getStr(d.Field1) == "1") {
                        pay_str += "已收款";
                        pay_str += "-" + getStr(model.get("PayMethodName"));
                    } else {
                        pay_str += "未付款";
                    }
                } else {
                    //判断是否支付
                    if (getStr(d.Field1) == "1") {
                        pay_str += "已付款";
                    } else {
                        pay_str += "未付款";
                    }
                }

                Ext.getCmp("lab_Pay").setValue(pay_str);//给jitdisplayfield添加值

              //  Ext.getCmp("lab_Pay").jitSetValue("fadfasa");和setValue一样



                //Ext.getCmp("txtDeliveryName").setValue(getStr(d.DeliveryName));


                var delivery_str = "";
                var model2 = Ext.getStore("inoutStatusStore").getById(600);
                if (model2 != null) {
                    if (getStr(model2.get("PicUrl")) != "") {
                        delivery_str += '<a rel="fancybox_group1" href="' + getStr(model2.get("PicUrl")) + '" title="拍摄时间：' + getStr(model2.get("LastUpdateTimeFormat")) + '"><img src="/Lib/Image/image.png" /></a>';
                    }
                }
                delivery_str += getStr(d.DeliveryName);
                Ext.getCmp("lab_Delivery").setValue(delivery_str);


                Ext.getCmp("txtDataFromName").setValue(getStr(d.data_from_name));
                Ext.getCmp("txtActualAmount").setValue(getStr(d.actual_amount));//实际支付金额
             
                Ext.getCmp("txtField9").setValue(getStr(d.Field9));
                Ext.getCmp("txtSendTime").setValue(getStr(d.Field9));
                Ext.getCmp("txtField14").setValue(getStr(d.Field14));
                Ext.getCmp("txtField6").setValue(getStr(d.Field6));
                Ext.getCmp("txtField5").setValue(getStr(d.Field5));
                Ext.getCmp("txtField4").setValue(getStr(d.Field4));
                Ext.getCmp("txtCarrierName").setValue(getStr(d.carrier_name));
                Ext.getCmp("txtField2").setValue(getStr(d.Field2));//配送单号
     
                //Ext.getCmp("txtRemark").setValue(getStr(d.Remark));

              
             
                Ext.getCmp("cmbDefrayType").jitSetValue(Number(d.Field11));  
                Ext.getCmp("cmbDeliveyType").jitSetValue(Number(d.Field8));   //配送方式
                Ext.getCmp("txtCarrierName").jitSetValue(d.carrier_id);
                if (d.Field7 == "800") {
                    Ext.getCmp("txtCancelTime").jitSetValue(getStr(d.modify_time));
                }
                else {
                    Ext.getCmp("txtCancelTime").hide();
                }

                setImg();
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


function fnLoadItems() {
    var storeId = "salesOutOrderEditItemStore";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=GetInoutDetailInfoById";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id"))
    };
    Ext.getStore(storeId).load(
    {
        callback: function () {
            fnCalTotal();//计算总额
        }
    });
}

function fnAddItem(id, op, param) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "SalesOutOrderItem",
        title: "商品",
        url: "SalesOutOrderItem.aspx?op=" + op + "&id=" + id + getStr(param)
    });
    win.show();
}
function fnDeleteItem() {
    var grid = Ext.getCmp("gridView");
    var ids = JITPage.getSelected({ gridView: grid, id: "order_detail_id" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择商品");
        return;
    };
    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("order_detail_id", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
    fnCalTotal();//重新计算总额
}
function fnClose() {
    CloseWin('PosOrderEdit');
}
//计算订单金额、购买数量、消费总额
function fnCalTotal() {
    var pnl = Ext.getCmp("payPanel");
    var grid = Ext.getCmp("gridView");

    var tbTotalAmountCtrl = pnl.query('jittextfield[name="total_amount"]')[0];  //取订单金额
    var tbTotalNumCtrl = pnl.query('jittextfield[name="total_num"]')[0];   //购买数量

    var totalAmount = 0, totalNum = 0;
    if (grid.store.data.map != null) {   //取当前商品明细grid中的数据，还用了map
        for (var tmpItem in grid.store.data.map) {
            var objData = grid.store.data.map[tmpItem].data;
            var objItem = {};
            objItem.enter_price = getFloat(objData.enter_price);  //还要转换一下
            objItem.enter_qty = getFloat(objData.enter_qty);
            objItem.order_qty = getFloat(objData.order_qty);
            objItem.enter_amount = objItem.enter_price; //objItem.order_qty * objItem.enter_price;
            totalAmount += objItem.enter_amount;
            totalNum += objItem.order_qty;
        }
    }
   // tbTotalAmountCtrl.setValue(totalAmount);//支付信息中的订单金额
    tbTotalNumCtrl.setValue(totalNum);
    Ext.getCmp("txtTotalSum").jitSetValue(totalAmount)
}

//获取VIP的信息
function fnGetVipInfo() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetVipSummerInfo",
        params: { order_id: new String(JITMethod.getUrlParam("order_id")) },
        method: 'get',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            //会员号
            Ext.getCmp("txtVipCode").jitSetValue(d.VipCode);
            //姓名
            //微信
            Ext.getCmp("txtVipweixin").jitSetValue(d.WeiXin);
            //微博
            Ext.getCmp("txtVipwb").jitSetValue(d.SinaMBlog);
            //积分
            Ext.getCmp("txtVipintegration").jitSetValue(d.Integration);
            //总额
            //Ext.getCmp("txtTotalSum").jitSetValue(d.TotalSum);
            //标签
            Ext.getCmp("labTags").setValue(d.TotalSum);

            //myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnStatusSetting(statsu) {
    switch (statsu) {
        case "500": //未发货
            Ext.getCmp("txtCarrierName").readOnly = false;
            Ext.getCmp("txtField9").readOnly = false;
            Ext.getCmp("txtField2").setReadOnly(false);
            break;
    }
}

function fnSaveDeliveryInfo() {
    var receiveMan = Ext.getCmp("txtField14").getValue() || "";
    var addr = Ext.getCmp("txtField4").getValue() || "";
    var phone = Ext.getCmp("txtField6").getValue() || "";
    var deliveryType = Ext.getCmp("cmbDeliveyType").getValue() || "";
    var postCode = Ext.getCmp("txtField5").getValue() || "";
    var carrier_id = Ext.getCmp("txtCarrierName").getValue() || "";
    var deliveryCode = Ext.getCmp("txtField2").getValue() || "";

    var parameters = { order_id: new String(JITMethod.getUrlParam("order_id"))
            , ReceiveMan: receiveMan
            , Addr: addr
            , Phone: phone
            , PostCode: postCode
            , DeliveryType: deliveryType
            , Carrier_id: carrier_id
            , DeliveryCode: deliveryCode
    };
    if (deliveryType == 1) {
        parameters.Field9 = Ext.getCmp("txtField9").jitGetValue() || "";
    } else {
        parameters.SendTime = Ext.getCmp("txtSendTime").jitGetValue() || "";
    }

    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });


    //承运商
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=SaveDeliveryInfo",
        params: parameters,
        method: 'post',
        success: function (response) {
            myMask.hide();
            var d = Ext.decode(response.responseText);
            if (d.success) {

                //                 Ext.Msg.alert("提示", "修改成功");

                //                 fnGetInoutInfoById();
                fnRefresh();
            } else {
                Ext.Msg.alert("提示", "修改失败");
            }
        },
        failure: function () {
            myMask.hide();
            Ext.Msg.alert("提示", "修改失败");
        }
    });
}

function fnSaveDefrayType() {

    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=SaveDefrayType",
        params: { order_id: new String(JITMethod.getUrlParam("order_id"))
            , DefrayType: (Ext.getCmp("cmbDefrayType").getValue() || "")
        },
        method: 'post',
        success: function (response) {
            myMask.hide();
            var d = Ext.decode(response.responseText);
            if (d.success) {
                Ext.Msg.alert("提示", "修改成功");

                fnGetInoutInfoById();
                fnRefresh();
            } else {
                Ext.Msg.alert("提示", "修改失败");
            }
        },
        failure: function () {
            myMask.hide();
            Ext.Msg.alert("提示", "修改失败");
        }
    });
}

function fnDeliveryChanged(sender, newValue, oldValue, eOpts) {
    if (newValue == 1) {
        Ext.getCmp("txtField14").show();
        Ext.getCmp("txtField4").show();
        Ext.getCmp("txtField6").show();
        Ext.getCmp("txtField9").show();
        Ext.getCmp("txtField2").show();
        //        Ext.getCmp("txtField5").show();
        Ext.getCmp("txtCarrierName").show();
        Ext.getCmp("txtSendTime").hide();
        Ext.getCmp("txtPurchaseUnitName").hide();
    } else if (newValue != null && newValue != "") {
        Ext.getCmp("txtField14").hide();
        Ext.getCmp("txtField4").hide();
        Ext.getCmp("txtField6").hide();
        Ext.getCmp("txtField9").hide();
        Ext.getCmp("txtField2").hide();
        //        Ext.getCmp("txtField5").hide();
        Ext.getCmp("txtCarrierName").hide();
        Ext.getCmp("txtSendTime").show();
        Ext.getCmp("txtPurchaseUnitName").show();
    }
}

function fnRefresh() {
    window.location.reload();
}

// jifeng.cao begin
function fnOperationSubmit() {
    var form = Ext.getCmp('operationPanel').getForm();
    if (!form.isValid()) {
        return false;
    }

    //    var fileName = Ext.getCmp("PicUrl").getValue();
    //    var filesuffix = fileName.substring(fileName.lastIndexOf("."));
    //    if (filesuffix.toLowerCase() == ".jpg") { }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&method=UpdateStatus",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            order_id: new String(JITMethod.getUrlParam("order_id")),
            nextStatus: nextStatus,
            PicUrl: Ext.getCmp("PicUrl").getValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {

                        parent.window.fnGridReload();
                        CloseWin('PosOrderEdit');
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

//对应订单状态的展示方式
function fnOptChange(status) {

    Ext.getCmp("editPanel").expand();
    Ext.getCmp("payPanel").collapse();
    Ext.getCmp("deliveryPanel").collapse();
    Ext.getCmp("vipPanel").collapse();

    if (status == "900" || status == "800") {
        //审核未通过或订单已取消
        Ext.getCmp("btn_2").hide();
    }
    else if (status == "500" || status == "600") {
        //未发货或已发货
        Ext.getCmp("editPanel").collapse();
        Ext.getCmp("payPanel").collapse();
        Ext.getCmp("deliveryPanel").expand();
        Ext.getCmp("vipPanel").collapse();
    }

}

//审核
function fnbtn(status) {

    //下级状态值
    nextStatus = status;

    Ext.getCmp("DeliverCompany").hide(); //配送商
    Ext.getCmp("DeliverOrder").hide(); //配送单号
    Ext.getCmp("PayMethod").hide(); //收款方式
    Ext.getCmp("CheckResult").hide(); //审核不通过理由
    Ext.getCmp("PicUrl").hide(); //图片凭证

    //根据下级状态显示对应功能
    switch (status) {
        case 900: //审核不通过
            Ext.getCmp("CheckResult").show();
            break;
        case 600: //发货
            Ext.getCmp("DeliverCompany").show();
            Ext.getCmp("DeliverOrder").show();
            Ext.getCmp("PicUrl").show();
            break;
        case 700: //完成
            Ext.getCmp("PicUrl").show();
            break;
        case 10000: //收款
            Ext.getCmp("PayMethod").show();
            Ext.getCmp("PayMethod").allowBlank = false;
            Ext.getCmp("PicUrl").show();
            Ext.getCmp("PicUrl").fieldLabel = "上传凭证";
            break;
    }

    Ext.getCmp("operationPanel").getForm().reset();
    Ext.getCmp("operationWin").show();
}
function fnDownLoadDelivery(orderID) {
    var getUrl = '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=ExportDelivery';
    var form = $('<form>');
    form.attr('style', 'display:none;');
    form.attr('target', '');
    form.attr('method', 'post');
    form.attr('action', getUrl);
    var input1 = $('<input>');
    input1.attr('type', 'hidden');
    input1.attr('name', 'orderId');
    input1.attr('value', new String(JITMethod.getUrlParam("order_id")));
    $('body').append(form);
    form.append(input1);
    form.submit();
    form.remove();
}
function fnPrintPicking(orderID) {

    var url = "/Module/Order/Print/PrintPicking.aspx?orderID=" + new String(JITMethod.getUrlParam("order_id"));
    window.open(url, "拣货打印");
}
function fnPrintDelivery(orderID) {
    var url = '/Module/Order/Print/PrintDelivery.aspx?orderId=' + new String(JITMethod.getUrlParam("order_id"));
    window.open(url, '配送单打印');
}
function fnOperationCancel() {
    Ext.getCmp("operationWin").hide();
}

function fnSearch() {
    Ext.getCmp("pageBar1").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=GetInoutStatusList";
    Ext.getCmp("pageBar1").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar1").store.proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id"))
    };
    Ext.getCmp("pageBar1").moveFirst();

}

function fnShowLog() {

    Ext.getCmp("logWin").show();
    fnSearch();
}


function fnColumnStatusRemark(value, p, r, rowIndex) {
    var res = "";
    if (r.data.PicUrl != null && r.data.PicUrl != "") {
        res += '<a rel="fancybox_group' + (rowIndex + 2) + '" href="' + r.data.PicUrl + '" title="拍摄时间：' + r.data.LastUpdateTimeFormat + '"><img src="/Lib/Image/image.png" /></a>&nbsp;';
    }

    res += value;

    var con = "";
    if (r.data.CheckResultName != null && r.data.CheckResultName != "") {
        con += "未通过理由：" + r.data.CheckResultName + "，";
    }
    if (r.data.PayMethodName != null && r.data.PayMethodName != "") {
        con += "支付方式：" + r.data.PayMethodName + "，";
    }
    if (r.data.unit_name != null && r.data.unit_name != "") {
        con += "配送公司：" + r.data.unit_name + "，";
    }
    if (r.data.DeliverOrder != null && r.data.DeliverOrder != "") {
        con += "配送单号：" + r.data.DeliverOrder + "，";
    }
    if (r.data.Remark != null && r.data.Remark != "") {
        con += "备注：" + r.data.Remark + "，";
    }
    if (con.length > 0) {
        con = con.substring(0, con.length - 1);
        res += "[" + con + "]";
    }

    return res;
}

//图片加载fancybox
function setImg() {
    for (var i = 0; i < JITPage.PageSize.getValue(); i++) {
        if ($('a[rel=fancybox_group' + i + ']').length == 0) {
            continue;
        }
        $('a[rel=fancybox_group' + i + ']').fancybox({
            openEffect: 'none',
            closeEffect: 'none',
            prevEffect: 'none',
            nextEffect: 'none',
            closeBtn: true,
            titleFormat: function (title, currentArray, currentIndex, currentOpts) {
                if (title != null && title != "" && title.length > 0) {
                    return title;
                }
                return "暂无";
            }
        });
    }
};


// jifeng.cao end