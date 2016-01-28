define(['jquery','json2','template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
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
			dataMessage:$(".dataMessage"),
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116,                          // 下来框统一高度
			width:232,
			height:32,
			roleId:'',
			isSys:0
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
			
			//添加编辑角色初始化
			setTimeout(function(){
				that.addEditRoleDialog();
			},500);
			
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
            });
			//$.util.stopBubble(e);
			
            /**************** -------------------弹出窗口初始化 start****************/
			/*
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
			*/
			
			//添加角色保存数据
            $('#win').delegate(".saveBtn","click",function(e){
                if($('#addProm').form('validate')){
                    var fields = $('#addProm').serializeArray(), //自动序列化表单元素为JSON对象
						params = {"Is_Sys":that.elems.isSys};
					//console.log(fields);
					$.each(fields,function(i,para){
						//params.value=params.value=="0"?"":params.value;
						params[para.name]=para.value;
					});
					params['RoleMenuInfoList'] = [];
					var nodes = $('#limitsTreeBox').tree('getChecked',['checked','indeterminate']);//
					if(!nodes.length){
						$('.icon-tipBox').show();
						setTimeout(function(){
							$('.icon-tipBox').hide();
						},1000);
						return false;
					}
					$.each(nodes,function(i,para){
						params[para.name]=para.value;
						params['RoleMenuInfoList'].push({Menu_Id:para.Menu_Id});
					});
					
					params = JSON.stringify(params);
					//console.log(params);
					//return false;
                    that.setSaveRole(params,function(data){
                        $('#win').window('close');
                        that.loadPageData(e);
                    });
					
					//角色名称 Role_Name
					//所属层级标 识type_id
					//应用系统 Def_App_Id
					//系统保留（1:是，０：否）Is_Sys that.elems.isSys 
					 
                }
            });
			
			//监听删除，编辑事件
            that.elems.tabelWrap.delegate(".handle","click",function(e){
                var $this = $(this),
					$tr = $this.parents('tr'),
					flag = $this.attr('class'), 
					rowIndex=$(this).data("index");
				that.elems.tabel.datagrid('selectRow', rowIndex);	
				var row = that.elems.tabel.datagrid('getSelected');
				
                if(flag == 'handle deleteBtn'){
					$.messager.confirm('提示', '您确定要删除该角色吗？',function(r){
					   if(r){
						   that.setRemoveRole(row.Role_Id,function(data){
							   $tr.remove();
						   });
					   }
				    });
                }else if(flag == 'handle editBtn'){
					that.elems.roleId = row.Role_Id;
					that.elems.isSys = row.Is_Sys;
					$('#role_name2').val(row.Role_Name);
					$('#type_id2').combotree('setValue',row.type_id);
					$('#app_sys_id2').combobox('setValue',row.Def_App_Id);
					
					//拥有的权限
					that.getLimitsTree(row.Role_Id,row.Def_App_Id);
					$('#win').window({'title':'编辑角色'});
					$('#win').window('open');
				}
				$.util.stopBubble(e);
            });
			
			//跳转详情页
			/*
            that.elems.tabelWrap.delegate(".datagrid-btable tr","click",function(e){
                var $this = $(this),
					mid = JITMethod.getUrlParam("mid"),
					rowIndex=$(this).attr("datagrid-row-index");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
				
				location.href = "storeInfo.aspx?Item_Id=" + row.Id +"&mid=" + mid;
				$.util.stopBubble(e);
            });
			*/
			
			$('#addRoleBtn').on('click',function(){
				that.elems.roleId = '';
				that.elems.isSys = 0;
				$('#role_name2').val('');
				$('#type_id2').combotree('setValue',0);
				$('#app_sys_id2').combobox('setValue',0);
				//拥有的权限
				that.getLimitsTree('','');
				$('#win').window({'title':'新建角色'});
				$('#win').window('open');
			});
			
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
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
			
			//获取所属层级数据
            that.loadData.getClassify(function(data) {
                data[0].children.push({id:0,text:"请选择"});
                $('#type_id').combotree({
                    width:that.elems.width,
					height:that.elems.height,
                    editable:true,
                    lines:true,
                    panelHeight:that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data:data
                });
            });
			//获取应用系统数据
			that.getAppSys($('#app_sys_id'));
            
			
			
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
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.topics,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Role_Id', //主键字段
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
                    {field : 'Role_Name',title : '角色',width:70,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    {field : 'type_name',title : '所属层级',width:60,align:'center',resizable:false,formatter:function(value ,row,index){
                            return value;
                        }
					},
                    {field : 'Def_App_Name',title : '应用系统',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                             return value;
                        }
                    },
					{field : 'Role_Id',title : '操作',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           var htmlStr='<a href="javascript:;" title="编辑" data-index='+index+' class="handle editBtn"></a><a href="javascript:;" title="删除" class="handle deleteBtn"  data-index='+index+' ></a>';
                            return htmlStr;
                        }
                    }


                ]],
                onLoadSuccess : function(data) {
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
		setRemoveRole:function(roleId,callback){//删除角色
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/role/Handler/RoleHandler.ashx",
				data: {
					"action":'role_delete',
					"ids":roleId
				},
				success: function(data){
					if(data.success){
						if(callback){
							callback();
						}
					}else{
						alert(data.msg);
					}
				}
			});
		},
		getAppSys:function($dom){//应用系统
			var that = this;
			$.util.oldAjax({
				url: "/Framework/Javascript/Biz/Handler/AppSysHandler.ashx",
				data: {
					"action":'get_app_sys_list'
				},
				success: function(data){
					if(data.totalCount){
						var result = data.data;
						result.push({Def_App_Id:0,Def_App_Name:"请选择"});
						$dom.combobox({
							width:that.elems.width,
							height:that.elems.height,
							panelHeight:115,
							valueField: 'Def_App_Id',
							textField: 'Def_App_Name',
							data:result,
							onSelect: function(param){
								if($dom.attr('id')=='app_sys_id2'){
									//待开发
									var roleId=$('#type_id2').combobox('getValue');
									roleId = roleId==0?'':roleId;
									that.getLimitsTree(roleId,param.Def_App_Id);
								}
							}
						});
						//$dom.combobox('setText','请选择');
					}else{
						alert('数据库没有应用系统');
					}
				}
			});
		},
		addEditRoleDialog:function(data){
            var that=this;
            $('#win').window({title:"新建角色",width:600,height:600,top:($(window).height() - 600) * 0.5,left:($(window).width() - 600) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addProm');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            //$('#win').window('open');
			
			
			//获取所属层级数据
            that.loadData.getClassify(function(data) {
				data[0].children.push({id:0,text:"请选择"});
                $('#type_id2').combotree({
                    width:that.elems.width,
					height:that.elems.height,
                    editable:true,
                    lines:true,
                    panelHeight:that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data:data
                });
				
				
            });
			//获取应用系统数据
			that.getAppSys($('#app_sys_id2'));
			
			

			/*
			$('#Category').combotree({
				width: that.elems.width,
				height: that.elems.height,
				panelHeight: that.elems.panlH,
				lines:true,
				valueField: 'id',
				textField: 'text',
				data: that.elems.categoryList
			});

            if(data) {
                $("#editLayer").find(".imgPanl img").attr("src",data.ImageUrl);
            }
			*/
        },
		setSaveRole:function(params,callback){
			var that = this;
			$.util.oldAjax({
				  url: "/module/basic/role/Handler/RoleHandler.ashx",
				  data:{
					  action:'role_save',
					  role_id:that.elems.roleId,
					  role:params
				  },
				  success: function (data) {
					if(data.success){
						if(callback){
							callback(data);
						}
					}else{
						alert(data.msg);
					}
				}
			});
		},
		getLimitsTree:function(roleId,defAppId){
			var that = this;
			$.util.oldAjax({
				url: "/Module/Basic/Role/Handler/RoleHandler.ashx",
				  data:{
					  action:'get_sys_menus_by_role_id',
					  role_id:roleId,
					  app_sys_id:defAppId
				  },
				  success: function (data) {
					if(data.totalCount) {
						var result = data.data;
						
						$("#limitsTreeBox").tree({
							//animate:true,
							//lines: true,
							checkbox: true,
							cascadeCheck: false,
							//valueField: 'id',
							//textField: 'text',
							data: result
						});
						
						/*
						$('#limitsTreeBox').combotree({
							width:that.elems.width,
							height:that.elems.height,
							editable:true,
							lines:true,
							panelHeight:500,
							valueField: 'id',
							textField: 'Menu_Name',
							data:result
						});
						*/
						
					}else{
						alert("加载数据不成功");
					}
				}
			});
		},
        loadData: {
            args: {
                PageIndex: 1,
                PageSize: 6,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:10
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
				"app_sys_id":null,//应用系统
				"type_id":"",//所属层级
				"role_name":""//角色名
			},
            opertionField:{},

            getCommodityList: function (callback) {
                $.util.oldAjax({
                    url: "/module/basic/role/Handler/RoleHandler.ashx",
                      data:{
                          action:'search_role',
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
                    url: "/ApplicationInterface/Module/Basic/UnitAndType/TypeTreeHandler.ashx",
                    data:{},
                    success: function(data){
                        if(data){
                            if(callback)
                                callback(data);
                        }
                        else{
                            alert('数据没有所属层级');
                        }
                    }
                });
            }
        }
    };
    page.init();
});

