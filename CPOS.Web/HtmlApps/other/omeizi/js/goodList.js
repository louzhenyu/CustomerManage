define(['jquery', 'bdTemplate', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            goodList:$("#goodList") 
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
        	
        	this.url = "/OnlineShopping/data/Data.aspx?Action=getItemList";
        	this.customerId=window._customerId=$.util.getUrlParam("customerId");
        	this.itemTypeId=$.util.getUrlParam("itemTypeId");
        	this.isSending = false;
        	this.noMore = false;
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams:function(){
        	var urlstr = window.location.href.split("?"),
            params = {};
	        if (urlstr[1]) {
				var items = decodeURIComponent(urlstr[1]).split("&");
				for (var i = 0; i < items.length; i++) {
					itemarr = items[i].split("=");
					params[itemarr[0]] = itemarr[1];
				}
			}
			
			var reqContent={};
			var ReqContent={"common":{"isALD":"0",
	        	"channelId":"-1","userId":"",
	        	"customerId":this.customerId,
	        	"plat":"iPhone","locale":"zh"
        	},
        	"special":{
        		"itemTypeId":this.itemTypeId
        		}
        	}
        	
			reqContent=ReqContent;
			
			params.ReqContent = encodeURIComponent(JSON.stringify(reqContent));
			var str="ReqContent="+params.ReqContent;
			
			return this.url+"&"+str
        },
        
        loadData: function () {
        	$.ajax({
                url: this.buildAjaxParams(),
                beforeSend:function(){
                	self.isSending = true;
                    //if(self.page.pageIndex!=0){
                        $.native.loading.show();
                    //}
                },
                success: function (data) {
                	data=JSON.parse(data);
                    if (data.code ==200) {
                    	//判断是否有多页的
                    	if(data.content.itemList.length!=self.page.pageSize){
                    		self.noMore=true;
                    	}
						self.renderPageList(data.content.itemList);
                    } else {
                        alert(data.Message);
                    }
                },
                error:function(){
					alert("加载数据失败！")                	
                },
                complete:function(){
                	self.isSending = false;
                    $.native.loading.hide();
                    
                    setTimeout(function(){
                        if(self.page.pageIndex==0){
                            $.native.downpull.hide();
                        }else{
                            $.native.uppull.hide();
                        }  
                    },10);
                }
            });
        },
        initEvent: function () {
			window.onscroll = function(){
                console.log($.util.getScrollTop()+"---"+$.util.getWindowHeight()+"---"+$.util.getScrollHeight());
				if($.util.getScrollTop() + $.util.getWindowHeight()+50 >= $.util.getScrollHeight()){
			　　　　console.log("you are in the bottom!");
                    //alert("you are in the bottom!");
					if(!self.noMore&&!self.isSending){
						self.page.pageIndex++;
						self.loadData();
					}else{
                        $.native.uppull.hide();
                    }
			    }
			};
        },
        renderPageList: function (list) {
        	if(self.page.pageIndex==0){
        		this.ele.goodList.html(bd.template('tplListItem',{list:list}));
        	}else{
        		this.ele.goodList.append(bd.template('tplListItem',{list:list}));
        	}
            
        }
    };
    self = page;

    page.init();
});