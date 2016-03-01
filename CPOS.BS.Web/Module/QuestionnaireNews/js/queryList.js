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
            operation: $('#operation'),              //弹出框操作部分
            dataMessage: $(".dataMessage"),
            click:true,
            panlH: 116,                        // 下来框统一高度
            domain: "",
            isshow:false                      //是否显示选择类型弹出框
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {

            this.initEvent();

            this.loadPageData();

            this.elems.isshow = JITMethod.getUrlParam("isshow");

           

        },
        submitsaveBtn: false,
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getQuestionnaireList(function (data) {
                   //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);
            });


            //点击新增按钮
            that.elems.sectionPage.delegate("#addUserBtn", "click", function (e) {
                debugger;
                var that = this;
                $('#win').window({
                    title: "选择类型", width: 580, height: 430, top: ($(window).height() - 430) * 0.5,
                    left: ($(window).width() - 580) * 0.5
                });

                $(".optionclass").show();

                $('#win').window('open')

            });


            //点击类型图片
            $(".SelectType").delegate("img", "mouseover", function (e) {
                debugger;
                $(".SelectType img").removeClass("selected");
                $(".SelectType img").each(function () {
                    $(this).attr("src", $(this).data('nochecked'));
                });
                $(this).addClass("selected");
                $(this).attr("src", $(this).data('checked'));

               

            });

            //点击类型图片
            $(".SelectType").delegate("img", "click", function (e) {
                
                var typeid = $(".SelectType .selected").data("typeid");
                var mid = getUrlParam('mid') ? "&mid=" + getUrlParam('mid') : "";
                var PMenuID = getUrlParam('PMenuID') ? "&PMenuID=" + getUrlParam('PMenuID') : "";
                if (typeid) {
                    $.util.toNewUrlPath("/Module/QuestionnaireNews/QuestionnaireDetail.aspx?type=" + typeid + mid );
                }

            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=32;
            $('#QuestionnaireType').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":1,
                    "text":"问答"
                },{
                    "id":2,
                    "text":"投票"
                },{
                    "id":3,
                    "text":"测试"
                },{
                    "id":4,
                    "text":"报名"
                }, {
                    "id": 0,
                    "text": "全部"
                }]
            });

            $('#QuestionnaireType').combobox('setValue', "0");

           
            /**************** -------------------弹出easyui 控件  End****************/
            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true
            });
            $('#panlconent').layout({
                fit: true
            });
            $('#win').delegate(".saveBtn", "click", function (e) {

                if (!that.submitsaveBtn) {
                    that.submitsaveBtn = true;
                    debugger;
                    var typeid = $(".SelectType .selected").data("typeid");
                    var mid = getUrlParam('mid')?"&mid="+getUrlParam('mid'):"";
                    var PMenuID = getUrlParam('PMenuID') ? "&PMenuID=" + getUrlParam('PMenuID') : ""; 
                    if (typeid) {
                        $.util.toNewUrlPath("/Module/QuestionnaireNews/QuestionnaireDetail.aspx?type=" + typeid + mid );
                    } else {
                        that.submitsaveBtn = false;
                        $.messager.alert('提示', '请选择问卷类型！');
                    }

                } else {

                    $.messager.alert('提示', '正在提交请稍后！');
                }

            });
            /**************** -------------------弹出窗口初始化 end****************/


            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".handle", "click", function (e) {
                debugger;
                var rowIndex = $(this).data("index");
                var optType = $(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if (optType == "delete") {
                    $.messager.confirm('删除', '您是否确定删除？', function (r) {
                        if (r) {
                            that.delQuestionnaire(row.QuestionnaireID, function (data) {
                                $.messager.alert("提示","操作成功！");
                            });
                            that.loadPageData(e);
                        }
                    });

                }
                if (optType == "edit") {
                    $.util.toNewUrlPath("/Module/QuestionnaireNews/QuestionnaireDetail.aspx?type=" + row.QuestionnaireType + "&mid=" + JITMethod.getUrlParam("mid") + "&QuestionnaireID=" + row.QuestionnaireID);
                    
                }

            });
            /**************** -------------------列表操作事件用例 End****************/

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
             //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                that.loadData.seach[filed.name] = filed.value;
            });




        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");

            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $.util.stopBubble(e)
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if (!data.Data.QuestionnaireList) {

                return;
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
                data: data.Data.QuestionnaireList,
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'QuestionnaireID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    { field: 'QuestionnaireName', title: '问卷名称', width: 100, align: 'left', resizable: false },
                    {
                        field: 'QuestionnaireType', title: '类别', width: 50, resizable: false, align: 'center'
                       , formatter: function (value, row, index) {
                           
                           var status
                           switch (value) {
                               case 1: status = "问答"; break;
                               case 2: status = "投票"; break;
                               case 3: status = "测试"; break;
                               case 4: status = "报名"; break;
                           }
                           return status;
                        }},
                   
                    {field : 'Status',title : '操作',width:50,align:'left',resizable:false,
                    formatter: function (value, row, index) {
                            var status = row.Status;
                            var optstr = "";
                            
                            optstr += '<p class="handle exit" title="编辑" data-index="' + index + '" data-oprtype="edit"></p>';
                            if (status == 1) {
                                optstr += '<p class="handle delete"  title="删除" data-index="' + index + '" data-oprtype="delete"></p>';
                            }
                            return optstr;
                        }
                    },


                ]],

                onLoadSuccess : function() {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.Data.QuestionnaireList.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                    }
                    if (that.elems.isshow) {
                        $('#win').window({
                            title: "选择类型", width: 580, height: 430, top: ($(window).height() - 430) * 0.5,
                            left: ($(window).width() - 580) * 0.5
                        });

                        $(".optionclass").show();

                        $('#win').window('open')
                    }
                },
                onClickRow:function(rowindex,rowData){
                 
                },onClickCell:function(rowIndex, field, value){
                 
                }

            });


            debugger;
            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex:1,
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


           
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getQuestionnaireList(function (data) {
                that.renderTable(data);
            });
        },
        delQuestionnaire: function (QuestionnaireID,callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'Questionnaire.Questionnaire.DelQuestionnaire',
                    QuestionnaireID: QuestionnaireID
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(data);
                        }

                    } else {
                        debugger;
                        alert(data.Message);
                    }
                }
            });
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
            seach:{
                QuestionnaireName:"",
                QuestionnaireType:0
            },
            getQuestionnaireList: function (callback)
            {
                debugger;
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'Questionnaire.Questionnaire.GetQuestionnaireList',
                        QuestionnaireName: that.seach.QuestionnaireName,
                        QuestionnaireType: that.seach.QuestionnaireType,
                        PageSize: that.args.PageSize,
                        PageIndex: that.args.PageIndex
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
            }
        }

    };
    page.init();
});

