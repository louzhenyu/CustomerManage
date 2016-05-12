define(['jquery','template', 'tools','langzh_CN','easyui', 'artDialog','kkpager','kindeditor'], function ($) {


    //上传图片
    KE = KindEditor;



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

            allData:{} //页面所有存放对象基础数据
        },

        init: function () {
            var ActivityID= $.util.getUrlParam("ActivityID");
            this.loadData.args.ActivityID=  ActivityID;
            this.initEvent();
            var that=this;

            that.elems.section.find(".radio").eq(0).trigger("click");
            $(".checkBox").each(function(){
                $(this).trigger("click").trigger("click");
            });


            this.loadDataPage();
        },
        //动态创建页面元素


        initEvent: function () {
            var that = this;
            that.elems.simpleQuery.find(".panelDiv").fadeOut(0).eq(0).fadeIn("slow");

            /**************** -------------------初始化easyui 控件 start****************/
            var wd=160,H=32;

            $("#nav03").delegate(".textAddBtn","click",function() {
                if ($(select).find(".templatePanel").length < 5) {
                    var type = $(this).data("flag");
                    var select = "#" + type;
                    var submit = {is: true, msg: ""};
                    $(select).find(".templatePanel").each(function (index) {
                        var me = $(this), messageInfo = me.data("message");
                        if ($(this).find(".text textarea").val()) {

                        } else {

                            submit.is = false;
                            if (type == "SMS") {
                                submit.msg = "短信设置,第" + (me.index()) + "项未设置发送信息";
                            } else if (type == "WeChat") {
                                submit.msg = "微信设置,第" + (me.index()) + "项未设置发送信息";
                            }

                            return false;
                        }

                    });


                    if(submit.is) {
                        var messageInfo = {
                            MessageID: "",
                            MessageType: type,
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
                        break;
                    case 1:
                        if ($('#nav0_1').form('validate')) {    //easy_ui 控件验证 是否通过
                            if (issubmit) {  //是否可以提交了
                                var fields = $('#nav0_1').serializeArray(); //自动序列化表单元素为JSON对象
                                that.loadData.operation(fields, "SetActivity", function (data) {
                                   that.loadData.args.ActivityID = data.Data.ActivityID;

                                   that.loadData.GetCardholderCount(function(data){
                                       debugger;
                                      $(".tagPanel .optPanel  .hint").html(that.loadData.args.VipCardTypeName+"持卡人数："+data.Data.Count+"人");
                                   });
                                   //提交成功下一步的操作记录
                                    that.elems.submitBtn.html("下一步");
                                    $(".bgWhite").show();
                                    that.elems.submitBtn.data("submitindex",2); //点击下一步的索引值（根据跳转的第几个索引）
                                    that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                    $(flag).fadeIn("slow");
                                    that.elems.optPanel.find("li").removeClass("on");
                                    me.addClass("on");
                                    that.loadDataPage();
                                });
                            }
                        }
                        break;
                    case 2:
                        if(that.loadData.args.ActivityID&&that.elems.submitBtn.data("submitindex")==2) {
                        //此处验证判断是否是下一步的按钮操作
                            if ($('#nav0_2').form('validate')) {    //easy_ui 控件验证 是否通过
                                var fields = $('#nav0_2').serializeArray(); //自动序列化表单元素为JSON对象
                                that.loadData.operation(fields, "SetPrizes", function (data) {

                                    that.elems.submitBtn.html("确定");
                                    $(".bgWhite").show();
                                    that.elems.submitBtn.data("submitindex", 3);
                                    that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                    $(flag).fadeIn("slow");
                                    that.elems.optPanel.find("li").removeClass("on");
                                    me.addClass("on");
                                });
                            }
                        }else if(!that.loadData.args.ActivityID){
                        //在新增的时候  如果没有创建成功（ActivityID不存在）需要创建
                            if ($('#nav0_1').form('validate')) {    //easy_ui 控件验证 是否通过
                                if (issubmit) {  //是否可以提交了
                                    var fields = $('#nav0_1').serializeArray(); //自动序列化表单元素为JSON对象
                                    that.loadData.operation(fields, "SetActivity", function (data) {
                                        that.loadData.args.ActivityID = data.Data.ActivityID;
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
                    that.loadData.GetCouponList(function (data) {
                        if (data.CouponTypeList && data.CouponTypeList.length > 0) {
                            debugger;
                            var list = data.CouponTypeList;
                            var cacheList = [];
                            $.each(that.elems.prizesCache, function (index, filed) {
                                if (filed.type == that.loadData.args.PrizesType) {
                                    cacheList = filed.list;
                                }
                            });
                            $.each(cacheList, function (index, filed) {
                                $.each(list, function (listIndex, filedList) {
                                    if (filedList.CouponTypeID == filed.CouponTypeID) {
                                        list.splice(listIndex, 1);
                                        return false;
                                    }
                                });

                            });
                            if (list.length > 0) {
                                that.addCoupon(list);
                            } else {
                                $.messager.alert("提示", "您还没有建立优惠券哦，请去优惠券管理中添加！")
                            }

                        }
                        else {
                            $.messager.alert("提示", "没有有效的优惠券，请添加")
                        }
                    });
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
            });
            that.elems.section.delegate("#submitBtn","click",function(e){
                debugger;
                var index=$(this).data("submitindex");
                if(that.elems.optPanel.find("li").length==index){



                    that.loadData.operation("", "SetActivityMessage", function (data) {
                        debugger
                        var mid = $.util.getUrlParam("mid");
                        location.href = "birthDayQueryList.aspx?mid=" + mid;
                    });
                }
                that.elems.optPanel.find("li").eq(index).trigger("click");
                $.util.stopBubble(e);


            }) .delegate(".checkBox","click",function(e){
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
                     case "numberBox":
                         if(me.hasClass("on")) {
                             debugger;
                             $("input[data-name='"+me.data("filed")+"']").numberbox({disabled: false});
                             me.siblings(".numberbox").removeClass("bgColor");

                         }else{
                             $("input[data-name='"+me.data("filed")+"']").numberbox({disabled: true});
                             me.siblings(".numberbox").addClass("bgColor");
                         }
                         break;
                     case "optSelect":
                         if(me.hasClass("on")) {

                             me.parents(".tagPanel").find("."+dataName).show();
                         }else{
                             me.parents(".tagPanel").find("."+dataName).hide();
                             me.parents(".tagPanel").find("."+dataName).find(".templatePanel").remove();

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
           that.loadData.getSysVipCardTypeList(function(data){
               that.elems.vipCardTypeList=data.Data.SysVipCardTypeList;
               data.Data.SysVipCardTypeList.push({ "VipCardTypeID":"-1","VipCardTypeName":"全部"});
               data.Data.SysVipCardTypeList.push({ "VipCardTypeID":"-1000","VipCardTypeName":"请选择","selected":true});
               $("#vipCard").combobox({
                   valueField:'VipCardTypeID',
                   textField:'VipCardTypeName',

                   data:data.Data.SysVipCardTypeList,
                   onSelect:function(record){
                     that.loadData.args.VipCardTypeID=record.VipCardTypeID;
                       that.loadData.args.VipCardTypeName=record.VipCardTypeName
                   }
               });
               $("#vipCard").combobox("setValue","-1000");
           });



            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true
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
            }) ;
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
        loadDataPage:function(){
            console.log("执行");
            var that=this;
            //that.loadData.args.ActivityID = $.util.getUrlParam("ActivityID");
               if(that.loadData.args.ActivityID) {
                   that.loadData.getActivityDeatil(function (data) {
                       window.StartTime=data.Data.StartTime;
                       if (data.Data && data.Data.IsLongTime==1) {
                           if(!$('.checkBox[data-filed="IsLongTime"]').hasClass('on')){
                               $('.checkBox[data-filed="IsLongTime"]').trigger("click");
                           }

                       }else if(data.Data && data.Data.IsLongTime==0){
                           if($('.checkBox[data-filed="IsLongTime"]').hasClass('on')){
                               $('.checkBox[data-filed="IsLongTime"]').trigger("click");
                           }

                       }


                       //加载会员卡类型
                       if(!that.elems.vipCardTypeList){  //如果在事件绑定已经加载成功，此处不执行。
                           that.loadData.getSysVipCardTypeList(function(dataInfo){
                               that.elems.vipCardTypeList=dataInfo.Data.SysVipCardTypeList;
                               dataInfo.Data.SysVipCardTypeList.push({ "VipCardTypeID":"-1","VipCardTypeName":"全部"});
                               dataInfo.Data.SysVipCardTypeList.push({ "VipCardTypeID":"-1000","VipCardTypeName":"请选择","selected":true});
                               $("#vipCard").combobox({
                                   valueField:'VipCardTypeID',
                                   textField:'VipCardTypeName',

                                   data:dataInfo.Data.SysVipCardTypeList,
                                   onSelect:function(record){
                                       that.loadData.args.VipCardTypeID=record.VipCardTypeID;
                                       that.loadData.args.VipCardTypeName=record.VipCardTypeName
                                   }
                               });
                               $("#vipCard").combobox("setValue",data.Data.VipCardTypeID);
                               that.loadData.args.VipCardTypeName= $("#vipCard").combobox("getText")
                           });
                       }else{
                           $("#vipCard").combobox("setValue",data.Data.VipCardTypeID);
                           that.loadData.args.VipCardTypeName= $("#vipCard").combobox("getText")
                       }

                       if(data.Data.PointsMultiple) {
                           if(!$('.checkBox[data-filed="PointsMultiple"]').hasClass('on')){
                               $('.checkBox[data-filed="PointsMultiple"]').trigger("click");
                           }
                       } else{
                           if($('.checkBox[data-filed="PointsMultiple"]').hasClass('on')){
                               $('.checkBox[data-filed="PointsMultiple"]').trigger("click");
                           }
                       }
                       debugger;

                       that.elems.PrizesInfoList = data.Data.PrizesInfoList;
                       that.elems.prizesCache=[];
                       $("[data-prizestype]").each(function () {      //获取所有选择券的按钮，绑定奖品id和
                           var domBtn = $(this);
                           if(data.Data.PrizesInfoList&&data.Data.PrizesInfoList.length>0) {
                               $.each(that.elems.PrizesInfoList, function (index, filed) {
                                   if (domBtn.data("prizestype") == filed.PrizesType) {
                                       domBtn.data("prizesid", filed.PrizesID);
                                       if (filed.PrizesDetailList.length > 0) {
                                           if (!domBtn.parents(".checkBoxPanel").find(".checkBox").hasClass('on')) {
                                               domBtn.parents(".checkBoxPanel").find(".checkBox").trigger("click");
                                           }
                                       } else {
                                           if (domBtn.parents(".checkBoxPanel").find(".checkBox").hasClass('on')) {
                                               domBtn.parents(".checkBoxPanel").find(".checkBox").trigger("click");
                                           }

                                       }
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
                       if(data.Data.ActivityMessageInfoList&&data.Data.ActivityMessageInfoList.length>0){

                           $.each(data.Data.ActivityMessageInfoList,function(index,filed){
                               debugger;
                               var type=$.trim(filed.MessageType);
                               var  select= "#"+type;
                               $(select).find(".templatePanel:last").removeClass("borderNone");

                                filed["day"]= $.util.GetDateDiff (window.StartTime,new Date(filed.SendTime).format("yyyy-MM-dd"),"day");
                                filed["hour"] = new Date(filed.SendTime).format("hh");
                                debugger;
                               var html=bd.template('tpl_addMessageExit',{item:filed});
                               $(select).append(html);
                               if(filed.MessageID) { //如果有单条消息的删除 IsEnable	Bool	是否启用（True：启用，false不启用）
                                   if(!$('.checkBox[data-filed="'+type+'"]').hasClass('on')){
                                       $('.checkBox[data-filed="'+type+'"]').trigger("click");
                                   }
                               } else{
                                   if($('.checkBox[data-filed="'+type+'"]').hasClass('on')){
                                       $('.checkBox[data-filed="'+type+'"]').trigger("click");
                                   }
                               }

                               $(select).find(".templatePanel:last").addClass("borderNone");
                               /*var day= $.util.GetDateDiff (filed.SendTime,window.StartTime,"day");
                               var hour=new Date(filed.SendTime).format("hh")+":00";*/

                           })
                       }

                       setTimeout(function(){
                           $('#nav0_2').form('load', data.Data);
                           $('#nav0_1').form('load', data.Data);
                           $(".tooltip ").hide();
                       },400)


                   });
               }
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
                    {field : 'EndTime',title : '有效期至',width:60,align:'center',resizable:false},

                    {field : 'AvailableQty',title : '可用数量',width:60,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            if(!value){
                                return 0
                            }else{
                                return value
                            }
                        }
                    } ,

                    {field : 'PrizesDetailID',title : '删除',width:23,align:'left',resizable:false,
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
                VipCardTypeID:'',
              VipCardTypeName:'',
                PageIndex:1,
                PageSize:1000,
                PrizesType:1,
                ActivityType:1 //1：生日活动，2：普通活动
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
            //会员卡类型
            getSysVipCardTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'VIP.SysVipCardType.GetSysVipCardTypeList',
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSize
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
            GetCouponList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        /*IsEffective:true,*/
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
               if(this.args.VipCardTypeID==-1){
                   this.args.VipCardTypeID="";
               }
               $.util.ajax({
                   url: "/ApplicationInterface/Gateway.ashx",
                   data:{
                       action:'Marketing.Activity.GetCardholderCount',
                       VipCardTypeID:this.args.VipCardTypeID
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
                          break;
                      case "addCoupon":
                          prams.data.action="Marketing.Activity.SetPrizes";

                          var dom=$("#win").find(".checkBox.on");
                          if(dom.length==0){
                                  submit.is=false;
                                  submit.msg="请选择一个优惠券。"
                          }else{
                              prams.data["OperationType"]="1";
                              prams.data["PrizesInfoList"]=[
                                  {"PrizesType":this.args.PrizesType,"PrizesID":this.args.PrizesID,"IsEnable":true,"AmountLimit":null,
                                  "PrizesDetailList":[]
                                  }
                              ];
                              dom.each(function(){
                                 var  me=$(this);
                                  prams.data["PrizesInfoList"][0].PrizesDetailList.push({"CouponTypeID":me.data("id")})
                              })
                          }
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
                                      "SendTime":this.args.messageDiv.data("starttime"),IsEnable:true
                                  }
                              ];

                          }
                          break;
                      case "SetActivityMessage":
                          prams.data.action="Marketing.Activity.SetActivityMessage";


                              prams.data["OperationType"]="2";

                          if ($('#nav0_3').form('validate')) {    //easy_ui 控件验证 是否通过

                              prams.data["ActivityMessageList"] = [];
                              $(".templatePanel").each(function(){
                              var me=$(this),messageInfo=me.data("message");
                                  if(me.find("textarea").val()){
                                  prams.data["ActivityMessageList"].push({
                                    "MessageID":messageInfo.MessageID,//消息ID
                                    "MessageType": $.trim(messageInfo.MessageType),//发送消息的方式
                                    //"TemplateID":messageInfo.TemplateID,//消息模板的id
                                    "Content": me.find("textarea").val(),//发送信息内容
                                    "SendTime": me.data("starttime"),//发送时间
                                    "IsEnable": $('.on[data-filed="'+$.trim(messageInfo.MessageType)+'"]').length>0  //是否启用选择框 是否勾
                               })

                              }else{

                                  submit.is=false;
                                  if(messageInfo.MessageType=="SMS"){
                                      submit.msg="短信设置,第"+(me.index())+"项未设置发送信息";
                                  }else if(messageInfo.MessageType=="WeChat"){
                                      submit.msg="微信设置,第"+(me.index())+"项未设置发送信息";
                                  }

                                  return false;
                              }

                          }) ;

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
                                      submit.msg = messageTypeName+",第"+me.index()+"请选择一个天数";
                                      return false;
                                  } else if (!me.find('[data-name="hour"]').combobox("isValid")) {
                                      submit.is = false;
                                      submit.msg = messageTypeName+",第"+me.index()+"请选择一个小时数";
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
                          prams.data.action="Marketing.Activity.SetPrizes";
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
                          })
                          break

                  }
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
            }

        }

    };
    page.init();

});
function  onSelectDay(record){
    if(record.value!==0) {
        var me = $(this).parents(".templatePanel"), meDom = this;
        var date = new Date(window.StartTime);
        date.setDate(date.getDate() - parseInt(record.value));
        var str = window.startHour ? date.format("yyyy-MM-dd") + " " + window.startHour : date.format("yyyy-MM-dd");
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

