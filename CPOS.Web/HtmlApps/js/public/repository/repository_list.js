Jit.AM.defindPage({

    name: 'RepositoryList',
	ele:{
		pullDown:document.getElementById('pullDown'),
		pullUp:document.getElementById('pullUp')
	},
	initWithParam: function(param){
	   
	},
	page:{
		pageIndex:0,
		pageSize:10,
		nomoreData:false
	},
    onPageLoad: function () {
	   this.searchType = '';
	   
       this.loadData();
       this.initEvent();
    },
    loadData:function(){
    	this.searchKey = this.getUrlParam('Key');
    	
    	if(!this.searchKey){
    		this.alert("获取Key失败");
    	}
    	
       

       this.getClassify();
       
       this.searchText();

       
    },
    initEvent:function(){
    	var me = this;
    	$('#section').delegate('.searchBtn','click',function(){

            me.searchKey = $('.searchInput').val();
			me.searchType = '';
            if(!me.searchKey){
            	me.alert('请输入需要搜索的内容！');
                return;
            }
            me.pageIndex = 0;
            me.searchText();
       }).delegate('.jsFilter','click',function(){

			me.searchType = $(this).data("id");
            if(!me.searchKey){
            	me.alert('请输入需要搜索的内容！');
                return;
            }
            me.pageIndex = 0;
            me.searchText();
       });
    	
    },
    searchText:function(callback){
        var me = this;
        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'Knowledge.Knowledge.Search',
                'Key': encodeURIComponent(me.searchKey),
                'Type':me.searchType,
                'PageSize':me.page.pageSize,
                'PageIndex':me.page.pageIndex
            },
            beforeSend:function(){
        		Jit.UI.Loading(true);
            },
            success: function (data) {
                if(data.ResultCode==0){
                	me.buildResultList(data.Data.Knowledges);
            		if (data.Data.Knowledges && data.Data.Knowledges.length > 0) {
	                    if(data.Data.Knowledges.length!=me.page.pageSize){
	                    	me.page.nomoreData = true;
	                    }
	                } else {
	                	me.page.nomoreData = true;
	                }
            		me.refreshIscroll();
                }else{
                	me.alert(data.Message);
                }
            },
            complete:function(){
            	
            	Jit.UI.Loading(false);
            }
        });
    },
    getClassify:function(){
        var me = this;
        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'Knowledge.Type.GetTypeList'
            },
            beforeSend:function(){
        		Jit.UI.Loading(true);
            },
            success: function (data) {

                if(data.ResultCode==0){

                    me.buildClassify(data.Data.CategoryList);

                    me.initClassifyScroll();
                }
            },
            complete:function(){
            	Jit.UI.Loading(false);
            }
        });
    },
    buildClassify:function(datalist){

        var htmls = [];

        for(var i in datalist){

            htmls.push('<a href="javascript:;" class="jsFilter" data-id="'+datalist[i]['ID']+'"><span>'+datalist[i]['Name']+'</span></a>');
        }
        
        $('.navWrap').html(htmls.join(''));
    },
    initClassifyScroll:function(){

    	var me = this,
            li = $('.navWrap a');
    	
        var scrollwidth = 0;

        $('.navWrap a').each(function(idx,dom){

            scrollwidth += $(dom).innerWidth();
        });

        $('.navWrap').css({
            'width':scrollwidth+'px'
        });

        me.ImgScroll = new iScroll('classScroll', {
            snap: true,
            momentum: false,
            vScroll:false,
            hScrollbar: false,
            vScrollbar: false,
            onScrollEnd: function() {
                
            }
        });

    },
    buildResultList:function(datalist){
		var htmls = [], resultList = $("#resultList");
        if(datalist&&datalist.length==0){

            htmls.push('<li><a class="t-overflow"> 没有搜索到您要查找的内容！</a></li>');
        }else{
        	
        	for(var i in datalist){

	    		htmls.push('<li><a href="javascript:Jit.AM.toPage(\'RepositoryDetail\',\'itemId='+datalist[i]["ID"]+'\')" class="t-overflow">'+datalist[i]['Title']+'</a></li>');
	    	}
        }
        htmls = htmls.join("");
        
		if(this.page.pageIndex){
			resultList.append(htmls);
		}else{
			resultList.html(htmls);
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
		if(!this.isSending&&!this.page.nomoreData){
			this.page.pageIndex+=1;
			this.searchText(function(){
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