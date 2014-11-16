Jit.AM.defindPage({

	name:'ActivityList',
	
	page:{
		'pageIndex':0,
		'pageSize':10
	},
	ele:{
		pullDown:document.getElementById('pullDown'),
		pullUp:document.getElementById('pullUp')
	},
	
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入'+this.name+'.....');
		
		this.initEvent();
		this.loadData();
	},
	initEvent:function(){
		var self = this;		
	},
	loadData:function(callback){
		this.getActivityList();		
	},
	getActivityList:function(callback){
		var self =this;
		if(!self.isSending){
			self.ajax({
				url:'/ApplicationInterface/Gateway.ashx',
				interfaceType:'Project',
				type:'get',
				data:{
					'action':'LZLJ.Activity.Activity.GetHomePageActivityList',
					'PageIndex':self.page.pageIndex,
					'PageSize':self.page.pageSize,
				},
				beforeSend:function(){
						self.isSending =true;
						Jit.UI.Loading(true);
				},
				success:function(data){
					if(data.IsSuccess){
						if(callback){
							callback(data);
						}else{
							if(self.page.pageIndex==0){
								$("#activityList").html(template.render('tplListItem',{list:data.Data.Items}));
							}else{
								$("#activityList").append(template.render('tplListItem',{list:data.Data.Items}));
							}
							self.refreshIscroll();
						}
					}else{
						self.alert(data.Message);
					}
				},
				complete:function(){
					Jit.UI.Loading(false);
					self.isSending =false;
					console.log("第"+self.page.pageIndex+"数据请求complete");
						
				}
			});
		}
		
	},
	pullUpAction:function(){
		if(!this.isSending){
			this.page.pageIndex+=1;
			this.getActivityList();
		}
	},
	refreshIscroll:function(){
		var self = this;
		if(null!=document.getElementById("section")){
			if (!self.storeWrapper) {
                pullDownEl = self.ele.pullDown;
                pullUpEl = self.ele.pullUp;
                pullDownOffset = pullDownEl.offsetHeight;
                pullUpOffset = pullUpEl.offsetHeight;
                
                
				self.storeWrapper = new iScroll('section', {
					hScrollbar : false,
					vScrollbar : false,
					onRefresh : function() {
                    	if (pullDownEl.className.match('loading')) {
                            pullDownEl.className = '';
                            //pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                        } else if (pullUpEl.className.match('loading')) {
                            pullUpEl.className = '';
                            //pullUpEl.querySelector('.pullUpLabel').innerHTML = '显示更多...';
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
                        } else if (this.y < 5
                                        && pullDownEl.className.match('flip')) {
                                pullDownEl.className = '';
                                //pullDownEl.querySelector('.pullDownLabel').innerHTML = '准备刷新...';
                                this.minScrollY = -pullDownOffset;
                        } else if (this.y < (this.maxScrollY - 5)
                                        && !pullUpEl.className.match('flip')) {
                                pullUpEl.className = 'flip';
                                //pullUpEl.querySelector('.pullUpLabel').innerHTML = '准备刷新...';
                                this.maxScrollY = this.maxScrollY;
                        } else if (this.y > (this.maxScrollY + 5)
                                        && pullUpEl.className.match('flip')) {
                                pullUpEl.className = '';
                                //pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉显示更多...';
                                this.maxScrollY = pullUpOffset;
                        }
                    },
                    onScrollEnd : function() {
                            if (pullDownEl.className.match('flip')) {
                                    pullDownEl.className = 'loading';
                                    //pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
                                    //pullDownAction(); // Execute custom function (ajax call?)
                            } else if (pullUpEl.className.match('flip')) {
                                    pullUpEl.className = 'loading';
                                    //pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Loading...';
                                    self.pullUpAction(); // Execute custom function (ajax call?)
                            }
                    }
                    
				});
			} else {
				self.storeWrapper.refresh();
			}
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