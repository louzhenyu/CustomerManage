﻿define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
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
			height:32
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
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
            });
			
			
			
			//添加角色保存数据
            $('#win').delegate(".saveBtn","click",function(e){
                if($('#addProm').form('validate')) {
                    var fields = $('#addProm').serializeArray(); //自动序列化表单元素为JSON对象
                    that.operation(fields,function(data){
                        //alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);
                    });
                }
            });
			
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
					case 'reset':
						$.messager.confirm('重置密码', '您确定要密码重置为888888吗？',function(r){
						   if(r){
							   that.resetPassWord(userId);
						   }
						});
					break; 
					
					case 'edit':
						location.href = "staffDetail.aspx?userId=" + userId +"&mid=" + JITMethod.getUrlParam("mid");
						//that.addEditRoleDialog(userId,);
					break;
					
					case 'pause':
						that.setStatusEvent(userId,1,function(){
							$this.attr('class','handle runningBtn').data('flag','running');
							$('td[field="User_Status_Desc"]',$tr).find('div').text('正常');
						});
					break;
					case 'running':
						that.setStatusEvent(userId,-1,function(){
							$this.attr('class','handle pauseBtn').data('flag','pause');
							$('td[field="User_Status_Desc"]',$tr).find('div').text('停用');
						});
					break;
					
					case 'delete':
						$.messager.confirm('员工删除', '您确定要删除该员工吗？',function(r){
						   if(r){
							   that.setRemoveUser(userId,function(data){
								   $tr.remove();
							   });
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
			
			$('#addUserBtn').on('click',function(){
				location.href = "staffDetail.aspx?userId=&mid=" + JITMethod.getUrlParam("mid");
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
                //data[0].children.push({id:0,text:"请选择"});
				data.push({id:0,text:"请选择"});
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
			//获取角色列表
			that.getRoleList();
            
			
			
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
                    {field : 'User_Code',title : '用户名',width:50,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return value;
                        }
                    },
                    {field : 'User_Name',title : '姓名',width:30,align:'center',resizable:false,formatter:function(value ,row,index){
                            return value;
                        }
					},
                    {field : 'UnitName',title : '单位',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                             return value;
                        }
                    },
					{field : 'role_name',title : '角色',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                             return value;
                        }
                    },
					{field : 'User_Status_Desc',title : '状态',width:30,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           	return value;
                        }
                    },
					{field : 'WqrURL',title : '下载二维码',width:30,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                           	return value?'<span data-url="'+value+'" class="">下载</span>':'';
                        }
                    },
					{field : 'User_Id',title : '操作',width:90,align:'center',resizable:false,
                        formatter:function(value ,row,index){
						   var text = row.User_Status==1?'running':'pause';
                           var htmlStr='<a href="javascript:;" data-flag="reset" data-index='+index+' class="handle resetBtn" title="重置密码"></a>\
								<a href="javascript:;" data-flag="edit" data-index='+index+' class="handle editBtn" title="编辑"></a>\
								<a href="javascript:;" data-flag="'+text+'" data-index='+index+'  class="handle '+text+'Btn" title="启用"></a>\
								<a href="javascript:;" data-flag="delete" data-index='+index+'  class="handle deleteBtn" title="删除"></a>';
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
		resetPassWord: function(userId){
			$.util.oldAjax({
				url: "/module/basic/user/Handler/UserHandler.ashx",
				data:{
					action:'revertPassword',
					user:userId,
					password:'888888'
				},
				success: function(data){
					if(data.success){
						alert('密码重置成功！');
					}
					else{
						alert(data.msg);
					}
				}
			});
		},
		setStatusEvent:function(userId,tag,callback){
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/user/Handler/UserHandler.ashx",
				data: {
					"action":'user_delete',
					"ids":userId,
					"status":tag //停用时传-1，启用时传1
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
		setRemoveUser:function(userId,callback){//删除员工
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/user/Handler/UserHandler.ashx",
				data: {
					"action":'user_delete2',
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

		getRoleList:function(){//角色列表
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/role/Handler/RoleHandler.ashx",
				data: {
					action:'search_role',
					form:{"app_sys_id":null,"type_id":"","role_name":"","unit_id":""},
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
		addEditRoleDialog:function(data){
            var that=this;
            $('#win').window({title:"新建角色",width:600,height:600,top:15,left:($(window).width() - 550) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addProm');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
			
			
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
			
			
			//拥有的权限
			//that.getLimitsTree('');
			
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
							editable:true,
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
                limit:9
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
                          action:'search_user',
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

