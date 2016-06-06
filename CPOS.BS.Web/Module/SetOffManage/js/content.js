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
            $("#filterSource").combobox({
                width: 200,
                height: 32,
                panelHeight: that.elems.panlH,
                valueField: 'SetoffRoleId',
                textField: 'text',
				editable: false,
                data: [{
                    "SetoffRoleId": "CTW",
                    "text": "创意仓库"
                }, {
                    "SetoffRoleId": "Coupon",
                    "text": "优惠券"
                }, {
                    "SetoffRoleId": "SetoffPoster",
                    "text": "集客海报"
                },
                {
                    "SetoffRoleId": "Goods",
                    "text": "商品"
                }]
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
        },
        getSetOffDate: function (dateNum) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'Report.VipGoldReport.GetAggSetoffForToolList',
                    'type':dateNum
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        $('#sharetotal').find('span').text(data.Data.ShareTotal + '次');
                        $('#setofftotal').find('span').eq(0).text(data.Data.TotalSetOff + '人');
                        $('#setofftotal').find('span').eq(1).text(data.Data.AddTotalSetOff + '人');
                        if (data.Data.AddTotalSetOff >= 0) {
                            $('#setofftotal').find('img').attr("src", "images/2.1_24.png");
                        } else {
                            $('#setofftotal').find('img').attr("src", "images/down.png");
                            $('#setofftotal').find('span').eq(1).text(Math.abs(data.Data.AddTotalSetOff) + '人');
                        }
                        var len = data.Data.roletoolsources.length;
                        for (var i = 0; i < len; i++) {
                            if (data.Data.roletoolsources[i].SetoffRole == "CTW") {
                                //活动分享
                                $('#activityshare').find('span').text(data.Data.roletoolsources[i].ShareCount + '次');
                                $('#activitysetoff').find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                $('#activitysetoff').find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $('#activitysetoff').find('img').attr("src", "images/2.1_24.png");
                                } else {
                                    $('#activitysetoff').find('img').attr("src", "images/down.png");
                                }
                            }
                            if (data.Data.roletoolsources[i].SetoffRole == "SetoffPoster") {
                                //海报
                                $('#sharePoster').find('span').text(data.Data.roletoolsources[i].ShareCount + '次');
                                $('#SetoffPoster').find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                $('#SetoffPoster').find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $('#SetoffPoster').find('img').attr("src", "images/2.1_24.png");
                                } else {
                                    $('#SetoffPoster').find('img').attr("src", "images/down.png");
                                }
                            }
                            if (data.Data.roletoolsources[i].SetoffRole == 'Coupon') {
                                //优惠券
                                $('#Couponshare').find('span').text(data.Data.roletoolsources[i].ShareCount + '次');
                                $('#Couponsetoff').find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                $('#Couponsetoff').find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $('#Couponsetoff').find('img').attr("src", "images/2.1_24.png");
                                } else {
                                    $('#Couponsetoff').find('img').attr("src", "images/down.png");
                                }
                            }
                            if (data.Data.roletoolsources[i].SetoffRole == 'Goods') {
                                //商品
                                $('#Goodshare').find('span').text(data.Data.roletoolsources[i].ShareCount + '次');
                                $('#Goodssetoff').find('span').eq(0).text(data.Data.roletoolsources[i].SetoffCount + '人');
                                $('#Goodssetoff').find('span').eq(1).text(data.Data.roletoolsources[i].DiffCount + '人');
                                if (data.Data.roletoolsources[i].DiffCount >= 0) {
                                    $('#Goodssetoff').find('img').attr("src", "images/2.1_24.png");
                                } else {
                                    $('#Goodssetoff').find('img').attr("src", "images/down.png");
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
                                ['#5cbfc9', '#ff9517', '#03a2e9','#7cc442'],
                            legend: {
                                align: 'right',
                                layout: 'horizontal',
                                floating: 'ture',
                                verticalAlign: 'top'
                            },
                            credits: {
                                enabled: false
                            },
                            title: {
                                text: null
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
                            yAxis: {
                                labels: {
                                    enabled: false
                                },
                                gridLineWidth: 0,
                                showEmpty: false
                            },
                            tooltip: {
                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                    '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
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
                                    name: '创意仓库',
                                    data: [
                                        data.Data.lst[0].RoleContent[0].PeopleCount,
                                        data.Data.lst[1].RoleContent[0].PeopleCount,
                                        data.Data.lst[2].RoleContent[0].PeopleCount,
                                        data.Data.lst[3].RoleContent[0].PeopleCount,
                                        data.Data.lst[4].RoleContent[0].PeopleCount
                                    ]
                                },
                               {
                                   name: '优惠券',
                                   data: [
                                       data.Data.lst[0].RoleContent[1].PeopleCount,
                                       data.Data.lst[1].RoleContent[1].PeopleCount,
                                       data.Data.lst[2].RoleContent[1].PeopleCount,
                                       data.Data.lst[3].RoleContent[1].PeopleCount,
                                       data.Data.lst[4].RoleContent[1].PeopleCount
                                   ]
                               },
                               {
                                   name: '集客报',
                                   data: [
                                       data.Data.lst[0].RoleContent[2].PeopleCount,
                                       data.Data.lst[1].RoleContent[2].PeopleCount,
                                       data.Data.lst[2].RoleContent[2].PeopleCount,
                                       data.Data.lst[3].RoleContent[2].PeopleCount,
                                       data.Data.lst[4].RoleContent[2].PeopleCount
                                   ]
                               },
                                {
                                    name: '商品',
                                    data: [
                                        data.Data.lst[0].RoleContent[3].PeopleCount,
                                        data.Data.lst[1].RoleContent[3].PeopleCount,
                                        data.Data.lst[2].RoleContent[3].PeopleCount,
                                        data.Data.lst[3].RoleContent[3].PeopleCount,
                                        data.Data.lst[4].RoleContent[3].PeopleCount
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

        //渲染tabel
        renderTable: function (data) {
            var that = this;
            if (!data.Data.aggsetoffforToollist) {
                data.Data.aggsetoffforToollist = []
            }
            //表格处理
            that.elems.tabel.datagrid({
                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //单选
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                data: data.Data.aggsetoffforToollist,// 数据来源 排序的列
                sortOrder : 'desc', //倒序
                remoteSort : true, // 服务器排序*/
                idField: 'ID', //主键字段
                columns: [[
                    { field: 'SetoffRole', title: '类型', width: 100, align: 'center', resizable: false },
                    { field: 'ObjectName', title: '活动名称', width: 100, align: 'center', resizable: false,
						formatter:function (value, row, index) {
							//SuperRetailTraderID
							var htmlStr = '<span title="'+value+'" style="display:block;" class="t-overflow">'+value+'</span>';
							return htmlStr;
						}
					},
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
                OrderBy: "",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'
            },
            opertionField: {},
            getActivityList: function (callback) {
                var prams = { data: { action: "" } };
                prams.data = {
                    action: "VIP.VipGold.GetAggSetoffForToolList",
                    PageSize: this.args.PageSize,
                    PageIndex: this.args.PageIndex,
                    SetoffRoleId: this.args.source,                   //来源
                    starttime: this.args.ReserveDateBegin,               //开始时间
                    endtime: this.args.ReserveDateEnd,               //结束时间
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

