Jit.AM.defindPage({

    name: 'houseDetail',

	ele:{
		housesDetail:$('#housesDetail')
	},
	page:{
		'pageIndex':0,
		'pageSize':10
	},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('进入'+this.name);
		this.isSending = false;
		this.ajaxUrl = '/ApplicationInterface/Project/HuaAn/HuaAnHandler.ashx';
		
		this.DetailID = JitPage.getUrlParam("detailId");
		this.ThirdOrderNo = JitPage.getUrlParam("orderNo");	// 交易号
		this.PrePaymentID = JitPage.getUrlParam("payId");	//预付款订单ID
		if(!this.DetailID){
			this.alert("未获取到detailId，请检查url");
			return false;
		}
		if(!this.ThirdOrderNo){
			this.alert("未获取到orderNo，请检查url");
			return false;
		}
		if(!this.PrePaymentID){
			this.alert("未获取到payId，请检查url");
			return false;
		}
        this.initEvent();
        this.loadData();
    },
    initEvent: function() {
        var self = this;
        self.ele.housesDetail.delegate("#redeemBtn",JitPage.eventType,function(){
        	if(!$(this).hasClass("disabled")){
        		self.FundRansom();
        	}
        }).delegate("#buyBtn",JitPage.eventType,function(){
        	if(!$(this).hasClass("disabled")){
        		self.PayHouse();
        	}
        	
        });
    },
    loadData:function(){
    	this.GetMyHouseDetail();
    },
    GetMyHouseDetail:function(callback){
    	var self =this,
        	housesDetail = self.ele.housesDetail;
        	
    	if(!self.isSending){
    		JitPage.ajax({
	            url: self.ajaxUrl,
				interfaceType:'Project',
				interfaceMode:'V2.0',
				type:"get",
	            data: {
	                action: "GetMyHouseDetail",
	                DetailID:self.DetailID,
	                ThirdOrderNo:self.ThirdOrderNo,
	                PrePaymentID:self.PrePaymentID
	            },
	            beforeSend: function() {
	            	self.isSending = true;
	            	if(self.page.pageIndex==0){
	            		housesDetail.html('<div style="text-align:center;">正在加载,请稍后...</div>');
	            	}
	                
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	//渲染订单数据
	                	if (data.Data.HouseDetail) {
	                		
		                    self.renderHouseDetail(data.Data.HouseDetail);
		                    
		                } else {
	                		housesDetail.html('<div style="text-align:center;">暂无数据</div>');
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
    renderHouseDetail:function(itemObj){
    	switch (parseInt(itemObj.HouseState)) {
	        case 1:
	            itemObj.HouseStateDes = "已开盘";
	            break;
	        case 2:
	            itemObj.HouseStateDes = "在建中";
	            break;
	        default:
	            itemObj.HouseStateDes = "未知HoseState";
	            break;
	    }
	    //alert("DisabledRedeem:"+itemObj.DisabledRedeem);
	    //alert("DisablePay:"+itemObj.DisablePay);
		var html=template.render('housesDetailTemp',itemObj);
    	this.ele.housesDetail.html(html);
    },
    
    FundRansom:function(callback){
		var self = this;
		var toUrl="http://"+location.host+"/HtmlApps/html/special/worldunion/paysuccess.html?customerId="+self.getUrlParam('customerId');
		if(JitPage.getUrlParam("objectId")){
			toUrl += "&objectId="+JitPage.getUrlParam("objectId");
		}
		
		
		self.ajax({
            url: self.ajaxUrl,
			interfaceType:'Project',
			interfaceMode:'V2.0',
            data: {
                action: "FundRansom",
                HouseDetailID:self.DetailID,
                ThirdOrderNo:self.ThirdOrderNo,
                PrePaymentID:self.PrePaymentID,
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
	
	PayHouse:function(callback){
		var self = this;
		var toUrl="http://"+location.host+"/HtmlApps/html/special/worldunion/paysuccess.html?customerId="+self.getUrlParam('customerId');
		if(JitPage.getUrlParam("objectId")){
			toUrl += "&objectId="+JitPage.getUrlParam("objectId");
		}
		self.ajax({
            url: self.ajaxUrl,
			interfaceType:'Project',
			interfaceMode:'V2.0',
            data: {
                action: "PayHouse",
                HouseDetailID:self.DetailID,
                ThirdOrderNo:self.ThirdOrderNo,
                PrePaymentID:self.PrePaymentID,
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
	confirm:function(text,OKCallback,CancelCallback){
		Jit.UI.Dialog({
			'type': 'Confirm',
			'content': text,
			'LabelOk': '确认',
			'LabelCancel': '取消',
			'CallBackOk': function() {
				if(OKCallback){
					Jit.UI.Dialog("CLOSE");
					OKCallback();
				}
			},
			'CallBackCancel': function() {
				if(CancelCallback){
					Jit.UI.Dialog("CLOSE");
					CancelCallback();
				}else{
					Jit.UI.Dialog("CLOSE");
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