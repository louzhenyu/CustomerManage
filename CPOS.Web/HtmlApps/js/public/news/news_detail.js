Jit.AM.defindPage({

    name: 'RepositoryList',
	
	initWithParam: function(param){
	   
	},
	
    onPageLoad: function () {
	   
	   	var me = this;

	   	me.itemId = me.getUrlParam('itemId');

        Jit.UI.Loading(true);
        var pageParam=this.pageParam;
        me.ajax({
            url: '/Lj/data/Data.aspx',
            data: {
                'action': 'getNewsById',
                'newsId':me.itemId
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                var info = data.content;

                me.info = info;

                if(data.code == '200'){
                    if(pageParam.showCover=="1"){
                        $('.banner').attr('src',info.imageList[0].imageURL);
                    }
                    $('.articleWrap').html(info.description);

                    //$('.good').html(info.PraiseCount);

                    //$('.bad').html(info.TreadCount);

                    //$('.collect').html('收藏 '+info.KeepCount);
                }
            }
        });

        //浏览记录
        me.logToServer(1);
    },

    logToServer:function(type){

        var me = this;

        Jit.UI.Loading(true);

        me.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'AddEventStats',
                'id':me.itemId,
                'countType':type,
                'newsType':1,
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                var info = data.content;

                me.info = info;

                if(data.code == '200'){

                    if(type == 2 || type == 3){

                        Jit.UI.Dialog({
                            'content': '操作成功！',
                            'type': 'Alert',
                            'CallBackOk': function() {

                                Jit.UI.Dialog('CLOSE');
                            }
                        });
                    }
                }
            }
        });
    },

    comment:function(gd){

    	var me = this;

    	if(me.hasComment){

    		Jit.UI.Dialog({
                'content': '您已经评价过！不能重复评价！',
                'type': 'Alert',
                'CallBackOk': function() {

                    Jit.UI.Dialog('CLOSE');
                }
            });

    		return ;
    	}

        //浏览记录
        me.logToServer(2);

        me.hasComment = true;
    },
    collect : function(){

    	var me = this;
    	
    	me.logToServer(3);
    }
    
});