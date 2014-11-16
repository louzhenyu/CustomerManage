Jit.AM.defindPage({
    name: 'shifuTask',
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
       
    },
     //获得接单人数列表
    getPersonInfo:function(callback){
    	var that=this;
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'GetServicePerson',
                'ServicePersonId': Jit.AM.getUrlParam("personId")					//性别

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
		this.getPersonInfo(function(data){
			var personInfo=data.Data.ServicePerson;
			$("#head").attr("href",personInfo.Picture?personInfo.Picture:"../../../images/public/gree_default/pic003.png");
			$("#name").html(personInfo.Name?personInfo.Name:"");
			$("#star").html(personInfo.Star?personInfo.Star:"");
			$("#phone").html(personInfo.Mobile?personInfo.Mobile:"");
			$("#contact").attr("href",personInfo.Mobile?("tel:"+personInfo.Mobile):"javascript:;");
			$("#orderCount").html(personInfo.OrderCount?personInfo.OrderCount:"");
			$("#todayCount").html(personInfo.TodayOrder?personInfo.TodayOrder:"");
			$("#nice").html(personInfo.Star?personInfo.Star:"");
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
