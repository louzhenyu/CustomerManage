<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>访问统计</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<link rel="stylesheet" href="<%=StaticUrl+"/Module/Vip/Vip14/css/global.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/Vip/Vip14/css/style.css"%>" />

    <link rel="stylesheet" href="<%=StaticUrl+"/Module/static/css/daterangepicker/ui.daterangepicker.css"%>" type="text/css" />
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/static/css/daterangepicker/redmond/jquery-ui-1.7.1.custom.css"%>" type="text/css" title="ui-theme" />
    <style>
		table {background: #fff;}
        table tr th{font-weight: bold;}
        table tr td,table tr th{text-align: center;vertical-align: middle;padding:5px 10px;}
        /*.contentArea {
    margin-left:0px;
    float: left;
}*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <iframe id="myFrame" width="100%" height="338" src="http://stats.aladingyidong.com/index.php?module=Widgetize&action=iframe&moduleToWidgetize=Dashboard&actionToWidgetize=index&idSite=1&period=day&date=today&token_auth=e6680a32bf1ec624febb891bf79b1db8&segment=eventAction=@b0b946bd761c47898eaea4636cd45151" scrolling="no" frameborder="0" marginheight="0" marginwidth="0"></iframe>


	<div style="height: 40px; line-height:40px; text-align: left;">
	    <input type="text" value="" placeholder="请选择日期" id="dataInput" />
	</div>
	<div  id="section" data-js="/Module/Statistics/js/index" class=" m10 activityAnalyze">
        <div style="text-align:center"><%--class="commonMenu"--%>
            <span class="on one" ><em>访问数|转发数</em></span>
        </div>
		<table id="table">
            <thead>
                <tr class="title">
                    <th>统计对象</th>
                    <th>访问数</th>
                    <th>转发数</th>
                </tr>
            </thead>
            <tbody>

            </tbody>
        </table>
	</div>
	<script id="tableTemp" type="text/html">
	    <$for(var i=0,idata;i<list.length;i++){idata=list[i];$>
	        <tr>
	            <td><$=idata.name $></td>
	            <td><$=idata.visit $></td>
	            <td><$=idata.share $></td>
	        </tr>
	    <$}$>
	</script>
	<%--<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/static/js/mainNew"></script>--%>
	
	
</asp:Content>
