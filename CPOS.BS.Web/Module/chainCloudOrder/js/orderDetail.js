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
            StatusList:$("#StatusList"),        //操作记录     GetInoutStatusList
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
            var orderId = $.util.getUrlParam("orderId");
            //vipId保存起来用来做查询交易记录的参数
            this.loadData.args.orderId = orderId;
            this.elems.operation.find(".commonBtn[data-status]").hide();
            this.elems.operation.hide();
            this.initEvent();
            this.loadPageData();
            $.extend($.fn.datagrid.defaults.editors, {
                optionbtn: {
                    init: function(container, options){
                        var divtn = $('<div class="fontC" data-oprtype="save">保存</div><div class="fontC" data-oprtype="cancel">取消</div>').appendTo(container);
                        return divtn;
                    },
                    getValue: function(target){
                        //return $(target).val();
                    },
                    setValue: function(target, value){
                       // $(target).val(value);
                    },
                    resize: function(target, width){
                      /*  var input = $(target);
                        if ($.boxModel == true){
                            input.width(width - (input.outerWidth() - input.width()));
                        } else {
                            input.width(width);
                        }*/
                    }
                }
            });
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
                if(me.data("opttype")) {
                    //  download
                   switch(me.data("opttype")){
                       case "download": that.loadData.fnDownLoadDelivery(); break;
                       case "print2": that.loadData.fnPrintDelivery(); break;
                       case "print1":  that.loadData.fnPrintPicking(); break;
                   }

                }
            });
            that.elems.updateBtn.delegate(".commonBtn", "click", function (e) {
                var me=$(this)

                if(me.data("flag")) {
                    //  download
                    switch(me.data("flag")){
                        case "update":
                            that.loadData.GetLogisticsCompany(function(data) {
                                $("#carrier").combobox({
                                    panelHeight: that.elems.panlH,
                                    valueField: 'LogisticsID',
                                    textField: 'LogisticsName',
                                    data: data.Data.LogisticsCompanyList
                                    
                                });
								that.loadData.GetDeliveryType(function(data){
	
									$("#DeliveryType").combobox({
										panelHeight: that.elems.panlH,
										valueField: 'DeliveryId',
										textField: 'DeliveryName',
										data:data,
										onSelect:function(record){
										    debugger;
										    if (record.DeliveryId == "2")
										    {
										        if ($("#setUnitId").combotree("getValue"))
										        {
										            $("#unitphone").val("");
										            $("#UnitAddr").val("");
										        }
										        
										    }
											$("#optionDelivery").find("[data-flag]").hide()
												// var flag=$("#DeliveryType").combobox("getText");
											var selet = "[data-flag='"+record.DeliveryName+"']";
											$(selet).show();
	
										}
									});
									$("#DeliveryType").combobox("select",that.loadData.opertionField.DeliveryType);
									$("#optionDelivery").form('load',that.loadData.opertionField);
									$("#optionDelivery").show();
									//$("#carrier").combobox("setValue", that.elems.orderIdInfo.carrier_name);
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

            });
            /**************** -------------------弹出easyui 控件 start****************/
            var wd = 160, H = 32;
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

                            alert(data.msg);
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
        loadPageData: function (e) {
            var that = this;
            that.elems.operation.find(".commonBtn[data-status]").hide();
            that.elems.operation.hide();
           that.loadData.getSkuPropCfg(function(skuCfg){
               that.loadData.get_unit_tree(function(data) {
                   debugger;


                   $("#setUnitId").combotree({
                       // animate:true
                       lines: true,
                       checkbox: true,
                       id: 'id',
                       text: 'text',
                       data: data,
                       onChange: function (para)
                       {
                           debugger;
                           that.loadData.GetUnitInfo(para, function (data) {
                               if (data)
                               {
                                   debugger;
                                   $("#unitphone").val(data.UnitPhone);
                                   $("#UnitAddr").val(data.UnitAddress);
                               }

                           });
                       }

                   });
                   $(".tooltip ").hide();
                   that.loadData.getOrderInfo(function (data) {

                       data.topics.skuCfg=skuCfg;
                       that.elems.orderIdInfo=data.topics;
                       var orderinfo=data.topics;
                       that.elems.ishideOption=orderinfo.status==700?true:false;
                       if(that.elems.ishideOption){
                           $(".updateBtn").remove();
                       }
                       if(orderinfo.status=='800'){
                           $('.printBtn').attr('data-opttype','');
                           $('.printBtn').css({'background':'#ccc','cursor':'default'});
                       }
                       /* orderinfo.create_time=new Date(orderinfo.create_time).format("yyyy-MM-dd hh:mm");*/
                       if(orderinfo.Field7=='500' || orderinfo.Field7=='100' || orderinfo.Field7=='900'){
                           $('#deliveryExpressBox').hide();
                           $('#expressNumBox').hide();
                           $('#deliveryExpressBox2').hide();
                           $('#expressNumBox2').hide();
                       }

                       if(orderinfo.Field7=='800' || orderinfo.Field7=='700'){
                           $("[data-flag='update']").hide();
                       }

                       that.elems.ishideOption=true;
                       that.renderTable(orderinfo);

                       orderinfo.memberPrice=orderinfo.VipDiscount;
                       if(orderinfo.DeliveryName=="到店自提"){
                           $(".DeliveryName1").hide();
                           $(".DeliveryName2").show();
                           $(".DeliveryInformation").show();
                           $(".ConsigneeInformation").hide();

                       }else{
                           $(".DeliveryName1").show();
                           $(".DeliveryName2").hide();
                           $(".DeliveryInformation").hide();
                           $(".ConsigneeInformation").show();

                       }

                       that.elems.operation.find(".commonBtn[data-status]").each(function(){

                           var me=$(this).data("showstaus");

                           if(me&&me.toString().indexOf(orderinfo.status)!=-1){
                               $(this).show();
                           } else{
                               $(this).hide();
                           }
                           if(orderinfo.status==100||orderinfo.status==900){
                               if(orderinfo.DeliveryName=="到店自提"){
                                   $(".commonBtn.DeliveryName1").hide();
                                   $(".commonBtn.DeliveryName2").show();

                               }else{
                                   $(".commonBtn.DeliveryName1").show();
                                   $(".commonBtn.DeliveryName2").hide();

                               }
                           }


                       });

                       //收款按钮单独处理
                       if (getStr(orderinfo.Field1) == "1") {
                           orderinfo["paystatus"]= "已付款";
                           $(".commonBtn[data-optType='payment']").hide()
                           $(".Paystatus").html("实付金额：");
                           $(".PaystatusDetail").html("实付金额明细");
                       } else {
                           orderinfo["paystatus"] = "未付款";
                           $(".Paystatus").html("应付金额：");
                           $(".PaystatusDetail").html("应付金额明细");
                           if(orderinfo.status!="800"&&orderinfo.status!="900")
                           {
                               $(".commonBtn[data-optType='payment']").show()
                           }
                       }
                       that.loadData.opertionField.DeliveryType=orderinfo.DeliveryName=="送货到家"?'1':'2';//配送方式

                       if (orderinfo.DeliveryName != "到店自提") {
                           that.loadData.opertionField.Carrier_id = orderinfo.carrier_id;//配送商id
                           that.loadData.opertionField.ReceiveMan = orderinfo.Field14;  //收件人
                           that.loadData.opertionField.Addr = orderinfo.Field4;//配送地址
                           that.loadData.opertionField.Phone = orderinfo.Field6;//手机号
                       }
                       that.loadData.opertionField.DeliveryCode=orderinfo.Field2;//配送号

                       /*purchase_unit_id: "0da0d3a6a0d899e6d639bfbf25005300"
                        purchase_unit_name: "浙江巨圣鞋业(桥下店)"*/

                       debugger
                       $("#orderInfo").form('load',orderinfo);


                       if (orderinfo.DeliveryName == "到店自提") {
                           that.loadData.opertionField.Addr = orderinfo.unit_address;//配送地址
                           that.loadData.opertionField.Phone = orderinfo.unit_tel;//手机号
                       }

                       that.elems.operation.show();
                   })
               });

           });
            that.loadData.GetInoutStatusList(function(data){
				
                that.elems.StatusList.datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : true, //单选
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.topics,
                    sortName : 'brandCode', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    idField : 'Item_Id', //主键字段
                    /* pageNumber:1,*/
                    /* frozenColumns : [ [ {
                     field : 'brandLevelId',
                     checkbox : true
                     } //显示复选框
                     ] ],*/

                    columns : [[
                        {field : 'LastUpdateTimeFormat',title : '更新时间',width:100,align:"left",resizable:false,

                        },
                        {field : 'StatusRemark',title : '描述',width:225,align:"left",resizable:false,
                            formatter:function(value,row,index){
                                if(row.PayMethodName) {
                                  return row.StatusRemark+"[付款方式:"+row.PayMethodName+"]"
                                } else{
                                    return row.StatusRemark
                                }
                            }
                        },
                        { title: "备注", field: 'Remark', width: 90, menuDisabled: true, align: "left", flex: 1 }
                        /*                    { title: "单价", field: 'std_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
                         { title: "数量", field: 'order_qty', width: 90, menuDisabled: true, align: "right", flex: 1 },
                         { title: "折扣", field: 'discount_rate', width: 90, menuDisabled: true, align: "right", flex: 1 },
                         { title: "标准价", field: 'enter_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
                         { title: "总金额", field: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }
                         , { title: "备注", field: 'remark', width: 90, menuDisabled: true, align: "right", flex: 1 }*/
                    ]]


                });

            });

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
                    {field : 'item_name',title : '商品名称',width:200,align:"left",resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'prop_1_detail_name',title : '规格',width:60,align:'center',resizable:false,
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
                    {field : 'enter_qty',title : '数量',width:60,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(value)){
                                return 0+row.Field9;
                            }else{
                                return value+row.Field9;
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

                    {field : 'enter_price',title : '单价(元)',width:60,align:'center',resizable:false,
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
                            precision:2,
                            height: 31,
                            width: 136


                        }
                    }
                    },
					
					{field : 'enter_amount',title : '金额(元)',width:60,align:'center',resizable:false,
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
                            precision:2,
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
                    that.loadData.GetLogisticsCompany(function(data){
                        $("#DeliverCompany").combobox({
                               panelHeight: that.elems.panlH,
                            valueField:'LogisticsID',
                            textField:'LogisticsName',
                            data:data.Data.LogisticsCompanyList
                        });
                        $('#win').window('open');
                    });
                    isopen=false;
                    break;

                case 10000: //收款
                   $('#panlconent').find(".PayMethod").show();
                   $('#panlconent').find(".amount").show();
                    $("#PayMethod").combobox({novalidate:false});
                    $("#Amount").numberbox('setValue',that.elems.orderIdInfo.actual_amount);
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
                orderId: '',
                OptionName:'CheckResult'
            },
            getUitTree:{
                node:""
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
                SendTime: "",//配送时间
				

            },
            //获取门店信息
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
             //获取订单详情
            getOrderInfo: function (callback) {
                $.util.oldAjax({
                    url: "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx",
                    data: {
                        action: 'GetInoutInfoById_lj',
                        order_id: this.args.orderId
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
            //获取门店列表
            GetUnitInfo: function (UnitID, callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Basic.Unit.GetUnitInfo',
                        UnitID: UnitID
                    },
                    success: function (data) {
                        debugger;
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (callback) {
                                callback(result);
                            }

                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            },
            saveDeliveryInfo: function (callback) {
                var that = this;
               
                $.util.oldAjax({
                    url: "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx",
                    data: {
                        action: 'SaveDeliveryInfo',
                        order_id: this.args.orderId,
                        UnitId:this.opertionField.purchase_unit_id,
                        ReceiveMan: this.opertionField.ReceiveMan,// 联系人
                        Addr:  this.opertionField.Addr,//上海市 上海市 长宁区 长宁路8号
                        Phone:  this.opertionField.Phone,//手机
                        PostCode:  this.opertionField.PostCode,
                        DeliveryType:  this.opertionField.DeliveryType, //配送方式 1 送货到家， 2是到店自提
                        Carrier_id:  this.opertionField.Carrier_id,//配送商id8551f2a988899d9425ebc27d2082cfeb
                        DeliveryCode:  this.opertionField.DeliveryCode,//配送单号2343242
                        SendTime:  this.opertionField.SendTime//配送时间
                    },
                    beforeSend: function () {
                        $.util.isLoading()

                    },
                    success: function (data) {
                        debugger;
                        if (data.success) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },

            //获取配送方式
            GetDeliveryType: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/DeliveryHandler.ashx",
                    data: {
                        action: 'GetDeliveryType'

                    },
                    success: function (data) {
                        debugger;
                        if (data) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
             //获取配送商
            GetLogisticsCompany: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"Order.Logistics.GetLogisticsCompany",
                        IsSystem:this.args.IsSystem,
                        LogisticsName:this.args.LogisticsName

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
            //获取商品配置的规格值。
            getSkuPropCfg: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/SkuPropCfgHandler.ashx",
                    data: {
                        action: 'sku_prop_cfg',
                        QueryStringData:{
                            orderId: this.args.orderId
                        }

                    },
                    success: function (data) {
                        debugger;
                        if (data.data) {
                            if (callback) {
                                callback(data.data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },

            //获取支付方式。
            GetDefrayType: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/DefrayTypeHandler.ashx",
                    data: {
                        action: 'GetDefrayType',
                        page:1,
                        start:0,
                        limit:25
                    },
                    success: function (data) {
                        debugger;
                        if (data) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
            //获取付款方式; 和获取审核不通过的理由
            GetOptionsByName: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/OptionsHandler.ashx",
                    data: {
                        action: 'GetOptionsByName',
                        QueryStringData:{
                            OptionName:this.args.OptionName,
                            isShowAll:false,
                            page:1,
                            start:0,
                            limit:25
                        }

                    },
//                   /* 0: {OptionValue: 1, OptionText: "汇款", OptionTextEn: null, PersistenceHandle: null}
//                    1: {OptionValue: 2, OptionText: "现金", OptionTextEn: null, PersistenceHandle: null}
//                    2: {OptionValue: 3, OptionText: "支票", OptionTextEn: null, PersistenceHandle: null}
//                    3: {OptionValue: 4, OptionText: "其它", OptionTextEn: null, PersistenceHandle: null}*/
                    success: function (data) {
                        debugger;
                        if (data) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },

            //下载配送单
            fnDownLoadDelivery: function () {
                var getUrl = '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=ExportDelivery';
                var form = $('<form>');
                form.attr('style', 'display:none;');
                form.attr('target', '');
                form.attr('method', 'post');
                form.attr('action', getUrl);
                var input1 = $('<input>');
                input1.attr('type', 'hidden');
                input1.attr('name', 'orderId');
                input1.attr('value', this.args.orderId);
                $('body').append(form);
                form.append(input1);
                form.submit();
                form.remove();
            },
            fnPrintPicking: function () {

                var url = "/Module/Order/Print/PrintPicking.aspx?orderID=" + this.args.orderId;
                window.open(url, "拣货打印");
            },
            fnPrintDelivery: function () {
                var url = '/Module/Order/Print/PrintDelivery.aspx?orderId=' + this.args.orderId;
                window.open(url, '配送单打印');
            },


            GetInoutStatusList: function (callback) {
                $.util.oldAjax({
                    url: "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx",
                    data: {
                        action: 'GetInoutStatusList',
                        order_id: this.args.orderId,
                        page:1,
                        start:0,
                        limit:65
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


            operation: function (pram, operationType, callback) {
                debugger;
                var prams = {data: {action: "UpdateStatus"}};
                prams.url = "/Module/Order/InoutOrders/Handler/Inout3Handler.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                $.each(pram,function(index,filed){
                    prams.data[filed.name]=filed.value;
                })


                  //完成700；

                prams.data["nextStatus"]=this.args.nextStatus;
                prams.data["order_id"]=this.args.orderId;

                $.util.oldAjax({
                    url: prams.url,
                    data: prams.data,
                    beforeSend: function () {
                        $.util.isLoading()

                    },
                    success: function (data) {
                        if (data.success) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.msg);
                        }
                    }
                });
            }
        }
    };
    page.init();
});

