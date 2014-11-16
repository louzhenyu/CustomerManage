Jit.AM.defindPage({

    name: 'GoodsList',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入GoodsList.....');

        this.initEvent();
    },

    initEvent: function () {

        var me = this;

        /*
        (function(){
        var win = {
        sH : window.innerHeight,
        sW : window.innerWidth
        }
			
        $(window).resize(function(){
			
        if((win.sH-window.innerHeight > 100) && (win.sW == window.innerWidth)){
					
        //$('#nav').hide();
        }else{
				
        //$('#nav').show();
        }
        });
        })()
        */
        me.windowHeight = window.innerHeight;

        me.windowWidth = window.innerWidth;


        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemList',
                'isExchange': 0
            },
            success: function (data) {

                me.loadPageData(data.content);
            }
        });

        $('#btn_search').bind('click', function () {

            me.ajax({

                url: '../../../OnlineShopping/data/Data.aspx',

                data: {
                    'action': 'getItemList',
                    'itemName': $('#ipt_search').val()
                },
                success: function (data) {

                    $('[tplpanel=goods_list]').html('');

                    me.loadPageData(data.content);
                }
            });
        });

    },
    loadPageData: function (data) {

        var itemlists = data.itemList;

        var tpl = $('#Tpl_goods_list').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {

            var hashtpl = tpl;

            var itemdata = itemlists[i];

            if (itemdata.price > 0) {

                itemdata.oldprice = "￥" + itemdata.price;

            } else {

                itemdata.oldprice = '';
            }

            html += Mustache.render(tpl, itemdata);
        }

        $('[tplpanel=goods_list]').html(html);
    }
});