Jit.AM.defindPage({
    name: 'OrderSuccess',
    onPageLoad: function () {
        Jit.log('进入OrderSuccess.....');
        this.initData();
    },
    initData: function () {
        var inDate=this.getParams("InDate");  //预约日期
        var appointmentTime=this.getParams("appointmentTime");//预定时间
        var storeName=this.getParams("storeName"); //门店名称
        $("#inDate").html(inDate);
        $("#time").html(appointmentTime?appointmentTime:"未写预约时间");
        $("#storeName").html(storeName);
    },
    //取消订单
    cancelOrder:function(){
    	var orderId=this.getUrlParam("orderId");  //订单id
        this.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderStatus',
                'orderId': orderId,  //店ID
                'status':0
            },
            success: function (data) {
            	var message="";
                if(data.code=="200"){
                	message="取消订单成功!";
                }else{
                	message="取消订单失败!";
                }
                Jit.UI.Dialog({
                    'content': message,
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            }
        });
    }
});