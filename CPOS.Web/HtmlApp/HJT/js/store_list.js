/*定义页面*/
Jit.AM.defindPage({
    name: 'StoreList',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入StoreList.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;
        var city = decodeURI(me.getUrlParam('city'));
        var store = me.getUrlParam('store');

        //数据请求
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreListByCity',
                city: city,
                store: store,
                page: 1,
                pageSize: 10000
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.storeList;
        //debugger;
        /*清空页面数据*/
        var tpl = $('#store_list_temp').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            if (itemlists[i].imageURL == null || itemlists[i].imageURL == "") {
                itemlists[i].imageURL = "../images/nopic.png";
            }
            html += Mustache.render(tpl, itemlists[i]);
        }

        /*页面数据渲染*/
        $('[tplpanel=store_html_list]').html(html);
    },
    fnExChange: function (itemId) {
        var me = this;
        var InDate = me.getUrlParam("InDate");
        var OutDate = me.getUrlParam("OutDate");
        me.toPage('HousingType', '&storeId=' + itemId + "&InDate=" + InDate + "&OutDate=" + OutDate);
    }
});