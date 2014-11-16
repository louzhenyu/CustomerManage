Jit.AM.defindPage({
	name: 'Introduce',
	elements: {
		intoListArea: ''
	},
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad: function() {
		this.hideMask();
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		
		this.detailID = JitPage.getUrlParam("detailId");
		this.objectID = JitPage.getUrlParam("objectId");
		if(!this.detailID){
			this.alert("未获取到detailId，请检查url");
			return false;
		}
		if(!this.objectID){
			this.alert("未获取到objectId，请检查url");
			return false;
		}
		this.initEvent();
	},
	//绑定事件
	initEvent: function() {
		var self = this;
		$("section").delegate(".jsListItem",this.eventType,function(e){
        	var $this = $(this);
    		JitPage.toPageWithParam("HouseTypeDetail","houseTypeDetailId="+$this.data("type"));
        	
        }).delegate(".jsImmediate",this.eventType,function(e){
        	var $this = $(this).parent();
    		JitPage.toPageWithParam("Yuding","immediate=true");
        	self.stopBubble();
        });
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