Jit.AM.defindPage({

	name:'SelectAddress',
	
	onPageLoad:function(){
		
		var me = this;
		/*
		
		*/
		
		me.loadPageData();
	},
	loadPageData:function(){
	
		var me = this;
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'getVipAddressList',
				'orderId':me.getUrlParam('orderId'),
				'page':1,
				'pageSize':99
			},
			
			success:function(data){
				
				if(data.code == 200){
					
					var list = data.content.itemList,
						tpl = $('#Tpl_address_item').html(),
						html = '';
					
					me.data = list;
					
					if(list.length<=0){
						
						html = '<div style="text-align:center;line-height:60px;">暂无添加过配送地址！</div>';
						
					}else{
						
						for(var i=0;i<list.length;i++){
						
							html += Mustache.render(tpl,list[i]);
						}
					}
					
					
					$('#address_list').append(html);
					
					me.initEvent();
				}
			}
		});
	},
	initEvent:function(){
		
		var me = this;
		
		$('[name=address_item]').bind('click',function(evt){
			
			var addressId = $(evt.currentTarget).attr('adrId');
			
			me.selectAdrId = addressId;
			
			$('[name=address_item]').removeClass('cur');
			
			$(evt.currentTarget).addClass('cur');
		});
	},
	saveAddress:function(){
	
		var me = this;
		
		if(!me.selectAdrId){
			
			Jit.UI.Dialog({
				'content':'请选选择地址！',
				'type':'Alert',
				'CallBackOk':function(){
					Jit.UI.Dialog('CLOSE');
				}
			});
			
			return;
		}
		
		var me = this;
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'getOrderList',
				'addressId':me.selectAdrId,
			},
			success:function(data){
				
				if(data.code == 200){
					
					me.pageBack();
				}
			}
		});
	},
	setDefault:function(adrId){
		
		var me = this;
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'setVipAddress',
				'vipAddressID':adrId,
				'isDefault':1
			},
			success:function(data){
				
				if(data.code == 200){
					
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
	},
	modify:function(adrId){
		
		var me = this,list = me.data;
		
		for(var i=0;i<list.length;i++){
			
			if(list[i]['vipAddressID'] == adrId){
				
				me.setParams('editAddressData',list[i]);
				
				me.toPage('AddAddress','&type=edit&addressId='+adrId);
				
				return;
			}
		}
	},
	removeAddress:function(adrId){
		
		var me = this;
		
		me.ajax({
			url:'../../../OnlineShopping/data/Data.aspx',
			data:{
				'action':'setVipAddress',
				'vipAddressID':adrId,
				'isDelete':1
			},
			success:function(data){
				
				if(data.code == 200){
					
					me.loadPageData();
				}
			}
		});
	},
});