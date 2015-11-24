define(function($) {
	var temp = {
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
								<%if(list&&list.length){%>\
									<%for(var i=0;i<list.length;i++){%>\
											<span class="touchslider-nav-item <%if(i==0){%> touchslider-nav-item-current<%}%>"><%=i+1%></span>\
									<%}%>\
								<%}%>\
								</div>\
							</div>\
						</div>\
					</div>',

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
					</div>',
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
							<div class="jsListItem commonSelectArea" data-groupId="<%=idata.groupId%>" data-type="rightCategoryTemp" data-json=\'<%=idata.json%>\'  data-model="category">\
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
						<h2 class="title">广告图维护</h2>\
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
										<option value ="ad-2" <%if(idata.typeId==2){%>selected="selected"<%}%> >资讯</option>\
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
									<p><span class="size">图片尺寸600*300</span></p>\
									<span class="uploadBtn">上传<input class="uploadImgBtn input" type="file" /></span>\
								</div>\
								<div class="wrapInput">\
									<p class="typeContainer mb-15">\
									<select class="jsTypeSelect">\
										<option value ="ad-3">产品</option>\
										<option value ="ad-2">资讯</option>\
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
							    <span class=" two" data-model="2"><em></em></span>\
							    <span class="jsTab three on" data-model="3"><em></em></span>\
						    </div>',
		// 分类详细项
		categoryItemListTemp: '<div class="jsAreaTitle clearfix"><h2 class="title">图片维护</h2></div>\
                                <%if(itemList&&itemList.length){\
								    for(var i=0;i<itemList.length;i++){ var idata = itemList[i];%>\
									    <div class="jsAreaItem uploadArea clearfix"  data-objectId="<%=idata.objectId %>" data-displayindex="<%=idata.displayIndex %>" data-typeid="<%=idata.typeId%>" data-name="<%=idata.objectName%>" data-categoryareaid="<%=idata.categoryAreaId%>" data-imageurl="<%=idata.imageUrl%>">\
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
											    <p><span class="size">图片尺寸<%if(length==1){%>600X300<%}else if(length==2){%>300X300<%}else{%><%if(i==0){%>300X300<%}else{%>300X150<%}%><%}%></span></p>\
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