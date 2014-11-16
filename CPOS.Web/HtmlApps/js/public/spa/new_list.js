Jit.AM.defindPage({

    name: 'NewList',
    elements: {
        homeAdList: '',
        adItemTitle: '',
        newList: '',
        scrollList: '',
        scrolItems: '',
        scrollMenu: '',
        scrollTitle: ''
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
		//资讯类型对应ID
		var typeId=this.getUrlParam("typeId");
		var title=this.getUrlParam("title");
		$("title").html(decodeURIComponent(title));
        self.elements.homeAdList = $('#newAdList');
        self.elements.adItemTitle = $('#adItemTitle');
        self.elements.newList = $('#newList ul');
        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getNewsList',
                "page":1,
                "pageSize":1000,
                'NewsType':typeId
            },
            beforeSend: function() {
                self.elements.newList.html(' 数据正在加载，请稍后...');

            },
            success: function(data) {
                self.elements.newList.empty();
                if (data && data.code == 200&&data.content.ItemList) {
                    self.elements.newList.html(GetNewListHtml(data.content.ItemList));
                }
            }
        });

        self.elements.scrollList = $('#newAdList');
        self.elements.scrolItems = self.elements.scrollList.find('.picList');
        self.elements.scrollMenu = self.elements.scrollList.find('.dot');
        self.elements.scrollTitle = self.elements.scrollList.find('#scrollTitle');
	;
        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getTopList',
                "page": 1,
                "pageSize": 5,
                'NewsType': 0,
                'Type':1
            },
            success: function(data) {
                if (data && data.code == 200 && data.content.ItemList) {
                    self.elements.scrolItems.html(GetAdListHtml(data.content.ItemList));
                    self.elements.scrollMenu.html(GetAdMenuHtml(data.content.ItemList));
                    self.elements.scrollTitle.html(data.content.ItemList[0].NewsTitle);
                           self.ScrollEvent();
                } else {
                    self.elements.scrollList.hide();
                }
            }
        });









    }, //绑定事件
    initEvent: function() {
        var self = this;


      
    },ScrollEvent: function() {
         self=this;
        // 绑定滚动事件
        var menuList = $('.asideShow .dot em', self.elements.homeAdList);

        //重新設置大小
        ReSize();

        function ReSize() {
            self.elements.homeAdList.find('.commonBanner').css({
                width: (self.elements.homeAdList.width()) * menuList.size()
            });

            self.elements.homeAdList.find('.picList li').css({
                width: (self.elements.homeAdList.width())
            });
        }
        myScroll = new iScroll('newAdList', {
            snap: true,
            momentum: false,
            hScrollbar: false,
            vScroll: false,
            onScrollEnd: function() {
                if (this.currPageX > (menuList.size() - 1)) {
                    return false;
                };
                menuList.removeClass('on');
                menuList.eq(this.currPageX).addClass('on');
                self.elements.scrollTitle.html(menuList.eq(this.currPageX).data(KeyList.val));
            }
        });
        // menuList.bind('click', function() {
        //     myScroll.scrollToPage(menuList.index(this));
        // });
        $(window).resize(function() {
            ReSize();
            myScroll.refresh();
        });


    }

});
function GetAdListHtml(datas) {
    var adListHtml = new StringBuilder(),
        toPageName = '';
    for (var i = 0; i < datas.length; i++) {
        var dataInfo = datas[i];

        switch (parseInt(dataInfo.Type)) {
            case 1:
                toPageName = "NewDetail";
                break;
            case 2:
                toPageName = "ActDetail";
                break;
            case 3:
                toPageName = "Home";
                break;
        }
        adListHtml.appendFormat("<li ><a href=\"javascript:Jit.AM.toPage('{0}','&newsId={1}');\"><img src=\"{2}\" height=\"164\" ></a></li>", toPageName, dataInfo.NewsId, dataInfo.ImageUrl);
    };


    return adListHtml.toString();
}

function GetAdMenuHtml(datas) {
    var adListHtml = new StringBuilder();
    for (var i = 0; i < datas.length; i++) {
        var dataInfo = datas[i];
        adListHtml.appendFormat("<em data-val=\"{0}\" class=\"{1}\"></em>", dataInfo.NewsTitle, i == 0 ? 'on' : '');
    };

    return adListHtml.toString();
}




function GetNewListHtml(datas) {
    var htmlList = new StringBuilder();


    for (var i = 0; i < datas.length; i++) {
        var newInfo = datas[i];
        htmlList.append("<li>");
        htmlList.appendFormat("<a href=\"javascript:Jit.AM.toPage('NewDetail','&newsId={0}');\">", newInfo.NewsId);
        htmlList.appendFormat(" <img class=\"pic\" src=\"{0}\" >", newInfo.ImageUrl);
        htmlList.append(" <figure class=\"info\">");
        htmlList.appendFormat("<p class=\"text\">{0}</p>", newInfo.NewsTitle);
        htmlList.appendFormat("<span class=\"date\">{0}</span>", newInfo.PublishTime);
        htmlList.append("</figure>");
        htmlList.append("</a>");
        htmlList.append("</li>");
    };

    return htmlList.toString();
}
//拼接字符串，该方法效率要高于str+="str";
function StringBuilder() {
  this.strList = [];
  this.append = function(v) {
    if (v) {
      this.strList.push(v);
    };
  };
  this.appendFormat = function(v) {
    if (v) {
      if (arguments.length > 1) {
        for (var i = 1; i < arguments.length; i++) {
          v = v.replace("{" + (i - 1) + "}", arguments[i]);
        };
      }
      this.strList.push(v);
    };
  };
  this.toString = function() {
    return this.strList.join('');
  };
}