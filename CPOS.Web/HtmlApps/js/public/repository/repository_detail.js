Jit.AM.defindPage({

    name: 'RepositoryList',
	
	initWithParam: function(param){
	   
	},
	
    onPageLoad: function () {
	   
	   	var me = this;

	   	me.itemId = me.getUrlParam('itemId');

        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'Knowledge.Knowledge.GetKnowledgeDetail',
                'ID':me.itemId
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                var info = data.Data.KnowledgeInfo;

                me.info = info;

                if(data.ResultCode==0){

                    $('.banner').attr('src',info.ImageUrl);

                    $('.articleWrap').html(info.Content);

                    $('.good').html(info.PraiseCount);

                    $('.bad').html(info.TreadCount);

                    $('.collect').html('收藏 '+info.KeepCount);
                }
            }
        });
    },
    comment:function(gd){

    	var me = this;

    	if(me.hasComment){

    		Jit.UI.Dialog({
                'content': '您已经评价过！不能重复评价！',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });

    		return ;
    	}

    	var action = gd?'Knowledge.Knowledge.PraiseKnowledge':'Knowledge.Knowledge.TreadKnowledge';

        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': action,
                'ID':me.itemId
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.ResultCode==0){

                    if(gd){

                    	$('.good').html(me.info.PraiseCount+1);

                    }else{

                    	$('.bad').html(me.info.TreadCount+1);
                    }
                }
            }
        });

        me.hasComment = true;
    },
    collect : function(){

    	var me = this;
    	
    	me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'Knowledge.Knowledge.KeepKnowledge',
                'ID':me.itemId
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.ResultCode==0){

                    $('.collect').html('收藏 '+(me.info.KeepCount+1));
                }
            }
        });
    }
    
});