<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" 
 Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Controller/TemplateCtl.js" type="text/javascript" charset="gb2312"></script>
    <script src="Model/TemplateVM.js" type="text/javascript" charset="gb2312"></script>
    <script src="Store/TemplateStore.js" type="text/javascript" charset="gb2312"></script>
    <script src="View/TemplateView.js" type="text/javascript" charset="gb2312"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
        <div class="m10 article">
            <%--<div class="art-tit">
                <div id="view_Search" class="view_Search" >
                      <span id='span_panel'></span>
                </div>
            </div>--%>
            <div class="art-titbutton" >
                <div style="width:100%; padding:0px; border:0px solid #d0d0d0;">
                <div id="tabsMain" style="width:100%; height:40px;"></div>

                <div id="tab1" style="height:30px; background:rgb(241, 242, 245);">
                    
                </div>
                <div id="tab2" style="height:1px; overflow:hidden;">
                    
                </div>
                <div id="tab3" style="height:1px; overflow:hidden;">
                    
                </div>
                <div id="tab4" style="height:1px; overflow:hidden;">
                    
                </div>
                <div id="tab5" style="height:1px; overflow:hidden;">
                    
                </div>
                <div style="width:100%; padding-left:10px; padding-right:10px;">
                   <div id="gridTab1"></div>
                </div>

               
                
            </div>

            </div>
            <div class="DivGridView" id="DivGridView">
            </div>

           

              <div id="divModify" style="height:1px; overflow:hidden;"> 
                    <div style="width:100%; padding-left:10px; padding-right:10px;">
                     <div class="z_event_border" style="padding-left:10px; border-top:0px;">
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                            <td>简要描述:<div id="txtTemplateDesc1" style="margin-top:-5px;"></div></td>
                            
                            </tr>

                            <tr class="z_main_tb_tr" style=" height:22px;">
                            <td style="vertical-align:top; line-height:22px; height:22px;">
                                微信话术内容：<div id="txtTemplateId" style="margin-top:-5px;"></div>
                                          <div id="txtTemplateType" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td"  style="vertical-align:top; line-height:22px;">
                                <div id="txtTemplateDesc" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                            <tr class="z_main_tb_tr" style=" height:22px;">
                            <td style="vertical-align:top; line-height:22px; height:22px;">
                                短信话术内容：
                                          <div id="txtTemplateTypeSMS" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td"  style="vertical-align:top; line-height:22px;">
                                <div id="txtTemplateDescSMS" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                            <tr class="z_main_tb_tr" style=" height:22px;">
                            <td style="vertical-align:top; line-height:22px; height:22px;">
                                APP话术内容：
                                          <div id="txtTemplateTypeAPP" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                            <td class="z_main_tb_td"  style="vertical-align:top; line-height:22px;">
                                <div id="txtTemplateDescAPP" style="margin-top:-5px;"></div>
                            </td>
                            </tr>
                        </table>
                    </div>

                        <div style="height:5px;"></div>
                        
                        <div class="art-titbutton">
                            <div class="view_Button">
                                <span id="btnEmpty"></span>
                                <span id="btnSave"></span>
                            </div>
                        </div>

                    </div>
                </div>

        </div>
    </div>
</asp:Content>
