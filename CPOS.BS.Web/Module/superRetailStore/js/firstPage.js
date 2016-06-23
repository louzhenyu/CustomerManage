define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','highcharts','kindeditor'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            uiMask: $("#ui-mask"),
            click: true
        },
        LocationData: {},
        init: function () {
            var that = this;
            //加载页面数据并缓存
            that.loadPageData();
        },
        //加载页面数据请求。判断页面是否有数据
        loadPageData: function (e) {
            var that = this;
            that.loadData.getLoadInfo(function (data) {
                //无分销体系
				debugger;
                if (data.Data.IsDataNull == 0) {
                    $('.missDatecol').css('display', 'block');
					$('#oneHrefBtn').text('一键分销');
					$('#twoHrefBtn').text('添加商品');
					$('#threeHrefBtn').text('添加拓展工具');
                    that.LocationData = data.Data;
                } else{  //有分销有数据
					//data.Data.IsRTSalesOrder == 1
                    $($('.col')[0]).css('display', 'block');
                    $($('.col')[1]).css('display', 'block');
                    that.LocationData = data.Data;
                    that.getSuperRetailData();
                    that.initEvent();
                }
            })
        },
        //加载charts
        getSuperRetailData: function () {
            var that = this;
            //图表顶部数据
			if(that.LocationData.RTTotalSales){
				$($('.chartTitleData')[0]).find('span').eq(1).text(that.mathInit(that.LocationData.RTTotalSales.RTTotalSalesAmount));
            	$($('.chartTitleData')[1]).find('span').eq(1).text(that.mathInit(that.LocationData.RTTotalSales.RTDay7AddSalesAmount));
			}
            if(that.LocationData.RTTotalCount){
				$($('.chartTitleData')[2]).find('span').eq(1).text(that.LocationData.RTTotalCount.RTTotalCount);
            	$($('.chartTitleData')[3]).find('span').eq(1).text(that.LocationData.RTTotalCount.Day7AddRTCount);
			}
            
            //图表下侧数据 
            /*分销商品订单*/
            //左侧
			if(that.LocationData.DaySevenRTOrder){
				$($('.dataDiv')[0]).find('span').eq(0).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCount)
				if (that.LocationData.DaySevenRTOrder.DaysRTOrderCountRate >= 0) {
					$($('.dataDiv')[0]).find('img').attr("src", "images/2.1_24.png");
					$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCountRate + '%')
				} else {
					$($('.dataDiv')[0]).find('img').attr("src", "images/down.png");
					$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCountRate.toString().substr(1) + '%')
				}
				//右侧
				$($('.dataDiv')[1]).find('span').eq(0).text(that.mathInit(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmount))
				if (that.LocationData.DaySevenRTOrder.DaysTRTAvgAmountRate >= 0) {
					$($('.dataDiv')[1]).find('img').attr("src", "images/2.1_24.png");
					$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmountRate + '%')
				} else {
					$($('.dataDiv')[1]).find('img').attr("src", "images/down.png");
					$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmountRate.toString().substr(1) + '%')
				}
			}
            
            /*活跃分销商*/
            //左侧
			if(that.LocationData.DaySevenActivityRT){
				$($('.dataDiv')[2]).find('span').eq(0).text(that.LocationData.DaySevenActivityRT.DaysActiveRTCount)
				if (that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate >= 0) {
					$($('.dataDiv')[2]).find('img').attr("src", "images/2.1_24.png");
					$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate + '%')
				} else {
					$($('.dataDiv')[2]).find('img').attr("src", "images/down.png");
					$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate.toString().substr(1) + '%')
				}
			}
            /*分销拓展*/
            //左侧
			if(that.LocationData.DaySevenRTShare){
				$($('.dataDiv')[3]).find('span').eq(0).text(that.LocationData.DaySevenRTShare.DaysRTShareCount)
				if (that.LocationData.DaySevenRTShare.DaysRTShareCountRate >= 0) {
					$($('.dataDiv')[3]).find('img').attr("src", "images/2.1_24.png");
					$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysRTShareCountRate + '%')
				} else {
					$($('.dataDiv')[3]).find('img').attr("src", "images/down.png");
					$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysRTShareCountRate.toString().substr(1) + '%')
				}
				//右侧
				$($('.dataDiv')[4]).find('span').eq(0).text(that.LocationData.DaySevenRTShare.DaysAddRTCount)
				if (that.LocationData.DaySevenRTShare.DaysAddRTCountRate >= 0) {
					$($('.dataDiv')[4]).find('img').attr("src", "images/2.1_24.png");
					$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysAddRTCountRate + '%')
				} else {
					$($('.dataDiv')[4]).find('img').attr("src", "images/down.png");
					$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysAddRTCountRate.toString().substr(1) + '%')
				}
			}
            var DayVipRTSalesAmount = [];
            var DayUserRTSalesAmount = [];
            var DayDateStr = [];
            var isVipSaleCount = false,
                isUserSaleCount = false,
                isVipRtCount = false,
                isUserRtCount = false;
            that.LocationData.DaySevenRTSalesList = that.LocationData.DaySevenRTSalesList.reverse();
            for(var i=0;i<that.LocationData.DaySevenRTSalesList.length;i++){
                if(that.LocationData.DaySevenRTSalesList[i].DayVipRTSalesAmount!=''){
                    isVipSaleCount = true;
                }
                if(that.LocationData.DaySevenRTSalesList[i].DayUserRTSalesAmount!=''){
                    isUserSaleCount = true;
                }
                var DataCount = that.LocationData.DaySevenRTSalesList[i].DateStr;
                var DataMonth = DataCount.substring(4,6);
                var DataDay = DataCount.substring(6,8);
                if(DataMonth.substring(0,1)=='0'){
                    DataMonth = DataMonth.substring(1,2);
                }
                if(DataDay.substring(0,1)=='0'){
                    DataDay = DataDay.substring(7,8);
                }
                DataCount = DataMonth+"月"+DataDay+"日";
                DayVipRTSalesAmount.push(that.LocationData.DaySevenRTSalesList[i].DayVipRTSalesAmount);
                DayUserRTSalesAmount.push(that.LocationData.DaySevenRTSalesList[i].DayUserRTSalesAmount);
                DayDateStr.push(DataCount);

            }

            if(isVipSaleCount == false){
                for(var j =0;j<DayVipRTSalesAmount.length;j++){
                        DayVipRTSalesAmount[j]="";
                }
            }
            if(isUserSaleCount == false){
                for(var k =0;k<DayUserRTSalesAmount.length;k++){
                        DayUserRTSalesAmount[k]="";
                }
            }
            //近7天新增销售额
            $('#allSale').highcharts({
                chart: {
                    backgroundColor: '#f7f7fa',
                    plotBorderWidth:1,
                    spacingRight: 30,
                    plotShadow: false,
                    title: false
                },
                colors: ['#ffbd1f', '#ff8a00'],
                legend: {
                    verticalAlign: 'top',
                    itemDistance: 40,
                    x: 140
                },
                credits: {
                    enabled: false
                },
                title: {text: null},
                xAxis: {
                    zIndex: 0,
                    categories: DayDateStr,
                    tickLength:0


                },
                yAxis: {
                    title: {
                        text: '(元)' ,
                        rotation:0,
                        align:"high"
                    },
                    min:0,
                    gridLineColor: '#e5e5e5',//横向网格线颜色
                    gridLineWidth: 1,//横向网格线宽度
                },
                tooltip: { valueSuffix: '(元)' },
               
                series: [
                    {
                        name: '会员',
                        data: DayVipRTSalesAmount
                    },
                    {
                        name: '店员',
                        data:DayUserRTSalesAmount
                    }
                ]
            });
            //近7天新增分销商
            var DayAddVipRTCount = [];
            var DayAddUserRTCount = [];
            var addDateStr = [];
            that.LocationData.DaySevenRTCountList = that.LocationData.DaySevenRTCountList.reverse();
            for(var i=0;i<that.LocationData.DaySevenRTCountList.length;i++){
                if(that.LocationData.DaySevenRTCountList[i].DayAddVipRTCount!=''){
                    isVipRtCount = true;
                }
                if(that.LocationData.DaySevenRTCountList[i].DayAddUserRTCount!=''){
                    isUserRtCount = true;
                }
                var DataCount = that.LocationData.DaySevenRTCountList[i].DateStr;
                var DataMonth = DataCount.substring(4,6);
                var DataDay = DataCount.substring(6,8);
                if(DataMonth.substring(0,1)=='0'){
                    DataMonth = DataMonth.substring(1,2);
                }
                if(DataDay.substring(0,1)=='0'){
                    DataDay = DataDay.substring(7,8);
                }
                DataCount = DataMonth+"月"+DataDay+"日";
                DayAddVipRTCount.push(that.LocationData.DaySevenRTCountList[i].DayAddVipRTCount);
                DayAddUserRTCount.push(that.LocationData.DaySevenRTCountList[i].DayAddUserRTCount);
                addDateStr.push(DataCount);
            }
            if(isVipRtCount == false){
                for(var j =0;j<DayAddVipRTCount.length;j++){
                    DayAddVipRTCount[j]="";
                }
            }
            if(isUserRtCount == false){
                for(var k =0;k<DayAddUserRTCount.length;k++){
                    DayAddUserRTCount[k]="";
                }
            }
            $('#allSales').highcharts({
                chart: {
                    backgroundColor: '#f7f7fa',
                    plotBorderWidth: 1,
                    spacingRight: 30,
                    plotShadow: false,
                    title: false
                },
                colors: ['#ffbd1f', '#ff8a00'],
                legend: {
                    verticalAlign: 'top',
                    itemDistance: 40,
                    x: 140,
                },
                credits: {
                    enabled: false
                },
                title: { text: null },
                xAxis: {
                    zIndex: 0,
                    categories:addDateStr,
                    tickLength:0
                },
                yAxis: {
                    title: {
                        text: '(人)' ,
                        rotation:0,
                        align:"high",
                        margin:10
                    },
                    min:0,
                    gridLineColor: '#e5e5e5',//横向网格线颜色
                    gridLineWidth: 1,//横向网格线宽度

                },
                tooltip: { valueSuffix: '(人)' },
                series: [
                    {
                        name: '会员',
                        data: DayAddVipRTCount
                    },
                    {
                        name: '店员',
                        data: DayAddUserRTCount
                    }
                ]
            });
        },

        mathInit:function(num){
            num = parseFloat(num).toString();
            if(Number(num)>10000){
                num = parseInt(num).toString();
                num = num.substring(0,num.length-4)+"."+num.substring(num.length-4,num.length-1);
                num = Number(num).toFixed(2)+"万";
                return num;
            }else{
                return num;
            }
        },
        //7/30天切换时间
        initEvent: function () {
            var that = this;
            //点击切换查看5天数据
            that.elems.sectionPage.delegate(".checkDay", "click", function (e) {
                //判断查看的天数
                var dateNum = parseInt($(this).text().substr(1, 1));
                //判断查看的对象
                var checkObj = $(this).parent().prev().text();
                if (checkObj == '分销商品订单') {
                    if (dateNum == 3) {
                        //左侧
                        $($('.checkDay')[1]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[0]).css('color', '#999999');                 //近30天
						if(that.LocationData.DayThirtyRTOrder){
							$($('.dataDiv')[0]).find('span').eq(0).text(that.LocationData.DayThirtyRTOrder.DaysRTOrderCount)
							if (that.LocationData.DayThirtyRTOrder.DaysRTOrderCountRate >= 0) {
								$($('.dataDiv')[0]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DayThirtyRTOrder.DaysRTOrderCountRate + '%')
							} else {

								$($('.dataDiv')[0]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DayThirtyRTOrder.DaysRTOrderCountRate + '%')
							}
							//右侧
							$($('.dataDiv')[1]).find('span').eq(0).text(that.mathInit(that.LocationData.DayThirtyRTOrder.DaysTRTAvgAmount))
							if (that.LocationData.DayThirtyRTOrder.DaysTRTAvgAmountRate >= 0) {
								$($('.dataDiv')[1]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DayThirtyRTOrder.DaysTRTAvgAmountRate + '%')
							} else {
								$($('.dataDiv')[1]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DayThirtyRTOrder.DaysTRTAvgAmountRate + '%')
							}
						}
                        
                    } else {
                        $($('.checkDay')[0]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[1]).css('color', '#999999');                 //近30天
                        //左侧
						if(that.LocationData.DaySevenRTOrder){
							$($('.dataDiv')[0]).find('span').eq(0).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCount)
							if (that.LocationData.DayThirtyRTOrder.DaysRTOrderCountRate >= 0) {
								$($('.dataDiv')[0]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCountRate + '%')
							} else {
								$($('.dataDiv')[0]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[0]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysRTOrderCountRate + '%')
							}
							//右侧
							$($('.dataDiv')[1]).find('span').eq(0).text(that.mathInit(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmount))
							if (that.LocationData.DayThirtyRTOrder.DaysTRTAvgAmountRate >= 0) {
								$($('.dataDiv')[1]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmountRate + '%')
							} else {
								$($('.dataDiv')[1]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[1]).find('span').eq(2).text(that.LocationData.DaySevenRTOrder.DaysTRTAvgAmountRate + '%')
							}
						}
                    }
                }
                else if (checkObj == '活跃分销商') {
                    if (dateNum == 3) {
                        $($('.checkDay')[3]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[2]).css('color', '#999999');                 //近30天
						if(that.LocationData.DayThirtyActivityRT){
							$($('.dataDiv')[2]).find('span').eq(0).text(that.LocationData.DayThirtyActivityRT.DaysActiveRTCount)
							if (that.LocationData.DayThirtyActivityRT.DaysRTOrderCountRate >= 0) {
								$($('.dataDiv')[2]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DayThirtyActivityRT.DaysRTOrderCountRate + '%')
							} else {
								$($('.dataDiv')[2]).find('img').attr("src", "images/down.png");

								$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DayThirtyActivityRT.DaysRTOrderCountRate + '%')
							}
						}	
                    } else {
                        $($('.checkDay')[2]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[3]).css('color', '#999999');                 //近30天
						if(that.LocationData.DaySevenActivityRT){
							$($('.dataDiv')[2]).find('span').eq(0).text(that.LocationData.DaySevenActivityRT.DaysActiveRTCount)
							if (that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate >= 0) {
								$($('.dataDiv')[2]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate + '%')
							} else {
								$($('.dataDiv')[2]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[2]).find('span').eq(2).text(that.LocationData.DaySevenActivityRT.DaysRTOrderCountRate + '%')
							}
						}
                    }

                } else if (checkObj == '分销拓展') {
                    if (dateNum == 3) {
                        /*分销拓展*/
                        $($('.checkDay')[5]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[4]).css('color', '#999999');                 //近30天
                        //左侧
						
						if(that.LocationData.DayThirtyRTShare){
							$($('.dataDiv')[3]).find('span').eq(0).text(that.LocationData.DayThirtyRTShare.DaysRTShareCount)
							if (that.LocationData.DayThirtyRTShare.DaysRTShareCountRate >=0) {
								$($('.dataDiv')[3]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DayThirtyRTShare.DaysRTShareCountRate + '%')
							} else {
								$($('.dataDiv')[3]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DayThirtyRTShare.DaysRTShareCountRate + '%')
							}
							//右侧
							$($('.dataDiv')[4]).find('span').eq(0).text(that.LocationData.DayThirtyRTShare.DaysAddRTCount)
							if (that.LocationData.DayThirtyRTShare.DaysAddRTCountRate >= 0) {
								$($('.dataDiv')[4]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DayThirtyRTShare.DaysAddRTCountRate + '%')
							} else {
								$($('.dataDiv')[4]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DayThirtyRTShare.DaysAddRTCountRate + '%')
							}
						}	
                    } else {
                        /*分销拓展*/
                        $($('.checkDay')[4]).css('color', '#038efe');                 //近7天
                        $($('.checkDay')[5]).css('color', '#999999');                 //近30天
                        //左侧
						if(that.LocationData.DaySevenRTShare){
							$($('.dataDiv')[3]).find('span').eq(0).text(that.LocationData.DaySevenRTShare.DaysRTShareCount)
							if (that.LocationData.DaySevenRTShare.DaysRTShareCountRate >= 0) {
								$($('.dataDiv')[3]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysRTShareCountRate + '%')
							} else {
								$($('.dataDiv')[3]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[3]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysRTShareCountRate + '%')
							}
							//右侧
							$($('.dataDiv')[4]).find('span').eq(0).text(that.LocationData.DaySevenRTShare.DaysAddRTCount)
							if (that.LocationData.DaySevenRTShare.DaysAddRTCountRate >= 0) {
								$($('.dataDiv')[4]).find('img').attr("src", "images/2.1_24.png");
								$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysAddRTCountRate + '%')
							} else {
								$($('.dataDiv')[4]).find('img').attr("src", "images/down.png");
								$($('.dataDiv')[4]).find('span').eq(2).text(that.LocationData.DaySevenRTShare.DaysAddRTCountRate + '%')
							}
						}	
                    }
                }
            })
        },
        loadData: {
            args: {
            },
            getLoadInfo: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        'action': 'SuperRetailTrader.RTAboutReport.GetOverView'
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
        }
    };
    page.init();
});

