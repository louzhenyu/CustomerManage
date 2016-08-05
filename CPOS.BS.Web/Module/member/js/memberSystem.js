define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','kindeditor'], function ($) {
    //上传图片
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            optionScheme: "",  //当前被操作的分润方案。
            panlH: 116                           // 下来框统一高度
        },
        detailDate: {},
        selectDataUnit: [], //当前门店操作的缓存选择门店的数据
        notSelectUnit:[],//当前等级不可选门店集合
        selectAllUnit:[],//所有等级选中门店的机会{VipCardTypeID：等级id，VipCardTypeName：等级名称，unitList:已经选择门店集合，ProfitOwner:"员工或者门店"，domIndex：当前的索引等级（确定其唯一性）}}
        // selectDataUnitList:[],
        editData:[],
        CardBuyToProfitRuleIdList: [],     //缓存删除的激励方案
        ValueCard: '',//储值卡号
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                             //标签类型
            tagList: []                              //标签列表
        },
        levelList: [],//商户卡等级数据
        feeSplittingList: [{id: "Step", name: "梯度"}, {id: "Superposition", name: "叠加"}],
     init: function () {
            this.initEvent();
            this.loadPageData();

            this.getVipCardRuleList();
            this.getRewardsSetting();
        },
        releaseRewardsSetting:function(){
            var fields = $('#optionFormSet').serializeArray(); //自动序列化表单元素为JSON对象

            this.loadData.operation(fields,'setRewardsSetting',function(data){
                alert("积分使用规则已生效");
            });
        },

        //获取积分
        getRewardsSetting:function(){
            var fileds=$('#optionFormSet').serializeArray();
            this.loadData.GetCustomerList(function(data){
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

                      /*  if (configData[i].SettingCode == "EnableIntegral") {
                            if (configData[i].SettingValue == 1) {
                                $("[data-flag='EnableIntegral']").trigger("click");
                            } else{
                                $("[data-flag='EnableIntegral']").trigger("click").trigger("click");
                            }
                        }
                        debugger;
                        if (configData[i].SettingCode == "EnableRewardCash") {
                            if (configData[i].SettingValue == 1) {
                                $("[data-flag='EnableRewardCash']").trigger("click");
                            } else{
                                $("[data-flag='EnableRewardCash']").trigger("click").trigger("click");
                            }
                        }
                        if (configData[i].SettingCode == "RewardsType") {
                            var selectr = "[data-rewardstype='" + configData[i].SettingValue + "']";
                            $(selectr).trigger("click");    $("[data-flag='EnableIntegral']").trigger("click").trigger("click");
                            $("[data-flag='EnableRewardCash']").trigger("click").trigger("click");
                        }else{
                            $("[data-rewardstype='0']").trigger("click");
                        }  */
                    }
                    //EnableIntegral 积分  // EnableRewardCash 返现
                    //RewardsType  0 是商品 1订单
                    $('#optionFormSet').form('load',loadData);
                }

            });
        },
        //发布会员激励
        releaseVipCardScheme:function(){
                      var that=this,isSubmit=true;
                 if($("#vipScheme").form("validate")){
                     if($("#unitScheme").form("validate")){
                         var ruleInfoList=[];
                         $("#nav02").find(".scheme").each(function(){
                             var ruleInfoItem={},schemeIndex=$(this).index()+1;
                             // ProfitOwner	String	是	分润拥有者（Employee =员工、Unit =门店）
                            /* if($(this).parents(".nav02panel").data("type")>0){
                                 ruleInfoItem["ProfitOwner"]='Employee';
                             } else{
                                 ruleInfoItem["ProfitOwner"]='Unit';
                             }*/
                             ruleInfoItem["ProfitOwner"]=$(this).data("type");
                             var CardBuyToProfitRuleId=$(this).find(".title [data-filed='CardBuyToProfitRuleId']").data("value");
                           /* if(CardBuyToProfitRuleId) {
                                that.CardBuyToProfitRuleIdList.splice($.inArray(), 1)
                            }*/
                             $(this).find('[data-filed]').each(function(){  //取 不是list类型字段
                                 var name=$(this).data("filed"),value="";
                                 if($(this).hasClass("easyui-combobox")){
                                     value=$(this).combobox("getValue");
                                     if(name=="VipCardTypeID"){
                                         ruleInfoItem["VipCardTypeName"]=$(this).combobox("getText")
                                     }

                                 }else if($(this).hasClass("easyui-numberbox")&&$(this).parents(".linetext").length==0){
                                     value=$(this).numberbox("getValue");
                                 }else if($(this).is("p")){
                                     if($(this).data("value")) {
                                         value= $(this).data("value");
                                     }
                                 }else if($(this).hasClass("radio")){
                                     /*IsApplyAllUnits	int	是	是否应用全部门店（1= 是、0= 否）*/
                                    value= $(this).hasClass("on") ? 1 : 0;



                                 }
                                 if(value) {
                                     ruleInfoItem[name] = value;
                                 }

                             });
                             var selector='[data-name="{0}"].lineList'.format(ruleInfoItem["ProfitType"]);
                             var ProfitTypeInfoList=[] ;
                             $(this).find(selector).find(".linetext").each(function(){    //叠加/梯度数组
                                 var obj={ProfitType:ruleInfoItem["ProfitType"]},value="";
                                 if($(this).data("id")) {
                                     obj["ReRechargeProfitRuleId"] =$(this).data("id");
                                 }
                                 $(this).find('[data-filed]').each(function(){  //取 不是list类型字段
                                     var name=$(this).data("filed");
                                     if($(this).hasClass("easyui-numberbox")){
                                         value=$(this).numberbox("getValue");
                                     }
                                     if(value===""){
                                         value=0
                                     }
                                     obj[name]=value;
                                 });

                                 if(obj["LimitAmount"]||obj["ProfitPct"]){   //都是零或者不填的时候无效。
                                     ProfitTypeInfoList.push(obj);
                                 }

                             });
                           var  ruleList=$(this).find(selector).parents(".commonSelectWrap").data("rulelist");  //取出原来的数据
                             if(typeof (ruleList)==string){
                                 ruleList=JSON.parse(ruleList);
                             }
                             var selectList=[];
                             if(ruleList&&ruleList.length>0){   //验证是否有要删除的数据
                                 $.each(ruleList,function(index,rLItem){
                                       var IsDelete=1;
                                       if(ProfitTypeInfoList.length>0){
                                           $.each(ProfitTypeInfoList,function(i,PTIItem){
                                              if(rLItem.ReRechargeProfitRuleId== PTIItem.ReRechargeProfitRuleId){   //验证ruleList 对象里面的数据是删除还是添加
                                                  IsDelete=0;
                                                  //selectList.push(PTIItem);    //如果需要传递  IsDelete=0  PTIItem["IsDelete"]=0
                                                  //ProfitTypeInfoList.splice(i,1);
                                                  return false;
                                              }
                                           })
                                       }
                                     rLItem["IsDelete"]=IsDelete;
                                      if(IsDelete){    //如果是删除就补录上。
                                          selectList.push(rLItem);
                                      }
                                 })
                             }
                             debugger;
                             if(selectList.length>0){
                                 $.each(selectList,function(index,item){
                                     ProfitTypeInfoList.push(item)
                                 })

                             }

                             ruleInfoItem["ProfitTypeInfoList"]=ProfitTypeInfoList;

                             var nowList=[];//再次存一份数据用于和缓存数据比对  //编辑处理
                             if(ruleInfoItem["IsApplyAllUnits"]){
                                 ruleInfoItem["RuleUnitInfoList"]=[];
                             } else{
                                 var RuleUnitInfoList=[];
                                 if ($(this).data("unitlist")) {
                                       var string=$(this).data("unitlist"), selectDataUnit=[];
                                     if(typeof(string)=="string") {
                                          selectDataUnit = JSON.parse(string);
                                     } else{
                                         selectDataUnit=string;
                                     }
                                     if(selectDataUnit.length>0){
                                         $.each(selectDataUnit, function (i, treeNode) {
                                             if (treeNode.children && treeNode.children.length > 0) {
                                                 $.each(treeNode.children, function (index, children) {
                                                     RuleUnitInfoList.push({Id:"","UnitID":children.id,text:children.text}); //门店会有重复
                                                     nowList.push({Id:"","UnitID":children.id,text:children.text});
                                                 })
                                             }

                                         })
                                     }

                                 }
                                 if(RuleUnitInfoList.length>0) {
                                     ruleInfoItem["RuleUnitInfoList"] = RuleUnitInfoList
                                 }else{
                                     if(ruleInfoItem["ProfitOwner"]=='Employee'){
                                         $("#optList").find("p").eq(0).trigger("click");
                                     } else{
                                         $("#optList").find("p").eq(1).trigger("click");
                                     }
                                     $.messager.alert("提示","存在未选择门店项,请选择第"+schemeIndex+"项的门店");
                                     isSubmit=false;
                                     return false;

                                 }
                             }

                             //缓存的处理
                             var cacheList=[];

                             if ($(this).data("cache")) {
                                 var string = $(this).data("cache");
                                 if (typeof(string) == "string") {
                                     cacheList = JSON.parse(string);
                                 } else {
                                     cacheList = string;
                                 }
                             }
                                 var delList=[];
                             if(cacheList.length>0) {
                                 $.each(cacheList, function (cacheIndex, cacheItem) {  //缓存数据
                                     if (cacheItem.children && cacheItem.children.length > 0) {
                                         $.each(cacheItem.children, function (childrenIndex, children) {
                                             var IsDelete=1;
                                             $.each(nowList, function (nowIndex, nowItem) {
                                                 if (nowItem.UnitID == children.id) {   //找删除数据
                                                     ruleInfoItem["RuleUnitInfoList"][nowIndex]["Id"] = children.MappingUnitId;
                                                     //如果需要传递  IsDelete=0
                                                     // ruleInfoItem["RuleUnitInfoList"][nowIndex]["IsDelete"]=0

                                                     //nowList.splice(nowIndex, 1);
                                                     IsDelete=0;
                                                     return false;
                                                 }
                                             });
                                             if(IsDelete) {   //如果是删除就补录上
                                                 delList.push({
                                                     Id: children.MappingUnitId,
                                                     text: children.text,
                                                     "UnitID": children.id,
                                                     IsDelete: IsDelete
                                                 });
                                             }
                                         });
                                     }

                                 });
                             }
                             if(delList.length>0){
                                 $.each(delList, function (nowIndex, delItem) {
                                     ruleInfoItem["RuleUnitInfoList"].push(delItem);
                                 });
                             }

                           debugger;
                             ruleInfoList.push(ruleInfoItem);
                         });
                         if(that.CardBuyToProfitRuleIdList.length>0){
                             $.each(that.CardBuyToProfitRuleIdList,function(index,value){
                                 ruleInfoList.push({CardBuyToProfitRuleId:value,IsDelete:1});
                             })
                         }
                         if(isSubmit) {
                             var filed = [{name:"CardBuyToProfitRuleInfoList",value:ruleInfoList}];
                             that.loadData.operation(filed, "setVipCardProfitRule", function () {
                                //
                                 that.CardBuyToProfitRuleIdList=[];
                                 that.getVipCardRuleList(function(){
                                     alert("发布成功");
                                 });
                             });
                         }
                     }else{
                         $("#optList").find("p").eq(1).trigger("click");
                         $("#unitScheme").form("validate");

                     }
                 }else{
                      $("#optList").find("p").eq(0).trigger("click");
                     $("#vipScheme").form("validate") ;
                    // alert("当前页面验证数据不通过")
                 }
        },
        //发布会会员等级
        releaseVipCardSystem:function(){
            var VipCardRelateList=[];
             //发布会员卡体系的数据。
            var isSubmit=true;
                 $("#lumpList").find(".lumpList").each(function(){
                     var obj={},me=$(this),VipCardLevel=me.data("level"),VipCardTypeID=-100;
                     if(me.data("id")){
                         VipCardTypeID= me.data("id");
                     }

                     $(this).find(".lump").each(function(){

                         var name=$(this).data("filed"),value=$(this).data("value");
                         if(typeof(value)=="string"){
                             value=JSON.parse(value);
                         }
                         if(name=="VipCardType"){
                             value={IsPrepaid:0,IsOnlineSales:0,VipCardLevel:VipCardLevel,VipCardTypeID:$(this).data("id")};
                             $(this).find('[data-filed]').each(function(){
                                 var name1=$(this).data("filed"),value1="";
                                 if($(this).data("value")){
                                     value1=$(this).data("value");
                                 }
                                 if($(this).is("input")){
                                     value[name1]=$(this).val();
                                     if($.trim($(this).val())==""){
                                         alert("请输入正确等级名称");
                                         isSubmit=false;
                                     }
                                 }else if(name1=="PicUrl"){
                                      if(value1){
                                          value[name1]=value1;
                                      }else{
                                          alert("请上传一张会员卡图片");
                                          isSubmit=false;
                                      }

                                 }else if($(this).hasClass("checkBox")){
                                        value[name1]=$(this).hasClass("on") ? 1 : 0; //是否选中
                                 }
                                 return isSubmit; //如果是false 就暂停循环
                             }) ;
                         }
                         if(name=="VipCardUpgradeRewardList"){
                             var editValue=[];
                             if(typeof($(this).data("editValue"))=="string"){
                                 editValue=JSON.parse($(this).data("editValue"));
                             }
                            if(!value){value=[]}
                                 $.each(value, function (vIndex, vObj) {
                                     var    OperateType=1;

                                     $.each(editValue, function (eIndex, eObj) {  //editValue 为空 value就不动
                                         if (vObj.CardUpgradeRewardId == eObj.CardUpgradeRewardId) {
                                             editValue.splice(eIndex,1);
                                             OperateType=2;   //修改
                                             return false;
                                         }
                                     });
                                     value[vIndex]["OperateType"]= OperateType;
                                     if(OperateType==1&&VipCardTypeID!=-100) {
                                         value[vIndex]["VipCardTypeID"] = VipCardTypeID;
                                     }
                                 });

                                 $.each(editValue, function (eIndex, eObj) {  //editValue 为空 value就不动
                                      eObj["OperateType"]=0; //删除
                                    // eObj["VipCardTypeID"]=VipCardTypeID;
                                     value.push(eObj);
                                 });


                         }

                         debugger
                         if(name=="VipCardUpgradeRule"&&VipCardLevel>1){
                             if(value&&(value.IsPurchaseUpgrade==1||value.IsRecharge==1||value.IsBuyUpgrade==1)){
                                 isSubmit=true;
                             }else{
                                 isSubmit=false;
                                 alert("卡升级条件不可为空")
                             }


                         }
                         if(isSubmit) {
                             obj[name] = value;
                         }else{
                             return false;
                         }
                     });
                     if(isSubmit) {
                         VipCardRelateList.push(obj);
                     }else{
                         return false;
                     }

                 });
             if(isSubmit){
                 debugger;
                 var that=this;
                 var fields=[{name:"VipCardRelateList",value:VipCardRelateList}];
                 that.loadData.operation(fields,"setVipCard",function(){
                   that.loadPageData(function(){
                       that.getVipCardRuleList();
                   });
                     $.messager.alert("发布成功","您的会员体系已生效，快去设置会员推广激励，让店员们努力拓展会员吧！","",function(){
                         scroll(0,50)
                     });
               })
             }

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询


            /**************** -------------------初始化easyui 控件 start****************/
            that.elems.sectionPage.delegate(".uploadPanel.bgCover", "mouseenter", function () {
                $(this).find("p").stop().show(100);
                $(this).find("p").css({"color": '#fff'});

            }).delegate(".scheme .del", "click", function () {
                var me= $(this).parents(".scheme")
                that.optSelectAllUnit(me,[],"del");
                me.remove();
                if($(this).data("value")) {
                    that.CardBuyToProfitRuleIdList.push($(this).data("value"))
                }

            }).delegate("#nav02 .icon_add", "click", function () {
                var index = $("#optList").find(".optBtn.on").index(),type="";
                if(index==1){
                  type="Unit"
                }else{
                   type="Employee"
                }
                var html = bd.template('tpl_addSchemeList',{data:null,type:type});

                $(".nav02panel").eq(index).find(".listScheme").append(html);
                var dom= $(".nav02panel").eq(index).find(".listScheme .scheme:last");
                that.loadData.getLevelList(function (data) {
                    if (data.Data && data.Data.SysVipCardTypeList&&data.Data.SysVipCardTypeList.length>0) {
                        //升级条件（1=购卡升级、0=充值升级、2=消费升级）
                        that.levelList=[];

                        $.each(data.Data.SysVipCardTypeList,function(index,value){
                            if (value.IsPurchaseUpgrade) {   //IsPurchaseUpgrade购买升级
                                value["RefillCondition"] = 1
                            }
                            else if (value.IsRecharge) {   //IsRecharge 是否充值升级
                                value["RefillCondition"] = 0
                            }else if (value.IsBuyUpgrade) {   // IsBuyUpgrade	Int	是否消费升级
                                value["RefillCondition"] = 2
                            }
                            that.levelList.push(value);
                        });
                    }
                    that.initScheme(dom);
                });
            }).delegate(".uploadPanel.bgCover", "mouseleave", function () {
                    $(this).find("p").stop().hide(100)
                }).delegate('[data-filed="VipCardTypeName"]', "change", function () {
                    debugger;
                    if ($(this).parents(".lumpList").data("id") != -100) {
                        $(this).parents(".lump").find(".releasePanel").show();
                    }
                }).delegate('[data-filed="VipCardTypeName"]', "keyup", function () {
                debugger;
                if ($(this).parents(".lumpList").data("id") != -100) {
                    $(this).parents(".lump").find(".releasePanel").show();
                }
            }).delegate(".radio", "click", function (e) {
                    var me = $(this), name = me.data("name");

                    if (name == "unit") {
                           if(me.hasClass("disable")){
                               $.messager.alert("提示","您选择的会员卡的分润激励规则，已经应用到部分门店，不可再选择全部门店。");
                               return false;
                           }
                        var selector = "[data-name='{0}']".format(name);
                        var   VipCardTypeID=  $(this).parents(".scheme").find('[data-filed="VipCardTypeID"]').combobox("getValue")
                        if(!VipCardTypeID){
                            alert("必须选择一个卡等级");
                            return false;
                        }
                        var valueType = me.data("valuetype");

                        me.parents(".scheme").find(selector).removeClass("on");
                        me.addClass("on");
                        if (valueType == "portion") {
                            me.siblings(".radioBtn").show();
                            me.siblings(".radioBtn").trigger("click") ;
                        } else {
                            me.siblings(".radioBtn").hide();
                        }
                    }

                })

                .delegate(".checkBox", "click", function () {
                    $(this).toggleClass("on");
                  var filed=$(this).data("filed")
                    if(filed=="IsPrepaid"||filed=="IsOnlineSales"){
                        if ($(this).parents(".lumpList").data("id") != -100) {
                            $(this).parents(".lump").find(".releasePanel").show();
                        }
                    }

                }).delegate("#optPanel li", "click", function () {
                    var typeid = $(this).data("flag");
                    debugger;
                    $(".navPanel").hide();
                    $("#optPanel li").removeClass("on");
                    $(this).addClass("on");
                    $(typeid).show();

                }).delegate(".release", "click", function () {
                    var type = $("#optPanel li.on").data("flag");
                    switch (type) {
                        case "#nav01":
                            that.releaseVipCardSystem();
                            break;
                        case "#nav02":
                            that.releaseVipCardScheme();
                            break;
                        case "#nav03":
                            that.releaseRewardsSetting( );
                            break;
                    }
                }).delegate(".releaseBtn", "click", function () {
                    var type = $(this).parents(".lump").data("filed"), dom = $(this).parents(".lump"), VipCardTypeID = -100, isSubmit = true;
                    if ($(this).parents(".lumpList").data("id")) {
                        VipCardTypeID = $(this).parents(".lumpList").data("id");
                    }
                    var name = dom.data("filed"), value = dom.data("value");
                    if (typeof(value) == "string") {
                        value = JSON.parse(value);
                    }
                    if (name == "VipCardType") {
                        value = {IsPrepaid: 0, IsOnlineSales: 0};
                        dom.find('[data-filed]').each(function () {
                            var name1 = $(this).data("filed"), value1 = "";
                            if ($(this).data("value")) {
                                value1 = $(this).data("value");
                            }
                            if ($(this).is("input")) {
                                if($(this).validatebox("isValid")) {

                                    value[name1] = $(this).val();
                                    if ($.trim($(this).val()) == "") {
                                        alert("请输入正确等级名称");
                                        isSubmit = false;
                                    }
                                }else{
                                    isSubmit=false;
                                    $(this).focus();
                                }
                            } else if (name1 == "PicUrl") {
                                if (value1) {
                                    value[name1] = value1;
                                } else {
                                    alert("请上传一张会员卡图片");
                                    isSubmit = false;
                                }

                            } else if ($(this).hasClass("checkBox")) {
                                value[name1] = $(this).hasClass("on") ? 1 : 0; //是否选中
                            }
                            return isSubmit; //如果是false 就暂停循环
                        });
                    }
                    if (name == "VipCardUpgradeRewardList") {
                        var editValue = dom.data("editValue");
                        if (typeof(editValue) == "string") {
                            editValue = JSON.parse(editValue);
                        }
                        $.each(value, function (vIndex, vObj) {
                            var OperateType = 1;
                            $.each(editValue, function (eIndex, eObj) {  //editValue 为空 value就不动
                                if (vObj.CardUpgradeRewardId == eObj.CardUpgradeRewardId) {
                                    editValue.splice(eIndex, 1);
                                    OperateType = 2;   //修改
                                    return false;
                                }
                            });
                            value[vIndex]["OperateType"] = OperateType;
                            if (OperateType == 1 && VipCardTypeID != -100) {
                                value[vIndex]["VipCardTypeID"] = VipCardTypeID;
                            }
                        });

                        $.each(editValue, function (eIndex, eObj) {  //editValue 为空 value就不动
                            eObj["OperateType"] = 0; //删除
                            // eObj["VipCardTypeID"]=VipCardTypeID;
                            value.push(eObj);
                        });


                    }

                    if (isSubmit && VipCardTypeID != -100) {
                        var fileds = [];
                        switch (type) {
                            case "VipCardType":   //会员卡
                            case "VipCardUpgradeRule"://升级条件
                            case "VipCardRule":  //基本权益
                                fileds = [{name: "VipCardTypeID", value: VipCardTypeID}, {
                                    name: "OperateType",
                                    value: $(this).parents(".lump").index() + 1
                                }];
                                /*
                                 IsPurchaseUpgrade	Int	是否购买升级
                                 (1=是;0=否;)
                                 IsExtraMoney	Int	是否补差
                                 (1=可补;2=不可补;)
                                 Prices	Decimal	售价
                                 ExchangeIntegral	Int	积分
                                 IsRecharge	Int	是否充值升级
                                 （1=是;0=否;）                   UpGradeType
                                 OnceRechargeAmount	Decimal	单次充值金额
                                 IsBuyUpgrade	Int	是否消费升级
                                 */
                                if (value.IsPurchaseUpgrade) {   //IsPurchaseUpgrade购买升级
                                    value["UpGradeType"] = 1
                                }
                                if (value.IsRecharge) {   //IsRecharge 是否充值升级
                                    value["UpGradeType"] = 2
                                }
                                if (value.IsBuyUpgrade) {   // IsBuyUpgrade	Int	是否消费升级
                                    value["UpGradeType"] = 3
                                }


                                if (value.RuleID) {
                                    fileds.push({name: "OperateObjectID", value: value.RuleID});
                                    console.log(value.RuleID);
                                } else if (value.VipCardUpgradeRuleId) {
                                    fileds.push({name: "OperateObjectID", value: value.VipCardUpgradeRuleId});
                                    console.log(value.VipCardUpgradeRuleId);
                                }


                                $.each(value, function (name, value) {
                                    fileds.push({name: name, value: value})
                                });

                                that.loadData.operation(fileds, "setVipCardInfo", function () {
                                    that.loadPageData(function(){
                                        that.getVipCardRuleList(function(){
                                            alert("发布成功");
                                        });
                                    });

                                });
                                break;
                            case "VipCardUpgradeRewardList": //开卡礼
                                fileds.push({name: "OpeCouponTypeList", value: value});
                                that.loadData.operation(fileds, "setList", function (data) {

                                    alert("发布成功");
                                    that.loadPageData();
                                });

                                break;
                        }
                    }

                })
                .delegate(".lump .addBtnPanel", "click", function () {
                    debugger;
                    var dom = $(this).parents(".lump"), type = dom.data("type");
                    if (type) {
                        switch (type) {
                            case "interests":
                                that.addInterests();
                                break;  //基本权益
                            case "card":
                                that.addCouponList();
                                break;        //开卡礼
                            case "upgrade":
                                that.addUpgrade();
                                break;        //升级条件
                            case "level":
                                that.addLumpList();
                                break;    //新增等级
                        }
                        dom.data("operation", true);
                        that.elems.optionDom = dom;
                    } else {
                        $(this).parents(".lump").data("type", "level");
                        $("#lumpList").show();
                        that.registerUploadImgBtn();
                    }
                })
                .delegate(".lump .edit", "click", function () {
                    debugger;
                    var dom = $(this).parents(".lump"), type = dom.data("type"), data = "";
                    if (dom.data("value")) {
                        data = JSON.parse(dom.data("value"))
                    }
                    if (type) {
                        switch (type) {
                            case "interests":
                                that.addInterests(data);
                                break;  //基本权益
                            case "card":
                                that.addCouponList(data);
                                break;        //开卡礼
                            case "upgrade":
                                that.addUpgrade(data);
                                break;        //升级条件
                            case "level":
                                that.addLumpList(data);
                                break;    //新增等级
                        }
                        if(dom.data("filed")=="VipCardType"){
                            if ($(this).parents(".lumpList").data("id") != -100) {
                                $(this).parents(".lump").find(".releasePanel").show();
                            }
                        }
                        dom.data("operation", true);
                        that.elems.optionDom = dom;
                    }
                    if(dom.data("filed")=="VipCardType"){
                        if ($(this).parents(".lumpList").data("id") != -100) {
                            $(this).parents(".lump").find(".releasePanel").show();
                        }
                    }
                })
                .delegate(".optList p", "click", function () {
                    var index = $(this).parents(".optBtn").index();
                    $(this).parents(".optList").find(".optBtn").removeClass("on");
                    $(this).parents(".optBtn").addClass("on");
                    $(".nav02panel").hide();
                    $(".nav02panel").eq(index).show();
                }).delegate(".radioBtn", "click", function () {
                    if ($(this).data("name") == "unit") {
                        that.elems.optionScheme = $(this).parents(".scheme");
                     var   VipCardTypeID=that.elems.optionScheme.find('[data-filed="VipCardTypeID"]').combobox("getValue")
                        if(!VipCardTypeID){
                            alert("必須一個卡等級");
                            return false;
                        }
                        that.selectDataUnit = [];
                       var unitlistDom= that.elems.optionScheme.data("unitlist");
                        if (unitlistDom&&typeof(unitlistDom)=="string") {

                            that.selectDataUnit = JSON.parse(unitlistDom);
                        } else if(unitlistDom){
                            that.selectDataUnit = unitlistDom
                        }
                        that.selectUnit();
                    }else if($(this).data("name") == "add") {
                        //add_lineList
                        if ($(this).parents(".lineList").find(".linetext").length < 5) {
                            var type=$(this).parents(".commonSelectWrap ").find(".easyui-combobox").combobox('getValue');
                             var index=$(this).parents(".linetext").index();
                            var html = bd.template("add_lineList",{type:type});
                            if($(this).parents(".lineList").find(".linetext").length==0){
                                $(this).parents(".lineList").append(html);
                                $(this).parents(".lineList").find(".linetext:last").find(".easyui-numberbox").numberbox({
                                    disabled: false,
                                    //required: true
                                });
                            }else{
                                $(this).parents(".linetext").after(html);
                                $(this).parents(".lineList").find(".linetext").eq(index+1).find(".easyui-numberbox").numberbox({
                                    disabled: false,
                                    //required: true
                                });
                            }


                            debugger;
                            $(this).parents(".lineList").find(".listBtn").remove();
                        } else {
                            alert("最多可以添加五个梯度")
                        }
                    }else if($(this).data("name") == "del"){
                        var type=$(this).parents(".commonSelectWrap ").find(".easyui-combobox").combobox('getValue'),dom=$(this).parents(".lineList");

                        if(dom.find(".linetext").length==1){
                              if(type=="Step"){

                                  dom.append('<div class="radioBtn l listBtn" data-name="add">新增梯度分润</div>')
                              }else{
                                  dom.append(' <div class="radioBtn l listBtn" data-name="add">新增叠加分润</div>')
                              }
                        }
                        $(this).parents(".linetext").remove();
                        if( dom.find(".linetext:last").find('[data-name="add"]').length==0&&type=="Step"){
                            dom.find(".linetext:last").append('<div class="radioBtn r" data-name="add">添加</div>')
                        }

                    }


                }).delegate(".lumpList .delete", "click", function () {
                    $(this).parents(".lumpList").remove();
                    if($("#lumpList").find(".lumpList").length<7){
                        $("#addLump").show();
                        $("#addLump").parent().show();
                    }
                });

            //#win;弹窗事件
            $('#win').delegate(".searchBtn", "click", function () {
                if (that.elems.optionType == "selectUnit") {
                    that.renderTableUnit();
                }

            })
                .delegate(".radio", "click", function (e) {
                    var me = $(this), name = me.data("name");
                    debugger;
                    var selector = "[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                    if (name == 'upgrade') { //升级条件的弹框处理


                        $(selector).parents(".line").find(".easyui-numberbox").numberbox({
                            disabled: true
                        });
                        $(selector).parents(".line").find(".textbox-text").css({"background": "#efefef"});
                        $(selector).parent(".line").find(".easyui-datebox").datebox({
                            disabled: true
                        });

                        if ($(me).parents(".line").find(".easyui-numberbox").parents(".checkBox").length == 0) {
                            $(me).parents(".line").find(".easyui-numberbox").numberbox({
                                disabled: false
                            });
                        } else {
                            $(me).parents(".line").find(".easyui-numberbox").parents(".checkBox").each(function () {
                                $(this).trigger("click").trigger("click");
                            })
                        }
                        if (me.data("type") == "radio") {  //判断是是必须选中项。
                            if ($(me).parents(".line").find(".easyui-numberbox").parents(".checkBox.on").length == 0) {
                                $(me).parents(".line").find(".easyui-numberbox").eq(0).parents(".checkBox").trigger("click")
                            }
                        }
                    } else if (name == "integral") {  //设置权益积分的弹框处理
                        debugger;
                        if ($(selector).parents(".rowRline").prev().find(".checkBox.on").length > 0) {
                            $(selector).parents(".rowRline").find(".easyui-numberbox").numberbox({
                                disabled: true,
                                required: false
                            });
                            $(selector).parents(".rowRline").find(".textbox-text").css({"background": "#efefef"});
                            $(me).parent().find(".easyui-numberbox").numberbox({
                                disabled: false,
                                required: true
                            });
                            $(me).parent().find(".textbox-text").css({"background": "#fff"});
                        } else {
                            me.removeClass("on");
                        }
                    }
                    $.util.stopBubble(e);
                })
                .delegate(".optDom", "click", function () {   //刷新优惠券+
                    that.loadData.GetCouponTypeList(function (listData) {
                        debugger;
                        that.renderCouponList(listData,that.editData);
                    });
                })
                .delegate(".checkBox", "click", function (e) {
                    var me = $(this), parentRadio = $(me).parents(".line").find(".radio.on");
                    if (parentRadio.length > 0 && parentRadio.data("name") == "upgrade") {
                        me.toggleClass("on");
                        if (me.hasClass("on")) {

                            $(me).find(".easyui-numberbox").numberbox({
                                disabled: false

                            });
                        } else {
                            debugger;
                            $(me).find(".easyui-numberbox").numberbox({
                                disabled: true

                            });
                            $(me).find(".textbox-text").css({"background": "#efefef"});

                        }
                    } else if (me.data("name") == "interests") {
                        debugger;
                        me.toggleClass("on");
                        if (me.hasClass("on")) {
                            me.parent().next().find(".easyui-numberbox").numberbox({
                                disabled: false,
                                required: true
                            });
                            if (me.data("type") == "radio") {  //判断是否有radio按钮，进行二次交互处理。
                                if (me.parent().next().find(".radio.on").length > 0) {
                                    me.parent().next().find(".radio.on").trigger("click");
                                } else {
                                    me.parent().next().find(".radio").eq(0).trigger("click");
                                }
                            }

                        } else {
                            me.parent().next().find(".easyui-numberbox").numberbox({
                                disabled: true
                            });
                            me.parent().next().find(".textbox-text").css({"background": "#efefef"})
                        }
                    }

                })
                .delegate(".checkBox .textbox", "click", function (e) {

                    $.util.stopBubble(e)
                })
                .delegate(".optBtn", "click", function () {
                    var nodeData = $('#unitTreeSelect').tree("getChecked");
                    if (nodeData && nodeData.length > 0) {
                        for (var k = 0; k < nodeData.length; k++) {
                            var childen = nodeData.children;
                            if (!childen) {
                                var id = nodeData[k].id;

                                var nodeList = $("#unitGrid").datagrid('getData').rows;
                                var isDel = true;
                                if (nodeList.length > 0) {
                                    for (var j = 0; j < nodeList.length; j++) {
                                        if (nodeList[j].Id == id) {
                                            isDel = false;
                                            $("#unitGrid").datagrid("uncheckRow", j);
                                        }
                                    }

                                }
                                if (isDel) {
                                    that.reloadUnitData([{id: id}]);
                                }
                            }


                        }


                    }
                })
                .delegate(".saveBtn", "click", function () {
                    if (that.elems.optionType == "selectUnit") {  //选择门店，
                          if(that.selectDataUnit.length>0) {
                              that.elems.optionScheme.data('unitlist', JSON.stringify(that.selectDataUnit));
                              that.optSelectAllUnit( that.elems.optionScheme,that.selectDataUnit,"update");
                              //找到匹配的元素，
                              $("#win").window("close");
                          }else{
                              $.messager.alert("提示","请至少选择一个门店");
                          }

                    } else {
                        if ($("#optionForm").form("validate")) {   //会员体系的操作
                            var obj = {}, optionDom;


                            if (that.elems.optionDom.data("value")) {
                                var objData = JSON.parse(that.elems.optionDom.data("value"));
                                if (objData && objData["RuleID"]) {
                                    obj["RuleID"] = objData["RuleID"];
                                }
                                if (objData && objData["VipCardTypeID"]) {
                                    obj["VipCardTypeID"] = objData["VipCardTypeID"];
                                }
                                if (objData && objData["VipCardUpgradeRuleId"]) {
                                    obj["VipCardUpgradeRuleId"] = objData["VipCardUpgradeRuleId"];
                                }
                            }


                            switch (that.elems.optionType) {
                                case  "upgrade"://升级
                                case  "interests"://权益
                                    var fields = $("#optionForm").serializeArray();

                                    $("#win").find('[data-filed]').each(function () {
                                        debugger;
                                        var me = $(this);
                                        if (me.is("div")) {
                                            obj[me.data("filed")] = me.hasClass("on") ? 1 : 0;

                                        }
                                    });
                                    $.each(fields, function (index, filed) {  //必须反倒选择框录入值以后
                                        debugger;
                                        if (filed.value != "") {
                                            obj[filed.name] = filed.value;
                                        }
                                    });
                                    if (obj["IsExtraMoney"] == 0) {
                                        obj["IsExtraMoney"] = 2
                                    }
                                    if (obj["IsBuyUpgrade"] == 1) {
                                        if (!obj["BuyAmount"] && !obj["OnceBuyAmount"]) {
                                            $.messager.alert("提示", "消费升级,至少选择一种规则。");
                                        }
                                    }
                                    break;

                                case "card": //开卡礼物
                                    obj = [];
                                    $("#couponListSelect").find(".lineCouponInfo").each(function () {
                                        var item = $(this).data("item");
                                        item["CouponNum"] = $(this).find(".easyui-numberbox").numberbox("getValue");
                                        obj.push(item);
                                    });


                                    break;
                            }

                            debugger;
                            that.elems.optionDom.find(".edit").show();
                            that.elems.optionDom.find(".content").html(that.setLumpHtml(that.elems.optionType, obj));
                            that.elems.optionDom.data("value", JSON.stringify(obj));
                            $("#win").window("close");
                            //判断是否发布
                            debugger;
                            if (that.elems.optionDom.parents(".lumpList").data("id") != -100) {
                                if (that.elems.optionDom.find(".releasePanel").length == 0) {
                                    that.elems.optionDom.append(' <div class="releasePanel"><div class="releaseBtn">发布</div></div>')
                                }
                            }
                        }

                    }
                })
                .delegate(".lineCouponInfo .del","click",function(){   //删除优惠券+
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

            /**************** ------------------- 初始化easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true,
                onClose: function () {
                    $("body").eq(0).css("overflow-y", "auto");
                    //  $('#win').window("center")
                },
                onOpen: function () {

                }
            });
            $('#panlconent').layout({
                fit: true
            });

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },

        initScheme:function(dom,callback){
            var that=this;
            if(dom&&dom.length>0) {
                dom.find('[data-filed="ProfitType"]').combobox({
                    data: that.feeSplittingList,
                    valueField: 'id',
                    textField: 'name',
                    panelHeight: 100,
                    required:true,
                    onSelect: function (record) {
                        if(record) {
                            debugger;
                            var allDom = $(this).parents(".commonSelectWrap").find(".lineList");
                            allDom.hide();
                            $(this).parents(".commonSelectWrap").next(".explain").find("i").hide();
                            $(this).parents(".commonSelectWrap").next(".explain").find("i[data-name='"+record.id+"']").show();
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

                dom.find('[data-filed="VipCardTypeID"]').combobox({
                    data: that.levelList,
                    valueField: 'VipCardTypeID',
                    textField: 'VipCardTypeName',
                    required:true,
                    onSelect: function (record) {
                        if (record) {
                            debugger;
                            var optionDom = $(this).parents(".scheme");

                            /**** 返回值 levelSelect
                             * isExist;  该等级是否已经存在
                             *
                             * selectIsAll:true 选择全部门店， false 部分门店
                             * */
                          //  optionDom.find('[data-name="unit"].radio').show();

                            optionDom.find('[data-filed="IsApplyAllUnits"]').addClass("on").removeClass("disable");
                            optionDom.find('[data-valuetype="portion"]').removeClass("on");
                            optionDom.find('[data-valuetype="portion"]').siblings(".radioBtn").hide();
                            optionDom.find('[data-valuetype="portion"]').parents('.commonSelectWrap ').show();
                            var isShowAlert = true;
                            $.each(record, function (name, value) {
                                var selector = "[data-name='{0}']".format(name);
                                if (optionDom.find(selector).length > 0) {
                                    optionDom.find(selector).each(function () {
                                        if ($(this).data("value") == value) {
                                            $(this).show();
                                            isShowAlert = false;
                                            $(this).find(".easyui-numberbox").numberbox({
                                                disabled: false,

                                            });
                                            $(this).find(".textbox-text").css({"background": "#fff"});
                                        }
                                        else {
                                            $(this).hide();
                                            $(this).find(".easyui-numberbox").numberbox({
                                                disabled: true,
                                            });
                                            $(this).find(".textbox-text").css({"background": "#efefef"});
                                        }
                                    })
                                }

                            });
                            if (isShowAlert&&record.IsBuyUpgrade==1) {
                                $.messager.alert("提示", "您还没有为此卡设置可分润条件。您可以：<br/>勾选此卡“是否可充值”选项,或者<br/>将此卡升级条件设置为“购卡升级”或“充值升级”","",function(){
                                    optionDom.remove();
                                });

                                //return false;
                            }
                            dom.find('[data-filed="ProfitType"]').combobox('setValue', 'Superposition');

                            var allDom =  dom.find('[data-filed="ProfitType"]').parents(".commonSelectWrap").find(".lineList");
                            allDom.hide();

                            allDom.find(".easyui-numberbox").numberbox({
                                disabled: true,
                            });
                            allDom.find(".textbox-text").css({"background": "#efefef"});

                            if(record.IsPrepaid) {
                                var selector = "[data-name='{0}'].lineList".format("Superposition");
                                dom.find('[data-filed="ProfitType"]').parents(".commonSelectWrap").find(selector).show();
                                dom.find('[data-filed="ProfitType"]').parents(".commonSelectWrap").find(selector).find(".easyui-numberbox").numberbox({
                                    disabled: false

                                });
                                $(this).parents(".commonSelectWrap").find(selector).find(".textbox-text").css({"background": "#fff"});
                            }

                            var obj=that.returnLevelState(optionDom,record.VipCardTypeID);
                            if(obj.isExist&&obj.selectIsAll){
                                $.messager.alert("提示", "您选择的会员卡的分润激励规则，已经应用到全部门店，无须再进行设置了","error",function(){
                                    optionDom.remove();
                                });
                                optionDom.find('[data-filed="IsApplyAllUnits"]').removeClass("on").addClass("disable");
                                optionDom.find('[data-valuetype="portion"]').addClass("on");
                                optionDom.find('[data-valuetype="portion"]').siblings(".radioBtn").show();
                                optionDom.find('[data-valuetype="portion"]').parents('.commonSelectWrap ').hide();
                             //return false
                            }
                            if(obj.isExist){
                                optionDom.find('[data-filed="IsApplyAllUnits"]').removeClass("on").addClass("disable");
                                optionDom.find('[data-valuetype="portion"]').addClass("on");
                                optionDom.find('[data-valuetype="portion"]').siblings(".radioBtn").show();
                            }


                        }
                    }
                });


                dom.find(".easyui-numberbox").numberbox({
                    disabled: true,
                   required:true
                });
                dom.find(".lineList .easyui-numberbox").numberbox({    //充值分润不必填
                    disabled: true,
                    required:false,
                });
                dom.find(".textbox-text").css({"background": "#efefef"});
                dom.find("[data-filed='VipCardTypeID']").parent().find(".textbox-text").css({"background": "#fff"});

            }
            if(callback){
                callback()
            }
        },


        setLumpHtml:function(type,obj){  //封装成方法，有些特殊数据需要处理  方便拓展
            var html="";
           /* switch(type){
                case  "upgrade"://升级
                          html=bd.t
                    break;
                case  "interests"://权益

                    break;
                case "card": //开卡礼物

                    break;
            }*/
             html=bd.template('tpl_setLumpHtml',{type:type,data:obj});
            return html;
        },


        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
           $("#lumpList").find(".lumpList:last").find(".upLoadBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;



            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
               //self.loadData.args.imgSrc=data.url;
                $(ele).parent().css({"background-image":'url("'+data.url+'")'});
                $(ele).parent().addClass("bgCover");
                $(ele).parent().data("value",data.url);
                $(ele).parent().find("p").hide();
                if( $(ele).parents(".lumpList").data("id")!=-100) {
                    $(ele).parents(".lump").find(".releasePanel").show();
                }
            });

        },
        //上传图片
        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:"100%",
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=640',

                afterUpload: function (data) {
                    debugger;
                    if (data.error === 0) {
                        if (callback) {
                            callback(btn, data);
                        }
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else{
                        alert(data.msg);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },
        addCouponList:function(data){
            debugger;
            var that=this;
            that.elems.optionType="card";
            var top=$(document).scrollTop()+10;
             $("body").eq(0).css("overflow-y","hidden");

            var left=$(window).width() - 920>0 ? ($(window).width() -920)*0.5:80;
            $('#win').window({title: "设置开卡礼", width: 920, height: 665, top: top, left: left});
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
                    rowList[index]["CouponNum"]="";
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
                           argList[index]["CouponNum"]=number;
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

        //设置权益
        addInterests: function(data) {
            debugger;
            var that=this;
            that.elems.optionType="interests";
            var top=$(document).scrollTop()+60;
            // $("body").eq(0).css("overflow-y","hidden");

            var left=$(window).width() - 600>0 ? ($(window).width() -600)*0.5:80;
            $('#win').window({title: "设置基本权益", width: 600, height: 430, top: top, left: left});



            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tql_addInterests');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $('#win').find(".easyui-numberbox").numberbox({
                disabled: true,
                required: false
            });
            $('#win').find(".textbox-text").css({"background": "#efefef"});
            if(data){
              /*  $.each(data,function(name,value){
                 var me=$("[name="+name+"]");
                 if(value&&!(me.parents(".rowRline").prev().find(".checkBox").hasClass("on"))){
                 me.parents(".rowRline").prev().find(".checkBox").trigger("click");

                 }

                 });*/
                $.each(data,function(name,value){
                    var me=$("[name="+name+"]");
                    if(value&&!(me.parents(".rowRline").prev().find(".checkBox").hasClass("on"))){
                        me.parents(".rowRline").prev().find(".checkBox").trigger("click");
                        me.parents(".linetext").find(".radio").trigger("click");
                    }

                });

                $("#optionForm").form('load',data);
            }
        },
                //升级条件
        addUpgrade:function(data){ // 新增升级条件
            debugger;
            var that=this;
            that.elems.optionType="upgrade";
            var top=$(document).scrollTop()+60;
            // $("body").eq(0).css("overflow-y","hidden");

                var left=$(window).width() - 600>0 ? ($(window).width() -600)*0.5:80;
                $('#win').window({title: "设置升级条件", width: 600, height: 590, top: top, left: left});



            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addUpgrade');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            //number1 number2 有一个是必填的
            $("#number1").numberbox({
                onChange:function(newValue,oldValue){
                    if(newValue){
                        $("#number2").numberbox({
                            required:false
                        })
                        $(".tooltip").remove();
                    }else{
                        $("#number2").numberbox({
                            required:true
                        })
                    }
                }

            });
            $("#number2").numberbox({
                onChange:function(newValue,oldValue){
                    if(newValue){
                        $("#number1").numberbox({
                            required:false
                        })
                        $(".tooltip").remove();
                    }else{
                        $("#number1").numberbox({
                            required:true
                        })
                    }
                }

            });
            if(data) {
                $.each(data,function(name,value){
                    var me=$("[data-filed="+name+"]");
                    if(me.is("div")&&me.hasClass("radio")&&value){
                        me.trigger("click");
                    }
                    if(value===0){
                          data[name]="";
                    }

                });
                if(data["OnceBuyAmount"]&&!($('[name="OnceBuyAmount"]').parents(".checkBox").hasClass("on"))){
                    $('[name="OnceBuyAmount"]').parents(".checkBox").trigger("click");
                }
                if(data["BuyAmount"]&&!($('[name="BuyAmount"]').parents(".checkBox").hasClass("on"))){
                    $('[name="BuyAmount"]').parents(".checkBox").trigger("click");
                }


                if(data["IsExtraMoney"]==1&&!($('[data-filed="IsExtraMoney"]').hasClass("on"))){
                    $('[data-filed="IsExtraMoney"]').trigger("click");
                }
                $("#optionForm").form('load',data);
            } else{
                $(".upgrade .radio").eq(0).trigger("click");
            }
        },
        //新增新的级别
        addLumpList:function(data){
            if($("#lumpList").find(".lumpList").length<7) {
                if (data) {
                    data["objLength"] = data.VipCardType.VipCardLevel;
                    data["id"]=data.VipCardType.VipCardTypeID;
                    data["isDel"] = false;
                } else {
                    data = {};
                    data["objLength"] = $("#lumpList").find(".lumpList").length + 1;
                    data["isDel"] = true;
                    data["id"]=-100;

                }
                data["index"]="";
                switch (data["objLength"]){
                    case 1 : data["index"]="一";break;
                    case 2 : data["index"]="二";break ;
                    case 3 : data["index"]="三";break;
                    case 4 : data["index"]="四";break ;
                    case 5 : data["index"]="五";break;
                    case 6 : data["index"]="六";break ;
                    case 7 : data["index"]="七";break ;
                    case 8 : data["index"]="八";break;
                    case 9 : data["index"]="九";break;
                    case 10 : data["index"]="十";break ;
                    case 11 : data["index"]="十一";break ;
                    case 12 : data["index"]="十二";break ;

                }
                var html = bd.template('tpl_addLevel', data);
                $("#lumpList").append(html);
                $("#lumpList").find(".lumpList:last").find(".easyui-validatebox").validatebox();  //实例化一次jQuery easyUI验证框属性

            }else{
                alert("会员卡等级限制最多7个")
            }
            if($("#lumpList").find(".lumpList").length==7){
                $("#addLump").hide();
                $("#addLump").parent().hide();
            }
            this.registerUploadImgBtn();
        },

        //绑定门店数据
        renderTableUnit:function(node) {
            var that = this;
            if (!node) {
                node = $('#unitParentTree').tree("getSelected");
            }
            if (node) {
                that.loadData.unitSearch.Parent_Unit_ID = node.id;
            } else {

                $.messager.alert("提示", "请选择一个门店上级组织");
                return;
            }
            debugger;
            $("#unitSelectName").html(node.text);
            that.loadData.unitSearch.unit_name = $("#unit_name").val();

            that.loadData.getUnitList(function (data) {
                var isSubMit = false;
                var dataList = [];
                if (that.selectAllUnit.length > 0&&data.topics.length>0){
                    debugger;
                    var type=that.elems.optionScheme.data("type"),
                        domIndex=that.elems.optionScheme.index(),
                        VipCardTypeID=that.elems.optionScheme.find('[data-filed="VipCardTypeID"]').combobox("getValue");

                    $.each(that.selectAllUnit,function(index,unitItem){
                        if(type=unitItem.ProfitOwner&&VipCardTypeID==unitItem.VipCardTypeID&&unitItem.unitList&&domIndex!=unitItem.domIndex){
                            if(unitItem.unitList.length>0){ //排除自身

                                $.each(unitItem.unitList,function(unitIndex,Item){
                                    $.each(data.topics,function(topIndex,topItem){
                                       if(Item.id==topItem.Id){
                                           data.topics.splice(topIndex,1);
                                           return false;
                                       }

                                    })

                                })



                            }


                        }

                    });


                }
                    dataList = data.topics;




                $("#unitGrid").datagrid({
                    method: 'post',
                    iconCls: 'icon-list', //图标
                    singleSelect: false, //多选false 单选true
                    height: 430, //高度
                    fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped: true, //奇偶行颜色不同
                    collapsible: true,//可折叠
                    scrollbarSize: 18,
                    //数据来源
                    data: dataList,
                    sortName: 'Id', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    idField: 'Id', //主键字段
                    /* pageNumber:1,*/

                    columns: [[
                        {
                            field: 'Name', title: '店名', width: 60, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                return value;
                            }
                        },
                        {
                            field: 'ck',
                            checkbox: true
                        }


                    ]],
                    onLoadSuccess: function (data) {
                        debugger;

                        $("#unitGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        var select = that.elems.optionScheme.data("unitlist");
                        if (select && typeof(select) == "string") {
                            select = JSON.parse(select);

                        }
                        if(data.rows.length>0) {
                            $("#UnitDataMessage").hide();
                        }else{
                            $("#UnitDataMessage").show();
                        }
                        if (that.selectDataUnit.length > 0) {
                            select = that.selectDataUnit;
                        }
                        if (select && select.length > 0 && data.rows.length > 0) {
                            $.each(select, function (i, treeNode) {
                                if (treeNode.children && treeNode.children.length > 0) {
                                    $.each(treeNode.children, function (index, children) {
                                        $.each(data.rows, function (rowIndex, row) {
                                            if (children.id == row.Id) {
                                                $("#unitGrid").datagrid('checkRow', rowIndex);
                                                return false;
                                            }
                                        });

                                    })
                                }

                            })
                        }
                        isSubMit = true;

                    },
                    onCheck: function () {
                        var check= $("#unitGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if (isSubMit) {
                            that.cacheUnitData();
                        }

                    },
                    onUncheck: function () {
                        $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        if (isSubMit) {
                            that.cacheUnitData();
                        }

                    },
                    onCheckAll: function () {
                        $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        if (isSubMit) {
                            that.cacheUnitData();
                        }
                    }, onUncheckAll: function () {
                        $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        if (isSubMit) {
                            that.cacheUnitData();
                        }
                    }


                });
            })

        },
        //刷新选中门店数据
        reloadUnitData:function(delList){
            var that=this;
            var   selectDataUnit=that.selectDataUnit;

            if (selectDataUnit.length > 0&&delList&&delList.length>0) {
                var dataUnitlist=[];
                for (var i = 0; i < selectDataUnit.length; i++) {
                    for(var j=0;j<delList.length;j++) {
                        var delNode=delList[j]
                        if (delNode.id !== selectDataUnit[i].id) {
                            var children=[];
                            if(selectDataUnit[i].children&&selectDataUnit[i].children.length>0){
                                for (var K = 0; K < selectDataUnit[i].children.length; K++) {
                                    if (delNode.id !== selectDataUnit[i].children[K].id) {
                                        children.push(selectDataUnit[i].children[K])
                                    }
                                }
                            }
                            if(children.length>0) {
                                var nodeNew = {
                                    id: selectDataUnit[i].id,
                                    text: selectDataUnit[i].text,
                                    "children": children
                                };

                                dataUnitlist.push(nodeNew);
                            }
                        }
                    }

                }
                that.selectDataUnit=dataUnitlist;
                selectDataUnit=dataUnitlist;
            }



            $('#unitTreeSelect').tree("loadData",selectDataUnit);

        },
        //选择门店
        selectUnit:function(data){
            var that=this;
            that.elems.optionType="selectUnit";
            var top=$(document).scrollTop()+60;
            var left=$(window).width() - 1140>0 ? ($(window).width() - 1140)*0.5:80;
            $('#win').window({title:"选择门店",width:1140,height:630,top:top,left:left});
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_setUnitList');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $("#win").window("open");
            //组织层级
            that.loadData.getUnitClassify(function(data) {
                if(!(data&&data.length>0)){
                    data=[];
                } else{
                   that.renderTableUnit(data[0]);
                }
                debugger;
                $('#unitParentTree').tree({
                    id: 'id',
                    text: 'text',
                    data:data,
                    onClick:function(node) {
                        that.renderTableUnit(node);
                    }
                });
                var node = $('#unitParentTree').tree('find', data[0].id);
                $('#unitParentTree').tree('select', node.target);

            });
            $('#unitTreeSelect').tree({
                id: 'id',
                text: 'text',
                checkbox:true,
                formatter:function(node){
                    debugger;
                    var s = node.text;
                    if (node.children){
                        var long=18;

                        if(s&&s.length>long){
                            s= '<div class="bg"  title="'+s+'">'+s.substring(0,long)+'...</div>'
                        }else{
                            s= '<div class="bg" >'+s+'</div>'
                        }
                    }else{
                        s+='<em class="delete" title="删除" data-target='+node.id+'></em>'
                    }
                    return s;
                },onLoadSuccess:function() {

                    $(".bg").parents(".tree-node").addClass("bg");
                },
                data:[]

            });
            $("#unitTreeSelect").delegate(".delete","click", function (e) {
                var id=$(this).data("target");

                var nodeList=[] ;
                var isDel=true;
                var node= $('#unitParentTree').tree("getSelected");
                nodeList = $("#unitGrid").datagrid('getData').rows;

                if(nodeList.length>0){
                    for(var j=0;j<nodeList.length;j++){
                        if(nodeList[j].Id==id){
                            isDel=false;
                            $("#unitGrid").datagrid("uncheckRow",j);
                        }
                    }

                }
                if(isDel) {
                    that.reloadUnitData([{id: id}]);
                }


            });
            that.reloadUnitData();

        },

        //缓存选中门店的数据
        cacheUnitData:function(){
            debugger;
            var that=this;
            var isAdd = true,nodeList=[];


            var node= $('#unitParentTree').tree("getSelected");
            nodeList=$("#unitGrid").datagrid('getChecked');
            var nodeParent={id:node.id,text:node.text,"children":[]};
            var nodeObjList=[];
            if(nodeList.length>0){
                for(var j=0;j<nodeList.length;j++){
                    var obj={};
                    obj["id"]=nodeList[j].Id;
                    obj["text"]=nodeList[j].Name;
                    nodeObjList.push(obj)
                }
                nodeParent["children"]=nodeObjList;
            }
            var dataUnitlist=[],  selectDataUnit=that.selectDataUnit;

            if (selectDataUnit.length > 0) {
                for (var i = 0; i < selectDataUnit.length; i++) {
                    var nodeNew={id:selectDataUnit[i].id,text:selectDataUnit[i].text,"children":selectDataUnit[i].children};
                    if (nodeParent.id!== selectDataUnit[i].id) {
                        dataUnitlist.push(nodeNew);
                    }else{     //如果相等判断合并子节点

                    }

                }
            }
            if(nodeObjList.length>0){
                dataUnitlist.push(nodeParent)
            }

            that.selectDataUnit=dataUnitlist;   //当前选择的门店
            $('#unitTreeSelect').tree("loadData",dataUnitlist);
        },
        //加载页面的数据请求
        loadPageData: function (callback) {
                var that=this;
             that.loadData.operation([],"getVipCard",function(data){

                 var lumpList=data.Data.VipCardRelateList;
                 if(lumpList&&lumpList.length>0){
                     $("#lumpList").html("").show();
                     $("#addLump").data("type","level");
                     $.each(lumpList,function(index,item){

                         that.addLumpList(item);  //创建对应等级
                         var lumpListDom=$("#lumpList").find(".lumpList:last");//选择等级
                         $.each(item,function(name,value){
                             var optionDom = lumpListDom.find(".lump[data-filed=" + name + "]");
                             if(value) {
                                 if(name=="VipCardType"){     //会员卡的处理
                                     optionDom.data("id",value.VipCardTypeID);
                                   $.each(value,function(name1,value1){
                                      var lumpDom= optionDom.find("[data-filed="+name1+"]");
                                          if(lumpDom.is("input")){
                                              lumpDom.val(value1)
                                          }else if(name1=="PicUrl"){
                                              lumpDom.css({"background-image":'url("'+value1+'")'});
                                              lumpDom.addClass("bgCover");
                                              lumpDom.data("value",value1);
                                              lumpDom.find("p").hide();
                                          } else if(lumpDom.hasClass("checkBox")){
                                                 if(value1){
                                                     lumpDom.addClass("on");
                                                 } else{
                                                     lumpDom.removeClass("on");
                                                 }
                                          }
                                   });
                                     optionDom.find(".edit").show();

                                 }else if(!(lumpListDom.data("level")==1&&name=="VipCardUpgradeRule")) {  //权益等级优惠券统一处理   //等级为1的时候升级条件不做任何处理

                                     optionDom.find(".edit").show();
                                     optionDom.find(".content").html(that.setLumpHtml(optionDom.data("type"), value));
                                     optionDom.data("value", JSON.stringify(value));
                                     if(name=="VipCardUpgradeRewardList"){
                                         optionDom.data("editValue", JSON.stringify(value));
                                     }
                                 }

                             }
                         });


                     })
                 }

             });
            if(callback){
                callback();
            }
        },

        getVipCardRuleList:function(callback){
            var that=this;
            that.loadData.getLevelList(function (data) {
                if (data.Data && data.Data.SysVipCardTypeList&&data.Data.SysVipCardTypeList.length>0) {
                    //升级条件（1=购卡升级、0=充值升级、2=消费升级）
                     debugger;
                    that.levelList=[];
                    $.each(data.Data.SysVipCardTypeList,function(index,value){
                        if (value.IsPurchaseUpgrade) {   //IsPurchaseUpgrade购买升级
                            value["RefillCondition"] = 1
                        }
                        else if (value.IsRecharge) {   //IsRecharge 是否充值升级
                            value["RefillCondition"] = 0
                        }else if (value.IsBuyUpgrade) {   // IsBuyUpgrade	Int	是否消费升级
                            value["RefillCondition"] = 2
                        }
                        that.levelList.push(value);
                    });
                }
                $.util.isLoading();

            that.loadData.getVipCardRuleList(function(data){

                var cardProfitInfo=data.Data;
                debugger;
                $(".nav02panel").eq(0).find(".listScheme").html("");
                $(".nav02panel").eq(1).find(".listScheme").html("");

                if(cardProfitInfo&&cardProfitInfo.VipCardProfitRuleList.length>0){

                    $.each(cardProfitInfo.VipCardProfitRuleList,function(index,item){
                        debugger
                        var selectIndex=0;
                        if(item.ProfitOwner=="Unit"){
                            selectIndex=1
                        }
                        var html=bd.template('tpl_addSchemeList',{data:item});
                        $(".nav02panel").eq(selectIndex).find(".listScheme").append(html);

                        var dom= $(".nav02panel").eq(selectIndex).find(".listScheme .scheme:last");
                        //所有等级选中门店的机会{VipCardTypeID：等级id，VipCardTypeName：等级名称，unitList:已经选择门店集合，ProfitOwner:"员工或者门店"，domIndex：当前的索引等级（确定其唯一性）}}

                        that.selectAllUnit=[];
                        if(item.RuleUnitInfoList.length>0) {
                            var domSelectUnitList = [];
                            $.each(item.RuleUnitInfoList, function (treeIndex, treeNode) {
                                if (treeNode.children && treeNode.children.length > 0) {
                                    $.each(treeNode.children,function(nodeIndex,node){
                                        domSelectUnitList.push(node);

                                    });
                                }


                            });
                            that.selectAllUnit.push({
                                VipCardTypeID: item.VipCardTypeID,
                                unitList: domSelectUnitList,
                                ProfitOwner: item.ProfitOwner,
                                domIndex: dom.index()
                            })
                        }




                        that.initScheme(dom,function(){
                            dom.find('[data-filed]').each(function(){  //给不是list类型字段 赋值
                                debugger;
                                var name=$(this).data("filed"),value=item[name];
                                if($(this).hasClass("easyui-combobox")){
                                    if(value){
                                       $(this).combobox("select",value);
                                    }
                                }else if($(this).hasClass("radio")){
                                    /*IsApplyAllUnits	int	是	是否应用全部门店（1= 是、0= 否）*/
                                    if(value==0) {
                                        $(this).removeClass("on");
                                        $(this).siblings(".radioBtn").show();
                                        $(this).siblings('[data-valuetype="portion"]').addClass("on");
                                    }
                                }

                            });
                            var ruleList=item.VipCardReRechargeProfitRuleList;
                            if(ruleList&&ruleList.length>0){
                                var profitType=ruleList[0].ProfitType;
                                dom.find('[data-filed="ProfitType"]').combobox("select",profitType);
                               // $(".lineList")选择问题，
                                var lineListDom=dom.find('[data-name="'+profitType+'"].lineList');
                                dom.find('[data-name="'+profitType+'"].lineList').html("");
                                ruleList.reverse();
                                $.each(ruleList,function(i,ruleList){
                                    ruleList["type"]=profitType;
                                    var html = bd.template("add_lineListData",ruleList);
                                    lineListDom.prepend(html);
                                    lineListDom.find(".linetext:first").find(".easyui-numberbox").numberbox({
                                        disabled: false,
                                       // required: true
                                    });
                                }) ;
                             /*   if( lineListDom.find(".linetext:last").find('[data-name="add"]').length==0&&profitType=="Step"){
                                    lineListDom.find(".linetext:last").append('<div class="radioBtn r" data-name="add">添加</div>')
                                }*/
                            }else{ //如果是空的话添加好默认值表示没有设置
                                dom.find('[data-name="Step"].lineList').html('<div class="radioBtn l listBtn" data-name="add">新增梯度分润</div>');
                                dom.find('[ data-name="Superposition"].lineList').html('<div class="radioBtn l listBtn" data-name="add">新增叠加分润</div>');
                            }
                        });
                    });

                }
                if(callback){
                    callback();
                }
            });
            });

        },
         //已选门店的数据操作
         optSelectAllUnit:function(dom,unitTree,optType){
             var that=this;
             var type=dom.data("type"),
                 domIndex=dom.index(),
                 VipCardTypeID=dom.find('[data-filed="VipCardTypeID"]').combobox("getValue");
             if(unitTree.length>0&&optType=="update"){
                 var domSelectUnitList=[];
                 $.each(unitTree, function (treeIndex, treeNode) {
                     if (treeNode.children && treeNode.children.length > 0) {
                         $.each(treeNode.children,function(nodeIndex,node){
                             domSelectUnitList.push(node);
                         });
                     }
                 });
                  var Index=-1;
                 $.each(that.selectAllUnit,function(index,unitItem) {
                     if (type = unitItem.ProfitOwner && VipCardTypeID == unitItem.VipCardTypeID && unitItem.unitList && domIndex== unitItem.domIndex) {
                         Index=index;
                         return  false;
                     }
                 });
                 if(Index==-1) {
                     that.selectAllUnit.push({
                         VipCardTypeID: VipCardTypeID,
                         unitList: domSelectUnitList,
                         ProfitOwner: type,
                         domIndex: dom.index()
                     })
                 }else{
                     that.selectAllUnit[Index].unitList= domSelectUnitList;
                 }
             }else if(optType="del") {
                 $.each(that.selectAllUnit,function(index,unitItem) {
                     if (type = unitItem.ProfitOwner && VipCardTypeID == unitItem.VipCardTypeID && unitItem.unitList && domIndex== unitItem.domIndex) {
                         that.selectAllUnit.splice(index,1);
                         return  false;
                     }
                 });
             }
         },
         //判断当前等级是否存在已经添加的记录，且选择的是部分门店还是全部门店。
        /**** 返回值 levelSelect
         * isExist;  该等级是否已经存在
         *
         * selectIsAll:true 选择全部门店， false 部分门店
         * */
            returnLevelState:function(dom,VipCardTypeID) {
            debugger;
            var type = dom.data("type");
               //VipCardTypeID = dom.find('[data-filed="VipCardTypeID"]').combobox("getValue");

            var levelSelect={isExist:false};
            $("#nav02").find(".scheme[data-type='" + type + "']").each(function () {
                if(dom.index()!=$(this).index()) {
                    var ruleInfoItem = {};
                    $(this).find('[data-filed]').each(function () {  //取 不是list类型字段
                        var name = $(this).data("filed"), value = "";
                        if (name == "VipCardTypeID") {
                            value = $(this).combobox("getValue");
                        } else if ($(this).hasClass("radio")) {
                            /*IsApplyAllUnits	int	是	是否应用全部门店（1= 是、0= 否）*/
                            value = $(this).hasClass("on")

                        }
                        if (value !== "") {
                            ruleInfoItem[name] = value;
                        }
                    });
                    if(ruleInfoItem["VipCardTypeID"]==VipCardTypeID){
                        levelSelect["isExist"]=true;
                        if( !levelSelect["selectIsAll"]) {
                            levelSelect["selectIsAll"] = ruleInfoItem["IsApplyAllUnits"];
                        }
                    }
                }
            });
            return levelSelect;

        },


        loadData: {
            args: {
                bat_id:"1",
                PageIndex:1,
                PageSize: 10,
                SearchColumns:{ActivityType:1},    //查询的动态表单配置   ActivityType:1 //活动类型 （1：生日活动，2：普通活动）
                OrderBy:"",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'

            },
            unitSearch:{
                unit_name:'',//门店名称
                unit_status:'1',//int	否	状态（1:正常，０：失效）
                StoreType:'',//string	否	门店类型 直营店：DirectStore，加盟店：NapaStores
                Parent_Unit_ID:'',
                OnlyShop:'1' //int	是	只取门店（是：1,否：０）
            },

            opertionField:{},
            getActivityList: function (callback) {
                var prams={data:{action:""}};
                prams.data={
                    action: "Marketing.Activity.GetActivityList",
                    PageSize:this.args.PageSize,
                    PageIndex:this.args.PageIndex
                    /*OrderBy:this.args.Phone,
                     UnitID:"005c99f55c26916fbd305bd69e8948fd",//window.RoleCode=="Admin"?null:window.UnitID,//
                     Status:this.args.Status*/

                };
                /*$.each(this.args.SearchColumns, function(i, field) {

                    if (field.value!=-1) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });*/
                $.extend(prams.data,this.args.SearchColumns);

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,
                    /*  channelID:'7',
                     customerId:"464153d4be5944b19a13e325ed98f1f4",
                     userId:"550E2D12613D4580989B65AF984F578D",*/
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
            GetCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    beforeSend:function(){
                        $.util.isLoading();
                    },
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        IsNotLimitQty:1,
                        IsServiceLife:1,
                        PageIndex:1,
                        PageSize:100000

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

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                $.each(pram, function (i, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                });


                switch(operationType){
                    case "setVipCard" :prams.data.action="VIP.VipGold.SetVipCardTypeSystem";   break; //删除
                    case "getVipCard":prams.data.action="VIP.VipGold.GetVipCardTypeSystem" ;  break; //开始,
                    case "setList":prams.data.action="VIP.VipGold.UpdateVipCardUpgradeReward"; break;
                    case "setVipCardInfo" :prams.data.action="VIP.VipGold.UpdateVipCardTypeSystem"; break; //权益，升级，开卡礼物
                    case "setVipCardProfitRule": prams.data.action="VIP.VipGold.SetVipCardProfitRule"; break;
                    case "setRewardsSetting": prams.data.action="Basic.Customer.SetRewardsSetting"; break; //积分
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
                            alert(data.Message);
                        }
                    }
                });
            },

            //获取门店等级
            getUnitClassify: function(callback){
                $.util.isLoading();
                $.util.ajax({
                    url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
                    data:{
                        hasShop:0
                    },
                    success: function(data){
                        if(data){
                            if(callback)
                                callback(data);
                        }
                        else{
                            alert("门店数据加载不成功");
                        }
                    }
                });
            },
            //只获取门店列表
            getUnitList: function (callback) {
                $.util.isLoading();
                debugger;
                $.util.oldAjax({
                    url: "/module/basic/unit/Handler/UnitHandler.ashx",
                    data:{
                        action:'search_unit',
                        page:1,
                        //start:this.args.start,
                        limit:100000,
                        form:this.unitSearch
                    },
                    success: function (data) {
                        if (data.topics) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            $.messager.alert("提示","加载门店列表数据不成功");
                        }
                    }
                });
            },
            GetCustomerList: function (callback) {
                $.util.oldAjax({
                    url: "/Module/CustomerBasicSetting/Handler/CustomerBasicSettingHander.ashx",
                    data:{
                        action:"GetCustomerList",
                    },
                    success: function (data) {
                        if (data.data) {
                            if (callback)
                                callback(data.data);
                        }
                        else {
                            console.info("商户基础信息加载异常");
                        }
                    }
                });
            },
            getLevelList:function(callback){   //获取会员卡等级
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'VIP.VipGold.GetSysVipCardTypeList',
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
            getVipCardRuleList:function(callback){   //获取会员卡等级
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'VIP.VipGold.GetVipCardProfitRuleList',
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
            }

        }

    };
    page.init();
});

