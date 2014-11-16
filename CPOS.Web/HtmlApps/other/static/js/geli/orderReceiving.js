define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            orderList:$("#orderList"),
            orderListLayer:$("#orderListLayer"),
            orderDetailLayer:$("#orderDetailLayer")
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
        	this.url = "http://o2oapi.dev.aladingyidong.com/ApplicationInterface/Gateway.ashx";
        	
        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams:function(){
        	return this.url;
        },
        
        loadData: function () {
        	$.ajax({
                url: this.buildAjaxParams(),
                beforeSend:function(){
                	self.isSending = true;
                    $.native.loading.show();
                },
                success: function (data) {
                    if (data.ResultCode ==200) {
                    	if(data.Data.ItemInfos.length!=self.page.pageSize){
                    		self.noMore=true;
                    	}
						self.renderPageList(data.Data.ItemInfos);
                    } else {
                        //alert(data.Message);
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

                    // 假数据
                    var arr = [1,2,3,4,5];
                    self.renderPageList(arr);
                }
            });
        },
        initEvent: function () {
			window.onscroll = function(){
                //console.log($.util.getScrollTop()+"---"+$.util.getWindowHeight()+"---"+$.util.getScrollHeight());
				if($.util.getScrollTop() + $.util.getWindowHeight()+50 >= $.util.getScrollHeight()){
					if(!self.noMore&&!self.isSending){
						self.page.pageIndex++;
						//self.loadData();
					}else{
                        $.native.uppull.hide();
                    }
			    }
			};

            this.ele.orderList.delegate(".takeBtn","click",function(){
                self.ele.orderListLayer.hide();
                self.ele.orderDetailLayer.show();
            });

            this.ele.orderDetailLayer.delegate("#takeBtn","click",function(){
                location.href="serviceDetails.html";
            }).delegate(".closeBtn","click",function(){
                self.ele.orderListLayer.show();
                self.ele.orderDetailLayer.hide();
            });
        },
        renderPageList: function (list) {
        	if(self.page.pageIndex==0){
        		this.ele.orderList.html(template.render('tplListItem',{list:list}));
        	}else{
        		this.ele.orderList.append(template.render('tplListItem',{list:list}));
        	}
            
        }
    };
    self = page;

    page.init();
});