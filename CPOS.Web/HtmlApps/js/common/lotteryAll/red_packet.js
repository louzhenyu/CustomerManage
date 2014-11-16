Jit.AM.defindPage({

	name : 'RedPacket',
	
	initWithParam: function(param) {
	    
	},
    AlertMsg:{
        '1':{'msgTit':'谢谢您','msgSubTit':'活动还未开始，敬请期待','prize':'&nbsp;'},
        '2':{'msgTit':'很遗憾','msgSubTit':'您没有抽奖机会了','prize':'&nbsp;'},
        '4':{'msgTit':'很可惜','msgSubTit':'今天的红包被抢光了','prize':'&nbsp;'},
        '25':{'msgTit':'很遗憾','msgSubTit':'活动已经结束','prize':'&nbsp;'}
    },
    showAwardMsg:function(status){

        var me = this, msg = this.AlertMsg[status];
        console.log(msg);
        for(var key in msg){

            $('#'+key).html(msg[key]);
        }
    },
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入'+this.name+'.....');
		
		this.ajaxSending = false;
		
		this.data = null;
		
		this.eventId = this.getUrlParam("eventId");
	   
        JitPage.setHashParam('EventId',this.eventId);

		$('#winheight').css({'height':$(window).height()-152+'px'});

		if(!this.eventId){

			self.alert("eventId为空，未获取到活动信息");

			return false;
		}

		this.loadData();
        //获得初始化数据
        this.getInitData();
	},
    //获得初始化数据
    getInitData:function(){
        var me=this;
        //初次加载用户信息
        var datas = {
            'action': 'Event.EventPrizes.GetPrizeEvent',
            'Longitude': '0.0',
            'Latitude': '0.0',
            'eventId': me.eventId,
            'RecommandId': Jit.AM.getUrlParam('sender') || ''  //推荐人的id
        };
        //加载中奖信息
        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            'async': false,   //设置为同步请求
            data: datas,
            success: function(data) {

                if (data.IsSuccess && data.Data) {

                    var _data = data.Data;

                    //设置关注引导页 地址
                    JitPage.setHashParam('ContactUrl',data.Data.BootUrl);


                    var info = Jit.AM.getBaseAjaxParam(),
                        shareUrl = location.href.replace('userId='+info.userId,''),
                        shareUrl = shareUrl.replace('openId='+info.openId);

                    var shareInfo = {
                        'title':(data.Data.ShareRemark||'好友推荐'),
                        'desc':(data.Data.OverRemark||'大奖等你抢！'),
                        'link':shareUrl,
                        'imgUrl':(data.Data.ShareLogoUrl||JitCfg.shareIco)
                    }

                    me.data = _data;

                    //debugger用
                    //_data.content.IsLottery = 0;
                    //_data.IsShare = 1;

                    $('#hb_bg').css({

                        'backgroundImage':'url('+_data.PosterImageUrl+')'
                    });
                    _data.BootUrl=_data.BootUrl?_data.BootUrl:"";
                    _data.BootUrl = ( (_data.BootUrl.indexOf('http://')==-1)?('http://'+_data.BootUrl):_data.BootUrl );

                    $('[name=useDes]').attr('href',_data.BootUrl);
                    me.initIsShareEvent(shareInfo);

                }else {

                    me.alert(data.Message);
                }
            }
        });

    },
	loadData:function(){
		
		var me = this;

    	var hasOAuth = Jit.AM.getAppParam('Launch','CheckOAuth');

    	if(hasOAuth == 'unAttention'){
            var cfg = Jit.AM.getAppVersion();
			this.alert('参与本活动需要先关注微信公众号：'+cfg.APP_NAME);
            return false;
        }
        if(JitPage.getHashParam('UsePointsLottery') == '1'){

            this.showAward();

            JitPage.setHashParam('UsePointsLottery','0');
        }
	},
    //加载中奖信息
    loadLotteryData:function(){
        var me=this;
        if(!!!me.lotteryData){
            //初次加载用户信息
            var datas = {
                'action': 'Event.EventPrizes.GetEventPrizes',
                'Longitude': '0.0',
                'Latitude': '0.0',
                'eventId': me.eventId,
                'RecommandId': Jit.AM.getUrlParam('sender') || ''
            };
            //表示可用继续使用积分抽奖
            if(JitPage.getHashParam('UsePointsLottery') == '1'){
                datas['PointsLotteryFlag'] = 1;
            }
            
            //加载中奖信息
            me.ajax({
                url: '/ApplicationInterface/Gateway.ashx',
                'async': false,   //设置为同步请求
                data: datas,
                success: function(data) {

                    if (data.IsSuccess && data.Data) {

                        var _data = data.Data;

                        //设置关注引导页 地址
                        JitPage.setHashParam('ContactUrl',data.Data.BootUrl);


                        
                        me.lotteryData = _data;

                        //debugger用
                        //_data.content.IsLottery = 0;
                        //_data.IsShare = 1;

                        $('#hb_bg').css({

                            'backgroundImage':'url('+_data.PosterImageUrl+')'
                        });
                        _data.BootUrl=_data.BootUrl?_data.BootUrl:"";
                        _data.BootUrl = ( (_data.BootUrl.indexOf('http://')==-1)?('http://'+_data.BootUrl):_data.BootUrl );

                        $('[name=useDes]').attr('href',_data.BootUrl);

                        

                        me.initLotteryStatus();

                        me.checkNeedLogin();

                    }else {

                        me.alert(data.Message);
                    }
                }
            });
        }
    },
	initLotteryStatus:function(arg){

		var data = this.lotteryData.content;

		$('#prize').html(data.PrizesTypeName+''+data.PrizesName);

		$('#unget').show();
        
        $('#geted').hide();
	},
	usePointGetRedPacket:function(){
		
		JitPage.setHashParam('UsePointsLottery','1');

        location.reload();
	},
	checkNeedLogin:function(){
		/*
			SignFlag:  为1时不需要注册，为0时需要注册
		*/
		var me = this, data = me.lotteryData;

		me.needLogin = data.SignFlag?false:true;
		
        if(me.needLogin){

        	me.dialog({
				'title':'提示',
				'content':'您还不是会员，请先注册成为会员！',
				'labelOk':'注册',
				'callback_ok':'alert("表单ObjectId不存在！")'
			});

            return true;
        }

        return false;
	},
	initIsShareEvent:function(shareInfo){

		var me = this, data = me.data;

        var info = Jit.AM.getBaseAjaxParam();

        if(!data.IsShare){
        	//不是分享活动
            Jit.WX.shareFriends(shareInfo);
            
            return ;

        }else{
        	//是分享活动
            shareInfo.link = shareInfo.link + '&sender=' + info.userId + '&eventId=' + me.eventId;
            shareInfo.isAuth=true;   //需要高级auth认证
            //高级auth 认证
            Jit.WX.shareFriends(shareInfo);
        }

	},

	startEvent:function(){

        $('#sharePanel').hide();
    },
    /**
        抽奖状态码
        IsLottery=0  Status=1    该活动抽奖轮次没有启动
        IsLottery=1  Status=2    没有抽奖机会
        IsLottery=1  Status=3    抽中奖品
        IsLottery=0  Status=4    抽奖已结束
        IsLottery=0  Status=5    请先注册
        IsLottery=0  Status=6    请先签到
        IsLottery=0  Status=7    请先验证
        IsLottery=0  Status=8    未到现场不能抽奖
        IsLottery=1  Status=30   不能重复抽奖
     **/


    //点击抽奖事件
    showAward:function(){

        var me = this;
        Jit.UI.Loading();
        me.loadLotteryData();   //请求中奖信息
        Jit.UI.Loading(false);
        
        if(me.lotteryData.content.IsLottery == 0 && me.lotteryData.content.Status == 1){
            this.alert('该活动还没有启动!');
        }else if(me.lotteryData.content.IsLottery == 1 && me.lotteryData.content.Status == 3){
            //已抽奖
            $('#geted').show();
            $('#unget').hide();
        }else if(me.lotteryData.content.IsLottery == 1 && me.lotteryData.content.Status == 2){
            //判断是否可以继续用积分抽奖
            if(me.lotteryData.content.EventPointsLotteryFlag){
                //可用积分抽奖
                if(me.lotteryData.content.PersonPointsLotteryFlag){

                    this.dialog({
                        'title':'提示',
                        'content':'您已领过红包，再次领取需要使用'+me.lotteryData.content.EventPoints+'点积分。',
                        'labelOk':'返回',
                        'labelCancel':'继续',
                        'callback_ok':'JitPage.closeDialog()',
                        'callback_cancel':'JitPage.usePointGetRedPacket()'
                    });

                }else{

                    this.alert('您的积分不足，不能抽奖！');
                }
            }else{

                this.showAwardMsg(me.lotteryData.content.Status);

                $('#geted').show();
        
                $('#unget').hide();
            }
        }else if(me.lotteryData.content.IsLottery == 0&& me.lotteryData.content.Status == 4){
            me.alert("奖品已抽完,抽奖结束!");
        }else if(me.lotteryData.content.IsLottery == 1&& me.lotteryData.content.Status == 30){
            me.alert("不能重复抽奖!");
        }
    },
    showSharePanel:function(){

    	$('#shareMask').show();
    },
    hideSharePanel:function(){

    	$('#shareMask').hide();
    },
	dialog:function(cfg){
    	
    	var tpl = $('#tpl_dialog').html();

    	tpl = Mustache.render(tpl,cfg);

    	$(tpl).appendTo('body');
    },
    closeDialog:function(){

    	$('#dialog').remove();
    },
    alert:function(msg){

    	this.dialog({
    		'title':'提示',
			'content':msg,
			'labelOk':'确定',
			'callback_ok':'JitPage.closeDialog()'
    	});
    }
}); 