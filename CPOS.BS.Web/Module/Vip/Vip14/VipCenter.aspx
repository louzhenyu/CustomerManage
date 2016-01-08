<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" CodeBehind="VipCenter.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.Vip.Vip14.VipCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>页面详情页</title>
	<link rel="stylesheet" href="css/global.css" />
    <link rel="stylesheet" href="css/style.css?v=0.5" />
    <style>

        .qb_WeixinService {display:none;  width: 794px;position: fixed; top: 50%;left: 45%;margin: -245px 0 0 -347px;background: rgba(0,0,0,0.5);z-index: 9999;}
        .qb_WeixinService .quick_top {width: 794px;}
        .qb_WeixinService .quick {width: 794px;}
        .qb_WeixinService .quick_center{width: 794px;}
        .qb_WeixinService .quick_centernr {width: 794px;  height: 500px;}
        .qb_WeixinService .found {  text-align: center;}
        .qb_WeixinService .found img{border-radius:5px;  width: 265px;margin: 6px;}
        .qb_WeixinService .found a{color:#00cccc}
        .qb_WeixinService .found font{color:#00cccc}
        .qb_WeixinService .found  p{  font-size: 18px;  color: #999;}
        .qb_quick { z-index: 9000;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div data-js="/Module/static/js/Vip14/page.js?ver=0.2" id="section">
    <div class="followDataArea">
        <div class="userFollow clearfix">
        <div class="commonShow orderArea">
           <div class="orderContent">
           		<span class="tit"></span>
                <div class="orderInfo">
                	<div><p>当日已发货订单数 </p><span id="sentCount" class="num">0</span></div>
                    <div><p>已支付待发货订单数</p><span id="paidNotSentCount" class="num">0</span></div>
                    <div><p>货到付款待发货订单数</p><span id="cashOnDeliveryCount" class="num">0</span></div>
                </div>
           </div>
        </div>
        <div class="commonShowLeft">
            <div class="goodArea">
            	<div class="goodContent">
                	<span class="tit"></span>
                    <div class="goodInfo">
                        <div><p>上架商品数 </p><span id="upShelfCount" class="num">0</span></div>
                        <div><p>下架商品数 </p><span id="offShelfCount" class="num">0</span></div>
                    </div>
                </div>
            </div>
            
            <div class="storeNumArea">
            	<div class="storeNumContent">
                	<span class="tit"></span>
                    <div class="storeNumInfo">
                        <div><p>上线门店数 </p><span id="onlineUnitCount" class="num">0</span></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="followTrend">
        <div class="commonMenu_vip">
            <em class="setIcon"></em>
            <span class="on one"></span>
            <div class="textTag">今日新增 <strong id="todayVipCount">0</strong>，环比 <strong id="todayVipPercent">--</strong>。本月新增 <strong id="totalVipCount">0</strong>，环比 <strong id="MonthVipPercent">--</strong>。</di>
        </div>
        <div class="chart" id="chartSection">
            
        </div>
    </div>
</div>
<!-- 快速上手 -->
<div class="ui-mask"></div>
<div class="qb_quick">
    <div class="quick">
        <div class="quick_top">快速上手<a href="javascript:;" class="closeBtn"><img src="images/close.png" /></a></div>
        
        <div class="stepBox" id="oneStep" style="display:block;">
        	<div class="quick_title"><img src="images/ks_1.jpg" /></div>
            <div class="quick_center">
                <div class="quick_centernr">
                    <div class="found">
                        <span>创建云店：</span>
                        一键对接品牌微信，建立微商城、微官网
                        <p>① 微信服务号授权<a id="WeixinService" href="javascript:;">（立即授权）</a></p>
                        <p>② 上传并发布商品<a target="_blank" href="http://help.chainclouds.cn/?p=652">（商品发布介绍）</a></p>
                        <p>③ 装修云店<a  target="_blank" href="http://help.chainclouds.cn/?p=772">（装修入门介绍）</a></p>
                    </div>
                </div>
            </div>
            <div class="handleBtnBox">
                <a href="javascript:;" class="nextStep" id="oneNextStep">下一步</a>
            </div>
        </div>
        
        
        <div class="stepBox" id="twoStep">
        	<div class="quick_title"><img src="images/ks_2.jpg" /></div>
            <div class="quick_center">
                <div class="quick_centernr">
                    <div class="found">
                        <span>打通门店</span>
                        拥有实体门店需要在这里打通门店才能完成整个业务闭环哦！
                        <p>① 门店设置<a target="_blank" href="http://help.chainclouds.cn/?p=670">（打通秘籍上半部）</a></p>
                        <p>② 用户设置<a target="_blank" href="http://help.chainclouds.cn/?p=679">（打通秘籍下半部）</a></p>
                    </div>
                </div>
            </div>    
        	<div class="handleBtnBox">
                <a href="javascript:;" class="prevStep" id="twoPrevStep">上一步</a>
                <a href="javascript:;" class="nextStep" id="twoNextStep">下一步</a>
            </div>
        </div>
        
        
        <div class="stepBox" id="threeStep">
        	<div class="quick_title"><img src="images/ks_3.jpg" /></div>
            <div class="quick_center">
                <div class="quick_centernr">
                    <div class="found">
                        <span>管理会员</span>
                        在这里您可以创建出打通线上线下的整套会员体系哦！
                        <p>① 建立会员制度<a target="_blank"  href="http://help.chainclouds.cn/?p=713">（会员管理操作指引）</a></p>
                        <p>② 导入实体门店会员<a target="_blank"  href="http://help.chainclouds.cn/?p=733">（门店会员导入表格）</a></p>
                    </div>
                </div>
            </div>
        	<div class="handleBtnBox">
                <a href="javascript:;" class="prevStep" id="threePrevStep">上一步</a>
                <a href="javascript:;" class="finishStep">完成</a>
            </div>
        </div>
        
        
        
        
        <div class="unrealistic">
        	<div class="nextNoShow">
                <em></em>
                <span>下次不再显示</span>
            </div>
        </div>
    </div>
</div>


    <!-- 微信服务号授权 -->
<div class="qb_WeixinService">
    <div class="quick">
        <div class="quick_top">绑定微信帐号<a href="javascript:;" class="WeixinServicecloseBtn"><img src="images/close.png" /></a></div>
        
            <div class="quick_center">
                <div class="quick_centernr ">
                    <div class="found" >
                        <br />
                       <p>如果您已有<font>微信公众服务号并已通过实名认证</font>，请与连锁掌柜打通吧。</p><br /><br /><br />
                        <iframe id="receive" scrolling="no" src="http://open.chainclouds.com/receive" ></iframe>
                        <img src="images/weixinset.png" /><br />
                        如果你还没有微信公众服务号，可以<a target="_blank"  href="https://mp.weixin.qq.com/cgi-bin/readtemplate?t=register/step1_tmpl&lang=zh_CN">点击注册</a><br /><br /><br /><br />
                        绑定微信服务号，将可使用连锁掌柜所有功能，<a target="_blank"  href="\Module\helpCenter\helpCenterClass.aspx">了解所有功能</a><br />
                        请注意是微信公众服务号，不是订阅号哦，<a target="_blank"  href="http://kf.qq.com/faq/140806zARbmm140826M36RJF.html">有何区别？</a>
                    </div>
                </div>
            </div>
        
        
    </div>
</div>

<script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
