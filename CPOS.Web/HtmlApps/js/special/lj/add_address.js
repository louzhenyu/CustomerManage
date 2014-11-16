Jit.AM.defindPage({
    name: 'SelectAddress',
    onPageLoad: function() {
        var me = this;
        var province, cityName;
        /*读取省市信息*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getProvince'
            },
            success: function(data) {
                me.loadPageData(data.content);
            }
        });
        if (me.getUrlParam('type') == 'edit') {
            $('#pagetitle').html('修改收货地址');
            me.addressId = me.getUrlParam('addressId');
            var data = me.getParams('editAddressData');
            me.initPageData(data);
        } else {
            $('#pagetitle').html('新增收货地址');
        }
    },
    loadPageData: function(data) {
        var me = this;
        var itemlists = data.provinceList;
        $("#province").html("");
        $("#province").append("<option value=''>--请选择--</option>");
        $.each(itemlists, function(i, obj) {
            if (obj.Province == decodeURIComponent(me.getUrlParam('province'))) {
                $("#province").append("<option value='" + obj.Province + "' selected=\"true\">" + obj.Province + "</option>");
            } else {
                $("#province").append("<option value='" + obj.Province + "'>" + obj.Province + "</option>");
            }
        });
        JitPage.fnSelChange();
    },
    initPageData: function(data) {
        var me = this;
        $('#username').val(data.linkMan);
        $('#phone').val(data.linkTel);
        $('#address').val(data.address);
    },
    saveAddress: function() {
	
        var me = this,provinceArea=$('#province'),cityArea=$('#city');
		
        var adrdata = {
            'action': 'setVipAddress',
            'linkMan': $('#username').val(),
            'linkTel': $.trim($('#phone').val()),
            'cityID': $('#city').val(),
            'address':provinceArea.find('option').eq(provinceArea.get(0).selectedIndex).text()+','+cityArea.find('option').eq(cityArea.get(0).selectedIndex).text()+','+$('#address').val()
        }


        if (!adrdata.linkMan) {
            Jit.UI.Dialog({
                'content': '请输入您的联系人姓名',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;


        }
        var regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
        if (!adrdata.linkTel) {
            Jit.UI.Dialog({
                'content': '请输入您的手机号码',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;
        } else if (!regPhone.test(adrdata.linkTel)) {
            Jit.UI.Dialog({
                'content': '您输入的手机号码不正确',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;
        }



        if (!adrdata.address) {
            Jit.UI.Dialog({
                'content': '请输入您的联系地址',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;


        }



        if (me.addressId) {
            adrdata.vipAddressID = me.addressId;
        }
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: adrdata,
            success: function(data) {
                if (data.code == 200) {
                    /*
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {

							

                        }
                    });
					*/
                    var _data = {
                        'linkMan': adrdata.linkMan,
                        'linkTel': adrdata.linkTel,
                        'address': adrdata.address
                    };

                    if (!me.getUrlParam('orderId')) {
                        //当有没orderId时，不需要将地址绑定到订单
                        me.setParams('select_address', _data);
                    }

                    me.toPage("FCindex");
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
    fnSelChange: function() {
        var me = this;
        //城市下拉框改变事件
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getCityByProvince',
                Province: $("#province").val()
            },
            success: function(data) {
                var itemlists = data.content.cityList;
                $("#city").html("");
                $("#city").append("<option value=''>--请选择--</option>");
                $.each(itemlists, function(i, obj) {
                    if (obj.CityName == decodeURIComponent(me.getUrlParam('city'))) {
                        $("#city").append("<option value='" + obj.CityID + "' selected=\"true\">" + obj.CityName + "</option>");
                    } else {
                        $("#city").append("<option value='" + obj.CityID + "'>" + obj.CityName + "</option>");
                    }
                });
            }
        });
    }
});