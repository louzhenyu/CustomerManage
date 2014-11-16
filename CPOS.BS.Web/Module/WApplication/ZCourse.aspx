<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>课程管理</title>
    
    <script src="/Framework/javascript/Biz/ZCourse.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ZCourseSelectTree.js" type="text/javascript"></script>

    <script src="Model/ZCourseVM.js" type="text/javascript"></script>
    <script src="Store/ZCourseVMStore.js" type="text/javascript"></script>
    <script src="View/ZCourseView.js" type="text/javascript"></script>
    <script src="Controller/ZCourseCtl.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/kindeditor.js"></script>
    <script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div>
                <div id="tabsMain" style="width:100%; height:70px;"></div>
                <div id="tabInfo" style="height:61px; background:rgb(241, 242, 245);">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <table class="z_main_tb" style="width:450px;">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px; width:100px; text-align:right;">课程：</td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtZCourse" style="margin-top:5px;font-weight:;"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:32px;">
                                    <div id="btnSearch" style="margin-top:5px;"></div>
                                    <input id="hCourseId" type="hidden" value=""/>
                                </td>
                                <%--<td class="z_main_tb_td2" style="vertical-align:top; line-height:32px;">
                                    <div id="btnReset" style="margin-top:5px;"></div>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="pnlInfo" style="margin-top:10px; clear:both;">
                <%--<div id="tabsMain3" style="width:100%; height:370px;"></div>--%>
                <div id="tabInfo3" style="height:260px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">课程描述：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCourseDesc" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_2" style="height:260px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">课程简介：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCourseSummary" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_3" style="height:168px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">课程费用：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCourseFee" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_8" style="height:168px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">课程时间：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCourseStartTime" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_9" style="height:168px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">授课老师：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCouseCapital" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_10" style="height:168px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px; width:100px; float:left; text-align:right;">联系方式：</div>
                    <div class="" style="width:800px; float:left; padding-left:10px;">
                        <div id="txtCouseContact" style="margin-top:5px;"></div>
                    </div>
                </div>
                <%--<div id="tabInfo3_8" style="height:40px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px;width:100px; float:left; text-align:right;">课程时间：</div>
                    <div class="" style="width:500px; float:left;">
                        <div id="txtCourseStartTime" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_9" style="height:40px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px;width:100px; float:left; text-align:right;">授课老师：</div>
                    <div class="" style="width:500px; float:left;">
                        <div id="txtCouseCapital" style="margin-top:5px;"></div>
                    </div>
                </div>
                <div id="tabInfo3_10" style="height:40px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px;width:100px; float:left; text-align:right;">联系方式：</div>
                    <div class="" style="width:500px; float:left;">
                        <div id="txtCouseContact" style="margin-top:5px;"></div>
                    </div>
                </div>--%>
                <div id="tabInfo3_11" style="height:40px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px;width:100px; float:left; text-align:right;">邮箱：</div>
                    <div class="" style="width:450px; float:left;">
                        <div id="txtEmail" style="margin-top:5px;"></div>
                    </div>
                    <div style="float:left; margin-top:5px;">注：接收学员报名信息</div>
                </div>
                <div id="tabInfo3_12" style="height:40px; background:rgb(241, 242, 245);border:1px solid #ddd; clear:both;">
                    <div style="font-weight:; padding:4px;width:100px; float:left; text-align:right;">邮件标题：</div>
                    <div class="" style="width:450px; float:left;">
                        <div id="txtEmailTitle" style="margin-top:5px;"></div>
                    </div>
                </div>

                <div id="tabsMain3" style="width:100%; height:370px; margin-top:10px;"></div>
                <div id="tabInfo3_4" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <%--<div style="height:5px;"></div>
                        <div id="btnAddCourseApply" style="margin-bottom:5px;"></div>--%>
                        <div class="DivGridView" id="gridCourseApply">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_5" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddCourseReflections" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="gridCourseReflections">
                        </div>
                    </div>
                </div>
                <div id="tabInfo3_6" style="height:1px; overflow:hidden;">
                    <div class="" style="">
                        <div style="height:5px;"></div>
                        <div id="btnAddNews" style="margin-bottom:5px;"></div>
                        <div class="DivGridView" id="gridNews">
                        </div>
                    </div>
                </div>
            </div>

            <div id="pnlSave" style="display:none; height:48px; background:rgb(241, 242, 245); border:1px solid #ddd; 
                padding-top:10px; padding-right:10px; margin-top:10px;">
                <div id="btnSave" style="float:left;"></div>
            </div>

        </div>
    </div>

</asp:Content>
