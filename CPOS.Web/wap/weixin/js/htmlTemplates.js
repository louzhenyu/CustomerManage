// JavaScript Document
var Templates ={
		EventDetailTemp:function(ConnextObj){
				var inAppInput = '';
				switch(ConnextObj.buttonType){
						case "1" : inAppInput = '<input type="button"  class="btnStylecc" value="报名" onclick="Event.ActionApply()" id="CheckInSuccess" apptype="Apply" />';
						break;
						case "2" : inAppInput = '<input type="button"  class="btnStylecc" value="签到" onclick="Event.AppCheckIn.getPosition()" id="CheckInSuccess" apptype="CheckIn" /><input type="button"  class="btnStyleccgray" value="已签到" id="CheckInSuccessBtn" style="display:none"  />';
						break;
						case "4" : inAppInput = '<input type="button"  class="btnStyleccgray" value="已签到"  />';
						break;
						case "3" : inAppInput = '<input type="button"  class="btnStylecc" value="缴费" id="CheckInSuccess" />';
						break;
						default: ; 
					
					}
					
				if(ConnextObj.applyStatus == 1){
						inAppInput = '<input type="button"  class="btnStylecc" value="已报名待审批" />';
					}
				var iString = '<section class="AppBanner" style="text-align:center; overflow:hidden; position:relative;"><img src="'+ConnextObj.imageUrl+'" width="100%"  /><div class="WAppBannerTit"><div style="padding-right:12px">已报名：'+ConnextObj.applyCount+'<br>已签到: '+ConnextObj.checkinCount+'</div>'+inAppInput+'</div></section><section class="DetailAll"><h2>'+ConnextObj.title+'</h2><div class="DetailLi"><h3><span>时间</span></h3><p>'+ConnextObj.timeStr+'</p></div><div class="DetailLi"><h3><span>地点</span></h3><p>'+ConnextObj.address+'</p></div><div class="DetailLi"><h3><span>活动发起</span></h3><p>'+ConnextObj.organizer+'</p></div><div class="DetailLi"><h3><span>联系人</span></h3><p>'+ConnextObj.contact+'</p></div><div class="DetailLi"><h3><span>介绍</span></h3><p>'+ConnextObj.description+'</p></div></section>';
				return iString; 
			},
		EventDetailApply:function(ConnextObj, IsAction){
			// 默认Event 
			//IsAction = 1 是申请加入
			var SavaQuestionList = ConnextObj.questions;
			var QuestionString = new String();
			var RequiredTxt = '';
			//	questionType		类型：1:文本；2：文本域；3：单选题 4,多选题
			if(SavaQuestionList.length > 0){
				for(var i=0; i< SavaQuestionList.length;  i++){
				
					//单选题在这里处理
					if(SavaQuestionList[i].questionType =="3"){	
							
						if(SavaQuestionList[i].isRequired =="1"){
								// 必须项的标记
								 RequiredTxt = '<span class="xingred">*</span>';
							}else{
								RequiredTxt ='';	
							}
						
						
						var SaveQuestionOpation1 = SavaQuestionList[i].options, SaveQuestionOpation1String =new String();
						if(SaveQuestionOpation1.length > 0){
							for(var j=0; j<SaveQuestionOpation1.length; j++){
								//排列问题答案
								var isSelectedTxt = "";

								if(SaveQuestionOpation1[j].isSelected == "1"){
										//判断是否选中
									 isSelectedTxt = " checked=true";	
									}
								SaveQuestionOpation1String += "<label for='qt_"+SavaQuestionList[i].questionId+"_"+SaveQuestionOpation1[j].optionId+"'><input type='radio' name='Question"+SavaQuestionList[i].questionId+"' id='qt_"+SavaQuestionList[i].questionId+"_"+SaveQuestionOpation1[j].optionId+"' value='"+SaveQuestionOpation1[j].optionId+"' "+isSelectedTxt+" >&nbsp;&nbsp;"+SaveQuestionOpation1[j].optionText+"&nbsp;&nbsp;&nbsp;</label>";
							}
							
						QuestionString += '<div class="inputAndSelect RadioAbc" id="QuestionAbc_'+SavaQuestionList[i].questionId+'" value="'+SavaQuestionList[i].questionId+'" type="'+SavaQuestionList[i].questionType+'" isRequired="'+SavaQuestionList[i].isRequired+'">'+SavaQuestionList[i].questionText+'&nbsp;'+RequiredTxt+'<br />'+SaveQuestionOpation1String+'</div>';
						}
					}
					//单选题在这里结束处理
					
					//文本在这里处理
					if(SavaQuestionList[i].questionType =="1"){	
						if(SavaQuestionList[i].isRequired =="1"){
								// 必须项的标记
								 RequiredTxt = '<span class="xingred">*</span>';
							}else{
								RequiredTxt ='';	
							}
						QuestionString += '<div class="inputAndSelect InputAbc" id="QuestionAbc_'+SavaQuestionList[i].questionId+'" value="'+SavaQuestionList[i].questionId+'" isRequired="'+SavaQuestionList[i].isRequired+'" type="'+SavaQuestionList[i].questionType+'">'+SavaQuestionList[i].questionText+'&nbsp;'+RequiredTxt+'<br /><input type="text" class="AlumniInputstyle" value="" id="qt_'+SavaQuestionList[i].questionId+'" ></div>';
					}
					//文本在这里结束
					//文本域在这里处理
					if(SavaQuestionList[i].questionType =="2"){	
						if(SavaQuestionList[i].isRequired =="1"){
								// 必须项的标记
								 RequiredTxt = '<span class="xingred">*</span>';
							}else{
								RequiredTxt ='';	
							}
						QuestionString += '<div class="inputAndSelect InputAbc" id="QuestionAbc_'+SavaQuestionList[i].questionId+'" value="'+SavaQuestionList[i].questionId+'" isRequired="'+SavaQuestionList[i].isRequired+'" type="'+SavaQuestionList[i].questionType+'">'+SavaQuestionList[i].questionText+'&nbsp;'+RequiredTxt+'<br /><textarea type="text" class="AlumniInputstyle" value="" id="qt_'+SavaQuestionList[i].questionId+'" style="height:70px" ></textarea></div>';
					}
					//文本域在这里结束
					
					
					//多选项在这里开始
					if(SavaQuestionList[i].questionType =="4"){	
						if(SavaQuestionList[i].isRequired =="1"){
								// 必须项的标记
								 RequiredTxt = '<span class="xingred">*</span>';
							}else{
								RequiredTxt ='';	
							}
						var SaveQuestionOpation3 = SavaQuestionList[i].options, SaveQuestionOpation3String =new String();					
			
						if(SaveQuestionOpation3.length > 0 ){
								for(var k=0; k<SaveQuestionOpation3.length; k++){
								//排列问题答案
								var isSelectedTxt3 = "";

								if(SaveQuestionOpation3[k].isSelected == "1"){
										//判断是否选中
										 isSelectedTxt3 = " checked=true";	
									}else{
										isSelectedTxt3 = "";	
									}
								SaveQuestionOpation3String += "<label for='qt_"+SavaQuestionList[i].questionId+"_"+SaveQuestionOpation3[k].optionId+"'><input type='checkbox' name='Question"+SavaQuestionList[i].questionId+"' value='"+SaveQuestionOpation3[k].optionId+"' "+isSelectedTxt3+"  id='qt_"+SavaQuestionList[i].questionId+"_"+SaveQuestionOpation3[k].optionId+"' >&nbsp;&nbsp;"+SaveQuestionOpation3[k].optionText+"&nbsp;&nbsp;&nbsp;</label>";
								}	
							}
						QuestionString += '<div type="'+SavaQuestionList[i].questionType+'" class="inputAndSelect InputAbc" NumMax="'+SavaQuestionList[i].maxSelected+'" NumMin="'+SavaQuestionList[i].minSelected+'" id="QuestionAbc_'+SavaQuestionList[i].questionId+'" value="'+SavaQuestionList[i].questionId+'" isRequired="'+SavaQuestionList[i].isRequired+'">'+SavaQuestionList[i].questionText+'&nbsp;'+RequiredTxt+'<br />'+SaveQuestionOpation3String+'</div>';
					}
					//多选项在这里结束
					
					
				}
			}
			if(IsAction == "1"){
				var itSt = '<input type="button" class="inputsubmitStyle " value="申请加入" onClick="Group.ToJoinSubmit()" />';
				}else{
				var itSt = '<input type="button" class="inputsubmitStyle " value="报名" onClick="Event.ApplySubmit()" />';	
				}
			
			var istring = '<div class="inputAndSelect">姓名<span class="xingred">*</span><br /><input type="text" value="'+ConnextObj.userName+'" id="ApplyName" class="AlumniInputstyle" /></div><div class="inputAndSelect">班级<span class="xingred">*</span><br /><div class="AlumniInputstyle" style="line-height:40px;">'+ConnextObj.className+'<input value="'+ConnextObj.className+'" class="AlumniInputstyle" id="ApplyClass" style="display:none;" /></div></div><div class="inputAndSelect">手机号码<span class="xingred">*</span><br /><input type="text" value="'+ConnextObj.mobile+'" id="ApplyMobile"  class="AlumniInputstyle" ></div><div class="inputAndSelect">邮箱地址<span class="xingred">*</span><br /><input type="text" value="'+ConnextObj.email+'" id="ApplyEmail" class="AlumniInputstyle"  ></div><div id="AllQuestionId">'+QuestionString+'</div><div style="line-height:46px;" class="inputAndSelect">'+itSt+'</div>'; 
			
				return istring; 
			},
		EventPrizeTemp:function(ConnextObj,isTabClick){
			var tabArr = ['奖品列表','我的奖品','中奖校友'];
			var tabCode = [0,1,2];
			var istring ="";
			if(isTabClick!=1){
				 istring = '<section class="YaoJiangWarp"><div class="prizeIcn"><div style="width:180px; margin-left:auto; margin-right:auto; padding-top:10px;"><img src="images/prizetell.png" width="180" /></div>';	
				if(ConnextObj.hasPrizeOpen == 1){
					istring +='<div style="color:#797979; font-size:22px; text-align:right;"><input type="button" class="InputButtonYJ" value="点击即可摇奖" onclick="Event.AppCheckIn.getPosition(\'pritze\')" /></div>';
				}
					
				istring +='</div><div class="prizeMenu" type="'+ConnextObj.type+'">';
			
				for(var i=0; i<tabArr.length; i++){
					if(ConnextObj.type != tabCode[i]){
						istring += '<div class="prizeMenuLi"><a href="javascript:void(0)" onclick="PrizeTabFun(\''+tabCode[i]+'\')"  id="prizeMenuApc_'+tabCode[i]+'">'+tabArr[i]+'</a></div>';	
						}else{
						istring += '<div class="prizeMenuLi"><a href="javascript:void(0)" onclick="PrizeTabFun(\''+tabCode[i]+'\')" class="cHover" id="prizeMenuApc_'+tabCode[i]+'">'+tabArr[i]+'</a></div>';
						}
					}
				istring +='</div><div class="prizeDiv"><div class="prizediv_jp">';
				}
				
					if(ConnextObj.type==0){
						var iprizeslist = ConnextObj.prizes;
						if(iprizeslist.length > 0 ){
						for(var j=0; j<iprizeslist.length; j++){
							istring +='<div class="WAppPerson" ><div class="imgDiv"><img src="'+iprizeslist[j].imageUrl+'" width="60" height="60" ></div><div class="WAppPersonRight"><h3><strong>'+iprizeslist[j].prizeName+'</strong></h3><p class="AII">'+iprizeslist[j].prizeDesc+'</p></div></div>';		
							}
						}
					}
					if(ConnextObj.type==1){
						if(ConnextObj.hasUserAccount == 1 ){
							var iprizeslist = ConnextObj.myPrizes;
							if(iprizeslist.length > 0 ){
							for(var j=0; j<iprizeslist.length; j++){
								istring +='<div class="WAppPerson" ><div class="imgDiv"><img src="'+iprizeslist[j].imageUrl+'" width="60" height="60" ></div><div class="WAppPersonRight"><h3><strong>'+iprizeslist[j].prizeName+'</strong></h3><p class="AII">'+iprizeslist[j].prizeDesc+'</p></div></div>';		
								}
							}
						
						}else{
					istring += "<div class='GotoBind'>"+ConnextObj.noBindText+"<a href='javascript:void(0)' onclick='AppSet.Bg(\"AlumniConnext1\",\"Prize\")'>完成绑定</a></div>";	
						}
					
				}
				
				if(ConnextObj.type==2){
						if(ConnextObj.hasUserAccount == 1 ){
							var iprizeslist = ConnextObj.winners;
							if(iprizeslist.length > 0 ){
							for(var j=0; j<iprizeslist.length; j++){
								
								istring +='<div class="WAppPerson"><div class="imgDiv"> <img src="'+iprizeslist[j].imageUrl+'" width="60" height="60"> </div><div class="WAppPersonRight"><h3><strong>'+iprizeslist[j].userName+'</strong>| '+iprizeslist[j].userInfoEx+'</h3><p class="AII">'+iprizeslist[j].userDesc+'</p></div></div>';	
								if(j== iprizeslist.length - 1){
									istring +='<input type="hidden" id="hiddenIsNext" value="'+ConnextObj.isNext+'" isLoading="0" timeStamp="'+iprizeslist[j].timeStamp+'" >';	
									}
								}
							}
						
						}else{
					istring += "<div class='GotoBind'>"+ConnextObj.noBindText+"<a href='javascript:void(0)' onclick='AppSet.Bg(\"AlumniConnext1\",\"Prize\")'>完成绑定</a></div>";	
						}
					
				}
				if(isTabClick!=1){
					istring +='</div></div></section>';
				}
				return istring;
			},
		EventInteractiveTemp:function(ConnextObj,isTabClick){
				var istring = '<section class="YaoJiangWarp"><div class="prizeIcn"><div class="VoteIcnDiv"><img width="150" src="images/voteicn.png"></div>';
				if(ConnextObj.hasPollOpen =="1"){
				 istring += '<div class="VoteTxtDiv" style="">投票中<br /><input type="button"  value="现在参与" class="btnStylecc" style="margin-top:10px;"></div>';
				}else{
				istring += '<div class="VoteTxtDiv" style=""><br /><input type="button"  value="投票已结束" class="btnStyleccgray" style="margin-top:10px;"></div>';		
				}
				 istring += '</div><div class="discussDiv" ><div style="margin-right:110px; width:auto;"><input type="text" class="AlumniInputstyle"  /></div><input type="button"  value="发言" class="InputButtonYJ DisCussBtn" ></div><div class="prizeDiv">';
				 if(ConnextObj.hasUserAccount == 1 ){
					 var iprizeslist = ConnextObj.post;
					 for(var j=0; j<iprizeslist.length; j++){
					 istring += '<div class="WAppPerson" style="min-height:84px"><div class="imgDiv"> <img width="60" height="60" src="'+iprizeslist[j].userImageUrl+'"></div><div class="WAppPersonRight"><h3 style="font-size:14px;"><span class="frdisplay grayDate">'+iprizeslist[j].time+'</span><strong>'+iprizeslist[j].userName+'</strong></h3><p class="AII">'+iprizeslist[j].message+'</p></div></div>';
					 if(j== iprizeslist.length - 1){
								istring +='<input type="hidden" id="hiddenIsNext" value="'+ConnextObj.isNext+'" isLoading="0" timeStamp="'+iprizeslist[j].timeStamp+'" >';	
									}
					 }
				 }
				 istring += '</div></section>';
				return istring;
			},
		UserSearch:function(ConnextObj){
			var iString = '<div class="inputAndSelect"><input type="text" class="AlumniInputstyle" onfocus="FocusFun(this,\'姓名\',1)" onblur="FocusFun(this,\'姓名\',0)" id="SearchUsername" value="姓名"></div><div class="inputAndSelect"><div style="width:100%;"><label class="labelSelect"><select id="SearchIndustry">'
			var sli = ConnextObj.industries; 
			iString += '<option value="">请选择行业</option>';
			if(sli.length > 0 ){
				for(var i =0; i<sli.length; i++){
					
					iString += '<option value="'+sli[i].industryId+'">'+sli[i].cname+'</option>';
				}
			}
			
			iString += '</select></label></div></div><div class="inputAndSelect"><input type="text" class="AlumniInputstyle" value="公司" id="SearchCompany" onfocus="FocusFun(this,\'公司\',1)" onblur="FocusFun(this,\'公司\',0)"></div><div class="inputAndSelect"><input type="text" class="AlumniInputstyle" value="地址" onfocus="FocusFun(this,\'地址\',1)" onblur="FocusFun(this,\'地址\',0)"  id="SearchAdress"></div><div class="inputAndSelect"><input type="button" value="查询" class="inputsubmitStyle" onclick="User.searchResult(\'1\')"></div>';
			return iString; 
		},
		UserSearchResult:function(ConnextObj,type){
			if(type=="1"){
			var iString = '<h2 class="SearchTitle"><a href="javascript:User.ShowSearch()">&lt;&lt;返回校友查询</a></h2>';
			}else{
			var iString = '';	
			}
			var pusers = ConnextObj.users;
			if(pusers.length > 0 ){
				for(var i=0; i<pusers.length; i++){
				 iString += '<div class="WAppPerson"><div class="imgDiv"><div style="width:64px; margin:5px; overflow:hidden; height:64px;"><img src="'+pusers[i].imageUrl+'" width="64"></div></div><div class="WAppPersonRight"><h3><strong>'+pusers[i].userName+'</strong> |  '+pusers[i].userInfoEx+'</h3><p class="AII">'+pusers[i].userDesc+'</p></div></div>';
				}
			}
			return iString; 
		},
		UserNearbyResult:function(ConnextObj,type){
			//if(type=="1"){
			//var iString = '<h2 class="SearchTitle">查询结果：</h2>';
			//}else{
			var iString = '';	
			//}
			var pusers = ConnextObj.users;
			if(pusers.length > 0 ){
				for(var i=0; i<pusers.length; i++){
				 iString += '<div class="WAppPerson"><div class="imgDiv"><div style="width:64px; margin:5px; overflow:hidden; height:64px;"><img src="'+pusers[i].imageUrl+'" width="64px" ></div></div><div class="WAppPersonRight"><h3><strong>'+pusers[i].userName+'</strong> |  '+pusers[i].userInfoEx+'</h3><p class="AII">'+pusers[i].userDesc+'</p><p class="AIIspp"><i>'+pusers[i].distance+'</i><i style="text-align:right; width:70%">'+pusers[i].timeStr+'</i></p></div></div>';
				}
			}
			return iString; 
		},
		GroupDetailTemp:function(ConnextObj){
				var inAppInput = '';
				switch(ConnextObj.buttonType){
						case "1" : inAppInput = '<input type="button"  class="btnStylecc" value="申请加入" onclick="Group.GroupToJoin()" id="CheckInSuccess" apptype="ToJoin" />';
						break;
						case "2" : inAppInput = '<input type="button"  class="btnStyleccgray" value="已加入"  />';
						break;
						case "3" : inAppInput = '<input type="button"  class="btnStylecc" value="缴费" id="CheckInSuccess" />';
						break;
						default: ; 
					
					}
//
				var iString = '<section class="AppBanner" style="text-align:center; overflow:hidden; position:relative;"><img src="'+ConnextObj.imageUrl+'" width="100%"  /><div class="WAppBannerTit"><div class="WAppBannerTitbg">成员：'+ConnextObj.memberCount+'</div>'+inAppInput+'</div></section><section class="DetailAll"><h2>'+ConnextObj.groupName+'</h2><div class="DetailLi"><h3><span>介绍</span></h3><p>'+ConnextObj.groupDescription+'</p></div></section>';
				return iString; 
			}
}