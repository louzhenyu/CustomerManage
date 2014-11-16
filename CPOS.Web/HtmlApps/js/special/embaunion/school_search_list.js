Jit.AM.defindPage({

    name: 'SchoolSearchList',
    elements: {
        txtName: '',
        btSubmit: '',
        dataList: ''
    },
    urlParams: [],
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;

        self.elements.txtName = $('#txtName');
        self.elements.btSubmit = $('#btSubmit');
        self.elements.dataList = $('#dataList');
      FootNavHide(true);
        urlParams = {
            action: 'getUserList',
            vipName: self.getUrlParam('name'),
            className: self.getUrlParam('classes'),
            company: self.getUrlParam('company'),
            addr: self.getUrlParam('address')
        };

        self.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: urlParams,
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
                self.elements.dataList.empty();
                UIBase.loading.hide();
                if (data && data.code == 200 && data.content.vipList&&data.searchCount<=5) {
                    self.elements.dataList.html(GetAdListHtml(data.content.vipList));
                }else if (data.searchCount>5) {
                  self.elements.dataList.html('<p>很抱歉您已经超过查询次数。</p>');

                } else {

                    self.elements.dataList.html('<p>很抱歉没有查找到您的校友信息。</p>');
                }
            }
        });

    }, //绑定事件
    initEvent: function() {
        var self = this;
        //提交查询
        self.elements.btSubmit.bind('tap', function() {
            var txtName = self.elements.txtName.val();

            if (!txtName) {
                return Jit.UI.Dialog({
                    'content': '请输入您要查询用户的名称',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            };
            urlParams.vipName = txtName;
            urlParams.addr = '';
            urlParams.company = '';
            urlParams.className = '';


            self.ajax({
                url: '/Project/CEIBS/CEIBSHandler.ashx',
                data: urlParams,
                beforeSend: function() {
                    UIBase.loading.show();
                },
                success: function(data) {
                    self.elements.dataList.empty();
                    UIBase.loading.hide();
                    if (data && data.code == 200 && data.content.vipList) {
                        self.elements.dataList.html(GetAdListHtml(data.content.vipList));
                    } else {
                        self.elements.dataList.html('<p>很抱歉没有查找到您的校友信息。</p>');

                    }
                }
            });

        });
    }

});


function GetAdListHtml(datas) {
    var listHtml = new StringBuilder();
    for (var i = 0; i < datas.length; i++) {
        var dataInfo = datas[i];
        listHtml.append("<li>");
        listHtml.append("<a href=\"javascript:;\" class=\"clearfix\">");
        listHtml.appendFormat("<img class=\"headPic\" src=\"{0}\" />", dataInfo.headPic ? dataInfo.headPic : '../../../images/special/europe/headPic.png');
        listHtml.append("<figure class=\"info\">");
        listHtml.appendFormat("<span>{0}</span>", dataInfo.vipName);
        listHtml.appendFormat("<span>{0}</span>", dataInfo.className||'');
        listHtml.appendFormat("<span>{0}</span>", dataInfo.position||'');
        listHtml.append("</figure>");
        listHtml.append("</a>");
        listHtml.append("</li>");
    };


    return listHtml.toString();
}