Jit.AM.defindPage({

    name: 'PaySuccess',

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('进入paysuccess.....');

        this.LoadPayInfo();
    },

    initEvent: function() {

        // var me = this;

        // me.windowHeight = window.innerHeight;

        // me.windowWidth = window.innerWidth;

    }, //加载支付信息
    LoadPayInfo: function() {
        var me = this,
            payResult = $('#payResult');
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            beforeSend: function() {
              UIBase.loading.show();

            },
            data: {
                'action': 'isOrderPaid',
                'orderId': me.getParams('orderId_'+me.getBaseInfo().userId)
            },
            success: function(data) {
			UIBase.loading.hide();
                if (data.code == 200 && data.content.Status == 1) {
				
                    payResult.html('支付成功！');
					
                } else {
				
                    payResult.html('支付失败');
                }
            }
        });

    }

});