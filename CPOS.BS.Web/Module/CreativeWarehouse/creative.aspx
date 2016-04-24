<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>发起活动</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <link href="css/jquery.bxslider.css?v=0.1" rel="stylesheet" type="text/css" />
     <link href="../static/js/vendor/jquery-ui-1.11.4/jquery-ui.css" rel="stylesheet" type="text/css" />
      <link href="http://www.vveshow.com/plugins/jvveshow/1.0.0/resource/css/jvveshow/jvveshow.min.css" rel="stylesheet" type="text/css" />
    <link href="css/creative.css?v=0.1" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
window.onbeforeunload = function() {
var n = window.event.screenX - window.screenLeft;
var b = n > document.documentElement.scrollWidth - 20;
if (!(b && window.event.clientY < 0 || window.event.altKey)) {
window.event.returnValue = "未保存的数据可能会丢失!"; //这里可以放置你想做的操作代码
}
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="allPage" id="section"  data-js="js/creative">
       <div id="closeWin1" style="display: none"></div>
         <div class="contentArea_vipquery">
           <div class="navigation">
              <ul>
              <li  data-showpanel="nav01"><em>1</em>模板</li>
              <li data-showpanel="nav02"><em>2</em>编辑</li>
              <li data-showpanel="nav03"><em>3</em>设置</li>
              <li  class="one" data-showpanel="nav04"><em>4</em>推广</li>
              <img src="../styles/images/newYear/nav/jt.png" alt="" />
              <img src="../styles/images/newYear/nav/jton.png" class="hide" alt="" />
   </ul>
            </div>
           <div class="panelDiv" data-panel="nav01"  >
                     <!--<div class="imgTextPanel" style="height: 300px;">
                     <div class="imgUploadPanel">
                   <div class="l">
                       <p><b>图片尺寸建议</b></p>
                   <p>格式：JPG、PNG      尺寸：  大小：100KB</p>
                   </div>
                       </div>&lt;!&ndash;imgUploadPanel&ndash;&gt;
                         <div class="lineText">
                            <div class="l">
                                          <div class="imgPanel"> <img src="" data-imgcode="EditImg"/></div>
                                              <div class="uploadBtn btn" style="position: relative">
                                              <em class="upTip">上传图片</em>
                                              <div class="jsUploadBtn"  data-imgcode="EditImg"></div>
                                              </div>&lt;!&ndash;uploadBtn&ndash;&gt;
                                             &lt;!&ndash; <p class="textBtn" data-imgurl="">恢复默认图片</p>&ndash;&gt;
                            </div>
                            <div class="texList" style="width: 390px; float: right;">
                                                      <div class="commonSelectWrap">
                                                        <label class="searchInput" style="width: 390px;">
                                                          <input  name="CouponTypeName" type="text" value="" placeholder="填写分享标题">
                                                         </label>
                                                      </div>
                                                      <div class="commonSelectWrap">
                                                        <label class="searchInput" style="width: 390px; height: 100px">
                                                          <textarea  name="CouponTypeName" type="text" placeholder="填写分享摘要"></textarea>
                                                         </label>
                                                      </div>

                            </div> &lt;!&ndash;texList&ndash;&gt;
                         </div> &lt;!&ndash;l&ndash;&gt;

                </div>&lt;!&ndash;imgTextPanel&ndash;&gt;-->


                    <div class="mainPanel">
                     <div class="content">
                           <p class="title">您的品牌更适合以下哪个风格</p>
                          <div id="slider">
                          <!--<div class="slide"><img src="images/fenge01.png" /><em></em></div>
                          <div class="slide"><img src="images/fenge02.png" /><em></em></div>
                          <div class="slide"><img src="images/fenge03.png" /><em></em></div>
                          <div class="slide"><img src="images/fenge01.png" /><em></em></div>
                          <div class="slide"><img src="images/fenge02.png" /><em></em></div>-->
                          </div>
</div> <!--content-->

                   <div class="listBtn">
                    <p class="title">您希望通过活动实现</p>
                      <div>
                      <div class="btnItem disable" data-flag="HB" data-interaction="1"><p><em></em>关注</p></div>

                      <div class="btnItem disable"  data-flag="skill" data-interaction="2"><p><em></em>成交</p></div>
</div>

                    </div>

</div><!--mainPanel-->
           </div>  <!--panelDiv-->
         <div class="panelDiv" data-panel="nav02">




             <div class="content">
               <div class="commonSelectWrap" style="width:100px" >
                  <em class="tit">活动主题：</em>
                  <div class="searchInput" style="margin-left: 15px;">
                   <input  id="tempName" class="easyui-validatebox"  />
                </div> <!--inputBox-->
              </div><!--lineText-->

                <div class="example-wp "></div>
             </div>

   <div class="zsy"></div>
   </div> <!-- data-panel="nav02"-->
         <div class="panelDiv" data-panel="nav03">
             <div class="spread" style="width: 1010px;"  data-interaction="1">
               <form></form>
                <form id="setOptionForm">
                           <div class="spreadPanel">
                           <p class="title">时间设置</p>
                        <div class="commonSelectWrap" >
                            <em class="tit" style="width: 120px;">开始时间：</em>
                            <div class="selectBox" style="width: 100px;">
                                <input type="text" class="easyui-datebox" name="BeginTime" id="startDate" data-options="width:100,height:30,required:true" placeholder="请输入"/>
                            </div> <!--inputBox-->
                        </div><!--lineText-->
                        <div class="commonSelectWrap" >
                            <em class="tit" style="width: 120px;">结束时间：</em>
                            <div class="selectBox" style="width: 100px;">
                                <input type="text" class="easyui-datebox" name="EndTime"  id="endDate"  data-options="width:100,height:30,required:true" validType="compareDate[$('#startDate').datebox('getText'),'结束时间应该不能小于开始时间']" placeholder="请输入"/>
                            </div> <!--inputBox-->
                        </div><!--lineText-->
                         <p class="title">次数限制</p>
                         <div class="radioList"  >
                        <div class="radio on" data-name="HB" data-value="2" ><em></em><span>每人每日参与一次</span></div>
                        <div class="radio" data-name="HB" data-value="3"><em></em><span>每人每周参与一次</span></div>
                        <div class="radio" data-name="HB" data-value="1" data-input="true"><em></em><span >活动期间每人限参加<input class="easyui-numberbox" id="frequency" name="LotteryNum" data-options="width:60,height:30,min:0,max:100000,precision:0,disabled:true,required:true"/>次</span></div>
                        <div class="radio" data-name="HB" data-value="4" ><em></em><span>不限参加次数</span></div>
</div> <!--radioList-->

                </div> <!--spreadPanel-->
                           <div class="spreadPanel center attention" >
                            <div class="phonePanel"  data-imgcode="BackGroundImageUrl" data-type="bg" data-edit="true" data-size="640X1008px" data-imgurl="images/pageBg.png">

                               <div class="draggable" data-edit="true"  data-size="'120X120" data-imgurl="images/logo.png" data-imgcode="LogoImageUrl" ><img src="images/logo.png" width="39" data-imgcode="LogoImageUrl" height="39" ></div>

                               <div class="action"> <div><img src="images/packetClose.png"   data-imgcode="NotReceiveImageUrl"/>  <img src="images/packetOpen.png" class="hide" data-imgcode="ReceiveImageUrl" /> <em style="display: none"> 活动规则 <i>&gt;</i> </em></div> </div>

                            </div>
                </div> <!--spreadPanel-->
                           <div class="spreadPanel setSpread">
                            <div  data-tabname="tab01" data-type="Focus" >
                                   <p class="title">选择奖品</p>
                                                  <div class="tableWap HBList " style="margin-bottom: 10px;">
                                                   <div id="prize"></div>
                                                 </div>
                                                <div class="commonBtn icon icon_add w64" style="margin-left: 20px;" data-type="prize">添加奖品</div>
                            </div>


                </div>   <!--spreadPanel-->
                </form>
            </div>
             <div class="spread" data-interaction="2">
                      <div class="skillPanel" style="display: none">
                          <p  data-imgcode="ImageURL01" data-edit="true" data-size="640X370px" data-imgurl="images/skillKV.png" ><img src="images/skillKV.png" width="320" height="185"></p>
                          <div class="eventList">
                              <ul>
                                  <li class="on">3月28日<em class="editIconBtn"></em></li>
                                  <li>3月29日<em class="editIconBtn"></em></li>
                                  <li>3月27日<em class="editIconBtn"></em></li>
                                  <!--<li></li>-->
                              </ul>
                             <div class="eventAddBtn"> <em class="icon"> </em> <em class="textTip">添加秒杀</em></div>
                          </div>

                          <div class="timekeeping">
                              <div class="title"> <hr width="100%" size="1"> <p>距离抢购结束还剩</p></div>
                              <p class="time"><em>0</em><em>0</em>时<em>0</em><em>0</em>分<em>0</em><em>0</em>秒</p>
                          </div>
                          <div class="productList">
                              <div class="product">

                                  <div class="l"><img src="images/img.png" ></div>
                                    <div class=""></div>
                                   <div class="r">
                                       <p class="tit">麦斯威尔低脂三合一原味咖啡
                                           全新包装  待你品味 </p>
                                       <p class="info"><em>已有65人参加</em> <span>  &nbsp&nbsp 原价 <i>￥68</i></span></p>
                                       <p class="skill">抢购价 <em>￥38</em> </p>
                                       <div class="textBtn"> 立即抢购 </div>

                                   </div>
                                  <em class="editIconBtn"></em>
                              </div><!--product-->
                              <div class="product">

                                  <div class="l"><img src="images/img.png" ></div>
                                  <div class=""></div>
                                  <div class="r">
                                      <p class="tit">麦斯威尔低脂三合一原味咖啡
                                          全新包装  待你品味 </p>
                                      <p class="info"><em>已有65人参加</em> <span>  &nbsp&nbsp 原价 <i>￥68</i></span></p>
                                      <p class="skill">抢购价 <em>￥38</em> </p>
                                      <div class="textBtn"> 立即抢购 </div>

                                  </div>
                                  <em class="editIconBtn"></em>
                              </div><!--product-->
                          </div> <!--productList-->

                          <div class="productAdd"> <em class="icon"> 添加商品</em></div>

                      </div>  <!--skillPanel-->
             </div>
         </div> <!-- data-panel="nav03"-->
             <div class="panelDiv" data-panel="nav04">

              <div class="spread" style="width: 1010px;">
              <div class="spreadPanel">
              <p class="title">选择设置页面</p>
                   <div class="contentDiv ">
                     <div class="tabDiv on"  data-showtab="tab01">
                       <span class="l"></span>
                       <em>微信图文设置</em>
   </div> <!--tabDiv-->
                     <div class="tabDiv" data-showtab="tab02">
                       <span class="l"></span>
                       <em>微信朋友圈分享设置</em>
   </div> <!--tabDiv-->
                     <div class="tabDiv" data-showtab="tab03">
                       <span class="l"></span>
                       <em>引导关注</em>
   </div> <!--tabDiv-->
   </div> <!--contentDiv-->
   </div> <!--spreadPanel-->
              <div class="spreadPanel center attention" >
              <!--<div class="phoneTitle">商户公众号名称</div>-->
              <div class="phoneWebDiv" data-flag="微信图文" data-tabname="tab01" data-type="Reg">
             <p class="txtTitle" data-view="Title">我是一个图文标题</p>
             <div style="position: relative">

             <img src="images/tuwen.png" width="294"  height="163"  data-imgcode="picText" >
             <em class="editIconBtn" data-type="imgText"></em>
             </div>
             <span data-view="Summary" >这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图
             文素材这是一个图文素材图文素材这是一个图文素材图文素材
             这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图文素材
             这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图文素材</span>
   </div> <!--phoneWebDiv-->

    <div class="phoneWebDiv share" style="display: none" data-flag="分享设置" data-tabname="tab02" data-type="Share">
                   <img src="images/icon.png" class="shareIcon"/>
                   <em class="jt_tip"></em>
                           <div class="share">
                           <em class="editIconBtn"  data-type="shareInfo"></em>
                                <p class="txtTitle" data-view="Title">我是一个图文标题</p>

                               <div >
                                <div  class="shareImg" style="float:left"><img src="images/imgDefault.png" data-imgcode="shareImg">

                                </div>
                                 <span data-view="Summary" style="float: right; width: 120px; padding-right: 10px;" >我是一个分享的描述</span>
                                 </div>
                           </div> <!--share-->

                 </div> <!--phoneWebDiv share-->
    <div class="phoneWebDiv attention" data-flag="引导" data-tabname="tab03"  style="display: none; text-align: center" data-type="Focus">
             <div  data-edit="true"  data-size="640X1008px" data-imgurl="images/bgPhone.png" data-imgcode="bgPhone" style="height: 500px;" >
               <img src="images/bgPhone.png" class="bgPhone" data-imgcode="bgPhone" >
             </div>

              <div class="erweiMaPanel">
                <div  class="outside" style="display: none"  data-type="watch"><p data-view="PromptText" class="editText" >长按次二维码进行关注   </p> <em class="editIconBtn" data-type="text"></em></div>
                <div class="erWeiMa">
                  <div data-edit="true" data-size="100X100px" data-imgurl="images/imgDefault.png" data-default="images/imgDefault.png" data-imgcode="erWeiMa" style="width: 80px; margin: 0px auto; top: 62px; position: relative;"><em class="editIconBtn"></em>
                       <img src="images/imgDefault.png" data-imgcode="erWeiMa">
                  </div>
                      <p style="background: rgba(255, 255, 255, 0.8);position: absolute;bottom: -15px;"> 请上传您的品牌logo</p>
                  </div>
                  <div class="textBtn"  data-type="share"  style="display: none"  >分享有奖</div>
                  <div class="textBtn"  data-type="reg"  style="display: none" >注册有奖</div>
              </div>
    </div> <!--attention-->


   </div> <!--spreadPanel-->
              <div class="spreadPanel setSpread optionPrize">

                           <div  data-tabname="tab03"  style="display: none;" data-type="Share" data-msg="引导关注">
                                  <p class="title"> 微信吸粉</p>
                                  <div class="radioList" id="release" >
                                   <div class="checkBox" data-type="share" > <em></em> <span>分享有奖</span> <div class="btnList"><div class="commonBtn w50" data-option="select">查看</div> <div class="commonBtn icon icon_add w60" data-option="add"> 添加奖品</div> </div> </div>
                                   <div class="checkBox" data-type="watch"> <em></em> <span>关注有奖</span> <div class="btnList"> <div class="commonBtn w50" data-option="select">查看</div> <div class="commonBtn icon icon_add w60"  data-option="add"> 添加奖品</div></div></div>
                                   <div class="checkBox" data-type="reg"> <em></em> <span>注册有奖</span> <div class="btnList"> <div class="commonBtn w50" data-option="select">查看</div> <div class="commonBtn icon icon_add w60"  data-option="add">添加奖品</div> </div></div>
</div>

                                   <div class="tableWap expandList" style="display: none">
                                   <table id="expandGrid"></table>

</div>

                           </div>

   </div>   <!--spreadPanel-->
   </div>

   </div> <!-- data-panel="nav04"-->
         </div>
       <div class="btnopt" data-falg="nav01">
                           <div class=" commonBtn bgWhite prevStepBtn" data-flag="nav01" style="float: left; display: none">
                               上一步</div>
                           <div class=" commonBtn nextStepBtn" id="submitBtn" data-submitindex="1" data-flag="nav02" style="float:left;">下一步</div>
                       </div>
       </div>
     <div style="display: none">
         <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
         		<div class="easyui-layout" data-options="fit:true" id="panlconent">

         			<div data-options="region:'center'" style="padding:10px;">

         			</div>
         			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
         				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>

         			</div>
         		</div>

         </div>
          <div id="win1" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
                  		<div class="easyui-layout" data-options="fit:true" id="panlconent1">

                  			<div data-options="region:'center'" style="padding:10px;">

                  			</div>
                  			<div class="btnWrap" id="btnWrap1" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
                  				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>

                  			</div>
                  		</div>

                  </div>
                   <div id="winrelease" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
                          <div class="easyui-layout" data-options="fit:true" id="Div1">
                              <div data-options="region:'center'" style="padding:10px;">
                                 <div class="QRcode OnlineQRCodeId">
                                     <div class="title">线上活动</div>
                                     <div class="desc">完整版活动，活跃线上用户，提升忠诚度</div>
                                     <div class="codeimg "><img src="" /></div>
                                     <div class="downbtn "><a class="commonBtn downaddress sales icon icon_downLoad"  download="线上活动二维码" href="javascript:void(0)">下载</a></div>
                                     <div class="address"><input   type="text" class="addressinput" /><div style="position:relative;"><a  href="#none"    class="commonBtn copybtn addrcopy">复制</a></div></div>
                                 </div>
                                 <div class="QRcode OnfflineQRCodeId">
                                     <div class="title">门店应用</div>
                                     <div class="desc">精简版活动，活跃终端客户，提升好感度</div>
                                     <div class="codeimg "><img src="" /></div>
                                     <div class="downbtn "><a class="commonBtn downaddress  sales icon icon_downLoad" download="门店应用二维码" href="javascript:void(0)">下载</a></div>
                                     <div class="address"><input type="text" class="addressinput" /><div style="position:relative;"><a  href="#none"      class="commonBtn copybtn addrcopy">复制</a></div></div>
                                 </div>
                              </div>
                              <div class="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
                        				<a class="easyui-linkbutton commonBtn saveBtn release" style="background-color: #ff343f;margin-right: 12px;" >直接发布</a>
                        				<a class="easyui-linkbutton commonBtn closeBtn"  href="javascript:void(0)" onclick="javascript:$('#winrelease').window('close')" >完成</a>
                        			</div>
                          </div>
                      </div>
     </div>

         <script id="tpl_selectPrize" type="text/html">
          <div class="lineText">
                             <div class="commonSelectWrap">
                                       <em class="tit w120">选择查看类型:</em>
                                    <div class="searchInput bordernone">
                                            <input  class="easyui-combobox" id="selectType" data-options="width:160,height:32,valueField: 'label',
                                            textField: 'value',
                                            data: [{
                                              label: '0',
                                              value: '代金券',

                                           },

                                           {
                                              label: '1',
                                              value: '礼品券'
                                           }
                                           {
                                              label: '2',
                                              value: '积分'
                                           }
                                           {
                                              label: '-1',
                                              value: '选择奖品类型',
                                               selected:true
                                           }
                                           ]"  name="BatId" type="text" value="0"/>
                                    </div>
                             </div>
          </div> <!--lineText-->
        <div class="showPanel" style="display: none">
        <div class="optionBtn">
                <span class="listName"></span>
                <div class="commonBtn icon icon_add r">新增</div>
        </div><!--optionBrn-->
         <div class="tableWap">
                <div id="prizeListGrid"></div>
         </div><!--tableWap-->
        </div> <!--showPanel-->
</script>


       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"  data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>

     <script type="text/javascript">
         var version=new Date().getTime();
         var str = "<script type='text/javascript' src='http://www.vveshow.com/plugins/jvveshow/1.0.0/resource/app/jvveshow/jvveshow.min.js?versionInit='" + version + "'>";
         str+="\<\/script\>";
     document.write(str);
    </script>
</asp:Content>

