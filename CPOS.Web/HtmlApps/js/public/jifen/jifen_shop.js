/*定义页面*/
Jit.AM.defindPage({
    name: 'JiFenShop',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入JiFenShop.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemList',
                'isExchange': 1,
				'page':1,
				'pageSize':99
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
        //请求当前人员积分数据
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getVipValidIntegral'
            },
            success: function (data) {
                $('[jitval=count]').html(data == "" ? 0 : parseInt(data.data));
            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.itemList;

        /*清空页面数据*/
        var tpl = $('#Tpl_pro_list').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            if (itemlists[i].imageUrl == null || itemlists[i].imageUrl == "") {
                itemlists[i].imageUrl = "../images/nopic.png";
            }
            html += Mustache.render(tpl, itemlists[i]);
        }

        /*页面数据渲染*/
        $('[tplpanel=pro_list]').html(html);
    },
    fnExChange: function (itemId) {

        var me = this;

        me.toPage('GoodsJiFenDetail', '&goodsId=' + itemId);

        $(".popup").css("display", "block");
    },
    fnDivHide: function () {
        $(".popup").css("display", "none");
    }
});