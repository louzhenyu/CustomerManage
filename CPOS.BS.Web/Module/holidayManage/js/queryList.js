define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            tabelWrap:$('#tableWrap'),
            tabel:$("#gridTable"),
            dataMessage:$(".dataMessage"),
            click:true,
            panlH:116                           // 下来框统一高度
        },

        init: function () {
            this.initEvent();
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            /*      that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
             //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
             that.setCondition();
             //查询数据
             that.loadData.getHolidayList(function(data){
             //写死的数据
             //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
             //渲染table

             that.renderTable(data);


             });
             $.util.stopBubble(e);

             });*/

            that.elems.sectionPage.delegate(".commonBtn","click",function(e){
                var type=$(this).data("flag");
                that.elems.optionType="add";
                that.update();

            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;




            $('.datebox').datebox({
                width:wd,
                height:H
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
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){



                if ($('#optionForm').form('validate')) {
                    var param = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                        that.loadData.operation(param,that.elems.optionType,function(data){

                            alert("操作成功");
                            $('#win').window('close');
                            that.loadPageData(e);

                        });

                }




            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".fontC","click",function(e){
                var rowIndex=$(this).data("index");

                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.elems.optionType=$(this).data("oprtype")
                if(that.elems.optionType=="del") {
                    $.messager.confirm('删除', '确定删除此项以后将不再显示', function(r){
                        if (r){
                            that.loadData.operation(row, that.elems.optionType,function(data){
                                alert("操作成功");
                            });
                            that.loadPageData(e);
                        }
                    });

                }
                if( that.elems.optionType=="exit"){
                      debugger
                 that.update(row)
                }

            })
            /**************** -------------------列表操作事件用例 End****************/


        },
        update:function(data){
            var that=this;
            var title="假日信息";
            if(!data){
                title="添加假日"
            }
            $('#win').window({title:title,width:636,height:445,top:($(window).height() - 445) * 0.5,
                left:($(window).width() - 636) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_optionform');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#optionForm').form('load',data);
            $('#win').window('open');




        },


        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;

            that.loadData.getHolidayList(function(data){
                //写死的数据
                //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                //渲染table
                that.renderTable(data);


            });
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.HolidayList){

                data.Data.HolidayList=[];
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
                data:data.Data.HolidayList,
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[
                    {field : 'HolidayName',title : '标题',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=52;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,
                    {field : 'BeginDate',title : '开始日期',width:60,align:'center',resizable:false},
                    {field : 'EndDate',title : '结束日期',width:60,align:'center',resizable:false},
                    {field : 'Desciption',title : '描述',width:130,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=52;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,
                    { field : 'CreateTime',title : '创建日期',width:100,align:'center',resizable:false},
                    {field : 'isParent',title : '编辑',width:30,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC exit" data-index="'+index+'" data-oprtype="exit"></p>';
                        }
                    },

                    {field : 'Parent',title : '删除',width:30,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC delete" data-index="'+index+'" data-oprtype="del"></p>';
                        }
                    }
                ]],

                onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0){
                        that.elems.dataMessage.hide()
                    }else{
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                    /*  debugger;
                     if(that.elems.click){
                     that.elems.click = true;
                     debugger;
                     var index=rowindex, nodeData=rowData;
                     var mid = JITMethod.getUrlParam("mid");
                     location.href = "CarOrderDetail.aspx?OrderID=" + rowData.OrderID + "&vipId=" + rowData.VipID+"&UnitID="+rowData.UnitID + "&mid=" + mid;
                     }*/

                },onClickCell:function(rowIndex, field, value){
                    /*  if(field=="OrderID"){
                     that.elems.click=false;
                     }else{
                     that.elems.click=true;
                     }*/
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
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getHolidayList(function(data){
                that.renderTable(data);
            });
        },


        loadData: {
            args: {
                IsSystem:0,
                PageIndex: 1,
                PageSize: 10,
                LogisticsName:""

            },
            opertionField:{},


            getHolidayList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"Basic.Holiday.GetHolidayList",
                        PageSize:this.args.PageSize,
                        PageIndex:this.args.PageIndex

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
            operation:function(pram,operationType,callback){

                //根据不同的操作 设置不懂请求路径和 方法
                var prams={data:{action:"Basic.Holiday.SetHoliday"}};
                prams.url="/ApplicationInterface/Gateway.ashx";

                switch(operationType){
                    case "del":  //删除
                        prams.data.action= "Basic.Holiday.DeleteHoliday";
                        prams.data.HolidayId=pram.HolidayId;

                        break;
                    case "exit": //修改
                    case "add": //添加
                        $.each(pram, function(i, field) {
                            prams.data[field.name] = field.value; //提交的参数
                        });
                        break;
                }


                    $.util.ajax({
                        url: prams.url,
                        data: prams.data,
                        success: function (data) {
                            if (data.IsSuccess && data.ResultCode == 0) {
                                if (callback) {
                                    callback(data);
                                }

                            } else {
                                $.messager.alert("提示", data.Message);
                            }
                        }
                    });

            }


        }

    };
    page.init();
});

