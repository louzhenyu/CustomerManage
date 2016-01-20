define(['jquery', 'template', 'tools','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            listItem: $("#unitList"),             //所有的查询条件层dom
            Tooltip:$("#Tooltip"),
            listTable:$(".listTable .easyui-datagrid"), //
            click:true,
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
            var that=this;
            that.loadData.GetSysRetailRewardRule(function(data) {
                debugger;
                if (data.Data.SysRetailRewardRuleList && data.Data.SysRetailRewardRuleList.length > 0) {
                    var SysRetailRewardRuleList = data.Data.SysRetailRewardRuleList;
                    var fields = $("#addOneWay").serializeArray();
                    var  loadData={},
						 loadData2={};
                    $.each(fields, function (index, field) {
                        for (var i = 0; i < SysRetailRewardRuleList.length; i++) {
                            var item = SysRetailRewardRuleList[i];
                            if (field.name.indexOf(item.RewardTypeCode) != -1) {    //确定是那个类别的
                                if (field.name.indexOf("SellUserReward") != -1) {         //不同类别的销售员奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["SellUserReward"];
                                }
                                if (field.name.indexOf("RetailTraderReward") != -1) {  //不同类别的分销商奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["RetailTraderReward"] ;
                                }
                                if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同类别的分销商奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["RetailRewardRuleID"] ;
                                }
                            }
							debugger;
							//是否选择引流 或者 销售模块
							if(item.CooperateType == 'Sales' && item.Status == 1){
								loadData2 = item;
								$('#marketTag').addClass('on');
							}
							if(item.CooperateType == 'OneWay'){
								$('#drainageTag').addClass('on');
							}

                        }


                    })
					//console.log(loadData);
                    debugger;
                    $("#addOneWay").form('load',loadData);
					$('#addSalseWay').form('load',loadData2);
                }
            });

            that.loadData.args.CooperateType="TwoWay" ;
            that.loadData.GetSysRetailRewardRule(function(data) {
                debugger;
                if (data.Data.SysRetailRewardRuleList && data.Data.SysRetailRewardRuleList.length > 0) {
                    var SysRetailRewardRuleList = data.Data.SysRetailRewardRuleList;
                    var fields = $("#addTwoWay").serializeArray();
                    var  loadData={};
                    $.each(fields, function (index, field) {
                        for (var i = 0; i < SysRetailRewardRuleList.length; i++) {
                            var item = SysRetailRewardRuleList[i];
                            if (field.name.indexOf(item.RewardTypeCode) != -1) {    //确定是那个类别的
                                if (field.name.indexOf("SellUserReward") != -1) {         //不同类别的销售员奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["SellUserReward"];
                                }
                                if (field.name.indexOf("RetailTraderReward") != -1) {  //不同类别的分销商奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["RetailTraderReward"] ;
                                }
                                if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同类别的分销商奖励
                                    loadData[field.name]=SysRetailRewardRuleList[i]["RetailRewardRuleID"] ;
                                }

                            }
							if(item.CooperateType == 'TwoWay'){
								$('#drainageTag').addClass('on');
							}

                        }


                    })

                    debugger;
                    $("#addTwoWay").form('load',loadData);
                }
            });
        },

        initEvent: function () {
            var that = this;
			
			//监听复选框事件
			$('.checkBoxTag').on('click',function(){
				var $this = $(this);
				if($this.hasClass('on')){
					$this.removeClass('on');
				}else{
					$this.addClass('on');
				}
			});
			
			
			
            //点击查询按钮进行数据查询
             that.elems.simpleQueryDiv.delegate(".listBtn","click",function(){
                 debugger;
                  var me=$(this);

                  if(!me.hasClass("show")){
                      that.elems.simpleQueryDiv.find(".listBtn").removeClass("show");
                       me.addClass("show");
                      var classs="."+me.data("hide");
                      $(classs).hide(0);
                      var classs="."+me.data("show");
                      $(classs).show(0);

                  }


             }) ;
            that.elems.simpleQueryDiv.find(".listBtn").eq(0).trigger("click");
            that.elems.sectionPage.delegate(".submitBtn","click",function(){ //新增优惠券
                debugger
			   var type=$("[data-cooperatetype].show").data("cooperatetype"), fields=[],isSubmit=false;
			   if($('#drainageTag').hasClass('on')){
				   if(type=="OneWay"){
					   if($("#addOneWay").form("validate")){
						   isSubmit=true;
						   fields=$("#addOneWay").serializeArray();
					   }
				   }else if(type=="TwoWay") {
					   if($("#addTwoWay").form("validate")) {
						   isSubmit = true;
						   fields = $("#addTwoWay").serializeArray();
					   }
				   }
			   }
			   
			   if($('#marketTag').hasClass('on')){
				    isSubmit = true;
			   }
			   
			   if(isSubmit) {
					that.loadData.operation(fields, "", function () {
						alert("操作成功");
					})
			   }else{
			   		alert("引流与销售至少选中一个哦！");
			   }
			   
			   

            });

            that.elems.sectionPage.find("[data-validity].radio").eq(0).trigger("click");
            $(".listTable").delegate(".del","click",function(e){
                debugger;
                  var  index=$(this).data("index");
                   // index=parseInt(index);
                  that.elems.listTable.datagrid("deleteRow",index);

                var  data=that.elems.listTable.datagrid("getData");
                that.elems.listTable.datagrid("loadData",data);

            });
            that.elems.sectionPage.delegate(".checkBox","click",function(e){
                var me= $(this);
                me.toggleClass("on");
                $.util.stopBubble(e);
                debugger;
                var  className="."+me.data("toggleclass");
                if(me.hasClass("on")&&me.data("flag")=="SuitableForStore"){
                   $(className).hide(0);
                }else if(me.data("flag")=="SuitableForStore") {
                    $(className).show(0);
                }

            });

            /**************** -------------------初始化easyui 控件 start****************/


            /**************** -------------------初始化easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            /*that.elems.tabelWrap.delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="add") {
                    if(row.IsPaid!=1&&row.Status!=10&&row.Status!=11) {
                        that.addNumber(row);
                    }
                }
                if(optType=="delete"){
                    $.messager.confirm("删除优惠券操作","确认要删除该条记录",function(r){
                             if(r){
                                 alert("执行删除")
                             }
                    })
                }
            })*/
            /**************** -------------------列表操作事件用例 End****************/

        },



        //加载页面的数据请求
        loadData: {
            args: {
                bat_id:"1",
                CooperateType:"OneWay"
            },
            tag:{
                VipId:"",
                orderID:''
            },


            opertionField:{},



            GetSysRetailRewardRule: function (callback) {
                debugger;
                $.util.ajax({
                    url: "/ApplicationInterface/AllWin/SysRetailRewardRule.ashx",
                    data:{
                        action:"GetSysRetailRewardRule",
                        IsTemplate:'1',
                        CooperateType:this.args.CooperateType
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
                var prams={data:{action:"SaveRetailRewardRule"}};
                prams.url="/ApplicationInterface/AllWin/SysRetailRewardRule.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                prams.data["IsTemplate"]="1";    //模板保存
				
				var SysRetailRewardRuleList = [];
				
				//判断是否选择引流模块
				if($('#drainageTag').hasClass('on')){
					var cooperateType = $("[data-cooperatetype].show").data("cooperatetype");
					SysRetailRewardRuleList=[
						 {"CooperateType":cooperateType,"RewardTypeName":"首次关注奖励","RewardTypeCode":"FirstAttention","AmountOrPercent":"1"},
						 {"CooperateType":cooperateType,"RewardTypeName":"首笔交易奖励","RewardTypeCode":"FirstTrade","AmountOrPercent":"2"},
						 {"CooperateType":cooperateType,"RewardTypeName":"会员关注3个月内消费获得奖励","RewardTypeCode":"AttentThreeMonth","AmountOrPercent":"2"}
					];
					//prams.data["CooperateType"]=$("[data-cooperatetype].show").data("cooperatetype");
					$.each(pram, function (index, field){
						//  "SellUserReward":"5","RetailTraderReward":"5",
							for(var i=0;i<SysRetailRewardRuleList.length;i++){
								var item= SysRetailRewardRuleList[i];
								if(field.name.indexOf(item.RewardTypeCode)!=-1){    //确定是那个类别的
									if(field.name.indexOf("SellUserReward")!=-1) {         //不同类别的销售员奖励
										SysRetailRewardRuleList[i]["SellUserReward"]=field.value;
									}
									if(field.name.indexOf("RetailTraderReward")!=-1) {  //不同类别的分销商奖励
										SysRetailRewardRuleList[i]["RetailTraderReward"]=field.value;
									}
									if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同分销商的不同类别奖励模板ID
										SysRetailRewardRuleList[i]["RetailRewardRuleID"]=field.value;
									}
								}
	
							}
					});
				
				}
				
				
				
				
				//判断是否选择销售模块
			    //if($('#marketTag').hasClass('on')){
			       if($('#addSalseWay').form("validate")) {
					   var salseField = $("#addSalseWay").serializeArray(),
						   saleaObj = {"CooperateType":"Sales","RewardTypeName":"销售奖励","RewardTypeCode":"Sales","AmountOrPercent":"2"};
					   if($('#marketTag').hasClass('on')){
						   $.each(salseField,function(index,field) {
								saleaObj[field.name] = field.value;
						   });
					   }else{
						   $.each(salseField,function(index,field) {
								saleaObj[field.name] = 0;
						   });
					   }	   
					   
					   SysRetailRewardRuleList.push(saleaObj);
                   }
			    //}
				//console.log(SysRetailRewardRuleList);

				
                prams.data["SysRetailRewardRuleList"]= SysRetailRewardRuleList;

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

