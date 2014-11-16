/*定义页面*/
Jit.AM.defindPage({
	name: 'CouponList',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		this.initData();
		this.initEvent()
	},
	initData: function() {
		var self = this;
		self.elements.couponNav = $('.couponNav');
		self.elements.couponList = $('#couponList');
         
	    //获取列表类型
		self.ajax({
			interfaceMode:'V2.0',
			url: '/ApplicationInterface/Module/Coupon/CouponHandler.ashx',
			data: {
				'action': 'getCouponType'
			},
			success: function(data) {
				if (data && data.ResultCode == 200 && data.couponTypeList) {
					var htmlList = template.render('couponTabList', {
						'datas': data.couponTypeList
					});
					self.elements.couponNav.html(htmlList);
					var firstCouponTypeInfo = data.couponTypeList[0];
					if (firstCouponTypeInfo) {
						self.loadCouponList(firstCouponTypeInfo.CouponTypeID);
					};
				};
			}
		});
		Jit.WX.OptionMenu(true);
		//获取优惠券列表
	},
	loadCouponList: function(couponTypeID) {
            Jit.UI.Loading(true);
		var self = this;
		self.ajax({
			interfaceMode:'V2.0',
			url: '/ApplicationInterface/Module/Coupon/CouponHandler.ashx',
			data: {
				'action': 'getCouponList',
				'couponTypeID': couponTypeID
			},
			success: function(data) {
                  Jit.UI.Loading(false);
                  	self.elements.couponList.empty();
				if (data && data.ResultCode == 200 && data.couponList) {
					var htmlList = template.render('tpCouponList', {
						'datas': data.couponList
					});
					self.elements.couponList.html(htmlList);
				}else{
					self.elements.couponList.html('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">没有查找到您的优惠券信息!</div>');
				}
			}
		});
	},
	initEvent: function() {
		var self = this;
		//绑定类型事件
		self.elements.couponNav.delegate('a', self.eventType, function() {
       
			var element = $(this),
				couponTypeId = element.data('val');
			self.elements.couponNav.find('a').removeClass('on');
			element.addClass('on');
			self.loadCouponList(couponTypeId);
		});
	}
});