Jit.AM.defindPage({
    /*定义页面名称 必须和config文件中设置的属性一直*/
    name: 'OrderDetail',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入OrderDetail.....');
        this.initEvent();
    }, initEvent: function () {
        var me = this;
        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderList',
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
                }
            }
        });
    },
    initPageData: function (data) {
        var me = this;
        $('[jitval=itemName]').html(data.orderDetailList[0].itemName);
        $('[jitval=orderDate]').html("房间预订时间：" + data.orderDate+" "+data.orderDetailList[0].appointmentTime);
        $('[jitval=gg]').html(data.orderDetailList[0].itemCategoryName + " " + data.orderDetailList[0].qty + data.orderDetailList[0].GG);
        //$('[jitval=beginDate]').html("入住：" + data.orderDetailList[0].beginDate);
        //$('[jitval=endDate]').html("离店：" + data.orderDetailList[0].endDate);
        $('[jitval=qty]').html("&nbsp;&nbsp;" + data.orderDetailList[0].qty + data.orderDetailList[0].GG);
        $('[jitval=actualAmount]').html(data.actualAmount + "元");
        $('[jitval=addr]').html("地址：" + data.address);
        $('[jitval=linkTel]').html("联系手机：" + data.linkTel);
        $('[jitval=linkMan]').html("预订人：" + data.linkMan);
        $('[jitval=mobile]').html("联系电话：" + data.mobile == null ? "" : data.mobile);
    }
})