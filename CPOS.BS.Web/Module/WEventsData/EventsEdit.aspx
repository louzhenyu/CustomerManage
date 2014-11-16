<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动内容</title>
    <script src="/Framework/javascript/Biz/Options.js" type="text/javascript"></script>

    <script src="/Framework/javascript/Biz/EventType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/CitySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/LEventSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventRange.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventCheckinType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/QuestionnaireType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WeiXinPublic.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WApplicationInterface2.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WModel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/EventStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WEventAdmin.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/WQRCodeType.js" type="text/javascript"></script>

    <script src="Controller/EventsEditCtl.js" type="text/javascript"></script>
    <script src="Model/EventsVM.js" type="text/javascript"></script>
    <script src="Store/EventsEditVMStore.js" type="text/javascript"></script>
    <script src="View/EventsEditView.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 1px solid #d0d0d0;">
                <div id="tabInfo" style="height: 900px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb" style="">
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
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px; width:100px;">
                                    上一级活动
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtParentEvent">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>日期
                                </td>
                                <td class="z_main_tb_td2" style=" " colspan="3">
                                    <div id="txtStartDate" style="float:left;"></div>
                                    <div style="float:left;">至</div>
                                    <div id="txtEndDate" style="float:left;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>城市
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCityId" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>时间
                                </td>
                                <td class="z_main_tb_td2" style="" colspan="3">
                                    <div id="txtStartTime" style="float:left;"></div>
                                    <div style="float:left;">至</div>
                                    <div id="txtEndTime" style="float:left;"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    联系人
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtContact" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    电话
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtPhoneNumber" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    邮箱
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtEmail" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    设为首页
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="cmbIsDefault" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    设为置顶
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="cmbIsTop" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
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
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtAddress">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    活动图片
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtImageUrl">
                                    </div>
                                    <div style="position: absolute; left: 554px; top: 245px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;padding-bottom:10px; 
                                    padding-left:15px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr" style="border-top:1px solid #ddd;">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <b>设定活动参数</b>
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    
                                </td>
                            </tr>--%>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>签到范围
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCheckinRange"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>签到类型
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCheckinType"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    微信账号
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtWeiXinPublic" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    签到欢迎语模板
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtModelId"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    链接跳转
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtUrl">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    报名问卷
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtApplyQues"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    投票问卷
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="padding-top: 0px;">
                                    <div id="txtPollQues"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    管理员
                                </td>
                                <td class="z_main_tb_td2" colspan="1" style="padding-top: 0px;">
                                    <div id="txtWEventAdmin"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    举办点经度
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtLongitude"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    举办点维度
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtLatitude"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    活动状态
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtEventStatus"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    排序
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtDisplayIndex"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width:140px;">
                                    每人可抽奖次数
                                </td>
                                <td class="z_main_tb_td2"  style="padding-top: 0px;">
                                    <div id="txtPersonCount"></div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    固定二维码
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWXCode"></div>
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                    <div style=" float:left;"><div id="btnWXImage"></div> 
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;" colspan="3">
                                    <div id="txtDimensionalCodeURL" style="float:left;"></div>
                                </td>
                          </tr>  
                          <tr class="z_main_tb_tr">   
                                 <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    图片预览
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; padding-left:14px" colspan="3">
                                    <img id="imgView" alt="" src="" width="256px" height="156px" />
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    二维码号码
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWXCode2"></div>
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
</asp:Content>
