define(['jquery','template','tools','langzh_CN','easyui','artDialog','kkpager','kindeditor'],function($){
    //上传图片
    KE = KindEditor;
    var page = {
        elems: {
			unitId:'',
            optPanel:$("#optPanel"), //页面的顶部li
            submitBtn:$("#submitBtn"),//提交和下一步按钮
            section:$("#section"),
            editLayer:$(".uploadPicBox"), //图片上传
            simpleQuery:$("#simpleQuery"),//全部
            width:220,
            height:32,
            panlH:200,
			SaveAllPrams:null,
			unitId:'',
			isFirst:1,
			domain:'',
			imageContentDiv: $("#imageContentMessage"),    //所有的图文关联父层
            imageContentItems: $("#imageContentItems"),
            addImageMessageDiv: $("#addImageMessage"),  //弹出图文列表
			uiMask: $(".jui-mask"),//遮罩层
            chooseEventsDiv: $("#chooseEvents"),//选择活动层
			MaterialTextName:''
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
			that.elems.unitId = $.util.getUrlParam('Item_Id') || '';
			$contentArea.css({'minHeight':h+'px'});
			$('#nav01').fadeIn("slow");
			//$('#nav02').fadeIn("slow");
			
			//第一步
			that.getClassify();//获取上级组织层级
			that.getCityInfo('');//获取省，市，区
			
			//遍历图文素材下拉框
			$('#ReplyType').combobox({
				width: 380,
				height: that.elems.height,
				panelHeight: that.elems.panlH,
				lines:true,
				valueField: 'ReplyType',
				textField: 'text',
				data:[{
					ReplyType:1,
					text:'文本'
				},{
					ReplyType:3,
					text:'图文'
				},{
					ReplyType:'',
					text:'请选择'
				}],
				onSelect: function(param){
					$('.selectItem').hide();
					if(param.ReplyType==1){
						$('#textContent').show();
					}else if(param.ReplyType==3){
						$('#imageContentMessage').show();
					}
				}
			});
			$('#ReplyType').combobox('setText',"请选择");
			
			
			that.getStoreInfo(that.elems.unitId);//获取门店信息
			
		},
		
		validateInfo:function(){
			var that = this,
				prams = {action:'unit_save',unit_id:that.elems.unitId},
				fields = $('#nav0_1').serializeArray(),
				typeArray = {},
				$storePicBox = $('.storePicBox .picBox img'),
				coordArray = [],
				imageUrlArray = [];
			//console.log(fields);
			for(var i=0;i<fields.length;i++){
				var obj = fields[i];
				typeArray[obj['name']] = obj['value'];
			}
			
			if(typeArray.longitude){
				coordArray = typeArray.longitude.split(',');
				typeArray['longitude'] = coordArray[0];
				typeArray['dimension'] = coordArray[1];
			}else{
				typeArray['dimension'] = '';
			}
			for(var j=0;j<$storePicBox.length;j++){
				imageUrlArray[j] = {
					"ImageId":"",
					"ObjectId":"",
					"ImageURL":$storePicBox.eq(j).attr('src'),
					"DisplayIndex":j+1
				}
			}
			typeArray['ItemImageList'] = imageUrlArray;
			prams.unit = typeArray;
			prams.unit.Id = '';
			prams.unit.MaxWQRCod = '';
			prams.unit.WXCodeImageUrl = '';
			prams.unit.text = '';
			prams.unit.ReplyType = '';
			prams.unit.listMenutextMapping = [];

			prams.unit = JSON.stringify(prams.unit);
			return prams;
		},
		
        initEvent:function() {
            var that = this;
			
			//上传图片路由
			that.registerUploadImgBtn();
			
			//点击第一步保存
			$('.commonStepBtn').on('click',function(){
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
							var prams = that.validateInfo();
							//console.log(prams);
							that.setFirstStep(prams,function(){
								that.elems.SaveAllPrams = prams;
								that.elems.optPanel.find("li").removeClass("on");
								that.elems.optPanel.find("li").eq(1).addClass("on");
								$panelDiv.hide();
								$(idVal).fadeIn("slow");
								$('.qrStoreName').text(JSON.parse(prams.unit).Name);
							});
							
						}
					  	break;	
					  case '#nav03':
					  	var prams = that.elems.SaveAllPrams,
							$qrInfoBox = $('.qrInfoBox img'),
							unitObj = JSON.parse(prams.unit);
						prams.action = 'unit_save';

						unitObj.WXCodeImageUrl = $qrInfoBox.attr('src');//二维码地址
						unitObj.MaxWQRCod = $qrInfoBox.data('code');//二维码标识
						unitObj.ReplyType = $('#ReplyType').combobox('getValue')-0;//消息类型（1：文本，3：图文）
						//unitObj.listMenutextMapping = [];	MeterialTextInfo[]	图文信息数据
						switch(unitObj.ReplyType) {
							case 1:
								unitObj.text = $('#textContentVal').val();//文本编辑
								break;
							case 3:
								//图文消息
								var maxtrailDom = that.elems.imageContentDiv.find(".item"),
									materialTextIds = [];
								if(maxtrailDom.length > 0){
									maxtrailDom.each(function (i, k) {
										var obj = $(k).attr("data-obj");
										obj = JSON.parse(obj);
										materialTextIds.push({
											TextId: obj.TestId,
											DisplayIndex: (i + 1)
										});
									});
								}
								unitObj.listMenutextMapping = materialTextIds;
								break;
						}
						prams.unit = JSON.stringify(unitObj);
						//console.log(prams);	
						if(!unitObj.WXCodeImageUrl){
							alert('请生成二维码');
						}else{
							that.setFirstStep(prams,function(){
								location.href = "queryList.aspx?mid=" + JITMethod.getUrlParam("mid");	
							});
						}
						break;
					  default:
					  	break;
				}
			});
			
			
			//删除上传的图片
			$('.storePicBox').delegate('em','click',function(){
				var $this = $(this);
				$this.parents('.picBox').remove();	
			});
			
			//监听导航是否可以点击
			that.elems.optPanel.delegate('li','click',function(){
				//if(that.elems.SaveAllPrams){
					var $this = $(this),
						$li = $('li',that.elems.optPanel),
						$panelDiv = $('.panelDiv'),
						flag = $this.data('flag');
					if(flag == '#nav02'){
						if($('#nav0_1').form('validate')){
							var prams = that.validateInfo();
							that.elems.SaveAllPrams = prams;
						}else{
							return false;
						}
					}
					$li.removeClass('on');
					$this.addClass('on');
					$panelDiv.hide();
					$(flag).show();
				//}
			});
			
			$('.coordQueryBtn').on('click',function(){
				var provinceVal = $('#provinceId').combobox('getText'),
					townVal = $('#townId').combobox('getText'),
					cityVal = $('#CityId').combobox('getText'),
					addressVal = $('#Address').val(),
					addrStr = '';
				if(provinceVal=='' || provinceVal=='请选择'){
					alert('请选择省份');
					return false;
				}else if(townVal=='' || provinceVal=='请选择'){
					alert('请选择城市');
					return false;
				}else if(cityVal=='' || provinceVal=='请选择'){
					alert('请选择区域');
					return false;
				}else if(addressVal==''){
					alert('请输入详细地址');
					return false;
				}
				addrStr = provinceVal+townVal+cityVal+addressVal;
				that.mapCoordinates(addrStr);//获取门店经纬度
			});
			
			
			$('.qrCreateBtn').on('click',function(){
				var $this = $(this),
					hasQr = $('.qrInfoBox img').attr('src');
				if(!!hasQr){
					alert('您已生成二维码！');
					return false;
				}else{
					that.setQrImg('');
				}
			});
			
			
			$('.qrDownBtn').on('click',function(){
				var QRImgUrl = $('.setQRcodeArea img').attr('src');
				if(!QRImgUrl){
					alert('请先生成二维码');
				}else{
					window.open('/Module/Basic/Unit/Handler/UnitHandler.ashx?method=UploadImagerUrl&imageUrl=' + QRImgUrl);
				}
			});
			
			
			
			//点击获取图文的内容
			that.elems.imageContentDiv.delegate(".addBtn","click",function(){
				var $this = $(this);
				that.showMatrialText(true);
			});
			
			//保存图文事件
			that.elems.addImageMessageDiv.delegate(".saveBtn", "click", function () {
				that.showMatrialText(false);
			});
			//取消图文事件
			that.elems.addImageMessageDiv.delegate(".cancelBtn", "click", function () {
				//再取消的时候把所有的删除
				that.elems.imageContentDiv.find("[data-flag='add']").remove();
				$("#hasChoosed").html(0);
				that.showMatrialText(false);
			});
			
			
			//查询图文事件
			that.elems.addImageMessageDiv.delegate(".queryBtn","click",function(){
				var eventName = $("#theTitle").val();
				that.elems.MaterialTextName = eventName;  //图文名称
				//page.pageIndex = 0;  //只要查询就从头查询
				that.getMaterialTextList(function(data){
					if(data.Data.MaterialTextList.length==0){
						that.elems.imageContentItems.html('<p style="text-align:center;padding:50px 0">暂无图文素材列表！</P>');
						return false;
					}
					var obj = {
						pageSize: data.Data.MaterialTextList.length,
						currentPage: 1,
						allPage: data.Data.TotalPages,
						showAdd: true,  //表示的一个标识
						itemList: data.Data.MaterialTextList
					}
					var html = bd.template("addImageItemTmpl",obj);
					that.elems.imageContentItems.html(html);
					/*
					that.events.initPagination(1, data.Data.TotalPages, function (page) {
						that.loadMoreMaterial(page);
					}, that.elems.addImageMessageDiv);
					*/
				});

			});
			
			
			//选择一个项则让他选中  同时在页面中展示出来
			that.elems.addImageMessageDiv.delegate(".item", "click", function () {
				var $this = $(this);
				var addId = $this.attr("data-id");
				//已经有的图文数量
				var hasLength = that.elems.imageContentDiv.find(".item").length;
				if ($this.attr("isSelected") == "true") {  //表示已经选中则进行删除
					$this.removeClass("on").attr("isSelected", "false");
					$("#" + addId).remove();
					//表示已经选择的图文数量
					hasLength = hasLength - 1;
				} else {
					if (hasLength >= 10) {
						alert("图文素材最多选择10个!\r\n不能继续添加!");
						return false;
					}
					$this.addClass("on").attr("isSelected", "true");
					var clone = $this.clone();
					var domObj = that.elems.imageContentDiv.find("[data-id=" + addId + "]");
					if (domObj.length) {
						domObj.remove();
						hasLength = hasLength - 1;
					}
					//给克隆后的节点设置id
					clone.attr("id", $this.attr("data-id"));
					//将选中的内容添加到图文层
					that.elems.imageContentDiv.find(".list").append(clone);
					//表示已经选择的图文数量
					hasLength = hasLength + 1;
				}
				$("#hasChoosed").html(hasLength);
			});
			
			
			//已经选择的图文列表鼠标移动上去出现删除的按钮
			that.elems.imageContentDiv.find(".list").delegate(".item", "mouseover", function () {
				var $this = $(this);
				$this.addClass("hover");

			}).delegate(".item", "mouseout", function () {
				var $this = $(this);
				$this.removeClass("hover");
			});
			//删除图文消息    一种是删除dom 一种是删除数据库里面的
			that.elems.imageContentDiv.find(".list").delegate(".delBtn", "click", function () {
				var $this = $(this);
				//是否是已经存储在数据库的
				var itemDom = $this.parent().parent();
				itemDom.remove();
				var length = (that.elems.imageContentDiv.find(".item").length);
				//表示已经选择的图文数量
				$("#hasChoosed").html(length);
			})
			
			
			
			
			
			
			
			
			
	
			$('.jui-dialog-close').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			$('.jui-dialog .cancelBtn').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			
        },
		getStoreInfo:function(unitId){
			var that = this;
			$.util.oldAjax({
				url: "/module/basic/unit/Handler/UnitHandler.ashx",
				data: {
					action: 'get_unit_by_id',
					unit_id: unitId
				},
				success: function (data) {
					if (data) {
						var result = data.data,
							id = result.Id,
							itemImageList = result.ItemImageList || [];
						if(!!id){//有门店
							//待开发
							that.elems.SaveAllPrams = result;
							$('#nav0_1').form('load',result);
							var htmlStr = '';
							for(var i=0;i<itemImageList.length;i++){
								htmlStr += '<p class="picBox"><img src="'+itemImageList[i].ImageURL+'"><em></em></p>'
							}
							$('.storePicBox').prepend(htmlStr);
							
							$('#provinceId').combobox('select',65);
							setTimeout(function(){
								$('#provinceId').combobox('select',result.provinceId);
								setTimeout(function(){
									$('#townId').combobox('select',result.townId);
									setTimeout(function(){
										$('#CityId').combobox('select',result.CityId);
									},500);
								},500);
							},500);
							
							//定位树节点
							$('#Parent_Unit_Id').combotree('setValue',result.Parent_Unit_Id);


							
							
							//二维码模块遍历
							$('#nav0_2').form('load',result);
							
							setTimeout(function(){
								$('#ReplyType').combobox('select',0);
								setTimeout(function(){
									$('#ReplyType').combobox('select',result.ReplyType || 0);
								},1000);
							},1000);
							$('.qrInfoBox img').attr('src',result.WXCodeImageUrl).attr('data-code',result.WXCode);
							$('.qrStoreName').text(result.Name);
							
							//图文素材遍历
							if(!!result.listMenutext){
								var obj = {
									pageSize: result.listMenutext.length,
									currentPage: 1,
									allPage: 1,
									showAdd: true,  //表示的一个标识
									itemList: result.listMenutext
								}
								var html = bd.template("addImageItemTmpl",obj);
								that.elems.imageContentDiv.find(".list").html(html);
								
							}
							
						}
					}else{
						alert(data.msg);
					}
				}
			});
		},
		getClassify:function(){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
				data:{
					hasShop:0
				},
				success: function(data){
					if(data){
						data.push({id:0,text:"请选择"});
						$('#Parent_Unit_Id').combotree({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: 210,
							lines:true,
							valueField: 'id',
                    		textField: 'text',
							data:data,
							onSelect: function(param){
								
							}
						});
						//$('#Parent_Unit_Id').combobox('setText',"请选择");
					}
					else{
						alert('无组织层级');
					}
				}
			});
			
			//类型
			$('#StoreType').combobox({
                width: that.elems.width,
				height: that.elems.height,
				panelHeight: that.elems.panlH,
                valueField: 'StoreType',
                textField: 'text',
                data:[{
                    "StoreType":'DirectStore',
                    "text":"直营店"
                },{
                    "StoreType":'NapaStores',
                    "text":"加盟店"
                }]
            });
			setTimeout(function(){
				$('#StoreType').combobox('setText',"请选择");
			},200);
			
			
			
		},
		getCityInfo:function(id){
			var that = this;
			$.util.oldAjax({
				url: "/Framework/Javascript/Biz/Handler/CitySelectTreeHandler.ashx",
				data:{
					action:'',
					node:id
				},
				success: function(data){
					if(data){
						if(id==''){
							$('#provinceId').combobox({
								width: 70,
								height: that.elems.height,
								panelHeight: that.elems.panlH,
								lines:true,
								valueField: 'id',
								textField: 'text',
								data:data,
								onSelect: function(param){
									that.getCityInfo(param.id);
								}
							});
							$('#provinceId').combobox('setText',"请选择");
							$('#townId').combobox('setText',"请选择");
							$('#CityId').combobox('setText',"请选择");
						}else if(id.length==2){
							$('#townId').combobox({
								width: 70,
								height: that.elems.height,
								panelHeight: that.elems.panlH,
								lines:true,
								valueField: 'id',
								textField: 'text',
								data:data,
								onSelect: function(param){
									that.getCityInfo(param.id);
								}
							});
							//$('#townId').combobox('setText',"请选择");
						}else if(id.length==4){
							$('#CityId').combobox({
								width: 70,
								height: that.elems.height,
								panelHeight: that.elems.panlH,
								lines:true,
								valueField: 'id',
								textField: 'text',
								data:data,
								onSelect: function(param){}
							});
							//$('#CityId').combobox('setText',"请选择");
						}
						
						
					}else{
						alert(data.msg);
					}
				}
			});
		},
		setFirstStep:function(params,callback){
			var that = this;
			$.util.oldAjax({
				url: "/Module/Basic/Unit/Handler/UnitHandler.ashx",
				data: params,
				success: function(data){
					if(data.success) {
						that.elems.unitId = data.data;
						//that.elems.hasSave = 1;//unit
						if(callback){
							callback();
						}
					}else{
						alert(data.msg);
					}
				}
			});
			
		},
		//图片上传按钮绑定
        registerUploadImgBtn: function(){
            var that = this;
            // 路由上传按钮
            $('.storePicBox .uploadImgBtn').each(function(i,e) {
                that.addUploadImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent:function(e) {
            var that = this,
				$storePicBox = $('.storePicBox');
            //上传图片并显示
            that.uploadImg(e,function(ele,data) {
				var $ele = $(ele),
					result = data,
					thumUrl = result.thumUrl,//缩略图
					url = result.url,
					htmlStr = '<p class="picBox"><img src="'+url+'"><em></em></p>';//原图
				$('.ke-inline-block').before(htmlStr);
				if($('.picBox',$storePicBox).length==3){
					$('.addPicBtn').hide();
				}
				//$(htmlStr).insertBefore($ele);
				//alert($ele.attr('class'));
            });
        },
        //上传图片
        uploadImg: function (btn,callback){
            var that = this,
				w = 58,
				h = 62;
			var	uploadbutton = KE.uploadbutton({
					button: btn,
					width:58,
					//上传的文件类型
					fieldName: 'imgFile',
					//注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
					url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width='+w+'&height='+h,
					//&width='+w+'&height='+h+'&originalWidth='+w+'&originalHeight='+h
					afterUpload: function(data){
						if(data.error===0){
							if(callback) {
								callback(btn,data);
							}
						}else{
							alert(data.message);
						}
					},
					afterError: function (str) {
						alert('自定义错误信息: ' + str);
					}
				});
            uploadbutton.fileBox.change(function(e){
				uploadbutton.submit();
			});
        },
		mapCoordinates:function(address){
			// 百度地图API功能
			var map = new BMap.Map("l-map");
			//map.centerAndZoom(new BMap.Point(117.269945,31.86713), 13);
			//map.enableScrollWheelZoom(true);
			//var index = 0;
			var myGeo = new BMap.Geocoder();
			var coordVal = '';
			myGeo.getPoint(address, function(point){
				if (point) {
					coordVal = point.lng + "," + point.lat;
					$('#longitude').val(coordVal);
					//var address = new BMap.Point(point.lng, point.lat);
					//addMarker(address,new BMap.Label(index+":"+add,{offset:new BMap.Size(20,-10)}));
				}
			});
			
			//return coordVal;
			// 编写自定义函数,创建标注
			/*
			function addMarker(point,label){
				var marker = new BMap.Marker(point);
				map.addOverlay(marker);
				marker.setLabel(label);
			}
			*/
		},
		setQrImg:function(unitId){
			var that = this;
			$.util.oldAjax({
				url: "/Module/Basic/Unit/Handler/UnitHandler.ashx",
				data: {
					action:'CretaeWxCode',
					unit_id:unitId
				},
				success: function(data) {
					if(data.success) {
						var result = data.data;
						$('.setQRcodeArea img').attr('src',result.imageUrl).attr('data-code',result.MaxWQRCod);
						$('#WXCodeImageUrl').val(result.imageUrl);
						
					}else{
						alert(data.msg);
					}
				}
			});
		},
		
		
		
		//显示图文搜索的  进行获取
		showMatrialText: function (flag,type) {
			var that = this;
			if (!!!flag) {
				this.elems.uiMask.hide();
				this.elems.addImageMessageDiv.hide();
			}else{
				this.elems.uiMask.show();
				//动态的填充弹出层里面的内容展示
				this.loadPopMatrialText();
				this.elems.addImageMessageDiv.show();
			}
		},
		//加载图文列表数据
		loadPopMatrialText: function () {
			var that = this;
			//获取图文列表
			that.getMaterialTextList(function(data){
				if(data.Data.MaterialTextList.length==0){
					that.elems.imageContentItems.html('<p style="text-align:center;padding:50px 0">暂无图文素材列表！</P>');
					return false;
				}
				var obj = {
					pageSize: data.Data.MaterialTextList.length,
					currentPage: 1,
					allPage: data.Data.TotalPages,  //总页数
					showAdd: true,  //表示的一个标识
					itemList: data.Data.MaterialTextList
				}
				var html = bd.template("addImageItemTmpl", obj);
				that.elems.imageContentItems.html(html);
				/*
				that.events.initPagination(1, data.Data.TotalPages, function (page) {
					that.loadMoreMaterial(page);
				}, that.elems.addImageMessageDiv);
				*/
			});
			
			
		},
		//获取图文列表  点击图文列表的图文id的时候
        getMaterialTextList:function(callback) {
			var that = this;
            $.util.ajax({
                url: '/ApplicationInterface/Gateway.ashx',
                type: "post",
                data:
                {
                    'action': 'WX.MaterialText.GetMaterialTextList',
                    'Name': that.elems.MaterialTextName,  //图文名称
                    'PageSize': 100,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        }
    };
    page.init();
});

