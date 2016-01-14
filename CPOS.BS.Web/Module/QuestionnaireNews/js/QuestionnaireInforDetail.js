define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            QuestionnaireID: "",
            ActivityID:"",
            thead: $("#thead"),                    //表格head部分
            panlH: 116                           // 下来框统一高度
        },
        init: function () {
            this.initEvent();

            this.elems.ActivityID = JITMethod.getUrlParam("EventID");

            this.loadPageData();
        },
        initEvent: function () {
            var that = this;

            $(".navtit").on("click", function () {
                if (!$(this).hasClass("selected")) {
                    $(".navtit").toggleClass("selected");
                    $(".showcontent").toggle();
                }
            });
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition: function () {
            var that = this;
            var fileds = $("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                //filed.value=filed.value=="0"?"":filed.value;
                //that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name] = filed.value;
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            
            //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
            that.setCondition();
            //查询数据
            that.loadData.args.PageIndex = 1;
            that.getQuestionnaireInforList(function (data, columns) {
                
                that.renderTable(data, columns);
            });

            that.getQuestionAndOptionList();

            $.util.stopBubble(e);
        },



        //渲染tabel
        renderTable: function (data, columns) {
            var that = this;
            if (!data.ResultData) {

                return;
            }

            debugger;
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method: 'post',
                //iconCls : 'icon-list', //图标
                singleSelect: true, //单选
                // height : 332, //高度
                fitColumns: false, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                //collapsible : true,//可折叠
                //数据来源
                data: data.ResultData,
                //sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : '', //主键字段
                /*  pageNumber:1,*/
                //frozenColumns:[[]],
                columns: [columns],
                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    
                    if (data.rows.length > 0) {
                        $('#kkpager').show();
                    } else {
                        that.elems.dataMessage.show();
                        $('#kkpager').hide();
                    }
                    //that.elems.tabel.datagrid('getSelected');


                }
            });



            //分页
            //data.Data={};
            //data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            //var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
                pagerid: 'kkpager',
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.PageCount,
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
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            that.getQuestionnaireInforList(function (data, columns) {
                that.renderTable(data, columns);
            });
        },
        getQuestionnaireInforList: function (callback) {
            var that = this;
            debugger;
            if (that.elems.ActivityID != null && that.elems.ActivityID != "") {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Questionnaire.Questionnaire.GetQuestionnaireInfor',
                        ActivityID: that.elems.ActivityID
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;

                            if (callback) {
                                var columns = "";

                                $(".QuestionnaireName").html(result.QuestionnaireName);
                                if (result.TitleData.length > 0) {
                                    for (var i = 0; i < result.TitleData.length; i++) {

                                        columns += "{ field: '" + result.TitleData[i].NameID + "', title: '" + result.TitleData[i].Name + "',  resizable: true, align: 'center' },";
                                    }
                                }
                                columns = "[" + columns + "]";

                                callback(result, Ext.decode( columns));
                            }



                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            }

        },
        getQuestionAndOptionList: function () {
            var that = this;
            debugger;
            if (that.elems.ActivityID != null && that.elems.ActivityID != "") {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Questionnaire.Questionnaire.GetQuestionnaireOptionInfor',
                        ActivityID: that.elems.ActivityID
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            debugger;
                            
                            if (result.Questionlist)
                            {
                                for (var i = 0; i < result.Questionlist.length; i++) {
                                    if (result.Questionlist[i].Optionlist) {
                                        for (var j = 0; j < result.Questionlist[i].Optionlist.length; j++) {
                                            debugger;
                                            result.Questionlist[i].Optionlist[j].number = that.getnumber((j + 1));
                                        }
                                    }
                                }

                                $(".inlineBlockArea").html(bd.template("tpl_Optionlist", result));
                            }



                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            }

        }
         ,
        getnumber: function (index) {
            if (index > 0 && index < 20)
                return String.fromCharCode(64 + parseInt(index));
            else
                return "";
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
            tag: {
                VipId: "",
                orderID: ''
            },
            seach: {
                item_category_id: null,
                SalesPromotion_id: null,
                form: {
                    item_code: "",
                    item_name: "",
                    item_startTime: '',
                    item_endTime: ''
                }
            },
            opertionField: {}

        }
    }
    page.init();
});


