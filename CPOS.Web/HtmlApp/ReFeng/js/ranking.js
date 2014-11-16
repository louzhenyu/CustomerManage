Jit.AM.defindPage({

	name:'GoodsList',
	
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入Ranking.....');
		
		this.initEvent();
	},
	
	initEvent:function(){
		
		var me = this;
		
		/*
		(function(){
			var win = {
				sH : window.innerHeight,
				sW : window.innerWidth
			}
			
			$(window).resize(function(){
			
				if((win.sH-window.innerHeight > 100) && (win.sW == window.innerWidth)){
					
					//$('#nav').hide();
				}else{
				
					//$('#nav').show();
				}
			});
		})()
		*/
		me.windowHeight = window.innerHeight;
		
		me.windowWidth = window.innerWidth;
		
		
		me.ajax({
			url:'../../../Lj/Interface/Data.aspx',
			data:{
				'action':'getRecommend'
			},
			success:function(data){
				
				me.loadPageData(data.content);
			}
		});
	},
	loadPageData:function(data){
		
		var itemlists = data.recommendList;
		
		var tpl = $('#Tpl_ranking_list').html(),html = '';
		
		for(var i=0;i<itemlists.length;i++){
			
			var hashtpl = tpl;
			
			var itemdata = itemlists[i];
			
			
				//itemdata.parValue =parseInt(itemdata.recommendCount)*100;
				
		
			
			html += Mustache.render(tpl,itemdata);
		}
		
		$(html).insertAfter($("#list"))
	},
});