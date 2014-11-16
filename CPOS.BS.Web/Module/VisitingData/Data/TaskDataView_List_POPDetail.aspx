<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDataView_List_POPDetail.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.VisitingData.Data.TaskDataView_List_POPDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register src="/Framework/WebControl/HeadRel.ascx" tagname="HeadRel" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<uc1:HeadRel ID="HeadRel1" runat="server" />
<style type="text/css">
@charset "utf-8";
/* CSS Document */
html,body,div,span,iframe,h1,h2,h3,h4,h5,h6,p,blockquote,pre,a,address,big,cite,code,del,em,font,img,ins,small,strong,var,b,u,i,center,dl,dt,dd,ol,ul,li,fieldset,form,label,legend{margin:0; padding:0;}

body{ color:#333; font-size:12px; font-family:宋体; background-color:#fff;}

select,input,img{vertical-align:middle;}
ol,ul,li{ list-style:none;}
a img{ border:0;}
.graph{clear:both; overflow:hidden;}
.vert{ vertical-align:top;}
.email{ font-family:Arial, Helvetica, sans-serif;}

a:link{ color:#333; text-decoration:none;}
a:visited{ color:#333; text-decoration:none;}
a:hover{ color:#ff0000;text-decoration:none;}
a:active{ color:#ff0000;text-decoration:none;}

.newshow{ width:270px; border:0px #aeaeae solid;  background-color:#fff;}
.xiangxi{ clear:both; width:270px;height:365px; }
.xx_bt{ width:460px; margin:0 auto; line-height:26px; font-size:16px; font-weight:bold; text-align:center; margin-top:15px; color:#2f6495;}

.xx_txt{ background-color:#f5f5f5; width:260px; margin:0 auto; margin-top:5px; overflow:hidden;}
.xx_txt ul{ border:1px #d6d6d6 solid; margin:5px; padding-top:7px; padding-bottom:7px;}
.xx_txt li{ list-style-type:none; line-height:24px; color:#000; padding-left:17px; font-size:13px; height:24px ;overflow:hidden;}

.xx_img{ clear:both; width:260px; margin:0 auto; margin-top:5px;}
.xx_img p{ line-height:24px;}
</style>
</head>
<body>
<div class="newshow">
<div class="cj_1" style="display:none;">
<p class="a1"></p>
<p class="a2"><a href="#">关闭</a></p>
</div>
<div class="xiangxi">
<div class="xx_bt" style="display:none;"><label id="lblTitle"></label></div>
<div class="xx_txt">
<ul id="ul_store_plan" style="display:none;">
<li>名称：<label id="lblstore_StoreName"></label></li>
<li>地址：<label id="lblstore_Addr"></label></li>
<li>渠道：<label id="lblstore_ChannelName"></label></li>
<li>连锁：<label id="lblstore_ChainName"></label></li>
<li>联系人：<label id="lblstore_Manager"></label></li>
<li>联系电话：<label id="lblstore_Tel"></label></li>
</ul>

<ul id="ul_distributor_plan" style="display:none;">
<li>名称：<label id="lbldistributor_Distributor"></label></li>
<li>地址：<label id="lbldistributor_Addr"></label></li>
<li>联系人：<label id="lbldistributor_Manager"></label></li>
<li>联系电话：<label id="lbldistributor_Tel"></label></li>
</ul>

<ul id="ul_fact" style="display:none;">
<li>走店日期：<label id="lblfact_ExecutionTime"></label></li>
<li>拜访时间：<label id="lblfact_Time"></label></li>
<li>路途时间：<label id="lblfact_WorkingHoursJourneyTime"></label></li>
<li>店内时间：<label id="lblfact_WorkingHoursIndoor"></label></li>
<li>总时间：<label id="lblfact_WorkingHoursTotal"></label></li>
</ul>

</div>
<div class="xx_img" style="display:none">

</div>
</div>
</div>
<script language="javascript" type="text/javascript">

    $(function () {
        var POPID = JITMethod.getUrlParam("POPID");
        var POPType = JITMethod.getUrlParam("POPType");
        var VisitingTaskDataID = JITMethod.getUrlParam("VisitingTaskDataID");
        var viewtype = JITMethod.getUrlParam("viewtype");
        var data = JITMethod.getUrlParam("data");

        $.get("TaskDataView_List_POPDetail.aspx?mid=" + __mid + "&btncode=search&poptype=" + POPType + "&popid=" + POPID + "&r=" + Math.random(),
        function (result) {
            if (result) {
                var obj = eval(result);
                if (parseInt(POPType) == 1) {
                    $("#ul_store_plan").show();
                    $("#ul_distributor_plan").hide();

                    $("#lblstore_StoreName").html(getVal(obj[0].StoreName));
                    $("#lblstore_Addr").html(getVal(obj[0].Addr));
                    $("#lblstore_ChannelName").html(getVal(obj[0].ChannelName));
                    $("#lblstore_ChainName").html(getVal(obj[0].ChainName));
                    $("#lblstore_Manager").html(getVal(obj[0].Manager));
                    $("#lblstore_Tel").html(getVal(obj[0].Tel));
                }
                else if (parseInt(POPType) == 2) {
                    $("#ul_store_plan").hide();
                    $("#ul_distributor_plan").show();

                    $("#lbldistributor_Distributor").html(getVal(obj[0].Distributor));
                    $("#lbldistributor_Addr").html(getVal(obj[0].Addr));
                    $("#lbldistributor_Manager").html(getVal(obj[0].Manager));
                    $("#lbldistributor_Tel").html(getVal(obj[0].Tel));
                }

                if (viewtype == "fact") {
                    $("#ul_fact").show();

                    var jdata = Ext.JSON.decode(unescape(data));
                    $("#lblfact_ExecutionTime").html(getVal(jdata.ExecutionTime));
                    $("#lblfact_Time").html(getVal(jdata.InTime) + "--" + getVal(jdata.OutTime));
                    $("#lblfact_WorkingHoursJourneyTime").html(getVal(jdata.WorkingHoursJourneyTime));
                    $("#lblfact_WorkingHoursIndoor").html(getVal(jdata.WorkingHoursIndoor));
                    $("#lblfact_WorkingHoursTotal").html(getVal(jdata.WorkingHoursTotal) + "&nbsp;&nbsp;&nbsp;&nbsp;<a style='color:blue;' href=\"javascript:;\" onclick=\"parent.parent.fnMapView('"
    + jdata.POPID + "')\">查看数据</a>");
                }
            }
        });

    });

    function getVal(value) {
        if (value == null) {
            return "无";
        }
        else {
            return value;
        }
    }

    function fnColumnTime(value, p, r) {
        var res = "";
        var min = parseInt(value);
        if (!isNaN(min)) {
            if (min >= 60) {
                res = Math.floor(min / 60) + "小时" + (min % 60) + "分钟";
            }
            else if (min < 0) {
                res = "0分钟";
            }
            else {
                res = min + "分钟";
            }
        }
        else {
            res = value;
        }
        return res;
    }
</script>
</body>
</html>
