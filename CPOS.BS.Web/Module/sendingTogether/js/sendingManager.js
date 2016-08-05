define(['jquery', 'template', 'tools', 'kkpager','easyui', 'artDialog','datetimePicker','tips','zTree'], function ($) {
    var page = {
        params:{
            pageIndex: 0,
            WithdrawNo:'',
            VipName:'',
            Status:''
        },
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            thead:$("#thead"),                    //表格head部分
            dialogLabel:$("#dialogLabel"),          //标签选择层
            menuItems:$("#menuItems"),             //针对表格的操作菜单
            resultCount: $("#resultCount"),          //所有匹配的结果
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            optionBtn:$(".optionBtn"),       //操作按钮父类集合
            vipSourceId:''
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                           //标签列表
        },
        init: function () {
            var that = this;

            //绑定事件
            that.initEvent();
            //显示表格信息
            that.loadPageData();
        },
        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            setTimeout(function () {
                that.loadData.getWithdrawHome(function(data){
                    //if(data.Data.List!=null){
                        that.getDraw(data)
                   // }
                });
            }, 2000);
            $.util.stopBubble(e);


        },
        renderTable:function(){
            var that=this;
            $.util.partialRefresh(that.elems.tabel);
            that.loadData.getWithdrawDepositApplyList(function(data){
                debugger;
                if(!data.Data.List){
                    return;
                }
                //jQuery easy datagrid  表格处理
                that.elems.tabel.datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, // 多选
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.Data.List,
                    //sortName : 'brandCode', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    frozenColumns : [ [ {
                        field : 'ck',
                        width:70,
                        title:'全选',
                        align:'left',
                        checkbox : true
                    } //显示复选框
                    ] ],

                    columns : [[

                        {field : 'WithdrawNo',title : '提现单号',width:350,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },
                        {field : 'Name',title : '姓名',width:150,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'Phone',title : '手机号',width:200,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'VipType',title : '类别',width:100,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                if(value){
                                    switch (value){
                                        case 1:
                                            value = '会员';
                                            break;
                                        case 2:
                                            value = '员工';
                                            break;
                                        case 4:
                                            value = '分销商';
                                            break;
                                    }
                                    return value;
                                }
                            }
                        },
                        {field : 'Amount',title : '提现金额(元)',width:200,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'BankName',title : '银行名称',width:200,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },

                        {field : 'CardNo',title : '银行卡号',width:500,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'AccountName',title : '开户人姓名',width:200,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'Status',title : '状态',width:150,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                if(value){
                                    var str="";
                                    switch (value){
                                        case 1:
                                            str = '待审核';
                                            break;
                                        case 2:
                                            str = '审核通过';
                                            break;
                                        case 3:
                                            str = '已完成';
                                            break;
                                        case 4:
                                           str ="<span class='noReviewed' style='color:#038efe;cursor:pointer;' data-remark='"+row.Remark+"'>审核不通过</span>"
                                            break;
                                    }
                                    return str;
                                }
                            }
                        },


                        {field : 'ApplyDate',title : '申请日期',width:200,align:'center',resizable:false
                            ,formatter:function(value ,row,index){
                            if(value!=""){
                                return new Date(value).format("yyyy-MM-dd");
                            }else{
                                value = "-";
                                return value;
                            }

                        }},
                        {field : 'CompleteDate',title : '完成日期',width:200,align:'center',resizable:false
                            ,formatter:function(value ,row,index){
                            if(value!=""){
                                return new Date(value).format("yyyy-MM-dd");
                            }else{
                                value = "-";
                                return value;
                            }

                        }},

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
                    total: data.Data.TotalPage,
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
            });
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.renderTable();
        },

        // 图表数据
        getDraw:function(data){
            var that = this;
            var list = data.Data.List;
            var isNull = data.Data.isConfigRule;
           if(list!=null){
                for(var i=0;i<list.length;i++){
                    if(list[i].Vip==null){
                        list[i].Vip = 0;
                    }else{
                        list[i].Vip=that.mathInit(list[i].Vip);
                    }
                    if(list[i].User==null){
                        list[i].User = 0;
                    }else{
                        list[i].User=that.mathInit(list[i].User);
                    }
                    if(list[i].SRT==null){
                        list[i].SRT = 0;
                    }else{
                        list[i].SRT=that.mathInit(list[i].SRT);
                    }
                    if(list[i].Count==null){
                        list[i].Count = 0;
                    }else{
                        list[i].Count=that.mathInit(list[i].Count);
                    }
                    var html = "<ul><li><p>会员</p><p><em class='totle'>"+list[i].Vip+"</em><i>元</i></p></li><li><p>员工</p><p><em class='totle'>"+list[i].User+"</em><i>元</i></p></li><li><p>分销商</p><p><em class='totle'>"+list[i].SRT+"</em><i>元</i></p></li></ul>";
                    var html2 = "<ul><li><p>会员</p><p><em class='totle'>"+list[i].Vip+"</em><i>笔</i></p></li><li><p>员工</p><p><em class='totle'>"+list[i].User+"</em><i>笔</i></p></li><li><p>分销商</p><p><em class='totle'>"+list[i].SRT+"</em><i>笔</i></p></li></ul>";
                    if(list[i].Type=='1'){
                        $('.totalStatistic .itemTotle').eq(0).find('.record').find('.totle').html(list[i].Count);
                        $('.totalStatistic .content').eq(1).html(html);
                    }
                    if(list[i].Type=='2'){
                        $('.totalStatistic .itemTotle').eq(1).find('.record').find('.totle').html(list[i].Count);
                        $('.totalStatistic .content').eq(3).html(html);
                    }
                    if(list[i].Type=='3'){
                        $('.totalStatistic .item').eq(0).find('.record').find('.totle').html(list[i].Count);
                        $('.totalStatistic .content').eq(5).html(html);
                        that.loadData.args.countThree = list[i];
                    }
                    if(list[i].Type=='4'){
                        $('.totalStatistic .item').eq(1).find('.record').find('.totle').html(list[i].Count);
                        $('.totalStatistic .content').eq(7).html(html);
                        that.loadData.args.countFour = list[i];
                    }
                    if(list[i].Type=='5'){
                        that.loadData.args.countFive = list[i];
                    }
                    if(list[i].Type=='6'){
                        that.loadData.args.countSix = list[i];
                    }
                }
            }
            if(!isNull){
                isNull = false;
                isReviewed = false;
                that.update(isNull,isReviewed);
            }
            $('.totalStatistic .item').delegate('.viewPoint','click',function(){
                var txt = $(this).html(),
                    inddex =$(this).parents('.item').index(),
                    list = [];
                if(txt=='看笔数'){
                    $(this).html('看金额');
                    if(inddex=='2'){
                        list = that.loadData.args.countFive;
                        $('.totalStatistic .item').eq(0).find('.record').find('i').html('笔');
                        $('.totalStatistic .content').eq(5).find('li').find('i').html('笔');
                        $('.totalStatistic .item').eq(0).find('.record').find('.totle').html(list.Count);
                        $('.totalStatistic .content').eq(5).find('li').eq(0).find('.totle').html(list.Vip);
                        $('.totalStatistic .content').eq(5).find('li').eq(1).find('.totle').html(list.User);
                        $('.totalStatistic .content').eq(5).find('li').eq(2).find('.totle').html(list.SRT);
                    }else{
                        list = that.loadData.args.countSix;
                        $('.totalStatistic .item').eq(1).find('.record').find('i').html('笔');
                        $('.totalStatistic .content').eq(7).find('li').find('i').html('笔');
                        $('.totalStatistic .item').eq(1).find('.record').find('.totle').html(list.Count);
                        $('.totalStatistic .content').eq(7).find('li').eq(0).find('.totle').html(list.Vip);
                        $('.totalStatistic .content').eq(7).find('li').eq(1).find('.totle').html(list.User);
                        $('.totalStatistic .content').eq(7).find('li').eq(2).find('.totle').html(list.SRT);
                    }
                }else{
                    $(this).html('看笔数');
                    if(inddex=='2'){
                        list = that.loadData.args.countThree;
                        $('.totalStatistic .item').eq(0).find('.record').find('i').html('元');
                        $('.totalStatistic .content').eq(5).find('li').find('i').html('元');
                        $('.totalStatistic .item').eq(0).find('.record').find('.totle').html(list.Count);
                        $('.totalStatistic .content').eq(5).find('li').eq(0).find('.totle').html(list.Vip);
                        $('.totalStatistic .content').eq(5).find('li').eq(1).find('.totle').html(list.User);
                        $('.totalStatistic .content').eq(5).find('li').eq(2).find('.totle').html(list.SRT);
                    }else{
                        list = that.loadData.args.countFour;
                        $('.totalStatistic .item').eq(1).find('.record').find('i').html('元');
                        $('.totalStatistic .content').eq(7).find('li').find('i').html('元');
                        $('.totalStatistic .item').eq(1).find('.record').find('.totle').html(list.Count);
                        $('.totalStatistic .content').eq(7).find('li').eq(0).find('.totle').html(list.Vip);
                        $('.totalStatistic .content').eq(7).find('li').eq(1).find('.totle').html(list.User);
                        $('.totalStatistic .content').eq(7).find('li').eq(2).find('.totle').html(list.SRT);
                    }

                }

            })




        },
        //金额格式化
        mathInit:function(num){
            num = parseFloat(num).toString();
            if(Number(num)>10000){
                num = parseInt(num).toString();
                num = num.substring(0,num.length-4)+"."+num.substring(num.length-4,num.length-1);
                num = Number(num).toFixed(2)+"万";
                return num;
            }else{
                return num;
            }
        },
        initEvent: function () {
            $("#cc").combobox({//0待确认；1=已确认；2=已完成
                valueField:'id',
                textField:'text',
                width:198,
                height:32,
                data:[{
                    "id":-1,
                    "text":"请选择",
                    "selected":true
                },{
                    "id":1,
                    "text":"待审核"
                },{
                    "id":2,
                    "text":"审核通过"

                },{
                    "id":3,
                    "text":"已完成"
                }]
            });
            $("#category").combobox({//0待确认；1=已确认；2=已完成
                valueField:'id',
                textField:'text',
                width:198,
                height:32,
                data:[{
                    "id":-1,
                    "text":"请选择",
                    "selected":true
                },{
                    "id":1,
                    "text":"会员"
                },{
                    "id":2,
                    "text":"员工"
                },{
                    "id":4,
                    "text":"分销商"
                }]
            });
            var that = this;
            //绑定搜索按钮事件
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                debugger;
                var inputs = $('#phone').val();
                if(inputs!=""&&!(/^1[3|4|5|7|8]\d{9}$/.test(inputs))){
                    alert("手机号码有误，请重填");
                    return false;
                }
                that.setCondition();
                that.renderTable();
            });
            //收起展开
            that.elems.sectionPage.delegate(".packBtn","click", function (e) {
                debugger;
                var type= $(this).attr('data-type'),//1==图表；2==搜索选择
                    txt = $(this).html();
                if(txt == '收起'){
                    $(this).html('展开');

                    if(type=='1'){
                        $('.totalStatistic .totalContent').hide();
                        $(this).removeClass('top');
                        $(this).addClass('bottom');
                    }else{
                        $(this).parents('.queryTermArea').find('.contents').hide();
                        $(this).removeClass('up');
                        $(this).addClass('down');
                    }
                }else{
                    $(this).html('收起');

                    if(type=='1'){
                        $('.totalStatistic .totalContent').show();
                        $(this).removeClass('bottom');
                        $(this).addClass('top');
                    }else{
                        $(this).parents('.queryTermArea').find('.contents').show();
                        $(this).removeClass('down');
                        $(this).addClass('up');
                    }
                }
            });

            //导出事件
            that.elems.optionBtn.delegate(".commonBtn","click",function(e){
                var type=$(this).data("flag");
                if(type=="export"){
                    $.messager.confirm("导出订单列表","你确定导出当前列表的数据吗？",function(r){
                        if(r){
                            that.loadData.exportOderList();
                        }
                    });

                }

            });
            //审核不通过
            that.elems.sectionPage.delegate('.noReviewed','click',function(e){
                var txt = $(this).attr('data-remark');
                isNull = false;
                isReviewed = true;
                that.update(isNull,isReviewed);
                $('#win').find('.edits').find('span').attr('disabled',true);
                $('#win').find('.edits').find('textarea').attr('disabled',true);
                $('#win').find('.edits').find('span').eq(1).addClass('disab');
                $('#win').attr('data-type','3');//审核不通过
                $('#win').undelegate('span','click');

            })

            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onClose:function(){
                    $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate('span','click',function(){
                var isCheck = $(this).hasClass('on'),
                    txt = $(this).html();
                $(this).parents('.edits').find('span').removeClass('on');
                $(this).parents('.edits').find('span').removeClass('disab');
                $(this).parents('.edits').find('span').attr('disabled',false);
                $(this).parents('.tplWindow').find('textarea').attr('disabled',false);
                $(this).addClass('on');
                //$(this).parents('.edits').find("span:not([class='on'])").css('background','#fff');

            })
            $('#win').delegate(".saveBtn","click",function(e){
                var type = $('.edits').children('.on').attr('type'),
                    txt = $('.edits').children('.on').html(),
                    isData = $('#win').attr('data-type');//1==有数据；2==无数据，跳提示；3=审核不通过
                    remarks = $('.edits').children('textarea').html();
                if(isData=='1'){
                    if(txt == '审核不通过'){
                        if(remarks==''){
                            $.messager.alert('提示','备注不能为空');
                            return false;
                        }
                    }
                    that.loadData.args.CheckType = type|1;
                    that.loadData.args.Remark = "";
                    that.loadData.multiCheck(function(IsSuccess){
                        if(IsSuccess){
                            $('#win').window('close');
                        }
                    });
                }else if(isData=='2'){
                    location.href=("/module/sendingTogether/sendingManagerRule.aspx");
                }else{
                    location.href=("/module/sendingTogether/sendingManagerRule.aspx");
                }

                $('.edits').find('span').attr('disabled',true);
                $('.edits').find('textarea').attr('disabled',true);
                $('.edits').find('span').removeClass('on');
                $('.edits').find('span').eq(1).addClass('disab');

            });
            /**************** -------------------弹出窗口初始化 end****************/
            //打印事件
            $('#menuItems .exportBtn').on('click',function(e) {
                alert("打印功能开发中") ;
            });


            //绑定确认按钮事件
            $('#menuItems .affirmBtn,#menuItems .finishBtn').on('click',function(e) {
                var dataAll=that.elems.tabel.datagrid("getChecked");
                var str="";
                var isNull = true;
                //that.update();
                if(dataAll.length>0){
                    var type = $(this).data("statusid");
                    // if($(this).data("statusid")==1){
                    //     str="是否确定申请提现通过";
                    //
                    // }
                    if($(this).data("statusid")==2){
                        str="是否确定提现已完成" ;

                    }
                    var Status= $(this).data("statusid");
                    debugger
                    var applyId=dataAll[0].ApplyID;
                    //var  str="";
                    var isSubmit=false;
                    ids = [];
                    // if (dataAll[0].Status==2)
                    // {
                    //
                    //     isSubmit=true;
                    //
                    // }
                    for(var i=0;i<dataAll.length;i++)
                    {
                        //str+=dataAll[i].ApplyID+",";
                        if(type==1){
                            if (dataAll[i].Status!=1)
                            {

                                isSubmit=true;
                                break;
                            }
                        }else{

                            if (dataAll[i].Status!=2)
                            {

                                isSubmit=true;
                                break;
                            }
                        }
                        ids.push(dataAll[i].ApplyID);
                    }
                    that.loadData.args.Ids = ids;
                    that.loadData.args.IdsComplete = ids;
                    if(isSubmit){
                        if(type==1){
                            alert("请选择待审核的提现申请") ;
                        }else{
                            alert("请选择审核通过的提现申请") ;
                        }
                        return false;
                    }
                    if(type==1){
                        isReviewed = false;
                        that.update(isNull,isReviewed);
                    }else{
                        $.messager.confirm("操作提示",str,function(r){
                            if(r) {
                                that.loadData.multiComplete(function(IsSuccess){
                                 if(IsSuccess){
                                     that.renderTable();
                                     alert("操作成功")
                                 }
                                })
                            }
                        });
                    }

                    // $.messager.confirm("操作提示",str,function(r){
                    //     if(r) {
                    //         that.loadData.submitAuditStatus(Status,applyId,function(){
                    //             that.renderTable();
                    //             alert("操作成功")
                    //         }) ;
                    //     }
                    // });
                } else{
                    alert("请勾选需要操作的提现申请");
                }
                that.stopBubble(e);
            });

        },
        update:function(isNull,isReviewed){
            debugger;
            var that=this;
            if(isReviewed){
                $('#win').window({title:"审核",width:400,height:300,top:($(window).height() - 300) * 0.5,
                    left:($(window).width() - 400) * 0.5});
                //改变弹框内容，调用百度模板显示不同内容
                $('#panlconent').layout('remove','center');
                $('#win').attr('data-type','1');
                var html=bd.template('tpl_window');
                $('#win').find('.saveBtn').html("确定");
            }else if(isNull){
                $('#win').window({title:"提示",width:350,height:250,top:($(window).height() - 250) * 0.5,
                    left:($(window).width() - 350) * 0.5});
                //改变弹框内容，调用百度模板显示不同内容
                $('#panlconent').layout('remove','center');
                $('#win').attr('data-type','1');
                var html="<p style='text-align: center;margin-top:15%'>审核通过</p>";
                $('#win').find('.saveBtn').html("确定");
            }else{
                $('#win').window({title:"提示",width:350,height:250,top:($(window).height() - 250) * 0.5,
                    left:($(window).width() - 350) * 0.5});
                //改变弹框内容，调用百度模板显示不同内容
                $('#panlconent').layout('remove','center');
                $('#win').attr('data-type','2');
                var html="<p style='text-align: center;margin-top:15%'>你还没有设置提现规则，请先点击提现设置</p>";
                $('#win').find('.saveBtn').html("去提现设置");
            }
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $('#win').parents('.window').css('position','fixed');
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            var fileds =$("#queryFrom").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="0"?"":filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });
        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 10,
                countThree:'',
                countFour:'',
                countFive:'',
                countSix:'',
                CheckType:'',
                Ids:[],
                Remark:'',
                IdsComplete:[]

            },
            tag:{
                VipId:"",
                orderID:''
            },
            getUitTree:{
                node:""
            },
            seach:{
                sales_unit_id:"", //门店id
                Field7:"0",
                form:{
                    VipType:'',//会员类型
                    WithdrawNo:'',//提现单号
                    //Field1:"", //是否已付款 1 已付款 0 未付款。   全部不需要传递
                    Name:"",  //姓名
                    Phone:"",//电话号码
                    ApplyStartDate:"", //申请开始时间
                    ApplyEndDate:"",   // 申请结束时间
                    Status:'' ,     //订单渠道
                    CompleteStartDate:'' ,//完成开始时间
                    CompleteEndDate:"", //完成结束时间
                }
            },
            opertionField:{},
            //根据form数据 和 请求地址 导出数据到表格
            exportExcel: function (data, url) {
                var dataLink = JSON.stringify(data);
                var form = $('<form>');
                form.attr('style', 'display:none;');
                form.attr('target', '_blank');
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
            exportOderList: function () {
                page.setCondition();
                var obj = this.seach.form;
                obj.PageIndex = this.args.PageIndex;
                obj.PageSize = this.args.PageSize;
                var params = {
                    "Parameters":obj,
                    "locale":null
                };
                var getUrl = '/ApplicationInterface/Module/VIP/WithdrawDeposit/Export.ashx?action=VIP.WithdrawDeposit.GetWithdrawDepositApplyList&req='+JSON.stringify(params);
                this.exportExcel(this.seach.form, getUrl);
            },
            submitAuditStatus: function(statusId,applyId,callback){
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/WithdrawDepositGateway.ashx",
                    data: {
                        action: 'UpdateWDApply',
                        ApplyID: applyId,
                        Status: statusId
                    },
                    beforeSend: function () {
                        $.util.isLoading()

                    },
                    success: function (data) {
                        if (data.ResultCode == 0) {
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
            getWithdrawDepositApplyList: function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.WithdrawDeposit.GetWithdrawDepositApplyList',
                        PageSize: this.args.PageSize,
                        PageIndex: this.args.PageIndex,
                        VipType:this.seach.form.VipType,
                        WithdrawNo:this.seach.form.WithdrawNo,
                        Name:this.seach.form.Name,
                        Phone:this.seach.form.Phone,
                        ApplyStartDate:this.seach.form.ApplyStartDate,
                        ApplyEndDate:this.seach.form.ApplyEndDate,
                        Status:this.seach.form.Status,
                        CompleteStartDate:this.seach.form.CompleteStartDate,
                        CompleteEndDate:this.seach.form.CompleteEndDate,
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
            getWithdrawHome: function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.WithdrawDeposit.Get_R_Withdraw_Home',
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
            multiCheck: function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.WithdrawDeposit.MultiCheck',
                        type:this.args.CheckType,
                        Ids:this.args.Ids,
                        Remark:this.args.Remark
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if(callback){
                                callback(data.IsSuccess);
                            }

                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            multiComplete:function(callback){
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.WithdrawDeposit.MultiComplete',
                        Ids:this.args.IdsComplete
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if(callback){
                                callback(data.IsSuccess);
                            }

                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },

        },

        stopBubble: function (e) {
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        }
//		//通过隐藏form下载文件,导出文件
//		exportVipList: function () {
//			var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipList';//&req=';
//			var data = {
//				Parameters: {
//					SearchColumns: [],
//					PageSize: 10,
//					PageIndex: 1,
//					OrderBy: 'CREATETIME',
//					SortType: 'DESC',
//					VipSearchTags:  []
//				}
//			};
//			var dataLink = JSON.stringify(data);
//			var form = $('<form>');
//			form.attr('style', 'display:none;');
//			form.attr('target', '');
//			form.attr('method', 'post');
//			form.attr('action', getUrl);
//			var input1 = $('<input>');
//			input1.attr('type', 'hidden');
//			input1.attr('name', 'req');
//			input1.attr('value', dataLink);
//			$('body').append(form);
//			form.append(input1);
//			form.submit();
//			form.remove();
//		}

    };
    page.init();
});