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
                that.loadData.getOrderList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate("li","click",function(e){
                that.elems.operation.find("li").removeClass("on");
                $(this).addClass("on");
                that.loadPageData();
            });
            that.elems.optionBtn.delegate(".commonBtn","click",function(e){
              var type=$(this).data("flag");
                if(type=="export"){
                    $.messager.confirm("导出订单列表","你确定导出当前列表的数据吗？",function(r){
                        if(r){
                            that.loadData.exportOderList();
                        }
                    });

                }
                if(type=="add"){
                    that.cancelOrder();
                    if(that.elems.tabel.datagrid("getChecked").length>0){
                        that.cancelOrder();
                    }  else{
                        //alert("至少选择一笔订单");
                    }

                }
            });
            $('#sales').tooltip({
                content: function(){
                    return  $("#Tooltip");
                },
                showEvent: 'click',
                onShow: function(){
                    var nodes=$("#Tooltip").find(".treeNode").tree('getChecked');
                    $.each(nodes,function(){
                        var me=this
                        $("#Tooltip").find(".treeNode").tree('uncheck',me.target);
                    });
                    var t = $(this);
                    t.tooltip('tip').unbind().bind('mouseenter', function(){
                        t.tooltip('show');
                    }).bind('mouseleave', function(){
                        t.tooltip('hide');
                    });
                }
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;


            $('#txtDataFromID').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":0,
                    "text":"请选择"

                }]
            });

            that.loadData.getFromList(function(data){
                debugger;
                data.topics.push({ "PaymentTypeID": -1, PaymentTypeName: "请选择", "selected": true });

                for (i = 0; i < data.topics.length; i++)
                {
                    if (data.topics[i].PaymentTypeCode == "CCAlipayWap")
                    {
                        data.topics[i].PaymentTypeName = "平台支付宝支付(连锁掌柜)";
                    }
                }

                $('#txtDataFromID').combobox({
                    width:wd,
                    height:H,
                    panelHeight:that.elems.panlH,
                    valueField: 'Id',
                    textField: 'Description',
                    data:data.data
                });
            });

            $('#payment').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":-1,
                    "text":"全部",
                    "selected":true
                },{
                    "id":1,
                    "text":"已付款"
                },{
                    "id":0,
                    "text":" 未付款"
                }]
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
                      var data={};
                    data["fields"]=fields;
                    data['rowData']=that.elems.tabel.datagrid("getChecked");
                    that.loadData.operation(data,that.elems.optionType,function(data){

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

            })
            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="-1"?"":filed.value;
                that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });
           that.loadData.seach.Field7=that.elems.operation.find("li.on").data("field7");





        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.topics){

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
                data:data.topics,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/

                /*  pageNumber:1,*/
                frozenColumns:[[
                    {
                        field : 'ck',
                        width:70,
                        align:'center',
                        checkbox : true
                    }
                ]],
                columns : [[


                    {field : 'order_no',title : '订单编号',width:140,resizable:false,align:'center'},
                    {field : 'paymentcenter_id',title : '商户单号',width:100,resizable:false,align:'left'},
                    {field : 'total_amount',title : '订单金额(元)',width:100,resizable:false,align:'center'},
                    {field : 'payment_name',title : '支付方式',width:100,align:'center',resizable:false},
                    {field : 'total_qty',title : '商品数量',width:100,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return parseInt(value);
                            }
                        }
                    },
                    {field : 'DeliveryName',title : '配送方式',width:120,align:'center',resizable:false},
                    {field : 'vip_name',title : '会员信息',width:120,align:'left',resizable:false,
                        formatter:function(value,row,index){

                            return "<p>"+value+"</p><p>"+(row.vipPhone || '')+"</p>";

                        }
                    },

                    {field : 'data_from_name',title : '订单渠道',width:100,align:'left',resizable:false},
                    {field : 'Field1',title : '付款状态',width:100,align:'left',resizable:false,
                        styler: function(index,row){
                            /* status: "700"
                             status_desc: "已完成"*/
                            return 'color: #fc7a52;';    //rowStyle是一个已经定义了的ClassName(类名)

                        },
                        formatter:function(value,row,index){
                            var str="";
                            switch(value){
                                case "0": str="未付款";  break;
                                case "1": str="已付款";  break;
                            }
                            return str;
                        }
                    },

                    {field : 'status_desc',title : '订单状态',width:70,align:'center',resizable:false,
                        styler: function(index,row){
                            /* status: "700"
                             status_desc: "已完成"*/
                            return 'color: #fc7a52;';    //rowStyle是一个已经定义了的ClassName(类名)

                        }

                    },
                /*    {field : 'create_time',title : '下单时间',width:120,align:'left',resizable:false,
                     formatter:function(value ,row,index){
                     return new Date(value).format("yyyy-MM-dd hh:mm");
                     }
                     },*/
					 //Field9
                    {field : 'create_time',title : '下单日期',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            if(value) {
                                return new Date(value).format("yyyy-MM-dd hh:mm");
                            }
                        }
                    },

                   /* {field : 'modify_time',title : '订单状态更新时间',width:120,align:'left',resizable:false,
                     formatter:function(value ,row,index){
                     return new Date(value).format("yyyy-MM-dd hh:mm");
                     }
                     }*/

                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.show();
                    }
                    if( that.loadData.seach.Field7=="1234567890"){  //配送门店显示问题

                        $("#optBtnss").show();
                    }else{
                        $("#optBtnss").hide()
                    }
                },
                onClickRow:function(rowindex,rowData){
                    debugger;
                    if(that.elems.click){
                        that.elems.click = true;
                        debugger;

                        var mid = JITMethod.getUrlParam("mid");
                         location.href = "orderDetail.aspx?orderId=" + rowData.order_id +"&mid=" + mid;
                    }

                },onClickCell:function(rowIndex, field, value){
                    if(field=="ck"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }

            });



            //分页
            data.Data={};
            data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
                pno: page?page+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPageCount,
                totalRecords: data.totalCount,
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
            this.loadData.args.start = (currentPage-1)*15;
            that.loadData.getOrderList(function(data){
                that.renderTable(data);
            });
        },


        //取消订单
        cancelOrder:function(data){
            var that=this;
            that.elems.optionType="setunit";
            $('#win').window({title:"取消订单",width:360,height:260});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_setUnit');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            that.loadData.get_unit_tree(function(data) {
                debugger;


                $("#setUnit").combotree({
                    // animate:true
                    lines: true,
                    checkbox: true,
                    id: 'id',
                    text: 'text',
                    data: data

                });
                $(".tooltip ").hide();
                /*  })*/
            });
        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 0,
                PageSize: 6,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            getUitTree:{
                node:""
            },
            seach:{
                sales_unit_id:"", //门店id
                Field7:"0",
                form:{
                    order_no:"",
                    vip_no:"",//会员
                    //Field1:"", //是否已付款 1 已付款 0 未付款。   全部不需要传递
                    sales_unit_id:"",  //门店名称
                    order_date_begin:"", //下单时间
                    order_date_end:"",   // 下单时间
                    data_from_id:"" ,     //订单渠道
                    DeliveryId:null ,
                    ModifyTime_begin:"", //单据日期
                    ModifyTime_end:"" //单据日期
                }
            },
            opertionField:{},
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
            //导出订单列表
            exportOderList: function () {
                var getUrl = '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=Export_lj&Field7='+this.args.Field7+'&param='+JSON.stringify(this.seach.form);//&req=';
                var data = {
                    Field7:this.args.Field7,
                    param:this.seach.form
                };
                this.exportExcel(data, getUrl);
            },
            getOrderList: function (callback) {
                $.util.oldAjax({
                    url: "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx",
                      data:{
                          action:'PosOrder_lj',
                          sales_unit_id:this.seach.sales_unit_id,
                          Field7:this.seach.Field7,
                          page:this.args.page,
                          start:this.args.start,
                          limit:this.args.limit,
                          form:this.seach.form
                      },
                      success: function (data) {
                          debugger;
                        if (data.topics) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
            getFromList: function (callback) {   //获取来源
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/VipSourceHandler.ashx",
                    data:{
                        page:1,
                        start:0,
                        limit:25,
						vipSourceType:1
                    },
                    success: function (data) {
                        if (data) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("分类加载异常请联系管理员");
                        }
                    }
                });
            },
            get_unit_tree: function (callback) {
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
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"",QueryStringData:{}}};
                prams.url="/Module/Order/InoutOrders/Handler/Inout3Handler.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                $.each(pram.fields,function (index,filed) {

                    prams.data.QueryStringData[filed.name] = filed.value;
                });
                if(pram.rowData.length>0){
                    prams.data.QueryStringData["orderList"]="'"+pram.rowData[0].order_no+"'";
                    for(var i=1;i<pram.rowData.length;i++){
                        prams.data.QueryStringData["orderList"]+=","+"'"+pram.rowData[0].order_no+"'";
                    }
                }
                switch(operationType){
                    case "setunit":prams.data.action="SetUnit";  //上架
                        break;

                }


                $.util.oldAjax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.success ) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            $.messager.alert("提示",data.msg);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

