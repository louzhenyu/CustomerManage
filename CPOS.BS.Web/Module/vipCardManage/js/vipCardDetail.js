/**
 * Created by Administrator on 2015/9/6.
 */
define(['jquery','template', 'tools','langzh_CN','easyui', 'artDialog','kkpager','kindeditor'], function ($) {
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            statusList:$(".VipCardStatusList"),       //会员卡状态变更记录集合
            balanceList:$('.VipCardBalanceList'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
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
            var VipCardID = $.util.getUrlParam("VipCardID");
            var VipCardISN = $.util.getUrlParam("VipCardISN");
            if(VipCardISN){
                VipCardISN=VipCardISN.replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"")
            }
            this.loadData.args.VipCardID=VipCardID;
            this.loadData.args.VipCardISN=VipCardISN;
            this.initEvent();  //initEvent 一定是在权限前面，第一点，移除按钮可能会对绑定时间有影响 。第二点权限的菜单id要根据选中菜单的id来配置
            this.authority();
            this.loadPageData();
        },
        //权限控制
        authority:function(){
            return false;
            this.loadData.getFunctionList(function(data){
                debugger;
                if(data.Data.FunctionList&&data.Data.FunctionList.length>=0){
                    $("[data-authority]").each(function(){
                        var me=$(this),isRemove=true; //定义是否移除按钮， 获取全部按钮，遍历权限列表，如果权限列表有，不删除（isRemove=false）
                        for(var i=0;i<data.Data.FunctionList.length;i++){
                            if(me.data("authority").toLowerCase()==data.Data.FunctionList[i].FunctionCode.toLowerCase()){
                                isRemove=false;
                            }

                        }
                        if(isRemove){
                            me.remove();
                        }

                    })

                }
            })
        },
        initEvent: function () {
            var that = this;
            //指定选中正确的菜单
            $("#leftMenu").find("li").removeClass("on").each(function(){
                if( $(this).find("em").hasClass("vipCardManage_querylist")){
                    $(this).addClass("on");
                }
            });
            /**************** -------------------弹出easyui 控件  End****************/
            that.elems.sectionPage.delegate(".commonBtn","click", function (e) {
                debugger;
                var me=$(this);
                that.loadData.args.imgSrc=""

                   that.loadData.args.OperationType=me.data("optiontype");
                 if(that.loadData.args.OperationType==1){
                     that.adjustIntegral();
                 }else{
                     var  data={title:me.html()};
                     if(me.data("flag")){
                       data.title=me.data("flag");
                     }
                     that.SetVipCard(data);
                 }

            }).delegate(".imageListPanel","mouseenter",function(){
                var obj=$(this).offset();
                $(this).find(".imgShow").show().css({top:obj.top,left:obj.left});

            }).delegate(".imageListPanel","mouseleave",function(){
                $(this).find(".imgShow").hide();
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
            $("body").delegate("input","keydown",function(e){
                debugger;
                if(e.keyCode==13){
                    var str=$(this).val().replace(/;/g,"").replace(/\?/g,"").replace(/；/g,"").replace(/？/g,"");
                    if(that.elems.vipCardCode) {
                       str= str.replace(that.elems.vipCardCode,"");

                        that.elems.vipCardCode=str;
                    } else{
                        that.elems.vipCardCode=str;
                    }


                    $.util.stopBubble(e);
                    $(this).focus();
                    $(this).val(str);
                }

            });
            $('#win').delegate(".saveBtn","click",function(e){

                if ($('#optionform').form('validate')) {

                    var fields = $('#optionform').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,that.elems.optionType,function(data){
                        if(that.loadData.args.OperationType==1)
                        {
                            $.messager.alert("提示","此次余额变动"+that.elems.BalanceMones+",由原"+that.elems.vipCardInfo.BalanceAmount+"变动至"+page.elems.BalanceMoney);
                        }else {
                            $.messager.alert("提示", "操作成功");

                        }
                        $("#win").window("close");
                        that.loadPageData();

                    });

                }
            });

            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/

            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){


        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var  that=this;
            that.loadData.GetVipCardDetail(function(data){
                that.loadData.args.VipCardCode=data.Data.VipCardCode;
                that.elems.vipCardInfo=data.Data;
              /*  if(data.Data.Birthday&&new Date(data.Data.Birthday)){
                    data.Data.Birthday=new Date(data.Data.Birthday).format("MM-dd");
                } else{
                    data.Data.Birthday="01-01" ;
                }*/
                //会员卡状态ID (0未激活，1正常，2冻结，3失效，4挂失，5休眠)
                $(".adjust").hide();
                var isAddCLass=true;
                switch (data.Data.VipCardStatusId){
                    case 0:data.Data.VipCardStatusName="未激活";break;
                    case 1:data.Data.VipCardStatusName="正常";$(".adjust").show();  isAddCLass=false;        break;
                    case 2:data.Data.VipCardStatusName="已冻结";   break;
                    case 3:data.Data.VipCardStatusName="已失效"; break;
                    case 4:data.Data.VipCardStatusName="已挂失"; break;
                    case 5:data.Data.VipCardStatusName="已休眠"; break;
                  }
                 if(isAddCLass){
                     $("#status").addClass("bigColor");
                 }else{
                     $("#status").removeClass("bigColor");
                 }

                $("#simpleQuery").find(".commonBtn").each(function(){
                    debugger;
                    if($(this).data("show").toString().indexOf(data.Data.VipCardStatusId)==-1){
                          $(this).hide();
                    }else{
                        $(this).show();
                    }
                });
                data.Data.Gender=data.Data.Gender==1?"男":"女";
                $("#loadForm").form("load",data.Data);
                 that.renderTable(data);
                that.loadData.getVipCardStatusChangeLog(function(data){
                    that.renderTableBalance(data);
                })
            });
        },

        //卡操作记录
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.StatusLogList){

                data.Data.StatusLogList=[];
            }
           var dataMessage= $("#pageContianer").find(".dataMessage");

            //jQuery easy datagrid  表格处理
            that.elems.statusList.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.StatusLogList,
                /* sortName : 'brandCode', //排序的列
                 *//*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*//*
                 idField : 'OrderID', //主键字段*/
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {field : 'CreateTime',title : '日期',width:196,align:'center',resizable:false},

                    {field : 'UnitName',title : '办卡门店',width:100,align:'center',resizable:false} ,
                    {field : 'Action',title : '卡操作',width:80,align:'center',resizable:false} ,
                    {field : 'ChangeReason',title : '原因',width:80,align:'center',resizable:false} ,
                    {field : 'Remark',title : '备注',width:200,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=20;
                            debugger;
                             if(!value){value="无"}
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,

                    {field : 'CreateBy',title : '操作人',width:80,align:'center',resizable:false} ,
                    {field : 'ImageUrl',title : '图片',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var html='无';
                            if(value){
                                html='<div id="imageListPanel_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                   '<div>'
                            }

                            return html;
                        }

                    }
                /*    {field : 'VipCardStatusID',title : '卡状态',width:96,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            //会员卡状态标识(0未激活，1正常，2冻结，3失效，4挂失，5休眠)
                            switch (value){
                                case 0 : staus="未激活";break;
                                case 1 : staus= "正常";break;
                                case 2 : staus= "冻结";break;
                                case 3 : staus= "失效";break;
                                case 4 : staus= "挂失";break;
                                case 5 : staus="休眠";break;

                            }
                            return staus;
                        }
                    },*/


                ]],

                onLoadSuccess : function(datas) {
                    that.elems.statusList.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                     if(datas.rows.length>0){
                         dataMessage.hide()
                     } else{
                         dataMessage.show();
                     }
                    for(var i=0;i<datas.rows.length;i++) {
                        $('#imageListPanel_'+i).tooltip({
                            position: 'top',
                            content: '<img class="imgShow" width="257" height="176" src="'+datas.rows[i].ImageUrl+'">'
                        });
                    }
                }


            });



            //分页
        /*    kkpager.generPageHtml({
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


            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }*/
        },


        //余额变动记录
        renderTableBalance: function (data) {
            debugger;
            var that=this;
            if(!data.Data.VipCardList){

                data.Data.VipCardList=[];
            }
            //jQuery easy datagrid  表格处理
            var dataMessage= $("#pageContianer1").find(".dataMessage");

            that.elems.balanceList.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.VipCardList,
                /* sortName : 'brandCode', //排序的列
                 *//*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*//*
                 idField : 'OrderID', //主键字段*/
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {field : 'CreateTime',title : '日期',width:60,align:'center',resizable:false,
                        /* formatter:function(value ,row,index){
                         return new Date(value).format("yyyy-MM-dd")
                         }*/
                    },


                    {field : 'UnitName',title : '门店',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var long=20;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,
                    {field : 'ChangeAmount',title : '储值增减',width:80,align:'center',resizable:false} ,
                    {field : 'Reason',title : '原因',width:60,align:'center',resizable:false} ,
                    {field : 'Remark',title : '备注',width:100,align:'center',resizable:false,

                        formatter:function(value ,row,index){
                            var long=20;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,

                    {field : 'CreateBy',title : '操作人',width:80,align:'center',resizable:false} ,
                    {field : 'ImageUrl',title : '图片',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var html='无';
                            if(value){
                                html='<div id="imageList_'+index+'" > <img src="'+value+'" width="70" height="70"  />' +
                                    '<div>'
                            }

                            return html;
                        }

                    }

                ]],

                onLoadSuccess : function(data) {
                    that.elems.balanceList.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0){
                        dataMessage.hide()
                    } else{
                        dataMessage.show();
                    }
                    for(var i=0;i<data.rows.length;i++) {
                        $('#imageList_'+i).tooltip({
                            position: 'top',
                            content: '<img class="imgShow" width="257" height="176" src="'+data.rows[i].ImageUrl+'">'
                        });
                    }
                }


            });

            if(!(data.Data.TotalPageCount>0)){return false}
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


            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动g
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getVipCardStatusChangeLog(function(data){
                that.renderTableBalance(data);
            });
        },

        //调整余额;
        adjustIntegral:function(){
            var that=this;
            that.elems.optionType="updateIntegral";
            $('#win').window({title:"余额调整",width:630,height:500,top:($(window).height()-500)*0.5,left:($(window).width()-630)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_adjustIntegral');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
            that.registerUploadImgBtn ();
            that.loadData.args.imgSrc=""
        },
       // 设置会员卡状态
        SetVipCard:function(data){
            var that=this;
            that.elems.optionType="SetVipCard";
            if(!data.title){data.title="会员卡状态变更"}
            var height=380,width=630
            if(that.loadData.args.OperationType==3){
                height=460
            } else if(that.loadData.args.OperationType==2||that.loadData.args.OperationType==5){
                height=400
            }
            var html=bd.template('tpl_setVipCard');
            if(that.loadData.args.OperationType==10){
                html= bd.template('tpl_activate');
                height=300;
                width=380
            }  var dataReason=[];
            switch (that.loadData.args.OperationType){
                case 1:

                    break;
                case 2:   //升级
                    dataReason=[{
                        "label":"现金",
                        "value":"现金"

                    },{
                        "label":"积分抵扣",
                        "value":"积分抵扣"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];

                    break;
                case 3:   //挂失
                    dataReason=[{
                        "label":"卡遗失",
                        "value":"卡遗失" ,
                        "selected":true
                    }] ;

                    break;
                case 4:  //冻结
                    dataReason=[{
                        "label":"无效会员",
                        "value":"无效会员"

                    },{
                        "label":"消费异常",
                        "value":"消费异常"

                    },{
                        "label":"退卡",
                        "value":"退卡"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 5: //转卡
                    dataReason=[{
                        "label":"换卡",
                        "value":"换卡"

                    },{
                        "label":"芯片破损",
                        "value":"芯片破损"

                    },
                        {
                            "label":"补办",
                            "value":"补办"

                        },
                        {
                        "label":"卡号破损",
                        "value":"卡号破损"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                 break;
                case 6: //解挂
                    dataReason=[{
                        "label":"卡找到",
                        "value":"卡找到" ,
                        "selected":true
                    }] ;
                    break;
                case 7: //解冻
                    dataReason=[{
                        "label":"会员激活 ",
                        "value":"会员激活"

                    },{
                        "label":"消费异常已调整",
                        "value":"消费异常已调整"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 8: //作废
                    dataReason=[{
                        "label":"发卡错误 ",
                        "value":"发卡错误"

                    },{
                        "label":"新卡遗失",
                        "value":"新卡遗失"

                    },{
                        "label":"0",
                        "value":"请选择" ,
                        "selected":true
                    }];
                    break;
                case 9: //唤醒
                    dataReason=[{
                        "label":"会员激活",
                        "value":"会员激活" ,
                        "selected":true
                    }] ;
                    break;
                case 10: //激活
                    break;
            }
            $("#ChangeReason").combobox({data:dataReason})

            $('#win').window({title:data.title,width:width,height:height,top:($(window).height()-height)*0.5,left:($(window).width()-width)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');



            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').find(".showHide").each(function(){
                    var me= $(this);
                 if(me.data("show")&&me.data("show").indexOf(that.loadData.args.OperationType)!=-1){
                    me.show();

                } else if(me.data("show")){
                    me.remove();
                }
                if(me.data("hide")&&me.data("hide").indexOf(that.loadData.args.OperationType)!=-1){
                    me.remove();
                } else if(me.data("hide")){
                    me.show();
                }
            });


            $('#win').window('open');
            $("#optionform").form('load',that.elems.vipCardInfo);
           that.registerUploadImgBtn ();
            $("#ChangeReason").combobox({data:dataReason})
            that.loadData.args.imgSrc=""
        },
        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
            $("#win").find(".uploadBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;



            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                self.loadData.args.imgSrc=data.url;
                $(ele).siblings(".imgPanel").find("img").show().attr({"src":data.url,width:100,height:100});

            });

        },
        //上传图片
        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:37,
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=640',

                afterUpload: function (data) {
                    debugger;
                    if (data.error === 0) {
                        if (callback) {
                            callback(btn, data);
                        }
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else{
                        alert(data.msg);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },
        loadData: {
            args: {
                imgSrc:"",
                VipCardISN:"",
                VipCardID:"" ,
                OperationType:-100,
                VipCardCode:"" ,
                PageIndex:1,
                PageSize:10
            },
            tag:{

            } ,
            opertionField:{},
            GetVipCardDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",

                    data: {
                        action: "VIP.VIPCard.GetVipCardDetail",
                        VipCardID: this.args.VipCardID,
                        VipCardISN:this.args.VipCardISN
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
            getVipCardStatusChangeLog: function (callback) {


                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action: "VIP.VIPCard.GetVipCardBalanceChangeList",
                        VipCardCode:this.args.VipCardCode,
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSize
                    },
                    /*  channelID:'7',
                     customerId:"464153d4be5944b19a13e325ed98f1f4",
                     userId:"550E2D12613D4580989B65AF984F578D",*/
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


          /*  //获取门店
            get_unit_tree: function (callback) {
                $.util.oldAjax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx",
                    data:{

                        action: "get_unit_tree",
                        QueryStringData:{
                            _dc:'1433225205961',
                            node:this.getUitTree.node,
                            parent_id:'',
                            multiSelect:false,
                            isSelectLeafOnly:false,
                            isAddPleaseSelectItem:false,
                            pleaseSelectText:'--%E8%AF%B7%E9%80%89%E6%8B%A9--',
                            pleaseSelectID: -2
                        }

                    } ,

                    success: function (data) {

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
*/

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"VIP.VIPCard.SetVipCard"}};
                var submit={is:true,msg:""};
                var  BalanceMoney=0;
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                $.each(pram,function(index,field){

                    prams.data[field.name]=field.value;
                    if(field.name=="BalanceMoney"&&field.value==0){
                        submit.is=false;
                        submit.msg="余额调整数量不能为零，请重新填写";

                    }
                    if(field.name=="BalanceMoney"){

                        BalanceMoney =page.elems.vipCardInfo.BalanceAmount+parseInt(field.value);
                        if(BalanceMoney<0){
                            submit.is=false;
                            submit.msg="扣除余额不可超出当前余额";
                        }
                        page.elems.BalanceMones =field.value
                    }
                });

                if(this.args.OperationType==3||this.args.OperationType==1) {
                    if(this.args.imgSrc){
                        prams.data["ImageUrl"]=this.args.imgSrc
                    }else{
                       /* $.messager.alert("错误提示","必须上传一张图片");
                         return false;*/
                    }

                }

                prams.data["VipCardID"]= this.args.VipCardID;
                prams.data["OperationType"]= this.args.OperationType;
                if(!submit.is) {$.messager.alert("错误提示",submit.msg); return false;}
                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                page.elems.BalanceMoney=BalanceMoney;
                                callback(data);

                            }

                        } else {
                            $.messager.alert("操作失败提示","失败原因:"+data.Message);

                        }
                    }
                });
            },
            //页面权限分组
            getFunctionList: function (callback) {
                $.util.ajax({
                    url: "/applicationinterface/Gateway.ashx",
                    data: {
                        action: "Basic.Menu.GetFunctionList",
                        MenuID:$("#leftMenu").find(".on a").attr("id")?$("#leftMenu").find(".on a").attr("id"): $.util.getUrlParam("mid")

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
            }


        }

    };
    page.init();
});

