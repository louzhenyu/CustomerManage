define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'artDialog', 'highcharts', 'touchslider'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabel2: $("#gridTable2"),
            tabelWrap: $('#tableWrap'),
            tabelWrap2: $('#tableWrap2'),
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#opt,#Tooltip'),              //弹出框操作部分
            dataMessage: $(".dataMessage"),
            dataMessage2: $(".dataMessage2"),
            vipSourceId: '',
            click: true,
            panlH: 116                           // 下来框统一高度
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this,
				$notShow = $('.nextNotShow span');
            $notShow.on('click', function () {
                if ($notShow.hasClass('on')) {
                    $notShow.removeClass('on');
                } else {
                    $notShow.addClass('on');
                }
            })


            //查看全年计划
            $(".Annualplanbtn").on("click", function () {


                $("#Annualplanlayer").show();

                var h = $(".Annualplan").height();
                var w = $(".Annualplan").width();
                $(".planlayer").css("margin-top", -(h / 2));
                $(".planlayer").css("margin-left", -(w / 2));
            });

            $("#Annualplanlayer").on("click", function () {

                $("#Annualplanlayer").hide();
            });

            $(".close").on("click", function () {

                $("#Annualplanlayer").hide();
            });

            $(".downmodel").on("click", function (e) {
                
                e.stopPropagation();
            });
            //查看全年计划end

            $(".TemplatePreview").on("click", ".releasebtn", function () {
                $.util.toNewUrlPath("/Module/CreativeWarehouse/creative.aspx?TemplateId=" + $(this).data("id"));
            });

            //查看活动数据详情
            $(".ActivityGroups").on("click", ".ActivityGroupName", function () {

                $(".ActivityGroupName").addClass("graycolor");
                $(this).removeClass("graycolor");
                $(".arrow").hide();
                $(this).find(".arrow").show();
                that.loadData.seach.form.ActivityGroupCode = $(this).data("code");
                that.GetTemplateList(function (data) {
                    //活动数据
                    if (data.TemplateList) {
                        $(".TemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    } else {
                        $(".TemplatePreview").html("");
                    }

                });
            });


            $('#win').delegate(".saveBtn", "click", function (e) {
                $('#win').window('close');
            });

            //banner展示二维码
            $(".InSeasonList").on("mouseover", ".preview", function (e) {
                $(".qcode img").attr("src", $(this).data("src"));
                $(".qcode").show();

            });


            //banner隐藏二维码
            $(".InSeasonList").on("mouseout", ".preview", function (e) {
                $(".qcode").hide();


            });

            //banner发起活动
            $(".InSeasonList").on("click", ".start", function (e) {

                $.util.toNewUrlPath("/Module/CreativeWarehouse/creative.aspx?TemplateId=" + $(this).data("id"));

            });

            //banner浏览更多
            $(".InSeasonList").on("click", ".viewmore", function (e) {

                $(".ActivityGroupName").removeClass("graycolor");
                $(".arrow").show();
                that.loadData.seach.form.ActivityGroupCode = "";
                that.GetTemplateList(function (data) {
                    //活动数据
                    if (data.TemplateList) {
                        $(".TemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    } else {
                        $(".TemplatePreview").html("");
                    }

                });

            });

            

            //显示二维码
            $(".TemplatePreview").on("mouseover", ".ActivityContent", function () {
                $(this).find(".ActivityQRcode").show();
            });

            //隐藏二维码
            $(".TemplatePreview").on("mouseout", ".ActivityContent", function () {
                $(this).find(".ActivityQRcode").hide();
            });



        },
        //设置查询条件   取得动态的表单查询参数
        setCondition: function () {
            var that = this;
            var fileds = $("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                that.loadData.seach.form[filed.name] = filed.value;
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {

            var that = this;
           

            that.GetTemplateList(function (data) {
                if (data) {
                    if (data.BannerList) {
                        $(".InSeasonList").append(bd.template("tpl_InSeasonList", data));
                        //赋值导航start
                        var navhtml = "<span class='thisseasontouchslider-prev'></span>";
                        for (i = 0; i < data.BannerList.length; i++) {
                            if (i == 0)
                                navhtml += " <span class='thisseasontouchslider-nav-item thisseasontouchslider-nav-item-current'></span>";
                            else
                                navhtml += " <span class='thisseasontouchslider-nav-item '></span>";
                        }
                        navhtml += "<span class='thisseasontouchslider-next'></span>";

                        $(".thisseasontouchslider-nav").html(navhtml);
                        //赋值导航end

                        $(".touchsliderthisseason").touchSlider({
                            container: this,
                            duration: 350, // 动画速度
                            delay: 3000, // 动画时间间隔
                            margin: 5,
                            mouseTouch: true,
                            namespace: "touchslider",
                            next: ".thisseasontouchslider-next", // next 样式指定
                            pagination: ".thisseasontouchslider-nav-item",
                            currentClass: "thisseasontouchslider-nav-item-current", // current 样式指定
                            prev: ".thisseasontouchslider-prev", // prev 样式指定
                            autoplay: true, // 自动播放
                            viewport: ".thisseasontouchslider-viewport"
                        });
                    }

                    //即将上线
                    if (data.PlanList) {
                        $(".seasonlist_ul").html(bd.template("tpl_seasonlist", data));
                    }

                    ////活动数据
                    //if (data.TemplateList) {
                    //    $(".TemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    //}

                    if (data.PlanImageUrl)
                    {
                        $(".Annualplan").attr("src", data.PlanImageUrl);
                    }
                    
                }
            });
            debugger;

            $(".ActivityGroupName").get(0).click();
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;

            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.GetTemplateList(function (data) {
            });

        },
        //查询列表
        GetTemplateList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingActivity.GetTemplateList',
                    ActivityGroupCode: that.loadData.seach.form.ActivityGroupCode
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        //获取创意营销活动类型的信息
        GetMarketingGroupType: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.MarketingData.GetMarketingGroupType',
                    EventTypeId: 1
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        SetLEventTemplateReleaseStatus: function (callback) {//发布
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetLEventTemplateReleaseStatus'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },


        //下季活动列表
        NextSeasonList: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.NextSeasonList'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;

                        if (result) {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },

        loadData: {
            args: {
                bat_id: "1",
                PageIndex: 1,
                PageSize: 10,
                SearchColumns: {},    //查询的动态表单配置
                OrderBy: "",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status: -1,
                page: 1,
                start: 0,
                limit: 15
            },
            seach: {
                form: {
                    ActivityGroupCode: ""
                }
            }

        }

    };
    page.init();
});




