Jit.AM.defindPage({

    name: 'RepositoryList',
	
	initWithParam: function(param){
	   
        var me = this;

        if(param.displayHotNews == 'true'){

            me.displayHotNews = true;

        }else{

            me.displayHotNews = false;

            $('#hottab').hide();
        }
	},
	
    onPageLoad: function () {
	   
        var me = this;

        me.searchType = '';

        me.pageIndex = 1;

        me.pageSize = 9;

        me.hasNextPage = true;

        var newstype = me.getUrlParam('newsType');
        //渠道编码
        var ChannelCode=me.getUrlParam('ChannelCode');
        this.ChannelCode=ChannelCode;
        if(!newstype){

            me.getClassify();

        }else{

            $('#classScroll').hide();

            me.selectClassify(newstype,null);
        }

        if(me.displayHotNews){

            me.getHotNews();

        }

        me.initEvent();
    },
    initEvent:function(){

        var self = this;

        $(window).scroll(function(){
        　　var scrollTop = $(this).scrollTop();
        　　var scrollHeight = $(document).height();
        　　var windowHeight = $(this).height();
        　　if(scrollTop + windowHeight == scrollHeight){

        　　　　if(self.hasNextPage){
                    
                    self.getNextPage();
                }
        　　}
        });
    },
    getHotNews:function(isNextPage){

        var me = this;

        me.tpl = 'Tpl_news_hot_item';

        $('.navWrap a').removeClass('on');

        $($('.navWrap a').get(0)).addClass('on');

        Jit.UI.Loading(true);

        me.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'GetEventStats',
                'PageSize':me.pageSize,
                'PageIndex':me.pageIndex
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.code==200){

                    me.insertList(data.content.ItemList);

                    if(data.content.ItemList && data.content.ItemList.length<me.pageSize){

                        me.hasNextPage = false;
                    }
                }
            }
        });
    },
    searchText:function(isNextPage){

        var me = this;

        me.tpl = 'Tpl_news_item';

        Jit.UI.Loading(true);

        me.ajax({
            url: '/DynamicInterface/data/Data.aspx',
            data: {
                'action': 'getNewsList',
                'newsType': me.searchType,
                'pageSize':me.pageSize,
                'page':me.pageIndex
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.code==200){

                    me.insertList(data.content.ItemList);

                    if(data.content.ItemList && data.content.ItemList.length<me.pageSize){

                        me.hasNextPage = false;
                    }
                }
            }
        });
    },
    getNextPage:function(){

        var me = this;

        if(!me.hasNextPage){

            return;
        }

        me.pageIndex++;

        me.searchText(true);
    },
    selectClassify:function(typeId,target){

        var me = this;

        $('.navWrap a').removeClass('on');

        if(target){

            $(target).addClass('on');
        }
        
        me.searchType = typeId;

        me.pageIndex = 1;

        $('#newslist').html('');

        me.searchText();
    },
    getClassify:function(){

        var me = this;

        Jit.UI.Loading(true);

        me.ajax({
            url: '/DynamicInterface/data/Data.aspx',
            data: {
                'action': 'getNewsType',
                'ChannelCode':me.ChannelCode

            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.code==200){

                    me.buildClassify(data.content);

                    me.initClassifyScroll();
                }
            }
        });
    },
    buildClassify:function(datalist){

        var htmls = [];

        for(var i in datalist){
            
            htmls.push('<a cidx="'+i+'" onclick="JitPage.selectClassify(\''+datalist[i]['NewsTypeId']+'\',this)"><span>'+datalist[i]['NewsTypeName']+'</span></a>');
        }
        
        $('.navWrap').append(htmls.join(''));
        
        $('a[cidx=0]').click();
    },
    initClassifyScroll:function(){

    	var me = this,
            li = $('.navWrap a');
    	
        var scrollwidth = 0;

        $('.navWrap a').each(function(idx,dom){

            scrollwidth += $(dom).innerWidth();
        });

        $('.navWrap').css({

            'width':scrollwidth+'px'
        });

        me.ImgScroll = new iScroll('classScroll', {
            snap: true,
            momentum: false,
            vScroll:false,
            hScrollbar: false,
            vScrollbar: false
        });

    },
    
    insertList:function(datalist){

        var me = this;

        var pagehtml = me.buildResultList(datalist);

        $('#newslist').append(pagehtml);
    },
    buildResultList:function(datalist,nullalert){

        var me = this;
        
        if(!datalist){

            return;
        }

        if(nullalert && (datalist.length==0 || datalist == null)){

            return '<li><a class="t-overflow"> 暂时没有内容！</a></li>';
        }
        
        var tpl = $('#'+me.tpl).html(), html = '';

        for (var i = 0; i < datalist.length; i++) {

            var itemdata = datalist[i];
            
            html += Mustache.render(tpl, itemdata);
        }

        return html;
    }
});