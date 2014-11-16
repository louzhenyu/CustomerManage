define(['jquery', 'template', 'tools'], function () {
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
        	this.url = "http://"+location.host+"/OnlineShopping/data/Data.aspx?Action=getItemList";
        	this.customerId=window._customerId=$.util.getUrlParam("customerId");
        	this.itemTypeId=$.util.getUrlParam("itemTypeId");
        	this.param = location.href.split("?")[1];
        	if(!this.param){
        		alert("获取不到请求参数！");
        		return false;
        	}
        	
        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams:function(){
        	var urlstr = window.location.href.split("?"),
            params = {};
	        if (urlstr[1]) {
				var items = urlstr[1].split("&");
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
        		"itemTypeId":this.itemTypeId,
        		 "PageIndex":this.page.pageIndex,
        		 "PageSize":this.page.pageSize
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
                	debugger;
                	data=JSON.parse(data);
                   if (data.code ==200) {
                    	if(data.content.itemList.length!=self.page.pageSize){
                    		self.noMore=true;
                    	}
						self.renderPageList(data.content.itemList);
                    } else {
                        alert(data.Message);
                    }
                },
                error:function(){
					alert("加载数据失败！");
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
				if($.util.getScrollTop() + $.util.getWindowHeight() == $.util.getScrollHeight()){
			　　　　	//console.log("you are in the bottom!");
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
        		this.ele.goodList.html(template.render('tplListItem',{list:list,customerId:this.customerId}));
        	}else{
        		this.ele.goodList.append(template.render('tplListItem',{list:list,customerId:this.customerId}));
        	}
            
        }
    };
    self = page;

    page.init();
});