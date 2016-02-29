define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
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
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {

            this.initEvent();


        },
        initEvent: function () {
            var that = this;
              /*that.loadData.GetTagsByTypeName(function(data){
                  $("#ageGroup").combobox({
                      valueField:"TagsID" ,
                      textField :"TagsName",
                       data:data.Data.TagsList
                  });
              });*/
           /* that.loadData.search_user(function(data){
                $("#vipName").combobox({
                    valueField:"User_Id" ,
                    textField :"User_Name",
                    data:data.topics
                });
            });*/

            // 所在地联动事件
        /*    that.loadData.GetVipAddressList(function(data){
                if(data.content&&data.content.provinceList) {
                    data.content.provinceList.unshift({"CityID":"-1","Province":"请选择"});
                    $("#province").combobox({
                        data:data.content.provinceList,
                        valueField:'CityID',
                        textField:'Province',
                        onLoadSuccess:function(){
                            $(this).combobox("setValue",data.content.provinceList[0].CityID);

                        },onSelect:function(record){
                            if(record&&record.Province&&record.CityID!=-1){
                                that.loadData.GetCityByProvince(function(data){
                                    debugger;
                                    if(data.content&&data.content.cityList) {
                                        data.content.cityList.unshift({"CityID":"-1","CityName":"请选择"});
                                        $("#city").combobox({
                                            data:data.content.cityList,
                                            valueField:'CityID',
                                            textField:'CityName',
                                            onLoadSuccess:function(){
                                                $(this).combobox("setValue",data.content.cityList[0].CityID);

                                            },onSelect:function(record){
                                                //县
                                                if(record&&record.CityID&&record.CityID!=-1){
                                                    that.loadData.GetDistrictsByDistricID(function(data){
                                                        debugger;
                                                        if(data.content) {
                                                            data.content.unshift({"DistrictID":"-1","Name":"请选择"});
                                                            $("#area").combobox({
                                                                data:data.content,
                                                                valueField:'DistrictID',
                                                                textField:'Name',
                                                                onLoadSuccess:function(){
                                                                    $(this).combobox("setValue",data.content[0].DistrictID);
                                                                }
                                                            });
                                                        }
                                                    },record.CityID);
                                                }


                                            }
                                        });
                                    }
                                },record.Province);
                            }

                        }
                    });
                }
            });*/

            that.elems.sectionPage.delegate(".checkBox","click",function(e) {
                var me = $(this)
                me.toggleClass("on");
                if(!me.hasClass("on")) {
                    me.siblings().find(".easyui-validatebox").attr({"disabled":"disabled"});
                    me.siblings().find(".easyui-validatebox").parent().addClass("bgColor");
                }else{

                    me.siblings().find(".easyui-validatebox") .removeAttr("disabled");
                    me.siblings().find(".easyui-validatebox").parent().removeClass("bgColor");

                }
            }).delegate(".radio","click",function(e){
                debugger;
                var me= $(this), name= me.data("name");

                if(name) {
                    var selector = "[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                }
                $.util.stopBubble(e);
            }).delegate(".optBtn","click",function(){

              var  isSubmit=true;
                if ($('#createVipCard').form('validate')) {
                    var fields = $('#createVipCard').serializeArray(); //自动序列化表单元素为JSON对象
                    $.each( fields,function(index,filed){
                        if(filed.name=="TagsID"){
                            if(!filed.value){
                                $.messager.alert("操作提示","请选择一个年龄段");
                                isSubmit= false;
                                return false
                            }
                        }
                        if(filed.name=="SalesUserId"){
                            if(!filed.value){
                                $.messager.alert("操作提示","请选择一个售卡员工");
                                isSubmit= false;
                                return false
                            }
                        }

                    });
                    if(isSubmit) {
                        that.loadData.operation(fields, "save", function (data) {
                            debugger;
                            location.href = "/module/newVipManage/VipDetail.aspx?VipCardId="+data.Data.VipCardID+"&mid=" + $.util.getUrlParam("mid");

                        });
                    }

                }
            });

            that.elems.sectionPage.find(".checkBox").trigger("click").trigger("click");
            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                onBeforeOpen:function(){
                   that.elems.isClose=true;
                    $("#VipCardISN").focus();
                },
                onOpen:function(){

                    $("#VipCardISN").focus();
                },
               /* closed:true,
                closable:true*/
                onClose:function(){
                    if(that.elems.isClose){
                        /*var mid = JITMethod.getUrlParam("mid"),PMenuID = JITMethod.getUrlParam("PMenuID");

                        location.href = "/module/newVipManage/querylist.aspx?mid=" +mid+"&PMenuID="+PMenuID;*/
                        $.util.toNewUrlPath("/module/newVipManage/querylist.aspx");

                    }

                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){

                if ($('#payOrder').form('validate')) {

                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,"select",function(data){


                        if(data.Data&&data.Data.VipCardID) {
                            that.elems.isClose=false;
                            that.loadData.args.VipCardID=data.Data.VipCardID;
                            that.loadData.args.VipCardCode=data.Data.VipCardCode;

                            //0未激活，1正常，2冻结，3失效，4挂失，5休眠
                            var str="存在异常";
                            var  isCreation=false;
                            switch ( data.Data.VipCardStatusId) {

                                case 0:str="未激活";isCreation=true;break;
                                case 5:str="休眠";break;
                                case 1:str="激活";break;
                                case 2:str="冻结";break;
                                case 3:str="失效";break;
                                case 4:str="挂失";break;

                            }


                            if(!isCreation){
                                $.messager.alert("查询提示","该卡已"+str+"，请更换卡号后重新尝试","",function(){
                                    $("#VipCardISN").focus();
                                });

                            }else if(data.Data.MembershipUnitName){
                               $.messager.alert("查询提示","该卡已在"+data.Data.MembershipUnitName+"开过卡，只是未激活","",function(){
                                   $("#VipCardISN").focus();
                               });
                                isCreation=false;
                           }
                             if(isCreation) {
                                 data.Data.MembershipUnit = window.UnitName;
                                 data.Data.VipCardStatusId=str;
                                 $('#loadForm').form('load', data.Data);
                                 $("#win").window("close");
                             }
                        }else{
                            $.messager.alert("查询提示","系统内不存在该卡，请更换卡号后重新尝试","",function(){
                                $("#VipCardISN").focus();
                            });
                        }

                    });

                }
                $.util.stopBubble(e);
            });
            $("body").delegate("input[name='VipCardISN']","keydown",function(e){

                if(e.keyCode==13){
                    var str=$(this).val().replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"");
                    if(that.elems.vipCardCode) {
                        str= str.replace(that.elems.vipCardCode,"");

                        that.elems.vipCardCode=str;
                    } else{
                        that.elems.vipCardCode=str;
                    }


                    $.util.stopBubble(e);
                    $(this).focus();
                    $(this).val(str);
                    $('#win').find(".saveBtn").trigger("click");
                }

            }).delegate(".window-mask","click",function(){
                $(".messager-button").find(".l-btn-left").trigger("click");
                $("input[name='VipCardISN']").focus()
            });


            var str=""
            $("#birthday").datebox({
                keyHandler:{
                    query:function(q){
                        if(!str) {
                            if ($("#birthday").datebox("isValid")) {
                                var newValue=$("#birthday").datebox('getText');
                                if(newValue.indexOf("-")==-1){
                                    str = newValue.substr(0, 4) + "-" + newValue.substr(4, 2) + "-" + newValue.substr(6, 2);
                                    $('#birthday').datebox('setValue', str);
                                }else{
                                    console.log(str);
                                }
                            }
                        } else{
                            str=""
                        }
                        return false;
                    }
              }
          /*   onChange:function(newValue,oldValue){
             *//*   debugger;
                if(!str) {
                    if ($("#birthday").datebox("isValid")) {
                           if(newValue.indexOf("-")==-1){
                               str = newValue.substr(0, 4) + "-" + newValue.substr(4, 2) + "-" + newValue.substr(6, 2);
                               $('#birthday').datebox('setValue', str);
                           }
                    }
                } else{
                    str=""
                }*//*


            }*/
            });

          /*  document.onkeydown = function(e){
                var ev = document.all ? window.event : e;
                if(window.event.keyCode==13) {

                    window.event.keyCode=0

                }
            }*/
      /*      $('window').keydown(function(e){
                if(window.event.keyCode==13) {

                    window.event.keyCode=0

                }
            });*/
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数





        //刷卡查询
        slotCardQuery:function(){
            debugger;
            var top=60;
            top=$(document).scrollTop()+60;
            $('#win').window({title:"刷卡查询",width:360,height:200,left:($(window).width()-360)/2,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_slotCardQuery');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
              setTimeout(function(){
                  $('#win').window('open');
              },1000000000000)

        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 0,
                PageSize: 6,
                VipCardID:"",    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:1000
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                item_category_id:null,
                SalesPromotion_id:null,
                form:{
                    item_code:"",
                    item_name:"",
                    item_status:null,
                    item_can_redeem:null
                }
            },
            opertionField:{},
            //获取年龄段
            GetTagsByTypeName:function(callback){
                $.util.ajax({
                    url:"/ApplicationInterface/Gateway.ashx",

                    data:{
                        action:"VIP.Tags.GetTagsByTypeName",
                        TypeName:"年龄段"
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


     search_user:function(callback){
         $.util.oldAjax({
             url:"/module/basic/user/Handler/UserHandler.ashx",
             data:{
                 action:"search_user" ,
                 form:{},
                 page:this.args.page,
                 start:this.args.start,
                 limit:this.args.limit
             },success: function (data) {
                 if (data.topics) {
                     if (callback) {
                         callback(data);
                     }

                 } else {
                     alert(data.Message);
                 }
             }
         });
     },
            //获取所有省份
            GetVipAddressList:function(callback){
                $.util.ajax({
                    url:"/OnlineShopping/data/Data.aspx",
                    pathType:"public",
                    data:{
                        action:"getProvince"
                    },
                    success: function (data) {
                        if (data.code=="200") {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

            //根据省份获取城市列表
            GetCityByProvince:function(callback,province){
                $.util.ajax({
                    url:"/OnlineShopping/data/Data.aspx",
                    pathType:"public",
                    data:{
                        action:"getCityByProvince",
                        special:{
                            Province: province //前后端公共接口其它参数必须写入 special中；
                        }

                    },
                    success: function (data) {
                        if (data.code=="200") {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //根据城市获取区域列表
            GetDistrictsByDistricID:function(callback,districtId){
                $.util.ajax({
                    url:"/OnlineShopping/data/Data.aspx",
                    pathType:"public",
                    data:{
                        action:"getDistrictsByDistricID",
                        special:{
                            districtId: districtId //前后端公共接口其它参数必须写入 special中；
                        }

                    },
                    success: function (data) {
                        if (data.code=="200") {
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
                var prams={data:{action:""}},isSubmit=true;
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法



                $.each( pram,function(index,filed){
                    if(filed.name=="VipCardISN"){
                        filed.value=filed.value.replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"")
                    }
                    debugger;
                    prams.data[filed.name]=filed.value;
                });


                switch(operationType){

                    case "select":prams.data.action="VIP.VIPCard.GetVipCardDetail";

                        break;
                    case "save":prams.data.action="VIP.VIPCard.OpenVipCard";  //下架
                        prams.data["VipCardID"]=this.args.VipCardID;
                        prams.data["VipCardCode"]=this.args.VipCardCode;

                        prams.data["IsGift"]=$(".checkBox.on").length>0?0:1; //是否赠送（0=赠送；1=不赠送）

                       if($(".radio.on").length>0){
                           prams.data["Gender"]=$(".radio.on").data("sex");                    // 性别（1=男；2=女）

                       }else{
                           $.messager.alert("操作提示","请选择性别");
                           isSubmit=false;  //以后拓展用
                           return false;
                       }
                        if($('#birthday').datebox('getText')!= prams.data["Birthday"]){
                            prams.data["Birthday"]=$('#birthday').datebox('getText');
                        }

                        break;
                    case "sales":prams.data.action="UpdateSalesPromotion";  //更改促销分组
                        break;
                }

                  debugger;
                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("异常提示",data.Message,"",function(){
                                $("#VipCardISN").focus();
                            });
                        }
                    }
                });
            }


        }

    };
    page.init();
});

