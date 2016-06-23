define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','highcharts'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格活动body部分
            tableRank:$('#gridRank'),                //表格活动body部分
            tabelWrap:$('#tableWrap'),
            editLayer: $("#editLayer"),           //图片上传
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            addToolsBtn:$('#addTools'),
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            datamessage:  $("#pagecontianer").find(".dataMessage"),
            dataNoticeList:$('#notice'),
            panlH:116                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard: '',//储值卡号
        submitappcount: false,//是否正在提交追加表单
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
            this.chartsData();
        },
        //显示弹层
        showElements:function(selector){
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements:function(selector){

            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        //加载charts
        chartsData:function(){
            var that = this;
            // 获取集客效果数据
            that.loadData.getSuperRetail(function(data){
                var list = data;
                if(list!=null){
                    var Day30RTCount = list.Day30ActiveRTCount||0,//近30天活跃分销商数量
                        Day30NoRTCount = list.Day30NoActiveRTCount,//近30天非活跃分销商数量
                        Day30SalesRTCount = list.Day30SalesRTCount||0,//成交分销商数量
                        Day30SalesExpandRTCount = list.Day30SalesExpandRTCount,//近30天成交新增下线
                        Day30NoExpandRTCount = list.Day30SalesNoExpandRTCount,//成交未新增下线
                        Day30ExpandRTCount = list.Day30ExpandRTCount||0,//拓展下线的分销商
                        Day30JoinSalesRTCount = list.Day30JoinSalesRTCount,//拓展下线的分销商成交数量
                        Day30NoSalesRTCount = list.Day30JoinNoSalesRTCount;//拓展下线的分销商未成交数量
                    $('#activeCharts').find('.acount').html(Day30RTCount);
                    $('#completeCharts').find('.acount').html(Day30SalesRTCount);
                    $('#expandCharts').find('.acount').html(Day30ExpandRTCount);
                    if(Day30NoRTCount!=null||Day30RTCount!=0){
                        $('#activeCharts .charts').highcharts({
                            chart: {
                                plotBackgroundColor:null,
                                plotBorderWidth: null,
                                plotShadow: false
                            },
                            colors:
                                ['#ffd2d2','#ff4d4d']
                            ,
                            title: {
                                text: '',
                                style:{fontFamily:"Microsoft YaHei",
                                    color:'#666',
                                    fontSize:'18px'
                                }
                            },
                            legend:{
                                align:'right',
                                layout:'vertical',
                                reflow:'true',
                                verticalAlign:'bottom',
                                x: 0,
                                y: 0
                            },
                            credits: {
                                enabled: false
                            },
                            plotOptions: {
                                pie: {
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        color: '#000000',
                                        connectorColor: '#000000',
                                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                    },
                                    showInLegend: false
                                }
                            },
                            series: [{
                                type: 'pie',
                                name: '总览',
                                data: [['活跃分销商',Day30RTCount],['非活跃分销商',Day30NoRTCount]
                                ]
                            }]
                        });
                        $('.setOffVipModule').find('.chartsData').show();
                        $('#vipCharts').parents('.contents').children('.noContents').hide();
                        //return false;
                    }

                    if(Day30SalesExpandRTCount!=null||Day30NoExpandRTCount!=null){
                        $('#completeCharts .charts').highcharts({
                            chart: {
                                plotBackgroundColor:null,
                                plotBorderWidth: null,
                                plotShadow: false,
                            },
                            colors:
                                ['#d9f1fc','#01a1ff']
                            ,
                            title: {
                                text: '',
                                style:{fontFamily:"Microsoft YaHei",
                                    color:'#666',
                                    fontSize:'18px'
                                }
                            },
                            legend:{
                                align:'right',
                                layout:'vertical',
                                reflow:'true',
                                verticalAlign:'bottom',
                                x: 0,
                                y: 0
                            },
                            credits: {
                                enabled: false
                            },
                            plotOptions: {
                                pie: {
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        color: '#000000',
                                        connectorColor: '#000000',
                                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                    },
                                    showInLegend: false
                                }
                            },
                            series: [{
                                type: 'pie',
                                name: '总览',
                                data: [['成交新增下线',Day30SalesExpandRTCount],['成交未新增下线',Day30NoExpandRTCount]
                                ]
                            }]
                        });
                        $('.setOffStaffModule').find('.chartsData').show();
                        $('#staffCharts').parents('.contents').children('.noContents').hide();
                        //return false;
                    }
                    if(Day30JoinSalesRTCount!=null||Day30NoSalesRTCount!=null){
                        $('#expandCharts .charts').highcharts({
                            chart: {
                                plotBackgroundColor:null,
                                plotBorderWidth: null,
                                plotShadow: false,
                            },
                            colors:
                                ['#ffe29c','#ff8a00']
                            ,
                            title: {
                                text: '',
                                style:{fontFamily:"Microsoft YaHei",
                                    color:'#666',
                                    fontSize:'18px'
                                }
                            },
                            legend:{
                                align:'right',
                                layout:'vertical',
                                reflow:'true',
                                verticalAlign:'bottom',
                                x: 0,
                                y: 0
                            },
                            credits: {
                                enabled: false
                            },
                            plotOptions: {
                                pie: {
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        color: '#000000',
                                        connectorColor: '#000000',
                                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                    },
                                    showInLegend: false
                                }
                            },
                            series: [{
                                type: 'pie',
                                name: '总览',
                                data: [['已成交下线',Day30JoinSalesRTCount],['未成交下线',Day30NoSalesRTCount]
                                ]
                            }]
                        });
                    }



                }
            })

        },
        //加载商品列表
        initEvent: function () {
            var that = this;
            //点击切换分销商排名列表
            that.elems.operation.delegate("li","click",function(e){
                that.elems.operation.find("li").removeClass("on");
                $(this).addClass('on');
                that.setCondition();
                var value =that.elems.operation.find("li.on").data("field7").toString();
                that.loadData.getRTTopList(function(data){
                    that.renderTable(data,value);
                })
            });

            //点击下线人数碳层
            that.elems.tabelWrap.delegate(".insertePerson","click",function(e){
                that.loadData.args.PageIndex = 1;
                var $this = $(this),
                    id=$(this).data("id");
                that.loadData.tag.RetailTraderID = id;
                that.loadData.getSuperRetailTraderList(function(data){
                    that.renderTraderList(data);
                });
                $('#win').window({ title: "下线人数", width: 600, height: 625, top:15, left: ($(window).width() - 600) * 0.5 });
                $('#win').window('open');
                $('#win').parents('.window').css('position','fixed');
                $.util.stopBubble(e);
            });

            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=140,H=30;
            // $('#rewardRule').combobox({
            //     width:wd,
            //     height:H,
            //     panelHeight:that.elems.panlH,
            //     valueField: 'id',
            //     textField: 'text',
            //     data:[{
            //         "id":1,
            //         "text":"现金"
            //     },{
            //         "id":2,
            //         "text":"积分",
            //         "selected":true
            //     }]
            // });
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
                    $(that.elems.operation.find("li").get(1)).trigger("click");
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            // that.loadData.args.PageIndex=1;
            // var fileds=$("#seach").serializeArray();
            // $.each(fileds,function(i,filed){
            //     that.loadData.seach[filed.name] = filed.value;
            // });
            that.loadData.tag.busiType=that.elems.operation.find("li.on").data("field7");
        },


        //渲染tabel
        renderTable: function (data,value) {
            debugger;
            var that=this;
            if(!data.rsrtrttopinfo){
                return;
            }
            //jQuery easy datagrid  表格处理
            if(value=="1"){
                that.elems.tabel.datagrid({
                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选
                    showHeader:true,
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.rsrtrttopinfo,
                    sortName : 'brandCode',
                    //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    // frozenColumns : [ [ {
                    //     field : 'CTWEventId',
                    //     checkbox : true,
                    //     height:60
                    // } //显示复选框
                    // ] ],
                    columns : [[
                        {field : 'Idx',title : '排名',width:100,height:60,align:'left',resizable:false,sortable:true
                            ,formatter:function(value ,row,index) {
                            var str = value;
                            return str;
                        }
                        },
                        {field: 'SuperRetailTraderName', title: '姓名', width: 150, align: 'center', resizable: false
                            , formatter: function (value, row, index) {
                            return value;
                        }
                        },
                        {field : 'SalesAmount',title : '成交总额（元）',width:250,height:60,align:'center',resizable:false
                            ,formatter:function(value ,row,index) {
                            return value;
                        }
                        },
                        {field: 'dataTime', title: '加盟时间', width: 250, align: 'center', resizable: false
                            , formatter: function (value, row, index) {
                            return value;
                        }
                        }
                    ]],

                    onLoadSuccess : function(data) {
                        debugger;
                        that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if(data.rows.length>0) {
                            that.elems.dataMessage.hide();
                        }else{
                            $('#gridTable1').parents('.datagrid').hide();
                            that.elems.dataMessage.show();
                        }
                    },
                    onClickRow:function(rowindex,rowData){

                    },onClickCell:function(rowIndex, field, value){
                        if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                            that.elems.click=false;
                        }else{
                            that.elems.click=true;
                        }
                    }
                });
            }else{
                that.elems.tabel.datagrid({
                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选
                    showHeader:true,
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.rsrtrttopinfo,
                    sortName : 'brandCode',
                    //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    // frozenColumns : [ [ {
                    //     field : 'CTWEventId',
                    //     checkbox : true,
                    //     height:60
                    // } //显示复选框
                    // ] ],
                    columns : [[
                        {field : 'Idx',title : '排名',width:100,height:60,align:'left',resizable:false,sortable:true
                            ,formatter:function(value ,row,index) {
                            var str = value;
                            return str;
                        }
                        },
                        {field: 'SuperRetailTraderName', title: '姓名', width: 150, align: 'center', resizable: false
                            , formatter: function (value, row, index) {
                            return value;
                        }
                        },
                        {field : 'AddRTCount',title : '下线人数（人）',width:250,height:60,align:'center',resizable:false
                            ,formatter:function(value ,row,index) {
                            var Str = '<span data-id="'+row.SuperRetailTraderID+'" class="insertePerson" style="color:#038efe">'+value+'</span>';
                            return Str;
                        }
                        },
                        {field: 'dataTime', title: '加盟时间', width: 250, align: 'center', resizable: false
                            , formatter: function (value, row, index) {
                            return value;
                        }
                        }
                    ]],

                    onLoadSuccess : function(data) {
                        debugger;
                        that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if(data.rows.length>0) {
                            that.elems.dataMessage.hide();
                        }else{
                            $('#gridTable1').parents('.datagrid').hide();
                            that.elems.dataMessage.show();
                        }
                    },
                    onClickRow:function(rowindex,rowData){

                    },onClickCell:function(rowIndex, field, value){
                        if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                            that.elems.click=false;
                        }else{
                            that.elems.click=true;
                        }
                    }
                });
            }
        },

        //渲染tabel
        renderTraderList: function (data) {
            debugger;
            var that=this;
            if(!data.SuperRetailTraderList){
                return;
            }
            //jQuery easy datagrid  表格处理

            that.elems.tableRank.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                showHeader:true,
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.SuperRetailTraderList,
                sortName : 'brandCode',
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                // frozenColumns : [ [ {
                //     field : 'CTWEventId',
                //     checkbox : true,
                //     height:60
                // } //显示复选框
                // ] ],
                columns : [[
                    {field : 'SuperRetailTraderName',title : '分销商名称',width:200,height:60,align:'left',resizable:false,sortable:true
                        ,formatter:function(value ,row,index) {
                        var str = value;
                        return str;
                    }
                    },
                    {field: 'SuperRetailTraderPhone', title: '手机号', width: 200, align: 'center', resizable: false
                        , formatter: function (value, row, index) {
                        return value;
                    }
                    },
                    {field: 'JoinTime', title: '加盟时间', width: 250, align: 'center', resizable: false
                        , formatter: function (value, row, index) {
                        return value;
                    }
                    }
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tableRank.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.datamessage.hide();
                    }else{
                        $('#gridRank').parents('.datagrid').hide();
                        that.elems.datamessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){
                    if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }
            });
            kkpager.generPageHtml({
                pagerid:'kkpager',
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPages,
                totalRecords: data.TotalCount,
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

        },

        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            that.loadData.getSuperRetailTraderList(function(data){
                that.renderTraderList(data);
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.operation.find("li").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        loadData: {
            args: {
                PageIndex:1,
                PageSize:10
            },
            tag:{
                busiType:'',
                RetailTraderID:''
            },
            opertionField:{},
            //获取分销商30天数据
            getSuperRetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.SRTRTHome.GetRTRTHomeList'
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取分销商排名列表
            getRTTopList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.SRTRTHome.GetRTTopList',
                        'BusiType':this.tag.busiType,


                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

            //获取下线人数详情列表
            getSuperRetailTraderList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.SuperRetailTraderConfig.GetSuperRetailTraderList',
                        'PageIndex':this.args.PageIndex,
                        'PageSize':this.args.PageSize,
                        'IsFlag':1,
                        'SuperRetailTraderID':this.tag.RetailTraderID


                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

        }

    };
    page.init();
});

