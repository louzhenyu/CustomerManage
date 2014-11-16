define(['newJquery', 'tools', 'template','md5'], function () {
    var page =
        {
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx",
            //关联到的类别
            elems:
            {
               company:$("#company"),                //公司名称
               totalMoney:$("#totalMoney"),          //提现总额
               intoMoney:$("#intoMoney"),            //到账总额
               bank:$("#bank"),                      // 收款银行
               cardId:$("#cardId"),                  //收款卡号
               area:$("#area"),                      //收款地址
               canGetMoney:$("#canGetMoney"),        //可提现金额
               waitMoney:$("#waitMoney"),            //等待出账金额
               lastTime:$("#lastTime"),              //上次提现时间
               wishMoneyBtn:$("#wishMoneyBtn"),      //申请提现
               changePassBtn:$("#changePassBtn"),    //修改提现密码
               alipayPercent:$("#alipayPercent"),    //支付宝 汇率
               yinlianPercent:$("#yinlianPercent"),  //银联 汇率
               weekDay:$("#weekDay"),                //结算周期
               lessMoney:$("#lessMoney"),            //最低结算费用
               getMoneyPass:$("#getMoneyPass"),      //申请取现的密码
               currentPass:$("#currentPass"),        //当前密码
               newPass:$("#newPass"),                //新密码
               againPass:$("#againPass"),            //确认密码
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

            },
            init: function () {
                var that=this;
                //获得客户提现信息
                this.loadData.getMoneyInfo(function(data){
                    var info=data.Data.GetCustomerWithdrawal;
                    that.fillContent(info);
                });
                this.initEvent();
            },
            //填充页面上的提现信息
            fillContent:function(moneyInfo){
                if(moneyInfo){
                    this.elems.company.html(moneyInfo.CustomerName);              //客户名称
                    this.elems.totalMoney.html("￥"+moneyInfo.CountWithdrawalAmount+"元");  //提现总额
                    this.elems.intoMoney.html("￥"+moneyInfo.BeenAmount+"元");              //到账总额
                    this.elems.bank.html(moneyInfo.ReceivingBank);                //银行
                    this.elems.cardId.html(moneyInfo.BankAccount);                //卡号
                    this.elems.area.html(moneyInfo.OpenBank);                     //开户行
                    this.elems.canGetMoney.html(moneyInfo.CanWithdrawalAmount);   //可提现金额
                    //则不能提现
                    if(moneyInfo.CanWithdrawalAmount<=0){
                        //设置不可点击
                        this.elems.wishMoneyBtn.css({
                            "background": "gray",
                            "color":"white"
                            
                        }).data("canclick",false);//不可点击
                        
                    }
                    this.elems.waitMoney.html(moneyInfo.WaitForAmount);           //等待提现金额
                    this.elems.lastTime.html(moneyInfo.LastWithdrawalTime);       //上次提现时间
                    this.elems.alipayPercent.html(moneyInfo.PaypalRate*100+"%");          //支付宝汇率
                    this.elems.yinlianPercent.html(moneyInfo.CUPRate*100+"%");            //银联汇率
                    this.elems.weekDay.html("T + "+moneyInfo.OffPeriod);                 //周期
                    this.elems.lessMoney.html(moneyInfo.MinAmount);               //最低金额
                    $("#textCon").append(moneyInfo.PayRemark||"暂无说明");   //说明
                }else{
                    alert("未请求到数据!");
                }
            },
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            stopBubble: function (e) {
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                }
                else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
                e.preventDefault();
            },
            initEvent: function () {
                //初始化事件集
                var that = this;
               //申请提现
               this.elems.wishMoneyBtn.bind("click",function(){
                    var canclick=$(this).data("canclick");
                    if(canclick!=false){
                        that.elems.getMoneyPass.val("");
                        that.showElements("#getMoneyDiv");
                    }
               });
               //修改密码
               this.elems.changePassBtn.bind("click",function(){
                    that.elems.currentPass.val("");
                    that.elems.newPass.val("");
                    that.elems.againPass.val("");
                    that.showElements("#changePassDiv");
               });
               
               //确定申请提现
               $("#sureGet").bind("click",function(){
                      if(that.elems.getMoneyPass.val().length==0){
                        alert("提现密码不能为空!");
                        return;
                      };
                      //密码
                      that.loadData.args.WithdrawalPassword=MD5(that.elems.getMoneyPass.val());
                      //可提现金额
                      that.loadData.args.WithdrawalAmount=that.elems.canGetMoney.html();
                      //进行请求
                      that.loadData.pleaseMoney(function(data){
                            $("#text").html("申请提现成功!");
                            $("#getMoneyDiv").fadeOut();
                            $("#tips").fadeIn();
                            that.isSuccess=true;//提现成功
                      });
               });
               //确定申请提现
               $("#sureChangePass").bind("click",function(){
                      if(that.elems.currentPass.val().length==0){
                        alert("当前密码不能为空!");
                        return;
                      };
                      if(that.elems.newPass.val().length==0){
                        alert("新密码不能为空!");
                        return;
                      }
                      if(that.elems.newPass.val()==that.elems.currentPass.val()){
                        alert("新密码不能和当前密码一样!");
                        return;
                      }
                      if(that.elems.newPass.val().length==0){
                        alert("新密码不能为空!");
                        return;
                      }
                      if(that.elems.againPass.val().length==0){
                        alert("再次输入的密码不能为空!");
                        return;
                      }
                      if(that.elems.againPass.val()!=that.elems.newPass.val()){
                        alert("两次输入的密码不一致!");
                        return;
                      }
                      //旧密码
                      that.loadData.args.oldPass=MD5(that.elems.currentPass.val());
                      //新密码
                      that.loadData.args.newPass=MD5(that.elems.againPass.val());
                      //修改密码
                      that.loadData.changePass(function(data){
                         $("#changePassDiv").fadeOut(); 
                         $("#text").html("提现密码修改成功!");
                         that.showElements("#tips");
                      });
               });
               //关闭弹出层
               $("#sureOk").bind("click",function(){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().fadeOut();
                      setTimeout(function(){
                        if(that.isSuccess){
                            location.reload();
                        }
                      },2000);
               });
               //关闭弹出层
               $(".hintClose").bind("click",function(){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().fadeOut();
                      setTimeout(function(){
                        if(that.isSuccess){
                            location.reload();
                        }
                      },2000);
               });
            },
            //显示弹层
            showElements:function(selector){
                this.elems.uiMask.show();
                $(selector).show();
            },
            hideElements:function(selector){
                this.elems.uiMask.fadeOut(500);
                $(selector).fadeOut(500);
            }


        };

    page.loadData =
    {
        args:{},
        //获得用户提现的信息
        getMoneyInfo: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCustomerWithdrawal'
                },
                success: function (data) {

                    if (data.ResultCode == 0&&data.IsSuccess) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //申请提现
        pleaseMoney: function (callback) {
            var that=this;
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'ApplyForWithdrawal',
                    'WithdrawalAmount':this.args.WithdrawalAmount,
                    'WithdrawalPassword':this.args.WithdrawalPassword
                },
                success: function (data) {
                    if (data.ResultCode == 0&&data.IsSuccess) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        if(data.ResultCode==302){
                            alert("您的初始密码没有修改，请先修改!");
                            $("#getMoneyDiv").fadeOut();
                            $("#changePassDiv").fadeIn(500);
                        }else if(data.ResultCode==304){
                            $("#getMoneyDiv").fadeOut(); 
                            $("#text").html(data.Message);
                            $("#tips").fadeIn();
                        }else{
                            alert(data.Message);
                        }
                        
                    }
                }
            });
        },
        //修改提现密码
        changePass: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'SetWithdrawalPwd',
                    'OldWithdrawalPassword':this.args.oldPass,
                    'NewWithdrawalPassword':this.args.newPass
                },
                success: function (data) {
                    if (data.ResultCode == 0&&data.IsSuccess) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        }
    }
    //初始化
    page.init();
});