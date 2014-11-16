Jit.AM.defindPage({
    elems:{
        nav:$(".commonNav a"),
        precard:$("#precard"),              //充值卡充值
        cardQuery:$("#precard ._query"),     //查询按钮
        cardno:$("#precard ._cardno"),        //充值卡卡号
        rechargeWay:$(".recharge-onLine"),   //充值卡策略
        precardBtn:$("#precard ._chongzhi")  //充值按钮
    },
    options:{
        cardno:"",   //卡号
        skuId:"",     //在线充值策略id
        orderId:"",   //订单id
        pass:"",
        cardStatus:"",
        amount:0
    },
    page:{
        pageIndex:0,
        pageSize:10,
        allPage:2
    },
    onPageLoad: function () {
        this.initPage();
        this.initEvent();
    },
    initPage: function () {
        var me=this;
        this.getRechargeWay(function(data){
            try{
                var list=data.RechargeStrategyList;
                list=list?list:[];
                var html=bd.template("tpl_online",{list:list});
                me.elems.rechargeWay.html(html);
            }catch(ex){
                 Jit.UI.Loading(false);
            }
        });
        var baseInfo=Jit.AM.getBaseAjaxParam();
        //if(location.href.indexOf("userId")==-1){
        //    location.href=location.href+"&userId="+baseInfo.userId+"&openId="+baseInfo.openId;
        //}
        //页面配置出来
        var isOnline=this.pageParam.online;
        var isCardLine=this.pageParam.card;
        if(isOnline==0){  //在线充值隐藏
            $("._lineHide").hide();
            $("._precard").show(); 
        }
        if(isCardLine==0){
            $("._cardHide").hide();  

        }

    },
    initEvent:function(){
    	var me=this;
        //导航切换
        this.elems.nav.bind(this.eventType,function(e){
            var $t=$(this);
            //切换导航状态
            $t.addClass("on").siblings().removeClass("on");
            //控制显示隐藏
            $("."+$t.data("class")).show().siblings().hide();
        });
        //点击充值卡策略进行下单
        this.elems.rechargeWay.delegate("li",this.eventType,function(e) {
            //先下单 
            var $t=$(this);
            var skuId=$t.data("id"),amount=$t.data("amount");
            me.options.skuId=skuId;
            me.options.amount=amount;
            //下单
            me.setOrderInfo(function(data){
                var orderId=data.content.orderId;
                me.options.orderId=orderId;
                //修改状态
                me.submitOrder(function(data2){
                    if(data2.IsSuccess&&data2.ResultCode==0){
                        me.toPage('OrderPay', '&orderId=' + orderId+'&isGoodsPage=0&isHideGetToPay=1'+"&realMoney="+amount);
                    }
                });
            });
            //下单后跳转到支付界面
        });
        //充值卡号查询
        this.elems.cardQuery.bind(this.eventType,function(e){
            var cardno=me.elems.cardno.val();
            if(cardno==""){
                me.alert("卡号不能为空!");
                return;
            }
            me.options.cardno=cardno; 
            //商品处理
            me.getCardInfo(function(data){
                
                var cardInfo=data.CardInfo;
                if(!!cardInfo){
                    me.elems.precard.find("._cardInfo").show();
                    //卡状态
                    me.elems.precard.find("._status").html((cardInfo.UseStatus==0?"未使用":"已使用"));
                    if(cardInfo.UseStatus==1){
                        me.elems.precard.find("._inputPass").hide();
                        me.elems.precard.find(".infoBox").css("margin-bottom","0px");
                    }
                    me.elems.precard.find("._balance").html(cardInfo.Amount+"元");
                    me.options.amount=cardInfo.Amount;
                    me.options.cardStatus=cardInfo.CardStatus;

                }else{
                    me.alert("卡号不存在!");
                }
            });
        });
        //开始充值
    	this.elems.precardBtn.bind(this.eventType,function(e){
            var cardno=me.elems.cardno.val();
            if(cardno==""){
                me.alert("卡号不能为空!");
                return;
            }
            me.options.cardno=cardno;
            //有效
            if(me.options.cardStatus==0){
                var pass=$("#precard ._pass").val();
                if(pass.length){
                    me.options.pass=pass;
                    var baseInfo=Jit.AM.getBaseAjaxParam();
                    me.options.vipId=baseInfo.userId;
                    me.getMemberInfo(function(data){   //获得会员信息
                        me.options.mobile=data.Data.MemberInfo.Mobile;
                        //获得会员信息
                        me.recharge(function(){
                            me.alert("该 "+cardno+" 卡充值成功!",function(){
                                Jit.AM.toPage("GetVipCard");
                            });
                        });
                    });
                }else{
                    me.alert("密码不能为空!");
                }
            }else{
                me.alert("卡无效,不能充值!");
            }
        });
    },
    //获得充值策略信息
    getRechargeWay:function(callback){
        this.ajax({
            url: '/ApplicationInterface/Services/ServiceGateway.ashx',
            data: {
                'action': 'GetRechargeStrategy',
                'CardNo':this.options.cardno,
            },
            beforeSend:function(){
                Jit.UI.Loading(true);
                self.isSending = true; //正在请求
            },
            success: function (data) {
                if (data.ResultCode==0&& data.IsSuccess) {
                    if(callback){
                        //回调
                        callback(data.Data);
                    }

                }else{
                    me.alert(data.Message);
                }
            },
            complete:function(){
                self.isSending = false;
                Jit.UI.Loading(false);
            }
        });
    },
    //获得充值卡信息
    getCardInfo:function(callback){
        this.ajax({
            url: '/ApplicationInterface/Services/ServiceGateway.ashx',
            data: {
                'action': 'GetCardInfo',
                'CardNo':this.options.cardno,
            },
            beforeSend:function(){
                Jit.UI.Loading(true);
                self.isSending = true; //正在请求
            },
            success: function (data) {
                if (data.ResultCode==0&& data.IsSuccess) {
                    if(callback){
                        //回调
                        callback(data.Data);
                    }

                }else{
                    me.alert(data.Message);
                }
            },
            complete:function(){
                self.isSending = false;
                Jit.UI.Loading(false);
            }
        });
    },
    //下单
    setOrderInfo:function(callback) {
        var me=this;
        var list = [{
            'skuId': this.options.skuId, //策略ID
            'qty': 1,
            'totalAmount':me.options.amount,
        }];
        this.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setOrderInfo',
                'orderDetailList': list
            },
            success: function(data) {

                if (data.code == 200) {
                    if(callback){
                        callback(data);
                    }
                    //me.toPage('GoodsOrder', '&orderId=' + data.content.orderId);
                } else {
                    me.alert(data.description);
                }
            }
        });
    },
    //获得会员信息
    getMemberInfo:function(callback){
        this.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.GetMemberInfo',
                'VipSource':3
            },
            success: function (data) {
                if(data.ResultCode ==0){
                   if(callback){
                        callback(data);
                   }
                }else{
                    me.alert(data.Message);
                }
            }
        });
    },
    //提交订单到支付页面
    submitOrder:function(callback){
        var me=this;
        me.ajax({
            url:"/ApplicationInterface/Vip/VipGateway.ashx",
            data: {
                'action': 'SetOrderStatus',
                'OrderId': me.options.orderId,
                'Status': '100'
            },
            success: function(data) {
                if (data.ResultCode ==0) {
                    if(callback){
                        callback(data);
                    }
                }else{
                    me.alert(data.Message);
                }
            }
        });
    },
    //进行充值
    recharge:function(callback){
        var me=this;
        me.ajax({
            url:"/ApplicationInterface/Services/ServiceGateway.ashx",
            data: {
                'action': 'ActiveCard',
                'CardNo': me.options.cardno,
                'VipId':me.options.vipId,
                'Mobile':me.options.mobile,
                'CardPassword':me.options.pass
            },
            success: function(data) {
                if (data.ResultCode ==0) {
                    if(callback){
                        callback(data);
                    }
                }else{
                    me.alert(data.Message);
                }
            }
        });
    },
    alert:function(text,callback){
        Jit.UI.Dialog({
            type : "Alert",
            content : text,
            CallBackOk : function() {
                Jit.UI.Dialog("CLOSE");
                if(callback){
                    callback();
                }
            }
        });
    }
});