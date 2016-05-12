define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'kindeditor'], function ($) {

    KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            editLayer: $("#editLayer"), //CSV文件上传
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#operation'),              //弹出框操作部分
            vipSourceId: '',
            click: true,
            VipCardCode:'',
            dataMessage: $(".dataMessage"),
            panlH: 116                           // 下来框统一高度
        },
        detailDate: {},
        ValueCard: '',//储值卡号
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                             //标签类型
            tagList: []                              //标签列表
        },
        init: function () {
            var that = this;
            $(".loading").css({ width: $("#pageContianer").width() + "px" });
            $("#leftMenu").find("li").removeClass("on").each(function(){
                if( $(this).find("em").hasClass("newVipManage_querylist")){
                    $(this).addClass("on");
                }
            });
            this.initEvent();

           

            this.loadPageData();
            debugger;
            $("[name='VipCardCode']").focus();

            //导入会员初始化
            setTimeout(function () {
                that.inportStoreDialog();

            }, 500);

            that.registerUploadCSVFileBtn();
        },
        initEvent: function () {

            var that = this;

            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {

                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();

                if ($(this).data("flag")) {
                    $("#win").window("open");
                    return false;
                }
                if ($("[name='VipCardCode']").val()) {
                    if (that.loadData.args.type ==1) {
                        that.loadData.getVipCardStatus(function (data) {
                            //未激活没有门店信息
                            debugger;
                            if (data.Data && data.Data.VipCardStatusId == 0 && !data.Data.MembershipUnitName) {
                             /*   that.elems.dataMessage.html("此卡未开卡,找不到会员记录");
                                that.elems.tabel.datagrid({data:[]});0015135569*/

                                $.messager.alert("提示", "此卡未开卡,找不到会员记录","",function(){
                                    $("[name='VipCardCode']").focus();
                                    $(".loading").remove();
                                    that.loadData.args.type = 2;//回归点击查询

                                });
                            } else if(data.Data &&data.Data.VipID&&data.Data.VipCardID){

                                location.href = "VipDetail.aspx?vipId=" + data.Data.VipID + "&VipCardId="+data.Data.VipCardID+"&mid=" + $.util.getUrlParam("mid");

                            } else{
                                $.messager.alert("提示", "此卡此卡数据异常,请联系管理员","",function(){
                                    $("[name='VipCardCode']").focus();
                                    $(".loading").remove();
                                    that.loadData.args.type = 2;//回归点击查询

                                });
                            }
                        });
                    } else{
                        that.loadData.getVipList(function (data) {
                            //写死的数据
                            //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                            //渲染table

                            that.renderTable(data);


                        });
                    }
                }else{
                    that.loadData.getVipList(function (data) {
                        //写死的数据
                        //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                        //渲染table

                        that.renderTable(data);


                    });
                }


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
            var wd = 160, H = 32;
            /*case 0 : staus="未激活";break;
             case 1 : staus= "正常";break;
             case 2 : staus= "冻结";break;
             case 3 : staus= "失效";break;
             case 4 : staus= "挂失";break;
             case 5 : staus="休眠";break;*/
            $("body").delegate("input[name='VipCardCode']","keydown",function(e){

                if(e.keyCode==13){
                    debugger;
                    var str=$(this).val().replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"");
                      //如果特殊字符被替换，表示存在
                       that.loadData.args.type=1;



                    if(that.elems.vipCardCode) {
                        str= str.replace(that.elems.vipCardCode,"");
                        that.elems.vipCardCode=str;
                    } else{
                        that.elems.vipCardCode=str;
                    }


                    $.util.stopBubble(e);
                    $(this).focus();
                    $(this).val(str);
                    $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
                }

            });


            $('.datebox').datebox({
                width: 160,
                height: H
            });



            //导入会员
            $('#inportvipmanageBtn').on('click', function () {

                $('.inputCount').text("0");

                $('#CSVFileurl').val("");
                that.elems.editLayer.find("#nofiletext").show();
                that.elems.editLayer.find(".CSVFilelist").empty();

                $('#startinport').show();
                $('#closebutton').hide();

                $('.step').hide();
                $('#step1').show();

                $('#win1').window('open');
            });

            //开始导入
            $('#win1').delegate(".saveBtn", "click", function (e) {
                var CSVFileurl = $('#CSVFileurl').val();
                if (CSVFileurl != "") {
                    $('#startinport').hide();
                    $('.step').hide();
                    $('#step2').show();
                    that.inportEvent();
                } else {
                    $.messager.alert("提示", '请选择文件！');
                }
            });

            //关闭
            $('#win1').delegate(".closeBtn", "click", function (e) {

                $('#win1').window('close');
            });

        },
        /*导入start*/

        //执行导入
        inportEvent: function () {
            var that = this;
            var mid = JITMethod.getUrlParam("mid");
            $.util.oldAjax({
                url: "/Module/Basic/VIP/Handler/VipHandler.ashx",
                data: {
                    "mid": $.util.getUrlParam('mid'),
                    "action": 'ImportVip',
                    filePath: '/' + $('#CSVFileurl').val()
                },
                success: function (data) {
                    $('.step').hide();
                    $('#step3').show();
                    $('#closebutton').show();

                    if (data.success) {
                        $('#inputTotalCount').text(data.data.TotalCount);
                        var success = 0;
                        success = data.data.TotalCount - data.data.ErrCount;
                        $('#inputErrCount').text(success < 0 ? 0 : success);
                        if (data.data.ErrCount > 0 || data.data.TotalCount == 0) {
                            $('#error_report').attr("href", data.data.Url);
                            $('.menber_centernrb1').show();
                        } else {
                            $('.menber_centernrb1').hide();
                        }
                    } else {
                        if (data.data != null) {
                            $('#error_report').attr("href", data.data.Url);
                            $('.menber_centernrb1').show();
                        } else {
                            $('.menber_centernrb1').hide();
                            $.messager.alert("导入错误提示", data.msg);
                        }
                    }
                    that.loadPageData();

                }
            });
        },
        //导入窗口第一步
        inportStoreDialog: function (data) {
            var that = this;


            $('#win1').window({ title: "导入会员", width: 697, height: 600, top: 15, left: ($(window).width() - 550) * 0.5 });

            // $('#win').window('open');
        },

        //CSV文件上传按钮绑定
        registerUploadCSVFileBtn: function () {
            var self = this;
            // 注册上传按钮
            self.elems.editLayer.find(".uploadCSVFileBtn").each(function (i, e) {
                self.addUploadCSVFileEvent(e);
            });
        },
        //上传CSV文件区域的各种事件绑定
        addUploadCSVFileEvent: function (e) {
            var self = this;
            var CSVFilelist = self.elems.editLayer.find(".CSVFilelist");


            //上传CSV文件并显示
            self.uploadCSVFile(e, function (ele, data) {
                CSVFilelist.empty();
                CSVFilelist.append('<a id="fileurl" href="' + data.file.url + '" >' + data.file.name + '</a>');
                $('#CSVFileurl').val(data.file.localurl);

            });

        },
        //上传CSV文件
        uploadCSVFile: function (btn, callback) {
            var self = this;
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width: 100,
                //上传的文件类型
                fieldName: 'file',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Upload/UploadFile.ashx?method=file',
                afterUpload: function (data) {
                    debugger;
                    if (data.file.extension.toLocaleLowerCase() == ".xls" || data.file.extension.toLocaleLowerCase() == ".csv" || data.file.extension.toLocaleLowerCase() == ".xlsx") {
                        if (data.file.size < 1000001) {

                            if (data.success) {
                                self.elems.editLayer.find("#nofiletext").hide();
                                if (callback) {
                                    debugger;
                                    callback(btn, data);
                                }
                                //取返回值,注意后台设置的key,如果要取原值
                                //取缩略图地址
                                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                                //取原图地址
                                //var url = KE.formatUrl(data.url, 'absolute');
                            } else {
                                $.messager.alert(data.msg);
                            }
                        } else {
                            $.messager.alert("提示", "请上传要小于1M的文件！");
                        }
                    } else {

                        $.messager.alert("提示", "上传的文件格式只能是.xls、.xlsx！");
                    }
                },
                afterError: function (str) {
                    $.messager.alert("提示", '自定义错误信息: ' + str);
                }
            });
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },


        /*导入end*/

        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getVipList(function (data) {
                that.renderTable(data);
            });
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition: function(){
             var that=this;
            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            debugger;

            //查询每次都是从第一页开始
            that.loadData.args.PageIndex = 1;
            that.loadData.args.SearchColumns={};
            if ($("#seach").form("validate")) {
                var fileds = $("#seach").serializeArray();
                $.each(fileds, function (i, filed) {
                    if (filed.name == "VipCardStatusId") {
                        if (filed.value == -1) {
                            filed.value = '';
                        }

                    }

                });
            }

            that.loadData.args.SearchColumns = fileds;


        },

        //加载页面的数据请求
        loadPageData: function (e) {

            this.renderTable({Data: {VipInfoList: null}}, true);

        },

        //渲染tabel
        renderTable: function (data, fistLoad) {
            debugger;
            var that = this;
            that.loadData.args.type=2;
            $("[name='VipCardCode']").focus();

            if (!data.Data.VipInfoList) {

                data.Data.VipInfoList = [];

            }
            if(data.Data.VipInfoList.length==1){
                var rowData= data.Data.VipInfoList[0];
                if(rowData.VIPID||rowData.VipCardID) {
                    $.util.toNewUrlPath("VipDetail.aspx?vipId=" + rowData.VIPID + "&VipCardId=" + rowData.VipCardID)
                  console.log("此卡没有绑定会员信息")
                }else{
                    data.Data.VipInfoList=[];
                }
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
                data: data.Data.VipInfoList,
                /*sortName : 'MembershipTime', //排序的列*/
                sortOrder: 'desc', //倒序
                remoteSort: true, // 服务器排序
                idField: 'OrderID', //主键字段

                columns: [[

                    {field: 'VipCardCode', title: '会员编号', width: 96, align: 'left', resizable: false},
                    {
                        field: 'VipRealName', title: '会员姓名', width: 82, align: 'left', resizable: false,
                        formatter:function(value ,row,index){
                            var long=26;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {
                        field: 'VipName', title: '昵称', width: 82, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            var long = 26;
                            if (value && value.length > long) {
                                return '<div class="rowText" title="' + value + '">' + value.substring(0, long) + '...</div>'
                            } else {
                                return '<div class="rowText">' + value + '</div>'
                            }
                        }
                    },
                    {field: 'Phone', title: '手机号', width: 82, align: 'left', resizable: false},
                    {field: 'Gender', title: '性别', width: 46, align: 'left', resizable: false,
                        formatter:function(value ,row,index){
                            var str="";
                            switch (value){
                                case 1:str="男" ;break;
                                case 2:str="女" ;break;


                            }
                            return str;
                        }
                    },
                    {field: 'VipCardTypeName', title: '会员卡名称', width: 82, align: 'left', resizable: false},
                    {field: 'VipCardStatusId', title: '会员卡状态', width: 66, resizable: false, align: 'left',
                    //-1废卡，0未激活，1正常，2冻结，3失效，4挂失，5休眠
                        formatter:function(value ,row,index){
                           var str="";
                            switch (value){
                                case -1:str="<p class='fontColor'>废卡</p>" ;break;
                                case 0:str="<p class='fontColor'>未激活</p>" ;break;
                                case 1:str="正常" ;break;
                                case 2:str="<p class='fontColor'>已冻结</p>" ;break;
                                case 3:str="<p class='fontColor'>已失效</p>" ;break;
                                case 4:str="<p class='fontColor'>已挂失</p>" ;break;
                                case 5:str="<p class='fontColor'>已休眠</p>" ;break;

                            }
                            return str;
                        }
                    },
                    {field: 'MembershipUnitName', title: '会籍店', width: 66, resizable: false, align: 'left',
                     formatter:function(value ,row,index){
                         var long=26;
                         if(value&&value.length>long){
                             return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                         }else{
                             return '<div class="rowText">'+value+'</div>'
                         }
                     }
                    },
                    {
                        field: 'MembershipTime',
                        title: '注册时间',
                        width: 106,
                        align: 'center',
                        resizable: false
                    }

                ]],

                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {

                        that.elems.dataMessage.hide();
                    } else {
                        if (fistLoad) {
                            that.elems.dataMessage.html("请刷卡或者输入你需要查询的信息，点击查询");
                        } else {
                            that.elems.dataMessage.html("没有符合条件的记录");
                        }
                        that.elems.dataMessage.show();
                    }

                },
                 onClickRow:function(rowindex,rowData){
                 debugger;
                 if(that.elems.click){
                     that.elems.click=true;
                     location.href = "VipDetail.aspx?vipId=" + rowData.VIPID + "&VipCardId="+rowData.VipCardID+"&mid=" + $.util.getUrlParam("mid");
                 }

                 },onClickCell:function(rowIndex, field, value){
                 if(field=="OrderID"){  //有复选框，或者操作列的时候用到
                 that.elems.click=false;
                 }else{
                 that.elems.click=true;
                 }
                 }
            });


            if(fistLoad) {  return false;}
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

        },



        loadData: {
            args: {
                PageIndex: 1,
                PageSize: 10,
                SearchColumns: {},    //查询的动态表单配置
                OrderBy: "",           //排序字段
                type:2,  // 1 卡内码, 2会员编号
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status: -1
            },
            tag: {
                VipId: "",
                orderID: ''
            },
            getUitTree: {
                node: ""
            },
            opertionField: {},
            getVipList: function (callback) {
                var prams = {data: {action: ""}};

                prams.data = {
                    action: "VIP.VipManager.GetVipList",
                    PageSize: this.args.PageSize,
                    PageIndex: this.args.PageIndex
                };
                $.each(this.args.SearchColumns, function (i, field) {
                    if (field.value) {
                        prams.data[field.name] = field.value; //提交的参数
                    }

                });
                prams.data["VipTypeID"]= this.args.type;

                //每一次查完//归类到点击查询；

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,

                    success: function (data) {
                        debugger;
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示", data.Message);
                            page.loadData.args.type = 2;
                        }
                    }
                });


            },

            getVipCardStatus: function (callback) {

                var prams = {data: {action: ""}};

                prams.data = {
                    action: "VIP.VIPCard.GetVipCardDetail",

                };
                $.each(this.args.SearchColumns, function (i, field) {
                    if (field.name=="VipCardCode"&&field.value) {
                        prams.data["VipCardISN"] = field.value; //提交的参数
                    }

                });
                debugger;

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: prams.data,

                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示", data.Message);
                             page.loadData.args.type = 2;
                        }
                    }
                });


            }
        }

    };
    page.init();
});

