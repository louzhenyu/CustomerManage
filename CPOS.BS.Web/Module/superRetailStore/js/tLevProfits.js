define(['jquery', 'langzh_CN', 'easyui', 'template', 'tools','artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage: $("#section"),
			hierarchySystem: $('#hierarchySystem'),
            SuperRetailTraderProfitConfigId:[]
        },
        init: function () {
			$.util.isLoading(false);
            this.initEvent();
            this.setPriceRule();
            //三级分销利润提成体系
            //this.setProfitSystem();
        },
        initEvent:function(){
            var that = this;
			
			that.elems.hierarchySystem.delegate('.validatebox-text','blur',function(){
				debugger;
				setTimeout(function(){
					var $this = $(this),
						$residueProfit = $('#residueProfit'),
						$systemInput = $('.systemInput',that.elems.hierarchySystem),
						lev1 = $systemInput.eq(0).numberbox('getValue') || 0,
						lev2 = $systemInput.eq(1).numberbox('getValue') || 0,
						lev3 = $systemInput.eq(2).numberbox('getValue') || 0,
						num = parseFloat($('.systemInput').eq(2).numberbox('getValue')),
						residueNum = num - lev1 - lev2 - lev3,
						residueNum = parseFloat(residueNum).toFixed(1);
					$residueProfit.text(residueNum + '%');
					$('.affirmArea .lineText span').text(residueNum);
				},500);
			});
			
			$('.protocolName').on('keyup',function(){
				var $this = $(this),
					$numLimit = $('.numLimit span'),
					val = $this.val(),
					leng = val.length;
				$numLimit.text(leng>5?5:leng);
			});
            //一键分销按钮点击事件
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {
                //获取用户设置比例,判断100%
                var rate = [parseFloat($('.systemInput')[0].value), parseFloat($('.systemInput')[1].value), parseFloat($('.systemInput')[2].value), parseFloat($('.systemInput')[3].value)];
                var totalRate = rate[0] + rate[1] + rate[2] + rate[3];
                if (totalRate.toFixed(1) == 100) {
					$('#winmessage3').window({
						title: "提示", width: 390, height: 250, top:($(window).height() - 390) * 0.5,
						left: ($(window).width() - 250) * 0.5
					});
					$('#winmessage3').window('open');
                    //调用slidePriceBlock函数。设置色块显示
                    that.slidePriceBlock(rate);
                    //将设置的数据传递给后台更新
                    that.sendPrice(rate);
					
                } else {
                    $.messager.alert('提示', '您输入的数值总和不等于100%，请重新输入。');
                }
            });
			
			
            //三级分销利润保存
            that.elems.sectionPage.delegate(".saveProfit", "click", function (e) {
                //校验数据正确性
				/*
				var num = parseFloat($('.systemInput')[2].value),
					lev4 = $('.easyui-numberbox.systemInput').eq(4).numberbox('getValue') || 0,
					lev3 = $('.easyui-numberbox.systemInput').eq(5).numberbox('getValue') || 0,
					lev2 = $('.easyui-numberbox.systemInput').eq(6).numberbox('getValue') || 0,
					residueNum = num - lev4 - lev3 - lev2,
					residueNum = parseFloat(residueNum).toFixed(1);
				*/
				var residueNum = parseFloat($('#residueProfit').text());
				debugger;	
				if(residueNum<0){
					$.messager.alert('提示', '分润体系不能大于佣金体系,请重新设置');
					return;
				}else if(residueNum>0){
					//$('.affirmArea .lineText span').text(residueNum);
					$('#winmessage2').window({
						title: "提示", width: 475, height: 300, top:($(window).height() - 300) * 0.5,
						left: ($(window).width() - 475) * 0.5
					});
					$('#winmessage2').window('open');
				}else{
					that.sendProfit();
				}
            });
			//三级分销提成系统确认
            $('#winmessage2').delegate(".saveBtn","click",function(e){
				that.sendProfit();
            });
			
			
            // 三级分销提成体系 点击取消按钮重置数据
            that.elems.sectionPage.delegate(".cancelProfit", "click", function (e) {
                that.setProfitSystem();
            });
			
			
			//设置三级分销按钮的事件
			$('.switchBtn').on('click',function(){
				var $this = $(this),
					$parent = $this.parent(),
					$systemInput = $('.systemInput',$parent),
					$validatebox = $('.validatebox-text',$parent),
					flag = $this.data('flag');
				if($this.hasClass('on')){
					$this.removeClass('on');
					if(flag=='level'){
						$('.systemTwo .switchBtn').hide();
						$('.systemTwo .switchBtn').addClass('on');
						$('.systemTwo .switchBtn').trigger('click');
					}
					$systemInput.numberbox({
						//min: 0,
						//max: null,
						disabled: false
					});
				}else{
					$this.addClass('on');
					if(flag=='level'){
						$('.systemTwo .switchBtn').show();
					}
					$systemInput.numberbox({
						//min: 0,
						//max: null,
						disabled: true
					});
					//开关不启用是数值清零
					$systemInput.numberbox('setValue',0);
				}
				var num = parseFloat($('.systemInput')[2].value),
					lev4 = $('.easyui-numberbox.systemInput').eq(4).numberbox('getValue') || 0,
					lev3 = $('.easyui-numberbox.systemInput').eq(5).numberbox('getValue') || 0,
					lev2 = $('.easyui-numberbox.systemInput').eq(6).numberbox('getValue') || 0,
					residueNum = num - lev4 - lev3 - lev2,
					residueNum = parseFloat(residueNum).toFixed(1);
				$('#residueProfit').text(residueNum + '%');
				$('.affirmArea .lineText span').text(residueNum);	
				
			});
			
			
			//录入协议事件弹层
            $('#enteringProtocolBtn').bind('click', function (){
                $('#winmessage').window({
                    title: "分销商协议", width: 600, height: 385, top: ($(window).height() - 385) * 0.5,
                    left: ($(window).width() - 600) * 0.5
                });
                $('#winmessage').window('open');
				$('.protocolName').trigger('keyup');
            });
            //确认按钮
            $('#winmessage').delegate(".saveBtn","click",function(e){
				that.sendProtocol();
            });
			
			
        },
        //后台获取三级分销提成体系
        setProfitSystem: function () {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'SuperRetailTrader.SuperRetailTraderProfitConfig.GetSuperRetailTraderProfitConfig'
                },
                success: function (data) {
                    var profit = 0;                //分润分销比例
                    if (data.IsSuccess && data.ResultCode == 0) {
						var profitData = data.Data.ProfitConfigList;
                        for (var i = 0; i < profitData.length; i++) {
                            if (profitData[i].Level == 4) {
                               $('.easyui-numberbox.systemInput').eq(4).numberbox('setValue', profitData[i].Profit);   //一级分润
								$('#lev1Num').text(profitData[i].Profit);
                                profit = (Number(profit) + profitData[i].Profit).toFixed(1);
								that.Level4Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 3) {
                                $('.easyui-numberbox.systemInput').eq(5).numberbox('setValue', profitData[i].Profit);   //二级分润
								$('#lev2Num').text(profitData[i].Profit);
								//开关状态控制
								if(profitData[i].Status == 90){
									$('.systemTwo .switchBtn').removeClass('on');
								}else if(profitData[i].Status == 10){
									$('.systemTwo .switchBtn').addClass('on');
								}
								$('.systemTwo .switchBtn').trigger('click');
                                profit = (Number(profit) + profitData[i].Profit).toFixed(1);
								that.Level3Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 2) {
                                $('.easyui-numberbox.systemInput').eq(6).numberbox('setValue', profitData[i].Profit);//三级分润							
								$('#lev3Num').text(profitData[i].Profit);
								//开关状态控制
								if(profitData[i].Status == 90){
									$('.systemThree .switchBtn').removeClass('on');
								}else if(profitData[i].Status == 10){
									$('.systemThree .switchBtn').addClass('on');
								}
								$('.systemThree .switchBtn').trigger('click');
                                profit = (Number(profit) + profitData[i].Profit).toFixed(1);
								that.Level2Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 1) {//商品佣金
                                $('.systemFour').find('span').text('商品销售佣金比例' + profitData[i].Profit + '%');        									          						that.LevelProfit = profitData[i].Profit;
								that.Level1Id = profitData[i].SuperRetailTraderProfitConfigId;
                            }
                        }
                        $('.systemFifve').find('span').text('分润分销比例' + parseFloat($('.systemInput')[2].value).toFixed(1) + '%');
						var residueProfit = (parseFloat($('.systemInput')[2].value))-Number(profit);
						$('#residueProfit').text((residueProfit.toFixed(1) || 0) + '%');
                    } else {
                        alert(data.Message);
                    }
                }
            })
        },
        //将用户修改设置的三级分销提成体系传送给后台
        sendProfit:function(){
            var that = this,
				status2 = $('.systemThree .switchBtn').hasClass('on')?90:10,
				status3 = $('.systemTwo .switchBtn').hasClass('on')?90:10;
            var ProfitConfigList = [
				{
                    'Level': 1,
                    'Profit':that.LevelProfit,
                    'Status': 10,
                    'SuperRetailTraderProfitConfigId': that.Level1Id
                },
                {
                    'Level': 2,
                    'Profit':$($('.easyui-numberbox.systemInput')[6]).numberbox('getValue'),//三级分润输入框
                    'Status': status2,
                    'SuperRetailTraderProfitConfigId': that.Level2Id
                },
                {
                    'Level': 3,
                    'Profit': $($('.easyui-numberbox.systemInput')[5]).numberbox('getValue'),//二级分润输入框
                    'Status': status3,
                    'SuperRetailTraderProfitConfigId': that.Level3Id
                },
                {
                    'Level': 4,
                    'Profit': $($('.easyui-numberbox.systemInput')[4]).numberbox('getValue'),//一级分润输入框
                    'Status': 10,
                    'SuperRetailTraderProfitConfigId': that.Level4Id
                }
            ];
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'SuperRetailTrader.SuperRetailTraderProfitConfig.SetuperRetailTraderProfitConfig',
                    'ProfitConfigList': ProfitConfigList
                },
                success: function (data) {
                    var profit = 0;                //分润分销比例
					console.log(profitData);
                    if (data.IsSuccess && data.ResultCode == 0) {
						var profitData = data.Data.lst;
                        for (i = 0; i < profitData.length; i++) {
                            if (profitData[i].Level == 4) {
                                $('.easyui-numberbox.systemInput').eq(4).numberbox('setValue', profitData[i].Profit);   //一级分润
                                profit = profit + profitData[i].Profit;
								that.Level4Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 3) {
                                $('.easyui-numberbox.systemInput').eq(5).numberbox('setValue', profitData[i].Profit);   //二级分润
                                profit = profit + profitData[i].Profit;
								that.Level3Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 2) {
                                $('.easyui-numberbox.systemInput').eq(6).numberbox('setValue', profitData[i].Profit);   //三级分润
                                profit = profit + profitData[i].Profit;
								that.Level2Id = profitData[i].SuperRetailTraderProfitConfigId;
                            } else if (profitData[i].Level == 1) {//商品佣金
                                $('.systemFour').find('span').text('商品销售佣金比例' + profitData[i].Profit + '%');        									          						that.LevelProfit = profitData[i].Profit;
								that.Level1Id = profitData[i].SuperRetailTraderProfitConfigId;
                            }
                        }
                        $('.systemFifve').find('span').text('分润分销比例' + profit + '%');
						var residueProfit = (parseInt($('.systemInput')[2].value))-profit;
						$('#residueProfit').text(residueProfit + '%');
						$('#winmessage2').window('close');
						//重置商品定价规则的数据
						that.setPriceRule();
						$.messager.alert('提示', '三级分销提成体系保存成功');
                    } else {
						$('#winmessage2').window('close');
						$.messager.alert('提示', data.Message);
                    }
					
					
					
					
					/*
                    if (data.IsSuccess && data.ResultCode == 0) {
                        $.messager.alert('提示', '三级分销提成体系保存成功');
                    } else {
                        alert(data.Message);
                    }
					*/
                }
            })
        },
        //从后台获取商品定价规则数据。将数据显示至页面
        setPriceRule: function () {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'SuperRetailTrader.SuperRetailTraderConfig.GetTSuperRetailTraderConfig'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        //设置框的value修改
						that.customerValId = data.Data.Id;
                        var rate = [data.Data.Cost, data.Data.SkuCommission, data.Data.DistributionProfit, data.Data.CustomerProfit];
                        /*for (i = 0; i < 4; i++) {
							debugger;
                            var len = rate[i].length - 1;
                            rate[i] = rate[0].toString().substr(0, len);
                        }*/
                   /*     $('.easyui-numberbox.systemInput').eq(0).numberbox('setValue', rate[0]);
                         $('.easyui-numberbox.systemInput').eq(1).numberbox('setValue', rate[1]);
                         $('.easyui-numberbox.systemInput').eq(2).numberbox('setValue', rate[2]);
                         $('.easyui-numberbox.systemInput').eq(4).numberbox('setValue', rate[3]);

                         debugger;*/
                       $("#loadData").form("load",data.Data);
                        that.slidePriceBlock(rate);
						
						$('.protocolName').val(data.Data.AgreementName || '');
						$('.protocolContent').val(data.Data.Agreement || '');
						if(!!data.Data.AgreementName){
							$('.isEntering').text('已录入');
						}
						if(!!data.Data.Id){
							$('#setProfitArea').show();
							$('#slideimgBox').show();
							$('.priceContentzero').hide();
							//三级分销利润提成体系
            				that.setProfitSystem();	
						}
						$.util.isLoading(true);
                    } else {
                        alert(data.Message);
                    }
                }
            })
        },
        //将用户修改设置的商品定价规则传送给后台
        sendPrice: function (rate) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'SuperRetailTrader.SuperRetailTraderConfig.SetSuperRetailTraderConfig',
					'Id':that.customerValId,
                    'SkuCommission': rate[1],
                    'DistributionProfit': rate[2],
                    'CustomerProfit': rate[3],
                    'Cost': rate[0]
                 },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
						that.setPriceRule();
						//分销定价设置之后同时重置下面的三级分销提成体系 数据
						//that.setProfitSystem();
						//显示层
						$('#setProfitArea').show();
						//$.messager.alert('提示', '分销商品定价成功');
						//关闭loadingGif提示框
						setTimeout(function(){
							$('#winmessage3').window('close');
						},1000);
                    } else {
                        alert(data.Message);
                    }
                }
            })
        },
		//设置协议后台
        sendProtocol: function () {
            var that = this,
				protocolName = $('.protocolName').val(),
				protocolContent = $('.protocolContent').val(),
				$protocolHintBox = $('#protocolHintBox');
			if(protocolName==''){
				$protocolHintBox.text('协议名称不能为空');
				setTimeout(function(){
					$protocolHintBox.text('');
				},1200);
				return;
			}else if(protocolContent==''){
				$protocolHintBox.text('协议内容不能为空');
				setTimeout(function(){
					$protocolHintBox.text('');
				},1200);
				return;
			}
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    'action': 'SuperRetailTrader.SuperRetailTraderConfig.SetSuperRetailTraderConfig',
					'Id':that.customerValId,
					'IsFlag':true, //修改协议
                    'AgreementName': protocolName,//协议名称
                    'Agreement': protocolContent //协议内容
                 },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
						$('#winmessage').window('close');
						$('.isEntering').text('已录入');
						$.messager.alert('提示', '分销商协议保存成功！');
                    } else {
						$.messager.alert('提示', data.Message);
                    }
                }
            })
        },
        //根据商品定价规则数据。修改滑动色块等
        slidePriceBlock: function (rate) {
            //根据传入或设置的价格比例。设置色块
            /**************  设置规则  start  **************/
                //规则1：四个色块总宽度和(780px)固定。根据商户设置的比例确定色块具体宽度
                //规则2：色块宽度最小宽度为52。如规则1计算出某色块宽度为30px,则将该色块宽度设为52px。总宽度不变，最后一个色块减少 （52-30）px宽度
                //规则3：色块遮盖。如规则1计算出某色块宽度a（a<116）。则后面一个色框向该色块遮盖22px。
            /**************  设置规则  end  **************/
            //设置色块
                /*色块一*/
            var widthOne = rate[1] * 7.6, widthTwo = rate[2] * 7.6, widthThree = rate[3] * 7.6, widthFour = rate[0] * 7.6;
            var last = 0;      //存储由于前三个色块遮挡或补齐52px  造成的宽度差
			$('.priceContentone>div>span').text('商品销售佣金比例');
			$('.priceContenttwo>div>span').text('分销商分润比例');
			$('.priceContentthree>div>span').text('商家利润比例');
            if (widthOne > 116) {
                $('.priceContentone').css('width', widthOne+'px');
            } else if ( widthOne > 52) {
                $('.priceContentone').css('width', widthOne + 'px');
                $('.priceContenttwo').css('margin-left', '-13px');
                last = last + 13;
            } else {
                $('.priceContentone').css('width', '52px');
                $('.priceContenttwo').css('margin-left', '-13px');
                last = last + 13 + widthOne - 52;
				$('.priceContentone>div>span').text('商品');
            }
                /*色块二*/
            if (widthTwo > 116) {
                $('.priceContenttwo').css('width', widthTwo + 'px');
            } else if (widthTwo > 52) {
                $('.priceContenttwo').css('width', widthTwo + 'px');
                $('.priceContentthree').css('margin-left', '-13px');
                last = last + 13;
            } else {
                $('.priceContenttwo').css('width', '52px');
                $('.priceContentthree').css('margin-left', '-13px');
                last = last + 13 + widthTwo - 52;
				$('.priceContenttwo>div>span').text('分销');
            }
                /*色块三*/
            if (widthThree > 116) {
                $('.priceContentthree').css('width', widthThree + 'px');
            } else if (widthThree > 52) {
                $('.priceContentthree').css('width', widthThree + 'px');
                $('.priceContentfour').css('margin-left', '-13px');
                last = last + 13;
            } else {
                $('.priceContentthree').css('width', '52px');
                $('.priceContentfour').css('margin-left', '-13px');
                last = last + 13 + widthThree - 52;
				$('.priceContentthree>div>span').text('利润');
            }
            /*色块四*/
			
            //$('.priceContentfour').css('width', widthFour + last + 'px');
			if (widthFour < 52) {
				var averageLast = Math.ceil((52 - (widthFour + last))/3),
					oneWidth = $('.priceContentone').width(),
					twoWidth = $('.priceContenttwo').width(),
					threeWidth = $('.priceContentthree').width(),
					oneW = oneWidth - averageLast,
					twoW = twoWidth - averageLast,
					threeW = threeWidth - averageLast;
				$('.priceContentone').css('width',  oneW + 'px');
				$('.priceContenttwo').css('width', twoW + 'px');
				$('.priceContentthree').css('width', threeW + 'px');
                $('.priceContentfour').css('width', 52 + 'px');
				$('.priceContentfour>div>span').text('成本');
            }else{
				$('.priceContentfour').css('width', widthFour + last + 'px');
				$('.priceContentfour>div>span').text('分销商品成本比例');
			}
			
			
			
			
            //设置色块下方数字
            $('.contentText.colorOrange').find('span').text(parseFloat(rate[1]).toFixed(1) + '%');
            $('.contentText.colorred').find('span').text(parseFloat(rate[2]).toFixed(1) + '%');
            $('.contentText.colorGreen').find('span').text(parseFloat(rate[3]).toFixed(1) + '%');
            $('.contentText.colorblue').find('span').text(parseFloat(rate[0]).toFixed(1) + '%');
            //设置色块上方提示框
            $('#tiptext0ne').text(parseFloat(rate[1]).toFixed(1)+ '%');
            $('#tiptextTwo').text(parseFloat(rate[2]).toFixed(1) + '%');
            $('#tiptextThree').text(parseFloat(rate[3]).toFixed(1) + '%');
        }
    };
    page.init();
})