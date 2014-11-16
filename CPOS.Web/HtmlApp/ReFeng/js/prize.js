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
		
        image = {
            'back': { 'url': '../images/prize/tran1.png', 'img': null },
            'front': { 'url': '../images/prize/gjx.png', 'img': null }
        },prize='', isWinner = false, isLottery='';

        //初次加载用户信息
		
		var datas = {
			'action':'getEventPrizes',
			'Longitude': '0.0',
			'Latitude': '0.0',
			'eventId':(me.getUrlParam('eventId')||'E5A304D716D14CD2B96560EBD2B6A29C'),
			'recommender':localStorage.getItem('recommenderOpenId')
		}
        //加载中奖信息
        me.ajax({
            url: '/Lj/Interface/PrizesData.aspx',
            data: datas,
            success: function (data) {
			
                if (data.code == 200) {
					
					if(data.content.lotteryDesc == '恭喜您中奖了'){
						
						Jit.UI.Dialog({
							'type':'Dialog',
							'content':'您已经领取过您的新人惊喜，更多惊喜，请参与推荐有礼和天天有礼活动'
						});
					}
					
                    if (data.content.isLottery == "1") {
						
						var windata = data.content.prizeList[data.content.winningValue];
						
						$('#JPName').html(windata.prizeName);
						
						prize = windata.prizeName;
						
						loadcanvas();
						
					} else {
					
						$("#JPName").text(data.content.lotteryDesc);
						
						$("#asdi").hide();
					}
                    if (data.content.prizeList) {

                        var str = "", goodsList = $('#goodsList');
						
                        goodsList.empty();
						
                        for (var i = 0; i <data.content.prizeList.length; i++) {
						
                            str += GetGoodsItem(data.content.prizeList[i]);
                        }
                        goodsList.append(str);
                        
                    }

                }
            }
        });
		
        function GetGoodsItem(goodsInfo) {
            var str = "<dl><dt> <img src=\"" + goodsInfo.imageUrl 
					+ "\"></dt><dd><p><span>" + goodsInfo.prizeName 
					+ "</span>(" + goodsInfo.countTotal + "份) </p><p>" 
					+ "</p> &nbsp;<p>" + goodsInfo.prizeDesc + "</p>   </dd> </dl>";

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
	saveLottery:function(){
		var datas = {
			'action':'setEventPrizes',
			"Longitude": "0.0",
			"Latitude": "0.0",
			'eventId':(me.getUrlParam('eventId')||'E5A304D716D14CD2B96560EBD2B6A29C')
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