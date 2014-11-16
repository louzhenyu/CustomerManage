<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>活动内容</title>
    <script src="/Framework/javascript/Biz/ZCourse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ZForumType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
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
                <div id="tabInfo" style="height: 591px; background: rgb(241, 242, 245);">
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
                                    <font color="red">*</font>类别
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtForumType">
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
                                    城市
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtCity" style="">
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
                                    课程
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtCourse" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    是否报名
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtIsSignUp" style="">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;min-width:100px;">
                                    
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>报名邮件
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtEmail">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>报名邮件抬头
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtEmailTitle">
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
                                    <div style="position: absolute; left: 554px; top: 213px;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr" style="">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;padding-bottom:10px; 
                                    padding-left:15px;">
                                    <div id="txtContent">
                                    </div>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                    <div style="width:750px; margin-left:20px; margin-top:5px;">
                        <div id="btn1" class="z_tb1" onclick="fnChange(1)">简介</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn2" class="z_tb2" onclick="fnChange(2)">组织者</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn3" class="z_tb2" onclick="fnChange(3)">日程安排</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn4" class="z_tb2" onclick="fnChange(4)">场地膳食</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn5" class="z_tb2" onclick="fnChange(5)">赞助</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn6" class="z_tb2" onclick="fnChange(6)">圆桌会议</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn7" class="z_tb2" onclick="fnChange(7)">演讲嘉宾</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn8" class="z_tb2" onclick="fnChange(8)">往届论坛</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn9" class="z_tb2" onclick="fnChange(9)">联系我们</div>
        <div style=" width:10px; float:left; height:31px; display:block; overflow:hidden; border-bottom:1px solid #bbbbbb;"></div>
                        <div id="btn10" class="z_tb2" onclick="fnChange(10)">注册参加</div>
                    </div>
                    <div style="width:750px; margin-left:20px;">
                        <div id="pnl1">
                            <div id="txtContent"></div>
                        </div>
                        <div id="pnl2">
                            <div id="txtContent2"></div>
                        </div>
                        <div id="pnl3">
                            <div id="txtContent3"></div>
                        </div>
                        <div id="pnl4">
                            <div id="txtContent4"></div>
                        </div>
                        <div id="pnl5">
                            <div id="txtContent5"></div>
                        </div>
                        <div id="pnl6">
                            <div id="txtContent6"></div>
                        </div>
                        <div id="pnl7">
                            <div id="txtContent7"></div>
                        </div>
                        <div id="pnl8">
                            <div id="txtContent8"></div>
                        </div>
                        <div id="pnl9">
                            <div id="txtContent9"></div>
                        </div>
                        <div id="pnl10">
                            <div id="txtContent10"></div>
                        </div>
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
