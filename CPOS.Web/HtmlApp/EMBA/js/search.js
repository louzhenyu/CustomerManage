Jit.AM.defindPage({
    name: 'MyInfoPage',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发
        Jit.log('进入MyInfoPage.....');

        this.initData();
    },
    initData: function () {
		
		var me = this;
		
		me.initEvent();
		
		me.getData();
    },
	
	getData:function(){
	
		var me = this;
		
		me.ajax({
			url: '/OnlineShopping/data/Data.aspx?Action=getEmbaVipList',
			data: {
				'keyword':$('#searchKey').val(),
				'page':1,
				'pageSize':999
			},
			success: function(data) {
				/*
				Tips({
					msg: data.description
				});
				*/
				if(data.code == 200){
					
					if(data.content.vipList){
						
						me.refreshPanel(data.content.vipList);
					}
					
					me.Scroll.refresh();
				}
			},
			error: function() {
				Tips({
					msg: '服务器繁忙，请稍后.'
				});
			}
		});
	},
	refreshPanel:function(list){
		
		$('#list_panel').html('');
		
		var tpl = $('#Tpl_user_item').html(),html = '';
		
		for(var i=0;i<list.length;i++){
			
			if(!list[i].imageUrl){
			
				list[i].imageUrl = '../images/default_face.png';
			}
			
			var hashtpl = tpl;
			
			html += Mustache.render(tpl,list[i]);
		}
		
		$('#list_panel').html(html);
	},
    initEvent: function () {
	
		var me = this;
		
		me.Scroll = new iScroll('search_list', {
			useTransform: true,
			onBeforeScrollStart: function (e) {
				var target = e.target;
				while (target.nodeType != 1) target = target.parentNode;

				if (target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'TEXTAREA')
					e.preventDefault();
			}
		});
		
		document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
    }
});