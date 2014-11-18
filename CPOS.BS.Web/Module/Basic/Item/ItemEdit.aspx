
<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/ChildPage.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.Module.Basic.Item.ItemEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>商品信息</title>



   
    <script type="text/javascript" src="<%=StaticUrl+"/module/static/js/lib/tools-lib.js?v=1.2"%>"></script>
    <script type="text/javascript"  src="<%=StaticUrl+"/module/static/js/lib/bdTemplate.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/module/static/js/plugin/jquery.jqpagination.js"%>"></script>
    <script type="text/javascript" src="<%=StaticUrl+"/module/static/js/plugin/jquery.drag.js"%>"></script>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()"%>" />
    <link href="<%=StaticUrl+"/module/basic/Item/css/keywords.css?v=Math.random()"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/static/css/pagination.css"%>" rel="stylesheet" />
        <style type="text/css">
            .tr-Image {
                height: 150px;
                margin-top: 2em;
            }

            .imagetit {
                float: left;
                width: 110px;
                line-height: 16px;
                margin-top: 1em;
            }

            .uploadWarp {
                float: left;
                margin-top: 30px;
            }

            .viewImage {
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

            .info {
                float: left;
                width: 200px;
            }

            .exp {
                line-height: 28px;
                font-size: 15px;
                color: #828282;
            }

            .uplaodbtn {
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
                text-align: center;
                border-radius: 3px;
            }

            .upbtn {
                margin-top: 15px;
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

            #btnAddDisplay {
                margin:13px auto 3px 10px;
            }
            .itemCateTip {
                position:absolute;
                left:-9px;
                top:65px;
                color:red;
                width:500px;
            }
            .absolute {
                position:absolute;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="section" style="height: 520px; min-height: 520px;">
        <div class="m10 article">
            <div style="width: 100%; padding: 0px; border: 0px solid #d0d0d0;">
                <div id="tabsMain" style="width: 100%; height: 400px;">
                </div>
                <div id="tabInfo" style="height: 400px; background: rgb(241, 242, 245);">
                    <div class="z_detail_tb">
                        <div style="height: 5px;">
                        </div>
                        <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td">
                                    <font color="red">*</font>商品类别                                    
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtItemCategory" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    <div style="display:none">商品标签</div>
                                </td>
                                <td class="z_main_tb_td2" colspan="5">
                                    <div id="txtItemCategoryMapping" style="float: left; margin-top: 5px;">
                                    </div>
                                    <div id="btnItemCategoryMapping" style="float: left; margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td ">
                                    <font color="red">*</font>商品名称
                                    <span class="itemCateTip">
                                        如：苹果（Apple）MackBook Air MD760CH/B 13.3英寸宽屏笔记本电脑
                                    </span>
                                </td>
                                <td class="z_main_tb_td2" colspan="3">
                                    <div id="txtItemName" style="margin-top: 5px;" >

                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    商品编码
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtItemCode" style="margin-top: 5px;">
                                    </div>
                                    <%--隐藏--%>
                                     <div style="display:none">拼音助记码</div> 
                                    <div id="txtPyzjm" style="margin-top: 5px;">
                                    </div>
                                </td>
                             <%--   <td class="z_main_tb_td">
                                   
                                </td>
                                <td class="z_main_tb_td2">
                                   
                                </td>--%>
                            </tr>
                            <%--                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td">上传图片</td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align:top; line-height:32px; padding-top:0px;">
                                    <div id="txtImageUrl" style="margin-top:5px; float:left;"></div>
                                    <div  style="margin-top:5px; float:left;">
                                        <input type="button" id="uploadImage" value=" 选择图片 " />
                                    </div>
                                </td>
                            </tr>--%>
                            <tr class="z_main_tb_tr collapse" style="display:none">
                                <td class="z_main_tb_td">
                                    商品简称
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtItemNameShort" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    英文名
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtItemEnglish" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr">
                                <td class="z_main_tb_td">
                                    商品简介
                                </td>
                                <td class="z_main_tb_td2" colspan="5" style="vertical-align: top; line-height: 32px;
height:150px; 
                                   ">
                                    <div id="txtRemark" style="margin-top:20px;">
                                    </div>
                                   <div style="color:red;">仅用于商品列表页面的大图模式中显示</div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr" style="display: none;">
                                <td class="z_main_tb_td" colspan="6">
                                    <div style="float: left;">
                                        更多属性</div>
                                    <div class="collapseHeader" style="cursor: pointer; font-weight: bold; float: right;">
                                        点击展开↓</div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr collapse" style="display: none;">
                                <td class="z_main_tb_td">
                                    <font color="red">*</font>是否赠品
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtIfgifts" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    <font color="red">*</font>是否常用
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtIfoften" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    <font color="red">*</font>是否服务型
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtIfservice" style="margin-top: 5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr class="z_main_tb_tr collapse" style="display: none;">
                                <td class="z_main_tb_td">
                                    <font color="red">*</font>是否散货
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtIsGB" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                    散货排序
                                </td>
                                <td class="z_main_tb_td2">
                                    <div id="txtDisplayIndex" style="margin-top: 5px;">
                                    </div>
                                </td>
                                <td class="z_main_tb_td">
                                </td>
                                <td class="z_main_tb_td2">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="tabProp" style="height: 0px; overflow: auto;">   <%--高度设为0--%>
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlItemProp" style="overflow: auto;">
                            <%=JIT.CPOS.BS.Web.Framework.WebControl.PropHelper.PropHelperSingleton.CreationPropGroup("ITEM") %>
                        </div>
                    </div>
                </div>
                <div id="tabPrice" style="height: 1px; overflow: auto;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlPrice" style="height: 415px; overflow: auto;">
                            <div style="width: 400px; float: left; padding-top: 5px;">
                                <div id="gridPrice">
                                </div>
                            </div>
                            <div style="width: 250px; padding-left: 50px; padding-right: 10px; float: left; display: none;">
                                <div style="height: 5px;">
                                </div>
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td colspan="6">
                                            <div style="float: left; width: 250px; line-height: 32px; margin-left: 0px;">
                                                <div style="width: 250px; line-height: 22px; margin-left: 0px; padding-right: 0px;
                                                    text-align: left;">
                                                    <div style="float: left; width: 110px; text-align: right; padding-right: 10px;">
                                                        <font color="red">*</font>价格类型
                                                    </div>
                                                    <div id="txtItemPrice_TypeList">
                                                    </div>
                                                </div>
                                                <div style="clear: both; width: 250px; line-height: 22px; margin-left: 0px; padding-right: 0px;
                                                    text-align: left;">
                                                    <div style="float: left; width: 110px; text-align: right; padding-right: 10px;">
                                                        <font color="red">*</font>价格
                                                    </div>
                                                    <div id="txtItemPrice_Price">
                                                    </div>
                                                </div>
                                                <div style="width: 80px; line-height: 32px; margin-left: 100px;">
                                                    <div id="btnAddItemPrice">
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabSku" style="height: 1px; overflow: scroll;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                     <%--    <div style="height:200px;width:200px;">afadsfadsfasfsdfasdfsadfa</div>--%>
                    <%--    <div style="height: 5px;">
                        </div>--%>
                 <%--       <input type="button" value="添 加" id="btnAddDisplay"/>--%>
                        <div id="btnAddDisplay"></div>
                        <div style="overflow:auto" >
                            <div style="height: 5px;">
                            </div>
                            <table class="z_main_tb" style="display:none" id="z_sku_tb">  <%--默认先隐藏--%>
                                <tr style="line-height: 30px;">
                                    <td width='25%' colspan="4" style="vertical-align: top;">
                                        <div style="width: 80px; float: left;">
                                            <font color="red">*</font>条码
                                        </div>
                                        <div id="txtBarcode" style="float: left; margin-top: 5px;">
                                        </div>
                                       
                                    </td>
                                </tr>
                              <%-- 动态加载sku信息--%>
                                <%=JIT.CPOS.BS.Web.Framework.WebControl.PropHelper.PropHelperSingleton.CreationSkuProp("SKU") %>
                                <tr style="line-height: 30px;">
                                    <td class="b_bg" colspan="4">
                                        <div id="btnAddItemSku" style="float: left;">
                                        </div>
                                            <div id="btnCancelItemSku" style="float: left;">
                                        </div>
                                        <div style="color:red;float: left; margin-left:10px;  ">人人销售价不为0时，商品做为人人销售类型商品</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="b_bg" colspan="4">
                                        <div>
                                            &nbsp;</div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="pnlSku" style="height: 100%; overflow: auto;">
                            <div style="width: 660px; padding-left: 10px; padding-right: 10px;">
                             <%--   //绑定tbTableSku数据的代码在Framework下的javascript下的other里的cpos文件里--%>
                                <table id="tbTableSku" cellpadding="0" cellspacing="1" border="0" class="ss_jg" style="width: 100%;">
                                    <tr class="b_c3">
                                        <th scope="col" align="center" width="100">
                                            操作
                                        </th>
                                        <%
                                            if (this.SKUExist)
                                            {%><script type="text/javascript">                                                   var SKUExists = true;</script><%}
                                            else
                                            {%><script type="text/javascript">                                                   var SKUExists = false;</script><%}

                                            if (this.SkuProInfos != null && this.SkuProInfos.Count != 0)
                                            {
                                                foreach (var item in SkuProInfos.OrderBy(s => s.display_index))
                                                {
                                                    if (item != null)
                                                    {
                                            %>
                                        <th width="90" scope="col" align='center'>
                                            <%=item.prop_name%>
                                        </th>
                                        <%
                                                    }
                                                }
                                            }
                                            else
                                            {
                                        %><script type="text/javascript">
                                              if (SKUExists) {
                                                  alert("CustomerBasicSetting中SKUExist被设置为1，但未设置任何SKU属性，默认显示商品价格。");
                                              }
                                              var SKUExists = false;
                                            </script>
                                        <%
                                            }
                                        %>
                                        <th scope="col" align="center" width="90">
                                            条码      <%--表头--%>
                                        </th>
                                 <%--       价格的信息--%>
                                        <% if (ItemPriceTypeInfos != null)             
                                           {
                                               foreach (var info in ItemPriceTypeInfos)
                                               {%>
                                        <th width="90" scope="col" align='center'>
                                            <%=info.item_price_type_name%>
                                        </th>
                                        <%}
                                           }%>
                                    </tr>
                                </table>
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
                                <div id="gridImage" class=".x-grid-cell-inner {white-space: normal;}">
                                </div>
                            </div>
                            <div style="width: 400px; padding-left: 50px; padding-right: 10px; float: left;">
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
                                            <font color="red">*建议上传尺寸为640*640</font>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                          <div style="display:none">  标题</div>
                                        </td>
                                        <td colspan="5">
                                            <div id="txtImage_Title" style="margin-top: 10px;display:none">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                              <div style="display:none"> 说明</div>
                                        </td>
                                        <td colspan="5">
                                            <div id="txtImage_Description" style="margin-top: 10px;display:none">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="5">
                                            <div>
                                                <div id="btnAddImageUrl" style="float: left;">
                                                </div>
                                                <div id="btnAddImageUrlClear" style="float: left;">
                                                </div>
                                            </div>
                                            <div style="clear: both; color:red;">
                                                注：排序为1的图片将展示在首页</div>
                                        </td>
                                    </tr>
                                </table>
                                <div id="pnlSkuImage" style="display: none; position: absolute; left: 250px; top: 100px;
                                    width: 400px; height: 250px; border: 1px solid #666; background: rgb(241, 242, 245);
                                    z-index: 20000; overflow: scroll">
                                    <div style="height: 24px; font-weight: bold; line-height: 24px;">
                                        <div style="float: left; width: 100px; padding-left: 4px; height: 24px;">
                                            选择SKU</div>
                                        <div style="float: right; width: 30px; cursor: pointer;" onclick="fnCloseSku()">
                                            关闭</div>
                                    </div>
                                    <div id="skuList" style="clear: both;">
                                    </div>
                                    <div style="padding-top: 10px;">
                                        <div id="btnSelectSku">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabUnit" style="height: 1px; overflow: hidden;">
                    <div style="width: 100%; padding-left: 10px; padding-right: 10px;">
                        <div style="height: 5px;">
                        </div>
                        <div id="pnlUnit" style="height: 415px; overflow: auto;">
                            <div style="width: 400px; float: left; padding-top: 5px;">
                                <div id="gridUnit">
                                </div>
                            </div>
                            <div style="width: 250px; padding-left: 50px; padding-right: 10px; float: left;">
                                <div style="height: 5px;">
                                </div>
                                <table class="z_main_tb">
                                    <tr class="z_main_tb_tr">
                                        <td colspan="6">
                                            <div style="float: left; width: 250px; line-height: 32px; margin-left: 0px;">
                                                <div style="float: ; width: 250px; line-height: 22px; margin-left: 0px; padding-right: 0px;
                                                    text-align: left;">
                                                    <div style="float: left; width: 110px; text-align: right; padding-right: 10px;">
                                                        <font color="red">*</font>门店
                                                    </div>
                                                    <div id="txtItemUnit_List">
                                                    </div>
                                                </div>
                                                <div style="width: 80px; line-height: 32px; margin-left: 100px;">
                                                    <div id="btnAddItemUnit">
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <%--二维码--%>
                      <div id="tabQcode"  style="height: 1px; overflow: hidden;">
                     <table class="z_main_tb">
                            <tr class="z_main_tb_tr">
                                <td>
                                  <div class="uploadImageItem">
                                        <span class="imagetit">&nbsp&nbsp&nbsp&nbsp
                                             二维码图片预览</span>
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
                                           <select id="Valueselect" style="width: 320px;padding: 8px 0px 8px 5px;border: 1px solid #cecedc;">
                                                <option selected="selected" value="1" >文本</option>
                                                <option value="3" >图文</option>
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

    <script src="/Framework/javascript/Biz/ItemCategorySelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/UnitSelectTree.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/Status.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/YesNoStatus.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/ItemPriceType.js" type="text/javascript"></script>
    <script src="/Framework/javascript/Biz/SkuPropCfg.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <!--<script charset="utf-8" type="text/javascript" src="/Framework/Javascript/Other/kindeditor/examples/jquery.js"></script>-->
    <script src="/Framework/Javascript/Other/editor/kindeditor.js" type="text/javascript"></script>
   <%-- v=0.2起到缓存的目的--%>
    <script src="View/ItemEditView.js?v=0.51" type="text/javascript"></script> 
    <script src="Model/ItemVM.js?v=0.51" type="text/javascript"></script>
    <script src="Model/ItemDetailVM.js?v=0.51" type="text/javascript"></script>
    <script src="Store/ItemVMStore.js?v=0.51" type="text/javascript"></script>
    <script src="Store/ItemEditVMStore.js?v=0.51" type="text/javascript"></script>
    <script src="Controller/ItemEditCtl.js?v=0.58" type="text/javascript"></script>
    <style type="text/css">
        .uploadImageUrl {
            padding-left: 30px;
        }
    </style>
</asp:Content>
