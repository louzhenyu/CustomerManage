Jit.AM.defindPage({

    name: 'my_order',

	ele:{
		pullDown:document.getElementById('pullDown'),
		pullUp:document.getElementById('pullUp'),
		orderList:$('#orderList')
	},
	page:{
		'pageIndex':0,
		'pageSize':10
	},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('进入'+this.name);
		template.isEscape = false;
		//debugger;
        this.initEvent();
        this.loadData();
    },
    initEvent: function() {
        var self = this;
        $("#jsOrderStateList li").click(function(){
        	if(!self.isSending){
        		var $this=$(this);  
	        	self.ele.orderList.html("");
	        	$this.addClass("cur").siblings().removeClass("cur");
	        	self.GroupingTypeId = $this.data("code");
	        	
	        	if(self.tabFlag){
	        		clearTimeout(self.tabFlag);
	        	}
	        	self.tabFlag = setTimeout(function(){      	
		    		self.nomoreData = false;
		    		self.page.pageIndex = 0;
		    		self.GetOrders();
	        	},300);	
        	}
        });
    },
    loadData:function(){
    	this.GetOrders();
    },
    ProcessOrder:function(OrderID,ActionCode){
    	var self =this;
    	if(!OrderID){
    		self.alert("订单ID不能为空！");
    		return false;
    	}
    	if(!ActionCode){
    		self.alert("操作码错误！");
    		return false;
    	}
    	if(ActionCode==800){
    		self.confirm("确认取消订单？",function(){
				self.ProcessAction(OrderID,ActionCode);
    		});
    	}else{
    		self.ProcessAction(OrderID,ActionCode);
    	}
    },
    ProcessAction:function(OrderID,ActionCode){
    	var self =this;
    	//取消订单
    	if(ActionCode==800){
    		JitPage.ajax({
	    		type:"get",
	            url:"/ApplicationInterface/Vip/VipGateway.ashx",
	            data: {
	                action:"SetCancelOrder",
	                OrderID:OrderID,
	                ActionCode:ActionCode
	            },
	            beforeSend: function() {
					Jit.UI.AjaxTips.Tips({show:true,tips:"正在操作"});
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	self.GetOrders();
	                }else{
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
					Jit.UI.AjaxTips.Tips({show:false,tips:"正在操作"});	
	            }
	        });	
    	}else{
	    	JitPage.ajax({
	    		type:"get",
	            url: '/ApplicationInterface/Gateway.ashx',
				interfaceType:'Product',
	            data: {
	                action: "Order.Order.ProcessAction",
	                OrderID:OrderID,
	                ActionCode:ActionCode
	            },
	            beforeSend: function() {
					Jit.UI.AjaxTips.Tips({show:true,tips:"正在操作"});
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	self.GetOrders();
	                }else{
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
					Jit.UI.AjaxTips.Tips({show:false,tips:"正在操作"});	
	            }
	        });	
	     }
    },
    GetOrders:function(callback,type){
    	var self =this,
    		GroupingTypeId = type||self.GroupingTypeId||$("#jsOrderStateList li.cur").data("code"),
        	orderList = self.ele.orderList;
    	if(!self.isSending){
    		JitPage.ajax({
	            url: '/ApplicationInterface/Gateway.ashx',
				interfaceType:'Product',
	            data: {
	                action: "VIP.Order.GetOrders",
	                GroupingType:GroupingTypeId,
	                PageIndex: self.page.pageIndex,
	                PageSize: self.page.pageSize
	            },
	            beforeSend: function() {
	            	self.isSending = true;
	            	if(self.page.pageIndex==0){
	            		orderList.html('<div class="order_mod order_goods_list">正在加载,请稍后...</div>');
	            	}
	                
	            },
	            success: function(data) {
	                if(data.IsSuccess){
	                	//渲染订单状态
	                	if (data.Data.GroupingOrderCounts && data.Data.GroupingOrderCounts.length > 0) {
		                    self.renderOrderStatus(data.Data.GroupingOrderCounts);
		                }
		                
	                	//渲染订单数据
	                	if (data.Data.Orders && data.Data.Orders.length > 0) {
	                		
		                    self.renderOrderList(data.Data.Orders,GroupingTypeId);
		                    
		                    if(data.Data.Orders.length!=self.page.pageSize){
		                    	self.nomoreData = true;
		                    }
		                    
		                } else {
		                	self.nomoreData = true;
		                	if(self.page.pageIndex==0){
		                		orderList.html('<div class="order_mod order_goods_list">暂无订单</div>');
		                	}
		                    
		                }
    					self.refreshIscroll();
	                }else{
	                	self.alert(data.Message);
	                }                
	            },
	            complete:function(){
	            	self.isSending = false;
	            	self.GroupingTypeId = GroupingTypeId;
	            }
	        });
    	}
    	
    },
    renderOrderList:function(itemlists,type){

        for(var i in itemlists){

            itemlists[i]['OrderDate'] = itemlists[i]['OrderDate'].substr(0,itemlists[i]['OrderDate'].length-3);
        }

		var html=template.render('tplListItem',{itemlists:itemlists,GroupingTypeId:type});
    	if(this.page.pageIndex == 0){
        	this.ele.orderList.html(html);
    		
    	}else{
        	this.ele.orderList.append(html);
    	}
    },
    renderOrderStatus:function(statusList){
		var state_1 = 0 , state_2 = 0 , state_3 = 0;
        for (var i = 0; i < statusList.length; i++) {
            var statusInfo = statusList[i];
			if(statusInfo.GroupingType == 1){
				state_1 = statusInfo.OrderCount;
			}else if(statusInfo.GroupingType == 2){
				state_2 = statusInfo.OrderCount;
				
			}else if(statusInfo.GroupingType == 3){
				state_3 = statusInfo.OrderCount;
			}
        }
        $('#waitCount1').html(state_1);
        $('#waitCount2').html(state_2);
        $('#waitCount3').html(state_3);
        
		if(state_1>0){
			$('#waitCount1').show();
		}else{
			$('#waitCount1').hide();
		}
		if(state_2>0){
			$('#waitCount2').show();
		}else{
			$('#waitCount2').hide();
		}
		if(state_3>0){
			$('#waitCount3').show();
		}else{
			$('#waitCount3').hide();
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
		//$("#scrollContainer").css("margin-top","0");
		if(!this.isSending&&!this.nomoreData){
			this.page.pageIndex+=1;
			this.GetOrders(function(){
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