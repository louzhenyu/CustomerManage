define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section"),
            storeInfo:$("#storeInfo"),
            moduleList:$("#moduleList")
        },
        init: function () {
        	this.url = "/ApplicationInterface/Module/UnitAndItem/Item/Item.ashx";
        	this.customerId = $.util.getUrlParam("customerId")
        	if(!this.customerId){
        		alert("获取不到customerId！");
        		return false;
        	}
        	
        	this.isSending = false;
        	this.noMore = false;
        	
            this.loadData();
            
            this.initEvent();
        },
        buildAjaxParams:function(){
        	var urlstr = 'type=Product&action=getItemHomePageList&req={"Locale":null,"CustomerID":"679e8361b250487d8fdd07791dced25d","UserID":null,"OpenID":null,"Token":null,"Parameters":null}',
            params = {};
	        if (urlstr) {
				var items = decodeURIComponent(urlstr).split("&");
				for (var i = 0; i < items.length; i++) {
					itemarr = items[i].split("=");
					params[itemarr[0]] = itemarr[1];
				}
			}
			
			var reqContent = JSON.parse(decodeURIComponent(params.req));
			reqContent.CustomerID = this.customerId;
			
			params.req = encodeURIComponent(JSON.stringify(reqContent));
			var str="type=Product&action=getItemHomePageList&req="+params.req;
			
			return this.url+"?"+str;
        },
        
        loadData: function () {
        	$.ajax({
                url: this.buildAjaxParams(),
                type:"get",
                dataType:"json",
                beforeSend:function(){
                	self.isSending = true;
                    $.native.loading.show();
                },
                success: function (data) {
                    if (data.IsSuccess) {
						self.render(data.Data);
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
                }
            });
        },
        initEvent: function () {
			$("#section").delegate(".shareBtn","click",function(){
                $.native.share("store",self.customerId);
            }).delegate(".favoriteBtn","click",function(){
                var $this = $(this);
                
                $.native.favorite("store",self.customerId,self.favoriteFlag,function(data){
                    if(data){
                        $this.children("i").toggleClass("on");
                        self.favoriteFlag = !self.favoriteFlag;
                    }
                });
                
            });
        },
        render: function (data) {
            //var obj = {"ResultCode":0,"Message":"OK","IsSuccess":true,"Data":{"StoreTopBackGroundUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131030/F94E15F1124A4BD6BFBC8E624418C819.jpg\r\n","StoreLogoUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131205/FE0BE81391BA465FB6B67335C9A69B02.jpg","CountItem":15,"NewCountItem":0,"ModuleList":[{"ModuleName":"热卖商品","GoodListInfo":[{"ItemDescription":"Airpal 450 空气净化器","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131217/0B6217A867464E4288D2886A982C16D0.jpg","CostPrice":9800,"SalesPrice":9310},{"ItemDescription":"Airpal 200 空气净化器","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131217/BB3C5012A6014FC884EA5219F93A9A83.jpg","CostPrice":2980,"SalesPrice":2831},{"ItemDescription":"Airpal 礼包5:买满13080立减4138！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/7BEB193340DA4F42BFDB490ADA717524.jpg","CostPrice":13080,"SalesPrice":8942},{"ItemDescription":"Airpal 礼包2（450替换装）：买满17340立减3944！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/1BF8B4FA7F5D42049C2258395B645AAE.jpg","CostPrice":17340,"SalesPrice":13396},{"ItemDescription":"Airpal 礼包4:买满7240立减2174！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/FA3394C30BC34D6388E542CB43F244C5.jpg","CostPrice":7240,"SalesPrice":5066},{"ItemDescription":"Airpal 礼包1（450替换装）:买满14060立减3197！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/03521F87756D4BD4AA02697A15911DFE.jpg","CostPrice":14060,"SalesPrice":10863},{"ItemDescription":"Airpal 礼包3:买满24600立减6121！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/611751A0D1DD43668F20869034BE39CE.jpg","CostPrice":24600,"SalesPrice":18479},{"ItemDescription":"Airpal 420pro 空气净化器","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131217/C1D875C515934AA48B072EE65697DC3D.jpg","CostPrice":12800,"SalesPrice":12160},{"ItemDescription":"Airgle 950：买满15666立减2350！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/DF375CA53EEC4577A5BD9553CE4CD1C1.jpg","CostPrice":15666,"SalesPrice":13316},{"ItemDescription":"Airgle 800:买满12666立减1900！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/FECDF265E45B476C94B4AFC3269325E6.jpg","CostPrice":12666,"SalesPrice":10766},{"ItemDescription":"Airpal 礼包1:买满17060立减3647！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/94ED12702CB248F38ABC75372AD96F86.jpg","CostPrice":0,"SalesPrice":13413},{"ItemDescription":"Airpal 礼包2:买满20340立减4394！","ItemUrl":"","CostPrice":20340,"SalesPrice":15946},{"ItemDescription":"Airpal 礼包3（450替换装）:买满21600立减5671！","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20140119/CF58F35D86E74237B8A1A7F48FB64085.jpg","CostPrice":21600,"SalesPrice":15929},{"ItemDescription":"Airpal 045 空气净化器","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131217/C33AA20E69A04DB093907954EB031381.jpg","CostPrice":1580,"SalesPrice":1501},{"ItemDescription":"Airpal 010 车载空气净化器","ItemUrl":"http://bs.aladingyidong.com/Framework/Upload/Image/20131217/9A956C52D4EB4BFD8104E626E803DA8A.jpg","CostPrice":1280,"SalesPrice":1216}]}]}};
    		//debugger;
            if(data){
                this.ele.storeInfo.html(template.render('tplStoreInfo',data));
            }else{
                alert("数据有误");
            }
            
            if(data.ModuleList&&data.ModuleList.length){
                this.ele.moduleList.html(template.render('tplListItem',{list:data.ModuleList}));
            }
            //由native判断是否收藏
            window.setFavoriteStatus = function(data){
                //alert(data);
                if(data){
                    $("#favoriteBtn > i").addClass("on");
                    self.favoriteFlag = true;
                }else{
                    $("#favoriteBtn > i").removeClass("on");
                    self.favoriteFlag = false;
                }
            }
            
        }
    };
    self = page;

    page.init();
});