Jit.AM.defindPage({
    name: 'Introduce',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Introduce.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        /*页面异步请求数据*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': me.getUrlParam('storeId')
            },
            success: function (data) {
                me.loadStoreData(data.content);
            }
        });
    },
    loadStoreData: function (data) {
        debugger;
        $('[jitval=itemName]').html(data.storeName);
        $('[jitval=addr]').html(data.address);
        $('[jitval=tel]').html("Tel: " + data.tel);
        $('[jitval=fax]').html("Fax: " + data.fax);
        if (data.latitude != "" && data.longitude != null && data.latitude != 0 && data.longitude != 0) {
            $("#mapUrl").attr("href", "javascript:Jit.AM.toPage('H_Map','&lat=" + data.latitude + "&lng=" + data.longitude + "&addr=" + data.address + "&store=" + data.storeName + "');");
        }
        if (data.introduceContent != null && data.introduceContent != "") {
            $('[jitval=info]').html(data.introduceContent);
        } else {
            $('[jitval=info]').html('暂无内容');
        }
    },
    urlGoTo: function (url) {
        var me = this;
        me.toPage(url, '&storeId=' + me.getUrlParam('storeId') + '&city=' + me.getUrlParam('city') + "&lat=" + me.getUrlParam("lat") + "&lng=" + me.getUrlParam("lng"));
    }
});