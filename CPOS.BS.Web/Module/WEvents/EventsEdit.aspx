<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动内容</title>
 <script src="<%=StaticUrl+"/Framework/javascript/Biz/Options.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/EventType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/CitySelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/LEventSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/LEventsType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/MobileModule.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/QuestionnaireType.js"%>" type="text/javascript"></script> 
    <%--    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
  
    <script src="/Framework/javascript/Biz/WeiXinPublic.js" type="text/javascript"></script>--%>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WApplicationInterface2.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WModel.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/EventStatus.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WEventAdmin.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WQRCodeType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/MapSelect.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
  <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/examples/jquery.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/kindeditor.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/tools-lib.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/bdTemplate.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/plugin/jquery.jqpagination.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/plugin/jquery.drag.js"%>"></script>
   <script src="<%=StaticUrl+"/Module/WEvents/Controller/EventsEditCtl.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/Model/EventsVM.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/Store/EventsEditVMStore.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/WEvents/View/EventsEditView.js?ver=0.0.1"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()"%>" />
    <%-- <link href="css/keywords.css?v=Math.random()" rel="stylesheet" type="text/css" />--%>
    <link href="<%=StaticUrl+"/Module/WEvents/css/keywords.css?v=Math.random()"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/Module/static/css/pagination.css"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article" style="height: 100%;" >
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 100%; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="height: 100%;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>标题
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtTitle" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>活动标识
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtEventId" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width: 100px;
                                    width: 100px;">
                                    活动类型
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="cmbEventTypeID">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" colspan="4">
                                    <div>
                                        <div  style="float: left;">
                                        <font  color="red">*</font>日期
                                        </div>   
                                    <div id="txtStartDate"  style="float: left;"></div>  
                                    <div  style="float: left;"> 至</div>
                                    <div id="txtEndDate"  style="float: left;"></div>
                                    </div>
                                   
                                </td>
                               <%-- <td class="z_main_tb_td2" style="" colspan="3">
                                   
                                </td>--%>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>城市
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCityId" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" colspan="4">
                                    <div>
                                   <div style="float: left;"><font color="red">*</font>时间</div> 
                                     <div id="txtStartTime" style="float: left;">
                                    </div>
                                    <div style="float: left;">
                                        至</div>
                                    <div id="txtEndTime" style="float: left;">
                                    </div>
                                        </div>
                                </td>
                          <%--      <td class="z_main_tb_td2" style="" colspan="3">
                                   
                                </td>--%>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    联系人
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtContact" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" colspan="2">
                                    <div>
                                    <div style="float: left;">&nbsp 电话</div>
                                    <div style="float: left;" id="txtPhoneNumber" style="">
                                    </div>
                                 </div>
                                </td>
                               <%-- <td class="z_main_tb_td2" style="">
                                    <div id="txtPhoneNumber" style="">
                                    </div>
                                </td>--%>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; min-width: 100px;">
                                    邮件发送周期
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="cmbMailSendInterval" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    报名问卷
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="cmbMobileModuleID">
                                    </div>
                                </td>
                              <%--  <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    是否要门票
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="cmbIsTicketRequired" style="">
                                    </div>
                                </td>--%>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" colspan="4">
                                    

                                     <div>
                                    <div style="float: left;">&nbsp 邮箱 </div>
                                     <div id="txtEmail" style="float: left;">
                                    </div>
                                 </div>

                                </td>
                             <%--   <td class="z_main_tb_td2" style="">
                                    <div id="txtEmail" style="">
                                    </div>
                                </td>--%>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    关注引导页面地址
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtbootURL">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    主办方
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtOrganizer">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>地址
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtAddress">
                                    </div>
                                    <div style="position: absolute; left: 545px; top: 258px;" id="txtLongitudeLatitude">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    活动图片
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 554px; top: 290px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    活动简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="divIntro" style="margin-left: 10px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>活动详细
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px; padding-bottom: 10px;
                                    padding-left: 15px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <%-- 设为首页--%>
                                    抽奖条件
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="cmbFlag" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="tdRange" colspan="2">
                                    <div>
                                        <div style="float:left">
                                            <font id="fontRange" style="">距离</font>
                                        </div >
                                         <div style="float:left" id="txtRange">
                                        </div>
                                    </div>

                                    
                                </td>
                                <%--<td class="z_main_tb_td2" style="">
                                    <div id="cmbIsTop" style="">
                                    </div>
                                    <div id="cmbIsDefault" style="">
                                    </div>
                                </td>--%>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" colspan="2">

                                     <div>
                                        <div style="float:left">
                                            <font  style=""> 活动状态</font>
                                        </div >
                                         <div style="float:left" id="txtEventStatus">
                                        </div>
                                    </div>
                                   
                                </td>
                             <%--   <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtEventStatus">
                                    </div>
                                </td>--%>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 400px;" colspan="6" >
                                    <div>
                                       <div style="float:left;width:100px;padding-left: 5px;padding-right: 5px" ><font>是否推荐类型活动</font></div>
                                       <div style="float:left;width:105px"  id="txtIsShare" style=""> </div>
                                       <div style="float:left;" id="txtRewardPoints"><font> 推荐奖励积分(每一人)</font></div>
                                       <div style="float:left"  id="RewardPoints"> </div>
                                     </div>
                                </td>
                                <%--<td class="z_main_tb_td2" style=""  colspan="3">
                                    <div id="txtIsShare" style="">
                                    </div>
                                </td>--%>
                            <%--  <td colspan="2"> 
                                  <div>
                                      
                                  </div>

                              </td>--%>

                            </tr>
                            <tr class="z_main_tb_tr" id="trPoserImegUrl">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="lblPoserImegUrl">
                                    海报图地址
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="5">
                                    <div id="txtPoserImegUrl">
                                    </div>
                                    <div style="position: absolute; left: 554px; top: 778px;">
                                        <input type="button" id="upPoserImegUrl" value=" 选择图片..." />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" id="trShareRemarke" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="lblShareRemarke">
                                    转发消息标题
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="5">
                                    <div id="txtShareRemarke">
                                    </div>
                                </td>
                            </tr>

                            <tr class="z_main_tb_tr" id="trOver" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="lblOver">
                                    转发消息摘要文字
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="5">
                                    <div id="txtOver">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                               <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="lblOver">
                                   增加默认抽奖次数
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="5">
                                    <div id="txtPersonCount">
                                    </div>
                                </td>

                            </tr>
                            <tr class="z_main_tb_tr" id="tr2" >
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;" id="Td2" colspan="6">
                                     <div>
                                       <div style="float:left;width:100px;padding-left: 5px;padding-right: 5px" ><font>使用积分获得重复抽奖机会</font></div>
                                       <div style="float:left;width:105px"  id="IsPointsLottery" style=""> </div>
                                       <div style="float:left" id="txtPointsLottery"><font>&nbsp&nbsp&nbsp&nbsp&nbsp 每次抽奖使用积分</font></div>
                                       <div style="float:left"  id="PointsLottery"> </div>
                                     </div>
                                </td>
                               <%-- <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px; width: 140px;" >
                                    <div id="IsPointsLottery" style="">
                                    </div>
                                </td>--%>
                              <%--  <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    每次抽奖使用积分
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px; ">
                                    <div id="PointsLottery">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    推荐奖励积分
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="RewardPoints">
                                    </div>
                                </td>--%>
                            </tr>

                            <tr class="z_main_tb_tr" style="display: none">
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td2" colspan="6" style="vertical-align: top; line-height: 22px;
                                    width: 400px;">
                                    <b>签到与活动现场</b>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;" colspan="6">
                                    
                                 <div>
                                       <div style="float:left;width:100px;padding-left: 5px;padding-right: 5px" ><font>投票问卷</font></div>
                                       <div style="float:left;width:105px"  id="txtPollQues" style=""> </div>
                                       <div style="float:left" ><font>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 抽奖方式选择</font></div>
                                       <div style="float:left"  id="txtDrawMethod"> </div>
                                     </div>


                                </td>
                           <%--     <td class="z_main_tb_td2" colspan="1" style="padding-top: 0px;">
                                    <div id="txtPollQues">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                      签到欢迎语模板
                                    抽奖方式选择
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="padding-top: 0px;">
                                   <div id="txtModelId">
                                    </div>
                                    <div id="txtDrawMethod">
                                    </div>
                                </td>
                             <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    每人可抽奖次数
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    
                                </td>--%>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div style="float: left;">
                                        <div id="btnWXImage">
                                        </div>
                                    </div>
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;" colspan="3">
                                    <div id="txtDimensionalCodeURL" style="float: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                             <%--   <td colspan="6">
                                    <div>
                                        <div>
                                            <span>二维码预览</span>
                                              <div class="uploadWarp">
                                              <p class="viewImage" id="Rcodeimage"> 未获取二维码的图片</p>
                                              </div>
                                          </div>   
                                        <div>
                                             <span> 转发消息图标</span>
                                             <div class="uploadWarp">
                                              <p class="viewImage" id="icoimage"> 未上传转发消息图标</p>
                                              </div>
                                        </div>
   
                                </td>--%>

                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    二维码预览
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; padding-left: 14px; height: 175px"
                                    colspan="2">
                                    <img id="imgView" alt="" src="" width="156px" height="156px" />
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    转发消息图标
                                    <td class="z_main_tb_td2" style="padding-top: 10px; padding-left: 14px; vertical-align: top">
                                        <img id="imgeupload" alt="" src="" width="120px" height="90px" />
                                    </td>
                                    <td>
                                        <div class="z_main_tb_td" style="vertical-align: top;">
                                            <input type="button" id="uploadImageUrl" value=" 选择图片..." />
                                        </div>
                                    </td>
                                </td>
                            </tr>
                            <%--   <%-- <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    转发消息图标
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; padding-left: 14px; height: 175px"
                                    colspan="3">
                                    <img id="imgeupload" alt="" src="" width="120px" height="90px" />
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;" colspan="2">
                                    <%--  <input type="button" id="uploadImageUrl" value=" 选择图片 " style="margin-left: 10px" />
                                 
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    签到欢迎语消息
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtModelId">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td colspan="6">
                                    <div class="defaultReplyArea">
                                        <div class="tempEditArea">
                                            <!-- 文本编辑 -->
                                            <div class="commonItem clearfix" id="contentEditor" name="elems">
                                                <span class="tit">文本编辑</span>
                                                <div class="handleWrap mt-45">
                                                    <textarea id="text"></textarea>
                                                </div>
                                            </div>
                                            <!-- 图文消息 -->
                                            <div class="imgTextMessage hide" id="imageContentMessage" name="elems">
                                                <h2>
                                                    提示:按住鼠标左键可拖拽排序图文消息显示的顺序 <b>已选图文</b>&nbsp;&nbsp;<b id="hasChoosed" style="color: Red">0</b>&nbsp;&nbsp;个</h2>
                                                <div class="list">
                                                </div>
                                                <span class="addBtn">添加</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    举办点经度
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtLongitude">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    举办点维度
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtLatitude">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    二维码号码
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWXCode2">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    管理员
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="padding-top: 0px;">
                                    <div id="txtWEventAdmin">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 140px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtDisplayIndex">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    链接跳转
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtUrl">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <!-- 添加图文消息-弹层 -->
    <div class="ui-mask hide" id="ui-mask">
    </div>
    <div class="activeListPopupArea hide" id="chooseEvents">
    </div>
    <div class="addImgMessagePopup" id="addImageMessage">
        <div class="commonTitleWrap">
            <h2>
                添加图文消息</h2>
            <span class="cancelBtn">取消</span> <span class="saveBtn">确定</span>
        </div>
        <div class="addImgMessageWrap clearfix">
            <span class="tit">标题</span>
            <input type="text" id="theTitle" class="inputName" />
            <span class="tit">分类</span>
            <select class="selectBox" id="imageCategory">
                <option selected>请选择</option>
            </select>
            <span class="queryBtn">查询</span>
        </div>
        <div class="radioList" id="imageContentItems">
        </div>
    </div>
    <div id="sortHelper" style="display: none;">
        &nbsp;</div>
    <div id="dragHelper" style="position: absolute; display: none; cursor: move; list-style: none;
        overflow: hidden;">
    </div>
    <!--关键字项-->
    <script id="keywordItemTmpl" type="text/html">
    <tr>
        <th class="num">序号</th>
        <th class="word">关键字</th>
    </tr>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <tr data-keyword="<#=JSON.stringify(item)#>">
            <td class="num"><#=i+1#></td>
            <td class="word">
                <#=item.KeyWord#>
            </td>
        </tr>
    <#}#>
    </script>
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
    <!--菜单模板-->
    <script id="menuTmpl" type="text/html">
    <div class="modelBox">
        <div class="menuWrap">
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <span data-menu="<#=JSON.stringify(item)#>"  class="<#=i==0?'on':''#> <#=((item.Status==1)?'select':'')#>">
                    <b><#=item.Name#></b>
                    <div data-menu="<#=JSON.stringify(item)#>"  class="subMenuWrap">
                        <em class="pointer"></em>
                        <a href="javascript:;" data-parentId="<#=item.MenuId#>" class="addBtn">添加</a>
                        <#for(var j=0;j<item.SubMenus.length;j++){ var subItem=item.SubMenus[j];if(subItem!=null){#>
                            <a href="javascript:;"   data-menu="<#=JSON.stringify(subItem)#>" class="tempSubMenu <#=subItem.Status==1?'select':''#>"><#=subItem.Name#></a>
                            <#}#>
                        <#}#>
                    </div>
                </span>
            <#}#>
        </div>
    </div>
    </script>
    <script id="accountTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value=<#=item.ApplicationId#>><#=item.WeiXinName#></option>
    <#}#>
    </script>
    <!--option模板-->
    <script id="optionTmpl" type="text/html">
    <#showAll=showAll?showAll:false;if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value="<#=item.TypeId#>" data-obj=<#=JSON.stringify(item)#>><#=item.TypeName#></option>
    <#}#>
    </script>
    <!--option模板  模块-->
    <script id="moduleTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.PageID#>" value=<#=JSON.stringify(item)#>><#=item.ModuleName#></option>
    <#}#>
    </script>
    <!--活动类别模板-->
    <script id="eventTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.EventTypeId#>" value=<#=JSON.stringify(item)#>><#=item.EventTypeName#></option>
    <#}#>
    </script>
    <!--资讯类别模板-->
    <script id="NewsTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.NewsTypeId#>" value=<#=JSON.stringify(item)#>><#=item.NewsTypeName#></option>
    <#}#>
    </script>
    <!--弹出层的模板-->
    <script id="popDivTmpl" type="text/html">
    <div class="commonTitleWrap">
    	<h2><#=topTitle#></h2>
        <span id="cancelBtn" class="cancelBtn">取消</span>
        <span id="saveBtn" class="saveBtn">确定</span>
    </div>

    <div class="activeQueryWrap clearfix">
        <span class="tit" ><#=popupName?popupName:"活动名称"#></span>
        <input id="eventName" type="text" class="inputName" />
        <span class="tit"><#=popupSelectName?popupSelectName:"活动分类"#></span>
        <select id="pop_eventsType" class="selectBox">
        	<option selected>请选择</option>
        </select>
        <span id="searchEvents" class="queryBtn">搜索</span>
    </div>
    
    <div class="activeListWrap">
        <table width="1038" border="1" id="itemsTable">
          
        </table>
    </div>
    <div class="pagination">
      <a href="#" class="first" data-action="first">&laquo;</a>
      <a href="#" class="previous" data-action="previous">&lsaquo;</a>
      <input type="text" readonly="readonly" data-max-page="40" />
      <a href="#" class="next" data-action="next">&rsaquo;</a>
      <a href="#" class="last" data-action="last">&raquo;</a>
    </div>

    </script>
    <!--弹层的每行数据-->
    <script id="itemTmpl" type="text/html">
    <tr>
    <th width="65">操作</th>
    <#for(var i=0;i<title.length;i++){ var item=title[i];#>
    <th><#=item#></th>
    <#}#>
    </tr>
    <#if(type=="chooseNews"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input  data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.NewsName#></td>
                    <td><#=item.NewsTypeName#></td>
                    <td><#=item.PublishTime#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
     <#if(type=="chooseEvents"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.EventName#></td>
                    <td><#=item.EventTypeName#></td>
                    <td><#var result="";switch(item.EventStatus){case 10:result="未开始";break;case 20:result="运行中";break;case 30:result="暂停";break;case 40:result="停止";break;default:result="未定义";break;}#><#=result#></td>
                    <td><#=item.DrawMethod#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
    </script>
</asp:Content>
