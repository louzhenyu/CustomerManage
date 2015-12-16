<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>帮助中心</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/helpCenter/css/style.css?v1.2"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="section">
    <div class="allPage">
       <div class="help">
	<div class="Novice">
    	<div class="novice_title"><h1>新手入门</h1><span>获得宝典  勤加练习</span></div>
        <div class="novice_nr">
            <ul id="novice_nr">
                <li class="selectTag"><a onmouseover="selectTag('tagContent0',this)" href="javascript:void(0)">云店</a></li>
                <li><a onmouseover="selectTag('tagContent1',this)" href="javascript:void(0)">门店</a></li>
                <li><a onmouseover="selectTag('tagContent2',this)" href="javascript:void(0)">会员</a></li>
                <li><a onmouseover="selectTag('tagContent3',this)" href="javascript:void(0)">掌柜APP</a></li>
            </ul>
            <div id="tagContent">
                <div class="tagContent selectTag" id=tagContent0>
                    <ul>
                        <li><a target="_blank" href="http://help.chainclouds.cn/?p=785"><img src="images/tp01.png" /><span>微信服务号授权</span></a></li>
                        <li><a  target="_blank" href="http://help.chainclouds.cn/?p=652"><img src="images/tp02.png" /><span>上传并发布商品</span></a></li>
                        <li><a target="_blank" href="http://help.chainclouds.cn/?p=772"><img src="images/tp03.png" /><span>装修云店</span></a></li>
                    </ul>
                </div>
                <div class="tagContent" id="tagContent1">
                    <ul>
                        <li><a  target="_blank" href="http://help.chainclouds.cn/?p=670"><img src="images/tp04.png" /><span>门店设置</span></a></li>
                        <li><a  target="_blank" href="http://help.chainclouds.cn/?p=679"><img src="images/tp05.png" /><span>用户设置</span></a></li>
                    </ul>
                </div>
                <div class="tagContent" id="tagContent2">
                    <ul>
                        <li><a target="_blank" href="http://help.chainclouds.cn/?p=713"><img src="images/tp06.png" /><span>建立会员制度</span></a></li>
                        <li><a target="_blank" href="http://help.chainclouds.cn/?p=733"><img src="images/tp07.png" /><span>导入实体会员</span></a></li>
                    </ul>
                </div>
                <div class="tagContent" id="tagContent3">
                    <ul>
                        <li onmouseout="Downloadapp(false)" onmouseover="Downloadapp(true)" ><a  href=""><img class="QRcode"  src="images/erwm.png" /><img  src="images/tp08.png" /><span>下载掌柜APP</span></a>
                            
                        </li>
                        <li><a  target="_blank" href="http://help.chainclouds.cn/?p=830"><img src="images/tp09.png" /><span>APP使用指南</span></a></li>
                    </ul>
                </div>
            </div>
        </div>        
    </div>
</div>
<div class="help">
	<div class="Novice">
   	  <div class="novice_title"><h1>常用宝典</h1><span>温故知新  渐成老手</span><a href="/Module/helpCenter/helpCenterClass.aspx">更多</a></div>
		<div class="commonly_nr">
        	<ul class="commonly_nr2"><h2>会员活动</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=717">如何设置积分返现？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=803">如何创建优惠券？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=822">如何设置生日活动？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=820">如何招募线上会员？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=824">如何创建营销活动？</a></li>
            </ul>
            <span></span>
        	<ul class="commonly_nr2"><h2>云店管理</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=807">团购</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=812">秒杀</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=840">退换货管理</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=843">退款管理</a></li>
            </ul>
            <span></span>
        	<ul class="commonly_nr2"><h2>会员管理</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=874">卡基础设置</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=727">会员标签设置</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=730">会员查询</a></li>
            </ul>
            <span></span>
        	<ul class="commonly_nr2"><h2>门店实战</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=854">店长集客怎么玩</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=850">员工如何开小店</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=797">玩一场出色的异业合作</a></li>
            </ul>
        </div>
    </div>
</div>
<div class="help">
	<div class="Novice">
   	  <div class="novice_title"><h1>常见FAQ</h1><span>老手过招  华山论剑</span><a href="">更多</a></div>
		<div class="commonly_nr">
        	<ul class="commonly_nr1"><h2>安全问题</h2>
            	<li><a  target="_blank" href="http://help.chainclouds.cn/?p=695">更改密码</a></li>
            	<li><a  target="_blank" href="http://help.chainclouds.cn/?p=700">支付宝配置流程</a></li>
            	<li><a  target="_blank" href="http://help.chainclouds.cn/?p=697">微信支付配置流程</a></li>
            </ul>
            <span></span>
        	<ul class="commonly_nr1"><h2>常见操作</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=690">停用离职员工</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=693">停用已关闭门店</a></li>
            	<li><a target="_blank" href="http://open.chainclouds.com/receive">重新授权微信</a></li>
            </ul>
            <span></span>
        	<ul class="commonly_nr1"><h2>引粉秘籍</h2>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=702">店内吸粉大法？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=706">9大日常粉丝维护妙招？</a></li>
            	<li><a target="_blank" href="http://help.chainclouds.cn/?p=709">如何提升会员复购率？</a></li>
            </ul>
        </div>
    </div>
</div>
    </div>
</div>




<SCRIPT type=text/javascript>
function selectTag(showContent,selfObj){
	// 操作标签
	var tag = document.getElementById("novice_nr").getElementsByTagName("li");
	var taglength = tag.length;
	for(i=0; i<taglength; i++){
		tag[i].className = "";
	}
	selfObj.parentNode.className = "selectTag";
	// 操作内容
	for(i=0; j=document.getElementById("tagContent"+i); i++){
		j.style.display = "none";
	}
	document.getElementById(showContent).style.display = "block";
}


function Downloadapp(showapp)
{
    if (showapp) {
        $(".QRcode").show();

    } else {
        $(".QRcode").hide();
    }
}
</SCRIPT>
</asp:Content>