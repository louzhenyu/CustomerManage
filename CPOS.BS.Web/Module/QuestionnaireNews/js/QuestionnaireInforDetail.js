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
            QuestionnaireName:"",
            ActivityID:"",
            thead: $("#thead"),                    //表格head部分
            panlH: 116,                           // 下来框统一高度
            Timer: null,
            Timercount: 1
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

            $(".batchdelete").on("click", function () {
                if (that.elems.tabel.datagrid("getChecked").length < 1) {
                    $.messager.alert("提示", "至少选择一项数据。");
                    return;
                }
                $.messager.confirm("提示", "你是否确定要删除所选的数据吗？", function (r) {
                    if (r) {
                        var data = that.elems.tabel.datagrid("getChecked");
                        var VipIDs=new Array();
                        debugger;
                        for (var i = 0; i < data.length; i++) {
                             VipIDs[i] = data[i].ID;
                        }
                        that.DelQuestionnaireAnswerRecord(VipIDs);
                    }
                });

            });

            $(".exportBtn").on("click", function () {
                if (that.elems.QuestionnaireID == "")
                {
                    $.messager.alert("提示", "数据尚未加载完，请稍后再试。");
                    return;
                }

                $.messager.confirm("导出问卷详细列表", "你确定导出当前列表的数据吗？", function (r) {
                    if (r) {
                        that.exportList();
                    }
                });
            });

        },
        //根据form数据 和 请求地址 导出数据到表格
        exportExcel: function (data, url) {
            var dataLink = JSON.stringify(data);
            var form = $('<form>');
            form.attr('style', 'display:none;');
            form.attr('target', '');
            form.attr('method', 'post');
            form.attr('action', url);
            var input1 = $('<input>');
            input1.attr('type', 'hidden');
            input1.attr('name', 'req');
            input1.attr('value', dataLink);
            $('body').append(form);
            form.append(input1);
            form.submit();
            form.remove();
        },
        //导出订单列表
        exportList: function (QuestionnaireID, QuestionnaireName) {
            var that = this;
            page.setCondition();
            var getUrl = '/ApplicationInterface/Module/Questionnaire/Questionnaire/DownLoadQuestionnaireInfor.ashx?method=DownLoadQuestionnaireInfor';//&req=';
            getUrl += "&ActivityID=" + that.elems.ActivityID;
            getUrl += "&QuestionnaireID=" + that.elems.QuestionnaireID;
            getUrl += "&filename=" + that.elems.QuestionnaireName + "--问卷明细";
            var data = {
                
            };
            this.exportExcel(data, getUrl);
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
                singleSelect: false, //单选
                // height : 332, //高度
                fitColumns: false, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                //collapsible : true,//可折叠
                //数据来源
                data: data.ResultData,
                //sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'ID', //主键字段
                /*  pageNumber:1,*/
                //frozenColumns:[[]],

                frozenColumns: [[{
                    field: 'ck',
                    width: 70,
                    title: '全选',
                    align: 'center',
                    checkbox: true
                } //显示复选框
                ]],
                columns: [columns],
                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    
                    if (data.rows.length > 0) {
                        $('#kkpager').show();
                    } else {
                        //that.elems.dataMessage.show();
                        $('#kkpager').hide();
                    }
                    //that.elems.tabel.datagrid('getSelected');

                    //$(".datagrid-view").css("overflow", "auto");
                    $(".datagrid-body").css("height", "100px");
                    

                }
                , onClickCell: function (rowIndex, field, value) {
                    if (field == "ck") {    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click = false;
                    } else {
                        that.elems.click = true;
                    }
                }
            });

            //循环判断数据列表是否存在样式问题
            that.elems.Timer = setInterval(function () {
                that.elems.Timercount++;
                if (that.elems.Timercount > 20) {
                    clearInterval(that.elems.Timer);
                }
                if (($(".datagrid-view").width() - $(".datagrid-wrap").width()) > 0) {
                    var width = $(".datagrid-wrap").width();
                    $(".datagrid-view").width(width);
                    $(".datagrid-view2").width((width - 40));
                    $(".datagrid-header").width((width - 40));
                    $(".datagrid-body").width((width - 40));
                    clearInterval(that.elems.Timer);
                }
            }, 300);

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
                                that.elems.QuestionnaireID = result.QuestionnaireID;
                                that.elems.QuestionnaireName = result.QuestionnaireName;

                                if (result.TitleData) {
                                    if (result.TitleData.length > 0) {
                                        for (var i = 0; i < result.TitleData.length; i++) {
                                            if (result.TitleData[i].NameID == "ID") {
                                                break;
                                            }
                                            columns += "{ field: '" + result.TitleData[i].NameID + "', title: '" + result.TitleData[i].Name + "',  resizable: true, align: 'center' },";
                                        }
                                    }
                                }
                                columns = "[" + columns + "]";
                                debugger;
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
        DelQuestionnaireAnswerRecord: function ( VipIDs) {
            var that = this;
            debugger;
            if (that.elems.ActivityID != null && that.elems.ActivityID != "") {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Questionnaire.QuestionnaireAnswerRecord.DelQuestionnaireAnswerRecord',
                        VipIDs: VipIDs,
                        ActivityID: that.elems.ActivityID
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            that.loadPageData();
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


