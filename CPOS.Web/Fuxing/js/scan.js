Jit.AM.defindPage({
    name: 'Scan',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发
        Jit.log('进入MyInfoPage.....');

        this.initData();
    },
    initData: function () {




        var datas = { keyword: '', page: '1', pageSize: '30', vipId: this.getBaseInfo().userId };

        this.ajax({
            url: '/OnlineShopping/data/Data.aspx?action=GetSearchVipStaff',
            data: datas,
            success: function (data) {
                if (data.code == 200) {
                var userInfo=data.content.vipList[0];
                $('#scanImg').attr('src', userInfo.DCodeImageUrl);
                }
            }
        });

    }
});