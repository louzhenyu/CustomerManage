Jit.AM.defindPage({
    name: 'Code',
    onPageLoad: function () {

        //当页面加载完成时触发
        //        Jit.log('进入MyInfoPage.....');

        this.initEvent();

        this.initData();
    },
    initData: function () {
        //如果已经签到直接跳转到首页
        var me = Jit.AM, datas = { eventId: (me.getBaseAjaxParam()).eventId };
        this.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=checkSign',
            data: datas,
            success: function (data) {
                if (data.code == 200) {
                    if (data.content.isSigned == "1" && data.content.isRegistered == "1") {
                        me.toPage('HomePage');
                    } else {
                        FxLoading.Hide();

                    }
                } else {
                    FxLoading.Hide();
                }
            }
        });
        FxLoading.AutoHide();

    },
    initEvent: function () {


    }
});