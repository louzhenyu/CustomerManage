Jit.AM.defindPage({

	name:'GoodsList',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入order.....');
		this.storeId = JitPage.getUrlParam("storeId");
		this.orderKey = JitPage.getUrlParam("orderId");
		this.cateType = JitPage.getUrlParam("cateType");
		if(!this.orderKey){
			this.hideMask();
			this.alert("获取不到orderId,请检查URL！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		if(!this.storeId){
			this.hideMask();
			this.alert("获取不到storeId,请检查URL！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		
		// 堂吃或外送按钮的显示
		if(this.cateType=="cateInner"||this.cateType=="cateOuter"){
			$("#footer ."+this.cateType).show();
			if(this.cateType  == "cateInner"){
				$("#pplSelect").parent().show();
			}
			
		}else{
			$("#pplSelect").parent().show();
			$("#footer .cateType").show();
		}
		var self = this;
		self.loadPageData();
		self.renderPage();
		self.initPageEvent();
	},
	loadPageData:function(){
		this.maxpplCount = 20;
		this.orderObj = this.getParams(this.orderKey);
		
		this.itemCollection = this.orderObj!=null?this.orderObj.itemCollection:{};
		this.typeCollection = this.orderObj!=null?this.orderObj.typeCollection:{};
		this.totalCount 	= this.orderObj!=null?parseInt(this.orderObj.totalCount):0;
		
		var list = [];
		this.totalMoney = 0;
		for(var i in this.itemCollection){
			var idata = this.itemCollection[i];
			idata.obj.count = idata.count;
			if(idata.count!=0){
				list.push(idata.obj);	
				this.totalMoney += idata.count*idata.obj.price;
			}
		}
		this.list = list;
	},
	renderPage:function(){
		$("#totalCount em").eq(0).html(this.totalCount);
		$("#totalCount em").eq(1).html(this.totalMoney);	
		$("#itemList").html(template.render('itemListTemp',{"list":this.list}));
		
		var pplSelect = document.getElementById("pplSelect");
		for(var i=0;i<this.maxpplCount;i++){
			pplSelect.options.add(new Option((i+1),i+1));
		}
		this.hideMask();
	},
	initPageEvent:function(){
		var self = this;
		$("#section").delegate(".add","tap",function(e){
			var $this = $(this);
			self.monitorChange($this,$this.data("id"),$this.data("typeid"),true);
			
		}).delegate(".sub","tap",function(e){
			var $this = $(this);
			self.monitorChange($this,$this.data("id"),$this.data("typeid"),false);
			
		}).delegate(".addGoods","tap",function(){
			Jit.AM.toPage('CateList',"&orderId="+self.orderKey+"&storeId="+self.storeId);
		});
		$("#footer").delegate(".cateInner","tap",function(e){
			if(self.getOrderDetailList().length==0){
				self.alert("您还没有点菜哦，现在就去菜单点菜吧！",function(){
					Jit.AM.toPage('CateList',"&orderId="+self.orderKey+"&storeId="+self.storeId);
				});
				return false;
			}else if(document.getElementById("pplSelect").value==0){
				self.alert("请选择就餐人数！");
				return false;
			}else{
				var cateStr = self.cateType?"&cateType="+self.cateType:"";
				self.direction(function(){
					self.setOrderInfo(function(data){
						Jit.AM.toPage("CateBill","&orderId="+data+"&storeId="+self.storeId+cateStr);
					});
				},function(){
					self.setOrderInfo(function(data){
						Jit.AM.toPage("CateConfirm","&orderId="+data+"&storeId="+self.storeId+cateStr);
					});
				});
			}
		}).delegate(".cateOuter","tap",function(e){
			if(self.getOrderDetailList().length==0){
				self.alert("您还没有点菜哦，现在就去菜单点菜吧！",function(){
					Jit.AM.toPage('CateList',"&orderId="+self.orderKey+"&storeId="+self.storeId);
				});
				return false;
			}else{
				self.setOrderInfo(function(data){
					Jit.AM.toPage("CateTakeaway","&orderId="+data+"&storeId="+self.storeId);
				});
			}
		});
	},
	monitorChange:function($this,skuId,typeId,add){
		var self =this;
		if(add){
			//type 数量变化
			self.typeCollection[typeId].count+=1;
			// item 数量变化
			self.itemCollection[skuId].count+=1;
			// total
			self.totalCount+=1;
			self.totalMoney+=self.itemCollection[skuId].obj.price;
		}else{
			//type 数量变化
			self.typeCollection[typeId].count-=1;
			// item 数量变化
			self.itemCollection[skuId].count-=1;
			// total
			self.totalCount-=1;
			self.totalMoney-=self.itemCollection[skuId].obj.price;
		}
		
		
		$("#typeList .type"+typeId+" em").html(self.typeCollection[typeId].count);
		$this.siblings(".num").html(self.itemCollection[skuId].count);
		$("#totalCount em").eq(0).html(self.totalCount);
		$("#totalCount em").eq(1).html(self.totalMoney);	
		
		if(self.typeCollection[typeId].count!=0){
			$("#typeList .type"+typeId).addClass("on");
		}else{
			$("#typeList .type"+typeId).removeClass("on");
		}
		if(self.itemCollection[skuId].count!=0){
			$this.parents("li").eq(0).addClass("on");
		}else{
			$this.parents("li").eq(0).removeClass("on");
		}
		
		
		var obj = {
			"typeCollection":self.typeCollection,
			"itemCollection":self.itemCollection,
			"totalCount":self.totalCount
		};
		if(this.orderObj){
			obj.orderId = this.orderId;
		}
		self.setParams(this.orderKey,obj);
	},
	getOrderDetailList:function(){
		var arr =[];
		for(var i in this.itemCollection){
			var idata = this.itemCollection[i];
			var obj = {};
			if(idata.count!=0){
				obj.skuId = idata.obj.skuId;
				obj.salesPrice = idata.obj.price;
				obj.qty = idata.obj.count;
			}
			arr.push(obj);
		}
		return arr;
	},
	setOrderInfo:function(callback){
		var self=this;
		var url='',
			datas ={
				'storeId':self.storeId,
				'totalAmount':self.totalAmount,
				'actualAmount':self.totalAmount,
				'remark':$("#remark").val(),
				'joinNo':$('#pplSelect').val(),
				'orderDetailList':self.getOrderDetailList()
			};
		if(self.orderKey=="tempOrder"){
			//提交订单
			url="/OnlineShopping/data/Data.aspx";
			datas.action='setOrderInfo';
		}else{
			//更新订单
			url = "/Interface/data/OrderData.aspx";
			datas.action = 'setUpdateOrderInfo';
			datas.orderId = self.orderKey;
		}
		self.ajax({
			url:url,
			data:datas,
			beforeSend:function(){
				Jit.UI.Masklayer.show();
			},
			complete:function(){
				Jit.UI.Masklayer.hide();
			},
			success:function(data){
				if(data.code==200){
					//将数据按最新的orderId存在本地 并清空零时对象
					var tempObj = self.getParams(self.orderKey);
					//self.setParams(self.orderKey,null);
					self.setParams(data.content.orderId,tempObj);
					if(callback){
						callback(data.content.orderId);
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	// 弹层，选择两个方向
	direction:function(callbackOk,callbackCancel){
		var self =this;
		Jit.UI.Dialog({
			type : "Confirm",
			content : "下单前请确保您已经到店入座！",
			LabelOk:"已到店，现在下单",
			LabelCancel:"未到店，稍后下单",
			CallBackOk : function() {
				if(callbackOk){
					callbackOk();
				}else{
					self.setOrderInfo(function(data){
						Jit.AM.toPage("CateBill","&orderId="+data+"&storeId="+self.storeId);
					});
				}
				
			},
			CallBackCancel:function(){
				if(callbackCancel){
					callbackCancel();
				}else{
					self.setOrderInfo(function(data){
						Jit.AM.toPage("CateConfirm","&orderId="+data+"&storeId="+self.storeId);
					});
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