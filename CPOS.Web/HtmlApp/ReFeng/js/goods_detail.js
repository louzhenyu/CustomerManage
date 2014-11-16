Jit.AM.defindPage({

    name: 'GoodsDetail',

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('进入GoodsDetail.....');

        var me = this;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetail',
                'itemId': me.getUrlParam('goodsId')
            },
            success: function(data) {

                me.loadGoodsDetail(data.content);
            }
        });

        $('#btn_delNum').bind('click', function() {

            var num = $('#goods_number').val();

            num--;

            if (num <= 1) {

                num = 1;
            }

            $('#goods_number').val(num);

            $('#forpoint').html(parseInt(me.data.Forpoints) * num);
        });

        $('#btn_addNum').bind('click', function() {

            var num = $('#goods_number').val();

            num++;

            $('#goods_number').val(num);

            $('#forpoint').html(parseInt(me.data.Forpoints) * num);
        });
    },

    initEvent: function() {
        /*
        $('[name=prop_option]').children().bind('click',function(evt){
			
        $('[name=prop_option]').children().removeClass('selected');
			
        JitPage.skuId = $(evt.target).addClass('selected').attr('skuId');
			
        });
        */
        $('[name=prop_option]').each(function(i, item) {

            $(item).bind('click', function(evt) {

                var skuid = $(evt.target).attr('skuId');

                if (skuid) {

                    $(evt.currentTarget).children().removeClass('selected');

                    var skuid = $(evt.target).addClass('selected').attr('skuid');

                    var idx = parseInt($(evt.currentTarget).attr('propidx'));

                    var propdetailid = $(evt.target).attr('prop_detail_id');

                    JitPage.getSkuData(idx + 1, propdetailid, skuid);
                }
            });
        })
    },
    loadGoodsDetail: function(data) {

        this.data = data;

        this.isKeep = data.isKeep;

        this.initPageInfo();

        this.initPropHtml();

        this.initEvent();
    },
    addToGoodsCart: function() {

        var me = this;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setShoppingCart',
                'skuId': me.skuId,
                'qty': $('#goods_number').val()
            },
            success: function(data) {

                Jit.UI.Dialog({
                    'content': '添加成功!',
                    'type': 'Confirm',
                    'LabelOk': '再逛逛',
                    'LabelCancel': '去购物车结算',
                    'CallBackOk': function() {
                        me.toPage('GoodsList');
                    },
                    'CallBackCancel': function() {
                        me.toPage('GoodsCart');
                    }
                });
            }
        });
    },
    initPageInfo: function() {

        var data = this.data;

        $('[jitval=itemName]').html(data.itemName);

        // $('[jitval=itemImage]').attr('src', data.imageList[0].imageURL);

        if (data.imageList && data.imageList.length > 0) {
            var imgHtml = '',
                barHtml = '';
            for (var i = 0; i < data.imageList.length; i++) {
                imgHtml += "<li><img height=\"160\" src=\"" + data.imageList[i].imageURL + "\" ></li>";
                barHtml += "<li class=\"" + (i == 0 ? "on" : "") + "\">" + (i + 1) + "</li>";
            };
            $('#goodsImgs').html(imgHtml);
            if (data.imageList.length > 1) {
                $('#goodsImgBar').html(barHtml);
                loaded();
            };
        };


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
                onScrollEnd: function() {
                    if (this.currPageX > (menuList.size() - 1)) {
                        return false;
                    };
                    menuList.removeClass('on');
                    menuList.eq(this.currPageX).addClass('on');
                }
            });
            menuList.bind('click', function() {
                myScroll.scrollToPage(menuList.index(this));
            });

            $(window).resize(function() {
                ReSize();
                myScroll.refresh();
            });


        }



        //$('#description').html(data.remark);

        $('#forpoint').html(data.Forpoints);

        this.refreshKeepState();
        this.showMore(data.remark);
    },
    refreshKeepState: function() {

        var keephtml = this.isKeep ? '☆<br>取消收藏' : '☆<br>收藏';

        $('#btn_keep').html(keephtml);
    },
    initPropHtml: function() {

        var data = this.data,
            itemlists = this.data.itemList;

        var tpl = $('#prop_item').html(),
            prophtml = '';

        var doms = $('[name=propitem]');

        for (var i = 1; i <= 5; i++) {

            var hashtpl = tpl;

            var propName = data['prop' + i + 'Name'],
                propList = null;

            if (propName) {

                propList = data['prop' + i + 'List'];

                $($(doms.get(i - 1)).find('dt')).html(propName);

                if (propList) {

                    this.buildSkuItem(i, propList);

                    if (i == 1) {

                        this.getSkuData(2, propList[0]['prop' + i + 'DetailId'], propList[0]['skuId']);
                    }
                }

            } else {

                doms.get(i - 1).style.display = 'none';
            }
        }
    },
    getSkuData: function(idx, propId, skuId) {

        var me = this;

        if (!me.data['prop' + idx + 'Name']) {

            me.skuId = skuId;

            for (var i = 0; i < me.data.skuList.length; i++) {

                if (me.data.skuList[i]['skuId'] == skuId) {

                    me.scalePrice = me.data.skuList[i].salesPrice;

                    $('[jitval=itemPrice]').html('会员价：￥' + me.data.skuList[i].salesPrice);

                    $('[jitval=oldPrice]').html('￥' + me.data.skuList[i].price);

                    return;

                }
            }

            $('[jitval=itemPrice]').html('');

            return;
        }


        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getSkuProp2List',
                'propDetailId': propId,
                'itemId': me.getUrlParam('goodsId')
            },
            success: function(data) {

                if (data.code == 200) {

                    for (var name in data.content) {

                        for (var i = 1; i <= 5; i++) {

                            if (('prop' + i + 'List') == name) {

                                me.buildSkuItem(i, data.content[name]);

                                me.getSkuData(i + 1, data.content[name][0]['prop' + i + 'DetailId'], data.content[name][0]['skuId']);

                                if (!me.data['prop' + (i + 1) + 'Name']) {

                                    return;
                                }
                            }
                        }
                    }
                }
            }
        });
    },
    buildSkuItem: function(idx, list) {

        var optionhtml = '';

        for (var p = 0; p < list.length; p++) {

            optionhtml += '<a class="' + ((p == 0) ? 'selected' : '') + '" skuid="' + list[p].skuId + '" prop_detail_id="' + list[p]['prop' + idx + 'DetailId'] + '" >' + list[p]['prop' + idx + 'DetailName'] + '<i></i></a>';
        }

        $($($('[name=propitem]').get(idx - 1)).find('dd')).html(optionhtml);
    },
    setItemKeep: function() {

        var me = this;

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setItemKeep',
                'itemId': me.getUrlParam('goodsId'),
                'keepStatus': (this.isKeep ? '0' : '1')
            },
            success: function(data) {

                if (data.code == 200) {

                    me.isKeep = !me.isKeep;

                    me.refreshKeepState();
                }
            }
        });
    },
    submitOrder: function() {

        var me = this;

        if (!me.skuId) {

            return;
        }

        var list = [{
            'skuId': me.skuId,
            'salesPrice': parseInt(me.scalePrice),
            'qty': parseInt($('#goods_number').val(), 10)
        }];

        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'qty': list[0].qty,
                'totalAmount': list[0].qty * list[0].salesPrice,
                'action': 'setOrderInfo',
                'orderDetailList': list
            },
            success: function(data) {

                if (data.code == 200) {

                    me.toPage('GoodsOrder', '&orderId=' + data.content.orderId);

                } else {

                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });

    },
    showMore: function(data) {
        if (data.length > 200) {
            $("#description").html(data.substring(0, 200));
        } else {
            $("#description").html(data);
        }
        var $btn = $("div.btn_unfold>a");
        $btn.click(function() {
            if ($(".btn_unfold a ").text() == "^") {
                $(".btn_unfold a ").text("ˇ");
                $("#description").html(data.substring(0, 200));
            } else {
                $(".btn_unfold a ").text("^");
                $("#description").html(data);
            }
            return false;
        });
    }
});