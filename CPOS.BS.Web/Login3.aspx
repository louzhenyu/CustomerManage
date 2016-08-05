<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login3.aspx.cs" Inherits="JIT.CPOS.BS.Web.Login3" %>

<!doctype html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>O2OMarketing 会员云 | 杰亦特科技有限公司出品</title>
  
    <!--[if lte IE 6]>  
    <style type="text/css">  
#ie6-warning{background:#FF0; position:absolute;top:0; left:0;font-size:12px; line-height:24px; color:#F00;
padding:0 10px;}  
#ie6-warning img{float:right; cursor:pointer; margin-top:4px;}
#ie6-warning a{text-decoration:none;}  
</style>
<div id="ie6-warning">
<img src="" width="14" height="14" onclick="closeme();" alt="关闭提示" />
您正在使用 Internet Explorer 6 低版本的IE浏览器。为更好的浏览本页，建议您将浏览器升级到
<a href="http://www.microsoft.com/china/windows/internet-explorer/ie8howto.aspx" target="_blank">IE8</a>
 或以下浏览器：<a href="http://www.firefox.com.cn/download/">Firefox</a> / <a href="http://www.google.cn/chrome">
Chrome</a> / <a href="http://www.apple.com.cn/safari/">Safari</a> / <a href="http://www.Opera.com/">Opera</a>  
</div>  
<script type="text/javascript">  
function closeme()
{
   var div = document.getElementById("ie6-warning");
   div.style.display ="none";
}
function position_fixed(el, eltop, elleft){  
// check if this is IE6  
if(!window.XMLHttpRequest)  
window.onscroll = function(){  
el.style.top = (document.documentElement.scrollTop + eltop)+"px";  
el.style.left = (document.documentElement.scrollLeft + elleft)+"px";  
}  
else el.style.position = "fixed";  
}  
position_fixed(document.getElementById("ie6-warning"),0, 0);  
</script>  
<![endif]-->

    <style type="text/css">
