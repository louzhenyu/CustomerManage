define(['tools', 'template', 'kkpager', 'artDialog', 'json2', 'ajaxform','easyui','datetimePicker'], function () {
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
				var tops=$(document).scrollTop()+70;
				$(".jui-dialog").css({"top":tops+"px"})
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
						$this.parents('.dispatchingContent').find('.checkBox').removeClass('on');
					}else{
						$this.parents('.dispatchingContent').find('.formInputBox').attr('disabled',false).parent().css({'border':'1px solid #ddd'});
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

				$('.checkBox').bind('click',function(){
					var radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type');//启用为1，停用为2；
					var $this = $(this);
					if(radioType=='1'){
						if($this.hasClass('on')){
							$this.removeClass('on');
							$('#dispatching_pickup').attr('readonly','readonly');
						}else{
							$this.addClass('on');
							$('#dispatching_pickup').removeAttr('readonly');
						}
					}
				});
				
				//到店自提，提货设置
				$('.timePassageArea').delegate('.timeItem .editBtn','click',function(){
					var $this = $(this),
						$timeItem = $this.parents('.timeItem'),
						$startTime = $('.startTime',$timeItem),
						$endTime = $('.endTime',$timeItem),
						isChecked = $('.timePassageArea .checkBox').hasClass('on'),
						lengthTime = $timeItem.index(),//获取当前修改时间所在位置;
						radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type');//启用为1，停用为2;
					   

					//that.timeIndex = $($timeItem,'.timePassage').index();
					//alert(that.timeIndex);
					that.IsEdit = 1;
					if(radioType=='1'&&isChecked){
						$('.addTimePassageBox').show();
						$('.addTimePassageBox').css({'position':'absolute','top':'15px','right':'-30px'});
						$('.addTimePassageBox').attr('data-length',lengthTime);
						$('#dispatching_startTime').val($startTime.text());
						$('#dispatching_endTime').val($endTime.text());
					}
					
					$timeItem.remove();
						
				});
				//移除时间段
				$('.timePassageArea').delegate('.timeItem .removeBtn','click',function(){
					var $this = $(this),
						isChecked = $('.timePassageArea .checkBox').hasClass('on'),
						radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type');//启用为1，停用为2;
					if(radioType=='1'&&isChecked){
						$this.parents('.timeItem').remove();
					}

				});
				
				//保存时间段
				$('.timeSaveBtn').bind('click',function(){
					var startTime = $("#dispatching_startTime").val(),
						endTime = $("#dispatching_endTime").val(),
						isChecked = $('.timePassageArea .checkBox').hasClass('on'),
						lengthTime = $('.addTimePassageBox').attr('data-length'),//获取当前修改时间所在位置;
						lengthTimeCount = $('.timePassage').children('.timeItem').length,
						radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type');//启用为1，停用为2；

					that.IsEdit = 0;
					if(radioType=='1'&&isChecked){
						if(startTime == ''){
							that.alert('开始时间段不能为空！');
							return ;
						}
						if(endTime == ''){
							that.alert('结束时间段不能为空！');
							return ;
						}
						if(startTime >= endTime){
							that.alert('开始时间段不能大于或等于结束时间段！');
							return ;
						}
						if(parseInt(lengthTime)+1<=parseInt(lengthTimeCount)){
							$('.timeItem').eq(lengthTime).find('.startTime').html(startTime);
							$('.timeItem').eq(lengthTime).find('.endTime').html(endTime);
							$("#dispatching_startTime").val('');
							$("#dispatching_endTime").val('');
							$('.addTimePassageBox').hide();
						}
						else if(lengthTime =="null"||parseInt(lengthTime)+1>=parseInt(lengthTimeCount)){
							var timeStr = '<div class="timeItem">\
							<p><span class="startTime">'+startTime+'</span> 至 <span class="endTime">'+endTime+'</span></p>\
							<span class="editBtn">修改</span>\
							<span class="removeBtn">删除</span>\
							</div>';
							$('.timePassage').append(timeStr);
							$("#dispatching_startTime").val('');
							$("#dispatching_endTime").val('');
							$('.addTimePassageBox').hide();
						}
					}
				});
				//取消时间段
				$('.timeCancelBtn').bind('click',function(){
					var radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type'),//启用为1，停用为2；
						isChecked = $('.timePassageArea .checkBox').hasClass('on');

					if(radioType =='1'&&isChecked){
						if(that.IsEdit==1){
							$('.timeSaveBtn').trigger('click');
						}else{
							$("#dispatching_startTime").val('');
							$("#dispatching_endTime").val('');
							$('.addTimePassageBox').hide();
						}
					}
				});

				//编辑操作
				$('.tableWrap').delegate('.operateWrap','click',function(e){
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
							$('#dispatching_stockup').val(item.StockUpPeriod);//备货期
							//$('#dispatching_startTime').val(item.BeginWorkTime.substr(11,5));
							//$('#dispatching_endTime').val(item.EndWorkTime.substr(11,5));
							$('#dispatching_pickup').val(item.MaxDelivery);//可提货天数
							if(!!item.MaxDelivery){
								$('.deliveryNumBox .checkBox').addClass('on');
							}
							//提货时间段
							var quantumList = item.QuantumList || [];
							if(!!quantumList.length){
								$('.timePassageArea .checkBox').addClass('on');
								var timeStr = '';
								for(var i=0;i<quantumList.length;i++){
									var splitObj = quantumList[i].Quantum.split('-');
									timeStr += '<div class="timeItem">\
										<p><span class="startTime">'+splitObj[0]+'</span> 至 <span class="endTime">'+splitObj[1]+'</span></p>\
										<span class="editBtn">修改</span>\
										<span class="removeBtn">删除</span>\
									</div>';
								}
								$('.timePassage').html(timeStr);
							}
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

				//新增时间段
				$('#addTimeBtn').bind('click',function(){
					var isDis = $('.addTimePassageBox').css('display'),
						isChecked = $('.timePassageArea .checkBox').hasClass('on'),
						isNull = $('.timePassage').children('.timeItem').length,
					    radioType = $('.jui-dialog-dispatching .radioBox.on').attr('date-type');//启用为1，停用为2;
					$('.addTimePassageBox').attr('data-length','null');
					if(radioType=='1'&&isChecked){
						if(isDis == 'none'){
							if(isNull =='0'){
								$('.addTimePassageBox').css({'position':'relative','top':'0','right':'0'});
							}
							$('.addTimePassageBox').show();
						}else{
							alert('请把当前的时间段设置完成，再添加！');
						}
					}
				});

				//点击保存按钮
				$('.jui-dialog').delegate('.saveBtn','click',function(){
					var REG_INT = /^[0-9]\d*$/;
					var floatMath = /^(([0-9]+\.[0-9]*[0-9][0-9]*)|([0-9]*[0-9][0-9]*\.[0-9]+)|([0-9]*[0-9][0-9]*))$/;
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
						//判断配送方式[1送货上门\2店自提]
						if(deliveryId=='1'){							if($("#dispatching_describe").val() == ''){
								that.alert('配送费描述不能为空！');
								return ;
							}
							if($("#dispatching_cost").val() == ''){
								that.alert('默认配送费不能为空！');
								return ;
							}else if(parseInt($("#dispatching_cost").val())>1000){
								that.alert('默认配送费为小于或等于1000的整数！');
								return ;
							}
							else if(!floatMath.test($("#dispatching_cost").val())){
								$("#dispatching_cost").css('border','1px solid red');
								that.alert('默认配送费格式不对,请输入正确的数字');
								return;
							}else if(!REG_INT.test(parseInt($("#dispatching_cost").val()))){
								$("#dispatching_cost").css('border','1px solid red');
								that.alert('默认配送费为大于0的整数！');
								return ;
							}
							else if(!floatMath.test($("#dispatching_mincost").val())){
								$("#dispatching_mincost").css('border','1px solid red');
								that.alert('免配送费最低订单金额格式不对,请输入正确的数字');
								return;
							}else if(!REG_INT.test(parseInt($("#dispatching_mincost").val()))){
								$("#dispatching_mincost").css('border','1px solid red');
								that.alert('免配送费最低订单金额为大于0的整数！');
								return ;
							}
							/*
							 if(!REG_INT.test(parseInt($("#dispatching_mincost").val()))){
							 that.alert('！');
							 return ;
							 }
							 */
							if(parseInt($("#dispatching_mincost").val())>1000){
								that.alert('订单金额为小于或等于1000的整数！');
								return ;
							}

							obj.Parameters.Description = $('#dispatching_describe').val(); //配送描述
							obj.Parameters.DeliveryAmount = $('#dispatching_cost').val();//默认配送费
							obj.Parameters.AmountEnd = $('#dispatching_mincost').val()||0 ;//免配送费最低订单金额
						}else if(deliveryId=='2'){
							var isChecked = $('.timePassageArea .checkBox').hasClass('on'),
								lengthTimeCount = $('.timePassage').children('.timeItem').length;//添加的时间段

							if($('.addTimePassageBox').css('display')!='none'){
								that.alert('请把当前的时间段设置完毕，再保存！');
								return ;
							}
							if($("#dispatching_stockup").val() == ''){
								that.alert('备货期不能为空！');
								return ;
							}else if(!REG_INT.test($("#dispatching_stockup").val())){
								that.alert('备货期只能为正整数！');
								return ;
							}

							if($("#dispatching_pickup").val() == ''){
								that.alert('提货期不能为空！');
								return ;
							}else if(!REG_INT.test($("#dispatching_pickup").val())){
								that.alert('提货期只能为正整数！');
								return ;
							}
							if(isChecked&&lengthTimeCount=='0'){
								that.alert('请添加时间段！');
								return ;
							}
							/*
							 obj.Parameters.BeginWorkTime = dateStr+$('#dispatching_startTime').val()+':00';//门店工作时间开始
							 obj.Parameters.EndWorkTime = dateStr+$('#dispatching_endTime').val()+':00';//门店工作时间结束
							 */
							obj.Parameters.StockUpPeriod = $('#dispatching_stockup').val() || 0; //备货期
							obj.Parameters.MaxDelivery = 0;
							if($('.deliveryNumBox .checkBox').hasClass('on')){
								obj.Parameters.MaxDelivery = $('#dispatching_pickup').val();//提货期最长
							}
							obj.Parameters.QuantumList = [];
							if($('.timePassageArea .checkBox').hasClass('on')){
								var $timeItem = $('.timePassage .timeItem');
								for(var i=0;i<$timeItem.length;i++){
									var timeStr = $('.startTime',$timeItem.eq(i)).text()+'-'+$('.endTime',$timeItem.eq(i)).text();
									obj.Parameters.QuantumList.push({"Quantum":timeStr});
								}

							}

						}
					}
					var obj2=obj;
					obj = JSON.stringify(obj);
					//{ "Parameters": { "DeliveryId": "2", "TakeDeliveryId": null, "Status": 1, "StockUpPeriod":1, "BeginWorkTime":"0001-01-01T00:00:00", "EndWorkTime":"0001-01-01T00:00:00", "MaxDelivery":2 } }

					that.saveDeliveryInfo(obj,function(data){
						that.queryDispatchingList();
						if(obj2.Parameters.Status){
							alert('启用成功！');
							$tr.text('已启用').removeClass('blue');
						}else{
							alert('停用成功！');
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

							$("#dispatchingList").datagrid({
								method: 'post',
								iconCls: 'icon-list', //图标
								singleSelect: true, //单选
								// height : 332, //高度
								fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
								striped: true, //奇偶行颜色不同
								collapsible: true,//可折叠
								//数据来源
								data: data.Data.DeliveryList,
								/*sortName : 'MembershipTime', //排序的列*/
								sortOrder: 'desc', //倒序
								remoteSort: true, // 服务器排序
								idField: 'OrderID', //主键字段

								columns: [[

									{field: 'deliveryName', title: '配送方式', width: 196, align: 'left', resizable: false},
									{field: 'IsOpen', title: '状态', width: 182, align: 'left', resizable: false,
										formatter:function(value ,row,index){

											if(value){
												return '<div class="unstart">已启用</div>'
											}else{
												return '<div class="unstart blue">未启用</div>'
											}
										}
									},
									{
										field: 'deliveryId', title: '编辑', width: 46, align: 'left', resizable: false,
										formatter: function (value, row, index) {

											return '<div class="operateWrap" title="编辑" data-typeid="'+value+'" ><span class="editIcon opt exit"></span></div>';
										}
									}
								]]
							});
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