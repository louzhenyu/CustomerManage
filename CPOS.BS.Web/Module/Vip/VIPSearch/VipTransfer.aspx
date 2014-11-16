<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .sale_table { width:100%; font-family:"微软雅黑"; font-size:14px; background:#fff; border:1px solid #ccc;
    -webkit-box-shadow:0 0 5px rgba(0,0,0,.4);
    -moz-box-shadow:0 0 5px rgba(0,0,0,.4);
    -o-box-shadow:0 0 5px rgba(0,0,0,.4);
    box-shadow:0 0 5px rgba(0,0,0,.4);
    }
    .sale_table_outer { padding:30px;}
    .sale_table_inner { border:1px solid #ccc; background:#EAEAEA;}
    .sale_table dl { float:left; width:12%; text-align:center;}
    .sale_table dt { height:35px; line-height:35px; border-bottom:1px solid #C8D1D8; color:#333; background:#FDFDFD;}
    .sale_table dd { height:60px; line-height:60px; color:#666; border-right:1px solid #fff; border-bottom:1px solid #fff;}
    .sale_table dl.fore { width:40%; background:url(/images/sale_table_bg.png) no-repeat top center;}
    .sale_table dl.fore dd { color:#003a88;}

    /*清除浮动*/
    .clear{display:block;clear:both;overflow:hidden;height:0;line-height:0;font-size:0}
    .clearfix:after{content:" ";visibility:hidden;display:block;clear:both;height:0;font-size:0}
    .clearfix{*zoom:1}
    </style>
    <script src="Controller/VipTransferCtl.js" type="text/javascript"></script>
    <script src="Model/VipTransferVM.js" type="text/javascript"></script>
    <script src="Store/VipTransferVMStore.js" type="text/javascript"></script>
    <script src="View/VipTransferView.js" type="text/javascript"></script>
    <title>销售管理_导航</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="sale_table">
  <div class="sale_table_outer">
    <div class="sale_table_inner clearfix">
      <dl class="fore">
        <dt>顾客漏斗</dt>
        <dd>总数量</dd>
        <dd>激活数量</dd>
        <dd>购买者</dd>
        <dd>重复购买</dd>
        <dd>品牌大使</dd>
      </dl>
      <dl>
        <dt>门店招募</dt>
        <dd><span id="storeTotal"></span></dd>
        <dd><span id="storeActive"></span></dd>
        <dd><span id="storeConsumer"></span></dd>
        <dd><span id="storeMConsumer"></span></dd>
        <dd><span id="storeLoyalConsumer"></span></dd>
      </dl>
      <dl>
        <dt>微信关注</dt>
        <dd><span id="weixinTotal"></span></dd>
        <dd><span id="weixinActive"></span></dd>
        <dd><span id="weixinConsumer"></span></dd>
        <dd><span id="weixinMConsumer"></span></dd>
        <dd><span id="weixinLoyalConsumer"></span></dd>
      </dl>
      <dl>
        <dt>第三方数据</dt>
        <dd><span id="thirdPartyTotal">3,521</span></dd>
        <dd><span id="thirdPartyActive">3,212</span></dd>
        <dd><span id="thirdPartyConsumer">2,219</span></dd>
        <dd><span id="thirdPartyMConsumer">1,343</span></dd>
        <dd><span id="thirdPartyLoyalConsumer">452</span></dd>
      </dl>
      <dl>
        <dt>吸新活动</dt>
        <dd><span id="recruitmentTotal">2,449</span></dd>
        <dd><span id="recruitmentActive">2,449</span></dd>
        <dd><span id="recruitmentConsumer">1,983</span></dd>
        <dd><span id="recruitmentMConsumer">1,023</span></dd>
        <dd><span id="recruitmentLoyalConsumer">324</span></dd>
      </dl>
      <dl>
        <dt>老数据激活</dt>
        <dd><span id="awakeTotal">2,893</span></dd>
        <dd><span id="awakeActive">2,893</span></dd>
        <dd><span id="awakeConsumer">1,284</span></dd>
        <dd><span id="awakeMConsumer">632</span></dd>
        <dd><span id="awakeLoyalConsumer">219</span></dd>
      </dl>
    </div>
  </div>
</div>
</asp:Content>
