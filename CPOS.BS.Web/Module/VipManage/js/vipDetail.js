define(['jquery', 'template', 'tools', 'kkpager', 'artDialog','easyui', 'datetimePicker','zTree'], function ($) {

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
            tabs: $('#section .subMenu ul'),
            content: $("#content"),     //交易记录表格数据
            pointTable: $('#tblPoint'),   //积分明细
            amountTable: $('#tblAmount'),
            onlineTable: $('#tblOnline'),
            logsTable:$('#tblLogs'),
            servicesLog:$('#servicesLog'),
            consumerTable: $('#tblConsumer'),
            pager: $('#kkpager')
        },
        init: function () {
            //获得地址栏参数为vipId的值
            var vipId = $.util.getUrlParam("vipId");
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.VipId = vipId;
            //请求数据
            //this.loadPageData();
            //请求会员详细信息   注:暂无接口以及文档
            this.loadVipDetail();
            //初始化事件
            this.initEvent();
        },
        hidePanels: function () {
            $('#nav01,#nav02,#nav03,#nav04,#nav05,#nav06,#nav07,#nav08').hide();
        },
        //没有数据的table提示
        showTableTips: function (jqObj, tips) {
            var noContent = bd.template("tpl_noContent", { tips: tips });
            jqObj.html(noContent);
        },
        //事件绑定方法
        initEvent: function () {
            var that = this;
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
                    $.messager.confirm('删除客服记录', '您想确定要删除客服记录吗？', function(r){
                        if (r){
                            that.loadData.operation(row,optType,function(data){
                                alert("操作成功");

                                that.loadData.GetVipServicesLogList(function (data) {
                                    debugger;
                                    var list = data.Data.VipServicesLogList;
                                    list = list ? list : [];
                                    debugger;
                                    if (list.length) {
                                        that.elems.servicesLog.datagrid("loadData",list);

                                        if (data.Data.TotalPageCount > 0) {
                                            that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                        }
                                    }else {
                                        that.showTableTips(that.elems.servicesLog, "该会员无操作记录!");
                                    }
                                    panel.attr(loadKey, true);
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
                        if($(this).find("span").html().trim()==row.ServicesMode) {
                            $(this).trigger("click");
                            isqita=false;
                        }

                    });
                    if(isqita) {
                        $("#optionform").find(".radio.out").trigger("click");
                    }
                }

            })

            $('#nav06').delegate('.export', 'click', function (e) {
                that.loadData.exportVipOnlineOffline();
                that.stopBubble(e);
            });

            //更新数据
            $('#nav01').delegate('.saveBtn', 'click', function (e) {
                that.setEditVipInfoCondition();
                that.loadData.updateVipInfo(function (data) {
                    alert('会员信息更新成功',true);
                    //刷新数据
                    that.loadVipDetail();
                });
                that.stopBubble(e);
            });
            //保存客服记录
            $('#win').delegate('.saveBtn', 'click', function (e) {
               if($("#optionform").form("validate")) {
                   var fields = $("#optionform").serializeArray();

                   that.loadData.operation(fields, "addCoupon", function () {
                       $("#win").window("close");
                       alert("操作成功");
                       that.loadData.GetVipServicesLogList(function (data) {
                           debugger;
                           var list = data.Data.VipServicesLogList;
                           list = list ? list : [];
                           debugger;
                           debugger;
                           if (list.length) {

                               that.elems.servicesLog.datagrid({
                                   data: list,
                                   singleSelect: true,
                                   fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                   columns: [
                                       [
                                           {field: 'ServicesTime', title: '服务时间', width: 160},
                                           {field: 'ServicesMode', title: '服务方式', width: 200, align: 'left'},
                                           {field: 'Content', title: '服务内容', width: 500, align: 'left',
                                               formatter: function (value, row, index) {
                                                   var long = 56;
                                                   if (value && value.length > long) {
                                                       return '<div class="rowText" title="' + value + '">' + value.substring(0, long) + '...</div>'
                                                   } else {
                                                       return '<div class="rowText">' + value + '</div>'
                                                   }
                                               }

                                           },
                                           {field: 'id', title: '编辑', width: 81, align: 'center', resizable: false,
                                               formatter: function (value, row, index) {
                                                   return '<p class="fontC exit" data-index="' + index + '" data-oprtype="exit"></p>';
                                               }
                                           },
                                           {field: '', title: '删除', width: 81, align: 'center', resizable: false,
                                               formatter: function (value, row, index) {
                                                   return '<p class="fontC delete" data-index="' + index + '" data-oprtype="del"></p>';
                                               }
                                           }
                                       ]
                                   ]
                               });

                               if (data.Data.TotalPages > 1) {
                                   that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                               }
                           } else {
                               that.showTableTips(that.elems.servicesLog, "该会员无操作记录!");

                               if (data.Data.TotalPageCount > 0) {
                                   that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                               }
                           }
                           panel.attr(loadKey, true);
                       });
                   })
               }
            });

            this.elems.tabs.delegate('li', 'click', function () {
                $('li', that.elems.tabs).removeClass('on');
                that.hidePanels();
                var panelId = $(this).attr('data-id');
                var panel = $('#' + panelId);
                $(this).addClass('on');
                panel.show();
               // debugger;
                var loadKey = 'loaded';
                var point = that.loadData.args.point;
                var amount = that.loadData.args.amount;
                var online = that.loadData.args.onlineOffline;
                var order = that.loadData.args.order;
                var consumerCard = that.loadData.args.consumerCard;
                var logs = that.loadData.args.logs;
                var ServicesLog = that.loadData.args.ServicesLog;
                if (panel.attr(loadKey)) {
                    switch (panelId) {
                        case 'nav02':
                            that.createPager('nav02', order.PageIndex,
                                order.TotalPages, order.TotalCount);
                            break;
                        case 'nav03':
                            that.createPager('nav03', point.PageIndex,
                                point.TotalPages, point.TotalCount);
                            break;
                        case 'nav04':
                            that.createPager('nav04', amount.PageIndex,
                                amount.TotalPages, amount.TotalCount);
                            break;
                        case 'nav06':
                            that.createPager('nav04', online.PageIndex,
                                online.TotalPages, online.TotalCount);
                            break;
                        case 'nav05':
                            that.createPager('nav05', consumerCard.PageIndex,
                                consumerCard.TotalPages, consumerCard.TotalCount);
                            break;
                        case 'nav07':
                            that.createPager('nav07', ServicesLog.PageIndex,
                                ServicesLog.TotalPages, ServicesLog.TotalCount);
                            break;
                        case 'nav08':
                            that.createPager('nav08', logs.PageIndex,
                                logs.TotalPages, logs.TotalCount);
                            break;
                    }
                    return;
                }
            //    debugger;
                switch (panelId) {
                    case 'nav02':  //交易记录
                        that.loadData.getVipOrderList(function (data) {
                            var list = data.Data.VipOrderList;
                            list = list ? list : [];
                            if (list.length) {
                                //用百度模板引擎渲染成html字符串
                                var html = bd.template("tpl_content", { list: list });
                                //将数据添加到页面的id=content的对象节点中
                                that.elems.content.html(html);
                                order.TotalCount = data.Data.TotalCount;
                                order.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 1) {
                                    that.createPager('nav02', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                //没有内容的提示
                                that.showTableTips(that.elems.content, "该会员暂无交易记录!");
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
                                if (data.Data.TotalPages > 1) {
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
                            debugger;

                            if (list.length) {
                                ServicesLog.TotalCount = data.Data.TotalCount;
                                ServicesLog.TotalPages = data.Data.TotalPageCount;
                                 that.elems.servicesLog.datagrid({
                                     data:list,
                                     singleSelect:true,
                                     fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                     columns:[[
                                         {field:'ServicesTime',title:'服务时间',width:160},
                                         {field:'ServicesMode',title:'服务方式',width:200,align:'left'},
                                         {field:'Content',title:'服务内容',width:500,align:'left',
                                             formatter:function(value ,row,index){
                                                 var long=56;
                                                 if(value&&value.length>long){
                                                     return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                                 }else{
                                                     return '<div class="rowText">'+value+'</div>'
                                                 }
                                             }

                                        },
                                         {field : 'id',title : '编辑',width:81,align:'center',resizable:false,
                                             formatter:function(value ,row,index){
                                                 return '<p class="fontC exit" data-index="'+index+'" data-oprtype="exit"></p>';
                                             }
                                         },
                                         {field : '',title : '删除',width:81,align:'center',resizable:false,
                                             formatter:function(value ,row,index){
                                                 return '<p class="fontC delete" data-index="'+index+'" data-oprtype="del"></p>';
                                             }
                                         }
                                     ]]
                                 });

                                if (data.Data.TotalPageCount > 0) {
                                    that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                                }
                            }else {
                                that.showTableTips(that.elems.servicesLog, "该会员无操作记录!");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;

                    case 'nav03':  //积分
                        that.loadData.getVipPointList(function (data) {
                            var list = data.Data.VipIntegralList;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tpl_point', { list: list });
                                that.elems.pointTable.html(html);
                                point.TotalCount = data.Data.TotalCount;
                                point.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 1) {
                                    that.createPager('nav03', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                that.showTableTips(that.elems.pointTable, "该会员暂无积分记录!");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav05':    //消费卡
                        that.loadData.getVipConsumeCardList(function (data) {
                            var list = data.Data.VipConsumeCardList;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tpl_consumer', { list: list });
                                that.elems.consumerTable.html(html);
                                consumerCard.TotalCount = data.Data.TotalCount;
                                consumerCard.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 1) {
                                    that.createPager('nav05', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                that.showTableTips(that.elems.consumerTable, "该会员消费卡暂无变更记录!");
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
                                if (data.Data.TotalPages > 1) {
                                    that.createPager('nav06', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                that.showTableTips(that.elems.onlineTable, "该会员上线与下线暂无变更记录!");
                            }
                            panel.attr(loadKey, true);
                        });
                        break;
                    case 'nav04':  //余额
                        that.loadData.getVipAmountList(function (data) {
                            var list = data.Data.VipAmountList;
                            list = list ? list : [];
                            if (list.length) {
                                var html = bd.template('tpl_amount', { list: list });
                                that.elems.amountTable.html(html);
                                amount.TotalCount = data.Data.TotalCount;
                                amount.TotalPages = data.Data.TotalPages;
                                if (data.Data.TotalPages > 1) {
                                    that.createPager('nav04', 1, data.Data.TotalPages, data.Data.TotalCount);
                                }
                            } else {
                                that.showTableTips(that.elems.amountTable, "该会员余额暂无变更记录!");
                            }
                            panel.attr(loadKey, true);
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
            });;
        },
        //加载vip详细信息
        loadVipDetail: function () {
            var that = this;
            //获得详情信息
            this.loadData.getVipDetail(function (data) {
                var str = "暂未填写";
                //设置会员基本信息
                var info = data.Data.VipDetailInfo;
                //会员编号
                $("#vipCode").html(info.VipNo ? info.VipNo : "未知");
                //会员名称
                $("#vipName").html(info.VipRealName ? info.VipRealName : str);
                //修改部分的
                //$("#editVipRealName").val(info.VipRealName ? info.VipRealName : "");
                //会员昵称
                $("#vipWeixin").html(info.VipName ? info.VipName : str);
                //$("#editVipName").val(info.VipName ? info.VipName : "");
                //会员等级
                $("#vipLevel").html(info.VipLevel);
                //会籍店
                $("#vipUnit").html(info.UnitName ? info.UnitName : "未知");
                //$("#editStore").val(info.UnitName ? info.UnitName : "");
                //会员积分
                $("#vipPoint").html(info.VipIntegral ? info.VipIntegral : 0);
                //会员余额
                $("#vipBalance").html(info.VipEndAmount + "元");
                //设置手机号
                //$("#editPhone").val(info.Phone ? info.Phone : "");
                //设置标签
                var list = data.Data.VipTags ? data.Data.VipTags : [];
                if (list.length == 0) {
                    list = [{
                        TagName: "暂无标签信息"
                    }];

                }
                $("#labels").html(bd.template("tpl_vipTag", { list: list }));
                if (list[0].TagName == "暂无标签信息") {
                    $("#labels span").css({ "color": "black" });
                }
                //动态会员数据
                that.loadData.getVipDyniform(function (result) {
                    var html = bd.template('tpl_EditVipForm', result);
                   
                    $('#nav01').find('.promptContent').html(html);
                    //显示datepicker
                    that.showDatepicker();
                    //让树显示
                    that.showZtree();
                });
            });
        },
        //展示ztree
        showZtree:function(){
            //获得所有的ztree集合
            $(".ztree").each(function(i,j){
                var $t=$(this);
                //所有的数据集合
                var forminfo=$t.data("forminfo"),id=$t.attr("id");
                var zNodes=[];
                for(var j=0,length=forminfo.length;j<length;j++){
                    var item=forminfo[j];
                    if(item.ParrentUnitID==-99){  //父节点
                        item.ParrentUnitID==0;
                    }
                    item.name=item.UnitName;
                    item.id=item.UnitID;
                    item.pId=item.ParrentUnitID;
                    zNodes.push(item);
                }
                var setting = {
                    isSimpleData : true,
                    view:{
                        treeNodeKey : "id",
                        treeNodeParentKey : "pId",
                        chkStyle: "radio",
                        enable: true,
                        radioType: "all",
                    },
                    callback:{
                        onClick:function(event, treeId, treeNode){
                            //点击的子节点
                            //if(!!!treeNode.children){
                                $t.parent().find("input").val(treeNode.name).attr("unitId",treeNode.UnitID);
                                $t.hide();
                            //}
                                
                        }
                    }
                };
                var zTreeObj = $.fn.zTree.init($("#"+id), setting, []) ;
                var treeNodes = zTreeObj.transformTozTreeNodes(zNodes);
                zTreeObj.addNodes(null, treeNodes);
            });
        },
        //加载页面的数据请求,dataId表示显示哪个tab下面的表格
        //加载更多的资讯或者活动
        loadMoreData: function (dataId, currentPage) {
            var that = this;
            //请求接口参数下标从1开始      分页的是从1开始
            switch (dataId) {
                //积分明细                        
                case 'nav03':
                    this.loadData.args.point.PageIndex = currentPage;
                    that.loadData.getVipPointList(function (data) {
                        var list = data.Data.VipIntegralList;
                        list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tpl_point', { list: list });
                            that.elems.pointTable.html(html);
                        }
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
                            ServicesLog.TotalCount = data.Data.TotalCount;
                            ServicesLog.TotalPages = data.Data.TotalPageCount;
                            that.elems.servicesLog.datagrid({
                                data:list,

                                singleSelect:true,
                                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                                columns:[[
                                    {field:'ServicesTime',title:'服务时间',width:160},
                                    {field:'ServicesMode',title:'服务方式',width:200,align:'left'},
                                    {field:'Content',title:'服务内容',width:500,align:'left',
                                        formatter:function(value ,row,index){
                                            var long=56;
                                            if(value&&value.length>long){
                                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                            }else{
                                                return '<div class="rowText">'+value+'</div>'
                                            }
                                        }

                                    },
                                    {field : 'id',title : '编辑',width:81,align:'center',resizable:false,
                                        formatter:function(value ,row,index){
                                            return '<p class="fontC exit" data-index="'+index+'" data-oprtype="exit"></p>';
                                        }
                                    },
                                    {field : '',title : '删除',width:81,align:'center',resizable:false,
                                        formatter:function(value ,row,index){
                                            return '<p class="fontC delete" data-index="'+index+'" data-oprtype="del"></p>';
                                        }
                                    }
                                ]]
                            });

                            if (data.Data.TotalPages > 1) {
                                that.createPager('nav07', 1, data.Data.TotalPageCount, data.Data.TotalCount);
                            }
                        }else {
                            that.showTableTips(that.elems.servicesLog, "该会员无操作记录!");
                        }
                        panel.attr(loadKey, true);
                    });
                    break;
                //消费卡                        
                case 'nav05':
                    this.loadData.args.consumerCard.PageIndex = currentPage;
                    that.loadData.getVipConsumeCardList(function (data) {
                        var list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tpl_consumer', { list: list });
                            that.elems.consumerTable.html(html);
                        }
                    });
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
                    that.loadData.getVipAmountList(function (data) {
                        var list = data.Data.VipAmountList;
                        list = list ? list : [];
                        if (list.length) {
                            var html = bd.template('tpl_amount', { list: list });
                            that.elems.amountTable.html(html);
                        }
                    });
                    break;
                //交易记录                        
                case "nav02":
                    this.loadData.args.order.PageIndex = currentPage;
                    that.loadData.getVipOrderList(function (data) {
                        var list = data.Data.VipOrderList;
                        list = list ? list : [];   //模板引擎没有判断传递的list是否为null  次数判断  
                        if (list.length) {
                            //用百度模板引擎渲染成html字符串
                            var html = bd.template("tpl_content", { list: list });
                            //将数据添加到页面的id=content的对象节点中
                            that.elems.content.html(html);
                        } else {
                            //没有内容的提示
                            that.showTableTips(that.elems.content, "该会员暂无消费记录!");
                        }
                    });
                    break;
                default:
                    break;
            }

        },
        //显示弹层
        showElements: function (selector) {
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements: function (selector) {

            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
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
        //设置查询条件 取得更新会员动态表单参数
        setEditVipInfoCondition: function () {
            var that = this;
            var vipinfo = [];
            var inputDom = $('[name=editvipinfo]');
            inputDom.each(function (i, dom) {
                var $dom = $(dom);
                var dataText = $dom.attr("data-text");
                /*if($dom.val()==""){
        		    alert(dataText+"不能为空!");
        		    return false;
        	    }else{*/  //提交的时候可以进行非空验证
                var json = $dom.data("forminfo");
                var obj = {};
                var value;
                if (json.DisplayType == 5) {//表示的是下拉框  需要特殊处理
                    if ($dom.html() == "请选择") {
                        value = "";
                    } else {
                        value = $dom.attr("optionid");
                    }
                    obj.ColumnValue1 = value;
                }else if(json.DisplayType==205){   //树结构
                    debugger;
                    obj.ColumnValue1=$dom.attr("unitId")?$dom.attr("unitId"):"";
                } else {
                    obj.ColumnValue1 = $dom.val();
                }
                obj.ColumnName = json.ColumnName;
                obj.ControlType = json.DisplayType;
                //搜索的条件
                vipinfo.push(obj);
                //}

            });
            //将查询条件赋值
            that.loadData.args.EditVipInfoColumns = vipinfo;
        },
        //显示日期
        showDatepicker: function () {
            //获取表单之后让datepicker初始化
            $('.datepicker').datetimepicker({
                lang: "ch",
                format: 'Y-m-d',
                timepicker: false  //不显示小时和分钟
                //step: 5 //分钟步长
            });
        },
        //新增客服记录
        addCustomer :function(data){
            var that=this;
            that.elems.optionType="cancel";
            $('#win').window({title:"新增客服记录",width:630,height:380,top:($(window).height()-380)*0.5,left:($(window).width()-630)*0.5});
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
        },
        loadData: {
            args: {
                VipId: "", //会员Id
                EditVipInfoColumns:[],//更新会员的动态属性信息
                //积分
                point: {
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
                //交易记录
                order: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType:'DESC'
                },
                //帐内余额
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
                }
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
            //更新会员信息
            updateVipInfo: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: 'UpdateVipInfo',
                        VipId: this.args.VipId,
                        Columns:this.args.EditVipInfoColumns
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
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
            //获得会员的消费记录
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
            //获取动态的注册表单
            getVipDyniform: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetExistVipInfo",
                        VipId:this.args.VipId
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
            //获取会员消费信息列表
            getVipConsumeCardList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipConsumeCardList',
                        VipId: this.args.VipId,
                        PageIndex: this.args.consumerCard.PageIndex,
                        PageSize: this.args.consumerCard.PageSize
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

             // 编辑和新增客服记录
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/Applicationinterface/Gateway.ashx";
                switch(operationType){
                    case "addCoupon":
                    case "exit":
                        prams.data["ServicesMode"]=$(".radio.on[data-name]").find("span").html().trim();
                        prams.data.action="VIP.ServicesLog.SetVipServicesLog";  //上架
                        $.each(pram, function (index, field) {
                            if(field.value!=="") {
                                prams.data[field.name] = field.value;
                            }
                        });
                        break;
                    case "del":prams.data.action="VIP.ServicesLog.DelVipServicesLog"; //删除
                        prams.data["ServicesLogID"]=pram.ServicesLogID;
                        break;
                }
                prams.data["VipID"]=this.args.VipId;
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
                        PageSize: this.args.point.PageSize,
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
            //获取会员积分列表
            getVipPointList: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Vip/VipGateway.ashx',
                    data: {
                        action: 'GetVipIntegralList',
                        //用vipid:4e98550ffb2749e49f4a1b53a5da10b1测试
                        VipId: this.args.VipId,
                        PageIndex: this.args.point.PageIndex,
                        PageSize: this.args.point.PageSize
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
            }
        }

    };
    page.init();
});