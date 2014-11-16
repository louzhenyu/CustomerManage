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
       $(".sifuList").delegate(".taBtn",this.eventType,function(){
       		var $this=$(this);
       		if($(".sifuList").find("[data-sel=true]").length==0){
	       		$this.css("background","gray");
	       		$this.html("已选择");
	       		$this.attr("data-sel",true);
	       		var personId=$this.data("personid");
	       		//通知数据端
	       		that.selectPerson(personId,function(data){
	       			if(data.Data.IsSuccess){
	       				that.alert("选择成功！2秒关闭提示");
			       		setTimeout(function(){
			       			Jit.UI.Dialog('CLOSE');
			       		},2000);
	       			}else{
	       				that.alert(data.Message);
	       				$this.css("background","#39b44a");
	       				$this.html("就他了");
	       				$this.attr("data-sel",false);
	       			}
	       		});
	       	}else{
	       		that.alert("已选择了师傅，不能再选了！2秒关闭提示");
	       		setTimeout(function(){
	       			Jit.UI.Dialog('CLOSE');
	       		},2000);
	       	}
       });
    },
     //获得接单人数列表
    getReceiveMaster:function(callback){
    	var that=this;
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'GetReceiveMaster',
                'ServiceOrderNO': Jit.AM.getPageParam("ServiceOrderNo")					//性别

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
    //选择师傅
    selectPerson:function(personId,callback){
    	
    	var that=this;
    	//http://192.168.1.43:9004/HtmlApps/html/public/gree/shifuTask.html?customerId=gree&personId=1&version=1404368492601
    	var url="http://"+location.host;
    	var baseInfo=Jit.AM.getBaseAjaxParam();
    	url+="/HtmlApps/html/public/gree/shifuTask.html?customerId="+baseInfo.customerId+"&personId="+personId;
    	//数据请求
        this.ajax({
            url: '/ApplicationInterface/Project/Gree/GreeHandler.ashx',
            'interfaceMode':"V2.0",  
            data: {
                'action': 'SelectedServicePerson',
                'ServiceOrderNo': Jit.AM.getPageParam("ServiceOrderNo"),
                'PersonID':personId,
                'RetUrl':url
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
    initPage:function(){
		this.getReceiveMaster(function(data){
			var list=data.Data.ServicePersonList;
			list=(!!!list)?[]:list;
			var html=bd.template("shifu_tpl",{list:list});
			$(".sifuList").html(html);
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
