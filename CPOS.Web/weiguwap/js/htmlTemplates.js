var HtmlTemplates = {
		ServiceItemsData:function(data,curid){
			var HtmlString = '';
			var dataPP = data.DataItems;
			if(dataPP.length > 0 ){
				for(var i = 0 ; i< dataPP.length; i++){
						if(dataPP[i]["ServiceItemId"]==curid){
						HtmlString+= '<dd  class="ActiveDateDD" onclick="JSEvent.SerViceLi(this,event)" service-data-id="'+dataPP[i]["ServiceItemId"]+'">'+dataPP[i]["ItemTitle"]+'</dd>';	
						}else{
						HtmlString+= '<dd  service-data-id="'+dataPP[i]["ServiceItemId"]+'" onclick="JSEvent.SerViceLi(this,event)" >'+dataPP[i]["ItemTitle"]+'</dd>';	
						}
					}	
			}
			
			return HtmlString;			
		},
		DateItemsData:function(data,curid){
			var HtmlString = '';
			var dataPP = data.DataItems;
			if(dataPP.length > 0 ){
				for(var i = 0 ; i< dataPP.length; i++){
					var iStatus ="";
					if(dataPP[i]["Status"] == "1"){
							iStatus = "<span class='yiman'>满</span>";
						}
						if(dataPP[i]["DateItemId"]==curid){
						HtmlString+= '<dd  class="ActiveDateDD"  onclick="JSEvent.DateLi(this,event)" status="'+dataPP[i]["Status"]+'" date-data-id="'+dataPP[i]["DateItemId"]+'">'+dataPP[i]["ItemTitle"]+'&nbsp;&nbsp;'+iStatus+'</dd>';	
						}else{
						HtmlString+= '<dd  status="'+dataPP[i]["Status"]+'"  onclick="JSEvent.DateLi(this,event)" date-data-id="'+dataPP[i]["DateItemId"]+'">'+dataPP[i]["ItemTitle"]+'&nbsp;&nbsp;'+iStatus+'</dd>';	
						}
					}	
			}
			
			return HtmlString;			
		},
		HourItemsData:function(data){
			var HtmlString = '';
			var dataPP = data.DataItems;
			if(dataPP.length > 0 ){
				for(var i = 0 ; i< dataPP.length; i++){
					var iStatus ="";
					if(dataPP[i]["Status"] == "1"){
							iStatus = "<span class='yiman'>满</span>";
						}
					
						HtmlString+= '<li  status="'+dataPP[i]["Status"]+'"  onclick="JSEvent.HourLi(this,event)" hour-data-id="'+dataPP[i]["HourItemId"]+'"><i>'+dataPP[i]["ItemTitle"]+'</i>&nbsp;&nbsp;'+iStatus+'<span class="icn_skin Icn6"></span</li>';	

					}	
			}
			
			return HtmlString;			
		},
		TimeItemsData:function(data){
			var HtmlString = '';
			var dataPP = data.DataItems;
			if(dataPP.length > 0 ){
				for(var i = 0 ; i< dataPP.length; i++){
					var iStatus ="";
					if(dataPP[i]["Status"] != "1"){
							//iStatus = "<br /><span class='yiman'>已满</span>";
						
					
						HtmlString+= '<li  status="'+dataPP[i]["Status"]+'"  onclick="Reserve.TimeLi(\''+dataPP[i]["TimeItemId"]+'\',event,\'order\')" time-data-id="'+dataPP[i]["TimeItemId"]+'" id="timeli_'+dataPP[i]["TimeItemId"]+'">'+dataPP[i]["ItemTitle"]+'</li>';	
						}
					}	
			}
			
			return HtmlString;			
		},
		ReservationSData:function(data,isLogin){
			var HtmlString = '';
			var dataPP = data.DataItems;
			if(isLogin==0){
			 HtmlString = '<div class="TipTxt">还没登录,还没有预定的哦！</div><div style="min-height:180px;">';
				}else{
				if(dataPP.length > 0 ){
					 HtmlString = '<div class="TipTxt">您共有'+dataPP.length+'项预定</div><div style="min-height:180px;">';
					for(var i = 0 ; i< dataPP.length; i++){
						HtmlString +='<div class="HasReserveList" id="HasReserveList_'+dataPP[i]['ReservationId']+'"><div class="HasReserveLeft"><p class="s1">'+dataPP[i]['DateTitle']+'&nbsp;&nbsp;'+dataPP[i]['TimeTitle']+'&nbsp;&nbsp;'+dataPP[i]['Position']+'</p><p class="s2">'+dataPP[i]['ServiceTitle']+'</p></div><div class="HasReserveRight"><input type="button"  value="取消" onclick="Reserve.ActionCanel('+dataPP[i]['ReservationId']+')" class="cacelReserve" /></div></div>';
						}	
				
				}else{
					 HtmlString = '<div class="TipTxt">您暂时没有任何预定</div><div style="min-height:180px;">';	
				}
			}
			
				HtmlString+= "</div>"
			return HtmlString;		
		},
		SubmitOrder:function(data,txt,id){
			var ServiceItemIdText =$("#ReSeverSelect_1").find("i").text();
			var DateItemText =$("#ReSeverSelect_2").find("i").text();
	
			var Istring ='';
			Istring = DateItemText+ "&nbsp;"+txt+"&nbsp;的"+ServiceItemIdText; 
			var HtmlString = '<div class="PopDetail"><table width="100%" border="0"><tr><td width="30%" >会员号：</td><td>'+data.vip+'</td></tr><tr><td  >姓名：</td><td>'+data.username+'</td></tr><tr><td  >车牌：</td><td>'+data.bus+'</td></tr><tr><td  >余额：</td><td><span class="frdisplay"><a href="javascript:void(0)">在线充值</a></span>'+data.balance+'元</td></tr></table></div><div class="SureServer"><div class="SureServerLeft"><span style=" color:#ff8c00; font-size:16px;">您已经选择预约:</span><br />'+Istring+'服务，点击右侧完成预约</div><div class="SureServeRight"> <strong style=" color:#ff8c00; font-size:16px;">会员价:￥'+data.vipPrice+'元</strong><br /><a href="javascript:void(0)" onclick="Reserve.CSubmitOrder(\''+id+'\',event)" class="SureServerBtn"><span style="margin-left:-20px;">确定</span><i class="icn_skin IcnSure"></i></a> </div></div>';
			return HtmlString;
		},
		LoginTmp:function(actionType,id){
				var HtmlString = '<div class="PopLogin"><div class="PopLogintitle">在线预约仅向本店会员开放，请输入您的会员卡号及手机号码</div><div class="InputStyleA"><input type="text" " onfocus="FocusFun(this,\'会员卡号\',1)"  onblur="FocusFun(this,\'会员卡号\',0)" id="Vip" value="会员卡号" class="InputStyleText"></div><div class="InputStyleA"><input type="text" id="Phone" value="手机号码" onfocus="FocusFun(this,\'手机号码\',1)"  onblur="FocusFun(this,\'手机号码\',0)" class="InputStyleText"></div><div class="NowRe" style="width:100%"><a href="javascript:void(0)" onclick="AppSet.LoginSubmit(\''+actionType+'\',\''+id+'\')" class="NowReserveBtn">确定</a></div><div style="padding:20px 0; color:#828282; line-height:22px;"><p>如需开通会员卡，请来店办理</p><p>如需帮助，请拨打门店客户电话：<br>021-64454454<br></p></div></div>';
			return HtmlString;	
		}
}