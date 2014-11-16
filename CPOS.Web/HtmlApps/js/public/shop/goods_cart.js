Jit.AM.defindPage({

	name:'GoodsList',
	
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入GoodsList.....');
		
		//基础数据
		this.newTotalPrice = 0;			// 零时总价
		this.newTotalQty = 0;			// 
		this.loadPageData();
		this.addPageEvent();
		//人人销售的店员ID
		var salesUserId=Jit.AM.getPageParam("_salesUserId_"),
        	channelId=Jit.AM.getPageParam("_channelId_");  //渠道ID
        if(salesUserId&&channelId){
            var appVersion=Jit.AM.getAppVersion();
            appVersion.AJAX_PARAMS="openId,customerId,userId,locale,ChannelID";
            Jit.AM.setAppVersion(appVersion);
            Jit.AM.setPageParam("_salesUserId_",salesUserId);
            Jit.AM.setPageParam("_channelId_",channelId);
            //公用参数
            var baseInfo=Jit.AM.getBaseAjaxParam();
            baseInfo.ChannelID=channelId;
            Jit.AM.setBaseAjaxParam(baseInfo);
        }else{   //没有传递过来则把数据清空掉
            Jit.AM.setPageParam("_salesUserId_",null);
            Jit.AM.setPageParam("_channelId_",null);
        }
	},
	addPageEvent:function(){
		var self = this;
		$("#section").delegate(".subBtn","tap",function(e){
			// 减一
			self.modify($(this).data("skuid"),-1,this);
		}).delegate(".addBtn","tap",function(e){
			// 加一
			self.modify($(this).data("skuid"),1,this);
		}).delegate(".jsSelectAll","tap",function(e){
			// 全选
			self.allselect();
		}).delegate(".jsSelectBtn","tap",function(e){
			var $this = $(this),
				skuId = $this.data("skuid"),
				thisData = self.carData[skuId],
				qty = thisData.qty,
				price = thisData.salesPrice,
				goodsPrice = Math.mul(price , qty);			//单个商品总价
				
			var totalPrice = parseFloat($('#totalprice').html().substring(1));//parse
			if($this.hasClass('on')){
				self.newTotalPrice = Math.subtr(totalPrice,goodsPrice);
				self.newTotalQty  = self.newTotalQty - qty;
				$('#totalprice').html('￥'+self.newTotalPrice);
				$this.removeClass('on');
				self.autoAllSelectState();
			}else{
				self.newTotalPrice = Math.add(totalPrice,goodsPrice);
				self.newTotalQty  = self.newTotalQty + qty;
				$('#totalprice').html('￥'+self.newTotalPrice);
				$this.addClass('on');
				self.autoAllSelectState();
			}
		});
		
		
	},
	loadPageData:function(){
		
		var me = this;
		
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getShoppingCart',
				'page':1,
				'pageSize':99
			},
			success:function(data){
				$('#goods_list').empty();
				if(data.code == 200){
					$('#totalprice').html('￥'+data.content.totalAmount);
					me.initTotalprice  = $('#totalprice').html();
					
					// 初始化全局参数
					me.newTotalPrice = data.content.totalAmount;
					me.newTotalQty = data.content.totalQty;
					//me.orgData = data.content;
					var pagedata = {},
						carData = {},
						list = data.content.itemList;
										
					for(var i=0;i<list.length;i++){
						var idata = list[i];
						carData[idata.skuId] = idata;
						pagedata[idata['skuId']] = idata['qty'];
						me.buildGoodsItem(idata);
					}
					me.carList = list;
					me.carData = carData;			//购物车对象，以skuid为key
					me.data = pagedata;							// 记录产品数量
					me.autoAllSelectState();			
					
					if(list.length==0){
						
						Jit.UI.Dialog({
							'content':'购物车列表为空',
							'type':'Alert',
							'LabelOk':'返回商场首页',
							'CallBackOk':function(){
								
								me.toPage('GoodsList');
							}
						});
					}
				}
			},
			complete:function(){
        		Jit.UI.Loading(false);
			}
		});
	},
	allselect:function(){
		var $allselect = $('.jsSelectAll'),
			$select = $('.jsSelectBtn');
		if($allselect.hasClass('on')){
			$allselect.removeClass('on');
			$select.removeClass('on');	
					
			$('#totalprice').html('￥0.0');
		}else{
			$allselect.addClass('on');
			$select.addClass('on');
			
			$('#totalprice').html(this.initTotalprice);
		}
	},
	// 自动根据让购物车中的选中的商品 是否全选
	autoAllSelectState:function(){
		if($("#goods_list .on").length==this.carList.length){
			$('.jsSelectAll').addClass('on');
		}else{
			$('.jsSelectAll').removeClass('on');
		}
	},
	buildGoodsItem:function(data){
		
		var tpl = $('#Tpl_goods_item').html();
		
		tpl = Mustache.render(tpl,data);
		
		$('#goods_list').append(tpl);
	},
	modify:function(skuId,type,e){
		var me = this;
		if(me.data[skuId] == 1 && type == -1){
			return ;
		}
		if(me.submitFlag){
			clearTimeout(me.submitFlag);
		}
		type>0?(me.data[skuId]++) : (me.data[skuId]--);
		Jit.log(me.data[skuId]);
		$(e).siblings(".countMonitor").val(me.data[skuId]);
		me.submitFlag = setTimeout(function(){
			me.submitAction();
		},1000);
	},
	removeGoods:function(skuId){
	
		var me = this;
		
		me.data[skuId] = -1;
		
		me.submitAction();
	},
	submitAction:function(){
		
		var me = this;
		
		var list = [];
		
		for(var key in me.data){
			
			list.push({
				'skuId':key,
				'qty':me.data[key]
			});
		}
		
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'setShoppingCartList',
				'itemList':list
			},
			beforeSend:function(){
        		Jit.UI.Loading(true,"删除中...");
			},
			success:function(data){
				
				if(data.code == 200){
					
					me.loadPageData();
					
					TopMenuHandle.ReCartCount();
				}
			}
		});
	},
	submitOrder:function(){
		//alert(1);
		var me = this;
		
		var list = [];
		var $selectBtn = $('.jsSelectBtn.on');
		
		for(var i=0;i<$selectBtn.length;i++){
			var obj = me.carData[$selectBtn.eq(i).data('skuid')];
			list.push({
				'skuId':obj.skuId,
				'qty':obj.qty,
				'salesPrice':obj.salesPrice
			});
		}
		//提交订单
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'setOrderInfo',
				'orderDetailList':list,
				'qty':me.newTotalQty,
				'reqBy':'0',
				'totalAmount':me.newTotalPrice,
				'SalesUser':me.salesUserId     //店员ID
			},
			success:function(data){
				
				if(data.code == 200){
					
					me.toPage('GoodsOrder','&orderId='+data.content.orderId);
					
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