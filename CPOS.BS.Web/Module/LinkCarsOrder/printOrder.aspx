<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>打印结算单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/static/css/easyui.css"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/linkCarsOrder/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/linkCarsOrder/css/detail.css?v=0.4"%>" rel="stylesheet" type="text/css" />
     <style media="print">
            .Noprint,.commonHeader
            {
                display: none;
            }
            .PageNext
            {
                page-break-after: always;
            }
            .print{
                  display: block;
            }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div id="btnDiv" style="text-align: left; margin-top: 20px; margin-left: 10px" class="Noprint">
              <input type="button" value="打印" onclick='window.print();' style="width: 80px; height: 30px;
                  background-color: rgba(134, 192, 47, 0)" />

          </div>
  <div class="wordDIv print">
    <h1>兰博士深度养护连锁(凯马店)结算清单</h1>
    <form id="word">
  <!--  rowspan合并行，colspan 合并列-->
  <div class="panl">
      <div class="title">

      </div>

       <table>
        <tr >

                   <td colspan="3" style="border: none">订单号：<input class="bodernone" name="ordernone" value="00121165412"/></td>


                   <td class="txtR" style="border: none"> 结算日期:2015-08-09</td>

                   </tr>
            <tr>

            <td>车主姓名</td>
            <td colspan="1" ><input class="bodernone" name="userName" value="鼎鼎大名"/></td>
            <td>联系电话</td>
            <td class="txtL">18516098067</td>

            </tr>
            <tr>

            <td>车牌号码</td>
            <td colspan="1"><input class="bodernone" name="carNO" value="苏B82536"/></td>
            <td>服务类别</td>
            <td><input class="bodernone" name="carNO" value="洗车"/></td>


            </tr>
             <tr>
            <td>服务价格</td>
            <td colspan="1"><input class="bodernone" name="carNO" value="苏B82536"/></td>
            <td>优惠方式</td>
            <td><input class="bodernone" name="carNO" value="5元洗车券"/></td>
            </tr>
             <tr>

            <td>实收金额</td>
            <td><input class="bodernone" name="carNO" value="0"/></td>
            <td>支付方式</td>
            <td><input class="bodernone" name="carNO" value="在线支付"/></td>
             </tr>

             <tr><td colspan="3" style="height:100px;border:0px"></td>
             <td class="txtR" style="border:0px;line-height: 166px; padding-right: 25px;">客户签名:__________________</td></tr>
       </table>
 </div>
    </form>

  </div>

<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="js/main.js"></script>
</asp:Content>
