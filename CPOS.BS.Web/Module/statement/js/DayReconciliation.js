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
            dataMessage:$(".dataMessage"),
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
            debugger;
            $("#unitHtml").val(window.UnitName);
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {

                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();

                if($(this).data("flag")){
                    $("#win").window("open");
                    return false;
                }

                //查询数据
                that.loadData.getDayReconciliation(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);
            });
            /*  that.loadData.get_unit_tree(function(datas) {

             that.unitTree=datas;
             $("#unitTree").combotree({
             panelWidth:220,
             //width:220,
             //animate:true,
             multiple:false,
             valueField: 'id',
             textField: 'text',
             data:datas
             });
             $("#unitTree").combotree("setText","请选择门店");
             });*/
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;
            /*case 0 : staus="未激活";break;
             case 1 : staus= "正常";break;
             case 2 : staus= "冻结";break;
             case 3 : staus= "失效";break;
             case 4 : staus= "挂失";break;
             case 5 : staus="休眠";break;*/


            $('.datebox').datebox({
                width:160,
                height:H
            });


        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            if($("#seach").form("validate")) {
                var fileds = $("#seach").serializeArray();
                $.each(fileds, function (i, filed) {
                    if (filed.name == "VipCardStatusId") {
                        if (filed.value == -1) {
                            filed.value = '';
                        }

                    }

                });
            }

            that.loadData.args.SearchColumns= fileds;



        },

        //加载页面的数据请求
        loadPageData: function (e) {

            this.renderTable({Data:{DayReconciliationInfoList:null}},true)
        },

        //渲染tabel
        renderTable: function (data,fistLoad) {
            debugger;
            var that=this;

            if(!data.Data.DayReconciliationInfoList){

                data.Data.DayReconciliationInfoList=[];

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
                data:data.Data.DayReconciliationInfoList,
                /*sortName : 'MembershipTime', //排序的列*/
                sortOrder : 'desc', //倒序
                remoteSort : true, // 服务器排序
                idField : 'OrderID', //主键字段

                columns : [[

                    {field : 'MembershipTime',title : '售卡日期',width:96,align:'center',resizable:false},
                    {field : 'SaleCardCount',title : '售卡张数（含赠卡）',width:182,align:'center',resizable:false},
                    {field : 'SalesToAmount',title : '售卡总额',width:82,align:'center',resizable:false},
                    {field : 'RechargeTotalAmount',title : '充值总额',width:106,align:'center',resizable:false},
                    {field : 'StoredSalesTotalAmount',title : '储值消费总额',width:82,align:'center',resizable:false},
                    {field : 'IntegratDeductibleCount',title : '积分抵扣总数',width:66,resizable:false,align:'left'},
                    {field : 'IntegratDeductibleToAmount',title : '积分抵扣金额',width:106,align:'center',resizable:false}
                    /*{field : 'GiftCardCount',title : '可用积分总额',width:106,align:'center',resizable:false},
                    {field : 'SalesAmount',title : '积分兑换数',width:83,align:'center',resizable:false},
                    {field : 'GiftCardCount',title : '使用券数量',width:82,align:'center',resizable:false}*/
                ]],

                onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0){

                        that.elems.dataMessage.hide();

                        //that.mergeCellsByField(that.elems.tabel,"MembershipTime");
                    }else{
                        if(fistLoad){
                            that.elems.dataMessage.html("请选择一个礼拜之间的时间，点击查询");
                        } else{
                            that.elems.dataMessage.html("没有符合条件的记录");
                        }
                        that.elems.dataMessage.show();
                    }

                },
               /* onClickRow:function(rowindex,rowData){
                    debugger;
                    if(that.elems.click){
                    }

                },onClickCell:function(rowIndex, field, value){
                    if(field=="OrderID"){  //有复选框，或者操作列的时候用到
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }
                */
            });


            /*  if(data.Data.DayReconciliationInfoList.length>0) {
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


             if ((that.loadData.opertionField.displayIndex || that.loadData.opertionField.displayIndex == 0)) {  //点击的行索引的  如果不存在表示不是显示详情的修改。
             that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click", true);
             that.loadData.opertionField.displayIndex = null;
             }
             }*/
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getDayReconciliation(function(data){
                that.renderTable(data.Data.DayReconciliationInfoList);
            });
        },

        /*  mergeCellsByField()在DataGrid的onLoadSuccess中调用。
         * EasyUI DataGrid根据字段动态合并单元格
         * @param tableID 要合并table的id
         * @param colList 要合并的列,用逗号分隔(例如："name,department,office");*/
        //合并相同的值的属性行
        mergeCellsByField:function(table,colList){


            var ColArray = colList.split(",");
            var tTable =table ;
            var TableRowCnts=tTable.datagrid("getRows").length;
            var tmpA;
            var tmpB;
            var PerTxt = "";
            var CurTxt = "";
            var alertStr = "";
            //for (j=0;j<=ColArray.length-1 ;j++ )
            for (j=ColArray.length-1;j>=0 ;j-- )
            {
                //当循环至某新的列时，变量复位。
                PerTxt="";
                tmpA=1;
                tmpB=0;

                //从第一行（表头为第0行）开始循环，循环至行尾(溢出一位)
                for (i=0;i<=TableRowCnts ;i++ )
                {
                    if (i==TableRowCnts)
                    {
                        CurTxt="";
                    }
                    else
                    {
                        CurTxt=tTable.datagrid("getRows")[i][ColArray[j]];
                    }
                    if (PerTxt==CurTxt)
                    {
                        tmpA+=1;
                    }
                    else
                    {
                        tmpB+=tmpA;
                        tTable.datagrid('mergeCells',{
                            index:i-tmpA,
                            field:ColArray[j],
                            rowspan:tmpA,
                            colspan:null
                        });
                        tmpA=1;
                    }
                    PerTxt=CurTxt;
                }
            }
        },

        loadData: {
            args: {
                PageIndex: 1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1
            },
            tag:{
                VipId:"",
                orderID:''
            },
            getUitTree:{
                node:""
            },
            opertionField:{},
            getDayReconciliation: function (callback) {
                var prams={data:{action:""}};
                prams.data={
                    action: "Report.StoresReport.GetDayReconciliation",
                    PageSize:this.args.PageSize,
                    PageIndex:this.args.PageIndex
                };
                $.each(this.args.SearchColumns, function(i, field) {
                    if (field.value) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });
                debugger;
                if($.util.GetDateDiff(prams.data["EndDate"],prams.data["StareDate"],"day")>7){
                    $.messager.alert("提示","只能查看七天之内的信息")
                }else{
                    $.util.ajax({
                        url: "/ApplicationInterface/Gateway.ashx",
                        data: prams.data,

                        success: function (data) {
                            if (data.IsSuccess && data.ResultCode == 0) {
                                if (callback) {
                                    callback(data);
                                }

                            } else {
                                $.messager.alert("提示",data.Message);
                            }
                        }
                    });
                }


            },
            //获取门店
            get_unit_tree: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx",
                    data:{

                        action: "get_unit_tree",
                        QueryStringData:{
                            _dc:'1433225205961',
                            node:this.getUitTree.node,
                            parent_id:'',
                            multiSelect:false,
                            isSelectLeafOnly:false,
                            isAddPleaseSelectItem:false,
                            pleaseSelectText:'--%E8%AF%B7%E9%80%89%E6%8B%A9--',
                            pleaseSelectID: -2
                        }

                    } ,

                    success: function (data) {

                        if (data&&data.length>0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                            console.log("加载数据不成功");
                        }
                    }
                });
            },





        }

    };
    page.init();
});

