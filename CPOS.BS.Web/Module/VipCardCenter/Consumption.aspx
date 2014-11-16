<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>消费</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
        <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/consumption">
	<span class="packup"></span>
	<div class="commonNav-sub">
    	 <a href="javascript:void(0)" onclick='AddMid("Recharge.aspx")' >充值</a>
        <a href="javascript:;" class="on">消费</a>
        <a href="javascript:void(0)" onclick='AddMid("TransactionList.aspx")' >交易记录<em></em></a>
    </div>
    
    <div class="contentArea_vipCard">
    	<div class="commonTitWrap">
        	<span>消费</span>
        </div>
        <div class="mana-card">
            <div class="mana-card-t">
                <ul class="clearfix">
                    <li>
                        <input type="text" id="name" placeholder="手机号/身份证/会员名" style="width:190px" />
                    </li>
                    <li><a href="javascript:;" id="queryVip" class="btn-daochu">查询</a></li>
                    <!--<li><a href="javascript:;" class="btn-daochu">添加新会员</a></li>-->
                </ul>
            </div>
            <div class="mana-card-m">
                <table id="queryResult" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <th align="center" scope="col">会员账号</th>
                        <th align="center" scope="col">会员名</th>
                        <th align="center" scope="col">微信昵称</th>
                        <th align="center" scope="col">手机号</th>
                        <th align="center" scope="col">身份证</th>
                        <th align="center" scope="col">实体卡号</th>
                        <th align="center" scope="col">余额</th>
                        <th align="center" scope="col">消费</th>
                    </tr>
                    <tbody id="content">
                        <tr >
                            <td style="height: 200px;text-align: center;vertical-align: middle;" colspan="8" align="center">输入条件搜索内容</td>
                        </tr>
                    </tbody>
                </table>
                <div id="kkpager"></div>
            </div>
        </div>
    </div>
    
</div>
<div id="popDiv" style="display:none;position: fixed;z-index: 9999;left: 35%;top: 35%;">
    <div class="popup" id="popbox" style="width:300px;">
        <div class="popup-t">消费</div>
        <a href="javascript:;" class="btn-close hintClose">×</a>
        <div class="popup-m">
            <div class="tel-form">
                <ul>
                    <li>
                        <input type="text" id="amount" placeholder="输入消费金额" class="iptTxt">
                    </li>
                    <li>
                        <input type="text" id="code" placeholder="输入短信验证码" class="iptTxt">
                        <input type="button" id="btnSend" value="发送" class="ipt-btn" />
                    </li>
                    <li>
                        <input type="text" id="documentCode" placeholder="房型、备注等" class="iptTxt">
                    </li>
                </ul>
            </div>
        </div>
        <div class="popup-b"> <a href="javascript:;" id="btnSure">确 定</a> </div>
    </div>
</div>
<!-- 提示弹层 -->
<div id="ui-mask" class="ui-mask"></div>
<script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-item="<#=JSON.stringify(item)#>">
            <td><#=item.VipCode#></td>
            <td><#=item.VipRealName#></td>
            <td><#=item.VipName#></td>
            <td><#=item.Phone#></td>
            <td><#=item.IdCard#></td>
            <td><#=item.VipCode#></td>
            <td><#=item.Col4#></td>
            <td><a href="javascript:;" class="btn-pay">消费</a></td>
        </tr>
    <#}#>
</script>
<script id="tpl_date" type="text/html">
    <span data-day="-1">请选择</span>
    <#for(var i=0;i<list.length;i++){var item=list[i];#>
        <span data-day="<#=item.date#>"><#=item.dateName#></span>
    <#}#>
    <span data-day="0">自定义</span>
</script>
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="/Module/VipCardCenter/js/main.js"></script>
</asp:Content>