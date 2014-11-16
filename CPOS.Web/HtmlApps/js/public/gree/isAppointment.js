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
       //查看师傅
       $("#selectShifu").bind(this.eventType,function(){
       		that.toPage("SelectShifu");
       });
    },
    //定时器
    changeTime:function(selector){
    	var that=this;
    	var count=60;
    	var result=3;
    	this.timerId=setInterval(function(){
    		count--;
    		if(count==0){
    			clearInterval(that.timerId);
    			count=60;
    			$("#running").hide();
    			$("#success").show();
    		}else{
    			result+=Math.floor(Math.random(10)*10+1);
    			$("#num").html(result);
    			$(selector).html(count+"秒");	
    		}
    		
    	},1000);
    	//定时去获得有多少人接单
    	this.showOrders();
    },
    //一直轮询服务器获得接单的数据
    showOrders:function(){
    	var that=this;
    	var result=0;
    	this.orderTimerId=setInterval(function(){
    		that.getApplyCount(function(data){
    			if(data.Data.Count>0){
    				$("#running").hide();
    				$("#success").show();
    				$("#snum").html(data.Data.Count);	
    			}
    				
    		});
    		
    	},10000);
    },
    //获得接单人数
    getApplyCount:function(callback){
    	var that=this;
		var serviceNo=Jit.AM.getPageParam("ServiceOrderNo");	
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'GetApplyCount',
                'ServiceOrderNO': serviceNo				//性别

            },
            beforeSend: function () {
            },
            success: function (data) {
            	if(data.ResultCode==0){
            		 Jit.UI.Loading(false);
            		if(callback&&typeof callback=="function"){
            			callback(data);
            		}
            	}
               

            }
        });
    },
    initPage:function(){
		this.changeTime("#time");
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
