Jit.AM.defindPage({

    name: 'spaIndex',
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initEvent();
        $(".menu a").addClass("tm");
    }, //加载数据
    //绑定事件
    initEvent: function() {
        var i=0;
        $("#changeBg").click(function(){
        	i++;
        	if(i<=2){
        		if(i==0){
        			i="";
        			$("#theBg").attr("src","../../../images/public/spa_default/indexBg"+i+".jpg");
        			i=0;
        		}else{
        			$("#theBg").attr("src","../../../images/public/spa_default/indexBg"+i+".jpg");
        		}
        	}else{
        		i=-1;
        	}
        });
    }
	
});
