<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>支付方式管理</title>
      <link href="<%=StaticUrl+"/module/PayMent/css/style.css?v1.0"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<div class="payMentListArea" id="section" data-js="js/payMentList.js?v1.0">
    <div class="tableWrap cursorDef">
        <!-- 已确认名单表格 -->
     <!--   <table class="dataTable" style="display:inline-table;">
            <thead>
                <tr class="title">
                    <th width="40%">支付方式</th>
                    <th width="40%">支付状态</th>
                    <th width="20%">编辑</th>
                </tr>
            </thead>
            <tbody id="payMentList">
                <tr>
                    <td colspan="5"><img src="../static/images/loading.gif" width="32" height="32" alt="loading" /></td>
                </tr>
            </tbody>
        </table>-->
        <div id="payMentList">
        	<div class="loading" style="padding-top:40px;">
              <span><img src="../static/images/loading.gif"></span>
            </div>
        </div>
        <p class="tip-payment">至少配置一种支付方式，否则无法完成支付环节。</p>
    </div>
    
</div>

<div id="kkpager" style="padding-right:35px;text-align:right;"></div>

<!-- 遮罩层 -->
<div class="jui-mask"></div>
<!-- 弹出，商户支付宝支付 -->
<div id="jui-dialog-AlipayWap" class="jui-dialog jui-dialog-payMent">
	<div class="jui-dialog-tit">
    	<h2>商户支付宝支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap" style="margin:3px 0">
        	<a href="http://help.chainclouds.cn/?p=700" target="_blank" class="handleGuide" title="操作指引"></a>
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
                <p class="radioBox" data-value='busine'>启用</p>
                <p class="radioBox" data-radio="disable" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap" style="margin:3px 0 10px">
            <em class="tit">支付方式:</em>
            <div class="checkWrap">
                <p id="pagePay" class="checkBox on" data-value='pagePay'>网页支付</p>
                <p id="scanCodePay" class="checkBox" data-value="scanCodePay">扫码支付<strong>暂只支持超级分销</strong></p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 合作者身份(PID):</em>
            <p class="searchInput"><input id="AlipayWap_id" type="text" value="" /></p>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 服务商应用(APPID):</em>
            <p class="searchInput"><input id="AlipayWap_appid" type="text" value="" /></p>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 支付宝账号:</em>
            <p class="searchInput"><input id="AlipayWap_tbid" type="text" value="" /></p>
        </div>
		<div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 支付宝公钥:</em>
            <p class="searchInput"><input id="AlipayWap_publicKey" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 商户私钥:</em>
            <p class="searchInput"><input id="AlipayWap_privateKey" type="text" value="" /></p>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>


<!-- 弹出，支付宝线下支付 -->
<div id="jui-dialog-AlipayOffline" class="jui-dialog jui-dialog-payMent">
	<div class="jui-dialog-tit">
    	<h2>支付宝线下支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<!--<p class="radioBox" data-radio="disable" data-value='alading'>启用阿拉丁账号</p>-->
                <p class="radioBox" data-value='busine'>启用商家账号</p>
                <p class="radioBox" data-radio="disable" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 账号:</em>
            <p class="searchInput"><input id="AlipayOffline_id" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 秘钥:</em>
            <p class="searchInput"><input id="AlipayOffline_md5" type="text" value="" /></p>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>



<!-- 弹出，商户微信支付 -->
<div id="jui-dialog-WXJS" class="jui-dialog jui-dialog-payMent">
	<div class="jui-dialog-tit">
    	<h2>商户微信支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
        	<a href="http://help.chainclouds.cn/?p=697" target="_blank" class="handleGuide" title="操作指引"></a>
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
                <p class="radioBox" data-value='busine'>启用</p>
                <p class="radioBox" data-radio="disable" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 公众号APPID:</em>
            <p class="searchInput"><input id="WXJS_appid" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 微信支付商户号:</em>
            <p class="searchInput"><input id="WXJS_parnterid" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap" style="margin-bottom:10px;">
            <em class="tit"><span class="fontRed">*</span> API秘钥:</em>
            <p class="searchInput"><input id="WXJS_parnterkey" type="text" value="" disabled="disabled"/></p>
        </div>
        <p class="fontRed tipWx">提示：将字符串复制黏贴到微信支付商户平台，API安全的设置密钥中</p>
        
        <!--
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 公众平台秘钥:</em>
            <p class="searchInput"><input id="WXJS_appsecret" type="text" value="" /></p>
        </div>
		<div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 财付商户通身份标识别:</em>
            <p class="searchInput"><input id="WXJS_parnterid" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 财付通商户权限私钥:</em>
            <p class="searchInput"><input id="WXJS_parnterkey" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"> 支付加密密码:</em>
            <p class="searchInput"><input id="WXJS_paysignkey" type="text" value="" /></p>
        </div>
        -->
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>



