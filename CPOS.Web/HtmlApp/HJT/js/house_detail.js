Jit.AM.defindPage({
    /*定义页面名称 必须和config文件中设置的属性一直*/
    name: 'HouseDetail',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入HouseDetail.....');
        this.initEvent();
    }, initEvent: function () {
        var me = this;

        //数据请求
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetail',
                'storeId': me.getUrlParam('storeId'),
                "itemId": me.getUrlParam('itemId'),    //商品标识
                "beginDate": me.getUrlParam('InDate'), //开始日期
                "endDate": me.getUrlParam('OutDate')   //结束日期
            },
            success: function (data) {
                //debugger;
                this.isKeep = data.isKeep;     //是否收藏
                me.loadPicData(data.content);  //加载照片
                $("#price").html("￥" + data.content.skuList[0].salesPrice + "元");
                var html1 = "";
                //if (data.content.overCount > 0) {
                    html1 = "<a href=\"javascript:JitPage.toOrderDetail('" + data.content.itemId + "')\">预 定</a>";
//                } else {
//                    html1 = "<a href=\"#\">满房</a>";
//                }
                var html2 = data.content.itemName;
                var html3 = "<b id='price' >￥" + data.content.skuList[0].salesPrice + "元</b>";

                $("#itemInfo").html(html1 + html2 + html3);
                $("#remark").html(data.content.remark);
            }
        });
    }, loadPicData: function (data) {
        var itemlists = data.imageList;
        $.each(itemlists, function (i, v) {
            $("#ulStorePics").append("<li><img src=" + v.imageURL + "></li>");
        });
    }, toOrderDetail: function (itemId) {
        var me = this;
        var storeId = me.getUrlParam('storeId');
        me.toPage('OrderSubmit', '&storeId=' + storeId + '&itemId=' + itemId + "&InDate=" + me.getUrlParam("InDate") + "&OutDate=" + me.getUrlParam("OutDate"));

    }, setItemKeep: function () {

        var me = this;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setItemKeep',
                'itemId': me.getUrlParam('itemId'),
                'keepStatus': (this.isKeep ? '0' : '1')
            },
            success: function (data) {

                if (data.code == 200) {

                    me.isKeep = !me.isKeep;

                    me.refreshKeepState();
                }
            }
        });
    },
    refreshKeepState: function () {

        var keephtml = this.isKeep ? '<i></i>取消收藏' : '<i></i>收藏';

        $('#btn_keep').html(keephtml);
    }
})