<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Role.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/pub/JITStoreGrid.js" type="text/javascript"></script>
    <%--<script src="/Framework/javascript/Biz/ClientPosition.js" type="text/javascript"></script>--%>
    <script src="/Framework/javascript/pub/JITStoreSearchPannel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/pub/JITStoreSelectPannel.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/options.js" type="text/javascript"></script>
    <%--  <script src="/Framework/javascript/Biz/Province.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/District.js" type="text/javascript"></script>--%>
    <!--瀑布流-->
    <%--<script src="View/waterfall.js" type="text/javascript"></script>--%>
    <%--<!--图片放大-->
<link href="View/zoomer.css" rel="stylesheet" type="text/css" />
<script src="View/zoomer.js" type="text/javascript"></script>--%>
    <!--fancyboxbegin-->
    <%--<script type="text/javascript" src="/Framework/javascript/other/fancybox/jquery.mousewheel-3.0.2.pack.js"></script>--%>
    <script type="text/javascript" src="/Framework/javascript/other/fancybox/jquery.fancybox-1.3.1.js"
        charset="gb2312"></script>
    <link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox/jquery.fancybox-1.3.1.css"
        media="screen" />
    <!--fancyboxend-->
    <%--<!--fancyboxbegin-->
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/lib/jquery.mousewheel-3.0.6.pack.js"></script>

<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.js?v=2.1.4" charset="gb2312"></script>
<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/jquery.fancybox.css?v=2.1.4" media="screen" />

<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>

<link rel="stylesheet" type="text/css" href="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" />
<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>

<script type="text/javascript" src="/Framework/javascript/other/fancybox-v2/source/helpers/jquery.fancybox-media.js?v=1.0.5"></script>
<!--fancyboxend-->--%>
    <script src="Controller/TaskDataPhotoCtl.js" type="text/javascript"></script>
    <script src="Model/TaskDataPhotoVM.js" type="text/javascript"></script>
    <script src="Store/TaskDataPhotoVMStore.js" type="text/javascript"></script>
    <script src="View/TaskDataPhotoView.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Framework/Javascript/Other/photoControl/ImageView.js"></script>
    <script type="text/javascript" src="/Framework/Javascript/Other/photoControl/window1.js"></script>
    <script language="javascript" type="text/javascript">
var __imgpath="<%=CurrentUserInfo.ImgPath %>";
var __clientid=<%=CurrentUserInfo.ClientID %>;</script>
    <style type="text/css">
body,div{ margin:0; padding:0}
img{ border:0}
#container 
{
    text-align:center; 
}
#container .cell 
{
    padding:3px 3px 3px 3px; 
    *padding:3px 3px 3px 3px  !important;
    border:1px solid #E3E3E3; 
    background:#F5F5F5; 
    margin:3px;
    float:left;
    width:202px;
    height:278px;

}
#container .celldiv
{ 
  background-color:White;
  margin-top:3px;
  *margin-top:3px  !important;
  height:196px;
  width:202px;
  text-align:center;
}
#container .cell img 
{   
 max-width:190px;
 max-height:195px; 
 -ms-interpolation-mode: bicubic;
 }
 
 #container img 
{   
 max-width:700px;
 max-height:505px; 

 }
#container p 
{
    line-height:20px;
    margin-top:5px;
}

#jittextfield-1010-inputEl
{
    height:19px;
}
#jitbutton-1009-btnInnerEl
{
    height:19px;
    }
<!--fancybox 与 extjs 兼容性调整 begin-->
.x-border-box, .x-border-box *
{
    box-sizing: content-box;
    -moz-box-sizing: content-box;
    -ms-box-sizing: content-box;
    -webkit-box-sizing: content-box;
}
.x-form-trigger
{
    margin-left:7px;*margin-left:0px;
	width:17px;
}
.x-border-box .x-form-text{
	height:19px;
}

.gotop
{
    width:25px;
    position:fixed; 
    bottom:0px; 
    right:5px;
    
}
.gotop a
{
    display: block;
    position: relative;
    width: 17px;
    height: 66px;
    padding: 28px 4px 0;
    margin: 5px 0;
    text-align: center;
    line-height: 14px;
    text-decoration: none;

    background-image:url(/Framework/Image/icon/gotop/link.png);
    background-repeat:none;
}
.gotop a:hover
{
    background-image:url(/Framework/Image/icon/gotop/hover.png);
    background-repeat:none;
}
<!--fancybox 与 extjs 兼容性调整 end-->
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article" id="phontImg">
            <%-- <img src="http://report.jitmarketing.cn:9021/12/8757/1367481424031.jpg" />--%>
            <div class="art-tit">
                <div class="view_Search">
                    <span id='span_panel'></span>
                </div>
                <div class="view_Search2">
                    <span id='span_panel2'></span>
                </div>
            </div>
            <div style="height: 560px; overflow-y: auto;">
                <div id="container">
                </div>
                <div class="cb" style="width: 1083px; border: 1px solid #dbdbdb; margin: 5px;">
                    <table width="90%">
                        <tr>
                            <td align="center" style="height: 30px">
                                <b id="td_load">图片加载中...</b>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="gotop" style="display: none">
        <a href="#">返回顶部</a></div>
    <!--以下为old 图片控件-->
    <%--<div style="display:block; height:0px; overflow:hidden; ">
         <img style="visibility: hidden;float: right;" width="460px" height="360px" id='ssvfhpplmyid'
        onerror="javascript:ssvfhpplmyid.src='/Framework/Javascript/Other/photoControl/photoImg/no_picture.jpg';" alt="无图片" />
</div>--%>
    <script language="javascript" type="text/javascript">
        window.onscroll = function () {
            // alert("1");
        }
        //    var ssvfhpplwin = Ext.create('Ext.window.CustomerWindow', {
        //        picture: true,
        //        turnBar: 'button',
        //        closeAction: 'hide',
        //        imageId: 'ssvfhpplmyid',
        //        modal: true,
        //        datas: [{ name: '', url: ''}]
        //    });
        //    function showImages(str, title) {
        //        str = 'P_001.jpg|P_002.jpg'; title = '终端1|终端2';
        //        //        显示照片
        //        if (str == "" || str == null)
        //            str = "no_picture.jpg";
        //        var datas = getPictures(str, unescape(title));
        //        ssvfhpplwin.setDatas(datas);
        //        ssvfhpplwin.dataIndex = 0;
        //        ssvfhpplwin.show();
        //    }

        //    function getPictures(pictureStr, title) {
        //        var PTArr = pictureStr.toString().split('|');
        //        var pictures = new Array();
        //        for (var i = 0; i < PTArr.length; i++) {
        //            if (PTArr[i] != null && PTArr[i] != "") {
        //                var temp = new Object();
        //                temp.url = "" + PTArr[i];
        //                temp.name = title;
        //                pictures.push(temp);

        //            }
        //        }
        //        return pictures;
        //    }
    </script>
</asp:Content>
