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

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.GetCouponTypeList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){
                var mid = JITMethod.getUrlParam("mid");
                $.util.toNewUrlPath("addCoupon.aspx?mid=" + mid);
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
                closable:true
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){

                if (!that.submitappcount) {
                    that.submitappcount = true;

                    if ($('#optionForm').form('validate')) {

                        var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                        that.loadData.operation(fields, that.elems.optionType, function (data) {

                            that.submitappcount = false;
                            $('#win').window('close');
                            alert("操作成功");

                            that.loadPageData(e);

                        });
                    }

                } else {

                    $.messager.alert('提示', '正在提交请稍后！');
                }

            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){
                debugger;
                var rowIndex=$(this).data("index");
                var optType = $(this).data("flag");
                var couponTypeID = $(this).data("typeid");
                var CouponTypeName = $(this).data("typename");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.loadData.args.CouponTypeID = row.CouponTypeID;
                if(optType=="add") {

                    that.addNumber(row);

                }

                if (optType == "Download") {
                    $.messager.confirm('提示', '是否下载?', function (r) {
                        if (r) {
                            debugger;
                            window.open("/ApplicationInterface/Module/Marketing/Coupon/DownLoadCouponTicketNumber.ashx?method=download_CouponTicketNumber&couponTypeID=" + couponTypeID + "&filename=" + CouponTypeName + "_优惠券", "_blank");
                        }
                    });

                }

                if(optType=="delete"){
                    if (row.BeginTime&&row.EndTime) {
                        var Begindate = Date.parse(new Date(row.BeginTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var Enddate = Date.parse(new Date(row.EndTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var now = new Date();
                        if (Begindate <= now && Enddate >= now) {
                            $.messager.alert("操作提示","优惠券在使用时间范围内不可删除");
                            return false;
                        }
                    }else{
                        $.messager.alert("操作提示","该优惠券是领取及时生效类型不可删除");
                        return false;
                    }
                    $.messager.confirm("删除优惠券操作","确认要删除该条记录",function(r){
                        if(r){
                            that.loadData.operation("",optType,function(){
                                alert("操作成功");
                                that.loadPageData()
                            })

                        }
                    })
                }
            })
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
            $('#win').window('open')
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
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.CouponTypeList){

                return;
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
                data:data.CouponTypeList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[

                    {field : 'CouponTypeName',title : '优惠券名称',width:300,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'ParValue',title : '面值',width:200,resizable:false,align:'center'},
                    {
                        field: 'ValidityPeriod', title: '有效期', width: 200, align: 'left', resizable: false
                        ,formatter:function(value ,row,index) {
                        if (!value) {
                            return new Date(value).format("yyyy-MM-dd") + "至" + new Date(row.EndTime).format("yyyy-MM-dd");
                        }else{
                            return row.ValidityPeriod
                        }
                    }
                    },
                    {field : 'IssuedQty',title : '数量',width:100,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return parseInt(value);
                            }
                        }},
                    {field : 'SurplusQty',title : '剩余量',width:100,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            } else {
                                var result = parseInt(value);
                                if (result > 0) {
                                    return result;
                                } else {
                                    return -result;
                                }
                            }
                        }},

                    {
                        field: 'addOptdel', title: '操作', width: 200, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            var str = "<div class='operation'><div data-index=" + index + " data-flag='add' class='btnAdd  opt' title='追加'> </div>";
                            str += "<div data-index=" + index + "  data-flag='Download' data-TypeName='" + row.CouponTypeName + "' data-TypeID='" + row.CouponTypeID + "' class='btnDownload  opt' title='下载'> </div>";
                            str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
                            return str
                        }
                    }


                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
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



            //分页

            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPageCount,
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
            that.loadData.GetCouponTypeList(function(data){
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

            GetCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        CouponTypeName:this.seach.CouponTypeName,
                        ParValue:this.seach.ParValue,
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSize

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

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法

                if(this.args.CouponTypeID.length>0){
                    prams.data.CouponTypeID=this.args.CouponTypeID;
                }else{
                    alert("优惠券类型ID无效");
                    return false;

                }
                if(pram.length>0) {
                    $.each(pram, function (index, filed) {
                        if(filed.value!==""){
                            prams.data[filed.name]=filed.value;
                        }
                    });
                }
                switch(operationType){
                    case "add":prams.data.action="Marketing.Coupon.SetCoupon";  // 追加
                        break;
                    case "delete":prams.data.action="Marketing.Coupon.DelCouponType";  //删除
                        break;
                }


                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
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

