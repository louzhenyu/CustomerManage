/**
 * Created by Administrator on 2015/3/2.
 */
define(['jquery', 'template', 'tools', 'kkpager','langzh_CN','easyui'], function ($) {
    var page;
    Date.prototype.format =function(format)
    {
        var o = {
            "M+" : this.getMonth()+1, //month
            "d+" : this.getDate(), //day
            "h+" : this.getHours(), //hour
            "m+" : this.getMinutes(), //minute
            "s+" : this.getSeconds(), //second
            "q+" : Math.floor((this.getMonth()+3)/3), //quarter
            "S" : this.getMilliseconds() //millisecond
        }
        if(/(y+)/.test(format)) format=format.replace(RegExp.$1,
            (this.getFullYear()+"").substr(4- RegExp.$1.length));
        for(var k in o)if(new RegExp("("+ k +")").test(format))
            format = format.replace(RegExp.$1,
                    RegExp.$1.length==1? o[k] :
                    ("00"+ o[k]).substr((""+ o[k]).length));
        return format;
    };
    page = {
        createPager: function (dataId, pageIndex, totalPage, totalCount) {
            if (totalPage <= 1) return;
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
            var tbl = $('#' + dataId).find('table');
            tbl.after(that.elems.pager);
        },
        elems: {
            sectionPage: $("#section"),
            uiMask: $("#ui-mask"),
            tabs: $('#section .subMenu ul'),
            content: $("#content"),     //交易记录表格数据
            pointTable: $('#tblPoint'),   //积分明细
            amountTable: $('#tblAmount'),
            onlineTable: $('#tblOnline'),
            logsTable: $('#tblLogs'),
            consumerTable: $('#tblConsumer'),
            pager: $('#kkpager')
        },
        init: function () {
            //获得地址栏参数为vipId的值
            var OrderId = $.util.getUrlParam("OrderID");
            var vipId = $.util.getUrlParam("vipId");
            var UnitID = $.util.getUrlParam("UnitID");
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.OrderId = OrderId;
            this.loadData.args.VipID = vipId;
            this.loadData.args.UnitID = UnitID;
            //请求数据
            //this.loadPageData();
            //请求会员详细信息   注:暂无接口以及文档
            debugger;

            this.loadTestReport();
            this.loadOrderDetail();
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

                    that.loadData.operation(fields,"pay",function(data){

                        alert("收款成功");
                        $('#win').window('close');
                        that.loadData.GetOrderDetail(function (data) {
                            var  oderinfo=data.Data;
                            var  operateLog=data.Data.OrderHistoryList;

                            $.each(oderinfo, function(name, value) {

                                $("[data-panl='orderinfo']").find("p").each(function(){
                                    // data-prefix="￥"
                                    var optValue=value;
                                    if($(this).data("flag")==name){
                                        if($(this).data("prefix")){
                                            optValue=$(this).data("prefix")+value

                                        }
                                        if($(this).data("unit")){
                                            optValue=value+$(this).data("unit")

                                        }
                                        if(optValue){
                                            $(this).html(optValue);
                                        }else{
                                            $(this).html("未知");
                                        }

                                    }
                                });
                                if(name=='IsPaid'){
                                    if(value==1){
                                        $("#paymentBtn").find("a").html("已付款").removeClass("commonBtn").removeClass("paybtn").addClass("fontC");
                                    }else{
                                        $("#paymentBtn").find("a").html("收款").removeClass("fontC").addClass("commonBtn").addClass("paybtn");
                                    }
                                }



                            });
                            that.renderTable(data.Data.OrderHistoryList);
                        });

                    });
                }
            });
            $(".listTag").delegate("span", "click", function () {
                var check = $(this).parent().data("check");   //是否单选如果check=true 是单选如果是flase或者不存在就是多选
                this.selected = false;
                if (check) {
                    $(this).siblings().removeClass("on");
                    $(this).addClass("on");
                } else {
                    $(this).toggleClass("on");
                }


            });
            $("#CarNoProvince").click(function () {
                $('#win').window('open');
            });
            $("#listSpan").delegate("span", 'click', function () {
                $('#win').window('close');
                $("#CarNoProvince").val($(this).data("falg"));
            });
            $('#section table').delegate('td.checkBox', 'click', function (e) {
                $(this).toggleClass('on');
                that.stopBubble(e);
            });
            //打印
            $("#printBtn").click(function(){
                 $(".Noprint,.commonHeader,.subMenu, .subMenu,.commonMenu").hide(0);
                $("#printWord").css({"position":"fixed","top":"35px","left":"35px"});
                window.print();
                $(".Noprint,.commonHeader,.subMenu, .subMenu,.commonMenu").show(0);
                $("#printWord").css({"position":"relative","top":"0","left":"0"});

            });


            //更新数据
            $('#nav01').delegate('.saveBtn', 'click', function (e) {
                that.setEditVipInfoCondition();
                that.loadData.updateVipInfo(function (data) {
                    alert('会员信息更新成功', true);
                    //刷新数据
                    that.loadVipDetail();
                });
                that.stopBubble(e);
            });
             //收款
            $("#paymentBtn").delegate('.paybtn', 'click', function (e){
                $("#win").window("open") ;

            });

            that.elems.tabs.delegate('li', 'click', function () {
                debugger;
                $('li', that.elems.tabs).removeClass('on');
                that.hidePanels();
                var panelId = $(this).attr('data-id');
                var panel = $('#' + panelId);
                $(this).addClass('on');
                panel.show();
            });



        },

        //加载车况报告
        loadTestReport:function(){
            this.loadData.GetTestReport(function(data){

                if(data.Data&&data.Data.TestReport&&data.Data.TestReport[0]){
                  var  testReportinfo=data.Data.TestReport[0];
                      $.each(testReportinfo, function(name, value) {

                        $("[data-panl='carinfo']").find(".itemText").each(function(){
                            // data-prefix="￥"
                            var optValue=value;
                            if($(this).data("flag")==name){
                                $(this).html(value?value:"未填写");
                            }
                            if(name=="DetectTime"){
                                value=new  Date(value).format("yyyy-MM-dd hh:mm");

                            }
                            if(name=="IsUsedCar"){
                                value=value==0?" 否":"是"

                            }
                        });

                    });


                }


            })
        },

        //加载订单详细信息
        loadOrderDetail: function () {
            var that = this;
            that.loadData.GetOrderStatement(function (data) {
                debugger;
                var list = data.Data;
                if(data.Data.StatementDate) {
                    data.Data.StatementDate = new Date(data.Data.StatementDate).format("yyyy-MM-dd hh:mm");
                }
                data.Data.NewDate=new Date().format("yyyy-MM-dd hh:mm") ;
                $("#word").form("load",data.Data);
                 $("#UnitName").html((data.Data.UnitName?data.Data.UnitName:"")+"结算清单");

            });

            //获得详情信息
            that.loadData.GetOrderDetail(function (data) {
                var  oderinfo=data.Data;
                var  operateLog=data.Data.OrderHistoryList;
                that.payMent(data.Data);
                $.each(oderinfo, function(name, value) {

                    $("[data-panl='orderinfo']").find("p").each(function(){
                       // data-prefix="￥"
                        var optValue=value;
                       if($(this).data("flag")==name){
                           if($(this).data("prefix")){
                               optValue=$(this).data("prefix")+value

                           }
                           if($(this).data("unit")){
                               optValue=value+$(this).data("unit")

                           }
                           if(optValue){
                               $(this).html(optValue);
                           }else{
                               $(this).html("未知");
                           }
                              if(name=='ServiceMode'){
                               if(value==0){
                                  $(this).html("到店");
                                }else{
                                  $(this).html("上门");
                                }
                            }

                           if(name=='CreateTime'){
                               $(this).html(new Date(value).format("yyyy-MM-dd hh:mm"))
                           }
                           /*if(name=="ServiceTime"){
                               debugger;
                               if(value.split("-")[0].length>0&&value.split("-")[1].length>0){
                                   var satrTime= new Date(value.split("-")[0]).format("yyyy-MM-dd hh:mm");
                                   var endTime=new Date(value.split("-")[1]).format("yyyy-MM-dd hh:mm");
                                   $(this).html("从"+satrTime+"至"+endTime);
                               }else{
                                   $(this).html("未知");
                               }


                           }*/
                       }
                    });
                    if(name=='IsPaid'){
                         if(value==1){
                             $("#paymentBtn").find("a").html("已付款").removeClass("commonBtn").removeClass("paybtn").addClass("fontC");
                         }else{
                             $("#paymentBtn").find("a").html("收款").removeClass("fontC").addClass("commonBtn").addClass("paybtn");
                         }
                    }
                    if(name=='Status'){
                        //订单状态
                        debugger
                        var  staus="未知";
                        switch (value){
                            case 0 : staus="待接单";break;
                            case 1 : staus= "待取车";break;
                            case 2 : staus= "取车失败";break;
                            case 3 : staus= "待派工";break;
                            case 4 : staus= "已派工";break;
                            case 5 : staus="已开工";break;
                            case 6 : staus= "已完工";break;
                            case 8 : staus= "未派工"; break;
                            case 9 : staus= "服务中"; break;
                            case 10 : staus= "已取消"; break;
                            case 11 : staus= "交易关闭"; break;
                        }
                        $("#paymentyBtn").find("a").html(staus);
                    }
                });
                that.renderTable(data.Data.OrderHistoryList);
            });
            that.loadData.GetEvaluationList(function(data){
                debugger
               var  oderinfo={};
                if(data.Data.OrderList&&data.Data.OrderList.length>0){

                    oderinfo= data.Data.OrderList[0];
                }
                $.each(oderinfo, function(name, value) {

                    $("[data-panl='vipinfo']").find("p").each(function(){
                        // data-prefix="￥"
                        var optValue=value;
                        if($(this).data("flag")==name){
                            if($(this).data("prefix")){
                                optValue=$(this).data("prefix")+value

                            }
                            if($(this).data("unit")){
                                optValue=value+$(this).data("unit")

                            }
                            if(optValue){
                                $(this).html(optValue);
                            }else{
                                $(this).html("未知");
                            }

                        }
                    });

                });
            })

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
                //结算单的数据绑定
                case "nav02":


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
        payMent:function(data){
            debugger;

            var that=this;
            that.ValueCard=data.ValueCard;
            $('#win').window({title:"收款",width:500,height:500});

            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderPayMent');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            that.loadData.GetCouponList(function(data){
                if(data.Data&&data.Data.CouponList){


                    $("#coupon").combogrid({
                        data:data.Data.CouponList,
                        valueField:'CouponID',
                        textField:'CouponDesc',
                        panelWidth:260,
                        columns:[[
                            {field:'CouponCode',title:'优惠券编码',width:100},
                            {field:'CouponDesc',title:'优惠券名称',width:100},
                            {field:'ParValue',title:'金额',width:50},
                        ]], onSelect:function(index,record){
                            if($("#Amount").val()){
                                var ActualAmount=$("#ActualAmount").val(); ///实付金额
                                var Deduction=$("#Deduction").numberbox("getValue"); //纸质优惠券抵扣金额
                                var amount=$("#Amount").val();//订单金额
                                ActualAmount= amount- record.ParValue-Deduction;
                                $("#ActualAmount").val(ActualAmount>0 ?ActualAmount:0);
                            }

                        },
                        onLoadSuccess:function(){
                            $('#payOrder').form("load",data);
                        }
                    });
                }
            });
            that.loadData.GetPaymentListBycId(function(data){
                if(data.content&&data.content.paymentList){

                    debugger;
                    $("#pay").combobox({
                        data:data.content.paymentList,
                        panelHeight:100,
                        valueField:'paymentTypeId',
                        textField:'paymentTypeName',
                        onLoadSuccess:function(){
                            $('#payOrder').form("load",data);
                        },
                        onSelect:function(record){

                            if(record.paymentTypeId!="5FE8C37226524A82851402056DA2BEE5") {
                                $("#ValueCard").hide(function(){
                                    that.ValueCard= $(this).find("input").val()
                                    $(this).find("input").val("");
                                });

                            }else{
                                $("#ValueCard").show(
                                    function(){
                                        $(this).find("input").val(that.ValueCard);
                                    });
                            }
                        }

                    });
                }
            })
            $('#payOrder').form("load",data);

            $("#Deduction").numberbox({

                onChange:function(newValue,oldValue){
                    debugger;
                    var ActualAmount=$("#ActualAmount").val(); ///实付金额
                    // newValue纸质优惠券抵扣金额
                    var gridData=$("#coupon").combogrid("grid").datagrid('getSelected');
                    var amount=$("#Amount").val();//订单金额
                    ActualAmount= amount-(gridData?gridData.ParValue:0)-newValue;
                    $("#ActualAmount").val(ActualAmount>0 ?ActualAmount:0);
                }
            });
            $("#Amount").numberbox({ prefix:"￥"});

        },

        renderTable: function (data) {
            debugger;
            var that = this;

            //jQuery easy datagrid  表格处理
            $("#gridTable").datagrid({

                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //多选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data,
                sortName: 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'userid', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns: [
                    [

                        {field: 'HisDesc', title: '操作', width: 30, align: 'center', resizable: false},
                        {field: 'OperatorName', title: '操作人', width: 30, resizable: false, align: 'center'},
                        {field: 'CreateTime', title: '操作时间', width: 30, align: 'center', resizable: false},
                        {field: 'HisRemark', title: '备注', width: 30, align: 'center', resizable: false},


                    ]
                ],
                /* columns : [[
                 {field : 'productid',title : '订单号',width:1,align:'center',resizable:false},
                 {field : 'productname',title : '车牌号码',width:1,align:'center',resizable:false},
                 {field : '',title : '服务方式',width:1,resizable:false,align:'center',
                 formatter:function(value,row,index){
                 if(index%3>1){
                 return "到店"
                 }else{
                 return "上门"
                 }
                 }},
                 {field : 'status',title : '项目名称',width:1,align:'center',resizable:false},
                 {field : 'attr1',title : '手机号码',width:1,align:'center',resizable:false},
                 {field : 'unitcost',title : '付款状态',width:1,align:'center',resizable:false,
                 formatter:function(value){
                 if(value==12.00){
                 return "已付款"
                 }else{
                 return "未付款"
                 }
                 }
                 },
                 {field : 'listprice',title : '金额',width:1,align:'center',resizable:false},
                 {field : 'listprice',title : '服务门店',width:1,align:'center',resizable:false} ,
                 {field : 'listprice',title : '下单时间',width:1,align:'center',resizable:false},
                 {field : 'listprice',title : '订单状态',width:1,align:'center',resizable:false},
                 {field : 'productname',title : '服务完成时间',width:2,align:'center',
                 formatter:function(value){
                 return '2015-03-05 12:53:12'+row;
                 }
                 }

                 ]],*/
                onLoadSuccess: function () {
                    $("#gridTable").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                }
            });

            data.Data = [];
            data.Data.TotalPages = 1
            data.Data.TotalCount = 10;


            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex ? that.loadData.args.PageIndex : 1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPages,
                totalRecords: data.Data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //让  tbody的内容变成加载中的图标
                    //var table = $('table.dataTable');//that.tableMap[that.status];
                    //var length = table.find("thead th").length;
                    //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                    that.loadMoreData(n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);

            if (data.Data.TotalPageCount > 1) {
                kkpager.generPageHtml(
                    {
                        pno: data.Data.PageIndex,
                        mode: 'click', //设置为click模式
                        //总页码
                        total: data.Data.TotalPageCount,
                        totalRecords: data.Data.TotalCount,
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
                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);

            }

        },

        loadData: {
            args: {
                OrderId: "", //订单ID
                UnitID:'',//门店id
                VipID:'',//会员id
                EditVipInfoColumns: [],//更新会员的动态属性信息
                //积分
                point: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
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
                    OrderType: 'DESC'
                },
                //帐内余额
                amount: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
                },
                //消费卡
                consumerCard: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
                },
                //上线与下线
                onlineOffline: {
                    PageIndex: 1,
                    PageSize: 10,
                    TotalPages: 0,
                    TotalCount: 0,
                    OrderType: 'DESC'
                }
            },
            //获取所有省份
            GetVipAddressList: function (callback) {
                $.util.ajax({
                    url: "/OnlineShopping/data/Data.aspx",
                    pathType: "public",
                    data: {
                        action: "getProvince"
                    },
                    success: function (data) {
                        if (data.code == "200") {
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
            GetCityByProvince: function (callback, province) {
                $.util.ajax({
                    url: "/OnlineShopping/data/Data.aspx",
                    pathType: "public",
                    data: {
                        action: "getCityByProvince",
                        special: {
                            Province: province //前后端公共接口其它参数必须写入 special中；
                        }

                    },
                    success: function (data) {
                        if (data.code == "200") {
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
            GetDistrictsByDistricID: function (callback, districtId) {
                $.util.ajax({
                    url: "/OnlineShopping/data/Data.aspx",
                    pathType: "public",
                    data: {
                        action: "getDistrictsByDistricID",
                        special: {
                            districtId: districtId //前后端公共接口其它参数必须写入 special中；
                        }

                    },
                    success: function (data) {
                        if (data.code == "200") {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

            GetPaymentListBycId: function (callback) {
                $.util.ajax({
                    url: "/Interface/data/OrderData.aspx",
                    pathType:"public",
                    channelID:"2",
                    data:{
                        action:"GetPaymentListBycId"
                    },
                    success: function (data) {
                        if (data.code==200) {
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
                    url: "/ApplicationInterface/Services/ServiceGateway.ashx",
                    pathType:"public",
                    userId: this.args.VipId,
                    data: {
                        action: "GetCouponList",

                        PageSize:1000,
                        PageIndex:this.args.PageIndex,
                        Status:0  //指定优惠卷状态（0：未用，1：已使用，2：已过期，3：全部）
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
            //获取评价
            GetEvaluationList:function (callback) {

                $.util.ajax({
                    url: "/ApplicationInterface/car/Gateway.ashx",
                    pathType: "public",
                    data: {
                        action: "GetEvaluationList",
                        ObjectID:this.args.UnitID,//"8272d9bdd84c4565f7d55309e20970ec",//
                        StarLevel:3,
                        VipID:this.args.VipID //"2c826e1ea93b47b79c0920f90d1ce914"//
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
            } ,


            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Car/OrderGateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                prams.data.OrderID=this.args.OrderId;
                switch(operationType){
                    case "pay":prams.data.action="SetOrderPayment";  //收款
                        break;

                }
                var gridData=$("#coupon").combogrid("grid").datagrid('getSelected');
                $.each(pram, function(i, field) {
                    debugger;
                    if (field.value != "") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                    if(field.name=="CouponID"&&gridData){
                        prams.data[field.name] =gridData.CouponID;
                    }
                    if(field.name=="Deduction"){
                        if(isNaN(parseInt( field.value)+(gridData?gridData.ParValue:0))) {
                            prams.data["DeductionAmount"] = 0;
                        }
                        else{
                            prams.data["DeductionAmount"] = parseInt( field.value)+(gridData?gridData.ParValue:0);
                        }
                    }
                });
                $.util.ajax({
                    url: prams.url,
                    pathType:"public",
                    userId:window.adminUserID,
                    channelID:"7",
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
            //GetOrderStatement 获取结算单

            GetOrderStatement: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/car/OrderGateway.ashx",
                    pathType: "public",
                    data: {
                        action: "GetOrderStatement",
                        OrderID:this.args.OrderId
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
            //获取订单详情
            GetOrderDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/car/OrderGateway.ashx",
                    pathType: "public",
                    data: {
                        action: "GetOrderDetail",
                        OrderId:this.args.OrderId
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
            //获取车款报告
            GetTestReport: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Car/Gateway.ashx",
                    pathType:"public",
                    data: {
                        action: "GetTestReport",
                        VipID:this.args.VipID,
                        OrderID:this.args.OrderId
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