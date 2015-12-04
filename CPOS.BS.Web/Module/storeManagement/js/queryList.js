define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'artDialog', 'kindeditor'], function ($) {


    KE = KindEditor;


    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            editLayer: $("#editLayer"), //CSV文件上传
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
			dataMessage:$(".dataMessage"),
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116                             // 下来框统一高度
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var that = this;
            this.initEvent();
            this.loadPageData();



            //导入导出门店初始化
            setTimeout(function () {
                that.inportStoreDialog();

            }, 500);


            that.registerUploadCSVFileBtn();



        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;
			//状态
            $('#unit_status').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'unit_status',
                textField: 'text',
                data:[{
                    "unit_status":1,
                    "text":"正常"
                },{
                    "unit_status":-1,
                    "text":"停用"
                },{
                    "unit_status":0,
                    "text":"请选择"
                }]
            });
			//$('#unit_status').combobox("setValue",1);
			
			//类型
			$('#StoreType').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'StoreType',
                textField: 'text',
                data:[{
                    "StoreType":'DirectStore',
                    "text":"直营店"
                },{
                    "StoreType":'NapaStores',
                    "text":"加盟店"
                },{
                    "StoreType":'',
                    "text":"请选择"
                }]
            });
			
           	//组织层级
            that.loadData.getClassify(function(data) {
                if(!(data&&data.length>0)){
                    data=[];
                }
                data.push({id: 0, text: "请选择"});
                $('#Parent_Unit_ID').combotree({
                    width:wd,
                    height:H,
                    editable:true,
                    lines:true,
                    panelHeight:that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data:data
                });
            });
			
			/*
            that.loadData.args.bat_id='2';
            that.loadData.getClassify(function(data) {
                $("#Tooltip").find(".treeNode").tree({
                   //animate:true,
                    checkbox:true,
                    valueField: 'id',
                    textField: 'text',
                    data:data
                });
                data.push({id:0,text:"请选择"});
                $('#SalesPromotion_id').combobox({
                    width: wd,
                    height: H,
                    panelHeight: that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data: data
                });
            });
			*/

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
			/*
            $('#win').delegate(".saveBtn","click",function(e){
                if($('#payOrder').form('validate')) {
                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象
                    that.loadData.operation(fields,that.elems.optionType,function(data){
                        alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);
                    });
                }
            });
			*/
			//监听暂停，启用
            that.elems.tabelWrap.delegate(".handle","click",function(e){
                var $this = $(this),
					$tr = $this.parents('tr'),
					rowIndex=$(this).data("index"),
					optType=$(this).attr('class');
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="handle iconPlay"){
					that.statusEvent(row.Id,-1,$this);
                }
				if(optType=="handle iconPause"){
                    that.statusEvent(row.Id,1,$this);
                }
				$.util.stopBubble(e);
            });
			
			//跳转详情页
            that.elems.tabelWrap.delegate(".datagrid-btable tr","click",function(e){
                var $this = $(this),
					mid = JITMethod.getUrlParam("mid"),
					rowIndex=$(this).attr("datagrid-row-index");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
				
				location.href = "storeInfo.aspx?Item_Id=" + row.Id +"&mid=" + mid;
				$.util.stopBubble(e);
            });
			
			$('#addStoreBtn').on('click',function(){
				location.href = "storeInfo.aspx?Item_Id=&mid=" + JITMethod.getUrlParam("mid");
			});



		
            //导入门店
			$('#inportStoreBtn').on('click', function () {


			    $('.inputCount').text("0");
			    that.elems.editLayer.find("#nofiletext").show();
			    that.elems.editLayer.find(".CSVFilelist").empty();
			    $('#startinport').show();
			    $('#closebutton').hide();
			    $('.step').hide();
			    $('#step1').show();

			    $('#win').window('open');
			});

            //开始导入
			$('#win').delegate(".saveBtn", "click", function (e) {

			    var CSVFileurl = $('#CSVFileurl').val();
			    if (CSVFileurl != "") {
			        $('#startinport').hide();
			        $('.step').hide();
			        $('#step2').show();
			        that.inportEvent();
			    } else {
			        $.messager.alert("提示",'请选择文件！');
			    }
			});

            //关闭
			$('#win').delegate(".closeBtn", "click", function (e) {

			    $('#win').window('close');
			});

            /**************** 弹出窗口初始化 end ****************/
			
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex = 1;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="0"?"":filed.value;
                that.loadData.seach[filed.name]=filed.value;
            });
			 //查询数据
			that.loadData.getCommodityList(function(data){
				that.renderTable(data);
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
            if(!data.topics){
                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //dan选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.topics,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Id', //主键字段
                /* pageNumber:1,*/
                /*
                frozenColumns:[[
                    {
                        field : 'ck',
                        width:70,
                        title:'全选',
                        align:'center',
                        checkbox : true
                    }
                ]],
				*/
                columns : [[
                    {field : 'Name',title : '店名',width:70,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    {field : 'Contact',title : '联系人',width:125,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    {field : 'Telephone',title : '电话',width:58,resizable:false,align:'center',resizable:false,formatter:function(value ,row,index){
                            return value;
                        }
					},
                    {field : 'Parent_Unit_Name',title : '上级组织',width:58,align:'center',resizable:false,
                        formatter:function(value,row,index){
                           return value
                        }
                    },
                    {field : 'StoreType',title : '类型',width:60,align:'center',resizable:false,formatter:function(value ,row,index){
                            var staus;
                            switch (value){
                                case "DirectStore": staus="直营店";break;

                                case "NapaStores": staus= "加盟店"; break;
                            }
                            return staus;
                        }
					},
                    {field : 'Status',title : '状态',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var staus;
                            switch (value){
                                case "1": staus="正常";break;

                                case "-1": staus= "停用"; break;
                            }
                            return staus;
                        }
                    },
					{field : 'Id',title : '操作',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           var htmlStr = '',
						   	   status = row.Status;
                            switch(status){
                                case "1": htmlStr='<a href="javascript:;" class="handle iconPlay" data-index='+index+'></a>';break;

                                case "-1": htmlStr='<a href="javascript:;" class="handle iconPause" data-index='+index+'></a>'; break;
                            }
                            return htmlStr;
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
					/*
                     if(that.elems.click){
                     that.elems.click = true;
                     var mid = JITMethod.getUrlParam("mid");
                     location.href = "commodityExit.aspx?Item_Id=" + rowData.Item_Id +"&mid=" + mid;
                     }
					*/
                },onClickCell:function(rowIndex, field, rowData){
					 /*
                     if(field=="Id"){//在每一列有操作 而点击行有跳转页面的操作才使用该功能。此处不释与注释都可以。
                     	that.elems.click=false;
                     }else{
                     	that.elems.click=true;
                     }
					 */
                }

            });


            //分页
            data.Data={};
            data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            var page=parseInt(that.loadData.args.start/15);
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPage,
                totalRecords: data.totalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function(n){
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
            that.loadData.args.PageIndex = currentPage;
			
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },

        //取消订单
        cancelOrder:function(data){
            var that=this;
            that.elems.optionType="cancel";
            $('#win').window({title:"取消订单",width:360,height:260});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            this.loadData.tag.orderID=data.OrderID;
            $('#win').window('open');
        },
		statusEvent:function(id,tag,$dom){
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/unit/Handler/UnitHandler.ashx",
				data: {
					"mid":$.util.getUrlParam('mid'),
					"action":'unit_delete',
					"status":tag,
					"ids":id
				},
				success: function(data){
					if(data.success){
						if(tag==1){
							$dom.attr('class','handle iconPlay');
						}else{
							$dom.attr('class','handle iconPause');
						}
					}else{
						alert(data.msg);
					}
				}
			});
		},


        /*导入start*/

        //执行导入
		inportEvent: function () {
		    var that = this;
		    var mid = JITMethod.getUrlParam("mid");
		    $.util.oldAjax({
		        url: "/module/basic/Unit/Handler/UnitHandler.ashx",
		        data: {
		            "mid": $.util.getUrlParam('mid'),
		            "action": 'ImportUnit',
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
		                if (data.data.ErrCount > 0 || data.data.TotalCount==0) {
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


		    $('#win').window({ title: "导入门店", width: 696, height: 600, top: 15, left: ($(window).width() - 550) * 0.5 });
		    
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
		                $.messager.alert("提示", "上传的文件格式只能是.xls、.xlsx和.csv！");

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

        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 6,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:4
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
				unit_name:'',//门店名称
				unit_status:'',//int	否	状态（1:正常，０：失效）
				StoreType:'',//string	否	门店类型 直营店：DirectStore，加盟店：NapaStores
				Parent_Unit_ID:'',
				OnlyShop:'1' //int	是	只取门店（是：1,否：０）
			},
            opertionField:{},

            getCommodityList: function (callback) {
                debugger;
                $.util.oldAjax({
                    url: "/module/basic/unit/Handler/UnitHandler.ashx",
                      data:{
                          action:'search_unit',
                          page:this.args.PageIndex,
                          //start:this.args.start,
                          limit:this.args.limit,
                          form:this.seach
                      },
                      success: function (data) {
                        if (data.topics) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
            getClassify: function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
                    data:{
						hasShop:0
                    },
                    success: function(data){
                        if(data){
                            if(callback)
                                callback(data);
                        }
                        else{
                            alert("门店数据加载不成功");
                        }
                    }
                });
            }
        }
    };
    page.init();
});

