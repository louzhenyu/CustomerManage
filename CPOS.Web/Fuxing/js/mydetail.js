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
        //初次加载用户信息
        var datas = { keyword: '', page: '1', pageSize: '30', vipId: this.getBaseInfo().userId };
        var elements = { UserHead: $('#userhead'), UserName: $('#username'), Phone: $('#phone'), Position: $('#position'), Email: $('#email'), SayName: $('#sayname'),Company:$('#company'),DcodeImageUrl:$('#dcodeimageurl') };
        this.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
            data: datas,
            success: function (data) {
                if (data.code == 200 && data.content.vipList) {
                    SetData(data.content.vipList[0]);
                }
            }
        });


        //模拟数据
        function SetData(userInfo) {
            elements.UserHead.attr('src', userInfo.HeadImageUrl);
            elements.UserName.html(userInfo.staffName);
            if (userInfo.phone&&userInfo.phone.length>10) {
                 elements.Phone.html(userInfo.phone);
            }
            elements.Position.html(userInfo.staffPost);
            elements.Email.html(userInfo.email);
            elements.SayName.html(userInfo.staffName);
            elements.Company.html(userInfo.staffCompany);
            elements.DcodeImageUrl.attr('src', userInfo.DCodeImageUrl);
        }



    },
    initEvent: function () {



    }
});