<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta charset="UTF-8" />
  <title>活动详情</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="<%=StaticUrl+"/module/activityList/css/activityDetail.css?v=0.5"%>" rel="stylesheet" type="text/css" />
  <style type="text/css">
a:hover{color:#fff;}
#contentArea{background:#f4f8fa;}
#section{padding:30px 20px 0 20px;}
.contentArea-info{border-radius:5px 5px 0 0;}
.panelDiv{margin-top:0;}
.contentArea-info .title{border:1px solid #d8d8d8;}
.contentArea-info .title li{height:55px;line-height:55px;}
.contentArea-info .title li{width:33.33%;}
.borderArea{border:1px solid #d8d8d8;border-top:none;background:#fff;}
.clearBorder .combo,.clearBorder .numberbox{
	border: none;
	background: none;
}
.textbox-invalid {
	border-color: #d0d5d8;
	background-color: #fff;
}
.textbox-addon-right {
	right: 9px !important;
	top: 1px;
}
.inlineBlockArea {
	display: inline-block;
	width: 100%;
	padding: 21px 0;
}
.titleItem {
	display: inline-block;
	width: 100%;
	height: 41px;
	line-height: 39px;
	padding-left: 30px;
	font-size: 16px;
	border-top: 1px solid #e0e0e0;
	border-bottom: 1px solid #e0e0e0;
	background: #eaeff1;
	color: #333;
}
.dataTable{border:none;}
.dataTable tr{height:49px;line-height:48px;}
.dataTable th{text-align:center;font-size:14px;color:#4d4d4d;}
.dataTable td{font-size:13px;color:#666;}
.dataTable .tableHead{border-bottom:2px solid #07c8cf;}
.dataTable .addBtn{display:inline-block;  margin: 0 10px;width:20px;height:49px;background:url(images/icon-add.png) no-repeat center center;cursor:pointer;}
.dataTable .edit{display:inline-block;  margin: 0 10px;width:100%;height:49px;background:url(images/exit.png) no-repeat center center;cursor:pointer;}
.dataTable .delBtn{display:inline-block;  margin: 0 10px;width:20px;height:49px;background:url(images/delete.png) no-repeat center center;cursor:pointer;}

.addPrizeArea{height:73px;padding:20px;border-bottom:1px solid #d0d5d8;}
.commonHandleBtn{display:block;width:132px;height:32px;line-height:32px;text-align:center;font-size:14px;border-radius:4px;background:#07c8cf;color:#fff;}
.addPrizeArea .commonHandleBtn{}
.btnWrap{padding:55px 0 30px 0;text-align:center;}
.commonStepBtn{display:inline-block;width:148px;height:43px;line-height:43px;text-align:center;font-size:15px;border-radius:22px;background:#fc7a52;color:#fff;}

/*活动配置*/
.activityConfigArea{height:585px;padding:28px 0px 5px 70px;}
.activityConfigArea .examplePic,.activityConfigArea .createPic{float:left;width:324px;height:506px;border:3px solid #ccc;border-radius:5px;}
.activityConfigArea .createPicBox{float:left;width:324px;}
.activityConfigArea .redPackPrize{position: absolute;bottom: 61px;left: 50%;width: 210px;margin-left: -105px;}
.activityConfigArea .redPackPrize img{width:100%;}
.activityConfigArea .uploadPicBox{float:left;width:368px;height:506px;}
.activityConfigArea .createPic>img,.createPic .logoPic img{width:100%;height:auto;}
.uploadPicBox{padding:3px 0;}
.uploadPicBox>div{margin-bottom:27px;padding:3px 0;}
.uploadItem{font-size:14px;}
.uploadItem .tit{float:left;width:126px;height:32px;line-height:32px;text-align:right;color:#666;}
.uploadItem .infoBox{margin-left:126px;}
.uploadItem .exp{padding-top:7px;font-size:13px;color:#999;}
.createPic{position:relative;background:#f1f1f1 url(images/icon-red.jpg) no-repeat center 150px;}
.createPic .logoPic{position:absolute;top:15px;left:15px;width:50px;height:50px;}
.createPic .getRedBtn{display: block;position: absolute;bottom: 15%;left: 50%;margin-left: -105px;width: 210px;height:285px;}
.createPic .getRedBtn img{width:100%;}
.createPic .Redrule {position: absolute;bottom: 0px;line-height: 37px;text-align: center;color: #fff;font-weight: 600;font-size: 13px;right: 0px;  margin-right: 10px;}
.createPic .getRedBtn .OpenRedBtn{position: absolute;top: 50%;background-color: #ffea58;width: 70%;line-height: 37px;text-align: center;left: 50%; margin-left: -35%;color: #d13820;font-weight: 900;border-radius: 4px;font-size: 16px;}
.gameAction{line-height:33px;font-size:14px;width:168px;margin-left:120px;display:block;color:#4d4d4d;}
.seeRedBtn{display:inline-block;width:100%;padding:17px 0;text-align:center;font-size:15px;color:#0098ff;cursor:pointer;}

/*大转盘*/
.LuckyTurntable .bgpic {background:#f1f1f1;}
.LuckyTurntable .bgpic p {position:absolute;z-index:3; border-radius: 50px; }
.LuckyTurntable .bgpic p img{width:50px;height:50px;  border-radius: 50px;cursor:pointer;}
.LuckyTurntable .bgpic .Pic1 {position:absolute;z-index:3;width:50px;height:50px;  top: 62px;left: 132px; border:0px;}
.LuckyTurntable .Pic2 {bottom: 222px;left: 45px;}
.LuckyTurntable .Pic3 {bottom: 222px;left: 104px;}
.LuckyTurntable .Pic4 {bottom: 222px;left: 163px;}
.LuckyTurntable .Pic5 {bottom: 222px;left: 222px;}
.LuckyTurntable .Pic6 {bottom: 163px;left: 222px;}
.LuckyTurntable .Pic7 {bottom: 104px;left: 222px;}
.LuckyTurntable .Pic8 {bottom: 45px;left: 222px;}
.LuckyTurntable .Pic9 {bottom: 45px;left: 163px;}
.LuckyTurntable .Pic10 {bottom: 45px;left: 104px;}
.LuckyTurntable .Pic11 {bottom: 45px;left: 45px;}
.LuckyTurntable .Pic12 {bottom: 104px;left: 45px;}
.LuckyTurntable .Pic13 {bottom: 163px;left: 45px;}
.LuckyTurntable .createPic .regularpic {position:absolute;z-index:3;bottom: 114px; left: 114px;width:90px;height:90px; border-radius: 15px;cursor:pointer; }
.LuckyTurntable .createPic .realpic {  z-index: 3; position: absolute;}
.LuckyTurntable .createPic .bgpic1 {  z-index: 1; top: 181px; width:318px;height:318px;}
.LuckyTurntable .createPic .bgpic2 {  z-index: 2; top: 216px;height:250px; border-radius: 36px;width:250px;left:35px;}
.LuckyTurntable .border {border: 2px #fe9d1e solid;}
.LuckyTurntable .border1 {box-shadow:0 0 0 6px rgba(6, 6, 6, 0.09);}

.LuckyTurntable .regularpicupload{position:absolute;z-index:55;bottom: 114px; left: 114px;width:90px;height:90px; border-radius: 15px;cursor:pointer; }
.LuckyTurntable .createPicBox .ke-upload-file {height:90px}

#LuckyTurntablePrizeimg {width:159px; border-radius: 100px;height: 159px;}


.kvpic { max-height: 184px;}

/*刮刮卡*/
.ScratchCard .ScratchCardimg{top: 145px; position: absolute;left: 10px;width: 300px;}
.ScratchCard .activityConfigArea .createPic .ScratchCardbg{height:672px;width: 318px;}
.ScratchCard .text{top: 315px; position: absolute;left: 130px;font-size: 17px;font-weight: 600;color: #5aabfd;}
.ScratchCard .activityConfigArea {height:720px;}
.ScratchCard .activityConfigArea  .examplePic {height:678px;}
.ScratchCard .activityConfigArea  .createPic{height:678px;}
.ScratchCard .activityConfigArea .uploadPicBox{height:678px;}
.ScratchCard .activityConfigArea .ScratchCardimg{width: 300px;}
.ScratchCard .createPic{position:relative;background:#f1f1f1 url(images/icon-red.jpg) no-repeat center 60px;}
.ScratchCard .createPic .jiangpin {  position: absolute; width: 300px;height: 275px;border-radius: 5px;}
.ScratchCard .bgjiangpin {background-color: #ddd;width: 300px;height: 275px;position: absolute;bottom: 10px;left: 10px;border-radius: 5px;background:#ddd url(images/jiangpin.png) no-repeat center 110px;}
.ScratchCard .activityConfigArea .createPic .ActivityRule {position: absolute;top: 305px;left: 40px;width:235px;height:50px;border-radius: 5px;}

/*插件的上传按钮*/
form.ke-upload-area.ke-form{opacity:0;cursor:pointer;}
.commonHandleBtn.uploadImgBtn{background:url(images/uploadImgBtn.png) no-repeat 0 0;cursor:pointer;}
.ke-upload-file,.ke-button-common,input[type="button"],input[type="submit"],input[type="reset"], input[type="file"]::-webkit-file-upload-button,button{cursor:pointer !important;}

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

/*问卷奖品配置，弹出*/
.jui-dialog-Questionnaire{width:636px;height:470px;position:fixed;top:90px;margin-left:-318px;}

/*大转盘奖品配置，弹出*/
.jui-dialog-LuckyTurntable{width:636px;height:530px;position:fixed;top:90px;margin-left:-318px;}
.jui-dialog-LuckyTurntable .LuckyTurntableLeft {float:left}
.jui-dialog-LuckyTurntable .LuckyTurntableright {float: right; padding: 20px 70px 0 0;}
.jui-dialog-LuckyTurntable .LuckyTurntableright p{text-align:center;margin-top: 20px;margin-bottom: 10px;}
.jui-dialog-LuckyTurntable .LuckyTurntableright .uploadImgBtn{text-align:center;margin-top: 20px;  margin-left: 12px;}
.jui-dialog .LuckyTurntableLeft .tit{width:120px;}
.jui-dialog .LuckyTurntableLeft .hint-exp {text-align:left; }
.PrizeseOption {  margin-left: 20px;display: inline-block; }
.PrizeseOption .radio{ float:left;margin-right:20px; }


/*大转盘奖品选择，弹出*/
.jui-dialog-Prizeselect{width:636px;max-height:430px;position:fixed;top:90px;margin-left:-318px;}


/*奖品数量追加*/
.jui-dialog-prizeCountAdd{width:636px;height:322px;position:fixed;top:20%;margin-left:-318px;}
.prizeCountAddContent .commonSelectWrap{margin:60px 0 45px 0;}

/*图文消息*/
.imageTextArea{display:inline-block;height:160px;padding:15px 0;}
.imageTextArea .picBox{float:left;width:270px;height:130px;margin-left:5px;border:1px solid #d0d5d8;border-radius:3px;background:#f9f9f9 url(images/icon-img.png) no-repeat center center;}
.imageTextArea .picBox img{width:100%;height:100%;}
.imageTextArea .tit{float:left;width:75xp;}
.imageTextArea .commonHandleBtn{margin-bottom:5px;}
.ruleText textarea{width:217px;height:94px;padding:5px;border:1px solid #d0d5d8;border-radius:3px;}
.graphicHandleBox{float:left;width:640px;padding-bottom:17px;border-right:1px solid #d8d8d8;}
.graphicHandleBox .commonSelectWrap{margin:15px 0 15px 0;}
.uploadImgWrap{margin:77px 0 0 380px;color:#999;}
.graphicPreview{width:420px;height:355px;margin-left:670px;padding:12px;border:1px solid #d8d8d8;border-radius:7px;}
.previewPic{width:390px;height:218px;margin:18px 0 15px;background:#f9f9f9 url(images/icon-img.png) no-repeat center center;}
.previewPic img{width:100%;height:100%;}
.previewTit{height:30px;line-height:30px;overflow:hidden;font-size:24px;color:#000;}
.previewDsc{height:48px;line-height:24px;overflow:hidden;font-size:18px;color:#a9a9a9;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="allPage" id="section" data-js="js/activiDetail.js?ver=0.3"> 
    <!-- 内容区域 -->
    <div class="contentArea-info">
      <!--个别信息查询-->
      <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
        <div class="title">
          <ul id="optPanel">
            <li data-flag="#nav01" class="on one">基本信息</li>
            <li data-flag="#nav02">奖品配置</li>
            <li data-flag="#nav03" style="border-right:none">图文推送</li>
          </ul>
        </div>
        <div class="panelDiv" id="nav01" data-index="0">
        <form></form>
          <form id="nav0_1">
          	<div class="borderArea">
                <!--<p class="titleItem" style="border-top:none;">基本信息</p>-->
                <div class="inlineBlockArea">
                  <div class="commonSelectWrap"> <em class="tit">活动名称：</em>
                    <label class="searchInput" style="width: 446px;">
                      <input data-text="活动名称" class="easyui-validatebox"  data-flag="item_name" id="item_name" name="Title" type="text" data-options="required:true"  value="" placeholder="请输入活动名称">
                    </label>
                  </div>
                  <div class="commonSelectWrap"> <em class="tit">活动时间：</em>
                    <div class="selectBox" style="width:400px">
                      <input type="text" id="startDate"  class="easyui-datebox" name="BeginTime"   data-options="required:true,width:160,height:32"/>
                      &nbsp;&nbsp;至&nbsp;&nbsp;
                      <input type="text" id="endDate" class="easyui-datebox" name="EndTime" validType="compareEqualityDate[$('#startDate').datebox('getText'),'结束时间不能小于开始时间']" data-options="required:true,width:160,height:32"/>
                    </div>
                  </div>
                  <div class="commonSelectWrap" style="height:auto;  display: none;"> <em class="tit">活动内容：</em>
                    <label class="searchInput" style="width:926px;height:100px">
                      <textarea data-text="活动内容" class="easyui-validatebox" id="" data-flag="item_content" name="Content"  data-options="" value=""></textarea>
                    </label>
                  </div>
                </div>
                <!--
                <p class="titleItem">目标群体</p>
                <div class="inlineBlockArea">
                  <div class="commonSelectWrap">
                  	<em class="tit">卡类型：</em>
                    <label class="searchInput clearBorder">
                      <input data-text="卡类型" class="easyui-combobox" id="cardType" data-options="required:true,width:160,height:32" data-flag="cardType" name="VipCardType" type="text" value="" validType='selectIndex'>
                    </label>
                  </div>
                  
                  <div class="commonSelectWrap">
                  	<em class="tit">卡等级：</em>
                    <label class="searchInput clearBorder">
                      <input data-text="卡等级" class="easyui-combobox" id="cardGrade" data-options="required:true,width:160,height:32" data-flag="cardGrade" name="VipCardGrade" type="text" value="" validType='selectIndex'>
                    </label>
                  </div>
                </div>
                -->
                <p class="titleItem">活动设置</p>
                <div class="inlineBlockArea">
                  <div class="commonSelectWrap" style="display: none;">
                  	<em class="tit">活动类型：</em>
                    <span class="gameAction" data-id="081AEC92-CC16-4041-9496-B4F6BC3B11FC">游戏活动</span>
                    <!--
                    <label class="searchInput clearBorder">
                      <input data-text="活动类型" class="easyui-combobox" id="eventsType" data-flag="eventsType"  name="EventTypeID" type="text" value="" data-options="required:true,width:160,height:32"  validType='selectIndex'>
                    </label>
                    -->
                  </div>
                  <div class="commonSelectWrap">
                  	<em class="tit" style="width:126px">活动方式：</em>
                    <label class="searchInput clearBorder">
                      <input data-text="活动方式" class="easyui-combobox" id="lEventDrawMethod" data-flag="lEventDrawMethod" name="DrawMethodId" type="text" value="" data-options="required:true,width:160,height:32"  validType='selectIndex'>
                    </label>
                  </div>
                </div>
                <p class="titleItem">活动规则</p>
                <div class="inlineBlockArea">
                  <div class="commonSelectWrap" style="margin-right:55px;">
                  	<em class="tit">参与游戏次数：</em>
                    <label class="searchInput clearBorder"> 
                      <input data-text="参与游戏次数" class="easyui-combobox" id="personCount" data-flag="personCount" name="PersonCount" type="text" value="" data-options="required:true,width:160,height:32"  validType='selectIndex'>
                    </label>
                  </div>
                  <div class="commonSelectWrap"> <em class="tit" style="width:126px">参与游戏所需积分：</em>
                    <label class="selectBox">
                      <input data-text="参与游戏所需积分"  class="easyui-numberbox" data-options="width:160,height:32,min:0,precision:0" id="PointsLottery" name="PointsLottery"  type="text" value="">
                    </label>
                  </div>
                </div>
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav02">下一步</a>
            </div>
          </form>
        </div>
        
        <!--奖品配置-->
        <div class="panelDiv" id="nav02" data-index="1">
          <form id="nav0_2" class="PrizeSet" style="display:none;" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonHandleBtn" id="addPrizeBtn">+添加奖品</a>
              </div>
              <table class="dataTable" id="prizeListTable">
              	<thead>
                	<tr class="tableHead">
                        <th>奖品名称</th>
                        <th>奖品数量</th>
                        <th>已有生成数量</th>
                        <th>奖品剩余数量</th>
                        <th>操作</th>
                      </tr>
                </thead>
                <tbody>
                  
                </tbody>
              </table>
              
              <div class="activityConfigArea">
              		<div class="examplePic"><img src="images/red-bg.png" /></div>
                    <div class="uploadPicBox">
                    	<div id="beforeBgPic" class="uploadItem" data-flag=1  data-url="" data-batid="beforeGround">
                        	<p class="tit">领取前背景颜色：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(建议尺寸640*1008 大小为200K)</p>
                            </div>
                        </div>
                        
                       
                        
                        <div id="logoBgPic" class="uploadItem" data-flag=3  data-url="" data-batid="Logo">
                        	<p class="tit">logo图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(建议尺寸640*1008 大小为50K)</p>
                            </div>
                        </div>
                        
                        <div id="ruleBgPic" class="uploadItem" data-url="" data-batid="rule">
                        	<p class="tit">活动规则内容：</p>
                            <label class="searchInput">
                              <input data-text="活动规则内容" class="easyui-combobox" id="ruleOption" name="" type="text" value="" data-options="required:true,width:132,height:34">
                            </label>
                            <div class="infoBox ruleText"  style="margin-top:10px">
                            	<textarea placeholder="请输入"></textarea>
                            </div>
                            <div class="infoBox ruleImg"  style="margin-top:10px">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(建议尺寸640*1008 大小为200K)</p>
                            </div>
                        </div>
                        
                    </div>
                    <div class="createPicBox">
                        <div class="createPic">
                            <p class="logoPic"><img id="logoPic" src="images/icon-log.png" /></p>
                            <img id="redBgPic" src="" />
                            <span class="getRedBtn getRedBackBtn"><img src="images/Redbg.png" /><span class="Redrule">活动规则></span><div class="OpenRedBtn">打开红包</div></span>
                            <div class="backAction hide">
                            <p class="redPackPrize"><img src="images/backRedbg.png" /></p>
                            </div>
                        </div>
                        <span class="seeRedBtn">查看领取后页面</span>
                    </div>
                    
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="#nav01">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>

            <!--大转盘-->
            <form id="LuckyTurntable_form" class="LuckyTurntable PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonHandleBtn" id="LuckyTurntableaddBtn">+添加奖品</a>
              </div>
              <table class="dataTable" id="LuckyTurnListTable">
              	<thead>
                	<tr class="tableHead">
                        <th>奖品等级</th>
                        <th>奖品名称</th>
                        <th>奖品数量</th>
                       <th>奖品剩余数量</th>
                        <th>操作</th>
                      </tr>
                </thead>
                <tbody>
                  
                </tbody>
              </table>
              
              <div class="activityConfigArea">
              		<div class="examplePic"><img src="images/LuckyTurntable_bg.png" /></div>
                    <div class="uploadPicBox">
                    	<div id="LT_Data_kvPic" class="uploadItem" style="margin-top: 75px;" data-flag=10  data-url="" data-batid="LT_kvPic">
                        	<p class="tit">KV图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸:640*368)</p>
                            </div>
                        </div>
                        
                        <div id="LT_Data_Rule" class="uploadItem"  data-flag=11  data-url="" data-batid="LT_Rule">
                        	<p class="tit">活动规则内容：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸：640*1008)</p>
                            </div>
                        </div>
                        
                        <div id="LT_Data_bgpic1" class="uploadItem"  data-flag=12  data-url="" data-batid="LT_bgpic1">
                        	<p class="tit">背景颜色1：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸：640*640)</p>
                            </div>
                        </div>
                         <div id="LT_Data_bgpic2" class="uploadItem" data-flag=13  data-url="" data-batid="LT_bgpic2">
                        	<p class="tit">背景颜色2：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸：640*640)</p>
                            </div>
                        </div>
                      
                    </div>
                    <div class="createPicBox">
                        <div id="selectPrize" class="createPic bgpic">
                            <img  class="Pic1" id="Imge"  src="images/KVimg.png" />
                            <p class="Pic2"><img data-index=1 data-prizeLocationid=""  id="Img1" src="images/addimg.png" /></p>
                            <p class="Pic3"><img data-index=2 data-prizeLocationid="" id="Img2" src="images/addimg.png" /></p>
                            <p class="Pic4"><img data-index=3 data-prizeLocationid="" id="Img3" src="images/addimg.png" /></p>
                            <p class="Pic5"><img data-index=4 data-prizeLocationid="" id="Img4" src="images/addimg.png" /></p>
                            <p class="Pic6"><img data-index=5 data-prizeLocationid="" id="Img5" src="images/addimg.png" /></p>
                            <p class="Pic7"><img data-index=6 data-prizeLocationid="" id="Img6" src="images/addimg.png" /></p>
                            <p class="Pic8"><img data-index=7 data-prizeLocationid="" id="Img7" src="images/addimg.png" /></p>
                            <p class="Pic9"><img data-index=8 data-prizeLocationid="" id="Img8" src="images/addimg.png" /></p>
                            <p class="Pic10"><img data-index=9 data-prizeLocationid="" id="Img9" src="images/addimg.png" /></p>
                            <p class="Pic11"><img data-index=10 data-prizeLocationid="" id="Img10" src="images/addimg.png" /></p>
                            <p class="Pic12"><img data-index=11 data-prizeLocationid="" id="Img11" src="images/addimg.png" /></p>
                            <p class="Pic13"><img data-index=12 data-prizeLocationid="" id="Img12" src="images/addimg.png" /></p>
                            <div  id="LT_Data_regularpic" class="uploadItem"  data-flag=14  data-url="" data-batid="LT_regularpic">
                            <span class="regularpicupload"  ></span></div>
                            <img id="LT_regularpic"  class="regularpic realpic"  src="images/regularImg.png" />
                           <img id="LT_kvPic" class="realpic kvpic" src=""/>
                           <img id="LT_bgpic1" class="realpic bgpic1" src="images/bgpic1.png"/>
                           <img id="LT_bgpic2" class="realpic bgpic2" src="images/bgpic2.png"/>
                        </div>
                    </div>
                    
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="#nav01">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


             <!--刮刮卡-->
            <form id="ScratchCard_form" class="ScratchCard PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonHandleBtn" id="A1">+添加奖品</a>
              </div>
              <table class="dataTable" id="Table2">
              	<thead>
                	<tr class="tableHead">
                        <th>奖品等级</th>
                        <th>奖品名称</th>
                        <th>数量</th>
                       <%-- <th>中奖概率（%）</th>--%>
                        <th>追加</th>
                        <th>编辑</th>
                        <th>删除</th>
                      </tr>
                </thead>
                <tbody>
                  
                </tbody>
              </table>
              
              <div class="activityConfigArea">
              		<div class="examplePic"><img src="images/ScratchCardbg.png" /></div>
                    <div class="uploadPicBox">
                    	<div id="Div5" class="uploadItem" style="margin-top: 75px;" data-flag=15  data-url="" data-batid="beforeGround">
                        	<p class="tit">背景颜色：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸:640*1008)</p>
                            </div>
                        </div>
                        
                        <div id="Div6" class="uploadItem"  data-flag=16  data-url="" data-batid="BackGround">
                        	<p class="tit">活动规则内容：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸：90*90)</p>
                            </div>
                        </div>
                        
                        <div id="Div7" class="uploadItem"  data-flag=17  data-url="" data-batid="Logo">
                        	<p class="tit">奖品图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <p class="exp">(尺寸：640*300)</p>
                            </div>
                        </div>
                       
                       
                    </div>
                    <div class="createPicBox">
                        <div id="Div9" class="createPic bgpic">
                            <img id="ScratchCardbg"   class="ScratchCardbg" />
                            <img id="ScratchCardimg" class="ScratchCardimg"  src="images/ScratchCardimg.png" />
                            <p class="text">活动规则</p>
                            <img id="ActivityRule"   class="ActivityRule" src="images/ActivityRule.png" />
                            <span class="bgjiangpin">
                                <img id="jiangpin" class="jiangpin " src="images/bg.png" / />
                            </span>
                        </div>
                    </div>
                    
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="#nav01">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


            <!--问卷-->
            <form id="Questionnaire_form"  class="Questionnaire PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonHandleBtn" id="QuestionnaireaddBtn">+添加奖品</a>
              </div>
              <table class="dataTable" id="QuestionnaireTable">
              	<thead>
                	<tr class="tableHead">
                        <th>奖品名称</th>
                        <th>奖品数量</th>
                        <th>已有生成数量</th>
                        <th>奖品剩余数量</th>
                        <th>操作</th>
                      </tr>
                </thead>
                <tbody>
                  
                </tbody>
              </table>
              
              <div class="activityConfigArea">
              		  
                    选择表单：<input id="Questionnaires" class="easyui-combobox" data-options="required:true,width:190,height:32"  type="text" />
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="#nav01">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


        </div>
        <!--奖品配置End--> 
        
        <!--图文推送-->
        <div class="panelDiv" id="nav03" data-index="2">
          <form id="nav0_3">
          	<div class="borderArea">
            	<div class="inlineBlockArea">
                	<div class="graphicHandleBox">
                      <div class="commonSelectWrap" style="margin-top:0;">
                      	<em class="tit" style="width:75px;">标题：</em>
                        <label class="searchInput" style="width:525px;">
                          <input data-text="标题" type="text" class="easyui-validatebox"  data-flag="" id="graphicTitle" name="graphicTitle" data-options="required:true"  value="" placeholder="请输入">
                        </label>
                      </div>

                      <div class="imageTextArea uploadItem" data-flag=4 data-url="">
                      		<em class="tit" style="width:75px;">封面：</em>
                      		<p class="picBox"><img id="imageTextPic" src="" /></p>
                            <div class="uploadImgWrap">
                                <a href="javascript:;" class="commonHandleBtn uploadImgBtn">上传图片</a>
                                <span>(建议尺寸536*300 大小为100K)</span>
                            </div>
                      </div>
                      
                      <div class="commonSelectWrap" style="height:auto;">
                      	<em class="tit" style="width:75px;">摘要：</em>
                        <label class="searchInput" style="width:525px;height:100px">
                          <textarea data-text="摘要" class="easyui-validatebox" data-flag="" id="graphicDsc" name="graphicDsc" data-options="" value="" placeholder="请输入"></textarea>
                        </label>
                      </div>
                    </div>
                    
                    <div class="graphicPreview">
                    	<h2 class="previewTit">标题</h2>
                        <div class="previewPic"><img src="" /></div>
                        <p class="previewDsc">摘要</p>
                    </div>
               </div>
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="#nav02">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav04" style="margin-left:40px;">完成</a>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
  
 
<!-- 遮罩层 -->
<div class="jui-mask"></div>
<!--添加奖品，弹出-->
<div class="jui-dialog jui-dialog-redPackage" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品配置</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="redPackageContent">
    	<form id="addPrizeForm">
       <%-- <div class="commonSelectWrap">
            <em class="tit">奖品等级：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品等级" class="easyui-combobox" id="prizeLevel" data-options="required:true,width:190,height:32" data-flag="" name="PrizeLevel" type="text" value="">
            </label>
        </div>--%>
        
        <div class="commonSelectWrap">
            <em class="tit">奖品选择：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品选择" class="easyui-combobox" id="prizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
            </label>
        </div>
        
        <div class="commonSelectWrap" id="couponItem">
            <em class="tit">优惠券：</em>
            <label class="searchInput clearBorder">
              <input data-text="优惠券" class="easyui-combobox" id="couponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            </label>
        </div>
        
        
        <div class="commonSelectWrap"  id="integralItem" style="display:none">
        	<em class="tit">积分：</em>
            <label class="selectBox">
              <input data-text="积分"  class="easyui-numberbox" data-options="width:190,height:32,min:0,precision:0" id="integralOption" name="Point"  type="text" value="">
            </label>
        </div>
        
        
        
        <div class="commonSelectWrap">
            <em class="tit">奖品名称：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品名称" class="easyui-validatebox" id="prizeName" data-options="required:true,width:190,height:32" data-flag="" name="PrizeName" type="text" value="">
            </label>
        </div>
        <!--
        <div class="commonSelectWrap">
            <em class="tit">中奖概率：</em>
            <label class="searchInput clearBorder">
              <input data-text="中奖概率" class="easyui-numberbox" id="prizeProbability" data-options="required:true,width:190,height:32" data-flag="" name="Probability" type="text" value="">
            </label>
        </div>
        -->
        <div class="commonSelectWrap">
            <em class="tit">奖品总数量：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品总数量" class="easyui-numberbox" id="prizeCount" data-options="required:true,width:190,height:32" data-flag="" name="CountTotal" type="text" value="">
            </label>
        </div>
        
        <p class="hint-exp">提示：奖品数量不能超过券的生成数量！</p>
        </form>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
    </div>
</div>


    <!--大转盘添加奖品，弹出-->
<div class="jui-dialog jui-dialog-LuckyTurntable" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品配置</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="redPackageContent">
    	<form id="LuckyTurntablePrize">
            <div class="LuckyTurntableLeft">
        <div class="commonSelectWrap">
            <em class="tit">奖品等级：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品等级" class="easyui-combobox" id="LTprizeLevel" data-options="required:true,width:190,height:32" data-flag="" name="PrizeLevel" type="text" value="">
            </label>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit">奖品选择：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品选择" class="easyui-combobox" id="LTprizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
            </label>
        </div>
        
        <div class="commonSelectWrap" id="LTcouponItem" style="display:none">
            <em class="tit">优惠券选择：</em>
            <label class="searchInput clearBorder">
              <input data-text="优惠券选择" class="easyui-combobox" id="LTcouponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            </label>
        </div>

                  
        <div class="commonSelectWrap"  id="LTintegralItem" style="display:none">
        	<em class="tit">积分：</em>
            <label class="selectBox">
              <input data-text="积分"  class="easyui-numberbox" data-options="width:190,height:32,min:0,precision:0" id="LTintegralOption" name="Point"  type="text" value="">
            </label>
        </div>
        
        
        
        <div class="commonSelectWrap">
            <em class="tit">奖品名称：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品名称" class="easyui-validatebox" id="LTprizeName" data-options="required:true,width:190,height:32" data-flag="" name="PrizeName" type="text" value="">
            </label>
        </div>
        
      <%--  <div class="commonSelectWrap">
            <em class="tit">中奖概率：</em>
            <label class="searchInput clearBorder">
              <input data-text="中奖概率" class="easyui-numberbox" id="LTprizeProbability" data-options="required:true,width:190,height:32" data-flag="" name="Probability" type="text" value="">
            </label>
        </div>
        --%>
        <div class="commonSelectWrap">
            <em class="tit">奖品总数量：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品总数量" class="easyui-numberbox" id="LTprizeCount" data-options="required:true,width:190,height:32" data-flag="" name="CountTotal" type="text" value="">
            </label>
        </div>
        
        <p class="hint-exp">提示：奖品数量不能超过券的生成数量！</p>
                </div>
            <div class="LuckyTurntableright" >
                <span style="display:none;">
                <input data-text="奖品图片" class="easyui-validatebox" id="LT_dataPrizeimg" data-options="" data-flag="" name="ImageUrl" type="text" value="">
                    </span>
                <div><img id="LuckyTurntablePrizeimg" src="images/uploadpic.png" /></div>
                <p>尺寸：120*120</p>
                <a href="javascript:;" class="commonHandleBtn uploadImgBtn"></a>
            </div>
            <div style="clear:both;"></div>
        </form>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
    </div>
</div>


    <!--问卷奖品，弹出-->
<div class="jui-dialog jui-dialog-Questionnaire" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品配置</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="QuestionnaireContent">
    	<form id="Questionnaire">
       
        
        <div class="commonSelectWrap">
            <em class="tit">奖品选择：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品选择" class="easyui-combobox" id="questprizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
            </label>
        </div>
        
        <div class="commonSelectWrap" id="_questcouponOption">
            <em class="tit">优惠券：</em>
            <label class="searchInput clearBorder">
              <input data-text="优惠券" class="easyui-combobox" id="questcouponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            </label>
        </div>
        
        
        <div class="commonSelectWrap"  id="_questintegralItem" style="display:none">
        	<em class="tit">积分：</em>
            <label class="selectBox">
              <input data-text="积分"  class="easyui-numberbox" data-options="width:190,height:32,min:0,precision:0" id="Text4" name="Point"  type="text" value="">
            </label>
        </div>
        
        
        
        <div class="commonSelectWrap">
            <em class="tit">奖品名称：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品名称" class="easyui-validatebox" id="Text5" data-options="required:true,width:190,height:32" data-flag="" name="PrizeName" type="text" value="">
            </label>
        </div>
      
        <div class="commonSelectWrap">
            <em class="tit">奖品总数量：</em>
            <label class="searchInput clearBorder">
              <input data-text="奖品总数量" class="easyui-numberbox" id="Text6" data-options="required:true,width:190,height:32" data-flag="" name="CountTotal" type="text" value="">
            </label>
        </div>
        
        <p class="hint-exp">提示：奖品数量不能超过券的生成数量！</p>
        </form>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
    </div>
</div>


     <!--大转盘奖品选择，弹出-->
<div class="jui-dialog jui-dialog-Prizeselect" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品配置</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="redPackageContent">
    	<form id="Form2">
            <div class="PrizeseOption">
           

          </div>
        </form>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
    </div>
</div>

<!--奖品数量追加，弹出-->
<div class="jui-dialog jui-dialog-prizeCountAdd" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品数量追加</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="prizeCountAddContent">
    	 <div class="commonSelectWrap">
            <em class="tit" style="width:172px;">数量：</em>
            <label class="searchInput clearBorder" style="width:308px">
              <input data-text="奖品数量追加" class="easyui-numberbox" id="prizeCountAdd" data-options="required:true,width:308,height:32" data-flag="" name="" type="text" value="">
            </label>
        </div>
        <div class="btnWrap">
            <a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
	</div>
</div>

<!--数据部分-->
<script id="tpl_prizeList" type="text/html">
<#for(var i=0,idata;i<PrizeList.length;i++){ idata=PrizeList[i];#>
<tr data-eventid="<#=idata.EventId#>" data-prizesid="<#=idata.PrizesID#>" data-num="<#=idata.CountTotal#>">
	<td><#=idata.PrizeName#><span style="display:none"><input class="Prizedata" data-coupontypeid="<#=idata.CouponTypeID #>" type="hidden" /></span></td>
	<td class="numBox"><#=idata.CountTotal#></td>
	<td><#=idata.IssuedQty#></td>
	<td ><#=idata.RemainCount#></td>
	<td><em class="addBtn"></em><em class="delBtn"></em></td>
</tr>
<#}#>
</script>

    
<!--大转盘数据部分-->
<script id="tpl_LuckyTurnprizeList" type="text/html">
<#for(var i=0,idata;i<PrizeList.length;i++){ idata=PrizeList[i];#>
<tr data-eventid="<#=idata.EventId#>" data-prizesid="<#=idata.PrizesID#>" data-num="<#=idata.CountTotal#>">
	<td><#=idata.PrizeLevelName#><span style="display:none"><input class="Prizedata" data-coupontypeid="<#=idata.CouponTypeID #>" type="hidden" /></span></td>
	<td><#=idata.PrizeName#></td>
	<td class="numBox"><#=idata.CountTotal#></td>
	<td ><#=idata.RemainCount#></td>
	<td><em class="addBtn"></em><em class="delBtn"></em></td>
</tr>
<#}#>
</script>
  
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script> 

</asp:Content>

