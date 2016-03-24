<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta charset="UTF-8" />
  <title>活动详情</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="<%=StaticUrl+"/module/activityList/css/activityDetail.css?v=0.5"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="allPage" id="section" data-js="js/activiDetail.js?ver=0.3"> 
    <!-- 内容区域 -->
    <div class="contentArea-info">
      <!--个别信息查询-->
      <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
        <div class="title">
          <ul id="optPanel">
            <li data-flag="#nav01"  class="nav01  on one">基本信息</li>
            <li data-flag="#nav02"  class="nav02">封面设置</li>
            <li data-flag="#nav03"  class="nav03">奖品配置</li>
            <li data-flag="#nav04"  class="nav04">图文推送</li>
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
                  	<em class="tit" >活动方式：</em>
                    
                      <input data-text="活动方式" class="easyui-combobox textbox combo" id="lEventDrawMethod" data-flag="lEventDrawMethod" name="DrawMethodId" type="text" value="" data-options="required:true,width:160,height:32"  validType='selectIndex'>
                    
                  </div>
                </div>
                <p class="titleItem">游戏规则</p>
                <div class="inlineBlockArea">
                  <div class="commonSelectWrap" style="margin-right:55px;">
                  	<em class="tit" style="width:100px;">参与游戏次数：</em>
                      <input data-text="参与游戏次数" class="easyui-combobox  textbox combo" id="personCount" data-flag="personCount" name="PersonCount" type="text" value="" data-options="required:true,width:160,height:32"  validType='selectIndex'>
                   
                  </div>
                  <div class="commonSelectWrap"> <em class="tit" style="width:126px">参与游戏所需积分：</em>
                    <label class="selectBox">
                      <input data-text="参与游戏所需积分"  class="easyui-numberbox" data-options="width:160,height:32,min:0,precision:0" id="PointsLottery" name="PointsLottery"  type="text" value="">
                    </label>
                  </div>
                </div>
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn nextStepBtn btnopt commonBtn" data-flag="nav02">下一步</a>
            </div>
          </form>
        </div>
        
           <!--封面设置-->
        <div class="panelDiv" id="nav02" data-index="4">

           <form id="nav0_5">
                <div class="tableWrap coverbody" id="tableWrap">
                    <div class="startimg">
                        <img  class="StartBG" src="/Module/activityList/images/StartBG.png" />
                        <img class="_BGImageSrc" src="" />
                        <span class="startbtn">进入</span>
                        <span class="regular">活动规则></span>
                    </div>
                    <div class="startpageContent">
                        <div class="CoverSetting">
                            <div class="EnableCover checkBox on">
                                  <em></em>
                                  <span>启用封面</span>
                                 <div style="display:none;">
                                    <input class="checkvalue activityListdata"    data-text="活动规则启用" data-idname="IsShowQRegular" value="1"  type="text" />
                                </div>
                            </div>
                            <div class="commonSelectWrap">
                                <em class="tit">按钮名称：</em>
                                <label class="searchInput" style="width: 180px;">
                                    <input id="ButtonName" class="activityListdata" data-alerttext="按钮名称不能为空"  data-required="true"  data-idname="ButtonName"   name="ButtonName" type="text"
                                        value="进入">
                                </label>
                            </div>
                            <div class="commonSelectWrap colorplan">
                                <em class="tit">按钮颜色：</em>
                                <span style="display:none;">
                                <input id="StartPageBtnBGColor"  class="activityListdata"  data-idname="StartPageBtnBGColor"   type="text" value="#fc9a01"/>
                                    <input id="StartPageBtnTextColor"  class="activityListdata"  data-idname="StartPageBtnTextColor"     type="text"  value="#fff"/>
                                 </span>
                                <div class="color_plan" data-type="1">
                               
                                </div>
                            </div>
                            <div  id="CoverImageUrl"  class="commonSelectWrap uploadItem"  data-flag="Cover"  data-url="" data-batid="BGImageSrc" >
                            
                            <div class="infoBox">
                            	<a href="javascript:void(0)" data-uploadimgwidth=80  class="commonHandleBtn uploadImgBtn"></a>
                                <p class="exp">(建议尺寸:640*1008，大小50kb)</p>
                            </div>
                        </div>

                        </div>
                        <div class="CoverSettingrule">

                            <div  class="commonSelectWrap" style="margin-top: 20px;" >
                        	<p class="tit">活动规则：</p>
                            <div class="on rulebtn" data-flag="Cover1" data-name="r1" >
                                <input data-text="活动规则" class="easyui-combobox textbox combo" data-options="required:true,width:200,height:32" id="ruleType"   name="ruleType" type="text" value="">
                              </div>
                            <div class="infoBox CoverruleText" style="display: none;">
                            	<textarea id="QRegular"  class="activityListdata"  placeholder="请输入"  data-alerttext="规则内容不能为空！" data-required="true"  data-idname="QRegular"   ></textarea>
                            </div>
                            <div id="RuleImageUrl" class="infoBox ruleTextImg uploadItem"  data-flag="Cover1"  data-url="" data-batid="RuleImageUrl">
                                <a href="javascript:void(0)" data-uploadimgwidth=80  class="commonHandleBtn uploadImgBtn"></a>
                                <p class="exp">(建议尺寸:640*1008，大小50kb)</p>
                            </div>
                        </div>
                        </div>
                            

                        
                        
                    </div>


                    
                </div>

               <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn  " data-flag="nav01">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn " data-flag="nav03">下一步</a>
            </div>
               </form>
        </div>
        <!--封面设置End--> 

        <!--奖品配置-->
        <div class="panelDiv" id="nav03" data-index="1">
          <form id="nav0_2" class="PrizeSet" style="display:none;" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="addPrizeBtn">添加奖品</a>
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
                        	<p class="tit">背景图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="背景图片上传成功！">上传图片</a>
                                <p class="exp">(建议尺寸640*1008 大小为200K)</p>
                            </div>
                        </div>
                        
                       
                        
                        <div id="logoBgPic" class="uploadItem" data-flag=3  data-url="" data-batid="Logo">
                        	<p class="tit">logo图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="logo图片上传成功！" >上传图片</a>
                                <p class="exp">(建议尺寸100*100 大小为200K)</p>
                            </div>
                        </div>
                        
                        <div id="ruleBgPic" class="uploadItem" data-url="" data-batid="rule">
                        	<p class="tit">游戏规则内容：</p>
                           
                              <input data-text="游戏规则内容" class="easyui-combobox textbox combo" id="ruleOption" name="" type="text" value="" data-options="required:true,width:132,height:34">
                           
                            <div class="infoBox ruleText"  style="margin-top:10px">
                            	<textarea placeholder="请输入"></textarea>
                            </div>
                            <div class="infoBox ruleImg"  style="margin-top:10px">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn" data-alertinfo="游戏规则图片上传成功！" >上传图片</a>
                                <p class="exp">(建议尺寸640*1008 大小为200K)</p>
                            </div>
                        </div>
                        
                    </div>
                    <div class="createPicBox">
                        <div class="createPic">
                            <p class="logoPic"><img id="logoPic" src="images/icon-log.png" /></p>
                            <img id="redBgPic" src="" />
                            <span class="getRedBtn getRedBackBtn"><img src="images/Redbg.png" /><span class="Redrule">游戏规则></span><div class="OpenRedBtn">打开红包</div></span>
                            <div class="backAction hide">
                            <p class="redPackPrize"><img src="images/backRedbg.png" /></p>
                            </div>
                        </div>
                        <span class="seeRedBtn">查看领取后页面</span>
                    </div>
                    
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn" data-flag="nav02">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="nav04" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>

            <!--大转盘-->
            <form id="LuckyTurntable_form" class="LuckyTurntable PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="LuckyTurntableaddBtn">添加奖品</a>
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
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn" data-alertinfo="KV图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸:640*348)</p>
                            </div>
                        </div>
                        
                        <div id="LT_Data_Rule" class="uploadItem"  data-flag=11  data-url="" data-batid="LT_Rule">
                        	<p class="tit">游戏规则内容：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="游戏规则图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸：640*1008)</p>
                            </div>
                        </div>
                        
                        <div id="LT_Data_bgpic1" class="uploadItem"  data-flag=12  data-url="" data-batid="LT_bgpic1">
                        	<p class="tit">背景颜色1：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="背景颜色1图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸：640*660)</p>
                            </div>
                        </div>
                         <div id="LT_Data_bgpic2" class="uploadItem" data-flag=13  data-url="" data-batid="LT_bgpic2">
                        	<p class="tit">背景颜色2：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="背景颜色2图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸：492*492)</p>
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
            	<a href="javascript:;" class="commonStepBtn prevStepBtn " data-flag="nav02">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn " data-flag="nav04" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


             <!--刮刮卡-->
            <form id="ScratchCard_form" class="ScratchCard PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="A1">添加奖品</a>
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
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="背景颜色图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸:640*1008)</p>
                            </div>
                        </div>
                        
                        <div id="Div6" class="uploadItem"  data-flag=16  data-url="" data-batid="BackGround">
                        	<p class="tit">游戏规则内容：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn" data-alertinfo="游戏规则图片上传成功！" >上传图片</a>
                                <p class="exp">(尺寸：90*90)</p>
                            </div>
                        </div>
                        
                        <div id="Div7" class="uploadItem"  data-flag=17  data-url="" data-batid="Logo">
                        	<p class="tit">奖品图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="奖品图片上传成功！">上传图片</a>
                                <p class="exp">(尺寸：640*300)</p>
                            </div>
                        </div>
                       
                       
                    </div>
                    <div class="createPicBox">
                        <div id="Div9" class="createPic bgpic">
                            <img id="ScratchCardbg"   class="ScratchCardbg" />
                            <img id="ScratchCardimg" class="ScratchCardimg"  src="images/ScratchCardimg.png" />
                            <p class="text">游戏规则</p>
                            <img id="ActivityRule"   class="ActivityRule" src="images/ActivityRule.png" />
                            <span class="bgjiangpin">
                                <img id="jiangpin" class="jiangpin " src="images/bg.png" / />
                            </span>
                        </div>
                    </div>
                    
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn  btnopt commonBtn" data-flag="nav02">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn btnopt commonBtn" data-flag="nav04" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


            <!--问卷-->
            <form id="Questionnaire_form"  class="Questionnaire PrizeSet" >
            <div class="borderArea">
              <div class="addPrizeArea">
              		<a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="QuestionnaireaddBtn">添加奖品</a>
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
              		  
                    选择表单：<input id="Questionnaires" class="easyui-combobox textbox combo" data-options="required:true,width:190,height:32"  type="text" />
              </div>
              
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn prevStepBtn  btnopt commonBtn" data-flag="nav02">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn btnopt commonBtn" data-flag="nav04" data-page="redPackage" style="margin-left:40px;">下一步</a>
            </div>
          </form>


        </div>
        <!--奖品配置End--> 
        
        <!--图文推送-->
        <div class="panelDiv" id="nav04" data-index="2">
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
                                <a href="javascript:;" class="commonHandleBtn uploadImgBtn" data-alertinfo="封面图片上传成功！">上传图片</a>
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
            	<a href="javascript:;" class="commonStepBtn prevStepBtn btnopt commonBtn" data-flag="nav03">上一步</a>
            	<a href="javascript:;" class="commonStepBtn nextStepBtn btnopt commonBtn" data-flag="nav05" style="margin-left:40px;">完成</a>
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
              <input data-text="奖品选择" class="easyui-combobox textbox combo" id="prizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
            
        </div>
        
        <div class="commonSelectWrap" id="couponItem">
            <em class="tit">优惠券：</em>
             <input data-text="优惠券" class="easyui-combobox textbox combo" id="couponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            
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
              <input data-text="奖品等级" class="easyui-combobox textbox combo" id="LTprizeLevel" data-options="required:true,width:190,height:32" data-flag="" name="PrizeLevel" type="text" value="">
            
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit">奖品选择：</em>
             <input data-text="奖品选择" class="easyui-combobox textbox combo" id="LTprizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
           
        </div>
        
        <div class="commonSelectWrap" id="LTcouponItem" style="display:none">
            <em class="tit">优惠券选择：</em>
             <input data-text="优惠券选择" class="easyui-combobox textbox combo" id="LTcouponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            
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
                <a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="奖品图片上传成功！"></a>
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
              <input data-text="奖品选择" class="easyui-combobox textbox combo" id="questprizeOption" data-options="required:true,width:190,height:32" data-flag="" name="PrizeTypeId" type="text" value="">
            
        </div>
        
        <div class="commonSelectWrap" id="_questcouponOption">
            <em class="tit">优惠券：</em>
             <input data-text="优惠券" class="easyui-combobox textbox combo" id="questcouponOption" data-options="width:190,height:32" data-flag="" name="CouponTypeID" type="text" value="">
            
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
	<td><em class="addBtn" title="追加数量"></em><em class="delBtn" title="删除"></em></td>
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
	<td><em class="addBtn" title="追加数量"></em><em class="delBtn" title="删除"></em></td>
