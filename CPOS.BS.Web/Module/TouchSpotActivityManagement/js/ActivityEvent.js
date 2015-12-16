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
        ValueCard: '',//储值卡号
        submitappcount:false,//是否正在提交追加表单
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
                if (optType == "edit") {
                    that.showEdit();
                    that.update(row);
                    that.showRewardType(row.PrizeType);
                    that.showShareEventId(row.ContactTypeCode);
                    $('#addShareForm').form('load', { ContactTypeCode: row.ContactTypeCode, ContactEventName: row.ContactEventName, BeginDate: row.BeginDate.split(" ")[0].replace("/", "-").replace("/", "-"), EndDate: row.EndDate.split(" ")[0].replace("/", "-").replace("/", "-"), PrizeType: row.PrizeType, Integral: row.Integral, CouponTypeID: row.CouponTypeID, EventId: row.EventId, ChanceCount: row.ChanceCount, ContactEventId: row.ContactEventId, PrizeCount: row.PrizeCount, ShareEventId: row.ShareEventId, RewardNumber: row.RewardNumber.replace(/\s+/g, "") });
                }
                if(optType=="running"){
                    that.statusEvent(row.ContactEventId, 3, $this);
                }
				if(optType=="pause"){
				    that.statusEvent(row.ContactEventId, 2, $this);
                }
				$.util.stopBubble(e);
            });
			


            that.getActivityEventList();
            that.getCouponList();
			
			//添加活动
            $('#addShareBtn').bind('click', function () {

                that.showEdit();
                $('#addShareForm').form('load', { ContactTypeCode: "", ContactEventName: "", BeginDate: "", EndDate: "", PrizeType: "", Integral: "", CouponTypeID: "", EventId: "", ChanceCount: "", ContactEventId: "", PrizeCount: "", RewardNumber: "", ShareEventId: "" });
			    that.update();
			});


            //追加
			$('#tableWrap').delegate('.addBtn', 'click', function () {
			    $('#prizeCount_Add').form('load', {prizeCount:""});
			    var $this = $(this),
					$tr = $this.parents('tr'),
					$num = $('.numBox', $tr),
					num = $num.text() - 0,
					Contact_EventId = $this.data('contacteventid');
			    $('.jui-mask').show();
			    $('.jui-dialog-prizeCountAdd').show();
			    $('.jui-dialog-prizeCountAdd .saveBtn').unbind('click');
			    $('.jui-dialog-prizeCountAdd .saveBtn').bind('click', function () {
			        if (!that.submitappcount) {
			            that.submitappcount = true;
			            if ($('#prizeCount_Add').form('validate')) {
			                var addNum = $('#prizeCountAdd').val() - 0,
                                count = num + addNum;
			                //提交接口
			                $.util.ajax({
			                    url: that.elems.domain + "/ApplicationInterface/Gateway.ashx",
			                    data: {
			                        "action": 'WEvents.ContactEvent.SetContactEvent',
			                        "ContactEventId": Contact_EventId,
			                        "PrizeCount": addNum,
			                        "Method": "Append"

			                    },
			                    success: function (data) {
			                        if (data.IsSuccess && data.ResultCode == 0) {

			                            if (data.Data.Success) {
			                                $('.jui-mask').hide();
			                                $('.jui-dialog-prizeCountAdd').hide();
			                                that.loadPageData();
			                            } else {

			                                $.messager.alert('提示', data.Data.ErrMsg);
			                            }

			                            
			                        } else {
			                            $.messager.alert('提示', data.Message);
			                        }

			                        that.submitappcount = false;
			                    }
			                });
			            }
			        } else {

			            $.messager.alert('提示', '正在提交请稍后！');
			        }

			    });
			});

			
			//保存活动
			$('.jui-dialog-addShare .saveBtn').bind('click', function () {
			    
				if($('#addShareForm').form('validate')){//a03d28e78b2c18104d4812ba18a5c69b
				    var prams = { action: 'WEvents.ContactEvent.SetContactEvent' },
						fields = $('#addShareForm').serializeArray();
				    console.log(fields);
					for(var i=0;i<fields.length;i++){
						var obj = fields[i];
						prams[obj['name']] = obj['value'];
						
						if (obj['name'] == 'ChanceCount' && prams['PrizeType'] != 'Chance') {
							prams['ChanceCount'] = 0;
						}
						
						if (obj['name'] == 'Integral' && prams['PrizeType'] != 'Point') {
					        prams['Integral'] = 0;
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
			that.getWorkingEventList();
        },
		
		
		
        //渲染tabel
        renderTable: function (data) {
            var that = this;
            if (!data.IsSuccess) {
                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //单选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data.Data.ContactEventList,
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'ContactEventId', //主键字段
                /* pageNumber:1,*/
                /*
                frozenColumns:[[
                    {
                        field : 'ck',
                        width:70,
                        title:'全选',
                        align:'center',
                        checkbox : true
                    }
                ]],
				*/
                columns: [[
                    {
                        field: 'ContactEventName', title: '活动名称', width: 200, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var long = 17;
                            var html = ""
                            if (value && value.length > long) {
                                html = '<div class="rowTextnew" title="' + value + '">' + value.substring(0, long) + '...</div>'
                            } else {
                                html = '<div class="rowTextnew">' + value + '</div>'
                            }

                            return html
                        }
                    },
                    {
                        field: 'ContactTypeCode', title: '触点类型', width: 100, align: 'center', resizable: false, formatter: function (value, row, index) {
                            var PrizeType;
                            
                            switch (value) {
                                case "Focus": PrizeType = "关注"; break;
                                case "Reg": PrizeType = "注册"; break;
                                case "Comment": PrizeType = "评论"; break;
                                case "SignIn": PrizeType = "签到"; break;
                                case "Share": PrizeType = "分享"; break;

                            }
                            return PrizeType;
                        }
                    },
                    {
                        field: 'Reward', title: '奖品名称', width: 120, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var long = 8;
                            var html = ""
                            if (value && value.length > long) {
                                html = '<div class="rowTextnew" title="' + value + '">' + value.substring(0, long) + '...</div>'
                            } else {
                                html = '<div class="rowTextnew">' + value + '</div>'
                            }

                            return html
                        }
                    },
                    {
                        field: 'PrizeCount', title: '奖品数量', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return '<p class="numBox">' + value + '</p>';
                        }
                    },
                    {
                        field: 'JoinCount', title: '参与人数', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        field: 'PrizesID', title: '追加', width: 50, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return '<p class="handle addBtn" data-prizesid="' + value + '" data-ContactEventId="' + row.ContactEventId + '" data-index="' + index + '"></p>';
                        }
                    },
                    {
                        field: 'Date', title: '日期', width: 200, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        field: 'Status', title: '状态', width: 50, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var staus;
                            switch (value) {
                                case 1: staus = "未开始"; break;

                                case 2: staus = "进行中"; break;
                                case 3: staus = "暂停"; break;
                                case 4: staus = "已结束"; break;
                                default: staus = "未开始"; break;
                            }
                            return staus;
                        }
                    },
					{
					    field: 'ContactEventId', title: '操作', width: 100, align: 'center', resizable: false,
					    formatter: function (value, row, index) {
					        var htmlStr = '';
					        status = row.Status;
					        switch (status) {
					            case "1": htmlStr = '<a href="javascript:;" title="编辑" data-index=' + index + ' data-oprtype="edit" class="handle editBtn"></a>'; break;
					            case "2": htmlStr = '<a href="javascript:;" title="暂停" class="handle running"  data-oprtype="running" data-index=' + index + ' ></a>'; break;
					            case "3": htmlStr = '<a href="javascript:;" title="启动" class="handle iconPause"  data-oprtype="pause" data-index=' + index + ' ></a>'; break;
					            case "4": htmlStr = ''; break;
					            default: htmlStr = '<a href="javascript:;" title="编辑"  data-index=' + index + ' data-oprtype="edit" class="handle editBtn"></a>'; break;
					        }

					        return htmlStr;
					    }
					}


                ]],
                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow: function (rowindex, rowData) {
                        if (that.elems.click) {
                            var row = rowData;
                            that.update(row);
                            that.showRewardType(row.PrizeType);
                            that.showShareEventId(row.ContactTypeCode);
                            $('#addShareForm').form('load', { ContactTypeCode: row.ContactTypeCode, ContactEventName: row.ContactEventName, BeginDate: row.BeginDate.split(" ")[0].replace("/", "-").replace("/", "-"), EndDate: row.EndDate.split(" ")[0].replace("/", "-").replace("/", "-"), PrizeType: row.PrizeType, Integral: row.Integral, CouponTypeID: row.CouponTypeName, EventId: row.EventName, ChanceCount: row.ChanceCount, ContactEventId: row.ContactEventId, PrizeCount: row.PrizeCount, ShareEventId: row.ShareEventName, RewardNumber: row.RewardNumber.replace(/\s+/g, "") });
                            that.hideEdit();
                        }
                   
                    /*
                     if(that.elems.click){
                     that.elems.click = true;
                     var mid = JITMethod.getUrlParam("mid");
                     location.href = "commodityExit.aspx?Item_Id=" + rowData.Item_Id +"&mid=" + mid;
                     }
					*/
                }, onClickCell: function (rowIndex, field, rowData) {
                    if (field != "PrizesID" && field != "ContactEventId") {//在每一列有操作 而点击行有跳转页面的操作才使用该功能。此处不释与注释都可以。
                       that.elems.click=true;
                    }else{
                       that.elems.click=false;
                    }
                }

            });


            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPage,
                totalRecords: data.Data.TotalCount,
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
            if ((that.loadData.opertionField.displayIndex || that.loadData.opertionField.displayIndex == 0)) {  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click", true);
                that.loadData.opertionField.displayIndex = null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },
		statusEvent:function(id,status,$dom){
			var that = this;
			$.util.ajax({
			    url: that.elems.domain + "/ApplicationInterface/Gateway.ashx",
				  data:{
				      action: 'WEvents.ContactEvent.ChangeContactEventStatus',
				      ContactEventId: id,
				      Status: status
				  },
				  success: function (data) {
					  if(data.IsSuccess && data.ResultCode == 0) {
						  	if(status==2){
						  	    $dom.attr('class', 'handle iconPause').data('oprtype', 'pause');
							}else{
								$dom.attr('class','handle running').data('oprtype','running');
						  	}
						  	that.loadPageData();
					  } else {
						alert(data.Message);
					  }
				  }
			});
		},
		update: function (data) {
		    $('.jui-mask').show();
		    $('.jui-dialog-addShare').show();
		    var that = this;
		    /**************** -------------------弹出easyui 控件 start****************/
		    var wd = 190, H = 32;
		    //触点类型
		    $('#Activity_ContactTypeCode').combobox({
		        width: wd,
		        height: H,
		        panelHeight: that.elems.panlH,
		        valueField: 'key',
		        textField: 'value',
		        data: [
                //    {
		        //    "key": "Focus",
		        //    "value": "关注"
		        //}, {
		        //    "key": "Reg",
		        //    "value": "注册"
		        //}, {
		        //    "key": "Comment",
		        //    "value": "评论"
		        //}, {
		        //    "key": "SignIn",
		        //    "value": "签到"
		        //},
                {
		            "key": "Share",
		            "value": "分享"
		        }, {
		            "key": "",
		            "value": "请选择"
		        }],
		        onSelect: function (param) {
		            that.showShareEventId(param.key);
		            

		        }
		    });

		    //奖励
		    $('#Activity_PrizeType').combobox({
		        width: wd,
		        height: H,
		        panelHeight: that.elems.panlH,
		        valueField: 'key',
		        textField: 'value',
		        data: [{
		            "key": "Point",
		            "value": "积分"
		        }, {
		            "key": "Coupon",
		            "value": "优惠券"
		        }
                //, {
		        //    "key": "Chance",
		        //    "value": "抽奖机会"
                //}
                , {
		            "key": "",
		            "value": "请选择"
		        }],
		        onSelect: function (param) {
		            that.showRewardType(param.key);
		        }
		    });

		    //奖励次数
		    $('#Activity_RewardNumber').combobox({
		        width: wd,
		        height: H,
		        panelHeight: that.elems.panlH,
		        valueField: 'key',
		        textField: 'value',
		        data: [{
		            "key": "OnlyOne",
		            "value": "仅限一次"
		        }, {
		            "key": "OnceADay",
		            "value": "每天一次"
		        }, {
		            "key": "unlimited",
		            "value": "不限次数"
		        }, {
		            "key": "",
		            "value": "请选择"
		        }]
		    });

		    $('#win').window('open');
		},
        //根据奖励类型显示不同内容
		showRewardType:function(data)
		{
		    var that = this;
		    var $ActivityIntegral = $('#ActivityIntegral'),
                        $ActivityCouponType = $('#ActivityCouponType'),
                        $ActivityEvent = $('#ActivityEvent'),
                        $ActivityChanceCount = $('#ActivityChanceCount');
		    $ActivityIntegral.hide();
		    $ActivityCouponType.hide();
		    $ActivityEvent.hide();
		    $ActivityChanceCount.hide();
		    if (data == 'Coupon') {
		        $ActivityCouponType.show();

		        $("#Activity_Integral").numberbox({
		            required:false
		        });
		        $("#Activity_CouponTypeID").combobox({
		            required: true
		        });

		        $("#Activity_EventId").combobox({
		            required: false
		        });

		        $("#Activity_ChanceCount").numberbox({
		            required: false
		        });

		    } else if (data == 'Chance') {
		        $ActivityEvent.show();
		        $ActivityChanceCount.show();

		        $("#Activity_Integral").numberbox({
		            required: false
		        });
		        $("#Activity_CouponTypeID").combobox({
		            required: false
		        });

		        $("#Activity_EventId").combobox({
		            required: true
		        });

		        $("#Activity_ChanceCount").numberbox({
		            required: true
		        });

		    } if (data == 'Point') {
		        //获取优惠券列表
		        $ActivityIntegral.show();
		        $("#Activity_Integral").numberbox({
		            required: true
		            
		        });
		       
		        $("#Activity_CouponTypeID").combobox({
		            required: false
		        });

		        $("#Activity_EventId").combobox({
		            required: false
		        });

		        $("#Activity_ChanceCount").numberbox({
		            required: false
		        });

		    }

		},
        //根据分享显示分享活动
		showShareEventId: function (data) {
		    debugger;
		    var that = this;
		    var $ActivitySelect = $('#ActivitySelect');
		        $ActivitySelect.hide();
		        if (data == "Share") {
		            $ActivitySelect.show();
		            $("#Activity_Select").combobox({
		                required: true
		            });


		        } else {
		            $ActivitySelect.hide();
		            $("#Activity_Select").combobox({
		                required: false
		            });
		        }

		},
        //隐藏活动编辑选项
		hideEdit:function(data)
		{

		    $("#addActivity").hide();
		    $("#cancelActivity").hide();

		    $("#addShareForm input").attr("readonly", "readonly");
		    $("#addShareForm .textbox-addon").hide();
		    $(".combo-panel").hide();
		},
        //显示活动编辑选项
		showEdit: function (data)
		{
		    $("#ActivitySelect").hide();
		    $("#ActivityIntegral").hide();
		    $("#ActivityCouponType").hide();
		    $("#ActivityEvent").hide();

		    $("#addShareForm input").removeAttr("readonly");
		    $("#addShareForm .textbox-addon").show();
		    $(".combo-panel").show();

		    $("#addActivity").show();
		    $("#cancelActivity").show();
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
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'WEvents.ContactEvent.GetContactEventList',
                        PageIndex: this.args.PageIndex,
                        PageSize: this.args.PageSize

                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            }
			 
        },
		getActivityEventList: function () {
		    var that = this;
		    $.util.ajax({
		        url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsListHandler.ashx",
		        data: {
		            action: 'GetWorkingEventList'
		           
		        },
		        success: function (data) {

		            if (data.IsSuccess && data.ResultCode == 0) {
		                var result = data.Data,
							LEventsList = result.LEventsList;
		                $('#Activity_EventId').combobox({
		                    width: 190,
		                    height: that.elems.height,
		                    panelHeight: that.elems.panlH,
		                    lines: true,
		                    valueField: 'EventID',
		                    textField: 'Title',
		                    data: LEventsList
		                });
		            } else {
		                debugger;
		                alert(data.Message);
		            }
		        }
		    });
		},
		getCouponList: function () {
		    var that = this;
		    $.util.ajax({
		        url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
		        data: {
		            action: 'GetCouponTypeInfo'
		        },
		        success: function (data) {
		            debugger;
		            if (data.IsSuccess && data.ResultCode == 0) {
		                var result = data.Data,
							CouponTypeList = result.CouponTypeList;
		                $('#Activity_CouponTypeID').combobox({
		                    width: 190,
		                    height: that.elems.height,
		                    panelHeight: that.elems.panlH,
		                    lines: true,
		                    valueField: 'CouponTypeID',
		                    textField: 'CouponTypeName',
		                    data: CouponTypeList
		                });
		            } else {
		                debugger;
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
						$('#Activity_Select').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'EventID',
							textField: 'Title',
							data:lEventsList
						});

					} else {
					    debugger;
						alert(data.Message);
					}
				}
			});
		},
		
		setSaveShare: function (params) {
		    
			var that = this;
			$.util.ajax({
			    url: that.elems.domain + "/ApplicationInterface/Gateway.ashx",
				data: params,
				success: function(data) {
				    if (data.IsSuccess && data.ResultCode == 0) {

				        if (data.Data.Success) {
				            that.loadMoreData(1);
				            $('.jui-mask').hide();
				            $('.jui-dialog').hide();
				            if (data.Data.ContactEventId == -1) {
				                alert("触点已存在！");
				            } else {
				                alert('操作成功！');
				            }
				        } else {

				            $.messager.alert('提示', data.Data.ErrMsg);
				        }


				       
				    } else {
						alert(data.Message);
					}
				}
			});
		},

    };
    page.init();
});

