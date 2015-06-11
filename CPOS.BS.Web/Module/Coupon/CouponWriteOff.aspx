<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>优惠券核销</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
    <link href="css/jit-card.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/couponWriteOff">
        <span class="packup"></span>
        <div class="commonNav_coupon">
            <a href="javascript:;" class="on">优惠券核销</a> 
            <%-- <a href="javascript:void(0)" onclick='AddMid("couponWriteO
            ffLog.aspx")'>使用记录</a>--%>
        </div>
        <div class="contentArea_coupon">
            <div class="commonTitWrap">
                <span>优惠券核销</span>
            </div>

            <div class="mana-card">
                <div class="mana-card-t">
                    <ul>
                        <li>
                            <input type="text" id="name" placeholder="手机号/身份证/会员名" style="width: 190px" />
                        </li>
                        <li>
                        <input type="text" id="couponCode" placeholder="优惠券编号">
                        </li>
                        <li><a href="javascript:;" id="queryVip" class="btn-daochu">查询</a></li>
                        <!--<li><a href="javascript:;" class="btn-daochu">添加新会员</a></li>-->
                    </ul>
                </div>
                <div class="mana-card-m">
                    <table id="queryResult" width="95%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <th align="center" scope="col">
                                操作
                            </th>
                            <th align="center" scope="col">
                                会员账号
                            </th>
                            <th align="center" scope="col">
                                会员名
                            </th>
                            <th align="center" scope="col">
                                微信昵称
                            </th>
                            <th align="center" scope="col">
                                手机号
                            </th>
                            <th align="center" scope="col">
                                身份证
                            </th>
                        </tr>
                        <tbody id="content">
                            <tr>
                                <td style="height: 200px; text-align: center; vertical-align: middle;" colspan="8"
                                    align="center">
                                    输入条件搜索内容
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    
                    <div id="kkpager">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="popDiv" style="display: none; position: fixed; z-index: 9999; left: 35%;
        top: 35%;">
        <div class="popup" id="popbox" style="width: 730px;">
            <div class="popup-t">
                核销优惠券</div>
            <a href="javascript:;" class="btn-close hintClose">×</a>
            <div class="popup-m">
                <div class="tel-form">
                    <div class="mana-card-m">
                        <table id="couponList" width="95%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th align="center" scope="col"  style="width:60px;">
                                    操作
                                </th>
                                <th align="center" scope="col">
                                    优惠券编号
                                </th>
                                <th align="center" scope="col">
                                    优惠券名称
                                </th>
                                <th align="center" scope="col">
                                    有效期
                                </th>
                                <th align="center" scope="col">
                                    备注
                                </th>
                            </tr>
                            <tbody id="couponListContent">
                                <tr>
                                    <td style="height: 200px; text-align: center; vertical-align: middle;" colspan="8"
                                        align="center">
                                        无优惠券信息
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div id="kkpager2">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div id="tips" class="hide" style="position: fixed; z-index: 9999; left: 35%; top: 35%;">
            <div class="popup">
                <div class="popup-t">
                    请确认</div>
                <div class="popup-m">                
                    <div class="popup-mes" id="test">
                        是否确定需要核销？
                    </div>
                </div>
                <div class="popup-b">
                <a href="javascript:;"
                        id="complet">确定</a>
                    <a href="javascript:$('#tips').fadeOut();" id="exportOut">取消</a> 
                </div>
            </div>
        </div>
        <div id="tips1" class="hide" style="position: fixed; z-index: 9999; left: 35%; top: 35%;">
            <div class="popup">
                <div class="popup-t">
                    提示</div>
                <div class="popup-m">                
                    <div class="popup-mes" id="Div2">
                        组单号未填写，请填写了组单号再进行核销！
                    </div>
                </div>
                <div class="popup-b">
                <a href="javascript:$('#tips1').fadeOut();" id="A2">确定</a> 
                </div>
            </div>
        </div>
    <!-- 提示弹层 -->
    <div id="ui-mask" class="ui-mask">
    </div>
    <script id="tpl_content" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-item="<#=JSON.stringify(item)#>">
            <td><a href="javascript:;" class="btn-pay" style="width: 60px;">查看优惠券</a></td>
            <td><#=item.VipCode#></td>
            <td><#=item.VipRealName#></td>
            <td><#=item.VipName#></td>
            <td><#=item.Phone#></td>
            <td><#=item.IdCard#></td>
        </tr>
    <#}#>
    </script>
    <script id="tpl_couponList" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <tr data-item="<#=JSON.stringify(item)#>">
            <td><a href="javascript:;" class="btnWriteOff" style="width: 260px;">核销</a></td>
            <td><#=item.CouponCode#></td>
            <td><#=item.CouponName#></td>
            <td>
            <#if(item.BeginTime=="" && item.EndTime==""){#>
            永久有效
            <#}else{#>
            <#=item.BeginTime#>至<#=item.EndTime#>
            <#}#>
            </td>
            <td><input type="text"  date-format="yyyy-mm-dd" id="Comment" placeholder="" text="<#=item.Comment#>"/></td>
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
    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true"
        data-main="/Module/Coupon/js/main.js"></script>
</asp:Content>
