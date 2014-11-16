Jit.AM.defindPage({

    name: 'GroupGoodsDetail',

    initWithParam: function(param) {

        if (param.commentHide) {

            $('#commentArea').hide();
        };

        if (param['gdeDisplay'] == false) {

            $('#ppTitle').hide();

            $('#ppBody').hide();
        }
    },

    onPageLoad: function() {
        var me = this;
        //当页面加载完成时触发

        me.goodsId = me.getUrlParam('goodsId') || me.getParams('goodsId');

        me.setParams('goodsId', me.goodsId);

        Jit.log('商品ID: ' + me.goodsId);

        me.ajax({
            url: '/Interface/data/ItemData.aspx',
            data: {
                'action': 'getPanicbuyingItemDetail',
                'itemId': me.goodsId,
				'eventId': me.getUrlParam('eventId')
            },
            success: function(data) {

                if (data.code == '200') {

                    me.loadGoodsDetail(data.content);

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

        $('#btn_delNum').bind('click', function() {

            var num = $('#goods_number').val();

            num--;

            if (num <= 1) {

                num = 1;
            }

            $('#goods_number').val(num);

            var pt = parseInt(me.data.Forpoints) * num;

            pt = (isNaN(pt) ? '0' : pt);

            $('#forpoint').html((isNaN(pt) ? '0' : pt));

            me.setForPonit(pt);
        });

        $('#btn_addNum').bind('click', function() {

            var num = $('#goods_number').val();

            num++;

            $('#goods_number').val(num);

            var pt = parseInt(me.data.Forpoints) * num;

            pt = (isNaN(pt) ? '0' : pt);

            $('#forpoint').html((isNaN(pt) ? '0' : pt));

            me.setForPonit(pt);
        });
    },
    SetSpareTime: function(_unixTime) {
        var element = $('#spareTime'),
            unixTime = parseInt(_unixTime)

        function Adjust() {
            var endTime = new Date(unixTime),
                seconds = 1000,
                min = 60 * seconds,
                hour = 60 * min,
                days = 24 * hour,
                now = new Date().getTime(),
                dateDiff = endTime - now,
                tipe;
            if (dateDiff <= 0)
                tipe = '0天0时0分0秒';
            else {
                var tDays = parseInt(dateDiff / days);
                var tHours = parseInt((dateDiff - (tDays * days)) / hour);
                var tMins = parseInt((dateDiff - (tDays * days + tHours * hour)) / min);
                var tSeconds = parseInt((dateDiff - (tDays * days + tHours * hour + tMins * min)) / seconds);
                tipe = tDays + '天' + tHours + '时' + tMins + '分' + tSeconds + '秒';
            }
            element.html(tipe);
        }

        setInterval(Adjust, 1000);
    },
    setForPonit: function(val) {

        if (val && val > 0) {

            $('#lab_forpoint').show();

            $('#forpoint').show().html(val);
        } else {

            $('#lab_forpoint').hide();

            $('#forpoint').html('').hide();
        }
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
        });

        // var heightKey='heightKey';
        // $('.gde_body').each(function(){
        //     var self=$(this);
        //     self.data(heightKey,self.height());
        // });

        $('.gde_title>a').bind('click', function() {

            var self = $(this),
                parentDiv = self.parent(),
                parentBody = parentDiv.next('.gde_body');
            if (self.hasClass('on')) {
                //隐藏
                self.removeClass('on');
                parentBody.hide();
                // parentBody.animate({'height':0},400);
            } else {
                //显示
                parentBody.show();
                // parentBody.animate({'height':curHeight},400);
                self.addClass('on');
            }

        });

        $('.g_share').bind('click', function() {
            $('#share-mask').show();
            $('#share-mask-img').show().attr('class', 'pullDownState');
        });

        $('#share-mask').bind('click', function() {
            var that = $(this);
            $('#share-mask-img').attr('class', 'pullUpState').show();
            setTimeout(function() {
                $('#share-mask-img').hide(500);
                that.hide(1000);
            }, 500);
        })

    },
    loadGoodsDetail: function(data) {

        this.data = data;
		
		var endtime = (new Date(data.endTime.replace(/-/gi,'/'))).getTime();
		
		this.SetSpareTime(endtime);
		
        this.isKeep = data.isKeep;

        this.initPageInfo();

        this.initPropHtml();

        this.initEvent();
    },
    addToGoodsCart: function() {

        var me = this;

        if (!me.checkOAuth()) {

            return;
        }

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setShoppingCart',
                'skuId': me.skuId,
                'qty': $('#goods_number').val()
            },
            success: function(data) {

                if (data.code == '200') {
                    TopMenuHandle.ReCartCount();

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
    initPageInfo: function() {

        var data = this.data;

        $('[jitval=itemName]').html(data.itemName);
        $('#txtRebate').html(data.discountRate/10);
        $('#txtBuyCount').html(data.salesPersonCount);
        // $('[jitval=itemImage]').attr('src', data.imageList[0].imageURL);
        if (data.imageList && data.imageList.length > 0) {
            var imgHtml = '',
                barHtml = '';
            for (var i = 0; i < data.imageList.length; i++) {
                imgHtml += "<li><img  src=\"" + Jit.UI.Image.getSize(data.imageList[i].imageUrl, '480') + "\" ></li>";
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
            var goodsScroll = $('#groupGoodsScroll');
            // menuList = $('#goodsImgBar li');

            //重新設置大小
            ReSize();

            function ReSize() {
                goodsScroll.find('.goods_img_list').css({
                    width: (goodsScroll.width()) * goodsScroll.find('.goods_img_list li').size()
                });

                goodsScroll.find('.goods_img_list li').css({
                    width: (goodsScroll.width())
                });
            }

            //綁定滾動事件
            myScroll = new iScroll('groupGoodsScroll', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                onScrollEnd: function() {
                    // if (this.currPageX > (menuList.size() - 1)) {
                    //     return false;
                    // };
                    // menuList.removeClass('on');
                    // menuList.eq(this.currPageX).addClass('on');
                }
            });
            // menuList.bind('click', function() {
            //     myScroll.scrollToPage(menuList.index(this));
            // });
            $(window).resize(function() {
                ReSize();
                myScroll.refresh();
            });
            goodsScroll.css('overflow', '');
        }

        this.setForPonit(parseInt(data.Forpoints));

        this.refreshKeepState();

        if (data.remark) {
            $("#description").html(data.remark);
        };

        var itemParaIntroduce = $('#itemParaIntroduce');
        if (data.itemParaIntroduce) {
            itemParaIntroduce.html(data.itemParaIntroduce);
        };
        if (data.itemIntroduce) {
            $('#itemIntroduce').html(data.itemIntroduce);

        };
        if (!data.itemParaIntroduce && !data.itemIntroduce) {
            itemParaIntroduce.html('无');
        };

    },
    refreshKeepState: function() {

        var btnKeep = $('#btn_keep');

        if (this.isKeep) {

            btnKeep.addClass('g_favon').removeClass('g_fav');

        } else {

            btnKeep.addClass('g_fav').removeClass('g_favon');
        }
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

                //如果此商品 prop1Name 都不存在说明 此商品无规格选择
                if (i == 1) {

                    this.getSkuData(1, '', data['prop1List'][0]['skuId']);
                }
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

                    $('[jitval=itemPrice]').html('<span>￥' + me.data.skuList[i].salesPrice + '元</span>');
					
                    if (me.data.skuList[i].price == 0) {
					
                        $('[jitval=oldPrice]').remove();
						
                    } else {
					
                        $('[jitval=oldPrice]').html(me.data.skuList[i].price+'元');
                    }

                    return;
                }
            }

            $('[jitval=itemPrice]').html('');

            return;
        }


        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getSkuProp2List',
                'propDetailId': propId,
                'itemId': me.goodsId
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

            optionhtml += '<a class="' + ((p == 0) ? 'selected' : '') + '" skuid="' + list[p].skuId + '" prop_detail_id="' + list[p]['prop' + idx + 'DetailId'] + '" >' + list[p]['prop' + idx + 'DetailName'] + '</a>';
        }

        $($($('[name=propitem]').get(idx - 1)).find('dd')).html(optionhtml);
    },
    setItemKeep: function() {
        //添加收藏
        var me = this;

        if (!me.checkOAuth()) {

            return;
        }

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setItemKeep',
                'itemId': me.goodsId,
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
        //提交订单
        var me = this;

        if (!me.checkOAuth()) {

            return;
        }

        var me = this;

        if (!me.skuId) {

            return;
        }

        var list = [{
            'skuId': me.skuId,
            'salesPrice': me.scalePrice,
            'qty': parseInt($('#goods_number').val(), 10)
        }];

        me.ajax({
            url: '/Interface/data/ItemData.aspx',
            data: {
                'qty': list[0].qty,
                'totalAmount': list[0].qty * list[0].salesPrice,
                'action': 'setOrderInfo',
                'orderDetailList': list,
				'isPanicbuying':1,
				'eventId':me.getUrlParam('eventId')
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

                TopMenuHandle.ReCartCount();
            }
        });

    },
    checkOAuth: function() {

        if (JitCfg.CheckOAuth == 'unAttention') {

            this.toPage('unAttention');

            return false;
        }

        return true;
    }
});