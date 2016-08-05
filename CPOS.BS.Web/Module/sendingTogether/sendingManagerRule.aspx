<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>提现管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/sendingManagerRule.css?ver=0.2"/>

</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<body cache>
<div class="allPage" id="section" data-js="js/sendingManagerRule.js">
    <!-- 内容区域 -->
    <div class="module">
        <div class="panelDiv">
            <p class="title">提现规则</p>
            <p>适用于会员、店员、分销商</p>
         </div>
     </div>

     <div class="module">
         <form >
            <div class="contents">
                 <div class="panelDiv">
                     <p class="title">可提现金额设置</p>
                  </div>
                  <div class="editMessage">
                      <span class="checkBox"></span>
                      <p>订单退货期（客户确认收货后<input type="text" id="WithDrawDays" value="2" disabled="disabled"/>天）满后可提现
</p>
                      <p>取消选择，为订单支付成功后即可提现。</p>
                      <p>虚拟商品的订单暂不支持退货。</p>
                      <p>虚拟商品所产生的支付成功后可立即提现</p>
                   </div>
            </div>

            <div class="contents">
                 <div class="panelDiv">
                     <p class="title">提现条件</p>
                  </div>
                  <div class="editMessage">
                       <span class="radio"></span>
                       <p>账户金额累积满<input type="text" value="2" id="minCondition" min="1" disabled="disabled"/>元 可提现</p>
                    </div>
                    <div class="editMessage">
                         <span class="radio on" data-type="0"></span>
                         <p>账户金额无限制</p>
                     </div>
            </div>


            <div class="contents">
                 <div class="panelDiv">
                     <p class="title">提现额度</p>
                  </div>
                  <div class="editMessage">
                    <span class="radio"></span>
                    <p>每次最多可提现<input type="text" value="200" id="MaxAmount" min="1" disabled="disabled"/>元</p>
                  </div>
                  <div class="editMessage">
                      <span class="radio on" data-type="0"></span>
                      <p>提现额度无限制</p>
                  </div>
            </div>

            <div class="contents">
                <div class="panelDiv">
                     <p class="title">提现次数</p>
                  </div>
                  <div class="editMessage">
                    <span class="radio"></span>
                    <p>每人每月可提现<input type="text" value="2" id="DrawNum" min="1" disabled="disabled"/>次</p>
                  </div>
                  <div class="editMessage">
                      <span class="radio on" data-type="0"></span>
                      <p>提现次数无限制</p>
                  </div>
            </div>
        </form>
    </div>

    <div class="btnWrap">
        <a href="javascript:;" class="commonBtn saveBtn">确定</a>
    </div>


</div>
    </body>
</asp:Content>