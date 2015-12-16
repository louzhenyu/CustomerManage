define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
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
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
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
                that.loadData.getHomePageList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){

                   that.loadTemplate();





            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;


            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                onOpen:function(){
                    debugger;
                  $("#win").find(".radio").eq(0).trigger("click");
                },
                closable:true
            });
            $('#win1').window({
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
            $('#win,#winT').delegate(".saveBtn","click",function(e){
                    that.loadData.getHomePageTemplate(function(data) {
                        if (data.TemplateList.length > 0) {
                            var isAdd = true
                            for (var i = 0; i < data.TemplateList.length; i++) {
                                var row = data.TemplateList[i];
                                var dataDom = $("#win").find(".imgPanel .radio.on").data();
                                if (row.DisplayIndex == dataDom.index) {
                                    isAdd = false;
                                    location.href = "/module/AppConfig/homePageDetail.aspx?mid=" + $.util.getUrlParam("mid") + "&homeId=" + row.HomeId + "&optionType=Add"
                                }
                            }
                            if (isAdd) {
                                $.messager.alert("提示", "没有对应的模板，请联系管理员");
                            }
                        } else {
                            $.messager.alert("提示", "没有模板数据，请联系管理员");
                        }

                    });

             /*   if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,that.elems.optionType,function(data){
                        $('#win').window('close');
                        alert("操作成功");

                        that.loadPageData(e);

                    });
                }*/



            }).delegate(".radio","click",function(e){
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                    me.parents(".imgPanel").parent().find(".show").hide();
                    me.parents(".imgPanel").find(".show").show()
                }
                $.util.stopBubble(e);
            }).delegate(".radio img","click",function(e){
                debugger;
                var index=$(this).data("index");

                var item={src:"images/template0"+index+".jpg"};
                that.preview(item);

            }).delegate(".imgPanel","click",function(){
               $(this).find(".radio").trigger("click");

            }) ;
            $("#winT").delegate(".imgOpt div","click",function(){
                debugger;
                var dom= $(this).parent();
                    $(this).parent().find("div").removeClass("on");
                    $(this).addClass("on");
                  var multiple=dom.data("multiple");
                  if($(this).hasClass("magnify")){
                      if(multiple<1){
                          multiple=multiple/0.8;
                          multiple=multiple>1?1:multiple;
                      }else{
                          multiple=1;
                      }
                      dom.data("multiple",multiple);
                      var width=multiple*100+"%";

                      $(".imgBody").find("img").css({"width":width});
                  }
                 if($(this).hasClass("shrink")) {
                     if($(".imgBody").height()>442){
                         multiple=multiple*0.8;
                         dom.data("multiple",multiple);
                         var width=multiple*100+"%";

                         $(".imgBody").find("img").css({"width":width});
                     }


                 }
                dom.find("span").html("1:"+(multiple==1?"1":multiple.toFixed(2)))

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
                if(optType=="ChangeStatus") {
                    that.loadData.operation(row,optType,function(){
                        alert("操作成功");
                        that.loadPageData()
                    })


                }
                if(optType=="exit"){
                    location.href="/module/AppConfig/homePageDetail.aspx?mid="+ $.util.getUrlParam("mid")+"&homeId="+row.HomeId+"&optionType=Edit"
                }

                if(optType=="delete"){

                    $.messager.confirm("提示","确认删除主页吗",function(r){
                             if(r){
                                 that.loadData.operation(row,optType,function(){
                                     alert("操作成功");
                                     that.loadPageData()
                                 })

                             }
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

            $('#win').window('open');
            $('#win').find(".radio").eq(0).trigger("click");
        },
        //模板加载
        loadTemplate:function(data){
            debugger;
            var that=this;
            that.elems.optionType="template";

            var top=$(document).scrollTop()+30;
            $('#win').window({title:"请选择主页模板",width:860,height:600,top:top,left:($(window).width()-860)*0.5});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addTemplate',data);
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open')
        },
        //预览
        preview:function(data){
            debugger;
            var that=this;
            that.elems.optionType="template";

            var top=$(document).scrollTop()+30;
            $('#winT').window({title:"模板详情",width:860,height:580,top:top,left:($(window).width()-860)*0.5});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panelImgList').layout('remove','center');
            var html=bd.template('tpl_Template',{item:data});
            var options = {
                region: 'center',
                content:html
            };
            $('#panelImgList').layout('add',options);
            $('#winT').window('open')
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
               that.loadData.seach[filed.name] = filed.value;
            });





        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.getHomePageList(function(data){
                //写死的数据
                //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                //渲染table

                that.renderTable(data);


            });
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.moblieHomeList){

                data.moblieHomeList=[];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //单选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data:  data.moblieHomeList,
                sortName: 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns: [[

                    {
                        field: 'Title', title: '名称', width: 200, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var long = 56;
                            if (value && value.length > long) {
                                return '<div class="rowText" title="' + value + '">' + value.substring(0, long) + '...</div>'
                            } else {
                                return '<div class="rowText">' + value + '</div>'
                            }
                        }
                    },

                    {
                        field: 'CreateTime', title: '创建时间', width: 100, align: 'center', resizable: false
                        , formatter: function (value, row, index) {
                        /*   if (!value) {
                         return new Date(value).format("yyyy-MM-dd") + "至" + new Date(row.EndTime).format("yyyy-MM-dd");
                         }else{
                         return row.ValidityPeriod
                         }*/
                        return value;
                    }
                    },

                    {
                        field: 'HomeId', title: '编辑', width: 60, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return "<div data-index=" + index + " data-flag='exit' class='exit opt'></div>"
                        }
                    },
                    {
                        field: 'IsTemplate', title: '删除', width: 60, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            if(row.IsActivate==1) {
                                return "<div data-index=" + index + " data-flag='noDelete' class='noDelete opt'></div>"

                            } else{
                                return "<div data-index=" + index + " data-flag='delete' class='delete opt'></div>"
                            }
                        }
                    },
                    {
                        field: 'IsActivate', title: '操作', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            if(value==1){
                                return "<div data-index=" + index + " data-flag='ChangeStatus' class='noOpt'>当前主页</div>"

                            }else{
                                return "<div data-index=" + index + " data-flag='ChangeStatus' class='opt'>设为主页</div>"
                            }

                        }
                    }

                ]],

                onLoadSuccess: function (data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow: function (rowindex, rowData) {

                }, onClickCell: function (rowIndex, field, value) {
                    if (field == "addOpt" || field == "addOptdel") {    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click = false;
                    } else {
                        that.elems.click = true;
                    }
                }

            });



            //分页
             if(data.moblieHomeList.length>0) {
                 $("#pageContianer").show();
                 kkpager.generPageHtml({
                     pno: that.loadData.args.PageIndex,
                     mode: 'click', //设置为click模式
                     //总页码
                     total: data.pageCount,
                     totalRecords: data.totalCount,
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

             }else{
                 $("#pageContianer").hide();
             }
            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getHomePageList(function(data){
                that.renderTable(data);
            });
        },



        loadData: {
            args: {
                bat_id:"1",
                PageIndex:1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                CouponTypeID:''
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                CouponTypeName:"",
                ParValue:"",

            },
            opertionField:{},

            getHomePageList: function (callback) {
                $.util.oldAjax({
                    url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
                      data:{
                          action:'GetHomePageListByCustomer',
                          PageSize:this.args.PageSize,
                          PageIndex:this.args.PageIndex
                      },
                    success: function (data) {
                        debugger;
                        if (data.success) {
                            if (callback) {
                                callback(data.data);
                            }
                        } else {
                            $.messager.alert("提示",data.msg);
                        }
                    }
                });
            },
            getHomePageTemplate: function (callback) {
                $.util.oldAjax({
                    url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
                    data:{
                        action:'GetHomePageTemplate',
                    },
                    success: function (data) {
                        debugger;
                        if (data.success) {
                            if (callback) {
                                callback(data.data);
                            }
                        } else {
                            $.messager.alert("提示",data.msg);
                        }
                    }
                });
            },

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/Module/AppConfig/Handler/HomePageHandler.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                switch(operationType){
                    case "ChangeStatus":
                        prams.data.action="ChangeStatus";
                        prams.data["homeId"]=pram.HomeId;
                        break;
                    case "delete":  //删除
                        prams.data.action="DeleteHomePage";
                        prams.data["homeId"]=pram.HomeId;
                        break;
                }
                $.util.oldAjax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.success) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.msg);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

