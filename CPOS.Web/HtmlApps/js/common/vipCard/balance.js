Jit.AM.defindPage({
     onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入'+this.name);
        this.isSending = false;

        //传递页面的内容
        var num=this.getUrlParam("money")||0;
        $("#money").html(decodeURIComponent(num));
        this.loadData();
        this.initEvent();
    },
    ele:{
        balanceList:$("#dataList"),
        total:$("#total")
    },
    page:{
        pageIndex:0,
        pageSize:10,
        allPage:2
    },
    loadData: function () {
    	this.getBalance();
    },
    reachBottom:function(vars) {
	    var scrollTop = 0;
	    var clientHeight = 0;
	    var scrollHeight = 0;
	    if (document.documentElement && document.documentElement.scrollTop) {
	        scrollTop = document.documentElement.scrollTop;
	    } else if (document.body) {
	        scrollTop = document.body.scrollTop;
	    }
	    if (document.body.clientHeight && document.documentElement.clientHeight) {
	        clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight: document.documentElement.clientHeight;
	    } else {
	        clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight: document.documentElement.clientHeight;
	    }
	    scrollHeight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
	    if (scrollTop + clientHeight>= scrollHeight-vars) {
	        return true;
	    } else {
	        return false;
	    }
	},
    initEvent:function(){
    	var me=this;
    	$(window).bind("scroll",function(){
    		if(me.reachBottom(100)){
                if(!me.isSending){
                    me.page.pageIndex++;
                    me.getBalance();
                }

    		}
    	});
    },
    getBalance:function(){
        var self = this;
    	if(self.page.pageIndex>self.page.allPage-1){
            return false;
        }
        this.ajax({
            url: '/ApplicationInterface/Services/ServiceGateway.ashx',
            data: {
                'action': 'GetMyAccount',
                'PageIndex':self.page.pageIndex,
                'PageSize':self.page.pageSize
            },

            beforeSend:function(){
                Jit.UI.Loading(true);
                self.isSending = true;
            },
            success: function (data) {


                if (data && data.IsSuccess) {
                    self.page.allPage = data.Data.TotalPageCount;
                    self.ele.total.html(data.Data.Total);
                    if(data.Data.AccountList){
                        var htmlList = template.render('tplList', {'list': data.Data.AccountList});
                        if(self.page.pageIndex==0){
                            self.ele.balanceList.html(htmlList);
                        }else{
                            self.ele.balanceList.append(htmlList);
                        }
                    }else{
                        if(self.page.pageIndex==0){
                            self.ele.balanceList.html('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">没有查找到您的余额信息!</div>');
                        }
                    }


                }else{
                    self.ele.balanceList.html('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">'+data.Message+'</div>');
                }
            },
            complete:function(){
                self.isSending = false;
                Jit.UI.Loading(false);
            }
        });
	 }
});