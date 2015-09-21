<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>门店信息</title>
   <%-- <script src="../../../Framework/Javascript/Other/jquery-1.4.2.min.js"></script>--%>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitType.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/CitySelectTree.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/Status.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/BrandDetail.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/UnitSort.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/MapSelect.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WApplicationInterface.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WApplicationInterface2.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Framework/javascript/Biz/WQRCodeType.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/examples/jquery.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/kindeditor.js"%>"></script>
    <script charset="utf-8" type="text/javascript" src="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/lang/zh_CN.js"%>"></script>
    
    <script src="<%=StaticUrl+"/Module/static/js/lib/tools-lib.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/static/js/lib/bdTemplate.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/static/js/plugin/jquery.jqpagination.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/static/js/plugin/jquery.drag.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()"%>" />
    <link href="/Module/Basic/Unit/css/keywords.css?v=Math.random()" rel="stylesheet" type="text/css" />
    <link href="/Module/static/css/pagination.css" rel="stylesheet" />
    <style type="text/css">
		#allmap{height:500px;width:100%;}
		#r-result{width:100%; font-size:14px;background-color:#ccc;}
        #mapSection {
            display: none;
            width:100%;
            height:100%;
            z-index:999;
            position:absolute;
        }
        #mapSection .mapbtn {
            width:100px;
            border-radius:3px;
            border: solid 1px #aaa;
        }
        #allmap {
            width:100%;
        }
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=ALm5o36XjP25qgG12QCrt17C"></script>
    <style type="text/css">
         .tr-Image
        {
            height: 150px;
            margin-top: 2em;
        }
        .imagetit
        {
            float: left;
            width: 110px;
            line-height: 16px;
            margin-top: 1em;
        }
        .uploadWarp
        {
            float: left;
            margin-top: 30px;
        }
        .viewImage
        {
            float: left;
            width: 256px;
            height: 256px;
            line-height: 256px;
            /*margin-right: 30px;*/
            text-align: center;
            font-size: 16px;
            background: #d0d0d0;
            color: #fff;
            margin-left: 10px;
        }
        .info
        {
            float: left;
            width: 200px;
        }
        .exp
        {
            line-height: 28px;
            font-size: 15px;
            color: #828282;
        }
        .uplaodbtn
        {
            display: block;
            width: 96px;
            height: 30px;
            line-height: 30px;
            margin-top: 15px;
            text-align: center;
            border-radius: 7px;
            background: #b2c7ab center center;
            color: #fff;
            cursor: pointer;
            float: left;
            margin-top: 15px;
        }
       .upbtn span {
        display: block;
        float: left;
        width: 100px;
        height: 30px;
        line-height: 30px;
        text-align:center;
        border-radius: 3px;
        cursor: pointer;
        }
        .upbtn {
            margin-top:15px;
        }
        .imgTextMessage .addBtn {
            float: left;
            width: 100px;
            height: 30px;
            line-height: 30px;
            margin: 25px 20px;
            text-align: center;
            border-radius: 3px;
            background: #b2c7ab;
            color: #fff;
            cursor: pointer;
       }
        .imgTextMessage h2 {
            margin-bottom: 10px;
            font-size: 14px;
            font-weight: bold;
            color: #a2a2a2;
         }
        .show {
            display: block;
        }
        .section .z_main_tb_td2 {
            width:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="mapSection">
            <div id="r-result">
		    经度: <input id="longitude" type="text" style="width:100px; margin-right:10px;" />
		    纬度: <input id="latitude" type="text" style="width:100px; margin-right:10px;" />
            地点：<input id="map_area" type="text" style="width:100px; margin-right:10px;"/>
		    <input type="button" value="查询" class="mapbtn"  onclick="theLocation();" />
            <input type="button" value="确定" class="mapbtn"  onclick="confirmMap();" />
            <input type="button" value="关闭" class="mapbtn"  onclick="closeMap();" />
            </div>
            <div id="allmap"></div>
	</div>
    <div class="section">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
                <div id="tabsMain" style="width: 100%; height: 451px;">
                </div>
                <div id="tabInfo"  style="height: 427px; background: rgb(241, 242, 245); overflow: auto;">
                    <div class="z_detail_tb" style="width: 700px;">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px; width: 140px;">
                                    上级单位
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 32px;">
                                    <div id="txtParentUnit" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>门店编码
                                </td>
                                <td class="z_main_tb_td2" style="">
                                    <div id="txtUnitCode" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 32px;">
                                    <font color="red">*</font>门店名称
                                </td>
                                <td class="z_main_tb_td2" style="position:relative;">
                                    <div id="txtUnitName" style="margin-top: 5px;">
                                    </div>
                                    <span style="position:absolute; left:125px; top:4px; width:140px; height:40px; line-height:20px; color:red;">如：新天地店</span>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    英文名称
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUnitEnglish">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    门店简称
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUnitShortName">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>类型
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUnitType">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>城市/区县
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCity">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    邮编
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtPostcode">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                  电话
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtTelephone">
                                    </div>
                                </td>
                                <td  colspan="4" style="color:red;">
                                    如：15866666666、021-888888、4008888888
                                </td>
                            </tr><tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    传真
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtFax">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    邮箱
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtEmail">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; padding-left: 20px;">
                                    <font color="red">*</font> 经纬度
                                </td>
                                <td class="z_main_tb_td2" id="tdLng" colspan="3" style="padding-top: 0px;">
                                    <div id="txtLongitude">
                                    </div>
                                </td>
                               <td class="z_main_tb_td2" style="padding-left:0;margin-left:0;">
                                    <div id="mapTrigger"></div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <%-- 配送价格--%>
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtUnitFlag">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>地址
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="3">
                                    <div id="txtAddress">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <%-- 公众号--%>
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtWeiXinId">
                                    </div>
                                </td>
                            </tr>
                            <%-- <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                     服务地址
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="5">
                                      <div id="txtWebserversURL">
                                    </div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    固定二维码
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWXCode">
                                    </div>
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div style="float: left;">
                                        <div id="btnWXImage">
                                        </div>
                                    </div>
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;" colspan="3">
                                    <div id="txtDimensionalCodeURL" style="float: left;">
                                    </div>
                                </td>
                            </tr>
                            <%--<tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align:top; line-height:22px;">二维码</td>
                                <td class="z_main_tb_td2" style="padding-top:0px;" colspan="5">
                                    <div id="txtDimensionalCodeURL" style="float:left;"></div>
                                    <div style=" float:left;">
                                        <div id="btnWXImage"></div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px; width: 80px;">
                                    图片预览
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px; padding-left: 14px" colspan="3">
                                    <img id="imgView" alt="" src="" width="256px" heigth="256px" />
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    二维码号码
                                </td>
                                <td class="z_main_tb_td2" style="vertical-align: top; line-height: 22px;">
                                    <div id="txtWXCode2">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    LOGO
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="padding-top: 0px;">
                                    <div id="txtImageUrl" style="float: left;">
                                    </div>
                                    <div style="float: left;">
                                        <input type="button" id="uploadImageUrl" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    现场路径
                                </td>
                                <td class="z_main_tb_td2" colspan="3" style="padding-top: 0px;">
                                    <div id="txtFtpImagerURL">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    <font color="red">*</font>联系人
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 22px;
                                    padding-top: 0px;">
                                    <div id="txtContact">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    备注
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 32px;
                                    padding-top: 0px;">
                                    <div id="txtRemark">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="visibility: hidden;">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    二维码
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;" colspan="5">
                                    <div style="float: left;">
                                        <input type="button" id="uploadDimensionalCodeURL" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    创建人
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCreateUserName">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    创建时间
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtCreateTime">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none">
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    最后修改人
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtModifyUserName">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                    最后修改时间
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                    <div id="txtModifyTime">
                                    </div>
                                </td>
                                <td class="z_main_tb_td" style="vertical-align: top; line-height: 22px;">
                                </td>
                                <td class="z_main_tb_td2" style="padding-top: 0px;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="display:none">
                    <div id="tabProp" style="display:none">
                        <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                            <div style="height: 5px;">
                            </div>
                            <div id="pnlUnitProp" style="height: 415px; overflow: auto;">
                                <%=JIT.CPOS.BS.Web.Framework.WebControl.PropHelper.PropHelperSingleton.CreationPropGroup("UNIT") %>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabImage" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlImage" style="height: 415px; overflow: auto;">
                            <div style="width: 400px; float: left; padding-top: 5px;">
                                <div id="gridImage">
                                </div>
                            </div>
                            <div style="width: 350px; padding-left: 50px; padding-right: 10px; float: left;">
                                <div style="height: 5px;">
                                </div>
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td>
                                            <font color="red">*</font>图片地址
                                        </td>
                                        <td colspan="3">
                                            <div id="txtImage_ImageUrl" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div style="width: 80px; line-height: 32px; margin-left: 15px;">
                                                <input type="button" id="uploadImage2" value="选择图片" style="margin-left: 20px;" />
                                            </div>
                                        </td>
                                    </tr>                                    
                                    <tr style=" text-indent:54px;">
                                        <td  colspan="3">
                                            <font color="red">*建议上传尺寸为85*62</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <font color="red">*</font>排序
                                        </td>
                                        <td colspan="3">
                                            <div id="txtImage_DisplayIndex" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div>
                                                <div id="btnAddImageUrl">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabBrand" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlBrand" style="height: 415px; overflow: auto;">
                            <div style="width: 400px; float: left; padding-top: 5px;">
                                <div id="gridBrand">
                                </div>
                            </div>
                            <div style="width: 350px; padding-left: 50px; padding-right: 10px; float: left;">
                                <div style="height: 5px;">
                                </div>
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td>
                                            <font color="red">*</font>品牌
                                        </td>
                                        <td colspan="3">
                                            <div id="txtBrand_List" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div>
                                                <div id="btnAddBrand">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabUnitShort" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlUnitShort" style="height: 415px; overflow: auto;">
                            <div style="float: left; padding-top: 5px;">
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td style="width: 60px;">
                                            门店分类
                                        </td>
                                        <td colspan="3">
                                            <div id="txtUnitSort" style="margin-top: 10px;">
                                            </div>
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: 350px; padding-left: 50px; padding-right: 10px; float: left;">
                                <div style="height: 5px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               <div id="tabQcode"  style="height: 1px; overflow: hidden;">
                     <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td>
                                  <div class="uploadImageItem">
                                        <span class="imagetit">&nbsp&nbsp&nbsp&nbsp 二维码图片预览</span>
                                        <div class="uploadWarp">
                                            <p class="viewImage" id="image">
                                                未获取二维码的图片
                                            </p>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                         <tr class="z_main_tb_tr">
                             <td>
                                   <div class ="upbtn">
                                       
                                        <span id="createCode" style="background: rgb(20, 206, 219);margin-left:105px">生成二维码</span>
                                        <span id="downCode" style="background:rgb(23, 178, 33);margin-left: 85px;">下载</span>
                                    </div>
                             </td>
                         </tr>
                         <tr class="z_main_tb_tr"> 
                             <td>
                                 <div style="margin-top:20px;">
                                 <span >&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp  二维码</span>
                                  <span style="margin-left:10px;">
                                     <input type="text" readonly="readonly" id="txtRQcode" style="width: 320px;height: 32px;text-indent: 5px;border: 1px solid #cecedc;" />
                                 </span>
                                     </div>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <div style="margin-top:20px;">
                                       <span >&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 消息类型</span>
                                       <span style="margin-left:10px;">
                                           <select  id="Valueselect" style="width: 320px;padding: 8px 0px 8px 5px;border: 1px solid #cecedc;">
                                                <option selected="selected" value="1" >文本</option>
                                                <option value="3"  >图文</option>
                                            </select>
                                      </span>
                                 </div>
                             </td>

                         </tr>
                         <tr>
                             <td>
                                 <div >
                                   <div id="text-display"  style="margin-top:20px; margin-bottom:20px;">
                                  <span style="margin-left:48px;float: left;">文本编辑</span>
                                   <span>
                                         <textarea id="text" style="width: 320px;height: 125px;padding: 5px;border: 1px solid #cfcedc;resize: none;margin-left: 13px;"></textarea>
                                   </span>
                                    </div>
                                    <div style="margin-top:20px;margin-left:80px; margin-bottom:20px;display:none" class="imgTextMessage hide" id="imageContentMessage" name="elems">
                                        <h2>
                                          提示:按住鼠标左键可拖拽排序图文消息显示的顺序 <b>已选图文</b>&nbsp;&nbsp;<b id="hasChoosed" style="color: Red">0</b>&nbsp;&nbsp;个</h2>
                                         <div class="list">
                                        </div>
                                         <span class="addBtn">添加</span>
                                        </div>
                                   </div>
                             </td>
                         </tr>
                        
                    </table>  
               </div>
            <div class="DivGridView" id="divBtn">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
 <!-- 添加图文消息-弹层 -->
    <div class="ui-mask hide" id="ui-mask">
    </div>
    <div class="activeListPopupArea hide" id="chooseEvents">
    </div>
    <div class="addImgMessagePopup" id="addImageMessage">
        <div class="commonTitleWrap">
            <h2>
                添加图文消息</h2>
            <span class="cancelBtn">取消</span> <span class="saveBtn">确定</span>
        </div>
        <div class="addImgMessageWrap clearfix">
            <span class="tit">标题</span>
            <input type="text" id="theTitle" class="inputName" />
            <span class="tit">分类</span>
            <select class="selectBox" id="imageCategory">
                <option selected>请选择</option>
            </select>
            <span class="queryBtn">查询</span>
        </div>
        <div class="radioList" id="imageContentItems">
        </div>
    </div>
    <div id="sortHelper" style="display: none;">
        &nbsp;</div>
    <div id="dragHelper" style="position: absolute; display: none; cursor: move; list-style: none;
        overflow: hidden;">
    </div>
    <!--关键字项-->
    <script id="keywordItemTmpl" type="text/html">
    <tr>
        <th class="num">序号</th>
        <th class="word">关键字</th>
    </tr>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <tr data-keyword="<#=JSON.stringify(item)#>">
            <td class="num"><#=i+1#></td>
            <td class="word">
                <#=item.KeyWord#>
            </td>
        </tr>
    <#}#>
    </script>
    <!--弹出的图文项-->
    <script id="addImageItemTmpl" type="text/html">
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
            <div id="addImage_<#=(currentPage-1)*pageSize+i#>" data-id="addImage_<#=item.TestId#>" data-flag="<#=showAdd?'add':''#>" data-displayIndex="<#=i#>" data-obj="<#=JSON.stringify(item)#>" class="item">
        	    <em class="radioBox"></em>
                <p class="picWrap"><img src="<#=item.ImageUrl#>"></p>
                <div class="textInfo">
                    <span class="name"><#=item.Title?item.Title:"未设置图文名称"#></span>
                    <span><#=item.Text?item.Text:"未设置图文内容"#></span>
                    <span class="delBtn"></span>
                </div>
            </div>
        <#}#>
    </script>
    <!--菜单模板-->
    <script id="menuTmpl" type="text/html">
    <div class="modelBox">
        <div class="menuWrap">
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <span data-menu="<#=JSON.stringify(item)#>"  class="<#=i==0?'on':''#> <#=((item.Status==1)?'select':'')#>">
                    <b><#=item.Name#></b>
                    <div data-menu="<#=JSON.stringify(item)#>"  class="subMenuWrap">
                        <em class="pointer"></em>
                        <a href="javascript:;" data-parentId="<#=item.MenuId#>" class="addBtn">添加</a>
                        <#for(var j=0;j<item.SubMenus.length;j++){ var subItem=item.SubMenus[j];if(subItem!=null){#>
                            <a href="javascript:;"   data-menu="<#=JSON.stringify(subItem)#>" class="tempSubMenu <#=subItem.Status==1?'select':''#>"><#=subItem.Name#></a>
                            <#}#>
                        <#}#>
                    </div>
                </span>
            <#}#>
        </div>
    </div>
    </script>
    <script id="accountTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value=<#=item.ApplicationId#>><#=item.WeiXinName#></option>
    <#}#>
    </script>
    <!--option模板-->
    <script id="optionTmpl" type="text/html">
    <#showAll=showAll?showAll:false;if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option value="<#=item.TypeId#>" data-obj=<#=JSON.stringify(item)#>><#=item.TypeName#></option>
    <#}#>
    </script>
    <!--option模板  模块-->
    <script id="moduleTmpl" type="text/html">
    <#for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.PageID#>" value=<#=JSON.stringify(item)#>><#=item.ModuleName#></option>
    <#}#>
    </script>
    <!--活动类别模板-->
    <script id="eventTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.EventTypeId#>" value=<#=JSON.stringify(item)#>><#=item.EventTypeName#></option>
    <#}#>
    </script>
    <!--资讯类别模板-->
    <script id="NewsTypeTmpl" type="text/html">
    <#if(showAll){#>
        <option value="">全部</option>
    <#}#>
    <#itemList=itemList?itemList:[];for(var i=0;i<itemList.length;i++){var item=itemList[i];#>
        <option data-value="<#=item.NewsTypeId#>" value=<#=JSON.stringify(item)#>><#=item.NewsTypeName#></option>
    <#}#>
    </script>
    <!--弹出层的模板-->
    <script id="popDivTmpl" type="text/html">
    <div class="commonTitleWrap">
    	<h2><#=topTitle#></h2>
        <span id="cancelBtn" class="cancelBtn">取消</span>
        <span id="saveBtn" class="saveBtn">确定</span>
    </div>

    <div class="activeQueryWrap clearfix">
        <span class="tit" ><#=popupName?popupName:"活动名称"#></span>
        <input id="eventName" type="text" class="inputName" />
        <span class="tit"><#=popupSelectName?popupSelectName:"活动分类"#></span>
        <select id="pop_eventsType" class="selectBox">
        	<option selected>请选择</option>
        </select>
        <span id="searchEvents" class="queryBtn">搜索</span>
    </div>
    
    <div class="activeListWrap">
        <table width="1038" border="1" id="itemsTable">
          
        </table>
    </div>
    <div class="pagination">
      <a href="#" class="first" data-action="first">&laquo;</a>
      <a href="#" class="previous" data-action="previous">&lsaquo;</a>
      <input type="text" readonly="readonly" data-max-page="40" />
      <a href="#" class="next" data-action="next">&rsaquo;</a>
      <a href="#" class="last" data-action="last">&raquo;</a>
    </div>

    </script>
    <!--弹层的每行数据-->
    <script id="itemTmpl" type="text/html">
    <tr>
    <th width="65">操作</th>
    <#for(var i=0;i<title.length;i++){ var item=title[i];#>
    <th><#=item#></th>
    <#}#>
    </tr>
    <#if(type=="chooseNews"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input  data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.NewsName#></td>
                    <td><#=item.NewsTypeName#></td>
                    <td><#=item.PublishTime#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
     <#if(type=="chooseEvents"){#>
        <#if(itemList.length){#>
            <#for(var i=0;i<itemList.length;i++){ var item=itemList[i];#>
                <tr id="temp_<#=i#>">
                <td><input data-id="temp_<#=i#>" type="radio" name="item" value="<#=JSON.stringify(item)#>"></td>
            
                    <td><#=item.EventName#></td>
                    <td><#=item.EventTypeName#></td>
                    <td><#var result="";switch(item.EventStatus){case 10:result="未开始";break;case 20:result="运行中";break;case 30:result="暂停";break;case 40:result="停止";break;default:result="未定义";break;}#><#=result#></td>
                    <td><#=item.DrawMethod#></td>
                </tr>
            <#}#>
        <#}#>
    <#}#>
    </script>

    <link href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" rel="stylesheet" />
    <script src="<%=StaticUrl+"/Framework/Javascript/Other/editor/kindeditor.js"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Controller/UnitEditCtl.js?v=0.23"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Model/UnitVM.js?v=0.1"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Model/UnitDetailVM.js?v=0.1"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Store/UnitVMStore.js?v=0.1"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/Store/UnitEditVMStore.js?v=0.1"%>" type="text/javascript"></script>
    <script src="<%=StaticUrl+"/Module/Basic/Unit/View/UnitEditView.js?v=0.2"%>" type="text/javascript"></script>
    <script type="text/javascript">
            var map = new BMap.Map("allmap");
            map.centerAndZoom(new BMap.Point(116.331398, 39.897445), 11);
            map.enableScrollWheelZoom(true);
            map.addEventListener("click", function (e) {
                $('#longitude').val(e.point.lng);
                $('#latitude').val(e.point.lat);
            });
            // 用经纬度设置地图中心点
            function theLocation() {
                if ($('#map_area').val() != "") {
                    var area = $('#map_area').val();
                    map.clearOverlays();
                    var local = new BMap.LocalSearch(map, {
                        renderOptions: { map: map }
                    });
                    local.search(area);
                }
            }
            function initMap() {
                var lng = $('#longitude').val(), lat = $('#latitude').val();
                var reg = /^\d+\.?\d*$/;
                if (reg.test(lng) && reg.test(lat)) {
                    var new_point = new BMap.Point(lng, lat);
                    var marker = new BMap.Marker(new_point);  // 创建标注
                    map.addOverlay(marker);              // 将标注添加到地图中
                    setTimeout(function () {
                        map.panTo(new_point);
                    }, 1000);
                }
            }
            function closeMap() {
                $('#mapSection').hide();
            }
            function confirmMap() {
                var lng = $('#longitude').val();
                var lat = $('#latitude').val();
                var reg = /^\d+.?\d+$/;
                if (!reg.test(lng) || !reg.test(lat)) {
                    $('#mapSection').hide();
                    return;
                }
                Ext.getCmp("txtLongitude").jitSetValue(lng + ',' + lat);
                $('#mapSection').hide();
            }
            //$(function () {
            //    var lng = $('#tdLng');
            //    var btn = $('<input type="button" value="..." />');
            //    btn.click(function () {
            //        lng.find('input[type="text"]').trigger('click');
            //    });
            //    btn.appendTo(lng);
            //});
    </script>
    <style type="text/css">
        .uploadImageUrl
        {
            padding-left: 30px;
        }
    </style>
    </div>
</asp:Content>
