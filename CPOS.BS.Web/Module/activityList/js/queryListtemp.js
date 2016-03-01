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
            panlH:116                           // 下来框统一高度
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
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
				that.loadData.args.PageIndex = 1;
                that.loadData.getCommodityList(function(data){
                    that.renderTable(data);
                });
                $.util.stopBubble(e);
            });
            
            //列表操作事件用例
            that.elems.tabelWrap.delegate(".handle","click",function(e){
                var $this = $(this),
					$tr = $this.parents('tr'),
					rowIndex=$(this).data("index"),
					optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="delete"){
					that.deleteEvent(row.EventID,$tr);
                }
                if(optType=="running"){
					that.statusEvent(row.EventID,30,$this);
                }
				if(optType=="pause"){
                    that.statusEvent(row.EventID,20,$this);
                }
				if(optType=="down"){
					var date = new Date();
                    new Image().src=row.ImageUrl;
                    that.downloadFile(date.getTime()+'.jpg',row.ImageUrl);
				}

				if (optType == "detail") {
				    var mid = JITMethod.getUrlParam("mid");
				    location.href = "/Module/QuestionnaireNews/QuestionnaireInforDetail.aspx?EventID=" + row.EventID + "&mid=" + mid;
				}

				if((optType=="joinprize" || optType=="winprize")  && $this.text() != '0'){
					that.loadData2.action = (optType=="joinprize") ? 'events_lotterylog_list_query' : 'get_prizes_winner_list';
					
					if(optType=="joinprize"){
						that.loadData2.titName = '抽奖次数';
						that.loadData2.titValue = 'LotteryCount';
					}else{
						that.loadData2.titName = '奖品名称';
						that.loadData2.titValue = 'PrizeName';
					}
					
					that.loadData2.eventId = row.EventID;
					
					that.loadData2.page = 1;
					that.censusCount(function(data){
						 that.renderTable2(data);
					});
					
					$('.jui-mask').show();
					$('.jui-dialog-table').show();
				}
				$.util.stopBubble(e);
            });
			
			$('.jui-dialog-close').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog-table').hide();
				that.loadMoreData(that.loadData.args.PageIndex);
			})
			
			$('.jui-dialog-table .saveBtn').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog-table').hide();
				that.loadMoreData(that.loadData.args.PageIndex);
			})
			//跳转详情页
			that.elems.tabelWrap.delegate("tr","click",function(e){
                var $this = $(this),
					mid = JITMethod.getUrlParam("mid");
                var row = that.elems.tabel.datagrid('getSelected');
				//location.href = "activiDetail.aspx?EventID=" + row.EventID + "&mid=" + mid;
			});
			
			//跳转创建游戏页面
			$('#addNewGamesBtn').on('click',function(){
				location.href = 'activiDetail.aspx?mid='+JITMethod.getUrlParam("mid");
			})
            
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                //filed.value=filed.value=="0"?"":filed.value;
                //that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });
        },
		
        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
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
                data:data.LEventsList,
                //sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : '', //主键字段
                /*  pageNumber:1,*/
                //frozenColumns:[[]],
                columns : [[
                    {field : 'Title',title : '活动名称',width:110,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<a class="rowText" title="'+value+'">'+value.substring(0,long)+'...</a>'
                            }else{
                                return '<a class="rowText">'+value+'</a>'
                            }
                        }
                    },
                    {field : 'DrawMethodName',title : '活动方式',width:58,resizable:false,align:'center'},
                    {field : 'BeginTime',title : '开始时间',width:58,resizable:false,align:'center',
                    formatter: function (value, row, index) {
                        debugger;
                            return new Date(value).format("yyyy-MM-dd");
                        }
                    },
                    {field : 'EndTime',title : '结束时间',width:58,resizable:false,align:'center',
                        formatter:function(value ,row,index){
                            return new Date(value).format("yyyy-MM-dd");
                        }
                    },
					/*
                    {field : 'VipCardTypeName',title : '卡类型',width:58,resizable:false,align:'center'},
					{field : 'VipCardGradeName',title : '卡等级',width:58,resizable:false,align:'center'},
					*/
                    {field : 'JoinCount',title : '参与人数',width:58,align:'center',resizable:false,
                        formatter:function(value,row,index){
                          return '<p class="handle joinprize"  data-index="'+index+'"  data-oprtype="joinprize">'+value+'</p>';
                        }
                    },
                    {field : 'WinnerCount',title : '中奖人数',width:60,align:'center',resizable:false,
                        formatter:function(value,row,index){
                          return '<p class="handle winprize"  data-index="'+index+'"  data-oprtype="winprize">'+value+'</p>';
                        }
					},
                    { field: 'Status', title: '状态', width: 40, align: 'center', resizable: false }
                    //,
                    //{field : 'EventID',title : '操作',width:50,align:'left',resizable:false,
                    //formatter: function (value, row, index) {
                    //    var ophtml = "";
					//		var status = row.Status;
					//		if(status=='未开始'){
					//		    ophtml= '<p class="handle delete" title="删除" data-index="'+index+'" data-oprtype="delete"></p>';
					//		}else if(status=='运行中'){
					//		    ophtml = '<p class="handle running" title="暂停" data-index="' + index + '" data-oprtype="running"></p>';
					//		}else if(status=='暂停'){
					//		    ophtml = '<p class="handle pause" title="启动" data-index="' + index + '" data-oprtype="pause"></p>';
					//		}else if(status=='结束'){
					//		    ophtml = '';
					//		}

					//		if (row.DrawMethodName == "花样问卷")
					//		{
					//		    ophtml += '<p class="handle detail"  title="问卷详情" data-index="' + index + '" data-oprtype="detail"></p>';
					//		}

					//		ophtml += '<p class="handle down" title="下载二维码图片" data-index="' + index + '" data-oprtype="down"></p>';
					//		return ophtml;
                            
                    //    }
                    //}
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
        downloadFile:function (fileName, content){

            var aLink = document.createElement('a');
            //var blob = new Blob([content]);
            var evt = document.createEvent("HTMLEvents");
            evt.initEvent("click", false, false);//initEvent 不加后两个参数在FF下会报错, 感谢 Barret Lee 的反馈
            aLink.download = fileName;
            aLink.href =content; //URL.createObjectURL(content);
            aLink.dispatchEvent(evt);
			
        },
		deleteEvent:function(id,$dom){
			var that = this;
			$.util.ajax({
				  url: "/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
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
				  url: "/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
				  data:{
					  action:'StartOrStopEvent',
					  EventId:id,
					  Status:status
				  },
				  success: function (data) {
					  if(data.IsSuccess && data.ResultCode == 0) {
						  	if(status==30){
								$dom.attr('class','handle pause').data('oprtype','pause');
								$('td[field="Status"] div',$dom.parents('tr')).text('暂停');
							}else{
								$dom.attr('class','handle running').data('oprtype','running');
								$('td[field="Status"] div',$dom.parents('tr')).text('运行中');
							}
					  }else{
						alert(data.Message);
					  }
				  }
			});
		},
		loadData2: {
			page:1,
			start:0,
			limit:4,
			action:'',
			eventId:'',
			titName:'',
			titValue:''
		},
        //人数统计，弹层
        censusCount:function(callback){
            var that=this;
           	$.util.oldAjax({
				  url: "/Module/WEvents/Handler/EventsHandler.ashx",
				  data:{
					  action:that.loadData2.action,
					  EventId:that.loadData2.eventId,
					  page:that.loadData2.page,
					  //start:that.loadData2.start,
					  limit:that.loadData2.limit
				  },
				  success: function (data) {
					  var result = data.topics;
					  if(result.length>0){
						if(callback){
							callback(data);
						}
					  }else{
						alert("加载数据不成功");
					  }
				}
			});
        },
		renderTable2: function (data) {
            var that=this;
			if(!data.topics){
                data.topics=[];
            }
            that.elems.tabel2.datagrid({
                method : 'post',
                singleSelect : true, //单选
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                data:data.topics,
                columns : [[
                    {field : 'VipName',title : '会员名称',width:30,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    {field : that.loadData2.titValue,title : that.loadData2.titName,width:40,align:'center',resizable:false,align:'center',
						formatter:function(value ,row,index){
							return value;
						}
					},
					{field : 'CreateTime',title : '时间',width:30,resizable:false,align:'center',
                        formatter:function(value ,row,index){
                            return new Date(value).format("yyyy-MM-dd hh:mm:ss");
                        }
                    }
                ]],
				onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句
                }
            });
			
			var pageCount = data.totalCount%that.loadData2.limit==0? data.totalCount/that.loadData2.limit: data.totalCount/that.loadData2.limit +1;
			
            kkpager.generPageHtml({
				pagerid:'kkpager2',
                pno: that.loadData2.page,
                mode: 'click', //设置为click模式
                total: pageCount,//总页数
                totalRecords: data.totalCount,//总条数
                isShowTotalPage: true,
                isShowTotalRecords: true,
                click: function(n){
                    this.selectPage(n);
                    that.loadMoreData2(n);
                }
            }, true);
        },
		//加载更多的资讯或者活动
        loadMoreData2: function (currentPage) {
            var that = this;
            that.loadData2.page = currentPage;
            that.censusCount(function(data){
                that.renderTable2(data);
            });
        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 10,
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
				var item_name = this.seach.form.item_name,
					item_startTime = this.seach.form.item_startTime,
					item_endTime = this.seach.form.item_endTime;
                $.util.ajax({
                      url: "/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
                      data:{
                          action:'GetEventList',
						  Title:item_name,
						  DrawMethodName:'',
						  BeginTime:item_startTime,
						  EndTime:item_endTime,
                          PageSize:this.args.PageSize,
                          PageIndex:this.args.PageIndex
                      },
                      success: function (data) {
						  if (data.IsSuccess && data.ResultCode == 0) {
								var result = data.Data,
									lEventsList = result.LEventsList;
								if(callback) {
									callback(result);
								}
						  }else{
                            alert("加载数据不成功");
                          }
                    }
                });
            }
			 
        }

    };
    page.init();
});


