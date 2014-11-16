<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>充值</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
        <link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.2" rel="stylesheet" type="text/css" />
        <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="allPage" id="section" data-js="js/recharge">
	<span class="packup"></span>
	<div class="commonNav">
    	<a href="javascript:;" class="on">充值</a>
        <a href="Consumption.aspx" >消费</a>
        <a href="TransactionList.aspx" >交易记录<em></em></a>
    </div>
    
    <div class="contentArea">
    	<div class="commonTitWrap">
        	<span>充值</span>
        </div>
        
        
        <div class="recharge-list">
            <dl>
                <dt><span>步骤<i>1</i></span> 卡验证</dt>
                <dd>
                    <input type="text" id="cardNo" placeholder="输入卡号" class="iptTxt">
                    <a href="javascript:;" id="queryCard"  class="btn-green" >查询</a>
                </dd>
                <dd>
                    <p id="queryContent" style="display:none;">本卡状态:<span id="cardStatus">已使用</span>  &nbsp;&nbsp;&nbsp;&nbsp;     卡内余额:<span id="lastMoney">360.00</span></p>
                </dd>
            </dl>
            <dl>
                <dt><span>步骤<i>2</i></span> 选择会员</dt>
                <dd id="choose"><a href="javascript:;" id="chooseVip" class="btn-green">选择</a> </dd>
                <dd id="repeatChoose" style="display:none;"> 已选择了<span style="color:#5fafe4; padding-right:10px" id="chooseName">刘丽丽</span> <a href="javascript:;" id="repChoose" class="btn-green">重新选择</a> </dd>
            </dl>
            <dl>
                <dt><span>步骤<i>3</i></span> 手机号码确认</dt>
                <dd>
                    <p id="phoneContent">该会员的手机号码为:<span id="phone">--</span></p>
                    <!--<a href="javascript:;" id="changePhone" class="btn-green">修改</a> </dd>-->
            </dl>
            <dl>
                <dt><span>步骤<i>4</i></span> 充值</dt>
                <dd>
                    <input type="password" id="password"  placeholder="输入密码" class="iptTxt">
                    <a href="javascript:;"  id="intoMoney" class="btn-green" >充值</a>
                </dd>
            </dl>
        </div>
        <div id="vips" class="hide" style="top: 20%;left: 22%;position: fixed;z-index: 9999;">
            <div class="popup02" id="popbox">
                <div class="popup02-t">
                    <input type="text" placeholder="手机号/身份证/会员名" id="name" class="iptTxt">
                    <a href="javascript:;" id="queryVip"  class="btn-green" >查询</a>
                    <!--<input type="button" value="添加新会员" class="btn-green" />-->
                    <a href="javascript:;" class="btn-close hintClose">&times;</a>
                </div>
                <div class="popup02-m mana-card-m">
                    <table width="800" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <th align="center" scope="col">会员账号</th>
                            <th align="center" scope="col">会员名</th>
                            <th align="center" scope="col">微信昵称</th>
                            <th align="center" scope="col">手机号</th>
                            <th align="center" scope="col">身份证</th>
                            <th align="center" scope="col">实体卡号</th>
                            <th align="center" scope="col">余额</th>
                        </tr>
                        <tbody id="content">
                            <tr >
                                <td style="height: 200px;text-align: center;vertical-align: middle;" colspan="7" align="center">输入条件搜索内容</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="popup02-b">
                    <div id="kkpager">&nbsp;<span class="disabled">首页</span><span class="disabled">上一页</span><span class="curr">1</span><a title="下一页" onclick="return kkpager._clickHandler(2)" href="http://localhost:2860/module/VipCardCenter/AllBrowser.aspx#">下一页</a><a title="尾页" onclick="return kkpager._clickHandler(4)" href="http://localhost:2860/module/VipCardCenter/AllBrowser.aspx#">尾页</a>&nbsp;转到<span id="kkpager_gopage_wrap">
                        <input type="button" value="确定" onclick="kkpager.gopage()" id="kkpager_btn_go">
                        <input type="text" value="2" onblur="kkpager.blur_gopage()" onkeypress="return kkpager.keypress_gopage(event);" onfocus="kkpager.focus_gopage()" id="kkpager_btn_go_input">
                        </span>页</div>
                </div>
            </div>
        </div>
        
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