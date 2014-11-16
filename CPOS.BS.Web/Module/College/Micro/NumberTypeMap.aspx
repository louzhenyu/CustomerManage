<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master"
    AutoEventWireup="true" CodeBehind="NumberTypeMap.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.College.Micro.NumberTypeMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="/Module/static/css/artDialog.css" rel="stylesheet" type="text/css" />
    <title>期刊关联版块</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="/Module/College/Micro/js/NumberTypeMap">
        <div class="topIssuesInfo">
            <div class="operateWrap">
                <p class="getBack">
                    返回</p>
                <a href="javascript:;" class="commonBtn editNum">编辑</a>
            </div>
            <p class="picWrap">
                <img id="imSrc_f" src="../commres/images/tableImg.png" alt="" /></p>
            <div class="infoWrap">
                <h2 class="num">
                    刊号：<label id="lbNum"></label></h2>
                <p class="caption">
                    标题：<label id="lbTitle"></label></p>
                <p class="creationDate">
                    创建日期：<label id="lbTime"></label></p>
            </div>
        </div>
        <div class="subMenu-content">
            <div class="commonTitle" style="margin: 0 0 20px 20px;">
                关联版块</div>
            <!--表格操作按钮-->
            <div class="tableWrap">
                <div class="tablehandle">
                    <a href="javascript:;" class="commonBtn appInfoBtn">创建版块</a>
                </div>
                <!-- 已确认名单表格 -->
                <table id="table" class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                <span>关联</span>
                            </th>
                            <th>
                            </th>
                            <th>
                                日期<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                类型名称
                            </th>
                            <th>
                                级别<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                            <th>
                                父类型名称<img src="../commres/images/selectIcon02.png" alt="" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot id="footer" style="display: none;">
                        <tr>
                            <td colspan="9" class="tfooter">
                                <span>
                                    <img src="../CommRes/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tfoot>
                </table>
                <div id="kkpager" style="text-align: center;">
                </div>
            </div>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn savePage">保存</a> <a href="javascript:;" class="commonBtn cancelBtn">
                    取消</a>
            </div>
        </div>
    </div>
    <div id="dvType" style="display: none;">
        <div class="commdialog dig-newstype">
            <div class="row">
                <em class="tit">父类型</em>
                <div id="pre-typeList" class="selectBox w120">
                </div>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span> 板块名称</em>
                <p class="input">
                    <input id="ipname" type="text" value="" /></p>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span> 板块描述</em>
                <p class="input">
                    <input id="ipDesc" type="text" value="" /></p>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span> 缩略图</em>
                <div class="thumbBox">
                    <div id="dvimgUrl" class="uploadBtn">
                    </div>
                </div>
            </div>
            <div class="btns">
                <a class="commonBtn addBtn" href="javascript:;">保存</a> <a class="commonBtn cancelBtn"
                    href="javascript:;">取消</a>
            </div>
        </div>
    </div>
    <div id="dvNumber" style="display: none;">
        <div class="commdialog dig-number">
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>刊号</em>
                <p class="input">
                    <input id="ipNum" type="text" /></p>
            </div>
            <div class="row">
                <em class="tit"><span class="fontRed">*</span>主题</em>
                <p class="input">
                    <input id="ipTitle" type="text" class="w520" /></p>
            </div>
            <div class="row thumbrow">
                <em class="tit"><span class="fontRed">*</span>封面</em>
                <div class="thumbBox">
                    <div class="uploadBtn">
                    </div>
                </div>
            </div>
            <div class="btns">
                <a class="commonBtn addBtn" href="javascript:;">保存</a> <a class="commonBtn cancelBtn"
                    href="javascript:;">取消</a>
            </div>
        </div>
    </div>
    <script id="tableTemp" type="text/html">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <tr id="<#=idata.MicroTypeID #>">
                <td class="checkBox ckOper" data-id="<#=idata.MicroTypeID #>">
                 <em></em>
                </td>
                <td class="operateWrap">
                     <span data-id="<#=idata.MicroTypeID #>" class="editIcon editIcon"></span>
                     <span data-id="<#=idata.MicroTypeID #>" class="delBtn delIcon"></span>
                </td>
                <td>
                 <#=idata.CreateTime.replace(/T\d{2}:\d{2}:\d{2}.\d{0,3}$/, "") #>
                </td>
                <td>
                 <#=idata.MicroTypeName #>
                </td>
                <td>
                 <#=idata.TypeLevel #>
                </td>
                <td>
                 <#=idata.ParentName #>
                </td>               
            </tr>
        <#} #>
    </script>
    <script id="selType" type="text/html">
        <span class="selected text" data-val="">无</span>
        <div class="selectList">
        <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
            <p class="option" data-val="<#=idata.MicroTypeID #>" data-level="<#=idata.TypeLevel #>"><#=idata.MicroTypeName #></p>
        <#}#>
        </div>
    </script>
</asp:Content>
