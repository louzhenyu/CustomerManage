Jit.AM.defindPage({

    name: 'SharePage',
	
    directPage:'',

    eventId:'',

    isSender:'true',

	initWithParam: function(param){
		
		$("#wrapper").html("<img class='img' src="+param.mainBg+" />");
	},
	
    onPageLoad: function() {
		
    	this.initData();
    },
    initData:function(){
	
    	var me = this;

    	me.directPage = me.getUrlParam('pName');

    	me.eventId = me.getUrlParam('eventId');

    	me.isSender = me.getUrlParam('isSender');

    	if(me.isSender == 'true'){

    		$('.btn').hide();

    		var info = Jit.AM.getBaseAjaxInfo();

    		var url = location.host+'/HtmlApps/Auth.html?pageName=SharePage'
					+ '&customerId='+Jit.AM.CUSTOMER_ID
					+ '&isSender=false&eventId='+me.eventId
					+ '&pName='+me.directPage+'&RecommandId='+info.userId;
					
			Jit.WX.shareFriends("好友推荐",'',url,null);
    	}
    },
    directToPage:function(){
    	
    	var me = this;

    	var hasOAuth = Jit.AM.getAppParam('Launch','CheckOAuth');

    	if(hasOAuth == 'unAttention'){
			
			var cfg = Jit.AM.getAppVersion();

			Jit.UI.Dialog({
                'content': '参与本活动需要先关注微信公众号：'+cfg.APP_NAME,
                'type': 'Alert',
                'LabelOk': '确定',
                'CallBackOk': function(){

                    Jit.UI.Dialog('CLOSE');
                }
            });
			
			return false;

		}else if(hasOAuth == 'Attention'){

			if(me.isSender == 'true'){

				Jit.UI.Dialog({
	                'content': '点击右上角分享按钮，分享给好友',
	                'type': 'Alert',
	                'LabelOk': '确定',
	                'CallBackOk': function(){

	                    Jit.UI.Dialog('CLOSE');
	                }
	            });

			}else{

				var RecommandId = Jit.AM.getUrlParam('RecommandId');

				var paramstr = 'eventId='+me.eventId;

				if(RecommandId){

					paramstr += ('&RecommandId='+RecommandId);
				}
				
				Jit.AM.toPage(me.directPage,paramstr);
			}
		}
    }
});