Jit.AM.defindPage({

	name : 'LuckyEnvelope',
	
	initWithParam: function(param) {
        this.canAgain = true;//param.canAgain;
    },
    
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入'+this.name+'.....');
		
		this.ajaxSending = false;
		this.data = null;
		this.addressList = [];
		this.eventId = this.getUrlParam("eventId");
		if(!this.eventId){
			self.alert("eventId为空，未获取到活动信息");
			return false;
		}
		this.loadData();
		this.initEvent();
	},
	loadData:function(){
		//debugger;
		var me = this;

    	var hasOAuth = Jit.AM.getAppParam('Launch','CheckOAuth');

    	if(hasOAuth == 'unAttention'){
            var cfg = Jit.AM.getAppVersion();
			this.alert('参与本活动需要先关注微信公众号：'+cfg.APP_NAME);
            return false;
        }
		this.getEventPrizes();
	},
	initEvent : function() {
		var self = this;
		
		$("#section").delegate("#checkBtn", "click", function() {
			$(this).siblings("#tools").addClass("rotateState");
			setTimeout(function() {
				self.getPrize();
			}, 1000);
		}).delegate("#addressBtn", "click", function() {
			//Jit.AM.toPage("FCaddress");
			if (self.addressList.length == 0) {
				Jit.AM.toPage("FCaddress");
			} else {
				var idata = self.addressList[0];
				self.setParams('editAddressData', idata);
				self.toPage('FCaddress', '&type=edit&addressId=' + idata.vipAddressID + '&province=' + encodeURIComponent(idata.province) + '&city=' + encodeURIComponent(idata.cityName));
			}

		});
	},
	getPrize:function(){
		if (this.data.IsWinning == 1) {
			var imageUrl = this.data.WinnerList[0]?this.data.WinnerList[0].ImageUrl:"";
			this.loadPage("winningSec",{"ImageUrl":imageUrl});
			this.alert("恭喜您中了"+this.data.WinningDesc);
		} else {
			this.loadPage("loseSec");
		}
	},
	getEventPrizes:function(){
		var self = this;
		this.ajax({
			url : '/ApplicationInterface/Gateway.ashx',
			type:"get",
			data : {
				'action': 'Event.EventPrizes.GetEventPrizes',
	            'Longitude': '0.0',
	            'Latitude': '0.0',
	            'eventId': self.eventId,
	            'RecommandId': Jit.AM.getUrlParam('RecommandId') || ''
			},
			success : function(data) {
				if (data.IsSuccess) {
					self.data =_datas= data.Data.content;		//保存变量
					
					//debugger;
					
					//debugger;
					self.initIsShareEvent(_datas);
					//debugger;
					if(self.canAgain){
						self.loadPage("getPrizeSec");
					}else{
						if (data.Data.content.IsLottery == 1) {		//isLottery		是否可以抽奖  1 = 否，0 = 是
							if (data.Data.content.IsWinning == 1) {
								//debugger;
								var imageUrl = data.Data.content.WinnerList[0]?data.Data.content.WinnerList[0].ImageUrl:"";
								self.loadPage("winningSec",{"ImageUrl":imageUrl});
							} else {
								self.loadPage("loseSec");
							}
						} else {
							self.loadPage("getPrizeSec");
						}
					}
				}else{
					self.alert(data.Message);
				}
			}
		});
	},
	
	//初始化是否是推荐有礼类型活动抽奖
	initIsShareEvent:function(data){
		
		var self = this;
		
		self.IsShare = data.IsShare;
		
		var info = Jit.AM.getBaseAjaxParam();

		var url = location.href.split('?')[0];

		url = url + '?customerId='+Jit.AM.CUSTOMER_ID
			+ '&sender=' + info.userId
			+ '&eventId=' + self.eventId;
				
		var word = '大奖等你抢！';

        if(data.ShareRemark){

            word = data.ShareRemark;
        }
        
		Jit.WX.shareFriends("好友推荐",word,url,null);

		if(!data.IsShare){

			return ;
		}

		$('.sharePanel').show();

		$('.lottery_area').hide();

		$("#shareWrapper").html("<img class='img' src="+data.PosterImageUrl+" />");

		if(data.IsLottery == 1){
			//可抽奖
			$('#btn_join').html('立即参与').bind('click',function(){

				self.startEvent(data);
			});

		}else{
			
            $('#shareImg').show();
            
			$('#btn_join').html('查看奖品').bind('click',function(){

				self.showAward(data);
			});
		}
		$("#btn_check").bind('click',function(){
        	self.toPage('Record');
        });
	},
	startEvent:function(){

		$('.sharePanel').hide();

		$('.lottery_area').show();
	},
	showAward:function(){

		var self = this;

		//self.alert('你已抽奖，奖品为：'+self.data.WinningDesc+'<br>'+self.data.OverRemark);
		//self.alert('你已抽奖，奖品为：'+self.data.WinningDesc);
        Jit.UI.Dialog({
            'content': '你已抽奖，奖品为：'+self.data.WinningDesc,
            'type': 'Confirm',
            'LabelOk': '确定',
            'LabelCancel': '会员专享攻略',
            'CallBackOk': function() {
                Jit.UI.Dialog("CLOSE");
            },
            'CallBackCancel': function() {
				location.href="http://mp.weixin.qq.com/s?__biz=MjM5NTI0NjMyMQ==&mid=204527844&idx=1&sn=6059283611c5c6b1beefa23a749d80ad#rd";
            }
        });
	},
	loadPage : function(secName,obj) {
		$("#section").html(template.render(secName,obj)).show();
	},
	alert:function(text,callback){
    	Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
    }
}); 