Jit.AM.defindPage({
	name: 'Yuding',
	elements: {
		intoListArea: ''
	},
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad: function() {
		var self = this;
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		
		this.detailID = JitPage.getUrlParam("detailId");
		this.objectID = JitPage.getUrlParam("objectId");
		this.immediate = JitPage.getUrlParam("immediate");
		if(!this.detailID){
			this.alert("未获取到detailId，请检查url");
			return false;
		}
		if(!this.objectID){
			this.alert("未获取到objectId，请检查url");
			return false;
		}
		this.isSending = false;
		this.ajaxUrl = '/ApplicationInterface/Project/HuaAn/HuaAnHandler.ashx';
		
		
		// if(this.immediate=="true"){
		// 	this.VerifIsRegister(function(datas){
		// 		if(datas.IsRegister==0){
		// 			Jit.AM.toPageWithParam("DynamicForm");
		// 		}else if(datas.IsRegister==1){
		// 			self.BuyFund();
		// 		}
		// 	});
		// }else{
			this.hideMask();
			this.initEvent();
		//}
	}, 
	
	//绑定事件
	initEvent: function() {
		var self = this;
		$("section").delegate(".ydBtn",this.eventType,function(){
			self.VerifIsRegister(function(datas){
				if(datas.IsRegister==0){
					Jit.AM.toPageWithParam("DynamicForm","toPage=Yuding&immediate=true");
				}else if(datas.IsRegister==1){
					self.BuyFund();
				}
			});
		});
		
	},
	VerifIsRegister:function(callback){
		var self = this;
		if(!self.isSending){
    		self.ajax({
	            url: self.ajaxUrl,
				interfaceType:'Project',
				interfaceMode:'V2.0',
	            data: {
	                action: "VerifIsRegister"
	            },
	            beforeSend: function() {
	            	self.isSending = true;
	            },
	            success: function(data) {
	                if(data.IsSuccess){
						if(callback){
							callback(data.Data);
						}
	                }else{
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
	            	self.isSending = false;
	            }
	        });
    	}
	},
	BuyFund:function(callback){
		var self = this;
		var toUrl="http://"+location.host+"/HtmlApps/html/special/worldunion/paysuccess.html?customerId="+self.getUrlParam('customerId');
		//debugger;
		
		if(JitPage.getUrlParam("objectId")){
			toUrl += "&objectId="+JitPage.getUrlParam("objectId");
		}
		
		self.ajax({
            url: self.ajaxUrl,
            type:"get",
			interfaceType:'Project',
			interfaceMode:'V2.0',
            data: {
                action: "BuyFund",
                HouseDetailID:self.detailID,
                ToPageUrl:toUrl
            },
            success: function(data) {
                if(data.IsSuccess){
					if(callback){
						callback(data.Data);
					}else{
						var obj = data.Data.FormData;
						var form='<form action="'+data.Data.Url+'" method="post">';
						for(var i in obj){
							form+='<input type="hidden" name="'+i+'" value="'+obj[i]+'">';
						}
						form+='</form>';
						$(form).appendTo("html").submit();
					}
                }else{
                	self.alert(data.Message);
                }                
            }
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