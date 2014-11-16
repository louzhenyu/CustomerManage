Jit.AM.defindPage({

    name: 'CateOrderList',
    hideMask: function() {
        $('#masklayer').hide();
    },
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.orderId = this.getUrlParam('orderId');
        this.storeId = JitPage.getUrlParam("storeId");
        this.pageType = JitPage.getUrlParam("pageType");
        
        // 如果有order，则付款成功，去掉缓存中的菜单列表
        if(this.orderId){
			self.setParams(this.orderId,null);
        }
        
        var self = this;
        
        var topMenuList = $('#menuList');
        if (self.orderId || self.pageType=="feedback") {
            topMenuList.children().eq(1).addClass('on');
        } else {
            topMenuList.children().eq(0).addClass('on');
        }
        this.loadData();
        this.initEvent();
    }, 
    //加载数据
    loadData:function(){
    	var self = this,$currentMenu = $("#menuList .on");
        self.getOrderList($currentMenu.data("status"),$currentMenu.data("menu"));
    },
    initEvent: function() {
        var self = this;
		$('#menuList').delegate('a','tap',function(){
			var $this = $(this);
			$this.addClass("on").siblings().removeClass("on");
			self.getOrderList($this.data("status"),$this.data("menu"));
		});
		$('#itemList').delegate('.delete','tap',function(){
			var $this = $(this);
			self.confirm("您确认删除吗？",function(){
				self.deleteOrder($this.data("orderid"),function(){
                	// 删除本地存储的订单信息
                	self.setParams($this.data("orderid"),null);
					$this.parents(".items").eq(0).remove();
				});
			});
		}).delegate('.modify','tap',function(){
			var $this = $(this);
			Jit.AM.toPage("CateList","orderId="+$this.data("orderid")+"&storeId="+$this.data("storeid"));
		}).delegate('.toBill','tap',function(){
			var $this = $(this);
			Jit.AM.toPage("CateBill","orderId="+$this.data("orderid")+"&storeId="+$this.data("storeid"));
		});
    },
    getOrderList:function(status,hasMenu,callback){
    	var self = this;
    	var $itemList = $("#itemList");
        self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderList',
                'page': '1',
                'pageSize': '30',
                'status': status
            },
            beforeSend: function() {
               	$itemList.html('<div style=\"padding:15px;text-align:center;\" >数据正在加载，请稍候。。。</div>');
            },
            complete:function(){
            	self.hideMask();
            },
            success: function(data) {
                if (data && data.code == 200 && data.content.orderList&& data.content.orderList.length) {
                	var orderList = data.content.orderList;
                	for(var i=0;i<orderList.length;i++){
                		var idata= orderList[i];
                		var list = [];
                		for(var j=0;j<idata.orderDetailList.length;j++){
                			list.push(idata.orderDetailList[j].itemName);
                		}
                		orderList[i].orderDetailString = list.join("，");
                	}
                	var obj = {
                		list:orderList,
                		hasMenu:!!parseInt(hasMenu)
                	};
                    $itemList.html(template.render('itemListTemp',obj));
                }else{
                	 $itemList.html('<div style=\"padding:15px;text-align:center;\" >暂无数据</div>');
                }
            }
        });
    },
    deleteOrder:function(orderId,callback){
    	var self = this;
    	self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderStatus',
                'orderId': orderId,
                'status': 0
            },
			beforeSend:function(){
				Jit.UI.Masklayer.show();
			},
			complete:function(){
				Jit.UI.Masklayer.hide();
			},
            success: function(data) {
                if (data.code == 200) {
                    self.alert("删除成功",function(){
                       	//location.reload();
                       	if(callback){
                       		callback();
                       	}
                    });
                } else {
                    self.alert(data.description);
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
   },
   confirm:function(text,callckOK,callbackCancel){
   		Jit.UI.Dialog({
            'content': text,
            'type': 'Confirm',
            'LabelCancel': '取消',
            'LabelOK': '确定',
            'CallBackOk': function() {
				Jit.UI.Dialog("CLOSE");
                if(callckOK){
                	callckOK();
                }
            },
            'CallBackCancel':function(){
				Jit.UI.Dialog("CLOSE");
            	if(callbackCancel){
            		callbackCancel();
            	}
            }
        });
   }
});