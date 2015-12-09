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
            skuTable:$("#skuTable"),
            width:160,
            height:32,
            panlH:200,
            sku:$("#sku"),
            loadAlert:null,
            priceFilde:"item_price_type_name_",//数据库价格相关的字段，一般有销量库存，价格实体价格
            allData:{}, //页面所有存放对象基础数据
			eventId:$.util.getUrlParam('EventID'),
			jsonParams: {},
			activiType: 1, //活动类型1:红包，2：大转盘，3：刮刮卡
			Prizeindex: 1, //大转盘奖项index
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
			
			
			//第一步
			that.getFirstStep();
			
			
			//第二步，奖品配置模块数据初始化
			//that.getPrizeList();
			//that.addPrizeList();
			//第三步，图文素材
			
		},
        initEvent:function() {
            var that = this;
            $('.commonStepBtn').on('click', function () {
				var $this = $(this),
					idVal = $this.data('flag'),
					$panelDiv = $('.panelDiv');
				//if($this.hasClass('nextStepBtn')){/*prevStepBtn*/}
				switch(idVal){
					  case '#nav01':
					  	that.elems.optPanel.find("li").removeClass("on");
						that.elems.optPanel.find("li").eq(0).addClass("on");
						$panelDiv.hide();
						$(idVal).fadeIn("slow");
						break;
					  case '#nav02':
					      if($('#nav0_1').form('validate')){
					          var prams = {action:'SaveEventStep1',EventID:that.elems.eventId},
                                  fields = $('#nav0_1').serializeArray();
					          //console.log(fields);

					          for(var i=0;i<fields.length;i++){
					              var obj = fields[i];
					              prams[obj['name']] = obj['value'];
					          }
					          //console.log(prams);
					          prams.EventTypeID = '081AEC92-CC16-4041-9496-B4F6BC3B11FC';
					          if(prams.PointsLottery == ''){
					              prams.PointsLottery = 0;
					          }
					          that.setFirstStep(prams, function (DrawMethodId) {
					              that.elems.optPanel.find("li").removeClass("on");
					              that.elems.optPanel.find("li").eq(1).addClass("on");
					              $panelDiv.hide();
					              $(idVal).fadeIn("slow");
					              if (DrawMethodId == 2) {
					                  that.elems.activiType = 2;
					                  that.LuckyTurntable();
					              //} else if (DrawMethodId == 1) {
					              //    that.elems.activiType = 3;
					              //    that.ScratchCard();
					              }else{
					                  $(".PrizeSet").hide();
					                  $("#nav0_2").show();
					              }
								
								//保存第一步的同时获取第二步的数据
								//待开发
							});
							//第二步，奖品配置模块数据初始化
							that.getPrizeList(true);
							that.addPrizeList();
						}
						break;
					  case '#nav03':
					      if ($this.data('page') == 'redPackage') {
					          var array = [],
					              leng;
					          debugger;
					          if (that.elems.activiType == 1) {
					              var logo = $('#logoBgPic').data('url'),
                                      beforeGround = $('#beforeBgPic').data('url'),
                                      backGround = $('#backBgPic').data('url'),
                                      rule = $('#ruleBgPic').data('url'),
                                      ruleContent = $('.ruleText textarea').val(),
                                      ruleFlag = $('#ruleOption').combobox('getValue');
					              leng = $('#prizeListTable tbody tr td').length > 1;
                                      
					              if (!leng) {
					                  return $.messager.alert("提示", '请添加奖品信息！');
					              } else if (!beforeGround) {
					                  return $.messager.alert("提示", '请上传领取前背景图片！');
					              } else if (!backGround) {
					                  return $.messager.alert("提示", '请上传领取后背景图片！');
					              } else if (!logo) {
					                  return $.messager.alert("提示", '请上传logo图片！');
					              } else if (ruleFlag == 1 && !ruleContent) {
					                  return $.messager.alert("提示", '请填写活动规则内容文本！');
					              } else if (ruleFlag == 2 && !rule) {
					                  return $.messager.alert("提示", '请上传活动规则内容图片！');
					              }
					              array = [
                                              {
                                                  "ImageURL": logo,
                                                  "BatId": "Logo"
                                              },
                                              {
                                                  "ImageURL": beforeGround,
                                                  "BatId": "BeforeGround"
                                              },
                                              {
                                                  "ImageURL": backGround,
                                                  "BatId": "BackGround"
                                              },
                                              {
                                                  "ImageURL": rule,
                                                  "BatId": "Rule"
                                              }
					              ];

					              that.setSaveWxPrize(array, ruleFlag, ruleContent);
					          }
                              //大转盘
					          if (that.elems.activiType == 2) {
					              var LT_Data_kvPic = $('#LT_Data_kvPic').data('url'),
                                      LT_Data_Rule = $('#LT_Data_Rule').data('url'),
                                      LT_Data_bgpic1 = $('#LT_Data_bgpic1').data('url'),
                                      LT_Data_bgpic2 = $('#LT_Data_bgpic2').data('url'),
                                      LT_Data_regularpic = $('#LT_Data_regularpic').data('url');
					              leng = $('#LuckyTurnListTable tbody tr td').length > 1;
					              if (!leng) {
					                  return $.messager.alert("提示",'请添加奖品信息！');
					              } else if (!LT_Data_regularpic) {
					                  return $.messager.alert("提示", '请上传试试手气背景图片！');
					              } else if (!LT_Data_kvPic) {
					                  return $.messager.alert("提示", '请上传kv图片！');
					              } else if (!LT_Data_Rule) {
					                  return $.messager.alert("提示", '请上传活动规则图片！');
					              } else if (!LT_Data_bgpic1) {
					                  return $.messager.alert("提示", '请上传背景图片1！');
					              } else if (!LT_Data_bgpic2) {
					                  return $.messager.alert("提示", '请上传背景图片2！');
					              }
                                  
					              if ($("#selectPrize ._selected").length < 12) {
					             
					                  return $.messager.alert("提示", "奖品选项未全部添加！");
					              }

					              array = [
                                              {
                                                  "ImageURL": LT_Data_kvPic,
                                                  "BatId": "LT_kvPic"
                                              },
                                              {
                                                  "ImageURL": LT_Data_Rule,
                                                  "BatId": "LT_Rule"
                                              },
                                              {
                                                  "ImageURL": LT_Data_bgpic1,
                                                  "BatId": "LT_bgpic1"
                                              },
                                              {
                                                  "ImageURL": LT_Data_bgpic2,
                                                  "BatId": "LT_bgpic2"
                                              },
                                              {
                                                  "ImageURL": LT_Data_regularpic,
                                                  "BatId": "LT_regularpic"
                                              }
					              ];

					              that.setSaveWxPrize(array, 1, "");

					              var imgjsonstr = "[";
					              $("#selectPrize p img").each(function () {
					                  imgjsonstr += " {PrizeLocationID:" + ($(this).data("prizeLocationid") == undefined ? "''" : "'" + $(this).data("prizeLocationid") + "'") + ",Location:'" + $(this).data("index") + "',PrizeID:'" + $(this).data("prizesid") + "',ImageUrl:'" + $(this).attr("src") + "',PrizeName:'" + $(this).data("prizesname") + "'},";
					              });
					              imgjsonstr = imgjsonstr.trim(",") + "]";
					              var imgjson= Ext.decode(imgjsonstr);
					              that.SavePrizeLocation(imgjson);
					          }
					          
						}
					  	that.elems.optPanel.find("li").removeClass("on");
						that.elems.optPanel.find("li").eq(2).addClass("on");
						$panelDiv.hide();
						$(idVal).fadeIn("slow");
						//保存第二步的同时获取第三步的数据
						that.getThirdStep();
						break;
				    case '#nav04':
					  	if($('#nav0_3').form('validate')){
							var url = $('.imageTextArea').data('url');
							if(!url){
								return alert('请上传图片！');
							}
					  	    //待开发，保存第三步

							debugger;
							that.setThirdStep(that.elems.jsonParams);
						}
					  	break;
					  default:
					  	break;
				}
			})
			
			
			//奖品配置模块
            $('#addPrizeBtn').bind('click', function () {
                $('#addPrizeForm').form('clear');
				$('.jui-mask').show();
				$('.jui-dialog-redPackage').show();
            });


            //大转盘奖品选择
            $('#selectPrize .regularpic').bind('click', function () {
                
            });

            //大转盘奖品选择
            $('#selectPrize p img').bind('click', function () {
                $('.jui-mask').show();
                $('.jui-dialog-Prizeselect').show();

                that.elems.Prizeindex = $(this).data("index");


            });

            //大转盘奖品配置模块
            $('#LuckyTurntableaddBtn').bind('click', function () {

                $("#LuckyTurntablePrize").form("load", { PrizeLevel: "0", PrizeTypeId: "", CouponTypeID: "", Point: "", PrizeName: "", CountTotal: "", ImageUrl: "" });
                $("#LuckyTurntablePrizeimg").attr("src","images/uploadpic.png");
                $('.jui-mask').show();
                $('.jui-dialog-LuckyTurntable').show();
            });
			//上传图片路由
			that.registerUploadImgBtn();
			
			$('.jui-dialog-close').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			$('.jui-dialog .cancelBtn').bind('click',function(){
				$('.jui-mask').hide();
				$('.jui-dialog').hide();
			});
			$('.jui-dialog-redPackage .saveBtn').bind('click', function () {
				//保存奖品
				if($('#addPrizeForm').form('validate')){//a03d28e78b2c18104d4812ba18a5c69b
					var prams = {action:'SavePrize',EventId:that.elems.eventId},
						fields = $('#addPrizeForm').serializeArray();
					//console.log(fields);
					for(var i=0;i<fields.length;i++){
						var obj = fields[i];
						prams[obj['name']] = obj['value'];
						
						if(obj['name']=='Point' && obj['value']==''){
							prams[obj['name']] = 0;
						}
						
					}
					//console.log(prams);
					that.setSavePrize(prams);
				}
			});

            //大转盘奖品选择提交
			$('.jui-dialog-Prizeselect .saveBtn').bind('click', function () {
			    debugger;
			    var imageurl = $(".PrizeseOption .on").data("imageurl");
			    var prizesid = $(".PrizeseOption .on").data("prizesid");
			    var prizesname = $(".PrizeseOption .on").data("prizesname");

			    var img = $("#Img" + that.elems.Prizeindex);

			    img.attr("src", imageurl);
			    img.data("prizesid", prizesid);
			    img.data("prizesname", prizesname);
			    img.addClass("_selected");
			    $('.jui-mask').hide();
			    $('.jui-dialog-Prizeselect').hide();
			});


            //大转盘保存奖品start
			$('.jui-dialog-LuckyTurntable .saveBtn').bind('click', function () {
			    //保存奖品
			    
			    if ($('#LuckyTurntablePrize').form('validate')) {//a03d28e78b2c18104d4812ba18a5c69b
			        if ($("#LT_dataPrizeimg")[0].value != undefined && $("#LT_dataPrizeimg")[0].value != "") {
			            var prams = { action: 'SavePrize', EventId: that.elems.eventId },
                            fields = $('#LuckyTurntablePrize').serializeArray();
			            //console.log(fields);
			            for (var i = 0; i < fields.length; i++) {
			                var obj = fields[i];
			                prams[obj['name']] = obj['value'];

			                if (obj['name'] == 'Point' && obj['value'] == '') {
			                    prams[obj['name']] = 0;
			                }

			            }
			            //console.log(prams);
			            that.setSavePrize(prams);
			        } else {
			            $.messager.alert("提示","请上传奖品图片！");
			        }
			    }

			});
            //大转盘保存奖品end
			
			//追加
			$('#prizeListTable,#LuckyTurnListTable').delegate('.addBtn', 'click', function () {
				var $this = $(this),
					$tr = $this.parents('tr'),
					$num = $('.numBox',$tr),
					num = $num.text()-0,
					prizesId = $tr.data('prizesid');
				$('.jui-mask').show();
				$('.jui-dialog-prizeCountAdd').show();
				$('.jui-dialog-prizeCountAdd .saveBtn').unbind('click');
				$('.jui-dialog-prizeCountAdd .saveBtn').bind('click',function(){
					var addNum = $('#prizeCountAdd').val()-0,
						count = num+addNum;
					//提交接口
					$.util.ajax({
						url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
						data: {
							action: 'AppendPrize',
							EventId: that.elems.eventId,
							CountTotal: addNum,
							PrizesID: prizesId
						},
						success: function(data){
							if(data.IsSuccess && data.ResultCode == 0) {
								$('.jui-mask').hide();
								$('.jui-dialog-prizeCountAdd').hide();
								$num.text(count);
							}else{
								alert(data.Message);
							}
						}
					});
					
					
				});
			});
			
			
			
			//删除
			$('#prizeListTable,#LuckyTurnListTable').delegate('.delBtn', 'click', function () {
				var $this = $(this),
					$tr = $this.parents('tr'),
					prizesId = $tr.data('prizesid');
				
				$.messager.confirm('删除奖品', '您确定删除该奖品吗？',function(e){
					if(e){
						//$tr.remove();
						//提交接口
						$.util.ajax({
							url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
							data: {
								action: 'DeletePrize',
								EventId: that.elems.eventId,
								PrizesID: prizesId
							},
							success: function(data){
								if(data.IsSuccess && data.ResultCode == 0){
									$tr.remove();
								}else{
									alert(data.Message);
								}
							}
						});
						
					}
				});
					
			});


			
			//查看领取后页面
			$('.seeRedBtn').bind('click',function(){
				var $this = $(this),
					beforeBgPic = $('#beforeBgPic').data('url'),
					backBgPic = $('#backBgPic').data('url');
				if($this.text()=='查看领取后页面'){
					$('#redBgPic').attr('src',backBgPic);
					$('.getRedBackBtn').hide();
					
					$('.backAction').show();
					$this.text('查看领取前页面');
				}else{
					$('#redBgPic').attr('src',beforeBgPic);
					$('.getRedBackBtn').show();
					
					$('.backAction').hide();
					$this.text('查看领取后页面');
				}
			});
			
			//图文素材失去焦点
			$('#graphicTitle').blur(function(){
				$('.previewTit').text($(this).val());
			});
			
			$('#graphicDsc').blur(function(){
				$('.previewDsc').text($(this).val());
			})
			
            
			
        },
		//初始化基本信息数据
		getFirstStep:function(){
			var that = this;
			that.activityWay('081AEC92-CC16-4041-9496-B4F6BC3B11FC');//081AEC92-CC16-4041-9496-B4F6BC3B11FC
	        $.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetEventBaseData'
				},
				success: function (data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							VipCardTypeList = result.VipCardTypeList,//卡类型接口
							VipCardGradeList = result.VipCardGradeList,//卡级别接口
							PersonCountList = result.PersonCountList,//默认抽奖次数
							EventsTypeList = result.EventsTypeList;//活动类型数据
						
						//活动规则的选择
						var ruleData = [{
							  'RuleId':1,
							  'RuleName':'文本'
							},
							{
							  'RuleId':2,
							  'RuleName':'图片'	
							}
							];	
						$('#ruleOption').combobox({
							width: 132,
							height: 34,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'RuleId',
							textField: 'RuleName',
							data:ruleData,
							onSelect: function(param){
								$('#ruleBgPic .infoBox').hide();
								if(param.RuleId==1){
									$('.ruleText').show();
								}else{
									$('.ruleImg').show();
								}
							}
						});
						setTimeout(function(){$('#ruleOption').combobox('select',1);},200);
						/*
						$('#cardType').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'VipCardTypeID',
							textField: 'VipCardTypeName',
							data:VipCardTypeList
						});
						
						$('#cardGrade').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'VipCardGradeID',
							textField: 'VipCardGradeName',
							data:VipCardGradeList
						});
						*/
						/*
						$('#eventsType').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'EventTypeID',
							textField: 'Title',
							data:EventsTypeList,
							onSelect: function(param){
								that.activityWay(param.EventTypeID);
							}
						});
						*/
						$('#personCount').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'Id',
							textField: 'Name',
							data:PersonCountList
						});

						if(that.elems.eventId){
							that.modifyFirstStep();
						}else{
							setTimeout(function(){
								//$('#cardType').combobox('setText',"请选择");
								//$('#cardGrade').combobox('setText',"请选择");
								//$('#eventsType').combobox('setText',"请选择");
								$('#personCount').combobox('setText',"请选择");
								$('#lEventDrawMethod').combobox('setText',"请选择");
							},100)
						}
						
					}else {
						alert(data.Message);
					}
				}
			});
		},
		modifyFirstStep:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetStep1Info',
					EventID: that.elems.eventId
				},
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data.EventInfo,
							title = result.Title,//活动名称
							beginTime = result.BeginTime,//活动开始时间
							endTime = result.EndTime,//活动结束时间
							content = result.Content,//活动内容
							eventTypeID = result.EventTypeID,//活动类型
							drawMethodId = result.DrawMethodId,//活动方式
							vipCardType = result.VipCardType,//Vip卡类型
							vipCardGrade = result.VipCardGrade,//Vip卡等级
							personCount = result.PersonCount,//参与游戏次数
							pointsLottery = result.PointsLottery;//所需积分
						//alert(title);待开发
						$('#eventsType').combobox('select',eventTypeID);
						$('#nav0_1').form('load',result);
						setTimeout(function(){
							$('#lEventDrawMethod').combobox('select',drawMethodId);
						},500);
						
						
					}else{
						alert(data.Message);
					}
				}
			});
		
		},
		setFirstStep:function(params,callback){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: params,
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0) {
						//设置eventId
						that.elems.eventId = data.Data.EventId;
						if(callback){
						    callback(params.DrawMethodId);
						}
					}else{
						alert(data.Message);
					}
				}
			});
		},
		//活动方式
		activityWay:function(id){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetDrawMethodByType',
					EventTypeId: id
				},
				success: function (data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							LEventDrawMethodList = result.LEventDrawMethodList;
						
					    //只显示大转盘和红包 start(要显示全部请把这段代码删除)
						LEventDrawMethodList = 
                            [{DrawMethodID:'2',DrawMethodName:'大转盘'},{DrawMethodID:'4',DrawMethodName:'红包'}]
					    
                        //只显示大转盘和红包end
                        
						$('#lEventDrawMethod').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'DrawMethodID',
							textField: 'DrawMethodName',
							data:LEventDrawMethodList
						});
						
					}else{
						alert(data.Message);
					}
				}
			});
		},
        //大转盘
		LuckyTurntable:function()
		{
		    $(".PrizeSet").hide();
		    $("#LuckyTurntable_form").show();


		},
        //刮刮卡
		ScratchCard: function () {
		    $(".PrizeSet").hide();
		    $("#ScratchCard_form").show();


		},
        //获取奖品配置列表isLoadAll:是否初始化所有数据（否则只初始化奖品列表数据）
		getPrizeList:function(isLoadAll){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetStep2Info',
					EventID: that.elems.eventId
				},
				success: function (data) {
				    debugger;
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							prizeList = result.PrizeList,//奖品列表
							imageList = result.ImageList,//图片列表
							html = '<tr><td colspan="7">亲，你还没有添加活动奖品哦！</td></tr>';

					    //初始化奖品列表数据start

						$(".PrizeseOption").html('');
						if (prizeList) {
						    if (that.elems.activiType == 1) {
						        html = bd.template("tpl_prizeList", result);
						    }

						    if (that.elems.activiType == 2) {
						        
						        html = bd.template("tpl_LuckyTurnprizeList", result);

						    }

						    that.GetPrizeLocationList();

						     for (i = 0; i < prizeList.length; i++) {
						         $(".PrizeseOption").append("<div class='radio ' data-prizesname='" + prizeList[i].PrizeName + "' data-imageurl='" + prizeList[i].ImageUrl + "' data-prizesid='" + prizeList[i].PrizesID + "' ><em></em>" + prizeList[i].PrizeName + "</div>");

						     }

						}
						 $(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX.png" data-prizesid=""><em></em>谢谢您！</div>');
						 if (that.elems.activiType == 1) {
						     $('#prizeListTable tbody').html(html);
						 }

						 if (that.elems.activiType == 2) {
						     $('#LuckyTurnListTable tbody').html(html);
						 }
                         
					    //初始化奖品列表数据end

						 $(".PrizeseOption").delegate(".radio", 'click', function ()
						 {
						     var $this = $(this);
						     $(".PrizeseOption .radio").removeClass("on");
						     $this.addClass("on");
						 });

					    //初始化图片数据start
						 if (isLoadAll) {


						     if (imageList) {
						         for (var i = 0; i < imageList.length; i++) {

						             if (that.elems.activiType == 1) {
						                 if (imageList[i].BatId == 'Logo') {
						                     $('#logoBgPic').data('url', imageList[i].ImageURL);
						                     $('#logoPic').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'BeforeGround') {
						                     $('#beforeBgPic').data('url', imageList[i].ImageURL);
						                     $('#redBgPic').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'BackGround') {
						                     $('#backBgPic').data('url', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'Rule') {
						                     $('#ruleBgPic').data('url', imageList[i].ImageURL);
						                 }
						             }

						             //大转盘start
						             if (that.elems.activiType == 2) {
						                 debugger;
						                 if (imageList[i].BatId == 'LT_regularpic') {
						                     $('#LT_Data_regularpic').data('url', imageList[i].ImageURL);
						                     $('#LT_regularpic').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'LT_kvPic') {
						                     $('#LT_Data_kvPic').data('url', imageList[i].ImageURL);
						                     $('#LT_kvPic').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'LT_bgpic1') {
						                     $('#LT_Data_bgpic1').data('url', imageList[i].ImageURL);
						                     $('#LT_bgpic1').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'LT_bgpic2') {
						                     $('#LT_Data_bgpic2').data('url', imageList[i].ImageURL);
						                     $('#LT_bgpic2').attr('src', imageList[i].ImageURL);
						                 } else if (imageList[i].BatId == 'LT_Rule') {
						                     $('#LT_Data_Rule').data('url', imageList[i].ImageURL);
						                 }
						             }
						             //大转盘end
						         }
						     }

						     if (that.elems.activiType == 1) {
						         $('#ruleOption').combobox('select', result.RuleId);
						         $('.ruleText textarea').html(result.RuleContent);
						     }
						 }

					    //初始化图片数据end

						
						 
					}
					else {
						alert(data.Message);
					}
				}
			});
		},
		addPrizeList:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action:'DoGetOptionListByName',
					OptionName:"PrizeGrade",
					IsSort:"true"
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							Options = result.Options;
						$('#prizeLevel').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'OptionID',
							textField: 'OptionText',
							data:Options
						});
						$('#prizeLevel').combobox('select', 0);
                        //大转盘start
						$('#LTprizeLevel').combobox({
						    width: 190,
						    height: that.elems.height,
						    panelHeight: that.elems.panlH,
						    lines: true,
						    valueField: 'OptionID',
						    textField: 'OptionText',
						    data: Options
						});
						$('#LTprizeLevel').combobox('select', 0);
					    //大转盘end
					}else{
						alert(data.Message);
					}
				}
			});
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetPrizeType'
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							PrizeTypeList = result.PrizeTypeList;
						$('#prizeOption').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'PrizeTypeCode',
							textField: 'PrizeTypeName',
							data:PrizeTypeList,
							onSelect: function(param){
								var $couponItem = $('#couponItem'),
									$integralItem = $('#integralItem');
								$couponItem.hide();
								$integralItem.hide();	
								if(param.PrizeTypeCode == 'Point'){
									$integralItem.show();
								}else if(param.PrizeTypeCode == 'Coupon'){
									//获取优惠券列表
									that.getCouponList();
									$couponItem.show();
								}
							}
						});

					    //大转盘start
						$('#LTprizeOption').combobox({
						    width: 190,
						    height: that.elems.height,
						    panelHeight: that.elems.panlH,
						    lines: true,
						    valueField: 'PrizeTypeCode',
						    textField: 'PrizeTypeName',
						    data: PrizeTypeList,
						    onSelect: function (param) {
						        var $couponItem = $('#LTcouponItem'),
									$integralItem = $('#LTintegralItem');
						        $couponItem.hide();
						        $integralItem.hide();
						        if (param.PrizeTypeCode == 'Point') {
						            $integralItem.show();
						        } else if (param.PrizeTypeCode == 'Coupon') {
						            //获取优惠券列表
						            that.getCouponList();
						            $couponItem.show();
						        }
						    }
						});

					    //大转盘end
					}else{
						alert(data.Message);
					}
				}
			});
			
		},
		getCouponList:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetCouponTypeInfo'
				},
				success: function(data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var result = data.Data,
							CouponTypeList = result.CouponTypeList;
						$('#couponOption').combobox({
							width: 190,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'CouponTypeID',
							textField: 'CouponTypeName',
							data:CouponTypeList
						});

						$('#LTcouponOption').combobox({
						    width: 190,
						    height: that.elems.height,
						    panelHeight: that.elems.panlH,
						    lines: true,
						    valueField: 'CouponTypeID',
						    textField: 'CouponTypeName',
						    data: CouponTypeList
						});
					}else{
						alert(data.Message);
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
						that.getPrizeList(false);
						$('.jui-mask').hide();
						$('.jui-dialog').hide();
						alert('成功添加奖品！');
					}else{
						alert(data.Message);
					}
				}
			});
		},
		//图片上传按钮绑定
        registerUploadImgBtn: function(){
            var self = this;
            // 路由上传按钮
            self.elems.simpleQuery.find(".uploadImgBtn").each(function(i,e) {
                self.addUploadImgEvent(e);
            });

            // 大转盘试试手气上传按钮注册
            self.elems.simpleQuery.find(".regularpicupload").each(function (i, e) {
                self.addUploadImgEvent(e);
            });

            // 大转盘奖项上传按钮注册
            $(".LuckyTurntableright").find(".uploadImgBtn").each(function (i, e) {
                self.addLuckyTurntableUploadImgEvent(e);
            });

            
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent:function(e) {
            var self = this,
				$redBgPic = $('#redBgPic'),
				$logoPic = $('#logoPic'),
				$imageTextPic = $('#imageTextPic');
            //大转盘start
            var $kvPic = $('#LT_kvPic'),
                $bgpic1 = $('#LT_bgpic1'),
                $bgpic2 = $('#LT_bgpic2'),
                $regularpic = $('#LT_regularpic');
            //大转盘end
            //上传图片并显示
            self.uploadImg(e,function(ele,data) {
				var $uploadItem = $(ele).parents('.uploadItem'),
					flag = $uploadItem.data('flag'),
					result = data,
					thumUrl = result.thumUrl,//缩略图
					url = result.url;//原图
                debugger
				if(flag==1){
					$redBgPic.attr('src',url);
				}else if(flag==3){
					$logoPic.attr('src',url);
				}else if(flag==4){
					$imageTextPic.attr('src',url);
					$('.previewPic img').attr('src',url);
				}

                //大转盘start


				if (flag == 10) {
				    $kvPic.attr('src', url);
				} else if (flag == 11) {
				   
				} else if (flag == 12) {
				    $bgpic1.attr('src', url);
				} else if (flag == 13) {
				    $bgpic2.attr('src', url);
				}
				else if (flag == 14) {
				    $regularpic.attr('src', url);
				    $("#LT_regularpic").addClass("border1");
				}


                //大转盘end
				$uploadItem.data('url',url);
            });
        },
        //上传图片区域的各种事件绑定
        addLuckyTurntableUploadImgEvent: function (e) {
            var self = this,
				$LuckyTurntablePrizeimg = $('#LuckyTurntablePrizeimg');
            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                var result = data,
					thumUrl = result.thumUrl,//缩略图
					url = result.url;//原图
                $LuckyTurntablePrizeimg.attr('src', url);
                debugger;
                $("#LuckyTurntablePrize").form("load", {ImageUrl:url});
            });
        },
        //上传图片
        uploadImg: function (btn, callback) {
            var _width = 130;
            var that = this,
				w = 640,
				h = 1008,
				flag = $(btn).parents('.uploadItem').data('flag');
			if(flag==3){
				w = 100;
				h = 100;	
			}else if(flag==4){
				w = 536;
				h = 300;
			}
			if ($(btn).parents('.uploadItem').data("flag") == 14)
			{
			    _width = 88;
			}
			var	uploadbutton = KE.uploadbutton({
					button: btn,
					width: _width,
					//上传的文件类型
					fieldName: 'imgFile',
					//注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
					url: that.elems.domain+'/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width='+w+'&height='+h,
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
		setSaveWxPrize:function(param,ruleId,ruleContent){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action : 'SaveEventStep2',
					EventID : that.elems.eventId,
					ItemImageList : param,
					RuleContent:ruleContent,
					RuleId:ruleId
				},
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0){
						//alert('图片保存成功！');
					}else{
						alert(data.Message);
					}
				}
			});
		},
		SavePrizeLocation: function (param) {//保存奖品位置
		        var that = this;
		        $.util.ajax({
		            url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
		            data: {
		                action: 'SavePrizeLocation',
		                EventID: that.elems.eventId,
		                PrizeLocationList: param
		            },
		            success: function(data){
		                if(data.IsSuccess && data.ResultCode == 0){
		                    //alert('图片保存成功！');
		                }else{
		                    alert(data.Message);
		                }
		            }
		        });
		},
		GetPrizeLocationList: function () {//根据EventID获取奖品位置列表
		    var that = this;
		    $.util.ajax({
		        url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
		        data: {
		            action: 'GetPrizeLocationList',
		            EventID: that.elems.eventId
		        },
		        success: function (data) {
		            if (data.IsSuccess && data.ResultCode == 0) {
		                //alert('图片保存成功！');
		                debugger;
		                var data = data.Data;
		                var imgs = $("#selectPrize p img");
		                if (data.PrizeLocationList)
		                {
		                    for (i = 0; i < data.PrizeLocationList.length; i++)
		                    {
		                        $("#Img" + data.PrizeLocationList[i].Location).data("index", data.PrizeLocationList[i].Location);
		                        $("#Img" + data.PrizeLocationList[i].Location).data("prizesid", data.PrizeLocationList[i].PrizeID);
		                        $("#Img" + data.PrizeLocationList[i].Location).data("prizesname", data.PrizeLocationList[i].PrizeName);
		                        $("#Img" + data.PrizeLocationList[i].Location).attr("src", data.PrizeLocationList[i].ImageUrl);
		                        $("#Img" + data.PrizeLocationList[i].Location).data("prizeLocationid", data.PrizeLocationList[i].PrizeLocationID);
		                        $("#Img" + data.PrizeLocationList[i].Location).addClass("_selected");
		                       
		                    }
		                }
		            } else {
		                alert(data.Message);
		            }
		        }
		    });
		},
		getThirdStep:function(){
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action : 'GetStep3Info',
					EventID : that.elems.eventId,
				},
				success: function (data) {
				    debugger;
					if(data.IsSuccess && data.ResultCode == 0){
						var result = data.Data.MaterialText,
							pageDetail = data.Data.EventInfo,
							params = {},
							jsonArray = [];
						if(!!result){
							var	title = result.Title || '',//标题
							text = result.Text || '',//描述
							coverImageUrl = result.CoverImageUrl || '';//图片
							$('#graphicTitle').val(title);
							$('#graphicDsc').val(text);
							$('#imageTextPic').attr('src',coverImageUrl);
							$('.imageTextArea').data('url',coverImageUrl);
							
							//图文浏览
							$('.previewTit').text(title);
							$('.previewPic img').attr('src',coverImageUrl);
							$('.previewDsc').text(text);
							
							params.OriginalUrl = result.OriginalUrl;
							params.TextId = result.TextId;
						}
						
						//PageParamJson
						jsonArray = [
								{
									key: "eventId",
									value: pageDetail.EventID,
									pageModule: JSON.stringify(data.Data.ModulePage?data.Data.ModulePage:[{}]),
									pageDetail: JSON.stringify({
										EventId: pageDetail.EventID,
										EventName: pageDetail.EventName,
										EventTypeId: pageDetail.EventTypeId,
										EventTypeName: pageDetail.EventTypeName,
										BegTime: pageDetail.BeginTime,
										EndTime: pageDetail.EndTime,
										EventStatus: pageDetail.EventStatus,
										CityName: "",
										EventStatusName: pageDetail.statusDESC,
										DrawMethod: ""
									}),
									UnionTypeId: 3
							  }
						];
						params.UnionTypeId = "3";
						params.Author = '';
						params.PageId = data.Data.ModulePage.PageID;
						params.IsTitlePageImage = "1";
						params.ModuleName = data.Data.ModulePage.ModuleName;
						params.MappingId = data.Data.MappingId || '';//绘制id
						params.PageParamJson = JSON.stringify(jsonArray?jsonArray:[{}]);
						params.Title = '';
						params.Text = '';
						params.ImageUrl= '';
						
						that.elems.jsonParams = params;
					}else{
						alert(data.Message);
					}
				}
			});
		},
		setThirdStep:function(params){
			var that = this,
				title = $('#graphicTitle').val(),
				text = $('#graphicDsc').val(),
				coverImageUrl = $('.imageTextArea').data('url');
			
			params.Title = title;
			params.Text = text;
			params.ImageUrl = coverImageUrl;

			
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action : 'SaveEventStep3',
					EventID : that.elems.eventId,//活动id
					MaterialText: params,//图文素材
					MappingId: params.MappingId//活动关键字和图文素材的映射
				},
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0){
						location.href = "QueryList.aspx?&mid=" + JITMethod.getUrlParam("mid");	
					}else{
						alert(data.Message);
					}
				}
			});
		},
		getTimer:function(day){
			var doHandleMonth = function(month){
				   var m = month;  
				   if(month.toString().length == 1){  
					  m = "0" + month;
				   }  
				   return m; 
			};
			var today = new Date();
			var targetday_milliseconds=today.getTime() + 1000*60*60*24*day;
			today.setTime(targetday_milliseconds); //注意，这行是关键代码 
			var tYear = today.getFullYear();  
			var tMonth = today.getMonth();  
			var tDate = today.getDate();  
			tMonth = doHandleMonth(tMonth + 1);  
			tDate = doHandleMonth(tDate);
			return tYear+"-"+tMonth+"-"+tDate;
		}
    };
    page.init();
});


