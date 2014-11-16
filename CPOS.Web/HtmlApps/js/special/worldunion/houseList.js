Jit.AM.defindPage({

    name: 'houselist',

	ele:{
		pullDown:document.getElementById('pullDown'),
		pullUp:document.getElementById('pullUp'),
		housesList:$('#housesList')
	},
	page:{
		'pageIndex':1,
		'pageSize':10
	},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('进入'+this.name);
        // 公共参数
		this.isSending = false;
		this.ajaxUrl = '/ApplicationInterface/Project/HuaAn/HuaAnHandler.ashx';
		
		this.objectID = JitPage.getUrlParam("objectId");
		if(!this.objectID){
			this.alert("未获取到objectId，请检查url");
			return false;
		}
		
        this.initEvent();
        this.loadData();
    },
    initEvent: function() {
        var self = this;
        this.ele.housesList.delegate(".jsListItem",this.eventType,function(e){
        	var $this = $(this);
    		JitPage.toPageWithParam("IntroduceList","detailId="+$this.data("id"));
        	
        }).delegate(".jsImmediate",this.eventType,function(e){
        	var $this = $(this).parent();
    		JitPage.toPageWithParam("Yuding","immediate=true&detailId="+$this.data("id"));
        	self.stopBubble();
        });
    },
    loadData:function(){
    	this.GetHouses();
    },
    stopBubble: function (e) {
	    if (e && e.stopPropagation) {
	        //因此它支持W3C的stopPropagation()方法 
	        e.stopPropagation();
	    } else {
	        //否则，我们需要使用IE的方式来取消事件冒泡 
	        window.event.cancelBubble = true;
	    }
	},
    GetHouses:function(callback){
    	var self =this,
        	housesList = self.ele.housesList;
    	if(!self.isSending){
    		self.ajax({
	            url: self.ajaxUrl,
				interfaceType:'Project',
				interfaceMode:'V2.0',
	            data: {
	                action: "GetHouses",
	                PageIndex: self.page.pageIndex,
	                PageSize: self.page.pageSize
	            },
	            beforeSend: function() {
	            	self.isSending = true;
	            	if(self.page.pageIndex==1){
	            		housesList.html('<div style="text-align:center;">正在加载,请稍后...</div>');
	            	}
	                
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	//渲染订单数据
	                	if (data.Data.HouseList && data.Data.HouseList.length > 0) {
	                		//debugger;
		                    self.renderHouseList(data.Data.HouseList);
		                    
		                    if(data.Data.HouseList.length!=self.page.pageSize){
		                    	self.nomoreData = true;
		                    }
		                    
		                } else {
		                	self.nomoreData = true;
		                	if(self.page.pageIndex==1){
		                		housesList.html('<div  style="text-align:center;">暂无数据</div>');
		                	}
		                    
		                }
    					self.refreshIscroll();
	                }else{
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
	            	self.isSending = false;
	            }
	        });
    	}
    	
    },
    renderHouseList:function(itemlists){
		var html=template.render('housesListTemp',{list:itemlists});
    	if(this.page.pageIndex == 1){
        	this.ele.housesList.html(html);
    		
    	}else{
        	this.ele.housesList.append(html);
    	}
    },
    refreshIscroll:function(){
		var self = this;
		if(null!=document.getElementById("scrollContainer")){
			if (!self.storeWrapper) {
                var pullDownEl = self.ele.pullDown;
                var pullUpEl = self.ele.pullUp;
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
                                // if(!self.nomoreData){
                                	// pullUpEl.querySelector('.pullUpLabel').innerHTML = '准备加载...';	
                                // }else{
                                	// pullUpEl.querySelector('.pullUpLabel').innerHTML = '';
                                // }
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
                        
                        console.log(this.y);
                        if(this.y<60){
							//$("#scrollContainer").css("margin-top","40px");
                        }
                    },
                    onScrollEnd : function() {
                    		//console.log("lastY:"+this.y);
                    		this.lastY = this.y;
                        	//解决按住抖动拖动的bug
                    		if(this.refreshScroll){
                    			self.refreshIscroll();
                    		}
                            if (pullDownEl.className.match('flip')) {
                            		//console.log("pullDownAction");
                                    pullDownEl.className = 'loading';
                                    //pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
                                    self.pullDownAction(); // Execute custom function (ajax call?)
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
	pullDownAction:function(){
		
	},
	pullUpAction:function(callback){
		if(!this.isSending&&!this.nomoreData){
			this.page.pageIndex+=1;
			this.GetHouses(function(){
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
	confirm:function(text,OKCallback,CancelCallback){
		Jit.UI.Dialog({
			'type': 'Confirm',
			'content': text,
			'LabelOk': '确认',
			'LabelCancel': '取消',
			'CallBackOk': function() {
				if(OKCallback){
					Jit.UI.Dialog("CLOSE");
					OKCallback();
				}
			},
			'CallBackCancel': function() {
				if(CancelCallback){
					Jit.UI.Dialog("CLOSE");
					CancelCallback();
				}else{
					Jit.UI.Dialog("CLOSE");
				}
			}
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