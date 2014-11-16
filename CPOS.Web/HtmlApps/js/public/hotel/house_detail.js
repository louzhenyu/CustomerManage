Jit.AM.defindPage({
    name: 'HouseDetail',
	elements:{},
    onPageLoad: function () {
        Jit.log('进入HouseDetail.....');
        this.initEvent();
        this.elements.bookBtn=$('#bookBtn');
    },
    initEvent: function () {
        var me = this;

        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getHotelItemDetail',
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

                itemNameHtml = data.content.itemName;
                priceHtml = "<br><b style='padding:0;' id='price' >￥" + data.content.skuList[0].salesPrice + "元</b>";


                $(".name").html(itemNameHtml);

                if (Jit.AM.getBaseAjaxParam().userId) {
                    me.elements.bookBtn.attr("href", "javascript:JitPage.toOrderDetail('" + data.content.itemId + "')");
                } else {
                    me.elements.bookBtn.attr("href", "javascript:Jit.AM.toPage('GetVipCard')");
                }


                //备注
                $("#remark").html(data.content.remark);
                $('.describe').html(data.content.itemName+'<a style="color:white;" href=javascript:JitPage.toPage("H_IntroduceDetail","storeId='+me.getUrlParam('storeId')+'");>详情>></a>');
                if (data.content.skuList[0].VipLevelName == null || data.content.skuList[0].VipLevelName == "")
	        {
	            data.content.skuList[0].VipLevelName = "微信";
	        }
		$('#salesPrice').html( data.content.skuList[0].VipLevelName + "价¥" + data.content.skuList[0].salesPrice + "/晚");
                if (data.content.skuList[0].price > 0) {
                    $('#price').html("市场价¥" + data.content.skuList[0].price + "/晚")
                }
                else {
                    $('#price').html("市场价¥0/晚");
                }
                var imgHtml = "<img src=\"" + data.content.RoomImg + "\" >";
                if (data.content.RoomImg != null && data.content.RoomImg!="")
                {
                $('.pic').html(imgHtml);
                }
                //酒店描述
                $(".exp").html(data.content.RoomDesc);
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
                barHtml += "<em class=\"" + (i == 0 ? "on" : "") + "\"></em>";
            };
            $('#goodsImgs').html(imgHtml);
            if (imglists.length > 0) {
                $('#goodImgBar').html(barHtml);
                loaded();
            };
        }
        var myScroll;
        //拖拽事件
        function loaded() {
            var menuList = $('#goodImgBar em'),
                goodsScroll = $('#goodsScroll');

            //重新設置大小
            ReSize();

            function ReSize() {
                goodsScroll.find('#goodsImgs').css({
                    width: goodsScroll.width() * goodsScroll.find('li').size()
                });
                goodsScroll.find('li').css({
                    width: goodsScroll.width()
                });
            }

            //綁定滾動事件
            myScroll = new iScroll('goodsScroll', {
                snap: true,
                momentum: false,
                vScroll: false,
                hScrollbar: false,
                vScrollbar: false,
                onScrollEnd: function () {
                    if (this.currPageX > (menuList.size() - 1)) {
                        return false;
                    };
                    menuList.removeClass('on');
                    menuList.eq(this.currPageX).addClass('on');
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
        var itemId = me.getUrlParam('itemId');
        me.toPage('H_OrderSubmit', '&storeId=' + storeId + '&itemId=' + itemId);
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
                    debugger;
                    me.isKeep = !me.isKeep;
                    me.refreshKeepState();
                }
            }
        });
    },
    refreshKeepState: function () {
        //        debugger;
        //        var me = this;
        //        var keephtml = this.isKeep ? '<i></i>取消收藏' : '<i></i>收藏';
        //        $('#btn_keep').html(keephtml);
    }
})