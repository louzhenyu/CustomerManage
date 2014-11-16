
var iWinth ; 
var HtmlTemplate = {
		getItemList:function(data, atype){
			var Istring = '',
				getData = data.itemList
			if(getData.length > 0 ){
				for(var i =0; i<getData.length; i++ ){
					var iFalse = false, ies = '';  
					if(i % 2 == 0 && atype == 1){
							iFalse = true; 
					}
					if(i%2 != 0  && atype == 2){
							iFalse  = true;
						}
					if(iFalse  == true ){
						var itemName = getData[i].itemName; 
							itemName = (itemName.length > 6 ) ? itemName.substring(0,6)+".." : itemName;
							if(IsIEVersion() == 0 ){
								Istring += '<a href="welfareDetail.html?itemId='+getData[i].itemId+'">';
							}else{
								ies = " onclick='location.href=welfareDetail.html?itemId="+getData[i].itemId+"'";
								}
							Istring +='<li '+ies+'><div class="WelfareConnextBoxLi"><img width="100%" src="'+getData[i].imageUrl+'"> </div><div class="WelfareConnextBoxLiTxt"><h2>'+itemName+'</h2><h3>原价：'+getData[i].price+'<span class="CentertextLine"></span></h3><div class="WelfareRmb">￥'+getData[i].salesPrice+'</div></div></li>';	
							if(IsIEVersion() == 0 ){
								Istring += '</a>';	
							}
						}
					
					}
				}
			return Istring; 
			
		},
		getItemTypeList:function(data){
			var Istring = '',
				getData = data.itemTypeList;	
				if(getData.length > 0 ){
					for(var i =0; i<getData.length; i++ ){
							Istring +='<li onclick="Welfare.getItemList(\'1\',\''+getData[i].itemTypeId+'\')">'+getData[i].itemTypeName+'</li>';	
					}
				}
			return Istring;
		},
		getItemDetail:function(data){
			var Istring = '<div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>',
				getDataimg = data.imageList, msc = ''; 
				
					
				if(getDataimg.length > 0 ){
					for(var i=0; i<getDataimg.length; i++ ){
						Istring += '<li style="width:'+Win.W()+'px"><img src="'+getDataimg[i].imageURL+'" width="'+Win.W()+'" /></li>';
						if(i==0){
							msc += '<dd class="active">'+(i+1)+'</dd>';
						}else{
						msc += '<dd>'+(i+1)+'</dd>';	
						}
					}	
				}
				Istring +='</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
					Istring+= msc; 
				Istring +='</dl></div>';
				var Picse ="" ,btnType = '', ddj='', icnPType = ''; 
				var skuDatas = data.skuList;
				if(data.pTypeId =="2"){
					Picse =skuDatas[0].salesPrice;	
					icnPType = '<span class="Detailicn icnTG"></span>';
					btnType ='<a href="javascript:void(0)" onclick="Welfare.IsCanSumnitOrder(this,\'1\')" shref="submitOrder.html?itemId='+data.itemId+'" class="DetailActionBtn">'+data.buyTypeDesc+'</a>';
				}else{
					Picse =skuDatas[0].discountRate; 
					btnType ='<a href="javascript:void(0)" class="DetailActionBtn" onclick="Welfare.setDownloadItem()">立即下载</a>';
					icnPType = '<span class="Detailicn icnJUAN"></span>';
					ddj = '<div class="DetailmdkTs">使用此优惠可以额外获得'+skuDatas[0].integral+'点积分</div>'; 
				}
				Istring +='<div class="Detailmdk"><div class="DetailmdkINner"><div class="DetailmdkZv">￥'+Picse+'</div><div class="DetailmdkZBtn">'+btnType+'</div></div><div class="DetailmdkZvc">原价：'+skuDatas[0].price+'<span class="CentertextLine"></span></div>'+ddj+'</div>';
				var ppS = (data.pTypeId =="2") ? "购买" : "下载"; 
			
				var wdi = $("#DetailBigBox").width() - ($("#DetailBigBox").width() * 0.03*0.04 * 2) - 120 , PerS = Math.floor(wdi/37) - 1;
				
				Istring +='<div class="DetailSmallBox">'+icnPType+'<div class="DetailSmallBoxYW"><h2>'+data.itemName+'</h2><h3>'+skuDatas[0].skuProp1+'</h3><h4>中欧校友特享原价'+skuDatas[0].price+'元的' +skuDatas[0].discountRate+'折优惠！</h4><div class="DetailSmallBoxYWDlR"><span class="Detailicn Dicn1"></span>&nbsp;'+data.salesPersonCount+'人已'+ppS+'</div><div class="CpersonRen"><div class="CLeft Detailicn Dicn2" style="display:none;" onclick="WelfareAction.PersonAnimate(1)"></div><div style="width:'+wdi+'px; overflow:hidden; float:left;"><ul  id="GoUlId" page="0" style="width:'+ ( data.salesUserList.length * 37 )+'px">';
				var salesUserListdata = data.salesUserList;
				
				iWinth = wdi; 
				if(salesUserListdata.length > 0 ){
					Istring +="<span>";
					for(var i=0; i< salesUserListdata.length; i++){
						
						Istring +='<li><img src="'+salesUserListdata[i].imageURL+'" width="32" height="32" ></li>';
						if(i % PerS == 0 && i!=0){
							Istring+="</span><span style='display:none;'>";
						}
					}
						Istring+="</span>";
				}
			
				Istring +='</ul></div><div class="CRight Detailicn Dicn3" onclick="WelfareAction.PersonAnimate(2)"></div></div><div class="DetailSmallBoxYWDDR"><span class="Detailicn Dicn4"></span>&nbsp;'+data.endTime+'前使用 </div></div></div>';
				
				var storeInfoData = data.storeInfo;
				if(storeInfoData &&storeInfoData.storeName){
				Istring +='<div class="DetailSmallBox" ><div class="DetailSmallBoxYW" style="padding-bottom:0;"><h2>适用门店</h2>';
				//?storeId=1212&itemId=23232323
					var ieccs = ''; 
					if(IsIEVersion() == 0 ){
								Istring += '<a href="shopDetail.html?storeId='+storeInfoData.storeId+'&itemId='+data.itemId+'">';
							}else{
								ieccs = " onclick='location.href=shopDetail.html?storeId="+storeInfoData.storeId+"&itemId="+data.itemId;
								}
				Istring += '<div class="DetailSmallBoxYWLI" '+ieccs+'><div class="DetailSmallBoxYWLIIMg"><img src="'+storeInfoData.imageURL+'" width="80" height="80" ></div><div class="DetailSmallBoxYWLIIMgtt"><div class="DetailSmallBoxYWLIIMgH2">'+storeInfoData.storeName+'</div><div class="DetailSmallBoxYWLIIMgH3">'+storeInfoData.address+'</div></div></div>';
			if(IsIEVersion() == 0 ){
					Istring+="</a>";
			}
				Istring += '<div class="DDTow"><a href="shopList.html?itemId='+data.itemId+'">查看全部'+storeInfoData.storeCount+'家分店   ></a></div></div></div>'; 
				}
			Istring +='<div class="DetailSmallBox" ><div class="DetailSmallBoxYW" ><h2>使用须知</h2><div class="EPSdg">'+data.useInfo+'<br />详情请联系相关客服：<a href="tel:'+data.tel+'"><span class="yTel">'+data.tel+'</span></a></div></div></div>';
			
			var brandInfoData = data.brandInfo; 
			
			Istring += '<div class="DetailSmallBox" ><div class="DetailSmallBoxYW" style="padding-bottom:0;"><h2>品牌信息</h2>';
			if(brandInfoData.length > 0 ){
				var itemId = getParam("itemId") ? getParam("itemId") :"";
				for(var i=0; i<brandInfoData.length; i++ ){
						var iesii = ''; 
					if(IsIEVersion() == 0 ){
								Istring += '<a href="brandDetail.html?brandId='+brandInfoData[i].brandId+'&itemId='+itemId+'">';
							}else{
								iesii = " onclick='location.href=brandDetail.html?brandId="+brandInfoData[i].brandId+"&itemId="+itemId;
								}
					
			Istring += '<div class="DetailSmallBoxYWLI" '+iesii+'><div class="DetailSmallBoxYWLIIMg"><img src="'+brandInfoData[i].brandLogoURL+'" width="80" height="80" ></div><div class="DetailSmallBoxYWLIIMgtt"><div class="DetailSmallBoxYWLIIMgH2">'+brandInfoData[i].brandName+'</div><div class="DetailSmallBoxYWLIIMgH3">'+brandInfoData[i].brandEngName+'</div></div></div>'
			
			if(IsIEVersion() > 0 ){
						Istring+="</a>";
					}
				}
				Istring += '<div class="DDTow"><a href="brandDetail.html?brandId='+brandInfoData[0].brandId+'&itemId='+itemId+'">查看在该企业的校友  ></a></div></div></div>';
			}
			
			
			return Istring;
			
		},
		getStoreListByItem:function(data){
			var Istring = '', getStoreListByItemdata = data.storeList;
			var itemId = getParam("itemId") ? getParam("itemId") :"";
			if(getStoreListByItemdata.length > 0 ){
				for(var i=0; i<getStoreListByItemdata.length; i++ ){
					var ies = ''; 
					if(IsIEVersion() == 0 ){
								Istring += '<a href="shopDetail.html?storeId='+getStoreListByItemdata[i].storeId+'&itemId='+itemId+'">';
							}else{
								ies = " onclick='location.href=shopDetail.html?storeId="+getStoreListByItemdata[i].storeId+"&itemId="+itemId;
								}
			 Istring += '<div class="DetailSmallBoxYWLI" style="margin-top:18px;" '+ies+'><div class="DetailSmallBoxYWLIIMg" style="top:5px; left:10px;"><img width="80" height="80" src="'+getStoreListByItemdata[i].imageURL+'"></div><div class="DetailSmallBoxYWLIIMgtt" style="margin-right:35px; margin-left:100px;"><div class="DetailSmallBoxYWLIIMgH2">'+getStoreListByItemdata[i].storeName+'</div><div class="DetailSmallBoxYWLIIMgH3">'+getStoreListByItemdata[i].address+'</div><div class="DetailSmallBoxYWLIIMgH4"><span class="frdisplay">'+getStoreListByItemdata[i].distance+'</span><a href="tel:'+getStoreListByItemdata[i].tel+'" style="color:#ac0f12">'+getStoreListByItemdata[i].tel+'</a> </div></div><span class="Jro"></span></div></a>';		
			 	if(IsIEVersion() > 0 ){
						Istring+="</a>";
					}
				}
			}
			return Istring; 
		},
		getStoreDetail:function(data){
			var Istring ="";
			 Istring += '<div class="DetailSmallBox" ><div style="padding-bottom:0;" class="DetailSmallBoxYW"><div class="DetailSmallBoxYWLI"><div class="DetailSmallBoxYWLIIMg" style="top:5px;"><img width="80" height="80" src="'+data.imageURL+'"></div><div class="DetailSmallBoxYWLIIMgtt"><div class="DetailSmallBoxYWLIIMgH2">'+data.storeName+'</div><div class="DetailSmallBoxYWLIIMgH3" style="padding-top:10px;">电话：<a href="tel:'+data.tel+'"><span class="yTel">'+data.tel+'</span></a></div></div></div><div class="DDTow" style="text-align:left;">地址： '+data.address+'</div></div></div>';	
			 if(data.longitude!=null && data.latitude!=null){
				Istring += '<div class="DetailSmallBox" style="border:none;"><iframe src="googlemap.html?lat='+data.longitude+'&lng='+data.latitude+'" frameborder="0" scrolling="no" width="100%" height="350" ></iframe></div>';
			 }
			  Istring += '<div class="DetailSmallBox" style="border:none;"><div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>';
				var getDataimg = data.imageList, msc = ''; 
				
					
				if(getDataimg.length > 0 ){
					for(var i=0; i<getDataimg.length; i++ ){
						Istring += '<li ><img src="'+getDataimg[i].imageURL+'" width="'+Win.W()+'" /></li>';
						if(i==0){
							msc += '<dd class="active">'+(i+1)+'</dd>';
						}else{
						msc += '<dd>'+(i+1)+'</dd>';	
						}
					}	
				}
				Istring +='</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
					Istring+= msc; 
				Istring +='</dl></div></div>';
			return Istring; 
		},
		getBrandDetail:function(data){
			var Istring = "";	
			Istring+= '<div class="DetailSmallBox"><div class="DetailSmallBoxYW" style="padding-bottom:0;"><div class="DetailSmallBoxYWLI" style="margin-top:0"><h2>简介</h2><div class="DetailSmallBoxYWLIIMgH2">'+data.brandName+'<br>'+data.brandEngName+'</div><div style="text-align:left; font-size:14px;" class="DetailSmallBoxYWLIIMgH3">'+data.brandDesc+'</div></div></div><div style="padding:20px; color:#ac0f12; font-size:12px;" class="DetailSmallBoxYWLIIMgH3">垂询或预订，敬请致电： <a href="tel:'+data.tel+'"><span class="yTel">'+data.tel+'</span></a></div></div>';
			var getBrandData = data.brandAlumnusList;
			if(getBrandData.length > 0 ){
			Istring +='<div class="DetailSmallBox"><div class="DetailSmallBoxYW" style="padding-bottom:0;"><h2>该企业校友</h2>';
				for(var i = 0; i< getBrandData.length; i++){
			Istring +='<div class="DetailSmallBoxYWLI"><div class="DetailSmallBoxYWLIIMg"><img width="80" height="80" src="'+getBrandData[i].imageURL+'"></div><div class="DetailSmallBoxYWLIIMgtt"><div class="DetailSmallBoxYWLIIMgH2">'+getBrandData[i].userName+' | '+getBrandData[i].schoolInfo+'</div><div class="DetailSmallBoxYWLIIMgH3">'+getBrandData[i].jobTitle+'</div></div></div>';
				}
			Istring +='</div></div>';
			
			
			}
			return Istring; 
		},
	getStoreListByItemSroder:function(data){
			var Istring = '', getStoreListByItemdata = data.storeList;
		
			if(getStoreListByItemdata.length > 0 ){
				for(var i=0; i<getStoreListByItemdata.length; i++ ){
		
			
			 Istring += '<div style=" padding-top:0; border:none; padding-bottom:10px; position:relative;" onclick="WelfareAction.CDDownPP(\''+getStoreListByItemdata[i].storeId+'\')" storeid="'+getStoreListByItemdata[i].storeId+'" id="StoreListc_'+getStoreListByItemdata[i].storeId+'"><span class="HasSelectNo Detailicn"></span><div class="SorderDiv"><div class="SorderDivInnercc"><div class="SorderDivInnerccPP1">'+getStoreListByItemdata[i].storeName+'</div><div class="SorderDivInnerccPP2">'+getStoreListByItemdata[i].address+'</div></div></div></div>';
			 
				}
			}
			return Istring;
		},
	getDonationList:function(data){
			var Istring = '<li class="DonationTitle"><div class="DonationListName">姓名</div><div class="DonationListTime"> 捐赠时间 </div></li>';
			var getDonationListData = data.userlist;
			if(getDonationListData.length > 0 ){
				for(var i =0; i<getDonationListData.length; i++){
					var isdd = ''; 
					if(i == getDonationListData.length - 1){
							isdd =' style="border-bottom:0;"';
						}
					Istring += '<li '+isdd+'><div class="DonationListName">'+getDonationListData[i].username+'</div><div class="DonationListTime">'+getDonationListData[i].donationtime+'</div></li>';	
				}	
				
			}else{
				Istring += '<li  style="border-bottom:0; text-align:center; line-height:40px;">暂无捐赠人员</li>';	
			}
			return Istring; 
		},
	CourseGetItemList:function(data, atype){
			var Istring = '',
				getData = data.itemList
			if(getData.length > 0 ){
				for(var i =0; i<getData.length; i++ ){
					var iFalse = false, ies = '';  
					if(i % 2 == 0 && atype == 1){
							iFalse = true; 
					}
					if(i%2 != 0  && atype == 2){
							iFalse  = true;
						}
					if(iFalse  == true ){
						var itemName = getData[i].itemName; 
							//itemName = (itemName.length > 6 ) ? itemName.substring(0,6)+".." : itemName;
							if(IsIEVersion() == 0 ){
								Istring += '<a href="courseDetail.html?itemId='+getData[i].itemId+'">';
							}else{
								ies = " onclick='location.href=welfareDetail.html?itemId="+getData[i].itemId+"'";
								}
							Istring +='<li '+ies+'><div class="WelfareConnextBoxLi"><img width="100%" src="'+getData[i].imageUrl+'"> </div><div class="WelfareConnextBoxLiTxt"><h2>'+itemName+'</h2></div></li>';	
							if(IsIEVersion() == 0 ){
								Istring += '</a>';	
							}
						}
					
					}
				}
			return Istring; 
			
		},
	getCourseDetail:function(data){
			var Istring = '<section class="DetailAll"><div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>',
				getDataimg = data.imageList, msc = ''; 
				
					
				if(getDataimg.length > 0 ){
					for(var i=0; i<getDataimg.length; i++ ){
						Istring += '<li style="width:'+(Win.W()*0.94)+'px"><img src="'+getDataimg[i].imageURL+'" width="'+(Win.W()*0.94)+'" /></li>';
						if(i==0){
							msc += '<dd class="active">'+(i+1)+'</dd>';
						}else{
						msc += '<dd>'+(i+1)+'</dd>';	
						}
					}	
				}
				Istring +='</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
				Istring+= msc; 
				Istring +='</dl></div>';
				Istring += '<div class="kecLi"><strong>课程简介</strong></div><div class="kecLi">'+data.introduce+'</div><div class="kecLi" style="border-bottom:none; cursor:pointer; " onclick="location.href=\'coursePageDetail.html?itemId='+data.itemId+'\'"><strong>查看课程详情</strong><span class="Jro" style="top:15px;"></span></div>';
			
				Istring += "</section>";
				Istring +=' <section class="DetailAll" style="margin-top:20px;"><div class="kecLi"><strong>课程费用</strong></div><div class="kecLi"<strong style="color:red">'+data.cost+'</strong></div></section>';
				/*Istring +='<section class="DetailAll" style="margin-top:20px;"><div class="ApplyEventParent">  <h4>申请 <span id="Pnum" class="Pnum">1</span> 人</h4><div class="PaidPei1"><div  class="EventPersonAll"> <div class="EventPerson"><div class="EventPersonInner"><img width="75" height="75"  onload="photoSize(this,75,75)" src="http://alumniapp.ceibs.edu:8080/ceibs/FileUploadServlet/upfiles/qronghao.e08sh2_middle_1371363916136.jpg?x=1" style="margin-left: 0px;"></div><div class="EventPersonName">邱荣浩 Raymond QIU</div></div></div></section>';*/
				Istring +='<div class="CourseBtn"><ul><li  ><a href="courseOnline.html?itemId='+data.itemId+'" class="CourseBtnAA">索取申请表</a></li><li style="width:34%"><a href="javascript:viod(0)" class="CourseBtnAA">推荐信</a></li><li><a class="CourseBtnAA" style="margin-right:0" onclick="location.href=\'coursePageDetail.html?itemId='+data.itemId+'\'">联系我们</a></li></ul></div>';
			return Istring;
			
		},
		getCoursePageDetail:function(data){
				var Istring = '<section class="DetailAll"><div class="kecLi"><strong>'+data.itemName+'</strong><br />'+data.startTime+'</div> <div class="kecLi">'+data.cost+'</div> <div class="kecLi"><strong>开课介绍：</strong><br>'+data.introduce+'</div> <div class="kecLi"><strong>描述：</strong><br>'+data.description+'</div> <div class="kecLi" style="border-bottom:none;">注:我院保留对课程信息（包括价格、日期、地点、师资、课程安排和其他细节等）进行调整的权利。</div></section>';
				return Istring; 
		},
	newsList:function(data){
			var Istring = "";
			var getData = data.newsList; 
			if(getData !=null && getData.length > 0 ){
				for(var i=0; i<getData.length; i++){
					var getTimea = getData[i].time, 
						 getTimeArr = getTimea.split(" "),  getTime1 = "" , getTime2 = "";
						if(getTimeArr[0]!=null){
								 getTime1 = getTimeArr[0];
							}
						if(getTimeArr[1]!=null){
								getTime2 = getTimeArr[1];
							}
						var ies = ''; 
						if(IsIEVersion() == 0 ){
								Istring += '<a href="newsDetail.html?newsId='+getData[i].newsId+'">';
							}else{
								ies = " onclick='location.href=newsDetail.html?newsId="+getData[i].newsId;
								} 
					Istring += '<div class="DetailSmallBox" '+ies+'><div class="DetailSmallBoxYW" style="padding:12px 2% 6px 2%;"><p class="newsListp1">'+getTime2+'<br /><span style="font-size:12px;">'+getTime1+'</span></p><p class="newsListp2">'+getData[i].title+'</p><span class="blank1"></span> </div><span style="top:25px;" class="Jro"></span></div>';
						if(IsIEVersion() > 0 ){
						Istring+="</a>";
						}
					}	
			}
			return Istring;
	},
	newsDetailData:function(data){
			var Istring = '<div class="DetailSmallBox newsDetailBox"><div style="padding:12px 2% 6px 2%;" class="DetailSmallBoxYW"><p class="newsListp1">8:00<br><span style="font-size:12px;">2013-09-04</span></p><p class="newsListp2" style="margin-right:10px;">'+data.title+'</p><span class="blank1"></span></div><div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>',
				getDataimg = data.imageList, msc = ''; 
				if(getDataimg.length > 0 ){
					for(var i=0; i<getDataimg.length; i++ ){
						Istring += '<li style="width:'+(Win.W()*0.94)+'px"><img src="'+getDataimg[i].imageURL+'" width="'+(Win.W()*0.94)+'" /></li>';
						if(i==0){
							msc += '<dd class="active">'+(i+1)+'</dd>';
						}else{
						msc += '<dd>'+(i+1)+'</dd>';	
						}
					}	
				
				Istring +='</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
				Istring+= msc; 
				Istring +='</dl></div>';
			}
			Istring +='<div class="newsDetailDescription">'+data.description+'</div></div>';
			return Istring;
	},
	bbsListData :function(data){
		
		var Istring = "";
			var getData = data.bbsList; 
			if(getData !=null && getData.length > 0 ){
				for(var i=0; i<getData.length; i++){
					
						var ies = ''; 
						if(IsIEVersion() == 0 ){
								Istring += '<a href="bbsDetail.html?bbsId='+getData[i].bbsId+'">';
							}else{
								ies = " onclick='location.href=bbsDetail.html?bbsId="+getData[i].bbsId;
								} 
					Istring += '<div class="DetailSmallBoxYW DetailBBSList"><span class="spanDDD">■</span><h1>'+getData[i].title+'</h1><span class="BBSTime">'+getData[i].time+'</span><span class="Jro" style="top:25px;"></span></div>';
						if(IsIEVersion() > 0 ){
						Istring+="</a>";
						}
					}	
			}
			return Istring;
		},
		consultList:function(data){
				var Istring = "";
			var getData = data.consultList;
		
			if(getData !=null && getData.length > 0 ){
				for(var i=0; i<getData.length; i++){
					
						var ies = ''; 
						if(IsIEVersion() == 0 ){
								Istring += '<a href="consultDetail.html?consultId='+getData[i].consultId+'">';
							}else{
								ies = " onclick='location.href=consultDetail.html?consultId="+getData[i].consultId;
								} 
					Istring += '<div class="DetailSmallBoxYW DetailBBSList"><span class="spanDDD">■</span><h1>'+getData[i].title+'</h1><span class="BBSTime">'+getData[i].time+'</span><span class="Jro" style="top:25px;"></span></div>';
						if(IsIEVersion() > 0 ){
						Istring+="</a>";
						}
					}	
			}
			return Istring;
		}
}
