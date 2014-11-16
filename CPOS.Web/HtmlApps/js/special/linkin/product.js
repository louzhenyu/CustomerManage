Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    isCloseText:false,  //是否手动关闭title介绍
    moduleName:{
    	module2:"品牌一点通",
    	module3:"B2B组合",
    	module4:"市场活动",
    	module5:"O2O组合",
    	module6:"客户口碑",
    	module7:"黄金销售"
    },
    initEvent:function(){
        var me=this;
        //点击播放按钮开始播放
    	$(".status").bind(this.eventType,function(){
    		$("#video").show();
    		$(".picWrap").find("img").hide();
    		$("#video")[0].play();
    		$(this).hide();
    		$("#titleDesc").hide();
    	});
    	//类别切换
    	$(".headBar").delegate(".tit",this.eventType,function(){
    		var $this=$(this);
    		$this.addClass("on");
    		//为了解决video上面的层点击响应的问题
    		//设置video隐藏   让封面图片显示
    		$("#video").hide();
    		$(".picWrap").find("img").show();
    		$(".status").show();
    		
    	});
    	$(".menuList").delegate("a",this.eventType,function(){
    		var $this=$(this);
    		var first=$(".headBar .first");
    		var moduleType=$this.data("type");
    		if(moduleType==first.data("type")){
    			first.parent().removeClass("on");
    			return false;
    		}else{
    			me.moduleType=moduleType;
	    		me.getVideos();
	    		first.html($this.html());
	    		first.data("type",moduleType);
	    		first.parent().removeClass("on");
	    		return false;
    		}
    		
    	});
    	
    	//监听播放事件
    	$("#video").bind("play",function(){
    		$(".status").hide();
    		$("#titleDesc").hide();
    	});
    	//监听播放完成事件
    	$("#video").bind("ended",function(){
    		if(!me.isIphone){
    			$(".status").show();
    		}
    		if(!me.isCloseText){
    			$("#titleDesc").show();
    		}
    		$(this).attr("poster",$(this).attr("poster")+"?v="+Math.random());
    	});
    	//监听关闭简介事件
       $(".textClose").bind(this.eventType,function(){
       		$(this).parent().hide();
       		me.isCloseText=true;
       });
       //点击视频播放
       $("#moreVideo").delegate(".item",this.eventType,function(){
       		var $this=$(this);
       		$this.siblings().removeClass("itemActive");
       		$this.addClass("itemActive");
       		var videoURL=$this.data("video"),
       			poster=$this.data("poster"),
       			intro=$this.data("intro");
       		if(videoURL.indexOf("?")>0){
       			videoURL+="&__r="+Math.random();
       		}else{
       			videoURL+="?__r="+Math.random();
       		}
       		if(poster.indexOf("?")>0){
       			poster+="&__r="+Math.random();
       		}else{
       			poster+="?__r="+Math.random();
       		}
       		$("#video").attr("src",videoURL);
       		$("#video").attr("poster",poster);
       		var title=$this.find(".tit").html();
       		$(".sloganBox").find("p").remove();
       		$(".sloganBox").append("<p class='mtmb'>"+intro+"</p>");
	        $("#titleDesc").find("p").remove();
	        $("#titleDesc").append("<p>"+title+"</p>");
       });
    },
    //获得视频
    getVideos:function(){
        var moduleType=this.moduleType;
        this.ajax({
            url:"/ApplicationInterface/LEventsAlbum/LEventsAlbumGateway.ashx",
            data:{
                "action":"GetEventsAlbumList",
                 "ModuleType":moduleType  
            },
            success:function(data){
                console.log(data);
                if(data.ResultCode==0){
                	//多个视频
                	if(data.Data.EventsAlbumList&&data.Data.EventsAlbumList.length){
                		var firstVideo=data.Data.EventsAlbumList[0];
                		var video=$("#video");
	                    video.attr("poster",firstVideo.ImageUrl);
	                    video.attr("src",firstVideo.VideoURL);
	                    $(".sloganBox").find("p").remove();
	                    $("#titleDesc").find("p").remove();
	                    $(".sloganBox").append("<p class='mtmb'>"+firstVideo.Intro+"</p>");
	                    $("#titleDesc").append("<p>"+firstVideo.Title+"</p>");
	                    var html=bd.template("tpl_video_item",{list:data.Data.EventsAlbumList});
	                    $("#moreVideo").html(html);//更多视频
	                    $(".picWrap").find("img").attr("src",firstVideo.ImageUrl);
                	}
                    
                }else{
                    Jit.UI.Dialog({
                        'content': data.Message,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });
    },
    initPage:function(){
        this.initEvent();
        var isIphone= navigator.userAgent.indexOf('iPhone') > -1; 
        this.isIphone=isIphone;
        //是ios则把播放按钮给隐藏
        if(isIphone){
        	$(".status").hide();
        }
        var moduleType=this.getUrlParam("moduleType");
        this.moduleType=moduleType;
        $(".headBar .first").html(this.moduleName["module"+moduleType]);
        this.getVideos();
    }
});