body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, dl, dt, dd, ul, ol, li, pre, form, fieldset, legend, button, input, textarea, th, td, img { border: none; margin: 0; padding: 0;  }
body, button, input, select, textarea { font-size: 12px; font-family: THeiti,"微软雅黑",Arial, Helvetica, sans-serif; word-break: break-all; word-wrap: break-word; color: #565656; resize: none; outline: none; }
body { background-color:#fff;}
h1, h2, h3, h4, h5, h6 { font-size: 14px; font-weight: normal; }
em { font-style: normal; }
ul, ol, img { list-style: none; border: 0; }
table, th, td, tr { border-collapse: collapse; border-spacing: 0; border: 0; font-size: 12px; margin: 0; padding: 0; }
textarea, input[type="text"],input[type="password"],input[type="button"],input[type="submit"]{ resize: none; outline: none; -webkit-appearance: none;   }
.zoom { overflow: hidden; zoom: 1;  }
.frdisplay{ float:right; display:block;}
.fldisplay{ float:left; display:block; _display:inline;}
a,a:hover{ text-decoration:none; color:#7c7c7c;}
.common{ width:1000px; margin-left:auto; margin-right:auto;}
.header{ height:80px; overflow:hidden; clear:both;}
.header h1{ width:557px; height:43px; padding-top:20px;}
.skin{ background-image:url(images/skin.png); background-repeat:no-repeat;}
.logo{ width:557px; height:43px; background-position:-15px -12px;}
.bq{background:url(images/bannerbg.jpg) repeat-x left top; height:409px;}
.bannerBg{background:url(images/bg.jpg) no-repeat center top; height:409px;}
.bannerBox{ height:409px; position:relative;}
.Wd{ width:700px; height:474px; background:url(images/otoicn.png) no-repeat left top; position:absolute; left:-70px; top:30px;}
.LoginBox{ width:371px; height:317px; background:url(images/loginbg.png) no-repeat left top; position:absolute; right:0px; bottom:0;}
.blank1{ height:1px; overflow:hidden; display:block; clear:both;}
.Interduct{ clear:both; overflow:hidden;}
.Interduct dt{ clear:both; width:196px; height:63px; overflow:hidden; padding-bottom:50px;}
.InterductTile{ background-position:-15px -251px; width:196px; height:63px;}
.Interduct dd{ width:200px; float:left; display:block; display:_inline; text-align:center; margin-left:64px;}
.InterductDD1{ width:115px; margin-left:auto; margin-right:auto; height:142px; clear:both; overflow:hidden; background-position:-11px -76px;}
.InterductDD2{ width:115px; margin-left:auto; margin-right:auto; height:142px; clear:both; overflow:hidden; background-position:-144px -76px;}
.InterductDD3{ width:115px; margin-left:auto; margin-right:auto; height:142px; clear:both; overflow:hidden; background-position:-274px -76px;}
.InterductDD4{ width:115px; margin-left:auto; margin-right:auto; height:142px; clear:both; overflow:hidden; background-position:-409px -76px;}
.InterductDdTxt{ text-align:left; line-height:20px; font-size:14px; padding-top:5px;}
.footer{ height:400px; clear:both; background:url(images/footerbg.png) repeat left top; padding-top:30px;}
.footerDiv1{ width:270px; color:#cccccc; font-size:14px; margin-right:100px;}
.footerDivTitle{ width:85px; height:26px; background-position:-26px -356px; text-indent:-9999px;}
.footerDivTitle2{ width:85px; height:26px; background-position:-26px -398px; text-indent:-9999px;}
.footerDivTitle3{ width:120px; height:26px; background-position:-26px -436px; text-indent:-9999px;}
.inputDiv{ width:256px; height:35px; background-position:-543px -251px; display:block; position:relative; }
.inputTextA{ width:256px; height:75px; background-position:-543px -314px;position:relative; }
.inputBtn{ clear:both; padding-top:10px; overflow:hidden;}
a.BtnSend,a.BtnSend:hover{ width:84px; height:34px; background-position:-716px -79px; display: block; text-decoration:none; float:right;}
.inputText{width:245px; height:30px;top:3px; left:5px; background:none; color:#fff; font-size:14px; position:absolute; z-index:2; background:none;}
.inputTextarea{width:245px; top:3px; left:5px; height:70px; overflow:hidden;  background:none;  color:#fff; font-size:14px;  z-index:2; position:absolute;}
.LoginConnext{width:266px; margin-left:52px; margin-top:50px;}
.CompanyName{ height:42px; background-position:-222px -266px; margin-bottom:12px; position:relative;}
.userName{ height:42px; background-position:-222px -319px;  margin-bottom:12px;  position:relative;}
.pwd{ height:42px; background-position:-222px -372px; margin-bottom:18px;   position:relative;}
.LoginText{ width:210px; margin-top:8px; height:22px;  font-size:14px; color:#565454; position:absolute; left:50px; top:2px; background:none; z-index:2}
.Loginbtn{ height:50px; width:267px; background-position:-222px -434px; display:block; cursor:pointer}
.saveLoginState{ width:121px; height:36px;  background-position:-222px -501px; display:block; margin-top:6px; cursor:pointer}
.saveLoginStateNo{width:121px; height:36px;  background-position:-222px -544px; display:block; margin-top:6px; cursor:pointer}
.tip{width:210px; margin-top:8px; height:22px;  font-size:14px; color:#cdcdcd; position:absolute; left:53px; top:2px; background:none; z-index:1}
</style>
<script type="text/javascript" src="/Framework/Javascript/Other/jquery1.6.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="common header">
  <h1 class="fldisplay"><img src="images/px1.png" class="skin logo"  /></h1>
</div>
<div class="bq">
  <div class="bannerBg">
    <div  class="common bannerBox">
      <div class="Wd"></div>
      <div class="LoginBox">
      	<div class="LoginConnext">
            <div style=" font-weight:100; text-align:left; color:red;">
            <b><asp:Label ID="lblInfor" runat="server" style="color:red"></asp:Label></b>
                     
             </div>
        	<%--<div class="skin CompanyName" onClick="InputGetFocus('companyName')">
            <span class="tip">输入公司名称</span>
            <input type="text" runat="server" class="LoginText" value="" runat="server" id="txtCustomerCode" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)"  />
            </div>--%>
            <div class="skin userName" onClick="InputGetFocus('txtUsername')">
                <span class="tip">输入用户名</span>
                <input type="text" runat="server" class="LoginText" value="" id="txtUsername" runat="server" style="background-color: #fff;"  onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)"  />
            </div>
            <div class="skin pwd" onClick="InputGetFocus('txtPassword')">
                <span class="tip">输入密码</span>
                <input type="password" runat="server" class="LoginText" id="txtPassword"  onKeyPress="InputKeyDown(this)" style="background-color: #fff;"   runat="server" onBlur="InputBlur(this)"  />
            </div>
          
           <asp:ImageButton id="btnLogin" runat="server" class="skin Loginbtn" style="clear:both" ImageUrl="images/px1.png" OnClick="btnLogin_Click" />
            
            
            <div class="skin saveLoginStateNo" rel="0" onClick="SaveLogin(this)" >
            <input type="checkbox" id="chkRemember" runat="server" style="filter:alpha(opacity=0);opacity:0;"  />
            </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div class="blank1" style="height:120px;"></div>
<div class="Interduct common">
  <dt><img src="images/px1.png" class="skin InterductTile"  /></dt>
  <dd style="margin-left:0;"><img src="images/px1.png" class="skin InterductDD1"  />
    <div class="InterductDdTxt">利用移动技术实现创新的购物体验和消费者互动。</div>
  </dd>
  <dd><img src="images/px1.png" class="skin InterductDD2"  />
    <div class="InterductDdTxt">发挥线下门店优势，借助微信把线下人流导引到品牌会员中心。</div>
  </dd>
  <dd><img src="images/px1.png" class="skin InterductDD3"  />
    <div class="InterductDdTxt">利用移动互联网技术实现品牌企业的线上、线下融合。</div>
  </dd>
  <dd><img src="images/px1.png" class="skin InterductDD4"  />
    <div class="InterductDdTxt">通过专业的会员数据分析实现精准会员营销。</div>
  </dd>
</div>
<div class="blank1" style="height:60px;"></div>
<div class="footer">
  <div class="common" style="overflow:hidden">
    <div class="footerDiv1 fldisplay">
      <h2 class="footerDivTitle skin">About us</h2>
      <p style="padding-top:10px; line-height:22px;">我们的团队既有资深的零售终端执行专家、数据采集和分析专家又有资深的商务智能信息系统研发专家，具备多个行业的丰富市场调研运作管理经验与专业审核分析能力。公司与上海大学计算机学院结成的联合研发中心是我们研发持续创新的保证。<br />
        <br />
        <span style="font-size:12px;">copyrights©</span><strong style="font-size:14px;">o2oMarketing</strong></p>
    </div>
    <div class="footerDiv1 fldisplay">
      <h2 class="footerDivTitle2 skin">find us</h2>
      <p style="padding-top:10px; line-height:22px;"><img src="images/addressMap.jpg" /><br />
        电话：400-110-2002   31151366<br>
        传真：31151367 <br>
        地址：上海市静安区延平路121号三和大厦    15层D座</p>
    </div>
    <div class="footerDiv1 fldisplay" style="margin-right:0; width:256px;">
      <h2 class="footerDivTitle3 skin">contact us</h2>
      <div style="padding-top:20px;">
        <div class="inputDiv skin"><span class="tip" style="left:5px;">姓名</span>
          <input type="text" id="PUserName" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
        <div class="blank1" style="height:10px;"></div>
        <div class="inputDiv skin"><span class="tip" style="left:5px;">公司名称</span>
          <input type="text" id="PCompany" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
        <div class="blank1" style="height:10px;"></div>
        <div class="inputDiv skin"><span class="tip" style="left:5px;">公司邮箱</span>
           <input type="text" id="PEmail" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
        <div class="blank1" style="height:10px;"></div>
        <div class="inputDiv skin"><span class="tip" style="left:5px;">公司总机+分级</span>
           <input type="text" id="PTel" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
        <div class="blank1" style="height:10px;"></div>
        <div class="inputDiv skin"><span class="tip" style="left:5px;">手机号</span>
           <input type="text" id="PPhone" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
        <div class="blank1" style="height:10px;"></div>
        <div class="inputDiv skin"><span class="tip" style="left:5px;">行业</span>
           <input type="text" id="PIndustry" runat="server" class="inputText" onKeyPress="InputKeyDown(this)" onBlur="InputBlur(this)" value="" />
        </div>
       
        <div class="inputBtn" style="text-align:right;">
            <div style="float:right;">
                <asp:ImageButton id="btnSend" runat="server" class="skin BtnSend" style="clear:both" ImageUrl="images/send.png" OnClick="btnSend_Click" />
                <input type="hidden" id="hMsg" value="" runat="server" />
            </div>
            
        </div>
      </div>
    </div>
  </div>
</div>
<input type="hidden" id="hdPwd" runat="server" />
    </form>
    <script type="text/javascript">
        function InputGetFocus(id) {
            $("#" + id).focus();
        }
        function InputKeyDown(o) {
            var getValue = $(o).val();
         
            if (getValue.length >= 0) {
                $(o).siblings(".tip").hide();

            }
        }
        function InputBlur(o) {
            var getValue = $(o).val();
            if (getValue.length == 0) {
                $(o).siblings(".tip").show();
            }
        }
        function SaveLogin(o) {
            if ($(o).hasClass("saveLoginStateNo")) {
                $(o).removeClass("saveLoginStateNo").addClass("saveLoginState").attr("rel", "1");
                $("#chkRemember").attr("checked", true);

            } else {
                $(o).removeClass("saveLoginState").addClass("saveLoginStateNo").attr("rel", "0");
                $("#chkRemember").attr("checked", false);
            }
        }
    $(function(){
     if($("#txtCustomerCode").val()!=""){
        $("#txtCustomerCode").siblings(".tip").hide();
     }
     if($("#txtUsername").val()!=""){
        $("#txtUsername").siblings(".tip").hide();
     }
     if($("#txtPassword").val()!=""){
        $("#txtPassword").siblings(".tip").hide();
     }
   })
        
        function InputKeyDownAll(o) {
            if (document.getElementById("PUserName").value.length > 0)
                InputKeyDown(document.getElementById("PUserName"));
            if (document.getElementById("PCompany").value.length > 0)
                InputKeyDown(document.getElementById("PCompany"));
            if (document.getElementById("PEmail").value.length > 0)
                InputKeyDown(document.getElementById("PEmail"));
            if (document.getElementById("PTel").value.length > 0)
                InputKeyDown(document.getElementById("PTel"));
            if (document.getElementById("PPhone").value.length > 0)
                InputKeyDown(document.getElementById("PPhone"));
            if (document.getElementById("PIndustry").value.length > 0)
                InputKeyDown(document.getElementById("PIndustry"));
        }
</script>
</body>
</html>

