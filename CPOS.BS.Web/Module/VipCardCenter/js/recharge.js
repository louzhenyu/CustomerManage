define(['newJquery', 'tools', 'template',,'kkpager'], function () {
    var page =
        {
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/Card/CardEntry.ashx",
            //关联到的类别
            elems:
            {
                cardNo:$("#cardNo"),                                    //卡号
                btnQueryCard:$("#queryCard"),                           //卡号查询按钮
                btnQueryVip:$("#queryVip"),                             //会员查询按钮
                queryContent:$("#queryContent"),                        //卡号查询结果展示层
                cardStatus:$("#cardStatus"),                            //卡状态
                lastMoney:$("#lastMoney"),                              //卡内余额
                chooseDiv:$("#choose"),                                 //选择会员的层
                btnChooseVip:$("#chooseVip"),                           //选择会员按钮
                repeatChooseDiv:$("#repeatChoose"),                     //重新选择层
                chooseName:$("#chooseName"),                            //已选择的会员名称
                phoneContent:$("#phoneContent"),                        //手机展示层
                phone:$("#phone"),                                      //手机号
                btnChangePhone:$("#changePhone"),                       //修改手机号
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

            },
            init: function () {
                var that=this;
                this.initEvent();
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
            //显示弹层
            showElements:function(selector){
                this.elems.uiMask.show();
                $(selector).show();
            },
            hideElements:function(selector){
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
            initTables:function(){
                var that=this;
                $("#loading").show();
                //初始化当前页为0
                this.loadData.args.PageIndex=0;
                //请求结果
                this.loadData.getCardVip(function(data){
                    var list=data.Data.VipList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                    if(data.Data.TotalPage<1){
                      $("#content").html("<p style='text-align:center'>暂无数据</p>");
                   }
                    kkpager.generPageHtml({
	                        pno : 1,
	                        mode : 'click', //设置为click模式
	                        //总页码  
	                        total :data.Data.TotalPage,  
                            isShowTotalPage:false,
                            isShowTotalRecords:false,
	                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
	                        //适用于不刷新页面，比如ajax
	                        click : function(n){
		                        //这里可以做自已的处理
		                        //...
		                        //处理完后可以手动条用selectPage进行页码选中切换
		                        this.selectPage(n);
                                
                                that.loadMoreData(n);
	                        },
	                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
	                        getHref : function(n){
		                        return '#';
	                        }
	
                        },true);
               
                });
            },
             //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex=currentPage-1;
                this.loadData.getCardVip(function(data){
                   var list=data.Data.VipList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                });
            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //查询卡信息  验证卡
                this.elems.btnQueryCard.click(function(e){
                    if(that.elems.cardNo.val().length==0){
                        alert("卡号不能为空!");
                        return ;
                    }
                    //验证卡号
                    that.loadData.validateCard(function(data){
                        //卡信息
                        var cardInfo=data.Data.CardList.length?data.Data.CardList[0]:"";
                        var cardStatus="";
                        if(cardInfo==""){//卡不存在
                            cardStatus="卡不存在";
                            that.canUse=false;
                            that.elems.lastMoney.html(0.00);
                        }else{
                            if(cardInfo.UseStatus==1){
                                cardStatus="卡已使用";
                                that.canUse=false;
                            }else{
                                if(cardInfo.CardStatus==0){
                                    cardStatus="卡正常";
                                    that.canUse=true;
                                }else if(cardInfo.CardStatus==1){
                                    cardStatus="卡已冻结";
                                    that.canUse=false;    //不能充值
                                }else if(cardInfo.CardStatus==2){
                                    cardStatus="已为废卡";
                                    that.canUse=false;   //不能充值
                                }
                            }
                            that.elems.lastMoney.html(cardInfo.Amount+cardInfo.Bonus-cardInfo.ConsumedAmount);
                        }
                        that.cardStatus=cardStatus;
                        that.elems.cardStatus.html(cardStatus);
                        that.elems.queryContent.show();
                    });
                });
                //选择会员弹出层
                this.elems.btnChooseVip.click(function(e){
                    //卡正常   可以正常充值
                    if(that.canUse){
                        that.showElements("#vips");
                        //去查询会员数据
                       // that.initTables();
                    }else{
                        if(that.cardStatus==""||that.cardStatus==undefined){
                            alert("验证卡之后再选择会员!");
                        }else{
                            alert(that.cardStatus+",不能进行充值!");
                        }
                        
                    }
                     that.stopBubble(e);
                });
                //重新选择
                $("#repChoose").click(function(e){
                    //再次触发事件
                    that.elems.btnChooseVip.trigger("click");
                });
                //查询会员
                this.elems.btnQueryVip.click(function(e){
                    that.initTables();
                    that.stopBubble(e);
                });
                //选中一行表示选中会员
                $("#content").delegate("tr","click",function(e){
                    var vipItem=$(this).data("item");
                    //让内容显示
                    $("#repeatChoose").show();
                    //会员名
                    that.elems.chooseName.html(vipItem.VipName);
                    //存储会员ID
                    that.loadData.args.vipId=vipItem.VIPID;
                    //手机号
                    $("#phone").html(vipItem.Phone);
                    $(".hintClose").trigger("click");
                    that.stopBubble(e);
                });
                //充值接口
                $("#intoMoney").click(function(e){
                    //卡号
                    var cardNo=that.elems.cardNo.val();
                    //手机号
                    var mobile=$("#phone").html();
                    //密码
                    var password=$("#password").val();
                    that.loadData.args.cardNo=cardNo;
                    that.loadData.args.mobile=mobile;
                    that.loadData.args.password=password;
                    //进行激活
                    that.loadData.activeCard(function(data){
                        if(data.IsSuccess){
                            alert("卡充值成功!");
                        }else{
                            alert("卡充值失败!");
                        }
                    });
                });
                //关闭弹出层
               $(".hintClose").bind("click",function(){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().parent().fadeOut();
               });
            }
        };

    page.loadData =
    {
        args:{
            PageIndex:0,
            PageSize:10,
            Status:"-1"
        },
        //获取渠道
        validateCard: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'ValidateCard',
                    'CardNo':page.elems.cardNo.val()
                },
                success: function (data) {
                    
                    if (data.ResultCode == 0) {
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
         //根据手机号身份证会员名查询会员
        getCardVip: function (callback) {
        debugger;
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCardVip',
                    'PageSize':this.args.PageSize,
                    'PageIndex':this.args.PageIndex,
                    'Criterion':$("#name").val()   //手机号/身份证/会员名
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
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
         //给卡进行充值  激活
        activeCard: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'ActiveCard',
                    'CardNo':this.args.cardNo,
                    'VipID':this.args.vipId,
                    'Mobile':this.args.mobile,
                    'CardPassword':this.args.password
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
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