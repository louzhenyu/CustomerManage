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
            skuTable: $("#skuTable"),
            colorplan: $(".colorplan"),             //调色板
            width:160,
            height:32,
            panlH:200,
            sku:$("#sku"),
            loadAlert:null,
            priceFilde:"item_price_type_name_",//数据库价格相关的字段，一般有销量库存，价格实体价格
            allData:{}, //页面所有存放对象基础数据
            eventId: $.util.getUrlParam('EventID'),
            CoverId:"",
            eventName:"",
            DrawMethod:1,
			jsonParams: {},
			activiType: 1, //活动类型1:红包，2：大转盘，3：刮刮卡,4:问卷
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

		    //加载调色板
			$(".color_plan").append(bd.template("tpl_colorplan", ""));
			
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
                debugger
                switch (idVal) {
                    case 'nav01':
                        that.navshow("nav01", $panelDiv);
                        break;
                    case 'nav02':
                        that.BasicInformationStep(idVal, $panelDiv, $this);
                        break;
                    case 'nav03':
                        that.CoverSettingStep(idVal, $panelDiv, $this);
                        break;
                    case 'nav04':
                        that.PrizeAllocationStep(idVal, $panelDiv, $this);
                        break;
                    case 'nav05':
                        that.GraphicPushStep(idVal, $panelDiv, $this);
                    default:
                        break;
                }
            });



            //调色板事件start
            that.elems.colorplan.on("click", ".writecolor", function (e) {

                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", "background-color:#FFFFFF;color:#000;");
                    $(".regular").attr("style", "color:#FFFFFF");
                    $("#StartPageBtnBGColor").val("#FFFFFF");
                    $("#StartPageBtnTextColor").val("#000");
                }

                if (type == 2) {
                    $(".Endbtn").attr("style", "background-color:#FFFFFF;color:#000;");
                    $("#QResultBGColor").val("#FFFFFF");
                    $("#QResultBtnTextColor").val("#000");
                }
            });

            that.elems.colorplan.on("click", ".blackcolor", function (e) {
                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", "background-color:#000000;color:#fff;");
                    $(".regular").attr("style", "color:#000");
                    $("#StartPageBtnBGColor").val("#000");
                    $("#StartPageBtnTextColor").val("#fff");
                }

                if (type == 2) {
                    $(".Endbtn").attr("style", "background-color:#000000;color:#fff;");
                    $("#QResultBGColor").val("#000");
                    $("#QResultBtnTextColor").val("#fff");
                }
            });

            $("body").on("click", function () {
                $(".colorplan .Colorplate").each(function () {
                    if ($(this).css("display") != "none") {
                        $(this).hide();
                    }
                });
            });


            that.elems.colorplan.on("click", ".allcolor", function (e) {
                $(".colorplan .Colorplate").toggle();
                e.stopPropagation();
            });

            $(".colorplan").on("click", ".Colorplate div", function (e) {
                debugger;
                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", $(this).attr("style"));
                    $(".regular").attr("style", "color:" + $(this).css("background-color"));
                    $("#StartPageBtnBGColor").val($(this).css("background-color"));
                    $("#StartPageBtnTextColor").val($(this).css("color"));
                }

                if (type == 2) {
                    $(".Endbtn").attr("style", $(this).attr("style"));
                    $("#QResultBGColor").val($(this).css("background-color"));
                    $("#QResultBtnTextColor").val($(this).css("color"));
                }
                $(".colorplan .Colorplate").hide();
            });



            //调色板事件end
			//进入按钮字体设置事件
            $("#ButtonName").keyup(function () {
                $(".startbtn").text($(this).val());

            });
            //进入按钮字体设置事件end

            //封面设置页点击启用规则
            $(".startpageContent").delegate(".checkBox", "click", function (e) {
                var me = $(this)
                me.toggleClass("on");

                var checkvalue = $(this).find(".checkvalue");
                if (checkvalue.val() == 1) {
                    checkvalue.val(0);
                    $("#QRegular").data("required", false);
                } else {
                    checkvalue.val(1);
                    $("#QRegular").data("required", true);

                }

            });


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

            //问卷奖品配置模块
            $('#QuestionnaireaddBtn').bind('click', function () {

                $("#Questionnaire").form('clear');
                $('.jui-mask').show();
                $('.jui-dialog-Questionnaire').show();
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
            //问卷
			$('.jui-dialog-Questionnaire .saveBtn').bind('click', function () {
			    //保存奖品
			    if ($('#Questionnaire').form('validate')) {//a03d28e78b2c18104d4812ba18a5c69b
			        var prams = { action: 'SavePrize', EventId: that.elems.eventId },
						fields = $('#Questionnaire').serializeArray();
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
			$('#prizeListTable,#LuckyTurnListTable,#QuestionnaireTable').delegate('.addBtn', 'click', function () {
				var $this = $(this),
					$tr = $this.parents('tr'),
					$num = $('.numBox',$tr),
					num = $num.text()-0,
					prizesId = $tr.data('prizesid');
				var CouponTypeID = $tr.find(".Prizedata").data("coupontypeid");
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
							PrizesID: prizesId,
							CouponTypeID: CouponTypeID
						},
						beforeSend: function () {
						    $.util.isLoading()

						},
						success: function(data){
							if(data.IsSuccess && data.ResultCode == 0) {
								$('.jui-mask').hide();
								$('.jui-dialog-prizeCountAdd').hide();
								$num.text(count);
							}else{
								alert(data.Message);
							}
						},
						complete: function () {
						    $.util.isLoading(true);
						}
					});
					
					
				});
			});
			
			
			
			//删除
			$('#prizeListTable,#LuckyTurnListTable,#QuestionnaireTable').delegate('.delBtn', 'click', function () {
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
							beforeSend: function () {
							    $.util.isLoading()

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
					beforeBgPic = $('#beforeBgPic').data('url');
				if($this.text()=='查看领取后页面'){
				    $('#redBgPic').attr('src', beforeBgPic);
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
        //保存基本信息
        BasicInformationStep: function (idVal, $panelDiv, $this) {
            var that = this;
            if ($('#nav0_1').form('validate')) {
                var prams = { action: 'SaveEventStep1', EventID: that.elems.eventId },
                    fields = $('#nav0_1').serializeArray();
                //console.log(fields);

                for (var i = 0; i < fields.length; i++) {
                    var obj = fields[i];
                    prams[obj['name']] = obj['value'];
                }
                //console.log(prams);
                prams.EventTypeID = '081AEC92-CC16-4041-9496-B4F6BC3B11FC';
                if (prams.PointsLottery == '') {
                    prams.PointsLottery = 0;
                }

                that.setFirstStep(prams, function (DrawMethodId) {
                    if (DrawMethodId == 2) {
                        that.elems.activiType = 2;
                        that.LuckyTurntable();
                        //} else if (DrawMethodId == 1) {
                        //    that.elems.activiType = 3;
                        //    that.ScratchCard();
                    } else if (DrawMethodId == 5) {
                        that.elems.activiType = 4;
                        that.Questionnaire();
                        that.getQuestionnaireList(function (data) {
                            $("#Questionnaires").combobox({
                                width: 132,
                                height: 34,
                                panelHeight: that.elems.panlH,
                                lines: true,
                                valueField: 'QuestionnaireID',
                                textField: 'QuestionnaireName',
                                data: data.Data.QuestionnaireList
                            });
                        });

                    } else {
                        $(".PrizeSet").hide();
                        $("#nav0_2").show();
                    }


                    that.navshow(idVal, $panelDiv);

                    //保存第一步的同时获取第二步的数据
                    //待开发
                });
                //第二步，奖品配置模块数据初始化
                that.getPrizeList(true);
                that.addPrizeList();
            }
        },
        //保存奖品配置
        PrizeAllocationStep: function (idVal, $panelDiv, $this) {
            var that = this;
            if ($this.data('page') == 'redPackage') {
                var array = [],
                    leng;
                debugger;
                if (that.elems.activiType == 1) {
                    var logo = $('#logoBgPic').data('url'),
                        beforeGround = $('#beforeBgPic').data('url'),
                        rule = $('#ruleBgPic').data('url'),
                        ruleContent = $('.ruleText textarea').val(),
                        ruleFlag = $('#ruleOption').combobox('getValue');
                    leng = $('#prizeListTable tbody tr td').length > 1;

                    if (!leng) {
                        return $.messager.alert("提示", '请添加奖品信息！');
                    } else if (!beforeGround) {
                        return $.messager.alert("提示", '请上传领取前背景图片！');
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
                                    "ImageURL": "",
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
                        return $.messager.alert("提示", '请添加奖品信息！');
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
                    var imgjson = Ext.decode(imgjsonstr);
                    that.SavePrizeLocation(imgjson);
                }

                //花样问卷
                if (that.elems.activiType == 4) {
                    var QN_text = $('#Questionnaires').combobox('getText');
                    QN_value = $('#Questionnaires').combobox('getValue');
                    leng = $('#QuestionnaireTable tbody tr td').length > 1;

                    if (!leng) {
                        return $.messager.alert("提示", '请添加奖品信息！');
                    } else if (!QN_value || QN_value == "") {
                        return $.messager.alert("提示", '请选择表单！');
                    }
                    array = [

                    ];

                    that.setSaveWxPrize(array, 1, "", QN_value, QN_text);
                }

            }
            //保存第二步的同时获取第三步的数据
            that.navshow(idVal, $panelDiv);
            that.getThirdStep();
        },
        //保存图文推送
        GraphicPushStep: function (idVal, $panelDiv, $this) {
            var that = this;
            if ($('#nav0_3').form('validate')) {
                var url = $('.imageTextArea').data('url');
                if (!url) {
                    return alert('请上传图片！');
                }
                //待开发，保存第三步

                debugger;
                that.setThirdStep(that.elems.jsonParams);
            }
        },
        //封面设置
        CoverSettingStep: function (idVal, $panelDiv, $this) {
            var that = this;
            var param = {
                action: 'SaveCover',
                EventID: that.elems.eventId
            };
            debugger;
            param.IsShow = $(".checkvalue").val();
            param.ButtonText = $("#ButtonName").val();
            param.ButtonColor = $("#StartPageBtnBGColor").val();
            param.ButtonFontColor = $("#StartPageBtnTextColor").val();
            param.CoverImageUrl = $("#CoverImageUrl").data('url');
            param.RuleType = $("#ruleType").combobox("getValue");
            if (that.elems.CoverId != "") {
                param.CoverId = that.elems.CoverId;
            }

            if (param.IsShow == "1") {

                if (!param.CoverImageUrl) {
                    return alert('请上传封面背景图片！');
                }

                if (param.RuleType == "") {
                    return alert('请选择规则类型！');
                }

                if (param.RuleType == "2") {

                    param.RuleText = $("#QRegular").val();
                } else {

                    param.RuleImageUrl = $("#RuleImageUrl").data('url');
                    if (!param.RuleImageUrl) {
                        return alert('请上传规则图片！');
                    }

                }

                

            }


            if ($('#nav0_5').form('validate')) {

                that.SaveCover(param, function () {

                    that.navshow(idVal, $panelDiv);

                });

            }

          
        },
        navshow: function (idVal, $panelDiv)
        {
            var that = this;
            that.elems.optPanel.find("li").removeClass("on");
            $("." + idVal).addClass("on");
            $panelDiv.hide();
            $("#"+idVal).fadeIn("slow");
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
							    if (param) {
							        if (param.RuleId == 1) {
							            $('.ruleText').show();
							        } else {
							            $('.ruleImg').show();
							        }
							    }
							}
						});

						$('#ruleType').combobox({
						    width: 200,
						    height: 34,
						    panelHeight: that.elems.panlH,
						    lines: true,
						    valueField: 'RuleId',
						    textField: 'RuleName',
						    data: [{
						        RuleId: "1",
						        RuleName:"图片"
						    }, {
						        RuleId: "2",
						        RuleName: "文字"
						    }],
						    onSelect: function (param) {
						        debugger;
						        $('.CoverSettingrule .infoBox').hide();
						        if (param) {
						            if (param.RuleId == 2) {
						                $('.CoverruleText').show();
						            } else {
						                $('.ruleTextImg').show();
						            }
						        }
						    }
						});

						$("#ruleType").combobox("select", 1);

						setTimeout(function(){$('#ruleOption').combobox('select',1);},200);
						
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
						
                        //获取封面数据
						if (data.Data.CoverId && data.Data.CoverId != "") {
						    var param = {
						        action: 'GetCover',
						        CoverId: data.Data.CoverId
						    };
						    that.elems.CoverId = data.Data.CoverId;
						    that.GetCover(param, function (data) {
						        debugger;
						        if (data) {
						            data = data.CoverInfo;
						            if (data.IsShow||data.IsShow == "0") {
						                $(".EnableCover").removeClass("on");
						            } else {
						                $(".EnableCover").addClass("on");
						                $(".checkvalue").val(1);

						            }

						            $("#QRegular").val(data.RuleText);



						            if (data.ButtonText && data.ButtonText != "") {
						                $("#ButtonName").val(data.ButtonText);

						                $(".startbtn").html(data.ButtonText);
						            }
						            if (data.ButtonColor && data.ButtonColor != "") {
						                $("#StartPageBtnBGColor").val(data.ButtonColor);
						            }
						            if (data.ButtonFontColor && data.ButtonFontColor != "") {
						                $("#StartPageBtnTextColor").val(data.ButtonFontColor);
						                $(".startbtn").attr("style", "background-color:" + data.ButtonColor + ";color:" + data.ButtonFontColor + ";");
						                $(".regular").attr("style", "color:" + data.ButtonColor + ";");
						            }
						            $("#CoverImageUrl").data("url", data.CoverImageUrl);
						            $("._BGImageSrc").attr("src", data.CoverImageUrl);
						            $("#ruleType").combobox("select", data.RuleType);
						            
						            $("#RuleImageUrl").data("url", data.RuleImageUrl);
						        }

						    });
						}

					}else{
						alert(data.Message);
					}
				}
			});
		
		},
		setFirstStep:function(params,callback){
			var that = this;
			$.util.ajax({
			    url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
			    async: false,
				data: params,
				success: function(data){
					if(data.IsSuccess && data.ResultCode == 0) {
					    //设置eventId
					    debugger;
					    that.elems.eventId = data.Data.EventId;
					    that.elems.DrawMethod = params.DrawMethodId;
					    that.elems.eventName = params.Title;
						if(callback){
						    callback(params.DrawMethodId);
						}
					}else{
						alert(data.Message);
					}
				}
			});
		},
        //保存封面
		SaveCover: function (params, callback) {
		        var that = this;
		        $.util.ajax({
		            url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
		            async: false,
		            data: params,
		            beforeSend: function () {
		                $.util.isLoading()

		            },
		            success: function(data){
		                if(data.IsSuccess && data.ResultCode == 0) {
		                    if (callback) {
		                        callback();
		                    }
		                }else{
		                    alert(data.Message);
		                }
		            }
		        });
		}, //获取封面
		GetCover: function (params, callback) {
		    var that = this;
		    $.util.ajax({
		        url: that.elems.domain + "/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
		        async: false,
		        data: params,
		        success: function (data) {
		            if (data.IsSuccess && data.ResultCode == 0) {
		                if (callback) {
		                    callback(data.Data);
		                }
		            } else {
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
                            [{ DrawMethodID: '2', DrawMethodName: '大转盘' }, { DrawMethodID: '4', DrawMethodName: '红包' }, { DrawMethodID: '5', DrawMethodName: '花样问卷' }]
					    
                        //只显示大转盘和红包end
                        
						$('#lEventDrawMethod').combobox({
							width: that.elems.width,
							height: that.elems.height,
							panelHeight: that.elems.panlH,
							lines:true,
							valueField: 'DrawMethodID',
							textField: 'DrawMethodName',
							data: LEventDrawMethodList,
							onSelect: function (param) {
							    
							}
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
        //问卷
		Questionnaire: function () {
		    $(".PrizeSet").hide();
		    $("#Questionnaire_form").show();


		},
        //刮刮卡
		ScratchCard: function () {
		    $(".PrizeSet").hide();
		    $("#ScratchCard_form").show();


		},
        //获取奖品配置列表isLoadAll:是否初始化所有数据（否则只初始化奖品列表数据）
		getPrizeList: function (isLoadAll) {
		    debugger;
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action: 'GetStep2Info',
					EventID: that.elems.eventId,
					DrawMethod: that.elems.DrawMethod
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
						    if (that.elems.activiType == 1 || that.elems.activiType == 4) {
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
						//$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX.png" data-prizesid=""><em></em>谢谢您！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_CH.png" data-prizesid=""><em></em>谢谢您（橙黄）！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_JH.png" data-prizesid=""><em></em>谢谢您（橘黄）！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_QL.png" data-prizesid=""><em></em>谢谢您（浅蓝）！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_TL.png" data-prizesid=""><em></em>谢谢您（天蓝）！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_ZS.png" data-prizesid=""><em></em>谢谢您（紫色）！</div>');
						$(".PrizeseOption").append('<div class="radio " data-prizesname="" data-imageurl="images/THX_HB.png" data-prizesid=""><em></em>谢谢您（红包空）！</div>');
						 if (that.elems.activiType == 1) {
						     $('#prizeListTable tbody').html(html);
						 }

						 if (that.elems.activiType == 2) {
						     $('#LuckyTurnListTable tbody').html(html);
						 }

						 if (that.elems.activiType == 4) {
						     $('#QuestionnaireTable tbody').html(html);
						 }
                         
					    //初始化奖品列表数据end

						 $(".PrizeseOption").delegate(".radio", 'click', function ()
						 {
						     var $this = $(this);
						     $(".PrizeseOption .radio").removeClass("on");
						     $this.addClass("on");
						 });

						 if (that.elems.activiType == 4) {
						     debugger;
						     if (result.QuestionnaireID && result.QuestionnaireID != "") {
						         $("#Questionnaires").combobox("setValue", result.QuestionnaireID);
						     }
						 }

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
						         if (result.RuleId == 0)
						         {
						             result.RuleId = 1;
						         }
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
				beforeSend: function () {
				    $.util.isLoading()

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


					    //问卷start
						$('#questprizeOption').combobox({
						    width: 190,
						    height: that.elems.height,
						    panelHeight: that.elems.panlH,
						    lines: true,
						    valueField: 'PrizeTypeCode',
						    textField: 'PrizeTypeName',
						    data: PrizeTypeList,
						    onSelect: function (param) {
						        var $couponItem = $('#_questcouponOption'),
									$integralItem = $('#_questintegralItem');
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

					    //问卷end
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

						$('#questcouponOption').combobox({
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
				beforeSend:function()
				{
				    $.util.isLoading()

				},
				success: function(data) {
					if(data.IsSuccess && data.ResultCode == 0) {
						that.getPrizeList(false);
						$('.jui-mask').hide();
						$('.jui-dialog').hide();
						alert('成功添加奖品！');
					}else{
						alert(data.Message);
					}
				},
				complete: function () {
				    $.util.isLoading(true);
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
        addUploadImgEvent: function (e) {
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
				debugger;
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

                //封面
				if (flag == "Cover") {
				    $("._BGImageSrc").attr('src', url);
				}
				
                //封面end

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
			} else if (flag == "Cover" || flag == "Cover1") {
			    _width = 80;
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
							    callback(btn, data);
							    if ($(btn).data("alertinfo")) {
							        alert($(btn).data("alertinfo"));
							    } else {
							        alert("图片上传成功！");
							    }
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
        setSaveWxPrize: function (param, ruleId, ruleContent, QuestionnaireID, QuestionnaireName) {
			var that = this;
			$.util.ajax({
				url: that.elems.domain+"/ApplicationInterface/Module/WEvents/EventsSaveHandler.ashx",
				data: {
					action : 'SaveEventStep2',
					EventID: that.elems.eventId,
					EventName: that.elems.eventName,
					ItemImageList : param,
					RuleContent:ruleContent,
					RuleId: ruleId,
					DrawMethod: that.elems.DrawMethod,
					QuestionnaireID: QuestionnaireID,
					QuestionnaireName: QuestionnaireName
				},
				beforeSend: function () {
				    $.util.isLoading()

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
		            beforeSend: function () {
		                $.util.isLoading()

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
		getQuestionnaireList: function (callback) {//获取问卷数据
		    debugger;
		    var that = this;
		    $.util.ajax({
		        url: "/ApplicationInterface/Gateway.ashx",
		        async: false,
		        data: {
		            action: 'Questionnaire.Questionnaire.GetQuestionnaireList',
		            PageSize: 100,
		            PageIndex: 1
		        },
		        success: function (data) {
		            if (data.IsSuccess && data.ResultCode == 0) {
		                var result = data.Data;
		                if (callback) {
		                    callback(data);
		                }

		            } else {
		                debugger;
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
				beforeSend: function () {
				    $.util.isLoading()

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


