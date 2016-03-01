define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom

            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            optionBtn: $('.optionBtn'),
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
                //查询数据
                that.loadData.getActivityList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.optionBtn.delegate(".commonBtn", "click", function (e) {
                var mid = JITMethod.getUrlParam("mid");
                location.href = "activityDetail.aspx?mid=" + mid;
            });
            /**************** -------------------初始化easyui 控件 start****************/
            var  wd=160,H=32;
                $("#StatusList").combobox({
                    width:wd,
                    height:H,
                    panelHeight:that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data:[{
                        "id":"-1",
                        "text":"全部",
                        "selected":true
                    },
                    {
                        "id":1,
                        "text":"暂停"
                    },{
                        "id":2,
                        "text":"未开始"
                    },{
                        "id":3,
                        "text":"运行中"
                    },{
                        "id":4,
                        "text":"结束"
                    }]
                });

            /**************** ------------------- 初始化easyui 控件  End****************/


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

                if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,that.elems.optionType,function(data){
                        $('#win').window('close');
                        alert("操作成功");

                        that.loadPageData(e);

                    });
                }
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){
                debugger;
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.loadData.args.CouponTypeID=row.CouponTypeID;

                if(optType=="delete"){

                    $.messager.confirm("删除操作","确认要删除该条记录以后将不再显示",function(r){
                        if(r){
                            that.loadData.operation(row,optType,function(){
                                alert("操作成功");
                                that.loadPageData()
                            })

                        }
                    })
                }
                if (optType == "exit") {
                    var mid = JITMethod.getUrlParam("mid");
                    location.href = "activityDetail.aspx?mid=" + mid+"&ActivityID="+row.ActivityID;
                }
                if(optType=="start"||optType=="end"){
                    that.loadData.operation(row,optType,function(){
                        alert("操作成功");
                        that.loadPageData()
                    })
                }

            });
            /**************** -------------------列表操作事件用例 End****************/
        },


        //收款
        addNumber:function(data){
            debugger;
            var that=this;
            that.elems.optionType="add";
            $('#win').window({title:"优惠券追加",width:430,height:230});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_AddNumber');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open')
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

            debugger;
            var that=this;
            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');

            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            that.loadData.args.SearchColumns={ActivityType:2};
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                if(filed.value!=-1) {
                    that.loadData.args.SearchColumns[filed.name] = filed.value;
                }
            });

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e)
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.ActivityList){

                data.Data.ActivityList=[]
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
                data:data.Data.ActivityList,
               //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'ActivityID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[
                    {field : 'ActivityName',title : '活动名称',width:100,align:'left',resizable:false},

                    {field : 'TargetGroups',title : '目标群体',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'BeginEndData',title : '起止日期',width:100,resizable:false,align:'left'},


                    {field : 'SendCouponQty',title : '领券人数',width:100,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return parseInt(value);
                            }
                        }},
                    {field : 'Status',title : '活动状态',width:100,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            //1：暂停，2：未开始，3：运行中，4结束
                            var str;
                            switch (value){
                                case 1: str="暂停";   break;
                                case 2: str="未开始"; break;
                                case 3: str="运行中"; break;
                                case 4: str="结束"; break;
                            }

                            return str;
                        }},
                    {field : 'addOpt',title : '操作',width:52,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            debugger;
                            var html="";
                            switch (row.Status){
                                case 1: html= "<div title='编辑' data-index="+index+" data-flag='exit' class='exit  opt'> </div>" +
                                    "<div title='开始' data-index="+index+" data-flag='start' class='start opt'></div>";
                                    break;
                                case 2:  html= "<div title='编辑' data-index="+index+" data-flag='exit' class='exit  opt'> </div>" +
                                    "<div title='删除' data-index="+index+" data-flag='delete' class='delete opt'></div>";  break;
                                case 3:   html= "<div title='暂停'  data-index="+index+" data-flag='end' class='end  opt'> </div>";  break;

                                case 4:html="<div title='删除' data-index="+index+" data-flag='delete' class='delete opt'></div>"; break;
                            }


                            return html;
                        }

                    }




                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.html("没有符合条件的查询记录");
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                    if(that.elems.click) {

                        that.elems.click=true;
                        if(rowData.Status==1||rowData.Status==2) {
                            var mid = $.util.getUrlParam("mid");
                            location.href = "activityDetail.aspx?mid=" + mid + "&ActivityID=" + rowData.ActivityID;
                        }else if(rowData.Status==3){
                            $.messager.alert("提示", "活动运行中,不可修改");
                        }else if(rowData.Status==4){
                            $.messager.alert( "提示","活动已经结束,不可修改");

                        }
                    }
                },onClickCell:function(rowIndex, field, value){
                    if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }

            });



            //分页

            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
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
        //分页
        loadMoreData: function (currentPage) {

            var that = this;
            //that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');

            this.loadData.args.PageIndex = currentPage;
            that.loadData.getActivityList(function(data){
                that.renderTable(data);
            });
        },



        loadData: {
            args: {
                bat_id:"1",
                PageIndex:1,
                PageSize: 10,
                SearchColumns:{ActivityType:2},    //查询的动态表单配置   ActivityType:1 //活动类型 （1：生日活动，2：普通活动）
                OrderBy:"",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'

            },

            opertionField:{},
            getActivityList: function (callback) {
                var prams={data:{action:""}};
                prams.data={
                    action: "Marketing.Activity.GetActivityList",
                    PageSize:this.args.PageSize,
                    PageIndex:this.args.PageIndex
                    /*OrderBy:this.args.Phone,
                     UnitID:"005c99f55c26916fbd305bd69e8948fd",//window.RoleCode=="Admin"?null:window.UnitID,//
                     Status:this.args.Status*/

                };
                /*$.each(this.args.SearchColumns, function(i, field) {

                    if (field.value!=-1) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });*/
                $.extend(prams.data,this.args.SearchColumns);

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,
                    /*  channelID:'7',
                     customerId:"464153d4be5944b19a13e325ed98f1f4",
                     userId:"550E2D12613D4580989B65AF984F578D",*/
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


            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"VIP.SysVipCardType.DelSysVipCardType"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.ActivityID=pram.ActivityID;
                switch(operationType){
                    case "delete" : prams.data.action="Marketing.Activity.DelActivity";    break //删除
                    case "start":prams.data.action="Marketing.Activity.SetActivityState" ;  break //开始
                    case "end" : prams.data.action="Marketing.Activity.SetActivityState" ;    break //暂停
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

