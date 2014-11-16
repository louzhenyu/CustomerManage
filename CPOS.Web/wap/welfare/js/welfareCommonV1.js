

var WelfareAction = {
	
	EventAnimate:function(id1,id2,Prev){

		if(Prev == "0"){
			$("#BackPrev").hide();
			
		}else{
			$("#BackPrev").show();
			
		
		}
		//$(id1).css("left",$(id1).width());
		var oleft = $(id2).width(); 
		
		if($(id2).attr("defaultleft") == "0"){
				oleft  = -oleft; 
				
			}
		$(id2).animate({left:oleft},500,function(){
				$(this).hide();
				$(id1).show().css("opacity",0);
				var oleft1 = $(id1).width(), cLeft = 0 ; 
				
				if($(id1).attr("defaultleft") == "0"){
						oleft1  = -oleft1; 
					}
				
				$(id1).css("left", oleft1); 
				$(id1).animate({"left":0,"opacity":1},500);
				$("#backPrevPosition").val(Prev);
		})	
			$(document).scrollTop(0);
		}
}


var Course = {
		getHighLevelCourseList:function(isfirst, itemTypeId){
				
			/*if(itemTypeId && itemTypeId!=""){
				WelfareAction.CloseSearch();	
				$("#waterwall1,#waterwall2").html('');
							
			}else{
				 itemTypeId = getParam("itemTypeId") ? getParam("itemTypeId") :"";
			}*/
			 courseTypeId = getParam("courseTypeId") ? getParam("courseTypeId") :"4";
			
			if(isfirst == "1"){
				$("#CourseConnext").attr("page","1"); 
				ChangeTitle(getParam("courseTypeId"));
			}
			$("#CourseConnext").attr("isloading","1");
			var page  = $("#CourseConnext").attr("page");	
			var jsonarr = {'action':"getHighLevelCourseList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'courseTypeId':getParam("courseTypeId"), 'page':page, "pageSize":22}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					if(isfirst == "1"){
						Win.Loading();		
					}else{
						Win.Loading('',"#CourseConnext");	
					}
						
				},
				dataType : 'json',
				success:function(o){
					if(deBug == 1){
						var o = getHighLevelCourseList;
					}
					$("#CourseConnext").attr("isloading","0");
					if(isfirst == "1"){
						Win.Loading("CLOSE"); 
					}
					Win.Loading("CLOSE","#CourseConnext");
					if(o.code == "200"){
						//$("#CourseConnext").attr("page",parseInt(page)+1).attr("isnext",o.content.isNext); 
						$("#CourseConnext").attr("isnext","0"); 
						var ReturnHtml1 = HtmlTemplate.CourseGetItemList(o.content,1);
						var ReturnHtml2  = HtmlTemplate.CourseGetItemList(o.content,2);
						
						if(isfirst != "1"){
							$("#waterwall1").append(ReturnHtml1);
							$("#waterwall2").append(ReturnHtml2);
							}else{
							$("#waterwall1").html(ReturnHtml1);
							$("#waterwall2").html(ReturnHtml2);	
							}
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		getCourseDetail:function(){
			var courseId = getParam("courseId") ? getParam("courseId") :"";	
			var courseTypeId = getParam("courseTypeId") ? getParam("courseTypeId") :"4";	
				ChangeTitle(courseTypeId);
			var jsonarr = {'action':"getCourseDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{'courseTypeId':courseTypeId,"courseId":courseId}})};
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
					
						var ReturnHtml = HtmlTemplate.getCourseDetail(o.content);
					
						$("#CourseDetailDiv").html(ReturnHtml);
						if(o.content.imageList !=null && o.content.imageList.length > 0 ){
							loadImGWine();
							setTimeout(function(){myScrollWine.refresh();},100)
							$(".DetailImgIeo").css("width", o.content.imageList.length * $("#wrapper").width())
						}
					
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		getCoursePageDetail:function(type){
			var courseTypeId = getParam("courseTypeId") ? getParam("courseTypeId") :"4";
				if(type !="Contact"){
				ChangeTitle(courseTypeId);
				}
			var jsonarr = {'action':"getCourseDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{'courseId':getParam("courseId"),"courseTypeId":getParam("courseTypeId")}})};
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
						if(type !="Contact"){
						var ReturnHtml = HtmlTemplate.getCoursePageDetail(o.content);
						}else{
						var ReturnHtml = HtmlTemplate.getCourseContact(o.content);	
						}
						$("#CoursePageDetailDiv").html(ReturnHtml);
							desc="联系我们";
							weiXinFollow();
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		SudentsComment:function(){
			var courseTypeId = getParam("courseTypeId") ? getParam("courseTypeId") :"";	
				if(courseTypeId=="1"){
					$("#Btoo").text("MBA学员感言");	
				}
				if(courseTypeId=="2"){
					$("#Btoo").text("EMBA学员感言");	
				}
				if(courseTypeId=="3"){
					$("#Btoo").text("FMBA学员感言");	
				}
				if( getParam("back")!="1"){
					$("#BackPrev").hide();
				}
			var jsonarr = {'action':"getCourseDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"courseTypeId":getParam("courseTypeId"),"courseId":getParam("courseId")}})};
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
					
						var ReturnHtml = HtmlTemplate.SudentsComment(o.content);
					
						$("#StudentsCommentList").html(ReturnHtml);
							desc=$("#Btoo").text();
							weiXinFollow();
						
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		CourseSetOrder:function(){
			var username=$.trim($("#username").val()),company=$.trim($("#company").val()),
			phone = $("#phone").val(),email = $("#email").val(), post = $("#post").val(),remark = $("#remark").val();
		
//		if(username==""  || company=="" || phone=="" || email==""){
//			alert("请输入必须字段1"); 
//			return false;
//}
			if (username == "" || phone == ""  || company == ""||email=="") {
    alert("请输入必须字段");
    return false;
}
			
		var jsonarr = {'action':"setCourseApply",ReqContent:JSON.stringify({"common":Base.All(),"special":{"courseId":getParam("courseId"),"applyName":username,"company":company,"post":post,"email":email,"phone":phone,"remark":remark,"emailId":getParam("emailId")}})};
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
						desc=$("#Btoo").text();
						weiXinFollow();
						alert(o.description);
					if(o.code == "200"){
					
						
					}else{
						//alert(o.description);	
					}
					
				}
			})	
		},
	getCourseOrderPayment:function(){
		var jsonarr = {'action':"getOrderInfo",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					Win.Loading(); 		
				},
				dataType : 'json',
				success:function(o){
					
						var o = getOrderInfo;
					
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						//value="<%=Constants.Server_Host %>phone_controller?action=wx_online_payment_callback&type=wap" 
						$("#prod_name").val(o.content.itemname);
						$("#order_id").val(o.content.orderid);
						$("#prod_price").val(o.content.salesprice);
						var host_server= "http://alumniapp.ceibs.edu:8080/ceibs";
						$("#merchant_url").val(host_server+'/wap/welfare/welfareDetail.html?itemId='+o.content.itemid);
						var call_back_url=host_server+"/phone_controller?action=wx_online_payment_callback&type=wap&module=1&";
						call_back_url+= 'paymentAmount='+o.content.totalamount
						//+'&paymentTypeId='+o.content.paymentTypeId;
						+'&paymentTypeId=1&local=zh&openId='+Base.openId()+'&';
						$("#call_back_url").val(call_back_url);
						
						$("#itemname").text(o.content.itemname);
						$("#ordercode").text(o.content.ordercode);
						$("#totalamount").text(o.content.totalamount);
						$("#remark").text(o.content.remark);
						$("#deliveryname").text(o.content.deliveryname);
						$(".deliveryaddress").text(o.content.deliveryaddress);
						$(".deliverytime").text(o.content.deliverytime);
					
						
					}
				}
			})
	}
	
}

var News = {
	newsList:function(){
		var jsonarr = {'action':"getNewsListByCourseId",ReqContent:JSON.stringify({"common":Base.All(),"special":{"courseTypeId":getParam("courseTypeId")}})};
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
						//var o = newsListData;
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						var ReturnHtml = HtmlTemplate.newsList(o.content);
						$("#newsList").html(ReturnHtml);
						
						title=o.content.pageTitle;
						$("#Btoo").text(title);
						
						
					}
				}
			})
	},
	newsDetail:function(){
		var jsonarr = {'action':"getNewsById",ReqContent:JSON.stringify({"common":Base.All(),"special":{"newsId":getParam("newsId")}})};
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
						var ReturnHtml = HtmlTemplate.newsDetailData(o.content);
						$("#newsDetail").html(ReturnHtml);
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						if(o.content.imageList !=null && o.content.imageList.length > 0 ){
							loadImGWine();
							setTimeout(function(){myScrollWine.refresh();},100)
							$(".DetailImgIeo").css("width", o.content.imageList.length * $("#wrapper").width())
						}
						title=o.content.pageTitle;
						desc=$(".newsDetailDescription").text();
						
						
					}
				}
			})
	}
}