<!-- 弹出，银联网页支付 -->
<div id="jui-dialog-CupWap" class="jui-dialog jui-dialog-payMent">
	<div class="jui-dialog-tit">
    	<h2>银联网页支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<!--<p class="radioBox" data-radio="disable" data-value='alading'>启用阿拉丁账号</p>-->
                <p class="radioBox" data-value='busine'>启用商家账号</p>
                <p class="radioBox" data-radio="disable" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 账号ID:</em>
            <p class="searchInput"><input id="CupWap_merchantid" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 加密证书:</em>
            <p class="searchInput"><input id="CupWap_certificatecilepath" type="text" value="" /></p><span class="uploadFileBox">上传文件</span>
        </div>
        
		<div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 加密密码:</em>
            <p class="searchInput"><input id="CupWap_certificatefilepassword" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 解密证书:</em>
            <p class="searchInput"><input id="CupWap_decryptcertificatefilepath" type="text" value="" /></p><span class="uploadFileBox">上传文件</span>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 解密密码:</em>
            <p class="searchInput"><input id="CupWap_packetencryptkey" type="text" value="" /></p>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>


<!-- 弹出，银联语音支付 -->
<div id="jui-dialog-CupVoice" class="jui-dialog jui-dialog-payMent">
	<div class="jui-dialog-tit">
    	<h2>银联语音支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<!--<p class="radioBox" data-radio="disable" data-value='alading'>启用阿拉丁账号</p>-->
                <p class="radioBox" data-value='busine'>启用商家账号</p>
                <p class="radioBox" data-radio="disable" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 账号ID:</em>
            <p class="searchInput"><input id="CupVoice_merchantid" type="text" value="" /></p>
        </div>
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 加密证书:</em>
            <p class="searchInput"><input id="CupVoice_certificatecilepath" type="text" value="" /></p><span class="uploadFileBox">上传文件</span>
        </div>
		<div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 加密密码:</em>
            <p class="searchInput"><input id="CupVoice_certificatefilepassword" type="text" value="" /></p>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 解密证书:</em>
            <p class="searchInput"><input id="CupVoice_decryptcertificatefilepath" type="text" value="" /></p><span class="uploadFileBox">上传文件</span>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit"><span class="fontRed">*</span> 解密密码:</em>
            <p class="searchInput"><input id="CupVoice_packetencryptkey" type="text" value="" /></p>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
    </div>
</div>


<!-- 弹出，消费者自助支付 -->
<div id="jui-dialog-CustomerSelfPay" class="jui-dialog jui-dialog-payMent" style="width:460px;min-height:200px;margin-left:-230px;">
	<div class="jui-dialog-tit">
    	<h2>消费者自助支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<p class="radioBox on" data-value='alading'>启用</p>
                <p class="radioBox" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
     </div>
</div>   

<!-- 弹出，货到付款 -->
<div id="jui-dialog-GetToPay" class="jui-dialog jui-dialog-payMent" style="width:460px;min-height:200px;margin-left:-230px;">
	<div class="jui-dialog-tit">
    	<h2>货到付款</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<p class="radioBox" data-value='alading'>启用</p>
                <p class="radioBox" data-value="unalading">停用</p>
            </div>
        </div>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
     </div>
</div>



<!-- 弹出，连锁掌柜支付宝支付 -->
<div id="jui-dialog-CCAlipayWap" class="jui-dialog jui-dialog-payMent" style="width:460px;min-height:200px;margin-left:-230px;">
	<div class="jui-dialog-tit">
    	<h2>连锁掌柜支付宝支付</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="payMentContent">
    	<div class="commonSelectWrap">
            <em class="tit">是否启用:</em>
            <div class="radioWrap">
            	<p class="radioBox on" data-value='alading'>启用</p>
                <p class="radioBox" data-value="unalading">停用</p>
            </div>
        </div>
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn">取消</a>
        </div>
     </div>
</div>



<script id="tpl_payMentList" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        	<tr>
				<td><#=item.IsNativePay==0?'平台':''#><#=item.PaymentTypeName#></td>
				<#if(item.IsOpen=='true'){#>
					<td class="unstart">已启用</td>
				<#}else{#>
					<td class="unstart blue">未启用</td>
				<#}#>
				<td class="operateWrap" title="编辑" data-usertype="{'IsOpen':<#=item.IsOpen#>,'IsDefault':<#=item.IsDefault#>,'IsCustom':<#=item.IsCustom#>,'IsNativePay':<#=item.IsNativePay#>}" data-typecode="<#=item.PaymentTypeCode#>" data-typeid="<#=item.PaymentTypeID#>" data-channelid="<#=item.ChannelId#>" >
					<span class="editIcon"></span>
				</td>
			</tr>
    <#}#>
</script>
</asp:Content>
