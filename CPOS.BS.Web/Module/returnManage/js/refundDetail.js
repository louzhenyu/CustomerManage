define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            updateBtn: $(".updateBtn"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('.optionBtn'),              //操作按钮部分
            vipSourceId: '',
            click: true,
            ishideOption:false,
            dataMessage: $("#pageContianer").find(".dataMessage"),
            commodity:$("#commodity"),        //商品列表
            panlH: 116                           // 下来框统一高度
        },
        detailDate: {},
        ValueCard: '',//储值卡号
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                             //标签类型
            tagList: []                              //标签列表
        },
        init: function () {
            //获得地址栏参数为Item_Id的值
            var RefundID = $.util.getUrlParam("RefundID");
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.RefundID = RefundID;
            this.elems.operation.find(".commonBtn[data-status]").hide();
            this.elems.operation.hide();
            this.initEvent();
            this.loadPageData();






        },
        initEvent: function () {
            var that = this;
            $("[data-flag='update']").show();
            //点击查询按钮进行数据查询


            that.elems.operation.delegate(".commonBtn", "click", function (e) {
                debugger;
                var me=$(this);
                that.elems.title=$(this).html()+"操作";
                $.messager.confirm("确认收款操作提示","操作不可逆,请核对信息无误后,点击确定完成该操作。", function (r) {
                           if(r){
                               that.loadData.operation('','',function(){
                                   that.loadPageData();
                                   alert("操作成功");
                               })

                           }
                })

            });
           /* that.elems.updateBtn.delegate(".commonBtn", "click", function (e) {
                var me=$(this)

                if(me.data("flag")) {
                    //  download
                    switch(me.data("flag")){
                        case "update":
                            that.loadData.GetTUnit(function(datas){

                                $("#carrier").combobox({
                                    panelHeight: that.elems.panlH,
                                    valueField: 'unit_id',
                                    textField: 'unit_name',
                                    data:datas
                                });

                            that.loadData.GetDeliveryType(function(data){

                                $("#DeliveryType").combobox({
                                    panelHeight: that.elems.panlH,
                                    valueField: 'DeliveryId',
                                    textField: 'DeliveryName',
                                    data:data,
                                    onSelect:function(record){
                                        debugger;
                                        $("#optionDelivery").find("[data-flag]").hide()
                                            // var flag=$("#DeliveryType").combobox("getText");
                                        var selet = "[data-flag='"+record.DeliveryName+"']";
                                        $(selet).show();

                                    }
                                });
                                $("#DeliveryType").combobox("select",that.loadData.opertionField.DeliveryType);
                            $("#optionDelivery").form('load',that.loadData.opertionField);
                            $("#optionDelivery").show();
                                //$("#carrier").combobox("setValue", that.elems.RefundIDInfo.carrier_name);
                                me.parents(".panlDiv").find("[data-type]").hide().show();
                                var selet = "[data-type='"+me.data("type")+"']";
                                $(selet).hide();

                            });
                            });
                            break;
                        case "save":
                           var fileds= $('#optionDelivery').serializeArray();
                            $.each(fileds,function(index,filed){
                                that.loadData.opertionField[filed.name]=filed.value;
                            });
                            that.loadData.saveDeliveryInfo(function(){
                                $("#DeliveryType").combobox("select",that.loadData.opertionField.DeliveryType);
                                me.parents(".panlDiv").find("[data-type]").hide().show();
                                var selet = "[data-type='"+me.data("type")+"']";
                                $(selet).hide();
                                that.loadPageData();
                            });
                            break;
                        case "cancel":
                            $("#DeliveryType").combobox("select",that.loadData.opertionField.DeliveryType);
                            me.parents(".panlDiv").find("[data-type]").hide().show();
                            var selet = "[data-type='"+me.data("type")+"']";
                            $(selet).hide();
                            break;
                    }

                }

            });*/
            /**************** -------------------弹出easyui 控件 start****************/
            var wd = 200, H = 30;
            $('#item_status').combobox({
                width: wd,
                height: H,
                panelHeight: that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data: [
                    {
                        "id": 1,
                        "text": "上架"
                    },
                    {
                        "id": -1,
                        "text": "下架"
                    },
                    {
                        "id": 0,
                        "text": "全部"
                    }
                ]
            });

            $('#txtDataFromID').combobox({
                width: wd,
                height: H,
                panelHeight: that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data: [
                    {
                        "id": 1,
                        "text": "上架"
                    },
                    {
                        "id": -1,
                        "text": "下架"
                    },
                    {
                        "id": 0,
                        "text": "全部"
                    }
                ]
            });
            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true
            });
            $('#panlconent').layout({
                fit: true
            });




            $('#win').delegate(".saveBtn", "click", function (e) {


                    if ($('#orderOption').form('validate')) {
                        var fields = $('#orderOption').serializeArray(); //自动序列化表单元素为JSON对象

                        that.loadData.operation(fields, that.elems.optionType, function (data) {

                            alert("操作成功");
                            $('#win').window('close');
                            that.loadPageData(e);

                        });
                    }

            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            $('#tableWrap').delegate(".fontC","click",function(e) {
                debugger;
                that.elems.rowIndex = $(this).data("index");

                var optType = $(this).data("oprtype");
                if( that.elems.rowIndex!=null) {
                    that.elems.commodity.datagrid('rejectChanges');
                    that.elems.commodity.datagrid('beginEdit', that.elems.rowIndex);
                    $('#tableWrap').find(".fontC").click(function () {
                        var optType = $(this).data("oprtype");
                        switch (optType) {
                            case "cancel":
                                that.elems.commodity.datagrid('endEdit', that.elems.rowIndex);  //结束编辑
                                that.elems.rowIndex = null; break;
                            case "save":
                                alert("功能开发中");
                                //that.loadPageData();

                                that.elems.commodity.datagrid('endEdit', that.elems.rowIndex);  //结束编辑
                                that.elems.rowIndex = null;  break;

                        }
                    });
                }


            });


            /*d.Field1 == "1" || d.Field7 == "800" || d.Field7 == "900" */  //
            // 收款按钮不显示
            /**************** -------------------列表操作事件用例 End****************/
        },


        //设置查询条件   取得动态的表单查询参数


        //加载页面的数据请求
        loadPageData: function () {
            var that = this;
            that.elems.operation.find(".commonBtn[data-status]").hide();
            that.elems.operation.hide();

            that.loadData.getRefundOrderDetail(function (data) {

                var salesReturnInfo=data.Data;

                //Status	Int	状态）
                //that.renderTable(salesReturnInfo);
                var staus,isShowhtml=false,htmlText;
                switch (salesReturnInfo.Status){
                    case 1: staus="待退款";break;
                    case 2: staus="已完成";isShowhtml=true; htmlText="<p>退款已完成</p>";break;
                }
                salesReturnInfo.StatusName=staus;
                if(salesReturnInfo.OrderDetail) {
                    salesReturnInfo.ItemName = salesReturnInfo.OrderDetail.ItemName

                    var item = salesReturnInfo.OrderDetail.SkuDetail;
                    var skuList = [];
                    if (item) {
                        for (var i = 0; i < 5; i++) {
                            debugger;
                            var valueKey = "PropDetailName" + i;
                            var nameKey = "PropName" + i;
                            if (item[nameKey] && item[valueKey]) {
                                skuList.push({skuName: item[nameKey], skuValue: item[valueKey]})
                            }

                        }
                        if (skuList.length) {
                            var html = bd.template('suklistValue', { list: skuList });
                            $("#skuList").html(html);
                        }
                    }
                }
                that.elems.operation.find(".commonBtn[data-status]").each(function() {

                    var me = $(this).data("showstaus");

                    if (me && me.toString().indexOf(salesReturnInfo.Status) != -1) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });

                if(isShowhtml) {
                    that.elems.operation.html(htmlText);
                }
                that.elems.operation.show();
                salesReturnInfo.Points=salesReturnInfo.Points+"分 (抵扣"+salesReturnInfo.PointsAmount+"元)";
                $("#salesReturnInfo").form('load',salesReturnInfo);

                if(salesReturnInfo.ItemID){
                     $(".showItem").show();
                }else{
                    $(".hideItem").show();
                  var  str="<a target='_blank' href='/module/chainCloudOrder/orderDetail.aspx?orderId=" + salesReturnInfo.OrderID +"&mid=" +  JITMethod.getUrlParam("mid")+ "'style='color: #07c8cf'>订单详情</a>";
                    $("#hrefOrderDetail").html(str);
                }

            })


        },
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.InoutDetailList){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.commodity.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.InoutDetailList,
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
                   {field : 'imageUrl',title : '图片',width:70,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var html=' <img src="images/商品.png" width="70" height="70"  />';
                            if(value){
                                html=' <img src="'+value+'" width="70" height="70"  />'
                            }

                            return html;
                        }

                    },
                    {field : 'item_name',title : '商品名称',width:225,align:"left",resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'prop_1_detail_name',title : '规格',width:263,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            var value=""

                           for(var i=1;i<6;i++){
                               debugger;
                               var  filed= "sku_prop_", filedName="sku_prop_{0}_name",prop_value="prop_{0}_detail_name";
                               prop_value=prop_value.format(i);
                               filedName=filedName.format(i);
                               if(data.skuCfg[filed+i]=="1"&&row[prop_value]){

                                   value+="<em class='sku'>{0}:{1}</em>".format(data.skuCfg[filedName],row[prop_value]);

                               }

                           }
                           return value;

                        }
                    },
                    {field : 'order_qty',title : '数量',width:60,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return parseInt(value);
                            }
                        },editor: {
                        type: 'numberbox',
                        options: {
                            min: 0,
                            precision: 0,
                            height: 31,
                            width: 136


                        }
                    }

                    },

                    {field : 'std_price',title : '单价',width:60,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return value;
                            }
                        },editor: {
                        type: 'numberbox',
                        options: {
                            min: 0,
                            precision: 0,
                            height: 31,
                            width: 136


                        }
                    }


                    },
                    { title: "操作", field: '', width: 180, align: 'center',hidden:that.elems.ishideOption, resizable: false,
                        formatter: function (value, row, index) {
                          return "<div class='fontC' data-index="+index+">修改</div>"
                        }, editor: {
                        type: 'optionbtn'
                       }
                    }
