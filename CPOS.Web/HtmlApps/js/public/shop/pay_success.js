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
            //是否是余额支付成功
        var flag=me.getUrlParam("useBalance"); 
        var sucStr='<div class="paybar">'+
    	'<img src="../../../images/public/shop_default/icon-success.png">'+
        '<p>支付成功!</p></div>';
        var errStr='<div class="paybar">'+
    	'<img src="../../../images/public/shop_default/icon-error.png">'+
        '<p>支付失败!</p></div>';  
    debugger; 
        if(flag){
        	payResult.html(sucStr);
        }else{
	        me.ajax({
	            url: '/OnlineShopping/data/Data.aspx',
	            beforeSend: function() {
	                payResult.html('数据正在加载,请稍后...');
	
	            },
	            data: {
	                'action': 'isOrderPaid',
	                'orderId': me.getParams('orderId_'+me.getBaseInfo().userId)
	            },
	            success: function(data) {
	                if (data.code == 200 && data.content.Status == 1) {
					
	                    payResult.html(sucStr);
						
	                } else {
					
	                    payResult.html(errStr);
	                }
	            }
	        });
        }    

    }

});