<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPicking.aspx.cs" ValidateRequest="true"
    Inherits="JIT.CPOS.BS.Web.Module.Order.Print.PrintPicking" %>

<%@ Register Src="/Framework/WebControl/HeadRel.ascx" TagName="HeadRel" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>拣货单打印</title>
    <uc1:HeadRel ID="HeadRel1" runat="server" />
    <script src="Controller/PrintPickingCtl.js?v=0.3" type="text/javascript"></script>
    <script src="Store/PrintPickingStore.js" type="text/javascript"></script>
    <script src="Model/PrintPickingVM.js?v=0.2" type="text/javascript"></script>
    <script src="View/PrintPickingView.js?v=0.3" type="text/javascript"></script>
    <style type="text/css">
        @media print
        {
            .ipt
            {
                display: none;
            }
        }
        table
        {
            border-collapse: collapse;
        }
        .ebooking_list_t
        {
            width: 946px;
            margin: 0 auto;
            text-align: left;
        }
        .ebooking_list_t h2
        {
            width: 500px;
            padding-left: 16px;
            font-weight: 700;
            font-size: 14px;
        }
        .ebooking_list_t .pages_spilt
        {
            margin-top: -12px;
            text-align: right;
        }
        .form_area_detail
        {
            width: 946px;
            margin: 12px auto 30px auto;
            padding: 8px 0;
            border: 1px solid #06c;
            background: #e9f1fe;
        }
        .form_area_detail table
        {
            width: 880px;
            margin: 0 auto;
        }
        .form_area_detail th, .form_area_detail td
        {
            padding: 3px;
            text-align: left;
        }
        .form_area_detail th
        {
            height: 20px;
            width: 100px;
            font-weight: 700;
            text-align: right;
        }
        .x-grid-cell-inner
        {
            white-space: normal;
        }
    </style>
    <style media="print">
        .Noprint
        {
            display: none;
        }
        .PageNext
        {
            page-break-after: always;
        }
    </style>
</head>
<body>
    <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" id="wb" name="wb" width="0"
        height="0">
    </object>
    <div class="section" style="min-height: 0px; height: auto; border: 0;">
        <div id="btnDiv" style="text-align: left; margin-top: 20px; margin-left: 10px" class="Noprint">
            <input type="button" value="打印" onclick='window.print();' style="width: 80px; height: 30px;
                background-color: rgba(134, 192, 47, 0)" />
            <input type="button" value="打印预览" style="width: 80px; height: 30px" onclick='window.print();' />
        </div>
        <div class="m10 article">
            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
</body>
</html>
