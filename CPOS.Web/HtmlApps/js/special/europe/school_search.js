Jit.AM.defindPage({

    name: 'SchoolSearch',
    elements: {
        txtName: '',
        txtClasses: '',
        txtCompany: '',
        btSubmit: '',
        txtAddress: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
      FootNavHide(true);
        self.elements.txtName = $('#txtName');
        self.elements.txtClasses = $('#txtClasses');
        self.elements.txtCompany = $('#txtCompany');
        self.elements.btSubmit = $('#btSubmit');
        self.elements.txtAddress = $('#txtAddress');

    }, //绑定事件
    initEvent: function() {
        var self = this;
        //提交查询
        self.elements.btSubmit.bind('click', function() {
            var txtName = self.elements.txtName.val(),
                txtClasses = self.elements.txtClasses.val(),
                txtCompany = self.elements.txtCompany.val(),
                txtAddress = self.elements.txtAddress.val();

            if (!txtName) {
                return Jit.UI.Dialog({
                    'content': '请输入您要查询用户的名称',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            };

            UIBase.loading.show();

            self.toPage('SchoolSearchList','&name='+txtName+'&classes='+txtClasses+'&company='+txtCompany+'&address='+txtAddress);


            UIBase.loading.hide();



        });



    }

});