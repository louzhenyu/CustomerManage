Jit.AM.defindPage({

    name: 'Account',
    elements: {
        txtName: '',
        txtInterest: '',
        imgHead: '',
        btAccountAuth: '',
        txtAccountAuth: ''
    },
    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initLoad();
        this.initEvent();

    }, //加载数据
    initLoad: function () {
        var self = this;
        self.elements.txtName = $('#txtName');
        self.elements.txtInterest = $('#txtInterest');
        self.elements.imgHead = $('#imgHead');
        self.elements.vipStatus = $('#vipStatus');
        self.elements.btAccountAuth = $('#btAccountAuth');

        UIBase.loading.show();


        if (!Validates.isLogin()) {
            self.toPage('NewLogin')
        } else {
            UIBase.loading.hide();
        }

        // vipInfo.status=   11、15：未提交认证 12：已提交认证


        self.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'getUserInfo'
            },
            beforeSend: function () {
                UIBase.loading.show();
            },
            success: function (data) {
                UIBase.loading.hide();
                if (data && data.code == 200 && data.content.vipList) {
                    var dataInfo = data.content.vipList[0];
                    self.elements.txtName.html(dataInfo.vipName);
                    self.elements.txtInterest.html(dataInfo.hobby);
                    if (dataInfo.headImg) {
                        self.elements.imgHead.attr('src', dataInfo.headImg);
                    };

                    switch (dataInfo.optionTextEn) {
                        case "Unapprove":
                            Jit.UI.Dialog({
                                'content': "您当前还未认证，前往认证?",
                                'LabelCancel': '取消',
                                'type': 'Confirm',
                                'CallBackOk': function () {
                                    self.toPage('NewAddUserInfo', 'pageIndex=' + dataInfo.pageIndex);
                                },
                                'CallBackCancel': function () {
                                    Jit.UI.Dialog('CLOSE');

                                }
                            });
                            self.elements.btAccountAuth.attr('href', "javascript:Jit.AM.toPage('NewAddUserInfo','pageIndex=" + dataInfo.pageIndex + "')");
                            break;
                        case "Approve":
                            
                            break;
                        case "ApproveSuccess":
           
                            break;
                        case "ApproveFail":
                            Jit.UI.Dialog({
                                'content': dataInfo.notApproveReson,
                                'LabelCancel': '取消',
                                'type': 'Confirm',
                                'CallBackOk': function () {
                                    self.toPage('NewAddUserInfo', 'pageIndex=' + dataInfo.pageIndex);
                                },
                                'CallBackCancel': function () {
                                    Jit.UI.Dialog('CLOSE');

                                }
                            });
                            self.elements.btAccountAuth.attr('href', "javascript:Jit.AM.toPage('NewAddUserInfo','pageIndex=" + dataInfo.pageIndex + "')");
                            break;
                    }

                    self.elements.vipStatus.html(dataInfo.statusText || '未认证');

                }
            }
        });


    },

    //绑定事件
    initEvent: function () {
        var self = this;
        self.elements.txtName.bind('tap', function () {
            Jit.AM.toPage('NewAddUserInfo', '&pageIndex=1');
        });

    }

});