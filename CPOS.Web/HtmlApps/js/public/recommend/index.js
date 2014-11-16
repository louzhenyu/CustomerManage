Jit.AM.defindPage({

    name: 'Recommend',
	
	initWithParam: function(param){
		var isRecommender = Jit.AM.getUrlParam("recommender")==1?true:false;
		var html=[];
		if(isRecommender){
			if (param.recommender&&param.recommender.length!=0) {
				for(var i=0;i<param.recommender.length;i++){
					var idata=param.recommender[i];
					var src = idata.indexOf("/")==-1?("/HtmlApps/images/public/recommend/"+idata):idata;
					html.push("<img class='img' src="+src+" />");
				}
	        }
		}else{
			if (param.recommended&&param.recommended.length!=0) {
				for(var i=0;i<param.recommended.length;i++){
					var idata=param.recommended[i];
					var src = idata.indexOf("/")==-1?("/HtmlApps/images/public/recommend/"+idata):idata;
					html.push("<img class='img' src="+src+" />");
				}
	        }
		}
		$("#wrapper").html(html.join(""));
	},
	
    onPageLoad: function() {
		
    	this.initData();
        this.initEvent();
    },
    initData:function(){
	
    	this.url = location.href;
		
    	this.recommenderId = Jit.AM.getUrlParam("openId");
		
		if(Jit.AM.getUrlParam("recommender")==1){
		
			Jit.AM.setPageParam('recommenderOpenId',this.getUrlParam('recommenderId'));
		}
    },
    initEvent: function() {
    	
		if(Jit.AM.getUrlParam("recommender")!=1){
		
			var url = location.host+'/HtmlApps/Auth.html?pageName=Recommend'
					+ '&customerId='+Jit.AM.CUSTOMER_ID
					+ '&recommender=1&recommenderId='+this.recommenderId;
					
			Jit.WX.shareFriends("好友推荐",'',url,null);
		}
		
		var me = this;
		
		me.ajax({
			url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getVipDetail',
                'userId': me.getUrlParam('userId')
            },
            success: function(data) {
			
                
            }
		})
    }
});