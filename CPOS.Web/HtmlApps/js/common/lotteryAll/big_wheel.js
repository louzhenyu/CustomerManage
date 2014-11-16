Jit.AM.defindPage({
	name : 'BigWheel',
	elements : {
		lotteryCount : $('#lotteryCount'),
		lotteryExplain : $('#lotteryExplain'),
		rotatePane : $('.rotate_img'),
		rotateStart :$('.rotate_start') 
	},
	initWithParam: function(param) {

		if(param.prizeList && typeof param.prizeList == 'string'){

			this.prizeList = eval('('+param.prizeList+')');
		}else{
			this.prizeList = param.prizeList;	//角度列表
		}
		
		this.prizeCount = parseInt(param.prizeCount,10);	
		$("#prizeImage").attr("src",param.prizeImage);
		
        this.canAgain = false;//param.canAgain;
    },
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入' + this.name + '....');
		
		this.eventId = this.getUrlParam("eventId");
		this.sender = this.getUrlParam("sender");

		if(!this.eventId){
			self.alert("eventId为空，未获取到活动信息");
			return false;
		}
		
		this.canPlay = true;
		this.isRotating = false;
		this.prizePx ='';		// 奖品角度 根据返回值从prizeList中取一项
		this.result = ""; 		//抽奖结果信息
		
		
		this.loadData();
		this.initEvent();
	},
	initEvent : function() {
		var self = this;
		//this.BindLotteryEvent();
	},
	loadData:function(){

		var me = this;

    	var hasOAuth = Jit.AM.getAppParam('Launch','CheckOAuth');

    	if(hasOAuth == 'unAttention'){
            var cfg = Jit.AM.getAppVersion();
			this.alert('参与本活动需要先关注微信公众号：'+cfg.APP_NAME);
            return false;
        }

		this.getEventPrizes();
	},
	//获取 转动角度
	GetPrizePx : function(px) {
		var position = 0;
		for (var i = 0; i < this.prizeList.length; i++) {
			var prizeItem = this.prizeList[i];
			var indexList = prizeItem.indexList.split(",");
			if (prizeItem.item == px) {
				//随机取出一个角度
				if (indexList.length > 1) {
					position = indexList[parseInt(Math.random() * indexList.length)];
				} else {
					position = indexList[0];
				}
				break;
			};
		};
		return position;
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
	            'RecommandId': Jit.AM.getUrlParam('sender') || ''
			},
			success : function(data) {
				if (data.IsSuccess && data.Data) {
					
					var _datas = self.data= data.Data.content;
					
					self.needLogin = data.Data.SignFlag?false:true;
                    if(self.needLogin){

                        Jit.UI.Dialog({
                            'content': '您还不是会员，请先注册成为会员！',
                            'type': 'Confirm',
                            'LabelOk': '注册',
                            'LabelCancel': '不抽奖了',
                            'CallBackOk': function() {
								register.init();

                                Jit.UI.Dialog("CLOSE");
                            },
                            'CallBackCancel': function(){

                                Jit.UI.Dialog("CLOSE");
                            }
                        });

                        return;
                    }
                    
					self.initIsShareEvent(_datas);

					self.initPrizeParam(data.Data.content);

                    self.buildPrizeList(data.Data.content.PrizesList);
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
        
		Jit.WX.shareFriends("好友推荐",word,url,null,true);  //true设置需要高级auth认证

		if(!data.IsShare){
			// 如果不可分享   显示出转盘，隐藏分享层
			self.startEvent(data);
			return ;
		}

		$('.sharePanel').show();

		$('.lottery_area').hide();

		$("#shareWrapper").html("<img class='img' src="+data.PosterImageUrl+" />");

		if(data.IsLottery == 0){
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
	buildPrizeList: function(plist) {

        var tpl = $('#tpl_PrizeItem').html(),
            html = '';

        for (var i = 0; i < plist.length; i++) {

            var itemdata = plist[i];

            html += Mustache.render(tpl, itemdata);
        }

        $('#goodsList').html(html);
    },
	//初始化抽奖参数
	initPrizeParam : function(data) {
		var self = this, o = data;
		this.result = o;
		if(self.canAgain){
			if (o.IsWinning == 1) {
				self.canPlay = true;
				self.elements.lotteryCount.html(1);
				self.elements.lotteryExplain.html('');
				if (o.PrizeIndex) {
					if (o.PrizeIndex == 1) {//一等奖
						o.prizeIndex = 1;
					} else if (o.PrizeIndex == 2) {//二等奖
						o.prizeIndex = 2;
					} else if (o.PrizeIndex == 3) {//三等奖
						o.prizeIndex = 3;
					} else {
						o.prizeIndex = 4;
					}
				} else {
					o.prizeIndex = 4;
				}
			} else {
				o.prizeIndex = 4;
			}
			self.prizePx = self.GetPrizePx(o.prizeIndex);
	
			self.BindLotteryEvent();
		}else{
			if(o.IsLottery==0){	//不要问为什么，接口这么定义的
				if (o.IsWinning == 1) {
					self.canPlay = true;
					self.elements.lotteryCount.html(1);
					self.elements.lotteryExplain.html('');
					if (o.PrizeIndex) {
						if (o.PrizeIndex == 1) {//一等奖
							o.prizeIndex = 1;
						} else if (o.PrizeIndex == 2) {//二等奖
							o.prizeIndex = 2;
						} else if (o.PrizeIndex == 3) {//三等奖
							o.prizeIndex = 3;
						} else {
							o.prizeIndex = 4;
						}
					} else {
						o.prizeIndex = 4;
					}
				} else {
					o.prizeIndex = 4;
				}
				self.prizeIndex = o.prizeIndex;
				self.prizePx = self.GetPrizePx(o.prizeIndex);
		
				self.BindLotteryEvent();
			
			}else{
				self.canPlay = false;
				self.elements.lotteryCount.html(0);
				self.elements.lotteryExplain.html('每天只有一次摇奖机会哦，请明天再试');
				self.elements.rotateStart.bind('click', function() {
					self.alert('每天只有一次摇奖机会哦，请明天再试');
				});
				
			}
		}
		
		
	}, //绑定抽奖事件
	BindLotteryEvent : function() {
		var that = this;
		var rotateHandle = new RotateHandle({
			rgTag : that.elements.rotatePane, //操作图层
			reTag : that.elements.rotateStart, //转动按钮
			prizeCount : that.prizeCount, //奖项数量
			px : that.prizePx,
			prizeList : that.prizes, //奖项正确地址,以指心计算 如果不指定默认会根据奖项数量计算
			callback : function(px) {//摇奖完成后执行的回调函数(px=回调奖项)
				that.RotateCallBack(px);
			}
		});
		that.elements.rotateStart.bind('click', function() {
			if (that.isRotating || !that.checkAccess()) {
				return;
			}
			that.isRotating = true;
			rotateHandle.exeRotate();
		});
		//抽奖回调函数
	},
	RotateCallBack : function() {
		if(this.prizeIndex!=4){
			this.alert("恭喜您中了"+this.result.WinningDesc);
		}else{
			this.alert("很遗憾您没有中奖");
		}
		this.isRotating = false;

		this.canPlay = false;
	},
	checkAccess : function() {
		if (!this.canPlay) {
			this.elements.lotteryCount.html('0');

			var msg = '每天只有一次摇奖机会哦，请明天再试';

			if(this.IsShare){

				msg += '<br>'+this.data.ShareRemark;
			}

			this.alert(msg);

			return false;
		}
		return true;
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

//转动操作 tagId=标签名称,rotateTime=转动时间
function RotateHandle(setings) {
	var _setings = {
		rgTag : '', //操作图层
		reTag : '', //转动按钮
		exeTime : '', //转动时间毫秒
		prizeCount : '', //奖项数量
		prizeList : [], //奖项正确地址 如果不指定默认会根据奖项数量计算
		callback : '',
		px : '' //奖项
	}, changeNum = 60, //每次增加60变换一次速度
	rotatePane, rotateStart, _time, degcount = 0, deg = 0, flat = 0.6, maxDegCount = -1, //可旋转的最大次数
	stopTime = 0, cssText;
	$.extend(_setings, setings);

	if (!_setings.reTag || !_setings.rgTag) {
		return false;
	};

	//转动
	function StartRotate() {
		var identity = getIdentity();
		deg = degcount += identity;
		cssText = "-webkit-transform: rotate(" + deg + "deg);transform: rotate(" + deg + "deg);";
		//console.log(degcount+"|----|"+maxDegCount);
		if (degcount >= maxDegCount) {
			StopRotate();
			rotatePane.get(0).style.cssText = cssText;
			if (_setings.callback) {
				_setings.callback();
			};
			return false;
		};
		
		rotatePane.get(0).style.cssText = cssText;
		stopTime++;
	}

	//停止转动
	function StopRotate() {
		//maxDegCount = -1;
		degcount = degcount%360;
		stopTime = 0;
		clearInterval(_time);
	}

	//执行转动
	function ExeRotate() {
		if (stopTime > 0) {//如果有转盘还在转动不做任何操作
			return false;
		};

		_time = setInterval(StartRotate, 1);
	}

	//获取每次转动的大小
	function getIdentity() {
		var count = maxDegCount / changeNum;
		//要更新速度次数
		var alreadycount = degcount / changeNum;
		//已更新速度次数
		var identity = count / 2 - Math.abs(alreadycount - count / 2);
		identity = identity <= 1 ? 1 : identity;
		// return identity * flat;
		return identity * flat;
	}

	function SetPrizeIndex(px) {
		maxDegCount = 10 * 360 + parseInt(px);
	}

	//初始化
	rotatePane = $(_setings.rgTag), rotateStart = $(_setings.reTag);
	if (_setings.px >= 0) {
		SetPrizeIndex(_setings.px);
	};
	this.go = function(px) {
		SetPrizeIndex(px);
	};
	this.exeRotate = function() {
		ExeRotate();
	};
}