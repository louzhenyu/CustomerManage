define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'highcharts', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            click: true,
            dataMessage: $("#pageContianer").find(".dataMessage"),
            panlH: 118                         // 下拉框统一高度
        },
        detailDate: {},
        //加载图表
        init: function () {
            this.initEvent();
            this.loadPageData();
            this.getSetOffDate(1);
        },
        initEvent: function () {
            var that = this;
            // 下拉框数据查询 --门店选择
            that.getshopDate(function (data) {
                data.push({"id":"","text":"请选择","selected": true});
                $('#storeList').combotree({
                    width: 200,
                    height: 32,
                    editable: true,
                    lines: true,
                    panelHeight: that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data: data
                });
            });
            
            $("#StatusList").combobox({
                width: 200,
                height: 32,
                panelHeight: that.elems.panlH,
                valueField: 'SetoffRoleId',
                textField: 'text',
                data: [{
                    "SetoffRoleId": "1",
                    "text": "员工"
                }, {
                    "SetoffRoleId": "2",
                    "text": "客服"
                }, {
                    "SetoffRoleId": "3",
                    "text": "会员"
                }]
            });
            //发送通知选择框
            $(".checkBox").bind("click", function (e) {
                var me = $(this);
                me.toggleClass("on");
                $.util.stopBubble(e);
                debugger;
                $('#addUnit').tooltip("hide");  //取消按钮
                var className = "." + me.data("toggleclass");
                if (me.hasClass("on") && me.data("flag") == "SuitableForStore") {
                    $(className).hide(0);
                } else if (me.data("flag") == "SuitableForStore") {
                    $(className).show(0);
                }
                if (me.hasClass("on") && me.data("flag") == "ConditionValue") {
                    $('#ConditionValue').numberbox({
                        min: 0,
                        max: null,
                        disabled: false
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({ "background": "#fff" });
                } else if (me.data("flag") == "ConditionValue") {
                    $('#ConditionValue').numberbox({
                        /*  max: 0,*/
                        disabled: true
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({ "background": "#efefef" });
                }
            });
            that.elems.sectionPage.find(".checkBox").trigger("click").trigger("click");
            //发送通知
            $('#sendmessage').bind('click', function (){
                $('#winmessage').window({
                    title: "发送消息至", width: 600, height: 450, top:400,
                    left: ($(window).width() - 600) * 0.5 
                });
                $('#winmessage').window('open');
            });
            //确认按钮
            $('#winmessage').delegate(".saveBtn","click",function(e){

                //只设置一个
                if (($($(".checkBox")[0]).hasClass("on")==true && $('#textApp').val().length > 0)||($($(".checkBox")[1]).hasClass("on")==true&&$('#textwebCat').val().length > 0)){
                    if ($('#textApp').val().length > 0 && $('#textwebCat').val().length > 0 ) {
                        var NoticeInfoList = [{ "NoticePlatformType": 1, "Title": "集客通知", "Text": $('#textApp').val() },{ "NoticePlatformType": 2, "Title": "集客通知", "Text": $('#textwebCat').val() }];
                    } else if ($('#textApp').val().length > 0) {
                        var NoticeInfoList = [{ "NoticePlatformType": 1, "Title": "集客通知", "Text": $('#textApp').val() }];
                    } else {
                        var NoticeInfoList = [{ "NoticePlatformType": 2, "Title": "集客通知", "Text": $('#textwebCat').val() }];
                    }
                    $('#winmessage').window('close');
                    that.setmessage(NoticeInfoList);
                }
                else {
                    $.messager.alert('提示',"设置选项至少选择一个");
                }
               
            });
            //近7天集客来源
            $('#checkDSeven').bind('click', function () {
                var dateNum = 1;
                $('#checkDSeven').css('color', '#038efe');
                $('#checkDThi').css('color', '#999999');
                that.getSetOffDate(dateNum);
            });
            //近30天集合来源
            $('#checkDThi').bind('click', function () {
                var dateNum = 2;
                $('#checkDThi').css('color', '#038efe');
                $('#checkDSeven').css('color', '#999999');
                that.getSetOffDate(dateNum);
            });
            //点击查询按钮
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getActivityList(function (data) {
                     that.renderTable(data);
                });
                $.util.stopBubble(e);
            });
            /**************** -------------------列表操作事件用例 End****************/
        },
        //获取门店下拉框信息

        getshopDate: function (callback) {
            $.util.oldAjax({
                url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
                data: {
                    'hasShop': 1
                },
                success: function (data) {
                    if (data) {
                        if (callback)
                            callback(data);
                    }
                }
            });
        },
        getSetOffDate: function (dateNum) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'Report.VipGoldReport.GetAggSetoffForSourceList',
                    'type':dateNum
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        $($('.numNow')[0]).find('span').text(data.Data.ShareTotal + '次');
                        $($('.numNow')[1]).find('span').eq(0).text(data.Data.TotalSetOff + '人');
                        if (data.Data.AddTotalSetOff >= 0) {
                            $($('.numNow')[1]).find('img').attr("src", "images/2.1_24.png");
                            $($('.numNow')[1]).find('span').eq(1).text(data.Data.AddTotalSetOff + '人');
                        } else {
                            $($('.numNow')[1]).find('img').attr("src", "images/down.png");
                            $($('.numNow')[1]).find('span').eq(1).text(data.Data.AddTotalSetOff.toString().substr(1) + '人');
                        }
                        // data.Data.addTotalSetOff
                        var len = data.Data.roletoolsources.length;
                        for (var i = 0; i < len; i++) {
                            if (data.Data.roletoolsources[i].SetoffRole == 1) {
                                $($('.numNow2')[0]).text(data.Data.roletoolsources[i].ShareCount + '次');
                                $($('.numNow2')[1]).find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $($('.numNow2')[1]).find('img').attr("src", "images/2.1_24.png");
                                    $($('.numNow2')[1]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                } else {
                                    $($('.numNow2')[1]).find('img').attr("src", "images/down.png");
                                    $($('.numNow2')[1]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount.toString().substr(1) + '人');
                                }
                            }
                            if (data.Data.roletoolsources[i].SetoffRole == 2) {
                                $($('.numNow2')[2]).text(data.Data.roletoolsources[i].ShareCount + '次');
                                $($('.numNow2')[3]).find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $($('.numNow2')[3]).find('img').attr("src", "images/2.1_24.png");
                                    $($('.numNow2')[3]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                } else {
                                    $($('.numNow2')[3]).find('img').attr("src", "images/down.png");
                                    $($('.numNow2')[3]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount.toString().substr(1) + '人');
                                }
                            }
                            if (data.Data.roletoolsources[i].SetoffRole == '3') {
                                $($('.numNow2')[4]).text(data.Data.roletoolsources[i].ShareCount + '次');
                                $($('.numNow2')[5]).find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >=0) {
                                    $($('.numNow2')[5]).find('img').attr("src", "images/2.1_24.png");
                                    $($('.numNow2')[5]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                } else {
                                    $($('.numNow2')[5]).find('img').attr("src", "images/down.png");
                                    $($('.numNow2')[5]).find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount.toString().substr(1) + '人');
                                }
                            }
                        }
                        //设置图表数据
                        $('#staffCharts').highcharts({
                            chart: {
                                plotBackgroundColor: null,
                                plotBorderWidth: null,
                                plotShadow: false,
                                title: false,
                                type: 'column'
                            },
                            colors:
                                ['#5cbfc9', '#ff9517', '#03a2e9'],
                            legend: {
                                align: 'right',
                                layout: 'horizontal',
                                floating: 'ture',
                                verticalAlign: 'top'
                            },
                            credits: {
                                enabled: false
                            },
                            xAxis: {
                                categories: [
                                    data.Data.lst[0].datetime,
                                    data.Data.lst[1].datetime,
                                    data.Data.lst[2].datetime,
                                    data.Data.lst[3].datetime,
                                    data.Data.lst[4].datetime
                                ]
                            },
                            title: {
                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                    '<td style="padding:0"><b>{point.y} 人</b></td></tr>',
                                footerFormat: '</table>',
                                useHTML: true,
                                text:null
                            },
                            yAxis: {
                                min: 0,
                                title: {
                                    text: '集客趋势 (人数)'
                                }
                            },
                            tooltip: {
                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                    '<td style="padding:0"><b>{point.y} 人</b></td></tr>',
                                footerFormat: '</table>',
                                shared: true,
                                useHTML: true
                            },
                            plotOptions: {
                                column: {
                                    pointPadding: 0.2,
                                    borderWidth: 0
                                }
                            },
                            series: [
                                {
                                    name: '会员',
                                    data: [
                                        data.Data.lst[0].RoleContent[0].PeopleCount,
                                        data.Data.lst[1].RoleContent[0].PeopleCount,
                                        data.Data.lst[2].RoleContent[0].PeopleCount,
                                        data.Data.lst[3].RoleContent[0].PeopleCount,
                                        data.Data.lst[4].RoleContent[0].PeopleCount
                                    ]
                                },
                               {
                                   name: '客服',
                                   data: [
                                       data.Data.lst[0].RoleContent[1].PeopleCount,
                                       data.Data.lst[1].RoleContent[1].PeopleCount,
                                       data.Data.lst[2].RoleContent[1].PeopleCount,
                                       data.Data.lst[3].RoleContent[1].PeopleCount,
                                       data.Data.lst[4].RoleContent[1].PeopleCount
                                   ]
                               },
                               {
                                   name: '店员',
                                   data: [
                                       data.Data.lst[0].RoleContent[2].PeopleCount,
                                       data.Data.lst[1].RoleContent[2].PeopleCount,
                                       data.Data.lst[2].RoleContent[2].PeopleCount,
                                       data.Data.lst[3].RoleContent[2].PeopleCount,
                                       data.Data.lst[4].RoleContent[2].PeopleCount
                                   ]
                               }]
                        });
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //设置查询条件   
        setCondition: function () {
            var that = this;
            //设置loading
            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex = 1;
            var fileds = $("#seach").serializeArray();
            //根据查询条件的name值  设定对应的args参数的值
            $.each(fileds, function (i, filed) {
                if (filed.value != -1) {
                    that.loadData.args[filed.name] = filed.value;
                }
            });
        },
        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e)
        },
        //发送通知
        setmessage: function (NoticeInfoList) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'VIP.VipGold.SendNotice',
                    'NoticeInfoList': NoticeInfoList
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        $.messager.alert('提示', '消息发送成功');
                    } else {
                        alert(data.Message);
                    }
                }
            })
        },
        //渲染tabel
        renderTable: function (data) {
            var that = this;
            if (!data.Data.aggsetoffforSourcelist) {
                //data.Data.ActivityList = []
            }
            //表格处理
            that.elems.tabel.datagrid({
                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //单选
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                data: data.Data.aggsetoffforSourcelist,// 数据来源 排序的列
                remoteSort : true, // 服务器排序*/
                idField: 'ID', //主键字段
                columns: [[
                    { field: 'SetoffRole', title: '来源', width: 100, align: 'left', resizable: false },
                    { field: 'UnitName',   title: '门店', width: 100, align: 'left', resizable: false },
                    { field: 'UserName', title: '名称', width: 100, align: 'left', resizable: false },
                    {field: 'PushMessageCount', title: '推送次数', width: 100, align: 'center', sortable: true, remotesort: true},
                    { field: 'ShareCount', title: '分享次数', width: 100, align: 'center', sortable: true, remotesort: true },
                    { field: 'SetoffCount', title: '会员人数', width: 100, align: 'center', sortable: true, remotesort: true },
                    { field: 'OrderAmount', title: '会员销量', width: 100, align: 'center', sortable: true, remotesort: true }
                ]],
                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.html("没有符合条件的查询记录");
                        that.elems.dataMessage.show();
                    }
                },
                //绑定排序时间
                onSortColumn: function (sort, order) {
                    that.loadData.args.OrderBy = sort;
                    if (order == "desc") {
                        that.loadData.args.SortType = 2;
                    } else {
                        that.loadData.args.SortType = 1;
                    }
                    that.loadData.getActivityList(function (data) {
                        that.renderTable(data);
                        that.elems.tabel.datagrid({ sortOrder: that.loadData.args.SortType ==1?"asc":"desc"});
                    });
                }
            });
            
            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                total: data.Data.TotalPageCount,//总页码
                totalRecords: data.Data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click 适用于不刷新页面
                click: function (n) {
                    this.selectPage(n);
                    that.loadMoreData(n);
                },
                getHref: function (n) {
                    return '#';
                }
            }, true);
            if ((that.loadData.opertionField.displayIndex || that.loadData.opertionField.displayIndex == 0)) {  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click", true);
                that.loadData.opertionField.displayIndex = null;
            }
        },
        //分页
        loadMoreData: function (currentPage) {
            var that = this;
            //that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getActivityList(function (data) {
                that.renderTable(data);
            });
        },
        loadData: {
            args: {
                bat_id: "1",
                PageIndex: 1,
                PageSize: 10,
                ReserveDateBegin: "",   //开始时间
                ReserveDateEnd: "",    //结束时间
                source: "",             //来源
                Status:"",             //门店
                OrderBy: "",           //排序字段
                SortType: null    //如果有提供OrderBy，排序方式（1=asc、2=desc）
            },
            opertionField: {},
            getActivityList: function (callback) {
                var prams = { data: { action: "" } };
                prams.data = {
                    action: "VIP.VipGold.GetAggSetoffForSourceList",
                    PageSize: this.args.PageSize,
                    PageIndex: this.args.PageIndex,
                    SetoffRoleId: this.args.Status,                 //集客来源
                    starttime: this.args.ReserveDateBegin,          //开始时间
                    enime: this.args.ReserveDateEnd,               //结束时间
                    unitid: this.args.source,   //门店ID
                    SortName: this.args.OrderBy,//排序名称
                    SortOrder: this.args.SortType//排序方式
                };
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,
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

