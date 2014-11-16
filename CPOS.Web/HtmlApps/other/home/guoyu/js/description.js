define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        init: function () {
        	this.url = "http://o2oapi.aladingyidong.com/ApplicationInterface/Customer/CustomerGateway.ashx";
        	this.customerId=$.util.getUrlParam("customerId");
        	this.page=$.util.getUrlParam("page");
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams:function(){
        	var urlstr = window.location.href.split("?"),
            params = {};
           
	        if (urlstr[1]) {
				var items = urlstr[1].split("&");
				for (var i = 0; i < items.length; i++) {
					itemarr = items[i].split("=");
					params[itemarr[0]] = itemarr[1]; 
				}
			}
			params.Action="GetCustomerBasicSetting";
			params.ReqContent={
				"CustomerID":this.customerId
			};
			var reqContent = params.ReqContent;
			
			params.ReqContent = encodeURIComponent(JSON.stringify(reqContent));
			var str="action="+params.Action+"&type=Product"+"&req="+params.ReqContent;
			return this.url+"?"+str
        },
        
        loadData: function () {
        	var that=this;
        	$.ajax({
                url: this.buildAjaxParams(),
                beforeSend:function(){
                	self.isSending = true;
                },
                success: function (data) {
                	data=JSON.parse(data);
                    if (data.ResultCode ==0&&data.IsSuccess) {
                    	var html="";
                    	if(data.Data){
	                    	switch(that.page){
	                    		case "story" :
	                    			html+=data.Data.BrandStory;
	                    			break;
	                    		case "aboutCard" :
	                    			html+=data.Data.BrandRelated;
	                    			break;
	                    		case "aboutUs" :
	                    			html+=data.Data.AboutUs;
	                    			break;
	                    	}
	                    	$("#section").html(html);
	                    }
                    } else {
                    	$("#section").html("未获取到数据!");
                        alert(data.Message);
                    }
                },
                error:function(){
					alert("加载数据失败！");
                },
                complete:function(){
                    $.native.downpull.hide();
                }
            });
        },
        initEvent: function () {
			
        }
    };

    page.init();
});