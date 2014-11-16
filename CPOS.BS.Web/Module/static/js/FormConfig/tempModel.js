define(function($) {
    var temp = {
        // 弹窗-删除表单
        delFormLayer: '<div id="delFormLayer"  class="commonPopupLayer">\
	                        <div class="titleWrap">\
    	                        <h2 class="title">删除表单</h2>\
                                <span class="closeBtn"></span>\
                            </div>\
                            <div class="contentWrap">\
    	                        <p class="textTit"><%=textTit%></p>\
                                <p class="btnWrap">\
        	                        <span class="<%=OKClass%> delBtn"><%=OKText%></span>\
        	                        <span class="<%=CancelClass%> editBtn"><%=CancelText%></span>\
                                </p>\
                            </div>\
                        </div>',

        // 弹窗-创建新表单
        addFormLayer: '<div id="addFormLayer" class="commonPopupLayer">\
	                    <div class="titleWrap">\
    	                    <h2 class="title">创建新表单</h2>\
                            <span class="closeBtn"></span>\
                        </div>\
                        <div class="contentWrap">\
    	                    <p class="textTit clearMargin">请给新表单命名</p>\
                            <p class="inputWrap"><input type="text" value=""/></p>\
                            <p class="btnWrap">\
        	                    <span class="jsAddBtn createdBtn">创建表单</span>\
        	                    <span class="jsCancelBtn cancelBtn">取消</span>\
                            </p>\
                        </div>\
                    </div>',
        // 弹窗-添加扩展属性
        extendPropertyLayer: '<div id="extendPropertyLayer" class="commonPopupLayer addAttrExtendLayer" style="display:none;">\
                                <div class="titleWrap">\
                                    <h2 class="title">添加扩展属性</h2>\
                                    <span class="closeBtn"></span>\
                                </div>\
                                <div class="formBox">\
    	                            <p class="clearfix"><span class="tit">属性名</span><input type="text"/></p>\
                                    <p class="clearfix">\
        	                            <span class="tit">类型</span>\
                                        <select>\
            	                            <option value="1">文本</option>\
                                            <option value="4">日期</option>\
                                            <option value="5">时间</option>\
                                            <option value="6">下拉框</option>\
                                            <option value="7">单选框</option>\
                                            <option value="8">多选框</option>\
                                            <option value="9">超文本</option>\
                                            <option value="10">密码框</option>\
                                        </select>\
                                    </p>\
                                    <p class="clearfix"><span class="tit">下拉选项</span><input type="text"/> <span class="tip">(多个请使用逗号分隔)</span></p>\
                                    <p class="clearfix"><span class="tit">属性说明</span><textarea class=""></textarea></p>\
                                    <p class="btnWrap">\
        	                            <span class="jsOKBtn createdBtn">完成</span>\
        	                            <span class="jsCancelBtn cancelBtn">取消</span>\
                                    </p>\
                                </div>\
                            </div>',
        // 表单列表页 表单项
		formList: '<%for(var i=0;i<itemList.length;i++){var idata=itemList[i]; %>\
				     <div class="commonCreatedLayer">\
    	                <p class="titName">\
        	                <span>会员卡领卡表单</span>\
                            <input class="titleInput" type="text" value="会员卡领卡表单">\
                        </p>\
                        <div class="btnWrap">\
        	                <span class="delBtn" data-count="<%=i%>">删除</span>\
        	                <span class="editBtn">编辑</span>\
                            <span class="renameBtn">重命名</span>\
                        </div>\
                    </div>\
			    <%}%>',
        //表单编辑页 左侧 属性项
        tempPPTItem:'<%for(var i=0;i<list.length;i++){var idata=list[i];%>\
                            <div class="jsPropertyListItem commonAttrArea" data-json="<%=idata.json%>">\
        	                    <p>\
                                    <span class="tit"><%=idata.ColumnDesc%><span>*</span></span>\
                                    <input class="inputBox" type="text">\
                                    <span class="checkBox">必填</span>\
                                    <span class="closeBtn"></span>\
                                </p>\
                            </div>\
                        <%}%>',
        //表单编辑页 右侧 属性项 
		pptItem: '<%if(list&&list.length){%>\
                    <%for(var i=0;i<list.length;i++){var idata=list[i]; %>\
                        <div class="jsPPTItem list" data-key="<%=idata.ColumnName%>" data-json="<%=idata.json%>">\
                	        <span class="tit"><%=idata.ColumnDesc%></span>\
                	        <span class="icon"></span>\
                        </div>\
                    <%}%>\
                <%}else{%>\
                    <div class="list not">暂无</div>\
                <%}%>',
	};
	return temp;
});