Jit.AM.defindPage({
    name: 'appointment',
    elems:{},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initPage();
        this.initEvent();
        this.DateTimeEvent();
        var param=this.pageParam;
    }, //加载数据
    //绑定事件
    initEvent: function() {
       var that=this;
       $("#date,#time").bind(this.eventType,function(){
       		that.DateTimeEvent();
       });
       //选择性别
       $(".sex").delegate("span",this.eventType,function(){
       		var $this=$(this);
       		if(!$this.hasClass("on")){
       			$this.addClass("on").siblings().removeClass("on");
       		}
       });
       //获得订单
       this.elems.getOrder.bind(this.eventType,function(){
       		if($("#orderNo").val().length==0){
       			that.alert("订单号不能为空!");
       			return;
       		}
       		that.getOrderData($("#orderNo").val(),function(data){
       			that.elems.name.val(data.Data.CustomerName);
	       		that.elems.address.val(data.Data.ServiceAddress);
	       		that.initMap(data.Data.ServiceAddress);
	       		//将订单号保存在本地
	       		Jit.AM.setPageParam("OrderNo",$("#orderNo").val());
       		});
       		
       });
       //地址改变
       this.elems.address.bind("blur",function(){
       		that.initMap(that.elems.address.val());
       });
       //开始预约
       $("#appoint").bind(this.eventType,function(){
       	
       	  //预约日期
	      Jit.AM.setPageParam("OrderTime",that.elems.getDate.val()+" "+that.elems.getTime.val());
    	  Jit.AM.setPageParam("OrderName",that.elems.name.val());
    	  Jit.AM.setPageParam("OrderAddress",that.elems.address.val());
       	  Jit.AM.setPageParam("OrderMsg",$("#msg").val());
       	  Jit.AM.setPageParam("OrderGender",$(".selSexBox span").find(".on").data("gender"));
       	 Jit.AM.setPageParam("ServiceOrderNo",$("#orderNo").val());
       		that.alert("预约成功!2秒自动关闭提示!");
       		setTimeout(function(){
       			Jit.UI.Dialog('CLOSE');
       			that.toPage("SelectAir");
       		},2000);
       });
    },
    initPage:function(){
    	var currentDate=this.currentTime();
    	
    	//获得订单按钮
    	this.elems.getOrder=$("#getOrder");
    	this.elems.getDate=$("#txtDate");
    	this.elems.getTime=$("#txtTime");
    	this.elems.name=$("#name");
    	this.elems.address=$("#address");
    	this.elems.getDate.val(currentDate.date);
    	this.elems.getTime.val(currentDate.time);
    },
    //获得订单
    getOrderData:function(OrderNo,callback){
    	var that=this;
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'GetOrder',
                'OrderNO': OrderNo
                
            },
            beforeSend: function () {
                Jit.UI.Loading();
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
    initMap:function(address){
    	var that=this;
    	//根据地址获得经纬度
    	$.ajax({
    		type:"get",
    		 url:"http://api.map.baidu.com/geocoder/v2/?address="+address+"&output=json&ak=33803ef538063a4592c943d49cd76767",
    		 dataType : "jsonp",
	         jsonp: "callback",
	         success:function(data){
	         	var result=data.result;
				debugger;
	         	if(result.location){
		         	//经度 lng
		         	var lng=result.location.lng;
		         	//维度  lat
		         	var lat=result.location.lat;
		         	Jit.AM.setPageParam("Longitude",lng);
		         	Jit.AM.setPageParam("Latitude",lat);
		         	// 百度地图API功能
					var map = new BMap.Map("allmap");            // 创建Map实例
					var point = new BMap.Point(lng, lat);    // 创建点坐标
					map.centerAndZoom(point,15);                     // 初始化地图,设置中心点坐标和地图级别。
					map.addControl(new BMap.ZoomControl());  
		         }else{
		         	that.alert("未能正确定位地址!2秒自动关闭提示!");
		       		setTimeout(function(){
		       			Jit.UI.Dialog('CLOSE');
		       		},2000);
		         }
	         }
    	});
    	
    },
    currentTime:function(){ 
        var now = new Date();
        var year = now.getFullYear();       //年
        var month = now.getMonth() + 1;     //月
        var day = now.getDate();            //日
       
        var hh = now.getHours();            //时
        var mm = now.getMinutes();          //分
       
        var clock = year + "-";
       
        if(month < 10)
            clock += "0";
       
        clock += month + "-";
       
        if(day < 10)
            clock += "0";
           
        clock += day + " ";
       var time="";
        if(hh < 10)
            time += "0";
           
        time += hh + ":";
        if (mm < 10) time += '0'; 
        time += mm; 
        return {date:clock,time:time}; 
    },
    DateTimeEvent : function() {
		//日期事件
		var self = this, 
			currYear = (new Date()).getFullYear(),
			opt = {};
		//opt.datetime = { preset : 'datetime', minDate: new Date(2014,3,25,15,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
		opt.datetime = {preset : 'datetime'};
		opt.date = {
			preset : 'date',
			minDate:new Date()
		};
		opt.time = {
			preset : 'time',
			minDate:new Date()
		};
		opt["default"] = {
			theme : 'android-ics light', //皮肤样式
			display : 'modal', //显示方式
			mode : 'scroller', //日期选择模式
			lang : 'zh',
			startYear : 1900, //开始年份
			endYear : (currYear + 30), //结束年份,
			CallBack : function(a,b,c) {
			}
		};

		$("#txtDate").mobiscroll().date(opt['default']);
		$("#txtTime").mobiscroll().time(opt['default']);
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
