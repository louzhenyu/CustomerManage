define(['jquery', 'tools','easyui', 'kkpager', 'artDialog'], function ($) {
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
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
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
           //


            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getLEventTemplateList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){

                $.util.toNewUrlPath("creative.aspx");

            });



            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
           /* 10=待上架   20=待发布   30=已发布   40=已下架）*/
            $('#item_status').combobox({
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":10,
                    "text":"待上架",

                },{
                    "id":20,
                    "text":"待发布"

                } ,{
                    "id":30,
                    "text":"已发布"

                },{
                    "id":40,
                    "text":"已下架"

                },
                    {
                    "id":"0",
                    "text":"全部",
                    "selected":true
                }]
            });

            that.loadData.getMarketingTypeList(function(data) {
                if(data.Data&&data.Data.SysMarketingGroupTypeDropDownList&&data.Data.SysMarketingGroupTypeDropDownList.length>0) {

                    data.Data.SysMarketingGroupTypeDropDownList.push( {
                        "ActivityGroupId":"-1",
                        "Name":"请选择",
                        "selected":true
                    });
                    $('#MarketingType').combobox({
                        panelHeight:that.elems.panlH,
                        valueField: 'ActivityGroupId',
                        textField: 'Name',
                        data: data.Data.SysMarketingGroupTypeDropDownList
                    });
                }
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

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="exit"){
                    $.util.toNewUrlPath("creative.aspx?TemplateId="+row.TemplateId)
                } else {
                    that.loadData.operation(row, optType, function (data) {
                        alert("操作成功");
                        that.loadPageData(e);
                    });
                }


            }) ;
            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="-1"?"":filed.value;
                that.loadData.args.SearchColumns[filed.name]=filed.value;

            });





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
            if(!data.Data.LEventTemplateList){

                data.Data.LEventTemplateList=[];
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
                data:data.Data.LEventTemplateList,
                columns : [[
                    {field : 'TemplateName',title : '主题名称',width:125,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'ActivityGroupName',title : '主题类型',width:60,align:'center',resizable:false} ,
                    {field : 'TemplateStatus',title : '主题状态',width:60,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           switch(value){
                               case  10: value="待上架"; break;
                               case  20: value="待发布";  break;
                               case  30: value="已发布";  break;
                               case  40: value="已下架";  break;

                           }
                            return value;
                        }
                    } ,
                    {field : 'DisplayIndex',title : '显示顺序',width:80,resizable:false,align:'center'},
                    {field: 'TemplateId', title: '操作', width: 200, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            var str = "<div class='operation'>";
                            switch (row.TemplateStatus){
                                case 10:
                                case 40:
                                    str+= "<div data-index=" + index + " data-flag='putAway' class='putAway  opt' title='上架'> </div>";break;
                                case 20:
                                case 30:
                                    str+= "<div data-index=" + index + " data-flag='soldOut' class='soldOut  opt' title='下架'> </div>";break;
                            }


                            str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                            str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
                            return str
                        }
                    }

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
                  /*    debugger;
                     if(that.elems.click){
                     that.elems.click = true;
                     debugger;

                     //var mid = JITMethod.getUrlParam("mid");
                    var url = "commodityExit.aspx?Item_Id=" + rowData.Item_Id;
                         $.util.toNewUrlPath(url);
                     }*/

                },onClickCell:function(rowIndex, field, value){
                    if (field == "ck" || field == "Item_Id") {    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                     that.elems.click=false;
                     }else{
                     that.elems.click=true;
                     }
                }

            });



            //分页

            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPageCount,
                totalRecords:data.Data.TotalCount,
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
            this.loadData.args.PageIndex = currentPage-1;
            that.loadData.getLEventTemplateList(function(data){
                that.renderTable(data);
            });
        },



        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 0,
                PageSize: 10,
                SearchColumns:{

                },    //查询的动态表单配置
            },
            tag:{
                VipId:"",
                orderID:''
            },
            opertionField:{},

            getLEventTemplateList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                      data:{
                          action:'GetLEventTemplateList',
                          TemplateName:this.args.SearchColumns.TemplateName,
                          ActivityGroupId:this.args.SearchColumns.ActivityGroupId,
                          TemplateStatus:this.args.SearchColumns.TemplateStatus ,
                          PageIndex:this.args.PageIndex,
                          PageSize:this.args.PageSize
                      },
                      success: function (data) {
                          debugger;
                          if (data.IsSuccess&&data.ResultCode==0) {
                              if (callback)
                                  callback(data);
                          }else{
                              alert(data.Message)
                          }
                    }
                });
            },
            getMarketingTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                       action:"GetSysMarketingGroupTypeList"
                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }


                    }
                });
            },
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.TemplateId=pram["TemplateId"];

                switch(operationType){
                    case "putAway":prams.data.action="SetLEventTemplateStatus";  //上架
                        prams.data.TemplateStatus="20";
                        break;
                    case "soldOut":prams.data.action="SetLEventTemplateStatus";  //下架
                        prams.data.TemplateStatus="40";
                        break;
                    case "delete":prams.data.action="DeleteLEventTemplate";  //删除
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

