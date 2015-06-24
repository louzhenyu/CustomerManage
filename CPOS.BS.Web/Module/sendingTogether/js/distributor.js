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
			//显示表格信息
            that.loadPageData();
			//绑定事件
            that.initEvent();
        },
		//加载页面的数据请求
        loadPageData: function () {
            var that = this;
           that.renderTable();
			
        },
        renderTable:function(){
            var that=this;
            that.loadData.getRetailTraders(function(data){
                debugger;
                if(!data.Data.RetailTraderList){
                    data.Data.RetailTraderList=[];
                    that.elems.tabel.datagrid({data:data.Data.RetailTraderList});
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
                    data:data.Data.RetailTraderList,
                    //sortName : 'brandCode', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    columns : [[

                        {field : 'RetailTraderID',title : '操作',width:200,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                               var status=row.Status==1?"停用":'启用'; //操作按钮文字 如果为1 分销商是正常 需要停用。反之亦然
                                return '<div class="rowText" data-index="'+index+'"> <em class="fontC"data-opttype="status">'+status+'</em> <em class="fontC" data-opttype="setReward">设置奖励</em></div>'
                            }
                        },
                        {field : 'RetailTraderName',title : '商家名称',width:301,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },

                        {field : 'RetailTraderAddress',title : '地址',width:301,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },

                        {field : 'RetailTraderMan',title:"联系人",width:200,align:'center',resizable:false},
                        {field : 'VipCount',title : '会员数',width:100,align:'center',resizable:false},
                        {field : 'EndAmount',title : '余额(元)',width:100,align:'center',resizable:false},
                        {field : 'CooperateType',title : '活动类型',width:100,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                var status="";
                                switch (value){
                                    case "TwoWay": status="互相集客" ;break;
                                    case "OneWay" : status="单向集客" ;break;
                                }
                                return status;
                            }
                        },
                        {field : 'UnitName',title : '所属门店',width:281,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=16;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                         }
                        },
                        {field : 'Status',title : '状态',width:100,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                var status="";
                                switch (value){
                                    case "0": status="停用" ;break;
                                    case "1" : status="正常" ;break;
                                }
                                return "<p style='color: #fc7a52'> "+status+" </p>";
                            }
                        },
                        {field : 'QRImageUrl',title : '二维码',width:100,align:'center',resizable:false
                            ,formatter:function(value ,row,index){
                            return '<div class="rowText" data-index="'+index+'" ><div class="fontC" data-opttype="down">下载</div></div>';
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

            that.renderTable();

        },
        windowsLoadData:function(type){
            var that=this;
            if(type.toLocaleLowerCase()=="oneway") {
                that.loadData.GetSysRetailRewardRule(function (data) {
                    debugger;
                    if (data.Data.SysRetailRewardRuleList && data.Data.SysRetailRewardRuleList.length > 0) {
                        var SysRetailRewardRuleList = data.Data.SysRetailRewardRuleList;
                        var fields = $("#addOneWay").serializeArray();
                        var loadData = {}
                        $.each(fields, function (index, field) {
                            for (var i = 0; i < SysRetailRewardRuleList.length; i++) {
                                var item = SysRetailRewardRuleList[i];
                                if (field.name.indexOf(item.RewardTypeCode) != -1) {    //确定是那个类别的
                                    if (field.name.indexOf("SellUserReward") != -1) {         //不同类别的销售员奖励
                                        loadData[field.name] = SysRetailRewardRuleList[i]["SellUserReward"];
                                    }
                                    if (field.name.indexOf("RetailTraderReward") != -1) {  //不同类别的分销商奖励
                                        loadData[field.name] = SysRetailRewardRuleList[i]["RetailTraderReward"];
                                    }
                                    if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同类别的分销商奖励
                                        loadData[field.name] = SysRetailRewardRuleList[i]["RetailRewardRuleID"];
                                    }

                                }

                            }


                        });

                        debugger;
                        $("#addOneWay").form('load', loadData);

                    }
                    $("[data-show='oneway'].radio").trigger("click");
                });
            }
            if(type.toLocaleLowerCase()=="twoway"){
                that.loadData.args.CooperateType="TwoWay" ;
                that.loadData.GetSysRetailRewardRule(function(data) {
                    debugger;
                    if (data.Data.SysRetailRewardRuleList && data.Data.SysRetailRewardRuleList.length > 0) {
                        var SysRetailRewardRuleList = data.Data.SysRetailRewardRuleList;
                        var fields = $("#addTwoWay").serializeArray();
                        var  loadData={}
                        $.each(fields, function (index, field) {
                            for (var i = 0; i < SysRetailRewardRuleList.length; i++) {
                                var item = SysRetailRewardRuleList[i];
                                if (field.name.indexOf(item.RewardTypeCode) != -1) {    //确定是那个类别的
                                    if (field.name.indexOf("SellUserReward") != -1) {         //不同类别的销售员奖励
                                        loadData[field.name]=SysRetailRewardRuleList[i]["SellUserReward"];
                                    }
                                    if (field.name.indexOf("RetailTraderReward") != -1) {  //不同类别的分销商奖励
                                        loadData[field.name]=SysRetailRewardRuleList[i]["RetailTraderReward"] ;
                                    }
                                    if (field.name.indexOf("RetailRewardRuleID") != -1) {  //不同类别的分销商奖励
                                        loadData[field.name]=SysRetailRewardRuleList[i]["RetailRewardRuleID"] ;
                                    }

                                }

                            }


                        })

                        debugger;
                        $("#addTwoWay").form('load',loadData);

                    }
                    $("[data-show='twoway'].radio").trigger("click");
                });
            }
        },
        setReward:function(data){
            var that=this;
            that.elems.optionType="setReward";
            $('#win').window({title:"分销商设置奖励",width:656,height:530,top: ($(window).height()-530)/2, left:($(window).width()-656)/2});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_setReward');
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent').layout('add',options);
            $('#win').window('open');
              debugger;
           that.loadData.args.RetailTraderID=data.RetailTraderID;
            that.windowsLoadData(data.CooperateType);//表单赋值


        },
        saveit:function(src)
        {
            I1.document.location=src;
            page.savepic();
        },
        savepic: function()
        {
            if(I1.document.readyState=="complete")
                I1.document.execCommand("saveas");
            else
             console.log("执行");
             window.setTimeout("savepic()",10);
        },
        downloadFile1:function (fileName, content,url){

            $.ajax({
                type:'GET',
            url: url ,
            resDataType:'blob',
            imgType:'jpg',
            success:function(resText,resXML){
                //var objectUrl = window.URL.createObjectURL(resText);
               /* img.src = objectUrl;
                img.onload = function(){
                    window.URL.revokeObjectURL(objectUrl);
                };
                document.body.appendChild(img);*/
                var aLink = document.createElement('a');
                //var blob = new Blob([content]);
                var evt = document.createEvent("HTMLEvents");
                evt.initEvent("click", false, false);//initEvent 不加后两个参数在FF下会报错, 感谢 Barret Lee 的反馈
                aLink.download = fileName;
                aLink.href = URL.createObjectURL(resText);
                aLink.dispatchEvent(evt);
            },
            fail:function(err){
                console.log(err)
            }
        });
       },
        downloadFile:function (fileName, content){

                    var aLink = document.createElement('a');
                    //var blob = new Blob([content]);
                    var evt = document.createEvent("HTMLEvents");
                    evt.initEvent("click", false, false);//initEvent 不加后两个参数在FF下会报错, 感谢 Barret Lee 的反馈
                    aLink.download = fileName;
                    aLink.href =content//; URL.createObjectURL(content);
                    aLink.dispatchEvent(evt);

        },
        initEvent: function () {
            var that=this;
            $("#cc").combobox({//
                valueField:'id',
                textField:'text',
                width:160,
                height:32,
                data:[{
                    "id":-1,
                    "text":"全部",
                    "selected":true
                },{
                    "id":0,
                    "text":"停用"
                },{
                    "id":1,
                    "text":"正常"

                }]
            });
            $("#Way").combobox({//
                valueField:'id',
                textField:'text',
                width:160,
                height:32,
                data:[ {
                    "id":"",
                    "text":"全部" ,
                    "selected":true
                },{
                    "id":"TwoWay",
                    "text":"互相集客(资源共享)",
                },{
                    "id":"OneWay",
                    "text":"单向集客(赚取佣金)"

                }]
            });
            $("#cc").combobox("setValue","-1");
            $("#win").delegate(".radio","click",function(e){
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                    me.addClass("show");
                    var classs="."+me.data("hide");
                    $(classs).hide(0);
                    var classs="."+me.data("show");
                    $(classs).show(0);
                }

            });




                that.loadData.get_unit_tree(function(datas) {
                    var data=[{}];
                    data[0]["id"]=window.UnitID;
                    data[0]["text"]=window.UnitName;
                    data[0].children=datas;
                    that.unitTree=data;
                    $("#unitTree").combotree({
                        panelWidth:220,
                        //width:220,
                        //animate:true,
                        multiple:false,
                        valueField: 'id',
                        textField: 'text',
                        data:data
                    });
                    $("#unitTree").combotree("setText","请选择门店");
                });

			//绑定搜索按钮事件
			$('.queryBtn').on('click',function(){
                debugger;
             var fileds =$("#queryFrom").serializeArray();
                $.each(fileds,function(index,filed){
                  that.loadData.seach[filed.name] = filed.value;
                });
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
            $('#win').delegate(".saveBtn","click",function(e){
                debugger
                var type=$("[data-cooperatetype].on").data("cooperatetype"), fields=[],isSubmit=false;
                if(type=="OneWay"){
                    if($("#addOneWay").form("validate")){
                        isSubmit=true;
                        fields=$("#addOneWay").serializeArray();
                    }
                }else if(type=="TwoWay") {
                    if ($("#addTwoWay").form("validate")) {
                        isSubmit = true;
                        fields = $("#addTwoWay").serializeArray();
                    }
                }

                if(isSubmit) {
                    that.loadData.operation(fields, that.elems.optionType, function () {
                        alert("操作成功");
                        $("#win").window("close");
                        that.renderTable();
                    })
                }

            });
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
                PageSize:15,
                RetailTraderID:'',
                bat_id:"1",
                CooperateType:"OneWay",
                OrderBy:"",           //排序字段
                SortType: 'DESC'    //如果有提供OrderBy，SortType默认为'ASC'
            },
            seach:{
                RetailTraderName:"" ,   //分销商名称
                RetailTraderAddress:"" ,   //分销商地址
                RetailTraderMan:"" ,   //联系人
                Status:"" ,  //状态
                UnitID:window.UnitID
            },
            getUitTree:{
                node:window.UnitID
            },
            opertionField:{},
            //获取分销商励配置
            GetSysRetailRewardRule: function (callback) {
                debugger;
                $.util.ajax({
                    url: "/ApplicationInterface/AllWin/SysRetailRewardRule.ashx",
                    data:{
                        action:"GetSysRetailRewardRule",
                        IsTemplate:'0',
                        RetailTraderID:this.args.RetailTraderID,
                        CooperateType:this.args.CooperateType
                    },
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
            },

            //获取分销商列表
            getRetailTraders: function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/AllWin/RetailTrader.ashx",
                    data: {
                        action: 'GetRetailTraders',
                        PageSize: this.args.PageSize,
                        PageIndex: this.args.PageIndex,
                        RetailTraderName:this.seach.RetailTraderName,
                        RetailTraderAddress:this.seach.RetailTraderAddress,
                        RetailTraderMan:this.seach.RetailTraderMan,
                        UnitID:this.seach.UnitID,
                        Status: this.seach.Status,
                        CooperateType:this.seach.CooperateType
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
                $.ajax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx?method=get_unit_tree&parent_id=&_dc=1433225205961&node="+this.getUitTree.node+"&multiSelect=false&isSelectLeafOnly=false&isAddPleaseSelectItem=false&pleaseSelectText=--%E8%AF%B7%E9%80%89%E6%8B%A9--&pleaseSelectID=-2",
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
            },
            operation:function(pram,operationType,callback){
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

                        prams.data["CooperateType"]=$("[data-cooperatetype].on").data("cooperatetype");
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
            }


        }
    };
    page.init();
});