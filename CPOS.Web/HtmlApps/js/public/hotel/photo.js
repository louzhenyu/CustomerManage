Jit.AM.defindPage({
    name: 'Photo',
    onPageLoad: function () {
        Jit.log('进入Photo.....');
        var me = this;
        this.initPage(function () {
            me.imagesLoaded();  //进行瀑布流布局
            me.imagesSwipe();
        });
    },
    imagesLoaded: function () {
        //瀑布流
        $('#Gallery').imagesLoaded(function () {
            var options = {
                autoResize: true,
                container: $('#main'),
                offset: 4,
                itemWidth: 150
            };
            var handler = $('#Gallery li');
            handler.wookmark(options);
        });
    },
    imagesSwipe: function () {
        $('#Gallery a').photoSwipe();
    },
    initPage: function (callback) {
        debugger;
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': me.getUrlParam('storeId')
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {
                    if (data.content && data.content.imageList.length > 0) {
                        var html = bd.template("albumsTpl", data);
                        $("#Gallery").html(html);
                        callback();
                    }
                }
            }
        });
    }
});