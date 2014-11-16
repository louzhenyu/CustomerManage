

var Event = {	
		EventList:function(){
		
			var jsonarr = {'action':"getSchoolEventList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'eventId':"65461E02F2AD41088EEAEB0B13B9D152","dataFrom":getParam("dataFrom")}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						var odi = o.content.eventLeven1List;
						if(odi.length > 0 && odi[0].eventLeven2List.length > 0){
						var isBM = odi[0].eventLeven2List[0].overCount;
						
							o.content.overCount = isBM;
						}
				
						var tpl = _.template($("#EventScript").html(),o.content)
						$("#ConnextID").html(tpl);
						$("#userName").val(o.content.userName);
						$("#userClass").val(o.content.userClass);
						$("#userPhone").val(o.content.userPhone);
						$("#userEmail").val(o.content.userEmail);
					}else{
						alert(o.description);	
					}
				}
			})
		},
		ClickCheckBox:function(o){
			var getRel = $(o).find(".checkBox").attr("rel");
			if(getRel =="3"){
				return false;	
			}
		
			if(getRel == "1"){
					 $(o).find(".checkBox").attr("rel","0");
					 $(o).find(".checkBox").find("img").attr("src","images/icn3.jpg");
					 $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())-1);
			}else{
				 $(o).find(".checkBox").attr("rel","1");
					 $(o).find(".checkBox").find("img").attr("src","images/icn2.jpg");
					 $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())+1);
			}
		},
		ClickRudioBox:function(o,id){
			var getRel = $(o).find(".checkBox").attr("rel");
			if(getRel =="3"){
				return false;	
			}
			var i = 0 ; 
			$(".AUU_"+id).each(function() {
				if($(this).attr("rel") == "1"){
					i++;	
				}
               	$(this).attr("rel","0");
				$(this).find("img").attr("src","images/icn9.jpg");
            });
			if(getRel == "1"){
					 $(o).find(".checkBox").attr("rel","0");
					 $(o).find(".checkBox").find("img").attr("src","images/icn9.jpg");
					 $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())-i);
			}else{
					 $(o).find(".checkBox").attr("rel","1");
					 $(o).find(".checkBox").find("img").attr("src","images/icn7.jpg");
					 $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())+1-i);
			}	
		},
		SfSelect:function(){
			var i = 0 ; 
			var eventList = [];
			$(".checkBox").each(function() {
                if($(this).attr("rel") == "1"){
					
					i++;
					eventList.push({"eventId":$(this).attr("eventId")});
				}
            });	
			if(i ==0){
				var oString = '';
				if($(".Lang").attr("lang") == "zh"){
					oString  = "Select at least one";
					}else{
					oString  = "至少选择一项";	
					}
				alert(oString);
				return false;	
			}
			return eventList;
		},
		Dis:function(){
			var getWinw = $(window).width(), getDomH = $(document).height(), 	getWinH= $(window).height();
			$("#appbg").css("height",getDomH);
			$("#appbg").css("width",getWinw);
			var getDocumentSH = $(document).scrollTop();
			var t = getDocumentSH + ((getWinH-380)/2), l = (getWinw -280) / 2;
			$("#appbgBox").css({"left":l, "top":t});
			$("#appbg,#appbgBox").show();
		},
		SelectDiv:function(){
			if(this.SfSelect() != false){
				this.Dis();
			}	
		},
		CacelBox:function(){
			$("#appbg,#appbgBox").hide();	
		},
		SureSubmit:function(){
			var userName = $.trim($("#userName").val()),userClass = $.trim($("#userClass").val()),userPhone = $.trim($("#userPhone").val()),userEmail = $.trim($("#userEmail").val()),company = $.trim($("#company").val()),position = $.trim($("#position").val());
				var oString = '';
			if(userName=="" || userClass=="" ||userPhone ==""||userEmail ==""||company ==""||position =="" ){
			
				if($(".Lang").attr("lang") == "zh"){
					oString  = "Please enter the required fields";
					}else{
					oString  = "请输入必填项";	
					}
					alert(oString);
				return false;	
			}
			if($(".Lang").attr("lang") == "zh"){
				var  regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
				 if( !regAee.test(userPhone)){
						if($(".Lang").attr("lang") == "zh"){
							oString  = "Please enter correct phone!";
							}else{
							oString  = "请正确输入手机号码！";	
							}
						alert(oString);
						return false;  
					}
			}
			var eventObj = this.SfSelect();
			if(eventObj == false){
				return false; 
			}
			var jsonarr = {'action':"setSchoolEventList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'userName':userName,userClass:userClass,userPhone:userPhone,userEmail:userEmail,company:company,position:position,eventList:eventObj,dataFrom:getParam("dataFrom")}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					
				
						Win.Loading("CLOSE"); 
						
						alert(o.description);	
						Event.CacelBox();
				}
			})
		},
		Lang:function(o){
			var getlang = $(o).attr("lang");
			
			if(getlang == "en"){
				$(o).text("中文");
				$(".zh").hide();
				$(".en").show();
				 $(o).attr("lang","zh");
			}else{
				$(o).text("English");
				$(".en").hide();
				$(".zh").show();
				 $(o).attr("lang","en");
			}
		}
}