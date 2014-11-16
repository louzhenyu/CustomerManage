Jit.AM.defindPage({

	name:'GoodsList',
	
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入GoodsList.....');
		
		var me = this;
		
		me.loadPageData();
	},
	
	loadPageData:function(){
		
		var me = this;
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'getShoppingCart',
				'page':1,
				'pageSize':99
			},
			success:function(data){
				
				$('#goods_list').html('');
				
				if(data.code == 200){
					
					$('#totalpty').html(data.content.totalQty);
					
					$('#totalprice').html('￥'+data.content.totalAmount);
					
					$('#payprice').html('￥'+data.content.totalAmount);
					
					var pagedata = {},list = data.content.itemList;
					
					me.orgData = data.content;
					
					for(var i=0;i<list.length;i++){
						
						pagedata[list[i]['skuId']] = list[i]['qty'];
						
						me.buildGoodsItem(list[i]);
					}
					
					me.data = pagedata;
					
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
			}
		});
	},
	buildGoodsItem:function(data){
		
		var tpl = $('#Tpl_goods_item').html();
		
		tpl = Mustache.render(tpl,data);
		
		$('#goods_list').append(tpl);
	},
	modify:function(skuId,type){
	
		var me = this;
		
		if(me.data[skuId] == 1 && type == -1){
		
			return ;
		}
		
		type>0?(me.data[skuId]++) : (me.data[skuId]--);
		
		me.submitAction();
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
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'setShoppingCartList',
				'itemList':list
			},
			success:function(data){
				
				if(data.code == 200){
					
					me.loadPageData();
				}
			}
		});
	},
	submitOrder:function(){
	
		var me = this;
		
		var list = [];
		
		/*
		for(var key in me.data){
			
			list.push({
				'skuId':key,
				'qty':me.data[key],
				'salesPrice':parseInt(me.scalePrice),
			});
		}
		*/
		var data = me.orgData.itemList;
		
		for(var i=0;i<data.length;i++){
			
			list.push({
				'skuId':data[i].skuId,
				'qty':data[i].qty,
				'salesPrice':data[i].salesPrice,
			});
		}
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'setOrderInfo',
				'orderDetailList':list,
				'qty':me.orgData.totalQty,
				'totalAmount':me.orgData.totalAmount,
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