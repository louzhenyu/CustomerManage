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

        //获取地址栏参数
        var city = decodeURI(me.getUrlParam('city'));
        var storeId = me.getUrlParam('storeId');

        //根据城市名称 获取酒店列表 并根据坐标计算酒店离当前位置的距离
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreListByCityName',
                CityName: city,
                Latitude: me.getUrlParam('lat'),
                Longitude: me.getUrlParam('lng'),
                BeginDate: me.getParams('InDate'),
                EndDate: me.getParams('OutDate'),
                store: storeId,
                page: 1,
                pageSize: 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    me.loadPageData(data.content);
                }
            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.storeList;

        /*清空页面数据*/
        var tpl = $('#store_list_temp').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            //酒店距离
            if (itemlists[i].Distance != null && itemlists[i].Distance != "" && itemlists[i].Distance != 0) {
                itemlists[i].Distance = "距我 " + itemlists[i].Distance + "km";
            } else {
                itemlists[i].Distance = "未知距离";
            }
            var isfull = itemlists[i].IsFull;
            //酒店是否满房
            if (itemlists[i].IsFull != null && itemlists[i].IsFull != "" && itemlists[i].IsFull == 1) {
                itemlists[i].IsFull = "<img src=\"../../../images/public/hotel_default/full_room.png\" style=\"text-align: right;\">";
            } else {
                itemlists[i].IsFull = "";
            }

            //酒店图片
            if (itemlists[i].imageURL == null || itemlists[i].imageURL == "") {
                //itemlists[i].imageURL = "../images/nopic.png";
            }
            var xhtml = Mustache.render(tpl, itemlists[i]);
            if (isfull == 1) {
                xhtml = xhtml.replace('<li>', '<li class="fullStatus">');
                // xhtml.replace("<img src="../../../images/public/hotel_default/full_room.png" style="text-align: right;">",'');
            }
            html += xhtml;
        }

        /*页面数据渲染*/
        $('[tplpanel=store_html_list]').html(html);
    },
    fnExChange: function (storeId) {
        var me = this;
        me.toPage('H_HousingType', '&storeId=' + storeId + '&city=' + me.getUrlParam('city') + "&lat=" + me.getUrlParam("lat") + "&lng=" + me.getUrlParam("lng"));
    }
});