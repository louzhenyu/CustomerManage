Jit.AM.defindPage({
    name: 'OrderDetail',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入OrderDetail.....');
        var me = this;
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderList',
                'orderId': me.getUrlParam('orderId'),
                'page': 1,
                'pageSize': 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    var order = data.content.orderList[0], tpl = $('#Tpl_order_info').html(), html = '', totalprice = 0;

                    me.initPageData(order);

                    for (var i = 0; i < order.orderDetailList.length; i++) {
                        html += Mustache.render(tpl, order.orderDetailList[i]);
                    }
                    $('#order_list').append(html);
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });
    },
    initPageData: function (data) {
        var me = this;
        $('#totalprice').html("￥" + data.totalAmount);
        $('#goodsAmount').html("￥" + data.totalAmount);
        $('#ordersAmount').html("￥" + data.totalAmount);
        $('#totalqty').html(data.totalQty);
        $('#ordersCount').html(data.totalQty);
        $('#linkMan').html(data.linkMan);
        $('#linkTel').html(data.linkTel);
        $('#addr').html(data.addr);
        $('#date').html(data.deliveryTime);
    }
});