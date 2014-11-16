var feedback ={
	init:function(){
		this.params = {};
		this.ajaxSending = false;
		this.toString = String.prototype.toString;
		this.regObj={
			require:/\s/
		};
	},
	setParams:function(paramsArr){
		this.params = paramsArr;
	},
	submit:function(data,callback){
		var self= this;
		this.setParams(data);
		if(this.validate()){
			if(!this.ajaxSending){
				var datas = {
					action:'feedback',
					name:data.name.value,		//会员真实姓名
					phone:data.phone.value,		//联系电话
					area:data.area.value,		//购买地区
					seller:data.seller.value,		//出售商家
					state:data.state.value		//反馈情况
				};
				Jit.AM.ajax({
					url:'/module/productcheck.ashx',
					data:datas,
					beforeSend:function(){
						self.ajaxSending = true;
					},
					success:function(data){
						if(data){
							if(data.code ==200){
								if(callback){
									callback();
								}
							}else{
								self.alert(data.description);
							}
						}else{
							self.alert("服务器返回数据出错");
						}
					},
					error:function(){
						Jit.UI.Dialog({
							type:"Alert",
							content:"网络故障或服务器接口不存在",
							CallBackOk:function(data){
								Jit.UI.Dialog("CLOSE");
							}
						});
					},
					complete:function(){
						self.ajaxSending = false;
					}
				});
			}
		}else{
			this.alert(this.tips);
		}
	},
	validate:function(){
		var params = this.params;
		var flag =  true;
		if(!$.isEmptyObject(params)){
			for(var i in params){
				var idata = params[i];
				if(!!idata.reg){
					ireg =this.toString.call(idata.reg)=="[object RegExp]"?idata.reg:this.regObj[idata.reg]; 
					if(ireg){
						if(ireg.test(idata.value)||idata.value.length==0){
							flag = false;
							this.tips = idata.key + "不能为空";
							break;
						}
					}else{
						flag = false;
						this.tips = "正则key关键字"+idata.reg+"不在regObj对象中";
						break;
					}
					
				}
			}
		}else{
			flag = false;
			this.tips = "请用setParams方法设置参数";
		}
		return flag;
	},
	alert:function(text){
		Jit.UI.Dialog({
			type:"Alert",
			content:text,
			CallBackOk:function(data){
				Jit.UI.Dialog("CLOSE");
			}
		});
	}
};
