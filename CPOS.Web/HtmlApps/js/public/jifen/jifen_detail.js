/*定义页面*/
Jit.AM.defindPage({
    name: 'JiFenGift',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入JiFenGift.....');
		this.initShare();
        this.initEvent();
    },
    initShare:function(){
    	var appVersion = Jit.AM.getAppVersion();
    	var href = location.href.split("?")[0]+"?customerId="+Jit.AM.getUrlParam("customerId");
    	var imgUrl = $('img').eq(0).attr('src');
        Jit.WX.shareFriends(appVersion.APP_NAME,null,href,imgUrl);
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
                'action': 'getIntegralDetailList'
            },
            success: function (data) {
                me.loadPageData(data.content);

            }
        });
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getIntegralRuleList'
            },
            success: function (data) {
                me.loadData(data.content);

            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.VipIntegralDetailList;

        /*清空页面数据*/
        var tpl = $('#Tpl_jifen_list').html(), html = '';

        if (itemlists.length > 0) {
            for (var i = 0; i < itemlists.length; i++) {
                html += Mustache.render(tpl, itemlists[i]);
            }
        } else {
            html += "<tr><td colspan='3' style=\"text-align:left;\" >暂无记录</td></tr>";
        }

        /*页面数据渲染*/
        $('[tplpanel=jifen_list]').html(html);
    },
    loadData: function (data) {

        var itemlists = data.IntegralRuleList;

        /*清空页面数据*/
        var tpl = $('#Tpl_rule_list').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            html += Mustache.render(tpl, itemlists[i]);
        }

        /*页面数据渲染*/
        $('[tplpanel=rule_list]').html(html);
    },
    openWindow: function () {
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderIntegral'
            },
            success: function (data) {
                var itemlists = data.content.itemList;
                var itemhtml = "";
                if (itemlists.length > 0) {
                    for (var i = 0; i < itemlists.length; i++) {
                        itemhtml += "<tr><td><span>" + itemlists[i].CreateTime + "</span></td><td>" + itemlists[i].itemName + "</td><td align=\"left\"><b>" + itemlists[i].Integral + "</b></td></tr>";
                    }
                } else {
                    itemhtml += "<tr><td colspan=\"3\">暂无数据</td></tr>";
                }
                var height = $(window).scrollTop() + 120;                
                var panel = $('<div class="popup" style="margin-top:' + height + 'px"><a href="javascript:;" onclick="javascript:JitPage.CLOSE();" class="btn_close">&times;</a><table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><th scope="col">时间</th><th align="left" scope="col">兑换奖品</th><th scope="col">所用积分</th></tr>' + itemhtml + '</table></div>');
                panel.css({
                    'left': '0',
                    'right': '0',
                    'top': '0',
                    'z-index': '99'
                });
                panel.appendTo($('body'));
            }
        });
    },
    CLOSE: function () {
        var panel = $('.popup');
        if (panel) {
            (panel).remove();
        }
    }
});