var BBS  = {
		BBSList:function(){
			var jsonarr = {'action':"getForumEntriesList",ReqContent:JSON.stringify({"common":Base.All(),"special":{"forumTypeId":"1"}})};
			//1=行业论坛
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
						if(deBug == 1){
							var o = bbsListData;
						}
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						var ReturnHtml = HtmlTemplate.bbsListData(o.content);
						$("#BBSList").html(ReturnHtml);
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						$("#Btoo").text(o.content.pageTitle);
						title=o.content.pageTitle;
					
						
					}
				}
			})	
		},
		Detail:function(){
		if(getParam("forumId") ==""){
			alert("该论坛不存在！");
			return false; 
				
		}
		var jsonarr = {'action':"getForumDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"forumId":getParam("forumId")}})};
	
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
							$("#title").text(o.content.title);
							
							
							title=o.content.title;
						  var tpl = _.template($('#bbsScript').html(), o.content);
                  			  $("#bbsDetail").html(tpl).show();
								if(o.content.imageURL!=null){
									imgUrl = o.content.imageURL;	
								}
							emailId = o.content.emailId;
						
					
							desc = $("#desc").text();
							
						
								
						
					}else{
						alert(o.description)	
					}
				}
			})	
		}
}
var Consult = {
	consultList:function(){
		//ChangeTitle(getParam("forumTypeId"))
		
		var jsonarr = {'action':"getForumEntriesList",ReqContent:JSON.stringify({"common":Base.All(),"special":{"forumTypeId":getParam("forumTypeId")}})};
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
						var ReturnHtml = HtmlTemplate.consultList(o.content);
					
						$("#BBSList").html(ReturnHtml);
						$("#Btoo").text(o.content.pageTitle);
						 title=document.title;
						desc = $("#Btoo").text();
					}else{
						alert(o.description)	
					}
				}
			})
	},
	CeisNewList:function(){
		//ChangeTitle(getParam("forumTypeId"))
		
		var jsonarr = {'action':"getZONewsOrZKList",ReqContent:JSON.stringify({"common":Base.All(),"special":{"typeId":getParam("typeId")}})};
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
						var ReturnHtml = HtmlTemplate.CeisNewList(o.content);
					
						$("#BBSList").html(ReturnHtml);
						$("#Btoo").text(o.content.pageTitle);
						 title=document.title;
						desc = $("#Btoo").text();
					}else{
						alert(o.description)	
					}
				}
			})
	},
	CeisDetail:function(){
		//ChangeTitle(getParam("forumTypeId"))
		
		var jsonarr = {'action':"getZONewsOrZKDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"textId":getParam("textId")}})};
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
						 var tpl = _.template($('#CeisNewListId').html(), o.content);
                  			  $("#CeisDetail").html(tpl).show();
								
								title=o.content.title;
								desc =$("#desc").text();

					}else{
						alert(o.description)	
					}
				}
			})
	},
	Detail:function(){
		if(getParam("forumId") ==""){
			alert("该id不存在！");
			return false; 
				
		}

		var jsonarr = {'action':"getForumDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"forumId":getParam("forumId"),"forumTypeId":getParam("forumTypeId")}})};
			
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
							$("#title").text(o.content.title);
							$("#Btoo").text(o.content.pageTitle);
							
								courseId = o.content.courseId;
							   var tpl = _.template($('#consultScript').html(), o.content);
                  			  $("#bbsDetail").html(tpl).show();
								if(o.content.imageURL!=null){
									imgUrl = o.content.imageURL;	
								}
								title=o.content.title;
								desc =$("#desc").text();
								emailId = o.content.emailId;
					
						
					}else{
						alert(o.description)	
					}
				}
			})	
		},
	GetApplyTabel:function(){
		var username=$.trim($("#username").val()),usercompany=$("#usercompany").val(),usertitle=$.trim($("#usertitle").val()),email=$.trim($("#email").val()),mobile=$.trim($("#mobile").val());
		if(email!="" && !IsEmail(email)){
			Pop.Alert("请正确输入您的邮箱！");
			return false; 
		}
		if(username=="" || usertitle=="" || usercompany=="" || mobile==""){
			Pop.Alert("请输入必填项!");	
			return false;
		}
		var jsonarr = {'action':"GetApplyTabel",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					Win.Loading(); 		
				},
				dataType : 'json',
				success:function(o){
						var o = consultData;
					
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						Pop.Alert("索取成功。我们将于邮件的方式发送到您的邮箱中。");
						//location.href = document.URL; 
					}
				}
			})	
	}
}
function ChangeTitle(typeid){
		if(typeid != ""){
			if(getParam("forumTypeId") == "2"){
				$("#Btoo").text("活动预告");	
			}
			if(getParam("forumTypeId") == "3"){
				$("#Btoo").text("MBA招生咨询会");	
			}
			if(getParam("forumTypeId") ==  "4"){
				$("#Btoo").text("EMBA招生咨询会");	
			}
			if(getParam("forumTypeId") ==  "5"){
				$("#Btoo").text("FMBA招生咨询会");	
			}	
			if(getParam("courseTypeId") == "1"){
				$("#Btoo").text("MBA课程");	
			}
			if(getParam("courseTypeId") == "2"){
				$("#Btoo").text("EMBA课程");	
			}
			if(getParam("courseTypeId") == "3"){
				$("#Btoo").text("FMBA课程");	
			}
			if(getParam("courseTypeId") == "4"){
				$("#Btoo").text("高层经理培训课程");	
			}
		}
}
