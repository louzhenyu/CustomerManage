define(function($) {
    var temp = {
        pageList: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
                        <tr>\
                            <td><a href="pageEdit.aspx?key=<%=idata.PageKey%>&id=<%=idata.MappingID%>" class="editText" data-key="<%=idata.PageKey%>" data-id="<%=idata.MappingID%>">编辑</a></td>\
                            <td><%=idata.PageKey%></td>\
                            <td><%=idata.Title%></td>\
                            <td><%=idata.Version%></td>\
                            <td><%=idata.LastUpdateTime%></td>\
                          </tr>\
                    <%}%>',

        baseInfo: '<div class="areaWrap">\
        	            <div class="clearfix">\
            	            <div class="item">\
                	            <em>页面名：</em>\
                                <span><%=ModuleName%></span>\
                            </div>\
                            <div class="item">\
                	            <em class="tit">版本：</em>\
                                <span><%=Version%></span>\
                            </div>\
                        </div>\
                        <div class="clearfix">\
            	            <div class="item">\
                	            <em>更新人：</em>\
                                <span><%=Auther%></span>\
                            </div>\
                            <div class="item">\
                	            <em class="tit">时间：</em>\
                                <span><%=LastUpdateTime%></span>\
                            </div>\
                        </div>\
                        <div class="clearfix">\
            	            <div class="item width100">\
                	            <em>页面地址：</em>\
                                <span><a style="color:blue;" target="_blank" href="<%=PageUrl%>"><%=PageUrl%></a></span>\
                            </div>\
                        </div>\
                        <div class="clearfix">\
            	            <div class="item width100">\
                	            <em>Auth认证页面地址：</em>\
                                <span><%=PageAuthUrl%></span>\
                            </div>\
                        </div>\
                    </div>',
        pageInfo:' <div class="line">\
    <div class="commonInputWrap">  \
        <em class="tit">页面标题</em>  \
        <div class="inputBox">  \
          <input type="text" class="inputBox jsNodeValue" data-name="title" value=\'<%=title%>\' data-node="1" />\
        </div> <!--inputBox-->\
        </div> <!--commonInputWrap-->\
        </div><!--line-->\
                    <div class="commonItem clearfix " style="display: none">\
                        <span class="tit">页面模板</span>\
                        <div class="handleWrap w-495 jsNodeValue" data-value=\'<%=model%>\'  data-node="2" >\
                            <%for(var i=0;i<list.length;i++){var idata=list[i];%>\
                                <div class="jsPageMoudle moudle <%if(idata.id==model){%>on<%}%>" data-id="<%=idata.id%>">\
                                    <p><span class="mask"></span></p>\
                                </div>\
                            <%}%>\
                        </div>\
                    </div>',
        paramsList: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
                        <div class="line  <%if(idata.Key=="logo"){%>lineHalf<%}%> " >   <div class="commonInputWrap jsParamValue" data-key="<%=idata.Key%>" data-value=<%=idata.defaultValue%>>\
                            <em class="tit"><%=idata.Name%></em>\
                            <%if(idata.valueType =="option"){%>\
                            <div class="radioList" data-key="<%=idata.Key%>">\
                                  <%for(var j=0;j<idata.values.length;j++){%>\
                                   <div class="radio  <%if(idata.values[j]==idata.defaultValue){%>on<%}%>" data-name="<%=idata.Key%>" data-value="<%=idata.values[j]%>"><em></em><span><%=idata.valuesText[j]%></span></div>\
                                 <%}%>\
                             </div>\
                             </div> </div>\
                            <%}else if(idata.valueType =="string"){%>\
                                <div class="handleWrap">\
                                    <input type="text" class="inputBox jsParamValue" value=\'<%=idata.defaultValue%>\' data-key="<%=idata.Key%>" />\
                                </div>\
                                </div> </div>\
                            <%}else if(idata.valueType =="color"){%>\
                                 <div class="scheme"> \
    <span class="one"></span><span class="two"></span><span class="all"></span>\
        <div class="colorPanel" >\
        <div style="background-color:#960001;color:#fff;">文字</div>\
        <div style="background-color:#ff0101;color:#fff;">文字</div>\
        <div style="background-color:#fc9a01;color:#fff;">文字</div> \
        <div style="background-color:#fffe03;color:#EA8E39;">文字</div>\
        <div style="background-color:#04fd01;color:#274F13;">文字</div> \
        <div style="background-color:#01ffcd;color:#007765;">文字</div>\
        <div style="background-color:#00ffff;color:#44828F;">文字</div>\
        <div style="background-color:#0102fa;color:#fff;">文字</div>\
        <div style="background-color:#9c00fc;color:#fff;">文字</div>\
        <div style="background-color:#ff00fe;color:#fff;">文字</div>\
        <div style="background-color:#e7b8b0;color:#C94320;">文字</div> \
        <div style="background-color:#f4cccc;color:#E26563;">文字</div>\
        <div style="background-color:#fde4d0;color:#EA8E39;">文字</div>\
        <div style="background-color:#fff2cd;color:#EA8E39;">文字</div>\
        <div style="background-color:#d8ead2;color:#69A84F;">文字</div>\
        <div style="background-color:#c3f8f2;color:#007765;">文字</div>\
        <div style="background-color:#d2e0e3;color:#44828F;">文字</div>\
        <div style="background-color:#cfe2f3;color:#3E85C7;">文字</div>\
        <div style="background-color:#dad2e9;color:#674FA7;">文字</div>\
        <div style="background-color:#e7d3dc;color:#A54E79;">文字</div>\
        <div style="background-color:#DD7E6A;color:#fff;">文字</div>\
        <div style="background-color:#E99897;color:#660000;">文字</div>\
        <div style="background-color:#FAC99E;color:#7B3C07;">文字</div> \
        <div style="background-color:#FFE598;color:#7E6000;">文字</div>\
        <div style="background-color:#B6D7A8;color:#274F13;">文字</div>\
        <div style="background-color:#7DDED5;color:#007765;">文字</div>\
        <div style="background-color:#A3C4CB;color:#0C353B;">文字</div> \
        <div style="background-color:#9FC5E9;color:#073863;">文字</div> \
        <div style="background-color:#B3A6D4;color:#20124D;">文字</div> \
        <div style="background-color:#D7A5BE;color:#fff;">文字</div> \
        <div style="background-color:#C94320;color:#fff;">文字</div>\
        <div style="background-color:#E26563;color:#fff;">文字</div>\
        <div style="background-color:#F5B172;color:#fff;">文字</div>\
        <div style="background-color:#FDDA64;color:#fff;">文字</div>\
        <div style="background-color:#92C47F;color:#fff;">文字</div>\
        <div style="background-color:#39C6B5;color:#fff;">文字</div>\
        <div style="background-color:#79A3AF;color:#fff;">文字</div>\
        <div style="background-color:#6FA8DD;color:#fff;">文字</div>\
        <div style="background-color:#8D7CC3;color:#fff;">文字</div>\
        <div style="background-color:#BD7D9F;color:#fff;">文字</div>\
        <div style="background-color:#A61C00;color:#fff;">文字</div>\
        <div style="background-color:#CC0001;color:#fff;">文字</div>\
        <div style="background-color:#EA8E39;color:#fff;">文字</div>\
        <div style="background-color:#F1C332;color:#fff;">文字</div>\
        <div style="background-color:#69A84F;color:#fff;">文字</div>\
        <div style="background-color:#05A792;color:#fff;">文字</div>\
        <div style="background-color:#44828F;color:#fff;">文字</div>\
        <div style="background-color:#3E85C7;color:#fff;">文字</div>\
        <div style="background-color:#674FA7;color:#fff;">文字</div>\
        <div style="background-color:#A54E79;color:#fff;">文字</div>\
        <div style="background-color:#5B0F01;color:#fff;">文字</div>\
        <div style="background-color:#660000;color:#fff;">文字</div>\
        <div style="background-color:#7B3C07;color:#fff;">文字</div>\
        <div style="background-color:#7E6000;color:#fff;">文字</div>\
        <div style="background-color:#274F13;color:#fff;">文字</div>\
        <div style="background-color:#007764;color:#fff;">文字</div> \
        <div style="background-color:#0C353B;color:#fff;">文字</div> \
        <div style="background-color:#053863;color:#fff;">文字</div>\
        <div style="background-color:#20124D;color:#fff;">文字</div>\
        <div style="background-color:#000000;color:#fff;">文字</div> \
        <div title="点击关闭"><img style="margin: 0px;width: 31px;" src="images/slash.png" /></div> \
        <div style="background-color:#FFFFFF;color:#000">文字</div>\
        <div style="background-color:#E1E1E1;color:#000">文字</div> \
        <div style="background-color:#C3C3C3;color:#000">文字</div>  \
        <div style="background-color:#A5A5A5;color:#000;">文字</div> \
        <div style="background-color:#868789;color:#fff;">文字</div> \
        <div style="background-color:#696969;color:#fff;">文字</div> \
        <div style="background-color:#4C4A4B;color:#fff;">文字</div> \
        <div style="background-color:#232323;color:#fff;">文字</div> \
        <div style="background-color:#000000;color:#fff;">文字</div> \
        </div> \
        </div>\
                            <%}else if(idata.valueType =="imgArray"){%>\
                                <div class="bgImg">\
                                    <div class="uploadBtn btn"> \
                                        <em class="upTip">更换</em>\
                                         <div class="jsUploadBtn" ></div>  \
                                     </div> <div class="imgList"> \
                                     <%for(var k=0;k<idata.defaultValue.length;k++){%>\
                                        <% if(idata.defaultValue[k].imgUrl&&idata.defaultValue[k].imgUrl!="图片地址"){%>\
                                            <p class="imgOption" data-value="<%=idata.defaultValue[k].imgUrl%>"><span><%=$returnImgName(idata.defaultValue[k].imgUrl)%></span> <em class="imgDelBtn">删除</em></p>\
                                        <%}else{%>\
                                            <p class="imgOption" data-value=""><span></span> <em class="imgDelBtn">删除</em></p>\
                                        <%}%>\
                                     <%}%>\
                                     </div>\
                                 <p class="tip layerColor ">可上传<em><%=idata.arrayLength%>张</em>，支持格式：jpg、jpeg、png，尺寸640*1008PX，<br/>大小300K以内</p>\
                              </div>\
                              </div> </div>\
							<%}else if(idata.valueType =="image"){%>\
                                <div class="bgImg">\
                                    <div class="uploadBtn btn bColor"> \
                                        <em class="upTip">上传</em>\
                                         <div class="jsUploadBtn" ></div>  \
                                     </div> \
                                <div class="btn del" >删除</div>  \
                                <%if(idata.Key=="backgroundImage"){%>\
                                        <p class="tip">建议上传尺寸为640*1008,文件大小不超过100k的图片。</p>\
                                <%}else if(idata.Key=="logo"){%>\
                                        <p class="tip">建议上传尺寸为114*114,文件大小不超过100k的图片。</p>\
                                <%} else{%>\
                                        <p class="tip">建议上传尺寸为536*300,文件大小不超过100k的图片。</p>\
                                <%}%>\
                              </div>\
                              </div> </div>\
							<%}else if(idata.valueType =="Array"||idata.valueType =="ArraySimple"){%>\
						 <div class="menuPanel" data-flag="<%=idata.Key%>"> \
        <div class="menuUl"><ul>\
								<%if(idata.arrayLength){for(var k=0;k<idata.arrayLength;k++){%>\
								    <%if(idata.arrayLength<=5){%>\
                                         <li <%if(idata.arrayLength==4){%> style="width:93px; <%}%>"   class="borderNone  <%if(k==0){%> on lb<%}else if((k+1)%5==0){%>rb<%}%>" ><%=$menuToC(k)%></li>\
								     <%}else{%>\
								       <li style="width:93px;" class="<%if(k>3){%>borderNone<%}%> <%if(k==0){%> on lb<%}else if((k+1)%4==0){%>rb<%}%>" ><%=$menuToC(k)%></li>\
								     <%}%>\
								<%}%>\
							<%}%>\
        </ul></div></div></div>\
								<div class="line uploadWrap jsParamValue" data-key="<%=idata.Key%>">\
									<%=idata.html%>\
									\
                            <%}else{%>\
                                <div class="handleWrap">\
                                    未知类型<%=idata.valueType%>\
                                </div></div></div>\
                            <%}%>\
                        </div>\
                    <%}%>',
        paramJsonBool:  ' <div class="line" >\
    <div class="commonInputWrap" data-key="<%=key%>"  data-value="<%=defaultValue%>" > \
        <em class="tit"><%=name%></em>\
        <div class="radioList"> \
        <div class="radio <%if(defaultValue=="true"){%>on<%}%>" data-value="true" data-name="<%=radioName%>""><em></em><span>是</span></div>\
    <div class="radio <%if(defaultValue=="false"){%>on<%}%>" data-value="false" data-name="<%=radioName%>"><em></em><span>否</span></div>\
    </div>  <!--radioList--></div></div>',
        paramJsonString: '<div class="line" ">\
    <div class="commonInputWrap" data-key="<%=key%>" data-value="<%=defaultValue%>"> \
        <em class="tit"><%=name%></em> \
        <div class="inputBox"> \
        <input type="text" placeholder="<%=name%>" value="<%=defaultValue%>" /> \
        </div>\
        </div> <!--commonInputWrap-->\
        </div> ',
        paramJsonUrl:'<div class="line" >\
    <div class="commonInputWrap" data-key="<%=key%>" data-value="<%=jsonValue%>">\
        <em class="tit"><%=name%></em> \
        <div class="selectDom" >\
        <select  class="jsTypeSelect">\
        <option <%if(!defaultValue.typeid){%>selected<%}%> value ="cg-null">请选择</option> \
        <option <%if(defaultValue.typeid==1){%>selected<%}%> value="cg-1">商品品类</option> \
        <option <%if(defaultValue.typeid==2){%>selected<%}%> value="cg-2">商品</option> \
        <option <%if(defaultValue.typeid==3){%>selected<%}%> value="cg-3">自定义链接</option>\
        </select>\
        <p class="infoContainer jsAreaItem" data-typeid="<%=defaultValue.typeid%>" data-name="<%=defaultValue.name%>" data-id="<%=defaultValue.id%>">\
        <%if(defaultValue.typeid==3){%>\
        <input class="jsNameInput" type="text"  value="<%=defaultValue.url%>" />\
        <%}else{%>\
        <input class="jsNameInput" type="text" disabled="disabled" value="<%=defaultValue.name%>" />  \
       <%}%>\
        <span class="jsChooseBtn tagBtn" style="opacity: 1;">选择</span> \
        </p> \
        </div>  <!--radioList-->\
        </div> <!--commonInputWrap-->\
        </div> <!--line-->',
        paramJsonImage: '<div class="line" style="margin-top:-30px;">\
    <div class="commonInputWrap linkParamValue" data-key="<%=key%>" data-value="<%=defaultValue%>"> \
        <em class="tit"><%=name%></em>\
         <div class="bgImg">\
      <div class="uploadBtn btn "> \
                                        <em class="upTip">上传</em>\
                                         <div class="jsUploadBtn" ></div>  \
                                     </div> \
                 <p class="tip">支持格式：jpg、jpeg、png，尺寸148*376PX，大小100K以内。</p>\
                              </div>\
                              </div> </div>',
        paramJsonOption: '<div style="margin:5px 0;">\
                            <span style="display: inline-block;width: 80px;"><%=name%></span>\
                            <select class="jsTrigger" data-key="<%=key%>"  >\
                                <%for(var j=0;j<option.values.length;j++){%>\
                                    <option <%if(option.values[j]==defaultValue){%> selected <%}%> value=\'<%=option.values[j]%>\'><%=option.valuesText[j]%></option>\
                                <%}%>\
                            </select>\
                        </div>',
        paramJsonImageAdd: '<div class="line" style="margin-top:-30px;">\
    <div class="commonInputWrap linkParamValue" data-key="<%=key%>" data-value="<%=defaultValue%>"> \
        <em class="tit"><%=name%></em>\
         <div class="bgImg">\
      <div class="uploadBtn btn "> \
                                        <em class="upTip">上传</em>\
                                         <div class="jsUploadBtn" ></div>  \
                                     </div> \
        <div class="btn del" >删除</div>                              \
                 <p class="tip">支持格式：jpg、jpeg、png，尺寸148*376PX，大小100K以内。</p>\
                              </div>\
                              </div> </div>',
        paramJsonOption: '<div style="margin:5px 0;">\
                            <span style="display: inline-block;width: 80px;"><%=name%></span>\
                            <select class="jsTrigger" data-key="<%=key%>"  >\
                                <%for(var j=0;j<option.values.length;j++){%>\
                                    <option <%if(option.values[j]==defaultValue){%> selected <%}%> value=\'<%=option.values[j]%>\'><%=option.valuesText[j]%></option>\
                                <%}%>\
                            </select>\
                        </div>',
        // 弹层：分类
        category: '<%for(var i=0;i<categoryList.length;i++){var idata=categoryList[i]; %>\
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
			<%}%>',
        menuLi:'<%if(arrayLength){for(var k=0;k<arrayLength;k++){%>\
								    <%if(arrayLength<=5){%>\
                                         <li <%if(arrayLength==4){%> style="width:93px <%}%>"   class="borderNone  <%if(k==0){%> on lb<%}else if((k+1)%5==0){%>rb<%}%>" ><%=$menuToC(k)%></li>\
								     <%}else{%>\
								       <li style="width:93px" class="<%if(k>3){%>borderNone<%}%> <%if(k==0){%> on lb<%}else if((k+1)%4==0){%>rb<%}%>" ><%=$menuToC(k)%></li>\
								     <%}%>\
								<%}%>\
							<%}%>'

	};
	return temp;
});