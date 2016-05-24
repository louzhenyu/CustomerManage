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
                 if(data.Data&&data.Data.Last7DaysOperationData){

                     var sevenInfo=data.Data.Last7DaysOperationData;

                     if(sevenInfo.OfflineUV+sevenInfo.WxUV) {
                         sevenInfo["UVPercent"] = sevenInfo.OfflineUV / (sevenInfo.OfflineUV + sevenInfo.WxUV) * 100;   //门店访客所占百分比比例
                     }else{
                         sevenInfo["UVPercent"]=100
                     }
                   if(sevenInfo.OfflineOrderPayCount+sevenInfo.WxOrderPayCount) {
                       sevenInfo["PayCountPercent"] = sevenInfo.OfflineOrderPayCount / (sevenInfo.OfflineOrderPayCount + sevenInfo.WxOrderPayCount) * 100;   //门店成交单数比例

                   }else{
                       sevenInfo["PayCountPercent"]=100
                   }
                    if(sevenInfo.OfflineOrderPayMoney+sevenInfo.WxOrderPayMoney) {
                        sevenInfo["PayMoneyPercent"] = sevenInfo.OfflineOrderPayMoney / (sevenInfo.OfflineOrderPayMoney + sevenInfo.WxOrderPayMoney) * 100;   //门店成交金额比例
                    }else{
                        sevenInfo["PayMoneyPercent"]=100;
                    }

                     if(Number(sevenInfo["WxOrderAVG"])==0&&Number(sevenInfo["OfflineOrderAVG"])==0){
                         $(".Rectangle_1").css({"width":"0px"});
                         $(".Rectangle_0").css({"width":"0px"}) ;
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
                     $(".one").find('[data-filed]').each(function(){
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
                     $(".two").find('[data-filed]').each(function(){
                         var me=$(this), filed=me.data("filed");
                         if(monthInfo[filed]){
                             me.data("value",monthInfo[filed])
                         }else{
                             me.data("value",0);
                         }
                         if(me.is("i")){
                             me.removeClass("up");
                             me.removeClass("down");
                             if(Number(me.data("value"))>=0){
                                me.addClass("up");
                             }else{
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
                    $(".three").find('[data-filed]').each(function(){
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

