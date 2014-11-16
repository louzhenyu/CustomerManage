<%@ Page Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true"
    Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>房态管理</title>
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
.house_detail { font-family:"微软雅黑";}
.house_wrap { margin-top:5px; border:1px solid #BEBEBE; width:auto; padding:20px; background:#F7F9F8;}
.house_wrap p i { display:inline-block; *display:inline; *zoom:1; width:70px; height:20px; margin-right:10px; vertical-align:-5px;}
.house_wrap p span { padding-right:80px;}
.color01 { background:#FFD4CD;}
.color02 { background:#FDE5C3;}

.house_month { width:70px; padding-top:95px;}
.house_month td { height:81px; font-size:14px;}

.house_table { margin-top:25px; margin-left:-1px; width:530px;}
.house_table h3 { height:35px; line-height:35px; text-align:center; background:#E5E5E5; border-left:1px solid #ccc; border-right:1px solid #ccc; border-top:1px solid #ccc;}
.house_month td,.house_table th,.house_table td { border:1px solid #ccc; text-align:center; background:#fff; padding:1px;}
.house_table th { height:35px;}
.house_table td { height:61px;}
.house_table td i { display:block; height:22px; line-height:22px;}
.house_table td.cur { border:2px solid #1B8CF2; padding:0;}

.house_choose { width:210px; padding:15px; background:#fff; border:2px solid #88C6FF;}
.house_choose h4 { font-size:14px; margin:5px 0;}
.house_choose label { margin-right:20px;}
.house_choose div { margin-top:20px;}
.house_choose div input { background:#1B8CF2; width:85px; height:28px; color:#fff; border:0; cursor:pointer;}


.contentArea {
    margin-left:0px;
    float: left;
}
</style>
    <script src="Store/StoreItemDailyPriceVMStore.js" type="text/javascript"></script>
    <script src="View/StoreItemDailyStatusView.js" type="text/javascript"></script>
    <script src="Controller/StoreItemDailyStatusCtl.js" type="text/javascript"></script>
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
                    <p>
                        <span><i class="color01"></i>满房</span><span><i class="color02"></i> 非满房</span></p>
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
                    <div class="house_choose" style="display: none; position: absolute">
                        <p id="pDate">
                            2013/12/25</p>
                        <h4>
                            设置满房：</h4>
                        <label>
                            <input type="radio" name="rdoHouseType" value="0" onclick="fnGetStock(0,2);" />
                            满房
                        </label>
                        <label>
                            <input type="radio" name="rdoHouseType" value="100" onclick="fnGetStock(100,0);" />
                            非满房
                        </label>
                        <span style="display: none" id="stock">
                            <br />
                            <span>可用库存<input type="text" name="txt_StockAmount" value="" style="width: 30px;" />
                                已用库存<input type="text" name="txt_UserAmount" value="" style="width: 30px;" />
                            </span></span>
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
