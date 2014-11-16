var Tpl ={
	getEventsEntriesList:function(data){
			var iString = '';
			var getDataList = data.entriesList;
			if(getDataList.length > 0 ){
				for(var i=0; i<getDataList.length; i++)	{
						iString+= '<a href="photoDetail.html?entriesId='+getDataList[i].entriesId+'"><div class="DaRenImgDivBox"><div class="DaRenImgDivBoxInner"><span class="DesIndex icnSkin">'+getDataList[i].displayIndex+'</span> <img src="'+getDataList[i].workUrl+'" width="100%" onload="photoSize(this)"> <span class="BrOpacity"></span><div class="BrTxt"><span class="bricn1 icnSkin">'+getDataList[i].commentCount+'</span><span class="bricn2 icnSkin">'+getDataList[i].praiseCount+'</span></div></div></div></a>';
				}
			}else{
				iString +="<div style='text-align:center; padding-top:10px;'>暂未公布</div>";	
			}
			return iString;
		},
	getEventsEntriesMonthDaren:function(data){
		var iString = '';
			var getDataList = data.entriesList;
			if(getDataList.length > 0 ){
				for(var i=0; i<getDataList.length; i++)	{
						iString+= '<a href="photoDetail.html?entriesId='+getDataList[i].entriesId+'"><div class="DaRenImgDivBox"><div class="DaRenImgDivBoxInner"><img src="'+getDataList[i].workUrl+'" width="100%" onload="photoSize(this)"> <span class="BrOpacity"></span><div class="BrTxt"><span class="bricn2 icnSkin" style="margin-left:80px;">'+getDataList[i].praiseCount+'</span></div></div></div></a>';
				}
			}else{
				iString +="<div style='text-align:center; padding-top:10px;'>暂未公布</div>";	
			}
			return iString;
	},
	getEventsEntriesCommentList:function(data,pagesize,page){
			var iString = '';
			iString += '<div class="DaRenTT" style="line-height:0;"><div style="overflow:hidden; position:relative; text-align:center;">';
			if (data.preEntriesId != null) {
				iString+='<a href="photoDetail.html?entriesId='+data.preEntriesId+'">';
			iString += '<div class="prevPhotoDetail"><table width="100%"><tr><td height="200" valign="middle" align="center"><span class="icnSkin prevD"></span></td></tr></table></div>';
				iString+='</a>';
			}
			iString += '<img src="'+data.workUrl+'" height="200" style="margin:0 auto;">';
			if(data.nextEntriesId!=null){
				iString+='<a href="photoDetail.html?entriesId='+data.nextEntriesId+'">';
			iString += '<div class="nextPhotoDetail "><table width="100%"><tr><td height="200" valign="middle" align="center"><span class="icnSkin NextD"></span></td></tr></table></div>';
				iString+='</a>';
			}
			iString += '</div></div>';
			iString += '<div class="photoActionDiv"><div class="PhotoCommentTextarea"><div><textarea class="PhotoCommentTextareaStyle" id="PhotoCommentTextareaId"></textarea></div></div><div class="PhotoCommentBtndiv"><div class="PhotoCommentZan icnSkin" onclick="DaRen.setEventsEntriesPraise(\'' + data.entriesId + '\')">' + data.praiseCount + '</div><div class="PhotoCommentBtndivc"><input type="button" class="PhotoCommentBtndivInput icnSkin" value="" onclick="DaRen.setEventsEntriesComment(\'' + data.entriesId + '\')"></div></div>';
			var getCommentCount = data.commentCount;
			var MaxPage = Math.ceil(getCommentCount/pagesize);
			iString +='<div class="PhotoCommentList" id="PhotoCommentList">';
				var getCommentList =data.commentList; 
				if(getCommentList.length > 0 ){ 
					for(var j=0; j<getCommentList.length; j++){
						var getPhone = getCommentList[j].phone; 
							var ccgetPhone = '';
							if(getPhone.length > 4){
						  	ccgetPhone = getPhone.substring(0,3)+"****"+getPhone.substring(7,getPhone.length);
							}
						iString +='<div class="PhotoCommentLi"><strong>'+getCommentList[j].userName+'</strong>('+ccgetPhone+')：'+getCommentList[j].content+'<p class="PhotoCommentLiP">'+getCommentList[j].createTime+'</p></div>';
					}
				}else{
					iString +='<div id="NoCommentId">暂无评论</div>';	
				}
				if(MaxPage >page　){
					iString +='<div class="moreComment" onclick="DaRen.getEventsEntriesCommentList(\'1\')">点击查看更多</div>';
					}
			iString +='</div>';
		iString +='</div>'
		return 	iString;
	},
	MoreComment:function(data,pagesize,page){
		var iString = '';
		var getCommentCount = data.commentCount;
		var MaxPage = Math.ceil(getCommentCount/pagesize);
		var getCommentList =data.commentList; 
		if(getCommentList.length > 0 ){ 
					for(var j=0; j<getCommentList.length; j++){
						var getPhone = getCommentList[j].phone; 
							var ccgetPhone = '';
							if(getPhone.length > 4){
						  	ccgetPhone = getPhone.substring(0,3)+"****"+getPhone.substring(7,getPhone.length);
							}
						iString +='<div class="PhotoCommentLi"><strong>'+getCommentList[j].userName+'</strong>('+ccgetPhone+')：'+getCommentList[j].content+'<p class="PhotoCommentLiP">'+getCommentList[j].createTime+'</p></div>';
					}
			}
		if(MaxPage >page　){
					iString +='<div class="moreComment" onclick="DaRen.getEventsEntriesCommentList(\'1\')">点击查看更多</div>';
			}
		return iString; 
	}
		
}