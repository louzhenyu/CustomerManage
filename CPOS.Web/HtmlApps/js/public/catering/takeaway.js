Jit.AM.defindPage({

    name: 'CateTakeaway',
    storeId: '',
    orderId: '',
    // keyList: {
    //     inputInfo: 'inputInfo'
    // },
    elements: {
        txtTel: '',
        txtUserName: '',
        txtAddress: '',
        txtExplain: '',
        btSubmit: '',
        toSelectAddress: ''

    },
    hideMask:function(){
    	$("#masklayer").hide();
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.txtTel = $('#txtTel');
        self.elements.txtUserName = $('#txtUserName');
        self.elements.txtAddress = $('#txtAddress');
        self.elements.txtExplain = $('#txtExplain');
        self.elements.btSubmit = $('#btSubmit');
        self.elements.toSelectAddress = $('#toSelectAddress');


        self.storeId = self.getUrlParam('storeId');
        self.orderId = self.getUrlParam('orderId');
        self.takeawayInfo = self.getParams("takeawayInfo");
        
        if (self.orderId&&self.takeawayInfo) {
        	self.takeawayInfo.orderId = self.orderId;
        	self.setUpdateOrderDelivery();
        }else{
        	self.hideMask();
        }
        if (!self.storeId) {
			console.log("URL获取不到storeId！");
			document.getElementById("storeArea").style.display = "block";
            self.loadStore(function(data){
            	for(var i=0;i<data.storeList.length;i++){
					var idata = data.storeList[i];
					document.getElementById("storeSelect").options.add(new Option(idata.storeName,idata.storeId));
				}
            });
        };



        var data = self.getParams('select_address');
        if (data && data.address) {
            self.elements.txtTel.val(data.linkTel);
            self.elements.txtUserName.val(data.linkMan);
            self.elements.txtAddress.html(data.address);
        };



    },
    initEvent: function() {

        var self = this;
        self.elements.toSelectAddress.bind('click', function() {
            self.toPage('SelectAddress');
        });
        //提交外送信息
        self.elements.btSubmit.bind('click', function() {
            self.setUpdateOrderDelivery();
        });
    }, 
    setUpdateOrderDelivery:function(){
    	var self = this;
    	var datas = null;
    	if(!self.takeawayInfo||(self.takeawayInfo&&!self.orderId)){
	        datas = {
	            'action': 'setUpdateOrderDelivery',
	            'storeId': self.storeId?self.storeId:document.getElementById("storeSelect").value,
	            'mobile': self.elements.txtTel.val(),
	            'remark': self.elements.txtExplain.val(),
	            'deliveryId': 2,
	            'deliveryAddress': self.elements.txtAddress.html(),
	            'username': self.elements.txtUserName.val(),
	            'orderId': self.orderId
	        };
			if(!datas.storeId){
				self.alert("请选择门店！");
				return false;
			}
	        if (!datas.mobile) {
	            self.alert("请输入手机号码！",function(){
	               	self.elements.txtTel.focus();
	            });
				return false;
	        } else if (!IsMobileNumber(datas.mobile)) {
	            self.alert("手机号码格式不正确！",function(){
	                self.elements.txtTel.focus();
	            });
				return false;
	        }
	        if (!datas.username) {
	            self.alert("请输入用户名！",function(){
	            	self.elements.txtUserName.focus();
	            });
				return false;
	        }
	
	
	        if (!datas.deliveryAddress) {
	            self.alert("请输入联系地址！",function(){
	                self.elements.txtAddress.focus();
	            });
				return false;
	        }
			
			if(!datas.orderId){
				//缺 存储信息 跳转到选菜页面
				self.setParams("takeawayInfo",datas);
				self.toPageWithParam("CateList","cateType=cateOuter&storeId="+datas.storeId);
				return false;
			}
    	}else{
    		datas = self.takeawayInfo;
    		self.setParams("takeawayInfo",null);
    	}


        self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: datas,
            beforeSend:function(){
				Jit.UI.Masklayer.show();
            },
            complete:function(){
				Jit.UI.Masklayer.hide();
			},
            success: function(data) {
                if (data && data.code == 200) {
                    //datas.orderId = data.content.orderId;
                    self.SetOrderStatus();
                } else {
                    self.alert(data.description);
                }
            }
        });
    },
    //提交成功后设置订单状态
    SetOrderStatus: function() {
        var self = this;
        self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderStatus',
                'orderId': self.orderId,
                'status': '100'
            },
            complete:function(){
				Jit.UI.Masklayer.hide();
				self.hideMask();
            },
            success: function(data) {
                if (data && data.code == 200) {
                    self.alert('提交成功',function(){
                    	self.toPage('OrderPay', 'orderId=' + self.orderId + '&payRerurn=CateOrderList');
                    });
                } else {
					self.alert(data.description);
                }
            }
        });
    },
	loadStore:function(callback){
		var self=this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getStoreListByItem',
				'page':1,
				'pageSize':20
			},
			beforeSend:function(){
				self.timer = new Date().getTime();
				Jit.UI.AjaxTips.Tips({show:false});
			},
			complete:function(){
				console.log("请求门店列表耗时"+(new Date().getTime()- self.timer)+"毫秒");
			},
			success:function(data){
				if(data.code==200){
					if(callback){
						callback(data.content);
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	alert : function(text, callback) {
		Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if (callback) {
					callback();
				}
			}
		});
	}
});