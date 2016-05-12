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
			dataMessage:$(".dataMessage"),
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116,                          // 下来框统一高度
			width:232,
			height:32,
			userId:'',
			indx:-1,
			idText:'',
			rowParams:''
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
		bindSaveBtn: function(){
			var that = this;
			$('#staffSaveBtn').bind('click',function(){
				if($('#seach').form('validate')) {
					if($('.setUnitBtn.on').length==0){
						alert('至少一个设为所属单位！');
						return false;
					}
					$.util.isLoading();
					$('#staffSaveBtn').unbind('click');
                    var fields = $('#seach').serializeArray(),
						obj = {}; //自动序列化表单元素为JSON对象
					$.each(fields,function(i,para){
						//params.value=params.value=="0"?"":params.value;
						obj[para.name]=para.value;
					});
					obj["userRoleInfoList"] = $('#gridTable').datagrid('getData').rows;
					
					that.setStaffSave(obj);
					
				}
			});
		},
        initEvent: function () {
            var that = this;
			$('body').delegate('.commonSelectWrap','click',function(){
				$('.tooltip-right').hide();
			})
			/*待开发
			$('#roleIdBox').delegate('input','click',function(){
				alert(1);
				$('.tooltip-right').hide();
			})
			*/
			//添加角色保存数据
            $('#win').delegate(".saveBtn","click",function(e){
                if($('#addProm').form('validate')) {
                    var fields = $('#addProm').serializeArray(),
						row = that.elems.rowParams,
						obj = {Id:that.elems.idText}; //自动序列化表单元素为JSON对象
					if(row==''){
						obj["DefaultFlag"] = 0;
					}else{
						obj["DefaultFlag"] = row.DefaultFlag;
					}
					$.each(fields,function(i,para){
						//params.value=params.value=="0"?"":params.value;
						obj[para.name]=para.value;
					});
					
					obj['UnitName']=$('#type_id').combobox('getText');
					obj['ApplicationDescription']=$('#app_sys_id').combobox('getText');
					obj['RoleName']=$('#role_id').combobox('getText');
					
					
					//权限的前端数据保存
					if(that.elems.indx==-1){
						if(!$('.setUnitBtn',$('#tableWrap')).hasClass('on')){
							obj.DefaultFlag=1;
						}
						$('#gridTable').datagrid('insertRow',{
							index: 0,// 索引从0开始
							row: obj
							/*
							{
								DefaultFlag: 0,
								UnitName: '总部',
								ApplicationDescription: '管理后台',
								RoleName: '管理员',
								RoleId: "346e181932ce47599b8a0abd81ce9130",
								UnitId: "1090f6ea1ebc4d68861f6f3e138647ce",
								Id: ""
							}
							*/
						});
					}else{
						$('#gridTable').datagrid('updateRow',{
							index: that.elems.indx,
							row: obj
						});
					}
					
					
					$('#win').window('close')
					/*
                    that.operation(fields,function(data){
                        //alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);
                    });
					*/
                }
            });
			
			
			$('.addStaffBtn').on('click',function(){
				that.elems.indx = -1;
				that.elems.idText = '';
				that.elems.rowParams = '';
				that.addEditRoleDialog('');
			});
			
			
			//最后的保存
			that.bindSaveBtn();
			
			
			
			//监听删除，编辑事件
            that.elems.tabelWrap.delegate(".handle","click",function(e){
                var $this = $(this),
					$tr = $this.parents('tr'),
					flag = $this.data('flag'),
					rowIndex=$(this).data("index");
                that.elems.tabel.datagrid('selectRow', rowIndex);
				var row = that.elems.tabel.datagrid('getSelected'),
					userId = row.User_Id;	
				switch(flag){
					case 'setUnit':
						if($this.hasClass('on')){
							$.messager.confirm('提示', '您确定要将此单位取消所属单位？',function(r){
							   if(r){
								   $this.removeClass('on');
								   $('#gridTable').datagrid('updateRow',{
										index: rowIndex,
										row: {
											DefaultFlag : 0
										}
								   });
							   }
							});
						}else{
							$.messager.confirm('提示', '您确定要将此单位设为所属单位？',function(r){
							   if(r){
								   $('.setUnitBtn').removeClass('on');
								   $this.addClass('on');
								   
								   var leng = $('#gridTable').datagrid('getData').total;
								   
								   for(var i=0;i<leng;i++){
								   		$('#gridTable').datagrid('updateRow',{
											index: i,
											row: {
												DefaultFlag : 0
											}
									    });
								   }
								   //待开发$('#gridTable').datagrid('setColumnOption',{DefaultFlag : 0});
								   $('#gridTable').datagrid('updateRow',{
										index: rowIndex,
										row: {
											DefaultFlag : 1
										}
								   });
								   
								   //$('#gridTable').datagrid('acceptChanges');
								   //$('#gridTable').datagrid('getData').rows
								   
							   }
							});
						}
						
					break; 
					
					case 'edit':
						that.elems.indx = rowIndex;
						that.elems.idText = row.Id;
						that.elems.rowParams = row;
						that.addEditRoleDialog(row);//userId
					break;
					
					case 'delete':
						$.messager.confirm('提示', '您确定要删除该权限吗？',function(r){
						   if(r){
							   /*
							   that.setRemoveUser(userId,function(data){
								   $tr.remove();
							   });
							   */
							   $('#gridTable').datagrid('deleteRow',rowIndex);
							   var obj = $('#gridTable').datagrid('getData').rows;
							   $('#gridTable').datagrid('loadData',obj);
						   }
						});
					break;
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
			that.elems.userId=JITMethod.getUrlParam("userId");
			
			//获取新建用户角色信息
			that.setCondition();
			that.getUserInfo(that.elems.userId);
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
                //sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'User_Id', //主键字段
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
                    {field : 'DefaultFlag',title : '设为所属单位',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
						   var tag = value?'on':'';
                           var htmlStr='<a href="javascript:;" data-flag="setUnit" data-index='+index+' class="handle setUnitBtn '+tag+'" title="设为所属单位"></a>';
                            return htmlStr;
                        }
						//待开发
                    },
                    {field : 'UnitName',title : '单位',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                             return value;
                        }
                    },
					{field : 'ApplicationDescription',title : '系统',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           	return value;
                        }
                    },
					{field : 'RoleName',title : '角色',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                             return value;
                        }
                    },
					
					{field : 'User_Id',title : '操作',width:50,align:'left',resizable:false,
                        formatter:function(value ,row,index){
						   var text = row.User_Status==1?'running':'pause';
                           var htmlStr='<a href="javascript:;" data-flag="edit" data-index='+index+' class="handle editBtn opt exit" title="编辑"></a>\
								<a href="javascript:;" data-flag="delete" data-index='+index+'  class="handle deleteBtn opt delete" title="删除"></a>';
                            return htmlStr;
                        }
                    }

                ]],
                onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
					/*
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.show();
                    }
					*/
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

			/*
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
			*/
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
			
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },
		setStaffSave:function(params){
			var that = this;
			$.util.oldAjax({
				url: "/module/Basic/User/Handler/UserHandler.ashx",
				data: {
					"action":'user_save',
					"user_id":that.elems.userId,
					"user":JSON.stringify(params)
				},
				success: function(data){
					if(data.success){
						window.history.go(-1);
					}else{
						$.util.isLoading(true);
						alert(data.msg);
						that.bindSaveBtn();
					}
				}
			});
			
			
			
			
		},
		getUserInfo:function(userId){
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/user/Handler/UserHandler.ashx",
				data: {
					"action":'get_user_by_id',
					"user_id":userId
				},
				success: function(data){
					var result = data.data;
					if(result){
						$('#seach').form('load',result);
						if(result.User_Password != null){
							$('#User_Password').attr('readonly','readonly');
							$('#User_Code').attr('readonly','readonly');
						}
					}else{
						alert('加载数据失败');
					}
				}
			});
		
		},
		setRemoveUser:function(userId,callback){//删除员工
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/unit/Handler/UnitHandler.ashx",
				data: {
					"action":'unit_delete2',
					"ids":userId
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
		
		addEditRoleDialog:function(rowObj){
			//console.log(rowObj);
			
            var that=this,
				tit='新增角色';
			if(rowObj!=''){
				tit = '编辑角色';
			}	
            $('#win').window({title:tit,width:638,height:418,top:110,left:($(window).width() - 638) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addProm');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
			
			//获取单位数据
            that.loadData.getClassify(function(data) {
				//data[0].children.push({id:0,text:"请选择"});
				data.push({id:0,text:"请选择"});
                $('#type_id').combotree({
                    width:that.elems.width,
					height:that.elems.height,
                    //editable:true,
                    lines:true,
                    panelHeight:that.elems.panlH,
                    valueField: 'id',
                    textField: 'text',
                    data:data
                });
            });
			//获取系统数据
			that.getAppSys();
			//获取角色数据
			that.getRoleList('');
			
			if(!!rowObj){
				setTimeout(function(){
					$('#type_id').combotree('setValue',rowObj.UnitId);
					//$('#app_sys_id').combobox('setValue',rowObj.Id);
					//$('#type_id').combobox('setText',rowObj.UnitName);
					$('#app_sys_id').combobox('setText',rowObj.ApplicationDescription);
					$('#role_id').combobox('setValue',rowObj.RoleId);
				},3000);
			}
        },
		getAppSys:function(){
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
						$('#app_sys_id').combobox({
							width:that.elems.width,
							height:that.elems.height,
							panelHeight:115,
							editable:false,
							valueField: 'Def_App_Id',
							textField: 'Def_App_Name',
							data:result,
							onSelect: function(param){
								//获取角色数据
								that.getRoleList(param.Def_App_Id);
							}
						});
						//$dom.combobox('setText','请选择');
					}else{
						alert('数据库没有应用系统');
					}
				}
			});
		},
		getRoleList:function(id){//角色列表
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/role/Handler/RoleHandler.ashx",
				data: {
					action:'search_role',
					form:{"app_sys_id":id,"type_id":"","role_name":"","unit_id":""},
					page:1,
					start:0,
					limit:1000
				},
				success: function(data){
					if(data.totalCount){
						var result = data.topics;
						result.push({Role_Id:0,Role_Name:"请选择"});
						$('#role_id').combobox({
							width:that.elems.width,
							height:that.elems.height,
							panelHeight:115,
							editable:false,
							valueField: 'Role_Id',
							textField: 'Role_Name',
							data:result
						});
						//$dom.combobox('setText','请选择');
					}else{
						alert('加载数据失败');
					}
				}
			});
		},
		setSaveRole:function(){
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/role/Handler/RoleHandler.ashx",
				  data:{
					  action:'role_save',
					  role_id:'',
					  role:''
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
		getLimitsTree:function(roleId){
			var that = this;
			$.util.oldAjax({
				url: "/Module/Basic/Role/Handler/RoleHandler.ashx",
				  data:{
					  action:'get_sys_menus_by_role_id',
					  role_id:roleId,
					  app_sys_id:'D8C5FF6041AA4EA19D83F924DBF56F93'
				  },
				  success: function (data) {
					if(data.totalCount) {
						var result = data.data;
						/*
						$("#limitsTreeBox").tree({
							//animate:true
							lines: true,
							checkbox: true,
							valueField: 'Menu_Id',
							textField: 'Menu_Name',
							data: result
						});
						*/
						$('#limitsTreeBox').combotree({
							width:that.elems.width,
							height:that.elems.height,
							//editable:true,
							lines:true,
							panelHeight:500,
							valueField: 'id',
							textField: 'Menu_Name',
							data:result
						});
						
						
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
                limit:100
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
				"user_code":"",
				"user_name":"",
				//"user_status":"1",
				"role_id":"",
				"unit_id":""
			},
            opertionField:{},
			
            getCommodityList: function (callback) {
                $.util.oldAjax({
                    url: "/module/basic/user/Handler/UserHandler.ashx",
                      data:{
                          action:'get_user_role_info_by_user_id',
						  user_id:JITMethod.getUrlParam("userId") || ''
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
                $.util.oldAjax({
                    url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
                    data:{
						hasShop:1
					},
                    success: function(data){
                        if(data){
                            if(callback)
                                callback(data);
                        }
                        else{
                            alert('数据加载失败');
                        }
                    }
                });
            }
			
			
        }
    };
    page.init();
});

