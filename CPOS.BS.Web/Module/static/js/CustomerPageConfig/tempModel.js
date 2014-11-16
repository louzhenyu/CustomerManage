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
        pageInfo:'<div class="commonItem clearfix">\
                        <span class="tit">页面标题</span>\
                        <div class="handleWrap">\
                            <input type="text" class="inputBox jsNodeValue" value=\'<%=title%>\' data-node="1" />\
                        </div>\
                    </div>\
                    <div class="commonItem clearfix">\
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
                        <div class="commonItem clearfix" data-key="<%=idata.Key%>">\
                            <span class="tit"><%=idata.Name%></span>\
                            <%if(idata.valueType =="option"){%>\
                                <div class="handleWrap">\
                                    <select class="selectBox jsParamValue" data-key="<%=idata.Key%>" >\
                                        <%for(var j=0;j<idata.values.length;j++){%>\
                                            <option <%if(idata.values[j]==idata.defaultValue){%> selected <%}%> value=\'<%=idata.values[j]%>\'><%=idata.valuesText[j]%></option>\
                                        <%}%>\
                                    </select>\
                                </div>\
                            <%}else if(idata.valueType =="string"){%>\
                                <div class="handleWrap">\
                                    <input type="text" class="inputBox jsParamValue" value=\'<%=idata.defaultValue%>\' data-key="<%=idata.Key%>" />\
                                </div>\
                            <%}else if(idata.valueType =="image"){%>\
                                <div class="handleWrap uploadWrap">\
                                    <p class="viewPic jsParamValue" data-key="<%=idata.Key%>" >\
                                    <%if(idata.defaultValue){%><img src="<%=idata.defaultValue%>" /><%}else{%>暂无<%}%>\
                                    </p>\
                                    <div class="info">\
                                        <p class="exp">建议上传尺寸为536*300,文件大小不超过100k的图片。</p>\
                                        <buttom class="jsUploadBtn uploadBtn" value="上传">上传</buttom>\
                                    </div>\
                                </div>\
							<%}else if(idata.valueType =="Array"||idata.valueType =="ArraySimple"){%>\
								<div class="handleWrap uploadWrap jsParamValue" data-key="<%=idata.Key%>">\
									<%=idata.html%>\
								</div>\
                            <%}else{%>\
                                <div class="handleWrap">\
                                    未知类型<%=idata.valueType%>\
                                </div>\
                            <%}%>\
                        </div>\
                    <%}%>',
        paramJsonString: '<div style="margin:5px 0;">\
                            <span style="display: inline-block;width: 80px;"><%=name%></span>\
                            <input class="jsTrigger" type="text" data-key="<%=key%>" value=\'<%=defaultValue%>\' />\
                        </div>',
        paramJsonImage: '<div style="margin:5px 0;">\
                            <span style="display: inline-block;width: 80px;"><%=name%></span>\
                            <span class="jsTrigger" data-key="<%=key%>" data-value="<%=defaultValue%>" style="display:inline-block;width: 179px;max-height: 100px;line-height: 100px;margin-right: 30px;text-align: center;font-size: 16px;background: #d0d0d0;color: #fff;" >\
                            <%if(defaultValue){%><img src="<%=defaultValue%>" style="max-width:100%;max-height:100%;" /><%}else{%>暂无<%}%>\
                            </span>\
                            <span style="display:inline-block;width: 179px;height: 100px;">\
                                <span>建议上传尺寸为536*300,文件大小不超过100k的图片。</span>\
                                <buttom class="jsUploadBtn uploadBtn" value="上传">上传</buttom>\
                            </span>\
                        </div>',
        paramJsonOption: '<div style="margin:5px 0;">\
                            <span style="display: inline-block;width: 80px;"><%=name%></span>\
                            <select class="jsTrigger" data-key="<%=key%>"  >\
                                <%for(var j=0;j<option.values.length;j++){%>\
                                    <option <%if(option.values[j]==defaultValue){%> selected <%}%> value=\'<%=option.values[j]%>\'><%=option.valuesText[j]%></option>\
                                <%}%>\
                            </select>\
                        </div>'

	};
	return temp;
});