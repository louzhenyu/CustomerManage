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
            dataMessage:$(".dataMessage"),
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

                if($(this).data("flag")){
                    $("#win").window("open");
                    return false;
                }

                //查询数据
                that.loadData.getVipBirthdayCount(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);
            });
            that.loadData.get_unit_tree(function(datas) {

                that.unitTree=datas;
                $("#unitTree").combotree({
                    panelWidth:220,
                    //width:220,
                    //animate:true,
                    multiple:false,
                    valueField: 'id',
                    textField: 'text',
                    data:datas
                });
                $("#unitTree").combotree("setText","请选择门店");
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;
            /*case 0 : staus="未激活";break;
             case 1 : staus= "正常";break;
             case 2 : staus= "冻结";break;
             case 3 : staus= "失效";break;
             case 4 : staus= "挂失";break;
             case 5 : staus="休眠";break;*/
            $('#payment').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":-99,
                    "text":"全部"

                },{
                    "id":0,
                    "text":"未激活"
                },{
                    "id":1,
                    "text":"正常",
                    "selected":true
                },{
                    "id":2,
                    "text":"已冻结"
                },{
                    "id":3,
                    "text":"已失效"
                },{
                    "id":4,
                    "text":"已挂失"
                },{
                    "id":5,
                    "text":"已休眠"
                }]
            });
            $('#Gender').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":"-1",
                    "text":"全部",
                    "selected":true
                },{
                    "id":1,
                    "text":"男"
                },{
                    "id":2,
                    "text":"女",

                }]
            });
            //最近消费
            $('#Consumption').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":"0",
                    "text":"全部"
                },{
                    "id":3,
                    "text":"近3个月以内",
                    "selected":true
                },{
                    "id":6,
                    "text":"近6个月以内",

                },{
                    "id":12,
                    "text":"近12个月以内",

                },{
                    "id":24,
                    "text":"近24个月以内"

                }]
            });
            $("#Month").combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":"01",
                    "text":"1月"
                },{
                    "id":"02",
                    "text":"2月"
                },{
                    "id":"03",
                    "text":"3月"
                },{
                    "id":"04",
                    "text":"4月"
                },{
                    "id":"05",
                    "text":"5月"
                },{
                    "id":"06",
                    "text":"6月"
                },{
                    "id":"07",
                    "text":"7月"
                },{
                    "id":"08",
                    "text":"8月"
                },{
                    "id":"09",
                    "text":"9月"
                },{
                    "id":"10",
                    "text":"10月"
                },{
                    "id":"11",
                    "text":"11月"
                },{
                    "id":"12",
                    "text":"12月"
                }]
            });
            var str=new Date().getMonth()+1;
            if(str<10){str="0"+str}
            $("#Month").combobox("setValue",str);
            that.loadData.getSysVipCardTypeList(function(data){
                $('#VipCardTypeCodeID').combobox({
                    width:236,
                    height:H,
                    panelHeight:that.elems.panlH,
                    valueField: 'VipCardTypeID',
                    textField: 'VipCardTypeName',
                    data:data.Data.SysVipCardTypeList
                });
            });

            $('.datebox').datebox({
                width:110,
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
                closable:true,
                onOpen: function(){
                    $("#VipCardISN").focus();
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){

                if ($('#payOrder').form('validate')) {

                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,"",function(data){


                        if(data.Data.VipCardID) {
                            var mid = JITMethod.getUrlParam("mid");
                            location.href = "vipCardDetail.aspx?VipCardID=" + data.Data.VipCardID + "&mid=" + mid;

                        }else{
                            $.messager.alert("没有该卡的任何信息。");
                        }
                    });

                }
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
            var that=this;
            var str='<div class="loading"><span><img src="../static/images/loading.gif"></span></div>';
            if(this.elems.dataMessage.is(":hidden")){
                $(".datagrid-body").html(str);
            } else{
                that.elems.dataMessage.html(str);
            }
            debugger;

            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){

                    if(filed.value==-1){
                        filed.value='';
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
            if(!data.Data.VipBirthdayInfoList){

                data.Data.VipBirthdayInfoList=[];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.VipBirthdayInfoList,
                /* sortName : 'brandCode', //排序的列
                 *//*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*//*
                 idField : 'OrderID', //主键字段*/
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {field : 'VipCardCode',title : '卡号',width:96,align:'center',resizable:false},
                    {field : 'VipCardTypeName',title : '卡类型',width:56,resizable:false,align:'left'},
                    {field : 'VipCardStatusId',title : '卡状态',width:56,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            //会员卡状态标识(0未激活，1正常，2冻结，3失效，4挂失，5休眠)
                            var staus;
                            switch (value){
                                case 0 : staus="未激活";break;
                                case 1 : staus= "正常";break;
                                case 2 : staus= "已冻结";break;
                                case 3 : staus= "已失效";break;
                                case 4 : staus= "已挂失";break;
                                case 5 : staus="已休眠";break;
                            }
                            return staus;
                        }
                    },
                    {field : 'VipName',title : '会员姓名',width:96,align:'left',resizable:false},
                    {field : 'Phone',title : '手机号',width:66,align:'left',resizable:false},
                    {field : 'Gender',title : '性别',width:46,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            //会员卡状态标识(0未激活，1正常，2冻结，3失效，4挂失，5休眠)
                           var  staus;
                            switch (value){
                                case "1" : staus="男";break;
                                case "2" : staus= "女";break;

                            }
                            return staus;
                        }
                    },
                    {field: 'Birthday', title: '生日', width: 83, align: 'left', resizable: false
                        /* formatter:function(value ,row,index){
                         return new Date(value).format("yyyy-MM-dd")
                         }*/
                    },
                    {field : 'SpendingDateShow',title : '最近消费',width:83,align:'left',resizable:false},
                    {field : 'MembershipUnitName',title : '办卡门店',width:136,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=26;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }

                    } ,
                    {field : 'MembershipTime',title : '办卡日期',width:83,align:'left',resizable:false }
                ]],

                onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0){
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.html("没有符合条件的记录");
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                 /*   debugger;
                    if(that.elems.click){
                        that.elems.click = true;
                        debugger;
                        var index=rowindex, nodeData=rowData;
                        /!*if(rowData.VipCardStatusID==0){
                         $.messager.alert("操作提示","此卡未激活,请激活此卡。");
                         return false;
                         }*!/


                        var mid = JITMethod.getUrlParam("mid");
                        location.href = "vipCardDetail.aspx?VipCardID=" + rowData.VipCardID+"&mid=" + mid;
                    }*/

                },onClickCell:function(rowIndex, field, value){
                    if(field=="OrderID"){  //有复选框，或者操作列的时候用到
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }

            });


             if(data.Data.VipBirthdayInfoList.length>0) {
                 //分页
                 kkpager.generPageHtml({
                     pno: that.loadData.args.PageIndex,
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


                 if ((that.loadData.opertionField.displayIndex || that.loadData.opertionField.displayIndex == 0)) {  //点击的行索引的  如果不存在表示不是显示详情的修改。
                     that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click", true);
                     that.loadData.opertionField.displayIndex = null;
                 }
             }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getVipBirthdayCount(function(data){
                that.renderTable(data);
            });
        },



        loadData: {
            args: {
                PageIndex: 1,
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
            getUitTree:{
                node:""
            },
            opertionField:{},
            getVipBirthdayCount: function (callback) {
                var prams={data:{action:""}};
                prams.data={
                    action: "Report.VipReport.GetVipBirthdayCount",
                    PageSize:this.args.PageSize,
                    PageIndex:this.args.PageIndex
                    /*     OrderBy:this.args.Phone,
                     UnitID:"005c99f55c26916fbd305bd69e8948fd",//window.RoleCode=="Admin"?null:window.UnitID,//
                     Status:this.args.Status*/

                };
                $.each(this.args.SearchColumns, function(i, field) {

                    if (field.value) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    pathType:"public",
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
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            },
            getSysVipCardTypeList: function (callback) {


                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action: "VIP.SysVipCardType.GetSysVipCardTypeList"
                    },
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

            get_unit_tree1: function (callback) {
                $.ajax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx?method=get_unit_tree&parent_id=&_dc=1433225205961&node="+this.getUitTree.node+"&multiSelect=false&isSelectLeafOnly=false&isAddPleaseSelectItem=false&pleaseSelectText=--%E8%AF%B7%E9%80%89%E6%8B%A9--&pleaseSelectID=-2",
                    success: function (data) {
                        debugger;
                        var datas=JSON.parse(data);
                        if (datas&&datas.length>0) {
                            if (callback) {
                                callback(datas);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
            //获取门店
            get_unit_tree: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx",
                    data:{

                        action: "get_unit_tree",
                        QueryStringData:{
                            _dc:'1433225205961',
                            node:this.getUitTree.node,
                            parent_id:'',
                            multiSelect:false,
                            isSelectLeafOnly:false,
                            isAddPleaseSelectItem:false,
                            pleaseSelectText:'--%E8%AF%B7%E9%80%89%E6%8B%A9--',
                            pleaseSelectID: -2
                        }

                    } ,

                    success: function (data) {

                        if (data&&data.length>0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                            console.log("加载数据不成功");
                        }
                    }
                });
            },


            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"VIP.VIPCard.GetVipCardDetail"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                /*    switch(operationType){
                 case "pay":prams.data.action="SetOrderPayment";  //收款
                 break;
                 case "cancel":prams.data.action="SetOrderCancel";  //收款
                 break;
                 }*/
                $.each(pram,function(index,filed){
                    if(filed.name=="VipCardISN"){
                        filed.value=filed.value.replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"")
                    }
                    prams.data[filed.name]=filed.value;
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
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

