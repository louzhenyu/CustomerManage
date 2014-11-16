Jit.AM.defindPage({
    name: 'appointment',
    elems:{},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initPage();
        this.initEvent();
        var param=this.pageParam;
    }, //加载数据
    //绑定事件
    initEvent: function() {
       var that=this;
       //选择性别
       $(".airList").delegate(".checkBox",this.eventType,function(){
       		var $this=$(this);
       		if($this.hasClass("on")){
       			$this.removeClass("on");
       		}else{
       			$this.addClass("on");
       		}
       });
       //开始预约
       $("#addAir").bind(this.eventType,function(){
       		//判断是否选中了空调
       		var length=$(".airList").find(".on").length;
       		if(length==0){
       			that.alert("请至少选择一个安装位置!");
       			return ;
       		}
       		//提交预约
       		that.submitOrderData(function(data){
       			//预约单号
       			Jit.AM.setPageParam("ServiceOrderNo",data.Data.ServiceOrderNO);
       			Jit.UI.Dialog('CLOSE');
       			that.toPage("IsAppointment");
       		});
       		
       });
    },
    initPage:function(){

    },
    //提交预约
    submitOrderData:function(callback){
    	var that=this;
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'SubmitAppOrder',
                'OrderNO': Jit.AM.getPageParam("ServiceOrderNo"),
                'ServiceType':"1",     					//1 安装   2维修
                'ServiceOrderDateEnd':"2014-08-06",
                'ServiceOrderDate':Jit.AM.getPageParam("OrderTime"),					//预约日期
                'ServiceAddress':Jit.AM.getPageParam("OrderAddress"),					//预约地址
                'Surname':Jit.AM.getPageParam("OrderName"),
                'Msg':Jit.AM.getPageParam("OrderMsg"),								//消息
                'Gender':Jit.AM.getPageParam("OrderGender")||"1",
                'Longitude':Jit.AM.getPageParam("Longitude"),
                'Latitude':Jit.AM.getPageParam("Latitude"),							
                'DeviceList':[{
                	DeviceID:1,
					DeviceName:"格力",
					InstallPosition:"1",
					DeviceCount:1
                }]
            },
            beforeSend: function () {
                Jit.UI.Loading();
            },
            success: function (data) {
            	if(data.ResultCode==0){
            		 Jit.UI.Loading(false);
            		 debugger;
            		if(callback&&typeof callback=="function"){
            			callback(data);
            		}
            	}
            }
        });
    },
	alert:function(text,callback){
		Jit.UI.Dialog({
			'content' : text,
			'type' : 'Alert',
			'CallBackOk' : function() {
				Jit.UI.Dialog('CLOSE');
				if(callback){
					callback();
				}
			}
		});
	}
	
});
