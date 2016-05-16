define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','datetimePicker'], function ($) {
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
        //显示弹层
        showElements:function(selector){
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements:function(selector){

            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getBargainList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);
                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){
                $('#goodsBasic_exit').find('input').val('');
				$('#goodsBasic_exit').find('input').attr('disabled',false);
				$('#goodsBasic_exit').find('input').css({'background':'#fff','border':'1px solid #ccc'});
				that.loadData.args.EventID ="";
                that.showElements("#goodsBasic_exit");
            });
            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });
            //获取时间插件
             $('#campaignBegin').datetimepicker({
                lang: "ch",
                format: 'Y-m-d H:i',
                step: 5 //分钟步长
             });
             $('#campaignEnd').datetimepicker({
                lang: "ch",
                format: 'Y-m-d H:i',
                step: 5 //分钟步长
             });
            //新增活动(编辑活动)
            $('#saveCampaign').bind('click',function(){
				var myDate = new Date();	
                var event_name = $('#campaignName').val();
                var event_start = $("#campaignBegin").val();
                var event_end = $('#campaignEnd').val();
				var event_Id = that.loadData.args.EventID;
				var newDate = myDate.toLocaleDateString().split('/');
				if (newDate[1].length=="1"){
					newDate[1] = 0+newDate[1];
				}
				if (newDate[2].length=="1"){
					newDate[2] = 0+newDate[2];
				}
				var newTime = newDate[0]+"-"+newDate[1]+"-"+newDate[2]+" "+myDate.getHours()+":"+myDate.getMinutes();
                if(event_name!=""&&event_start!=""&&event_end!=""){
                    that.loadData.goods.EventName = event_name;
                    that.loadData.goods.BeginTime = event_start;
                    that.loadData.goods.EndTime = event_end;					
                    if(event_start<=event_end){
                        if(event_Id==""){

                            //var CommodityStatus = 1;
                            if(event_end<newTime){

                                var CommodityStatus = 3;
                                $.messager.alert('提示','无法添加已过期的活动，请重新选择！');
                                return false;
                            }
                            else if(event_start>newTime){
                                var CommodityStatus =1;

                            }
                            else if(event_start<newTime&&newTime<event_end){
                                var CommodityStatus = 2;

                            }
                            that.loadData.setBargain(function(data){
                                var event_id = data.EventId;
                                var mid = JITMethod.getUrlParam("mid");
                                location.href = "addCommodity.aspx?eventId="+event_id+"&mid="+ mid+"&CommodityStatus="+CommodityStatus;
                            })

                        }
                        else{
                            that.loadData.setBargain()
                            window.location.reload();//刷新当前页面.
                        }
                    }
                    else{
                       $.messager.alert('提示','活动开始时间不能大于结束时间'); 
                    }
                    
                }
                else{
					if(event_name==""){
						$.messager.alert('提示','活动标题不能为空'); 
					}
					else if(event_start==""){
						$.messager.alert('提示','活动开始时间不能为空'); 
					}
					else if(event_end==""){
						$.messager.alert('提示','活动结束时间不能为空'); 
					}
                    
                }
                //var event_id = data.Data.EventId;
                

            })


            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=198,H=32;
            $('#item_status').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":1,
                    "text":"未开始"
                },{
                    "id":2,
                    "text":"进行中"

                },{
                    "id":3,
                    "text":"已结束"
                },{
                    "id":-1,
                    "text":"全部",
					"selected":true

                }]
            });
            $('#item_status').combobox("setValue",-1);


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
                that.loadData.args.EventID = row.EventId;
                if(optType=="add") {

                    that.addNumber(row);

                }
                if(optType=="exit"){
                    var mid = JITMethod.getUrlParam("mid");
                    var CommodityStatus = row.Status;
                    location.href = "addCommodity.aspx?eventId="+couponTypeID+"&mid="+ mid+"&CommodityStatus="+CommodityStatus;
                
                }
                //延长时间
                if(optType=="watchOn"){
                    var name = row.EventName;
                    var start = row.BeginTime;
                    var end = row.EndTime;
                    $('#campaignName').val(name);
					$('#campaignBegin').val(start);
					$('#campaignEnd').val(end);
                    $('#goodsBasic_exit').find('input').attr('disabled',true);
                    $('#campaignName').css('border','none');
					$('#campaignBegin').css('background','#ccc');
					$('#campaignEnd').attr('disabled',false);      
                    that.showElements("#goodsBasic_exit");                
                
                }
                //提前结束
                if(optType=="stop"){
                   $.messager.confirm("提示","是否提前结束活动？",function(r){
                        if(r){
                            var name = row.EventName;
							var start = row.BeginTime;
							var end = row.EndTime;
							var myDate = new Date();
							var newDate = myDate.toLocaleDateString().split('/');
							if (newDate[1].length=="1"){
								newDate[1] = 0+newDate[1];
							}
							if (newDate[2].length=="1"){
								newDate[2] = 0+newDate[2];
							}
							var newTime = newDate[0]+"-"+newDate[1]+"-"+newDate[2]+" "+myDate.getHours()+":"+myDate.getMinutes();
							
                            that.loadData.setBargainStatus(function(data){
								that.loadData.goods.EventName = name;
								that.loadData.goods.BeginTime = start;
								that.loadData.goods.EndTime = newTime;
								that.loadData.setBargain();
                                alert("操作成功");
                                that.loadPageData()
                            })

                        }
                    })               
                
                }

                if(optType=="pause"){
                    var t = $(this);
                    $(t).removeClass('pauseBtn');
                    $(t).addClass('runningBtn');   
                }

                if (optType == "deleteOn") {
                    $.messager.alert("提示","进行中的活动无法删除!");
                }

                if(optType=="delete"){
                    if (row.BeginTime&&row.EndTime) {
                        var Begindate = Date.parse(new Date(row.BeginTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var Enddate = Date.parse(new Date(row.EndTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var now = new Date();

                        // if (Begindate <= now && Enddate >= now) {
                        //     $.messager.alert("操作提示","优惠券在使用时间范围内不可删除");
                        //     return false;
                        // }
                    }
                    // else{
                    //     $.messager.alert("操作提示","该优惠券是领取及时生效类型不可删除");
                    //     return false;
                    // }
                    $.messager.confirm("提示","是否确认删除？",function(r){
                        if(r){
                            
                            that.loadData.deleteBargain(function(data){
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
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.BargainList){

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
                data:data.BargainList,
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

                    {field : 'EventName',title : '活动名称',width:300,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'ItemCount',title : '商品数量',width:100,resizable:false,align:'left'},
                    {field : 'PromotePersonCount',title : '发起人数',width:100,resizable:false,align:'left'},
                    {field : 'BargainPersonCount',title : '帮砍人数',width:100,resizable:false,align:'left'},
                    {field : 'BeginTime',title : '开始时间',width:150,align:'left',resizable:false
                        ,formatter:function(value ,row,index) {
                        if (value) {
                            return value
                        }
                    }
                    },
                    {field : 'EndTime',title : '结束时间',width:150,align:'left',resizable:false
                        ,formatter:function(value ,row,index) {
                            if (value) {
                                return value
                            }
                        }
                    },

                    {field : 'Status',title : '状态',width:100,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                switch(value){
                                    case 1:
                                        value = "未开始";
                                        break;
                                    case 2:
                                        value = "进行中";
                                        break;
                                    case 3:
                                        value = "已结束";
                                        break;
                                    // case 10:
                                    //     value = "暂停";
                                    //     break;
                                }
                                return value;
                            }
                        }},

                    {field : 'EventId',title : '操作',width:200,align:'left',resizable:false,
                        formatter: function (value, row, index) {
                            var status = row.Status;
                            var str='';
                            switch(status){
                                case 1:
                                    var str= "<div class='operation'><div data-index=" + index + " data-flag='watch' class='watch opt' title='延长时间'> </div>";
                                    str += "<div data-index=" + index + "  data-flag='stopOn' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='stopOn opt' title='提前结束'> </div>";
                                    str += "<div data-index=" + index + "  data-flag='exit' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='exit opt' title='编辑'> </div>";
                                    str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
                                    break;
                                case 2:
                                    var str = "<div class='operation'><div data-index=" + index + " data-flag='watchOn' class='watchOn opt' title='延长时间'> </div>";
                                    str += "<div data-index=" + index + "  data-flag='stop' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='stopOut opt' title='提前结束'> </div>";
                                    str += "<div data-index=" + index + "  data-flag='exit' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='exit opt' title='编辑'> </div>";
									str += "<div data-index=" + index + " data-flag='deleteOn' class='delete opt' title='删除'></div></div>";
									break;
                                case 3:
                                    var str = "<div class='operation'><div data-index=" + index + " data-flag='watch' class='watch opt'  title='结束' style='cursor:default'> </div>";
                                    str += "<div data-index=" + index + "  data-flag='stopOn' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='stopOn opt'> </div>";
									str += "<div data-index=" + index + "  data-flag='exit' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='exit opt' title='编辑'> </div>";
                                    str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
                                    break;
                                // case 10:
                                //     var str= "<div class='operation'><div data-index=" + index + " data-flag='pause' class='pauseBtn opt' title='暂停'> </div>";
                                //     str += "<div data-index=" + index + "  data-flag='radio' data-TypeName='" + row.EventName + "' data-TypeID='" + row.EventId + "' class='stopOn opt' title='编辑'> </div>";
                                //     break;
                            }
                            
                            
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
            that.loadData.getBargainList(function(data){
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
                EventID:'',
                start:''
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                EventName:'',
                EventStatus:'',
                BeginTime:'',
                EndTime:''
            },
            goods:{
                EventId:"",
                EventName:"",
                BeginTime:"",
                EndTime:""
            },
            opertionField:{},
            //新增砍价活动(编辑砍价活动)
            setBargain: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.SetBargain',
                        'EventId':this.args.EventID,
                        'EventName':this.goods.EventName,
                        'BeginTime':this.goods.BeginTime,
                        'EndTime':this.goods.EndTime,
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
							if(data.Data!=null){
								if (callback) {
									callback(data.Data);
								}
							}else {
                            alert(data.Message);
							}
                            
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取砍价活动列表
            getBargainList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.GetBargainList',
                         'EventName':this.seach.EventName,
                         'EventStatus':this.seach.EventStatus,
                         'BeginTime':this.seach.BeginTime,
                         'EndTime':this.seach.EndTime,
                        'PageIndex':this.args.PageIndex,
                        'PageSize':this.args.PageSize
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
            //提前结束活动
            setBargainStatus: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.SetBargainStatus',
                        'EventId':this.args.EventID
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
            //删除砍价活动列表
            deleteBargain: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.DeleteBargain',
                        'EventId':this.args.EventID,
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

                if(this.args.EventID.length>0){
                    prams.data.EventID=this.args.EventID;
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

