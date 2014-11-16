Jit.AM.defindPage({

    name: 'ALD_DoJoin',

    elements: {
    },
    skuList:[
        '6d6ef5dc12042af1fab0d56da8b7495a',
        '4026cccbc1443d497a29de10f6618538',
        '401189288a91dd9adce07f77d2e80a9a',
        'e2adc13bf92ab539eea5300c00a58bf2',
        'c6abfafe0f7cb883dd724112049dc5e6',
        '8ff6c37e99439958f0c172ac53110091',
        '6b4f8998acde3819f8c0b3d1dee8dacb'
    ],
    onPageLoad: function() {
        
        //当页面加载完成时触发
        
        Jit.log('页面进入' + this.name);
    },
    join:function(idx){

        var me = this;

        this.skuId = me.skuList[idx-1];

        $('.join-popup').show();

        $('.mask').show().on('click',function(){

            $('.mask').hide();

            $('.join-popup').hide();
        });
    },
    //加载数据
    submit:function(){

    	var me = this;
        
        Jit.UI.Loading(true);
        
        var list = [{
            'skuId': me.skuId,
            //'salesPrice': me.scalePrice,
            'qty': 1
        }];

        var remark = [];

        remark.push($('#name').val());
        remark.push($('#company').val());
        remark.push($('#duty').val());
        remark.push($('#phone').val());
        remark.push($('#industry').val());
        remark.push($('#jointype').val());

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'qty': 1,
                //'totalAmount': list[0].qty * list[0].salesPrice,
                'action': 'setOrderInfo',
                'orderDetailList': list,
                'remark':remark.join(',')
            },
            success: function(data) {
                if (data.code == 200) {
                    
                    me.ajax({
                        url: '/OnlineShopping/data/Data.aspx',
                        data: {
                            'action': 'setOrderAddress',
                            'orderId': data.content.orderId,
                            'linkMan': $('#name').val(),
                            'linkTel': $('#phone').val(),
                            'address': '',
                            'deliveryId' : 1
                        },
                        success: function (data){

                            Jit.UI.Loading(false);

                            if (data.code == 200) {

                                Jit.UI.Dialog({
                                    'content': '您已成功提交加盟申请！',
                                    'type': 'Alert',
                                    'CallBackOk': function() {

                                        $('.mask').hide();

                                        $('.join-popup').hide();
                                        
                                        Jit.UI.Dialog('CLOSE');
                                    }
                                });
                            }
                        }
                    });
                    
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
    },

    alert:function(text,callback){

    	Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
    }

});

