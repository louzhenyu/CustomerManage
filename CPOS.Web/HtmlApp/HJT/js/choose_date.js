Jit.AM.defindPage({

    name: 'ChooseDate',

    onPageLoad: function () {
		
        this.initEvent();
		
		this.initPage();
    },

    initEvent: function () {

        var me = this;

        me.windowHeight = window.innerHeight;

        me.windowWidth = window.innerWidth;
		
		me.date = new Date();
		
		me.thisYear   = me.date.getFullYear();
		
        me.thisMonth  = me.date.getMonth()+ 1;
		
        me.thisDay    = me.date.getDate();
    },
	
	initPage:function(){
		
		var me = this,year,month,
			firstDate = new Date(me.thisYear+'/'+me.thisMonth+'/1');
		
		for(var i=0;i<=2;i++){
			
			if(me.thisMonth == 12){
				
				year = me.thisYear + 1;
				
				month = 1;
				
			}else{
				
				year = me.thisYear;
				
				month = me.thisMonth + i;
			}
			
			curDate = new Date(year+'/'+month+'/1');
			
			$($('.cui_cldmonth').get(i)).html(me.getDateWithFormat(curDate,'YYYY年MM月'));
			
			me.buildMonthView($('[name=month_'+i+']'),curDate);
		}
		
		$('.valid').bind('click',function(evt){
			
			var target = $(evt.currentTarget);
			
			me.selectDate(target);
		});
		
		me.dateType = 'In';
	},
	
	setInDate:function(){
		$($('.btn_date').find('.active')).removeClass('active');
		$($('.btn_date').children().get(0)).addClass('active');
		this.dateType = 'In';
	},
	setOutDate:function(){
		$($('.btn_date').find('.active')).removeClass('active');
		$($('.btn_date').children().get(1)).addClass('active');
		this.dateType = 'Out';
	},
	selectDate:function(target){
		
		var me = this,dom;
		
		if(me.InDate && me.dateType == 'Out'){
		
			var od = target.attr('data-date');
			
			od = new Date(od.replace(/-/gi,'/'));
			
			id = new Date(me.InDate.replace(/-/gi,'/'));
			
			if(od.getTime()<=id.getTime()){
				
				return ;
			}
		}else if(me.OutDate && me.dateType == 'In'){
			
			var id = target.attr('data-date');
			
			id = new Date(id.replace(/-/gi,'/'));
			
			od = new Date(me.OutDate.replace(/-/gi,'/'));
			
			if(od.getTime()<=id.getTime()){
				
				return ;
			}
		}
		
		
		
		if(me.dateType == 'In'){
			
			dom = $('.selected-start');
			
			$(dom.find('i')).html('');
			
			if(dom.attr('dayDis')){
				
				$(dom.find('i')).html(dom.attr('dayDis'));
				
				dom.removeClass('selected-start').removeClass('cui_cld_daycrt');
				
			}else{
			
				dom.removeClass('selected-start').removeClass('cui_cld_day_havetxt cui_cld_daycrt');
			}
			
			target.addClass('selected-start cui_cld_day_havetxt cui_cld_daycrt');
			
			$(target.find('i')).html('入住');
			
			me.InDate = target.attr('data-date');
			
			me.setParams('InDate',target.attr('data-date'));
			
			me.setOutDate();
			
		}else if(me.dateType == 'Out'){
			
			dom = $('.selected-end');
			
			$(dom.find('i')).html('');
			
			dom.each(function(i,em){
				
				if($(em).attr('dayDis')){
				
					$($(em).find('i')).html($(em).attr('dayDis'));
					
					$(em).removeClass('selected-end').removeClass('cui_cld_daycrt');
					
				}else{
				
					$(em).removeClass('selected-end').removeClass('cui_cld_day_havetxt cui_cld_daycrt');
				}
			});
			
			target.addClass('selected-end cui_cld_day_havetxt cui_cld_daycrt');
			
			$(target.find('i')).html('退房');
			
			me.focusDates();
			
			me.OutDate = target.attr('data-date');
			
			me.setParams('OutDate',target.attr('data-date'));
		}
		
	},
	focusDates : function(){
	
		var inDom = $('.selected-start'),
			outDom = $('.selected-end'),
			loop = false;
		
		if(inDom.length==0 || outDom.length==0){
		
			return;
		}
		
		$('.valid').each(function(i,item){
			
			if($(item).hasClass('selected-start')){
				
				loop = true;
				
			}else{
				if($(item).hasClass('selected-end')){
				
					loop = false;
					
					return;
				}
				if(loop){
				
					$(item).addClass('selected-end cui_cld_day_havetxt cui_cld_daycrt');
				}
			}
			
		});
	},
	buildMonthView: function(panel,date){
		
		var me = this,
			buildMonth = date.getMonth();
		
		var thisMonthFirstDate = date,
			loopDate = thisMonthFirstDate,daystr;
			
		var nulldays = loopDate.getDay(),
			panel = $(panel),daydom,daynum,
			dayDis = ['今天','明天','后天'];
		
		for(var i=1;i<=nulldays;i++){
			
			panel.append($('<li class="cui_cld_dayfuture invalid"></li>'));
		}
		
		do{
			daystr = me.getDateWithFormat(loopDate,'YYYY-MM-DD');
			
			daynum = loopDate.getDate();
			
			if( (daynum<me.thisDay && loopDate.getMonth() == me.date.getMonth()) ){
				
				daydom = $('<li data-date="'+daystr+'" class="cui_cld_daypass invalid"><em>&nbsp;'+daynum+'&nbsp;</em></li>');
				
			}else if( (daynum>=me.thisDay && loopDate.getMonth() == me.date.getMonth()) ){
				
				var cday = daynum-me.thisDay;
				
				if(cday<=2){
					
					daydom = $('<li data-date="'+daystr+'" dayDis="'+dayDis[cday]+'" class="valid cui_cld_day_havetxt"><em>'+daynum+'</em><i>'+dayDis[cday]+'</i></li>');
					
				}else{
					
					daydom = $('<li data-date="'+daystr+'" class="valid"><em>'+daynum+'</em><i></i></li>');
				}
				
			}else if(loopDate.getMonth() > me.date.getMonth()){
				
				daydom = $('<li data-date="'+daystr+'" class="valid"><em>'+daynum+'</em><i></i></li>');
			}
			
			panel.append(daydom);
			
			loopDate = me.changeDate(loopDate,0,0,1);
			
		}while( loopDate.getMonth() == buildMonth )
		
	},
	
	getDateWithFormat:function(date,format){
	
		var me = this;
		
		format = format.replace('YYYY',date.getFullYear());
		
		format = format.replace('MM',date.getMonth()+1);
		
		format = format.replace('DD',date.getDate());
		
		return format;
	},
	changeDate:function(date,year,month,day){
		
		if(!date){
		
			date = new Date();
		}
		
		if(year){
		
			date.setFullYear(date.getFullYear()+year);
		}
		if(month){
		
			date.setMonth(date.getMonth()+month);
		}
		if(day){
		
			date.setDate(date.getDate()+day);
		}
		
		return date;
	}
});