define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            operation:$('.optionBtns'),
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
                that.loadData.getMessageTemplateList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){
                var mid = $.util.getUrlParam("mid");
               location.href="/module/messageTemplate/Addmeass.aspx?mid=" + mid ;
                //location.href = "refundDetail.aspx?RefundID=" + rowData.RefundID + ;
            });

            /**************** -------------------弹出easyui 控件 start****************/


            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".fontC","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.loadData.args.TemplateID=row.TemplateID;
                switch (optType){
                    case 'del':
                        $.messager.confirm("提示","确认删除该条记录将不再显示",function(r){
                            if(r) {
                                that.loadData.DelMessageTemplate(function () {
                                    alert("操作成功");
                                    that.loadPageData()
                                })
                            }
                        });
                        break;
                    case 'exit':
                        location.href="/module/messageTemplate/Addmeass.aspx?mid=" + $.util.getUrlParam("mid")+"&TemplateID="+row.TemplateID;
                        break;
                }


            })
            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

            var that=this;
            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');

            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="-1"?"":filed.value;
                that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
           // that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');

            that.loadData.getMessageTemplateList(function(data){
                //写死的数据
                //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                //渲染table

                that.renderTable(data);


            });
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            that.elems.click=true;
            if(!data.Data.MessageTemplateInfoList){

                data.Data.MessageTemplateInfoList=[];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.MessageTemplateInfoList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                /*  pageNumber:1,*/
               /* frozenColumns:[[
                    {
                        field : 'ck',
                        checkbox : true
                    }
                ]],*/
                columns : [[

                    {field : 'Title',title : ' 标题',width:105,align:'left',resizable:false},
                    {field : 'Content',title : '内容',width:180,align:'left',resizable:false},
                    {field : 'LastUpdateTime',title : '更新日期',width:100,align:'left',resizable:false},
                    {field : 'brand',title : '编辑',width:36,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC exit" data-index="'+index+'" data-oprtype="exit"></p>';
                        }
                    },
                    {field : 'TemplateID',title : '删除',width:36,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC delete" data-index="'+index+'" data-oprtype="del"></p>';
                        }
                    }
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.html("数据没有对应记录");
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                      debugger;
                     if(that.elems.click) {
                         that.elems.click = true;
                         debugger;


                         location.href="/module/messageTemplate/Addmeass.aspx?mid=" + $.util.getUrlParam("mid")+"&TemplateID="+rowData.TemplateID;

                     }

                },onClickCell:function(rowIndex, field, value){
                      if(field=="TemplateID"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                     that.elems.click=false;
                     }else{
                     that.elems.click=true;
                     }
                }

            });

            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex=currentPage;
            that.loadData.getMessageTemplateList(function(data){
                that.renderTable(data);
            });
        },


        //取消订单
        cancelOrder:function(data){
            var that=this;
            that.elems.optionType="cancel";
            $('#win').window({title:"取消订单",width:360,height:260});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            this.loadData.tag.orderID=data.OrderID;
            $('#win').window('open');
        },
        loadData: {
            args: {
                PageIndex:1,
                PageSize:10,
                SearchColumns:{},    //查询的动态表单配置
                GroupNewsId:"",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'
            },
            opertionField:{},

            getMessageTemplateList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                      data:{
                          action:'Basic.MessageTemplate.GetMessageTemplateList',

                          /*PageSize:this.args.PageSize,
                          PageIndex:this.args.PageIndex*/
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
            DelMessageTemplate: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Basic.MessageTemplate.DelMessageTemplate',

                        TemplateID:this.args.TemplateID
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

