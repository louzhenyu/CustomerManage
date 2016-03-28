define(['jquery','tools','template', 'kindeditor',"easyui"], function ($) {
    var page = {
        elem:{
            $createSigninQR:$(".createSigninQR"),
			$downloadVipQR:$(".downloadVipQR"),
			$signinQRImg:$(".signinQRImg"),
        },
        //初始化参数
        init: function () {
			var that = this;			/*that.elem.$downloadVipQR.attr('onlick','javascript:window.location.href="'+location.host+'/ApplicationInterface/Module/Market/MarketNameApply/GenerateMarketEventQR.ashx"');*/
			that.elem.$downloadVipQR.attr('href','/ApplicationInterface/Module/Market/MarketNameApply/GenerateMarketEventQR.ashx?type=Product&action=');
            that.initEvent();
        },
        //初始化事件
        initEvent:function(){
			var that = this;
			that.elem.$createSigninQR.bind('click',function(){
				that.createSigninQR();
			});
        },
        // 初始化页面数据
        loadPageData:function(){

        },
        createSigninQR:function(){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Gateway.ashx",
				data:{
					action:"Market.CreateQRCode.DownloadQRCode",
					"VipDCode":9
				},
				success: function (data) {
					if (data.IsSuccess) {
						that.elem.$signinQRImg.html('<img src="' + data.Data.imageUrl + '" />');

					} else {
						$.messager.alert("操作失败提示",data.Message);
					}
				},error: function(data) {
					$.messager.alert("操作失败提示",data.Message);
				}
			});
		}

    };
    page.init();
}) ;
