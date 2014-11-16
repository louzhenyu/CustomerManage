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
			data={"content":{"isLottery":1,"isWinning":1,"validTime":"120","winningValue":null,"lotteryDesc":null,"lotteryCount":null,"lotteryNumber":"0","prizeList":[{"prizesID":"0C1A4CEA57D84B01969FA260A90A5689","prizeName":"一等奖","prizeDesc":"IPAD Mini  3000/个","displayIndex":"1","countTotal":"3000","imageUrl":"http://o2oapi.aladingyidong.com/images/zoevent/1.jpg"},{"prizesID":"C4B45E4B13C14E6C9BF2EEA5F932FDAD","prizeName":"二等奖","prizeDesc":"100积分","displayIndex":"2","countTotal":"3000","imageUrl":"http://o2oapi.aladingyidong.com/images/zoevent/1.jpg"},{"prizesID":"20325A6805FB4052B9F870035A6E8096","prizeName":"三等奖","prizeDesc":"50积分","displayIndex":"3","countTotal":"3000","imageUrl":"http://o2oapi.aladingyidong.com/images/zoevent/1.jpg"}],"winnerList":null},"code":"200","description":"数据库操作错误","exception":null,"data":null};
                if (data.code == 200) {
					alert('sss');
					if(data.content.lotteryDesc == '恭喜您中奖了'){
						
						Jit.UI.Dialog({
							'type':'Dialog',
							'content':'您已经领取过您的新人惊喜，更多惊喜，请参与推荐有礼和天天有礼活动'
						});
					}
					
                    if (data.content.isLottery == "1") {
						
						var windata = data.content.prizeList[data.content.winningValue - 1];
						
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