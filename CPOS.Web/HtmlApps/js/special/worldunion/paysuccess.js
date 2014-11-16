Jit.AM.defindPage({

    name: 'PaySuccess',
    elements:{},
    onPageLoad: function() {
        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
		
		this.type = JitPage.getUrlParam("type");
		this.retStatus = JitPage.getUrlParam("retStatus");
		this.retMsg = JitPage.getUrlParam("retMsg");
		
		$("#retMsg").html( decodeURIComponent(this.retMsg) );
		
		//alert(this.type+"|"+this.retStatus+"|"+this.retMsg);
		if(this.type==1||this.type ==2){
			$("#checkMore").html("浏览我的房产").show().click(function(){
				JitPage.toPageWithParam('MyHousesList');
			});
		}else if(this.type == 3){
			$("#checkMore").html("浏览更多楼盘").show().click(function(){
				JitPage.toPageWithParam('HousesList');
			});
		}
		
    }, //绑定事件
    initEvent: function() {
      
    }
   

});