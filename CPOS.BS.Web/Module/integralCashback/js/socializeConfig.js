define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            contentArea_vipquery:$(".contentArea_vipquery"),
            panlH:116                           // 下来框统一高度
        },
        init: function () {
            this.loadPageData();
            this.initEvent();

             $(".rowL").each(function(){
                var height=$(this).parent().height();
                 $(this).css({"line-height":height+"px"});
             });
        },
        initEvent: function () {
            $('.easyui-numberbox').numberbox({
                width:100,
                height:32
            });
            var that = this;
            that.elems.sectionPage.delegate(".radio","click",function(e){
               var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                }
                $.util.stopBubble(e);
            });

            that.elems.sectionPage.delegate(".checkBox","click",function(e){
                var me= $(this);
                me.toggleClass("on");
                $.util.stopBubble(e);
            });
            that.elems.simpleQueryDiv.delegate(".listBtn","click",function(){
                var me=$(this);

                if(!me.hasClass("show")){
                    that.elems.simpleQueryDiv.find(".listBtn").removeClass("show");
                    me.addClass("show");
                }
               var  showClass="."+me.data("show"),hideClass="."+me.data("hide");

                $(showClass).show(0);
                $(hideClass).hide(0);
            });

            //that.elems.simpleQueryDiv.find(".listBtn").eq(0).trigger("click");

            that.elems.contentArea_vipquery.delegate(".submit","click",function(){
                var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                that.loadData.operation(fields,that.elems.optionType,function(data){

                    alert("操作成功");


                });

            });
        },





        //加载页面的数据请求
        loadPageData: function () {
            var that=this;
            var fileds=$('#optionForm').serializeArray();
            that.loadData.GetCustomerList(function(data){
                debugger;
                var configData=data.requset;
                if(configData.length>0) {
                    var loadData={};
                    for (var i = 0; i < configData.length; i++) {
                        for (var j = 0; j < fileds.length; j++) {
                            if (configData[i].SettingCode == fileds[j].name) {
                                loadData[fileds[j].name]=configData[i].SettingValue;
                            }
                        }


                        if (configData[i].SettingCode == "EnableEmployeeSales") {
                            if (configData[i].SettingValue == 1) {
                                $("[data-flag='EnableEmployeeSales']").addClass("on");
                            }else{
                                $("[data-flag='EnableEmployeeSales']").removeClass("on");
                            }
                        }
                        if (configData[i].SettingCode == "EnableVipSales") {
                            if (configData[i].SettingValue == 1) {
                                $("[data-flag='EnableVipSales']").addClass("on");
                            }else{
                                $("[data-flag='EnableVipSales']").removeClass("on");
                            }
                        }
                        if (configData[i].SettingCode == "SocialSalesType") {
                            var selectr = "[data-socialsalestype='" + configData[i].SettingValue + "']";
                            $(selectr).trigger("click");
                        }
                    }
                    //EnableIntegral 积分  // EnableRewardCash 返现
                    //RewardsType  0 是商品 1订单
                    $('#optionForm').form('load',loadData);
                }


            });
        },


        loadData: {
            args: {

            },
            opertionField:{},

            GetCustomerList: function (callback) {
                $.util.oldAjax({
                    url: "/Module/CustomerBasicSetting/Handler/CustomerBasicSettingHander.ashx",
                    data:{
                        action:"GetCustomerList",
                        mid: $.util.getUrlParam("mid")
                    },
                    success: function (data) {
                        debugger;
                        if (data.data) {
                            if (callback)
                                callback(data.data);
                        }
                        else {
                            alert("分类加载异常请联系管理员");
                        }
                    }
                });
            },
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"Basic.Customer.SetSalesSetting"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.ItemInfoList=[];

                //获取是商品还是订单
                prams.data.SocialSalesType= $("[data-socialsalestype].show").data("socialsalestype");
                if( prams.data.SocialSalesType==0) {           //按订单是0  按商品是1

                    $.each( pram,function(index,filed){
                        if(filed.value!=="") {
                            prams.data[filed.name] = filed.value;
                        }
                    });
                }
                if($("[data-flag].checkBox.on").length>0){
                    $("[data-flag].checkBox").each(function(){
                        var name=$(this).data("flag");
                        if($(this).hasClass('on')){
                            prams.data[name] = 1;
                        }else{
                            prams.data[name] = 0;
                        }
                    })
                }

         /*       switch(operationType){
                    case "putaway":prams.data.action="ItemToggleStatus";  //上架
                        prams.data.Status="1";
                        break;
                    case "soldOut":prams.data.action="ItemToggleStatus";  //下架
                        prams.data.Status="-1";
                        break;
                    case "sales":prams.data.action="UpdateSalesPromotion";  //更改促销分组
                        break;
                }*/


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

