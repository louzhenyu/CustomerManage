define(function ($) {
    var temp = {
        actionList: '<%for(var i=0;i<list.length;i++){ var item = list[i];%>\
		        <div data-type="<%=item.type%>" class="action"></div>\
		<%}%>\
		',
		followInfoModel:' <div class="jsListItem <%if(!FollowId){%>jsListItemEmpty<%}%> commonSelectArea"  data-type="rightFollowInfoTemp" data-key="<%=key%>" data-model="followInfo">\
		<div class="follow"> \
		<div class="backBg"></div>\
		<%if(Title){%>\
		 <p><%=Title%></p> \
		<%}else{%>\
		 <p>欢迎进入微信商城</p> \
		<%}%>\
		<div class="followBtn">立即关注</div>\
		</div>\
		<div class="handle"> \
		<div class="bg"> \
		</div>\
		<span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>\
	</div>\
	</div>',
		rightFollowInfoTemp: '<div class="option SearchList"><img class="arrows" src="images/leftJiantou.png">\
                            <h2 class="title">立即关注</h2>\
                            <div class="wrapBtn">\
                                <span class="jsCancelBtn">取消</span>\
                                <span class="jsSaveFollowBtn">保存</span>\
                            </div>\
                        </div>\
            <div class="jsAreaItem  uploadArea clearfix search" data-typeid="<%=TypeId%>" data-id="<%=TextId%>"  style="width:90%; margin: 0 auto; padding:0;border: none;padding-top: 10px; padding-bottom: 30px;">\
                  <%if(TypeId){%>\
                  <div class="lineBox">\
                  <div class="tit">欢迎语:</div> \
                  <div class="inputBox"><input type="text" id="titleName" value="<%=Title%>"/></div>\
                  </div>\
                  <div class="lineBox">\
                  <div class="tit">链接类型:</div>\
                                   <div class="wrapInput" style="width: 420px;">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect"  style="width: 100%">\
												    <option value ="cg-null" <%if(!TypeId){%>selected="selected"<%}%>>选择链接到的模块</option>\
												    <option value ="cg-3" <%if(TypeId==3){%>selected="selected"<%}%>>自定义链接</option>\
												    <option value ="cg-35" <%if(TypeId==35){%>selected="selected"<%}%>>图文素材</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix mb-15">\
												    <%if(Url){%>\
												    <input class="jsNameInput" style="width: 360px" type="text" value="<%=Url%>">\
												    <%}else{%>\
													   <input class="jsNameInput" style="width: 360px" type="text" value="<%=TextTitle%>">\
													<%}%>\
												    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
										    </div>\
						</div>\
                  <%}else{%>\
                   <div class="lineBox">\
                  <div class="tit">欢迎语:</div> \
                  <div class="inputBox"><input type="text" id="titleName" value="欢迎进入微信商城" placeholder="请输入长度在2-16之间"/></div>\
                  </div>\
                  <div class="lineBox">\
                  <div class="tit">链接类型:</div>\
                                 <div class="wrapInput" style="width: 420px;">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect" style="width: 100%">\
												    <option value ="cg-null" >选择链接到的模块</option>\
												    <option value ="cg-3" >自定义链接</option>\
												    <option value ="cg-35" >图文素材</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix mb-15">\
												    <input class="jsNameInput" style="width:360px;" type="text" value="">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
										    </div>\
										    </div>\
                  <%}%>\
</div> ',

        navigationModel: '<div class="jsListItem jsTouchslider <%if(!CategoryAreaGroupId){%>jsListItemEmpty<%}%> commonSelectArea" data-type="rightNavigationTemp" data-key="<%=key%>"  data-model="nav">\
                       <div class="navlist"><ul>\
                        <%if(itemList&&itemList.length){\
                            for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
                             <%if(idata.imageUrl){%>\
                              <li><a><img src="<%=idata.imageUrl%>"><p><%=idata.navName%></p></a></li>\
		                      <%}else{%>\
		                       <li><a><img src="images/nav/<%=i%>.png"><p><%=idata.navName%></p></a></li>\
		                      <%}%>\
                           <%}%>\
                        <%}else{%>\
                             <li><a><img src="images/nav/1.png"><p>首页</p></a></li>\
                             <li><a><img src="images/nav/2.png"><p>搜索</p></a></li>\
                             <li><a><img src="images/nav/3.png"><p>购物车</p></a></li>\
                             <li><a><img src="images/nav/4.png"><p>我的</p></a></li>\
                       <% }%>\
                        </ul>\
                    </div>\
                    <div class="handle">\
                    <div class="bg"></div>\
                    <span class="jsExitGroup"></span>\
						    <span class="jsRemoveGroup"></span>\
					    </div>\
            </div>',
        'rightNavigationTemp': '<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
						<h2 class="title">底部导航 \
						<!--<input type="checkbox" checked="true" name="ad" value="" style="width: 20px;" id="ADTempCheck"/>启用幻灯片-->\
                        </h2>\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveNavBtn">保存</span>\
							    </div>\
						    </div>\
						    <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸32px*32px，20KB以内</span></div>\
                                <%if(itemList&&itemList.length){\
								    for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
									    <div class="jsAreaItem uploadArea concise clearfix"  data-objectid="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
											        <option value ="cg-null"  <%if(!idata.typeId){%>selected="selected"<%}%>>选择链接到的模块</option>\
												    <option value ="cg-1" <%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
												    <option value ="cg-2"<%if(idata.typeId==2){%>selected="selected"<%}%>>产品</option>\
												    <option value ="cg-3"<%if(idata.typeId==3){%>selected="selected"<%}%>>自定义链接</option>\
												   <option value ="cg-31"<%if(idata.typeId==31){%>selected="selected"<%}%>>首页</option>\
												  <option value ="cg-32"<%if(idata.typeId==32){%>selected="selected"<%}%>>搜索</option>\
												  <option value ="cg-33"<%if(idata.typeId==33){%>selected="selected"<%}%>>购物车</option>\
												  <option value ="cg-34"<%if(idata.typeId==34){%>selected="selected"<%}%>>我的订单</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix mb-15">\
											     <%if(idata.typeId==3){%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.url%>">\
		                                              <%}else{%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
		                                               <%}%>\
												    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
                                                <p><input class="jsNameInput navName" type="text" value="<%=idata.navName%>" placeholder="导航名称"/></p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
								    <%for(var i=0;i<length;i++){%>\
									    <div class="jsAreaItem uploadArea concise clearfix"   data-objectid="" data-displayindex="" data-typeid="" data-name="" data-categoryareaid="" data-imageurl="">\
										    <div class="wrapPic">\
											    <p><img src="images/handleLayer/default.png"></p>\
											    <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
											    <option value ="cg-null">选择链接到的模块</option>\
												    <option value ="cg-1">商品类型</option>\
												    <option value ="cg-2">产品</option>\
												    <option value ="cg-3">自定义链接</option>\
												    <option value ="cg-31">首页</option>\
												    <option value ="cg-32">搜索</option>\
												    <option value ="cg-33">购物车</option>\
												    <option value ="cg-34">我的订单</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix mb-15">\
												    <input class="jsNameInput" type="text" value="">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
											    <p><input class="jsNameInput navName" type="text" value="" placeholder="导航名称"/></p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}%>',
        titleBtnModel: '<div id="titlePanel">\
                    <div id="addTitle" class="addBtn" style="display:none">添加标题</div>\
                    <div class="setTitle">\
                    <div class="tittleName"><em class="tit"> 标题文字：</em> <div class="titleInput"><input type="text" name="titleName"></div></div>\
                     <div class="wrapRadio">\
                     <div class="radio" data-name="titleStyle" data-value="left"><em></em><span>靠左显示</span></div>\
                     <div class="radio" data-name="titleStyle" data-value="center"><em></em><span>居中显示</span></div>\
                     <div class="radio" data-name="titleStyle" data-value="hide"><em></em><span>不显示</span></div>\
					</div>\
                    </div>\
                  </div>',
        adModel: '<div class="jsListItem jsTouchslider <%if(!(list&&list.length)){%>jsListItemEmpty<%}%>  commonSelectArea" data-type="rightADTemp" data-key="<%=key%>"  data-model="ad">\
						<div class="touchslider touchslider-demo">\
							<div class="touchslider-viewport">\
								<div style="height:100%;width:100%;">\
									<%if(list&&list.length){%>\
										<%for(var i=0;i<list.length;i++){%>\
										<%if(list[i].imageUrl){%>\
											<div class="touchslider-item"><img src="<%=list[i].imageUrl%>" /></div>\
										<%}else{%>\
										<div class="touchslider-item"> <img src="images/adList/01.png" /></div>\
										<%}%>\
										<%}%>\
									<%}else{%>\
										<div class="touchslider-item">添加广告</div>\
									<%}%>\
								</div>\
							</div>\
							<div class="dotWrap">\
								<div class="dotContainer">\
								<%if(list&&list.length>1){%>\
									<%for(var i=0;i<list.length;i++){%>\
											<span class="touchslider-nav-item <%if(i==0){%> touchslider-nav-item-current<%}%>"></span>\
									<%}%>\
								<%}%>\
								</div>\
							</div>\
						</div>\
						<div class="handle">\
						<div class="bg"></div>\
						<span class="jsExitGroup"></span>\
						    <span class="jsRemoveGroup"></span>\
					    </div>\
					</div>',
        secondKillModel: ' <div class="jsListItem <%if(!groupId){%>jsListItemEmpty<%}%> commonSelectArea" data-displayindex="<%=displayIndex%>" data-id="<%=groupId%>" data-type="rightSecondKillTemp" data-typeid="<%=shopType%>"  data-key="<%=key%>"   data-model="secondKill">\
    <div class="secondKill">\
        <div class="tit">\
        <b><%=titleName%></b>\
        <%if(shopType!=3){%>\
        <img src="images/time.png">\
        <%}%>\
        <div class="timeList <%if(shopType==3){%>hide<%}%>" data-time="<%=second%>">\
        <em>00</em>:<em>00</em>:<em>00</em> \
         </div>\
        </div>\
            <%if(arrayList&&arrayList.length==2){%>\
 <div style="" class="commonIndexArea" type="2">\
	<div class="leftBox">\
		<a href="javascript:;">\
		<%if(arrayList[0].imageUrl){%>\
		<img src="<%=arrayList[0].imageUrl %>"></a>\
		<%}else{%>\
		<img src="images/itemlist/2_1.png"></a>\
		<%}%>\
		</div>\
		<div class="rightBox">\
		<a href="javascript:;">\
	   <%if(arrayList[1].imageUrl){%>\
		<img src="<%=arrayList[1].imageUrl %>"></a>\
		<%}else{%>\
		<img src="images/itemlist/2_2.png"></a>\
		<%}%>\
		</div>\
		</div>\
           <%}else if(arrayList&&arrayList.length==3){%>\
     <div style="" class="commonIndexArea" type="3">\
	<div class="leftBox"> \
		<a href="javascript:;">\
			   <%if(arrayList[0].imageUrl){%>\
		<img src="<%=arrayList[0].imageUrl %>" />"\
		<%}else{%>\
		<img src="images/itemlist/3_1.png"/>\
		<%}%>\
		</a>  \
		</div> \
		<div class="rightBox rightBoxModel2">\
		<a href="javascript:;">\
					   <%if(arrayList[1].imageUrl){%>\
		<img src="<%=arrayList[1].imageUrl %>"/>\
		<%}else{%>\
		<img src="images/itemlist/3_2.png"/>\
		<%}%>\
		</a>\
		<a href="javascript:;">\
			   <%if(arrayList[2].imageUrl){%>\
		<img src="<%=arrayList[2].imageUrl %>" />"\
		<%}else{%>\
		<img src="images/itemlist/3_3.png"/>\
		<%}%>\
		</a>\
		</div> \
		</div>\
            <%}else  if(arrayList&&arrayList.length==1){%>\
          <div class="commonIndexArea" type="1">\
		<div class="allBox">\
			<a href="javascript:;"> \
					   <%if(arrayList[0].imageUrl){%>\
		<img src="<%=arrayList[0].imageUrl %>"\
		<%}else{%>\
		<img src="images/itemlist/1_1.png">\
		<%}%>\
			</a> \
			</div>\
			</div>\
             <%}else{%>\
           <div class="commonIndexArea" type="3"> \
		<div class="leftBox">\
			<a href="javascript:;">\
			<img src="images/itemlist/3_1.png"></a>\
			</div>\
			<div class="rightBox rightBoxModel2">\
			<a href="javascript:;"> \
			<img src="images/itemlist/3_2.png"></a> <a href="javascript:;"> \
			<img src="images/itemlist/3_3.png"></a> \
			</div>\
			</div>\
             <%}%>\
           </div>\
                               <div class="handle">\
	<div class="bg"> \
		</div>  \
		<span class="jsExitGroup"></span> \
		<span class="jsRemoveGroup"></span> \
		</div>\
                </div>',
        entranceModel: '<p class="space"></p>\
        <div class="jsListItem commonSelectArea" data-type="rightEntranceTemp" data-key="<%=key%>"  data-model="entrance">\
                            <div class="navPicArea">\
                            <%var t=8; if(listLength){ %>\
                            <% t=listLength; %>\
                           <% }%>\
                                <%for(var i=0;i<t;i++){ %>\
                                <%if(itemList&&itemList[i]){ %>\
                                <a href="javascript:;">\
                                 <%if(itemList[i].imageUrl){ %>\
                                    <img src="<%=itemList[i].imageUrl %>" alt="" />\
                                    <%}else{%> \
                                   <img src="images/classify/2_0<%=i+1%>.png" alt="" />\
                                    <%} %>\
                                </a>\
                                <%}else{ %>\
                                <a href="javascript:;">\
                                    <img src="images/classify/2_0<%=i+1%>.png" alt="" />\
                                </a>\
                                <%} %>\
                                <%} %>\
                            </div>\
                             <div class="handle">\
                            <div class="bg"></div>\
	                       <span class="jsExitGroup"></span>  \
						    <span class="jsRemoveGroup"></span>\
					    </div>\
                        </div>',
        titleModel: '<div class="titlePanel <%if(titleStyle=="hide"){%> hide <%}%>" >\
                            <div class="titleTxt  <%if(titleStyle=="center"){%> center <%}%>">\
                            <div class="span"><%=title%></div>\
                            </div>\
                    </div>',
        rightTitleTemp: '<div class="clearfix">\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveEntranceBtn">保存</span>\
							    </div>\
							    <div class="wrapRadio">\
							     <input type="radio" name="titleStyle" class="radiobtn"  value="tl1" /> 样式1\
                                 <input type="radio" name="titleStyle" class="radiobtn" value="tl2" /> 样式2\
							    </div>\
						    </div>\
						    <div class="wrapInput">\
						    <p></p>\
						    </div>\
						    ',
        eventModel: '<div class="jsListItem commonSelectArea" data-type="rightEventTemp" data-key="<%=areaFlag%>"  data-model="event">\
					<div class="noticeList">\
						<div class="list clearfix">\
							<%for(var i=0;i<arrayList.length;i++){var idata=arrayList[i];%>\
								<div class="noticeArea <%if(i%3==1){%>mlmr<%}%>">\
									<div class="box">\
										<%if(idata.typeId==2){%>\
											<h2 class="title clock">限时抢购</h2>\
											<div class="timeList" data-time="<%=idata.remainingSec%>">\
												<em>00</em>:<em>00</em>:<em>00</em>\
											</div>\
										<%}else if(idata.typeId==1){%>\
											<h2 class="title group">疯狂团购</h2>\
											<div class="timeList" data-time="<%=idata.remainingSec%>">\
												<em>00</em>:<em>00</em>:<em>00</em>\
											</div>\
										<%}else if(idata.typeId==3){%>\
											<h2 class="title clock">热销榜单</h2>\
										  <div class="end">你值得拥有</div>\
										<%}else{%>\
											<h2 class="title clock">未知分类</h2>\
										<%}%>\
										<%if(i==1){%>\
										 <div class="center"> \
												<img src="<%=arrayList[i].imageUrl%>" width="104" height="120">\
									      </div>\
										<%}else{%>\
												<img src="<%=arrayList[i].imageUrl%>" width="106" height="120">\
										<%}%>\
									</div>\
									<div class="info">\
									</div>\
								</div>\
							<%}%>\
						</div>\
					</div>\
					   <div class="handle">\
				      <div class="bg"></div> \
	                  <span class="jsExitGroup"></span> \
						<span class="jsRemoveGroup"></span>\
					</div>\
				</div>',
        eventEmptyModel: '<div class="jsListItem <%if(!groupId){%>jsListItemEmpty<%}%> commonSelectArea" data-type="rightEventTemp"  data-model="event">\
						<div class="noticeList"> \
	<div class="list clearfix"> \
		<div class="noticeArea">\
		<div class="box"> \
		<h2 class="title clock">\
		限时抢购</h2> \
		<div class="timeList" data-time="8400">\
		<em>00</em>:<em>00</em>:<em>00</em>\
	</div>\
	<img src="images/skill/1.png">\
		</div> \
		<div class="info"> \
		</div>\
		</div> \
		<div class="noticeArea">\
		<div class="box"> \
		<h2 class="title clock"> \
		疯狂团购</h2>\
		<div class="timeList" data-time="8400">\
		<em>00</em>:<em>00</em>:<em>00</em>\
	</div>\
	<div class="center">\
		<img src="images/skill/2.png">\
		</div>\
		</div> \
		<div class="info">\
		</div>\
		</div>\
		<div class="noticeArea">\
		<div class="box">\
		<h2 class="title clock">\
		热销榜单</h2>\
		<div class="end">你值得拥有</div>\
		<img src="images/skill/3.png">\
		</div>  \
		<div class="info"> \
		</div> \
		</div> \
		</div> \
		</div>\
						<div class="handle">\
						      <div class="bg"></div>\
						      <span class="jsExitGroup"></span>\
						    <span class="jsRemoveGroup"></span>\
					    </div>\
					</div>',
        eventEmptyModel2: '<div class="jsListItem jsListItemEmpty commonSelectArea" data-type="rightEventTemp" data-model="event">\
						  <div class="commonIndexArea">\
							<div class="leftBox"><a href="javascript:;"></a></div>\
							<div class="rightBox  rightBoxModel2">\
								<a href="javascript:;"><img src=""></a>\
								<a href="javascript:;"><img src=""></a>\
							</div>\
							<div class="handle"> <span class="jsRemoveGroup">X</span> </div>\
						  </div>\
						</div>',
        SearchEmptyModel: '<div class="jsListItem commonSearchArea commonSelectArea"   data-type="rightSearchTemp" data-key="<%=key%>"  data-model="Search" >\
                            <a href="javascript:Jit.AM.toPage(\'Category\')" class="allClassify"></a>\
                            <div class="commonSearchBox"> \
            <p class="searchBtn"><input id="searchBtn" type="button" /></p>\
            <p class="searchInput"><input id="searchContent" type="text" value="搜索店铺内的宝贝..." placeholder="搜索店铺内的宝贝..." /></p>\
        </div>\
                         <div class="handle">\
                         <div class="bg"></div>\
                         <span class="jsExitGroup"></span>\
		    <span class="jsRemoveGroup"></span>\
		</div>\
                      </div>',
        rightSearchTemp: '<div class="option SearchList"><img class="arrows" src="images/leftJiantou.png">\
                            <h2 class="title">搜索框设置</h2>\
                            <div class="wrapBtn">\
                                <span class="jsCancelBtn">取消</span>\
                                <span class="jsSaveSearchBtn">保存</span>\
                            </div>\
                        </div>\
            <div class=" uploadArea clearfix search">\
                 <div class="list" style="display: inline-block">\
                 <div class="checkBox on" data-name="SearchStyle"><em ></em><span>显示分类</span></div>\
             </div>\
    </div> ',
        //创意组合
        originalityModel: '	<%if(idata.itemList.length==3){%>\
							<div class="jsListItem commonSelectArea"  data-id="<%=idata.CategoryAreaGroupId%>" data-displayindex="<%=idata.displayIndex %>" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp" data-key="<%=key%>"  data-model="category">\
								<div class="commonIndexArea">\
									<div class="leftBox">\
									<%if(idata.itemList[0].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/3_1.png"></a>\
									<%}%>\
									</div>\
									<div class="rightBox rightBoxModel2">\
								<%if(idata.itemList[1].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[1].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/3_2.png"></a>\
									<%}%>\
									<%if(idata.itemList[2].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[2].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/3_3.png"></a>\
									<%}%>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
								</div>\
							</div>\
						<%}else if(idata.itemList.length==1){%>\
							<div class="jsListItem commonSelectArea" data-id="<%=idata.CategoryAreaGroupId %>"   data-displayindex="<%=idata.displayIndex %>" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp"  data-key="<%=key%>"  data-model="category">\
								<div class="commonIndexArea">\
									<div class="allBox">\
									<%if(idata.itemList[0].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/1_1.png"></a>\
									<%}%>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
								</div>\
							</div>\
						<%}else if(idata.itemList.length==2){%>\
							<div class="jsListItem commonSelectArea" data-id="<%=idata.CategoryAreaGroupId %>"  data-displayindex="<%=idata.displayIndex %>" data-groupid="<%=idata.GroupId%>" data-type="rightCategoryTemp" data-key="<%=key%>" data-json=\'<%=idata.json%>\'  data-model="category">\
								<div class="commonIndexArea">\
									<div class="leftBox">\
										<%if(idata.itemList[0].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/2_1.png"></a>\
									<%}%>\
									</div>\
									<div class="rightBox">\
										<%if(idata.itemList[1].imageUrl){%>\
										<a href="javascript:;"><img src="<%=idata.itemList[1].imageUrl%>"></a>\
									<%}else{%>\
										<a href="javascript:;"><img src="images/itemlist/2_2.png"></a>\
									<%}%>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
								</div>\
							</div>\
					<%}%>',
        categoryEmptyModel: '<div class="jsListItem jsListItemEmpty commonSelectArea" data-type="rightCategoryTemp" data-model="category">\
				<div class="commonIndexArea">\
					<div class="leftBox">\
						<a href="javascript:;"><img src="images/itemlist/3_1.png"></a>\
					</div>\
					<div class="rightBox  rightBoxModel2">\
						<a href="javascript:;"><img src="images/itemlist/3_2.png"></a>\
						<a href="javascript:;"><img src="images/itemlist/3_3.png"></a>\
					</div>\
					<div class="handle">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
			</div>',
        /////////////////////////////////////////////////////////////////////////////////////////右侧
        // 右侧广告编辑模版
        rightADTemp: '<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
						<h2 class="title">广告轮播 \
						<!--<input type="checkbox" checked="true" name="ad" value="" style="width: 20px;" id="ADTempCheck"/>启用幻灯片-->\
                        </h2>\
						<div class="wrapBtn">\
							<span class="jsCancelBtn">取消</span>\
							<span class="jsSaveADBtn">保存</span>\
						</div>\
					</div>\
					<div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸640px*212px，200KB以内</span></div>\
					',
        // 右侧广告详细项
        adItemListTemp: '<%if(list&&list.length){\
						for(var i=0;i<list.length;i++){ var idata = list[i];%>\
							<div class="jsAreaItem uploadArea clearfix" data-name="<%=idata.objectName %>" data-objectid="<%=idata.objectId %>" data-adid="<%=idata.adId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-imageurl="<%=idata.imageUrl%>">\
								<div class="wrapPic">\
									<%if(idata.imageUrl){%>\
									<p><img src="<%=idata.imageUrl%>"></p>\
									<%}else{%>\
								     <img src="images/handleLayer/default.png"/>\
									<%}%>\
									<span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
								</div>\
								<div class="wrapInput">\
									<p class="typeContainer mb-15">\
									<select class="jsTypeSelect">\
									<option value ="cg-null"  <%if(!idata.typeId){%>selected="selected"<%}%> >选择链接到的模块</option>\
									   <option value ="cg-1"  <%if(idata.typeId==1){%>selected="selected"<%}%> >商品类型</option>\
										<option value ="cg-2" <%if(idata.typeId==2){%>selected="selected"<%}%> >商品</option>\
										<option value ="cg-4" <%if(idata.typeId==4){%>selected="selected"<%}%> >商品分组</option>\
										<option value ="cg-3" <%if(idata.typeId==3){%>selected="selected"<%}%> >自定义链接</option>\
										<option value ="cg-99" <%if(idata.typeId==99){%>selected="selected"<%}%> >无</option>\
									</select>\
									<p class="infoContainer clearfix">\
									<%if(idata.typeId==3){%>\
								      <input class="jsNameInput" type="text" value="<%=idata.url%>" >\
									<%}else{%>\
									  <input class="jsNameInput" type="text" value="<%=idata.objectName%>" >\
									 <%}%>\
										<span class="jsChooseBtn tagBtn">选择</span>\
									</p>\
									<p><a href="javascript:;" class="jsDeladItemTemp delBtn"></a></p>\
								</div>\
							</div>\
						<%}%>\
					<%}else{%>\
						<%for(var i=0;i<length;i++){%>\
							<div class="jsAreaItem uploadArea clearfix">\
								<div class="wrapPic">\
								<p><img src="images/handleLayer/default.png"></p>\
									<span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
								</div>\
								<div class="wrapInput">\
									<p class="typeContainer mb-15">\
									<select class="jsTypeSelect">\
									<option value ="cg-null"  >选择链接到的模块</option>\
									   <option value ="cg-1"  >商品类型</option>\
										<option value ="cg-2" >商品</option>\
										<option value ="cg-4"  >商品分组</option>\
										<option value ="cg-3" >自定义链接</option>\
										<option value ="cg-99" >无</option>\
									</select>\
									</p>\
									<p class="infoContainer clearfix">\
										<input class="jsNameInput" type="text" value="">\
										<span class="jsChooseBtn tagBtn">选择</span>\
									</p>\
									<p><a href="javascript:;" class="jsDeladItemTemp delBtn"></a></p>\
								</div>\
							</div>\
						<%}%>\
					<%}%>',


        // 右侧活动编辑模板
        rightEventTemp: '<div class="clearfix option"> <img class="arrows" src="images/leftJiantou.png">\
                            <h2 class="title">限时抢购/疯狂团购/热销榜单</h2>\
                            <div class="wrapBtn">\
                                <span class="jsCancelBtn">取消</span>\
                                <span class="jsSaveEventBtn">保存</span>\
                            </div>\
                        </div>\
                        <div class="jsTabContainer menuLayer clearfix">\
                            <%for(var i=0;i<eventTypeList.length;i++){var ievent = eventTypeList[i];%>\
                                <span class="jsTab <%if(i==0){%> on<%}%>" data-typeid="<%=ievent.key%>"\
                                    <%if(list){for(var j=0;j<list.length;j++){var idata= list[j];if(idata.typeId == ievent.key){%>\
                                         data-currentitemid="<%=idata.itemId%>" data-displayindex="<%=idata.displayIndex%>" data-inputvalue="<%=idata.eventName%>" data-eventid ="<%=idata.eventId%>" data-value=\'<%=idata.json%>\'\
                                    <%}}}%>\
                                ><%=ievent.value%></span>\
                            <%}%>\
                        </div>\
                       <%if(list&&list.length>0){%>\
                        <div class="jsAreaTitle clearfix" data-eventid="<%=list[0].eventId%>" data-type="sk-1" data-typeid="<%=eventTypeList[0].key%>" >\
                                     	<div class="tit">选择分组:</div>\
                                     	<div class="wrapInput">\
											   <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" style="width:258px;" value="<%=list[0].eventName%>">\
												     <span class="jsChooseEventListBtn tagBtn" style="width:76px;">选择</span>\
											    </p>\
										    </div>\
                              </div>\
                       <%}else{%>\
		                <div class="jsAreaTitle clearfix" data-eventid="<%=eventId%>" data-type="sk-1" data-typeid="<%=shopType%>" >\
                                     	<div class="tit">选择分组:</div>\
                                     	<div class="wrapInput">\
											   <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" style="width:258px;" value="<%=eventName%>">\
												     <span class="jsChooseEventListBtn tagBtn" style="width:76px;">选择</span>\
											    </p>\
										    </div>\
                              </div>\
		               <%}%>\
                        <div class="SelectList">\
                            <ul class="jsGoodsList">\
                            </ul>\
                        </div>\
                        <div class="pageWrap" style="display:none;">\
                            <div class="pager" id="kPageList"></div>\
                        </div>',
        // 右侧活动编辑模板  商品刘表
        goods: '<%for(var i=0;i<itemList.length;i++){var idata=itemList[i];%>\
				<li class="jsGoodsItem" data-itemid="<%=idata.itemId%>" data-eventid="<%=idata.eventId%>">\
					<label class="clearfix">\
					<div class="RadioDiv">\
					<div class="radio <%if(currentItemId==idata.itemId){%>on<%}%>" data-name="pic" data-value=\'<%=idata.json%>\'><em></em></div>\
					<p>显示在主页</p>\
					</div>\
					\
					<p class="wrapPic"><img src="<%=idata.imageUrl%>" width="120" height="120" alt=""></p>\
					<div class="goodsInfo">\
						<p><%=idata.itemName%></p>\
					</div>\
					</label>\
				</li>\
			<%}%>',
        goodsKill: '<div class="typeSelectList"><ul class="jsGoodsList">\
        <%for(var i=0;i<itemList.length;i++){var idata=itemList[i];%>\
				<li class="jsGoodsItem" data-imageurl="<%=idata.imageUrl%>" data-itemid="<%=idata.itemId%>" data-itemName="<%=idata.itemName%>" data-eventid="<%=idata.eventId%>">\
					<label class="clearfix">\
					<span class="wrapRadio"><div class="radio <%if(currentItemId==idata.itemId){%>on<%}%>" data-name="pic"  data-value=\'<%=idata.json%>\' ><em></em></div></span>\
					<p class="wrapPic"><img src="<%=idata.imageUrl%>" alt=""></p>\
					<div class="goodsInfo">\
						<p><%=idata.itemName%></p>\
						<span class="price">￥<%=idata.salesPrice%></span>\
					</div>\
					</label>\
				</li>\
			<%}%>\
			</ul></div>\
			',
        //掌声秒杀模板
        rightSecondKillTemp: '<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
        <h2 class="title"><%=titleName%></h2>\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveKillBtn">保存</span>\
							    </div>\
						    </div>',
        SecondKillModelTemp: '<div class="clearfix" style="width:90%; margin:0 auto;">\
							    <h2 class="title">模板设置</h2>\
						    </div>\
                            <div class="jsSectionCTabContainer moduleType clearfix"  data-model="kill">\
							    <span class="jsTab one" data-model="1"><em></em></span>\
							    <span class="jsTab two" data-model="2"><em></em></span>\
							    <span class="jsTab three on" data-model="3"><em></em></span>\
						    </div>',
        SencondKillListTemp: '<div class="jsAreaTitle clearfix" data-eventid="<%=eventId%>" data-type="sk-1" data-typeid="<%=shopType%>" ></div>\
                                <%if(arrayList&&arrayList.length){%>\
                               <%if(arrayList.length==1){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸640px*346px，200KB以内</span></div>\
						     <%}else if(arrayList.length==2){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸320px*346px，200KB以内</span></div>\
						     <%}else if(arrayList.length==3){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸① 320px*346px，②③ 320px*173px，200KB以内 </span></div>\
						     <%}%>\
								   <% for(var i=0;i<arrayList.length;i++){ var idata = arrayList[i];%>\
									    <div class="jsAreaItem concise uploadArea clearfix"  data-itemid="<%=idata.itemId %>" data-eventid="<%=idata.eventId %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.itemName%>" data-eventAreaItemId="<%=idata.eventAreaItemId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <%if(idata.imageUrl){%>\
		                                          <p><img src="<%=idata.imageUrl%>"></p>\
											    <%}else{%>\
											    <p><img src="images/handleLayer/default.png"/></p>\
											    <%}%>\
											  <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											   <p class="infoContainer clearfix">\
												   <input class="jsNameInput" type="text" style="width:258px;" value="<%=idata.eventName%>">\
												    <span class="jsChooseEventBtn tagBtn" style="width:76px;">选择分组</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
								<%if(length==1){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸640px*346px，200KB以内</span></div>\
						     <%}else if(length==2){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸320px*346px，200KB以内</span></div>\
						     <%}else if(length==3){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸① 320px*346px，②③ 320px*173px，200KB以内 </span></div>\
						     <%}%>\
								    <%for(var i=0;i<length;i++){%>\
									    <div class="jsAreaItem concise uploadArea clearfix"  data-typeid="<%=shopType%>">\
										    <div class="wrapPic">\
											    <p><img src="images/handleLayer/default.png"></p>\
											   <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
											 </div>\
										    <div class="wrapInput">\
											    <p class="infoContainer clearfix">\
												  <input class="jsNameInput" type="text" style="width:258px;" value="">\
												    <span class="jsChooseEventBtn tagBtn" style="width:76px;">选择分组</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}%>',
        // 右侧分类编辑模板
        rightCategoryTemp: '<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
       						 <h2 class="title">创意组合</h2>\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveCategoryBtn">保存</span>\
							    </div>\
						    </div>',
        categoryModelTemp: '<div class="jsSectionCTabContainer moduleType clearfix">\
                               <p>显示方式:</p>\
							    <span class="jsTab one" data-model="1"><em></em></span>\
							    <span class="jsTab two" data-model="2"><em></em></span>\
							    <span class="jsTab three on" data-model="3"><em></em></span>\
						    </div>',
        // 分类详细项
        categoryItemListTemp: '	 <%if(itemList&&itemList.length){\
       						 if(itemList.length==1){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸640px*346px，200KB以内</span></div>\
						     <%}else if(itemList.length==2){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸320px*346px，200KB以内</span></div>\
						     <%}else if(itemList.length==3){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸① 320px*346px，②③ 320px*173px，200KB以内 </span></div>\
						     <%}%>\
								   <%for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
									    <div class="jsAreaItem concise uploadArea clearfix"  data-objectid="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-groupid="<%=idata.GroupId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
										    <%if(idata.imageUrl){%>\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <%}else{%>\
											    <p><img src="images/handleLayer/default.png"></p>\
											    <%}%>\
											    <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
											       <option value ="cg-null"  <%if(!idata.typeId){%>selected="selected"<%}%> >选择链接到的模块</option>\
												    <option value ="cg-1"<%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
												    <option value ="cg-2"<%if(idata.typeId==2){%>selected="selected"<%}%>>产品</option>\
												    <option value ="cg-3" <%if(idata.typeId==3){%>selected="selected"<%}%> >自定义链接</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix">\
											        <%if(idata.typeId==3){%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.url%>">\
		                                              <%}else{%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
		                                               <%}%>\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
							     <%if(length==1){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸640px*346px，200KB以内</span></div>\
						     <%}else if(length==2){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸320px*346px，200KB以内</span></div>\
						     <%}else if(length==3){%>\
        					     <div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸① 320px*346px，②③ 320px*173px，200KB以内 </span></div>\
						     <%}%>\
								    <%for(var i=0;i<length;i++){%>\
											<div class="jsAreaItem concise uploadArea clearfix">\
										    <div class="wrapPic">\
											    <p><img src="images/handleLayer/default.png"></p>\
											    <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
											    <option value ="cg-null"  selected="selected">选择链接到的模块</option>\
												    <option value ="cg-1">商品类型</option>\
												    <option value ="cg-2">产品</option>\
													<option value ="cg-3">自定义链接</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" value="">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}%>',

        // 右侧分类入口（C8区）编辑模版
        rightEntranceTemp: '<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
							    <h2 class="title">分类导航 </h2>\
							     <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveEntranceBtn">保存</span>\
							    </div>\
						    </div>\
						     <div class="wrapRadio">\
						     <div class="tit">显示数量：</div>\
							    <!--<div type="radio" name="entranceStyle" class="radiobtn"  value="s1" /> 样式1-->\
                                <div  data-name="entranceStyle" class="radio on"  data-value="4" ><em></em><span>4项</span></div>\
                                <div  data-name="entranceStyle" class="radio"  data-value="8" ><em></em><span>8项</span></div>\
							    </div>\
					<div class="hint"><b> 图片:</b><span>支持格式jpg、jpeg、png，建议尺寸160px*160px，100KB以内</span></div>\
					',
        // 右侧分类入口（C8区）详细项
        entranceItemListTemp: '<%var t=4;if(listLength){ %>\
                                    <%t=listLength;%>\
                                 <% }%>\
                                   <%for(var i=0;i<t;i++){%>\
                                    <%if(itemList&&itemList[i]){ var idata = itemList[i];%>\
                                        <div class="jsAreaItem uploadArea concise clearfix"  data-objectid="<%=idata.objectId%>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
										       <%if(idata.imageUrl){%>\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <%}else{%>\
											    <p><img src="images/handleLayer/default.png"></p>\
											    <%}%>\
											    <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
													<option value ="cg-null"  <%if(!idata.typeId){%>selected="selected"<%}%> >选择链接到的模块</option>\
									   				<option value ="cg-1"  <%if(idata.typeId==1){%>selected="selected"<%}%> >商品类型</option>\
													<option value ="cg-2" <%if(idata.typeId==2){%>selected="selected"<%}%> >商品</option>\
													<option value ="cg-3" <%if(idata.typeId==3){%>selected="selected"<%}%> >自定义链接</option>\
													 <option value ="cg-33" <%if(idata.typeId==33){%>selected="selected"<%}%> >购物车</option>\
												    <option value ="cg-34" <%if(idata.typeId==34){%>selected="selected"<%}%> >我的订单</option>\
												    <option value ="cg-37" <%if(idata.typeId==37){%>selected="selected"<%}%> >会员中心</option>\
													<option value ="cg-99" <%if(idata.typeId==99){%>selected="selected"<%}%> >无</option>\
									 			</select>\
											    </p>\
											    <p class="infoContainer clearfix <%if(!(idata.typeId==1||idata.typeId==2||idata.typeId==3)){%>hide<%}%>">\
												   <%if(idata.typeId==3){%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.url%>">\
		                                              <%}else{%>\
		                                              <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
		                                               <%}%>\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
                                    <%}else{ %>\
                                        <div class="jsAreaItem uploadArea concise clearfix">\
                                            <div class="wrapPic">\
                                                <p><img src="images/handleLayer/default.png"></p>\
                                                <span class="uploadBtn"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>\
                                            </div>\
                                            <div class="wrapInput">\
                                                <p class="typeContainer mb-15">\
                                                <select class="jsTypeSelect">\
													<option value ="cg-null"  >选择链接到的模块</option>\
									   				<option value ="cg-1"  >商品类型</option>\
													<option value ="cg-2" >商品</option>\
													<option value ="cg-3" >自定义链接</option>\
													 <option value ="cg-33">购物车</option>\
												    <option value ="cg-34">我的订单</option>\
												    <option value ="cg-37">会员中心</option>\
													<option value ="cg-99">无</option>\
                                                </select>\
                                                </p>\
                                                <p class="infoContainer clearfix"  style="display:none;">\
                                                    <input class="jsNameInput" type="text" value="">\
                                                    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
                                            </div>\
                                        </div>\
                                    <%} %>\
                                <%} %>',

        productListModel: '	<%if(object.styleType=="s1"){%>\
							<div class="jsListItem <%if(!object.CategoryAreaGroupId){%> jsListItemEmpty <%}%> commonSelectArea" data-title="大图" data-groupid="<%=object.CategoryAreaGroupId %>"  data-id="<%=object.CategoryAreaGroupId %>" data-displayindex="<%=object.displayIndex %>" data-groupId="<%=object.groupId%>" data-type="rightProductTemp" data-key="<%=key%>" data-model="product">\
		                         <div class="imagePanel" type="1">\
	<div class="item">\
		<img src="images/publicList/1_1.png">\
		<div class="txtPanel"> \
		<p class="name <%if(object.showName==0){%> hide <%}%>"> \
		商品名称</p> \
		<p> \
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥360.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>">￥400.00</b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">9.0折</b> \
		</p>  \
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">  \
		<b>销量:</b> <b>18285</b></p>   \
	</div>  \
	</div>\
	<div class="item"> \
		<img src="images/publicList/1_2.png"> \
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>"> \
		商品名称</p> \
		<p> \
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥39.50 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>">￥79.00</b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">5.0折</b> \
		</p>  \
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">  \
		<b>销量:</b> <b>18285</b></p>   \
	</div> \
	</div> \
	</div>  \
									<div class="handle" data-groupid="<%=object.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
								</div>\
							</div>\
						<%}else if(object.styleType=="s2"){%>\
							<div class="jsListItem <%if(!object.CategoryAreaGroupId){%> jsListItemEmpty <%}%> commonSelectArea" data-title="小图"  data-id="<%=object.CategoryAreaGroupId %>" data-displayindex="<%=object.displayIndex %>" data-groupId="<%=object.groupId%>" data-type="rightProductTemp" data-key="<%=key%>" data-model="product">\
                                 <div class="imagePanel" type="小图"> \
	<div class="item">\
		<div class="itemL">\
		<img src="images/publicList/2_1.png" />\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥280.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>" >￥300.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">9.3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>185</b></p>\
	</div> \
	</div> \
	<div class="itemR"> \
		<img src="images/publicList/2_2.png" />  \
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥800.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>" >￥1000.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">8折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1857</b></p>\
	</div> \
	</div> \
	<div class="itemL"> \
		<img src="images/publicList/2_3.png" /> \
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥999.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>" >3330.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>11185</b></p>\
	</div> \
	</div> \
	<div class="itemR"> \
		<img src="images/publicList/2_4.png" />\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥1949.50 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>" >3899.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1995</b></p>\
	</div> \
	</div>\
	</div>\
	</div>\
									<div class="handle" data-groupid="<%=object.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
							</div>\
						<%}else if(object.styleType=="s3"){%>\
						<div class="jsListItem <%if(!object.CategoryAreaGroupId){%> jsListItemEmpty <%}%> commonSelectArea" data-title="一大两小"  data-id="<%=object.CategoryAreaGroupId %>" data-displayindex="<%=object.displayIndex %>" data-groupId="<%=object.groupId%>" data-type="rightProductTemp" data-key="<%=key%>" data-model="product">\
								 <div class="imagePanel" type="3">\
		<div class="item">\
			<img src="images/publicList/1_1.png">\
		<div class="txtPanel"> \
		<p class="name <%if(object.showName==0){%> hide <%}%>"> \
		商品名称</p> \
		<p> \
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥360.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>">￥400.00</b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">9.0折</b> \
		</p>  \
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">  \
		<b>销量:</b> <b>18285</b></p>   \
	</div>\
		</div>\
		<div class="item">\
			<div class="itemL">\
			<img src="images/publicList/2_1.png" />\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥999.00 </b><b class="original  <%if(object.showPrice==0){%> hide <%}%>" >3330.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1111</b></p>\
	</div> \
		</div>\
		<div class="itemR">\
			<img src="images/publicList/2_2.png" />\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥99.00 </b><b class="original   <%if(object.showPrice==0){%> hide <%}%>" >330.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1685</b></p>\
	</div> \
		</div> \
		</div> \
</div>\
									<div class="handle" data-groupid="<%=object.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
							</div>\
					<%}else if(object.styleType=="s4"){%> \
		<div class="jsListItem <%if(!object.CategoryAreaGroupId){%> jsListItemEmpty <%}%> commonSelectArea" data-title="详细列表"  data-id="<%=object.CategoryAreaGroupId %>" data-displayindex="<%=object.displayIndex %>" data-groupId="<%=object.groupId%>" data-type="rightProductTemp" data-key="<%=key%>" data-model="product">\
								<div class="imagePanel" typeof="4">\
			          <div class="item">\
		<img src="images/publicList/4_1.png" class="imgL">\
			<div class="rightPanel">\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥999.00 </b><b class="original  <%if(object.showPrice==0){%> hide <%}%>" >3330.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">3折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1111</b></p>\
	</div> \
		</div> <!--rightPanel--> \
		</div><!--item-->\
		<div class="item">\
			<img src="images/publicList/4_2.png" class="imgL">\
			<div class="rightPanel">\
		<div class="txtPanel">\
		<p class="name <%if(object.showName==0){%> hide <%}%>">\
		商品名称</p>\
		<p>\
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥800.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>" >￥1000.00 </b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%> ">8折</b>\
		</p>\
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">\
		<b>销量:</b> <b>1856</b></p>\
	</div> \
		</div> <!--rightPanel-->\
		</div><!--item-->\
		<div class="item">\
			<img src="images/publicList/4_3.png" class="imgL">\
			<div class="rightPanel">\
		<div class="txtPanel"> \
		<p class="name <%if(object.showName==0){%> hide <%}%>"> \
		商品名称</p> \
		<p> \
		<b class="Price <%if(object.showSalesPrice==0){%> hide <%}%>">￥360.00 </b><b class="original <%if(object.showPrice==0){%> hide <%}%>">￥400.00</b> <b class="discount <%if(object.showDiscount==0){%> hide <%}%>">9.0折</b> \
		</p>  \
		<p class="sales <%if(object.showSalesQty==0){%> hide <%}%>">  \
		<b>销量:</b> <b>18285</b></p>   \
	</div>\
		</div>\
		</div>\
			         <div>\
			         									<div class="handle" data-groupid="<%=object.groupId%>">\
										<div class="bg"></div>\
										<span class="jsExitGroup"></span>\
										<span class="jsRemoveGroup"></span>\
									</div>\
					<%}%>',
		rightProductTemp:'<div class="option"> <img class="arrows" src="images/leftJiantou.png">\
       						 <h2 class="title">商品列表</h2>\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveProductBtn">保存</span>\
							    </div>\
						    </div>\
                  <%if(itemList&&itemList.length>0){var idata=itemList[0];%>\
                  <div class="jsAreaItem concise uploadArea clearfix" style="padding:0"  data-objectid="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
						<div class="wrapTit">商品来源:</div>\
						<div class="wrapInput">\
							<p class="typeContainer mb-15">\
					          <select class="jsTypeSelect" style="width: 420px;">\
											       <option value ="cg-null"  <%if(!idata.typeId){%>selected="selected"<%}%> >选择链接到的模块</option>\
												    <option value ="cg-1"    <%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
												    <option value ="cg-4"    <%if(idata.typeId==4){%>selected="selected"<%}%> >商品分组</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix">\
												    <input class="jsNameInput" style="width: 360px;" type="text" value="<%=idata.objectName%>">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
			<%}else{%>\
                  <div class="jsAreaItem concise uploadArea clearfix"  style="padding:0"  >\
						<div class="wrapTit">商品来源:</div>\
						<div class="wrapInput">\
							<p class="typeContainer mb-15">\
					          <select class="jsTypeSelect" style="width: 420px;">\
											       <option value ="cg-null">选择链接到的模块</option>\
												    <option value ="cg-1"  >商品类型</option>\
												    <option value ="cg-4" >商品分组</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix"  >\
												    <input class="jsNameInput" style="width: 360px;" type="text" value="">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
			<%}%>\
			 <div class="line"><div class="tit">显示个数:</div>\
			 <div class="wrapRadio">\
                     <div class="radio" data-name="showCount" data-value="6"><em></em><span>6个</span></div>\
                     <div class="radio" data-name="showCount" data-value="12"><em></em><span>12个</span></div>\
                     <div class="radio" data-name="showCount" data-value="18"><em></em><span>18个</span></div>\
			</div>\
			</div>\
			<div class="line"><div class="tit">列表版式:</div>\
			 <div class="wrapRadio lineBlock">\
                     <div class="radio" data-name="styleType" data-value="s1"><em></em><span>大图</span></div>\
                     <div class="radio" data-name="styleType" data-value="s2"><em></em><span>小图</span></div>\
                     <div class="radio" data-name="styleType" data-value="s3"><em></em><span>一大两小</span></div>\
                     <div class="radio" data-name="styleType" data-value="s4"><em></em><span>详细列表</span></div>\
			</div>\
			</div>\
           <div class="line"><div class="tit">列表内容:</div>\
			 <div class="wrapRadio" style="height:136px;">\
                     <div class="checkBox" data-name="allList" data-value="showName"><em></em><span>显示商品名称</span></div>\
                     <div class="checkBox" data-name="allList" data-value="showDiscount"><em></em><span>显示商品折扣</span></div>\
                     <div class="checkBox" data-name="allList" data-value="showSalesPrice"><em></em><span>显示商品售价</span></div>\
                     <div class="checkBox" data-name="allList" data-value="showSalesQty"><em></em><span>显示销量</span></div>\
                     <div class="checkBox" data-name="allList" data-value="showPrice"><em></em><span>显示商品原价</span></div>\
			</div>\
			</div>\
			',


        //图文素材
		materialTextList: '<%for(var i=0;i<MaterialTextList.length;i++){var idata=MaterialTextList[i]; %>\
						<tr class="categoryItem" data-id="<%=idata.TextId%>" data-name="<%=idata.Title %>">\
							<td><%=idata.Title %></td>\
						</tr>\
					<%} %>',
        // 弹层：分类
        category: '<%for(var i=0;i<categoryList.length;i++){var idata=categoryList[i]; %>\
						<tr class="categoryItem" data-id="<%=idata.categoryId %>" data-name="<%=idata.categoryName %>">\
							<td><%=idata.categoryName %></td>\
						</tr>\
					<%} %>',
		activity: '<%for(var i=0;i<eventList.length;i++){var idata=eventList[i]; %>\
						<tr class="categoryItem" data-id="<%=idata.EventId %>" data-name="<%=idata.EventName %>">\
							<td><%=idata.EventName %></td>\
						</tr>\
					<%} %>',
        groupList: '<%for(var i=0;i<groupList.length;i++){var idata=groupList[i]; %>\
						<tr class="categoryItem" data-id="<%=idata.categoryId %>" data-name="<%=idata.categoryName %>">\
							<td><%=idata.categoryName %></td>\
						</tr>\
					<%} %>',
        // 弹层：商品
        product: '<%for(var i=0;i<itemList.length;i++){var idata=itemList[i]; %>\
				<tr class="productItem" data-eventid="<%=idata.eventId%>" data-id="<%=idata.itemId%>" data-name="<%=idata.itemName%>">\
					<td><%=idata.categoryName%></td>\
					<td><%=idata.itemName%></td>\
				</tr>\
			<%}%>'
    };
    return temp;
});