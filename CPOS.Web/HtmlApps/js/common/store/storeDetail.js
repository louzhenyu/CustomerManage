Jit.AM.defindPage({

	name:'CateBill',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		this.storeId = JitPage.getUrlParam("storeId");
		if(!this.storeId){
			this.hideMask();
			this.alert("获取不到storeId,请检查URL！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		
		this.loadPageData();
		this.initPageEvent();
		
	},
	loadPageData:function(callback){
		var self =this;
		/*页面异步数据请求_获取酒店详细信息*/
        self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': self.storeId
            },
            beforeSend: function () {
                //Jit.UI.Masklayer.show();
            },
            success: function (data) {
                if (data.code == 200) {
                	if(callback){
                		callback();
                	}else{
                		//信息
						$("#infoArea").html(template.render('tplInfo',data.content));
						// slider heroImage						
						$("#touchslider").html(template.render('tplSlider',{list:data.content.imageList}));
						
						
						$("#touchslider").touchSlider({
							container: this,
							duration: 350, // the speed of the sliding animation in milliseconds
							delay: 2000, // initial auto-scrolling delay for each loop
							margin: 10, // borders size. The margin is set in pixels.
							mouseTouch: true,
							namespace: "touchslider",
							pagination: ".jsNavItem",
							currentClass: "on", // class name for current pagination item.
							autoplay: true, // whether to move from image to image automatically
							viewport: ".touchslider-viewport"
						});
                	}
                }else{
                	self.alert(self.description);
                }
            },
            complete:function(){
            	//Jit.UI.Masklayer.hide();        	
            	self.hideMask();
            }

        });

	},
	renderPage:function(data){
		$("#sectionInfo").html(template.render('sectionInfoTemp',data));
		this.hideMask();
	},
	initPageEvent:function(){
		var self = this;
		$("#infoArea").delegate(".jsLocation","tap",function(){
			var $this =$(this);
			Jit.AM.toPage('Map', "storeId=" + $this.data("storeid") + "&lng=" + $this.data("lng") + "&lat=" + $this.data("lat") + "&addr=" + $this.data("address") + "&store=" + $this.data("storename"));
		}).delegate(".jsMoreBtn","tap",function(){
			var $this =$(this);
			if($this.prev()[0].style.height=="auto"){
				$this.prev()[0].style.height="";
			}else{
				$this.prev()[0].style.height="auto";
			}
			$this.toggleClass("on");
		}).delegate(".jsBookOnline","tap",function(){
			self.alert("敬请期待");
		});
		
		
	},
	alert:function(text,callback){
    	Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
    }
});