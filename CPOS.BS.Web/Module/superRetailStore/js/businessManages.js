define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'kkpager', 'artDialog', 'kindeditor','datetimePicker'], function ($) {
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
			offlineTabel: $("#gridTable2"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            editLayer: $("#editLayer"), //CSV文件上传
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId: '',
            click: true,
            ParentUnitIDLength: "",
            dataMessage: $("#pageContianer").find(".dataMessage"),
			dataMessage2: $("#pageContianer2").find(".dataMessage"),
            panlH: 116                             // 下来框统一高度
        },
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                            //标签类型
            tagList: []                              //标签列表
        },
        init: function () {
            var that = this;
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this;
			 //初始化日期控件
            $('#JoinSatrtTime').datetimepicker({
                lang:"ch",
                format:'Y-m-d H:i',
                step:5 //分钟步长
            });
            $('#JoinEndTime').datetimepicker({
                lang:"ch",
                format:'Y-m-d H:i',
                step:5 //分钟步长
            });
			
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var wd = 200, H = 30;
            //$('#StoreType').combobox("setValue",1);
            //类型
            $('#SuperRetailTraderFrom').combobox({
                width: wd,
                height: H,
                panelHeight: that.elems.panlH,
                valueField: 'SuperRetailTraderFrom',
                textField: 'text',
                data: [{
                    "SuperRetailTraderFrom": 'User',
                    "text": "店员"
                }, {
                    "SuperRetailTraderFrom": 'Vip',
                    "text": "会员"
                }, {
                    "SuperRetailTraderFrom": '',
                    "text": "请选择",
                    "selected": true
                }]
            });
			
			
            that.elems.tabelWrap.delegate(".offlineTextBox","click",function(e){
				//that.loadData.args.PageIndex = 1; 
				that.loadData.args.PageIndex2 = 1;
                var $this = $(this),
					id=$(this).data("id");
				that.loadData.args.superRetailTraderId = id;
				that.loadData.getOfflineNumList(function(data){
					that.renderOfflineTable(data);
				});
				$('#win').window({ title: "下线人数", width: 600, height: 610, top: 15, left: ($(window).width() - 550) * 0.5 });
				$('#win').window('open');
				$.util.stopBubble(e);
            });
			
			
			
			//导出退款单
            $('#exportBtn').bind("click", function (e) {
				that.ExportSalesReturnExcel();
				$.util.stopBubble(e);
				//that.ExportSalesReturnExcel(that.elems.operation.find("li.on").data("status"));
            });
			$('.saveBtn').bind('click',function(e){
				$('#win').window('close');
				//重新遍历数据
				that.loadData.getCommodityList(function (data) {
					that.renderTable(data);
				});
				$.util.stopBubble(e);
			})
			
			
			

            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true,
				onClose:function(){
					that.loadData.getCommodityList(function (data) {
						that.renderTable(data);
					});
				}
            });
            $('#panlconent').layout({
                fit: true
            });

			
            /**************** 弹出窗口初始化 end ****************/

        },

        //设置查询条件   取得动态的表单查询参数
        setCondition: function () {
            var that = this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex = 1;
            var fileds = $("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                filed.value = filed.value == "0" ? "" : filed.value;
                that.loadData.seach[filed.name] = filed.value;
            });
            //查询数据
            that.loadData.getCommodityList(function (data) {
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
            var that = this;
            if (!data.SuperRetailTraderList) {
                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //dan选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data.SuperRetailTraderList,
                sortName: 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'SuperRetailTraderID', //主键字段
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
                columns: [[
                    {
                        field: 'SuperRetailTraderName', title: '分销商姓名', width: 50, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
					{
                        field: 'SuperRetailTraderPhone', title: '手机号', width: 60, resizable: false, align: 'center', resizable: false, formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        field: 'SuperRetailTraderFrom', title: '分销商发展来源', width: 50, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
							switch(value){
								case 'User':
								  return '店员';
								  break;
								case 'Vip':
								  return '会员';
								  break;
								default:
								  return '';
							}
                        }
                    },
					{
					    field: 'NumberOffline', title: '下线人数', width: 50, align: 'center', resizable: false,
					    formatter: function (value, row, index) {
							//SuperRetailTraderID
							var htmlStr = '<span data-id="'+row.SuperRetailTraderID+'" class="offlineTextBox">'+value+'</span>';
                            return htmlStr;
					    }
					},
                    {
                        field: 'OrderCount', title: '订单总数', width: 50, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value
                        }
                    },
                    {
                        field: 'WithdrawCount', title: '已提现次数', width: 50, align: 'center', resizable: false, formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        field: 'WithdrawTotalMoney', title: '已提现总额(元)', width: 50, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
					{
                        field: 'JoinTime', title: '加盟时间', width: 80, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    }
					
					
                ]],

                onLoadSuccess: function (data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow: function (rowindex, rowData) {
                    /*
                     if(that.elems.click){
                     that.elems.click = true;
                     var mid = JITMethod.getUrlParam("mid");
                     location.href = "commodityExit.aspx?Item_Id=" + rowData.Item_Id +"&mid=" + mid;
                     }
					*/
                }, onClickCell: function (rowIndex, field, rowData) {
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
            data.Data = {};
            kkpager.generPageHtml({
				pagerid:'kkpager',
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPages,
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
        },
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            that.loadData.getCommodityList(function (data) {
                that.renderTable(data);
            });
        },



		
		//下线人数表格
        renderOfflineTable: function (data) {
            var that = this;
            if (!data.SuperRetailTraderList) {
                return;
            }
            that.elems.offlineTabel.datagrid({
                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: true, //dan选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data.SuperRetailTraderList,
                sortName: 'brandCode', //排序的列
                idField: 'SuperRetailTraderPhone', //主键字段
                columns: [[
                    {
                        field: 'SuperRetailTraderName', title: '分销商姓名', width: 33, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    },
					{
                        field: 'SuperRetailTraderPhone', title: '手机号', width: 33, resizable: false, align: 'center', resizable: false, formatter: function (value, row, index) {
                            return value;
                        }
                    },
                    {
                        field: 'JoinTime', title: '加盟时间', width: 34, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            return value;
                        }
                    }
                ]],
                onLoadSuccess: function (data) {
                    that.elems.offlineTabel.datagrid('clearSelections');
					if (data.rows.length > 0) {
                        that.elems.dataMessage2.hide();
                    }else{
                        that.elems.dataMessage2.show();
                    }
                },
                onClickRow: function (rowindex, rowData) {
                }, onClickCell: function (rowIndex, field, rowData) {
                }
            });
            //分页
            data.Data = {};
            kkpager.generPageHtml({
				pagerid:'kkpager2',
                pno: that.loadData.args.PageIndex2,
                mode: 'click', //设置为click模式
                total: data.TotalPages,//总页码
                totalRecords: data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                click: function (n) {
                    this.selectPage(n);
                    that.loadMoreData2(n);
                },
                getHref: function (n) {
                    return '#';
                }
            }, true);
        },
        loadMoreData2: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex2 = currentPage;
            that.loadData.getOfflineNumList(function(data) {
                that.renderOfflineTable(data);
            });
        },
		
		//根据form数据 和 请求地址 导出数据到表格
        exportExcel: function (data, url) {
            var dataLink = JSON.stringify(data);
            var form = $('<form>');
            form.attr('style', 'display:none;');
            form.attr('target', '');
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
        //导出退款列表
        ExportSalesReturnExcel: function () {
            page.setCondition();
			var obj = this.loadData.seach;
			obj.PageIndex = this.loadData.args.PageIndex;
			obj.PageSize = this.loadData.args.PageSize;
			var params = {
				"Parameters":obj,
				"locale":"zh"
			};
            var getUrl = '/ApplicationInterface/Module/SuperRetailTrader/SuperRetailTraderExport.ashx?req='+JSON.stringify(params);
            this.exportExcel(this.loadData.seach, getUrl);
        },
		
        loadData: {
            args: {
                bat_id: "1",
                PageIndex: 1,
				PageIndex2: 1,
                PageSize: 10,
                SearchColumns: {},    //查询的动态表单配置
                OrderBy: "",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status: -1,
                page: 1,
                start: 0,
                limit: 10
            },
            tag: {
                VipId: "",
                orderID: ''
            },
            seach: {
                SuperRetailTraderName:'',//分销商姓名
				SuperRetailTraderFrom:'',//分销商来源（User =员工、Vip=会员）
				JoinSatrtTime:'',//加盟开始时间
				JoinEndTime:''//加盟结束时
            },
            opertionField: {},
            getCommodityList: function (callback) {
				var params = {
					action: 'SuperRetailTrader.SuperRetailTraderConfig.GetSuperRetailTraderList',
					PageIndex: this.args.PageIndex,
					PageSize: this.args.PageSize
				};
				params = $.extend({}, params,this.seach);
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: params,
                    success:function(data){
                        if(data.IsSuccess && data.ResultCode==0){
							var reslut = data.Data;
                            if(callback){
                                callback(reslut);
                            }
                        }else{
                            alert(data.Message);
                        }
                    }
                });
            },
			getOfflineNumList: function (callback) {
				var params = {
					action: 'SuperRetailTrader.SuperRetailTraderConfig.GetSuperRetailTraderList',
					SuperRetailTraderID: this.args.superRetailTraderId,
					PageIndex: this.args.PageIndex2,
					PageSize: this.args.PageSize
				};
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: params,
                    success: function(data){
                        if(data.IsSuccess && data.ResultCode==0) {
							var reslut = data.Data;
                            if(callback){
                                callback(reslut);
                            }
                        }else{
                            alert(data.Message);
                        }
                    }
                });
            }
        }
    };
    page.init();
});

