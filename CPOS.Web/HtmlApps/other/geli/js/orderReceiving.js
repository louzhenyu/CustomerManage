define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            orderList:$("#orderList"),
            orderListLayer:$("#orderListLayer"),
            orderDetailLayer:$("#orderDetailLayer")
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
        	this.url = "/ApplicationInterface/Project/Gree/GreeHandler.ashx";
        	this.customerId = $.util.getUrlParam("customerId");//"e703dbedadd943abacf864531decdac1";
            this.userId = $.util.getUrlParam("userId");   //"82B04CE0C05E4AFF9D2C51743B2E0A08";


            this.grabed = false;
            if(!this.customerId){
                alert("获取不到请求参数customerId！");
                return false;
            }
            if(!this.userId){
                alert("获取不到请求参数userId！");
                return false;
            }

            //alert(this.userId);

            this.dataList = [];
            this.locationList = [];

        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            this.initEvent();
        },
        GrabOrder:function(orderNo,callback){
            this.ajax({
                url: this.url,
                data:{
                    action:"GrabOrder",
                    interfaceType:"Project",
                    customerId:this.customerId,
                    userId:this.userId,
                    ServiceOrderNO:orderNo
                },
                beforeSend:function(){
                    self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {
                    self.grabed = true;
                    if (data.IsSuccess) {
                        if(callback){
                            callback(data);
                        }
                    } else {
                        alert(data.Message);
                    }
                },
                complete:function(){
                    self.isSending = false;
                    //$.native.loading.hide();
                    
                    setTimeout(function(){
                        if(self.page.pageIndex==0){
                            //$.native.downpull.hide();
                        }else{
                            //$.native.uppull.hide();
                        }  
                    },10);

                }
            })
        },
        GrabState:function(orderNo,callback){
            this.ajax({
                url: this.url,
                data:{
                    action:"GrabState",
                    interfaceType:"Project",
                    customerId:this.customerId,
                    userId:this.userId,
                    ServiceOrderNO:orderNo
                },
                beforeSend:function(){
                    self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {
                    
                    if (data.IsSuccess) {
                        if(callback){
                            callback(data);
                        }
                    } else {
                        alert(data.Message);
                    }
                },
                complete:function(){
                    self.isSending = false;
                    //$.native.loading.hide();
                    
                    setTimeout(function(){
                        if(self.page.pageIndex==0){
                            //$.native.downpull.hide();
                        }else{
                            //$.native.uppull.hide();
                        }  
                    },10);

                }
            });
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
                    action:"GetRunningServiceOrder",
                    interfaceType:"Project",
                    customerId:this.customerId,
                    userId:this.userId
                },
                beforeSend:function(){
                	self.isSending = true;
                    $.native.loading.show();
                },
                success: function (data) {
                    if (data.IsSuccess) {
                    	if(data.Data.SubOrderList.length!=self.page.pageSize){
                    		self.noMore=true;
                    	}
                        //alert(data.Data.SubOrderList.length);
                        if(data.Data.SubOrderList.length!=0){
                            var orderList = data.Data.SubOrderList;
                            //保存全局变量
                            for (var i =0; i < orderList.length ; i++) {
                                switch(orderList[i].ServiceType){
                                    case 1:
                                      orderList[i].ServiceTypeName = "安装";
                                      break;
                                    case 2:
                                      orderList[i].ServiceTypeName = "维修";
                                      break;
                                    default:
                                      orderList[i].ServiceTypeName = "未知";
                                }
                                var locationItem = {
                                    latitude:orderList[i].Latitude,
                                    longitude:orderList[i].Longitude,
                                    title:orderList[i].Message,
                                    description:orderList[i].ServiceAddress
                                }                      
                                self.dataList.push(orderList[i]);
                                self.locationList.push(locationItem);
                            };
                            self.renderPageList(orderList);
                        }else{
                            setTimeout(function(){
                                //debugger;
                               $.native.noData();
                            },100);
                            
                        }
                        
                    } else {
                        //alert(data.Message);
                    }
                },
                error:function(){
					alert("加载数据失败！")  

                    // 假数据
                    var arr = [];
                    var testObj = {"ServiceOrderNO":"0001","ServiceType":1,"ServiceOrderDate":"2014-07-10T00:00:00+08:00","ServiceOrderDateEnd":"2014-07-21T00:00:00+08:00","InstallOrderDate":"2014-07-20T00:00:00+08:00","ServiceAddress":"上海市静安区延平路","VipID":"82B04CE0C05E4AFF9D2C51743B2E0A08","CustomerName":"王明","CustomerPhone":"12345678901","Latitude":23.123,"Longitude":121.456,"Distance":"100","Message":"无","InstallCount":2,"DeviceList":[{"DeviceID":"01","DeviceName":"格力01","InstallPosition":1,"DeviceCount":1},{"DeviceID":"02","DeviceName":"格力02","InstallPosition":2,"DeviceCount":1}]};

                    for (var i = 5; i >= 0; i--) {
                        arr.push(testObj);
                    };
                    self.renderPageList(arr);              	
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
            window.showDetail= function(index){
                $("#orderInfo").html(template.render('tplOrder',self.dataList[index]));
                self.ele.orderListLayer.hide();
                self.ele.orderDetailLayer.show();
            }
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

            this.ele.orderList.delegate(".takeBtn","click",function(e){
                var index = $(this).attr("data-index");
                $("#orderInfo").html(template.render('tplOrder',self.dataList[index]));
                self.ele.orderListLayer.hide();
                self.ele.orderDetailLayer.show();
            });

            this.ele.orderDetailLayer.delegate("#takeBtn","click",function(){
                var orderNo =  $(this).attr("data-orderNo");
                if(!self.grabed){
                    self.GrabOrder(orderNo,function(data){
                        //alert(JSON.stringify(data));
                        self.GrabStateFlag = setInterval(function(){
                            self.GrabState(orderNo,function(data){
                                if(data.Data.IsSuccess==1){
                                    location.href="serviceDetails.html?customerId="+self.customerId+"&userId="+self.userId+"&orderNo="+orderNo;            
                                }
                            });
                        },1000);
                        
                        
                    });
                }
                                
            }).delegate(".closeBtn","click",function(){
                if(self.GrabStateFlag){
                    clearInterval(self.GrabStateFlag);
                }
                self.ele.orderListLayer.show();
                self.ele.orderDetailLayer.hide();
            })

            this.ele.section.delegate(".mapBtn","click",function(){
                var index = $(this).attr("data-index");
                
                $.native.mapView(self.locationList,index);
            });
        },
        renderPageList: function (list) {
        	if(self.page.pageIndex==0){
        		this.ele.orderList.html(template.render('tplListItem',{list:list}));
        	}else{
        		this.ele.orderList.append(template.render('tplListItem',{list:list}));
        	}
            
        }
    };
    self = page;

    page.init();
});