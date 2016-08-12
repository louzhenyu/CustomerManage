define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','highcharts'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格活动body部分
            tabelWrap:$('#tableWrap'),
            editLayer: $("#editLayer"),           //图片上传
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            addToolsBtn:$('#addTools'),
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            dataNoticeList:$('#notice'),
            panlH:116,                           // 下来框统一高度
            editMessage:$('.editMessage')     //编辑表格部分
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        //显示弹层
        showElements:function(selector){
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements:function(selector){

            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        //加载事件
        initEvent: function () {
            var that = this;
            //点击checkBox
            that.elems.editMessage.delegate(".checkBox","click",function(e){
                var isCheack = $(this).hasClass('on'),
                    inputs = $(this).parents('.editMessage').find('input');
                if(isCheack){
                    $(this).removeClass('on');
                    inputs.attr('disabled','disabled');
                }else{
                    $(this).addClass('on');
                    inputs.attr('disabled',false);
                }
            });
            //点击radio
            that.elems.editMessage.delegate(".radio","click",function(e){
                var isCheack = $(this).hasClass('on'),
                    radios = $(this).parents('.contents').find('.radio'),
                    inputs = $(this).parents('.editMessage').find('input'),
                    inputtt = $(this).parents('.contents').find('input');
                if(!isCheack){
                    radios.removeClass('on');
                    $(this).addClass('on');
                    if(inputs.length=='0'){
                        inputtt.attr('disabled','disabled');
                    }else{
                        inputtt.attr('disabled',false);
                    }
                }
            });

            //设置提现规则
            that.elems.sectionPage.delegate(".saveBtn","click",function(e){

                var item = $('.module').find('.contents')
                var isTrue = false;
                item.each(function(i){
                    var $tt = $(this).find('.on');
                    var inpnts = $tt.parents('.editMessage').find('input');
                    var exp =/^[0-9]*[1-9][0-9]*$/;//正整数正则
                    if(i==0){
                        if($tt.length=='0'){
                            that.loadData.args.BeforeWithDrawDays =0;
                            isTrue = true;
                        }else{
                            if(inpnts.val()==""){
                                alert('可提现金额设置不能为空');
                                isTrue = false;
                                return false;
                            }else if(!exp.test(inpnts.val())){
                                alert('可提现金额设置只可输入正整数');
                                isTrue = false;
                                return false;
                            }
                            else{
                                that.loadData.args.BeforeWithDrawDays =inpnts.val();
                                isTrue = true;
                            }
                        }
                    }
                    if(i==1){
                        if(inpnts.length=='0'){
                            that.loadData.args.MinAmountCondition =0;
                            isTrue = true;
                        }else{
                            if(inpnts.val()==""){
                                alert('提现条件不能为空');
                                isTrue = false;
                                return false;
                            }else if(!exp.test(inpnts.val())){
                                alert('提现条件只可输入正整数');
                                isTrue = false;
                                return false;
                            }
                            else {
                                that.loadData.args.MinAmountCondition = inpnts.val();
                                isTrue = true;
                            }
                        }
                    }
                    if(i==2){
                        if(inpnts.length=='0'){
                            that.loadData.args.WithDrawMaxAmount =0;
                            isTrue = true;
                        }else{
                            if(inpnts.val()==""){
                                alert('提现额度不能为空');
                                isTrue = false;
                                return false;
                            }else if(!exp.test(inpnts.val())){
                                alert('提现额度只可输入正整数');
                                isTrue = false;
                                return false;
                            }else {
                                that.loadData.args.WithDrawMaxAmount = inpnts.val();
                                isTrue = true;
                            }
                        }
                    }
                    if(i==3){
                        if(inpnts.length=='0'){
                            that.loadData.args.WithDrawNum =0;
                            isTrue = true;
                        }else{
                            if(inpnts.val()==""){
                                alert('提现次数不能为空');
                                isTrue = false;
                                return false;
                            }else if(!exp.test(inpnts.val())){
                                alert('提现次数只可输入正整数');
                                isTrue = false;
                                return false;
                            }else {
                                that.loadData.args.WithDrawNum = inpnts.val();
                                isTrue = true;
                            }
                        }
                    }
                })
                if(isTrue){
                    that.loadData.setVipWithDrawRule(function(IsSuccess){
                        if(IsSuccess){
                            alert('提现规则设置成功');
                           // $.util.toNewUrlPath( "/module/sendingTogether/sendingManager.aspx");
                        }
                    });
                }

            });

            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=140,H=30;
            // $('#rewardRule').combobox({
            //     width:wd,
            //     height:H,
            //     panelHeight:that.elems.panlH,
            //     valueField: 'id',
            //     textField: 'text',
            //     data:[{
            //         "id":1,
            //         "text":"现金"
            //     },{
            //         "id":2,
            //         "text":"积分",
            //         "selected":true
            //     }]
            // });
            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onClose:function(){
                    $(that.elems.operation.find("li").get(1)).trigger("click");
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            // that.loadData.args.PageIndex=1;
            // var fileds=$("#seach").serializeArray();
            // $.each(fileds,function(i,filed){
            //     that.loadData.seach[filed.name] = filed.value;
            // });
            that.loadData.tag.busiType=that.elems.operation.find("li.on").data("field7");
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.getVipWithDrawRule(function(data){

                that.getRule(data);
            })
            $.util.stopBubble(e);
        },
        getRule:function(data){

            var WithDrawDays = data.BeforeWithDrawDays,//可提现天数
                Condition = data.MinAmountCondition,//最低提现条件
                MaxAmount = data.WithDrawMaxAmount,//每次最多提现额度
                DrawNum = data.WithDrawNum;//提现次数限制数量
            
            if(WithDrawDays!='0'){
                $('#WithDrawDays').parents('.editMessage').children('.checkBox').trigger('click');
                $('#WithDrawDays').val(WithDrawDays);
            }
            if(Condition=='0'){
                $('#minCondition').parents('.contents').find(".radio[data-type='0']").trigger('click');
            }else{
                $('#minCondition').parents('.contents').find(".radio:not([data-type='0'])").trigger('click');
                $('#minCondition').val(Condition);
            }
            if(MaxAmount=='0'){
                $('#MaxAmount').parents('.contents').find(".radio[data-type='0']").trigger('click');
            }else{
                $('#MaxAmount').parents('.contents').find(".radio:not([data-type='0'])").trigger('click');
                $('#MaxAmount').val(MaxAmount);
            }
            if(DrawNum=='0'){
                $('#DrawNum').parents('.contents').find(".radio[data-type='0']").trigger('click');
            }else{
                $('#DrawNum').parents('.contents').find(".radio:not([data-type='0'])").trigger('click');
                $('#DrawNum').val(DrawNum);
            }
        },

        loadData: {
            args: {
                'BeforeWithDrawDays':'',
                'MinAmountCondition':'',
                'WithDrawMaxAmount':'',
                'WithDrawNum':''
            },
            tag:{
                busiType:'',
                RetailTraderID:''
            },
            opertionField:{},
            //获取提现规则
            getVipWithDrawRule: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.WithdrawDeposit.GetVipWithDrawRule'
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        }else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //设置提现规则
            setVipWithDrawRule: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.WithdrawDeposit.SetVipWithDrawRule',
                        'BeforeWithDrawDays':this.args.BeforeWithDrawDays,
                        'MinAmountCondition':this.args.MinAmountCondition,
                        'WithDrawMaxAmount':this.args.WithDrawMaxAmount,
                        'WithDrawNum':this.args.WithDrawNum
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.IsSuccess);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

            //获取下线人数详情列表
            getSuperRetailTraderList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.SuperRetailTraderConfig.GetSuperRetailTraderList',
                        'PageIndex':this.args.PageIndex,
                        'PageSize':this.args.PageSize,
                        'SuperRetailTraderID':this.tag.RetailTraderID


                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

        }

    };
    page.init();
});

