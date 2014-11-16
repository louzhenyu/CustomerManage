<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    CodeBehind="PolicySettings.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.WXSalesPolicy.PolicySettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>返利设置</title>
    <link href="../styles/css/reset-pc.css" rel="stylesheet" type="text/css" />
    <link href="../styles/css/common-layout.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/artDialog.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="scripts/policySetting">
        <div class="commonTitWrap">
            <span>各部分订单金额的返现比例为</span>
        </div>
        <div id="settings">
            <span>加载中...</span>
        </div>
        <div class="tips"></div>
        <div>
            <input type="button" class="commonBtn btnSave" value="保存" />
        </div>
        <script id="tpl_setting" type="text/html">
            <# var len = list.length;#>
            <#for(var i=0;i<len;i++){ var item=list[i];#>
                <div data-rateid="<#=item.RateId#>" class="line noEmpty">
                    <input maxlength="7"   data-min="0" data-max="9999999" value="<#=item.AmountBegin#>" type="text" /><span class="to">~</span><input maxlength="7"  data-min="0" data-max="9999999" value="<#=item.AmountEnd#>"  type="text" /><span  class="p">部分</span><span class="c">返利比例为</span><input maxlength="5" data-min="0" data-max="1" value="<#=item.Coefficient#>"  type="text" /><span>%</span>
                </div>
            <#}#>
            <#if(len<10){var count=10-len;#>
                <#for(var m=0;m<count;m++){#>
                <div data-rateid="" class="line emptyInput">
                    <input maxlength="7"    data-min="0" data-max="9999999" type="text" /><span class="to">~</span><input data-min="0" maxlength="7"  data-max="9999999"  type="text" /><span class="p">部分</span><span class="c">返利比例为</span><input maxlength="5" data-min="0" data-max="1"  type="text" /><span>%</span>
                </div>
                <#}#>
            <#}#>
        </script>
    </div>
    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" data-main="scripts/main"></script>
</asp:Content>
