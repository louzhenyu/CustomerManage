<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true"
    Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>房价管理</title>
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
.house_detail { font-family:"微软雅黑";}
.house_wrap { margin-top:5px; border:1px solid #BEBEBE; width:auto; padding:20px; background:#F7F9F8;}
.house_wrap p i { display:inline-block; *display:inline; *zoom:1; width:70px; height:20px; margin-right:10px; vertical-align:-5px;}
.house_wrap p span { padding-right:80px;}
.color01 { background:#FFD4CD;}
.color02 { background:#FDE5C3;}

.house_month { width:70px; padding-top:70px;}
.house_month td { height:81px; font-size:14px;}

.house_table { margin-left:-1px; width:530px;}
.house_table h3 { height:35px; line-height:35px; text-align:center; background:#E5E5E5; border-left:1px solid #ccc; border-right:1px solid #ccc; border-top:1px solid #ccc;}
.house_month td,.house_table th,.house_table td { border:1px solid #ccc; text-align:center; background:#fff; padding:1px;}
.house_table th { height:35px;}
.house_table td { height:45px;}
.house_table td i { display:block; height:22px; line-height:22px;}
.house_table td.cur { border:2px solid #1B8CF2; padding:0;}

.house_choose { width:210px; padding:15px; background:#fff; border:2px solid #88C6FF;}
.house_choose h4 { font-size:14px; margin:5px 0;}
.house_choose label { display:block; margin-bottom:5px;}
.house_choose label input { height:22px; padding:0 10px; border:1px solid #ccc;}

.house_choose div { margin-top:20px;}
.house_choose div input { background:#1B8CF2; width:85px; height:28px; color:#fff; border:0; cursor:pointer;}


.contentArea {
    margin-left:0px;
    float: left;
}
</style>
    <script src="Store/StoreItemDailyPriceVMStore.js?v=0.3" type="text/javascript"></script>
    <script src="View/StoreItemDailyPriceView.js?v=0.3" type="text/javascript"></script>
    <script src="Controller/StoreItemDailyPriceCtl.js?v=0.3" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height: 44px;">
                    <div id='span_panel' style="float: left; width: 900px; overflow: hidden;">
                    </div>
                    <div id='btn_panel' style="float: left; width: 200px;">
                    </div>
                </div>
            </div>
            <div class="art-titbutton">
                <div class="view_Button">
                    <span id='span_BatchUpdate'></span>
                </div>
            </div>
            <div class="cb">
            </div>
            <div class="house_detail">
                <div class="house_wrap">
                    <div class="house_month fl">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblMonth">
                        </table>
                    </div>
                    <div class="house_table fl">
                        <h3 id="h_housetype">
                            </h3>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th scope="col">
                                    星期日
                                </th>
                                <th scope="col">
                                    星期一
                                </th>
                                <th scope="col">
                                    星期二
                                </th>
                                <th scope="col">
                                    星期三
                                </th>
                                <th scope="col">
                                    星期四
                                </th>
                                <th scope="col">
                                    星期五
                                </th>
                                <th scope="col">
                                    星期六
                                </th>
                            </tr>
                            <tbody id="tblWeek">
                            </tbody>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                <%--    房价设置--%>
                    <div class="house_choose" style="display: none;position :absolute">
                        <p id="pDate">
                            2013/12/25</p>
                        <h4>
                            设置房价：</h4>
                        <label>
                            现价
                            <input type="text" value="1" id="txtNowPrice" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                onafterpaste="this.value=this.value.replace(/\D/g,'')" />
                        </label>
                        <label>
                            原价
                            <input type="text" value="1" id="txtSourcePrice" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                onafterpaste="this.value=this.value.replace(/\D/g,'')" />   <%--readonly="readonly"--%>
                        </label>
                        <div>
                            <input type="hidden" id="hdnTDID" />
                            <input type="button" value="确定" onclick="fnUpdate()" />
                            <input type="button" value="取消" onclick="fnCloseDiv()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
