define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'artDialog', 'highcharts', 'copy'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabel2: $("#gridTable2"),
            tabel3: $("#gridTable3"),
            tabelWrap: $('#tableWrap'),
            tabelWrap2: $('#tableWrap2'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
			dataMessage:$(".dataMessage"),
			dataMessage2: $(".dataMessage2"),
			dataMessage3: $(".dataMessage3"),
			vipSourceId: '',
            isloadzclip:false,
            click:true,
            panlH:116                           // 下来框统一高度
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {

                
                $(".ActivityGroupName").addClass("graycolor");
                var selecedcode= $("#ActivityGroupCode").combobox("getValue");
                if (selecedcode != "" && selecedcode != " ") {
                    $("#" + selecedcode).removeClass("graycolor");
                } else {
                    $(".ActivityGroupName").removeClass("graycolor");
                }

                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.args.PageIndex = 1;

                $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
                that.GetMyActivityList(function (data) {
                    that.loadActivityData(data);
                });
                $.util.stopBubble(e);
            });

            //打开清单列表
            $("#windesc").on("click", ".viewlist", function () {
                $(".ActivityDate").html("");
                var startdate = $(this).data("startdate");
                var enddate = $(this).data("enddate");
                if (that.loadData.details.InteractionType == 1) {
                    $('#win').window({
                        title: "奖品发放清单", width: 800, height: 500, top: 20,
                        left: ($(window).width() - 800) * 0.5
                    });
                    $(".exportlist").attr("href", "/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?param={'LeventId':'" + that.loadData.details.CTWEventId + "'}&method=GameAwardsListExport");

                    that.GetEventPrizeDetailList(function (data) {
                        that.renderTable3(data);
                    });
                } else if (that.loadData.details.InteractionType == 2) {
                    $('#win').window({
                        title: "销售清单", width: 800, height: 500, top: 20,
                        left: ($(window).width() - 800) * 0.5
                    });
                    $(".exportlist").attr("href", "/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx?param={'LeventId'='" + that.loadData.details.CTWEventId + "'}&method=SalesItemsListExport");

                    that.GeEventItemDetailList(function (data) {
                        that.renderTable3(data);
                    });
                }
                $(".ActivityDate").html(startdate.substring(0, startdate.indexOf(" ")) + "-" + enddate.substring(0, enddate.indexOf(" ")));
                $('#win').window('open');

            });

            //奖品清单确认按钮事件
            $("#win").on("click", ".submit", function () {
                $('#win').window('close');
            });


            $(".addActivityBtn").on("click",  function () {
                $.util.toNewUrlPath("/Module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx");
            });
            //查看活动数据详情
            $(".listdata").on("click", ".ActivityDesc .edit", function () {
                $.util.toNewUrlPath("/Module/CreativeWarehouse/creative.aspx?CTWEventId=" + $(this).data("eventid") + "&TemplateId=" + $(this).data("id"));
            });


            //查看活动数据详情
            $(".listdata").on("click", ".viewdata", function () {
                debugger;
                that.loadData.details.EventId = $(this).data("id");
                that.loadData.details.InteractionType = $(this).data("interactiontype");
                that.loadData.details.CTWEventId = $(this).data("eventid");
                var startdate = $(this).data("startdate");
                var enddate = $(this).data("enddate");
                var html = "";
                var data = [];

                data.InteractionType = that.loadData.details.InteractionType;
                data.CTWEventId = that.loadData.details.CTWEventId;
                data.EventId = that.loadData.details.EventId;
                data.startdate = startdate;
                data.enddate = enddate;

                $('#windesc').window({
                    title: "活动详情", width: 1220, height: 600, top: 20,
                    left: ($(window).width() - 1220) * 0.5
                });

                $('#panlconent').layout();

                $('#windesc').window('open');

                if (that.loadData.details.InteractionType == 1) {
                    that.loadData.details.EventType = "Game";

                    that.GetLEventStats(function (_data) {

                        data.LeventsStats = _data.LeventsStats;

                        //改变弹框内容，调用百度模板显示不同内容
                        $('#panlconent').layout('remove', 'center');
                        var html = bd.template('tpl_gameEventDataDetail', data);
                        var options = {
                            region: 'center',
                            content: html
                        };
                        $('#panlconent').layout('add', options);



                        that.GetEventPrizeList(function (_data) {
                            that.renderTable2(data);

                        });

                    });

                } else {
                    that.loadData.details.EventType = "Sales";

                   
                    that.GetPanicbuyingEventStats(function (_data) {
                        data.LeventsStats = _data.PanicbuyingEventStatsInfo;


                        //改变弹框内容，调用百度模板显示不同内容
                        $('#panlconent').layout('remove', 'center');
                        var html = bd.template('tpl_EventDataDetail', data);
                        var options = {
                            region: 'center',
                            content: html
                        };
                        $('#panlconent').layout('add', options);


                        that.GeEventItemList(function (_data) {
                            that.renderTable2(data);

                        });

                    });



                   

                  
                    //活动销量，活动订单
                    that.GetPanicbuyingEventRankingStats(function (_data) {
                        var data_categories = [];
                        var data_series = [];
                        var data = _data.OrderMoneyRankList;
                        for (var i = 0; i < data.length; i++) {
                            data_categories.push(data[i].DateStr);
                            data_series.push(data[i].OrderActualAmount);
                        }
                        that.inithighcharts("ActivitySales", data_categories, data_series);
                        data_categories = [];
                        data_series = [];
                        var data = _data.OrderCountRankList;
                        for (var i = 0; i < data.length; i++) {

                            data_categories.push(data[i].DateStr);
                            data_series.push(data[i].OrderCount);
                        }
                        that.inithighcharts("ActivityOrder", data_categories, data_series);
                    });
                }

               
                //粉丝，会员增长
                that.GetVipAddRankingStats(function (_data) {
                    var data_categories = [];
                    var data_series = [];
                    var data = _data.GameVipAddRankingList;
                    for (var i = 0; i < data.length; i++) {
                        data_categories.push(data[i].DateStr);
                        data_series.push(data[i].FocusVipCount);
                    }
                    that.inithighcharts("fansContainer", data_categories, data_series);
                    data_categories = [];
                    data_series = [];
                    var data = _data.PromotionVipAddRankingList;
                    for (var i = 0; i < data.length; i++) {

                        data_categories.push(data[i].DateStr);
                        data_series.push(data[i].RegVipCount);
                    }
                    that.inithighcharts("registerContainer", data_categories, data_series);
                });

               

            });

            //结束事件
            $("#windesc").on("click", ".endbtn", function () {

                $.messager.confirm('是否结束', '是否确认结束活动?', function (r) {
                    if (r) {
                        that.EndOfEvent(function (data) {


                        });
                    }
                });
            });
            

            //返回事件
            $("#windesc").on("click", ".returnbtn", function () {


                $('#windesc').window('close');
            });

            //延期事件
            $("#windesc").on("click", ".defer", function () {

                $('#windefer').window({
                    title: "活动延期", width: 400, height: 250, top: ($(window).height() - 250) * 0.5,
                    left: ($(window).width() - 400) * 0.5
                });
                $("._startdate").html($(this).data("startdate"));
                $("#_endTime").datebox("setValue", $(this).data("enddate").replace("/", "-").replace("/", "-"));

                $('#windefer').window('open');


            });

            $(".deferbtn").on("click", function () {
                var startdate = $("._startdate").html();
                var enddate = $("#_endTime").datebox("getValue");
                if (startdate != "" && enddate != "") {
                    that.DelayEvent(startdate, enddate, function (data) {
                        $('#windefer').window('close');
                    });
                }
            });

            //追加事件
            $("#windesc").on("click", ".addPrizes", function () {

                $('#winadd').window({
                    title: "追加数量", width: 400, height: 200, top: ($(window).height() - 200) * 0.5,
                    left: ($(window).width() - 400) * 0.5
                });
                $('#winadd').window('open');



            });

            $(".addbtn").on("click", function () {
                var id = $(this).data("id");
                that.AppendPrize(id, 0, function (data) {
                    $('#winadd').window('close');
                    $.messager.alert("提示", "保存成功！");
                });
            });


            //查看活动数据详情
            $(".ActivityGroups").on("click", ".ActivityGroupName", function () {
                
                $(".ActivityGroupName").addClass("graycolor");
                $(this).removeClass("graycolor");

                $("#Status").combobox("setValue", "");
                $("#ActivityGroupCode").combobox("setValue", "");
                $("#EventName").val("");

                that.loadData.seach.form.Status = "";
                that.loadData.seach.form.EventName = "";
                that.loadData.seach.form.ActivityGroupCode= $(this).data("code");
                that.GetMyActivityList(function (data) {
                    that.loadActivityData(data);
                });
            });

           
           


            //活动发布预览
            $(".listdata").on("click", ".releasebtn", function () {
                $('#winrelease').window({
                    title: "活动", width: 590, height: 590, top: ($(window).height() - 590) * 0.5,
                    left: ($(window).width() - 590) * 0.5
                });

                var OnfflineQRCodeId = $(this).data("onfflineqrcode");
                var OnlineQRCodeId = $(this).data("onlineqrcode");
                var status = $(this).data("status");

                that.loadData.details.CTWEventId = $(this).data("eventid");

                if (status != "10") {
                    $(".release").hide();
                } else {
                    $(".release").show();
                }
                $('#winrelease').window('open');

                $("#winrelease .OnlineQRCodeId .codeimg img").attr("src", OnlineQRCodeId);
                $("#winrelease .OnlineQRCodeId .downaddress").attr("href", OnlineQRCodeId);
                $("#winrelease .OnfflineQRCodeId .codeimg img").attr("src", OnfflineQRCodeId);
                $("#winrelease .OnfflineQRCodeId .downaddress").attr("href", OnfflineQRCodeId);
                $("#winrelease .addressinput").val("");


                if (!that.elems.isloadzclip) {
                    that.elems.isloadzclip = true;
                    //复制地址
                    $(".addrcopy").zclip({
                        path: "/Module/static/js/plugin/ZeroClipboard.swf",
                        copy: function () {
                            return $(this).parents(".address").find(".addressinput").val();
                        },
                        afterCopy: function () {/* 复制成功后的操作 */
                            $.messager.alert("提示", "复制成功！");
                        }
                    });
                }

               
            });

            //显示二维码
            $(".listdata").on("mouseover", ".ActivityContent", function () {
                $(this).find(".ActivityQRcode").show();
            });

            //隐藏二维码
            $(".listdata").on("mouseout", ".ActivityContent", function () {
                $(this).find(".ActivityQRcode").hide();
            });

            //公众号粉丝增长数排行导出Excel
            $("#windesc").on("click", ".fansexportbtn", function () {
                //that.GameVipAddExport();
            });
            

            //活动发布
            $("#winrelease").on("click", ".release", function () {
                that.ChangeCTWEventStatus(function () {
                    $('#winReleaseSuccess').window({
                        title: "直接发布", width: 472, height: 274, top: ($(window).height() - 274) * 0.5,
                        left: ($(window).width() - 472) * 0.5
                    });
                    $('#winReleaseSuccess').window('open');
                    that.loadPageData();
                });
            });
            
            $(".viewall").on("click", function () {

                $(".ActivityGroupName").removeClass("graycolor");
                that.loadData.seach.form.ActivityGroupCode = "";
                that.loadData.seach.form.Status = "";
                that.loadData.seach.form.EventName = "";
                that.GetMyActivityList(function (data) {
                    that.loadActivityData(data);
                });
            });
            
        },
        //初始化柱状图
        inithighcharts: function (nameid, data_categories, data_series) {

            $('#' + nameid).highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: data_categories,
                    crosshair: true
                },
                yAxis: {
                    labels: {
                        enabled: false
                    },
                    title: {
                        text: null
                    }
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr>' +
                        '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: '',
                    data: data_series

                }]
            });
        },
        loadActivityData: function (data) {
            var html = "";
            if (data.MyActivityList) {
                html= bd.template("tpl_Activity", data);
                $(".listdata").html(html);
            }

            if (data.EventStatusCoountList)
            {
                for (var i = 0; i < data.EventStatusCoountList.length; i++)
                {
                    var code = data.EventStatusCoountList[i].ActivityGroupCode.replace(/^\s+|\s+$/g, "");
                    $("#" + code + " .runningNum").html(data.EventStatusCoountList[i].Running);
                    $("#" + code + " .unreleasedNum").html(data.EventStatusCoountList[i].Prepare);
                    $("#" + code + " .invalidNum").html(data.EventStatusCoountList[i].End);
                }
            }
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                that.loadData.seach.form[filed.name]=filed.value;
            });
        },
        //渲染tabel
        renderTable2: function (data) {
            debugger;
            var that = this;
            if (that.loadData.details.InteractionType == 1) {
                if (!data.EventPrizeList) {
                    data.EventPrizeList = [];
                }

                //jQuery easy datagrid  表格处理
                $("#gridTable2").datagrid({

                    method: 'post',
                    singleSelect: true, //单选
                    // height : 332, //高度
                    fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped: true, //奇偶行颜色不同
                    //数据来源
                    data: data.EventPrizeList,
                    columns: [[
                        {
                            field: 'PrizeName', title: '奖品名称', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'winnerCount', title: '已发放', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'RemindCount', title: '剩余', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'NotUsedCount', title: '已发放未使用', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'UsedCount', title: '已使用', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'prizeSale', title: '代动销量', width: 110, align: 'left', resizable: false
                        },

                        {
                            field: 'PrizesID', title: '操作', width: 50, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                return "<a class='addPrizes'  data-id='" + value + "' href='javascript:void(0);' >追加奖品</a>";

                            }
                        }




                    ]],
                    onLoadSuccess: function (data) {
                        that.elems.tabel2.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if (data.rows.length > 0) {
                            that.elems.dataMessage2.hide();
                            $('#kkpager2').show();
                        } else {
                            that.elems.dataMessage2.show();
                            $('#kkpager2').hide();
                        }
                        //that.elems.tabel.datagrid('getSelected');
                    }
                });

            } else if (that.loadData.details.InteractionType == 2) {
                if (!data.EventItemList) {
                    data.EventItemList = [];
                }

                //jQuery easy datagrid  表格处理
                that.elems.tabel2.datagrid({

                    method: 'post',
                    singleSelect: true, //单选
                    // height : 332, //高度
                    fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped: true, //奇偶行颜色不同
                    //数据来源
                    data: data.EventItemList,
                    columns: [[
                        {
                            field: 'item_name', title: '商品名称', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'SkuName', title: '规格', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'price', title: '原价', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'SalesPrice', title: '团购价', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'KeepQty', title: '已销售', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'SoldQty', title: '总销量', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'InverTory', title: '团购库存剩余', width: 110, align: 'left', resizable: false
                        },

                        {
                            field: 'item_id', title: '操作', width: 50, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                return "<a class='viewdesc' data-interactiontype='" + row.InteractionType + "' data-id='" + value + "' href='javascript:void(0);' >查看详情</a>";

                            }
                        }




                    ]],
                    onLoadSuccess: function (data) {
                        that.elems.tabel2.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if (data.rows.length > 0) {
                            that.elems.dataMessage2.hide();
                            $('#kkpager2').show();
                        } else {
                            that.elems.dataMessage2.show();
                            $('#kkpager2').hide();
                        }
                        //that.elems.tabel.datagrid('getSelected');
                    }
                });
            }




            //分页
            //data.Data={};
            //data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            //var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
                pagerid: 'kkpager2',
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
        //渲染tabel
        renderTable3: function (data) {
            debugger;
            var that = this;
            if (that.loadData.details.InteractionType == 1) {
                if (!data.EventPrizeDetailList) {
                    data.EventPrizeDetailList = [];
                }

                //jQuery easy datagrid  表格处理
                $("#gridTable3").datagrid({

                    method: 'post',
                    singleSelect: true, //单选
                    // height : 332, //高度
                    fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped: true, //奇偶行颜色不同
                    //数据来源
                    data: data.EventPrizeDetailList,
                    columns: [[
                        {
                            field: 'PrizeName', title: '奖品名称', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'vipname', title: '中奖人', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'winTime', title: '中奖时间', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'PrizeUsed', title: '是否使用', width: 110, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                var result = "";
                                switch (value) {
                                    case 0: result = "否"; break;
                                    case 1: result = "是"; break;
                                    default: result = ""; break;
                                }
                                return result;


                            }
                        }, {
                            field: 'subscribe', title: '是否关注', width: 110, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                var result = "";
                                switch (value) {
                                    case 0: result = "否"; break;
                                    case 1: result = "是"; break;
                                    default: result = ""; break;
                                }
                                return result;
                            }
                        }




                    ]],
                    onLoadSuccess: function (data) {
                        that.elems.tabel3.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if (data.rows.length > 0) {
                            that.elems.dataMessage3.hide();
                            $('#kkpager3').show();
                        } else {
                            that.elems.dataMessage3.show();
                            $('#kkpager3').hide();
                        }
                        //that.elems.tabel.datagrid('getSelected');
                    }
                });

            } else if (that.loadData.details.InteractionType == 2) {
                if (!data.EventItemDetailList) {
                    data.EventItemDetailList = [];
                }

                //jQuery easy datagrid  表格处理
                that.elems.tabel3.datagrid({

                    method: 'post',
                    singleSelect: true, //单选
                    // height : 332, //高度
                    fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped: true, //奇偶行颜色不同
                    //数据来源
                    data: data.EventItemDetailList,
                    columns: [[
                        {
                            field: 'order_no', title: '订单号', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'item_name', title: '商品名称', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'SkuName', title: '规格', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'price', title: '原价', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'SalesPrice', title: '团购价', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'vipname', title: '订购人', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'DeliveryName', title: '配送方式', width: 110, align: 'left', resizable: false
                        }, {
                            field: 'create_time', title: '成交日期', width: 110, align: 'left', resizable: false
                        }




                    ]],
                    onLoadSuccess: function (data) {
                        that.elems.tabel3.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if (data.rows.length > 0) {
                            that.elems.dataMessage3.hide();
                            $('#kkpager3').show();
                        } else {
                            that.elems.dataMessage3.show();
                            $('#kkpager3').hide();
                        }
                        //that.elems.tabel.datagrid('getSelected');
                    }
                });
            }




            //分页
            //data.Data={};
            //data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            //var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
                pagerid: 'kkpager3',
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
        //游戏类活动奖品统计列表
        GetEventPrizeList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetEventPrizeList',
                    LeventId: that.loadData.details.EventId,
                    PageIndex: that.loadData.args.PageIndex,
                    PageSize: that.loadData.args.PageSize
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //带游戏的创意仓库活动统计
        GetLEventStats: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetLEventStats',
                    CTWEventId: that.loadData.details.CTWEventId,
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },

        //团购类活动商品统计列表
        GeEventItemList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GeEventItemList',
                    CTWEventId: that.loadData.details.CTWEventId,
                    PageIndex: that.loadData.args.PageIndex,
                    PageSize: that.loadData.args.PageSize
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //获取游戏与促销会员增长排行
        GetVipAddRankingStats: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetVipAddRankingStats',
                    CTWEventId: that.loadData.details.CTWEventId
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //获取带促销活动的创意仓库的统计
        GetPanicbuyingEventStats: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetPanicbuyingEventStats',
                    CTWEventId: that.loadData.details.CTWEventId
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //带促销的创意仓库订单排行统计    
        GetPanicbuyingEventRankingStats: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetPanicbuyingEventRankingStats',
                    CTWEventId: that.loadData.details.CTWEventId
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },

        //游戏类活动奖品详情
        GetEventPrizeDetailList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetEventPrizeDetailList',
                    CTWEventId: that.loadData.details.CTWEventId,
                    PageIndex: that.loadData.args.PageIndex,
                    PageSize: that.loadData.args.PageSize
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },

        //团购类商品信息详情
        GeEventItemDetailList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GeEventItemDetailList',
                    CTWEventId: that.loadData.details.CTWEventId,
                    PageIndex: that.loadData.args.PageIndex,
                    PageSize: that.loadData.args.PageSize
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //结束活动
        EndOfEvent: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingActivity.EndOfEvent',
                    EventType: that.loadData.details.EventType,
                    CTWEventId: that.loadData.details.CTWEventId
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //结束活动
        DelayEvent: function (BegDate, EndDate, callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingActivity.DelayEvent',
                    EventType: that.loadData.details.EventType,
                    CTWEventId: that.loadData.details.CTWEventId,
                    BegDate: BegDate,
                    EndDate: EndDate
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //发布活动
        ChangeCTWEventStatus: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingActivity.ChangeCTWEventStatus',
                    CTWEventId: that.loadData.details.CTWEventId,
                    Status: 20
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //追加
        AppendPrize: function (PrizesID, CountTotal, callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
                data: {
                    action: 'AppendPrize',
                    EventId: that.loadData.details.EventId,
                    PrizesID: PrizesID,
                    CountTotal: CountTotal
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //加载页面的数据请求
        loadPageData: function (e) {


            $('#Status').combobox({
                width: 200,
                height: 30,
                lines: true,
                valueField: 'ID',
                textField: 'Name',
                data: [{
                    ID: "10",
                    Name: "待发布"
                }, {
                    ID: "20",
                    Name: "运行中"
                }, {
                    ID: "30",
                    Name: "暂停"
                }, {
                    ID: "40",
                    Name: "结束"
                }, {
                    ID: " ",
                    Name: "全部"
                }]
            });

            $('#ActivityGroupCode').combobox({
                width: 200,
                height: 30,
                lines: true,
                valueField: 'ID',
                textField: 'Name',
                data: [{
                    ID: "Holiday",
                    Name: "节假日主题营销"
                }, {
                    ID: "Unit",
                    Name: "炒作门店活动"
                }, {
                    ID: "Product",
                    Name: "打造爆款产品"
                }, {
                    ID: " ",
                    Name: "全部"
                }]
            });

            debugger;
            var that = this;
            // $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $(".ActivityGroupName").get(0).click();
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
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                //数据来源
                data:data.LEventsList,
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
                    
                    {field : 'EventID',title : '操作',width:50,align:'left',resizable:false,
                    formatter: function (value, row, index) {
                        var ophtml = "";
							var status = row.Status;
							if(status=='未开始'){
							    ophtml= '<p class="handle delete" title="删除" data-index="'+index+'" data-oprtype="delete"></p>';
							}else if(status=='运行中'){
							    ophtml = '<p class="handle running" title="暂停" data-index="' + index + '" data-oprtype="running"></p>';
							}else if(status=='暂停'){
							    ophtml = '<p class="handle pause" title="启动" data-index="' + index + '" data-oprtype="pause"></p>';
							}else if(status=='结束'){
							    ophtml = '';
							}

							if (row.DrawMethodName == "花样问卷")
							{
							    ophtml += '<p class="handle detail"  title="问卷详情" data-index="' + index + '" data-oprtype="detail"></p>';
							}

							ophtml += '<p class="handle down" title="下载二维码图片" data-index="' + index + '" data-oprtype="down"></p>';
							return ophtml;
                            
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

            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.GetMyActivityList(function (data) {
                that.loadActivityData(data);
            });
        },
        //我的活动
        GetMyActivityList: function (callback) {
			var that = this;
			$.util.ajax({
			    url: "/ApplicationInterface/Gateway.ashx",
				  data:{
				      action: 'CreativityWarehouse.MarketingActivity.GetMyActivityList.GetMyActivityList',
				      EventName: that.loadData.seach.form.EventName,
				      ActivityGroupCode: that.loadData.seach.form.ActivityGroupCode,
				      Status: that.loadData.seach.form.Status
				  },
				  beforeSend: function () {
				      $.util.isLoading()

				  },
				  success: function (data) {
				      if (data.IsSuccess && data.ResultCode == 0) {
				          var result = data.Data;
				          if (callback) {
				              callback(result);
				          }
				      } else {
				          debugger;
				          alert(data.Message);

				      }
				  }
			});
        },
        //公众号粉丝增长数排行导出Excel
        GameVipAddExport: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Module/CreativityWarehouse/MarketingData/ExportExcelHandler.ashx",
                data: {
                    ctweventId:that.loadData.details.CTWEventId
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
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
            details: {
                EventId: "",
                InteractionType: "",
                EventType: "",
                CTWEventId: ""
            },
            seach:{
                form:{
                    EventName: "",
                    ActivityGroupCode: "",
                    Status: ''
                }
            }
			 
        }

    };
    page.init();
});


