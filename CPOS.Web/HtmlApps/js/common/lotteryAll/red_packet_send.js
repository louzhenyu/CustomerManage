Jit.AM.defindPage({

    name:'RedPacketSend',
    
    onPageLoad:function(){
        
        //当页面加载完成时触发
        Jit.log('进入Record.....');
        
        this.initEvent();
    },
    
    initEvent:function(){
        
        var me = this;
        
        /*
        (function(){
            var win = {
                sH : window.innerHeight,
                sW : window.innerWidth
            }
            
            $(window).resize(function(){
            
                if((win.sH-window.innerHeight > 100) && (win.sW == window.innerWidth)){
                    
                    //$('#nav').hide();
                }else{
                
                    //$('#nav').show();
                }
            });
        })()
        */
        me.windowHeight = window.innerHeight;
        
        me.windowWidth = window.innerWidth;
        
        
        me.ajax({
            url:'/Lj/Interface/Data.aspx',
            data:{
                'action':'getRecommendRecord'
            },
            success:function(data){
                if(data&&data.content){
                    me.loadPageData(data.content);
                }
                var info = Jit.AM.getBaseAjaxParam(),
                shareUrl = location.href.replace('userId='+info.userId,''),
                shareUrl = shareUrl.replace('openId='+info.openId);
                var shareInfo = {
                    'title':"我发的红包",
                    'desc':"瞧瞧我发了好多红包!快来领取红包吧!",
                    'link':shareUrl,
                    'imgUrl':JitCfg.shareIco
                };
                //分享开始
                me.initIsShareEvent(shareInfo);
            }
        });
    },
    loadPageData:function(data){
        
        var itemlists = data.recordList;

        if(itemlists == null){

            return;
        }
        
        $('#parValue').html(data.integral);

        $('#recommendNum').html(itemlists.length);
        
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