Jit.AM.defindPage({

	name : 'scanRepeat',

	onPageLoad : function() {
		//当页面加载完成时触发
		var self = this;
		feedback.init();
		Jit.log('进入scanning页面...');
		//判断是否添加关注
		if(this.getUrlParam("CheckOAuth")=="unAttention"){
			this.renderPage("scanFail");
		}else{
			this.initEvent();
			this.traceCode = this.getUrlParam("traceCode");	// ? this.getUrlParam("traceCode") : "0126901798163985211514411797208837";
			this.traceCount = 0;
			//记录扫描次数
			if (!!this.traceCode) {
				this.ajax({
					url : '/module/productcheck.ashx',
					data : {
						'action' : 'checkProduct',
						'traceCode' : this.traceCode
					},
					success : function(data) {
						if (data) {
							if (data.code == 200) {
								//alert(JSON.stringify(data.content));
								//alert("data.content.isValid:"+data.content.isValid);
								//alert("data.content.traceCount:"+data.content.traceCount);
								
								if (data.content.isValid == 1) {
									if (data.content.currentPoint != 0 && data.content.currentPoint != null) {
										if (data.content.traceCount == 0 || data.content.traceCount == null) {
											self.renderPage("scanSuccess", data.content, function() {
												$("#goShop").show();
											});
										} else {
											self.traceCount = data.traceCount;
											self.renderPage("scanRepeat", data.content);
										}
									}else{
										self.renderPage("scanSuccess", data.content);
									}
									
								} else {
									self.renderPage("scanWarn");
								}
							} else {
								self.alert(data.description);
							}
						} else {
							self.alert("返回数据为空");
						}
					},
					error : function() {
						self.alert("网络故障或服务器接口不存在");
					}
				});
			} else {
				this.alert("获取traceCode失败");
				return false;
			}
		}
	},
	initEvent : function() {
		var self = this;
		$("#section").delegate("#submit", "click", function() {
			var object = {};
			$(this).siblings("div.infoFeed").find("p").each(function(i, e) {
				var nodes = e.children;
				var obj = {
					key : nodes[1].innerHTML,
					value : nodes[2].value,
					name:nodes[2].name,
					reg : e.className == "must" ? "require" : null
				}; 
				object[obj.name] = obj;
			});
			feedback.submit(object,function(){
				self.alert("提交成功！",function(){
					Jit.AM.toPage("JiFenShop");
				});
			});
		});
	},
	alert : function(text, callback) {
		Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function(data) {
				if (callback) {
					callback();
				} else {
					Jit.UI.Dialog("CLOSE");
				}
			}
		});
	},
	renderPage : function(pageName, data, callback) {
		$("#section").html(template.render(pageName,data));
		if (callback) {
			callback();
		}
	}
});
