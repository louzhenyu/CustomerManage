<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>页面详情页</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/module/CustomerPageConfig/css/style.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="customerId" type="hidden" value="<%=this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.ToString()%>" />
    <%--data-js="CustomerPageConfig/pageEdit"，是以下面的main.js里的require的路径来做为依据的--%>
     <div id="section" data-js="CustomerPageConfig/js/pageEdit" class="section m10">
        <div class="scratchCardSetArea">
	        <div class="commonTitleWrap">
                <%--<span class="cancelBtn">取消</span>--%>
                <span id="saveBtn" class="saveBtn">保存</span>
            </div>
    
            <div class="infoShowArea">
                <div id="baseInfo">
                
		            
                </div>
            </div>
    
            <div class="tempEditArea">
    	        <div class="areaWrap">
        	        <div id="pageInfo">
                    
                    </div>
            
                    <div id="paramsList">
                    
                    </div>
        
                </div>
            </div>
    
        </div>



        <!-- 刮刮卡设置-弹层 -->
        <div class="scratchCardPopup" style="display:none;">
	        <div class="commonTitleWrap">
    	        <h2>刮刮卡设置</h2>
                <span class="cancelBtn">取消</span>
                <span class="saveBtn">确定</span>
            </div>
            <div>
    	        <div class="setList on">
        	        <span class="checkBox"></span>
                    <p class="text">零售通用版——标准版</p>
                </div>
                <div class="setList">
        	        <span class="checkBox"></span>
                    <p class="text">零售通用版——标准版</p>
                </div>
                <div class="setList on">
        	        <span class="checkBox"></span>
                    <p class="text">零售通用版——标准版</p>
                </div>
            </div>
        </div>
        <div class="ui-mask"></div>
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/Module/static/js/main"%>"></script>
</asp:Content>
