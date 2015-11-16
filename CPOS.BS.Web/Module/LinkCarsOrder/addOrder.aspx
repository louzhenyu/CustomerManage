<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" xmlns="http://www.w3.org/1999/html">
    <meta charset="UTF-8" />
    <title>袋数先生—快速下单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/static/css/easyui.css"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/linkCarsOrder/css/add.css?v=0.1"%>" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addOrder.js?ver=0.3">
        <form>
        </form>
        <form id="select">
        <div class="select">
            <div class="commonSelectWrap">
                <em class="tit">车牌号:</em>
                <div class="searchInput carno">
                    <input id="CarNoProvince" name="CarNoProvince" value="沪" class="easyui-validatebox phone"
                        data-options="required:true," style="width: 33px;" />
                    <input id="carNo" name="CarNoNum" class="easyui-combogrid phone" data-options="required:true,validType:'carNO'"
                        style="width: 100px; float: right;" />
                </div>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">车主姓名：</em>
                <label class="searchInput">
                    <input data-text="车主姓名" id="VipRealName" class="easyui-validatebox phone" data-flag="VipRealName"
                        name="VipRealName" type="text" value="" />
                </label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">手机号：</em>
                <label class="searchInput bordernone">
                    <select data-text="手机号" id="Phone" data-flag="Phone" name="Phone" class="easyui-combogrid Phone">
                    </select>
                </label>
            </div>
        </div>
        </form>
        <div class="tabs-panels" style="border-top-width: 1px; margin-top: 15px; padding-top: 25px;">
            <div class="commonSelectWrap">
                <em class="tit">服务项目：</em>
                <div class="searchInput bordernone">
                    <div class="checkBox on" id="ServiceItem">
                        <em></em><b>洗车</b>
                    </div>
                </div>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">服务价格：</em>
                <label class="searchInput ">
                    <input type="text" id="Amount" class="easyui-numberbox" value="29" data-options="min:0,max:99999,width:137,required:true,precision:2,prefix:'￥'" />
                </label>
            </div>
            <div class="main">
                <div class="daylist">
                    <ul>
                        <li data-panl="nav01">今天</li>
                        <li data-panl="nav02">明天</li>
                        <li data-panl="nav03">后天</li>
                    </ul>
                </div>
                <div class="panlList">
                    <div class="panel" id="nav01">
                    </div>
                    <div class="panel" id="nav02">
                    </div>
                    <div class="panel" id="nav03">
                    </div>
                </div>
            </div>
            <div class="commonSelectWrap marBot35">
                <em class="tit">优惠券:</em>
                <div class="selectBox bodernone">
                    <input id="coupon" class="easyui-combogrid" data-options="width:137,height:26,validType:'selectIndex'"
                        name="CouponID" />
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="commonSelectWrap marBot35" style="height: 100px">
                <em class="tit">备注：</em>
                <label class="searchInput" style="width: 400px; height: 100px">
                    <input data-text="备注" id="WaiterRemark" type="text" />
                </label>
            </div>
            <div class="saveBtn">
                <a href="javascript:;" class="commonBtn">提交订单</a>
            </div>
        </div>
    </div>
    <div style="display: none">
        <div id="win" class="easyui-dialog" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true"
            title="选择车归属省份" style="overflow: hidden; width: 400px; padding: 10px;">
            <div id="listSpan">
                <span data-falg="京">京(北京)</span> <span data-falg="沪">沪(上海)</span> <span data-falg="粤">
                    粤(广东)</span> <span data-falg="浙">浙(浙江)</span> <span data-falg="津">津(天津)</span>
                <span data-falg="渝">渝(重庆)</span> <span data-falg="川">川(四川)</span> <span data-falg="吉">
                    吉(吉林)</span> <span data-falg="辽">辽(辽宁)</span> <span data-falg="鲁">鲁(山东)</span>
                <span data-falg="湘">湘(湖南)</span> <span data-falg="冀">冀(河北)</span> <span data-falg="新">
                    新(新疆)</span> <span data-falg="甘">甘(甘肃)</span> <span data-falg="青">青(青海)</span>
                <span data-falg="陕">陕(陕西)</span> <span data-falg="宁">宁(宁夏)</span> <span data-falg="豫">
                    豫(河南)</span> <span data-falg="晋">晋(山西)</span> <span data-falg="皖">皖(安微)</span>
                <span data-falg="苏">苏(江苏)</span> <span data-falg="鄂">鄂(湖北)</span> <span data-falg="贵">
                    贵(贵州)</span> <span data-falg="黔">黔(贵州)</span> <span data-falg="云">云(云南)</span>
                <span data-falg="桂">桂(广西)</span> <span data-falg="藏">藏(西藏)</span> <span data-falg="赣">
                    赣(江西)</span> <span data-falg="闽">闽(福建)</span> <span data-falg="琼">琼(海南)</span>
                <span data-falg="黑">黑(黑龙江)</span> <span data-falg="蒙">蒙(内蒙古)</span> <span data-falg="使">
                    使(大使馆)</span>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true"
        data-main="js/main.js"></script>
</asp:Content>
