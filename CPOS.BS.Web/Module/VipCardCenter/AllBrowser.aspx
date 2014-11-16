<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
     <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <title>总览</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/allBrowser">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	<a href="javascript:;" class="on">总览</a>
         <a href="javascript:void(0)" onclick='AddMid("MakeNewCard.aspx")' >制新卡</a>
         <a href="javascript:void(0)" onclick='AddMid("CardManage.aspx")'>卡管理<em></em></a>
    </div>
    
    <div class="contentArea_vipCard">
    	<div class="commonTitWrap">
        	<span>总览</span>
        </div>
        
        
        <!-- 表格展示 -->
        <div class="dataTableShow"  >
            <table  id="queryResult" class="display" cellspacing="0" width="100%">
                <thead>
                     <tr class="title">
                        <td >渠道</td>
                        <td >制卡数量(金额)</td>
                        <td >激活数量(金额)</td>
                    </tr>
                </thead>
                <tbody id="content">
                    
                </tbody>
            </table>
            <div id="kkpager"></div>
        </div>
        
    </div>

</div>
<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-orderPayId="<#=''#>">
            <td><#=item.ChannelTitle#></td>
            <td><#=item.Amount#></td>
            <td><#=item.ActivatedAmount+"元"#></td>
        </tr>
    <#}#>
</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/VipCardCenter/js/main.js"></script>
</asp:Content>