Jit.AM.defindPage({
    elems:{},
    onPageLoad: function () {
        var that=this;
        this.initPage();
    },
    initWithParam:function(param){
    	this.param=param;
        //根据config中参数设置客户的会员卡图片
        $('[name=vipcardImg]').attr('src',param.vipCardImg);
    },
    initPage: function () {
    	Jit.UI.Loading(true);
    	this.getVipBenefits();
    },
    //获得会员权益
    getVipBenefits:function(){
    	var me=this;
    	me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.GetMemberBenefits',
            },
            success: function (data) {
                Jit.UI.Loading(false);
                if(data.ResultCode == 0){
                	var res=data.Data.MemberBenefits;
                	if(res.length){
                		var str=res[0];
                		$("#vipBenefits").html(str);
                	}
                }
            }
        });
    }
});