</tr>
<#}#>
</script>

     <%-- 颜色板 --%>
    <script id="tpl_colorplan" type="text/html">
         <img class="writecolor" src="/Module/activityList/images/writecolor.png"  />
                                   <img class="blackcolor" src="/Module/activityList/images/blackcolor.png"  />
                                   <img class="allcolor" src="/Module/activityList/images/allcolor.png"  />
                                <div class="Colorplate" >
                                    <%-- 第1行 --%>
                                    <div style="background-color:#960001;color:#fff;">文字</div>
                                    <div style="background-color:#ff0101;color:#fff;">文字</div>
                                    <div style="background-color:#fc9a01;color:#fff;">文字</div>
                                    <div style="background-color:#fffe03;color:#EA8E39;">文字</div>
                                    <div style="background-color:#04fd01;color:#274F13;">文字</div>
                                    <div style="background-color:#01ffcd;color:#007765;">文字</div>
                                    <div style="background-color:#00ffff;color:#44828F;">文字</div>
                                    <div style="background-color:#0102fa;color:#fff;">文字</div>
                                    <div style="background-color:#9c00fc;color:#fff;">文字</div>
                                    <div style="background-color:#ff00fe;color:#fff;">文字</div>
                                    <%-- 第2行 --%>
                                    <div style="background-color:#e7b8b0;color:#C94320;">文字</div>
                                    <div style="background-color:#f4cccc;color:#E26563;">文字</div>
                                    <div style="background-color:#fde4d0;color:#EA8E39;">文字</div>
                                    <div style="background-color:#fff2cd;color:#EA8E39;">文字</div>
                                    <div style="background-color:#d8ead2;color:#69A84F;">文字</div>
                                    <div style="background-color:#c3f8f2;color:#007765;">文字</div>
                                    <div style="background-color:#d2e0e3;color:#44828F;">文字</div>
                                    <div style="background-color:#cfe2f3;color:#3E85C7;">文字</div>
                                    <div style="background-color:#dad2e9;color:#674FA7;">文字</div>
                                    <div style="background-color:#e7d3dc;color:#A54E79;">文字</div>
                                    <%-- 第3行 --%>
                                    <div style="background-color:#DD7E6A;color:#fff;">文字</div>
                                    <div style="background-color:#E99897;color:#660000;">文字</div>
                                    <div style="background-color:#FAC99E;color:#7B3C07;">文字</div>
                                    <div style="background-color:#FFE598;color:#7E6000;">文字</div>
                                    <div style="background-color:#B6D7A8;color:#274F13;">文字</div>
                                    <div style="background-color:#7DDED5;color:#007765;">文字</div>
                                    <div style="background-color:#A3C4CB;color:#0C353B;">文字</div>
                                    <div style="background-color:#9FC5E9;color:#073863;">文字</div>
                                    <div style="background-color:#B3A6D4;color:#20124D;">文字</div>
                                    <div style="background-color:#D7A5BE;color:#fff;">文字</div>
                                    <%-- 第4行 --%>
                                    <div style="background-color:#C94320;color:#fff;">文字</div>
                                    <div style="background-color:#E26563;color:#fff;">文字</div>
                                    <div style="background-color:#F5B172;color:#fff;">文字</div>
                                    <div style="background-color:#FDDA64;color:#fff;">文字</div>
                                    <div style="background-color:#92C47F;color:#fff;">文字</div>
                                    <div style="background-color:#39C6B5;color:#fff;">文字</div>
                                    <div style="background-color:#79A3AF;color:#fff;">文字</div>
                                    <div style="background-color:#6FA8DD;color:#fff;">文字</div>
                                    <div style="background-color:#8D7CC3;color:#fff;">文字</div>
                                    <div style="background-color:#BD7D9F;color:#fff;">文字</div>

                                    <%-- 第5行 --%>
                                    <div style="background-color:#A61C00;color:#fff;">文字</div>
                                    <div style="background-color:#CC0001;color:#fff;">文字</div>
                                    <div style="background-color:#EA8E39;color:#fff;">文字</div>
                                    <div style="background-color:#F1C332;color:#fff;">文字</div>
                                    <div style="background-color:#69A84F;color:#fff;">文字</div>
                                    <div style="background-color:#05A792;color:#fff;">文字</div>
                                    <div style="background-color:#44828F;color:#fff;">文字</div>
                                    <div style="background-color:#3E85C7;color:#fff;">文字</div>
                                    <div style="background-color:#674FA7;color:#fff;">文字</div>
                                    <div style="background-color:#A54E79;color:#fff;">文字</div>

                                    <%-- 第6行 --%>
                                    <div style="background-color:#5B0F01;color:#fff;">文字</div>
                                    <div style="background-color:#660000;color:#fff;">文字</div>
                                    <div style="background-color:#7B3C07;color:#fff;">文字</div>
                                    <div style="background-color:#7E6000;color:#fff;">文字</div>
                                    <div style="background-color:#274F13;color:#fff;">文字</div>
                                    <div style="background-color:#007764;color:#fff;">文字</div>
                                    <div style="background-color:#0C353B;color:#fff;">文字</div>
                                    <div style="background-color:#053863;color:#fff;">文字</div>
                                    <div style="background-color:#20124D;color:#fff;">文字</div>
                                    <div style="background-color:#000000;color:#fff;">文字</div>

                                    <%-- 第7行 --%>
                                    <div ><img style="margin: 0px;width: 31px;" src="/Module/activityList/images/slash.png" /></div>
                                    <div style="background-color:#FFFFFF;color:#000">文字</div>
                                    <div style="background-color:#E1E1E1;color:#000">文字</div>
                                    <div style="background-color:#C3C3C3;color:#000">文字</div>
                                    <div style="background-color:#A5A5A5;color:#000;">文字</div>
                                    <div style="background-color:#868789;color:#fff;">文字</div>
                                    <div style="background-color:#696969;color:#fff;">文字</div>
                                    <div style="background-color:#4C4A4B;color:#fff;">文字</div>
                                    <div style="background-color:#232323;color:#fff;">文字</div>
                                    <div style="background-color:#000000;color:#fff;">文字</div>
                                </div>
         </script>

  
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script> 

</asp:Content>

