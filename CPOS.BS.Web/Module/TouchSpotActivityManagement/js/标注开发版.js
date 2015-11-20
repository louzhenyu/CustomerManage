define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager'], function ($) {
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
            operation:$('#operation'),              //弹出框操作部分
            vipSourceId:'',
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
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getCarOderList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);
            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;
            $('#payment').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":0,
                    "text":"全部"
                },{
                    "id":2,
                    "text":"已付款"
                },{
                    "id":1,
                    "text":"未付款"
                }]
            });
            $('#proname').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":1,
                    "text":"洗车"
                },{
                    "id":2,
                    "text":"保养"
                },{
                    "id":3,
                    "text":"美妆"
                }]
            });
            $('#orderSource').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":1,
                    "text":"洗车"
                },{
                    "id":2,
                    "text":"保养"
                },{
                    "id":3,
                    "text":"美妆"
                }]
            });
            $('.datebox').datebox({
                width:wd,
                height:H
            });
            /**************** -------------------弹出easyui 控件  End****************/


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

                    that.loadData.operation(fields,that.elems.optionType,function(data){

                        alert("操作成功");
                        $('#win').window('close');
                       that.loadPageData(e);

                    });
                }
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".fontC","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                  if(optType=="payment") {
                      if(row.IsPaid!=1&&row.Status!=10&&row.Status!=11) {
                          that.payMent(row);
                      }
                  }
                  if(optType=="cancel"){
                    that.cancelOrder(row);
                  }
            })
            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
             //查询每次都是从第一页开始
            that.loadData.args.PageIndex=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                   if(filed.name=="IsPaid"){
                       filed.value=filed.value-1+'';
                       if(filed.value==-1){
                           filed.value='';
                       }

                   }
            });

            that.loadData.args.SearchColumns= fileds;



        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e)
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.OrderList){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.OrderList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'OrderID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                frozenColumns:[[
                    {
                        field : 'ck',
                        width:22,
                        checkbox : true
                    },//显示复选框
                    {field : 'OrderNo',title : '订单号',width:96,align:'center',resizable:false},
                    {field : 'OrderID',title : '操作',width:96,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var html="";
                                if(row.IsPaid==0&&row.Status!=10&&row.Status!=11) {
                                 html=   '<p class="fontC" data-oprType="payment" data-index="'+index+'"> 收款</p>';
                                }
                            if($.util.GetDateDiff(new Date().format("yyyy-mm-dd"),row.ReserveTime,"day")>=1&&row.Status!=6&&row.Status!=10&&row.Status!=11){
                                html+='<p class="fontC" data-oprType="cancel" data-index="'+index+'"> 取消</p>';
                            }
                            return html;
                        }

                    }
                ]],
                columns : [[

                    {field : 'CarNumber',title : '车牌号码',width:66,align:'center',resizable:false},
                    {field : 'ServiceMode',title : '服务方式',width:66,resizable:false,align:'center'
                       , formatter:function(value,row,index){
                            if(value==0){
                               return "到店"
                            }else{
                                return "上门"
                            }
                        }},
                    {field : 'ServiceItemsName',title : '项目名称',width:106,align:'center',resizable:false},
                    {field : 'Phone',title : '手机号码',width:82,align:'center',resizable:false},
                    {field : 'IsPaid',title : '付款状态',width:60,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            if(value==1){
                                return "已付款"
                            }else{
                                return '<p> 未付款</p>';
                            }
                        }
                    },
                    {field : 'Amount',title : '金额',width:30,align:'center',resizable:false},
                    {field : 'UnitName',title : '服务门店',width:200,align:'center',resizable:false} ,
                    {field : 'CreateTime',title : '下单时间',width:83,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           return value.substring(0,value.length-3);
                        }
                    },
                    {field : 'Status',title : '订单状态',width:96,align:'center',resizable:false,
                        formatter:function(value ,row,index){
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
                            return staus;
                        }
                    },


                ]],

                onLoadSuccess : function() {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                },
                onClickRow:function(rowindex,rowData){
                  /*debugger;
                    if(that.elems.click){
                        that.elems.click = true;
                        debugger;
                        var index=rowindex, nodeData=rowData;
                        var mid = JITMethod.getUrlParam("mid");
                        location.href = "CarOrderDetail.aspx?OrderID=" + rowData.OrderID + "&vipId=" + rowData.VipID+"&UnitID="+rowData.UnitID + "&mid=" + mid;
                    }*/

                },onClickCell:function(rowIndex, field, value){
                  /*  if(field=="OrderID"){
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }*/
                }

            });



            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex+1:1,
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


            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage-1;
            that.loadData.getCarOderList(function(data){
                that.renderTable(data);
            });
        },
        //收款
        payMent:function(data){
            debugger;
            var that=this;
            that.elems.optionType="pay";
            $('#win').window({title:"收款",width:500,height:500});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderPayMent');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);

            that.loadData.tag.VipId=data.VipID;
            that.loadData.tag.orderID=data.OrderID;
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
                            $('#pay').combobox('setValue',data.content.paymentList[0].paymentTypeId);
                            var paymentTypeId=  data.content.paymentList[0].paymentTypeId
                            if(paymentTypeId!="5FE8C37226524A82851402056DA2BEE5") {
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
            $('#win').window('open')
        },

        //取消订单
        cancelOrder:function(data){
            var that=this;
            that.elems.optionType="cancel";
            $('#win').window({title:"取消订单",width:360,height:260});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            this.loadData.tag.orderID=data.OrderID;
            $('#win').window('open');
        },
       loadData: {
            args: {
                PageIndex: 0,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1
            },
            tag:{
                VipId:"",
                orderID:''
            },
           opertionField:{},
            getCarOderList: function (callback) {
                var prams={data:{action:""}};
                prams.data={
                    action: "GetOrderList",
                    PageSize:this.args.PageSize,
                    PageIndex:this.args.PageIndex,
                    OrderBy:this.args.OrderBy,
                    UnitID:"005c99f55c26916fbd305bd69e8948fd",//window.RoleCode=="Admin"?null:window.UnitID,//
                    Status:this.args.Status

                };
                $.each(this.args.SearchColumns, function(i, field) {

                    if (field.value) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });

                $.util.ajax({
                    url: "http://api.dev.linkcars.net/ApplicationInterface/Car/OrderGateway.ashx",
                    pathType:"public",
                    data: prams.data,
                    channelID:'7',
                    customerId:"464153d4be5944b19a13e325ed98f1f4",
                    userId:"550E2D12613D4580989B65AF984F578D",
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
               var prams={data:{action:""}};
               prams.url="";
               //根据不同的操作 设置不懂请求路径和 方法
               prams.data.OrderID=this.tag.orderID;
               switch(operationType){
                   case "pay":prams.data.action="SetOrderPayment";  //收款
                       break;
                   case "cancel":prams.data.action="SetOrderCancel";  //收款
                       break;
               }
               var gridData=$("#coupon").length>0?$("#coupon").combogrid("grid").datagrid('getSelected'):null;
               $.each(pram, function(i, field) {
                   debugger;
                   if (field.value != "") {
                       prams.data[field.name] = field.value; //提交的参数
                   }
                   if(field.name=="CouponID"&&gridData){
                       prams.data[field.name] =gridData.CouponID;
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
           },


        }

    };
    page.init();
});

