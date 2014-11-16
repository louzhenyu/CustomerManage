Jit.AM.defindPage({
	name: 'Lottery',
	eventId: '',
	InfaceUrl: '/lj/Interface/data.aspx',
	canPlay: false,
	isRotating: false,
	elements: {
		lotteryCount: '',
		lotteryExplain: '',
		rotatePane: '',
		rotateStart: ''
	},
	prizePx: '', //中奖地址
	prizeList: [{
		item: 1,
		indexList: [63]
	}, {
		item: 2,
		indexList: [334]
	}, {
		item: 3,
		indexList: [156, 224]
	}, {
		item: 4,
		indexList: [20, 113, 201, 290]
	}],
	initWithParam:function(param){
		
		if(param.prizeImage){
			
			$('.rotate_img').attr('src',param.prizeImage);
		}
		
		if(param.prizeList){
			
			this.prizeList = param.prizeList;
		}
		
		this.prizeCount = param.prizeCount || 8;
	},
	onPageLoad: function() {
		//当页面加载完成时触发
		var me = this;
		
		me.initEvent();
		
		me.openShareFunction(['eventId'],function(userId){
			
			if(!userId){
			
				return;
			}
			
			alert('该页面是由 userId 为'+userId+'的用户推荐给你的！');
		});
	},
	
	GetPrizePx: function(px) {
		var position = 0;
		for (var i = 0; i < this.prizeList.length; i++) {
			var prizeItem = this.prizeList[i];
			if (prizeItem.item == px) {

				if (prizeItem.indexList.length > 1) {
					position = prizeItem.indexList[parseInt(Math.random() * prizeItem.indexList.length)];
				} else {
					position = prizeItem.indexList[0];
				}

				break;
			};
		};
		return position;
	},
	result: "", //抽奖结果信息
	initEvent: function() {
		var me = this;
		me.LoadLotteryInfo();
	}, //加载抽奖信息
	LoadLotteryInfo: function() {
		var me = this;
		me.eventId = me.getUrlParam('eventId');
		me.elements.lotteryCount = $('#lotteryCount');
		me.elements.lotteryExplain = $('#lotteryExplain');
		me.elements.rotatePane = $('.rotate_img');
		me.elements.rotateStart = $('.rotate_start');
		me.ajax({
			url: me.InfaceUrl,
			data: {
				'action': 'getEventPrizes',
				"Longitude": "0.0",
				"Latitude": "0.0",
				eventId: me.eventId
			},
			success: function(data) {
				
				if (data.code == 200) {
					
					me.result = data.content;
					
					me.initPrizeParam(true);
				} else {

					Jit.UI.Dialog({
						'content': data.description,
						'type': 'Alert',
						'CallBackOk': function() {}
					});

				}
			}

		});

	}, //初始化抽奖参数
	initPrizeParam: function(isFirst) {
		var self = this,
			o = self.result;
		var p = o.prizes;
		
		if(o.isLottery == '1'){
			
			self.canPlay = true;
			
			self.elements.lotteryCount.html(1);
			
		}else{
			
			self.canPlay = false;
			
			self.elements.lotteryCount.html(0);
			//self.elements.lotteryExplain.html('每天只有一次摇奖机会哦，请明天再试，或参与“推荐有礼”领取更多惊喜');
			self.elements.rotateStart.bind('click', function() {
				Jit.UI.Dialog({
					'content': '已经没有抽奖机会了！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			});
		}
		
		if (o.isWinning == "1") {
			
			self.elements.lotteryCount.html(1);
			
			self.elements.lotteryExplain.html('');
			
			self.prizePx = self.GetPrizePx(o.prizeIndex||self.prizeList.length);
		}
		if(isFirst){
			
			self.BindLotteryEvent();
		}
		
	}, //绑定抽奖事件
	BindLotteryEvent: function() {
		
		var that = this;
		
		var rotateHandle = new RotateHandle({
			rgTag: that.elements.rotatePane, //操作图层
			reTag: that.elements.rotateStart, //转动按钮
			prizeCount: that.prizeCount, //奖项数量
			px: that.prizePx,
			prizeList: that.prizes, //奖项正确地址,以指心计算 如果不指定默认会根据奖项数量计算
			callback: function(px) { //摇奖完成后执行的回调函数(px=回调奖项)
				that.RotateCallBack(px);
			}
		});
		that.elements.rotateStart.bind('click', function() {
			
			if (that.isRotating || !that.checkAccess()) {
				return;
			}
			that.canPlay = false;
			that.isRotating = true;
			rotateHandle.go(that.prizePx);
			rotateHandle.exeRotate();
		});
		//抽奖回调函数
	},
	RotateCallBack: function() {

		var that = this;
		
		Jit.UI.Dialog({
			'content': that.result.winningDesc,
			'type': 'Alert',
			'CallBackOk': function() {
				Jit.UI.Dialog('CLOSE');
			}
		});
		that.isRotating = false;
		that.ajax({
			url: that.InfaceUrl,
			data: {
				action: "getEventPrizes",
				eventId: that.eventId
			},
			success: function(data) {
				/*
				if (data.content && data.content.isLottery == "1") {
					that.canPlay = true;
					that.elements.lotteryExplain.html('您还可以抽奖1次');
				} else {
					that.elements.lotteryExplain.html('没有抽奖机会了哦，请明天再试');
				}
				*/
				if (data.code == 200) {
					
					that.result = data.content;
					
					that.initPrizeParam(false);
				}
			}

		});

	},
	checkAccess: function() {
		var that = this;
		if (!that.canPlay) {
			that.elements.lotteryCount.html('0');
			Jit.UI.Dialog({
				'content': '每天只有一次摇奖机会哦，请明天再试',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
			return false;
		}
		return true;
	},
	saveLottery: function() {
		
	}
});


//转动操作 tagId=标签名称,rotateTime=转动时间
function RotateHandle(setings) {
	var _setings = {
		rgTag: '', //操作图层
		reTag: '', //转动按钮
		exeTime: '', //转动时间毫秒
		prizeCount: '', //奖项数量
		prizeList: [], //奖项正确地址 如果不指定默认会根据奖项数量计算
		callback: '',
		px: '' //奖项
	},
		changeNum = 60, //每次增加60变换一次速度
		rotatePane,
		rotateStart,
		_time,
		degcount = 0,
		deg = 0,
		flat = 0.6,
		maxDegCount = -1, //可旋转的最大次数
		stopTime = 0,
		cssText;
	$.extend(_setings, setings);

	if (!_setings.reTag || !_setings.rgTag) {
		return false;
	};


	//转动
	function StartRotate() {
		var identity = getIdentity();
		deg = degcount += identity;
		cssText = "-webkit-transform: rotate(" + deg + "deg);-moz-transform: rotate(" + deg + "deg);"
		// cssText += "-moz-transform: rotate(" + deg + "deg);"
		// cssText += " -o-transform: rotate(" + deg + "deg);"

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
		maxDegCount = -1;
		stopTime = 0;
		clearInterval(_time);
	}

	//执行转动
	function ExeRotate() {
		if (stopTime > 0) { //如果有转盘还在转动不做任何操作	
			return false;
		};
		_time = setInterval(StartRotate, 1);
	}

	//获取每次转动的大小
	var getIdentity = function() {
		var count = maxDegCount / changeNum //要更新速度次数
		var alreadycount = degcount / changeNum //已更新速度次数
		var identity = count / 2 - Math.abs(alreadycount - count / 2)
		identity = identity <= 1 ? 1 : identity;
		// return identity * flat;
		return identity * flat;
	}

		function SetPrizeIndex(px) {
			maxDegCount = 20 * 360 + px;
		}

    //初始化
	rotatePane = $(_setings.rgTag),
	rotateStart = $(_setings.reTag);
	if (_setings.px >= 0) {
		SetPrizeIndex(_setings.px);
	};
	this.go = function(px) {
		degcount=0;
		SetPrizeIndex(px);
	};
	this.exeRotate = function() {
		ExeRotate();
	}
}