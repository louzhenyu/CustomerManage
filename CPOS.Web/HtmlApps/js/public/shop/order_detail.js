Jit.AM.defindPage({
	name : 'OrderDetail',
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入OrderDetail.....');
		
		this.loadData();
		this.initEvent();
	},
	loadData:function(){
		var self = this;
		self.ajax({
			url : '/ApplicationInterface/Gateway.ashx',
			data : {
				'action' : 'Order.Order.GetOrderDetail',
				'orderId' : self.getUrlParam('orderId')
			},
			success : function(data) { 
				if (data.IsSuccess) {
					var order = data.Data.OrderListInfo, 
						tpl = $('#Tpl_order_info').html(), 
						html = '';

					self.renderPage(order);

					for (var i = 0; i < order.OrderDetailInfo.length; i++) {
						html += Mustache.render(tpl, order.OrderDetailInfo[i]);
					}
					$('#order_list').append(html);
				} else {
					self.alert(data.Message);
				}
			}
		});
	},
	initEvent:function(){
		
	},
	renderPage : function(data) {
		//debugger;
		//debugger;
		//  状态 1未付款/2待处理/3已发货/0已取消 可为空
		var me = this;
		$('#totalprice,#ordersAmount').html("￥" + data.TotalAmount);
		//配送费
		$("#devilyAmount").html("￥"+(data.DeliveryAmount?data.DeliveryAmount:0));
		$("#goodsAmount").html("￥"+Math.round((data.TotalAmount-data.DeliveryAmount)*10)/10);
		$("#ordersAmount").html("￥" + data.ActualDecimal);
		$('#totalqty').html(data.TotalQty);
		$('#ordersCount').html(data.TotalQty);
		$("#couponAmount").html("￥" +data.CouponAmount);
		$("#orderIntegral").html("￥"+data.UseIntegralToAmount+"("+data.OrderIntegral+")");
		$("#vipEndAmount").html("￥" +data.VipEndAmount);
		$('#createTime').html(data.OrderDate);
		//货到付款
		if(data.PaymentTypeCode=="GetToPay"){
			$("#getToPay").show();
			if(data.IsPayment!="1"){
				$('#paystatus').html('未支付(货到付款)');
			}else{
				$('#paystatus').html('已支付(货到付款)');
			}
			
		}else{
			if(data.IsPayment!="1"){
			
				$('#paystatus').html('未支付');
				
			}else{
			
				$('#paystatus').html('已支付');
			}
		}
		data.status = parseInt(data.Status);

        //备注信息
        if(data.Remark){
            $("#Remark").html(data.Remark).parents("div").eq(0).show();
        }


		
		// 提货信息
		// $('#linkMan').html(data.linkMan);
		// $('#linkTel').html(data.linkTel);
		// $('#addr').html(data.address);
		// $('#date').html(data.receiptTime);

		var tpl = '', 
			html = '';

		if(data.DeliveryID==1){
			
			tpl = $("#TplReceipt").html();

		}else if(data.DeliveryID==2){

			tpl = $("#TplPickup").html();			
		}
		
		html = Mustache.render(tpl, data);

		$('#deliveryContianer').html(html);

		$('#orderStatus').html(data.StatusDesc);

		

		if(data.status==700 || data.status==800 || data.status==900){
			//已结束订单
			return;
		}

		var htmls = [];
		if(data.IsPayment == '0'){
			//未付款订单
			if(data.status==100 || data.status==500 || data.status==510 || data.status==410){

				htmls.push('<a href="javascript:JitPage.ProcessOrder(\''+data.OrderID+'\',800);" class="grey" id="orderCancel">取消订单</a>');

				htmls.push('<a href="javascript:Jit.AM.toPage(\'OrderPay\',\'orderId='+data.OrderID+'\');" class="red" id="orderCancel">确认付款</a>');

			}else{

				htmls.push('<a href="javascript:Jit.AM.toPage(\'OrderPay\',\'orderId='+data.OrderID+'\');" class="red" id="orderCancel">确认付款</a>');
			}
			
			$('#btnList').html(htmls.join(''));
			
		}else if(data.IsPayment == '1'){
			//已付款订单
			
			if(data.status==100 || data.status==500 || data.status==510){

				htmls.push('<a href="javascript:JitPage.ProcessOrder(\''+data.OrderID+'\',800);" class="grey" id="orderCancel">取消订单</a>');

			}else if(data.status==600 || data.status==610){

				htmls.push('<a href="javascript:JitPage.ProcessOrder(\''+data.OrderID+'\',700);" class="red" id="orderCancel">确认提货</a>');
			}

			$('#btnList').html(htmls.join(''));
		}
		//货到付款
		if(data.PaymentTypeCode=="GetToPay"){
			//让确认付款隐藏
			$("#btnList").find(".red").hide();
		}
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
				//self.ProcessAction(OrderID,ActionCode);
				self.submitOrderCancel(OrderID);
    		});
    	}else if(ActionCode==700){
    		self.confirm("确认您已提货？",function(){
				self.ProcessAction(OrderID,ActionCode);
    		});
    	}else{
    		self.ProcessAction(OrderID,ActionCode);
    	}
    },
    //新的取消订单接口
    submitOrderCancel:function(OrderID){
    	var self =this;

    	self.ajax({
            url: '/ApplicationInterface/Vip/VipGateway.ashx',
			interfaceType:'Product',
            data: {
                action: "SetCancelOrder",
                OrderId:OrderID
            },
            beforeSend: function() {

				Jit.UI.AjaxTips.Tips({show:true,tips:"正在操作"});
            },
            success: function(data) {

                if(data.IsSuccess){

                	location.reload();
                }else{

                	self.alert(data.Message);
                }                
            },
            complete:function(){

				Jit.UI.AjaxTips.Tips({show:false,tips:"正在操作"});	
            }
        });	
    },
    ProcessAction:function(OrderID,ActionCode){
    	
    	var self =this;

    	self.ajax({
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

                	location.reload();
                }else{

                	self.alert(data.Message);
                }                
            },
            complete:function(){

				Jit.UI.AjaxTips.Tips({show:false,tips:"正在操作"});	
            }
        });	
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