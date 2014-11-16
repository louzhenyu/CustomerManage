/*定义页面*/
Jit.AM.defindPage({
	name: 'CouponDetail',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('进入Coupon.....');
		this.initData();
		this.initEvent()
	},
	initData: function() {
		var self = this,
			urlCouponId = self.getUrlParam('couponId');
		self.elements.txtStatus = $('#txtStatus');
		self.elements.txtCouponDesc = $('#txtCouponDesc');
		self.elements.txtVipName = $('#txtVipName');
		self.elements.txtBeginDate = $('#txtBeginDate');
		self.elements.txtEndDate = $('#txtEndDate');
		self.elements.txtResidueDay = $('#txtResidueDay');
		self.elements.txtCouponCode = $('#txtCouponCode');
		self.elements.imgCouponCode = $('#imgCouponCode');
		self.elements.txtDate = $('#txtDate');
		self.elements.txtCountDown = $('.countDown');
		self.elements.txtCouponName=$('#txtCouponName');

        Jit.UI.Loading(true);
		//获取列表类型
        self.ajax({
            interfaceMode: 'V2.0',
            url: '/ApplicationInterface/Module/Coupon/CouponHandler.ashx',
            data: {
                'action': 'getCouponDetail',
                'cuponID': urlCouponId
            },
            success: function (data) {
                Jit.UI.Loading(false);

                if (data.ResultCode && data.ResultCode == 200 && data.couponDetail) {
                    self.elements.txtCouponDesc.html(data.couponDetail.CouponDesc);
                    self.elements.txtVipName.html(data.couponDetail.VipName);
                    if (data.couponDetail.iseffective == 1) {   //  是否是永久有效
                        self.elements.txtDate.html('有效期：永久');
                        self.elements.txtCountDown.hide();
                    } else {
                        self.elements.txtBeginDate.html(data.couponDetail.BeginDate.substring(0, data.couponDetail.BeginDate.indexOf("T")));
                        self.elements.txtEndDate.html(data.couponDetail.EndDate.substring(0, data.couponDetail.EndDate.indexOf("T")));
                    }
                    if (data.couponDetail.isexpired == 1 || data.couponDetail.Status == 1) {
                        self.elements.txtStatus.find('span').html('不可用');
                        self.elements.txtStatus.find('em').addClass('not');
                        self.elements.txtCountDown.hide();

                        if (data.couponDetail.isexpired == 1) {
                            $("#txtDate").append("(已过期)");
                        }
                    };
                    self.elements.txtCouponName.html(data.couponDetail.CouponName);
                    self.elements.txtResidueDay.html(data.couponDetail.diffDay);
                    self.elements.txtCouponCode.html(data.couponDetail.CouponCode);
                    self.elements.imgCouponCode.attr('src', data.couponDetail.QRUrl);
                };
            }
        });
		Jit.WX.OptionMenu(true);
	},
	initEvent: function() {
		var self = this;
	}
});