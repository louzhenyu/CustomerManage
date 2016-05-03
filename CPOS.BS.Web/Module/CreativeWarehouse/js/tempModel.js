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
        imgEdit: '         <div class="imgUploadPanel">\
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
        setPrize: ' <div class="lineText" style="border-bottom: 1px solid #e1e7ea;"> \
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
        imgText: '                     <div class="imgTextPanel">\
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
        shareInfo: '<div class="imgTextPanel" style="height: 300px;"><div class="imgUploadPanel"><div class="l"> <p><b>图片尺寸建议</b></p>\
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
        addIntegral: '<form id="win2OptionForm">\
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
        addNumber: '<form id="win2OptionForm" style="padding-top: 40px;">\
        <input name="id" type="text" style="display: none" data-flag="缓存id">\
    <div class="commonSelectWrap">  \
        <em class="tit w120">追加：</em> \
    <div class="searchInput bordernone"> \
        <input class="easyui-numberbox" data-options="min:1,precision:0,max:10000,required:true,width:200,height:32" name="number" type="text"><em id="addIntegralNumber">积分</em>\
        </div> \
        </div> \
        </form>',
        editText: '<div class="textColorPanel"> \
    <div class="panelTitle">关注有奖话术建议：</div> \
        <div class="desc">话术字数请在12个汉子字以内</div>\
        <div class="inputBox" ><input type="text" class="easyui-validatebox" data-options="required:true" value="<%=text%>" ></div>\
        </div>',

        setEvent: ' <div class="addSales">\
    <form id="eventInfo">  \
        <div class="commonSelectWrap">  \
        <em class="tit w120">活动名称：</em>\
    <div class="searchInput">  \
        <input type="text" name="EventName"  class="easyui-validatebox" data-options="required:true,validType:\'maxLength[16]\'"  />  \
        </div>     \
        <em class="explain">例如：微商城7月上旬团购</em>   \
    </div>                                                \
    <div class="commonSelectWrap"> \
        <em class="tit w120">开始时间：</em>   \
    <div class="selectBox">       \
        <input type="text" class="easyui-datetimebox" name="BeginTime" id="BeginTime"  data-options="required:true,showSeconds:false,width:200,height:30" value="" />\
        </div> \
        <em class="explain">示例：2016-03-18  09:00</em>\
    </div>   \
    <div class="commonSelectWrap">  \
        <em class="tit w120">结束时间：</em> \
    <div class="selectBox">  \
        <input type="text" class="easyui-datetimebox" name="EndTime"  id="EndTime" data-options="required:true,showSeconds:false,width:200,height:30" validType="compareDate[$(\'#BeginTime\').datetimebox(\'getText\'),\'结束时间应该大于开始时间\']"   value="" />\
        </div> \
        <em class="explain">示例：2016-03-18  09:00</em> \
    </div> \
    <div class="commonSelectWrap" style="display: none"> \
        <em class="tit w120">上架状态：</em>\
    <div class="selectBox">  \
        <input type="text"  name="EventStatus" class="easyui-combobox" data-options="width:200,height:30,\
    valueField: \'label\',\
        textField: \'value\',\
        data: [{     \
        label: \'10\', \
        value: \'不在商城显示\'  \
    },{   \
        label: \'20\',  \
        value: \'在商城显示\', \
        selected:true \
    }]"  />  \
    </div>  \
    <em class="explain">上架商品显示在推出的活动中</em> \
        </div> \
        </form>\
        </div>',
        skuItemList:' <div class="skuPanel" data-id="<%=ItemId%>"> \
    <div class="top">  \
        <p class="title">商品信息</p> \
        <div class="productInfo"> \
        <p><%=ItemName%></p>\
        <div class="commonSelectWrap" >\
        <em class="tit">每人限购：</em> \
    <div class="selectBox" >\
        <input   class="easyui-numberbox" name="SinglePurchaseQty" value="<%=SinglePurchaseQty%>" data-options="min:0,precision:0,width:80,height:30" />\
        </div> <!--inputBox--> \
        </div><!--lineText--> \
        </div>   \
        </div>\
        <div class="bottom"> \
        <p class="title">规格信息</p> \
        <div class="skuList" id="skuList">\
        <%for(var i=0;i<SkuList.length;i++){var idata = SkuList[i];%> \
        <div class="skuObj"> \
        <div class="l"> \
        <div class="checkBox" data-MappingId="<%=idata.MappingId%>"  data-skuid="<%=idata.SkuID%>" data-select="<%=idata.IsSelected%>"><em></em><span><%=idata.SkuName%></span></div>  \
    </div> \
    <div class="r"> <div class="commonSelectWrap" > \
        <em class="tit">商品数量：</em>   \
    <div class="selectBox" > \
        <input   class="easyui-numberbox" name="Qty" data-options="value:<%=idata.Qty%>,min:0,precision:0,width:80,height:30,disabled:true" />\
        </div> <!--inputBox-->\
        </div><!--lineText--> \
        <div class="commonSelectWrap" >\
        <em class="tit" style="width: 85px;">已售数量基数：</em>\
    <div class="selectBox" >  \
        <input   class="easyui-numberbox" name="KeepQty" data-options="value:<%=idata.KeepQty%>,min:0,precision:0,width:80,height:30,disabled:true" /> \
        </div> <!--inputBox-->  \
        </div><!--lineText-->  \
        <div class="commonSelectWrap" > \
        <em class="tit">原价：</em>  \
    <div class="selectBox" >  \
<input name="price" class="skuPrice" readonly="readonly" value="<%=idata.price%>" type="text" />\
        </div> <!--inputBox-->\
        </div><!--lineText-->\
        <div class="commonSelectWrap" >  \
        <em class="tit" style="width: 85px;">活动价：</em> \
    <div class="selectBox" >\
        <input   class="easyui-numberbox" name="SalesPrice"  data-options="value:<%=idata.SalesPrice%>,min:0,precision:0,width:80,height:30,disabled:true" />\
        </div> <!--inputBox-->   \
        </div><!--lineText--></div>\
    </div> <!--skuObj-->\
     <%}%> \
    </div> \
    </div> \
    </div>',
        eventItemList:'  <%for(var i=0;i<ItemList.length;i++){var idata = ItemList[i];%> \
        <div class="product" data-id="<%=idata.EventItemMappingId%>" data-itemid="<%=idata.ItemID%>" data-itemname="<%=idata.ItemName%>">\
    <div class="l"><img src="<%=idata.ImageUrl%>" ></div> \
        <div class="r">   \
        <p class="tit"><%=$ItemNameSub(idata.ItemName)%> </p> \
    <p class="info" ><em>已有<%=idata.SalesPersonCount%>人参加</em> <span>  &nbsp&nbsp 原价 <i>￥<%=idata.Price%></i></span></p> \
    <p class="skill"  >抢购价 <em>￥<%=idata.SalesPrice%></em> </p>\
    <div class="textBtn"> 删除商品 </div> \
        </div> \
        <em class="editIconBtn"></em> \
        </div><!--product-->\
            <%}%>'

    };
    return temp;
});