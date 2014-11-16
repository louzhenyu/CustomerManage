Jit.AM.defindPage({

    name: 'RushListGoods',
	
    elements: {
	
        rushGoodsList: ''
    },

    initWithParam: function(param) {

        $('#goodsTitleUrl').attr('href', param.toUrl);

        if (param['bannerDisplay'] == false) {

            $('#goodsTitleUrl').hide();
        }
    },

    onPageLoad: function() {
        
        this.InitPageInfo();
		
        this.initEvent();
    },
    InitPageInfo: function() {
	
        var self = this;
		
        self.elements.rushGoodsList = $('.snappedList');
    },

    initEvent: function() {
		
        var self = this;
		
        var paramList = {
                'action':'getPanicbuyingItemList',
                'eventTypeId': 2,
                'page': 1,
                'pageSize': 99
            };
			
		Jit.UI.AjaxTips.Loading(true);
		
        self.ajax({
            url: '/Interface/data/ItemData.aspx',
            data: paramList,
            success: function(data) {
				
				Jit.UI.AjaxTips.Loading(false);
				
                if (data.code == 200 && data.content.itemList) {
				
					self.elements.rushGoodsList.html(self.GetRushGoodsList(data.content.itemList));
					
					setInterval(function(){
						
						self.timeDown();
					},1000);
					
                };
            }
        });
    },
    GetRushGoodsList: function(datas) {
		
		var tpl = $('#tpl_item').html(), html = '';
		
        for (var i = 0; i < datas.length; i++) {
            
			var dataInfo = datas[i];
			
			dataInfo.overTime = dataInfo.deadlineTime;
			
			dataInfo.imageUrl = dataInfo.imageUrl;
			//Jit.UI.Image.getSize(dataInfo.imageUrl,480);

			dataInfo.discountRate = dataInfo.discountRate/10;
			
			html += Mustache.render(tpl, dataInfo);
        };
		
        return html;
    },
    timeDown:function(){
		
		var domlist = $('[time-date]'),endtime,second,
			nowtime = new Date().getTime();
		
		var _h,_m,_s;
		
		domlist.each(function(idx,dom){
			
			endtime = $(dom).attr('time-date');
			
			endtime = endtime.replace(/-/g,'/');
			
			endtime = new Date(endtime).getTime();
			
			second = parseInt((endtime - nowtime)/1000);
			
			_h = Math.floor(second/3600);
			
			_m = Math.floor((second%3600)/60);
			
			_s = Math.floor(((second%3600)%60));
			
			//console.log(_h+' '+_m+' '+_s);
			
			$(dom).find('[tn=time-h-max]').html( Math.floor(_h/10)>9?9:Math.floor(_h/10));
			
			$(dom).find('[tn=time-h-min]').html( Math.floor(_h%10) );
			
			$(dom).find('[tn=time-m-max]').html( Math.floor(_m/10) );
			
			$(dom).find('[tn=time-m-min]').html( Math.floor(_m%10) );
			
			$(dom).find('[tn=time-s-max]').html( Math.floor(_s/10) );
			
			$(dom).find('[tn=time-s-min]').html( Math.floor(_s%10) );
		});
	}
});