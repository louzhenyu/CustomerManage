Jit.AM.defindPage({
    name: 'NewUser',
    onPageLoad: function () {
        //CheckSign();
        //当页面加载完成时触发
        Jit.log('进入NewUser.....');

        this.initEvent();

        this.initData();
    },
    initData: function () {
        var me = this;

        //延时隐藏微信菜单。
        var weixinTime = '', exeCount = 0;
        function WeixinHide() {
            if (typeof WeixinJSBridge != 'undefined') {
                // 隐藏右上角的选项菜单入口;  
                WeixinJSBridge.call('hideOptionMenu');
            };
        }
        weixinTime = window.setInterval(function () {
            WeixinHide();
            exeCount++;
            if (exeCount > 20) {
                window.clearInterval(weixinTime);
            };
        }, 100);

        Jit.UI.Masklayer.show();
        if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().customerId == null) {//如果userId不为空，则表示缓存已有基础数据，如果无，则需要给值
            if (me.getUrlParam('customerId') != null && me.getUrlParam('customerId') != "") {
                Jit.AM.setBaseAjaxParam({ "customerId": me.getUrlParam('customerId'), "userId": "", "openId": "" });
            }
        }

        image = {
            'back': { 'url': '/HtmlApps/images/special/hs/prize/tran1.png', 'img': null },
            'front': { 'url': '/HtmlApps/images/special/hs/prize/gjx.png', 'img': null }
        }, prize = '', isWinner = false, isLottery = '';

        //初次加载用户信息
        /*
        正式：9b3755b32dbeef75fdeaea09973e180a
        测试：23ACF90F91E441DDA3E8242D77BAEB5E
        */
        var datas = {
            'action': 'getEventPrizes2',
            'Longitude': '0.0',
            'Latitude': '0.0',
            'eventId': (me.getUrlParam('eventId') || "23ACF90F91E441DDA3E8242D77BAEB5E"),
            'recommender': localStorage.getItem('recommenderOpenId')
        }
        //加载中奖信息
        me.ajax({
            url: '/Lj/Interface/Data.aspx',
            data: datas,
            success: function (data) {

                if (data.code == 200) {
                    if (data.content.prizes) {

                        var str = "", goodsList = $('#goodsList');

                        goodsList.empty();

                        for (var i = 0; i < data.content.prizes.length; i++) {
                            str += GetGoodsItem(data.content.prizes[i]);
                        }
                        goodsList.append(str);
                    }

                    Jit.UI.Masklayer.hide();
                    var params = Jit.AM.getBaseAjaxParam();
                    if (params == null || params.userId == null) {
                        debugger;
                        loadcanvas();
                        Jit.UI.Dialog({
                            'content': "请先完善资料",
                            'type': 'Alert',
                            'CallBackOk': function () {
                                me.toPage('Perfect',"&backpage=Prize");
                            }
                        });

                        return;
                    }


                    if (data.content.isLottery == "0") {//未抽奖

                        //                        if (data.content.isWinning)
                        //                        {
                        //                        var windata = data.content.prizes[data.content.winningValue - 1];

                        $('#JPName').html(data.content.winningDesc);

                        prize = data.content.winningDesc;
                        //                        }
                        loadcanvas();

                    } else {//已经抽奖

                        $('#JPName').html("<font style='color:black'>" + data.content.winningDesc + "</font>");

                        //$("#asdi").hide();
                    }



                }
            }
        });

        function GetGoodsItem(goodsInfo) {
            //+ "</span>(" + goodsInfo.countTotal + "份) </p><p>"

//            var str = "<dl><dt> <img src=\"" + goodsInfo.imageUrl
//					+ "\"></dt><dd><p><span>" + goodsInfo.prizeName
//					+ "</span></p><p>"
//					+ "</p> &nbsp;<p>" + goodsInfo.prizeDesc + "</p>   </dd> </dl>";

            var str = "<dl><dt> <img src=\"" + goodsInfo.imageUrl
            					+ "\"><em>"+goodsInfo.prizeName+"</em></dt><dd><p><span>" + goodsInfo.sponsor
            					+ "</span></p><p>"
            					+ "</p> &nbsp;<p></p>   </dd> </dl>";

            return str;
        }
    },
    Winner: function () {
        var datas = { "Longitude": "0.0", "Latitude": "0.0" }, me = this;
        me.ajax({
            url: '/lj/Interface/data.aspx?Action=getEventPrizes',
            data: datas,
            success: function (o) {
                if (o.code == "200") {
                    if (isLottery == "0") {
                        $("#AgianEI").show();
                    }
                }
            }
        })
    },
    saveLottery: function () {

        var datas = {
            'action': 'setEventPrizes',
            "Longitude": "0.0",
            "Latitude": "0.0",
            'eventId': (me.getUrlParam('eventId') || '23ACF90F91E441DDA3E8242D77BAEB5E')
        }

        //加载中奖信息
        me.ajax({
            url: '/Lj/Interface/PrizesData.aspx',
            data: datas,
            success: function (data) {
                alert('saveLottery');
            }
        })
    },
    initEvent: function () {



    }
});

var ggj = {
    


};