define(['jquery', 'template', 'tools', 'easyui'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#operation'),              //弹出框操作部分
            dataMessage: $(".dataMessage"),
            click: true,
            panlH: 116,                        // 下来框统一高度
            domain: "",
            isshow: false                      //是否显示选择类型弹出框
        },
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                             //标签类型
            tagList: []                              //标签列表
        },
        init: function () {

            this.initEvent();

            this.loadPageData();




        },
        initEvent: function () {
            var that = this;
        },




       
        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.GetHomePageStats(function (result) {
                var data;
                if (result)
                {
                    data = result.Data;
                }
                if (data)
                {
                    $(".UnitCount").html(data.UnitCount+"家");//门店数
                    $(".UnitCurrentDayOrderAmount").html(data.UnitCurrentDayOrderAmount);//门店当日总业绩
                    var UnitCurrentDayOrderAmountDToD = data.UnitCurrentDayOrderAmountDToD;
                    if (UnitCurrentDayOrderAmountDToD < 0) {
                        $(".UnitCurrentDayOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UnitCurrentDayOrderAmountDToD = -UnitCurrentDayOrderAmountDToD;
                    } else {
                        $(".UnitCurrentDayOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UnitCurrentDayOrderAmountDToD").html(UnitCurrentDayOrderAmountDToD+"%");//门店当日总业绩环比
                    $(".UnitMangerCount").html(data.UnitMangerCount + "人");//门店店长数
                    $(".UnitCurrentDayAvgOrderAmount").html(data.UnitCurrentDayAvgOrderAmount);//当日店均业绩
                    var UnitCurrentDayAvgOrderAmountDToD = data.UnitCurrentDayAvgOrderAmountDToD;
                    if (UnitCurrentDayAvgOrderAmountDToD < 0) {
                        $(".UnitCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UnitCurrentDayAvgOrderAmountDToD = -UnitCurrentDayAvgOrderAmountDToD;
                    } else {
                        $(".UnitCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UnitCurrentDayAvgOrderAmountDToD").html(UnitCurrentDayAvgOrderAmountDToD + "%");//当日店均业绩环比
                    $(".UnitUserCount").html(data.UnitUserCount + "人");//门店员工数
                    $(".UserCurrentDayAvgOrderAmount").html(data.UserCurrentDayAvgOrderAmount);//当日人均业绩
                    var UserCurrentDayAvgOrderAmountDToD = data.UserCurrentDayAvgOrderAmountDToD;
                    if (UserCurrentDayAvgOrderAmountDToD < 0) {
                        $(".UserCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UserCurrentDayAvgOrderAmountDToD = -UserCurrentDayAvgOrderAmountDToD;
                    } else {
                        $(".UserCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UserCurrentDayAvgOrderAmountDToD").html(UserCurrentDayAvgOrderAmountDToD + "%");//当日人均业绩环比
                    $(".RetailTraderCount").html(data.RetailTraderCount + "家");//分销商数
                    $(".CurrentDayRetailTraderOrderAmount").html(data.CurrentDayRetailTraderOrderAmount);//当日分销业绩
                    var CurrentDayRetailTraderOrderAmountDToD = data.CurrentDayRetailTraderOrderAmountDToD;
                    if (CurrentDayRetailTraderOrderAmountDToD < 0) {
                        $(".CurrentDayRetailTraderOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        CurrentDayRetailTraderOrderAmountDToD = -CurrentDayRetailTraderOrderAmountDToD;
                    } else {
                        $(".CurrentDayRetailTraderOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".CurrentDayRetailTraderOrderAmountDToD").html(CurrentDayRetailTraderOrderAmountDToD + "%");//当日分销业绩环比
                    $(".VipCount").html(data.VipCount + "人");//会员数   
                    $(".NewVipCount").html(data.NewVipCount + "人");//当日新增会员数
                    $(".EventsCount").html(data.EventsCount + "个");//活动数
                    $(".EventJoinCount").html(data.EventJoinCount + "人");//活动参与人次

                    //业绩排名前5名
                    if (data.PerformanceTop) {
                        $(".PerformanceTop").html("");
                        for (i = 0; i < data.PerformanceTop.length; i++) {
                            $(".PerformanceTop").append("<li>" + (i + 1) + " " + data.PerformanceTop[i].UnitName + "<span class='red'>￥" + data.PerformanceTop[i].OrderPeopleTranAmount + "</span></li>");
                        }
                    }

                    //业绩排名后5名
                    if (data.PerformanceLower)
                    {
                        $(".PerformanceLower").html("");
                        for (i = 0; i < data.PerformanceLower.length; i++)
                        {
                            $(".PerformanceLower").append("<li>" + (i + 1) + " " + data.PerformanceLower[i].UnitName + "<span class='green'>￥" + data.PerformanceLower[i].OrderPeopleTranAmount + "</span></li>");
                        }
                    }

                    debugger;
                    var degbase = 3.6;//角度转换百分比基数
                    //会员贡献率
                    var VipContributePect = data.VipContributePect;
                    if (VipContributePect < 50) {
                        $(".VipContributePectring").css("-webkit-transform", "rotate(" + (VipContributePect * degbase) + "deg)");
                        $(".VipContributePectmovering").hide();
                    } else {
                        $(".VipContributePectmovering").show();
                        $(".VipContributePectring").css("-webkit-transform", "rotate(180deg)");
                        $(".VipContributePectmovering").css("-webkit-transform", "rotate(" + ((VipContributePect - 50) * degbase) + "deg)");
                    }
                    $(".VipContributePect").html(VipContributePect + "%");

                    //月度月均达成
                    var MonthArchivePect = data.MonthArchivePect;
                    if (MonthArchivePect < 50) {
                        $(".MonthArchivePectring").css("-webkit-transform", "rotate(" + (MonthArchivePect * degbase) + "deg)");
                        $(".MonthArchivePectmovering").hide();
                    } else {
                        $(".MonthArchivePectmovering").show();
                        $(".MonthArchivePectring").css("-webkit-transform", "rotate(180deg)");
                        $(".MonthArchivePectmovering").css("-webkit-transform", "rotate(" + ((MonthArchivePect - 50) * degbase) + "deg)");
                    }
                    $(".MonthArchivePect").html(MonthArchivePect + "%");


                    $(".CurrentMonthSingleUnitAvgTranCount").html(data.CurrentMonthSingleUnitAvgTranCount);//单店月均客流
                    $(".CurrentMonthUnitAvgCustPrice").html(data.CurrentMonthUnitAvgCustPrice);//门店月均单价
                    $(".CurrentMonthSingleUnitAvgTranAmount").html(data.CurrentMonthSingleUnitAvgTranAmount);//单店月均业绩
                    $(".CurrentMonthTranAmount").html(data.CurrentMonthTranAmount);//门店月均总业绩

                    $(".PreAuditOrder").html(data.PreAuditOrder?data.PreAuditOrder:0);//待审核订单
                    $(".PreSendOrder").html(data.PreSendOrder ? data.PreSendOrder : 0);//待发货订单
                    $(".PreTakeOrder").html(data.PreTakeOrder ? data.PreTakeOrder : 0);//门店待提货订单
                    $(".PreRefund").html(data.PreRefund ? data.PreRefund : 0);//待退货
                    $(".PreReturnCash").html(data.PreReturnCash ? data.PreReturnCash : 0);//待退款



                }

            });

            that.loadData.GetMenuList(function (result) {
                var data;
                if (result) {
                    data = result.Data;
                    if (data)
                    {
                        for (i = 0; i<data.MenuList.length; i++) {
                            var menu = data.MenuList[i];
                            $("[data-menucode]").each(function () {
                                var menucode = $(this).data("menucode");
                                if (menu.Menu_Code == menucode)
                                {
                                    debugger;
                                    if (menu.SubMenuList.length > 0&&menu.SubMenuList[0].SubMenuList&&menu.SubMenuList[0].SubMenuList.length>0) {
                                        $(this).find(".menusrc").attr("href", menu.SubMenuList[0].SubMenuList[0].Url_Path + "?CustomerId=" + $.util.getUrlParam("CustomerId") + "&mid=" + menu.SubMenuList[0].SubMenuList[0].Menu_Id+ "&PMenuID=" +menu.SubMenuList[0].Menu_Id+ "&MMenuID=" + menu.Menu_Id);
                                           console.log(menu.Menu_Code+"url____"+menu.SubMenuList[0].SubMenuList[0].Url_Path);
                                    } else {
                                        $(this).find(".menusrc").attr("href", "JavaScript:void(0)");
                                    }
                                }
                            });

                        }

                    }
                }

            });

            $.util.stopBubble(e)
        },

      
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            
        },

        loadData: {
            args: {
                PageIndex: 1,
                PageSize: 10,
                SearchColumns: {},    //查询的动态表单配置
                OrderBy: "",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status: -1
            },
            tag: {
                VipId: "",
                orderID: ''
            },
            seach: {
                QuestionnaireName: "",
                QuestionnaireType: 0
            },
            //获取首页模块链接地址
            GetMenuList: function (callback) {
                debugger;
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Basic.Menu.GetMenuList'
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取首页统计数据
            GetHomePageStats: function (callback) {
                debugger;
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Basic.HomePageStats.GetHomePageStats'
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
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
        }

    };
    page.init();
});

