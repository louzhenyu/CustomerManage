Jit.AM.defindPage({

	name:'SelectAddress',
	
	onPageLoad:function(){
		this.loadPageData();
		this.initEvent();
	},
	loadPageData:function(){
		var me = this;
		me.storeList($("#city").val());
		me.provinceSel();
		
	},
	initEvent:function(){
		var me = this;
		$('#address_list').delegate(".address_store",'click',function(e){
			var that = $(this);
			if(e.target.className!="telephone"){
				if(that.hasClass('cur')){
					return;
				}else{
					$('.address_store').removeClass('cur');
					that.addClass('cur');
				}
				setTimeout(me.affirmAddress,100);
			}
		});
		
	},
	storeList:function(cityId){
		var me = this;
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getStoreListByItem',
				'orderId':me.getUrlParam('orderId'),
				'CityID':cityId,
				'page':1,
				'pageSize':99
			},
			success:function(data){
				if(data.code == 200){
					var list = data.content.storeList,
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
					$('#address_list').html(html);
					
					//me.initEvent();
					//$('.address_store').eq(0).trigger('click');
				}
			}
		});
	},
	provinceSel: function(){
		var me = this;
		me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getProvince'
            },
            success: function (data) {
				var itemlists = data.content.provinceList;
				var provinceStrList=[];
				$("#province").html("");
				$("#province").append("<option value=''>--请选择--</option>");
				$.each(itemlists, function (i, obj) {
					if (obj.Province == decodeURIComponent(me.getUrlParam('province'))) {
						provinceStrList.push("<option value='" + obj.Province + "' selected=\"true\">" + obj.Province + "</option>");
					} else {
						provinceStrList.push("<option value='" + obj.Province + "'>" + obj.Province + "</option>");
					}
				});
				$("#province").append(provinceStrList.join(""));
				JitPage.fnSelCityByProvince($("#province").val());
            }
        });
	},
	fnSelCityByProvince: function (province) {
        var me = this;
        //城市下拉框改变事件
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getCityByProvince',
                'Province': province
            },
            success: function (data) {
                var itemlists = data.content.cityList;
                var cityStrList = [];
                $("#city").html("");
                $("#city").append("<option value=''>--请选择--</option>");
                $.each(itemlists, function (i, obj) {
                    if (obj.CityName == decodeURIComponent(me.getUrlParam('city'))) {
                        cityStrList.push("<option value='" + obj.CityID + "' selected=\"true\">" + obj.CityName + "</option>");
                    } else {
                    	cityStrList.push("<option value='" + obj.CityID + "'>" + obj.CityName + "</option>");
                    }
                });
                $("#city").append(cityStrList.join(""));
            }
        });
    },
	affirmAddress:function(){
		var me = JitPage;
		var storeid = $('#address_list .cur').data('storeid'),
			deliveryAddress = $('#address_list .cur').data('address'),
			mobile =$('#address_list .cur').data('tel');
		me.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'setUpdateOrderDelivery',
				'storeId':storeid,
				'mobile':mobile,
				'deliveryAddress':deliveryAddress,
				'deliveryId':2,
				'orderId':Jit.AM.getUrlParam('orderId')
			},
			success:function(data){
				if(data.code == 200){
					me.pageBack();
				}
			}
		});
	}
});