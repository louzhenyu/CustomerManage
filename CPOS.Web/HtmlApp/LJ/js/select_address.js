Jit.AM.defindPage({

    name: 'SelectAddress',

    onPageLoad: function () {

        var me = this;
        /*
		
        */

        me.loadPageData();
    },
    loadPageData: function () {
        var me = this;

        var _data = {
            'action': 'getVipAddressList',
            'page': 1,
            'pageSize': 99
        }

        if (me.getUrlParam('orderId')) {

            _data.orderId = me.getUrlParam('orderId');
        }

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',

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

                        for (var i = 0; i < list.length; i++) {

                            html += Mustache.render(tpl, list[i]);
                        }
                    }


                    $('#address_list').append(html);

                    me.initEvent();
                }
            }
        });
    },
    initEvent: function () {

        var me = this;

        $('[name=address_item]').bind('click', function (evt) {

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
            'address': adrobj.address
        };

        if (!me.getUrlParam('orderId')) {
            //当有没orderId时，不需要将地址绑定到订单
            me.setParams('select_address', _data);
            me.pageBack();

            return;
        }

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderAddress',
                'orderId': me.getUrlParam('orderId'),
                'linkMan': adrobj.linkMan,
                'linkTel': adrobj.linkTel,
                'address': adrobj.address
            },
            success: function (data) {

                if (data.code == 200) {

                    me.pageBack();
                              me.initEvent();
                }
            }
        });
    },
    setDefault: function (adrId) {

        var me = this;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
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
                }
            }
        });
    },
    modify: function (adrId) {

        var me = this, list = me.data;

        for (var i = 0; i < list.length; i++) {

            if (list[i]['vipAddressID'] == adrId) {

                me.setParams('editAddressData', list[i]);

                me.toPage('AddAddress', '&type=edit&addressId=' + adrId + '&province=' + escape(list[i].province) + '&city=' + escape(list[i].cityName));

                return;
            }
        }
    },
    removeAddress: function (adrId) {
        var me = this;
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setVipAddress',
                'vipAddressID': adrId,
                'isDelete': 1
            },
            success: function (data) {
                if (data.code == 200) {
                    $('div[name=address_area]').remove();
                    me.loadPageData();
                              me.initEvent();
                    return ;
                }
            }
        });

    }
});