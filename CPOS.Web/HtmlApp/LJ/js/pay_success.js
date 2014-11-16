Jit.AM.defindPage({

    name: 'PaySuccess',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入paysuccess.....');

        this.initEvent();
    },

    initEvent: function () {

        var me = this;

        me.windowHeight = window.innerHeight;

        me.windowWidth = window.innerWidth;

        me.paySuccess();

    },
    paySuccess: function () {
        JitPage.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                action: "setOrderPayment",
                orderId: JitPage.getUrlParam('orderId'),
                paymentTypeId: "1"
            },
            success: function (data) {
                //Jit.log(data);
                $("#payResult").html(data.description);
            }
        });
    }

});