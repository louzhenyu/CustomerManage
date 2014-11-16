<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>会员统计</title>
    
    <meta charset="utf-8">
    <meta http-equiv='X-UA-Compatible' content='IE=EmulateIE7' />
    <link href="<%=StaticUrl+"/Module/Vip/vipShow/css/common.css"%>" type="text/css" rel="stylesheet" />
     <style type="text/css">
    .sale_table { width:100%; font-family:"微软雅黑"; font-size:14px; background:#fff;
    -webkit-box-shadow:0 0 5px rgba(0,0,0,.4);
    -moz-box-shadow:0 0 5px rgba(0,0,0,.4);
    -o-box-shadow:0 0 5px rgba(0,0,0,.4);
    box-shadow:0 0 5px rgba(0,0,0,.4);
    }
    .sale_table_outer { padding:30px;}
    .sale_table dl { float:left; width:12%; text-align:center;}
    .sale_table dt { height:35px; line-height:35px; border-bottom:1px solid #C8D1D8; color:#333; background:#FDFDFD;}
    .sale_table dd { height:41px; line-height:60px; color:#666; border-right:1px solid #fff; border-bottom:1px solid #fedbc3;}
    .sale_table dl.fore { width:40%; background:url(/Module/vip/vipShow/images/sale_table_bg1.png) no-repeat top center;}
    .sale_table dl.fore dd { color:#003a88;}

    /*清除浮动*/
    .clear{display:block;clear:both;overflow:hidden;height:0;line-height:0;font-size:0}
    .clearfix:after{content:" ";visibility:hidden;display:block;clear:both;height:0;font-size:0}
    .clearfix{*zoom:1}
    </style>
    <%--<script src="Controller/VipTransferCtl.js" type="text/javascript"></script>
    <script src="Model/VipTransferVM.js" type="text/javascript"></script>
    <script src="Store/VipTransferVMStore.js" type="text/javascript"></script>
    <script src="View/VipTransferView.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="MainMenuA">
            <div class="LcTu" style="padding-bottom: 20px;">
                <div id="loading" style="display: none;">数据正在加载中...</div>
                <div  style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 16px; font-weight: bold;  left: 172px; top: 296px; width: 140px; text-align: center;"><span class="WeixinPerson" id="CountAll">0</span>人</div>

                <div  style="position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 200px; top: 65px; width: 80px; text-align: center"><span class="WeixinPerson" id="weixin"></span>人</div>
                <div  style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 37px; top: 154px; width: 80px; text-align: center"><span class="WeixinPerson" id="secondCode"></span>人</div>
                <div style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 10px; top: 327px; width: 80px; text-align: center"><span class="WeixinPerson" id="ShopIpad"></span>人</div>
                <div  style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 123px; top: 452px; width: 80px; text-align: center"><span class="WeixinPerson" id="VipApp"></span>人</div>
                <div  style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 305px; top: 442px; width: 80px; text-align: center"><span class="WeixinPerson" id="Weibo"></span>人</div>
                <div  style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 400px; top: 312px; width: 80px; text-align: center"><span class="WeixinPerson" id="WebShop"></span>人</div>
                <div style="cursor:pointer;position: absolute; color: #fe7c24; font-size: 14px; font-weight: bold; left: 365px; top: 147px; width: 80px; text-align: center"><span class="WeixinPerson" id="TelSevire"></span>人</div>

                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 355px; top: -16px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_weixin"><span>+</span><span id="AddTxt_weixin">12</span></div>
                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: -64px; top: 98px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_secondCode"><span>+</span><span id="AddTxt_secondCode">12</span></div>
                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: -90px; top: 449px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_ShopIpad"><span>+</span><span id="AddTxt_ShopIpad">12</span></div>
                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold;  left: 36px; top: 552px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_VipApp"><span>+</span><span id="AddTxt_VipApp">12</span></div>

                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold;  left: 455px; top: 547px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_Weibo"><span>+</span><span id="AddTxt_Weibo">12</span></div>

                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 547px; top: 412px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_WebShop"><span>+</span><span id="AddTxt_WebShop">12</span></div>

                <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 561px; top: 109px; width: 104px; text-align: center; height: 78px; line-height: 78px; filter: alpha(opacity=0); opacity: 0;" id="Add_TelSevire"><span>+</span><span id="AddTxt_TelSevire">12</span></div>


                <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 292px; top: 88px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_weixin"><span>+</span><span id="DOP_weixin"></span></div>
                <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 68px; top: 188px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_secondCode"><span>+</span><span id="DOP_secondCode"></span></div>
                
                <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 44px; top: 275px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_ShopIpad"><span>+</span><span id="DOP_ShopIpad"></span></div>
				  <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 115px; top: 387px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_VipApp"><span>+</span><span id="DOP_VipApp"></span></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 265px; top: 440px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_Weibo"><span>+</span><span id="DOP_Weibo"></span></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 352px; top: 112px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_TelSevire"><span>+</span><span id="DOP_TelSevire"></span></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 44px; font-weight: bold; left: 392px; top: 261px; width: 104px; text-align: center; height: 52px; line-height: 52px; display: none;" id="DUP_WebShop"><span>+</span><span id="DOP_WebShop"></span></div>
                  
                  
                  <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 178px; top: 0px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor:pointer; " onclick="fnShowList('3')"></div>
              <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 13px; top: 75px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;" onclick="fnShowList('4')"></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 0px; top: 243px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;" onclick="fnShowList('2')" ></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 99px; top: 370px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;" onclick="fnShowList('1')" ></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 282px; top: 361px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;" onclick="fnShowList('7')" ></div>
                  <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 376px; top: 232px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;" onclick="fnShowList('6')" ></div>
              <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 347px; top: 67px; width: 122px; text-align: center; height: 117px; line-height: 78px; z-index: 1; cursor: pointer;"  onclick="fnShowList('5')" ></div>
                   <div style="position: absolute; color: #fe7c24; font-size: 50px; font-weight: bold; left: 150px; top: 160px; width: 179px; text-align: center; height: 176px; line-height: 78px; z-index: 1; cursor: pointer;"  onclick="fnShowList('')" ></div>
            </div>
            <input type="hidden" value="" id="timesamp" />

        </div>
        <%--<div class="sale_table">
            <div class="sale_table_inner clearfix">
                  <dl class="fore">
                    <dt>顾客漏斗</dt>
                    <dd>总数量</dd>
                    <dd>激活数量</dd>
                    <dd>购买者</dd>
                    <dd>重复购买</dd>
                    <dd>品牌大使</dd>
                  </dl>
                  <dl>
                    <dt>门店招募</dt>
                    <dd><span id="storeTotal"></span></dd>
                    <dd><span id="storeActive"></span></dd>
                    <dd><span id="storeConsumer"></span></dd>
                    <dd><span id="storeMConsumer"></span></dd>
                    <dd><span id="storeLoyalConsumer"></span></dd>
                  </dl>
                  <dl>
                    <dt>微信关注</dt>
                    <dd><span id="weixinTotal"></span></dd>
                    <dd><span id="weixinActive"></span></dd>
                    <dd><span id="weixinConsumer"></span></dd>
                    <dd><span id="weixinMConsumer"></span></dd>
                    <dd><span id="weixinLoyalConsumer"></span></dd>
                  </dl>
                  <dl>
                    <dt>第三方数据</dt>
                    <dd><span id="thirdPartyTotal">3,521</span></dd>
                    <dd><span id="thirdPartyActive">3,212</span></dd>
                    <dd><span id="thirdPartyConsumer">2,219</span></dd>
                    <dd><span id="thirdPartyMConsumer">1,343</span></dd>
                    <dd><span id="thirdPartyLoyalConsumer">452</span></dd>
                  </dl>
                  <dl>
                    <dt>吸新活动</dt>
                    <dd><span id="recruitmentTotal">2,449</span></dd>
                    <dd><span id="recruitmentActive">2,449</span></dd>
                    <dd><span id="recruitmentConsumer">1,983</span></dd>
                    <dd><span id="recruitmentMConsumer">1,023</span></dd>
                    <dd><span id="recruitmentLoyalConsumer">324</span></dd>
                  </dl>
                  <dl>
                    <dt>老数据激活</dt>
                    <dd><span id="awakeTotal">2,893</span></dd>
                    <dd><span id="awakeActive">2,893</span></dd>
                    <dd><span id="awakeConsumer">1,284</span></dd>
                    <dd><span id="awakeMConsumer">632</span></dd>
                    <dd><span id="awakeLoyalConsumer">219</span></dd>
                  </dl>
            </div>
        </div>--%>
    </div>
<script type="text/javascript" src="../vipShow/js/common.js"></script>
<script type="text/javascript">
$(function(){
	function ResizeWinDows(){var  mainHeight = $("#MainConnextId").height(),MainMenuHeight = $("#MainMenuId").height();
	if(mainHeight >= MainMenuHeight){
			$("#MainMenuId").css("height",mainHeight);	
		}
	if((Win.H() -75 ) >= mainHeight ){
	
			$("#MainMenuId").css("height",Win.H() -75);
		}
	}
	ResizeWinDows();
	$(window).resize(function(){
		ResizeWinDows();	
	})
	$("#timesamp").val('');
	ShopDetailAjax(1);
})
var  idArr = ["weixin", "secondCode", "ShopIpad", "VipApp", "Weibo", "WebShop", "TelSevire"];
var  Win={
		Loading:function(type){
				
			
				if(type == "CLOSE"){
						$(".loading").hide();
						return false; 
					}
				 var getWindowsWidth = $(window).width(), getWindowsHeight = $(window).height();
					$(".loading").css({left: (getWindowsWidth - $(".loading").width())/ 2 , top: (getWindowsHeight - $(".loading").height())/ 2 }); 
					$(".loading").show();
			},
		W: function(){
				return $(window).width(); 
			},
		H:function(){
			return  $(window).height();
			},
		DH:function(){
			return  $(document).height();
			}
	}
var url = '/Module/Vip/VipSearch/Handler/VipHandler.ashx'; 

//var url = 'test.php'; 
function ShopDetailAjax(type){

		//var jsonarr = {'action':"getShopDetail",ReqContent:JSON.stringify({"common":{},"special":{"timesamp":$("#timesamp").val()}})};
			$.ajax({
				type:'get',
				url:url,
				data:{method:"GetShowCount",Timestamp:$("#timesamp").val()},
				timeout:90000,
				cache:false,
				beforeSend:function(){
					if(type==1){
					    $("#loading").show();
					}
				},
				dataType : 'json',
				success:function(data){//alert(JSON.stringify(data));
					$("#loading").hide();
					if(data.Code == 200){
						//	var data = {"Code":"200","Description":"操作成功","Exception":null,"Content":{"vipSourceId":null,"vipSourceName":null,"iCount":8,"newTimestamp":1378694165637,"VipAttentionInfoList":[{"vipSourceId":"1","vipSourceName":"会员APP","iCount":4,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"2","vipSourceName":"门店Pad","iCount":2,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"3","vipSourceName":"微信","iCount":4,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"4","vipSourceName":"二维码","iCount":1,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"5","vipSourceName":"电话客服","iCount":3,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"6","vipSourceName":"电商导入","iCount":1,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"7","vipSourceName":"微博","iCount":1,"newTimestamp":1378694165637,"VipAttentionInfoList":null},{"vipSourceId":"8","vipSourceName":"网站注册","iCount":0,"newTimestamp":1378694165637,"VipAttentionInfoList":null}]},"count":0,"NewTimestamp":1378694165513};
					var odata = {};
					var olength =data.Content;
					if(olength.length > 0 ){
						for(var i=0; i<olength.length; i++){
							switch(olength[i].VipSourceID){
								case "3":
								    odata.weixin = olength[i].Rnt;
								break;
								case "4":
								    odata.secondCode = olength[i].Rnt;
								break;
								case "2":
								    odata.ShopIpad = olength[i].Rnt;
								break;
								case "1":
								    odata.VipApp = olength[i].Rnt;
								break;
								case "7":
								    odata.Weibo = olength[i].Rnt;
								break;
								case "6":
								    odata.WebShop = olength[i].Rnt;
								break;
								case "5":
								    odata.TelSevire = olength[i].Rnt;
								break;
							    default:
							        break;
							}
						}
					var o = {"Code":"200","content":{"count":"","weixin":odata.weixin,"secondCode":odata.secondCode,"ShopIpad":odata.ShopIpad,"VipApp":odata.VipApp,"Weibo":odata.Weibo,"WebShop":odata.WebShop,"TelSevire":odata.TelSevire},"description":"操作成功"};
					
                     //   if(o.Code == 200){
						$("#timesamp").val(data.NewTimestamp)
						if(type == 1){
							
							$("#weixin").text(o.content.weixin);
							$("#secondCode").text(o.content.secondCode);
							$("#ShopIpad").text(o.content.ShopIpad);
							$("#VipApp").text(o.content.VipApp);
							$("#Weibo").text(o.content.Weibo);
							$("#WebShop").text(o.content.WebShop);
							$("#TelSevire").text(o.content.TelSevire);
								AnimateFunction();
							
							}else{
						//	var o = {"Code":"200","content":{"count":"","weixin":data.count,"secondCode":"0","ShopIpad":"0","VipApp":"0","Weibo":"0","WebShop":"0","TelSevire":"0"},"description":"操作成功"};	
								AddNewData(o.content)
							}	
						}
					}
						//setTimeout(function(){ShopDetailAjax()},5000);
					},
				error:function(){
						//setTimeout(function(){ShopDetailAjax()},5000);	
					}
				})
}
var Timeouta = null;
function AnimateFunction(){
	
	var  AString = ''; IntNum = new Array();
		var c = 0; 
	for(var i=0; i<idArr.length; i++){
			var getStyle = $("#"+idArr[i]).parent().attr("style");
			
			AString += '<div style="'+getStyle+'" id="WarpApp'+idArr[i]+'"><Span class="WeixinPerson">'+($("#"+idArr[i]).text())+'</Span></div>';
			c += parseInt($("#"+idArr[i]).text());	

			//IntNum.push(parseInt($("#"+idArr).text()));
		}	
	$(".LcTu").append(AString);
	$("div[id^=WarpApp]").animate({"left":195,"top":238,"opacity":0},1500,function(){
		$("#CountAll").text(c); 
			$(this).remove();
		});
		//var m=0;
		
	/*Timeouta = setInterval(function(){
		c += IntNum[m]
		 if(m>=IntNum.length){clearTimeout(Timeouta); return false;}
		m++;*/
	//$("#CountAll").text(c); },500)
}
function AddNewData(Dataobj){
	var NewDataArray = new Array();
	//var  idArr = ["weixin", "secondCode", "ShopIpad", "VipApp", "Weibo", "WebShop", "TelSevire"];	
	NewDataArray.push(Dataobj.weixin);
	NewDataArray.push(Dataobj.secondCode);
	NewDataArray.push(Dataobj.ShopIpad);
	NewDataArray.push(Dataobj.VipApp);
	NewDataArray.push(Dataobj.Weibo);
	NewDataArray.push(Dataobj.WebShop);
	NewDataArray.push(Dataobj.TelSevire);
	
	for(var u=0; u<NewDataArray.length; u++){
		// 检查数据是否为空
			if(NewDataArray[u]=="" || NewDataArray[u]=="0"){
					NewDataArray[u] = 0;
					
				}
				$("#AddTxt_"+idArr[u]).text(NewDataArray[u]);
		}
	for(var j=0; j<idArr.length; j++){
		
			if(parseInt(NewDataArray[j])!=0){
					if(j>0 && j<4){
					$("#Add_"+idArr[j]).css({"left":parseInt($("#Add_"+idArr[j]).css("left"))-50, "opacity":0});	
						}else{
					$("#Add_"+idArr[j]).css({"left":parseInt($("#Add_"+idArr[j]).css("left"))+50, "opacity":0});	
					}
			
				}
		}
	for(var g=0; g<NewDataArray.length; g++){
		
		if(NewDataArray[g]!=0){
			$("#Add_"+idArr[g]).attr("index",g);
			if(g>0 && g<4){
			
			$("#Add_"+idArr[g]).show().animate({"opacity":1,"left":parseInt($("#Add_"+idArr[g]).css("left"))+50},400,function(){ 	
			
					var indexo = $(this).attr("index");
					
			 		AnimateAddNewData(idArr[indexo],NewDataArray[indexo]);	
					$(this).hide();
				 }); 
			}else{
			
				$("#Add_"+idArr[g]).show().animate({"opacity":1,"left":parseInt($("#Add_"+idArr[g]).css("left"))-50},400,function(){
					 var indexo = $(this).attr("index");
					 
			 		AnimateAddNewData(idArr[indexo],NewDataArray[indexo]);	
					 $(this).hide();
				}); 	
				}
		}
	}
}
function AnimateAddNewData(idDe,num){

		var bstring = '<div id="NewUI_'+idDe+'" style="'+$("#Add_"+idDe).attr("style")+'"><span>+</span><span>'+num+'</span></div>'
		$(".LcTu").append(bstring);
		var ParentLeft = parseInt($("#"+idDe).parent().css("left")), ParentTop = parseInt($("#"+idDe).parent().css("top"));
			$("#NewUI_"+idDe).animate({left:ParentLeft, top: ParentTop, opacity:0},900,function(){$(this).remove();
				$("#"+idDe).text((parseInt(num)+parseInt($("#"+idDe).text())));
				if($("#DUP_"+idDe).length > 0){
						$("#DOP_"+idDe).text(num)
						
					
						$("#DUP_"+idDe).show().attr("cleft",$(this).css("left")).attr("ctop",$(this).css("top")).animate({"left":247,"top":338,"opacity":0},800,function(){
								$("#CountAll").text((parseInt(num)+parseInt($("#CountAll").text()))); 
								$(this).css({"left":$(this).attr("cleft"),"top":$(this).attr("ctop"),"opacity":"1"}).hide();	
									
							})
					}
		})
}
function fnShowList(type) {
    //location.href = "VipSearch.aspx?mid=" + getUrlParam("mid") + "&vip_source_id=" + type;
    if (type == '') {
        location.href = '/module/vipmanage/querylist.aspx?mid=' + getUrlParam('mid');
        return;
    }
    location.href = "/module/vipmanage/querylist.aspx?mid=" + getUrlParam("mid") + "&vip_source_id=" + type;
}
</script>
</asp:Content>
