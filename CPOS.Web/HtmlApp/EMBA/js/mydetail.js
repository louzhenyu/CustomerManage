Jit.AM.defindPage({
    name: 'MyDetail',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发
        Jit.log('进入MyInfoPage.....');

        this.initEvent();

        this.initData();
    },
    initData: function () {


        //初次加载用户信息,DcodeImageUrl:$('#dcodeimageurl')
        var datas = { keyword: '', page: '1', pageSize: '30' };
        var elements = { UserHead: $('#userhead'), UserName: $('#username'), Phone: $('#phone'), Position: $('#position'), Email: $('#email'), SayName: $('#sayname'), Company: $('#company'), School: $('#school'), Class: $('#class'), Hobby: $('#hobby'), MyValue: $('#myValue'), NeedValue: $('#needValue') };
        this.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=getVipDetail',
            data: datas,
            success: function (data) {
                if (data.code == 200 && data.content) {
                    SetData(data.content);
                }
            }
        });


        //模拟数据
        function SetData(userInfo) {
            elements.UserHead.attr('src', userInfo.qRVipCode);
            elements.UserName.html(userInfo.vipRealName);
            elements.Phone.html(userInfo.phone);         
            elements.Position.html(userInfo.position);
            elements.Email.html(userInfo.email);
            elements.SayName.html(userInfo.staffName);
            elements.Company.html(userInfo.company);
            //elements.DcodeImageUrl.attr('src', userInfo.DCodeImageUrl);
            elements.School.html(userInfo.school);
            elements.Class.html(userInfo.className);
            elements.Hobby.html(userInfo.hobby);
            elements.MyValue.html(userInfo.myValue);
            elements.NeedValue.html(userInfo.needValue);
        }



    },
    initEvent: function () {



    }
});