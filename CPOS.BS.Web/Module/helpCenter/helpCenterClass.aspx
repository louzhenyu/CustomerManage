<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>帮助中心</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/helpCenter/css/style.css?v1.2"%>" />
    <style type="text/css">
        body {  overflow: hidden;}
        .Tips {  float: right;background-color: red; border-radius: 4px;width: 45px;font-size: 15px;line-height: 21px;  color: #fff;text-align: center;}
        .classalert{  height: 30px;}
        .key {  float: right;font-size: 15px;line-height: 21px;  color: #f00;margin-left: 10px;font-weight: 700;}
        #contentArea {min-height:500px;height:auto;}
        .class{padding: 10px; height: 50px;  margin-top: 10px;}
        .classify{float: left;margin-right: 35px;  font-size: 18px;width: 80px;}
        .options {float: left;}
        .options span{min-width: 90px;display: inline-block;font-size: 18px;color: #22d3d3;margin:0 10px;}
        .options span a{font-size: 16px;color: #22d3d3;}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  class="classalert"><span class="key">按Ctrl+F搜索问题关键字</span><span class="Tips">Tips</span></div>
       <div class="class">
           <div class="classify">商品</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=761">商品列表</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=763">商品发布</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=765">商品品类</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=769">商品规格</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=767">商品分组</a></span></div>
           
       </div>
    <div class="class">
        <div class="classify">订单</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=836">订单管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=840">退换货管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=843">退款管理</a></span></div>
        </div>
    <div class="class">
        <div class="classify">会员</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=730">会员管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=888">开卡</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=874">卡类型管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=727">标签管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=717">积分返现</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=719">假日设置</a></span></div>
        </div>
       <div class="class">
        <div class="classify">云店</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=772"">云店装修</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=774">支付方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=776">配送方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=807">团购</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=812">抢购/秒杀</a></span></div>
        </div>
        <div class="class">
        <div class="classify">营销</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=814">优惠券查询</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=818">互动游戏</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=820">触点活动</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=822">生日营销</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=824">营销活动</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=826">消息模板</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=828">社会化销售</a></span></div>
        </div>  
       <div class="class">
        <div class="classify">微信</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=785">微信公众号授权</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=787">微信菜单管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=789">微信关注回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=794">微信关键字回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=792">微信图文素材</a></span></div>
        </div>
    <div class="class">
        <div class="classify">设置</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=753">组织结构</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=755">门店</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=757">角色</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=759">员工</a></span></div>
        </div>
    <div class="class">
        <div class="classify">异业合作</div><div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=740">奖励模板</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=743">分销商管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=745">分销商奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=747">分销商提现</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=749">销售员奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=751">销售员提现</a></span></div>
        </div>
</asp:Content>