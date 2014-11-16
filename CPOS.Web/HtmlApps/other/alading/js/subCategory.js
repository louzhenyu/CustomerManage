define(['template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            categoryList:$("#categoryList")
        },
        init: function () {
        	this.url = "http://api.aladingyidong.com/Gateway.ashx";
        	this.urlParam = 'Action=GetMallItemCategories&ReqContent={"CityID":21,"deviceToken":"747d1c07cd25190ca6e533ef10a5bf135d645a66f6c4b26cd46dca4f8117a271","UserID":"aa7c90bba10b44a79519cec5a7478b0b","Platform":"2","Parameters":{},"channel":"7","Locale":1,"plat":"iphone"}';

        	this.categoryId = $.util.getUrlParam("categoryId");
        	if(!this.categoryId){
                alert("获取不到categoryId！");
                return false;
            }

        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams:function(categoryId,paramString){
            categoryId = categoryId||this.categoryId;
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
            reqContent.Parameters.ParentID = categoryId;
            
            params.ReqContent = JSON.stringify(reqContent);
            var str="Action="+params.Action+"&ReqContent="+params.ReqContent;
            return str;
        },
        loadData: function () {
        	$.ajax({
                url: this.url+"?"+this.buildAjaxParams(),
                beforeSend:function(){
                	self.isSending = true;
                    $.native.loading.show();
                },
                success: function (data) {
                    if (data.ResultCode ==200) {
						self.renderCategoryList(data.Data.CategoryInfos);
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
			window.onscroll = function(){
                //console.log($.util.getScrollTop()+"---"+$.util.getWindowHeight()+"---"+$.util.getScrollHeight());
                if($.util.getScrollTop() + $.util.getWindowHeight()+50 >= $.util.getScrollHeight()){
                    $.native.uppull.hide();
                }
            };
        },
        renderCategoryList: function (list) {
    		this.ele.categoryList.html(template.render('tplListItem',{list:list}));
        }
    };
    self = page;

    page.init();
});