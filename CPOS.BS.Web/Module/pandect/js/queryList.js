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
            that.elems.sectionPage.delegate(".btnList p","click",function(){
                that.showPanel($(this).data("type"))
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

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },


        //加载页面的数据请求
        loadPageData: function (e) {
            var that=this;
            var fileds=[{name:"DateCode",value:new Date().format("yyyy-MM-dd")}];
            that.loadData.operation(fileds,"",function(data){
              // 调试用
              // var data= {"ResultCode":0,"Message":"OK","IsSuccess":true,"Data":{"GoodsRankList":{"Top10Views":[{"ItemId":"DDF042D3222F422384AAB5CE96D22857","ItemName":" Ronshen/容声 RS-D1自动上水壶电热水壶套装烧水壶泡茶壶煮茶器 ","ItemSoldCount":10,"Rate":"13.16%"},{"ItemId":"1F825A1B92B94253934783F221B5B5DD","ItemName":"vans","ItemSoldCount":15,"Rate":"24.59%"},{"ItemId":"9719519321D64B68939AA9DFF83406DC","ItemName":"ada","ItemSoldCount":5,"Rate":"12.20%"},{"ItemId":"4614DCD4F214424B8C8720677FCDA7E4","ItemName":"韩国正品the saem 得鲜按钮口红MO6姨妈色秀智同款","ItemSoldCount":24,"Rate":"61.54%"},{"ItemId":"A7F4395E6077492C970C46553ECA1915","ItemName":"车厘子","ItemSoldCount":5,"Rate":"15.62%"},{"ItemId":"6A889469F1F147CC8F6553F56F83C773","ItemName":"xiaojunzi","ItemSoldCount":4,"Rate":"13.33%"},{"ItemId":"5F6786B6FF8E4371A6F140A43C974A1A","ItemName":"不锈钢水壶一号","ItemSoldCount":1,"Rate":"5%"},{"ItemId":"435FC4E3873F43549CB1A73D4E4D5E11","ItemName":"Yoice/优益 YC105全自动上水壶抽水电热水壶茶具套装烧水壶煮茶器","ItemSoldCount":1,"Rate":"4.35%"},{"ItemId":"96C6C68B5E8144FA860135F70ECBAE1E","ItemName":"家用整套装组合功夫茶具粗陶瓷礼盒茶杯子茶壶台湾复古办公室创意 ","ItemSoldCount":7,"Rate":"36.84%"},{"ItemId":"C02F859A9614425C917756FE1527B104","ItemName":"Grelide/格来德 WWK-4201S大容量304不锈钢电热水壶电热烧开水壶 ","ItemSoldCount":4,"Rate":"13.79%"}],"Least10Views":[{"ItemId":"6E718B4CE2CF47E09E29CF599A76DB24","ItemName":"fdsaf","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"B13881DE2DC043978C4B3150DAAD1B25","ItemName":"nike","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"342EC9925AFB40A981DC5554C32FA0F3","ItemName":"www","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"38EB2FE9721F426EA4B8D8C13E4E01F5","ItemName":"vans","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"94ABD1E4F9314FE2B7DA9BF0389014B0","ItemName":"bbb","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"8D296418F67645E6922487446D0E5143","ItemName":"2016年新茶 谢裕大黄山毛峰明前绿茶30g*2听装 春茶","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"98E07407CF804D98867197AD1C42653C","ItemName":"1111","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"26AFB9B317A44BECBEB417EB08BA1DCD","ItemName":"2016年新茶 谢裕大黄山毛峰明前嫩芽50g单听装","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"611B0868445847F1A81250F20ADC8BDE","ItemName":"2016新茶上市 西湖牌雨前龙井茶100克听装 杭州一级绿茶 茶叶 ","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"DF2957F3B81B49CCAB32B0C98BDAEF65","ItemName":"test001","ItemSoldCount":0,"Rate":"0%"}],"Top10Sales":[{"ItemId":"4614DCD4F214424B8C8720677FCDA7E4","ItemName":"韩国正品the saem 得鲜按钮口红MO6姨妈色秀智同款","ItemSoldCount":24,"Rate":"61.54%"},{"ItemId":"1F825A1B92B94253934783F221B5B5DD","ItemName":"vans","ItemSoldCount":15,"Rate":"24.59%"},{"ItemId":"FEFEB5AF6368473C99057C60E0701A0D","ItemName":"藤椅子茶几三件套庭院简约户外家具休闲椅阳台桌椅组合藤椅五件套 ","ItemSoldCount":11,"Rate":"73.33%"},{"ItemId":"DDF042D3222F422384AAB5CE96D22857","ItemName":" Ronshen/容声 RS-D1自动上水壶电热水壶套装烧水壶泡茶壶煮茶器 ","ItemSoldCount":10,"Rate":"13.16%"},{"ItemId":"96C6C68B5E8144FA860135F70ECBAE1E","ItemName":"家用整套装组合功夫茶具粗陶瓷礼盒茶杯子茶壶台湾复古办公室创意 ","ItemSoldCount":7,"Rate":"36.84%"},{"ItemId":"9719519321D64B68939AA9DFF83406DC","ItemName":"ada","ItemSoldCount":5,"Rate":"12.20%"},{"ItemId":"7548983B80534244919B415A12886D23","ItemName":"骨瓷水具套装杯壶陶瓷凉水冷水杯子套装耐热家用茶具套装茶杯饮具 ","ItemSoldCount":5,"Rate":"62.5%"},{"ItemId":"A7F4395E6077492C970C46553ECA1915","ItemName":"车厘子","ItemSoldCount":5,"Rate":"15.62%"},{"ItemId":"C02F859A9614425C917756FE1527B104","ItemName":"Grelide/格来德 WWK-4201S大容量304不锈钢电热水壶电热烧开水壶 ","ItemSoldCount":4,"Rate":"13.79%"},{"ItemId":"6A889469F1F147CC8F6553F56F83C773","ItemName":"xiaojunzi","ItemSoldCount":4,"Rate":"13.33%"}],"Least10Sales":[{"ItemId":"98E07407CF804D98867197AD1C42653C","ItemName":"1111","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"DF2957F3B81B49CCAB32B0C98BDAEF65","ItemName":"test001","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"B13881DE2DC043978C4B3150DAAD1B25","ItemName":"nike","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"38EB2FE9721F426EA4B8D8C13E4E01F5","ItemName":"vans","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"26AFB9B317A44BECBEB417EB08BA1DCD","ItemName":"2016年新茶 谢裕大黄山毛峰明前嫩芽50g单听装","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"611B0868445847F1A81250F20ADC8BDE","ItemName":"2016新茶上市 西湖牌雨前龙井茶100克听装 杭州一级绿茶 茶叶 ","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"8D296418F67645E6922487446D0E5143","ItemName":"2016年新茶 谢裕大黄山毛峰明前绿茶30g*2听装 春茶","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"6E718B4CE2CF47E09E29CF599A76DB24","ItemName":"fdsaf","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"F5F5CF84ADD34ADDBC071015CA921766","ItemName":"232","ItemSoldCount":0,"Rate":"0%"},{"ItemId":"94ABD1E4F9314FE2B7DA9BF0389014B0","ItemName":"bbb","ItemSoldCount":0,"Rate":"0%"}]},"Last30DaysTransform":{"Rate_OrderVipPayCount_UV":"13.30","Rate_OrderVipCount_UV":"41.38","Rate_OrderVipPayCount_OrderVipCount":"32.14","WxUV":203,"WxOrderVipCount":84,"WxOrderVipPayCount":27,"Rate_UV_Last":"00.00","Rate_OrderVipCount_Last":"200.00","Rate_OrderVipPayCount_Last":"200.00","WxPV":12505,"WxOrderCount":376,"WxOrderPayCount":52,"Rate_PV_Last":"00.00","Rate_OrderCount_Last":"276.00","Rate_OrderPayCount_Last":"188.89","WxOrderMoney":146706.15,"WxOrderPayMoney":12719.40,"WxOrderAVG":244.60,"Rate_OrderMoney_Last":"213.73","Rate_OrderPayMoney_Last":"-36.42","Rate_OrderAVG_Last":"-77.99"},"Last7DaysOperationData":{"WxUV":70,"OfflineUV":11,"WxOrderPayCount":18,"OfflineOrderPayCount":11,"WxOrderPayMoney":2230.30,"OfflineOrderPayMoney":1100.00,"WxOrderAVG":123.91,"OfflineOrderAVG":100.00}}}
              if(data.Data&&data.Data.Last7DaysOperationData){

                     var sevenInfo=data.Data.Last7DaysOperationData;

                     if(sevenInfo.OfflineUV+sevenInfo.WxUV) {
                         sevenInfo["UVPercent"] = sevenInfo.OfflineUV / (sevenInfo.OfflineUV + sevenInfo.WxUV) * 100;   //门店访客所占百分比比例
                     }else{
                         sevenInfo["UVPercent"]=0;
                         $(".ricePanel").eq(0).addClass("noData")
                     }
                   if(sevenInfo.OfflineOrderPayCount+sevenInfo.WxOrderPayCount) {
                       sevenInfo["PayCountPercent"] = sevenInfo.OfflineOrderPayCount / (sevenInfo.OfflineOrderPayCount + sevenInfo.WxOrderPayCount) * 100;   //门店成交单数比例

                   }else{
                       sevenInfo["PayCountPercent"]=0;
                       $(".ricePanel").eq(1).addClass("noData")
                   }
                    if(sevenInfo.OfflineOrderPayMoney+sevenInfo.WxOrderPayMoney) {
                        sevenInfo["PayMoneyPercent"] = sevenInfo.OfflineOrderPayMoney / (sevenInfo.OfflineOrderPayMoney + sevenInfo.WxOrderPayMoney) * 100;   //门店成交金额比例
                    }else{
                        sevenInfo["PayMoneyPercent"]=0;
                        $(".ricePanel").eq(2).addClass("noData")
                 }

                     if(Number(sevenInfo["WxOrderAVG"])==0&&Number(sevenInfo["OfflineOrderAVG"])==0){
                         $(".Rectangle_1").css({"width":"0px"});
                         $(".Rectangle_0").css({"width":"0px"}) ;
                         $(".Rectangle_0").parents(".textPanel").addClass("panelBg");
                     } else{
                         if( sevenInfo["WxOrderAVG"]<sevenInfo["OfflineOrderAVG"]){
                             $(".Rectangle_1").css({"width":"200px"});
                            var w= (sevenInfo["WxOrderAVG"]/sevenInfo["OfflineOrderAVG"])*200;
                             $(".Rectangle_0").css({width:w});
                         }else{
                             $(".Rectangle_0").css({"width":"200px"});

                             var w= (sevenInfo["OfflineOrderAVG"]/sevenInfo["WxOrderAVG"])*200;
                             $(".Rectangle_1").css({width:w});

                         }
                     }
                     $(".onePanelDiv").find('[data-filed]').each(function(){
                         var filed=$(this).data("filed");
                         if(sevenInfo[filed]){
                             $(this).data("value",sevenInfo[filed])
                         }else{
                             $(this).data("value",0);
                         }
                     });
                     var i= 0,j=180;
                     var t1 = setInterval(function() {
                         var isStop=true;
                         if(i==50){
                             j=180;
                         }
                         j=j+3.6;
                         // $(".minRice").html(i+"%");
                         $(".minRice").each(function(){
                             var value=Number($(this).data("value")); var dom=$(this).parent();
                             if(isNaN(value)){
                                 value=0;
                             }
                             if((value-1)>=i){
                                 //$(this).html((i+1)+"%");
                                 isStop=false;
                                 if(i>=50){
                                     dom.find(".pieL").css("-o-transform","rotate(" + j + "deg)");
                                     dom.find(".pieL").css("-moz-transform","rotate(" + j+ "deg)");
                                     dom.find(".pieL").css("-webkit-transform","rotate(" + j + "deg)");
                                 }else{
                                     dom.find(".pieR").css("-o-transform","rotate(" + j + "deg)");
                                     dom.find(".pieR").css("-moz-transform","rotate(" + j+ "deg)");
                                     dom.find(".pieR").css("-webkit-transform","rotate(" + j + "deg)");

                                 }

                             }
                         });
                         if(isStop){
                             clearInterval(t1);
                         }

                         i = i + 1;
                     },20);
                 }

                 if(data.Data&&data.Data.Last30DaysTransform){
                     var monthInfo=data.Data.Last30DaysTransform;
                     $(".twoPanelDiv").find('[data-filed]').each(function(){
                         var me=$(this), filed=me.data("filed");
                         if(monthInfo[filed]){
                             me.data("value",monthInfo[filed])
                         }else{
                             me.data("value",0);
                         }
                         if(me.is("i")){
                             me.removeClass("up");
                             me.removeClass("down");
                             if(Number(me.data("value"))>0){
                                me.addClass("up");
                             }else if(Number(me.data("value"))<0){
                                 me.addClass("down");
                             }
                             me.data("value",Math.abs(me.data("value"))+"%");
                         }
                     });
                 }

                $('[data-filed]').each(function(){
                           var me=$(this);
                    if(!me.hasClass("minRice")) {
                        if (me.data("separator")) {
                            me.html($.util.groupSeparator(me.data("value")));
                        } else {
                            me.html(me.data("value"));
                        }
                    }

                });

                if(data.Data&&data.Data.GoodsRankList){
                    var goodsRankList=data.Data.GoodsRankList;
                    $(".threePanelDiv").find('[data-filed]').each(function(){
                        var me=$(this), filed=me.data("filed");
                        that.renderTable(me,goodsRankList[filed])
                    });
                }


            });
        },

        //渲染tabel
        renderTable: function (dom,list) {
            debugger;
            if(!list){
               list=[]
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
                    {field : 'ItemSoldCount',title : '销售数量',width:30,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return "<em style='color:#999'>"+value+"</em>"
                        }
                    },
                    {field : 'Rate',title : '转化率',width:20,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return "<em style='color:#ff3333'>"+value+"</em>"
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
        //收款
        showPanel:function(type){
            debugger;
            var that=this;
            that.elems.optionType="pay";
            var top=$(document).scrollTop()+60;
            $("body").eq(0).css("overflow-y","hidden");

            if(type=="tsfks") {
                var left=$(window).width() - 800>0 ? ($(window).width() - 800)*0.5:80;
                $('#win').window({title: "收款", width: 750, height: 800, top: top, left: left});
            }else{
                var left=$(window).width() - 740>0 ? ($(window).width() - 740)*0.5:80;
                $('#win').window({title: "收款", width: 610, height: 740, top: top, left: left});
            }
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var templateName="tpl_"+type;

            var html=bd.template(templateName);
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);


            $('#win').window('open')
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

           operation:function(pram,operationType,callback){
               debugger;
               var prams={data:{action:"Report.MailReport.GetAllRank"}};
               prams.url="/ApplicationInterface/Gateway.ashx";
               //根据不同的操作 设置不懂请求路径和 方法
               $.each(pram, function(i, field) {
                   debugger;
                   if (field.value != "") {
                       prams.data[field.name] = field.value; //提交的参数
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
           }


        }

    };
    page.init();
});

