Jit.AM.defindPage({
    name: 'UserEdit',
    onPageLoad: function() {
        CheckSign();
        //当页面加载完成时触发

        this.initEvent();

        this.initData();
    },
    initData: function() {


        //初次加载用户信息
        var datas = {
            keyword: '',
            page: '1',
            pageSize: '30',
            vipId: this.getBaseInfo().userId
        }, me = this;
        var elements = {
            UserName: $('#username'),
            Phone: $('#phone'),
            Position: $('#position'),
            Email: $('#email'),
            editBottom: $('#edit'),
            Company: $('#company'),
            oldPhone: ''
        };
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
            data: datas,
            success: function(data) {
                if (data.code == 200 && data.content.vipList) {
                    SetData(data.content.vipList[0]);
                }
            }
        });


        //设置信息
        function SetData(userInfo) {
            elements.UserName.html(userInfo.staffName);
            elements.Phone.val(userInfo.phone);
            elements.Position.val(userInfo.staffPost);
            elements.Email.val(userInfo.email);
            elements.Company.val(userInfo.staffCompany);
            elements.oldPhone = userInfo.phone;
        }
        //提交编辑信息
        elements.editBottom.bind('click', function() {
            var params = {
                staffName: elements.UserName.html(),
                phone: elements.oldPhone,
                staffPost: $.trim(elements.Position.val()),
                email: $.trim(elements.Email.val()),
                profile: '',
                seats: '',
                staffCompany: $.trim(elements.Company.val()),
                phoneNew: $.trim(elements.Phone.val())
            };
            var regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/,
                regEmail = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

            if (params.phoneNew && !regPhone.test(params.phoneNew)) {
                Tips({
                    msg: "您输入的手机号码不正确",
                    fs: elements.phoneNew
                });
                return false;
            } else if (params.email && !regEmail.test(params.email)) {
                Tips({
                    msg: "您输入的邮箱地址不正确",
                    fs: elements.Email
                });
                return false;

            };


            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=SetVipStaff',
                data: params,
                success: function(data) {
                    if (data.code == 200) {

                        Tips({
                            msg: data.description,
                            fn: function() {
                                me.toPage('MyDetail');
                            }
                        });

                    } else {
                        Tips({
                            msg: data.description
                        });
                    }
                }
            });

        });



    },
    initEvent: function() {



    }
});