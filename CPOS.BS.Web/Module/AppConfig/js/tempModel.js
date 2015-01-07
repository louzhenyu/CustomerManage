define(function ($) {
    var temp = {
        navigationModel:'<div class="jsListItem jsTouchslider commonSelectArea" data-type="rightNavigationTemp" data-key="<%=key%>"  data-model="nav">\
                       <div class="navlist"><ul>\
                        <%if(itemList&&itemList.length){\
                            for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
                              <li><a><img src="<%=idata.imageUrl%>"><p><%=idata.navName%></p></a></li>\
                           <%}%>\
                        <%}else{%>\
                             <li><a><img src="images/dhicon01.png"><p>首页</p></a></li>\
                             <li><a><img src="images/dhicon02.png"><p>搜索</p></a></li>\
                             <li><a><img src="images/dhicon03.png"><p>购物车</p></a></li>\
                             <li><a><img src="images/dhicon04.png"><p>我的</p></a></li>\
                       <% }%>\
                        </ul>\
                    </div>\
                    <div class="handle">\
						    <span class="jsRemoveGroup">X</span>\
					    </div>\
            </div>',
       'rightNavigationTemp':'<div class="clearfix">\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveNavBtn">保存</span>\
							    </div>\
							    <div class="wrapRadio">\
							     <input type="radio" name="navStyle" class="radiobtn"  value="s1" /> 样式1\
                                 <input type="radio" name="navStyle" class="radiobtn" value="s2" /> 样式2\
							    </div>\
						    </div>\
						    <div class="jsAreaTitle clearfix"><h2 class="title">图片维护</h2></div>\
                                <%if(itemList&&itemList.length){\
								    for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
									    <div class="jsAreaItem uploadArea clearfix"  data-objectid="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
												    <option value ="cg-1"<%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
												    <option value ="cg-2"<%if(idata.typeId==2){%>selected="selected"<%}%>>产品</option>\
												    <option value ="cg-3"<%if(idata.typeId==3){%>selected="selected"<%}%>>自定义链接</option>\
												   <option value ="cg-31"<%if(idata.typeId==31){%>selected="selected"<%}%>>首页</option>\
												  <option value ="cg-32"<%if(idata.typeId==32){%>selected="selected"<%}%>>搜索</option>\
												  <option value ="cg-33"<%if(idata.typeId==33){%>selected="selected"<%}%>>购物车</option>\
												  <option value ="cg-34"<%if(idata.typeId==34){%>selected="selected"<%}%>>我的订单</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix mb-15">\
												    <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
                                                <p><input class="jsNameInput navName" type="text" value="<%=idata.navName%>" placeholder="导航名称"/></p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
								    <%for(var i=0;i<length;i++){%>\
									    <div class="jsAreaItem uploadArea clearfix">\
										    <div class="wrapPic">\
											    <p><span class="size">图片尺寸70*70</span></p>\
											    <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
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
            titleBtnModel: '<div id="titlePanl">\
                    <div id="addTitle" class="addBtn">添加标题</div>\
                    <div class="setTitle">\
                     <div class="wrapRadio">\
						<input type="radio" name="titleStyle" checked="checked" class="radiobtn"  value="tl1" /> 标题样式1\
                        <input type="radio" name="titleStyle" class="radiobtn" value="tl2" /> 标题样式2\
					</div>\
					<div class="text">\
					    <input type="text" name="title" class="titleText" value="" placeholder="请填写标题名称"/>\
					    <p>*输入字符长度2-6较为合适</p>\
					</div>\
                    </div>\
                  </div>',
        adModel: '<div class="jsListItem jsTouchslider commonSelectArea" data-type="rightADTemp" data-key="<%=key%>"  data-model="ad">\
						<div class="touchslider touchslider-demo">\
							<div class="touchslider-viewport">\
								<div style="height:100%;width:100%;">\
									<%if(list&&list.length){%>\
										<%for(var i=0;i<list.length;i++){%>\
											<div class="touchslider-item"><img src="<%=list[i].imageUrl%>" /></div>\
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
											<span class="touchslider-nav-item <%if(i==0){%> touchslider-nav-item-current<%}%>"><%=i+1%></span>\
									<%}%>\
								<%}%>\
								</div>\
							</div>\
						</div>\
						<div class="handle">\
						    <span class="jsRemoveGroup">X</span>\
					    </div>\
					</div>',
        secondKillModel:' <div class="jsListItem commonSelectArea" data-type="rightSecondKillTemp"  data-key="<%=key%>"   data-model="secondKill">\
    <div class="secondKill">\
        <div class="tit">\
        <%if(shopType==1){%>\
        <b>疯狂团购</b> \
        <%}else{%>\
        <b>掌上秒杀</b>\
        <%}%>\
                 <img src="images/time.png" width="15" >\
                <div class="timeList">\
                    <span><%=_h%></span>:\
                    <span><%=_m%></span>: \
                    <span><%=_s%></span>\
                </div>\
            </div>\
            <%if(arrayList&&arrayList.length==2){%>\
          <ul class="imgList two">\
                <li><div><img src="<%=arrayList[0].imageUrl %>"> <p>￥<%=arrayList[0].salesPrice %></p> <span><%=arrayList[0].discount%>折</span></div></li>\
                <li class="center"><div><img src="images/defImg02.png"> <p>￥999</p> <span>5.8折</span></div></li>\
                <li><div><img src="<%=arrayList[1].imageUrl %>"> <p>￥<%=arrayList[1].salesPrice %></p> <span><%=arrayList[1].discount%>折</span></div></li>\
            </ul>\
           <%}else if(arrayList&&arrayList.length==3){%>\
             <ul class="imgList">\
                <li><div><img src="<%=arrayList[0].imageUrl %>"> <p>￥<%=arrayList[0].salesPrice %></p> <span><%=arrayList[0].discount %>折</span></div></li>\
                <li class="center"><div><img src="<%=arrayList[1].imageUrl %>"> <p>￥<%=arrayList[1].salesPrice %></p> <span><%=arrayList[1].discount%>折</span></div></li>\
                <li><div><img src="<%=arrayList[2].imageUrl %>"> <p>￥<%=arrayList[2].salesPrice %></p> <span><%=arrayList[2].discount%>折</span></div></li>\
            </ul>\
            <%}else{%>\
            <ul class="imgList">\
                <li><div><img src="images/defImg01.png"> <p>￥188</p> <span>7.8折</span></div></li>\
                <li class="center"><div><img src="images/defImg02.png"> <p>￥999</p> <span>5.8折</span></div></li>\
                <li><div><img src="images/defImg03.png"> <p>￥230</p> <span>8.8折</span></div></li>\
            </ul>\
             <%}%>\
           </div>\
                    <div class="handle">\
						    <span class="jsRemoveGroup">X</span>\
					    </div>\
                </div>',
        entranceModel: '<div class="jsListItem commonSelectArea" data-type="rightEntranceTemp" data-key="<%=key%>"  data-model="entrance">\
                            <div class="navPicArea">\
                            <%var t=8; if(listLength){ %>\
                            <% t=listLength; %>\
                           <% }%>\
                                <%for(var i=0;i<t;i++){ %>\
                                <%if(itemList&&itemList[i]){ %>\
                                <a href="javascript:;">\
                                    <img src="<%=itemList[i].imageUrl %>" alt="" />\
                                </a>\
                                <%}else{ %>\
                                <a href="javascript:;">\
                                    <img src="images/nullBg.png" alt="" />\
                                </a>\
                                <%} %>\
                                <%} %>\
                            </div>\
                             <div class="handle">\
						    <span class="jsRemoveGroup">X</span>\
					    </div>\
                        </div>',
        titleModel: '<div  class="titlePanl">\
                            <div class="titleTxt">\
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
        eventModel: '<div class="jsListItem commonSelectArea" data-type="rightEventTemp" data-key="<%=key%>"  data-model="event">\
					<div class="noticeList">\
						<div class="list clearfix">\
							<%for(var i=0;i<list.length;i++){var idata=list[i];%>\
								<div class="noticeArea <%if(i%3==1){%>mlmr<%}%>">\
									<div class="box">\
										<%if(idata.typeId==2){%>\
											<h2 class="title clock">限时抢购</h2>\
										<%}else if(idata.typeId==1){%>\
											<h2 class="title group">疯狂团购</h2>\
										<%}else if(idata.typeId==3){%>\
											<h2 class="title graph">热销榜单</h2>\
										<%}else{%>\
											<h2 class="title graph">未知分类</h2>\
										<%}%>\
										<img src="<%=list[i].imageUrl%>">\
									</div>\
									<div class="info">\
									</div>\
								</div>\
							<%}%>\
						</div>\
					</div>\
					<div class="handle">\
						<span class="jsRemoveGroup">X</span>\
					</div>\
				</div>',
        eventEmptyModel: '<div class="jsListItem commonSelectArea" data-type="rightEventTemp" data-model="event">\
						<div class="noticeList">\
							<div class="list clearfix">\
								<div class="noticeArea">\
									<div class="box">\
										<h2 class="title clock">限时抢购</h2>\
										<img src="">\
									</div>\
									<div class="info">\
									</div>\
								</div>\
								<div class="noticeArea mlmr">\
									<div class="box">\
										<h2 class="title group">疯狂团购</h2>\
										<img src="">\
									</div>\
									<div class="info">\
									</div>\
								</div>\
								<div class="noticeArea">\
									<div class="box">\
										<h2 class="title graph">热销榜单</h2>\
										<img src="">\
									</div>\
									<div class="info">\
									</div>\
								</div>\
							</div>\
						</div>\
						<div class="handle">\
						    <span class="jsRemoveGroup">X</span>\
					    </div>\
					</div>',
    SearchEmptyModel: '<div class="jsListItem commonSearchArea commonSelectArea"   data-type="rightSearchTemp" data-key="<%=key%>"  data-model="Search" >\
                            <a href="javascript:Jit.AM.toPage(\'Category\')" class="allClassify"></a>\
                            <div class="commonSearchBox"> \
            <p class="searchBtn"><input id="searchBtn" type="button" /></p>\
            <p class="searchInput"><input id="searchContent" type="text" value="搜索店铺内的宝贝..." placeholder="搜索店铺内的宝贝..." /></p>\
        </div>\
                         <div class="handle">\
		    <span class="jsRemoveGroup">X</span>\
		</div>\
                      </div>',
        rightSearchTemp: '<div class="clearfix SearchList">\
                            <h2 class="title">搜索框设置</h2>\
                            <div class="wrapBtn">\
                                <span class="jsCancelBtn">取消</span>\
                                <span class="jsSaveSearchBtn">保存</span>\
                            </div>\
                        </div>\
            <div class=" uploadArea clearfix">\
                 <div class="list wrapRadio" style="display: inline-block">\
                    <b>样式选择</b>\
                <input type="radio" name="SearchStyle" class="radiobtn"  value="s1" /> 样式1\
                <input type="radio" name="SearchStyle" class="radiobtn" value="s2" /> 样式2\
             </div>\
        <div class="wrapInput" style="float:left">\
        <select class="jsTypeSelect">\
            <option  value="logo-1" selected="selected">显示分类</option>\
            <option  value="logo-2">显示logo</option>\
        </select>\
        <div class="seeahType clearfix ">\
         <b>logo上传</b>\
         <div  class="jsAreaItem uploadArea"> \
             <div class="wrapPic">\
             <p><span class="size">图片尺寸60*90</span></p>\
				<span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
				</div>\
        </div>\
            </div>\
        </div>\
    </div> ',
        //左侧分类模板
        categoryModel: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
						<%if(idata.itemList.length==3){%>\
							<div class="jsListItem commonSelectArea" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp" data-key="<%=key%>"  data-index="<%=i%>"  data-model="category">\
								<div class="commonIndexArea">\
									<div class="leftbox">\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									</div>\
									<div class="rightbox rightboxModel2">\
										<a href="javascript:;"><img src="<%=idata.itemList[1].imageUrl%>"></a>\
										<a href="javascript:;"><img src="<%=idata.itemList[2].imageUrl%>"></a>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<span class="jsTowardsUp">↑</span>\
										<span class="jsRemoveGroup">X</span>\
										<span class="jsTowardsDown">↓</span>\
									</div>\
								</div>\
							</div>\
						<%}else if(idata.itemList.length==1){%>\
							<div class="jsListItem commonSelectArea" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp"  data-key="<%=key%>" data-index="<%=i%>"  data-model="category">\
								<div class="commonIndexArea">\
									<div class="allbox">\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<span class="jsTowardsUp">↑</span>\
										<span class="jsRemoveGroup">X</span>\
										<span class="jsTowardsDown">↓</span>\
									</div>\
								</div>\
							</div>\
						<%}else if(idata.itemList.length==2){%>\
							<div class="jsListItem commonSelectArea" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp" data-key="<%=key%>" data-index="<%=i%>" data-json=\'<%=idata.json%>\'  data-model="category">\
								<div class="commonIndexArea">\
									<div class="leftbox">\
										<a href="javascript:;"><img src="<%=idata.itemList[0].imageUrl%>"></a>\
									</div>\
									<div class="rightbox">\
										<a href="javascript:;"><img src="<%=idata.itemList[1].imageUrl%>"></a>\
									</div>\
									<div class="handle" data-groupid="<%=idata.groupId%>">\
										<span class="jsTowardsUp">↑</span>\
										<span class="jsRemoveGroup">X</span>\
										<span class="jsTowardsDown">↓</span>\
									</div>\
								</div>\
							</div>\
						<%}%>\
					<%}%>',
        categoryEmptyModel: '<div class="jsListItem jsListItemEmpty commonSelectArea" data-type="rightCategoryTemp" data-model="category">\
				<div class="commonIndexArea">\
					<div class="leftbox">\
						<a href="javascript:;"></a>\
					</div>\
					<div class="rightbox  rightboxModel2">\
						<a href="javascript:;"></a>\
						<a href="javascript:;"></a>\
					</div>\
					<div class="handle">\
						<span class="jsRemoveGroup">X</span>\
					</div>\
				</div>\
			</div>',
        /////////////////////////////////////////////////////////////////////////////////////////右侧
        // 右侧广告编辑模版
        rightADTemp: '<div class="clearfix">\
						<h2 class="title">广告图维护 \
						<!--<input type="checkbox" checked="true" name="ad" value="" style="width: 20px;" id="ADTempCheck"/>启用幻灯片-->\
                        </h2>\
						<div class="wrapBtn">\
							<span class="jsCancelBtn">取消</span>\
							<span class="jsSaveADBtn">保存</span>\
						</div>\
					</div>',
        // 右侧广告详细项
        adItemListTemp: '<%if(list&&list.length){\
						for(var i=0;i<list.length;i++){ var idata = list[i];%>\
							<div class="jsAreaItem uploadArea clearfix"  data-objectid="<%=idata.objectId %>"  data-adid="<%=idata.adId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-imageurl="<%=idata.imageUrl%>">\
								<div class="wrapPic">\
									<p><img src="<%=idata.imageUrl%>"></p>\
									<span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
								</div>\
								<div class="wrapInput">\
									<p class="typeContainer mb-15">\
									<select class="jsTypeSelect">\
										<option value ="ad-3" <%if(idata.typeId==3){%>selected="selected"<%}%> >产品</option>\
										<option value ="ad-2" <%if(idata.typeId==2){%>selected="selected"<%}%> >自定义链接</option>\
									</select>\
									</p>\
									<p class="infoContainer clearfix">\
										<input class="jsNameInput" type="text" <%if(idata.typeId==2){%>value="<%=idata.url%>"<%}else{%>value="<%=idata.objectName%>" disabled="disabled"<%}%>>\
										<span class="jsChooseBtn tagBtn">选择</span>\
									</p>\
									<p><a href="javascript:;" class="jsDeladItemTemp delBtn">删除</a></p>\
								</div>\
							</div>\
						<%}%>\
					<%}else{%>\
						<%for(var i=0;i<length;i++){%>\
							<div class="jsAreaItem uploadArea clearfix">\
								<div class="wrapPic">\
									<p><span class="size">图片尺寸640*210</span></p>\
									<span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
								</div>\
								<div class="wrapInput">\
									<p class="typeContainer mb-15">\
									<select class="jsTypeSelect">\
										<option value ="ad-3">产品</option>\
										<option value ="ad-2">自定义链接</option>\
									</select>\
									</p>\
									<p class="infoContainer clearfix">\
										<input class="jsNameInput" type="text" value="">\
										<span class="jsChooseBtn tagBtn">选择</span>\
									</p>\
									<p><a href="javascript:;" class="jsDeladItemTemp delBtn">删除</a></p>\
								</div>\
							</div>\
						<%}%>\
					<%}%>',


        // 右侧活动编辑模板
        rightEventTemp: '<div class="clearfix">\
                            <h2 class="title">模板设计</h2>\
                            <div class="wrapBtn">\
                                <span class="jsCancelBtn">取消</span>\
                                <span class="jsSaveEventBtn">保存</span>\
                            </div>\
                        </div>\
                        <div class="jsTabContainer menuLayer clearfix">\
                            <%for(var i=0;i<eventTypeList.length;i++){var ievent = eventTypeList[i];%>\
                                <span class="jsTab <%if(i==0){%> on<%}%>" data-typeid="<%=ievent.key%>"\
                                    <%if(list){for(var j=0;j<list.length;j++){var idata= list[j];if(idata.typeId == ievent.key){%>\
                                         data-currentitemid="<%=idata.itemId%>" data-displayindex="<%=idata.displayIndex%>" data-eventid ="<%=idata.eventId%>" data-value=\'<%=idata.json%>\'\
                                    <%}}}%>\
                                ><%=ievent.value%></span>\
                            <%}%>\
                        </div>\
                        <div class="clearfix">\
                            <h2 class="title">选择商品</h2>\
                        </div>\
                        <div class="typeSelectList">\
                            <ul class="jsGoodsList">\
                            </ul>\
                        </div>\
                        <div class="pageWrap" style="display:none;">\
                            <div class="pagination">\
                                <a href="javascript:;" class="first" data-action="first">&laquo;</a>\
                                <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>\
                                <input type="text" readonly="readonly" />\
                                <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>\
                                <a href="javascript:;" class="last" data-action="last">&raquo;</a>\
                            </div>\
                        </div>',
        // 右侧活动编辑模板  商品刘表
        goods: '<%for(var i=0;i<itemList.length;i++){var idata=itemList[i];%>\
				<li class="jsGoodsItem" data-itemid="<%=idata.itemId%>" data-eventid="<%=idata.eventId%>">\
					<label class="clearfix">\
					<span class="wrapRadio"><input type="radio" name="pic" value=\'<%=idata.json%>\' <%if(currentItemId==idata.itemId){%>checked = "checked"<%}%> ></span>\
					<p class="wrapPic"><img src="<%=idata.imageUrl%>" alt=""></p>\
					<div class="goodsInfo">\
						<p><%=idata.itemName%></p>\
						<span class="price">￥<%=idata.salesPrice%></span>\
					</div>\
					</label>\
				</li>\
			<%}%>',
        goodsKill: '<div class="typeSelectList"><ul class="jsGoodsList">\
        <%for(var i=0;i<itemList.length;i++){var idata=itemList[i];%>\
				<li class="jsGoodsItem" data-imageurl="<%=idata.imageUrl%>" data-itemid="<%=idata.itemId%>" data-itemName="<%=idata.itemName%>" data-eventid="<%=idata.eventId%>">\
					<label class="clearfix">\
					<span class="wrapRadio"><input type="radio" name="pic" value=\'<%=idata.json%>\' <%if(currentItemId==idata.itemId){%>checked = "checked"<%}%> ></span>\
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
        rightSecondKillTemp:'<div class="clearfix">\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveKillBtn">保存</span>\
							    </div>\
						    </div>',
        SecondKillModelTemp:'<div class="clearfix">\
							    <h2 class="title">模板设置</h2>\
						    </div>\
                            <div class="jsSectionCTabContainer moduleType clearfix"  data-model="kill">\
							    <span class="jsTab one" data-model="1"><em></em></span>\
							    <span class="jsTab two" data-model="2"><em></em></span>\
							    <span class="jsTab four on" data-model="3"><em></em></span>\
						    </div>',
        SencondKillListTemp: '<div class="jsAreaTitle clearfix"><h2 class="title line30">选择商品类型</h2>\
                                     <div class="wrapInput" >\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
												     <option value ="sk-2"<%if(shopType==2){%>selected="selected"<%}%>>掌上秒杀</option>\
												    <option value ="sk-1"<%if(shopType==1){%>selected="selected"<%}%>>疯狂团购</option>\
											    </select>\
											    </p>\
										    </div>\
                              </div>\
        <div class="jsAreaTitle clearfix"><h2 class="title">图片维护</h2></div>\
                                <%if(arrayList&&arrayList.length){\
								    for(var i=0;i<arrayList.length;i++){ var idata = arrayList[i];%>\
									    <div class="jsAreaItem uploadArea clearfix"  data-itemid="<%=idata.itemId %>" data-eventid="<%=idata.eventId %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.itemName%>" data-eventAreaItemId="<%=idata.eventAreaItemId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <p><img src="<%=idata.imageUrl%>"></p>\
										    </div>\
										    <div class="wrapInput">\
											   <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" value="<%=idata.itemName%>">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
								    <%for(var i=0;i<length;i++){%>\
									    <div class="jsAreaItem uploadArea clearfix">\
										    <div class="wrapPic">\
											    <p><span class="size">图片尺寸<%if(length==1){%>600X240<%}else if(length==2){%>300X300<%}else{%><%if(i==0){%>300X300<%}else if (i==1){%>300X300<%}else{%>300X300<%}%><%}%></span></p>\	\
											 </div>\
										    <div class="wrapInput">\
											    <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" value="">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}%>',
        // 右侧分类编辑模板
        rightCategoryTemp: '<div class="clearfix">\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveCategoryBtn">保存</span>\
							    </div>\
						    </div>',
        categoryModelTemp: '<div class="clearfix">\
							    <h2 class="title">模板设置</h2>\
						    </div>\
                            <div class="jsSectionCTabContainer moduleType clearfix">\
							    <span class="jsTab one" data-model="1"><em></em></span>\
							    <span class="jsTab two" data-model="2"><em></em></span>\
							    <span class="jsTab three on" data-model="3"><em></em></span>\
						    </div>',
        // 分类详细项
        categoryItemListTemp: '<div class="jsAreaTitle clearfix"><h2 class="title">图片维护</h2></div>\
                                <%if(itemList&&itemList.length){\
								    for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
									    <div class="jsAreaItem uploadArea clearfix"  data-objectid="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
												    <option value ="cg-1"<%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
												    <option value ="cg-2"<%if(idata.typeId==2){%>selected="selected"<%}%>>产品</option>\
											    </select>\
											    </p>\
											    <p class="infoContainer clearfix">\
												    <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
								    <%}%>\
							    <%}else{%>\
								    <%for(var i=0;i<length;i++){%>\
									    <div class="jsAreaItem uploadArea clearfix">\
										    <div class="wrapPic">\
											    <p><span class="size">图片尺寸<%if(length==1){%>600X300<%}else if(length==2){%>300X300<%}else{%><%if(i==0){%>321X294<%}else if (i==1){%>318X156<%}else{%>318X137<%}%><%}%></span></p>\
											    <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
												    <option value ="cg-1">商品类型</option>\
												    <option value ="cg-2">产品</option>\
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
        rightEntranceTemp: '<div class="clearfix">\
							    <div class="wrapBtn">\
								    <span class="jsCancelBtn">取消</span>\
								    <span class="jsSaveEntranceBtn">保存</span>\
							    </div>\
							    <div class="wrapRadio">\
							    <input type="radio" name="entranceStyle" class="radiobtn"  value="s1" /> 样式1\
                                <input type="radio" name="entranceStyle" class="radiobtn" value="s2" /> 样式2\
							    </div>\
						    </div>',
        // 右侧分类入口（C8区）详细项
        entranceItemListTemp: '<%var t=8;if(listLength){ %>\
                                    <%t=listLength;%>\
                                 <% }%>\
                                   <%for(var i=0;i<t;i++){%>\
                                    <%if(itemList&&itemList[i]){ var idata = itemList[i];%>\
                                        <div class="jsAreaItem uploadArea clearfix"  data-objectid="<%=idata.objectId%>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
										    <div class="wrapPic">\
											    <p><img src="<%=idata.imageUrl%>"></p>\
											    <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
										    </div>\
										    <div class="wrapInput">\
											    <p class="typeContainer mb-15">\
											    <select class="jsTypeSelect">\
												    <option value ="et-null">请选择</option>\
												    <option value ="et-8"<%if(idata.typeId==8){%>selected="selected"<%}%>>全部分类</option>\
												    <option value ="et-1"<%if(idata.typeId==1){%>selected="selected"<%}%>>商品类型</option>\
											    </select>\
											    <b class="delIcon">X</b>\
											    </p>\
											    <p class="infoContainer clearfix <%if(idata.typeId==8){%>hide<%}%>">\
												    <input class="jsNameInput" type="text" value="<%=idata.objectName%>">\
												    <span class="jsChooseBtn tagBtn">选择</span>\
											    </p>\
										    </div>\
									    </div>\
                                    <%}else{ %>\
                                        <div class="jsAreaItem uploadArea clearfix">\
                                            <div class="wrapPic">\
                                                <p><span class="size">图片尺寸160X144</span></p>\
                                                <span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
                                            </div>\
                                            <div class="wrapInput">\
                                                <p class="typeContainer mb-15">\
                                                <select class="jsTypeSelect">\
												    <option value ="et-null">请选择</option>\
                                                    <option value ="et-8">全部分类</option>\
                                                    <option value ="et-1">商品类型</option>\
                                                </select>\
											    <b class="delIcon" style="display:none;">X</b>\
                                                </p>\
                                                <p class="infoContainer clearfix"  style="display:none;">\
                                                    <input class="jsNameInput" type="text" value="">\
                                                    <span class="jsChooseBtn tagBtn">选择</span>\
                                                </p>\
                                            </div>\
                                        </div>\
                                    <%} %>\
                                <%} %>',

        // 弹层：分类
        category: '<%for(var i=0;i<categoryList.length;i++){var idata=categoryList[i]; %>\
						<tr class="categoryItem" data-id="<%=idata.categoryId %>" data-name="<%=idata.categoryName %>">\
							<td><%=idata.categoryName %></td>\
						</tr>\
					<%} %>',
        // 弹层：商品
        product: '<%for(var i=0;i<itemList.length;i++){var idata=itemList[i]; %>\
				<tr class="productItem" data-id="<%=idata.itemId%>" data-name="<%=idata.itemName%>">\
					<td><%=idata.categoryName%></td>\
					<td><%=idata.itemName%></td>\
				</tr>\
			<%}%>'
    };
    return temp;
});