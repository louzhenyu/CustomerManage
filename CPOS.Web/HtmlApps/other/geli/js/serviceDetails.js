define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
        	this.url = "/ApplicationInterface/Project/Gree/GreeHandler.ashx";
        	this.customerId = $.util.getUrlParam("customerId");//"e703dbedadd943abacf864531decdac1";
            this.userId = $.util.getUrlParam("userId");   //"82B04CE0C05E4AFF9D2C51743B2E0A08";
            this.orderNo = $.util.getUrlParam("orderNo"); 

            if(!this.customerId){
                alert("获取不到请求参数customerId！");
                return false;
            }
            if(!this.userId){
                alert("获取不到请求参数userId！");
                return false;
            }
            if(!this.orderNo){
                alert("获取不到请求参数orderNo！");
                return false;
            }

        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            this.initEvent();
        },
        
        buildAjaxParams:function(param){
        	var _param = {
                url: "",
                type: "get",
                dataType: "json",
                data: null,
                beforeSend: function () {
                    
                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown){
                    
                }
            };
            
            $.extend(_param,param);
            
            
            var action = param.data.action,
                interfaceType = param.data.interfaceType||'Product',
                _req = {
                    'CustomerID':(param.data.customerId?param.data.customerId:null),
                    'UserID':param.data.userId?param.data.userId:null,
                    'Parameters':param.data
                };

            delete param.data.customerId;
            delete param.data.userId;
            delete param.data.action;
            delete param.data.interfaceType;

            var _data = {
                'req':JSON.stringify(_req)
            };
            
            _param.data = _data;

            _param.url = _param.url+'?type='+interfaceType+'&action='+action;

            return _param;
        },
        ajax:function(param){

            var _param = this.buildAjaxParams(param);
            
            $.ajax(_param);
        },
        loadData: function () {
        	this.ajax({
                url: this.url,
                data:{
                    action:"GetSubscribeOrderDetail",
                    interfaceType:"Project",
                    customerId:this.customerId,
                    userId:this.userId,
                    ServiceOrderNO:this.orderNo
                },
                beforeSend:function(){
                	self.isSending = true;
                    $.native.loading.show();
                },
                success: function (data) {
                    if (data.IsSuccess) {
                    	
                        var orderObj = data.Data.UnOrder;
                        //保存全局变量
                        switch(orderObj.ServiceType){
                            case 1:
                              orderObj.ServiceTypeName = "安装";
                              break;
                            case 2:
                              orderObj.ServiceTypeName = "维修";
                              break;
                            default:
                              orderObj.ServiceTypeName = "未知";
                        }
						$("#serviceInfo").html(template.render("tplService",orderObj));
                    } else {
                        alert(data.Message);
                    }
                },
                error:function(){
					alert("加载数据失败！")            	
                },
                complete:function(){
                	self.isSending = false;
                    $.native.loading.hide();
                    
                    setTimeout(function(){
                        if(self.page.pageIndex==0){
                            $.native.downpull.hide();
                        }else{
                            $.native.uppull.hide();
                        }  
                    },10);

                }
            });
        },
        initEvent: function () {
			window.onscroll = function(){
				if($.util.getScrollTop() + $.util.getWindowHeight()+50 >= $.util.getScrollHeight()){
					if(!self.noMore&&!self.isSending){
						self.page.pageIndex++;
						//self.loadData();
					}else{
                        $.native.uppull.hide();
                    }
			    }
			};
           
            this.ele.section.delegate(".receiptBtn","click",function(){
                location.href="orderReceiving.html?customerId="+self.customerId+"&userId="+self.userId;
            });
        }
    };
    self = page;

    page.init();
});