Jit.AM.defindPage({
	name: 'Introduce',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		
		this.detailID = JitPage.getUrlParam("detailId");
		this.objectID = JitPage.getUrlParam("objectId");
		this.houseTypeDetailId = JitPage.getUrlParam("houseTypeDetailId");
		if(!this.detailID){
			this.alert("未获取到detailId，请检查url");
			return false;
		}
		if(!this.objectID){
			this.alert("未获取到objectId，请检查url");
			return false;
		}
		if(!this.houseTypeDetailId){
			this.alert("未获取到房型houseTypeDetailId，请检查url");
			return false;
		}
		this.initEvent();
	}, //加载数据
	//绑定事件
	initEvent: function() {

		$(".houseTypeImg")[0].src ="/HtmlApps/images/special/worldunion/"+this.houseTypeDetailId+".jpg";

		$("section").delegate(".ydBtn",this.eventType,function(){
			JitPage.toPageWithParam("Yuding");
		});
		this.hideMask();


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