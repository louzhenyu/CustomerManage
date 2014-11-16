/*定义页面*/
Jit.AM.defindPage({
    name: 'Reserve',
    onPageLoad: function () {
        //当页面加载完成时触发
        //Jit.log('进入Reserve.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        //数据请求
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                action: "getOrderList",
                page: "1",
                pageSize: "15"
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
    },
    loadPageData: function (data) {
        //debugger;

        /*清空页面数据*/
        var tpl = $('#Tpl_list').html(), html = '';

        for (var i = 0; i < data.orderList.length; i++) {
            html += Mustache.render(tpl, data.orderList[i]);
        }

        /*页面数据渲染*/
        $('[tplpanel=order_list]').html(html);
    }
});