/*                    { title: "单价", field: 'std_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
                    { title: "数量", field: 'order_qty', width: 90, menuDisabled: true, align: "right", flex: 1 },
                    { title: "折扣", field: 'discount_rate', width: 90, menuDisabled: true, align: "right", flex: 1 },
                    { title: "标准价", field: 'enter_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
                    { title: "总金额", field: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }
                    , { title: "备注", field: 'remark', width: 90, menuDisabled: true, align: "right", flex: 1 }*/
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.commodity.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                }


            });



         /*   //分页
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
            }*/
        },

        //staus
        fnbtn:function(status) {
            var that=this ;
            that.orderOption();
            //下级状态值
            this.loadData.args.nextStatus = status;

            $('#panlconent').find(".DeliverCompany").hide(); //配送商
            $('#panlconent').find(".DeliverOrder").hide(); //配送单号
            $('#panlconent').find(".PayMethod").hide(); //收款方式
            $('#panlconent').find(".CheckResult").hide(); //审核不通过理由
            $('#panlconent').find(".amount").hide(); //订单金额
            /*禁用验证*/
            $("#PayMethod").combobox({novalidate:true});
            $("#CheckResult").combobox({novalidate:true});
            $("#DeliverCompany").combobox({novalidate:true});

            $("#Remark").validatebox({novalidate:true});
            var isopen=true;
            //根据下级状态显示对应功能
            switch (status) {
                case 900: //审核不通过
                   $('#panlconent').find(".CheckResult").show();
                    that.loadData.args.OptionName="CheckResult";//获取审核不通过的条件
                    $("#CheckResult").combobox({novalidate:false});
                    $("#Remark").validatebox({novalidate:false});
                    that.loadData.GetOptionsByName(function(data){
                        $("#CheckResult").combobox({
                            panelHeight: that.elems.panlH,
                            valueField: 'OptionValue',
                            textField: 'OptionText',
                            data:data
                        });
                        $('#win').window('open');
                    });
                    isopen=false;
                    break;
                case 600: //发货
                   $('#panlconent').find(".DeliverCompany").show();
                   $('#panlconent').find(".DeliverOrder").show();
                    $("#DeliverCompany").combobox({novalidate:false});
                    that.loadData.GetTUnit(function(data){
                        $("#DeliverCompany").combobox({
                            panelHeight: that.elems.panlH,
                                valueField: 'unit_id',
                                textField: 'unit_name',
                                data:data
                        });
                        $('#win').window('open');
                    });
                    isopen=false;
                    break;

                case 1000: //收款
                   $('#panlconent').find(".PayMethod").show();
                   $('#panlconent').find(".amount").show();
                    $("#PayMethod").combobox({novalidate:false});
                    $("#Amount").numberbox('setValue',that.elems.RefundIDInfo.actual_amount);
                   that.loadData.args.OptionName="PayMethod";
                    //获取付款方式
                    that.loadData.GetOptionsByName(function(data){
                        $("#PayMethod").combobox({
                            panelHeight: that.elems.panlH,
                            valueField: 'OptionValue',
                            textField: 'OptionText',
                            data:data
                        });
                        $('#win').window('open');
                    });
                    isopen=false;
                    break;
            }
          if(isopen){
              $('#win').window('open');
          }

        },

        //取消订单
       orderOption: function () {
            var that = this;
            //that.elems.optionType = "cancel";
            if(!that.elems.title){
                that.elems.title="订单操作"
            }
            $('#win').window({title: that.elems.title, width: 660, height: 300,top:($(window).height()-300)/2,left:($(window).width()-660)/2});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove', 'center');
            var html = bd.template('tpl_OrderPayMent');
            var options = {
                region: 'center',
                content: html
            };
            $('#panlconent').layout('add', options);

        },
        loadData: {

            args: {
                order_detail_id: "",
                nextStatus:'',
                RefundID: ''
            },

            opertionField: {
                //order_id:""

                ReceiveMan: "",// 联系人
                Addr: "",//上海市 上海市 长宁区 长宁路8号
                Phone: "",//手机
                PostCode: '',
                DeliveryType: 2, //配送方式 1 送货到家， 2是到店自提
                Carrier_id: "",//配送商id8551f2a988899d9425ebc27d2082cfeb
                DeliveryCode: "",//配送单号2343242
                SendTime: ""//配送时间

            },
            getRefundOrderDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Order.SalesReturn.GetRefundOrderDetail',
                        RefundID:this.args.RefundID //"17E9557F-5144-4C1C-8FE3-90E47ECB9A1C"//
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


            operation: function (pram, operationType, callback) {
                debugger;
                var prams = {data: {action: "Order.SalesReturn.SetRefundOrder"}};
                prams.url = "/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

             /*   $.each(pram,function(index,filed){
                    prams.data[filed.name]=filed.value;
                });


                //完成700；

                if (this.args.OperationType) {
                    prams.data["OperationType"] = this.args.OperationType;
                }*/
                prams.data["RefundID"]=this.args.RefundID;

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
            }

        }
    };
    page.init();
});

