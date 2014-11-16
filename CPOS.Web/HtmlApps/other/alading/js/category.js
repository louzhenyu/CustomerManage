define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            categoryMainList:$("#categoryMainList")
        },
        init: function () {
        	this.url = "http://api.aladingyidong.com/Gateway.ashx";
        	this.urlParam = 'Action=GetMallItems&ReqContent={"CityID":21,"UserID":"aa7c90bba10b44a79519cec5a7478b0b","deviceToken":"747d1c07cd25190ca6e533ef10a5bf135d645a66f6c4b26cd46dca4f8117a271","Platform":"2","Parameters":{"PageSize":10,"PageIndex":0,"CategoryID":"96eef1be-daa2-4df1-a424-ce08f1bc70ae"},"channel":"7","Locale":1,"plat":"iphone"}';
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
			//reqContent.Parameters.PageIndex = this.page.pageIndex;
			//reqContent.Parameters.PageSize = this.page.pageSize;
			
			params.ReqContent = encodeURIComponent(JSON.stringify(reqContent));
			var str="Action="+params.Action+"&ReqContent="+params.ReqContent;
			return this.url+"?"+str
        },
        buildURL:function(categoryId,paramString){
        	paramString = paramString||this.urlParam;
        	var params = {};
	        if (paramString) {
				var items = paramString.split("&");
				for (var i = 0; i < items.length; i++) {
					itemarr = items[i].split("=");
					params[itemarr[0]] = itemarr[1];
				}
			}
			
			
			var reqContent = JSON.parse(decodeURIComponent(params.ReqContent));
			reqContent.Parameters.CategoryID = categoryId;
			
			params.ReqContent = JSON.stringify(reqContent);
			var str="Action="+params.Action+"&ReqContent="+params.ReqContent;
			return str;
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
						self.renderCategoryList(data.Data);
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
                        $.native.downpull.hide();
                    },10);
                }
            });
        },
        initEvent: function () {
			$("#section").delegate(".mainCategory .categateMainMenu","click",function(e){
				// if($(this).data("id").length){
				// 	location.href="goodList.html?"+self.buildURL($(this).data("id"));
				// }
                //$(this).parent().toggleClass("on").siblings().removeClass("on");
                //debugger;
                $(this).parent().siblings().children(".commonSubList").hide();
                $(this).siblings().toggle(500);
			});
            window.onscroll = function(){
                //console.log($.util.getScrollTop()+"---"+$.util.getWindowHeight()+"---"+$.util.getScrollHeight());
                if($.util.getScrollTop() + $.util.getWindowHeight()+50 >= $.util.getScrollHeight()){
                    $.native.uppull.hide();
                }
            };
        },
        renderCategoryList: function (list) {
    		this.ele.categoryMainList.html(template.render('tplListItem',{list:list}));
        }
    };
    self = page;

    page.init();
});