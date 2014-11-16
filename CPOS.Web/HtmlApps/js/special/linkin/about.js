Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    initEvent:function(){
        var me=this;
        //点击播放按钮开始播放
    	$(".status").bind(this.eventType,function(){
    		$("#video")[0].play();
    		$(this).hide();
    		$("#titleDesc").hide();
    	});
    	//监听播放事件
    	$("#video").bind("play",function(){
    		try{
    			$("#video")[0].webkitRequestFullScreen();
    		}catch(e){}
    		$(".status").hide();
    		$("#titleDesc").hide();
    	});
    	//监听暂停事件
    	$("#video").bind("pause",function(){
    		// $(".status").show();
    		// if(!me.isCloseText){
    			// $("#titleDesc").show();
    		// }
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
    	//监听加载更多视频事件
       $(".foldIcon").bind(this.eventType,function(){
       		$("#moreDesc").addClass("on");
       });
    },
    //获得二维码
    getVideos:function(){
		var moduleType=this.getUrlParam("moduleType");
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
	                    $(".sloganBox").append("<p class='mtmb'>"+firstVideo.Title+"</p>");
	                    $("#titleDesc").append("<p>"+firstVideo.Intro+"</p>");
	                    //$("#moreDesc").append("<p>"+firstVideo.Intro+"</p>");
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
	onBgSize: function() {
		var w = document.body.offsetWidth;
		$("#picWrapper").css('width', w);
		$("#picWrapper").find('li').css('width', w);

	},
	//背景图片滑动
	onImgSlider: function() {
		var myScroll = new iScroll('picWrapper', {
			snap: true,
			momentum: false,
			hScrollbar: false,
			onScrollEnd: function() {
			//document.querySelector('#indicator > li.active').className = '';
			//document.querySelector('#indicator > li:nth-child(' + (this.currPageX+1) + ')').className = 'active';
		}
		});
		
	},
    initPage:function(){
		window.onresize = this.onBgSize;
		this.onBgSize();
		this.onImgSlider();
        this.getVideos();
        this.initEvent();
        var isIphone= navigator.userAgent.indexOf('iPhone') > -1; 
        this.isIphone=isIphone;
        //是ios则把播放按钮给隐藏
        if(isIphone){
        	$(".status").hide();
        }
    }
});
