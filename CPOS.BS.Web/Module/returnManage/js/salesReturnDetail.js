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
            var SalesReturnID = $.util.getUrlParam("SalesReturnID");
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.SalesReturnID = SalesReturnID;
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
                var me=$(this);
                that.elems.title=$(this).html()+"操作";
                if(me.data("status")) {
                    that.fnbtn(me.data("status"))

                }
                /*if(me.data("opttype")) {
                    //  download
                   switch(me.data("opttype")){
                       case "download": that.loadData.fnDownLoadDelivery(); break;
                       case "print2": that.loadData.fnPrintDelivery(); break;
                       case "print1":  that.loadData.fnPrintPicking(); break;
                   }

                }*/

            });

            /**************** -------------------弹出easyui 控件 start****************/
            var wd = 200, H = 30;


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
            that.elems.updateBtn.delegate(".commonBtn", "click", function (e) {
                var me=$(this);
                that.loadData.args.OperationType=5;
                if(me.data("flag")) {
                    //  download
                    switch(me.data("flag")){
                        case "update":
                            me.parents(".panlDiv").find("[data-type]").hide().show();
                            var selet = "[data-type='"+me.data("type")+"']";
                            me.parents(".panlDiv").find(selet).hide();

                            break;
                        case "save":

                            var optType="#"+me.parent().data("opttype");
                            debugger;

                            if ($(optType).form('validate')) {
                                if($(optType).length>0) {
                                    var fields = $(optType).serializeArray(); //自动序列化表单元素为JSON对象
                                    var name='ActualQty';
                                        if(fields[0]["name"]==name) {
                                            if (that.elems.qty && fields[0].value&& fields[0].value > that.elems.qty) {
                                                $.messager.alert("提示","实退数量不可大于销售数量");
                                                return false;
                                            }
                                        }

                                    that.loadData.operation(fields, "", function () {
                                        alert("操作成功");
                                        that.loadPageData(function(){
                                            me.parents(".panlDiv").find("[data-type]").hide().show();
                                            var selet = "[data-type='"+me.data("type")+"']";
                                            me.parents(".panlDiv").find(selet).hide();
                                        });

                                    });
                                }
                            }
                            break;
                        case "cancel":

                            me.parents(".panlDiv").find("[data-type]").hide().show();
                            var selet = "[data-type='"+me.data("type")+"']";
                            me.parents(".panlDiv").find(selet).hide();
                            break;
                    }

                }

            });



            $('#win').delegate(".saveBtn", "click", function (e) {


                    if ($('#orderOption').form('validate')) {
                        var fields = $('#orderOption').serializeArray(); //自动序列化表单元素为JSON对象

                        that.loadData.operation(fields, that.elems.optionType, function (data) {

                            alert("操作成功");
                            $('#win').window('close');
                            that.loadPageData();

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
        setCondition: function () {
            debugger;
            var that = this;
            //查询每次都是从第一页开始
            that.loadData.args.start = 0;
            var fileds = $("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                filed.value = filed.value == "-1" ? "" : filed.value;
                that.loadData.seach[filed.name] = filed.value;
                that.loadData.seach.form[filed.name] = filed.value;
            });
            that.elems.operation.find("li.on").data("")


        },

        //加载页面的数据请求
        loadPageData: function (callback) {
            var that = this;
            that.elems.operation.find(".commonBtn[data-status]").hide();
            that.elems.operation.hide();

               that.loadData.getSalesReturnDetail(function (data) {
                   if (callback) {
                       callback(data);
                   }
                   $("#ActualQtyPanl").html(" ");
                   $("#ServicesTypePanl").html(" ");
                   $("#ConfirmAmountPanl").html(" ");
                   var salesReturnInfo=data.Data;
                   //DeliveryType	Int	退回商品方式（1=送回门店；2=快递送回）
                   salesReturnInfo.DeliveryTypeName=salesReturnInfo.DeliveryType==2?"送回门店":"快递送回";
                   //ServicesType	Int	服务类型（1=退货；2=换货）
                   salesReturnInfo.ServicesTypeName=salesReturnInfo.ServicesType==1?"退货":"换货";

                   //Status	Int	状态）

                   var staus,isShowhtml=false,htmlText;
                   switch (salesReturnInfo.Status){
                       case 1: staus="待审核";break;
                       case 2: staus="取消申请";isShowhtml=true; htmlText="<p>已取消退货申请</p>";break;
                       case 3: staus="审核不通过";break;
                       case 4: staus="待收货（审核通过)";break;
                       case 5: staus="拒绝收货";break;
                       case 6: staus="已完成（待退款）"; isShowhtml=true; htmlText="<p>退货已完成</p>";break;
                       case 7: staus= "已完成（已退款）"; isShowhtml=true; htmlText="<p>退货已完成</p>"; break;
                   }
                   salesReturnInfo.StatusName=staus;
                    that.renderTable(salesReturnInfo);
                   salesReturnInfo.applyQty =salesReturnInfo.Qty;
                   salesReturnInfo.OrderDetail.ItemName+="X"+salesReturnInfo.OrderDetail.Qty ;
                   var item=salesReturnInfo.OrderDetail.SkuDetail;
                   that.elems.qty=salesReturnInfo.OrderDetail.Qty;
                   var skuList=[];
                   if(item) {
                       for (var i = 0; i < 5; i++) {
                           debugger;
                          var valueKey="PropDetailName"+i;
                          var nameKey="PropName"+i;
                           if(item[nameKey]&&item[valueKey]){
                               skuList.push({skuName:item[nameKey],skuValue:item[valueKey]})
                           }

                       }
                       if (skuList.length) {
                           var html = bd.template('suklistValue', { list: skuList });
                          $("#skuList").html(html);
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
                       $(".updateBtn").hide();
                   }else if(salesReturnInfo.Status==1||salesReturnInfo.Status==3){
                       $(".updateBtn").show();
                       $("#ConfirmUpdate").hide();
                   }else{
                       $(".updateBtn").show();
                   }
                   that.elems.operation.show();
                   debugger;
                   $("#salesReturnInfo").form('load',salesReturnInfo);

                   $("#salesReturnInfo").form('load',salesReturnInfo.OrderDetail);






                   var html = bd.template('tpl_ActualQtyForm');
                   $("#ActualQtyPanl").html(html);
                   debugger
                   $("#ActualQtyPanl .easyui-numberbox").numberbox({
                       width:200,height:30,min:0,precision:0
                   });
                   $("#ActualQty").form('load',salesReturnInfo);
                    html = bd.template('tpl_ServicesTypeForm');
                   $("#ServicesTypePanl").html(html);
                   $("#ServicesType").form('load',salesReturnInfo);
                   var wd = 200, H = 30;
                   $('#ServicesTypeText').combobox({
                       width: wd,
                       height: H,
                       panelHeight: that.elems.panlH,
                       valueField: 'id',
                       textField: 'text',
                       data: [
                           {
                               "id": 1,
                               "text": "退货"
                           },
                           {
                               "id": 2,
                               "text": "换货"
                           },

                       ]
                   });

                   html = bd.template('tpl_ConfirmAmountForm');
                   $("#ConfirmAmountPanl").html(html);
                   $("#ConfirmAmountPanl .easyui-numberbox").numberbox({
                       width:200,height:30,min:0,precision:0
                   });
                   $("#ConfirmAmount").form('load',salesReturnInfo.OrderDetail)



               })



        },
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.HistoryList){

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
                data:data.HistoryList,
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

                    {field : 'OperationDesc',title : '操作',width:100,align:"left",resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },

                    {field : 'CreateTime',title : '时间',width:120,align:'left',resizable:false},

                    {field : 'OperatorName',title : '操作人',width:60,align:'left',resizable:false},
                    {field : 'HisRemark',title : '备注',width:225,align:"left",resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
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

            this.loadData.args.OperationType = status;
            if(status==1||status==3){
                $.messager.confirm(that.elems.title,"确定完成"+that.elems.title+"吗？",function(r){
                      if(r) {
                          that.loadData.operation("", "", function () {
                              alert("操作成功");
                              that.loadPageData();
                          });
                      }
                });
                 return false;
            }


            that.orderOption();
            //下级状态值
           // this.loadData.args.OperationType = status;


            /*禁用验证*/

            var isopen=true;
            //根据下级状态显示对应功能

          if(isopen){
              $('#win').window('open');
          }

        },

        //取消订单
       orderOption: function () {
            var that = this;
            //that.elems.optionType = "cancel";
            if(!that.elems.title){
                that.elems.title="退货操作"
            }
            $('#win').window({title: that.elems.title, width: 660, height: 300,top:($(window).height()-300)/2,left:($(window).width()-660)/2});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove', 'center');
            var html = bd.template('tpl_salesReturnOption');
            var options = {
                region: 'center',
                content: html
            };
            $('#panlconent').layout('add', options);
           $("#orderOption").find("textarea").val("1234").val("") ;    //textarea
        },
        loadData: {

            args: {
                order_detail_id: "",
                OperationType:'',
                SalesReturnID: '',
                OptionName:'CheckResult'
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
             //获取退货详情
            getSalesReturnDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Order.SalesReturn.GetSalesReturnDetail',
                        SalesReturnID:this.args.SalesReturnID //"EF58CDF7-9375-4472-8163-097F5CBA6C5E"//
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
                var prams = {data: {action: "Order.SalesReturn.SetSalesReturn"}};
                prams.url = "/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                $.each(pram,function(index,filed){
                    prams.data[filed.name]=filed.value;
                });


                  //完成700；

               if (this.args.OperationType) {
                   prams.data["OperationType"] = this.args.OperationType;
               }
                prams.data["SalesReturnID"]=this.args.SalesReturnID;

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

