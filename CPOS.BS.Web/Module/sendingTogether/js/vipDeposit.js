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
            tabel:$("#dataTable"),                   //表格body部分
            thead:$("#thead"),                    //表格head部分
            dialogLabel:$("#dialogLabel"),          //标签选择层
            menuItems:$("#menuItems"),             //针对表格的操作菜单
            resultCount: $("#resultCount"),          //所有匹配的结果
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
            that.getTableInfo(function(data){
                debugger;
                if(!data.Data.WithdrawDepositList){
                    return;
                }
                //jQuery easy datagrid  表格处理
                that.elems.tabel.datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, // 多选
                    // height : 330, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.Data.WithdrawDepositList,
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

                        {field : 'WithdrawNo',title : '提现单号',width:300,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },

                        {field : 'ApplyDate',title : '申请日期',width:200,align:'left',resizable:false
                            ,formatter:function(value ,row,index){
                            return new Date(value).format("yyyy-MM-dd");
                        }
                        },
                        {field : 'VipName',title : '销售员名称',width:200,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(value&&value.length>long){
                                    return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                }else{
                                    return '<div class="rowText">'+value+'</div>'
                                }
                            }
                        },
                        {field : 'CardNo',title : '银行卡号',width:400,align:'left',resizable:false},
                        {field : 'BankName',title : '银行名称',width:200,align:'left',resizable:false},
                        {field : 'AccountName',title : '开户人姓名',width:200,align:'left',resizable:false},
                        {field : 'Amount',title : '提现金额',width:150,align:'left',resizable:false,
                            formatter:function(value,row,index){
                                if(isNaN(parseInt(value))){
                                    return 0;
                                }else{
                                    return value;
                                }
                            }},
                        {field : 'Status',title : '状态',width:100,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var status="";
                                switch (value){//0=待确认；1=已确认；2=已完成
                                    case 0 : status="待确认" ;break;
                                    case 1 : status="已确认" ;break;
                                    case 2 : status="已完成" ;break;
                                }
                                return  status ;
                            }
                        },
                        {field : 'CompleteDate',title : '完成日期',width:200,align:'left',resizable:false
                            ,formatter:function(value ,row,index){
                            if(value) {
                                return new Date(value).format("yyyy-MM-dd");
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




                if (data.Data.TotalPageCount >0) {
                    kkpager.generPageHtml(
                        {
                            pno: page.params.pageIndex,
                            mode: 'click', //设置为click模式
                            //总页码
                            total: data.Data.TotalPageCount,
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
                                //点击下一页或者上一页,进行加载数
                                page.params.pageIndex = --n;
                                that.renderTable();
                            },
                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                            getHref: function (n) {
                                return '#';
                            }

                        }, true);

                }
            });
        },
        initEvent: function () {
            $("#cc").combobox({//0待确认；1=已确认；2=已完成
                valueField:'id',
                textField:'text',
                width:200,
                height:30,
                data:[{
                    "id":"-1",
                    "text":"全部",
                    "selected":true
                },{
                    "id":0,
                    "text":"待确认"
                },{
                    "id":1,
                    "text":"已确认"

                },{
                    "id":2,
                    "text":"已完成"
                }]
            });
            var that = this;
			//绑定搜索按钮事件
			$('.queryBtn').on('click',function(){
                debugger;
             var fileds =$("#queryFrom").serializeArray();
                $.each(fileds,function(index,filed){
                    if(filed.value==-1){
                        filed.value=""
                    }
                    page.params[filed.name] = filed.value;
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

                if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,that.elems.optionType,function(data){
                        $('#win').window('close');
                        alert("操作成功");

                        that.loadPageData(e);

                    });
                }
            });
            /**************** -------------------弹出窗口初始化 end****************/


            $('#menuItems .exportBtn').on('click',function(e) {
                        alert("打印功能开发中") ;
            });
			
			//绑定确认按钮事件
			$('#menuItems .affirmBtn,#menuItems .finishBtn').on('click',function(e) {
               var dataAll=that.elems.tabel.datagrid("getChecked");
                var str=""
                    if(dataAll.length>0){
                        if($(this).data("statusid")==1){
                             str="是否确定申请提现通过";

                        }
                        if($(this).data("statusid")==2){
                            str="是否确定提现已完成" ;

                        }
                        debugger
                        var applyId=dataAll[0].ApplyID;
                        var  str="";
                        var isSubmit=false;
                        if (dataAll[0].Status==2)
                        {

                            isSubmit=true;

                        }
                        for(var i=1;i<dataAll.length;i++)
                        {
                            str+=dataAll[i].ApplyID+",";
                           if (dataAll[i].Status==2)
                            {

                                isSubmit=true;
                                 break;
                            }
                        }
                        if(isSubmit){

                            $.messager.alert("提示","选项中有已完成的选项！请取消") ;
                            return false;
                        }

                        applyId=str+applyId;
                        var Status= $(this).data("statusid");
                        $.messager.confirm("操作提示",str,function(r){
                            if(r) {
                               that.submitAuditStatus(Status,applyId,function(){
                                   that.renderTable();
                                   alert("操作成功")
                               }) ;
                            }
                        });
                    } else{
                        alert("没有选中提现记录");
                    }
                that.stopBubble(e);
            });
			
        },
		submitAuditStatus: function(statusId,applyId,callback){
			$.util.ajax({
				url: "/ApplicationInterface/Vip/WithdrawDepositGateway.ashx",
				data: {
					action: 'UpdateWDApply',
					ApplyID: applyId,
					Status: statusId
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
		getTableInfo: function(callback){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Vip/WithdrawDepositGateway.ashx",
				data: {
					action: 'GetWithdrawDeposit',
					PageSize: 10,
					PageIndex: page.params.pageIndex,
					WithdrawNo:page.params.WithdrawNo,
					VipName:page.params.VipName,
					Status:page.params.Status,
                    IsVip:"2" //店员/**88 88888888888888888888888888888888888888888888888888*/
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