define(['jquery', 'template', 'tools', 'kkpager', 'artDialog','easyui', 'kindeditor'], function ($) {
    KE = KindEditor;
    var page = {
        createPager: function (dataId, pageIndex, totalPage, totalCount) {
            debugger;
            if (totalPage < 1) return;
            var that = this;
            kkpager.generPageHtml({
                pno: pageIndex,
                mode: 'click', //设置为click模式
                //总页码  
                total: totalPage,
                totalRecords: totalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //点击下一页或者上一页 进行加载数据
                    that.loadMoreData(dataId, n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);
            var tbl = $('#' + dataId);
            tbl.append(that.elems.pager);
        },
        elems: {
            sectionPage: $("#section"),
            uiMask: $("#ui-mask"),
            tabIndex:0,
            tabs: $('#section .subMenu ul'),
            content: $("#content"),     //交易记录表格数据
            pointTable: $('#tblPoint'),   //卡操作记录（原来积分）
			tblIntegralBox:$('#tblIntegralBox'),//积分记录
			tblBalanceBox:$('#tblBalanceBox'),
			tblCashBox:$('#tblCashBox'),
            amountTable: $('#tblAmount'),
            vipTagList: $('#tagList'),//会员标签
            groupTagList:$('#groupTagList'),//可选择的会员标签
            onlineTable: $('#tblOnline'),
            logsTable:$('#tblLogs'),
            dataMessage:$(".dataMessage"),
            servicesLog:$('#servicesLog'),
            consumerTable: $('#tblConsumer'),
            pager: $('#kkpager'),
            vipInfo:[]
        },
        init: function () {
            //获得地址栏参数为vipId的值
            var vipId = $.util.getUrlParam("vipId");
            var VipCardId = $.util.getUrlParam("VipCardId");
      /*     if(VipCardISN){

             VipCardISN=VipCardISN.replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"")

             }*/
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.VipId = vipId;
            this.loadData.args.VipCardId = VipCardId;

            //请求数据
            //this.loadPageData();
            //请求会员详细信息   注:暂无接口以及文档

            //初始化事件
            this.initEvent();  //initEvent 一定是在权限前面，第一点，移除按钮可能会对绑定时间有影响 。第二点权限的菜单id要根据选中菜单的id来配置
            this.authority();
            this.loadVipDetail();
        },
          //权限控制
        authority:function() {
            return false;
            this.loadData.getFunctionList(function (data) {

                if (data.Data.FunctionList && data.Data.FunctionList.length >= 0) {
                    $("[data-authority]").each(function () {
                        var me = $(this), isRemove = true; //定义是否移除按钮， 获取全部按钮，遍历权限列表，如果权限列表有，不删除（isRemove=false）
                        for (var i = 0; i < data.Data.FunctionList.length; i++) {
                            if (me.data("authority").toLowerCase() == data.Data.FunctionList[i].FunctionCode.toLowerCase()) {
                                isRemove = false;
                            }

                        }
                        if (isRemove) {
                            me.remove();
                        }

                    })
                }
            });
        },
        hidePanels: function () {
            $('#nav01,#nav02,#nav03,#nav04,#nav05,#nav06,#nav07,#nav08,#nav09,#nav010,#nav011,#nav012,#nav013').hide();
        },
        //没有数据的table提示
        showTableTips: function (jqObj, tips) {
            var noContent = bd.template("tpl_noContent", { tips: tips });
            jqObj.html(noContent);
        },
        //调整积分;
        adjustIntegral:function(data){
            var that=this;
            that.elems.optionType="updateIntegral";
            var top=$(document).scrollTop()+60;
            $('#win').window({title:"积分调整",width:630,height:500,top:top,left:($(window).width()-630)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_adjustIntegral');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
            that.registerUploadImgBtn ();
            that.loadData.args.imgSrc=""
        },



        //事件绑定方法
        initEvent: function () {
            $('#Birthday').datebox().datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    return now >=date;
                }
            })

			$('#win').delegate('.tit .radioBox','click',function(){
				var $this = $(this);
				if($this.hasClass('on')){
					return;
				}else{
					$('#win .tit .radioBox').removeClass('on');
					$this.addClass('on');
					/*
					$('#NewCardCode').attr('disabled','disabled');
					$('#ChangeVipCard').attr('disabled','disabled');
					var tit = $this.data('rd');
					if(tit==1){
						$('#NewCardCode').attr('disabled',false);
					}else if(tit==2){
						$('#ChangeVipCard').attr('disabled',false);
					}
					*/
					
					var tit = $this.data('rd');
                    console.log(tit);
					if(tit==1){
						$('#NewCardCode').validatebox({
							required:true
						});
						$('#ChangeVipCard').combobox({
							required:false,
							validType:''
						});
					}else if(tit==2){
						$('#ChangeVipCard').combobox({
							required:true,
							validType:'selectIndex'
						});
						$('#NewCardCode').validatebox({
							required:false
						});
					}
					
					
				}
			});
			
            $("#leftMenu").find("li").removeClass("on").each(function(){
                if( $(this).find("em").hasClass("newVipManage_querylist")){
                    $(this).addClass("on");
                }
            });
            $("body").delegate("input[name='NewCardCode']","keydown",function(e){

                if(e.keyCode==13){
                    that.loadData.args.SearchColumns=[];
                    var str=$(this).val().replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"");
                    if(that.elems.vipCardCode) {
                        str= str.replace(that.elems.vipCardCode,"");

                        that.elems.vipCardCode=str;
                    } else{
                        that.elems.vipCardCode=str;
                    }

                    debugger;

                    $.util.stopBubble(e);
                    $(this).focus();
                    $(this).val(str);
                    if( $(this).val()) {
                        that.loadData.args.SearchColumns = $("#optionform").serializeArray();
                        that.loadData.getVipCardStatus(function (data) {
                            debugger;
                            var isSubmit=true;
                            if (data.Data && data.Data.VipCardID) {

                                if (data.Data.MembershipUnitName || data.Data.VipCardStatusId != 0) {
                                    $("#win").find(".hint").html("该卡已被使用，请更换新卡后重新尝试");
                                    $("[name='NewCardCode']").focus();
                                    isSubmit=false;

                                }
                                if(isSubmit) {
                                    if (that.loadData.args.OperationType == 2) { //升级
                                        if(that.elems.vipCardInfo.VipCardName==data.Data.VipCardName){
                                            $("#win").find(".hint").html("同等级的卡不可以做升级操作");
                                            isSubmit=false;
                                        }
                                    }
                                    if (that.loadData.args.OperationType == 5) { //转卡
                                        if(that.elems.vipCardInfo.VipCardName!=data.Data.VipCardName){
                                            $("#win").find(".hint").html("不同等级的卡不可以做转卡操作");
                                            isSubmit=false;
                                        }
                                    }
                                }

                                if(isSubmit) {
                                    $("#win").find(".hint").html("可操作");
                                }

                            } else {
                                $("#win").find(".hint").html("系统内不存在该卡，请更换卡号后重新尝试");
                                $("[name='NewCardCode']").focus();

                            }
                        });
                    }
                }

            });

            var that = this;

            //卡操作事件绑定
            $("#simpleQuery").delegate(".commonBtn","click", function (e) {
                debugger;
                var me=$(this);
                that.loadData.args.imgSrc="";

                that.loadData.args.OperationType=me.data("optiontype");
                if(that.loadData.args.OperationType==1){
                    that.adjustIntegral();
                }else{
                    var  data={title:me.html()};
                    if(me.data("flag")){
                        data.title=me.data("flag");
                    }
                    that.SetVipCard(data);
                }

            })
            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                onClose:function(){
                    that.elems.vipCardCode="";
                },
                closable:true
            });
            $('#panlconent').layout({
                fit:true
            });

            /**************** -------------------弹出窗口初始化 end****************/

            $("#win").delegate(".radio","click",function(e){
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                }
                if(me.find("span").html().trim()=="其他"){
                    me.siblings(".easyui-validatebox").show(0).removeAttr("disabled");
                    me.siblings(".easyui-validatebox").validatebox({
                            required: true
                        });

                } else{
                    me.siblings(".easyui-validatebox").validatebox({
                  required: false

                    });
                    me.siblings(".easyui-validatebox").val("");
                    me.siblings(".easyui-validatebox").hide(0).attr({"disabled":"disabled"});
                }
                $.util.stopBubble(e);
            });
            $('#section table').delegate('td.checkBox', 'click', function (e) {
                $(this).toggleClass('on');
                that.stopBubble(e);
            });
            $('#nav04').delegate('.export', 'click', function (e) {
                that.loadData.exportVipAmount();
                that.stopBubble(e);
            });
            $('#nav03').delegate('.export', 'click', function (e) {
                that.loadData.exportVipIntegral();
                that.stopBubble(e);
            }).delegate('.adjust',"click",function(e){
                that.adjustIntegral();
            });
            $('#nav05').delegate('.export', 'click', function (e) {
                that.loadData.exportVipConsumerCard();
                that.stopBubble(e);
            });
            $('#nav02').delegate('.export', 'click', function (e) {
                that.loadData.exportVipOrderList();
                that.stopBubble(e);
            });
            $('#nav08').delegate('.export', 'click', function (e) {
                that.loadData.exportVipLogs();
                that.stopBubble(e);
            });
            $('#nav07').delegate('.export', 'click', function (e) {
               // that.loadData.exportVipLogs();
                that.stopBubble(e);
            });
            $('#nav07').delegate('.addICon', 'click', function (e) {
                 that.addCustomer();
                that.stopBubble(e);
            });

            $('#nav07').delegate(".fontC","click",function(e){
                debugger;
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.servicesLog.datagrid('selectRow', rowIndex);
                var row = that.elems.servicesLog.datagrid('getSelected');
                if(optType=="del") {
                    $.messager.confirm('提示', '您确定要删除客服记录吗？记录删除后将不再显示', function(r){
                        if (r){
                            that.loadData.operation(row,optType,function(data){
                                alert("操作成功");

                                that.loadData.GetVipServicesLogList(function (data) {
                                    debugger;
                                    var list = data.Data.VipServicesLogList;
                                    list = list ? list : [];
                                    debugger;

                                        that.elems.servicesLog.datagrid("loadData",list);

                                        if (data.Data.TotalPageCount > 0) {
                                            that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                        }


                                });
                            });

                        }
                    });

                }
                if(optType=="exit"){
                    that.elems.optionType="exit";
                    that.addCustomer();
                    $("#optionform").form('load',row);
                    var isqita=true;
                    $("#optionform").find(".radio").each(function(){
                        if($(this).find("span").html().trim()==row.ServicesMode.trim()) {
                            $(this).trigger("click");
                            isqita=false;
                        }

                    });
                    if(isqita) {
                        $("#optionform").find(".radio.out").trigger("click");
                    }
                }

            });

            $('#nav06').delegate('.export', 'click', function (e) {
                that.loadData.exportVipOnlineOffline();
                that.stopBubble(e);
            });

            //更新会员数据
            $('.linePanel').delegate('.saveBtn', 'click', function (e) {
                if($("#updateVip").form("validate")) {
                    debugger;
                    var fields = $("#updateVip").serializeArray();


                    that.loadData.operation(fields, "UpdateVip", function () {
                      //刷新数据
                        alert("操作成功");
                      that.loadVipDetail();
                  });
              }
            }).delegate(".adjust","click",function(){
                var type=$(this).data("type");

                switch (type){
                    case "integral": that.adjustIntegral();//调整积分
                     break;
                    case "balance":  that.adjustBalance();
                        that.loadData.args.OperationType=$(this).data("optiontype");
                    //调整余额
                     break;
					 case "cash":
					 	that.adjustCash();
                        //that.loadData.args.OperationType=$(this).data("optiontype");
                    //调整返现
                     break;
                }


            });
              ///标签事件的绑定和操作
            $('#nav09').delegate(".icon", "click", function () {   //删除操作按钮操作

                var me = $(this);
                me.parent().remove();
                debugger

                $("#groupTagList").find(".tagbtn").removeClass("bgtext");
                $("#tagList").find(".tagbtn").each(function(){
                    var tagLitNode=$(this);
                    $("#groupTagList").find(".tagbtn").each(function(){
                            var groupTagListNode=$(this);
                              if(groupTagListNode.data("tagid")==tagLitNode.data("tagid")){
                                  groupTagListNode.addClass("bgtext")
                              }

                    });
                });


            }).delegate(".tagbtn .icon", "mouseenter", function () {  //停靠显示和隐藏删除按钮
                $(this).find(".icon").stop().show(110)
            }).delegate(".tagbtn .icon", "mouseleave", function () {
                $(this).find(".icon").stop().hide(800)
            }).delegate(".tagbtn", "mouseenter", function () {  //停靠显示和隐藏删除按钮
                $(this).find(".icon").stop().show(110)
            }).delegate(".tagbtn", "mouseleave", function () {
                $(this).find(".icon").stop().hide(800)
            }).delegate(".tagbtn", "click", function () {
                var node=$(this);
                var obj={}

                    if (node.parents(".groupTaglist").length > 0) {
                        if(node.data("flag")&&node.data("flag")=='add'){ //添加按钮添加
                            if($("#TagsName").val().trim()) {
                                obj = {id: "", name: $("#TagsName").val().trim()}
                            }else{
                                return false;
                            }

                        }else {
                            obj = {id: node.data("tagid"), name: node.html()}
                            $(this).addClass("bgtext");
                        }
                        var domNode = $('<div class="tagbtn" data-tagid="123"></div>').html(obj.name).data("tagid", obj.id).data("tagname",obj.name);
                        domNode.append('<em class="icon"></em>');
                        console.log(domNode.data("tagid"));
                        var isAdd=true
                       $("#tagList").find(".tagbtn").each(function(){
                            var tagLitNode=$(this);
                            if(tagLitNode.data("tagname")==obj.name){
                                $.messager.alert("提示","已经添加不可重复");
                                isAdd=false;
                            }

                        });
                        if(isAdd) {
                            $("#tagList").append(domNode);
                        }
                    }


            }).delegate("li","click",function(){
                $(this).parent().find("li").removeClass("on");
                $(this).addClass("on");


                var typeId=$(this).data("tagid"),list=[];
                $.each(that.elems.TagTypesAndTags,function(index,filed){
                         if(typeId==filed.TypeId){
                             debugger;
                             list=filed.TagsList?filed.TagsList:[];
                             return false;
                         }

                });

                if (list.length) {
                    list = list ? list : [];
                    var html = bd.template('groupTagBtn', { list:list });
                    that.elems.groupTagList.html(html);
                    $("#groupTagList").find(".tagbtn").removeClass("bgtext");
                    $("#tagList").find(".tagbtn").each(function(){
                        var tagLitNode=$(this);
                        $("#groupTagList").find(".tagbtn").each(function(){
                            var groupTagListNode=$(this);
                            if(groupTagListNode.data("tagid")==tagLitNode.data("tagid")){
                                groupTagListNode.addClass("bgtext")
                            }

                        });
                    });

                } else{
                    list = list ? list : [];
                    var html = bd.template('groupTagBtn', { list:list });
                    that.elems.groupTagList.html(html);
                }

            }).delegate(".fontC","click",function(){
                debugger;
                that.loadData.args.vipTagList.PageIndex+=1;
                that.loadData.getTagTypeAndTags(function(data){
                    var list = data.Data.TagTypesAndTags;
                    if(that.loadData.args.vipTagList.PageIndex>=data.Data.TotalPages){
                        that.loadData.args.vipTagList.PageIndex=0;
                    }
                    that.elems.TagTypesAndTags=data.Data.TagTypesAndTags;
                    list = list ? list : [];

                    if (list.length) {
                        var html = bd.template('tagTypeList', { list: list });
                        $("ul.groupTag").html(html);
                        $("ul.groupTag li").eq(0).trigger("click")
                    }

                });
            }).delegate(".commonBtn","click",function(e){
                that.loadData.args.vipTagList.IdentityTagsList=[];
                $("#tagList").find(".tagbtn").each(function(){
                    var tagLitNode=$(this);
                    var obj={TagsId:tagLitNode.data("tagid"),TagsName:tagLitNode.data("tagname")}
                    that.loadData.args.vipTagList.IdentityTagsList.push(obj);

                });
                that.loadData.setVipTags(function(data){
                    alert("保存成功");
                    that.loadVipDetail();
                    that.loadData.GetVipDetail(function (data) {
                        debugger;
                        var list = data.Data.VipTags;
                        list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tagListBtn', { list: list });
                            that.elems.vipTagList.html(html);
                        }
                    });
                });

                $.Unit.stopBubble(e);
            });





            //保存客服记录
            $('#win').delegate('.saveBtn', 'click', function (e) {
               if($("#optionform").form("validate")) {
                   var fields = $("#optionform").serializeArray();


                   that.loadData.operation(fields, that.elems.optionType, function (rowData) {
                       $("#win").window("close");

                       switch (that.elems.optionType) {
                           case "addCoupon":
                               that.loadData.GetVipServicesLogList(function (data) {
                                   debugger;
                                   var list = data.Data.VipServicesLogList;
                                   list = list ? list : [];
                                   that.elems.servicesLog.datagrid("loadData", list);



                                   if (data.Data.TotalPageCount > 0) {
                                       that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                   }


                               });
                               alert("操作成功");
                            break;
                           case "updateIntegral":
                               $.messager.alert("提示","此次积分变动"+that.elems.Qty+",由原"+that.elems.vipCardInfo.Integration+"变动至"+page.elems.VipIntegral);
                             /*  that.loadData.GetVipCardDetail(function (data) {
                                   var list =data.Data.StatusLogList;
                                   list = list ? list : [];
                                   that.elems.pointTable.datagrid("loadData", list);


                               });*/
                               break;
                           case "VipCardUpdate":
                               if(that.loadData.args.OperationType==1)
                               {
                                   $.messager.alert("提示","此次余额变动"+that.elems.BalanceMones+",由原"+that.elems.vipCardInfo.BalanceAmount+"变动至"+page.elems.BalanceMoney);
                               }else {
                                  alert("操作成功");

                               }
                               if(that.loadData.args.OperationType==2||that.loadData.args.OperationType==5){
                                   debugger;
                                   location.href = "VipDetail.aspx?vipId=" +that.loadData.args.VipId + "&VipCardId="+rowData.Data.VipCardID+"&mid=" + $.util.getUrlParam("mid");

                               }
                            break;
							case "VipCashUpdate":
                               if(that.loadData.args.OperationType==1)
                               {
                                   $.messager.alert("提示","此次返现变动"+that.elems.CashMones+",由原"+that.elems.vipCardInfo.ValidReturnAmount+"变动至"+page.elems.CashMoney);
                               }else {
                                  alert("操作成功");

                               }
                               
                            break;

                       }
                       that.loadVipDetail();

                   });
               }
            });

            this.elems.tabs.delegate('li', 'click', function () {
                $('li', that.elems.tabs).removeClass('on');
                that.hidePanels();
                var panelId = $(this).attr('data-id');
                var panel = $('#' + panelId);
                that.elems.dataMessage= $('#' + panelId).find(".dataMessage");
                $(this).addClass('on');
                panel.show();
                that.elems.tabIndex=$(this).index();
               // debugger;
                var loadKey = 'loaded';
                var point = that.loadData.args.point;
				var cash = that.loadData.args.cash;
				var balance = that.loadData.args.balance;
                var amount = that.loadData.args.amount;
                var online = that.loadData.args.onlineOffline;
                var order = that.loadData.args.order;
				var integralList = that.loadData.args.integralList;//新积分列表
                var consumerCard = that.loadData.args.consumerCard;
                var logs = that.loadData.args.logs;
                var ServicesLog = that.loadData.args.ServicesLog;
                if (panel.attr(loadKey)) {
                    switch (panelId){
                        case 'nav02'://交易记录
							/*
							that.loadData.getVipOrderList(function (data) {
                                var list = data.Data.VipOrderList;
                                list = list ? list : [];
                                that.elems.content.datagrid("loadData", list);
                            });
							*/
							that.createPager('nav02', order.PageIndex,
                                order.TotalPages, order.TotalCount);
                         break;
                        case 'nav03'://卡操作记录
                            that.createPager('nav03', point.PageIndex,
                                point.TotalPages, point.TotalCount);
                            break;
                        case 'nav04'://电子优惠券
                            that.createPager('nav04', amount.PageIndex,
                                amount.TotalPages, amount.TotalCount);
                            break;
                        case 'nav06':
                            that.createPager('nav06', online.PageIndex,
                                online.TotalPages, online.TotalCount);
                            break;
                        case 'nav05':
                           /* that.createPager('nav05', consumerCard.PageIndex,
                                consumerCard.TotalPages, consumerCard.TotalCount);*/
                            break;
                        case 'nav07'://客服记录
                            that.createPager('nav07', ServicesLog.PageIndex,
                                ServicesLog.TotalPages, ServicesLog.TotalCount);
                            break;
                        case 'nav08':
                            that.createPager('nav08', logs.PageIndex,
                                logs.TotalPages, logs.TotalCount);
                            break;
						case 'nav011'://返现记录
							that.createPager('nav011', cash.PageIndex,
                                cash.TotalPages, cash.TotalCount);
                        	break;
						case 'nav012'://余额记录
							that.createPager('nav012', balance.PageIndex,
                                balance.TotalPages, balance.TotalCount);
                        	break;
						case "nav013"://新积分列表
							that.createPager('nav013', integralList.PageIndex,
                                integralList.TotalPages, integralList.TotalCount);
                        break;
                    }
                    return;
                }
            	//debugger;
                switch (panelId) {
                    // 会员标签加载
                    case 'nav09':
                        //that.loadData.args.VipId='928e7a79db0f4d8cbd935064e83026ad';
                        that.loadData.GetVipAddedTags(function (data) {
                            debugger;
                            var list = data.Data.VipTags;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tagListBtn', { list: list });
                                that.elems.vipTagList.html(html);
                            }
                            that.loadData.getTagTypeAndTags(function(data){
                                var list = data.Data.TagTypesAndTags;
                                that.elems.TagTypesAndTags=data.Data.TagTypesAndTags;
                                list = list ? list : [];
                                /*  if (list.length) {
                                    list = list ? list : [];
                                    var html = bd.template('groupTagBtn', { list:list });
                                    $("ul.groupTag li").eq(0);
                                    that.elems.groupTagList.html(html);
                                    $("#groupTagList").find(".tagbtn").removeClass("bgtext");
                                    $("#tagList").find(".tagbtn").each(function(){
                                        var tagLitNode=$(this);
                                        $("#groupTagList").find(".tagbtn").each(function(){
                                            var groupTagListNode=$(this);
                                            if(groupTagListNode.data("tagid")==tagLitNode.data("tagid")){
                                                groupTagListNode.addClass("bgtext")
                                            }

                                        });
                                    });

                                }
*/

                                if (list.length) {
                                    var html = bd.template('tagTypeList', { list: list });
                                    $("ul.groupTag").html(html);
                                    $("ul.groupTag li").eq(0).trigger("click")
                                }

                            });
                        });

                        break;

                    case 'nav02':  //交易记录
                        that.loadData.getVipOrderList(function (data) {
                            var list = data.Data.VipOrderList;
                            list = list ? list : [];
                              debugger;
                                //用百度模板引擎渲染成html字符串
                          /*   *//*   var html = bd.template("tpl_content", { list: list });
                                //将数据添加到页面的id=content的对象节点中*//*
                                that.elems.content.datagrid();*/
                            that.elems.content.datagrid({
                                data:list,
                                striped : false,
                                singleSelect:true,
                                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                rowStyler: function(index,row){
                                    if (row.OldVipCardCode){
                                        return 'background-color:#edeeee;';
                                    }
                                },


                                columns:[[

                                    {field : 'OrderNo',title : '订单编号',width:120,resizable:false,align:'left'},
										{field : 'TotalAmount',title : '订单金额(元)',width:120,resizable:false,align:'center'},								
										{field : 'PayType',title : '支付方式',width:120,align:'left',resizable:false},
										
					
										{field : 'PayStatus',title : '支付状态',width:100,align:'left',resizable:false},
					
										{field : 'OrderStatus',title : '订单状态',width:70,align:'center',resizable:false,
											styler: function(index,row){
												/* status: "700"
												 status_desc: "已完成"*/
												return 'color: #fc7a52;';    //rowStyle是一个已经定义了的ClassName(类名)
					
											}
					
										},
										{field : 'CreateTime',title : '下单日期',width:120,align:'center',resizable:false,
											formatter:function(value ,row,index){
												if(value) {
													return new Date(value).format("yyyy-MM-dd hh:mm");
												}
											}
										}

                                ]],

                                onLoadSuccess : function(data) {
                                    debugger;
                                    that.elems.content.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                    if(data.rows.length>0) {
                                        that.elems.dataMessage.hide();
                                    }else{
                                        that.elems.dataMessage.show();
                                    }
                                }

                            });
                                order.TotalCount = data.Data.TotalCount;
                                order.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 0) {
                                    that.createPager('nav02', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }

                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav08': //操作日志
                        that.loadData.getVipLogs(function (data) {
                            var list = data.Data.VipLogs;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tpl_logs', { list: list });
                                that.elems.logsTable.html(html);
                                logs.TotalCount = data.Data.TotalCount;
                                logs.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 0) {
                                    that.createPager('nav08', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            }else {
                                that.showTableTips(that.elems.logsTable, "该会员暂无操作记录!");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;

                    case 'nav07': //客服及路径
                        that.loadData.GetVipServicesLogList(function (data) {
                            var list = data.Data.VipServicesLogList;
                            list = list ? list : [];

                                 that.elems.servicesLog.datagrid({
                                     data:list,
                                     striped : true,
                                     singleSelect:true,
                                     fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                     columns:[[
                                         {field:'ServicesTime',title:'服务时间',width:160,align:'left'},
                                         {field:'ServicesMode',title:'服务方式',width:175,align:'left'},
                                         {field: 'UnitName', title: '服务门店', width: 175, align: 'left',
                                             formatter: function (value, row, index) {

                                                 var long = 28;
                                                 if (value && value.length > long) {
                                                     return '<div class="rowText" title="' + value + '">' + value.substring(0, long) + '...</div>'
                                                 } else {
                                                     return '<div class="rowText">' + value + '</div>'
                                                 }
                                             }



                                         },
                                         {field:'UserName',title:'服务员工',width:175,align:'left'},
                                         {field:'Content',title:'服务内容',width:175,align:'left',
                                             formatter:function(value ,row,index){

                                                 var long=28;
                                                 if(value&&value.length>long){
                                                     return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                 }else{
                                                     return '<div class="rowText">'+value+'</div>'
                                                 }
                                             }

                                        },
                                         {field : 'id',title : '编辑',width:81,align:'left',resizable:false,
                                             formatter:function(value ,row,index){
                                                 return '<p class="fontC exit" data-index="'+index+'" data-oprtype="exit"></p>';
                                             }
                                         },
                                         {field : 'VipID',title : '删除',width:81,align:'left',resizable:false,
                                             formatter:function(value ,row,index){
                                                 return '<p class="fontC delete" data-index="'+index+'" data-oprtype="del"></p>';
                                             }
                                         }
                                     ]],

                                     onLoadSuccess : function(data) {
                                         debugger;
                                         that.elems.servicesLog.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                         that.elems.dataMessage.hide();
                                         if(data.rows.length>0) {
                                             that.elems.dataMessage.hide();
                                         }else{
                                             that.elems.dataMessage.show();
                                         }
                                     }

                                 });
								 
								ServicesLog.TotalCount = data.Data.TotalCount;
                                ServicesLog.TotalPages = data.Data.TotalPageCount;

                                if (data.Data.TotalPageCount > 0) {
                                    that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                }


                            panel.attr(loadKey, true);
                        });
                        break;

                    case 'nav03':  //卡操作记录
                        that.loadData.GetVipCardDetail(function (data) {
                            var list = data.Data.StatusLogList;
                            list = list ? list : [];

                                that.elems.pointTable.datagrid({

                                    method : 'post',
                                    iconCls : 'icon-list', //图标
                                    singleSelect : true, //多选
                                    // height : 332, //高度
                                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                    striped : true, //奇偶行颜色不同
                                    collapsible : true,//可折叠
                                    //数据来源
                                    data:list,


                                    columns : [[

                                        {field : 'CreateTime',title : '日期',width:196,align:'left',resizable:false},

                                        {field : 'UnitName',title : '办卡门店',width:100,align:'left',resizable:false} ,
                                        {field : 'Action',title : '卡操作',width:80,align:'center',resizable:false} ,
                                        {field : 'ChangeReason',title : '原因',width:80,align:'center',resizable:false} ,
                                        {field : 'Remark',title : '备注',width:200,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=20;
                                                debugger;
                                                if(!value){value="无"}
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        } ,

                                        {field : 'CreateBy',title : '操作人',width:80,align:'center',resizable:false} ,
                                        {field : 'ImageUrl',title : '图片',width:100,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var html='无';
                                                if(value){
                                                    html='<div id="imageListPanel_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                                        '<div>'
                                                }

                                                return html;
                                            }

                                        }
                                        /*    {field : 'VipCardStatusID',title : '卡状态',width:96,align:'center',resizable:false,
                                         formatter:function(value ,row,index){
                                         //会员卡状态标识(0未激活，1正常，2冻结，3失效，4挂失，5休眠)
                                         switch (value){
                                         case 0 : staus="未激活";break;
                                         case 1 : staus= "正常";break;
                                         case 2 : staus= "冻结";break;
                                         case 3 : staus= "失效";break;
                                         case 4 : staus= "挂失";break;
                                         case 5 : staus="休眠";break;

                                         }
                                         return staus;
                                         }
                                         },*/


                                    ]],

                                    onLoadSuccess : function(data) {
                                        console.log("nav03 执行");
                                        debugger;
                                        that.elems.pointTable.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                        that.elems.dataMessage.hide();
                                        if(data.rows.length>0) {
                                            that.elems.dataMessage.hide();
                                        }else{
                                            that.elems.dataMessage.show();
                                        }
                                        for(var i=0;i<data.rows.length;i++) {
                                            $('#imageListPanel_'+i).tooltip({
                                                position: 'top',
                                                content: '<img class="imgShow" width="257" height="176" src="'+data.rows[i].ImageUrl+'">'
                                            });
                                        }
                                    }
                                });

							
                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav05':    //消费卡
                        that.loadData.getVipCardList(function (data) {
                            debugger;
                            var list = data.Data.VipCardList;
                            list = list ? list : [];
                            if (list.length) {
                                for(var i=0;i<list.length;i++){
                                    //正常，冻结，失效，挂失，休眠)
                                   switch (list[i].VipCardStatusID){
                                       case  0:list[i]["VipCardStatusName"]="未激活"; break;
                                       case  1:list[i]["VipCardStatusName"]="正常";break;
                                       case  2:list[i]["VipCardStatusName"]="已冻结"; break;
                                       case  3:list[i]["VipCardStatusName"]="已失效";


                                           break;
                                       case  4:list[i]["VipCardStatusName"]="已挂失"; break;
                                       case  5:list[i]["VipCardStatusName"]="已休眠"; break;
                                   }
                                    list[i].ImageUrl= list[i].ImageUrl? list[i].ImageUrl:"images/default.png";
                                }
                                var myMid = JITMethod.getUrlParam("mid");
                                var html = bd.template('tpl_consumer', { list: list,mid:myMid });

                                that.elems.consumerTable.html(html);
                                consumerCard.TotalCount = data.Data.TotalCount;
                                consumerCard.TotalPages = data.Data.TotalPages;
                               /* if (data.Data.TotalPages > 0) {
                                    that.createPager('nav05', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }*/
                            } else {
                                that.elems.consumerTable.html( "该会员没有办理过会员卡");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav06':   //上线与下线
                        that.loadData.getVipOnlineOffline(function (data) {
                            debugger;
                            var list = data.Data.VipOnlineOfflines;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tpl_online', { list: list });
                                that.elems.onlineTable.html(html);
                                online.TotalCount = data.Data.TotalCount;
                                online.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 0) {
                                    that.createPager('nav06', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                that.showTableTips(that.elems.onlineTable, "该会员上线与下线暂无变更记录!");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav04':  //优惠券
                        that.loadData.getVipConsumeCardList(function (data) {
                            debugger;
                            var list = data.Data.VipConsumeCardList;
                            list = list ? list : [];

                                that.elems.amountTable.datagrid({

                                    method : 'post',
                                    iconCls : 'icon-list', //图标
                                    singleSelect : true, //单选
                                    // height : 332, //高度
                                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                    striped : true, //奇偶行颜色不同
                                    collapsible : true,//可折叠
                                    //数据来源
                                    data:list,


                                    columns : [[

                                        {field : 'CouponCode',title : '优惠券编码',width:100,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=26;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },

                                        {field : 'CouponName',title : '优惠券名称',width:100,align:'left',resizable:false },

                                        {field : 'Remark',title : '优惠券描述',width:200,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                            var long=56;
                                            if(value&&value.length>long){
                                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                            }else{
                                                return '<div class="rowText">'+value+'</div>'
                                            }
                                        }
                                        },  {field : 'EndDate',title : '有效期至',width:80,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                return value;//new Date(value).format("yyyy-MM-dd");
                                            }
                                        }, {field : 'CouponStatus',title : '状态',width:60,align:'center',resizable:false}



                                    ]],

                                    onLoadSuccess : function(data) {
                                        debugger;
                                        that.elems.amountTable.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                        if(data.rows.length>0) {
                                            that.elems.dataMessage.hide();
                                        }else{
                                            that.elems.dataMessage.show();
                                            //that.elems.amountTable.html("该会员无优惠券!");
                                        }
                                    }


                                });
                                amount.TotalCount = data.Data.TotalCount;
                                amount.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 0) {
                                    that.createPager('nav04', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }

                            panel.attr(loadKey, true);
                        });
                        break;
					case 'nav011':  //返现
                        that.loadData.getVipCashList(function (data) {
                            var list = data.Data.VipAmountDetailList;
                            list = list ? list : [];
								/*
                                var CumulativeIntegral=that.elems.vipInfo.CumulativeIntegral? that.elems.vipInfo.CumulativeIntegral:0;
                                var VipIntegral=that.elems.vipInfo.VipIntegral? that.elems.vipInfo.VipIntegral:0;
                                $("#CumulativeIntegral").html(CumulativeIntegral);
                                $("#VipIntegral").html(VipIntegral);
								*/
                                that.elems.tblCashBox.datagrid({

                                    method : 'post',
                                    iconCls : 'icon-list', //图标
                                    singleSelect : true, //多选
                                    //height : 332, //高度
                                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                    striped : true, //奇偶行颜色不同
                                    collapsible : true,//可折叠
                                    //数据来源
                                    data:list,


                                    columns : [[
                                        {field : 'CreateTime',title : '时间',width:180,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                return new Date(value).format("yyyy-MM-dd hh:mm:ss");
                                            }
                                        },
                                        {field : 'UnitName',title : '门店',width:125,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },

                                        {field : 'Amount',title : '返现增减',width:58,align:'center',resizable:false},
                                        {field : 'AmountSourceName',title : '变更类型',width:100,align:'center',resizable:false},

                                        {field : 'Reason',title : '原因',width:100,align:'left',resizable:false} ,
                                        {field : 'Remark',title : '备注',width:160,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },
                                        {field : 'CreateByName',title : '操作人',width:80,align:'center',resizable:false },
                                        {field : 'ImageUrl',title : '图片',width:80,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var html='无';
                                                if(value){
                                                    html='<div id="imageListPanel_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                                        '<div>'
                                                }

                                                return html;
                                            }
                                        }

                                    ]],

                                    onLoadSuccess : function(data) {
                                        console.log("nav011 执行");
                                        debugger;
                                        that.elems.tblCashBox.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                        that.elems.dataMessage.hide();
                                        if(data.rows.length>0) {
                                            that.elems.dataMessage.hide();
                                        }else{
											that.elems.tblCashBox.hide();
                                            that.elems.dataMessage.show();
                                        }
                                        
                                    }


                                });
								//that.loadData.args.cash.TotalCount
                                cash.TotalCount = data.Data.TotalCount;
                                cash.TotalPages = data.Data.TotalPageCount;
                                if (data.Data.TotalPageCount > 0) {
                                    that.createPager('nav011', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                }
                            panel.attr(loadKey, true);
                        });
                        break;
						case 'nav012':  //余额记录
                        that.loadData.getVipBalanceList(function (data) {
                            var list = data.Data.VipAmountDetailList;
                            list = list ? list : [];
								/*
                                var CumulativeIntegral=that.elems.vipInfo.CumulativeIntegral? that.elems.vipInfo.CumulativeIntegral:0;
                                var VipIntegral=that.elems.vipInfo.VipIntegral? that.elems.vipInfo.VipIntegral:0;
                                $("#CumulativeIntegral").html(CumulativeIntegral);
                                $("#VipIntegral").html(VipIntegral);
								*/
                                that.elems.tblBalanceBox.datagrid({

                                    method : 'post',
                                    iconCls : 'icon-list', //图标
                                    singleSelect : true, //多选
                                    // height : 332, //高度
                                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                    striped : true, //奇偶行颜色不同
                                    collapsible : true,//可折叠
                                    //数据来源
                                    data:list,


                                    columns : [[
                                        {field : 'CreateTime',title : '时间',width:180,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                return new Date(value).format("yyyy-MM-dd hh:mm:ss");
                                            }
                                        },
                                        {field : 'UnitName',title : '门店',width:125,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },

                                        {field : 'Amount',title : '余额增减',width:58,align:'center',resizable:false,
                                            formatter:function(value,row,index){
                                                if(isNaN(parseInt(value))){
                                                    return 0;
                                                }else{
                                                    return parseInt(value);
                                                }
                                            }
                                        },
                                        {field : 'AmountSourceName',title : '变更类型',width:100,align:'left',resizable:false},

                                        {field : 'Reason',title : '原因',width:100,align:'left',resizable:false} ,
                                        {field : 'Remark',title : '备注',width:160,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },
                                        {field : 'CreateByName',title : '操作人',width:80,align:'center',resizable:false },
                                        {field : 'ImageUrl',title : '图片',width:80,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var html='无';
                                                if(value){
                                                    html='<div id="imageListPanel_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                                        '<div>'
                                                }

                                                return html;
                                            }
                                        }

                                    ]],

                                    onLoadSuccess : function(data) {
                                        console.log("nav012 执行");
                                        debugger;
                                        that.elems.tblBalanceBox.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                        that.elems.dataMessage.hide();
                                        if(data.rows.length>0) {
                                            that.elems.dataMessage.hide();
                                        }else{
											that.elems.tblBalanceBox.hide();
                                            that.elems.dataMessage.show();
                                        }
										/*
                                        for(var i=0;i<data.rows.length;i++) {
                                            $('#imageListPanel_'+i).tooltip({
                                                position: 'top',
                                                content: '<img class="imgShow" width="257" height="176" src="'+data.rows[i].ImageUrl+'">'
                                            });
                                        }
										*/
                                    }


                                });
								//that.loadData.args.balance.TotalCount
                                balance.TotalCount = data.Data.TotalCount;
                                balance.TotalPages = data.Data.TotalPageCount;
                                if (data.Data.TotalPageCount > 0) {
                                    that.createPager('nav012', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                }
								
                            panel.attr(loadKey, true);
                        });
                        break;
						
					case "nav013":
						that.loadData.getVipPointList(function(data) {
                            var list = data.Data.VipIntegralList;
                            list = list ? list : [];
                                that.elems.tblIntegralBox.datagrid({
                                    method : 'post',
                                    iconCls : 'icon-list', //图标
                                    singleSelect : true, //多选
                                    // height : 332, //高度
                                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                    striped : true, //奇偶行颜色不同
                                    collapsible : true,//可折叠
                                    //数据来源
                                    data:list,
                                    columns : [[
                                        {field : 'Date',title : '时间',width:180,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                return new Date(value).format("yyyy-MM-dd hh:mm:ss");
                                            }
                                        },
                                        {field : 'UnitName',title : '门店',width:125,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },

                                        {field : 'Integral',title : '积分增减',width:58,align:'center',resizable:false,
                                            formatter:function(value,row,index){
                                                if(isNaN(parseInt(value))){
                                                    return 0;
                                                }else{
                                                    return parseInt(value);
                                                }
                                            }
                                        },
                                        {field : 'VipIntegralSource',title : '变更类型',width:100,align:'center',resizable:false},

                                        {field : 'Reason',title : '原因',width:100,align:'left',resizable:false} ,
                                        {field : 'Remark',title : '备注',width:160,align:'left',resizable:false,
                                            formatter:function(value ,row,index){
                                                var long=56;
                                                if(value&&value.length>long){
                                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                }else{
                                                    return '<div class="rowText">'+value+'</div>'
                                                }
                                            }
                                        },
                                        {field : 'CreateByName',title : '操作人',width:80,align:'center',resizable:false },
                                        {field : 'ImageUrl',title : '图片',width:80,align:'center',resizable:false,
                                            formatter:function(value ,row,index){
                                                var html='无';
                                                if(value){
                                                    html='<div id="imageListPanel_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                                        '<div>'
                                                }

                                                return html;
                                            }
                                        }

                                    ]],

                                    onLoadSuccess : function(data) {
                                        console.log("nav013 执行");
                                        debugger;
                                        that.elems.tblIntegralBox.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                                        that.elems.dataMessage.hide();
                                        if(data.rows.length>0) {
                                            that.elems.dataMessage.hide();
                                        }else{
                                            that.elems.dataMessage.show();
                                        }
                                        for(var i=0;i<data.rows.length;i++) {
                                            $('#imageListPanel_'+i).tooltip({
                                                position: 'top',
                                                content: '<img class="imgShow" width="257" height="176" src="'+data.rows[i].ImageUrl+'">'
                                            });
                                        }
                                    }


                                });
                                integralList.TotalCount = data.Data.TotalCount;
                                integralList.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 0) {
                                    that.createPager('nav013',1,data.Data.TotalPages, data.Data.TotalCount);
                                }
                            panel.attr(loadKey,true);
							
                        });
                        break;	
						
                    default:
                        break;
                }
            });
            //点击空白区域让指定的内容隐藏
            $("body").bind("click",function(e){
                  var target  = $(e.target);
                  if(target.closest(".selectList").length == 0){
                    $(".selectList").hide();
                  }
            });
            //所有的下拉框选择事件
            $("#nav01").delegate(".selectBox span", "click", function (e) {
                //获得当前元素jquery对象
                var $t = $(this);
                var selList=$t.parent().find(".selectList");
                //判断下拉列表是否是显示状态
                if(selList.is(":hidden")){
                    selList.show();
                }else{
                    selList.hide();
                }
                that.stopBubble(e);
            }).delegate(".selectBox p", "click", function (e) {
                //获得当前元素jquery对象
                var $t = $(this);
                //获得选择内容的id
                var optionId = $t.data("optionid");
                //改变显示的内容  及设置id
                $t.parent().parent().find(".text").html($t.html());
                $t.parent().parent().find(".text").attr("optionid", optionId);
                $t.parent().hide();
                that.stopBubble(e);
            }).delegate(".selectList","mouseleave",function(e){   //下拉列表离开隐藏
                $(this).hide();
                that.stopBubble(e);
            }).delegate(".ztree","mouseleave",function(e){   //树结构移开则隐藏
                $t=$(this);
                $t.hide();
                that.stopBubble(e);
            }).delegate("[name=editvipinfo]","click",function(e){  //针对tree进行的事件
                var $t=$(this);
                var forminfo=$t.data("forminfo");
                if(forminfo.DisplayType==205){ //tree
                    $t.parent().parent().find(".ztree").show();
                }
                that.stopBubble(e);
            });
        },
        //加载vip详细信息
        loadVipDetail: function () {
            $(".linePanel").find(".commonBtn").hide();
            var that = this;
            //获得详情信息
            this.loadData.GetVipCardDetail(function (data) {

                $("#simpleQuery").find(".commonBtn").each(function () {
                    debugger
                    if ($(this).data("show").toString().indexOf(data.Data.VipCardStatusId) == -1 || data.Data.VipCardID == null) {
                        $(this).hide();
                    }else{
                        $(this).show();
                    }
                });
                 debugger;
                var str = "暂未填写";
                //设置会员基本信息
                   if(data.Data &&data.Data.Col22=="N"){
                       $("#BirthdayHint").html("生日被修改过不可再修改");
                       $("#Birthday").datebox({ disabled:true});
                   }
                   debugger;
                var VipCardStatusName="未知";
                var isAddCLass=true;
                switch (data.Data.VipCardStatusId){
                    case -1:VipCardStatusName="废卡" ;break;
                    case 0:VipCardStatusName="未激活" ;break;
                    case 1:VipCardStatusName="正常" ;isAddCLass=false;$(".linePanel").find(".commonBtn").show();break;
                    case 2:VipCardStatusName="已冻结" ;break;
                    case 3:VipCardStatusName="已失效" ;
                        data.Data.CumulativeIntegral=0;         // 累计积分
                        data.Data.Integration=0;      //当前积分
                        data.Data.TotalAmount=0;  //累计金额
                        data.Data.BalanceAmount=0;  //当前余额
                        break;
                    case 4:VipCardStatusName="已挂失" ;break;
                    case 5:VipCardStatusName="已休眠" ;break;

                }

                if (data.Data.VipCardID == null)
                {
                    VipCardStatusName = "";
                }

                if(isAddCLass){
                    $("#status").addClass("bigColor");
                }else{
                    $("#status").removeClass("bigColor");
                }

                that.elems.vipCardInfo=data.Data;
                data.Data["VipCardStatusName"]=VipCardStatusName;
                that.loadData.args.VipId=data.Data.VipID;
                that.loadData.args.VipCardId=data.Data.VipCardID;
                that.loadData.args.VipCode=data.Data.VipCode;
                that.loadData.args.VipCardCode=data.Data.VipCardCode;
                debugger;
                $("#updateVip").form("load",data.Data);
                $("#loadForm").form("load",data.Data);
                that.elems.tabs.find("li").eq(that.elems.tabIndex).trigger("click");

            });
        },



        //调整余额;
        adjustBalance:function(){
            var that=this;
            that.elems.optionType="VipCardUpdate";
            var top=$(document).scrollTop()+60;
            $('#win').window({title:"余额调整",width:630,height:500,top:top,left:($(window).width()-630)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_adjustBalance');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
            that.registerUploadImgBtn ();
            that.loadData.args.imgSrc=""
        },
		//调整返现;
        adjustCash:function(){
            var that=this;
            that.elems.optionType="VipCashUpdate";
            var top=$(document).scrollTop()+60;
            $('#win').window({title:"返现调整",width:630,height:500,top:top,left:($(window).width()-630)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_adjustCash');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
            that.registerUploadImgBtn ();
            that.loadData.args.imgSrc=""
        },
        // 设置会员卡状态
        SetVipCard:function(data){
            var that=this;
            that.elems.optionType="VipCardUpdate";
            if(!data.title){data.title="会员卡状态变更"}
            var height=380,width=630
            if(that.loadData.args.OperationType==3){
                height=460
            } else if(that.loadData.args.OperationType==2||that.loadData.args.OperationType==5){
                height=400
            }
            var html=bd.template('tpl_setVipCard');
            if(that.loadData.args.OperationType==10){
                html= bd.template('tpl_activate');
                height=450;
                width=380
            }
			var dataReason=[],
				dataVipCard=[];
            switch (that.loadData.args.OperationType){
                case 1:

                    break;
                case 2:   //升级
                    dataReason=[{
                        "label":"现金",
                        "value":"现金"

                    },{
                        "label":"积分抵扣",
                        "value":"积分抵扣"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
					
					that.loadData.getVipCardTypeList(function(data){
						dataVipCard = data.Data.VipCardTypeIdList;
						dataVipCard = dataVipCard ? dataVipCard : [];
						dataVipCard.push({
							"VipCardTypeId": "",
							"VipCardTypeName": "请选择",
							"selected":true
						});
						console.log(dataVipCard);
					});
					

                    break;
                case 3:   //挂失
                    dataReason=[{
                        "label":"卡遗失",
                        "value":"卡遗失" ,
                        "selected":true
                    }] ;

                    break;
                case 4:  //冻结
                    dataReason=[{
                        "label":"无效会员",
                        "value":"无效会员"

                    },{
                        "label":"消费异常",
                        "value":"消费异常"

                    },{
                        "label":"退卡",
                        "value":"退卡"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 5: //转卡
                    dataReason=[{
                        "label":"换卡",
                        "value":"换卡"

                    },{
                        "label":"芯片破损",
                        "value":"芯片破损"

                    },
                        {
                            "label":"补办",
                            "value":"补办"

                        },
                        {
                            "label":"卡号破损",
                            "value":"卡号破损"

                        },{
                            "label":"0",
                            "value":"请选择" ,
                            "selected":true
                        }];
                    break;
                    break;
                case 6: //解挂
                    dataReason=[{
                        "label":"卡找到",
                        "value":"卡找到" ,
                        "selected":true
                    }] ;
                    break;
                case 7: //解冻
                    dataReason=[{
                        "label":"会员激活 ",
                        "value":"会员激活"

                    },{
                        "label":"消费异常已调整",
                        "value":"消费异常已调整"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 8: //作废
                    dataReason=[{
                        "label":"发卡错误 ",
                        "value":"发卡错误"

                    },{
                        "label":"新卡遗失",
                        "value":"新卡遗失"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 9: //唤醒
                    dataReason=[{
                        "label":"会员激活",
                        "value":"会员激活" ,
                        "selected":true
                    }] ;
                    break;
                case 10: //激活
                    break;
            }
            $("#ChangeReason").combobox({data:dataReason});
			$("#ChangeVipCard").combobox({data:dataVipCard});//选择会员卡
            var top=$(document).scrollTop()+60;
            $('#win').window({title:data.title,width:width,height:450,top:top,left:($(window).width()-width)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');



            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').find(".showHide").each(function(){
                var me= $(this);
                if(me.data("show")&&me.data("show").indexOf(that.loadData.args.OperationType)!=-1){
                    me.show();

                } else if(me.data("show")){
                    me.remove();
                }
                if(me.data("hide")&&me.data("hide").indexOf(that.loadData.args.OperationType)!=-1){
                    me.remove();
                } else if(me.data("hide")){
                    me.show();
                }
            });


            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
            that.registerUploadImgBtn ();
            $("#ChangeReason").combobox({data:dataReason});
			$("#ChangeVipCard").combobox({data:dataVipCard});//选择会员卡
            that.loadData.args.imgSrc=""
        },

        //加载页面的数据请求,dataId表示显示哪个tab下面的表格
        //加载更多的资讯或者活动
        loadMoreData: function (dataId, currentPage) {

            var that = this;
            //that.elems.dataMessage.show();
            //请求接口参数下标从1开始      分页的是从1开始
            switch (dataId) {
                //卡操作记录
                case 'nav03':
					this.loadData.args.point.PageIndex = currentPage;
                    that.loadData.GetVipCardDetail(function (data) {
                        var list = data.Data.StatusLogList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断
                      	/*if(list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }*/
                        that.elems.pointTable.datagrid("loadData", list);
                    });
                    break;
                    //操作日志
                case 'nav08':
                    this.loadData.args.logs.PageIndex = currentPage;
                    that.loadData.getVipLogs(function (data) {
                        var list = data.Data.VipLogs;
                        list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tpl_logs', { list: list });
                            that.elems.logsTable.html(html);
                        }
                    });
                    break;
                case 'nav07':
                    this.loadData.args.ServicesLog.pageIndex = currentPage;
                    that.loadData.GetVipServicesLogList(function (data) {
                        var list = data.Data.VipServicesLogList;
                        list = list ? list : [];
                        debugger;
                        if (list.length) {
                            that.elems.servicesLog.datagrid("loadData",list);
                        }else {

                        }
                        panel.attr(loadKey, true);
                    });
                    break;
                //消费卡
                case 'nav05':
                  /*  this.loadData.args.cardList.PageIndex = currentPage;
                    that.loadData.getVipConsumeCardList(function (data) {
                        var list = list ? list : [];
                        if(list.length){
                            var html = bd.template('tpl_consumer', { list: list });
                            that.elems.consumerTable.html(html);
                        }
                    });*/
                    break;
                //上线与下线
                case 'nav06':
                    this.loadData.args.onlineOffline.PageIndex = currentPage;
                    that.loadData.getVipOnlineOffline(function (data) {
                        var list = data.Data.VipOnlineOfflines;
                        list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tpl_online', { list: list });
                            that.elems.onlineTable.html(html);
                        }
                    });
                    break;
                //帐内余额
                case 'nav04':
                    this.loadData.args.amount.PageIndex = currentPage;
                    debugger;
                    that.loadData.getVipConsumeCardList(function (data) {
                        var list = data.Data.VipConsumeCardList;
                        list = list ? list : [];
                        /*if (list.length) {
                           *//* var html = bd.template('tpl_amount', { list: list });*//*

                        }*/ that.elems.amountTable.datagrid("loadData", list);
                    });
                    break;
                //交易记录
                case "nav02":
                    this.loadData.args.order.PageIndex = currentPage;
                    that.loadData.getVipOrderList(function (data) {
                        var list = data.Data.VipOrderList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断
                      /* if (list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }*/
						that.elems.content.datagrid("loadData", list);//that.elems.amountTable
                    });
                    break;
				//返现
                case "nav011":
                    this.loadData.args.cash.PageIndex = currentPage;
                    that.loadData.getVipCashList(function (data) {
                        var list = data.Data.VipAmountDetailList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断
                      	/*if(list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }*/
                        that.elems.tblCashBox.datagrid("loadData", list);
                    });
                    break;
				//余额
                case "nav012":
                    this.loadData.args.balance.PageIndex = currentPage;
                    that.loadData.getVipBalanceList(function (data) {
                        var list = data.Data.VipAmountDetailList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断
                      	/*if(list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }*/
                        that.elems.tblBalanceBox.datagrid("loadData", list);
                    });
                    break;
				//积分列表
                case "nav013":
                    this.loadData.args.integralList.PageIndex = currentPage;
                    that.loadData.getVipPointList(function (data) {
                        var list = data.Data.VipIntegralList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断
                      	/*if(list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }*/
                        that.elems.tblIntegralBox.datagrid("loadData", list);
                    });
                    break;	
						
                default:
                    break;
            }

        },
        //新增客服记录
        addCustomer :function(data){
            var that=this;
            that.elems.optionType="addCoupon";
            var top=$(document).scrollTop()+60;
            $('#win').window({title:"新增客服记录",width:660,height:380,top:top,left:($(window).width()-660)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addCustomer');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            //this.loadData.tag.orderID=data.OrderID;
            $('#win').window('open');
            $("#win").find(".radio").eq(0).trigger("click");
            $("#searchInputUnitName").val(window.UnitName);
            $("#userName").val(window.UserName);

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
        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
            $("#win").find(".uploadBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;



            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                self.loadData.args.imgSrc=data.url;
                $(ele).siblings(".imgPanel").find("img").show().attr({"src":data.url,width:100,height:100});

            });

        },
        //上传图片
        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:37,
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
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },

        loadData: {

            args: {
                VipId: "", //会员Id
                VipCardISN:"",//卡内码
                VipCode:"",
                OperationType:-100,//卡状态的操作
                EditVipInfoColumns:[],//更新会员的动态属性信息
                SearchColumns:[],
                //卡操作
                point: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
				integralList: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //操作日志
                logs: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
                },
                //会员卡
                cardList: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
                },

                //交易记录
                order: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },

                amount: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //消费卡
                consumerCard: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //客服记录
                ServicesLog: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //上线与下线
                onlineOffline:
                {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //上线与下线
                vipTagList:
                {
                    PageIndex: 1,
                    PageSize: 3,
                    TotalPages: 0,
                    IdentityTagsList:[],
                    TotalCount: 0
                },
				balance: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
				cash: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                }
            },
            //设置会员标签
            setVipTags:function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipTags.ashx",
                    data: {
                        action: 'SetVipTags',
                        VipId: this.args.VipId,
                        IdentityTagsList:this.args.vipTagList.IdentityTagsList
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
            // 获取系统已有的标签
            getTagTypeAndTags: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipTags.ashx",
                    data:{
                        action:"GetTagTypeAndTags",
                        PageIndex: this.args.vipTagList.PageIndex,
                        PageSize: this.args.vipTagList.PageSize
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

            //获取会员已添加的标签
            GetVipAddedTags: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: 'GetVipDetail',
                        VipId: this.args.VipId

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


            //获取会员详情
            GetVipCardDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.VIPCard.GetVipCardDetail',
                        VipId: this.args.VipId,
                        VipCardID:this.args.VipCardId
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
            //获取会员操作日志
            getVipLogs: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: 'GetVipLogs',
                        VipId: this.args.VipId,
                        PageIndex: this.args.logs.PageIndex,
                        PageSize: this.args.logs.PageSize
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

            //获得会员订单
            getVipOrderList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipOrderList",
                        VipId: this.args.VipId,   //参数会员id
                        PageIndex: this.args.order.PageIndex,
                        PageSize: this.args.order.PageSize
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
             //获取会员消费记录
            getVipCardTransLogList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "VIP.VipCardTransLog.GetVipCardTransLogList",
                        VipCardCode: this.args.VipCardCode   //参数会员id
                       /* PageIndex: this.args.order.PageIndex,
                        PageSize: this.args.order.PageSize*/
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

             //获取会员持卡列表
            getVipCardList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "VIP.VIPCard.GetVipCardList",
                        VipId: this.args.VipId,   //参数会员id
                        PageIndex: this.args.cardList.PageIndex,
                        PageSize: this.args.cardList.PageSize
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
			
			 //获取会员卡类型列表
            getVipCardTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
					async: false,
                    data: {
                        action: "VIP.VIPCard.GetVipCardTypeList",
                        VipId: this.args.VipId   //参数会员id
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
            getVipConsumeCardList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipConsumeCardList',
                        VipId:this.args.VipId,
                        PageIndex: this.args.amount.PageIndex,
                        PageSize: this.args.amount.PageSize
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

            //获取会员客服记录列表
            GetVipServicesLogList: function (callback) {
                $.util.ajax({
                    url: '/Applicationinterface/Gateway.ashx',
                    data: {
                        action: 'VIP.ServicesLog.GetVipServicesLogList',
                        VipID:this.args.VipId ,
                        PageIndex: this.args.ServicesLog.PageIndex,
                        PageSize: this.args.ServicesLog.PageSize
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

             // 按钮操作接口调用
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                var submit={is:true,msg:""};
                var  VipIntegral=0;
                var  BalanceMoney=0;
				var  CashMoney = 0;
                prams.url="/Applicationinterface/Gateway.ashx";
                switch(operationType){
                    //客服记录的新增和添加
                    case "addCoupon":
                    case "exit":
                        prams.data["ServicesTime"]=new Date().format("yyyy-MM-dd hh:mm");
                        prams.data["ServicesMode"]=$(".radio.on[data-name]").find("span").html();
                        prams.data.action="VIP.ServicesLog.SetVipServicesLog";  //编辑和新增客服记录
                        $.each(pram, function (index, field) {
                            if(field.value!=="") {
                                prams.data[field.name] = field.value;
                            }
                        });
                        break;

                    case "del":prams.data.action="VIP.ServicesLog.DelVipServicesLog"; //删除  客服记录
                        prams.data["ServicesLogID"]=pram.ServicesLogID;
                        break;
                    case  "updateIntegral":
                        prams.data.action="VIP.VipIntegral.SetVipIntegral";

                        prams.data["VipCode"]=this.args.VipCode;
                        $.each(pram, function (index, field) {
                                prams.data[field.name] = field.value;
                            if(field.name=="Qty"&&field.value==0){
                                submit.is=false;
                                submit.msg="积分调整数量不能为零，请重新填写";

                            }
							
                            if(field.name=="Qty"){

                                VipIntegral =page.elems.vipCardInfo.Integration+parseInt(field.value);
                                if(VipIntegral<0){
                                    submit.is=false;
                                    submit.msg="扣除积分不可超出当前积分";
                                }
                                page.elems.Qty =field.value
                            }
                        });
                        prams.data["IntegralSourceID"]="27";
                        prams.data["ImageUrl"]=this.args.imgSrc;
                        break;
                    case "UpdateVip":

                        $.each(pram, function (index, field) {
                            if(field.value!=="") {
                                prams.data[field.name] = field.value;
                            }
                        });

                        prams.data["Col22"]="";
                        if(!page.elems.vipCardInfo.Col22||page.elems.vipCardInfo.Col22.toLowerCase()=="y") {
                           if(prams.data["Birthday"]&&prams.data["Birthday"]!=page.elems.vipCardInfo.Birthday){
                                prams.data["Col22"]="Y";
                           } else{
                               prams.data["Birthday"] =page.elems.vipCardInfo.Birthday;//Birthday 输入框被禁用 生日不改
                           }
                        } else{
                            prams.data["Birthday"] =page.elems.vipCardInfo.Birthday;  //如果是N 生日不改
                        }
                        prams.data.action="VIP.VipManager.UpdateVip";
                      break;
                    case "VipCardUpdate":




                        $.each(pram,function(index,field){

                            prams.data[field.name]=field.value;
                            if(field.name=="Amount"&&field.value==0){
                                submit.is=false;
                                submit.msg="余额调整数量不能为零，请重新填写";
                            }
                            if(field.name=="Amount"){
                                BalanceMoney =page.elems.vipCardInfo.BalanceAmount+parseInt(field.value);
                                if(BalanceMoney<0){
                                    submit.is=false;
                                    submit.msg="扣除余额不可超出当前余额";
                                }
                                page.elems.BalanceMones =field.value
                            }
                        });

                        if(this.args.OperationType==3||this.args.OperationType==1) {
                            if(this.args.imgSrc){
                                prams.data["ImageUrl"]=this.args.imgSrc
                            }else{
                                /* $.messager.alert("错误提示","必须上传一张图片");
                                 return false;*/
                            }

                        }

                        prams.data["VipCardID"]= this.args.VipCardId;
                        prams.data["OperationType"]= this.args.OperationType;
						prams.data["AmountSourceID"]= '23';
                        if(this.args.OperationType==1) {  //累计积分，余额  返现, 调用接口，
                            prams.data.action = "VIP.VipAmount.SetVipAmount";
                        }else {
                            prams.data.action = "VIP.VIPCard.SetVipCard";
							
							
							var tit = $('#win .tit .radioBox.on').data('rd');
							if(tit==1){
								delete prams.data.VipCardTypeId;
							}else if(tit==2){
								delete prams.data.NewCardCode;
							}
                        }

                        break;
					case "VipCashUpdate":

                        prams.data.action="VIP.VipAmount.SetVipAmount";
                        $.each(pram,function(index,field){
                            prams.data[field.name]=field.value;
                            if(field.name=="Amount"&&field.value==0){
                                submit.is=false;
                                submit.msg="返现调整数量不能为零，请重新填写";

                            }
                            if(field.name=="Amount"){
								

                                CashMoney =page.elems.vipCardInfo.ValidReturnAmount+parseInt(field.value);
                                if(CashMoney<0){
                                    submit.is=false;
                                    submit.msg="扣除返现不可超出当前返现";
                                }
                                page.elems.CashMones =field.value
                            }
                        });

                        if(this.args.OperationType==1) {
                            if(this.args.imgSrc){
                                prams.data["ImageUrl"]=this.args.imgSrc
                            }else{
                                /* $.messager.alert("错误提示","必须上传一张图片");
                                 return false;*/
                            }

                        }

                        prams.data["VipCardID"]= this.args.VipCardId;
						prams.data["AmountSourceID"]= '24';
						
                        //prams.data["OperationType"]= this.args.OperationType;

                        break;	
						
						
						
						

                }
                prams.data["VipID"]=this.args.VipId;
                if(!submit.is) {$.messager.alert("提示",submit.msg); return false;}


                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            page.elems.VipIntegral=VipIntegral;
                            page.elems.BalanceMoney=BalanceMoney;
							page.elems.CashMoney = CashMoney;
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            },

            //根据form数据 和 请求地址 导出数据到表格
            exportExcel: function (data, url) {
                var dataLink = JSON.stringify(data);
                var form = $('<form>');
                form.attr('style', 'display:none;');
                form.attr('target', '');
                form.attr('method', 'post');
                form.attr('action', url);
                var input1 = $('<input>');
                input1.attr('type', 'hidden');
                input1.attr('name', 'req');
                input1.attr('value', dataLink);
                $('body').append(form);
                form.append(input1);
                form.submit();
                form.remove();
            },
            //导出会员操作记录
            exportVipLogs: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipLogs';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.logs.PageSize,
                        PageIndex: this.args.logs.PageIndex,
                        OrderType: this.args.logs.OrderType,
                        VipId: this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
            //导出上线与下线
            exportVipOnlineOffline: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipOnlineOffline';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.onlineOffline.PageSize,
                        PageIndex: this.args.onlineOffline.PageIndex,
                        OrderType: this.args.onlineOffline.OrderType,
                        VipId: this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
            //导出消费卡记录
            exportVipConsumerCard: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipConsumerCard';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.consumerCard.PageSize,
                        PageIndex: this.args.consumerCard.PageIndex,
                        OrderType: this.args.consumerCard.OrderType,
                        VipId: this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
            //导出交易记录
            exportVipOrderList: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipOrderList';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.order.PageSize,
                        PageIndex: this.args.order.PageIndex,
                        OrderType: this.args.order.OrderType,
                        VipId: this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
            //导出积分
            exportVipIntegral: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipIntegral';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.point.PageSize,//this.args.integralList.PageIndex
                        PageIndex: this.args.point.PageIndex,
                        OrderType: this.args.point.OrderType,
                        VipId: this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
            //导出账户余额
            exportVipAmount: function () {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipAmount';//&req=';
                var data = {
                    Parameters: {
                        PageSize: this.args.amount.PageSize,
                        PageIndex: this.args.amount.PageIndex,
                        OrderType: this.args.amount.OrderType,
                        VipId:this.args.VipId
                    }
                };
                this.exportExcel(data, getUrl);
            },
			//获取返现列表
			getVipCashList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Gateway.ashx',
                    data: {
                        action: 'VIP.VipAmount.GetVipAmountDetail',
                        //用vipid:4e98550ffb2749e49f4a1b53a5da10b1测试
                        VipId:this.args.VipId,
                        VipCardCode:this.args.VipCode,
						Type:2,//返现
                        PageIndex: this.args.cash.PageIndex,
                        PageSize: this.args.cash.PageSize
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
			//获取余额列表
			getVipBalanceList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Gateway.ashx',
                    data: {
                        action: 'VIP.VipAmount.GetVipAmountDetail',
                        //用vipid:4e98550ffb2749e49f4a1b53a5da10b1测试
                        VipId:this.args.VipId,
                        VipCardCode:this.args.VipCode,
						Type:1,//余额
                        PageIndex: this.args.balance.PageIndex,
                        PageSize: this.args.balance.PageSize
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
		
			
            //获取会员积分列表
            getVipPointList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipIntegralList',
                        //用vipid:4e98550ffb2749e49f4a1b53a5da10b1测试
                        VipId:this.args.VipId,
                        VipCode:this.args.VipCode,
                        PageIndex: this.args.integralList.PageIndex,
                        PageSize: this.args.integralList.PageSize
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
            //获取会员上线与下线
            getVipOnlineOffline: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipOnlineOffline',
                        //for test '191BF3A2-285E-4915-9401-F89F9C6F1507'
                        VipId: this.args.VipId,
                        PageIndex: this.args.onlineOffline.PageIndex,
                        PageSize: this.args.onlineOffline.PageSize
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
            //获取会员账户余额列表
            getVipAmountList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipAmountList',
                        //for test vipid:d5d8e5a8bde9408298bc75f3a33d50d8
                        VipId: this.args.VipId,
                        PageIndex: this.args.amount.PageIndex,
                        PageSize: this.args.amount.PageSize
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
            //获得会员详细信息     
            getVipDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipDetail",
                        VipId: this.args.VipId,
                        VipCardISN:this.args.VipCardISN
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

            //页面权限分组
            getFunctionList: function (callback) {
                $.util.ajax({
                    url: "/applicationinterface/Gateway.ashx",
                    data: {
                        action: "Basic.Menu.GetFunctionList",
                        MenuID:$("#leftMenu").find(".on a").attr("id")?$("#leftMenu").find(".on a").attr("id"): $.util.getUrlParam("mid")

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
            getVipCardStatus: function (callback) {

                var prams = {data: {action: ""}};

                prams.data = {
                    action: "VIP.VIPCard.GetVipCardDetail",

                };
                $.each(this.args.SearchColumns, function (i, field) {
                    if (field.name=="NewCardCode"&&field.value) {
                        prams.data["VipCardISN"] = field.value; //提交的参数
                    }

                });
                debugger;

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,

                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示", data.Message);
                            page.loadData.args.type = 2;
                        }
                    }
                });


            }
        }

    };
    page.init();
});