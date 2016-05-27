define(['jquery', 'template', 'tools', 'kkpager','easyui', 'artDialog','datetimePicker','tips','zTree'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#dataTable"),                   //表格body部分
            tableDiv:$("#dataTable").parents(".tableWrap"),
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            vipSourceId:''
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var that = this;

			//绑定事件
            that.initEvent();
            //显示表格信息
            that.loadPageData();
        },
		//加载页面的数据请求
        loadPageData: function () {
            var that = this;
            $('.queryBtn').trigger("click")
			
        },
        renderTable:function(){
            var that=this;
            $.util.partialRefresh(that.elems.tabel);
            that.loadData.GetSellerMonthRewardList(function(data){
                debugger;
                if(!data.Data.SellerMonthRewardList){
                    return;
                }
                //jQuery easy datagrid  表格处理
                that.elems.tabel.datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : true, // 单选
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.Data.SellerMonthRewardList,
                    //sortName : 'brandCode', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    columns : [[
                        {field : 'YearAndMonth',title : '年/月 ',width:111,align:'left',resizable:false},
                        {field : 'user_name',title : '姓名 ',width:301,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },


                        {field : 'user_telephone',title : '手机号 ',width:111,align:'left',resizable:false},

                        {field : 'MonthAmount',title : '奖励(元)',width:100,align:'left',resizable:false},

                        {field : 'UnitName',title : '所属门店',width:281,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=16;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
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




                if (data.Data.TotalPages >0) {
                    kkpager.generPageHtml(
                        {
                            pno:  that.loadData.args.PageIndex,
                            mode: 'click', //设置为click模式
                            //总页码
                            total: data.Data.TotalPages,
                            totalRecords:data.Data.TotalCount,
                            isShowTotalPage: true,
                            isShowTotalRecords: true,
                            //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                            //适用于不刷新页面，比如ajax
                            click: function (n) {
                                //这里可以做自已的处理
                                //...
                                //处理完后可以手动条用selectPage进行页码选中切换
                                this.selectPage(n);
                                //点击下一页或者上一页 进行加载数据
                                that.loadMoreData(n);
                            },
                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                            getHref: function (n) {
                                return '#';
                            }

                        }, true);

                }
            });
        },
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;

            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.renderTable();

        },




        initEvent: function () {
            var that=this;

          //  that.loadData.get_unit_tree(function(data) {
              //  debugger;
                //that.loadData.getUitTree.node=data[0].id;
                that.loadData.get_unit_tree(function(datas) {
                    debugger;
                  //  data[0].children=datas;
                    that.unitTree=datas;
                    $("#unitTree").combotree({
                        panelWidth:220,
                        multiple:false,
                        valueField: 'id',
                        textField: 'text',
                        data:datas
                    });
                    $("#unitTree").combotree("setText","请选择门店");
                    var year=new Date().getFullYear();
                    var mouth=new Date().getMonth()+1;
                    debugger;
                    $("#Year").combobox("select",year);
                    $("#Month").combobox("select",mouth);
                })
           // });

			//绑定搜索按钮事件
			$('.queryBtn').on('click',function(){
                debugger;
             var fileds =$("#queryFrom").serializeArray();
                $.each(fileds,function(index,filed){
                  that.loadData.seach[filed.name] = filed.value;
                });

                $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
                that.renderTable();
			});

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


            $("#Year").combobox({//
                valueField:'id',
                textField:'text',
                width:200,
                height:32,
                data:[/*{
                 "id":"0",
                 "text":"全部",
                 "selected":true
                 },*/{
                    "id":2015,
                    "text":"2015年"
                },{
                    "id":2016,
                    "text":"2016年"

                },{
                    "id":2017,
                    "text":"2017年"

                },{
                    "id":2018,
                    "text":"2018年"

                },{
                    "id":2019,
                    "text":"2019年"

                },{
                    "id":2020,
                    "text":"2020年"

                }]
            });
            $("#Month").combobox({
                valueField:'id',
                textField:'text',
                width:200,
                height:32,
                data:[ /*{
                 "id":"0",
                 "text":"全部" ,
                 "selected":true
                 },*/{
                    "id":1,
                    "text":"1月"
                },{
                    "id":2,
                    "text":"2月"
                },{
                    "id":3,
                    "text":"3月"
                },{
                    "id":4,
                    "text":"4月"
                },{
                    "id":5,
                    "text":"5月"
                },{
                    "id":6,
                    "text":"6月"
                },{
                    "id":7,
                    "text":"7月"
                },{
                    "id":8,
                    "text":"8月"
                },{
                    "id":9,
                    "text":"9月"
                },{
                    "id":10,
                    "text":"10月"
                },{
                    "id":11,
                    "text":"11月"
                },{
                    "id":12,
                    "text":"12月"
                }]
            });

            var year=new Date().getFullYear();
            var mouth=new Date().getMonth()+1;
            debugger;
            $("#Year").combobox("select",year);
            $("#Month").combobox("select",mouth);

            /**************** -------------------弹出窗口初始化 end****************/

		    that.elems.tableDiv.delegate(".fontC","click",function(){
                var me=$(this),optType=me.data("opttype"),index=me.parent().data("index");
                that.elems.tabel.datagrid("selectRow",index);
                var row= that.elems.tabel.datagrid("getSelected");
                switch (optType){
                    case "setReward":
                         that.setReward(row);

                        break; //  设置奖励
                    case "status":
                        var status=row.Status==1?"你确认停用该分销商":"你确认启用该分销商";
                        $.messager.confirm("操作提示",status,function(r){
                               if(r){
                                    that.loadData.operation(row,optType,function(){
                                       alert("操作成功");
                                        that.renderTable();
                                    });
                               }
                        });

                        break;//改变状态停用和启用
                    case "down":
                       // window.location.href=row.QRImageUrl;
                        new Image().src=row.QRImageUrl;
                       //that.saveit('images/duihao.png');

                        //that.saveit(downloadFile);
                        that.downloadFile('1111111.jpg',row.QRImageUrl);
                        break;
                }

            });

			

        },

        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize:10,
                RetailTraderID:'',
                bat_id:"1",
                CooperateType:"OneWay",
                OrderBy:"",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'
            },
            seach:{
                SellerOrRetailName:"" ,   //姓名
                Year:"" ,   //年份
                Month:"" ,   //月份
                UnitID:""
            },
            getUitTree:{
                node:""
            },
            opertionField:{},

            //获取列表
            GetSellerMonthRewardList: function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/AllWin/RetailTrader.ashx",
                    data: {
                        action: 'GetSellerMonthRewardList',
                        PageSize: this.args.PageSize,
                        PageIndex: this.args.PageIndex,
                        SellerOrRetailName:this.seach.SellerOrRetailName,
                        Year:this.seach.Year,
                        UnitID:this.seach.UnitID,
                        Month: this.seach.Month
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if(callback){
                                callback(data);
                            }

                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取门店
            get_unit_tree: function (callback) {
                $.ajax({  // &node="+this.getUitTree.node+"
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx?method=get_unit_tree&parent_id=&_dc=1433225205961&multiSelect=false&isSelectLeafOnly=false&isAddPleaseSelectItem=false&pleaseSelectText=--%E8%AF%B7%E9%80%89%E6%8B%A9--&pleaseSelectID=-2",
                    success: function (data) {
                        debugger;
                        data=JSON.parse(data);
                        if (data&&data.length>0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            }
            /*operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/AllWin/RetailTrader.ashx";
                //根据不同的操作 设置不懂请求路径和 方法



                switch(operationType){
                    case "status":prams.data.action="ToggleRetailStatus";  //停用启用
                        prams.data=pram;
                        prams.data.Status=pram.Status==0?"1":"0";
                        break;
                    case "setReward":
                        prams={data:{action:"SaveRetailRewardRule"}};
                        prams.url="/ApplicationInterface/AllWin/SysRetailRewardRule.ashx";
                        //根据不同的操作 设置不懂请求路径和 方法

                        prams.data["IsTemplate"]="0";    //模板保存
                        var SysRetailRewardRuleList=[
                            {"RewardTypeName":"首次关注奖励","RewardTypeCode":"FirstAttention","AmountOrPercent":"1"},
                            {"RewardTypeName":"首笔交易奖励","RewardTypeCode":"FirstTrade","AmountOrPercent":"2"},
                            {"RewardTypeName":"会员关注3个月内消费获得奖励","RewardTypeCode":"AttentThreeMonth","AmountOrPercent":"2"}
                        ];

                        prams.data["CooperateType"]=$("[data-cooperatetype].show").data("cooperatetype");
                        $.each(pram, function (index, field) {
                            //   "SellUserReward":"5","RetailTraderReward":"5",
                            for(var i=0;i<SysRetailRewardRuleList.length;i++){
                                var item= SysRetailRewardRuleList[i];
                                if(field.name.indexOf(item.RewardTypeCode)!=-1){    //确定是那个类别的
                                    if(field.name.indexOf("SellUserReward")!=-1) {         //不同类别的销售员奖励
                                        SysRetailRewardRuleList[i]["SellUserReward"]=field.value;
                                    }
                                    if(field.name.indexOf("RetailTraderReward")!=-1) {  //不同类别的分销商奖励
                                        SysRetailRewardRuleList[i]["RetailTraderReward"]=field.value;
                                    }
                                    if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同分销商的不同类别奖励模板ID
                                        SysRetailRewardRuleList[i]["RetailRewardRuleID"]=field.value;
                                    }
                                }

                            }
                        });
                        prams.data["SysRetailRewardRuleList"]= SysRetailRewardRuleList;

                        prams.data["RetailTraderID"]=this.args.RetailTraderID;
                        break;
                    case "sales":prams.data.action="UpdateSalesPromotion";  //更改促销分组
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
            }*/


        }
    };
    page.init();
});