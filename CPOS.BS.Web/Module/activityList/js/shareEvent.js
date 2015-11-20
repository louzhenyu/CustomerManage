define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
			tabel2:$("#gridTable2"), 
            tabelWrap:$('#tableWrap'),
			tabelWrap2:$('#tableWrap2'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
			dataMessage:$(".dataMessage"),
			dataMessage2:$(".dataMessage2"),
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116,                           // 下来框统一高度
			domain:""
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this;
            //列表操作事件用例
            that.elems.tabelWrap.delegate(".handle","click",function(e){
                var $this = $(this),
					$tr = $this.parents('tr'),
					rowIndex=$(this).data("index"),
					optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="delete"){
					that.deleteEvent(row.ShareId,$tr);
                }
                if(optType=="running"){
					that.statusEvent(row.ShareId,0,$this);
                }
				if(optType=="pause"){
                    that.statusEvent(row.ShareId,1,$this);
                }
				$.util.stopBubble(e);
            });
			
			
			//追加
			$('#tableWrap').delegate('.addBtn','click',function(){
				var $this = $(this),
					$tr = $this.parents('tr'),
					$num = $('.numBox',$tr),
					num = $num.text()-0,
					prizesId = $this.data('prizesid'),
					shareId = $this.data('shareid');
				$('.jui-mask').show();
				$('.jui-dialog-prizeCountAdd').show();
				$('.jui-dialog-prizeCountAdd .saveBtn').unbind('click');
				$('.jui-dialog-prizeCountAdd .saveBtn').bind('click',function(){
					var	addNum = $('#prizeCountAdd').val()-0,
						count = num+addNum;
					//提交接口
					$.util.ajax({
						url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventShareHandler.ashx",
						data: {
							action: 'AppendEventShare',
							ShareId: shareId,
							AppendQty: addNum,
							PrizesId: prizesId
						},
						success: function(data){
							if(data.IsSuccess && data.ResultCode == 0) {
								$('.jui-mask').hide();
								$('.jui-dialog-prizeCountAdd').hide();
								$num.text(count);
							}else{
								alert(data.Message);
							}
						}
					});
					
					
				});
			});
			
			
			//分享配置
			$('#addShareBtn').bind('click',function(){
				$('.jui-mask').show();
				$('.jui-dialog-addShare').show();
			});
			
			//保存分享设置
			$('.jui-dialog-addShare .saveBtn').bind('click',function(){
				if($('#addShareForm').form('validate')){//a03d28e78b2c18104d4812ba18a5c69b
					var prams = {action:'AddEventShare'},
						fields = $('#addShareForm').serializeArray();
					console.log(fields);
					for(var i=0;i<fields.length;i++){
						var obj = fields[i];
						prams[obj['name']] = obj['value'];
						
						if(obj['name']=='Point' && obj['value']==''){
							prams[obj['name']] = 0;
							prams['PrizeName'] = $('#couponOption').combobox('getText');
						}
						if(obj['name']=='Point' && obj['value']!=''){
							prams['PrizeName'] = '积分';
						}
						
					}
					console.log(prams);
					that.setSaveShare(prams);
				}
			});
			
			
			$('.jui-dialog-close').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			})
			
			$('.jui-dialog .cancelBtn').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			})
            
        },

		
        //加载页面的数据请求
        loadPageData: function () {
            var that = this;
			that.loadData.args.PageIndex = 1;
			that.loadData.getCommodityList(function(data){
				that.renderTable(data);
			});
			that.getPrizeTypeList();
			that.getWorkingEventList();
			that.getPersonCountList();
        },
		
		
		
        //渲染tabel
        renderTable: function (data) {
            var that=this;
			if(!data.LEventsList){
                data.LEventsList=[];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
				
                method : 'post',
                //iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                //collapsible : true,//可折叠
                //数据来源
                data:data.EventShareList,
                //sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : '', //主键字段
                /*  pageNumber:1,*/
                //frozenColumns:[[]],
                columns : [[
                    {field : 'PrizeName',title : '奖品名称',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<a class="rowText" title="'+value+'">'+value.substring(0,long)+'...</a>'
                            }else{
                                return '<a class="rowText">'+value+'</a>'
                            }
                        }
                    },
                    {field : 'CountTotal',title : '奖品数量',width:50,resizable:false,align:'center',
						formatter:function(value ,row,index){
                            return '<p class="numBox">'+value+'</p>';
                        }
					},
                    {field : 'EventName',title : '对应活动',width:100,resizable:false,align:'center'},
					{field : 'WinnerCount',title : '获奖人数',width:50,resizable:false,align:'center'},
                    {field : 'EventState',title : '状态',width:50,align:'center',resizable:false,align:'center'},
                    {field : 'PrizesID',title : '追加',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="handle addBtn" data-prizesid="'+value+'" data-shareid="'+row.ShareId+'" data-index="'+index+'"></p>';
                        }
                    },
                    {field : 'ShareState',title : '操作',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
							var status = row.ShareState;
							
							if(value=='未开始'){
								return '<p class="handle delete" data-index="'+index+'" data-oprtype="delete"></p>';
							}else if(value=='启用'){
								return '<p class="handle running" data-index="'+index+'" data-oprtype="running"></p>';
							}else if(value=='停用'){
								return '<p class="handle pause" data-index="'+index+'" data-oprtype="pause"></p>';
							}else if(value=='结束'){
								return '';
							}
                            
                        }
                    }
                ]],
				onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
					if(data.rows.length>0) {
						that.elems.dataMessage.hide();
						$('#kkpager').show();
					}else{
						that.elems.dataMessage.show();
						$('#kkpager').hide();
					}
					//that.elems.tabel.datagrid('getSelected');
                }
            });



            //分页
            //data.Data={};
            //data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            //var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
				pagerid:'kkpager',
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.PageCount,
                totalRecords: data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //让  tbody的内容变成加载中的图标
                    //var table = $('table.dataTable');//that.tableMap[that.status];
                    //var length = table.find("thead th").length;
                    //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                    that.loadMoreData(n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },
		deleteEvent:function(id,$dom){
			var that = this;
			$.util.ajax({
				  url:  that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
				  data:{
					  action:'DeleteEvent',
					  EventId:id
				  },
				  success: function (data) {
					  if(data.IsSuccess && data.ResultCode == 0) {
							$dom.reomve();
					  }else{
						alert(data.Message);
					}
				}
			});
		},
		statusEvent:function(id,status,$dom){
			var that = this;
			$.util.ajax({
				  url:  that.elems.domain+"/ApplicationInterface/Module/WEvents/EventShareHandler.ashx",
				  data:{
					  action:'StartOrStopShare',
					  ShareId:id,
					  state:status
				  },
				  success: function (data) {
					  if(data.IsSuccess && data.ResultCode == 0) {
						  	if(status==0){
								$dom.attr('class','handle pause').data('oprtype','pause');
							}else{
								$dom.attr('class','handle running').data('oprtype','running');
							}
					  }else{
						alert(data.Message);
					  }
				  }
			});
		},
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 7,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                item_category_id:null,
                SalesPromotion_id:null,
                form:{
                    item_code:"",
                    item_name:"",
                    item_startTime:'',
                    item_endTime:''
                }
            },
            opertionField:{},
            getCommodityList: function (callback) {
				var that = this;
                $.util.ajax({
                      url: "/ApplicationInterface/Module/WEvents/EventShareHandler.ashx",
                      data:{
                          action:'GetEventShareList',
                          PageSize:this.args.PageSize,
                          PageIndex:this.args.PageIndex
                      },
                      success: function (data) {
						  if (data.IsSuccess && data.ResultCode == 0) {
								var result = data.Data;
								if(callback) {
									callback(result);
								}
						  }else{
                            alert("加载数据不成功");
                          }
                    }
                });
            }
			 
        },
		getPrizeTypeList:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetPrizeType'
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							PrizeTypeList = result.PrizeTypeList;
						$('#prizeOption').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'PrizeTypeCode',
							textField: 'PrizeTypeName',
							data:PrizeTypeList,
							onSelect: function(param){
								var $couponItem = $('#couponItem'),
									$integralItem = $('#integralItem');
								$couponItem.hide();
								$integralItem.hide();	
								if(param.PrizeTypeCode == 'Point'){
									$integralItem.show();
								}else if(param.PrizeTypeCode == 'Coupon'){
									//获取优惠券列表
									that.getCouponList();
									$couponItem.show();
								}
							}
						});
						
					}else{
						alert(data.Message);
					}
				}
			});
			
		},
		getCouponList:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetCouponTypeInfo'
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							CouponTypeList = result.CouponTypeList;
						$('#couponOption').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'CouponTypeID',
							textField: 'CouponTypeName',
							data:CouponTypeList
						});
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getWorkingEventList:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
				data: {
					action: 'GetWorkingEventList'
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							lEventsList = result.LEventsList;
						$('#workingEvent').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'EventID',
							textField: 'Title',
							data:lEventsList
						});
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getPersonCountList:function(){
			var that = this,
				personCountList = [
					{
						Id: "OnlyOne",
						Name: "仅一次"
					},
					{
						Id: "NoLimit",
						Name: "无限制"
					}
				];
			$('#shareTimes').combobox({
				width: 190,
				height: that.elems.height,
				panelHeight: that.elems.panlH,
				lines:true,
				valueField: 'Id',
				textField: 'Name',
				data:personCountList
			});
		},
		setSaveShare:function(params){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventShareHandler.ashx",
				data: params,
				success: function(data) {
					if(data.IsSuccess && data.ResultCode == 0) {
						that.loadMoreData(1);
						$('.jui-mask').hide();
						$('.jui-dialog').hide();
						alert('成功添加分享设置！');
					}else{
						alert(data.Message);
					}
				}
			});
		},

    };
    page.init();
});

