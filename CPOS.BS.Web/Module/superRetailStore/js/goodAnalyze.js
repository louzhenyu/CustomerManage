define(['jquery', 'tools','easyui', 'artDialog','highcharts'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section")
        },
        init: function () {
            var that=this;
            that.initEvent();
            that.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            //

            that.elems.sectionPage.delegate(".orderList","click", function (e) {
                var top=$(document).scrollTop()+160;
                var left=$(window).width() - 442>0 ? ($(window).width() - 442)*0.5:80;
                $('#win').window({title:"提示",width:518,height:418,top:top,left:left});
                $('#win').window("open");
            });
            /**************** -------------------弹出easyui 控件 start****************/


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
                onClose:function(){
                    that.loadData.getSuperRetailTraderItemList(function(data){
                        that.renderTable(data);
                    });
                }
            });
            $('#panlconent').layout({
                fit:true
            });
         /*   $('#win').delegate(".saveBtn","click",function(e){
                    var treeList=$("#unitTreeSelect").tree("options").data;
                if (treeList.length>0) {
                    var list=[];
                    for(var i=0;i<treeList.length;i++){
                        if(treeList[i].children&&treeList[i].children.length>0){
                             for(var j=0;j<treeList[i].children.length;j++){
                                 var node= treeList[i].children[j];
                                 list.push({ItemId:node.ItemId,SkuId:node.SkuId})
                             }
                        } else{
                            list.push({ItemId:treeList[i].ItemId,SkuId:treeList[i].SkuId})
                        }
                    }
                     var fields=[{name:"ItemIdList",value:list}];
                    that.loadData.operation(fields,"setSuper",function(data){

                        alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);

                    });
                }else{
                    $.messager.alert("提示","请至少选择一个商品");
                }
            });*/

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
            // var fileds=[{name:"DateCode",value:new Date().format("yyyy-MM-dd")}];
            that.loadData.operation("", "", function (data) {
                // 调试用

                // var data= {"ResultCode":0,"Message":"OK","IsSuccess":true,"Data":{"GoodsRankList":{"Top10Views":[{"ItemId":"DDF042D3222F422384AAB5CE96D22857","ItemName":" Ronshen/容声 RS-D1自动上水壶电热水壶套装烧水壶泡茶壶煮茶器 ","ItemSoldCount":10,"Rate":"13.16%"},{"ItemId":"1F825A1B92B94253934783F221B5B5DD","ItemName":"vans","ItemSoldCount":15,"Rate":"24.59%"},{"ItemId":"9719519321D64B68939AA9DFF83406DC","ItemName":"ada","ItemSoldCount":5,"Rate":"12.20%"},{"ItemId":"4614DCD4F214424B8C8720677FCDA7E4","ItemName":"韩国正品the saem 得鲜按钮口红MO6姨妈色秀智同款","ItemSoldCount":24,"Rate":"61.54%"},{"ItemId":"A7F4395E6077492C970C46553ECA1915","ItemName":"车厘子","ItemSoldCount":5,"Rate":"15.62%"},{"ItemId":"6A889469F1F147CC8F6553F56F83C773","ItemName":"xiaojunzi","ItemSoldCount":4,"Rate":"13.33%"},{"ItemId":"5F6786B6FF8E4371A6F140A43C974A1A","ItemName":"不锈钢水壶一号","ItemSoldCount":1,"Rate":"5%"},{"ItemId":"435FC4E3873F43549CB1A73D4E4D5E11","ItemName":"Yoice/优益 YC105全自动上水壶抽水电热水壶茶具套装烧水壶煮茶器","ItemSoldCount":1,"Rate":"4.35%"},{"ItemId":"96C6C68B5E8144FA860135F70ECBAE1E","ItemName":"家用整套装组合功夫茶具粗陶瓷礼盒茶杯子茶壶台湾复古办公室创意 ","ItemSoldCount":7,"Rate":"36.84%"},{"ItemId":"C02F859A9614425C917756FE1527B104","ItemName":"Grelide/格来德 WWK-4201S大容量304不锈钢电热水壶电热烧开水壶 ","ItemSoldCount":4,"Rate":"13.79%"}],"Least10Views":[{"ItemId":"6E718B4CE2CF47E09E29CF599A76DB24","ItemName":"fdsaf","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"B13881DE2DC043978C4B3150DAAD1B25","ItemName":"nike","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"342EC9925AFB40A981DC5554C32FA0F3","ItemName":"www","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"38EB2FE9721F426EA4B8D8C13E4E01F5","ItemName":"vans","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"94ABD1E4F9314FE2B7DA9BF0389014B0","ItemName":"bbb","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"8D296418F67645E6922487446D0E5143","ItemName":"2016年新茶 谢裕大黄山毛峰明前绿茶30g*2听装 春茶","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"98E07407CF804D98867197AD1C42653C","ItemName":"1111","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"26AFB9B317A44BECBEB417EB08BA1DCD","ItemName":"2016年新茶 谢裕大黄山毛峰明前嫩芽50g单听装","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"611B0868445847F1A81250F20ADC8BDE","ItemName":"2016新茶上市 西湖牌雨前龙井茶100克听装 杭州一级绿茶 茶叶 ","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"DF2957F3B81B49CCAB32B0C98BDAEF65","ItemName":"test001","ItemSoldCount":0,"Rate":"0%"}],"Top10Sales":[{"ItemId":"4614DCD4F214424B8C8720677FCDA7E4","ItemName":"韩国正品the saem 得鲜按钮口红MO6姨妈色秀智同款","ItemSoldCount":24,"Rate":"61.54%"},{"ItemId":"1F825A1B92B94253934783F221B5B5DD","ItemName":"vans","ItemSoldCount":15,"Rate":"24.59%"},{"ItemId":"FEFEB5AF6368473C99057C60E0701A0D","ItemName":"藤椅子茶几三件套庭院简约户外家具休闲椅阳台桌椅组合藤椅五件套 ","ItemSoldCount":11,"Rate":"73.33%"},{"ItemId":"DDF042D3222F422384AAB5CE96D22857","ItemName":" Ronshen/容声 RS-D1自动上水壶电热水壶套装烧水壶泡茶壶煮茶器 ","ItemSoldCount":10,"Rate":"13.16%"},{"ItemId":"96C6C68B5E8144FA860135F70ECBAE1E","ItemName":"家用整套装组合功夫茶具粗陶瓷礼盒茶杯子茶壶台湾复古办公室创意 ","ItemSoldCount":7,"Rate":"36.84%"},{"ItemId":"9719519321D64B68939AA9DFF83406DC","ItemName":"ada","ItemSoldCount":5,"Rate":"12.20%"},{"ItemId":"7548983B80534244919B415A12886D23","ItemName":"骨瓷水具套装杯壶陶瓷凉水冷水杯子套装耐热家用茶具套装茶杯饮具 ","ItemSoldCount":5,"Rate":"62.5%"},{"ItemId":"A7F4395E6077492C970C46553ECA1915","ItemName":"车厘子","ItemSoldCount":5,"Rate":"15.62%"},{"ItemId":"C02F859A9614425C917756FE1527B104","ItemName":"Grelide/格来德 WWK-4201S大容量304不锈钢电热水壶电热烧开水壶 ","ItemSoldCount":4,"Rate":"13.79%"},{"ItemId":"6A889469F1F147CC8F6553F56F83C773","ItemName":"xiaojunzi","ItemSoldCount":4,"Rate":"13.33%"}],"Least10Sales":[{"ItemId":"98E07407CF804D98867197AD1C42653C","ItemName":"1111","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"DF2957F3B81B49CCAB32B0C98BDAEF65","ItemName":"test001","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"B13881DE2DC043978C4B3150DAAD1B25","ItemName":"nike","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"38EB2FE9721F426EA4B8D8C13E4E01F5","ItemName":"vans","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"26AFB9B317A44BECBEB417EB08BA1DCD","ItemName":"2016年新茶 谢裕大黄山毛峰明前嫩芽50g单听装","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"611B0868445847F1A81250F20ADC8BDE","ItemName":"2016新茶上市 西湖牌雨前龙井茶100克听装 杭州一级绿茶 茶叶 ","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"8D296418F67645E6922487446D0E5143","ItemName":"2016年新茶 谢裕大黄山毛峰明前绿茶30g*2听装 春茶","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"6E718B4CE2CF47E09E29CF599A76DB24","ItemName":"fdsaf","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"F5F5CF84ADD34ADDBC071015CA921766","ItemName":"232","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"94ABD1E4F9314FE2B7DA9BF0389014B0","ItemName":"bbb","ItemSoldCount":0,"Rate":"0%"}]},"Last30DaysTransform":{"Rate_OrderVipPayCount_UV":"13.30","Rate_OrderVipCount_UV":"41.38","Rate_OrderVipPayCount_OrderVipCount":"32.14","WxUV":203,"WxOrderVipCount":84,"WxOrderVipPayCount":27,"Rate_UV_Last":"00.00","Rate_OrderVipCount_Last":"200.00","Rate_OrderVipPayCount_Last":"200.00","WxPV":12505,"WxOrderCount":376,"WxOrderPayCount":52,"Rate_PV_Last":"00.00","Rate_OrderCount_Last":"276.00","Rate_OrderPayCount_Last":"188.89","WxOrderMoney":146706.15,"WxOrderPayMoney":12719.40,"WxOrderAVG":244.60,"Rate_OrderMoney_Last":"213.73","Rate_OrderPayMoney_Last":"-36.42","Rate_OrderAVG_Last":"-77.99"},"Last7DaysOperationData":{"WxUV":70,"OfflineUV":11,"WxOrderPayCount":18,"OfflineOrderPayCount":11,"WxOrderPayMoney":2230.30,"OfflineOrderPayMoney":1100.00,"WxOrderAVG":123.91,"OfflineOrderAVG":100.00}}}
                if (data.Data) {

                    /*   data.Data={
                           "ResultCode": 0,
                           "Message": "OK",
                           "IsSuccess": true,
                           "Data": {
                               "SharedRTProduct": null,
                               "SalesRTProduct": null,
                               "RTProductCRate": null,
                               "ShareMoreItemsList": null,
                               "SalesMoreItemesList":null,
                               "ShareLessItemsList": null,
                               "SalesLessItemesList": null
                           }
                       };*/
                    if(data.Data.SharedRTProduct){
                          $(".rice.bg1").addClass("riceData")
                    }
                    if(data.Data.SalesRTProduct){
                        $(".rice.bg2").addClass("riceData");
                    }
                    if(data.Data.RTProductCRate){
                          $("#RTProductCRate").hide();
                    }


                    if (data.Data.SalesRTProduct) {
                        data.Data.SalesRTProduct["count"] = data.Data.SalesRTProduct["Day30F2FSalesRTProductCount"] + data.Data.SalesRTProduct["Day30ShareSalesRTProductCount"];
                        $('#container1').highcharts({
                            chart: {
                                plotBackgroundColor: null,
                                plotBorderWidth: null,
                                backgroundColor: "transparent",
                                plotShadow: false,
                                reflow: false,
                                type: 'pie',
                                height:252,
                                width:352
                            },
                            credits: {
                                enabled: false,
                            },
                            title: {
                                text: ''
                            },
                            colors: [
                                '#ff8a00',//第一个颜色，欢迎加入Highcharts学习交流群294191384
                                '#ffe29c',//第二个颜色
                            ],
                            tooltip: {
                                enabled: false,
                            },
                            plotOptions: {
                                pie: {
                                    size:160,
                                    allowPointSelect: false,
                                    cursor: 'pointer',
                                    innerSize:'3%',
                                    dataLabels: {
                                        enabled: true,
                                        color: '#999',
                                        connectorColor: '#999',
                                        /* format:'<b>{point.name}</b>: {point.percentage} %'*/
                                        formatter: function (data) {
                                            debugger;
                                            var str=this.y ? this.y:"0";
                                            if(this.y>10000){
                                                str=(this.y/10000)+"万";
                                            }
                                            return str
                                        }
                                        /*format: '<b>{point.name}</b>: {point.percentage:.1f} %'*/
                                    }
                                }
                            },

                            series: [{
                                type: 'pie',
                                innerSize: '99.999%',
                                data: [

                                    ['线下已销售商品', data.Data.SalesRTProduct.Day30F2FSalesRTProductCount],
                                    ['线上已销售商品', data.Data.SalesRTProduct.Day30ShareSalesRTProductCount]

                                ]
                            }]
                        });

                        $('#container').highcharts({
                            chart: {
                                plotBackgroundColor: null,
                                plotBorderWidth: null,
                                backgroundColor: "transparent",
                                plotShadow: false,
                                reflow: false,
                                type: 'pie',
                                height:252,
                                width:352
                            },
                            credits: {
                                enabled: false,
                            },
                            title: {
                                text: ''
                            },
                            colors: [
                                '#01a1ff',//第一个颜色，欢迎加入Highcharts学习交流群294191384
                                '#d9f1fc',//第二个颜色
                            ],
                            tooltip: {
                                enabled: false,
                            },
                            plotOptions: {
                                pie: {
                                    size:160,
                                    innerSize:'3%',
                                    allowPointSelect: false,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: true,
                                        color: '#999',
                                        connectorColor: '#999',
                                        /* format:'<b>{point.name}</b>: {point.percentage} %'*/
                                        formatter: function (data) {
                                            var str=this.y ? this.y:"0";
                                            if(this.y>10000){
                                                str=(this.y/10000)+"万";
                                            }
                                            return str
                                        }
                                        /*format: '<b>{point.name}</b>: {point.percentage:.1f} %'*/
                                    }
                                }
                            },
                            series: [{
                                type: 'pie',
                                innerSize: '99.999%',
                                data: [
                                    ['未分享商品', data.Data.SharedRTProduct.Day30NoSharedRTProductCount],
                                    ['已分享商品', data.Data.SharedRTProduct.Day30SharedRTProductCount]

                                ]
                            }]
                        });


                    }
                    $(".onePanelDiv").find('[data-filed]').each(function () {
                        var filed = $(this).data("filed").split(".");
                        var value = "";
                        switch (filed.length) {
                            case 1:
                                value = data.Data[filed[0]];
                                break;
                            case 2:
                                if(data.Data[filed[0]]) {
                                    value = data.Data[filed[0]][filed[1]];
                                }
                                break;
                            case 3:
                                if(data.Data[filed[0]]&&data.Data[filed[0]][filed[1]]) {
                                    value = data.Data[filed[0]][filed[1]][filed[2]];
                                }
                                break;
                        }
                        if (value) {
                            $(this).data("value", value)
                        } else {
                            $(this).data("value", 0);
                        }
                    });
                    $('[data-filed]').each(function () {
                        var me = $(this);
                        if(me.data("value")==0){
                            debugger;
                            me.siblings(".hidden").hide()
                        }else{
                            me.siblings(".hidden").show()
                        }

                        if (me.data("separator")) {
                            me.html($.util.groupSeparator(me.data("value")));
                        } else {

                            if (me.parent().hasClass("panelCol")) { //柱状图处理
                                var height = me.data("value") * 220 / 100;
                                me.css({"height": height});
                                me.html(me.data("value") + "%");
                            } else {
                                me.html(me.data("value"));
                            }

                        }
                    });


                    var goodsRankList = data.Data;
                    $(".threePanelDiv").find('[data-filed]').each(function () {
                        var me = $(this), filed = me.data("filed"),show=me.data("show");
                        that.renderTable(me, goodsRankList[filed],show);
                    });


                    var html = "";
                    //data.Data.DateCode="2016-5-8";
/*                    var day27=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 27)).format("MM.dd",true),
                        day21=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 21)).format("MM.dd",true),
                        day20=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 20)).format("MM.dd",true),
                        day13=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 13)).format("MM.dd",true),
                        day14=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 14)).format("MM.dd",true),
                        day6=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 6)).format("MM.dd",true),
                        day7=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 7)).format("MM.dd",true),
                        day0=new Date(data.Data.DateCode).format("MM.dd",true);*/
                    var day27=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 28)).format("MM.dd",true),
                        day21=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 22)).format("MM.dd",true),
                        day20=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 21)).format("MM.dd",true),
                        day13=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() -14)).format("MM.dd",true),
                        day14=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 15)).format("MM.dd",true),
                        day6=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 7)).format("MM.dd",true),
                        day7=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 8)).format("MM.dd",true),
                        day0=new Date(new Date(data.Data.DateCode).setDate(new Date(data.Data.DateCode).getDate() - 1)).format("MM.dd",true);
                  debugger;
                    html += "<em>"+day27+"~"+day21+"</em>";
                    html += "<em>"+day20+"~"+day14+"</em>";
                    html += "<em>"+day13+"~"+day7+"</em>";
                    html += "<em>"+day6+"~"+day0+"</em>";
                    $("#dataDay").html(html);
                }


            });
        },

        //渲染tabel
        renderTable: function (dom,list,isShow) {
            if(!(list&&list.length>0)){
               return
            }
            dom.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:list,
                sortName : 'ItemId', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'ItemId', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {field : 'ItemName',title : '商品名称',width:60,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=8;
                            if(value&&value.length>long){

                                return '<div class="rowText easyui-tooltip" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }


                    },
                    {field : 'ShareCount',title : '分享次数',width:30,align:'left',resizable:false,hidden:isShow ,
                        formatter:function(value ,row,index){
                            return "<em style='color:#999'>"+value+"</em>"
                        }
                    },
                    {field : 'OrderCount',title : '销售数量',width:30,align:'left',resizable:false, hidden:!isShow ,
                        formatter:function(value ,row,index){
                            return "<em style='color:#999'>"+value+"</em>"
                        }
                    },
                    {field : 'CRate',title : '转化率',width:20,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return "<em style='color:#ff3333'>"+value+"%</em>" ;
                        }

                    }


                ]],

                onLoadSuccess : function() {
                    dom.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                },
                onClickRow:function(rowindex,rowData){


                },onClickCell:function(rowIndex, field, value){

                }

            });




        },




        loadData: {
            args: {
            },

            opertionField:{},

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"SuperRetailTrader.RTAboutReport.GetInfoAboutItems"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                $.each(pram, function (i, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                });
                switch(operationType){
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

