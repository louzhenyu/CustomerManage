Jit.AM.defindPage({
    name: 'UserEdit',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发

        this.initEvent();

        this.initData();
    },
    initData: function () {


        //初次加载用户信息
        var datas = { keyword: '', page: '1', pageSize: '30', vipId: this.getBaseInfo().userId }, me = this;
        var elements = { UserName: $('#username'), Phone: $('#phone'), Position: $('#position'), Email: $('#email'),
            editBottom: $('#edit'), Company: $('#company'), oldPhone: '', School: $('#school'), Class: $('#class'), 
            Hobby: $('#hobby'), MyValue: $('#myValue'), NeedValue: $('#needValue')
        };
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=getVipDetail',
            data: datas,
            success: function (data) {
                if (data.code == 200 && data.content) {
                    SetData(data.content);
                }
            }
        });


        //设置信息
        function SetData(userInfo) {
			console.log(elements);
            elements.UserName.html(userInfo.vipRealName);
            elements.Phone.val(userInfo.phone);         
            elements.Position.val(userInfo.position);  
            elements.Email.val(userInfo.email);
            elements.Company.val(userInfo.company);  
            //elements.DcodeImageUrl.attr('src', userInfo.DCodeImageUrl);
            elements.School.val(userInfo.school);  
            elements.Class.val(userInfo.className);  
            elements.Hobby.val(userInfo.hobby);  
            elements.MyValue.html(userInfo.myValue); 
            elements.NeedValue.html(userInfo.needValue);
        }
        //提交编辑信息
        elements.editBottom.bind('click', function () {
            var params = { 'vipRealName': elements.UserName.html(), 'position': $.trim(elements.Position.val()),
                'email': $.trim(elements.Email.val()), 'profile': '', 'seats': '', 
                'company': $.trim(elements.Company.val()),'phone':$.trim(elements.Phone.val()),
                'school': $.trim(elements.School.val()), 'className': $.trim(elements.Class.val()),
                'hobby': $.trim(elements.Hobby.val()), 'myValue': $.trim(elements.MyValue.val()),
                'needValue': $.trim(elements.NeedValue.val())};
           var regPhone= /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/,regEmail=/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

            if (params.phoneNew&&!regPhone.test(params.phoneNew)) {
                Tips({msg:"您输入的手机号码不正确",fs:elements.phoneNew});
             return false;
            }else if (params.email&&!regEmail.test(params.email)) {
                Tips({msg:"您输入的邮箱地址不正确",fs:elements.Email});
            return false;

            };


            me.ajax({
                url: '/onlineshopping/data/emba.aspx?Action=updateVip',
                data: params,
                success: function (data) {
                    if (data.code == 200) {
                        Tips({ msg: data.description });
                    }
                }
            });

        });




    },
    initEvent: function () {



    }
});