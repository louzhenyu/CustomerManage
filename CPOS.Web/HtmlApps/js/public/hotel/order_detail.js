
Jit.AM.defindPage({
    /*定义页面名称 必须和config文件中设置的属性一直*/
    name: 'OrderDetail',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入OrderDetail.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?ReqIsHotel=1',
            data: {
                'action': 'getOrderList4Blossom',
                'orderId': me.getUrlParam('orderId'),
                'page': 1,
                'pageSize': 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    if (data.content.orderList.length > 0) {
                        var order = data.content.orderList[0];

                        me.initPageData(order);
                    }
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                };
            }
        });
    },
    initPageData: function (data) {
        var me = this;
        $('[jitval=orderCode]').html("订单编号：" + data.orderCode);
        $("#orderCode").val(data.orderCode);
        $('[jitval=itemName]').html("酒店：" + data.storeName);
        $("#storeName").val(data.storeName);

        $('[jitval=orderDate]').html("预订时间：" + data.orderDate);
        $('[jitval=gg]').html("房型：" + data.orderDetailList[0].itemName); //+ " " + data.orderDetailList[0].qty + data.orderDetailList[0].GG
        $("#itemName").val(data.orderDetailList[0].itemName);
        $('[jitval=beginDate]').html("入住：" + data.orderDetailList[0].beginDate);
        $('[jitval=endDate]').html("离店：" + data.orderDetailList[0].endDate);
        $('[jitval=qty]').html("数量：" + data.orderDetailList[0].qty + data.orderDetailList[0].GG);
        $('[jitval=actualAmount]').html(data.actualAmount + "元");
        $('[jitval=addr]').html("地址：" + data.address);
        $('[jitval=linkTel]').html("联系手机：" + data.linkTel);
        $("#linkTel").val(data.linkTel);
        $('[jitval=linkMan]').html("入住人姓名：" + data.linkMan);
        $('[jitval=useScore]').html("使用积分：" + data.integral);
        $('[jitval=useCoupon]').html("使用优惠券：" + data.couponAmount);
        $('[jitval=useBalance]').html("使用余额：" + data.vipEndAmount);
        $("#hdLinkMan").val(data.linkMan);
        $('[jitval=mobile]').html("联系电话：" + data.mobile == null ? "" : data.mobile);
        $('[jitval=optiontext]').html("状态：<span class='optiontext'>" + data.statusDesc + "</span>");
        $('[jitval=remark]').html("备注：" + (data.remark || ''));
        //订单已取消  则让取消按钮不可用
        if (data.status == 800) {
            // $(".canceOrderBtn").css("background", "gray").removeAttr("onclick");
             $(".canceOrderBtn").css("display", "none");

        }
        if (data.optionvalue == 300 || data.optionvalue == 400 || data.optionvalue == 500) {
            $('[jitval=optiontext]').html("订单状态：" + data.optiontext + "");
            $(".total2").css("display", "none");
            $("#status").html("");
        } else {
        }

    },
    fnCancelOrders: function () {
        var me = this;
        Jit.UI.Dialog({
            'content': '您确定要取消此订单吗?',
            'type': 'Confirm',
            'LabelOk': '确定',
            'LabelCancel': '取消',
            'CallBackOk': function () {
                JitPage.ajax({
                    type: "get",
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "SetCancelOrder",
                        OrderID: me.getUrlParam('orderId'),
                        ActionCode: 800
                    },
                    success: function (data) {


                        Jit.UI.Masklayer.hide();
                        if (data.IsSuccess) {
                            me.ajax({
                                url: '/OnlineShopping/data/Data.aspx',
                                data: {
                                    'action': 'sendMail',
                                    'type': 'cancel',
                                    'UserName': $("#hdLinkMan").val(),
                                    'Mobile': $("#linkTel").val(),
                                    'StoreName': $("#storeName").val(),
                                    'RoomName': $("#itemName").val(),
                                    'OrderNo': $("#orderCode").val(),
                                    'OrderDate': me.getParams('InDate') + ' 至 ' + me.getParams('OutDate')

                                }
                            });
                            Jit.UI.Dialog({
                                'content': "订单取消成功!",
                                'type': 'Alert',
                                'CallBackOk': function () {
                                    me.toPage('H_OrderList');
                                }
                            });
                        } else {
                            Jit.UI.Dialog({
                                'content': data.Message,
                                'type': 'Alert',
                                'CallBackOk': function () {
                                    me.toPage('H_OrderList');
                                }
                            });
                        }
                    }
                });
                //数据请求
                /*
                me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: {
                'action': 'cancelOrders',
                'ordersId': me.getUrlParam('orderId')
                },
                beforeSend: function () {
                Jit.UI.Masklayer.show();
                },
                success: function (data) {
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {

                me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: {
                'action': 'sendMail',
                'type': 'cancel',
                'UserName': $("#hdLinkMan").val(),
                'Mobile': $("#linkTel").val(),
                'StoreName': $("#storeName").val(),
                'RoomName': $("#itemName").val(),
                'OrderNo': $("#orderCode").val(),
                'OrderDate': me.getParams('InDate') + ' 至 ' + me.getParams('OutDate')

                }
                });

                Jit.UI.Dialog({
                'content': data.description,
                'type': 'Alert',
                'CallBackOk': function () {
                me.toPage('H_OrderList');
                }
                });
                } else {
                Jit.UI.Dialog({
                'content': data.description,
                'type': 'Alert',
                'CallBackOk': function () {
                me.toPage('H_OrderList');
                }
                });
                }
                }
                });*/
            },
            'CallBackCancel': function () {
                Jit.UI.Dialog('CLOSE');
            }
        });
    }
});