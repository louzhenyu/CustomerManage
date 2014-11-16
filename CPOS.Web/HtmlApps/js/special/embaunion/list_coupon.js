/*定义页面*/
Jit.AM.defindPage({
    name: 'Coupon',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Coupon.....');
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
                'action': 'myCouponList'
            },
            success: function (data) {
                me.loadPageData(data.content);

            }
        });
    },
    loadPageData: function (data) {

        var couponlists = data.couponList;

        /*清空页面数据*/
        var tpl = $('#Tpl_coupon_list').html(), html = '';

        if (couponlists.length > 0) {
            for (var i = 0; i < couponlists.length; i++) {
                html += Mustache.render(tpl, couponlists[i]);
            }
        } else {
            html += "<tr><td style=\"padding-top:15px\" colspan=\"3\">暂无数据</td></tr>";
        }

        /*页面数据渲染*/
        $('[tplpanel=coupon_list]').html(html);
    }
});