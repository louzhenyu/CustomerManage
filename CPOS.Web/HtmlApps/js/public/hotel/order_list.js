/*定义页面*/
Jit.AM.defindPage({
    name: 'OrderList',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入OrderList.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?ReqIsHotel=1',
            data: {
                action: "getOrderList4Blossom",
                page: "1",
                pageSize: "15"
            },
            success: function (data) {
                if (data.code == 200) {
                    me.loadPageData(data.content);
                }
            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.orderList;

        //循环订单列表
        var orderHtml = "";
        for (var i = 0; i < itemlists.length; i++) {
            var itemhtml = "";
            orderHtml += '<div class="orderListBox">';
            orderHtml += "<dl onclick=\"javascript:Jit.AM.toPage('H_OrderDetail','&orderId=" + itemlists[i].orderId + "');\">";
            orderHtml += '<dt>';
            //orderHtml += '<a href="#">取消</a>';
            //orderHtml += '<a href="#">支付</a>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
				orderHtml += "<p class='serialNumber'>订单编号：" + itemlists[i].orderCode + "</p>";
				orderHtml += "<p>酒店：" + itemlists[i].storeName + "</p>";
                orderHtml += "<p>房型：" + itemlists[i].orderDetailList[0].itemName + "</p>";
            }
            orderHtml += '</dt>';
            orderHtml += '<dd>';
            orderHtml += '<span>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                orderHtml += '入住：' + itemlists[i].orderDetailList[0].beginDate + '<br>';
                orderHtml += '离店：' + itemlists[i].orderDetailList[0].endDate + '';
            }
            orderHtml += '</span>';
            orderHtml += '<p>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                orderHtml += '数量：' + itemlists[i].orderDetailList[0].qty + itemlists[i].orderDetailList[0].GG + '';
            }
            orderHtml += '</p>';
            orderHtml += '<p class="price">订单房费：' + itemlists[i].totalAmount + '元</p>';
			orderHtml += "<p>状态：<span class='status'>" + itemlists[i].statusDesc + "<span></p>";
            orderHtml += '</dd>';
            orderHtml += '</dl>';
            orderHtml += '</div>';
        }
        $('[jitval=order_list]').html(orderHtml);
    }
});