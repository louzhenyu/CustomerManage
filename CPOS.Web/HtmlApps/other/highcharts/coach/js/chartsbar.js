define(['jquery', 'tools','mobileScroll','echarts'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        init: function () {
        	var customerId=$.util.getUrlParam("customerId");
        	$("#section a").each(function(i){
        		$(this).attr("href",$(this).attr("href")+"&customerId="+customerId);
        	});
        	this.DateTimeEvent();
        	this.initEvent();
        	this.generateCharts([4, 4, 3, 4, 5,2,3]);
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
				theme : 'android-ics light', //皮肤样式
				display : 'modal', //显示方式
				mode : 'scroller', //日期选择模式
				lang : 'zh',
				startYear : 1900, //开始年份
				endYear : (currYear + 30), //结束年份,
				CallBack : function(a,b,c) {
				}
			};
			var curDate=this.currentTime().date;
			var ttx="";
			var theYear=new Date().getFullYear();
			ttx=theYear+"-"+((currMonth+1)<10?("0"+(currMonth+1)):(currMonth+1))+"-"+"01";
			$("#txtDate").attr("placeholder",ttx);
			 $("#txtTime").attr("placeholder",curDate);
			$("#txtDate").mobiscroll().date(opt['default']);
			$("#txtTime").mobiscroll().date(opt['default']);
		},
		//初始化时间
		initEvent:function(){
			var that=this;
			//生成蜘蛛网
			$("#search").click(function(e){
				//产生随机数
				var arr=that.randomArray(1,5);
				that.generateCharts(arr);
				
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
		generateCharts:function(array){
			var option={
				
				title : {
			        text: '按评分项统计柱状图',
			        textAlign:"center",
			        x:"center",
			        padding:50,
			        y:-20,
			        //subtext: '数据来自网络'
			    },
			    tooltip : {
			        trigger: 'axis'
			    },
			    legend: {  //图例
			    	orient:"horizontal",
			    	x:"center",
			    	y:"top",
			    	padding:2,
			    	background:"auto",
			        data:['Goal', 'Reality', 'Options', 'Demo Strategy', 
			                'Demo selling skills','Recognition','Will']
			    },
			    toolbox: {
			        show : false,
			        feature : {
			            mark : {show: true},
			            dataView : {show: true, readOnly: false},
			            magicType: {show: true, type: ['line', 'bar']},
			            restore : {show: true},
			            saveAsImage : {show: true}
			        }
			    },
			    calculable : true,
			    xAxis : [
			        {
			            type : 'value',
			            boundaryGap : [0, 0.5],
			            min:0,
			            max:5
			        }
			    ],
			    yAxis : [
			        {
			            type : 'category',
			            data : ['Goal', 'Reality', 'Options', 'Demo Strategy', 
			                'Demo selling skills','Recognition','Will']
			        }
			    ],
			    series : [
			    	{
		                name: '平均值',
		                type:"bar",
		                data: [2.2, 3.5, 5, 4, 3.8,4, 4.8]
	            	}
			    ]
			};
			
			echarts.init($('#container')[0]).setOption(option);
			/*
			$('#container').highcharts({
	            chart: {
	                type: 'bar'
	            },
	            title: {
	                text: '按评分项统计柱状图'
	            },
	            subtitle: {//二级标题
	                text: ''
	            },
	            xAxis: {
	            	categories: ['Goal', 'Reality', 'Options', 'Demo Strategy', 
			                'Demo selling skills','Recognition','Will'],
	                title: {
	                    text: null
	                },
	                min: 0,
	                
	            },
	            yAxis: {
	            	title: {
	                    text: '平均值 ',
	                    align: 'high'
	                },
	                labels: {
	                    overflow: 'justify'
	                }
	            },
	            tooltip: {
	                valueSuffix: '' //后缀名提示
	            },
	            plotOptions: {
	                bar: {
	                    dataLabels: {
	                        enabled: true
	                    }
	                }
	            },
	            legend: {
	                layout: 'vertical',
	                align: 'right',
	                verticalAlign: 'top',
	                x: -40,
	                y: 100,
	                floating: true,
	                borderWidth: 1,
	                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor || '#FFFFFF'),
	                shadow: true,
	                data: [
				       {
				                name:'蒸发量',
				                icon : 'image://../asset/ico/favicon.png',
				                textStyle:{fontWeight:'bold', color:'green'}
				       },
				       '降水量','最高气温','最低气温'
				     ]
	            },
	            credits: {
	                enabled: false
	            },
	            series: [ {
	                name: '平均值',
	                data: [2.2, 3.5, 5, 4, 3.8,4, 4.8]
	            }]
	        });*/
		}
		
    };

    page.init();
});


