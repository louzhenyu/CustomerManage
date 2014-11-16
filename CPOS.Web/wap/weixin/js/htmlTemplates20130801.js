// JavaScript Document
var Templates ={
		EventDetailTemp:function(ConnextObj){
				var inAppInput = '';
				switch(ConnextObj.buttonType){
						case "1" : inAppInput = '<input type="button"  class="btnStylecc CheckInSuccess" value="网页报名" onclick="Event.ActionApply()"  apptype="Apply" />';
						break;
						case "2" : inAppInput = '<input type="button"  class="btnStylecc CheckInSuccess" value="签到" onclick="Event.AppCheckIn.getPosition()" apptype="CheckIn" /><input type="button"  class="btnStyleccgray CheckInSuccessBtn" value="已签到"  style="display:none"  />';
						break;
						case "4" : inAppInput = '<input type="button"  class="btnStyleccgray" value="已签到"  />';
						break;
						case "3" : inAppInput = '<input type="button"  class="btnStylecc CheckInSuccess" value="现在缴费" onclick="Event.pay()" />';
						break;
						case "5" : inAppInput = '<input type="button"  class="btnStyleccgray" value="已缴费"  />';
						break;
						case "6" : inAppInput = '<input type="button"  onclick="Event.ExitEvent()" class="btnStyleccgray" value="退出活动"  />';
						break;
						default: break;; 
					
					}
					
				/*if(ConnextObj.applyStatus == 1){
						inAppInput = '<input type="button"  class="btnStylecc" value="已报名待审批" />';
					}*/
				var Std =closeHTML( ConnextObj.description.replace(/&lt;/gi,"<").replace(/&gt;/gi,">").replace(/\'/gi," ").replace(/\"/gi," "));
				
				var des = ConnextObj.description.replace(/\'/gi," ").replace(/\"/gi," ");
				if(des.length > 80){
					des  =  closeHTML(des.substring(0,80)+"...");	
				}
	/*		<div class="WAppBannerTit"><div class="WAppLy"><a href="javascript:void(0)" onclick="Event.ActionEventApplyPerson(\''+GET1["eventId"]+'\',\''+ConnextObj.applyCount+'\')">已报名<span class="fontPco">'+ConnextObj.applyCount+'</span>人</a></div><div class="Muri">'+inAppInput+'</div></div>*/
				var iString = '<section class="AppBanner" id="AppBanner" style="text-align:center; overflow:hidden; position:relative;"><canvas id="BannerCanvas"></canvas></section>';
				var Bf = '', Bg = '';
				if(ConnextObj.applyStatus == "false"){
						Bfalse =' style="display:none"';
						Bg = ' style="display:block"';
					}else{
						Bfalse =' style="display:block"';
						Bg = ' style="display:none"';
						}
					
				
				iString += '<div id="HasApplyOK" '+Bfalse+'><section class="DetailAll2" style="padding-bottom:0;" ><div class="DetailLi" style="border:0; padding-right:30px;" onClick="Event.OpenList()"><span class="eicn1 EventIcn"></span><h3>活动主题</h3>'+ConnextObj.title+'<span class="EventJt"></span></div></section><section class="DetailAll2" style=" border-top:0; padding:0; height:2px; overflow:hidden; clear:both; width:92%;"></section><section class="DetailAll2" style=" border-top:0; padding:0; height:2px; overflow:hidden; clear:both; width:90%;"></section></div>';
				iString += '<section class="DetailAll" id="EventListOPen" '+Bg+'><div class="DetailLi" onclick="Event.CloseList()" style="padding-right:30px;"><span class="eicn1 EventIcn"></span><h3>活动主题</h3>'+ConnextObj.title+'<span class="EventJtUp"></span></div><div class="DetailLi"><span class="eicn2 EventIcn"></span><h3>活动协会</h3>'+ConnextObj.organizer+'</div><div class="DetailLi"><span class="eicn3 EventIcn"></span><h3>活动时间</h3>'+ConnextObj.timeStr+'</div><div class="DetailLi"><span class="eicn4 EventIcn"></span><h3>联系人:</h3>'+ConnextObj.contact+'</div><div class="DetailLi" ><span class="eicn5 EventIcn"></span><h3>活动地址</h3>'+ConnextObj.address+'</div><div class="DetailLi" style="border:0;"><span class="eicn6 EventIcn"></span><h3><span>介绍:</span></h3><div id="descriptionlang" style="display:none;">'+Std+'</div><div id="descriptionShort">'+des+'</div><div style="padding:10px; font-size:16px; text-align:right"><a href="javascript:void(0)" rel="0" onclick="DescriptIonMore(this)">查看更多</a></div></div></section>';
			//	if(ConnextObj.applyStatus == "true"){
				iString += '<section class="DetailAll2" style="padding-bottom:0; padding-top:8px; margin-top:20px;" onClick="Event.PrizeDetail(\'0\')"><div class="DetailLi" style="border:0;"><span class="eicn7 EventIcn"></span><h3>刮刮奖</h3><span class="EventJtEI"></span></div></section>';
				//}
				iString +='<div class="DetailBox">'+inAppInput+'</div>';
				//&nbsp;&nbsp;已签到<span class="fontPco">'+ConnextObj.checkinCount+'</span>人
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
		
		
			var istring ="";
				if(ConnextObj.type==0){
						var iprizeslist = ConnextObj.prizes;
						if(iprizeslist.length > 0 ){
						istring = '<dt class="s1"></dt>';
						for(var j=0; j<iprizeslist.length; j++){
							istring += '<dd>'+(j+1)+'. '+iprizeslist[j].prizeName+'  '+iprizeslist[j].prizeDesc+'</dd>';
							}
						}
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
					 istring += '<div class="WAppPerson" style="min-height:84px"><div class="imgDiv"><div style="width:64px; margin:5px; overflow:hidden; height:64px;"><img width="64"  src="'+iprizeslist[j].userImageUrl+'"></div></div><div class="WAppPersonRight"><h3 style="font-size:14px;"><span class="frdisplay grayDate">'+iprizeslist[j].time+'</span><strong>'+iprizeslist[j].userName+'</strong></h3><p class="AII">'+iprizeslist[j].message+'</p></div></div>';
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
				var inAppInput = '', RecentEvent = '';
				switch(ConnextObj.buttonType){
						case "1" : inAppInput = '<input type="button"  class="btnStylecc" value="申请加入" onclick="Group.GroupToJoin()" apptype="ToJoin" />';
						break
						case "3" : inAppInput = '<input type="button"  class=" btnStylecc"  onclick="GroupPay()" value="续费"  />';
						break
						case "2" : inAppInput = '<input type="button"  class="btnStylecc" value="缴费" onclick="GroupPay()"  />';
						break
						case "4" : inAppInput = '<input type="button"  class="btnStyleccgray" value="已加入"  />';
						break
						case "5" : inAppInput = '<input type="button"  class="btnStylecc" value="退出该协会" onclick="Group.submitGroupQuit()"  />';
						break
						default: inAppInput = ''; 
					
					}
				if(ConnextObj.comingEventCount!="0"){	
					if(ConnextObj.comingEventCount=="1"){
						RecentEvent = '<a href="base.html?eventId='+ConnextObj.latestEventId+'#Event/Detail" class="btnStylecc">近期活动</a>';
					}else{
						RecentEvent = '<a href="groupEventList.html?groupId='+GET1['groupId']+'&groupName='+encodeURIComponent(ConnextObj.groupName)+'" class="btnStylecc">近期活动</a>';
						}
				}
		if(ConnextObj.imageUrl==""){
				ConnextObj.imageUrl = 'http://alumniapp.ceibs.edu:8080/ceibs/FileUploadServlet/upfiles/event_default.png';
			}
         var iString = '<section class="AppBanner" style="text-align:center; overflow:hidden; position:relative;"><img src="'+ConnextObj.imageUrl+'" width="100%"  /></section><section class="DetailAll"><h2><span class="Jintou"></span>'+ConnextObj.groupName+'</h2><div class="WAppBannerTit"><div class="WAppLy">会员<span class="fontPco"> '+ConnextObj.memberCount+' </span>人</div><div class="Muri">'+inAppInput+'</div></div><div class="DetailLi"><h3><span>介绍:</span></h3><p >'+ConnextObj.groupDescription.replace(/&lt;/gi,"<").replace(/&gt;/gi,">")+'</p></div><div style="margin-top:20px;">'+RecentEvent+'</div></section>';
				return iString; 
			
			},
		GroupListTemp:function(ConnextObj){
				var Cont = ConnextObj.groups;
				var FF = IsIEVersion();
				var iString = '';
				var RandImage = ['images/groupimage.png'];
				if(Cont.length > 0 ){
					for(var i =0; i< Cont.length; i++){
						var imageUrlc = '', bClick = '';
						if(Cont[i].imageUrl==""){imageUrlc= RandImage[0];}else{imageUrlc=Cont[i].imageUrl;}
						if(FF <=0 ){
							iString += "<a href='groupDetail.html?groupId="+Cont[i].groupId+"#Group/Detail'>"	
						}else{
							bClick = 'onclick="Group.GotoGroupDetail(\''+Cont[i].groupId+'\',this)"';
							}
					 iString += '<div class="WAppGounp" '+bClick+'><div class="imgDiv"><div class="ImageInnerDiv" ><img width="64" src="'+imageUrlc+'"></div></div><div class="WAppPersonRight"><h3><strong>'+Cont[i].groupName+'</strong></h3><p class="AII">会员：<span class="groupTxtStyle">'+Cont[i].memberCount+'</span> &nbsp;&nbsp;活动：<span class="groupTxtStyle">'+Cont[i].eventCount+'</span> </p></div><span class="GroupJtou"></span></div>';
					 	if(FF <=0 ){
								iString += "</a>";
							}
					}
				}
		
			return iString; 
		},
		GroupPersonTemp:function(ConnextObj){
			var Cont = ConnextObj.users;
			
				var iString = '';
				if(Cont.length > 0 ){
					for(var i =0; i< Cont.length; i++){
						iString+=' <div class="GroupPersonO"><div class="GroupPersonOInner"><img src="'+Cont[i].imageUrl+'" class="PersonHead"></div><div class="GroupPersonName">'+Cont[i].userName+'</div></div>';	
					}
				}
			return iString; 	
		},
		getComingEventsForGroup:function(ConnextObj){
				var Cont = ConnextObj.events;
				var FF = IsIEVersion();
				var iString = '';
				var RandImage = ['http://alumniapp.ceibs.edu:8080/ceibs/FileUploadServlet/upfiles/event_default.png'];
				if(Cont.length > 0 ){
					for(var i =0; i< Cont.length; i++){
						var imageUrlc = '', bClick = '';
						if(Cont[i].imageUrl==""){imageUrlc= RandImage[0];}else{imageUrlc=Cont[i].imageUrl;}
						if(FF <=0 ){
							iString += "<a href='base.html?eventId="+Cont[i].eventId+"#Event/Detail'>"	
						}else{
							bClick = 'onclick="Group.GotoGroupDetail(\''+Cont[i].eventId+'\',this)"';
							}
						var detailD = Cont[i].description;
						if(detailD.length > 40){
								detailD = detailD.substring(0,40)+"...";
						}
					 iString += '<div class="OOEventList" '+bClick+'><h3>'+Cont[i].title+'</h3><p class="Sode">开始时间:'+Cont[i].startDate+' 结束时间：'+Cont[i].endDate+'</p><div class="OOEventListImg"><img src="'+imageUrlc+'"></div><div class="OOEventListDetail">'+detailD+'</div></div>';
					 	if(FF <=0 ){
								iString += "</a>";
							}
					}
				}
			return iString; 	
		},
		EventApplyPerson:function(ConnextObj,type,page,isNext){
			var iString = "";
			var odata = ConnextObj.users;
			if(odata.length > 0){
				for(var i =0 ; i< odata.length; i++){
				
				iString +='<div class="EventPerson"><div class="EventPersonInner"><img src="'+((odata[i].imageUrl!="")?odata[i].imageUrl:"images/default.png")+'"  onload="photoSize(this,75,75)" onerror="ImgError(this,75,75)"></div><div class="EventPersonName">'+odata[i].userName+'</div></div>';
				}
				if(isNext =="1"){
					iString+= '<div style="text-align:right; line-height:24px; clear:both;" class="CheckMore"><a href="javascript:void(0)" onclick="Event.EventApplyPerson(\''+type+'\',\''+(parseInt(page)+1)+'\')" >查看更多</a>';	
				}
				if( page > 1){
					iString += '<a href="javascript:void(0)" onclick="Event.CloseEventApplyPerson(\''+type+'\',this)" rel="0">收起</a>';	
				}
				if(odata.isNext =="1"){
					iString += "</div>";
				}
			}else{
				$("#EventPersonAllPay"+type).parent().hide();
				//iString+= '<div style="text-align:center; padding-top:20px;">没有数据</div>';	
				
			}
			return iString;
		}
}
var Init = function(){
	var iosLink = 'https://itunes.apple.com/cn/app/id543840943';
		if(navigator.userAgent.indexOf("iPad") > 0 ){
			iosLink = 'https://itunes.apple.com/cn/app/id589764491';
			}
		var iString = '';
		iString += '<div style="clear:both; font-size:12px;">';
     	  	iString += ' <div class="frdisplay" style="width:120px; margin-top:5px; margin-left:12px;"><a href="'+iosLink+'"><img src="images/iosbtn.jpg" /></a><a href="http://weixun.co/app/ceibsalumni/android_test/iAlumni.apk"><img src="images/androidbtn.jpg" style="margin-top:5px;" /></a></div>';
      			 iString += ' 更多校友录协会活动通知报名、商机互动、查询所有校友通讯录功能请下载中欧校友录App。请校友关注此公众号，关注后可在微信中直接报名协会活动，还能查询商业动态，校友使用校友汇App遇到问题也请告诉公众号小秘书。';
	  		iString += '</div>';
     	iString += '<div style="clear:both; margin-top:12px; padding:10px 0; ">';
        	iString += '<div style="line-height:22px; font-size:12px;"><span style="color:#fb5240; font-size:16px;">关注方法</span><br>';
         	 iString += '方法一：“请选择右上角的“<img src="images/tipd.png" width="50" />”，然后点击“查看公众账号”查看并进行关注。<br />';
			  iString += '方法二：打开微信”朋友们“的”添加朋友“->点击”查找微信公众帐号“->搜索”<span style="color:red">中欧APP平台</span>“后点击”关注“';
		  iString += '</div>';
       iString += '</div>';
	   if($("#FollowStyle").length > 0){
	   	$("#FollowStyle").html(iString);
	   }
	  var LoginString = '';
	    LoginString+='<div class="AlumniConnext1Title"><a class="AlumniBack icnbg" href="javascript:void(0)" onClick="AppSet.Close(\'AlumniConnext1\')" ></a>登陆中欧校友账号</div><span style=" height:20px;" class="blank1"></span>';
		LoginString +='<div style="width:82%; margin-left:auto; margin-right:auto; line-height:28px;"><p style="padding-top:10px; padding-bottom:10px; font-size:16px;">登陆后，您可以查询中欧校友库、活动快速报名与现场参与。</p></div>';
   	 	LoginString +='<div class="inputAndSelect" style="position:relative;">'
     		 LoginString +='<div style=" border: 1px solid #B2BAC0; border-radius: 4px 4px 4px 4px; background-color:#fff; padding-right:108px; ">';
				 LoginString +='<input type="text" value="中欧校友账号" onblur="FocusFun(this,\'中欧校友账号\',0)" onfocus="FocusFun(this,\'中欧校友账号\',1)" class="AlumniInputstyle" id="username" style="border:none; background-color:none; padding-right:1%; width:97%;">';
				 LoginString +='<div style=" background-color:#f3f5f5; height:40px; position:absolute; right:1px; top:1px; line-height:40px; width:100px; padding-left:8px; border-radius:3px">@CEIBS.EDU</div>';
     		LoginString +='</div>';
    	LoginString +='</div>';
    LoginString +='<div class="inputAndSelect">';
      	LoginString +='<input type="text" onblur="FocusFun(this,\'中欧校友密码\',0,1)" onfocus="FocusFun(this,\'中欧校友密码\',1,1)" value="中欧校友密码" class="AlumniInputstyle" id="pwd">';
    LoginString +='</div>';
    LoginString +='<div style="line-height:46px;" class="inputAndSelect">';
      LoginString +='<input type="button" class="inputsubmitStyle " value="登陆" onClick="AppSet.LoginSubmit()" />';
     LoginString +='</div>';
	 if($("#AlumniConnext1").length > 0 ){
		 $("#AlumniConnext1").html(LoginString);
		}
} 
$(function(){
	Init()
})
function closeHTML(str){ 
	var arrTags=["span","font","b","u","i","h1","h2","h3","h4","h5","h6","p","li","ul","table","div"]; 
	for(var i=0;i<arrTags.length;i++){ 
	var intOpen=0; 
	var intClose=0; 
	var re=new RegExp("\\<"+arrTags[i]+"( [^\\<\\>]+|)\\>","ig"); 
	var arrMatch=str.match(re); 
	if(arrMatch!=null) intOpen=arrMatch.length; 
	re=new RegExp("\\<\\/"+arrTags[i]+"\\>","ig"); 
	arrMatch=str.match(re); 
	if(arrMatch!=null) intClose=arrMatch.length; 
	for(var j=0;j<intOpen-intClose;j++){ 
		str+="</"+arrTags[i]+">"; 
	} 
	/*for(var j=(intOpen-intClose-1);j>=0;j--){ 
	str+="</"+arrTags[i]+">"; 
	}*/ 
	} 
	return str;
}
