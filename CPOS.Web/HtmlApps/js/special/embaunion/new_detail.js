Jit.AM.defindPage({

    name: 'NewDetail',
    curNewsId: '',
    eventStatsID:'',
    newsType:'',
    elements: {
        detailArea: '',
        detailTitle: '',
        daDetilTitle: '',
        deTime: '',
        txtAuthor: '',
        txtBroCount: '',
        btFavorite: '',
        btRelay: '',
        txtPraise: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.curNewsId = self.getUrlParam('newsId');
        self.elements.detailArea = $('#detailArea');
        self.elements.detailTitle = $('#detailTitle');
        self.elements.daDetilTitle = $('#daDetilTitle');
        self.elements.deTime = $('#deTime');
        self.elements.txtAuthor = $('#txtAuthor');
        self.elements.txtBroCount = $('#txtBroCount');
        self.elements.btFavorite = $('#btFavorite');
        self.elements.btRelay = $('#btRelay');
        self.elements.txtPraise = $('#txtPraise');

        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getNewsDetailByNewsID',
                'newsId': self.curNewsId
            },
            beforeSend: function() {
                UIBase.loading.show();

            },
            success: function(data) {
                UIBase.loading.hide();
                if (data && data.code == 200 && data.content.News) {
                    var newsInfo = data.content.News;
                    self.elements.detailTitle.html(newsInfo.NewsTitle.length >= 10 ? newsInfo.NewsTitle.substr(0, 10) : newsInfo.NewsTitle);
                    self.elements.daDetilTitle.html(newsInfo.NewsTitle);
                    self.elements.deTime.html(newsInfo.PublishTime);
                    self.elements.detailArea.html(newsInfo.Content);
                    self.elements.txtAuthor.html(newsInfo.Author);
                    self.elements.txtBroCount.html(newsInfo.BrowseCount || 0);
                    self.elements.txtPraise.html(newsInfo.PraiseCount || 0);
                    self.newsType=newsInfo.NewsType;
                    // self.eventStatsID=newsInfo.eventStatsID;
                    if (newsInfo.NewsCountID) {
                        self.elements.btFavorite.addClass('on').html('已收藏');
                    };


                }
            }
        });

    }, //绑定事件
    initEvent: function() {
        //点击收藏事件
        var self = this;


        //CountType    浏览为1，赞为2,收藏为3
        // NewsType    1为咨询，2为其他未定
        //点击收藏
        self.elements.btFavorite.bind('click', function() {
            if (self.elements.btFavorite.hasClass('on')) {
                return false;
            };

            self.ajax({
                url: '/Project/CEIBS/CEIBSHandler.ashx',
                data: {
                    'action': 'AddEventStats',
                    'newsId': self.newsId,
                    'CountType': '3',
                    'NewsType': '1'
                },
                success: function(data) {
                    if (data && data.code == 200) {
                        self.elements.btFavorite.addClass('on').html('已收藏');
                    }
                }
            });
        });

        self.elements.txtPraise.bind('click', function() {
            if (self.elements.txtPraise.hasClass('on')) {
                return false;
            };

            self.ajax({
                url: '/Project/CEIBS/CEIBSHandler.ashx',
                data: {
                    'action': 'AddEventStats',
                    'newsId': self.newsId,
                    'CountType': '2',
                    'NewsType': '1'
                },
                success: function(data) {
                    if (data && data.code == 200) {
                        var curPraiseCount = parseInt(self.elements.txtPraise.html());
                        self.elements.txtPraise.addClass('on')
                        self.elements.txtPraise.html(curPraiseCount += 1);
                        self.elements.txtPraise.parents('li').addClass('on')
                    }
                }
            });
        });



        self.elements.btRelay.bind('click', function() {
            $('#share-mask').show();
            $('#share-mask-img').show().attr('class', 'pullDownState');
        });
        $('#share-mask').bind('click', function() {
            var that = $(this);
            $('#share-mask-img').attr('class', 'pullUpState').show();
            setTimeout(function() {
                $('#share-mask-img').hide(500);
                that.hide(1000);
            }, 500);
        })



    }

});