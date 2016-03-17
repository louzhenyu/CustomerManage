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

            var that = this,
				UA = navigator.userAgent.toLowerCase();
            if (!(UA.indexOf("chrome") > 0 || UA.indexOf("safari") > 0)) {
                location.href = 'browserDown.html';
            } else {
                this.initEvent();
                this.loadPageData();
            }
			
        },
        initEvent: function () {
            var that = this,
				$uiMask = $('.ui-mask'),
				$closeBtn = $('.closeBtn'),
                $WeixinServicecloseBtn = $(".WeixinServicecloseBtn"),
				$qbQuick = $('.qb_quick'),
				$WeixinServiceqbQuick = $('.qb_WeixinService'),
				
				$notShow = $('.nextNotShow span');
				
				
			$notShow.on('click',function(){
				if($notShow.hasClass('on')){
					$notShow.removeClass('on');
				}else{
					$notShow.addClass('on');
				}
			});
			
			//绑定微信服务号授权start
			$('#win2').window({title:"绑定微信帐号",width:922,height:422,top:($(window).height() - 422) * 0.5,left:($(window).width() - 922) * 0.5});
			$(".wxAuthBox").bind("click", function () {
			    $('#win2').window('open');
			});
			$('#receive').attr("src",window.weixinUrl);
        },

		quicklyDialog: function(){
			var that=this,
				$notShow = $('.nextNotShow span'),
				cooksName = '';
			$('#win').window({title:"快速上手",width:922,height:422,top:($(window).height() - 422) * 0.5,left:($(window).width() - 922) * 0.5,
			onClose:function(){
				if($notShow.hasClass('on')){
					$.util.setCookie('chainclouds_management_system_index', 'zmind');
				}
				//var mid = JITMethod.getUrlParam("mid"),PMenuID = JITMethod.getUrlParam("PMenuID");
				//location.href = "/module/newVipManage/querylist.aspx?mid=" +mid+"&PMenuID="+PMenuID;
			}
			});
			cooksName = $.util.getCookie('chainclouds_management_system_index');
			if(!cooksName){
				$(document).ready(function() {
					setTimeout(function(){
						$('#win').window('open');
					},1000);
				});
			}else{
				$(document).ready(function() {
					$('#win').window('close');
				});
			}
			//改变弹框内容，调用百度模板显示不同内容
			/*$('#panlconent').layout('remove','center');
			var html=bd.template('tpl_addProm');
			var options = {
				region: 'center',
				content:html
			};
			$('#panlconent').layout('add',options);*/
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
                    debugger
                    $(".UnitCount").html((data.UnitCount?data.UnitCount:"0")+"家");//门店数
                   
                    that.NumberFormat(data.UnitCurrentDayOrderAmount, function (result, isyuan) {
                        debugger;
                        $(".UnitCurrentDayOrderAmount").html("<span style='font-size: 14px;'>￥</span>" + result);//门店当日总业绩
                        if (isyuan)
                            $(".UnitCurrentDayOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("");
                        else
                            $(".UnitCurrentDayOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("万");

                    });



                    var UnitCurrentDayOrderAmountDToD = data.UnitCurrentDayOrderAmountDToD;
                    if (UnitCurrentDayOrderAmountDToD < 0) {
                        $(".UnitCurrentDayOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UnitCurrentDayOrderAmountDToD = -UnitCurrentDayOrderAmountDToD;
                    } else {
                        if (UnitCurrentDayOrderAmountDToD == null || UnitCurrentDayOrderAmountDToD == 0)
                            $(".UnitCurrentDayOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/defaultdrop.png");
                        else
                            $(".UnitCurrentDayOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UnitCurrentDayOrderAmountDToD").html((UnitCurrentDayOrderAmountDToD ? UnitCurrentDayOrderAmountDToD.toFixed(1) : "0") + "%");//门店当日总业绩环比
                    $(".UnitMangerCount").html((data.UnitMangerCount ? data.UnitMangerCount : "0") + "人");//门店店长数
                    that.NumberFormat(data.UnitCurrentDayAvgOrderAmount, function (result, isyuan) {
                        $(".UnitCurrentDayAvgOrderAmount").html("<span style='font-size: 14px;'>￥</span>" + result);//当日店均业绩
                        if (isyuan)
                            $(".UnitCurrentDayAvgOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("");
                        else
                            $(".UnitCurrentDayAvgOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("万");

                    });


                    var UnitCurrentDayAvgOrderAmountDToD = data.UnitCurrentDayAvgOrderAmountDToD;
                    if (UnitCurrentDayAvgOrderAmountDToD < 0) {
                        $(".UnitCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UnitCurrentDayAvgOrderAmountDToD = -UnitCurrentDayAvgOrderAmountDToD;
                    } else {
                        if (UnitCurrentDayAvgOrderAmountDToD == null || UnitCurrentDayAvgOrderAmountDToD == 0)
                            $(".UnitCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/defaultdrop.png");
                        else
                            $(".UnitCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UnitCurrentDayAvgOrderAmountDToD").html((UnitCurrentDayAvgOrderAmountDToD ? UnitCurrentDayAvgOrderAmountDToD.toFixed(1) : "0") + "%");//当日店均业绩环比
                    $(".UnitUserCount").html((data.UnitUserCount ? data.UnitUserCount : "0") + "人");//门店员工数
                    that.NumberFormat(data.UserCurrentDayAvgOrderAmount, function (result, isyuan) {
                        $(".UserCurrentDayAvgOrderAmount").html("<span style='font-size: 14px;'>￥</span>" + result);//当日人均业绩
                        if (isyuan)
                            $(".UserCurrentDayAvgOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("");
                        else
                            $(".UserCurrentDayAvgOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("万");

                    });


                    var UserCurrentDayAvgOrderAmountDToD = data.UserCurrentDayAvgOrderAmountDToD;
                    if (UserCurrentDayAvgOrderAmountDToD < 0) {
                        $(".UserCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        UserCurrentDayAvgOrderAmountDToD = -UserCurrentDayAvgOrderAmountDToD;
                    } else {
                        if (UserCurrentDayAvgOrderAmountDToD == null || UserCurrentDayAvgOrderAmountDToD == 0)
                            $(".UserCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/defaultdrop.png");
                        else
                        $(".UserCurrentDayAvgOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".UserCurrentDayAvgOrderAmountDToD").html((UserCurrentDayAvgOrderAmountDToD ? UserCurrentDayAvgOrderAmountDToD.toFixed(1) : "0") + "%");//当日人均业绩环比
                    $(".RetailTraderCount").html((data.RetailTraderCount ? data.RetailTraderCount : "0") + "家");//分销商数
                    that.NumberFormat(data.CurrentDayRetailTraderOrderAmount, function (result, isyuan) {
                        $(".CurrentDayRetailTraderOrderAmount").html("<span style='font-size: 14px;'>￥</span>" + result);//当日分销业绩
                        if (isyuan)
                            $(".CurrentDayRetailTraderOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("");
                        else
                            $(".CurrentDayRetailTraderOrderAmount").parents(".achievementnum").find(".CurrentMonthunit").html("万");

                    });


                    var CurrentDayRetailTraderOrderAmountDToD = data.CurrentDayRetailTraderOrderAmountDToD;
                    if (CurrentDayRetailTraderOrderAmountDToD < 0) {
                        $(".CurrentDayRetailTraderOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        CurrentDayRetailTraderOrderAmountDToD = -CurrentDayRetailTraderOrderAmountDToD;
                    } else {
                        if (CurrentDayRetailTraderOrderAmountDToD == null||CurrentDayRetailTraderOrderAmountDToD == 0)
                            $(".CurrentDayRetailTraderOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/defaultdrop.png");
                        else
                        $(".CurrentDayRetailTraderOrderAmountDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".CurrentDayRetailTraderOrderAmountDToD").html((CurrentDayRetailTraderOrderAmountDToD ? CurrentDayRetailTraderOrderAmountDToD.toFixed(1) : "0") + "%");//当日分销业绩环比
                    $(".VipCount").html((data.VipCount ? data.VipCount : "0") + "人");//会员数   
                    $(".NewVipCount").html((data.NewVipCount ? data.NewVipCount : "0") + "人");//当日新增会员数

                    var NewVipDToD = data.NewVipDToD;
                    if (NewVipDToD < 0) {
                        $(".NewVipDToD").parents(".achievementnum").find(".rise").attr("src", "images/drop1.png");
                        NewVipDToD = -NewVipDToD;
                    } else {
                        if (NewVipDToD == null || NewVipDToD == 0)
                            $(".NewVipDToD").parents(".achievementnum").find(".rise").attr("src", "images/defaultdrop.png");
                        else
                            $(".NewVipDToD").parents(".achievementnum").find(".rise").attr("src", "images/rise1.png");
                    }
                    $(".NewVipDToD").html((NewVipDToD ? NewVipDToD.toFixed(1) : "0") + "%");//新增会员环比


                    $(".EventsCount").html((data.EventsCount ? data.EventsCount : "0") + "个");//活动数
                    $(".EventJoinCount").html((data.EventJoinCount ? data.EventJoinCount : "0") + "人");//活动参与人次

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
                    $(".VipContributePect").html((VipContributePect ? VipContributePect : "0") + "%");

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
                    $(".MonthArchivePect").html((MonthArchivePect ? MonthArchivePect : "0") + "%");


                    $(".CurrentMonthSingleUnitAvgTranCount").html(data.CurrentMonthSingleUnitAvgTranCount ? data.CurrentMonthSingleUnitAvgTranCount : 0);//单店月均消费人次
                    $(".CurrentMonthUnitAvgCustPrice").html(data.CurrentMonthUnitAvgCustPrice ? data.CurrentMonthUnitAvgCustPrice : 0);//门店月均单价
                    

                    that.NumberFormat(data.CurrentMonthSingleUnitAvgTranAmount, function (result, isyuan) {
                        $(".CurrentMonthSingleUnitAvgTranAmount").html(result);//单店月均业绩
                        if (isyuan)
                            $(".CurrentMonthSingleUnitAvgTranAmount").parents(".optiondesc").find(".CurrentMonthunit").html("元");
                        else
                            $(".CurrentMonthSingleUnitAvgTranAmount").parents(".optiondesc").find(".CurrentMonthunit").html("万元");

                    });

                    
                    that.NumberFormat(data.CurrentMonthTranAmount, function (result, isyuan) {
                        $(".CurrentMonthTranAmount").html(result);//门店月均总业绩
                        if (isyuan)
                            $(".CurrentMonthTranAmount").parents(".optiondesc").find(".CurrentMonthunit").html("元");
                        else
                            $(".CurrentMonthTranAmount").parents(".optiondesc").find(".CurrentMonthunit").html("万元");

                    });

                    $(".PreAuditOrder").html(data.PreAuditOrder?data.PreAuditOrder:0);//待审核订单
                    $(".PreSendOrder").html(data.PreSendOrder ? data.PreSendOrder : 0);//待发货订单
                    $(".PreTakeOrder").html(data.PreTakeOrder ? data.PreTakeOrder : 0);//门店待提货订单
                    $(".PreRefund").html(data.PreRefund ? data.PreRefund : 0);//待退货
                    $(".PreReturnCash").html(data.PreReturnCash ? data.PreReturnCash : 0);//待退款

					
					that.quicklyDialog();
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
        },//数据格式转换
        NumberFormat: function (oldformatdata, callback)
        {
            var isyuan = true;
            var result = 0;
            if (oldformatdata) {
                if (oldformatdata > 9999) {
                    
                    result = (oldformatdata / 10000).toFixed(2);
                    isyuan = false;
                } else {
                    result= oldformatdata.toFixed(0);
                }
            }
            if (callback) {
                callback(result, isyuan);
            }
        }
        ,
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

