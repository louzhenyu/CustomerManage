Jit.AM.defindPage({
    name: 'MyInfoPage',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发
        Jit.log('进入MyInfoPage.....');

        this.initEvent();

        this.initData();
    },
    initData: function () {
        image = {
            'back': { 'url': '../images/tran1.png', 'img': null },
            'front': { 'url': '../images/gjx.png', 'img': null }
        },prize='', isWinner = false, isLottery='';

        //初次加载用户信息
        var datas = { "Longitude": "0.0", "Latitude": "0.0" }, me = this;

        //加载中奖信息
        me.ajax({
            url: '/lj/Interface/data.aspx?Action=getEventPrizes',
            data: datas,
            success: function (data) {
                if (data.code == 200) {
                    if (data.content.eventRound != "0") {
                        isLottery = data.content.isLottery;
                        if (data.content.isLottery == "1") {
                            $("#JPName").text(o.content.winningDesc);
                            $("#asdi").hide();
                        } else {
                            prize = data.content.winningDesc;
                            loadcanvas()
                        }
                    } else {
                        $(".prize_inner").css({ "background-image": "url(../images/ggcardmkf.png)" });
                    }
                    if (data.content.prizes) {

                        var str = "", goodsList = $('#goodsList');
                        goodsList.empty();
                        for (var i = 0; i <data.content.prizes.length; i++) {
                            str += GetGoodsItem(data.content.prizes[i]);
                        }
                        goodsList.append(str);
                        
                    }

                }
            }
        });


        function GetGoodsItem(goodsInfo) {
            var str = "<dl><dt> <img src=\"" + goodsInfo.imageUrl + "\"></dt><dd><p><span>" + goodsInfo.prizeName + "</span>(" + goodsInfo.countTotal + "份) </p><p>" + goodsInfo.sponsor + "</p><p>" + goodsInfo.prizeDesc + "</p>   </dd> </dl>";

            return str;
        }



    },
    initEvent: function () {



    }
});

var ggj = {
    Winner: function () {
        var datas = { "Longitude": "0.0", "Latitude": "0.0" }, me = Jit.AM;
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
    }


};