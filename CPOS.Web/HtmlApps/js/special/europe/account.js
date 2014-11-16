Jit.AM.defindPage({

    name: 'Account',
    elements: {
        txtName: '',
        txtInterest: '',
        imgHead: '',
        btAccountAuth: '',
        txtAccountAuth: ''
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initLoad();
        this.initEvent();

    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.txtName = $('#txtName');
        self.elements.txtInterest = $('#txtInterest');
        self.elements.imgHead = $('#imgHead');
        self.elements.vipStatus = $('#vipStatus');

        UIBase.loading.show();

                // var    baseInfo = Jit.AM.getBaseAjaxParam();

                // alert(baseInfo.userId);

        if (!Validates.isLogin()) {
            self.toPage('UserBind')
        } else {
            UIBase.loading.hide();
        }



        self.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'getUserInfo'
            },
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
                UIBase.loading.hide();
                if (data && data.code == 200 && data.content.vipList) {
                    var dataInfo = data.content.vipList[0];
                    self.elements.txtName.html(dataInfo.vipName);
                    self.elements.txtInterest.html(dataInfo.hobby);
                    if (dataInfo.headImg) {
                        self.elements.imgHead.attr('src', dataInfo.headImg);
                    };
                    self.elements.vipStatus.html(dataInfo.vipStatus);
            
                }
            }
        });


    },

    //绑定事件
    initEvent: function() {
        var self = this;

        // self.elements.txtName.bind('tap', function() {
        //     Jit.AM.toPage(self.configParam.toUserPage);
        // });



    }

});