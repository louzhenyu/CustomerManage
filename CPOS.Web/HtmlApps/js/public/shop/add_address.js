Jit.AM.defindPage({
	name : 'SelectAddress',
	onPageLoad : function() {
		var me = this;
		var province, cityName;

		if (me.getUrlParam('type') == 'edit') {
			$('#pagetitle').html('修改收货地址');
			me.addressId = me.getUrlParam('addressId');
			var data = me.getParams('editAddressData');
			me.initPageData(data);
		} else {
			$('#pagetitle').html('新增收货地址');
		}

		me.fnGetProvince();
	},
	fnGetProvince : function() {
		var self = this;
		$.area.getProvince(function(data) {
			var itemlists = data.provinceList;
			var $provinceSelect = $("#province");
			$provinceSelect.empty();
			var optionStr = "<option value=''>--请选择--</option>";
			$.each(itemlists, function(i, obj) {
				if (obj.Province == decodeURIComponent(self.getUrlParam('province'))) {
					optionStr += "<option value='" + obj.Province + "' selected=\"true\">" + obj.Province + "</option>";
				} else {
					optionStr += "<option value='" + obj.Province + "'>" + obj.Province + "</option>";
				}
			});
			$provinceSelect.html(optionStr);
			self.fnGetCityByProvince($provinceSelect.val());
		});
	},
	fnGetCityByProvince : function(province) {
		var self = this;
		$.area.getCityByProvince(province, function(data) {
			var itemlists = data.cityList;
			var $citySelect = $("#city");
			$citySelect.empty();
			var optionStr = "<option value=''>--请选择--</option>";
			$.each(itemlists, function(i, obj) {
				if (obj.CityName == decodeURIComponent(self.getUrlParam('city'))) {
					optionStr += "<option value='" + obj.CityID + "' selected=\"true\">" + obj.CityName + "</option>";
				} else {
					optionStr += "<option value='" + obj.CityID + "'>" + obj.CityName + "</option>";
				}
			});
			$citySelect.html(optionStr);
			self.fnGetDistrictByCity($citySelect.val());
		});
	},
	fnGetDistrictByCity : function(city) {
		var self = this;
		$.area.getDistrictByCity(city, function(data) {
			var itemlists = data;
			var $districtSelect = $("#district");
			$districtSelect.empty();
			var optionStr = "<option value=''>--请选择--</option>";
			$.each(itemlists, function(i, obj) {
				console.log(obj.Name +"%%%"+ decodeURIComponent(self.getUrlParam('districtName')));
				if (obj.Name == decodeURIComponent(self.getUrlParam('districtName'))) {
					optionStr += "<option value='" + obj.DistrictID + "' selected=\"true\">" + obj.Name + "</option>";
				} else {
					optionStr += "<option value='" + obj.DistrictID + "'>" + obj.Name + "</option>";
				}
			});
			$districtSelect.html(optionStr);
		});
	},
	initPageData : function(data) {
		var me = this;
		$('#username').val(data.linkMan);
		$('#phone').val(data.linkTel);
		$('#address').val(data.address);
	},
	saveAddress : function() {
		var me = this, 
			provinceArea = $('#province').val(), 
			cityArea = $('#city').val(),
			districtArea = $("#district").val(),
			detailArea = $('#address').val();
		var adrdata = {
			'action' : 'setVipAddress',
			'linkMan' : $('#username').val(),
			'linkTel' : $.trim($('#phone').val()),
			'cityID' : 	$('#district').val(),
			'address' : detailArea
		};

		if (!adrdata.linkMan) {
			$.alert('请输入您的联系人姓名');
			return;
		}
		var regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
		if (!adrdata.linkTel) {
			$.alert('请输入您的手机号码');
			return;
		} else if (!Jit.fn.valid.isPhoneNumber(adrdata.linkTel)) {
			$.alert('您输入的手机号码不正确');
			return;
		}
		if (!provinceArea.length||!cityArea.length||!districtArea.length) {
			$.alert('请选择省市区');
			return;
		}
		if (!detailArea.length) {
			$.alert('请输入您的联系地址');
			return;
		}

		if (me.addressId) {
			adrdata.vipAddressID = me.addressId;
		}
		me.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : adrdata,
			success : function(data) {
				if (data.code == 200) {
					var _data = {
						'linkMan' : adrdata.linkMan,
						'linkTel' : adrdata.linkTel,
						'address' : adrdata.address
					};
					if (!me.getUrlParam('orderId')) {
						//当有没orderId时，不需要将地址绑定到订单
						me.setParams('select_address', _data);
					}
					me.pageBack();
				} else {
					$.alert(data.description);
				}
			}
		});
	}
}); 