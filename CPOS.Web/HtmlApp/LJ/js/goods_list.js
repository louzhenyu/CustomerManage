Jit.AM.defindPage({

    name: 'ListGoods',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入ListGoods.....');

        this.initEvent();
    },

    initEvent: function () {

        var me = this;
        me.windowHeight = window.innerHeight;

        me.windowWidth = window.innerWidth;


        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemList',
                'isExchange': 0
            },
            success: function (data) {

                me.loadPageData(data.content);
            }
        });

    },
    loadPageData: function (data) {

        var itemlists = data.itemList;

        var tpl = $('#Tpl_goods_list').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            var itemdata = itemlists[i];
            if (itemdata.price > 0) {
                itemdata.oldprice = "￥" + itemdata.price;
            } else {
                itemdata.oldprice = '';
            }
            html += Mustache.render(tpl, itemdata);
        }

        $('[tplpanel=goods_list]').html(html);
    }
});