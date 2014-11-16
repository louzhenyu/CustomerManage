define(['jquery', 'tools','mobileScroll','echarts'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        url:"/ApplicationInterface/Project/PG/PGHandler.ashx",
        init: function () {
        	var that=this;
        	var customerId=$.util.getUrlParam("customerId"),
        		token=$.util.getUrlParam("token"),
        		userId=$.util.getUrlParam("userId"),
        		email=$.util.getUrlParam("email");
        	that.customerId=customerId;
        	that.token=token;
        	that.userId=userId;
        	that.email=email;	
        	$("#section a").each(function(i){
        		$(this).attr("href",$(this).attr("href")+"&customerId="+customerId);
        	});
        	this.DateTimeEvent();
        	this.initEvent();
        	//请求数据
        	this.loadData();
        },
        currentTime:function(){ 
	        var now = new Date();
	        var year = now.getFullYear();       //年
	        var month = now.getMonth() + 1;     //月
	        var day = now.getDate();            //日
	       
	        var hh = now.getHours();            //时
	        var mm = now.getMinutes();          //分
	       
	        var clock = year + "-";
	       
	        if(month < 10)
	            clock += "0";
	       
	        clock += month + "-";
	       
	        if(day < 10)
	            clock += "0";
	           
	        clock += day + " ";
	       var time="";
	        if(hh < 10)
	            time += "0";
	           
	        time += hh + ":";
	        if (mm < 10) time += '0'; 
	        time += mm; 
	        return {date:clock,time:time}; 
	    },
	    //获得ajax的请求数据
	    loadData:function(callback){
	    	var that=this;
	    	$.util.ajax({
	    		url:that.url,
	    		customerId:that.customerId,
	    		userId:that.userId,
	    		data:{
	    			action:'CommonRequestReportForm',
	    			PGToken:that.token,
	    			ReportAction: "ireport!studentStatMonthlyReport.action",
	    			DynamicParam:"coachee="+that.email
	    		},
	    		success:function(data){
	    			debugger;
	    			data=JSON.parse(data.Data.JsonResult);
	    			if(data.error.err_code==0){
	    				//处理data数据
						var list=data.list;
						list=list?list:[];
						var months=[];var datas={};
						var pointArrayArray = [];
						var averages = [];
						var arrayData = [];

						var items = [];
						items[0] = "Identify joint Value Drivers";
						items[1] = "Align Objectives";
						items[2] = "Co-create and Sell the plan";
						items[3] = "Close the Sell";
						items[4] = "Excute Perfectly";
						items[5] = "Average";

						if (list.length == 0) {
	    			        for (var k = 0; k < 6; k++) {
	    			            var arr0 = [0, 0, 0, 0, 0, 0];
	    			            var obj0 = {};
	    			            obj0.name = items[k];
	    			            obj0.type = "line";
	    			            obj0.stack = "";
	    			            obj0.data = arr0;
	    			            arrayData.push(obj0);
	    			        }

	    			        var currYear = (new Date()).getFullYear();
	    			        var firstYear = currYear;
	    			        var currMonth = (new Date()).getMonth();
	    			        var firstMonth = currMonth - 5;
	    			        if (currMonth < 6) {
	    			            firstYear = currYear - 1;
	    			            firstMonth = 12 + currMonth - 5;
	    			        }

	    			        for (var m = 0; m < 6; m++) {
	    			            var monthString = firstYear + "-" + firstMonth;
	    			            months.push(monthString);
	    			            firstMonth++;
	    			            if (firstMonth > 12) {
	    			                firstMonth = firstMonth - 12;
	    			                firstYear++;
	    			            }
	    			        }

	    			        datas.months = months;
	    			        datas.legendnames = items;
	    			        datas.datas = arrayData;

	    			        //生成报表
	    			        that.generateCharts(datas);
	    			    } else {
	    			        for (var i = list.length - 1; i >= 0; i--) {
	    			            var item = list[i];
	    			            var month = item.month;
	    			            var avg = item.total_avg;
	    			            months[list.length - i - 1] = month;
	    			            var arr = item.item_avg.split("|");
	    			            pointArrayArray.push(arr);
	    			            averages.push(avg);
	    			        }

	    			        var names = items;

	    			        for (var j = 0, jl = pointArrayArray[0].length; j < jl; j++) {
	    			            var arr = [];
	    			            for (var i = 0, len = pointArrayArray.length; i < len; i++) {
	    			                arr.push(pointArrayArray[i][j]);
	    			            }

	    			            var obj = {};
	    			            obj.name = names[j];
	    			            obj.type = "line";
	    			            obj.stack = "";
	    			            obj.data = arr;
	    			            arrayData.push(obj);
	    			        }

	    			        // 平均分线
	    			        var avgobj = {};
	    			        avgobj.name = names[5];
	    			        avgobj.type = "line";
	    			        avgobj.stack = "";
	    			        avgobj.data = averages;
	    			        arrayData.push(avgobj);

	    			        datas.months = months;
	    			        datas.legendnames = names;
	    			        datas.datas = arrayData;

	    			        //生成报表
	    			        that.generateCharts(datas);
	    			    }
	    			}else{
	    				alert(data.error.err_msg);
	    			}
	    			
	    		}
	    		
	    	});
	    },
        DateTimeEvent : function() {
			//日期事件
			var self = this, 
				currYear = (new Date()).getFullYear(),
				currMonth=(new Date()).getMonth(),
				opt = {};
			//opt.datetime = { preset : 'datetime', minDate: new Date(2014,3,25,15,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
			opt.datetime = {preset : 'datetime'};
			opt.date = {
				preset : 'date',
				minDate:new Date()
			};
			opt.time = {
				preset : 'time',
				minDate:new Date()
			};
			opt["default"] = {
			    theme: 'android-ics light', //皮肤样式
			    display: 'modal', //显示方式
			    mode: 'scroller', //日期选择模式
			    lang: 'en',
			    dateFormat: 'yy-mm-dd',
			    dateOrder: 'yyyymmdd',
			    startYear: 1990, //开始年份
			    endYear: (currYear + 30), //结束年份,
			    CallBack: function (a, b, c) {
			    }
			};
			var curDate = this.currentTime().date;
			var ttx = "";
			var theYear = new Date().getFullYear();
			var theMonth = currMonth + 1 - 5;
			if (theMonth <= 0) {
			    theYear = theYear - 1;
			    theMonth = 12 + theMonth;
			}

			theYear = theYear + "-";
			if (theMonth < 10) {
			    theYear = theYear + "0";
			}

			ttx = theYear + theMonth + "-01";
			$("#txtDate").val(ttx);
			$("#txtTime").val(curDate);
			$("#txtDate").mobiscroll().date(opt['default']);
			$("#txtTime").mobiscroll().date(opt['default']);
		},
		//初始化时间
		initEvent:function(){
			var that=this;
			//生成蜘蛛网
			$("#search").click(function(e){
				
				that.loadData();
				
			});
		},
		//随机产生数组
		randomArray:function(begin,end){
			var arr=[];
			for(var i=0;i<7;i++){
				arr.push(Math.floor(Math.random(begin,end)*end+begin));
			}
			return arr;
		},
		//生成图表
		generateCharts:function(object){
			var option = {
			    tooltip : {
			        trigger: 'axis'
			    },
			    legend: {
			        data: object.legendnames
			    },
			    toolbox: {
			        show : false,
			        feature : {
			            mark : {show: true},
			            dataView : {show: true, readOnly: false},
			            magicType : {show: true, type: ['line', 'bar', 'stack', 'tiled']},
			            restore : {show: true},
			            saveAsImage : {show: true}
			        }
			    },
			    calculable : true,
			    xAxis : [
			        {
			            type : 'category',
			            boundaryGap : false,
			            data : object.months    //X轴坐标显示内容
			        }
			    ],
			    yAxis : [
			        {
			            type : 'value',
			            min:0,
			            max:5
			        }
			    ],
			    series : object.datas/*[
			        {
			            name:'邮件营销',
			            type:'line',
			            stack: '总量',
			            data:[120, 132, 101, 134, 90, 230, 210]
			        }
			    ]*/
			};
			
			echarts.init($('#container')[0]).setOption(option);
			/*
					$('#container').highcharts({
	            title: {
	                 text: 'Score Trends',
	                margin:80,  
	                x:0,
	                y:10
	            },
	            subtitle: {
	                text: '',
	                x: -20
	            },
	            xAxis: {
	                categories: ['14-02', '14-03', '14-04', '14-05', '14-06', '14-07']
	            },
	            yAxis: {
	            	min: 1,
	                max:5,
	                step:1,
	                offset:1,
	                allowDecimals:false,
	                title: {
	                    text: '(平均值)'
	                },
	                plotLines: [{
	                    value: 0,
	                    width: 1,
	                    color: '#808080'
	                }]
	            },
	            tooltip: {
	                valueSuffix: '°C'
	            },
	            legend: {
	                align: 'left',
	                verticalAlign: 'top',
	                borderWidth: 0,
	                floating: true,
	                x:50,
	                y: 30,
	                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor || '#FFFFFF'),
	                shadow: true
	            },
	            series: [{
	                name: 'Identify Joint',
	                data: [3.1, 2.5, 2.6, 3.4, 3.7,3.8]
	            }, {
	                name: 'Objectives',
	                data: [1.2, 1.4, 1.5, 1.6, 1.7,1.5]
	            }, {
	                name: 'Execute Perfectly',
	                data: [3.1, 3.2, 3.3, 3.4, 3.5,4.6]
	            },
	            {
	                name: 'Average Score',
	                data: [2.5, 2.3, 2.8, 2.6, 2.2,2.0]
	            },{
	                name: 'Co-create',
	                data: [4.1, 3.8, 3.6, 4.2, 4.1,3.9]
	            },
	            {
	                name: 'Close the Sell',
	                data: [4.1, 4.2, 4.3, 4.4, 4.7,4.8]
	            }
	            ]
	        });*/
			
		}
		
    };

    page.init();
});



