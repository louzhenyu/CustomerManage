Jit.AM.defindPage({

    name: 'MyFavorites',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入MyFavorites.....');

        this.initEvent();
    },

    initEvent: function () {

        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemKeep',
                'page': 1,
                'pageSize': 100
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
    },
    loadPageData: function (data) {
        var itemlists = data.itemList;
        var tpl = $('#Tpl_pro_list').html(), html = '';

        if (itemlists.length > 0) {
            for (var i = 0; i < itemlists.length; i++) {
                var itemdata = itemlists[i];
                html += Mustache.render(tpl, itemdata);
            }
        } else {
            html += "暂无数据";
        }

        $('[tplpanel=pro_list]').html(html);
    }
});