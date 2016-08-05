define(['jquery','template', 'tools','easyui', 'artDialog','kkpager'], function ($) {

    var page = {
        elems: {
            optPanel:$("#optPanel"), //页面的顶部li
            submitBtn:$("#submitBtn"),   //提交和下一步按钮
            section:$("#section"),    //
            editLayer:$("#editLayer"), //图片上传
            simpleQuery:$("#simpleQuery"),
            tagPanel:$(".tagPanel"),                   //表格body部分
            tabelWrap:$('#tableWrap'),                //表格head部分
            SpecialDateList: [],         //弹出框
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            width:160,
            height:32,
            panlH:200,
            prizesCache:[],//缓存已经选择过的优惠券。
            messageDeleteList:[],//缓存删除的数据
            allData:{} //页面所有存放对象基础数据
        },
        feeSplittingList: [{id: "Step", name: "梯度"}, {id: "Superposition", name: "叠加"}],
        init: function () {
            var ActivityID= $.util.getUrlParam("ActivityID");
            this.loadData.args.ActivityID=  ActivityID;
            this.initEvent();
            var that=this;


            this.loadDataPage();
        },

         bgOption:function(){
             $("#optPanel").parent().removeClass("bg02").removeClass("bg03") ;
             if($("#optPanel li.on").index()==1){
                 $("#optPanel").parent().addClass("bg02")
             }
             if($("#optPanel li.on").index()==2){
                 $("#optPanel").parent().addClass("bg03")
             }
         },

        initEvent: function () {
            var that = this;
            that.elems.simpleQuery.find(".panelDiv").fadeOut(0).eq(0).fadeIn("slow");
           $("#ProfitType").combobox({
                data: that.feeSplittingList,
                valueField: 'id',
                textField: 'name',
                panelHeight: 100,
                editable:false,
                required:true,
                onSelect: function (record) {
                    if(record) {
                        debugger;
                        var allDom = $(this).parents(".commonSelectWrap").find(".lineList");
                        allDom.hide();

                        allDom.find(".easyui-numberbox").numberbox({
                            disabled: true,
                        });
                        allDom.find(".textbox-text").css({"background": "#efefef"});

                        var selector = "[data-name='{0}'].lineList".format(record.id);
                        $(this).parents(".commonSelectWrap").find(selector).show();
                        $(this).parents(".commonSelectWrap").find(selector).find(".easyui-numberbox").numberbox({
                            disabled: false,

                        });
                        $(this).parents(".commonSelectWrap").find(selector).find(".textbox-text").css({"background": "#fff"});
                    }
                }
            });
            $("#ProfitType").combobox('select','Superposition');

            /**************** -------------------初始化easyui 控件 start****************/
            var wd=160,H=32;

            $("#nav03").delegate(".textAddBtn","click",function() {
                if ($(select).find(".templatePanel").length < 5) {
                    var type = $(this).data("flag");
                    var select = "#" + type;
                    var submit = {is: true, msg: ""};
                    var selectInput=".text [data-activity='{0}']".format(that.loadData.args.ActivityType);
                    $(select).find(".templatePanel").each(function (index) {
                        var me = $(this), messageInfo = me.data("message");
                        var messageTypeName=type == "SMS" ? "短信设置":"微信设置";
                      /*  if(type == "SMS") {                           不存在 textarea 注释 防止恢复
                            if ($(this).find(".text textarea").val()) {

                            } else {

                                submit.is = false;
                                if (type == "SMS") {
                                    submit.msg = "短信设置,第" + (me.index()) + "项未设置发送信息";
                                }
                                 else if (type == "WeChat") {
                                 submit.msg = "微信设置,第" + (me.index()) + "项未设置发送信息";
                                 }

                                return false;
                            }
                        }*/
                        if (!me.find('[data-name="day"]').combobox("isValid")) {
                            submit.is = false;
                            submit.msg = messageTypeName + ",第" + me.index() + "请选择一个天数";
                            return false;
                        } else if (!me.find('[data-name="hour"]').combobox("isValid")) {
                            submit.is = false;
                            submit.msg = messageTypeName + ",第" + me.index() + "请选择一个小时数";
                            return false;
                        }
                        if(type == "SMS") {
                            var isSubmit=true;
                             if(me.find(selectInput).find("input").length==1){
                                 isSubmit= $.trim(me.find(selectInput).find("input").val()).length>0
                            }
                            if(me.find(selectInput).find("input").length==2){
                                isSubmit= $.trim(me.find(selectInput).find("input").eq(0).val()).length>0 &&$.trim(me.find(selectInput).find("input").eq(1).val()).length>0
                            }
                            if (isSubmit) {

                            } else {

                                submit.is = false;
                                if (type == "SMS") {
                                    submit.msg = "短信设置,第" + (me.index()) + "项发送信息未填写完整";
                                }
                                else if (type == "WeChat") {
                                    submit.msg = "微信设置,第" + (me.index()) + "项发送信息未填写完整";
                                }

                                return false;
                            }
                        }

                    });


                    if(submit.is) {
                        var messageInfo = {
                            MessageID: "",
                            MessageType: type,
                            ActivityType:$('.radio[data-name="ActivityType"].on').data("value"),
                            TemplateID: "",
                            IsEnable: true,
                            SendTime: "",
                            Content: ""
                        };

                        $(select).find(".templatePanel:last").removeClass("borderNone");
                        var html = bd.template('tpl_addMessage', {item: messageInfo});
                        $(select).append(html);
                        $(select).find(".templatePanel:last").addClass("borderNone");
                        $(select).find(".templatePanel:last").find(".easyui-combobox").combobox();
                        $(select).find(".templatePanel:last").find("textarea").validatebox();
                        debugger;
                        $(select).find(".templatePanel:last").find(".easyui-validatebox").validatebox({
                            required:true,
                            validType:'maxLength[16]'
                        });
                        $(select).find(".templatePanel:last").find(".text p").hide();
                        $(select).find(".templatePanel:last").find(".text [data-activity='"+messageInfo.ActivityType+"']p").show();
                        $(select).find(".templatePanel:last").find(".text p").find(".easyui-validatebox").validatebox("disableValidation");  //禁用验证
                        $(select).find(".templatePanel:last").find(".text [data-activity='"+messageInfo.ActivityType+"']p").find(".easyui-validatebox").validatebox("enableValidation");

                    }else{
                        $.messager.alert("提示", submit.msg);
                    }
                } else {
                    $.messager.alert("提示", "一种类型消息最多添加5条");
                }

            });
            /* $('#BeginDate').datebox({  //如果BeginDate改变，所有的消息发送时间都要做相应的变更。
             onSelect: function(date){
             debugger;
             $("#nav03").find(".easyui-combobox").each(function(){
             var value= $(this).combobox("getValue");
             $(this).combobox("setValue",value);

             })
             }
             });*/


            /**************** -------------------初始化easyui 控件  End****************/
            that.elems.optPanel.delegate("li","click",function(e){
                debugger;
                var me=$(this), flag=$(this).data("flag"),issubmit=true;

                switch(me.index()){
                    case 0:
                        that.elems.submitBtn.html("下一步");
                        that.elems.submitBtn.data("submitindex", 1);  //点击下一步的索引值（根据跳转的第几个索引）
                        that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                        $(flag).fadeIn("slow");
                        $(".bgWhite").hide();
                        that.elems.optPanel.find("li").removeClass("on");
                        me.addClass("on");
                        that.bgOption();
                        break;
                    case 1:
                        if ($('#nav0_1').form('validate')) {    //easy_ui 控件验证 是否通过
                            if (issubmit) {  //是否可以提交了
                                var fields = $('#nav0_1').serializeArray(); //自动序列化表单元素为JSON对象
                                debugger;
                                that.loadData.operation(fields, "SetActivity", function (data) {
                                    that.loadData.args.ActivityID = data.Data.ActivityID;

                                    that.loadData.GetCardholderCount(function(data){

                                        debugger;
                                        $(".tagPanel .optPanel  .hint").html(that.loadData.args.VipCardTypeName+"活动目标总人数："+$.util.groupSeparator(data.Data.Count)+"人");
                                    });
                                    $("#nav02").find(".tagPanel").hide();
                                    if($('[data-name="ActivityType"].on').data("value")==3){
                                        $("#nav02").find(".tagPanel").eq(0).show();
                                    }else{
                                        $("#nav02").find(".tagPanel").eq(1).show();
                                    }
                                    //提交成功下一步的操作记录
                                    that.elems.submitBtn.html("下一步");
                                    $(".bgWhite").show();
                                    that.elems.submitBtn.data("submitindex",2); //点击下一步的索引值（根据跳转的第几个索引）
                                    that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                    $(flag).fadeIn("slow");
                                    that.elems.optPanel.find("li").removeClass("on");
                                    me.addClass("on");
                                    that.loadDataPage();
                                    that.bgOption();
                                });
                            }
                        }
                        break;
                    case 2:
                        if(that.loadData.args.ActivityID&&that.elems.submitBtn.data("submitindex")==2) {
                            //此处验证判断是否是下一步的按钮操作
                            if($('[data-name="ActivityType"].on').data("value")==3) {
                                if ($('#nav0_2').form('validate')) {    //easy_ui 控件验证 是否通过
                                    var ruleType=$("#ProfitType").combobox("getValue");
                                    var RechargeStrategyInfoList=[] ;
                                    $('[data-name="'+ruleType+'"].lineList').find(".linetext").each(function(){    //叠加/梯度数组
                                        var obj={RuleType:ruleType,IsEnable:1};
                                        if($(this).data("id")) {
                                            obj["RechargeStrategyId"] =$(this).data("id");
                                        }
                                        $(this).find('[data-filed]').each(function(){  //取 不是list类型字段
                                            var name=$(this).data("filed");
                                            if($(this).hasClass("easyui-numberbox")){
                                                obj[name]=$(this).numberbox("getValue");
                                            }
                                        });
                                        RechargeStrategyInfoList.push(obj);
                                    });

                                    var  ruleList=$("#nav02").find(".tagPanel").eq(0).find(".commonSelectWrap").data("rulelist");  //取出原来的数据
                                    if(typeof (ruleList)=="string"){
                                        ruleList=JSON.parse(ruleList);
                                    }
                                    var selectList=[];
                                    if(ruleList&&ruleList.length>0){   //验证是否有要删除的数据
                                        $.each(ruleList,function(index,rLItem){
                                            var IsAdd=true;
                                            if(RechargeStrategyInfoList.length>0){
                                                $.each(RechargeStrategyInfoList,function(i,PTIItem){
                                                    if(rLItem.RechargeStrategyId== PTIItem.RechargeStrategyId){   //验证ruleList 对象里面的数据是删除还是添加
                                                        IsAdd=false;
                                                        //selectList.push(PTIItem);    //如果需要传递  IsDelete=0  PTIItem["IsDelete"]=0
                                                        //ProfitTypeInfoList.splice(i,1);
                                                        return false;
                                                    }
                                                })
                                            }

                                            if(IsAdd){    //如果是删除就补录上。
                                                rLItem["IsEnable"]=0;
                                                selectList.push(rLItem);
                                            }
                                        })
                                    }
                                    debugger;
                                    if(selectList.length>0){
                                        $.each(selectList,function(index,item){
                                            RechargeStrategyInfoList.push(item)
                                        })

                                    }





                                    that.loadData.operation([{name:"RechargeStrategyInfoList",value:RechargeStrategyInfoList}], "SetPrizes", function (data) {  //设置分润策略

                                        that.elems.submitBtn.html("确定");
                                        $(".bgWhite").show();
                                        that.elems.submitBtn.data("submitindex", 3);
                                        that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                        $(flag).fadeIn("slow");
                                        that.elems.optPanel.find("li").removeClass("on");
                                        me.addClass("on");
                                        that.bgOption();
                                    });
                                }
                            }else{
                                that.elems.submitBtn.html("确定");
                                $(".bgWhite").show();
                                that.elems.submitBtn.data("submitindex", 3);
                                that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                $(flag).fadeIn("slow");
                                that.elems.optPanel.find("li").removeClass("on");
                                me.addClass("on");
                                that.bgOption();
                            }
                        }else if(!that.loadData.args.ActivityID){
                            //在新增的时候  如果没有创建成功（ActivityID不存在）需要创建
                            if ($('#nav0_1').form('validate')) {    //easy_ui 控件验证 是否通过
                                if (issubmit) {  //是否可以提交了
                                    var fields = $('#nav0_1').serializeArray(); //自动序列化表单元素为JSON对象
                                    that.loadData.operation(fields, "SetActivity", function (data) {

                                        that.loadData.GetCardholderCount(function(data){
                                            debugger;
                                            $(".tagPanel .optPanel  .hint").html(that.loadData.args.VipCardTypeName+"持卡人数:"+data.Data.Count+"人");
                                        });
                                        //提交成功下一步的操作记录
                                        that.elems.submitBtn.html("确定");
                                        $(".bgWhite").show();
                                        that.elems.submitBtn.data("submitindex", 3);
                                        that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                        $(flag).fadeIn("slow");
                                        that.elems.optPanel.find("li").removeClass("on");
                                        me.addClass("on");
                                        that.bgOption();
                                    });
                                }
                            }

                        }else{
                            //如果是编辑状态，又不是下一步按钮。只是查看直接跳转;
                            that.elems.submitBtn.html("确定");
                            $(".bgWhite").show();
                            that.elems.submitBtn.data("submitindex", 3);
                            that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                            $(flag).fadeIn("slow");
                            that.elems.optPanel.find("li").removeClass("on");
                            me.addClass("on");
                            that.bgOption();
                        }

                        if($('[data-name="ActivityType"].on').data("value")==3) {
                            $("#WeChat").find(".commonSelectWrap").hide();
                            $("#WeChat").find(".commonSelectWrap").find(".easyui-combobox").combobox('disableValidation')
                        }else{
                            $("#WeChat").find(".commonSelectWrap").show();
                            $("#WeChat").find(".commonSelectWrap").find(".easyui-combobox").combobox('enableValidation')
                        }
                        if($('[data-name="ActivityType"].on').data("value")==1) {
                            $(".panelSel").find(".tit").html("生日前");
                        } else{
                            $(".panelSel").find(".tit").html("活动前");
                        }
                        break;
                }

            });


            //选择券事件  和选择模板事件
            that.elems.tagPanel.delegate(".commonBtn","click",function(){
                var me=$(this);

                that.loadData.args.PrizesType=$(this).data("prizestype");
                if(that.loadData.args.PrizesType) {  //选择券，
                    that.loadData.args.PrizesID = $(this).data("prizesid");
                    var cacheList = [];
                    $.each(that.elems.prizesCache, function (index, filed) {
                        if (filed.type == that.loadData.args.PrizesType) {
                            cacheList = filed.list;
                        }
                    });

                    that.addCouponList(cacheList);

                }else{
                    that.loadData.getMessageTemplateList(function (data) {
                        debugger;
                        that.loadData.args.messageDiv= me.parents(".templatePanel");
                        if (data.Data.MessageTemplateInfoList && data.Data.MessageTemplateInfoList.length > 0) {
                            that.addMessageTemplate(data.Data.MessageTemplateInfoList);

                        }
                        else {
                            $.messager.alert("提示", "没有消息模板，请添加");
                        }
                    });

                }
            });
            that.elems.section.delegate(".bgWhite","click",function(){
                var index=0;
                that.elems.simpleQuery.find(".panelDiv").each(function (e) {
                    debugger;
                    if (!($(this).is(":hidden"))) {
                        index = $(this).data("index")-1;
                    }

                });
                that.elems.optPanel.find("li").eq(index).trigger("click");
            }).
                delegate('.radio',"click",function(){
                    var me = $(this), name = me.data("name");
                    var selector = "[data-name='{0}']".format(name);


                    me.parents(".commonSelectWrap").find(selector).removeClass("on");
                    me.addClass("on");
                    switch (name){
                        case "ActivityType":
                            that.loadData.args.ActivityType=me.data("value");
                            debugger;
                            var nodeList= $("#levelList").find(".checkBox[data-recharge='0']");
                            nodeList.show();
                            if(me.data("value")==3) {

                                if(nodeList.length>0){
                                    nodeList .hide();
                                    nodeList.removeClass("on");
                                    nodeList.eq(0).addClass('on').trigger("click",true);
                                }

                                $("#WeChat").find(".commonSelectWrap").hide();
                                $("#WeChat").find(".commonSelectWrap").find(".easyui-combobox").combobox('disableValidation')
                            }else{
                                $("#WeChat").find(".commonSelectWrap").show();
                                $("#WeChat").find(".commonSelectWrap").find(".easyui-combobox").combobox('enableValidation')
                            }
                            $("#SMS").find(".templatePanel").find(".easyui-validatebox").validatebox({
                                required:true,
                                validType:'maxLength[16]'
                            });
                            $("#SMS").find(".templatePanel").find(".text p").hide();
                            $("#SMS").find(".templatePanel").find(".text [data-activity='"+me.data("value")+"']p").show();
                            $("#SMS").find(".templatePanel").find(".text p").find(".easyui-validatebox").validatebox("disableValidation");  //禁用验证
                            $("#SMS").find(".templatePanel").find(".text [data-activity='"+me.data("value")+"']p").find(".easyui-validatebox").validatebox("enableValidation");

                            break;
                    }

                });
            that.elems.section.delegate("#submitBtn","click",function(e){
                debugger;
                var index=$(this).data("submitindex");
                if(that.elems.optPanel.find("li").length==index){

                    that.loadData.operation("", "SetActivityMessage", function (data) {
                        alert("操作成功")
                        debugger;
                        $.util.toNewUrlPath("QueryList.aspx");
                    });
                }
                that.elems.optPanel.find("li").eq(index).trigger("click");
                $.util.stopBubble(e);


            }).delegate(".radioBtn", "click", function () {
               if ($(this).data("name") == "add") {
                    //add_lineList
                    if ($(this).parents(".lineList").find(".linetext").length < 5) {
                        var type = $(this).parents(".commonSelectWrap ").find(".easyui-combobox").combobox('getValue');
                        var index=$(this).parents(".linetext").index();
                        var html = bd.template("add_lineList", {type: type});
                        if($(this).parents(".lineList").find(".linetext").length==0){
                            $(this).parents(".lineList").append(html);
                            $(this).parents(".lineList").find(".linetext:last").find(".easyui-numberbox").numberbox({
                                disabled: false,
                                required: true
                            });
                        }else{
                            $(this).parents(".linetext").after(html);
                            $(this).parents(".lineList").find(".linetext").eq(index+1).find(".easyui-numberbox").numberbox({
                                disabled: false,
                                required: true
                            });
                        }
                        debugger;
                        if ($(this).parents(".lineList").find(".linetext:first").find('[data-name="add"]').length == 0 && type == "Step") {
                            $(this).parents(".lineList").find(".linetext:first").append('<div class="radioBtn r" data-name="add">添加</div>')
                        }
                        $(this).parents(".lineList").find(".listBtn").remove();
                    } else {
                        alert("最多可以添加五个梯度")
                    }
                } else if ($(this).data("name") == "del") {
                    var type = $(this).parents(".commonSelectWrap ").find(".easyui-combobox").combobox('getValue'), dom = $(this).parents(".lineList");

                    if (dom.find(".linetext").length == 1) {
                        if (type == "Step") {

                            dom.append('<div class="radioBtn l listBtn" data-name="add">新增梯度分润</div>')
                        } else {
                            dom.append(' <div class="radioBtn l listBtn" data-name="add">新增叠加分润</div>')
                        }
                    }
                    $(this).parents(".linetext").remove();
                 /*   if (dom.find(".linetext:first").find('[data-name="add"]').length == 0 && type == "Step") {
                        dom.find(".linetext:first").append('<div class="radioBtn r" data-name="add">添加</div>')
                    }*/

                }
            }).delegate(".checkBox","click",function(e,IsProcedure){
                var me= $(this),dataName=me.data("name");me.toggleClass("on");
                debugger;
                switch (dataName){
                    case "datebox":
                        if(me.hasClass("on")) {
                            $("input[data-name='"+me.data("filed")+"']").datebox({disabled: true});
                            $("[data-name='"+me.data("filed")+"'].easyui-datebox").siblings(".datebox").eq(1).addClass("bgColor");
                        }else{
                            $("input[data-name='"+me.data("filed")+"']").datebox({disabled: false});
                            $("[data-name='"+me.data("filed")+"'].easyui-datebox").siblings(".datebox").eq(1).removeClass("bgColor");
                        }
                        break;
                    case "IsAllCardType":                                            3
                        that.loadData.args.VipCardTypeID=[];
                        if(me.hasClass("on")&&me.data("type")=="all") {
                            debugger;
                            $(".searchInput [data-name='IsAllCardType']").removeClass("on");
                            $('[data-name="IsAllCardType"]').each(function() {
                                var isAdd = true;
                                if ($('[data-name="ActivityType"].on').data("value") == 3&&$(this).data("recharge")==0) { //充值状态要筛选掉不可充值的卡
                                    isAdd = false

                                }
                                if ($(this).data("id") && isAdd) {
                                    that.loadData.args.VipCardTypeID.push($(this).data("id"))
                                }


                            })//获取 全部持卡人数


                        }else{
                            if(!IsProcedure) {
                                me.parents(".commonSelectWrap").find("[data-type='all']").removeClass("on");
                            }
                            $('[data-name="IsAllCardType"].on').each(function(){
                                debugger;
                                if($(this).data("id")){
                                    that.loadData.args.VipCardTypeID.push($(this).data("id"))
                                }
                            })

                        }
                        debugger;
                        if( that.loadData.args.VipCardTypeID.length>0){
                            that.loadData.GetCardholderCount(function(data){

                                $("#vipCount").html("全部持卡人数："+data.Data.Count+"人");
                            });
                        } else{
                            $("#vipCount").html("全部持卡人数："+0+"人");
                        }
                       /* that.loadData.GetCardholderCount(function(data){

                            $("#vipCount").html("全部持卡人数："+data.Data.Count+"人");
                        });*/
                        break;
                    case "optSelect":
                        if(me.data("filed")=="WeChat") {
                            me.addClass("on");
                        }
                        if (me.hasClass("on")) {

                            me.parents(".lineTitle").next().find("." + dataName).show();
                        } else {
                            me.parents(".lineTitle").next().find("." + dataName).hide();
                              var templatePanelList= me.parents(".lineTitle").next().find("." + dataName).find(".templatePanel");
                            if( templatePanelList.length>0) {
                                $.each(templatePanelList, function () {
                                    var obj = $(this).data("message");
                                    if (obj && typeof(obj) == "string") {
                                        obj = JSON.parse(obj)
                                    }
                                    if (obj) {
                                        that.elems.messageDeleteList.push(obj);
                                    }
                                })

                            }
                            templatePanelList.remove();
                        }
                        break;

                }
                if (me.data('type') == "add" && me.hasClass("on")) {
                    var id = "#" + me.data("filed");
                    if ($(id).find(".templatePanel").length == 0) {
                        $(id).find(".textAddBtn").trigger("click");
                    }
                }
                $.util.stopBubble(e);
            });
            $("#win").delegate(".radio","click",function(e){
                debugger;
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                }
                $.util.stopBubble(e);
            }).delegate(".checkBox","click",function(e){
                debugger;
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
            });



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
                    $("body").eq(0).css("overflow-y","auto");
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){
                if ($('#payOrder').form('validate')) {

                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields, that.elems.optionType,function(data){

                        alert("操作成功");
                        $('#win').window('close');
                        that.loadDataPage();
                    });
                }
            }).delegate(".listBtn","click",function(){
                debugger;
                var me=$(this);
                me.toggleClass("show");
            }).delegate(".optDom", "click", function () {   //刷新优惠券+
                that.loadData.GetCouponTypeList(function (listData) {
                    debugger;
                    that.renderCouponList(listData,that.editData);
                });
            }) .delegate(".lineCouponInfo .del","click",function(){   //删除优惠券+
                debugger;
                var rowIndex=-1,dom=$(this).parents(".lineCouponInfo"),dataRow=dom.data("item");
                rowList= $("#couponList").datagrid('getRows');
                if(rowList.length>0){   //uncheckRow
                    $.each(rowList,function(index,row){
                        if(dataRow.CouponTypeID==row.CouponTypeID){
                            $("#couponList").datagrid("uncheckRow",index);
                            dom.remove();
                            rowIndex=index;
                            return false;
                        }
                    })
                }
                if(rowIndex==-1){
                    dom.remove();
                }
                $("#couponListSelect").find(".lineCouponInfo").each(function(index){
                    $(this).find("i").html(index+1);
                });
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /*时间控件不能选择当前之前日期*/
            $('#BeginDate').datebox().datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    return d1 <= date;
                }
            });

            $("#name").datebox().datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    return d1 <= date;
                }
            });


            that.elems.tagPanel.delegate(".deletebtn", "click", function () {
                $(this).parents(".templatePanel").remove();
                var obj=$(this).parents(".templatePanel").data("message");
                if(obj&&typeof(obj)=="string"){
                       obj=JSON.parse(obj)
                }
                if(obj) {
                    that.elems.messageDeleteList.push(obj);
                }
            });


        },
        //新版添加优惠券
        addCouponList:function(data){
            debugger;
            var that=this;
            that.elems.optionType="addCoupon";
            var top=$(document).scrollTop()+10;
            $("body").eq(0).css("overflow-y","hidden");

            var left=$(window).width() - 920>0 ? ($(window).width() -920)*0.5:80;
            $('#win').window({title: "选择礼品", width: 920, height: 665, top: top, left: left});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tql_addCouponList');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');


            that.loadData.GetCouponTypeList(function(listData){
                debugger;
                that.renderCouponList(listData,data);
                debugger;
                that.setCouponList(data)
            });

        },
        renderCouponList:function(data,editData){
            var that=this, isSubMit=false;
            $("#couponList").datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.CouponTypeList,
                sortName : 'CouponTypeID', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'CouponTypeID', //主键字段
                /*  pageNumber:1,*/
                frozenColumns : [ [ {
                    field : 'CouponTypeID',
                    checkbox : true
                } //显示复选框
                ] ],

                columns : [[

                    {field : 'CouponTypeName',title : '优惠券名称',width:300,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div><p>'+row.ValidityPeriod+'</p>';
                            }else{
                                return '<div class="rowText">'+value+'</div><p>'+row.ValidityPeriod+'</p>';
                            }
                        }
                    }



                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    $("#couponList").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        $("#dataMessage").hide();
                    }else{
                        $("#dataMessage").show();
                    }
                    // $(".dataGridPanel .datagrid-header").hide();
                    that.editData=editData;
                    if(editData&&editData.length>0&&data.rows.length>0){   //找到匹配的券然后选中。
                        $.each(editData,function(index,editRow){
                            $.each(data.rows,function(rowIndex,row){
                                if(editRow.CouponTypeID==row.CouponTypeID){
                                    $("#couponList").datagrid('checkRow',rowIndex)
                                }
                            })


                        })
                    }


                    isSubMit=true
                },
                onCheck:function(rowIndex,rowData){
                    if(isSubMit) {
                        that.setCouponList()
                    }
                },onUncheck:function(rowIndex, rowData){
                    if(isSubMit) {
                        that.setCouponList([],rowData);
                    }
                }

            });
        },
        setCouponList:function(selectList,rowData){
            var rowList=$("#couponList").datagrid('getChecked');

            var argList=[]; //最终呈现的结果
            if(selectList&&selectList.length>0){ //编辑的时候
                argList= selectList;
            }

            if((!selectList||selectList.length==0)&&rowList.length>0){  //只是选择的时候

                $.each(rowList,function(index,row){
                    rowList[index]["NumLimit"]="";
                });
                argList=rowList;
            }
            if( $("#couponListSelect").find(".lineCouponInfo").length>0&&argList.length>0){
                $.each(argList,function(index,row){
                    $("#couponListSelect").find(".lineCouponInfo").each(function(){
                        debugger;

                        var   CouponTypeID=$(this).data("couponid"),number=$(this).find(".easyui-numberbox").numberbox("getValue");
                        if( row.CouponTypeID==CouponTypeID){
                            $(this).remove();         //防止重复添加
                            argList[index]["NumLimit"]=number;
                        }
                        if(rowData&&rowData.CouponTypeID==CouponTypeID){
                            $(this).remove();         //防止移除失效
                        }
                    })
                });
            }
            if(argList.length==0&&$("#couponListSelect").find(".lineCouponInfo").length>0){ //表格取消最后一个验证时候触发
                if(rowData){
                    $("#couponListSelect").find(".lineCouponInfo").each(function(){
                        var   CouponTypeID=$(this).data("couponid"),number=$(this).find(".easyui-numberbox").numberbox("getValue");
                        if(rowData&&rowData.CouponTypeID==CouponTypeID){
                            $(this).remove();         //防止移除失效
                            return false;
                        }
                    });
                }

            }
            var html=bd.template('tpl_addCoupon', {list:argList,length:$("#couponListSelect").find(".lineCouponInfo").length});
            if($("#couponListSelect").find(".lineCouponInfo").length>0){
                $("#couponListSelect").append(html);
            }else{
                $("#couponListSelect").html(html);
            }

            $("#couponListSelect").find(".easyui-numberbox").numberbox({
                required:true
            });

        },
        //添加优惠券;
        addCoupon:function(data){
            var that=this;
            var top=60;
            top=$(document).scrollTop()+60;

            that.elems.optionType="addCoupon";
            $('#win').window({title:"奖品选择",width:630,height:500,top:top,left:($(window).width()-630)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addTicket',{list:data});
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');

        },
        //添加模板
        addMessageTemplate:function(data){
            var submit={is:true,msg:""};
            if(!this.loadData.args.messageDiv.find('[data-name="day"]').combobox("isValid")){
                submit.is=false;
                submit.msg="请选择一个天数"
            } else
            if(!this.loadData.args.messageDiv.find('[data-name="hour"]').combobox("isValid")){
                submit.is=false;
                submit.msg="请选择一个小时数"
            }
            var top = 60;
            if(submit.is) {
                top = $(document).scrollTop() + 60;
                this.elems.optionType = "addMessage";

                $('#win').window({
                    title: "消息模板",
                    width: 630,
                    height: 500,
                    top: top,
                    left: ($(window).width() - 630) * 0.5
                });
                //改变弹框内容，调用百度模板显示不同内容
                $('#panlconent').layout('remove', 'center');
                var html = bd.template('tpl_addTemplate', {list: data});
                var options = {
                    region: 'center',
                    content: html
                };
                $('#panlconent').layout('add', options);
                $('#win').window('open');
            }else{
                $.messager.alert("提示",submit.msg);
            }
        },
        //初始化加载的页面数据
        loadDataPage:function() {
            var that = this;
            that.loadData.getLevelList(function (data) {    //idIsPrepaid	int	是否可充值 （1=是，0=否）
                console.log("执行");
                if (data.Data && data.Data.SysVipCardTypeList && data.Data.SysVipCardTypeList.length > 0) {
                    //升级条件（1=购卡升级、0=充值升级、2=消费升级）
                    debugger
                    $("#levelList").html("");
                    $.each(data.Data.SysVipCardTypeList, function (index, value) {

                        if (value.idIsPrepaid) {   // IsBuyUpgrade	Int	是否消费升级
                            value["isPrePaid"] = 1
                        } else {
                            value["isPrePaid"] = 0
                        }

                        var html = bd.template("tpl_levelItem", value);
                        $("#levelList").append(html);
                    });
                }
                if (that.loadData.args.ActivityID) {
                    that.loadData.getActivityDeatil(function (data) {
                        window.StartTime = data.Data.StartTime;
                        if (data.Data && data.Data.IsLongTime == 1) {
                            if (!$('.checkBox[data-filed="IsLongTime"]').hasClass('on')) {
                                $('.checkBox[data-filed="IsLongTime"]').trigger("click");
                            }       data.Data.EndTime="";

                        } else if (data.Data && data.Data.IsLongTime == 0) {
                            if ($('.checkBox[data-filed="IsLongTime"]').hasClass('on')) {
                                $('.checkBox[data-filed="IsLongTime"]').trigger("click");
                            }

                        }
                        if(data.Data&&data.Data.Status==1){

                            $("#BeginDate").datebox({disabled: true});
                            $("#BeginDate").siblings(".datebox").eq(0).addClass("bgColor");

                        }
                        that.loadData.args.ActivityType= data.Data.ActivityType;
                        $('[data-name="ActivityType"]').each(function() {

                            if ($(this).data("value") == data.Data.ActivityType) {
                                $(this).addClass("on").trigger("click");
                            } else {
                                $(this).removeClass("on");
                            }
                        });

                        debugger;
                       if(data.Data.IsAllCardType){
                           if( !$('[data-type="all"]').hasClass("on")) {
                               $('[data-type="all"]').trigger("click");
                           }

                       }else if(data.Data.VipCardTypeID&&data.Data.VipCardTypeID.length>0) {
                           that.loadData.args.VipCardTypeID=data.Data.VipCardTypeID;
                           $.each(data.Data.VipCardTypeID,function(index,Id){
                               $('[data-name="IsAllCardType"]').each(function(){
                                          if($(this).data("id")==Id){
                                              $(this).addClass("on");
                                              console.log(Id)
                                          }
                               });
                           });
                       }
                        $("#vipCount").html("全部持卡人数："+data.Data.HolderCardCount);
                        if (data.Data.PointsMultiple) {
                            if (!$('.checkBox[data-filed="PointsMultiple"]').hasClass('on')) {
                                $('.checkBox[data-filed="PointsMultiple"]').trigger("click");
                            }
                        } else {
                            if ($('.checkBox[data-filed="PointsMultiple"]').hasClass('on')) {
                                $('.checkBox[data-filed="PointsMultiple"]').trigger("click");
                            }
                        }
                        debugger;

                        that.elems.PrizesInfoList = data.Data.PrizesInfoList;
                        that.elems.prizesCache = [];
                        $("[data-prizestype]").each(function () {      //获取所有选择券的按钮，绑定奖品id和
                            var domBtn = $(this);
                            if (data.Data.PrizesInfoList && data.Data.PrizesInfoList.length > 0) {
                                $.each(that.elems.PrizesInfoList, function (index, filed) {
                                    if (domBtn.data("prizestype") == filed.PrizesType) {
                                        domBtn.data("prizesid", filed.PrizesID);
                                        that.elems.prizesCache.push({
                                            type: filed.PrizesType,
                                            "list": filed.PrizesDetailList
                                        });
                                        domBtn.parents(".checkBoxPanel").data("list", filed.PrizesDetailList);
                                        domBtn.parents(".checkBoxPanel").data("prizesid", filed.PrizesID);
                                        domBtn.parents(".checkBoxPanel").data("prizestype", filed.PrizesType);
                                        that.renderTable(filed.PrizesDetailList, domBtn.parents(".checkBoxPanel").siblings().find(".gridTable"));

                                        return false;
                                    }
                                });
                            }
                        });
                        $(".optSelect").find(".templatePanel").remove();
                        if (data.Data.ActivityMessageInfoList && data.Data.ActivityMessageInfoList.length > 0) {

                            $.each(data.Data.ActivityMessageInfoList, function (index, filed) {
                                debugger;
                                var type = $.trim(filed.MessageType);
                                var select = "#" + type;
                                $(select).find(".templatePanel:last").removeClass("borderNone");

                                filed["day"] = $.util.GetDateDiff(window.StartTime, new Date(filed.SendTime).format("yyyy-MM-dd"), "day");
                                filed["hour"] = new Date(filed.SendTime).format("hh");
                             /*   filed["day"]=AdvanceDays;
                                filed["hour"]=SendAtHour;*/
                                debugger;
                                filed["ActivityType"]=$('.radio[data-name="ActivityType"].on').data("value");
                                   if(filed.Content){
                                               var ContentLength=filed.Content.split(";");
                                       filed["activityName"]=ContentLength[0];
                                       if(ContentLength.length>1){
                                           filed["brandName"]=ContentLength[1];
                                       }
                                       if(that.loadData.args.ActivityType==1&&!filed["brandName"]){
                                           filed["brandName"]=ContentLength[0];
                                       }
                                       if(ContentLength.length==1&&that.loadData.args.ActivityType!=1){  //从生日切换到其他项目
                                           filed["activityName"]=""
                                           filed["brandName"]=ContentLength[0];
                                       }

                                   } else{
                                       filed["activityName"]="";
                                       filed["brandName"]="";
                                   }


                                var html = bd.template('tpl_addMessageExit', {item: filed});
                                $(select).append(html);
                                $(select).find(".templatePanel:last").find(".easyui-combobox").combobox({
                                    editable:false,
                                })

                                if (filed.MessageID) { //如果有单条消息的删除 IsEnable	1	是否启用（True：启用，false不启用）
                                    if (!$('.checkBox[data-filed="' + type + '"]').hasClass('on')) {
                                        $('.checkBox[data-filed="' + type + '"]').trigger("click");
                                    }else if(type=="WeChat"){
                                        $('.checkBox[data-filed="' + type + '"]').trigger("click");
                                    }
                                } else {
                                    if ($('.checkBox[data-filed="' + type + '"]').hasClass('on')) {
                                        $('.checkBox[data-filed="' + type + '"]').trigger("click");
                                    }
                                }

                                $(select).find(".templatePanel:last").addClass("borderNone");
                                /*var day= $.util.GetDateDiff (filed.SendTime,window.StartTime,"day");
                                 var hour=new Date(filed.SendTime).format("hh")+":00";*/

                            })
                        }/*else{
                            $('.checkBox[data-filed="WeChat"]').trigger("click");
                        }*/
                         $('.checkBox[data-filed="WeChat"]').trigger("click");

                        $("#SMS").find(".templatePanel").find(".easyui-validatebox").validatebox({
                            required:true,
                            validType:'maxLength[16]'
                        });
                        $("#SMS").find(".templatePanel").find(".text p").hide();
                        $("#SMS").find(".templatePanel").find(".text [data-activity='"+data.Data.ActivityType+"']p").show();
                        $("#SMS").find(".templatePanel").find(".text p").find(".easyui-validatebox").validatebox("disableValidation");  //禁用验证
                        $("#SMS").find(".templatePanel").find(".text [data-activity='"+data.Data.ActivityType+"']p").find(".easyui-validatebox").validatebox("enableValidation");
                        if(data.Data.RechargeStrategyInfoList&&data.Data.RechargeStrategyInfoList.length>0){

                            var profitType=data.Data.RechargeStrategyInfoList[0].RuleType;
                            var dom=$("#nav02").find(".tagPanel").eq(0).find(".commonSelectWrap");
                            $("#ProfitType").combobox('select',profitType);

                            var lineListDom=dom.find('[data-name="'+profitType+'"]');
                            lineListDom.html("");
                           var  ruleList=data.Data.RechargeStrategyInfoList;
                            dom.data("rulelist",JSON.stringify(ruleList));
                            debugger;
                            $.each(ruleList,function(i,item){
                                item["type"]=profitType;
                                var html = bd.template("add_lineListData",item);
                                lineListDom.append(html);
                                lineListDom.find(".linetext:last").find(".easyui-numberbox").numberbox({
                                    disabled: false,
                                    required: true
                                });
                            }) ;
                            if( lineListDom.find(".linetext:first").find('[data-name="add"]').length==0&&profitType=="Step"){
                                lineListDom.find(".linetext:first").find('[data-name="del"]').remove();
                                lineListDom.find(".linetext:first").append('<div class="radioBtn r" data-name="add">添加</div>')
                            }
                        }else{
                          /*  if(data.Data.RechargeStrategyInfoList[0].RuleType){

                            };*/
                          $("#ProfitType").combobox('select',"Step");

                        }

                        setTimeout(function () {
                            $('#nav0_2').form('load', data.Data);
                            $('#nav0_1').form('load', data.Data);
                            $(".tooltip ").hide();
                            if(data.Data&&data.Data.Status==1){
                                $("#BeginDate").datebox("setText",data.Data.StartTime);
                            }

                        }, 400)


                    });





                }

            })

        },
        //渲染tabel
        renderTable: function (data,table) {
            debugger;
            var that=this;
            if(!data){

                data=[]
            }
            //jQuery easy datagrid  表格处理
            table.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                /* width: 1000,
                 height : 332,//高度*/
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data,
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'PrizesDetailID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[
                    {field : 'CouponTypeName',title : '券名称',width:100,align:'left',resizable:false

                    } ,
                    {field : 'CouponTypeDesc',title : '券描述',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=52;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,
                    {field : 'NumLimit',title : '赠送数量',width:60,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            if(!value){
                                return 0
                            }else{
                                return value
                            }
                        }
                    } ,
                    {field : 'ValidityPeriod',title : '有效期至',width:60,align:'left',resizable:false},



                    {field : 'PrizesDetailID',title : '操作',width:23,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC delete" data-id='+value+' data-index="'+index+'" data-oprtype="delete"></p>';
                        }
                    }
                ]],
                onLoadSuccess : function(data) {
                    table.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    /* if(data.rows.length>0){
                     that.elems.dataMessage.hide()
                     }else{
                     that.elems.dataMessage.show();
                     }*/
                },
                onClickRow:function(rowindex,rowData){
                    /*   if(that.elems.click) {

                     that.elems.click=true;
                     if(rowData.Status!=3) {
                     var mid = $.util.getUrlParam("mid");
                     location.href = "birthDayDetail.aspx?mid=" + mid + "&ActivityID=" + rowData.ActivityID;
                     }else{
                     $.messager.alert( "提示","活动运行中不可进行编辑操作")
                     }
                     }*/
                },onClickCell:function(rowIndex, field, value){
                    if(field=="PrizesDetailID"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        $.messager.confirm("删除操作","确认要删除该条记录将不再显示",function(r){
                            if(r) {
                                that.loadData.operation(value, "delete", function () {
                                    alert("操作成功");
                                    that.loadDataPage()
                                });
                            }
                        });
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }
            });
        },
        loadData: {
            args: {
                imgSrc:"" ,
                ActivityID:"",
                VipCardTypeID:[],
                VipCardTypeName:'',
                PageIndex:1,
                PageSize:1000,
                PrizesType:1,
                ActivityType:1 //1(生日活动),2(营销活动),3(充值活动）
            },
            getClassifySeach:{
                Status:"1",
                node:"root",
                multiSelect:'',
                isAddPleaseSelectItem:'true',
                pleaseSelectText:"请选择",
                pleaseSelectID:"0",
                bat_id:"1" //1 是商品分类 0是促销分组

            },

            //获取消息模板
            getMessageTemplateList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Basic.MessageTemplate.GetMessageTemplateList',

                        /*PageSize:this.args.PageSize,
                         PageIndex:this.args.PageIndex*/
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取优惠券
            GetCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    beforeSend:function(){
                        $.util.isLoading();
                    },
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        /*IsEffective:true,*/
                        IsServiceLife:1,
                        IsNotLimitQty:1,
                        PageSize:100000,
                        PageIndex:1
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
            //获取持卡人数
            GetCardholderCount:function(callback){
                var fields = $('#nav0_1').serializeArray();
                var prams = {data: {action: "Marketing.Activity.GetCardholderCount"}};
                $.each(fields, function (i, field) {
                    if (field.value != "") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                });
               // prams.data.action = "Marketing.Activity.SetActivity";
                prams.data["IsLongTime"]=$('[data-filed="IsLongTime"].on').length?1:0; //0:不是 1：是
               // prams.data["IsAllCardType"]=$('[data-type="all"].on').length ? 1: 0;    //1是全部  0不是全部
                prams.data["VipCardTypeIDList"]=this.args.VipCardTypeID;
                prams.data["ActivityType"]=$('[data-name="ActivityType"].on').data("value");
                if(!prams.data["StartTime"]){
                    prams.data["StartTime"]=$("#BeginDate").datebox("getText");
                }

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",

                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            operation:function(pram,operationType,callback) {
                debugger;
                var prams = {data: {action: ""}};

                var submit={is:true,msg:""};
                prams.url = "/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法  \
                if(this.args.ActivityID){
                    prams.data.ActivityID = this.args.ActivityID;
                }


                prams.data["ActivityType"]=this.args.ActivityType;
                if(operationType!="delete") {
                    $.each(pram, function (i, field) {
                        if (field.value != "") {
                            prams.data[field.name] = field.value; //提交的参数
                        }
                    });
                }


                switch (operationType){
                    case "SetActivity" :
                        prams.data.action = "Marketing.Activity.SetActivity";
                        prams.data["IsLongTime"]=$('[data-filed="IsLongTime"].on').length?1:0; //0:不是 1：是
                        prams.data["IsAllCardType"]=$('[data-type="all"].on').length ? 1: 0;    //1是全部  0不是全部
                        prams.data["VipCardTypeIDList"]=this.args.VipCardTypeID;
                        prams.data["ActivityType"]=$('[data-name="ActivityType"].on').data("value");
                       if(!prams.data["StartTime"]){
                           prams.data["StartTime"]=$("#BeginDate").datebox("getText");
                       }
                        break;
                    case "addCoupon":
                        prams.data.action="Marketing.Activity.SetPrizes";
                        var obj = [];
                        $("#couponListSelect").find(".lineCouponInfo").each(function () {
                            var item = $(this).data("item");
                            item["NumLimit"] = $(this).find(".easyui-numberbox").numberbox("getValue");
                            obj.push(item);
                        });
                        if(obj.length==0){
                            submit.is=false;
                            submit.msg="请选择一个优惠券。"
                        }else{
                            prams.data["OperationType"]="1";
                            prams.data["PrizesDetailList"]=[];
                            prams.data["PrizesType"]=this.args.PrizesType;
                            prams.data["PrizesID"]=this.args.PrizesID;

                             if(obj.length>0){
                                 $.each(obj,function(inex,item){
                                     prams.data["PrizesDetailList"].push({
                                         PrizesDetailID: item["PrizesDetailID"] ? item["PrizesDetailID"] : "",
                                         CouponTypeID: item["CouponTypeID"],
                                         CouponTypeName: item["CouponTypeName"],
                                         IsEnable: 1,	//int	是否启用1 是启用
                                         NumLimit: item["NumLimit"]

                                     });
                                 })
                             }


                        }
                        var cacheList = [];
                        $.each(page.elems.prizesCache, function (index, filed) {
                            if (filed.type == page.loadData.args.PrizesType) {
                                cacheList = filed.list;
                            }
                        });
                        $.each(cacheList,function(cacheIndex,cacheItem){
                            var isDelet=true
                            $.each(prams.data["PrizesDetailList"],function(index,item){
                                 if(cacheItem.CouponTypeID==item.CouponTypeID){  //如果是同一张券，就认为是修改
                                     prams.data["PrizesDetailList"][index]["PrizesDetailID"]= cacheItem["PrizesDetailID"];
                                     isDelet=false;
                                     return false;
                                 }
                            });
                            if(isDelet){      //将需要删除的添加进去
                                prams.data["PrizesDetailList"].push({
                                    PrizesDetailID: cacheItem["PrizesDetailID"],
                                    CouponTypeID: cacheItem["CouponTypeID"],
                                    CouponTypeName: cacheItem["CouponTypeName"],
                                    IsEnable: 0,	//int	是否启用1 是启用
                                    NumLimit: cacheItem["NumLimit"]

                                });
                            }
                        });
                        break;
                    case "addMessage":
                        prams.data.action="Marketing.Activity.SetActivityMessage";

                        var dom=$("#win").find(".radio.on");
                        if(dom.length==0){
                            submit.is=false;
                            submit.msg="请选择一个模板。"
                        }else{
                            prams.data["OperationType"]="1";
                            debugger;
                            var messageInfo=this.args.messageDiv.data("message");
                            if(messageInfo.MessageID){
                                submit.is=false;
                                submit.msg="";
                                //messageInfo.TemplateID =dom.data("id");
                                this.args.messageDiv.data("message",messageInfo) ;
                                this.args.messageDiv.find("textarea").html(dom.data("forminfo").Content);
                                $('#win').window('close');

                                alert( "操作成功");
                            }

                            prams.data["ActivityMessageList"]=[
                                {"MessageID":messageInfo.MessageID,"MessageType":$.trim(messageInfo.MessageType),/*"TemplateID":dom.data("id"),*/
                                    "Content":dom.data("forminfo").Content,
                                    "SendTime":this.args.messageDiv.data("starttime"),IsEnable:1
                                }
                            ];

                        }
                        break;
                    case "SetActivityMessage":
                        prams.data.action="Marketing.Activity.SetActivityMessage";


                        prams.data["OperationType"]="2";

                        if ($('#nav0_3').form('validate')) {    //easy_ui 控件验证 是否通过

                            prams.data["ActivityMessageList"] = [];
                            var selectInput=".text [data-activity='{0}']".format(page.loadData.args.ActivityType);
                            $(".templatePanel").each(function(){
                                var me=$(this),messageInfo=me.data("message");
                                debugger;

                                 /*if(messageInfo.MessageType=="SMS") {  //textarea   不存在了所以注释防止启用
                                     if (me.find("textarea").val()) {

                                     } else {

                                         submit.is = false;
                                         if (messageInfo.MessageType == "SMS") {
                                             submit.msg = "短信设置,第" + (me.index()) + "项未设置发送信息";
                                         } else if (messageInfo.MessageType == "WeChat") {
                                             submit.msg = "微信设置,第" + (me.index()) + "项未设置发送信息";
                                         }

                                         return false;
                                     }
                                 }*/
                                  var Content="";
                                if(me.find(selectInput).find("input").length==1){
                                    Content= $.trim(me.find(selectInput).find("input").val())
                                }
                                if(me.find(selectInput).find("input").length==2){
                                    Content= $.trim(me.find(selectInput).find("input").eq(0).val())+";"+$.trim($(selectInput).find("input").eq(1).val())
                                }
                                debugger


                                if(!(messageInfo.MessageType == "WeChat"&&prams.data["ActivityType"]==3)) {   //充值满赠的微信消息是没有时间的实时的，时间为空，不需要验证
                                    var date = new Date(Date.parse(me.data("starttime").replace(/-/g, "/")));
                                    var now = new Date();
                                    var hour = me.find('[data-name="hour"]').combobox("getValue")
                                    if (hour !== "00") {
                                        date.setHours(date.getHours() + 1);
                                    }
                                    if (date < now) {
                                        var messageTypeName = "";
                                        if (messageInfo.MessageType == "SMS") {
                                            messageTypeName = "短信设置";
                                        } else if (messageInfo.MessageType == "WeChat") {
                                            messageTypeName = "微信设置";
                                        }
                                        submit.is = false;
                                        submit.msg = messageTypeName + ",第" + me.index() + "项的消息发送时间是：<br/>" + me.data("starttime") + "<br/> 时间已过消息无法发送。";
                                    }
                                }
                                if(!(prams.data["ActivityType"]==3&&messageInfo.MessageType=="WeChat")){
                                    prams.data["ActivityMessageList"].push({
                                        "MessageID":messageInfo.MessageID,//消息ID
                                        "MessageType": $.trim(messageInfo.MessageType),//发送消息的方式
                                        //"TemplateID":messageInfo.TemplateID,//消息模板的id
                                        AdvanceDays: me.find('[data-name="day"]').combobox("getValue"),
                                        SendAtHour: me.find('[data-name="hour"]').combobox("getValue"),
                                        "Content": Content,//发送信息内容
                                        "SendTime": me.data("starttime"),//发送时间
                                        "IsEnable":1 //$('.on[data-filed="'+$.trim(messageInfo.MessageType)+'"]').length  //是否启用选择框 是否勾
                                    })
                                }


                            }) ;
                            if(page.elems.messageDeleteList.length>0){
                                $.each(page.elems.messageDeleteList,function(){
                                    var me=this;
                                    me["IsEnable"]=0;
                                    prams.data["ActivityMessageList"].push(me);
                                })
                            }


                        }else{
                            submit.is=false;
                            $(".templatePanel").each(function(index) {
                                var me = $(this),  messageInfo=me.data("message");
                                var messageTypeName="";
                                if(messageInfo.MessageType=="SMS"){
                                    messageTypeName="短信设置";
                                }else if(messageInfo.MessageType=="WeChat"){
                                    messageTypeName="微信设置";
                                }

                                    if (!me.find('[data-name="day"]').combobox("isValid")) {
                                        submit.is = false;
                                        submit.msg = messageTypeName + ",第" + me.index() + "请选择一个天数";
                                        return false;
                                    } else if (!me.find('[data-name="hour"]').combobox("isValid")) {
                                        submit.is = false;
                                        submit.msg = messageTypeName + ",第" + me.index() + "请选择一个小时数";
                                        return false;
                                    }

                            });
                        }

                        break;
                    case "delete" :
                        prams.data.action = "Marketing.Activity.DelPrizesDetail";
                        prams.data["PrizesDetailID"]=pram;
                        break;
                    case  "SetPrizes" :
                        prams.data.action="Marketing.Activity.SetRechargeStrategy";
                        prams.data["OperationType"]="2";
                        prams.data["PrizesInfoList"]=[ ];
                        /*{"PrizesType":"","PrizesID":"","IsEnable":"","AmountLimit":"",
                         "PrizesDetailList":[]
                         }*/

                        $(".checkBoxPanel").each(function(){
                            var me=$(this);
                            if($(this).data("prizesid")){
                                var IsEnable=me.find('.checkBox.on[data-name="optSelect"]').length>0;
                                prams.data["PrizesInfoList"].push({"PrizesType":$(this).data("prizestype"),"PrizesID":$(this).data("prizesid"),"IsEnable":IsEnable,"AmountLimit":null,"PrizesDetailList":[]})
                            }
                        }) ;
                        break

                }
                debugger;
                if(!submit.is){
                    if(submit.msg) {
                        $.messager.alert("提示", submit.msg);
                    }
                    return false
                }
                $.util.ajax({
                    url: prams.url,
                    data: prams.data,
                    beforeSend: function () {
                        $.util.isLoading()

                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            },
            getActivityDeatil:function(callback){
                $.util.ajax({
                    url: " /ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Marketing.Activity.GetActivityDeatil',
                        ActivityID:this.args.ActivityID
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });

            },
            getHolidayList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"Basic.Holiday.GetHolidayList",
                        PageSize:100000,
                        PageIndex:1

                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("加载异常请联系管理员");
                        }
                    }
                });
            },
            getLevelList:function(callback){   //获取会员卡等级
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'VIP.VipGold.GetSysVipCardTypeList',
                        IsOnLineSale:-1,
                    },
                    beforeSend: function () {
                        // $.util.isLoading()

                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
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
function  onSelectDay(record){
    if(record.value!==0) {
        var me = $(this).parents(".templatePanel"), meDom = this;
        var date = new Date(window.StartTime);
        window.startHour= me.find('[data-name="hour"]').combobox("getValue");
        date.setDate(date.getDate() - parseInt(record.value));
        var str = window.startHour!=="0" ? date.format("yyyy-MM-dd") + " " + window.startHour+":00" : date.format("yyyy-MM-dd");
        $(this).parents(".templatePanel").data("starttime", str);
        console.log($(this).parents(".templatePanel").data("starttime"));
    }
}
function  onSelectHour(record){
    if(record.value!==0) {
        window.startHour = record.value+":00";
        var str = $(this).parents(".templatePanel").data("starttime");
        str = new Date(str).format("yyyy-MM-dd");
        $(this).parents(".templatePanel").data("starttime", str + " " + record.value+":00");
        console.log($(this).parents(".templatePanel").data("starttime"));
    }
}

