Jit.AM.defindPage({

	name : 'RedPacket',
	
	initWithParam: function(param) {
	    
	},
    
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入'+this.name+'.....');
		
		this.initData();
	},

    initData:function(){
        
        var me = this;
        
        me.ajax({
            url:'/ApplicationInterface/Event/EventGateway.ashx',
            data:{
                'action':'GetEventUserPrizeList',
                'EventId':JitPage.getHashParam('EventId')||me.getUrlParam('eventId')
            },
            success:function(data){
                
                if(data.IsSuccess&&data.ResultCode==0){
                    me.loadPageData(data.Data);
                }
                var info = Jit.AM.getBaseAjaxParam(),
                shareUrl = location.href.replace('userId='+info.userId,''),
                shareUrl = shareUrl.replace('openId='+info.openId);
                var shareInfo = {
                    'title':"我领的红包",
                    'desc':"瞧瞧我领了好多红包!快来参与吧!",
                    'link':shareUrl,
                    'imgUrl':JitCfg.shareIco
                };

                //分享开始
                me.initIsShareEvent(shareInfo);
            }
        });
    },
    loadPageData:function(data){
        
        var itemlists = data.GetEventUserPirzeList;

        if(itemlists == null){

            return;
        }
        
        //$('#parValue').html(data.integral);

        //$('#recommendNum').html(itemlists.length);
        
        var tpl = $('#tpl_record_item').html(),html = '';
        
        for(var i=0;i<itemlists.length;i++){
            
            var hashtpl = tpl;
            
            var itemdata = itemlists[i];
            
            html += Mustache.render(tpl,itemdata);
        }
        
        $("#infoList").html(html);
    },
    initIsShareEvent:function(shareInfo){

        var me = this, data = me.data;

        var info = Jit.AM.getBaseAjaxParam();
            //是分享活动
            shareInfo.link = shareInfo.link + '&sender=' + info.userId + '&eventId=' + me.eventId;
            shareInfo.isAuth=true;   //需要高级auth认证
            //高级auth 认证
            Jit.WX.shareFriends(shareInfo);

    }
}); 