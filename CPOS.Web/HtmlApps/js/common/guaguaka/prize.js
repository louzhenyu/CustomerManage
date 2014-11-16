Jit.AM.defindPage({
    eventId:'',
    name: 'NewUser',
    onPageLoad: function () {
        //CheckSign();
        //当页面加载完成时触发
        Jit.log('进入NewUser.....');
		
        this.eventId = this.getUrlParam('eventId')||'C17DA7B41FF54383BD28B047A6773EF2';

        this.initData();
    },
    initData: function () {
	
		var me = this;
		
        image = {
            'back': { 'url': '../../../images/special/lj/tran1.png', 'img': null },
            'front': { 'url': '../../../images/special/lj/gjx.png', 'img': null }
        },prize='', isWinner = false, isLottery='';

        //初次加载用户信息
		
		var datas = {
			'action':'getEventPrizes',
			'Longitude': '0.0',
			'Latitude': '0.0',
			'eventId':me.eventId,
		}
        //加载中奖信息
        me.ajax({
            url: '/lj/Interface/data.aspx',
            data: datas,
            success: function (data) {
				
                if (data.code == 200) {
					
					me.data = data.content;
					
					isLottery = (data.content.isWinning == '0'?true:false);
					
                    if (data.content.isLottery == "1") {
						
						prize = data.content.winningDesc;
						
						loadcanvas();
						
						$('#JPName').html(data.content.winningDesc);
						
					} else {
					
						$("#JPName").text(data.content.winningDesc);
						
						$("#asdi").hide();
					}
                    if (data.content.prizes) {

                        var str = "", goodsList = $('#goodsList'),prizesList = data.content.prizes;
						
						prizesList = prizesList.sort(function(a,b){
							
							if(a.displayIndex>b.displayIndex){
								return 1;
							}else{
								return -1;
							}
						});
						
                        goodsList.empty();
						
                        for (var i = 0; i <prizesList.length; i++) {
						
                            str += GetGoodsItem(prizesList[i]);
                        }
						
                        goodsList.append(str);
                    }

                }else{
					
					Jit.UI.Dialog({
						'content':data.description,
						'type':'Alert',
						'CallBackOk':function(){
							Jit.UI.Dialog('CLOSE');
						}
					});
				}
            }
        });
		
        function GetGoodsItem(goodsInfo) {
			
			var img = (goodsInfo.imageUrl || '/HtmlApps/images/common/misspic.png');
		
            var str = "<dl><dt> <img src=\"" + img
					+ "\"></dt><dd><p><span>" + goodsInfo.prizeName 
					+ "</span>(" + goodsInfo.countTotal + "份) </p><p>" 
					+ goodsInfo.prizeDesc + "</p>   </dd> </dl>";

            return str;
        }
    },
	Winner: function () {
		//点击刮奖区时触发
    },
	saveLottery:function(){
		//脱离刮奖区时触发
        var me=this;
		
		if(isLottery){
			//再来一次
			$("#AgianEI").show();
		}else{
			//已中奖
			
			Jit.UI.Dialog({
				'content':'恭喜你获得了：'+me.winningDesc,
				'type':'Alert',
				'CallBackOk':function(){
					Jit.UI.Dialog('CLOSE');
				}
			});
			
		}
	}
});