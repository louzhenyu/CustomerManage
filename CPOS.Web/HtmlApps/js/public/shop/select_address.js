Jit.AM.defindPage({

    name: 'SelectAddress',

    onPageLoad: function () {

        this.loadPageData();
        this.initEvent();
    },
    loadPageData: function () {
        var me = this;

        var _data = {
            'action': 'getVipAddressList',
            'page': 1,
            'pageSize': 99
        };

        if (me.getUrlParam('orderId')) {

            _data.orderId = me.getUrlParam('orderId');
        }

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',

            data: _data,

            success: function (data) {
                if (data.code == 200) {

                    var list = data.content.itemList,
						tpl = $('#Tpl_address_item').html(),
						html = '';

                    me.data = list;

                    if (list.length <= 0) {

                        html = '<div style="text-align:center;line-height:60px;">暂无添加过配送地址！</div>';

                    } else {
                        var    item;
                        for (var i = 0; i < list.length; i++) {
                                 item=list[i];
                             item.isDefault=item.isDefault==1?"cur":'';
                            html += Mustache.render(tpl, list[i]);
                        }
                    }


                    $('#address_list').append(html);
                }
            }
        });
    },
    initEvent: function () {

        var me = this;
		$('#address_list').delegate("[name=address_item]","click",function(evt){

            var addressId = $(evt.currentTarget).attr('adrId');

            me.selectAdrId = addressId;

            $('[name=address_area]').removeClass('cur');

            $(evt.currentTarget).parent('[name=address_area]').addClass('cur');

            me.saveAddress();

        });
    },
    saveAddress: function () {

        var me = this, list = me.data, adrobj = null;

        for (var i = 0; i < list.length; i++) {

            if (list[i]['vipAddressID'] == me.selectAdrId) {

                adrobj = list[i];
            }
        }

        var _data = {
            'linkMan': adrobj.linkMan,
            'linkTel': adrobj.linkTel,
            'address': adrobj.province+adrobj.cityName+adrobj.districtName+adrobj.address
        };
        if (!me.getUrlParam('orderId')) {
            //当有没orderId时，不需要将地址绑定到订单
            me.setParams('select_address', _data);
            me.pageBack();

            return;
        }

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderAddress',
                'orderId': me.getUrlParam('orderId'),
                'linkMan': adrobj.linkMan,
                'linkTel': adrobj.linkTel,
                'address': adrobj.province+adrobj.cityName+adrobj.districtName+adrobj.address,
				'deliveryId' : 1
            },
            success: function (data) {
				me.setParams('select_address', _data);
                if (data.code == 200) {

                    me.pageBack();
                }
            }
        });
    },
    setDefault: function (adrId) {

        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setVipAddress',
                'vipAddressID': adrId,
                'isDefault': 1
            },
            success: function (data) {

                if (data.code == 200) {

                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                 	$('.address_store').removeClass('cur');
                    $('#'+adrId).addClass('cur');
                }
            }
        });
    },
    modify: function (adrId) {

        var me = this, list = me.data;

        for (var i = 0; i < list.length; i++) {

            if (list[i]['vipAddressID'] == adrId) {

                me.setParams('editAddressData', list[i]);
                me.toPage('AddAddress', '&type=edit&addressId=' + adrId + '&province=' + encodeURIComponent(list[i].province) + '&city=' + encodeURIComponent(list[i].cityName)+ '&districtName=' + encodeURIComponent(list[i].districtName));

                return;
            }
        }
    },
    removeAddress: function (adrId) {
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setVipAddress',
                'vipAddressID': adrId,
                'isDelete': 1
            },
            success: function (data) {
                if (data.code == 200) {
                    $('div[name=address_area]').remove();
                    me.loadPageData();
                    return ;
                }
            }
        });

    },
	clickPageBack: function(){
		var me = this;
		if(me.data&&me.data.length==0){
			 me.pageBack();
		}else{
			if($(".address_store.cur").length){
				me.selectAdrId = $(".address_store.cur").attr('id');
			}else{
				me.selectAdrId = $($('[adrid]').get(0)).attr('adrId');
			}
			me.saveAddress();
		}
	}
});