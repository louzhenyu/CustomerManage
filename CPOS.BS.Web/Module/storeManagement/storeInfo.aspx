<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta charset="UTF-8" />
  <title>门店信息</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="<%=StaticUrl+"/module/storeManagement/css/storeInfo.css?v=0.4"%>" rel="stylesheet" type="text/css" />
  <style type="text/css">
a:hover{color:#fff;}
#contentArea{background:#f4f8fa;min-height:630px !important;}
#section{padding:30px 20px 0 20px;}
.contentArea-info{border-radius:5px 5px 0 0;}
.panelDiv{margin-top:0;}
.contentArea-info .title{border:1px solid #d8d8d8;}
.contentArea-info .title li{width:50%;height:55px;}
.contentArea-info .title .itemNav{display:inline-block;height:55px;margin:0 auto;}
.contentArea-info .title li em{float:left;width:33px;height:55px;}
.contentArea-info .title li span{float:left;height:55px;line-height:55px;margin-left:10px;}
.contentArea-info .title .one em{background:url(images/icon-end1.png) no-repeat center center;}
.contentArea-info .title .one.on em{background:url(images/icon-on1.png) no-repeat center center;}
.contentArea-info .title .two{border-right:none;}
.contentArea-info .title .two em{background:url(images/icon-end2.png) no-repeat center center;}
.contentArea-info .title .two.on em{background:url(images/icon-on2.png) no-repeat center center;}

.borderArea{border:1px solid #d8d8d8;border-top:none;background:#fff; overflow:hidden;}
.clearBorder .combo,.clearBorder .numberbox{border:none;background:none;}
.textbox-invalid {border-color:#d0d5d8;background-color:#fff;}
.textbox-addon-right {right:5px !important;top:1px;}
.inlineBlockArea {display: inline-block;width:100%;min-height:351px;padding:40px 0;}


.panel-body-noheader .combobox-item{font-size:12px;}
/*奖品配置，弹出*/
.jui-dialog-redPackage{width:636px;height:470px;position:fixed;top:90px;margin-left:-318px;}
.jui-dialog-tit{height:55px;}
.jui-dialog-tit h2{font:16px/55px "Microsoft YaHei";color:#66666d;}
.jui-dialog-close{position:absolute;top:0px;right:18px;width:30px;height:54px;background:url(images/icon-close-btn.png) no-repeat center center;cursor:pointer;}
.jui-dialog .tit{width:250px;}
.jui-dialog .redPackageContent{padding:10px 0 30px 0;}
.jui-dialog .searchInput{width:190px;}
.jui-dialog .commonSelectWrap{float:none;display:block;margin:20px 0 2px 0;}
.jui-dialog .hint-exp{padding-top:5px;text-indent:30px;text-align:center;font-size:14px;color:#999;}
.jui-dialog .btnWrap{padding:30px 0 0 0;}
.jui-dialog .commonBtn{width:150px;height:45px;line-height:45px;}
.jui-dialog .cancelBtn{border:none;background:#ccc;color:#fff;}




#storeIntroduce{clear:left;height:auto;}
#storeIntroduce .searchInput{width:505px;height:92px;}
#storeIntroduce .searchInput textarea{display:block;width:100%;height:100%;padding:5px;font-size:12px;}
#longitude{background:#f2f2f2;}
.coordQueryBtn{display:inline-block;margin-left:10px;}


.addStorePicArea{padding:40px 50px 50px 50px;}
.addStorePicArea .tit{font-size:15px;color:#999;}
.addStorePicArea .tipText{font-size:15px;color:#f00}
.addStorePicArea .tit span{color:#ccc;}
.storePicBox{display:inline-block;min-height:83px;padding:14px 0 7px 0;}
.storePicBox > p{float:left;margin-right:20px;cursor:pointer;}
.storePicBox .picBox{width:85px;height:62px;margin-bottom:10px;position:relative;}
.storePicBox .picBox img{display:block;width:100%;height:100%;border-radius:5px;}
.storePicBox .picBox em{display:none;position:absolute;top:0;left:0;width:100%;height:100%;border-radius:5px;background:rgba(51,51,51,0.5) url(images/icon-trash.png) no-repeat center center;}
.storePicBox .picBox:hover em{display:block;}
.commonSelectWrap .searchInput input{font-size:12px;}

/*插件的上传按钮*/
form.ke-upload-area.ke-form{opacity:0;cursor:pointer;}
.addPicBtn.uploadImgBtn,.addPicBtn{float:left;width:85px;height:62px;background:url(images/icon-addPic.png) no-repeat center center;cursor:pointer;}
.ke-upload-file,.ke-button-common,input[type="button"],input[type="submit"],input[type="reset"], input[type="file"]::-webkit-file-upload-button,button{cursor:pointer !important;}
.ke-upload-file{width:85px !important;height:62px;}
.ke-button-common{display:none;}

/*配置二维码*/
.setQRcodeArea{display:inline-block;min-height:420px;padding:50px 40px;}
.setQRcodeArea .qrInfoBox{float:left;width:430px;}
.setQRcodeArea .qrSetBox{min-height:300px;margin-left:450px;padding-top:30px;}
.setQRcodeArea .qrBg{width:300px;height:300px;margin-left:60px;background:url(images/qr-bg.png) no-repeat 0 0;}
.setQRcodeArea .qrBg img{display:inline-block;width:210px;height:210px;margin:50px 0 0 45px;background:#fff;}
.setQRcodeArea .qrBtnWrap{text-align:center;}
.setQRcodeArea .qrBtnWrap a{display:inline-block;width:200px;height:50px;}
.setQRcodeArea .qrCreateBtn{background:url(images/qr-createBtn.png) no-repeat 0 0;}
.setQRcodeArea .qrDownBtn{margin-left:20px;background:url(images/qr-downBtn.png) no-repeat 0 0;}
.setQRcodeArea .qrStoreName{padding:15px 0 24px 0;text-align:center;font-size:15px;color:#999;}
.qrSetBox .commonSelectWrap{margin:0 0 38px 0;float:none;display:block;}
.qrSetBox .commonSelectWrap .tit{width:88px;font-size:12px;color:#999;}


/*图文消息*/
.imgTextMessage{display:none;margin-left:92px;}		 
.imgTextMessage h2{margin-bottom:10px;font-size:14px;font-weight:bold;color:#a2a2a2;}
.imgTextMessage .list{border-top:1px dashed #dbdbe5;background:#fbfbfb;}
.imgTextMessage .item{position:relative;height:80px;max-width:auto;min-width:auto;padding:7px 0 0 20px;border-bottom:1px dashed #dbdbe5;}
.imgTextMessage .picWrap{float:left;width:90px;height:65px;margin-right:20px;background:#e7e7e7;}
.imgTextMessage .picWrap img{display:block;width:90px;height:65px;}
.imgTextMessage .textInfo{width:340px;white-space:nowrap;font-size:16px;color:#66666d;}
.imgTextMessage .textInfo span{display:block;height:22px;overflow:hidden;text-overflow:ellipsis;}
.imgTextMessage .textInfo .name{margin:2px 0 10px 0;}
.imgTextMessage .delBtn{display:none;position:absolute;top:5px;right:5px;width:25px;height:25px;background:url(images/closeBtn02.png) no-repeat center center;cursor:pointer;}

.imgTextMessage .item.hover{background:#ecf6ff;}
.imgTextMessage .item.hover .delBtn{display:block;}

.imgTextMessage .addBtn{float:left;margin:15px 0px;text-align:center;}

.commonTitleWrap{height:52px;padding:0 15px;border-radius:4px 4px 0 0;background:#f6f6f7;}
.commonTitleWrap h2{float:left;margin-top:15px;font-size:15px;font-weight:bold;color:#666;}

.commonTitleWrap .selectBox{width:210px;margin:9px 0 0 8px;padding:8px 0 8px 5px;border:none;border-radius:4px;background:#d0d4d7;color:#535b64;}
.commonTitleWrap span{float:right;width:95px;height:30px;line-height:30px;margin:11px 0 0 12px;border-radius:3px;text-align:center;color:#fff;cursor:pointer;}
.commonTitleWrap .releaseBtn{height:35px;line-height:35px;margin-top:9px;background:#7ac682;}
.commonTitleWrap .addBtn{background:#a2c3c8;}
.commonTitleWrap .delBtn{background:#ef5f4a;}

/*添加图文消息-弹层*/
.addImgMessagePopup{display:none;width:582px;padding-bottom:20px;position:fixed;_position:absolute;top:10px;left:50%;margin-left:-290px;border-radius:4px;background:#fff;z-index:9999;}
.addImgMessagePopup .commonTitleWrap{padding:0 20px;background:#f2f2fa;}
.addImgMessagePopup .commonTitleWrap span{width:80px;}
.addImgMessageWrap{height:95px;padding:32px 0 0 25px;border-radius:3px 3px 0 0;border-bottom:1px solid #cfcedc;background:#fff;}
.addImgMessageWrap .tit{float:left;height:30px;line-height:30px;font-size:15px;color:#66666d;}
.addImgMessageWrap input,.addImgMessageWrap select{float:left;width:280px;height:30px;_line-height:30px;margin:0 20px 0 12px;padding:0;text-indent:5px;border:1px solid #cecedc;border-radius:4px;background:#fff;}
.addImgMessageWrap select{width:175px;height:auto;padding:6px 0 6px 5px;cursor:pointer;}
.addImgMessageWrap .queryBtn{float:left;}
.addImgMessagePopup .radioBox{float:left;width:25px;height:25px;margin:16px 20px 0 0;background:url(images/on-icon3.png) no-repeat center center;cursor:pointer;}
.addImgMessagePopup .radioList{height:360px;overflow:auto;}
.addImgMessagePopup .item{position:relative;height:90px;padding:17px 0 0 20px;border-bottom:1px solid #cfcedc;}
.addImgMessagePopup .picWrap{float:left;width:78px;height:56px;margin-right:20px;background:#e7e7e7;}
.addImgMessagePopup .picWrap img{display:block;width:78px;height:56px;}
.addImgMessagePopup .textInfo{float:left;width:400px;font-size:16px;white-space: nowrap;color:#66666d;}
.addImgMessagePopup .textInfo span{display: block;height:20px;overflow:hidden; text-overflow:ellipsis;}
.addImgMessagePopup .textInfo .name{display:block;margin:2px 0 5px 0;}
.addImgMessagePopup .item.on .radioBox{background:url(images/on-icon.png) no-repeat center center;}
.addImgMessagePopup .item.on{background: #ecf6ff;}
/*
.show{display:inline-block;}
*/
.hide{display:none;}



</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="allPage" id="section" data-js="js/storeInfo.js?ver=0.4"> 
    <!-- 内容区域 -->
    <div class="contentArea-info">
      <!--个别信息查询-->
      <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
        <div class="title">
          <ul id="optPanel">
            <li data-flag="#nav01" class="on one"><div class="itemNav"><em></em><span>基本信息</span></div></li>
            <li data-flag="#nav02" class="two"><div class="itemNav"><em></em><span>二维码</span></div></li>
          </ul>
        </div>
        
        <!-- 设置组织层级 -->
        <div class="panelDiv" id="nav01" data-index="0">
        <form></form>
          <form id="nav0_1">
          	<div class="borderArea">
                <div class="inlineBlockArea">
					  <div class="commonSelectWrap">
                        <em class="tit">组织层级：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="上级组织层级" class="easyui-combobox" id="Parent_Unit_Id" data-flag="Parent_Unit_Id" name="Parent_Unit_Id" type="text" value="" data-options="required:true"  validType='selectIndex'>
                        </label>
                      </div>

                      <div class="commonSelectWrap">
                        <em class="tit">店名：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="店名" class="easyui-validatebox"  data-flag="Name" id="Name" name="Name" type="text" data-options="required:true"  value="" placeholder="请输入" >
                        </label>
                      </div>
                      <div class="commonSelectWrap">
                        <em class="tit">编码：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="店名" class="easyui-validatebox"  data-flag="Code" id="Code" name="Code" type="text" data-options="required:true"  value="" placeholder="请输入">
                        </label>
                      </div>

                      
                      <div class="commonSelectWrap cityLinkageBox">
                        <em class="tit">地址：</em>
                        <label class="searchInput clearBorder" style="width:70px;" >
                          <input class="easyui-combobox" id="provinceId" data-flag="provinceId" name="provinceId" type="text" value="" data-options="required:true,width:70,height:32" >
                        </label>
                        
                        <label class="searchInput clearBorder" style="width:70px;">
                          <input class="easyui-combobox" id="townId" data-flag="townId" name="townId" type="text" value="" data-options="required:true,width:70,height:32">
                        </label>
                        
                        <label class="searchInput clearBorder" style="width:70px;">
                          <input class="easyui-combobox" id="CityId" data-flag="CityId" name="CityId" type="text" value="" data-options="required:true,width:70,height:32">
                        </label>
                      </div>
                      

                      <div class="commonSelectWrap">
                        <em class="tit" style="width:62px;">详细地址：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="详细地址" class="easyui-validatebox"  data-flag="Address" id="Address" name="Address" type="text" data-options="required:true"  value="" placeholder="请输入">
                        </label>
                      </div>
                      
                      
                      <div class="commonSelectWrap">
                        <em class="tit">经纬度：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="经纬度" class="easyui-validatebox"  data-flag="longitude" id="longitude" name="longitude" type="text" data-options="required:true"  value="" placeholder="请查询">
                        </label>
                        <span class="commonBtn w80 coordQueryBtn">查询</span>
                      </div>
                      
                      
                      <div class="commonSelectWrap">
                        <em class="tit">联系人：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="联系人" class="easyui-validatebox"  data-flag="Contact" id="Contact" name="Contact" type="text" data-options="required:true"  value="" placeholder="请输入">
                        </label>
                      </div>
                      
                      
                      
                      <div class="commonSelectWrap">
                        <em class="tit">电话：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="电话" class="easyui-validatebox"  data-flag="Telephone" id="Telephone" name="Telephone" type="text" data-options="required:true"  value="" placeholder="请输入">
                        </label>
                      </div>

                      <div class="commonSelectWrap">
                        <em class="tit">类型：</em>
                        <label class="searchInput clearBorder">
                          <input data-text="类型" class="easyui-combobox" id="StoreType" data-flag="StoreType" name="StoreType" type="text" value="" data-options="required:true"  validType='selectIndex'>
                        </label>
                      </div>

                      
                      <div class="commonSelectWrap"  id="storeIntroduce">
                        <em class="tit">门店介绍：</em>
                        <label class="searchInput clearBorder">
                          <textarea data-text="门店介绍" class="easyui-validatebox" id="Remark" name="Remark" type="text" data-options=""  value="" placeholder="请输入"></textarea>
                        </label>
                      </div>
                      
                      

                      </div>
                </div>
            
            
            <div class="borderArea addStorePicArea">
            	<div class="tit">门店图片<span>（可以拍几张门店的实景照上传)</span></div>
            	<div class="storePicBox">
                	<!--<p class="picBox"><img src="images/pic.png" /><em></em></p>-->
                    <p class="addPicBtn uploadImgBtn"></p>
                </div>
                <p class="tipText">提示：图片尺寸至少85px*62px，最多上传三张图片哦</p>
            </div>
            
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn commonBtn saveBtn" data-flag="#nav02">保存</a>
            </div>
          </form>
        </div>
        
        <!--设置组织架构-->
        <div class="panelDiv" id="nav02" data-index="1">
          <form id="nav0_2">
            <div class="borderArea">
                <div class="setQRcodeArea">
                	<div class="qrInfoBox">
                    	<p class="qrBg"><img src="" /></p>
                        <p class="qrStoreName"></p>
                        <div class="qrBtnWrap">
                        	<a class="qrCreateBtn" href="javascript:;"></a>
                            <a class="qrDownBtn" href="javascript:;"></a>
                        </div>
                    </div>
                    <div class="qrSetBox">
                    	<div class="commonSelectWrap">
                            <em class="tit">二维码：</em>
                            <label class="searchInput clearBorder" style="width:380px;">
                              <input data-text="二维码" class="easyui-validatebox"  data-flag="WXCodeImageUrl" id="WXCodeImageUrl" name="WXCodeImageUrl" type="text" data-options=""  value="">
                            </label>
                        </div>
                        
                    	<div class="commonSelectWrap">
                            <em class="tit">消息类型：</em>
                            <label class="searchInput clearBorder" style="width:380px;">
                              <input data-text="消息类型" class="easyui-combobox" id="ReplyType" data-flag="ReplyType" name="ReplyType" type="text" value="" data-options=""  validType='selectIndex'>
                            </label>
                        </div>
                        <div class="commonSelectWrap selectItem" id="textContent" style="height:auto">
                            <em class="tit">文本内容：</em>
                            <label class="searchInput clearBorder" style="width:380px;height:102px;padding:5px 0">
                              <textarea data-text="文本内容" class="easyui-validatebox" id="textContentVal" name="Text" type="text" data-options=""  value="" placeholder="请输入"></textarea>
                            </label>
                        </div>
                        <div style="display:inline-block">
                        <div class="imgTextMessage selectItem" id="imageContentMessage" name="elems">
                            <h2>提示:按住鼠标左键可拖拽排序图文消息显示的顺序 <b>已选图文</b>&nbsp;&nbsp;<b id="hasChoosed" style="color: Red">0</b>&nbsp;&nbsp;个</h2>
                             <div class="list ui-sortable"></div>
                             <span class="addBtn commonBtn w80">添加</span>
                        </div>
                      	</div>
                      
                    </div>
                    
                </div>
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn commonBtn saveBtn" data-flag="#nav03" id="twoSaveBtn">保存</a>
            </div>
          </form>
        </div>
        <!--奖品配置End-->

      </div>
    </div>
  </div>
  

<!-- 添加图文消息-弹层 <div class="ui-mask hide" id="ui-mask"></div> -->
<div class="jui-mask"></div>
<div class="activeListPopupArea hide" id="chooseEvents"></div>
<div class="addImgMessagePopup" id="addImageMessage">
    <div class="commonTitleWrap">
        <h2>
            添加图文消息</h2>
        <span class="cancelBtn commonBtn w80">取消</span> <span class="saveBtn commonBtn w80">确定</span>
    </div>
    <div class="addImgMessageWrap clearfix">
        <span class="tit">标题</span>
        <input type="text" id="theTitle" class="inputName" />
        <span class="queryBtn commonBtn w80">查询</span>
    </div>
    <div class="radioList" id="imageContentItems">
    </div>
</div>
<div id="sortHelper" style="display: none;">&nbsp;</div>
<div id="dragHelper" style="position: absolute; display: none; cursor: move; list-style: none;
    overflow: hidden;">
</div>

<!--弹出的图文项-->
<script id="addImageItemTmpl" type="text/html">
<#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
		<div id="addImage_<#=(currentPage-1)*pageSize+i#>" data-id="addImage_<#=item.TestId#>" data-flag="<#=showAdd?'add':''#>" data-displayIndex="<#=i#>" data-obj="<#=JSON.stringify(item)#>" class="item">
			<em class="radioBox"></em>
			<p class="picWrap"><img src="<#=item.ImageUrl#>"></p>
			<div class="textInfo">
				<span class="name"><#=item.Title?item.Title:"未设置图文名称"#></span>
				<span><#=item.Text?item.Text:"未设置图文内容"#></span>
				<span class="delBtn"></span>
			</div>
		</div>
	<#}#>
</script>


<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=DpUcjN2pecxUdsjSXL4rV1iw"></script>
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script>


</asp:Content>
