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
        	this.url = "http://api.aladingyidong.com/Gateway.ashx";
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
			
			
			var reqContent = JSON.parse(decodeURIComponent(params.ReqContent));
			reqContent.Parameters.PageIndex = this.page.pageIndex;
			reqContent.Parameters.PageSize = this.page.pageSize;
			
			params.ReqContent = encodeURIComponent(JSON.stringify(reqContent));
			var str="Action="+params.Action+"&ReqContent="+params.ReqContent;
			return this.url+"?"+str
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
                    	if(data.Data.GroupPurchases.length!=self.page.pageSize){
                    		self.noMore=true;
                    	}
						self.renderPageList(data.Data.GroupPurchases);
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
				if($.util.getScrollTop() + $.util.getWindowHeight() ==$.util.getScrollHeight()){
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
        		this.ele.goodList.html(template.render('tplListItem',{list:list}));
        	}else{
        		this.ele.goodList.append(template.render('tplListItem',{list:list}));
        	}
            setInterval(function(){
				
				self.timeDown();
			},1000);
        },
        timeDown:function(){
		
			var domlist = $('[time-date]'),endtime,second;
			
			var _h,_m,_s;
			
			domlist.each(function(idx,dom){
				
				endtime = $(dom).attr('time-date');
				
				second = parseInt(endtime);
				
				_h = Math.floor(second/3600);
				
				_m = Math.floor((second%3600)/60);
				
				_s = Math.floor(((second%3600)%60));
				
                //超过99小时全部显示99
                _m = _h>99?99:_m;
                _s = _h>99?99:_s;
                _h = _h>99?99:_h;
                

				$(dom).attr('time-date',endtime-1);
				//console.log(_h+' '+_m+' '+_s);
				
				$(dom).find('[tn=time-h-max]').html( Math.floor(_h/10)>9?9:Math.floor(_h/10));
				
				$(dom).find('[tn=time-h-min]').html( Math.floor(_h%10) );
				
				$(dom).find('[tn=time-m-max]').html( Math.floor(_m/10) );
				
				$(dom).find('[tn=time-m-min]').html( Math.floor(_m%10) );
				
				$(dom).find('[tn=time-s-max]').html( Math.floor(_s/10) );
				
				$(dom).find('[tn=time-s-min]').html( Math.floor(_s%10) );
			});
		}
    };
    self = page;

    page.init();
});

