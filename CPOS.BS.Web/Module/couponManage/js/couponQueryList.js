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
                that.loadData.GetCouponTypeList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.tabelWrap.delegate(".CouponEventBtn", "click", function (e) {
                var rowIndex = $(this).data("index");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.loadData.tag.CouponID = row.CouponID;
                $.messager.confirm("提示", "确定要核销优惠券吗？", function (r)
                {
                    if (r)
                    {
                        that.CouponEvent(that.loadData.tag.CouponID, 1);
                    }

                });
              
        
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
            $('#item_status').combobox({
                width: wd,
                height: H,
                panelHeight: that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data: [{
                    "id": 0,
                    "text": "未核销"
                }, {
                    "id": 1,
                    "text": "已核销"
                }, {
                    "id": -1,
                    "text": "全部"
                }]
            });

            $('#item_status').combobox('setValue', -1);

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
           
            /**************** -------------------弹出窗口初始化 end****************/

        },


        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start = 0;

            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            var fileds=$("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
               that.loadData.seach[filed.name] = filed.value;
            });





        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
            debugger;
            var data = { "WriteOffCouponList": [], "TotalPage": 1, "TotalCount": 0 };
            that.elems.dataMessage.html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.renderTable(data);

            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if (!data.WriteOffCouponList) {

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
                data: data.WriteOffCouponList,
                sortName: 'Status', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'CouponID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[
                    {
                        field: 'VipName', title: '会员姓名', width: 120, align: 'left', resizable: false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    { field: 'Phone', title: '手机号', width: 100, resizable: false, align: 'center' },
                    {
                        field: 'CouponName', title: '优惠券名称', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var long = 8;
                            var html = ""
                            if (value && value.length > long) {
                                html = '<div class="rowTextnew" title="' + value + '">' + value.substring(0, long) + '...</div>'
                            } else {
                                html = '<div class="rowTextnew">' + value + '</div>'
                            }

                         return html
                        }
                    },
                    { field: 'CouponCode', title: '优惠券号', width: 120, align: 'center', resizable: false },
                    { field: 'UnitName', title: '核销门店', width: 180, align: 'center', resizable: false },

                    {
                        field: 'VerifyDate', title: '核销日期', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value.replace("年", "-").replace("月", "-").replace("日", "");
                        }
                    },
                    { field: 'VerifyPerson', title: '核销人员', width: 100, align: 'center', resizable: false },
                    { field: 'ConponStatus', title: '状态', width: 80, align: 'center', resizable: false },


                    {
                        field: 'CouponID', title: '操作', width: 80, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            if (row.ConponStatus == "未核销") {
                                return "<div data-index=" + index + " data-flag='CouponEvent' class='CouponEventBtn'>核销</div>"
                            } else {
                                return "";
                            }
                        }
                    }


                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    } else {

                        that.elems.dataMessage.html("没有符合条件的记录");
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){
                     
                }

            });



            //分页

            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPage,
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
        CouponEvent: function (CouponID, Comment, callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Coupon/CouponEntry.ashx",
                data: {
                    "action": 'WriteOffCoupon',
                    "CouponID": CouponID,
                    "Comment": Comment 
                },
                success: function (data) {
                    debugger;
                    if (data.IsSuccess) {
                        $.messager.alert("提示", "成功核销优惠券!");
                        //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                        that.setCondition();
                        //查询数据
                        that.loadData.GetCouponTypeList(function (data) {
                            //写死的数据
                            //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                            //渲染table

                            that.renderTable(data);


                        });
                        $.util.stopBubble(e);


                    } else {
                        $('#win').window('close');
                        alert(data.msg);
                    }
                }
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
                CouponTypeID: ''
            },
            tag:{
                VipId:"",
                orderID: '',
                CouponID: ''
            },
            seach:{
                CouponTypeName:"",
                ParValue:"",

            },
            opertionField:{},

            GetCouponTypeList: function (callback) {
                debugger;
                $.util.ajax({
                    url: "/ApplicationInterface/Coupon/CouponEntry.ashx",
                      data:{
                          action: 'WriteOffCouponList',
                          Mobile: this.seach.Mobile,
                          CouponCode: this.seach.CouponCode,
                          Status: this.seach.Status,
                          PageIndex:this.args.PageIndex,
                          PageSize:this.args.PageSize

                      },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {

                            $("#pageContianer").find(".dataMessage").html("没有符合条件的记录");
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

