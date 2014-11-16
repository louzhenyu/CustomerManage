Jit.AM.defindPage({

    name: 'WQuestion',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入WQuestion.....');

        this.initEvent();
    },

    initEvent: function () {

        Jit.log('初始化WQuestion页面事件....');

        var me = this;

        me.setParams('cjstest', 'success...');


        Jit.log(me.getUrlParam('urltest'));
    },
    send: function () {
        var me = this;
        //alert(me.getUrlParam('vipType'));
        if ($('#quesContent').val() == '' || $('#quesContent').val() == '请输入问题') {
            alert('请输入问题');
            return;
        }

        me.ajax({
            url: '/OnlineShopping/Data/Data.aspx',
            data: { action: "setUserMessageDataWap", toVipType: me.getUrlParam('vipType'), text: $('#quesContent').val() },
            success: function (data) {

                alert(data.description);
            }
        });

    }
});