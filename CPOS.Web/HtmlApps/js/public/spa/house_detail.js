Jit.AM.defindPage({
    name: 'HouseDetail',
    onPageLoad: function () {
        Jit.log('进入HouseDetail.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetail',
                'storeId': me.getUrlParam('storeId'),
                "itemId": me.getUrlParam('itemId'),    //商品标识
                "beginDate": me.getParams('InDate'), //开始日期
                "endDate": me.getParams('OutDate')   //结束日期
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();

                //是否收藏
                this.isKeep = data.content.isKeep == 0 ? false : true;

                //加载照片
                me.loadPicData(data.content);

                //预定 房间名 价格
                var reserveHtml = itemNameHtml = priceHtml = "";
                reserveHtml = "<a href=\"javascript:JitPage.toOrderDetail('" + data.content.itemId + "')\">预 约</a>";
                itemNameHtml = data.content.itemName;
                priceHtml = "<br><b style='padding:0;' id='price' >￥" + data.content.skuList[0].salesPrice + "元</b>";

                $("#itemInfo").html(reserveHtml + itemNameHtml + priceHtml);

                //酒店描述
                $("#hotelDescription").html(data.content.itemIntroduce);

                //备注
                $("#remark").html(data.content.remark);

                me.refreshKeepState();
            }
        });

        //分享 动画效果
        $('.btn_share').bind('click', function () {
            $('#share-mask').show();
            $('#share-mask-img').show().attr('class', 'pullDownState');
        });
        $('#share-mask').bind('click', function () {
            var that = $(this);
            $('#share-mask-img').attr('class', 'pullUpState').show();
            setTimeout(function () { $('#share-mask-img').hide(500); that.hide(1000); }, 500);
        })
    },
    loadPicData: function (data) {
        var imglists = data.imageList;
        if (imglists && imglists.length > 0) {
            var imgHtml = barHtml = "";
            for (var i = 0; i < imglists.length; i++) {
                imgHtml += "<li><img src=\"" + imglists[i].imageURL + "\" ></li>";
                barHtml += "<li class=\"" + (i == 0 ? "cur" : "") + "\"></li>";
            };
            $('#goodsImgs').html(imgHtml);
            if (imglists.length > 1) {
                $('#goodsImgBar').html(barHtml);
                loaded();
            };
        }
        var myScroll;
        //拖拽事件
        function loaded() {
            var menuList = $('#goodsImgBar li'),
                goodsScroll = $('#goodsScroll');

            //重新設置大小
            ReSize();

            function ReSize() {
                goodsScroll.find('.goods_img_list').css({
                    width: goodsScroll.width() * goodsScroll.find('.goods_img_list li').size()
                });
                goodsScroll.find('.goods_img_list li').css({
                    width: goodsScroll.width()
                });
            }

            //綁定滾動事件
            myScroll = new iScroll('goodsScroll', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                onScrollEnd: function () {
                    if (this.currPageX > (menuList.size() - 1)) {
                        return false;
                    };
                    menuList.removeClass('cur');
                    menuList.eq(this.currPageX).addClass('cur');
                }
            });
            menuList.bind('click', function () {
                myScroll.scrollToPage(menuList.index(this));
            });

            $(window).resize(function () {
                ReSize();
                myScroll.refresh();
            });
        }
    },
    toOrderDetail: function (itemId) {
        var me = this;
        var storeId = me.getUrlParam('storeId');
        me.toPage('OrderSubmit', '&storeId=' + storeId + '&itemId=' + itemId);
    },
    setItemKeep: function () {
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
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
        
        var me = this;
        var keephtml = this.isKeep ? '<i></i>取消收藏' : '<i></i>收藏';
        $('#btn_keep').html(keephtml);
    }
})