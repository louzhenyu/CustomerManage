define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','kindeditor'], function ($) {
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt'),              //表头分类查询
            optionBtn:$(".optionBtn"),       //操作按钮父类集合
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:150                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var TemplateID= $.util.getUrlParam("TemplateID");
            this.loadData.args.TemplateID=  TemplateID;
            this.initEvent();
            this.loadPageData();
            this.elems.dataMessage.show();

        },
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.GetMessageTemplateDeatile(function(data){

                $('#meassage').form("load",data.Data);
            });
            $.util.stopBubble(e);
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.Add=true;
            that.elems.liClick=true;
            that.elems.sectionPage.delegate("[name='Content']", "blur", function (e) {
                if (that.elems.Add) {
                    that.elems.index = $(this).position();
                    $(this).focus();
                    $(this).position(that.elems.index);
                    $.util.stopBubble(e);
                    that.elems.Add=false;
                }

            }).delegate(".commonBtn li", "click", function (e) {
                that.elems.Add = true;
                that.elems.liClick=false;
                var key=$(this).data("forminfo");
                var index =that.elems.index;
                var str=$("[name='Content']").val().substring(0,index)+key+$("[name='Content']").val().substring(index)
                $("[name='Content']").val(str);
                $(this).parents("ul").hide();
                $("[name='Content']").position(that.elems.index+key.length);
                $.util.stopBubble(e);
                $("[name='Content']").focus();
            }).delegate(".commonBtn", "click", function (e) {
                that.elems.Add = true;
                if(!that.elems.liClick) {
                    that.elems.liClick=true;
                    $(this).find("ul").show();
                }

            }).delegate(".commonBtn", "mouseover", function (e) {
                if(that.elems.liClick) {
                    that.elems.Add = true;
                    $(this).find("ul").show();
                }
            }).delegate(".commonBtn", "mouseout", function (e) {
                    that.elems.Add = true;
                    $(this).find("ul").hide();
            }).delegate(".btnOpt", "click", function () {
               var  type=$(this).data("flag");
                switch (type){
                    case "save":
                        if ($('#meassage').form('validate')) {
                            var fields = $('#meassage').serializeArray(); //自动序列化表单元素为JSON对象
                            that.loadData.operation(fields, "add", function (data) {
                                alert("操作成功");
                                var mid = JITMethod.getUrlParam("mid");
                                location.href = "/module/messageTemplate/internalMessage.aspx?mid=" + mid;
                            });


                        }
                        break
                    case "preview" :
                        var str=$("[name='Content']").val().replace(/#Name#/g,"张三")
                            .replace(/#Card#/g,"黄金卡")
                            .replace(/#Birthday#/g,"1991-02-09")
                            .replace(/#Cash#/g,"300元")
                            .replace(/#Coupon#/g,"优惠券")
                            .replace(/#Point#/g,"16分");
                        if(str.length>0){
                            $.messager.alert("预览",str);
                        }
                        break;
                }


            });
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){

                if(filed.value!=""&&filed.name=="UnitID") {
                     that.loadData.seach[filed.name] = filed.value;
                 }else{
                    that.loadData.seach[filed.name] = filed.value;
                }
            });
        },








        loadData: {
            args: {
                bat_id:"1",
                TemplateID:"",

            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                UnitID:window.UnitID, //门店id
                PhoneList:""


            },
            opertionField:{},


            GetMessageTemplateDeatile: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Basic.MessageTemplate.GetMessageTemplateDeatile',
                        TemplateID: this.args.TemplateID
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

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"Basic.MessageTemplate.SetMessageTemplate"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                if(this.args.TemplateID){
                    prams.data.TemplateID = this.args.TemplateID;
                }

                $.each(pram, function (i, field) {
                    if(field.value!="") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                });

                $.util.ajax({
                    url: prams.url,
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
            }


        }

    };
    page.init();
});




