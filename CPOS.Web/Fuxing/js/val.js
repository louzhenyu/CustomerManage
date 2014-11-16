Jit.AM.defindPage({
    name: 'Val',
    onPageLoad: function() {
        //如果已经注册直接跳转到首页
        var me = Jit.AM,
            datas = {
                eventId: (me.getBaseAjaxParam()).eventId
            }, curAuthVal = me.getPageParam(authKey);

            //如果已经认证，直接跳转到界面
        if (curAuthVal == "1") {
                me.toPage('HomePage');
        } else {
            this.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=checkSign',
                data: datas,
                success: function(data) {
                    if (data.code == 200) {
                        if (data.content.isRegistered == "1") {
                            me.toPage('HomePage');
                        } else {
                            FxLoading.Hide();
                        }
                    } else {
                        FxLoading.Hide();

                    }
                }
            });
        }

        FxLoading.AutoHide();

        this.initEvent();

        this.initData();
    },
    initData: function() {


    },
    initEvent: function() {
        var toVal = $('#toVal'),
            txtPhone = $('#txtPhone'),
            me = this;

        //手机验证
        toVal.bind('click', function() {
            var datas = {
                phone: txtPhone.val()
            }, regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
            if (!datas.phone) {
                Tips({
                    msg: '请输入您的手机号码或者VIP专属号'
                });
                return false;
            }

            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=setSignUpFosun',
                data: datas,
                success: function(data) {
                    Tips({
                        msg: data.description
                    });
                    if (data.code == 200) {
                        me.toPage('HomePage');
                        me.setPageParam(authKey, '1');
                    }
                },
                error: function() {
                    Tips({
                        msg: '服务器繁忙，请稍后.'
                    });
                }

            });

        });


    }
});