(function(win, undefined) {
	var news = {};

	news.getNewsList = function(param,callback) {
		var self = this;
		Jit.AM.ajax({
			url : '/dynamicinterface/data/data.aspx',
			data : {
				'action' : 'getNewsList',
				"page" : param.page?param.page:1,
				"pageSize" :  param.pageSize?param.pageSize:4,
				"NewsType" : param.NewsType
			},
			success : function(data) {
				if (data.code == 200) {
					if (callback) {
						callback(data.content.ItemList);
					}
				} else {
					self.alert(data.description);
				}
			},
			error:function(e){
				alert(JSON.stringify(e));
			}
		});
	};
	news.getNewsDetailByNewsID = function(param, callback) {
		var self = this;
		Jit.AM.ajax({
			url : '/dynamicinterface/data/data.aspx',
			data : {
				'action' : 'getNewsDetailByNewsID',
				'newsId' : param.newsId
			},
			success : function(data) {
				if (data.code == 200) {
					if (callback) {
						callback(data.content);
					}
				} else {
					self.alert(data.description);
				}
			},
			error:function(e){
				alert(JSON.stringify(e));
			}
		});
	};

	win.news = news;
})($); 