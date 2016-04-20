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
        </div><form>',
        imgEdit:'         <div class="imgUploadPanel">\
    <div class="l">\
        <p><b>图片尺寸建议</b></p>\
    <p>格式：JPG、PNG      尺寸：<%=size%>     大小：100KB</p>\
    </div>  \
    <div class="lineText">\
        <div class="uploadBtn btn"> \
        <em class="upTip">上传图片</em>    \
        <div class="jsUploadBtn"  data-imgcode="EditImg"></div>\
        </div><!--uploadBtn--> \
        <p class="textBtn" data-imgurl="<%=imgDefault%>">恢复默认图片</p> \
        </div>                             \
       <div class="imgPanel"> <img src="<%=imgUrl%>" data-imgcode="EditImg"/></div>\
        </div>',
        setPrize:' <div class="lineText" style="border-bottom: 1px solid #e1e7ea;"> \
    <div class="commonSelectWrap" style="margin-left: 10px;"> \
        <em class="tit w120">选择奖品类型:</em> \
    <div class="searchInput bordernone"> \
        <input  class="easyui-combobox" id="selectType" data-options="width:160,height:32" name="BatId" type="text" value="0"/>\
    </div>\
    </div>  \
    </div> <!--lineText-->  \
    <div class="showPanel" style="display: none"> \
        <div class="optionBtn">\
        <span class="listName"></span>  \
        <div class="commonBtn icon icon_add r"  style="width: 40px; padding-left: 30px;">新增</div> \
        </div><!--optionBrn-->\
        <div class="tableWap"> \
        <div id="prizeListGrid"></div> \
        </div><!--tableWap--> \
        </div> <!--showPanel-->',
        imgText:'                     <div class="imgTextPanel">\
    <div class="titleInput"><input type="text" id="title" value="<%=title%>" placeholder="填写图文标题">   </div>\
        <div class="imgUploadPanel" style="height: auto;">\
        <div class="imgPanel"> <img src="<%=imgUrl%>" id="imgShare" data-imgcode="EditImg"/></div>\
        <div class="l"> \
        <p><b>图片尺寸建议</b></p>\
    <p>格式：JPG、PNG      尺寸：900*500px  大小：100KB</p>\
    </div> \
    <div class="lineText">\
        <div class="uploadBtn btn">\
        <em class="upTip">上传图片</em> \
        <div class="jsUploadBtn"  data-imgcode="EditImg"></div> \
        </div><!--uploadBtn--> \
        <!-- <p class="textBtn" data-imgurl="<%=imgUrl%>">恢复默认图片</p>-->\
        </div>\
        </div><!--imgUploadPanel-->\
        <div class="msgInput">\
        <textarea name="Summary" id="Summary" placeholder="请填写摘要"><%=summary%></textarea>\
        </div>\
        </div><!--imgTextPanel-->',
        shareInfo:'<div class="imgTextPanel" style="height: 300px;"><div class="imgUploadPanel"><div class="l"> <p><b>图片尺寸建议</b></p>\
        <p>格式：JPG、PNG      尺寸：200*200px;  大小：100KB</p>\
    </div> \
    </div><!--imgUploadPanel--> \
    <div class="lineText"> \
        <div class="l">   \
        <div class="imgPanel"> <img src="<%=imgUrl%>" id="imgShare" data-imgcode="EditImg"/></div>\
        <div class="uploadBtn btn" style="position: relative">  \
        <em class="upTip">上传图片</em>   \
        <div class="jsUploadBtn"  data-imgcode="EditImg"></div> \
        </div><!--uploadBtn-->                      \
        <!-- <p class="textBtn" data-imgurl="">恢复默认图片</p>--> \
        </div>                                 \
        <div class="texList" style="width: 390px; float: right;">\
        <div class="commonSelectWrap">                             \
        <label class="searchInput" style="width: 390px;">    \
        <input  name="CouponTypeName" type="text" id="title" value="<%=title%>" placeholder="填写分享标题">  \
        </label>         \
        </div>     \
        <div class="commonSelectWrap"> \
        <label class="searchInput" style="width: 390px; height: 100px">  \
        <textarea   type="text" id="Summary" placeholder="填写分享摘要"><%=summary%></textarea>\
        </label>\
        </div> \
        </div> <!--texList-->\
        </div> <!--l-->\
        </div><!--imgTextPanel-->',
        addIntegral:'<form id="win2OptionForm">\
    <div class="commonSelectWrap">  \
        <em class="tit w120">赠送积分值：</em> \
    <div class="searchInput bordernone"> \
        <input class="easyui-numberbox" data-options="min:1,precision:0,max:10000,required:true,width:200,height:32" name="integral" type="text"><em>积分</em>\
        </div> \
        </div> \
        <div class="commonSelectWrap">\
        <em class="tit w120">奖品总数：</em>\
    <div class="searchInput bordernone">\
        <input class="easyui-numberbox" data-options="min:1,precision:0,required:true,width:200,height:32" name="number" type="text" ><em>份</em>\
        </div>  \
        </div>\
        </form>',
        addNumber:'<form id="win2OptionForm" style="padding-top: 40px;">\
        <input name="id" type="text" style="display: none" data-flag="缓存id">\
    <div class="commonSelectWrap">  \
        <em class="tit w120">追加：</em> \
    <div class="searchInput bordernone"> \
        <input class="easyui-numberbox" data-options="min:1,precision:0,max:10000,required:true,width:200,height:32" name="number" type="text"><em id="addIntegralNumber">积分</em>\
        </div> \
        </div> \
        </form>',
        editText:'<div class="textColorPanel"> \
    <div class="panelTitle">注册有奖话术建议：</div> \
        <div class="desc">话术字数请在12个汉子字以内</div>\
        <div class="inputBox" ><input type="text" class="easyui-validatebox" data-options="required:true" value="<%=text%>" ></div>\
        </div>'


	};
	return temp;
});