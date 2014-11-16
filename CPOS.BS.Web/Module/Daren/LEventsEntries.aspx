<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动参赛作品管理</title>
    <script src="/Framework/javascript/Biz/DarenType.js" type="text/javascript"></script>
    <script src="Controller/LEventsEntriesCtl.js" type="text/javascript"></script>
    
			<script type="text/javascript">
            function ClickCheckedEvent(o, id){
					if($(o).attr("checked") == "false"){
						$(o).attr("checked","true").addClass("DaRenCheckBoxTrue").removeClass("DaRenCheckBoxfalse") ;
						
					}else{
						$(o).attr("checked","false").addClass("DaRenCheckBoxfalse").removeClass("DaRenCheckBoxTrue") ;
					}
                fnSave(id, ($(o).attr("checked") == "true"));
				}
            function ClickCheckedEvent2(o, id){
					if($(o).attr("checked") == "false"){
						$(o).attr("checked","true").addClass("DaRenCheckBoxTrue").removeClass("DaRenCheckBoxfalse") ;
						
					}else{
						$(o).attr("checked","false").addClass("DaRenCheckBoxfalse").removeClass("DaRenCheckBoxTrue") ;
					}
                fnSave2(id, ($(o).attr("checked") == "true"));
				}
				function photoSize(imgObject,oWidth,oHeight)
				{
						
						
						if(imgObject.width >= imgObject.height){
									
									imgObject.width = (imgObject.width/imgObject.height) * oWidth;
									imgObject.height  = oHeight;
									imgObject.style.marginLeft = -((imgObject.width  - oWidth)/2 ) +"px";
							}else{
									imgObject.width  = oWidth;
								
									imgObject.height = (imgObject.height / imgObject.width) * oHeight;
									
							}
							
				}
            </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<style type="text/css">
    	.DaRenImgBox{ float:left; width:240px; height:280px; overflow:hidden; background-color:#f8f8f8; padding:12px; border-bottom:2px solid #e5e6e6; border-radius:5px; margin-right:20px; margin-left:5px; _display:inline; margin-top:15px;}
		.DaRenImgBox ul{ overflow:hidden;}
		.DaRenImgBox li{ clear:both;}
		.DaRenImgBox .DaRenImg{ height:180px; overflow:hidden; position:relative; cursor:pointer;}
		.DaRenImgIndex{ position:absolute; left:2px; top:2px; border-radius:2px; height:22px; line-height:22px; padding: 0 10px; background-color:#f8f8f8; color:#000;}
		.DaRenImgT{ clear:both;}
		.DaRenImgT .DaRenImgTLi{ width:100px; float:left; height:35px; margin-top:5px; line-height:35px; overflow:hidden; position:relative; font-size:14px;}
		.DaRenIcnSkin{ background-image:url(DaRenSkin.png); background-repeat:no-repeat;}
		.DaRenCheckBoxTrue{ width:21px; height:21px; background-position:-10px -16px; display:block; position:absolute; left:0; top:8px; cursor:pointer;}
		.DaRenCheckBoxfalse{ width:21px; height:21px; background-position:-48px -16px; display:block; position:absolute; left:0; top:8px; cursor:pointer;}
		.DaRenCheckBoxGood{ width:33px; height:33px; background-position:-9px -48px; display:block; position:absolute; left:0; top:0;}
		.DaRenCheckBoxComment{ width:33px; height:33px; background-position:-46px -48px; display:block; position:absolute; left:0; top:0;}
		.DaRenImgTLi em{ font-style:normal; display:block; margin-left:30px;}
		.DaRenImgTLi i{ font-style:normal; display:block; margin-left:40px;}
		.DaRenPageBox{ text-align:center; padding-top:20px; clear:both; overflow:hidden;}
		.DaRenPageBox a,.DaRenPageBox a:hover{ padding:0 12px; height:28px; line-height:28px; background-color:#8c9898; border-radius:2px; color:#fff; text-decoration:none; display:block; margin:0 5px; float:left; font-weight:bold;}
		.DaRenPageBox a.DaRenPageCur,.DaRenPageBox a.DaRenPageCur:hover{ background-color:#ea746b;}
		.DaRenIcnPrev{ width:11px; height:14px; display:block; background-position:-11px -105px; text-indent:-9999px; margin-top:8px;}
		.DaRenIcnNext{width:11px; height:14px; display:block; background-position:-26px -105px; text-indent:-9999px; margin-top:8px}
	    .DaRenNoData { text-align:center; height:28px; line-height:28px; }
    </style>
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div id="view_Search" class="view_Search" style="height:44px;" >
                    <div id='span_panel' style="float:left; width:620px;"></div>
                    <div id='btn_panel' style="float:left; width:100px;"></div>
                    <%--<div id='span_create' style="float:left; width:100px;"></div>--%>
                </div>
            </div>
            <div class="art-titbutton" >
                <div class="view_Button" >
                    <span id='span_create'></span>
                    <span id='span_create2'></span>
                </div>
            </div>
            <div id="grid" style="margin-top:0px;">
				
                
                
                
               
                
             
            </div>
			<div class="DaRenPageBox">
            	 <table align="center" style="margin-left:auto; margin-right:auto; height:30px;">
                  <tr>
                    <td id="page" align="center">
                        
                    </td>
                  </tr>
                </table>
            </div>
            <%--<a href="#" ><span class="DaRenIcnSkin DaRenIcnPrev"></span></a>
                        <a href="#">1</a>
                        <a href="#">2</a>
                        <a href="#" class="DaRenPageCur">3</a>
                        <a href="#">4</a>
                        <a href="#"><span class="DaRenIcnSkin DaRenIcnNext"></span></a>--%>

            <div class="DivGridView" id="DivGridView">
            </div>
            <div class="cb">
            </div>
        </div>
    </div>

</asp:Content>
