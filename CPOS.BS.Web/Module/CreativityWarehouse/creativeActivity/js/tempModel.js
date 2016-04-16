define(function($) {
    var temp = {
        pageList: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
                        <tr>\
                            <td><%=idata%></td>\
                          </tr>\
                    <%}%>',

        addStyle: '<form id="optionForm"><input name="ThemeId" value="" type="text" style="display: none" data-flag="编辑时候用到"><div class="commonSelectWrap">\
    <em class="tit">风格名称：</em>\
    <div class="searchInput" style="width:270px;" >  \
        <input  data-flag="" name="ThemeName" type="text" value="" class="easyui-validatebox" data-options="required:true" /> \
        </div> \
        </div>\
        <div class="lineText"> \
        <div class="tit">风格图片：</div> \
        <div class="inputBox">\
        <div class="logo" style="width:172px; height:270px;"  data-name="ImageUrl"><img data-imgcode="imgDefined" src="../../styles/images/newYear/imgDefault.png"></div>\
        <div class="uploadTip" style="left: 188px;">  \
        <div class="uploadBtn btn"> \
        <em class="upTip">上传</em> \
        <div class="jsUploadBtn" data-imgcode="imgDefined" data-msg="请上传一张风格图片" ></div> \
        </div><!--uploadBtn--> \
        <div class="tip">建议尺寸：172px*270px，大小：50K以内</div>\
    </div> <!--uploadTip--> \
    </div> <!--inputBox-->\
    </div><!--lineText--> \
    <div class="commonSelectWrap"> \
        <em class="tit">模板链接：</em>\
    <div class="searchInput" style="width:270px;"> \
        <input  data-flag="" name="H5Url" type="text" value=""  class="easyui-validatebox" data-options="required:true,validType:\'url\'"/>\
        </div>  \
        </div></form>',
        addActivity: '<form id="optionForm"> <div class="commonSelectWrap"> \
    <em class="tit">选择风格：</em> \
    <div class="selectBox">  \
        <input  data-flag="" name="ThemeId" id="style" type="text" value="" class="easyui-combobox" data-options="width:200,height:30,validType:\'selectIndex\'"/>\
        </div> \
        </div> \
        <div class="lineText"> \
        <div class="commonSelectWrap">\
        <em class="tit">目标方案：</em> \
         <div class="selectBox"  style="width:160px">\
        <input  data-flag="" name="InteractionType" type="text" id="InteractionType" value="" class="easyui-combobox" data-options="width:160,height:30,validType:\'selectIndex\'"/>\
        </div> \
        </div>\
        <div class="commonSelectWrap" >\
        <em class="tit">活动工具：</em> \
        <div class="selectBox" style="width:160px"> \
        <input  data-flag="" name="DrawMethodId" type="text" id="DrawMethodId" value="" class="easyui-combobox" data-options="width:160,height:30,validType:\'selectIndex\'"/> \
        </div>\
        </div>\
        <div class="commonSelectWrap" > \
        <em class="tit">选择活动：</em>\
        <div class="selectBox" style="width:160px">\
           <input  data-flag="" name="LeventId" id="LeventId" type="text" value="" class="easyui-combobox" data-options="width:160,height:30,validType:\'selectIndex\'"/>\
         </div>\
           </div>\
        </div><form>'


	};
	return temp;
});