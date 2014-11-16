Jit.AM.defindPage({
    eventId: '',
    name: 'ScratchCard',

    canAgain: false,

    onPageLoad: function() {
        //alert("aaa");
        this.eventId = this.getUrlParam('eventId');

		if(!this.eventId){

			self.alert("eventId为空，未获取到活动信息");

			return false;
		}

        this.loadData();
    },
    
    
    loadData: function() {

        var me = this;
        debugger;
        image = {
            'back': {
                'url': '../../../images/common/lotteryAll/tran1.png',
                'img': null
            },
            'front': {
                'url': '../../../images/common/lotteryAll/gjx.png',
                'img': null
            }
        }, prize = '', isWinner = false, isLottery = '';

        var hasOAuth = Jit.AM.getAppParam('Launch','CheckOAuth');
        if(hasOAuth == 'unAttention'){
            var cfg = Jit.AM.getAppVersion();
			this.alert('参与本活动需要先关注微信公众号：'+cfg.APP_NAME);
            return false;
        }


        //初次加载用户信息

        var datas = {
            'action': 'Event.EventPrizes.GetEventPrizes',
            'Longitude': '0.0',
            'Latitude': '0.0',
            'eventId': me.eventId,
            'RecommandId': Jit.AM.getUrlParam('sender') || ''
        };

        //加载中奖信息
        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: datas,
            success: function(data) {
                if (data.IsSuccess && data.Data) {
                    debugger;
                    //alert(JSON.stringify(data.Data));
                    var _data = data.Data.content;

                    me.IsShare =  data.Data.IsShare;
                    me.PosterImageUrl = data.Data.PosterImageUrl;
                    /*
                    _data.isShare = true;

                    _data.ShareRemark = 'AAAAAAAAAAAAAAAAAAAAAAAA';

                    _data.OverRemark = 'Over.....Over.....Over.....';

                    _data.PosterImageUrl = 'http://image.tianjimedia.com/uploadImages/2014/049/5BGJM9EU8868_1000x500.jpg'
                    */

                    //设置关注引导页 地址
                    JitPage.setHashParam('ContactUrl',data.Data.BootUrl);
                    var info = Jit.AM.getBaseAjaxParam(),
                        shareUrl = location.href.replace('userId='+info.userId,''),
                        shareUrl = shareUrl.replace('openId='+info.openId,'');
                        shareUrl+="&scope=snsapi_userinfo";  //高级Auth认证

                    /*
                    Jit.WX.shareFriends({
                        'title':data.Data.ShareRemark,
                        'desc':data.Data.OverRemark,
                        'link':shareUrl,
                        'imgUrl':data.Data.ShareLogoUrl
                    });
                    */

                    var shareInfo = {
                        'title':(data.Data.ShareRemark||'好友推荐'),
                        'desc':(data.Data.OverRemark||'大奖等你抢！'),
                        'link':shareUrl,
                        'imgUrl':(data.Data.ShareLogoUrl||JitCfg.shareIco)
                    }
                    shareInfo.isAuth=true;

                    me.data = _data;

                    me.buildPrizeList(_data.PrizesList);

                    me.isLottery = _data.IsLottery;

                    me.needLogin = data.Data.SignFlag?false:true;

                    if(me.needLogin){

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

                    me.initIsShareEvents(_data,shareInfo);

                    if (_data.IsLottery == 0) {
                        
                        //可以刮奖

                        prize = _data.WinningDesc;

                        loadcanvas(function() {

                            $('#JPName').html(_data.WinningDesc);
                        });

                    } else if (_data.IsLottery == 1) {
                        //已刮奖，展示奖品

                        $("#JPName").text(_data.WinningDesc);

                        $("#asdi").hide();

                        me.Winner();
                    }

                }else {
					me.alert(data.Message);
				}
            }
        });
    },

    //初始化是否是推荐有礼类型活动抽奖
    initIsShareEvents:function(data,shareInfo){
        debugger;
        var self = this;

        //self.IsShare = data.IsShare;

        var info = Jit.AM.getBaseAjaxParam();
        //alert("IsShare:"+self.IsShare);
        if(!self.IsShare){

            $('.lottery_area').show();
            Jit.AM.initShareEvent(shareInfo);
            
            return ;
        }else{

            shareInfo.link = shareInfo.link + '&sender=' + info.userId + '&eventId=' + self.eventId;
            shareInfo.isAuth=true;
            //alert(JSON.stringify(shareInfo));
            Jit.WX.shareFriends(shareInfo);
            
        }

        $('.sharePanel').show();

        $('.lottery_area').hide();

        $("#shareWrapper").html("<img class='img' src="+self.PosterImageUrl+" />");

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

                location.href = JitPage.getHashParam('ContactUrl');
				//location.href="http://mp.weixin.qq.com/s?__biz=MjM5NTI0NjMyMQ==&mid=204527844&idx=1&sn=6059283611c5c6b1beefa23a749d80ad#rd";
            }
        });
    },


    buildPrizeList: function(plist) {

        if(!plist){

            return;
        }

        var tpl = $('#tpl_PrizeItem').html(),
            html = '';

        for (var i = 0; i < plist.length; i++) {

            var itemdata = plist[i];

            html += template.render('tpl_PrizeItem',itemdata);

            //html += template.render(tpl, itemdata);
        }

        $('#goodsList').html(html);
    },
	

    Winner: function() {

    },

    saveLottery: function() {

        var me = this;

        if(me.hasLottery){

            return;
        }

        setTimeout(function() {

            me.hasLottery = true;

            if (me.IsShare && !me.isLottery) {

                Jit.UI.Dialog({
                    'content': '您抽中了：'+me.data.WinningDesc,
                    'type': 'Alert',
                    'LabelOk': '确定',
                    'CallBackOk': function() {

                        Jit.UI.Dialog('CLOSE');

                        $('.sharePanel').show();

                        $('.lottery_area').hide();

                        $('#shareImg').show();

                        $('#btn_join').html('查看奖品').unbind().bind('click',function(){

                            me.showAward(me.data);
                        });
                    }
                });
            }

        }, 600);
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