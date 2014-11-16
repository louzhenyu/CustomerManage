define(['tools', 'template', 'kkpager', 'artDialog', 'json2', 'ajaxform', 'datetimePicker'], function () {
    var page =
        {
			saveDataInfo: {},
            //关联到的类别
            elems:
            {
                uiMask: $(".jui-mask"),
				dataObj:{}
            },
            
            init: function () {
                var that = this;
                //var pageType = $.util.getUrlParam("pageType");
                //var pageName = decodeURIComponent($.util.getUrlParam("pageName"));
				
				that.queryDispatchingList();
                that.initEvent();
				//that.pager();
            },
            stopBubble: function (e) {
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                }
                else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
                e.preventDefault();
            },
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            //显示弹层
            showElements: function (selector) {
                this.elems.uiMask.show();
                $(selector).slideDown(500);
            },
            hideElements: function (selector) {
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
			alert: function (content) {
                var d = dialog({
                    fixed: true,
                    title: '提示',
                    content: content
                });
                d.showModal();
                setTimeout(function () {
                    d.close().remove();
                }, 3500);
            },
			pager: function(){
				var that = this;
				kkpager.generPageHtml({
					pno: 1,
					mode: 'click', //设置为click模式
					//总页码  
					total: 2,
					isShowTotalPage: false,
					isShowTotalRecords: false,
					isGoPage: false,
					//点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
					//适用于不刷新页面，比如ajax
					click: function (n) {
						//这里可以做自已的处理
						//...
						//处理完后可以手动条用selectPage进行页码选中切换
						this.selectPage(n);
						
						//that.loadMoreData(n);
					},
					//getHref是在click模式下链接算法，一般不需要配置，默认代码如下
					getHref: function (n) {
						return '#';
					}

				}, true);
			},
			loadMoreData: function(currentPage){
				var that = this;
                this.loadData.args.PageIndex = currentPage - 1;
                this.loadData.getEventsList(function (data) {
                    var list = data.Data.PanicbuyingEventList;
                    list = list ? list : [];
                    $.each(list, function (i) {
                        list[i].pageStr = (that.pageStr ? that.pageStr : "支付");
                    });
                    var html = bd.template("tpl_content", { list: list })
                    $("#goodsList").html(html);
                });
			},
            initEvent: function () {
                //初始化事件集
                var that = this,
					$tr;
					
				//获取时间插件
				$('#dispatching_startTime').datetimepicker({
					datepicker:false,
					format:'H:i',
					step:5
				});
				$('#dispatching_endTime').datetimepicker({
					datepicker:false,
					format:'H:i',
					step:5
				});	
					
				//单选框绑定事件
				$('.jui-dialog-dispatching .radioBox').bind('click',function(){
					var $this = $(this);
						
					$('#jui-dialog-'+that.elems.dataObj.typeid+' .radioBox').removeClass('on');
					$this.addClass('on');
					if($this.hasClass('unstartUs')){
						$this.parents('.dispatchingContent').find('.formInputBox').attr('disabled','disabled').parent().css({'border':'none'});
					}else{
						$this.parents('.dispatchingContent').find('.formInputBox').attr('disabled',false).parent().css({'border':'1px solid #dedede'});
					}
				});	
				
				//复选框选择
				$('.dispatchingListArea').delegate('.checkBox','click',function(){
					var $this = $(this);
					if($this.hasClass('on')){
						$this.removeClass('on');
					}else{
						$this.addClass('on');
					}
				});
				
				//编辑操作
				$('#dispatchingList').delegate('.operateWrap','click',function(e){
					var $this = $(this);
					$tr = $('.unstart',$this.parent());
					var deliveryId = that.elems.dataObj.typeid = $this.data('typeid');
					var obj = {};
					obj.Parameters = {};
					obj.Parameters.DeliveryId = deliveryId;
					obj = JSON.stringify(obj);
					

					that.entryInfo(obj,function(data){
						var item = that.saveDataInfo = data.DeliveryInfo;

						//判断状态是启用，还是停用
						if(item.Status==1){
							$('#jui-dialog-'+deliveryId+' .startUs').trigger('click');
						}else{
							$('#jui-dialog-'+deliveryId+' .unstartUs').trigger('click');
						}
						if(deliveryId == '1'){//送货上门
							$('#dispatching_describe').val(item.Description);
							$('#dispatching_cost').val(item.DeliveryAmount);
							$('#dispatching_mincost').val(item.AmountEnd);
						
						}else if(deliveryId == '2'){//到店自提
							$('#dispatching_stockup').val(item.StockUpPeriod);
							$('#dispatching_startTime').val(item.BeginWorkTime.substr(11,5));
							$('#dispatching_endTime').val(item.EndWorkTime.substr(11,5));
							$('#dispatching_pickup').val(item.MaxDelivery);
						}
						
					})

					that.showElements('#jui-dialog-'+deliveryId);
					$('#jui-dialog-'+deliveryId+' .radioBox').eq(0).trigger('click');
					
					
						
				});
                //关闭弹出层
                $(".jui-dialog-close").bind("click", function () {
                    that.elems.uiMask.slideUp();
                    $(this).parents('.jui-dialog').fadeOut();
                });
				$('.jui-dialog').delegate('.cancelBtn','click',function(){
					that.elems.uiMask.slideUp();
                    $(this).parents('.jui-dialog').fadeOut();
				});

				//点击保存按钮
				$('.jui-dialog').delegate('.saveBtn','click',function(){
					var REG_INT = /^[1-9]\d*$/;
					var $this = $(this),
						dateStr = '1970-01-01T',
						status = that.saveDataInfo.Status,
						radioValue = $('.radioBox.on',$this.parents('.dispatchingContent')).hasClass('startUs');

					var deliveryId = that.elems.dataObj.typeid;
					var obj = {};
					obj.Parameters = that.saveDataInfo;//原本的数据
					obj.Parameters.DeliveryId = deliveryId;//配送方式ID
					
					//判断状态[1启用\0停用]
					if(!radioValue){
						obj.Parameters.Status = 0;
						if(status == obj.Parameters.Status){
							that.elems.uiMask.slideUp();
							$this.parents('.jui-dialog').fadeOut();
							return false;
						}
					}else{
						obj.Parameters.Status = 1;
						//判断配送方式[1送货上门\2到店自提]
						if(deliveryId=='1'){
							if($("#dispatching_describe").val() == ''){
								that.alert('配送费描述不能为空！');
								return ;
							}

							if($("#dispatching_cost").val() == ''){
								that.alert('默认配送费不能为空！');
								return ;
							}else if(!REG_INT.test(parseInt($("#dispatching_cost").val()))){
								that.alert('默认配送费为大于0的整数！');
								return ;
							}
							
							if(!REG_INT.test(parseInt($("#dispatching_mincost").val()))){
								that.alert('订单金额为大于0的整数！');
								return ;
							}
							
							obj.Parameters.Description = $('#dispatching_describe').val(); //配送描述
							obj.Parameters.DeliveryAmount = $('#dispatching_cost').val();//默认配送费
							obj.Parameters.AmountEnd = $('#dispatching_mincost').val();//免配送费最低订单金额
						}else if(deliveryId=='2'){
							if($("#dispatching_stockup").val() == ''){
								that.alert('备货期不能为空！');
								return ;
							}else if(!REG_INT.test(parseInt($("#dispatching_stockup").val()))){
								that.alert('备货期为大于0的整数！');
								return ;
							}
							
							if($("#dispatching_startTime").val() == ''){
								that.alert('门店开始工作时间不能为空！');
								return ;
							}
							if($("#dispatching_endTime").val() == ''){
								that.alert('门店结束工作时间不能为空！');
								return ;
							}
							if($("#dispatching_startTime").val()>= $("#dispatching_endTime").val()){
								that.alert('开始时间不能大于或等于结束时间！');
								return ;
							}
							
							if($("#dispatching_pickup").val() == ''){
								that.alert('提货期最长不能为空！');
								return ;
							}else if(!REG_INT.test(parseInt($("#dispatching_pickup").val()))){
								that.alert('提货期为大于0的整数！');
								return ;
							}
							
							obj.Parameters.StockUpPeriod = $('#dispatching_stockup').val(); //备货期
							obj.Parameters.BeginWorkTime = dateStr+$('#dispatching_startTime').val()+':00';//门店工作时间开始
							obj.Parameters.EndWorkTime = dateStr+$('#dispatching_endTime').val()+':00';//门店工作时间结束
							obj.Parameters.MaxDelivery = $('#dispatching_pickup').val();//提货期最长
						}
					}
					var obj2=obj;
					obj = JSON.stringify(obj);
					//{ "Parameters": { "DeliveryId": "2", "TakeDeliveryId": null, "Status": 1, "StockUpPeriod":1, "BeginWorkTime":"0001-01-01T00:00:00", "EndWorkTime":"0001-01-01T00:00:00", "MaxDelivery":2 } }	
					that.saveDeliveryInfo(obj,function(data){
						//location.reload();
						if(obj2.Parameters.Status){
							alert('启用成功！');
							$tr.text('已启用').removeClass('blue');
						}else{
							alert('停启成功！');
							$tr.text('未启用').addClass('blue');
						}
						that.elems.uiMask.slideUp();
						$this.parents('.jui-dialog').fadeOut();
					});
					
				});
				
            },
			queryDispatchingList: function(callback){
				
				$.ajax({
					url: '/ApplicationInterface/Delivery/DeliveryEntry.ashx?type=Product&action=GetDeliveryList&req={}',//查询配送方式列表接口
					type: "get",
					success: function (data) {
						var data = JSON.parse(data);
						if ( data.IsSuccess == true) {
							var list = data.Data.DeliveryList;
							var html = bd.template("tpl_dispatchingList", { list: list })
							$("#dispatchingList").html(html);
							if (callback) {
								callback(data);
							}
						}else {
							alert(data.Message);
						}
					}
				});
			},
			entryInfo: function(params,callback){
				
				$.ajax({
					url: '/ApplicationInterface/Delivery/DeliveryEntry.ashx?type=Product&action=GetDeliveryDetail&req='+params,
					type: "get",
					success: function (data) {
						var data = JSON.parse(data);
						if (data.ResultCode == 0) {
							//表示成功
							if (callback) {
								callback(data.Data);
							}
						}else {
							alert(data.Message);
						}
					}
				});
				
			},
			//保存配送信息接口调用
			saveDeliveryInfo: function(params,callback){
				$.ajax({
					url: '/ApplicationInterface/Delivery/DeliveryEntry.ashx?type=Product&action=SaveDelivery&req='+params,
					type: "get",
					success: function (data) {
						var data = JSON.parse(data);
						if (data.ResultCode == 0) {
							if (callback) {
								callback(data);
							}
						}else {
							alert(data.Message);
						}
					}
				});
			}
			
        };

    page.init();
});