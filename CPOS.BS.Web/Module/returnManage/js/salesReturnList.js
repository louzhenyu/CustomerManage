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
            operation:$('#opt'),              //弹出框操作部分
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
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getSalesReturnList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            //导出退款单
            that.elems.tabelWrap.delegate(".commonBtn", "click", function (e) {

                var optType = $(this).data("flag");
                if(optType=="export")
                {
                    that.ExportSalesReturnExcel(that.elems.operation.find("li.on").data("status"));
                }
            });
            that.elems.operation.delegate("li","click",function(e){
                that.elems.operation.find("li").removeClass("on");
                $(this).addClass("on");
                that.loadPageData()
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
            $('#DeliveryType').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                value:"0",
                data:[{
                    "id":1,
                    "text":"快递送回"
                },{
                    "id":2,
                    "text":"送回门店"
                },{
                    "id":0,
                    "text":"全部"
                }]
            });

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
            /**************** -------------------弹出easyui 控件  End****************/

            that.loadData.getPayMentList(function(data){
                debugger;
                data.topics.push({"PaymentTypeID":-1,PaymentTypeName:"请选择","selected":true});
                $('#txtDataFromID').combobox({
                    width:wd,
                    height:H,
                    panelHeight:that.elems.panlH,
                    valueField: 'PaymentTypeID',
                    textField: 'PaymentTypeName',
                    data:data.topics
                });
            });
                for (i = 0; i < data.topics.length; i++) {
                    if (data.topics[i].PaymentTypeCode == "CCAlipayWap") {
                        data.topics[i].PaymentTypeName = "平台支付宝支付(连锁掌柜)";
                    }
                }

                $('#txtDataFromID').combobox({
                    width:wd,
                    height:H,
                    panelHeight:that.elems.panlH,
                    valueField: 'PaymentTypeID',
                    textField: 'PaymentTypeName',
                    data:data.topics
                });
            });
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
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                if(filed.value||filed.value=="") {
                    that.loadData.seach[filed.name] = filed.value==-1 ? "" : filed.value;
                }
            });

            that.loadData.seach.Status=that.elems.operation.find("li.on").data("status")



        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.elems.sectionPage.find(".queryBtn").trigger("click");
            $.util.stopBubble(e);
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
    //导出退货列表
    ExportSalesReturnExcel: function () {
        this.setCondition();
        var getUrl = '/ApplicationInterface/Module/Order/SalesReturn/DownLoadSales.ashx?method=ExportDownLoadSalesReturnOrderList&para='+JSON.stringify(this.loadData.seach);//;
        this.exportExcel(this.loadData.seach, getUrl);
    },
        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.SalesReturnList){

                return;
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
                data:data.Data.SalesReturnList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[

                    {field : 'ItemName',title : '商品名称',width:165,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=26;
                            if(value&&value.length>long){
                                return '<img src="'+row.ImageUrl+'" width="70" height="70"  /> <div class="rowText" title="'+value+'"><p>退货单号:'+row.SalesReturnNo+'</p>'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<img src="'+row.ImageUrl+'" width="70" height="70"  /> <div class="rowText"><p>退货单号:'+row.SalesReturnNo+'</p>'+value+'</div>'
                            }
                        }
                    },
                    {field : 'paymentcenterId',title : '商户单号',width:58,resizable:false,align:'center'},
                    {field : 'VipName',title : '会员名称',width:90,align:'center',resizable:false},
                   /* {field : 'DeliveryType',title : '数量',width:58,align:'center',resizable:false,
                        formatter:function(value,row,index){
                           if(isNaN(parseInt(value))){
                             return 0;
                           }else{
                              return parseInt(value);
                           }
                        }
                    },*/

                    {field : 'DeliveryType',title : '配送方式',width:120,align:'center',resizable:false,
                     formatter:function(value,row,index){
                         var staus="";
                         switch (value){
                             case 2: staus="送回门店";break;
                             case 1: staus="快递送回";break;
                         }
                         return staus;
                     }
                    },


                    {field : 'Status',title : '退货状态',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            debugger;
                            var staus;
                            switch (value){
                                case 1: staus="待审核";break;
                                case 2: staus="取消申请";break;
                                case 3: staus="审核不通过";break;
                                case 4: staus="待收货（审核通过）";break;
                                case 5: staus="拒绝收货";break;
                                case 6: staus="已完成（待退款）";break;
                                case 7: staus= "已完成（已退款）"; break;
                            }
                            return staus;
                        },styler: function(index,row){

                            return 'color: #fc7a52;';    // rowStyle是一个已经定义了的ClassName(类名)

                        }

                    },
                    {field : 'paymentName',title : '支付方式',width:90,align:'center',resizable:false},
                    {field : 'CreateTime',title : '申请日期',width:80,align:'center',resizable:false}

                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                      debugger;
                     if(that.elems.click){
                     that.elems.click = true;
                     debugger;

                     var mid = JITMethod.getUrlParam("mid");
                      location.href = "salesReturnDetail.aspx?SalesReturnID=" + rowData.SalesReturnID +"&mid=" + mid;
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

            kkpager.generPageHtml({
                pno:that.loadData.args.PageIndex,
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
            this.loadData.args.PageIndex = currentPage;

            that.loadData.getSalesReturnList(function(data){
                that.renderTable(data);
            });
        },



        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'

                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                SalesReturnNo:"",
                DeliveryType:"0",
                Status:0
            },
            opertionField:{},
            getPayMentList: function (callback) {   //获取支付方式
                $.util.oldAjax({
                    url: "/Module/PayMent/Handler/PayMentHander.ashx",
                    data:{
                        QueryStringData:{
                            mid:__mid
                        },
                        form:{
                            "Payment_Type_Name": "",
                            "Payment_Type_Code": "",
                            "IsOpen":"true"
                        },
                        page: 1,

                        action:"getPayMentTypePage"
                    },
                    success: function (data) {
                        if (data) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("支付方式加载异常");
                        }
                    }
                });
            },
            getSalesReturnList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                      data:{
                          action:'Order.SalesReturn.GetSalesReturnList',
                          SalesReturnNo:this.seach.SalesReturnNo,
                          DeliveryType:this.seach.DeliveryType,
                          Status:this.seach.Status,
                          paymentcenterId: this.seach.paymentcenterId ,
                          payId:this.seach.payId,
                          PageSize:this.args.PageSize,
                          PageIndex:this.args.PageIndex
                      },
                      success: function (data) {
                          debugger;
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
                prams.url="/ApplicationInterface/Module/Item/ItemNewHandler.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.ItemInfoList=[];
                $.each( pram.ItemInfoList,function(){
                           var me=this;
                    prams.data.ItemInfoList.push({Item_Id:me.Item_Id})
                });

                if(pram.SalesPromotionList) {
                    prams.data.SalesPromotionList=[];
                    $.each(pram.SalesPromotionList, function () {
                        var me = this;
                        prams.data.SalesPromotionList.push({ItemCategoryId: me.id})
                    });
                }
                switch(operationType){
                    case "putaway":prams.data.action="ItemToggleStatus";  //上架
                        prams.data.Status="1";
                        break;
                    case "soldOut":prams.data.action="ItemToggleStatus";  //下架
                        prams.data.Status="-1";
                        break;
                    case "sales":prams.data.action="UpdateSalesPromotion";  //更改促销分组
                        break;
                }


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

