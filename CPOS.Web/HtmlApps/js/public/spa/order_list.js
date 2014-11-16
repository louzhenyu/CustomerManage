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
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                action: "getOrderList",
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
            orderHtml += '<div class="mod mod_order order_list">';
            orderHtml += '<dl>';
            orderHtml += '<dt>';
            //orderHtml += '<a href="#">取消</a>';
            //orderHtml += '<a href="#">支付</a>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                orderHtml += "<h3 onclick=\"Jit.AM.toPage('OrderDetail','&orderId=" + itemlists[i].orderId + "')\">" + itemlists[i].orderDetailList[0].itemName + "</h3>";
                orderHtml += "<p>订单编号：" + itemlists[i].orderCode + "</p>";
                orderHtml += "<p>项目：" + itemlists[i].orderDetailList[0].itemCategoryName + "</p>";
            }
            orderHtml += '</dt>';
            orderHtml += '<dd>';
            orderHtml += '<span>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                orderHtml += '日期：' + itemlists[i].orderDetailList[0].beginDate + '<br>';
                orderHtml += '时间：' + (itemlists[i].orderDetailList[0].appointmentTime?itemlists[i].orderDetailList[0].appointmentTime:"未写预约时间") + '';
            }
            orderHtml += '</span>';
            orderHtml += '<span>';
            if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                orderHtml += '' + itemlists[i].orderDetailList[0].qty + itemlists[i].orderDetailList[0].GG + '';
            }
            orderHtml += '</span>';
            orderHtml += '<span>';
            orderHtml += '订单费：<b>' + itemlists[i].totalAmount + '元</b>';
            orderHtml += '</span>';
            orderHtml += '</dd>';
            orderHtml += '</dl>';
            orderHtml += '</div>';
        }
        if(orderHtml==""){
        	$('[jitval=order_list]').html("您还没有预约项目");
        }else{
        	$('[jitval=order_list]').html(orderHtml);
        }
      
    }
});