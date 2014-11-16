var HtmlTemplates = {
		getIntegrationExchange:function(data){
			var HtmlString = '';
		
			var dataPP = data.items;
			HtmlString = '<table width="100%" class="VipJfenBoxRdTB"><tr class="Dtitle"><td width="55%" align="center" height="40">品名	</td><td align="center" width="25%">所需积分</td><td align="center">数量</td></tr>';
			if(dataPP.length > 0 ){
				var getFen = $("#VipCardJf").text();
				for(var i = 0 ; i< dataPP.length; i++){
						HtmlString+= '<tr ><td width="55%" align="left" height="40" style="padding-left:15px;">'+dataPP[i].itemName+'</td><td align="center" width="25%"><span style="color:#cc0000;"><span>'+parseInt(dataPP[i].integration)+'</span>分</span></td><td align="center"><select style="width:100%;" onchange="VipCard.SelectFun(\''+dataPP[i].itemId+'\')" id="SelectID_'+dataPP[i].itemId+'" itemId="'+dataPP[i].itemId+'\" default="'+parseInt(dataPP[i].integration)+'" selectnum="0"><option value="0">0</option>';
						var dnum = Math.floor(getFen/parseInt(dataPP[i].integration));
						for(var k=1; k<=dnum; k++){
							HtmlString += '<option value="'+k+'">'+k+'</option>';	
						}
						
						HtmlString+= '</select></td></tr>';
					}	
				}
			HtmlString += '</table><div class="jfddeo"><input type="button" value="兑换" class="RuiHuanBtn" onclick="VipCard.setExchanges()" />积分余额 <span style="color:#cc0000;"><span id="Jfenoo">'+getFen+'</span>分</span></div>';
			return HtmlString;			
		},
		getOrders:function(data,isfirst,pagedd){
				var HtmlString = '';
				var dataPP = data.Orders;
				if(isfirst == "1"){
				var getWindowHeight1 = $(window).height();
			
				getWindowHeight = getWindowHeight1 - 100; 
				if(getWindowHeight < 200){
					getWindowHeight  = 200; 
				}
		
				if(pagedd== "Event"){
					getWindowHeight  = getWindowHeight1 -40
					
				}
				
				HtmlString += '<div id="ScollDd" style="overflow:auto;height:'+getWindowHeight+'px"><div style="position:relative; padding:12px 0;" id="listId"><div id="UListId">';
				}
				if(dataPP.length > 0 ){
					for(var i = 0 ; i< dataPP.length; i++){
						HtmlString += '<div class="VipJfenBoxRd1"><table width="100%" class="VipJfenBoxRdTB"><tr class="Dtitle"><td width="70%" align="left" height="40">'+dataPP[i].createTime+'</td><td align="right">'+dataPP[i].orderStatus+'</td></tr>';
						var getItemLength = dataPP[i].OrderDetails;
						if(getItemLength.length > 0 ){
							for(var j = 0; j< getItemLength.length; j++){
								HtmlString += '<tr><td width="70%" align="left" height="40" style="padding-left:15px;">'+getItemLength[j].itemName+'</td><td align="center"><span style="color:#cc0000;">'+parseInt(getItemLength[j].stdPrice)+'</span></td></tr>';
							}
						}else{
							HtmlString += '<tr><td width="70%" align="left" height="40" style="padding-left:15px;">没有数据</td><td align="center">&nbsp;</td></tr>';
						}
						HtmlString += '</table><div class="jfddeo1"><input type="button" value="付款" class="RuiHuanBtn1">总额： <span style="color:#cc0000;">'+dataPP[i].totalAmount+'元</span><br>已付：<span style="color:#cc0000;"> '+dataPP[i].paidAmount+'元</span></div></div>'	
					}
				}else{
						HtmlString += "<div style='padding:10px; text-align:center'>您还没有订购！</div>";
						
						
				}
					if(isfirst == "1"){
					HtmlString += '</div><div id="pullUp"><img src="images/382.gif" width="32" height="32" align="absmiddle" /><span class="pullUpLabel"></span></div></div></div>';
					}
			return HtmlString;
		},
		getEventInfo:function(data){
			var HtmlString = "", statu = "";
			if(data.isHasApply == "1"){
				statu = "活动正在火热报名中";	
			}
			if(data.isHasApply == "0" && data.isOverEvent=="1"){
				statu = "活动已经结束";		
				}
			if(data.isHasApply == "0" && data.isOverEvent=="0"){
				statu = "活动还未开始报名";		
				}
			var titleList = ["活动状态","活动时间","活动地址","活动简介","城市"],titletxtArr = [statu,data.eventTime,data.address,data.Description,data.cityId];
			 HtmlString += '<div class="LZWineDImage"><div style="overflow:hidden; height:200px;"><img src="'+data.imageURL+'" height="200"></div></div><div class="LZWineDConnext"><h1>'+data.title+'</h1>';
			 for(var i=0; i<titleList.length; i++){
				 	HtmlString+='<div class="WineDiv1"><h2><span class="enBi">●</span>'+titleList[i]+'</h2><p>'+titletxtArr[i]+'</p></div>';
				 }
				 HtmlString+= '</div><span class="blank" style="height:50px;"></span>';
			return HtmlString;
				 
		},
		getSkus:function(data){
			var HtmlString = "";
			var coData = data.Skus;
			if(coData.length > 0 ){
				for(var i=0; i<coData.length; i++ ){
					HtmlString +='<a href="wineDetail.html?skuId='+coData[i].skuId+'&EventId='+getParam("EventId")+'&itemId='+coData[i].itemId+'&back=1"><div class="WineLi"><span class="incSkin EventIcn0"></span><table width="100%"><tr><td width="110"><div class="WineLiLeft"><img src="'+coData[i].imageURL+'" width="90" /></div></td><td><div class="WineLiRight"><h2>'+coData[i].itemName+'</h2><p>规格：'+coData[i].gg+' </p><p>酒度：'+coData[i].degree+'</p><p>基酒年份：'+coData[i].baseWineYear+' </p><p>窖池窖龄：'+coData[i].agePitPits+'</p></div></td></tr></table></div></a>';
						
				}	
			}else{
				HtmlString = "<div style='padding:20px;'>数据不存在！</div>";
			}
			return HtmlString; 
		},
		getSkuDetail:function(data){
			var HtmlString = "", statu = "";
			
			var titleList = ["零售价","规格","酒度","基酒年份","窖池窖龄","介绍"],titletxtArr = [data.salesPrice,data.gg,data.degree,data.baseWineYear,data.agePitPits,data.itemRemark];
			 var dataImageList = data.imageList;
			 if(dataImageList == null){
					 dataImageList  = [];
				}
			 var Windth = dataImageList.length* 280 + 280;
			 HtmlString += '<div class="LZWineDImage"><div id="wrapper"><div id="scroller" style="width:'+Windth+'px"><ul>';
			 	 HtmlString += '<li><img src="'+data.imageURL+'" ></li>';
				
				 if(dataImageList.length > 0 ){
						for(var i =0; i<dataImageList.length; i++ ){
							HtmlString += '<li><img src="'+dataImageList[i].imageURL+'" ></li>';
						} 
					}
			 
			HtmlString += ' </ul></div></div><dl id="indicator">';
				 HtmlString += '<dd class="active">1</dd>';
				 if(dataImageList.length > 0 ){
					 for(var i=0; i< dataImageList.length; i++){
							 HtmlString += '<dd>'+(i+2)+'</dd>';
						 }
				 }
			HtmlString += '</dl></div><div class="LZWineDConnext"><h1>'+data.itemName+'</h1>';
			 for(var i=0; i<titleList.length; i++){
				 	if(i == 0){
					HtmlString+='<div class="WineDiv1"><h2><span class="enBi">●</span>'+titleList[i]+'</h2><p>'+titletxtArr[i]+' '+data.unit+'</p></div>';	
					}else{
				 	HtmlString+='<div class="WineDiv1"><h2><span class="enBi">●</span>'+titleList[i]+'</h2><p>'+titletxtArr[i]+'</p></div>';
					}
				 }
				 HtmlString+= '</div><span class="blank" style="height:50px;"></span>';
			return HtmlString;
				 
		},
		setOrderInfo:function(data){
			var HtmlString = '<div class="cTipDiv"><div class="cTipTitle">填写以下信息，完成购买：</div><div class="FipDiv" onclick="GOFocus(this,\'name\')"><div class="cTipDivName">姓名</div><div class="cTipDivNameInput"><input type="text" value="" id="name"  /></div></div><div class="FipDiv" onclick="GOFocus(this,\'phone\')"><div class="cTipDivName">手机号</div><div class="cTipDivNameInput"><input type="text" value="" id="phone" /></div></div><div class="FipDiv" onclick="GOFocus(this,\'tableNumber\')"><div class="cTipDivName">桌号</div><div class="cTipDivNameInput"><input type="text" value="" id="tableNumber" /></div></div><div class="FipDiv" onclick="GOFocus(this,\'ADtextarea\')"><div class="cTipDivName1"><strong>个性化信息</strong><span style="font-size:12px;">（15字以内）</span></div><div class="cTipDivNameInput1"><textarea class="ADtextarea" id="ADtextarea"></textarea></div></div><div class="FipDiv" onclick="LZEvent.LZWineSale()"><span class="incSkin EventIcn0 EventIcnUp"></span><div class="cTipDivName2" id="cTipDivName2">选择酒的品类</div></div><div style="clear:both; padding-top:20px;">  <input type="button"  value="确定" class="RuiHuanBtn1" onClick="LZEvent.setOrderInfo()" style="width:100%; margin:0;" ></div></div>';
			return HtmlString;
		},
		WineType:function(data){
			var HtmlString = "";
			var coData = data.Skus;
			if(coData.length > 0 ){
				for(var i=0; i<coData.length; i++ ){
					var getStringd = coData[i].price+"/"+coData[i].degree+"/"+coData[i].gg+"/"+coData[i].baseWineYear+"/"+coData[i].agePitPits;
					HtmlString +='<div class="CddTip" onclick="Animi(this)"><table width="100%"><tr><td width="30" align="center"><input type="radio" name="WineType" class="BBd" value="'+coData[i].skuId+'" pli="'+getStringd+'" price="'+coData[i].price+'" /></td><td>价格：'+coData[i].price+'<br><table width="100%"><tr><td width="25%">酒度</td><td  width="25%">'+coData[i].degree+'</td><td  width="25%">规格</td><td>'+coData[i].gg+'</td></tr><tr><td>基酒年份</td><td>'+coData[i].baseWineYear+'</td><td>窖池窖龄</td><td>'+coData[i].agePitPits+'</td></tr></table></td></tr></table></div>';
				}
				HtmlString += '<div style="clear:both; padding-top:20px;"><input type="button" style="width:100%; margin:0;"  class="RuiHuanBtn1" onclick="sureSelecType()" value="选择"></div>';
			}
			return HtmlString;
		}
}