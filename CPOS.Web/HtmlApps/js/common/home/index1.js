Jit.AM.defindPage({

    name: 'spaIndex',
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initEvent();
        var param=this.pageParam;
        //初始化页面内容
		if(param){
			//设置背景图
			if(param.backgroundImg){
				$("#theBg").attr("src",param.backgroundImg);
			}			
			//菜单项
		    if(param.links){
				var navs=$("#menu a");
				var length=param.links.length;
				for(var i=0;i<length;i++){
					var item=param.links[i];
					var doma=$(navs[i]);
					if(item.backgroundColor){
						doma.css({
							"background":item.backgroundColor,
							"opacity":0.6
						});
					}
					doma.html(item.title);
					doma.attr("href",item.toUrl);
				}
			}
		}
        $(".menu a").addClass("tm");
    }, //加载数据
    //绑定事件
    initEvent: function() {
       
    }
	
});
