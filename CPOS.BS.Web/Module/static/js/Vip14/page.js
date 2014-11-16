define(['jquery', 'tools', 'highcharts', 'mustache'], function ($, temp) {

    var page = {

        init: function () {

            $.util.ajax({
                customerId: $.util.getUrlParam('CustomerId') || '92a251898d63474f96b2145fcee2860c',
                url: "/ApplicationInterface/Vip/VipGateway.ashx",
                data: {
                    action: 'GetVipTotal'
                },
                success: function (data) {

                    if (data.IsSuccess) {

                        self.updateBaseInfo(data.Data);

                        self.initHighCharts(data.Data);

                        //self.updateEventTable(data.Data);
                    }
                },
                error: function () {
                    //self.initHighCharts();
                }
            });
        },
        initHighCharts: function (info) {
            var data = info.AttentionVipList;
            //var data = [{ "DisplayIndex": 0, "Date": "2014/5/11 0:00:00", "CumulativeNo": 0 }, { "DisplayIndex": 0, "Date": "2014/5/12 0:00:00", "CumulativeNo": 2 }, { "DisplayIndex": 0, "Date": "2014/5/13 0:00:00", "CumulativeNo": 2 }, { "DisplayIndex": 0, "Date": "2014/5/14 0:00:00", "CumulativeNo": 4 }, { "DisplayIndex": 0, "Date": "2014/5/15 0:00:00", "CumulativeNo": 6 }, { "DisplayIndex": 0, "Date": "2014/5/16 0:00:00", "CumulativeNo": 8 }, { "DisplayIndex": 0, "Date": "2014/5/17 0:00:00", "CumulativeNo": 9 }, { "DisplayIndex": 0, "Date": "2014/5/18 0:00:00", "CumulativeNo": 9 }, { "DisplayIndex": 0, "Date": "2014/5/19 0:00:00", "CumulativeNo": 10 }, { "DisplayIndex": 0, "Date": "2014/5/20 0:00:00", "CumulativeNo": 11 }, { "DisplayIndex": 0, "Date": "2014/5/21 0:00:00", "CumulativeNo": 12 }, { "DisplayIndex": 0, "Date": "2014/5/22 0:00:00", "CumulativeNo": 12 }, { "DisplayIndex": 0, "Date": "2014/5/23 0:00:00", "CumulativeNo": 12 }, { "DisplayIndex": 0, "Date": "2014/5/24 0:00:00", "CumulativeNo": 13 }, { "DisplayIndex": 0, "Date": "2014/5/25 0:00:00", "CumulativeNo": 14 }, { "DisplayIndex": 0, "Date": "2014/5/26 0:00:00", "CumulativeNo": 14 }, { "DisplayIndex": 0, "Date": "2014/5/27 0:00:00", "CumulativeNo": 15 }, { "DisplayIndex": 0, "Date": "2014/5/28 0:00:00", "CumulativeNo": 17 }, { "DisplayIndex": 0, "Date": "2014/5/29 0:00:00", "CumulativeNo": 18 }, { "DisplayIndex": 0, "Date": "2014/5/30 0:00:00", "CumulativeNo": 19 }, { "DisplayIndex": 0, "Date": "2014/5/31 0:00:00", "CumulativeNo": 20 }, { "DisplayIndex": 0, "Date": "2014/6/1 0:00:00", "CumulativeNo": 20 }, { "DisplayIndex": 0, "Date": "2014/6/2 0:00:00", "CumulativeNo": 22 }, { "DisplayIndex": 0, "Date": "2014/6/3 0:00:00", "CumulativeNo": 22 }, { "DisplayIndex": 0, "Date": "2014/6/4 0:00:00", "CumulativeNo": 23 }, { "DisplayIndex": 0, "Date": "2014/6/5 0:00:00", "CumulativeNo": 23 }, { "DisplayIndex": 0, "Date": "2014/6/6 0:00:00", "CumulativeNo": 23 }, { "DisplayIndex": 0, "Date": "2014/6/7 0:00:00", "CumulativeNo": 23 }, { "DisplayIndex": 0, "Date": "2014/6/8 0:00:00", "CumulativeNo": 23 }, { "DisplayIndex": 0, "Date": "2014/6/9 0:00:00", "CumulativeNo": 24}];
            var dataList = [], dateList = [];//两个数组
            var yMin = 0, flag = 0;
            for (var i = 0, idata; i < data.length; i++) {
                idata = data[i];
                dataList.push(idata.CumulativeNo);//数量
                dateList.push(idata.Date);//日期
                if (flag == 0) {
                    yMin = idata.CumulativeNo;
                    flag = 1;
                }
                else {
                    yMin = Math.min(yMin, idata.CumulativeNo);
                }
            }


            $('#chartSection').highcharts({
                title: {
                    text: '',
                    x: -20 //center
                },
                subtitle: {
                    text: '',
                    x: -20
                },
                xAxis: {
                    categories: dateList,
                    labels: {
                        formatter: function () {
                            //console.log(this.value);
                            var date = new Date(this.value);
                            return (date.getMonth()+1)+"."+date.getDate()
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: '人数'
                    },
                    min: yMin
                },
                tooltip: {
                    valueSuffix: '人'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: '人数',
                    data: dataList
                }]
            });

        },
        updateBaseInfo: function (info) {
            var vipinfo = info.VipInfo;
            $('#todayVipCount').html(vipinfo.TodayVipCount);
            $('#todayVipPercent').html(vipinfo.AddRatioByDay);
            $('#totalVipCount').html(vipinfo.MonthVipCount);
            $('#MonthVipPercent').html(vipinfo.AddRatioByMonth);
            $('#upShelfCount').html(vipinfo.UpShelfCount);
            $('#offShelfCount').html(vipinfo.OffShelfCount);
            $('#onlineUnitCount').html(vipinfo.OnlineUnitCount);
            $('#sentCount').html(vipinfo.SentCount);
            $('#paidNotSentCount').html(vipinfo.PaidNotSentCount);
            $('#cashOnDeliveryCount').html(vipinfo.CashOnDeliveryCount);
            var daystate = parseInt(vipinfo.AddRatioByDay),
				monthstate = parseInt(vipinfo.AddRatioByMonth);
            if (daystate > 0) {
                $('#daystate').addClass('rise');
            } else if (daystate < 0) {
                $('#daystate').addClass('drop');
            }
            if (monthstate > 0) {
                $('#monthstate').addClass('rise');
            } else if (monthstate < 0) {
                $('#monthstate').addClass('drop');
            }
        }
    }, self = page;
    page.init();
})