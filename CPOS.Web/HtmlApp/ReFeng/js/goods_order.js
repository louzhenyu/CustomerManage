Jit.AM.defindPage({

    name: 'GoodsDetail',

    onPageLoad: function() {

        var me = this;
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderList',
                'orderId': me.getUrlParam('orderId'),
                'page': 1,
                'pageSize': 99
            },
            success: function(data) {
                //console.log(data);
                if (data.code == 200) {

                    var order = data.content.orderList[0],
                        tpl = $('#Tpl_goods_info').html(),
                        html = '',
                        totalprice = 0;

                    me.initPageData(order);

                    for (var i = 0; i < order.orderDetailList.length; i++) {

                        //totalprice += order.orderDetailList[i].salesPrice;

                        html += Mustache.render(tpl, order.orderDetailList[i]);
                    }

                    $('#goods_list').append(html);

                } else {

                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });



        //获取订单优惠券列表
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'orderCouponList',
                'orderId': me.getUrlParam('orderId')
            },
            success: function(data) {

                if (data.code == 200) {


                }
            }
        });



        //获取优惠券是否可用
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'selectCoupon',
                'orderId': me.getUrlParam('orderId'),
                'couponId': '???'
            },
            success: function(data) {

                if (data.code == 200) {


                }
            }
        });


        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getDeliveryList'
            },
            success: function(data) {

                if (data.code == 200) {

                    var adslist = data.content.deliveryList,
                        dom;

                    for (var i = 0; i < adslist.length; i++) {
                        dom = $('[psfs=fs' + adslist[i].deliveryId + ']');
                        dom.show();
                        dom.find('p').html('');
                    }
                }
            }
        });

        me.initEvent();
    },
    initPageData: function(data) {
        var me = this;
        $('#totalprice').html("￥" + data.totalAmount);

        $('#totalqty').html(data.totalQty);

        $('#payprice').html("￥" + data.totalAmount);

        if (data.address||data.linkMan||data.linkTel) {

            $('#addressdetail').html(data.linkMan + '，' + data.linkTel + '，' + data.address);

            $('[value=radioA]').attr('checked', 'checked');

            me.hasAddress = true;
        }
        else {

            //获取用户设置的默认地址

            var _data = {
                'action': 'getVipAddressList',
                'page': 1,
                'pageSize': 99
            }

            if (me.getUrlParam('orderId')) {

                _data.orderId = me.getUrlParam('orderId');
            }

            me.ajax({
                url: '../../../OnlineShopping/data/Data.aspx',

                data: _data,

                success: function(data) {

                    if (data.code == 200 && data.content.itemList) {

                        var list = data.content.itemList,
                            defaultAddress;
                        for (var i = 0; i < list.length; i++) {
                            if (list[i].isDefault == "1") {
                                defaultAddress = list[i];
                                break;
                            };

                        }

                        if (defaultAddress) {

                            $('#addressdetail').html(defaultAddress.linkMan + '，' + defaultAddress.linkTel + '，' + defaultAddress.address);

                            $('[value=radioA]').attr('checked', 'checked');

                            me.hasAddress = true;

                            //为订单设置默认地址
                            me.ajax({
                                url: '../../../OnlineShopping/data/Data.aspx',
                                data: {
                                    'action': 'setOrderAddress',
                                    'orderId': me.getUrlParam('orderId'),
                                    'linkMan': defaultAddress.linkMan,
                                    'linkTel': defaultAddress.linkTel,
                                    'address': defaultAddress.address
                                },
                                success: function(data) {

                                    if (data.code == 200) {

                                        // body
                                    }
                                }
                            });
                        };
                    }
                }
            });

            JitPage.calculateAmount();

        }
    },
    initEvent: function() {

        var me = this,
            $goodsWrap = $('.goods_wrap');

        $('[name=select_address]').bind('click', function(evt) {

            var psfs = $(evt.currentTarget).attr('psfs');

            if (psfs == 'fs1') {

                me.toPage('SelectAddress', '&orderId=' + me.getUrlParam('orderId'));

            } else if (psfs == 'fs2') {

                me.toPage('SelectStore', '&orderId=' + me.getUrlParam('orderId'));
            }
        });
        $goodsWrap.delegate('#select-juan', 'click', function() {
            $('.ui-mask').show();
            $('.delivery_mode_list').show();
            //获取订单优惠券列表
            me.ajax({
                url: '../../../OnlineShopping/data/Data.aspx',
                data: {
                    'action': 'orderCouponList',
                    'orderId': me.getUrlParam('orderId')
                },
                success: function(data) {
                    //console.log(data);
                    if (data.code == 200) {
                        var data = data.content.couponList,
                            checkStyle = '',
                            htmlStr = '';
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].isChecked == '1') {
                                var checkStyle = 'ui-input ui-checked';
                            } else {
                                var checkStyle = 'ui-input';
                            }
                            htmlStr += '<li data-info="' + data[i].couponId + '"><span class="flex"><span type="checkbox" class="' + checkStyle + '"></span><span class="text-wrap clearfix"><b>' + data[i].couponType + '</b><span class="text">每满' + data[i].conditionValue + '元可以使用一张</span></span></span><span class="flex color">￥' + data[i].parValue + '</span><span class="flex exp">' + data[i].couponSource + '</span></li>';
                        }
                        $('.goods-order-list', $goodsWrap).html(htmlStr);

                    }
                }
            });
        });

        $goodsWrap.delegate('.delivery_mode_list li', 'click', function() {
            var $this = $(this),
                action = '',
                couponId = $this.data('info');
            if ($('.ui-input', $this).attr('class') == 'ui-input ui-checked') {
                action = 'cancelCoupon';
            } else if ($('.ui-input', $this).attr('class') == 'ui-input') {
                action = 'selectCoupon';
            }
            //获取优惠券是否可用
            me.ajax({
                url: '../../../OnlineShopping/data/Data.aspx',
                data: {
                    'action': action,
                    'orderId': me.getUrlParam('orderId'),
                    'couponId': couponId
                },
                success: function(data) {
                    //console.log(data);
                    if (data.code == 200) {
                        if (action == 'cancelCoupon') {
                            $('.ui-input', $this).attr('class', 'ui-input');
                        } else {
                            var data = data.content.result;
                            if (data == '0') {
                                alert('该抵用券不能使用!');
                            } else if (data == '1') {
                                $('.ui-input', $this).addClass('ui-checked');
                            }
                        }

                        var totalprice = $("#totalprice").text();
                        //计算总的数字
                        JitPage.calculateAmount();

                    }
                }
            });

        });

        $goodsWrap.delegate('.btn-yes', 'click', function() {
            $('.ui-mask').hide();
            $('.delivery_mode_list').hide();
        });
    },

    submitOrder: function() {

        var me = this;

        if (!me.hasAddress) {

            Jit.UI.Dialog({
                'content': '请填写详细配送地址！',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;
        }

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderPayment',
                'orderId': me.getUrlParam('orderId'),
                'paymentTypeId': ''
            },
            success: function(data) {

                if (data.code == 200) {
                    //调支付
                }
            }
        });
        var payType = $("input[class='ckpay'][checked=checked]").val();
        if (parseInt(payType) == 0) {
            Jit.UI.Dialog({
                'content': '订单已完成!',
                'type': 'Confirm',
                'LabelOk': '去逛逛',
                'LabelCancel': '我的订单',
                'CallBackOk': function() {
                    me.toPage('GoodsList');
                },
                'CallBackCancel': function() {
                    me.toPage('MyOrder');
                }
            });
            return;
        } else if (parseInt(payType) == 2) {
            if ($("#phonenum").val() == "") {
                Jit.UI.Dialog({
                    'content': '请填写电话！',
                    'type': 'Alert',
                    'CallBackOk': function() {

                        Jit.UI.Dialog('CLOSE');
                    }
                });

                return;
            } else {
                Jit.UI.Dialog({
                    'content': '',
                    'type': 'Confirm',
                    'LabelOk': '取消',
                    'LabelCancel': '支付完成',
                    'CallBackOk': function() {
                        Jit.UI.Dialog("CLOSE");
                    },
                    'CallBackCancel': function() {
                        me.toPage('PaySuccess', '&orderId=' + JitPage.getUrlParam('orderId'));
                    }
                });
            }

        }
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                action: "orderPay",
                payChannelID: payType,
                orderID: me.getUrlParam('orderId'),
                mobileNO: parseInt(payType) == 2 ? $("#phonenum").val() : ""
            },
            success: function(data) {
                //alert(data.content.PayUrl);
                if (data.code == "200" && parseInt(payType) != 2) {
                    window.location = data.content.PayUrl;
                }
            }
        });
    },
    checkPayType: function(obj) {
        $(".ckpay").each(function() {
            this.checked = false;
            $(this).removeAttr("checked", "");
        });
        obj.checked = true;
        $(obj).attr("checked", "checked");
    },
    calculateAmount: function() {
        var totalprice = $("#totalprice").text().replace('￥', '');

        //计算总的数字
        JitPage.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'orderCouponSum',
                'orderId': JitPage.getUrlParam('orderId'),
                'loadFlag': 1
            },
            success: function(data) {
                //console.log(data);
                if (data.code == 200) {
                    var data = data.content;
                    $("#payprice").text('￥' + parseInt(totalprice - data.amount));
                    $('#juan-count').text(data.count);
                    $('#juan-amount').text(data.amount);
                }
            },
            failure: function() {
                alert("计算错误");
            }
        });
    }
});