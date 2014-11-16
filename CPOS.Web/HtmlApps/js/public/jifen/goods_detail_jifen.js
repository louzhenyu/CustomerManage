Jit.AM.defindPage({

	name:'GoodsJiFenDetail',
	
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入GoodsDetail.....');
		
		var me = this;

		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getItemDetail',
				'itemId':me.getUrlParam('goodsId')
			},
			success:function(data){
				
				me.loadGoodsDetail(data.content);
			}
		});
		
		me.ajax({
			url:'/lj/Interface/RegisterData.aspx',
			data:{
				'action':'getIsRegistered'
			},
			success:function(data){
				
				if(data.content.IsRegistered=="2"){
					
					//me.setParams('IsRegister','true');
					me.IsRegister = true;
				}else{
				
					me.IsRegister = false;
				}
			}
		});
		
		
		var data = me.getParams('select_address');
		
	  if (data&&data.address) {
	  	data.deliveryId="1";
            data.deliveryId = parseInt(data.deliveryId);
            if (data.deliveryId == 2) {
                $('#shopAddress').html(data.address);
                var radioB = $('[value=radioB]');
                radioB.attr('checked', 'checked');
                radioB.addClass('on');
                $('#shopTitle').html(data.linkMan);
                $('#shopTel').html(data.linkTel);
                $('#shopAddress').parents('div').show();
            } else if (data.deliveryId == 1) {
                $('#linkMan').html(data.linkMan);
                $('#linkTel').html(data.linkTel);
                $('#linkAddress').html(data.address);
                var radioA = $('[value=radioA]');
                radioA.attr('checked', 'checked');
                radioA.addClass('on');
                $('#linkAddress').parents('div').show();
            }
            me.hasAddress = true;
        } else {

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
                url: '/OnlineShopping/data/Data.aspx',

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
                            $('#linkMan').html(defaultAddress.linkMan);
                            $('#linkTel').html(defaultAddress.linkTel);
                            $('#linkAddress').html(defaultAddress.address);
                            var radioA = $('[value=radioA]');
                            radioA.attr('checked', 'checked');
                            radioA.addClass('on');
                            $('#linkAddress').parents('div').show();
                            me.hasAddress = true;
                            //为订单设置默认地址
                            me.ajax({
                                url: '/OnlineShopping/data/Data.aspx',
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

        }
		
		
		$('#btn_delNum').bind('click',function(){
			
			var num = $('#goods_number').val();
			
			num--;
			
			if(num<=1){
			
				num = 1;
			}
			
			$('#goods_number').val(num);
			
			$('#totaljf').html(parseInt(me.data.integralExchange)*num);
		});
		
		$('#btn_addNum').bind('click',function(){
			
			var num = $('#goods_number').val();
			
			num++;
			
			$('#goods_number').val(num);
			
			$('#totaljf').html(parseInt(me.data.integralExchange)*num);
		});
		
		me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getVipValidIntegral'
            },
            success: function (data) {
                $("#myjf").html(data == "" ? 0 : parseInt(data.data));
            }
        });
	},
	
	initEvent:function(){
		
		var me = this;
		
		$('[name=prop_option]').each(function(i,item){
		
			$(item).bind('click',function(evt){
				
				var skuid = $(evt.target).attr('skuId');
				
				if(skuid){
					
					$(evt.currentTarget).children().removeClass('selected');
					
					var skuid = $(evt.target).addClass('selected').attr('skuid');
					
					var idx = parseInt($(evt.currentTarget).attr('propidx'));
					
					var propdetailid = $(evt.target).attr('prop_detail_id');
					
					JitPage.getSkuData(idx+1,propdetailid,skuid);
				}
			});
		});
		
		$('[name=select_address]').bind('click',function(evt){
			
			var psfs = $(evt.currentTarget).attr('psfs');
			
			if(psfs=='fs1'){
				
				me.toPage('SelectAddress','&orderId='+me.getUrlParam('orderId'));
				
			}else if(psfs=='fs2'){
				
				me.toPage('SelectStore','&orderId='+me.getUrlParam('orderId'));
			}
		});
		
		$('#btn_get').bind('click',function(){
			
			me.submitOrder();
		});
	},
	loadGoodsDetail:function(data){
	
		this.data = data;
		
		this.initPageInfo();
		
		this.initPropHtml();
		
		this.initEvent();
	},
	
	initPageInfo:function(){
		
		var data = this.data;
		
		$('[jitval=itemName]').html(data.itemName);
		
		$('[jitval=itemPrice]').html('商品积分值：'+data.integralExchange);
		
		$('#totaljf').html(data.integralExchange);
		
		 if (data.imageList && data.imageList.length > 0) {
            var imgHtml = '',
                barHtml = '';
            for (var i = 0; i < data.imageList.length; i++) {
                imgHtml += "<li><img height=\"160\" src=\"" + data.imageList[i].imageURL + "\" ></li>";
                barHtml += "<li class=\"" + (i == 0 ? "on" : "") + "\">" + (i + 1) + "</li>";
            };
            $('#goodsImgs').html(imgHtml);
            if (data.imageList.length > 1) {
                $('#goodsImgBar').html(barHtml);
                loaded();
            };
        };



        var myScroll;
        //拖拽事件
        function loaded() {
            var goodsScroll = $('#goodsScroll'),
                menuList = $('#goodsImgBar li');

            //重新設置大小
            ReSize();

            function ReSize() {
                goodsScroll.find('.goods_img_list').css({
                    width: (goodsScroll.width()) * goodsScroll.find('.goods_img_list li').size()
                });

                goodsScroll.find('.goods_img_list li').css({
                    width: (goodsScroll.width())
                });
            }

            //綁定滾動事件
            myScroll = new iScroll('goodsScroll', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                onScrollEnd: function() {
                    if (this.currPageX > (menuList.size() - 1)) {
                        return false;
                    };
                    menuList.removeClass('on');
                    menuList.eq(this.currPageX).addClass('on');
                }
            });
            menuList.bind('click', function() {
                myScroll.scrollToPage(menuList.index(this));
            });
            $(window).resize(function() {
                ReSize();
                myScroll.refresh();
            });
            goodsScroll.css('overflow', '');
        }
		
		
		$('#description').html(data.remark);
	},
	
	initPropHtml:function(){
	
		var data = this.data,
			itemlists = this.data.itemList;
		
		var tpl = $('#prop_item').html(),prophtml = '';
		
		var doms = $('[name=propitem]');
		
		for(var i=1;i<=5;i++){
			
			var hashtpl = tpl;
			
			var propName = data['prop'+i+'Name'],propList = null;
			
			if(propName){
				
				propList = data['prop'+i+'List'];
				
				$($(doms.get(i-1)).find('dt')).html(propName);
				
				if(propList){
					
					this.buildSkuItem(i,propList);
					
					if(i == 1){
						
						this.getSkuData(2,propList[0]['prop'+i+'DetailId'],propList[0]['skuId']);
					}
				}
				
			}else{
			
				doms.get(i-1).style.display = 'none';
			}
		}
	},
	getSkuData:function(idx,propId,skuId){
		
		var me = this;
		
		if(!me.data['prop'+idx+'Name']){
			
			me.skuId = skuId;
			
			for(var i=0;i<me.data.skuList.length;i++){
				
				if(me.data.skuList[i]['skuId'] == skuId){
					
					me.scalePrice = me.data.skuList[i].salesPrice;
					
					//$('[jitval=itemPrice]').html('商品积分值：'+me.data.skuList[i].salesPrice);
					
					//$('[jitval=oldPrice]').html('￥'+me.data.skuList[i].price);
					
					return;
					
				}
			}
			
			$('[jitval=itemPrice]').html('');
			
			return;
		}
		
		
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getSkuProp2List',
				'propDetailId':propId,
				'itemId':me.getUrlParam('goodsId')
			},
			success:function(data){
				
				if(data.code == 200){
					
					for(var name in data.content){
						
						for(var i=1;i<=5;i++){
							
							if(('prop'+i+'List') == name){
								
								me.buildSkuItem(i,data.content[name]);
								
								me.getSkuData(i+1,data.content[name][0]['prop'+i+'DetailId'],data.content[name][0]['skuId']);
								
								if(!me.data['prop'+(i+1)+'Name']){
									
									return;
								}
							}
						}
					}
				}
			}
		});
	},
	buildSkuItem:function(idx,list){
		
		var optionhtml = '';
		
		for(var p=0;p<list.length;p++){
				
			optionhtml += '<a class="'+((p==0)?'selected':'')
					   +  '" skuid="'+ list[p].skuId 
					   +  '" prop_detail_id="'
					   +  list[p]['prop'+idx+'DetailId']
					   +  '" >'+list[p]['prop'+idx+'DetailName']+'</a>';
		}
		
		$($($('[name=propitem]').get(idx-1)).find('dd')).html(optionhtml);
	},
	
	submitOrder:function(){
	
		var me = this;
		
		var adrs = me.getParams('select_address');
		
		if(!adrs){
		
			Jit.UI.Dialog({
				'content':'请先选择收货地址',
				'type':'Alert',
				'CallBackOk':function(){
					Jit.UI.Dialog('CLOSE');
				}
			});
			
			return;
		}
		
		if(!me.IsRegister){

			
			Jit.UI.Dialog({
				'content':'您尚未注册成为会员！无法兑换！',
				'type':'Alert',
				'LabelOk':'前住注册',
				'CallBackOk':function(){
				
					me.toPage('Register','&give=1');
				}
			});
			
			return ;
		}
		
		
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'itemID':me.getUrlParam('goodsId'),
				'linkMan':adrs.linkMan,
				'linkTel':adrs.linkTel,
				'address':adrs.address,
				'quantity':$('#goods_number').val(),
				'action':'setOrderIntegralInfo'
			},
			success:function(data){
				
				if(data.code == 200){
					
					Jit.UI.Dialog({
						'content':'订单提交成功！',
						'type':'Alert',
						'CallBackOk':function(){
						    //Jit.UI.Dialog('CLOSE');
						    me.toPage('JiFenShop');//订单提交成功后 进入积分首页
						}
					});
					
				}else{
					
					Jit.UI.Dialog({
						'content':data.description,
						'type':'Alert',
						'CallBackOk':function(){
							Jit.UI.Dialog('CLOSE');
						}
					});
				}
			}
		});
		
	}
});