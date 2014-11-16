Jit.AM.defindPage({

	name:'CateList',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入页面：'+this.name);
		
		this.typeList = [];
		this.typeObj  ={};
		this.itemList = [];
		this.itemObj={};
		
		this.orderId = JitPage.getUrlParam("orderId");
		this.storeId = JitPage.getUrlParam("storeId");
		this.orderKey = this.orderId?this.orderId:"tempOrder";
		//若到orderId，把本地缓存清空
		if(this.orderId!=""&&this.orderId!="tempOrder"){
			JitPage.setParams("tempOrder",null);
		}
		
		console.log(this.storeId);
		console.log(this.orderId);
		if(!this.storeId){
			this.hideMask();
			this.alert("URL获取不到storeId！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		this.orderObj = this.getParams(this.orderKey);
		this.itemCollection = this.orderObj!=null?this.orderObj.itemCollection:{};
		this.typeCollection = this.orderObj!=null?this.orderObj.typeCollection:{};
		this.totalCount 	= this.orderObj!=null?parseInt(this.orderObj.totalCount):0;
		if(this.totalCount!=0){
			$("#totalCount").addClass("on").find("em").html(this.totalCount);
		}
		var self = this;
		self.loadPageData();
		self.initPageEvent();
		
	},
	initPageEvent:function(){
		var self = this;
		$("#section").delegate(".add","tap",function(e){
			var $this = $(this);
			self.monitorChange($this,$this.data("id"),$this.data("typeid"),true);
			
		}).delegate(".sub","tap",function(e){
			var $this = $(this);
			self.monitorChange($this,$this.data("id"),$this.data("typeid"),false);
			
		}).delegate(".type","tap",function(e){
			self.loadDish($(this).data("id")=="all"+self.storeId?"":$(this).data("id"));
		});
		
		$("#footer").delegate(".dishes","tap",function(){
			Jit.AM.toPage('CateList',"orderId="+self.orderKey+"&storeId="+self.storeId);
		}).delegate(".rowNumber","tap",function(){
			Jit.AM.toPage('CateTakeNumber');
		}).delegate(".book","tap",function(){
			Jit.AM.toPage('CateSeats',"orderId="+self.orderKey+"&storeId="+self.storeId);
		}).delegate(".myOrder","tap",function(){
			Jit.AM.toPage('CateOrderList',"storeId="+self.storeId);
		}).delegate("#totalCount","tap",function(){
			var cateType = JitPage.getUrlParam("cateType"),
				typeStr = cateType?"&cateType="+cateType:"";
			if(self.totalCount==0){
				self.alert("至少选一个菜哦");
			}else{
				Jit.AM.toPage('CateOrder',"storeId="+self.storeId+"&orderId="+self.orderKey+typeStr);
			}
			
		});
	},
	monitorChange:function($this,skuId,typeId,add){
		var self =this;
		//itemCollection没有该条数据，添加空对象，添加count为0
		if(!self.itemCollection[skuId]){
			self.itemCollection[skuId] = {};
			self.itemCollection[skuId].count = 0;
		}
		if(add){
			//type 数量变化
			self.typeCollection[typeId].count+=1;
			// item 数量变化
			self.itemCollection[skuId].count+=1;
			// total
			self.totalCount+=1;
		}else{
			//type 数量变化
			self.typeCollection[typeId].count-=1;
			// item 数量变化
			self.itemCollection[skuId].count-=1;
			// total
			self.totalCount-=1;
		}
		
		
		
		$("#typeList .type"+typeId+" em").html(self.typeCollection[typeId].count);
		$this.siblings(".num").html(self.itemCollection[skuId].count);
		$("#totalCount em").html(self.totalCount);
		
		if(self.typeCollection[typeId].count!=0){
			$("#typeList .type"+typeId).addClass("on");
		}else{
			$("#typeList .type"+typeId).removeClass("on");
		}
		if(self.itemCollection[skuId].count!=0){
			$this.parents("li").eq(0).addClass("on");
			 // 用于在order页面显示已选择的商品
			self.itemCollection[skuId].obj = self.itemObj[skuId];
		}else{
			$this.parents("li").eq(0).removeClass("on");
			delete self.itemCollection[skuId];
		}
		if(self.totalCount!=0){
			$("#totalCount").addClass("on");
		}else{
			$("#totalCount").removeClass("on");
		}
		
		var obj = {
			"typeCollection":self.typeCollection,
			"itemCollection":self.itemCollection,
			"totalCount":self.totalCount
		};
		self.setParams(this.orderKey,obj);
	},
	
	loadPageData:function(){
		var self = this;
		self.loadType();
	},
	loadType:function(callback){
		var self=this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getItemTypeList'
			},
			beforeSend:function(){
				//Jit.UI.Masklayer.show();
				self.typeList = [];
				self.typeObj = {};
				self.timer = new Date().getTime();
			},
			complete:function(){
				//Jit.UI.Masklayer.hide();
				console.log("请求分类耗时"+(new Date().getTime()- self.timer)+"毫秒");
			},
			success:function(data){
				if(data.code==200){
					for(var i=0;i<data.content.itemTypeList.length;i++){
						var idata = data.content.itemTypeList[i];
						idata.itemTypeId = idata.itemTypeId?idata.itemTypeId:("all"+self.storeId);
						if(!self.typeCollection[idata.itemTypeId]){
							self.typeCollection[idata.itemTypeId]={};
							self.typeCollection[idata.itemTypeId].count=0;
						}
						idata.count = self.typeCollection[idata.itemTypeId].count;
						self.typeList[idata.displayIndex-1] = idata;
						self.typeObj[idata.itemTypeId] = idata;
					}
					self.hideMask();
					self.loadDish("");
					if(callback){
						callback(self.typeList,slef.typeObj);
					}else{
						$("#typeList").html(template.render('typeListTemp',{"list":self.typeList}));
						self.typeWrapper = new iScroll('typeWrapper', { hScrollbar: false, vScrollbar: false });
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	loadDish:function(typeId,callback){
		var self=this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getItemList',
				'page':1,
				'pageSize':99,
				'isExchange':0,
				'storeId':self.storeId,
				'itemTypeId':typeId
			},
			beforeSend:function(){
				Jit.UI.Masklayer.show();
				self.itemList = [];
				self.itemObj = {};
				self.timer = new Date().getTime();
				Jit.UI.AjaxTips.Tips({show:false});
			},
			complete:function(){
				Jit.UI.Masklayer.hide();
				console.log("请求商品耗时"+(new Date().getTime()- self.timer)+"毫秒");
			},
			success:function(data){
				if(data.code==200){
					for(var i=0;i<data.content.itemList.length;i++){
						var idata = data.content.itemList[i];
						if(!self.itemCollection[idata.skuId]){
							idata.count = 0;
						}else{
							idata.count = self.itemCollection[idata.skuId].count;
						}
						idata.typeId = typeId?typeId:"all"+self.storeId;
						self.itemObj[idata.skuId] = idata;
						self.itemList[idata.displayIndex-1] = idata;
					}
					if(callback){
						callback(self.itemList,slef.itemObj);
					}else{
						if(self.itemList.length==0){
							Jit.UI.AjaxTips.Tips({show:true,tips:"该分类暂无数据",left:"60%"});
							$("#itemList").empty();
						}else{
							$("#itemList").html(template.render('itemListTemp',{"list":self.itemList}));
							if(!self.itemWrapper){
								self.itemWrapper = new iScroll('itemWrapper', { hScrollbar: false, vScrollbar: false });	
							}else{
								self.itemWrapper.refresh();
							}
							
						}
						
					}
				}else{
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
    }
	
	
});