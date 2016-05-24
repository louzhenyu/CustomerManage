/**
 * Created by Administrator on 2016/4/20.
 */

define(['js/tempModel.js'],function(TEMP) {
    var salesPromotion = {
        dataMessage:$('#tableWrap .dataMessage'),//列表提示信息\
        pageIndex:0,
        pageSize:10,
        optionType:"",
        MappingId:"",//修改商品时候必传项
        optionEventId:"",
        eventType: "1",  //1是团购 2是抢购 3是热销
        skillPanel: $('[ data-interaction="2"] .skillPanel'),
        init: function (CTWEventId) {//团购 秒杀  热销的基础数据绘制。
            var fields=[],that=this;
            fields.push({name:"EventTypeId",value:that.eventType});
            fields.push({name:"PageIndex",value:0});
            fields.push({name:"PageSize",value:10});
            fields.push({name:"CTWEventId",value:CTWEventId});
            that.dataOption(fields,"getEvents",function(data){
                if(data.Data&&data.Data.PanicbuyingEventList&&data.Data.PanicbuyingEventList.length>0){
                    for(var i=0;i<data.Data.PanicbuyingEventList.length;i++){
                        var info=data.Data.PanicbuyingEventList[i];
                       var name= new Date(info.BeginTime).format("MM月dd日");
                        $("#eventList").append("<li data-id='" + info.EventId + "'>" + name + "<em class='editIconBtn'></em></li>");
                    }

                }
                 if($("#eventList li").length>=4){
                     $(".eventList .eventAddBtn").hide();
                 }
                $("#eventList li").eq(0).trigger("click");
            });

        },
        initEvent: function () {

            var that=this;
            that.dataOption("","selectCategory",function(data){
                var list=[{CategoryId:"-1",CategoryName:"全部",selected:true}];

                if( data.Data&&data.Data.ItemCategoryInfoList&&data.Data.ItemCategoryInfoList.length>0){
                    $.merge(list, data.Data.ItemCategoryInfoList);
                }
                $("#itemCategory").combobox({
                    valueField: 'CategoryId',
                    textField: 'CategoryName',
                    data:list
                });
            });
            var top = $(document).scrollTop()+60;
            $('#winProduct').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                top:top,
                width:820 ,
                height:620,
                title:"添加商品",
                onOpen:function(){
                    $("#winProduct").window("hcenter");
                },onClose:function(){
                    $("body").eq(0).css("overflow-y","auto");
                }
            });
            $('#winSales').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onOpen:function(){
                    $("#winSales").window("hcenter");
                },onClose:function(){
                    $("body").eq(0).css("overflow-y","auto");
                }
            });
            that.skillPanel.delegate(".eventList .editIconBtn", "click", function () {  //编辑活动

                that.optionEventId=$(this).parents("li").data("id");
                that.addEvent(true);
            }).delegate(".eventList .eventAddBtn", "click", function () { //添加活动
                that.addEvent();
            }).delegate(".eventList li", "click", function () {  //活动切换
                $(this).parent().find("li").removeClass("on");
                $(this).addClass("on");
                that.optionEventId=$(this).data("id");
                that.getItemList($(this));



            }).delegate(".product","mouseenter",function(){

                $("#ascrail2000").show();
            }).delegate(".product","mouseleave",function(){
                $("#ascrail2000").hide();
            }).delegate(".productAdd","click",function(){   //添加商品事件
                if( that.skillPanel.find(".eventList li").length==0){
                    alert("请先添加一个活动");
                    return false;
                }
                if( that.skillPanel.find(".eventList li.on").length==0){
                    alert("请选择一个活动");
                    return false;
                }
                  that.selectProduct();

            }).delegate("#eventItemList .editIconBtn","click",function(){  //编辑商品
                var itemDom=$(this).parents(".product");
                var rowData={
                       ItemId:itemDom.data("itemid"),
                       ItemName:itemDom.data("itemname"),
                       type:"editSKU"

                };
                that.MappingId=itemDom.data("id");
                   if(rowData.ItemId) {
                       that.selectSku(rowData);

                   } else{
                       console.info("活动商品列表绑定异常")
                   }
            }).delegate("#eventItemList .textBtn", "click",function(){
                var itemDom=$(this).parents(".product");
                var fields=[{name:"EventItemMappingId",value:itemDom.data("id")}];
                $.messager.confirm("提示","确定删除该商品？",function(r){
                    if(r) {
                        that.dataOption(fields, "delProduct", function () {

                            that.getItemList();

                        })
                    }
                });

            });
            $("#winProduct").delegate(".queryBtn","click",function(){
                var fields=  $('#search').serializeArray();
                that.pageIndex=0
                that.dataOption(fields,"selectProduct",function(data){
                    that.renderTable(data);
                });
            });

            $('#winSales').delegate(".saveBtn","click",function(){
                if(that.optionType=="addSKU"||that.optionType=="editSKU") {
                    var submitObj=that.saveEventSku();
                    if(submitObj.is){
                        that.dataOption(submitObj.fields,that.optionType,function(){
                            alert("添加成功");
                            $('#winSales').window('close');
                            that.getItemList();
                        })
                    }else{
                        $.messager.alert("提示",submitObj.msg);
                    }


                }else {
                    if ($('#eventInfo').form("validate")) {
                        var fields = $('#eventInfo').serializeArray();
                        that.dataOption(fields, that.optionType, function (data) {
                            alert("操作成功")
                            $('#winSales').window('close');
                            var str = "", time = "";
                            if (that.optionType == "addEvent") {

                                $("#eventList").append('<li data-id="'+data.Data.EventID+'"></li>');
                                if ($("#eventList").find("li").length >= 4) {
                                    $(".eventList .eventAddBtn").hide();
                                }
                               $("#eventList li:last").trigger("click");
                            } else if (that.optionType == "editEvent") {//编辑
                                $("#eventList").find("li").each(function () {
                                    if ($(this).data("id") == that.optionEventId) {
                                        $(this).trigger("click");
                                        return false;
                                    }

                                })

                            }

                        });
                    }

                }
            }).delegate(".checkBox","click",function(){
                $(this).toggleClass("on");
                if($(this).hasClass("on")){
                    $(this).parent().siblings().find(".easyui-numberbox").numberbox({
                        disabled:false
                        //required: false
                    });
                } else{
                    $(this).parent().siblings().find(".easyui-numberbox").numberbox({
                        disabled:true
                        //required: false
                    });
                }
            });

            template.helper("$ItemNameSub",function(str){
                if(str.length>25) {
                    return str.substring(0, 25) + '<em title="'+str+'" class="easyui-tooltip">...</em>'
                } else{
                    return str;
                }
            });
            $(".nicescroll-rails").hide();
            $('#eventItemList').niceScroll({
                cursorcolor: "#ccc",//#CC0071 光标颜色
                cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0
                touchbehavior:false, //使光标拖动滚动像在台式电脑触摸设备
                cursorwidth: "5px", //像素光标的宽度
                cursorborder: "1", // 	游标边框css定义
                cursorborderradius: "5px",//以像素为光标边界半径
                autohidemode: false, //是否隐藏滚动条
                horizrailenabled:true,//横向滚动条隐藏
                //background:"#00a0e8",  //滚动轨迹的背景，
                zindex:0
            });
           $("#ascrail2000-hr").remove()
        },
        //
        getItemList:function(dom){
            var that=this;
            $(".timekeeping").hide();
            $(".productList").hide();
            that.dataOption("","getEventItemList",function(data){
                if(data.Data) {
                    var html = that.render(TEMP.eventItemList, data.Data);
                    $("#eventItemList").html(html);
                }

                if(dom) {
                    dom.data("time",data.Data.DeadlineSecond);
                    dom.html(data.Data.BeginTime).append('<em class="editIconBtn"></em>');
                    that.outTime(dom,data.Data.TimeFlag);
                }
                $(".timekeeping").show(600);
                $(".productList").show(600);
            });
        },

        outTime:function(dom,type){
            var that=this;
            var time= dom.data("time");
            if(that.timer){
                clearInterval(that.timer);
            }
            var str="" ;
            //eventType: "1",  //1是团购 2是抢购 3是热销
            if(that.eventType==1){
                str="团购";
            }else if(that.eventType==2){
                str="抢购";
            }else if(that.eventType==3){
                str="热销";
            }
            if(type=="begin"){
                $("#timeInfo").html("距离"+str+"开始还剩");
            } else{
                $("#timeInfo").html("距离"+str+"结束还剩");
            }
            $(".time").each(function () {

                var me = $(this);
                var maxTime = parseInt(time);
                var isStart=me.data("start");
                that.timer = setInterval(function () {
                    if (maxTime >= 0) {
                        /*var hour = Math.floor(maxTime / 60/60);
                         var minutes = Math.floor(maxTime % (60*12));
                         var seconds = Math.floor(maxTime % 60);*/
                        var d = Math.floor(maxTime / 60 / 60 / 24);
                        var h = Math.floor(maxTime / 60 / 60);
                        var m = Math.floor(maxTime / 60 % 60);
                        var s = Math.floor(maxTime % 60);
                        html="";
                        if(h<10){
                            html+="<em>0</em><em>"+h+"</em>"
                        } else{
                            var hour= parseInt(h/10);
                            if(hour<10){
                                html+="<em>"+hour+"</em><em>"+h%10+"</em>"
                            }else{
                                html+="<em>"+parseInt(hour/10)+"</em><em>"+(hour%10)+"</em><em>"+(h%10)+"</em>"
                            }

                        }
                        html+="时";
                        if(m<10){
                            // m="0"+m;
                            html+="<em>0</em><em>"+m+"</em>"
                        }else{
                            html+="<em>"+parseInt(m/10)+"</em><em>"+m%10+"</em>"
                        }
                        html+="分" ;
                        if(s<10){
                            html+="<em>0</em><em>"+s+"</em>"
                        } else{
                            html+="<em>"+parseInt(s/10)+"</em><em>"+(s%10)+"</em>"
                        }
                        html+="秒";

                        me.html(html);

                        --maxTime;
                    }
                    else {
                        $("#timeInfo").html("活动已经结束");
                        clearInterval(that.timer);
                        me.html("<em>0</em><em>0</em>时<em>0</em><em>0</em>分<em>0</em><em>0</em>秒");
                    }
                }, 1000);
            });
        },

        selectProduct:function(){
            var that=this;
            //itemCategory

            var fields=  $('#search').serializeArray();
            that.dataOption(fields,"selectProduct",function(data){
                that.renderTable(data);
                $("#winProduct").window("open");
            });

        },
        selectSku:function(rowData){

            var that=this;
            debugger;
            var fields=[{name:"ItemId",value:rowData.ItemId}];
            var title = "设置商品";

             that.optionType="addSKU";

            var top = $(document).scrollTop()+20;
            $('#winSales').window({title: title, width: 820, height: 700, top: top});
            //改变弹框内容，调用百度模板显示不同内容
                that.dataOption(fields,"selectItemSku",function(data){
                    $('#SalesPanel').layout('remove', 'center');
                    if(data.Data&&data.Data.SkuList) {
                        data.Data["ItemName"]=rowData.ItemName;
                        data.Data["ItemId"]=rowData.ItemId;
                    var html = that.render(TEMP.skuItemList,data.Data);
                    var options = {
                        region: 'center',
                        content: html
                    };


                    $('#SalesPanel').layout('add', options);

                        $("#winProduct").window("close"); //关闭选择商品的，
                        $('#winSales').window('open');
                      //  $("body").eq(0).css("overflow-y","hidden");
                   $("#skuList .checkBox").each(function(){
                            if($(this).data("select")){
                                $(this).trigger("click");
                            }
                        });
                    }

                });






        },
        saveEventSku:function(){
            var fields=[],selectDom=$("#winSales .skuPanel");
            var submitObj={is:true,msg:"",fields:fields};
            fields.push({name:"ItemID",value:selectDom.data("id")});
           var SinglePurchaseQty= selectDom.find(".productInfo").find(".easyui-numberbox").numberbox("getValue");//限购数量
            if(SinglePurchaseQty>0) {
                fields.push({name: "SinglePurchaseQty", value: SinglePurchaseQty});
            }else{
                fields.push({name: "SinglePurchaseQty", value: 0});
            }
            var SkuList=[];
            if(selectDom.find("#skuList .checkBox.on").length>0) {
                selectDom.find("#skuList .checkBox.on").each(function () {
                   var me=$(this),index=me.parents(".skuObj").index()+ 1, domList=me.parent().siblings();
                    var obj={
                        MappingId:me.data("mappingid"),
                            SkuID:me.data("skuid"),
                        Qty:0,
                        KeepQty :0,
                        SoldQty:0,
                        SalesPrice:0,
                        price:domList.find(".skuPrice").val(),
                        index:index

                    };
                    domList.find(".easyui-numberbox").each(function(){
                        var name=$(this).attr("textboxname");
                        var number=parseInt($(this).numberbox("getValue"));
                        obj[name]=number;
                    });
                   SkuList.push(obj);
                });
                for(var i=0;i<SkuList.length;i++){

                    if(SkuList[i].Qty==0){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的商品库存不能为零";
                        break;
                    }
                    if(SkuList[i].KeepQty>SkuList[i].Qty){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的预设销量不能大于商品库存";
                        break;
                    }
                    if(SkuList[i].SalesPrice>SkuList[i].price){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的活动价不能大于原价";
                        break;
                    }
                    if(isNaN(SkuList[i].Qty)){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的商品库存不能为为空";
                        break;
                    }
                    if(isNaN(SkuList[i].KeepQty)){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的预设销量不能为空";
                        break;
                    }
                    if(isNaN(SkuList[i].SalesPrice)){
                        submitObj.is=false;
                        submitObj.msg="第"+SkuList[i].index+" 项的活动价不能为空";
                        break;
                    }


                }
                if(submitObj.is){
                    fields.push({name:"SkuList",value:SkuList});
                }else{
                    return submitObj;
                }

            }else{
                submitObj.is=false;
                submitObj.msg="请至少选择一个规格。";
                return submitObj;
            }
            submitObj.fields=fields;
            return submitObj ;
        },


        addEvent:function(rowData) {
            var that = this;
            debugger;

            var title = "",isOpen=true;
            if(rowData){
                title+="设置" ;
                that.optionType="editEvent" ;
            }else{
                title+="添加";
                that.optionType="addEvent";
            }
            if ( that.eventType == "1") {
                title += "团购";
            }else if( that.eventType== "2") {
                title += "抢购";

            }
            var top = $(document).scrollTop() + 100;
            $('#winSales').window({title: title, width: 550, height: 400, top: top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#SalesPanel').layout('remove', 'center');
            var html = that.render(TEMP.setEvent);
            var options = {
                region: 'center',
                content: html
            };

            $('#SalesPanel').layout('add', options);
            if (rowData) {
                isOpen=false;
                 that.dataOption("","getEvent",function(data){
                     $("#eventInfo").form("load", data.Data.PanicbuyingEvent);
                     $('#winSales').window('open');
                 })

            }
            if(isOpen){
                $('#winSales').window('open');
            }
            $('#BeginTime').datetimebox({
                onSelect: function (date) {
                    var time = new Date().format("hh:mm");
                    if (new Date().format("yyyy-MM-dd") == new Date(date).format("yyyy-MM-dd")) {

                        $(this).datetimebox("spinner").timespinner({min: time})
                    } else {
                        $(this).datetimebox("spinner").timespinner({min: '00:00'});

                    }
                    $(this).datetimebox("spinner").timespinner('setValue',time);
                }, onShowPanel: function () {
                    var selectDate = $(this).datetimebox("getValue");
                    var time = new Date().format("hh:mm");
                    if(selectDate){
                        if (new Date().format("yyyy-MM-dd") == new Date(selectDate).format("yyyy-MM-dd")) {

                            $(this).datetimebox("spinner").timespinner({min: time})
                        } else {
                            $(this).datetimebox("spinner").timespinner({min: '00:00'});
                            $(this).datetimebox("spinner").timespinner('setValue',time);
                        }
                    } else{
                        $(this).datetimebox("spinner").timespinner({min: time})
                        $(this).datetimebox("spinner").timespinner('setValue',time);
                    }

                }
            }).datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1 <= date;
                }
            });
            $('#EndTime').datetimebox({
                onSelect: function (date) {
                    var time = new Date().format("hh:mm");
                    if (new Date().format("yyyy-MM-dd") == new Date(date).format("yyyy-MM-dd")) {

                        $(this).datetimebox("spinner").timespinner({min: time})
                    } else {
                        $(this).datetimebox("spinner").timespinner({min: '00:00'});

                    }
                    $(this).datetimebox("spinner").timespinner('setValue',time);
                }, onShowPanel: function () {
                    var selectDate = $(this).datetimebox("getValue");
                    if(selectDate){
                        var time = new Date().format("hh:mm");
                        if (new Date().format("yyyy-MM-dd") == new Date(selectDate).format("yyyy-MM-dd")) {

                            $(this).datetimebox("spinner").timespinner({min: time})
                        } else {

                            $(this).datetimebox("spinner").timespinner({min: '00:00'});
                            $(this).datetimebox("spinner").timespinner('setValue',time);
                        }
                    } else{
                        $(this).datetimebox("spinner").timespinner({min: time});
                        $(this).datetimebox("spinner").timespinner('setValue',time);
                    }

                }
            }).datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1 <= date;
                }
            });
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        renderTable: function (data) {
            debugger;
            var that=this,list=[];
            if(data.Data&&data.Data.ItemInfoList&&data.Data.ItemInfoList.length>0){

                list=data.Data.ItemInfoList;
            }
            //jQuery easy datagrid  表格处理
           $("#productTable").datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                 //height : 460, //高度
                 width:783,
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:list,
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
                    {field : 'ItemCategoryName',title : '商品品类',width:60,align:'left',resizable:false} ,
                    {field : 'ItemName',title : '商品名称',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    }


                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    $("#productTable").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                          that.dataMessage.hide();
                    }else{
                        that.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                    debugger;
                    that.MappingId="";
                    that.selectSku(rowData);

                }

            });



            //分页
             // data.Data.TotalPage*that.pageSize  //总条数为返回
            kkpager.generPageHtml({
                pno: that.pageIndex ? that.pageIndex+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPage,
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

                    that.loadMoreData(n-1);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);

        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.pageIndex =currentPage;
            var fields=  $('#search').serializeArray();
            that.dataOption(fields,"selectProduct",function(data){
                that.renderTable(data)
            });
        },


        dataOption: function (pram, operationType, callback) {
             var that=this;
            var prams = {data: {action: ""}};
            var isSubMit=true;
            prams.url = "/ApplicationInterface/Events/EventsGateway.ashx";
            //根据不同的操作 设置不懂请求路径和 方法
            prams.data["IsCTW"] = "1";
            if(that.optionEventId) {
                prams.data["EventId"]=that.optionEventId;
            }
            prams.data["EventTypeId"] =that.eventType;
            $.each(pram, function (index, field) {
                if (field.value !== "") {
                    prams.data[field.name] = field.value;
                }
                if(field.name=='ItemCategoryID'&&field.value=="-1"){
                    prams.data[field.name] ="";
                }
            });
            switch (operationType) {
                case "addEvent":
                    if(prams.data.BeginTime>=prams.data.EndTime){
                        $.messager.alert("提示","结束时间要大于开始时间");
                        isSubMit=false;
                    }
                    prams.data.action="AddPanicbuyingEvent";
                    break;
                case "editEvent":
                    if(prams.data.BeginTime>=prams.data.EndTime){
                        $.messager.alert("提示","结束时间要大于开始时间");
                        isSubMit=false;
                    }
                    prams.data.action = "UpdatePanicbuyingEvent";
                    break; //新建
                case "getEvent":  //获取活动详情，用于编辑
                    prams.data.action="GetPanicbuyingEventDetails";

                    break;
                case "selectProduct": //商品查询
                    prams.data["PageIndex"]=that.pageIndex;
                    prams.data["PageSize"]=that.pageSize;
                    prams.data.action="GetItemList";
                    break;
                case "selectCategory":
                    prams.data.action="GetParentCategoryList";
                    break;
                case "selectItemSku" : //选取某个商品的活动sku
                    prams.data.action="GetItemSku";
                    break;
                case "addSKU": //活动关联sku
                        if(that.MappingId!=""){
                            prams.data["MappingId"]= that.MappingId;
                        }
                    prams.data.action="AddEventItemSku";
                    break;
                case "getEventItemList":
                     prams.data.action="GetEventMerchandise";
                    break;
                case "getEvents"://获取当前主题风格配置的团购抢购热销的活动列表
                    prams.data.action="GetPanicbuyingEvent";
                    break
                case "delProduct":
                    prams.data.action="RemoveEventItem";
                    break;
            }
              if(isSubMit) {
                  $.util.isLoading();
                  $.util.ajax({
                      url: prams.url,
                      data: prams.data,
                      success: function (data) {
                          if (data.IsSuccess && data.ResultCode == 0) {
                              if (callback) {
                                  callback(data);
                              }

                          } else {
                              if (operationType == "add") {
                                  console.info(data.IsSuccess + "code" + data.ResultCode);
                                  $.messager.alert("提示", "系统繁忙，请重新提交");
                              } else {
                                  $.messager.alert("提示", data.Message);
                              }

                          }
                      }
                  });
              }
        }


    };
    return salesPromotion;
});