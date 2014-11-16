Jit.AM.defindPage({
	
	toString:Object.prototype.toString,

	name:'StoreList',
	
	page:{
		'pageIndex':0,
		'pageSize':10
	},
	ele:{
		pullDown:$('#pullDown'),
		pullUp:$('#pullUp'),
		citySelect:$("#citySelect"),
		nameLike:$("#nameLike")
	},
	// hideMask:function(){
		// $("#masklayer").hide();
	// },
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		// 页面参数
		this.storeListPageParams	= 	JitPage.getParams("storeListPageParams");
		this.storeListPageDatas		= 	JitPage.getParams("storeListPageDatas");
		// 坐标定位
		this.coords = this.storeListPageDatas?this.storeListPageDatas.coords:null;
		var self = this;
		self.loadPageData();
		self.initPageEvent();
		
	},
	loadPageData:function(callback){
		var self = this;
		
		//ajax获取顶部显示状态
		this.GetDisPlaynone(function(datas){
			//debugger;
			//判断是否显示顶部搜索栏
			if(datas.Data.IsSearchAccessoriesStores){
				self.GetCityList(function(data){
					var citySelect = document.getElementById("citySelect");
					// 判断是否显示附近和全部
					if(datas.Data.IsAllAccessoriesStores){						
						citySelect.options.add(new Option("附近","-00"));
						citySelect.options.add(new Option("全部","-99"));
					}
					for(var i=0;i<data.Data.CityList.length;i++){
						var idata = data.Data.CityList[i];
						citySelect.options.add(new Option(idata.CityName,idata.Code));
					}
					// 从详情页返回时 选中跳转之前的城市
					if(self.storeListPageParams&&self.storeListPageParams.cityId){
						citySelect.value = self.storeListPageParams.cityId;
					}
					self.filterData();
				});
			}else{
				var citySelect = document.getElementById("citySelect");
				citySelect.options.add(new Option("全部","-99"));
				$("#storeFilter").hide();
				self.filterData();
			}
			
		});
		
	},
	GetCityList:function(callback){
		var self =this;
		self.ajax({
			url:'/ApplicationInterface/Gateway.ashx',
			data:{
				'type':'Product',
				'action':'UnitAndItem.Area.GetCityList'
			},
			success:function(data){
				if(data.IsSuccess){
					if(callback){
						callback(data);
					}
				}else{
					self.alert(data.Message);
				}
			}
		});
		
	},
	GetDisPlaynone:function(callback){
		var self =this;
		self.ajax({
			url:'/ApplicationInterface/Gateway.ashx',
			type:'get',
			data:{
				'action':'UnitAndItem.Unit.GetDisplaynone'
			},
			success:function(data){
				if(data.IsSuccess){
					if(callback){
						callback(data);
					}
				}else{
					self.alert(data.Message);
				}
			}
		});
	},
	SearchStoreList:function(pos,callback){
		var self =this;
		var data={
				'type':'Product',
				'action':'UnitAndItem.Unit.SearchStoreList',
				'NameLike':$("#nameLike").val()||"",
				'CityCode':$("#citySelect").val()||"",
				'Position':pos||"",
				'PageIndex':self.page.pageIndex,
				'PageSize':self.page.pageSize,
				'StoreID':'',
				'IncludeHQ':''
		};
		if(!self.isSending){
			self.ajax({
				url:'/ApplicationInterface/Gateway.ashx',
				type:'get',
				data:data,
				beforeSend:function(){				
					Jit.UI.AjaxTips.Tips({show:false});
					self.isSending =true;
					Jit.UI.Loading(true);
					self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '努力加载中...';
				},
				success:function(data){
					if(data.IsSuccess){
						if(callback){
							callback(data.Data);
						}else{
							if(data.Data.StoreListInfo&&data.Data.StoreListInfo.length){
								if(self.page.pageIndex ==0){
									$("#storeList").html(template.render('tplListItem',{list:data.Data.StoreListInfo}));
								}else{
									$("#storeList").append(template.render('tplListItem',{list:data.Data.StoreListInfo}));
								}
								if(data.Data.StoreListInfo.length==self.page.pageSize){								
									self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '上拉显示更多...';
								}else{				
									self.nomoreData = true;					
									
									if(self.page.pageIndex ==0){
										self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '';
									}else{
										self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '没有更多数据了';
									}
								}
							}else{
								if(self.page.pageIndex ==0){
									self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '';
								}else{
									self.ele.pullUp[0].querySelector('.pullUpLabel').innerHTML = '没有更多数据了';
								}
								self.nomoreData = true;
								if(self.page.pageIndex==0){
									$("#storeList").empty();
									Jit.UI.AjaxTips.Tips({show:true,tips:"该地区暂无门店"});								
								}
							}
							self.refreshIscroll();					
						}
					}else{
						self.alert(data.Message);						
					}
				},
				complete:function(a,b,c){					
					console.log("第"+self.page.pageIndex+"数据请求complete");
					self.isSending =false;
					Jit.UI.Loading(false);
				}
			});
		}
	},
	initPageEvent:function(){
		var self = this;
		$("header").delegate("#searchBtn","tap",function(e){
			self.page.pageIndex = 0;
			self.nomoreData = false;
			self.filterData();
		}).delegate("#nameLike,#citySelect","focus",function(e){
			$("#commonTopBg").animate({ 
				height: "50px"
			}, 100 );
			setTimeout(function(){				
				document.getElementById("commonTopBg").style.height = "";
			},3000);
		}).delegate("#citySelect","change",function(e){
			self.page.pageIndex = 0;
			self.nomoreData = false;
			self.filterData();
		});
		
		$("#storeList").delegate(".storeItem","click",function(e){
			var $this = $(this),
				storeId = $this.data("storeid");
			// 跳页面之前保存状态
			var obj = {};
			obj.cityId = $("#citySelect").val();
			obj.filterContent = $("#nameLike").val();
			JitPage.setParams("storeListPageParams",obj);

			obj = {};
			obj.coords = self.coords;
			JitPage.setParams("storeListPageDatas",obj);

			
			Jit.AM.toPageWithParam("StoreDetail","storeId="+storeId);
		});
		
	},
	filterData:function(){
		var self = this;
		if($("#citySelect").val()=="-00"){
			if(!self.coords){
				self.geoLocation(function(position){					
					self.coords = position.coords;
					self.SearchStoreList(self.coords.longitude+","+self.coords.latitude);
				});		
			}else{
				self.SearchStoreList(self.coords.longitude+","+self.coords.latitude);
			}
					
		}else{
			self.SearchStoreList();				
		}
		JitPage.setParams("storeListPageParams",null);
	},
	pullDownAction:function(){
		this.storeWrapper.refresh();
	},
	pullUpAction:function(callback){
		if(!this.isSending&&!this.nomoreData){
			this.page.pageIndex+=1;
			this.filterData(function(){
				if(callback){
					callback();
				}			
			});
		}else{
			this.storeWrapper.refresh();
			if(callback){
				callback();
			}	
		}
	},
	refreshIscroll:function(){
		var self = this;
		if(null!=document.getElementById("scrollContainer")){
			if (!self.storeWrapper) {
                var pullDownEl = self.ele.pullDown[0];
                var pullUpEl = self.ele.pullUp[0];
                var pullDownOffset = pullDownEl.offsetHeight;
                var pullUpOffset = pullUpEl.offsetHeight;
                
                
				self.storeWrapper = new iScroll('scrollContainer', {
					hScrollbar : false,
					vScrollbar : false,
					onRefresh : function() {
                    	if (pullDownEl.className.match('loading')) {
                            pullDownEl.className = '';
                            //pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                        } else if (pullUpEl.className.match('loading')) {
                            pullUpEl.className = '';
                            //ullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉显示更多...';
                        }
                    },
                    onBeforeScrollStart:function(e){
						var nodeType = e.explicitOriginalTarget ? e.explicitOriginalTarget.nodeName.toLowerCase():(e.target ? e.target.nodeName.toLowerCase():'');
					    if(nodeType !='select'&& nodeType !='option'&& nodeType !='input'&& nodeType!='textarea'){
					    	e.preventDefault();
					    } 
					               
					}, 
                    onScrollMove : function() {
                        if (this.y > 5 && !pullDownEl.className.match('flip')) {
                                pullDownEl.className = 'flip';
                                //pullDownEl.querySelector('.pullDownLabel').innerHTML = '准备刷新...';
                                this.minScrollY = 0;
                                //console.log(111111);
                        } else if (this.y < 5 && pullDownEl.className.match('flip')) {
                                pullDownEl.className = '';
                                //pullDownEl.querySelector('.pullDownLabel').innerHTML = '准备刷新...';
                                this.minScrollY = -pullDownOffset;
                                //console.log(222222);
                        } else if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                                pullUpEl.className = 'flip';
                                if(!self.nomoreData){
                                	pullUpEl.querySelector('.pullUpLabel').innerHTML = '准备加载...';	
                                }else{
                                	pullUpEl.querySelector('.pullUpLabel').innerHTML = '';
                                }
                                this.maxScrollY = this.maxScrollY;
                                //console.log(333333);
                        } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                                pullUpEl.className = '';
                                //pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉显示更多...';
                                this.maxScrollY = pullUpOffset;
                                //console.log(444444);
                        }
                        //解决按住抖动拖动的bug
                        if(Math.abs(this.y-this.lastY)>100){
                        	this.refreshScroll = true;
                        }else{
                        	this.refreshScroll = false;
                        }
                        //console.log(this.y);
                    },
                    onScrollEnd : function() {
                    		//console.log("lastY:"+this.y);
                    		this.lastY = this.y;
                    		if(this.refreshScroll){
                    			self.refreshIscroll();
                    		}
                            if (pullDownEl.className.match('flip')) {
                            		//console.log("pullDownAction");
                                    pullDownEl.className = 'loading';
                                    //pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
                                    //self.pullDownAction(); // Execute custom function (ajax call?)
                            } else if (pullUpEl.className.match('flip')) {
                            		//console.log("pullUpAction");
                                    pullUpEl.className = 'loading';
                                    //pullUpEl.querySelector('.pullUpLabel').innerHTML = '努力加载中...';
                                    self.pullUpAction(); // Execute custom function (ajax call?)
                            }
                    }
                    
				});
			} else {
				self.storeWrapper.refresh();
			}
	    }
	},
	geoLocation:function(callback){
		Jit.UI.Loading(true);
		var geol;
		try {
			if ( typeof (navigator.geolocation) != 'undefined') {
				geol = navigator.geolocation;
			} else {
				geol = google.gears.factory.create('beta.geolocation');
			}
		} catch (error) {
			
			Jit.UI.Loading(false);
			alert(error.message);
		}

		if (geol) {
			geol.getCurrentPosition(function(position) {
				Jit.UI.Loading(false);
				if(callback){
					callback(position);
				}
			}, function(error) {	
				Jit.UI.Loading(false);			
				$("#storeList").empty();
				switch(error.code) {
					case error.TIMEOUT :
						alert("获取定位超时，请重试");
						Jit.UI.AjaxTips.Tips({show:true,tips:"获取定位超时，请重试"});
						break;
					case error.PERMISSION_DENIED :
						alert("您拒绝了使用位置共享服务，查询已取消");
						Jit.UI.AjaxTips.Tips({show:true,tips:"您拒绝了使用位置共享服务，查询已取消"});
						break;
					case error.POSITION_UNAVAILABLE :
						alert("非常抱歉，我们暂时无法通过浏览器获取您的位置信息");
						Jit.UI.AjaxTips.Tips({show:true,tips:"非常抱歉，我们暂时无法通过浏览器获取您的位置信息"});
						break;
				}
			}, {
				//设置十秒超时
				timeout : 10000
			});
		}
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