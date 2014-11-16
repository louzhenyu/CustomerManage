Jit.AM.defindPage({

    name: 'IndexShop',
	
	initWithParam: function(param){
	
        
	},
	
    onPageLoad: function () {
	
		this.getHotTag();
    },

    getHotTag:function(){

    	var me = this;
    	
    	Jit.UI.Loading(true);

    	me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'Knowledge.Tag.GetHotTags',
				'page':0,
				'pageSize':99
            },
            success: function (data) {
				
				Jit.UI.Loading(false);

                if(data.ResultCode==0){

                    me.buildHotTags(data.Data.Tags);
                }
            }
        });
    },
    buildHotTags:function(datalist){

    	var htmls = [];

    	for(var i in datalist){

    		htmls.push('<a href="javascript:JitPage.searchWithText(\''+datalist[i]['Name']+'\')">'+datalist[i]['Name']+'</a>');
    	}
        
        $('.hotWord').html(htmls.join(''));
    },
    search:function(){

    	var text = Jit.trim($('.searchInput').val());

    	if(!text){

    		Jit.UI.Dialog({
                'content': '请输入需要搜索的内容！',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });

            return;
    	}

    	this.searchWithText(text);
    },
    searchWithText:function(text){

    	var me = this;
    	
    	Jit.UI.Loading(false);

    	me.toPage('RepositoryList','Key='+text);
    }
});