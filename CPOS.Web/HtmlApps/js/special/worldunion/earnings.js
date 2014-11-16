Jit.AM.defindPage({

    name: 'earnings',

	ele:{
		earningDetail:$('#earningDetail')
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
		
        this.initEvent();
        this.loadData();
    },
    initEvent: function() {
        var self = this;
    },
    loadData:function(){
    	this.GetHouseProfitList();
    },
    GetHouseProfitList:function(callback){
    	var self =this,
        	earningDetail = self.ele.earningDetail;
        	
    	if(!self.isSending){
    		JitPage.ajax({
	            url: self.ajaxUrl,
	            type:"get",
				interfaceType:'Project',
				interfaceMode:'V2.0',
	            data: {
	                action: "GetHouseProfitList"
	            },
	            beforeSend: function() {
	            	self.isSending = true;
	            	if(self.page.pageIndex==0){
	            		earningDetail.html('<div style="text-align:center;">正在加载,请稍后...</div>');
	            	}
	                
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	earningDetail.html('');
		                
		                if(!data.Data.IsBuyFund){
		                	$("#buyFundLayer").show();
		                }else{
		                	//渲染订单数据
		                	if (data.Data) {
			                    self.renderEarningDetail(data.Data);
			                } else {
		                		earningDetail.html('<div style="text-align:center;">暂无数据</div>');
			                }
		                }
	                }else{
	                	
	                	earningDetail.html('<div class="noData"><div style="height:200px;"></div><a href="javascript:JitPage.toPageWithParam(\'HousesList\');" class="commonBtn seeHousesBtn">查看可预订楼盘</a></div>');
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
	            	self.isSending = false;
	            }
	        });
    	}
    	
    },
    renderEarningDetail:function(itemObj){
    	itemObj.Date = new Date().getDate();
		var html=template.render('earningDetailTemp',itemObj);
    	this.ele.earningDetail.html(html);
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