define(['jquery','template','tools','langzh_CN','easyui','artDialog','kkpager','kindeditor'],function($){
    //上传图片
    KE = KindEditor;
    var page = {
        elems: {
            optPanel:$("#optPanel"), //页面的顶部li
            submitBtn:$("#submitBtn"),//提交和下一步按钮
            section:$("#section"),
            editLayer:$(".uploadPicBox"), //图片上传
            simpleQuery:$("#simpleQuery"),//全部
            width:160,
            height:32,
            panlH:200,
			hasSave:0,
			unitId:'',
			typeId:'',
			parendId:'',
			nextTypeId:'',
			hasAddChild:0,
			atJson:{},
			isFirst:1,
			initLevel:0,
			domain:''
        },
        init: function(){
            this.loadDataPage();
            this.initEvent();
        },
		loadDataPage: function(){
			var that = this,
				$centreArea = $('.centreArea'),
				$contentArea = $('#contentArea'),
				h = $centreArea.height();
			$contentArea.css({'minHeight':h+'px'});
			$('#nav01').fadeIn("slow");
			//$('#nav02').fadeIn("slow");
			
			//第一步
			that.getFirstStep();
			
		},
        initEvent:function() {
            var that = this;
			
			//点击第一步保存
			$('.commonStepBtn').on('click',function(){
				/*
				if(that.elems.initLevel == 2){
				}else if(that.elems.initLevel == 3){
				}else if(that.elems.initLevel == 4){
				}else if(that.elems.initLevel == 5){
				}
				*/
				var $this = $(this),
					idVal = $this.data('flag'),
					$panelDiv = $('.panelDiv');
				switch(idVal){
					  case '#nav01':
					  	that.elems.optPanel.find("li").removeClass("on");
						that.elems.optPanel.find("li").eq(0).addClass("on");
						$panelDiv.hide();
						$(idVal).fadeIn("slow");
						break;
					  case '#nav02':
					  	if($('#nav0_1').form('validate')){
							var prams = {action:'SaveTypeList'},
								fields = $('#nav0_1').serializeArray(),
								typeArray = [],
								j = 0,
								k = 1;
							//console.log(fields);
							for(var i=0;i<fields.length;i++){
								var obj = fields[i];
								if(obj['value'] != ''){
									if(obj['name'] == 'type_name'){
										typeArray[j] = {};
										typeArray[j][obj['name']] = obj['value'];
										typeArray[j]['type_Level'] = ++k;
										++j;
									}else{
										prams[obj['name']] = obj['value'];
									}
								}
							}
							prams.typeList = typeArray;
							//console.log(prams);
							
							that.setFirstStep(prams,function(){
								that.elems.optPanel.find("li").removeClass("on");
								that.elems.optPanel.find("li").eq(1).addClass("on");
								$panelDiv.hide();
								$(idVal).fadeIn("slow");
								that.getTreeList();
								that.getLevelInit();
							});
							
						}
						break;
					  default:
					  	break;
				}
			});
			

			//监听导航是否可以点击
			that.elems.optPanel.delegate('li','click',function(){
				if(that.elems.hasSave){
					var $this = $(this),
						$li = $('li',that.elems.optPanel),
						$panelDiv = $('.panelDiv'),
						flag = $this.data('flag');
					$li.removeClass('on');
					$this.addClass('on');
					$panelDiv.hide();
					$(flag).show();
					that.getTreeList();
				}
			});
			
			$('.editBtn').on('click',function(){
				var $this = $(this);
				if(!$this.hasClass('disableBtn')){
					$('#tissueName input').css({'borderColor':'#d0d5d8'}).removeAttr('disabled');
					$this.hide();
					$('.saveBtn').show();
				}
			});
			
			$('.saveBtn').on('click',function(){
				var tissueName = $('#tissueName input').val();
				if(tissueName==''){
					alert('请填写组织名称！');
					return false;
				}
				var $this = $(this),
					params = {
						action: 'SaveUnitStruct',
						'Unit_ID':that.elems.atJson.unit_id,
						'Unit_Name':tissueName,
						'Type_ID':that.elems.atJson.type_id,
						'parentUnit_ID':that.elems.atJson.parent_id
					};
				if(that.elems.hasAddChild){
					params.Unit_ID='';
					params.Type_ID=that.elems.atJson.next_type_id;
					params.parentUnit_ID=that.elems.atJson.unit_id;
				}
				
				$.util.ajax({
					url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeGateway.ashx",
					data: params,
					success: function(data){
						if(data.IsSuccess && data.ResultCode == 0) {
							var result = data.Data.unitStructInfo,
								unitName = result.unit_name;
							that.elems.atJson.unit_name = unitName; 	
							if(that.elems.hasAddChild){
								that.elems.atJson = result;
								that.elems.hasAddChild = 0;
								$('#tissueStatus').show();
							}						
							$this.hide();
							$('.editBtn').show();
							$('#tissueName input').css({'borderColor':'#fff'}).attr('disabled','disabled');
							that.getTreeList();//遍历树结构
							
							if(result.canAddChild==0){//是否可以添加子组织(0:不可以，1：可以)
								$('.addSubBtn').addClass('disableBtn');
							}else{
								$('.addSubBtn').removeClass('disableBtn');
							}
							
							if(result.childCount==0 && result.userRoleCount==0){//都为0时，可以删除
								$('.delBtn').removeClass('disableBtn');
							}else{
								$('.delBtn').addClass('disableBtn');
							}
							
							
						}else{
							alert(data.Message);
						}
					}
				});
			});
			
			$('.delBtn').on('click',function(){
				var $this = $(this);
				if(!$this.hasClass('disableBtn')){
					$.ajax({
						url: "/module/basic/unit/Handler/UnitHandler.ashx",
						async: false,
						data: {
							"mid":$.util.getUrlParam('mid'),
							"method":'unit_delete',
							"status":-1,
							"IsDelete":1,
							"ids":that.elems.atJson.unit_id
						},
						success: function(data){
							that.elems.isFirst = 1;
							that.getTreeList();//遍历树结构
							/*
							if(data.success){
							}else{
								alert(data.msg);
							}
							*/
						}
					});
				}
			});
			
			$('.addSubBtn').on('click',function(){
				var $this = $(this),
					obj = that.elems.atJson;
				if(!$this.hasClass('disableBtn')){
					that.elems.hasAddChild = 1;
					
					$('#parentName p').text(obj.unit_name || '无');//上级名称
					$('#nowName p').text(obj.next_type_name);//当前层级
					$('#tissueName input').val('').css({'borderColor':'#d0d5d8'}).removeAttr('disabled');//组织名称
					$('.editBtn').hide().removeClass('disableBtn');
					$('.saveBtn').show();
					
					$('#tissueName').show();
					$('#tissueStatus').hide();
				}
			});
			
			
			$('.jui-dialog-close').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			$('.jui-dialog .cancelBtn').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			
        },
		//初始化基本信息数据
		getFirstStep:function(){
			var that = this;
			that.getLevelInit();
		},
		setFirstStep:function(params,callback){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeGateway.ashx",
				data: params,
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0) {
						that.elems.hasSave = 1;
						if(callback){
							callback();
						}
					}else{
						alert(data.Message);
					}
				}
			});
			
		},
		//选择组织层级
		getLevelInit:function(){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeGateway.ashx",
				data: {
					action: 'GetTypeList',
					CustomerId: ''
				},
				success: function (data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							hasSave = result.HasSave,//是否保存
							typeList = result.TypeList,//保存的层级
							levelCount = result.LevelCount;//保存的层级数
						that.elems.hasSave = hasSave;
						that.getLevelName();
						
						setTimeout(function(){
							if(hasSave){//已保存过一次
								if(levelCount == 3){
									$('#districtName').val(typeList[1].type_name);
								}else if(levelCount == 4){
									$('#districtName').val(typeList[1].type_name);
									$('#pieceName').val(typeList[2].type_name);
								}else if(levelCount == 5){
									$('#districtName').val(typeList[1].type_name);
									$('#pieceName').val(typeList[2].type_name);
									$('#companyName').val(typeList[3].type_name);
								}
								
								$('#levelNameSelect').combobox('select',levelCount);
								$('.textbox-addon-right').hide();
								
								$('#nav01 .btnWrap').hide();
								$('.levelBox .searchInput').css({'borderColor':'#fff'});
								$('.inlineBlockArea .searchInput input').attr('disabled','disabled');
								
							}else{
								$('#levelNameSelect').combobox('setText',"请选择");
							}
						},500);
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getLevelName:function(){
			var that = this,
				$levelItem = $('.levelBox .commonSelectWrap'),
				jsonName = [
					{
						LevelCount: 2,
						type_name: "总部-门店"
					},
					{
						LevelCount: 3,
						type_name: "总部-区域-门店"
					},
					{
						LevelCount: 4,
						type_name: "总部-区域-片区-门店"
					},
					{
						LevelCount: 5,
						type_name: "总部-区域-片区-代理公司-门店"
					}
				],
				arrayName = [
					'第一层级名称：',
					'第二层级名称：',
					'第三层级名称：',
					'第四层级名称：',
					'第五层级名称：'
				];
			$('#levelNameSelect').combobox({
				width: 300,
				height: that.elems.height,
				panelHeight: that.elems.panlH,
				lines:true,
				valueField: 'LevelCount',
				textField: 'type_name',
				data:jsonName,
				onSelect: function(param){
					$('.levelBox').show();
					$levelItem.hide();
					$levelItem.eq(0).show();
					$levelItem.eq(4).show();
					that.elems.initLevel = param.LevelCount;
					
					$('.levelBox input').validatebox({    
							required: false
						});
					if(param.LevelCount==2){
						$levelItem.eq(4).find('.tit').text(arrayName[1]);
					}else if(param.LevelCount==3){
						$levelItem.eq(1).show();
						
						$levelItem.eq(1).find('.tit').text(arrayName[1]);
						$levelItem.eq(4).find('.tit').text(arrayName[2]);
						$('#districtName').validatebox({    
							required: true
						});  

					}else if(param.LevelCount==4){
						$levelItem.eq(1).show();
						$levelItem.eq(2).show();
						
						$levelItem.eq(1).find('.tit').text(arrayName[1]);
						$levelItem.eq(2).find('.tit').text(arrayName[2]);
						$levelItem.eq(4).find('.tit').text(arrayName[3]);
						
						$('#districtName').validatebox({    
							required: true
						});
						$('#pieceName').validatebox({    
							required: true
						});
					}else if(param.LevelCount==5){
						$levelItem.show();
						
						$levelItem.eq(1).find('.tit').text(arrayName[1]);
						$levelItem.eq(2).find('.tit').text(arrayName[2]);
						$levelItem.eq(3).find('.tit').text(arrayName[3]);
						$levelItem.eq(4).find('.tit').text(arrayName[4]);
						
						$('#districtName').validatebox({    
							required: true
						});
						$('#pieceName').validatebox({    
							required: true
						});
						$('#companyName').validatebox({    
							required: true
						});
					}
				}
			});
			
		
		},
		setSavePrize:function(params){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: params,
				success: function(data) {
					if(data.IsSuccess && data.ResultCode == 0) {
						$('.jui-mask').hide();
						$('.jui-dialog').hide();
						alert('成功添加奖品！');
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getTreeList:function(){
			var that = this,
				$treeBox = $('.treeBox');
			$.util.ajax({
				url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
				data: {
					hasShop : 0
				},
				success: function(data){
					if(data){
						var reslut = data;
						$treeBox.tree({
							data: reslut,
							onClick: function(node){//在用户点击的时候提示
								that.elems.hasAddChild = 0;
								that.getTreeDetails(node.id);
							},
							onLoadSuccess: function(){
								if(that.elems.isFirst==1){//第一次加载树结构
									$('.treeBox .tree-node').eq(0).trigger('click');
									that.elems.isFirst = 2;
								}
							}
						});
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getTreeDetails:function(id){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeGateway.ashx",
				data: {
					action: 'GetUnitStructByID',
					Unit_ID: id
				},
				success: function(data){
					if(data.ResultCode == 0){
						var reslut = data.Data.unitStructInfo,
							parentId = reslut.parent_id, //-99:没有上级
							parentUnitName = reslut.parentUnit_Name, //上级名称
							typeName = reslut.TYPE_NAME, //当前层级
							unitName = reslut.unit_name, //组织名称
							canAddChild = reslut.canAddChild, //是否可以添加子组织(0:不可以，1：可以)
							childCount = reslut.childCount, //下级组织数量
							userRoleCount = reslut.userRoleCount, //被多少用户角色数量
							status = reslut.Status; //1：已生效，0：失效
							
						that.elems.atJson = reslut;
						
						$('#parentName p').text(parentUnitName || '无');//上级名称
						$('#nowName p').text(typeName);//当前层级
						$('#tissueName input').val(unitName);//组织名称
						
						//解决沁晖bug
						$('#tissueName input').css({'borderColor':'#fff'}).attr('disabled','disabled');
						$('.editBtn').show();
						$('.saveBtn').hide();
						
						
						if(parentId=='-99'){
							$('#tissueName').hide();
							$('.editBtn').addClass('disableBtn');
							$('.delBtn').addClass('disableBtn');
						}else{
							$('#tissueName').show();
							$('.handleBtn a').removeClass('disableBtn');
						}
						if(childCount==0 && userRoleCount==0){//都为0时，可以删除
							$('.delBtn').removeClass('disableBtn');
						}else{
							$('.delBtn').addClass('disableBtn');
						}
						if(canAddChild==0){//是否可以添加子组织(0:不可以，1：可以)
							$('.addSubBtn').addClass('disableBtn');
						}else{
							$('.addSubBtn').removeClass('disableBtn');
						}
					}else{
						alert(data.Message);
					}
				}
			});
		}
    };
    page.init();
});

