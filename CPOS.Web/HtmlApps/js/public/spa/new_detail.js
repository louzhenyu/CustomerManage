Jit.AM.defindPage({

    name: 'NewDetail',
    elements: {
        detailArea: '',
        detailTitle: '',
        daDetilTitle: '',
        deTime: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
		
        this.initLoad();
    }, //加载数据
    loadData:function(newsId){
    	var self=this;
    	this.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getNewsDetailByNewsID',
                'newsId': newsId
            },
            beforeSend: function() {
                self.elements.detailArea.html(' 数据正在加载，请稍后...');

            },
            success: function(data) {
                self.elements.detailArea.html('');
                if (data && data.code == 200) {
                    self.elements.detailTitle.append(data.content.News.NewsTitle.length >= 10 ? data.content.News.NewsTitle.substr(0, 10) : data.content.News.NewsTitle);
                    self.elements.daDetilTitle.append(data.content.News.NewsTitle);
                    //self.elements.deTime.append(data.content.News.PublishTime);
                    self.elements.detailArea.append(data.content.News.Content);
                }
            }
        });

    },
    initLoad: function() {
        var self = this,
         	newsId = self.getUrlParam('newsId'),
         	typeId=self.getUrlParam("typeId");  //如果直接过来的typeId 需要先获取一次列表数据
        self.elements.detailArea = $('#detailArea');
        self.elements.detailTitle = $('#detailTitle');
        self.elements.daDetilTitle = $('#daDetilTitle');
        //self.elements.deTime = $('#deTime');
        if(typeId){
        	self.ajax({
	            url: '/dynamicinterface/data/data.aspx',
	            data: {
	                'action': 'getNewsList',
	                "page":1,
	                "pageSize":1000,
	                'NewsType':typeId
	            },
	            success: function(data) {
	                if (data && data.code == 200&&data.content.ItemList) {
	                    if(data.content&&data.content.ItemList){
	                    	self.loadData(data.content.ItemList[0].NewsId)
	                    }
	                }
	            }
	        });
        }else{
        	self.loadData(newsId);
        }
        
    }, //绑定事件
    initEvent: function() {
    }

});