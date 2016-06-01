<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>��б�</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/bargainManage/css/style.css?v=0.6"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- �������� -->
            <div class="contentArea_vipquery">
                <!--������Ϣ��ѯ-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">����ƣ�</em>
                                                      <label class="searchInput">
                                                          <input data-text="�����" data-flag="EventName" name="EventName" type="text" placeholder="����������" value="">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">״̬��</em>
                                                      <div class="searchInput bordernone">
                                                         <input id="item_status" data-text="״̬" data-flag="Status" class="easyui-combobox" data-options="min:0,precision:2, width:198,height:32" name="EventStatus" type="text" placeholder="������״̬" value="" >
                                                      </div>
                                                  </div>
                                                  <div class="moreQueryWrap">
                                                     <a href="javascript:;" class="commonBtn queryBtn">��ѯ</a>
                                                   </div>
                                                   <div class="clear"></div>
                                                   

                                                   <div class="commonSelectWrap">
                                                      <em class="tit">��ʼʱ�䣺</em>
                                                      <div class="searchInput bordernone" style="border:1px solid #ccc;border-radius:3px;background-color:#fff;">
                                                         <input id="BeginTime"  name="BeginTime"  data-options="width:198,height:32"/>
														 <span class='date'><b></b></span>
                                                      </div>
                                                  </div>

                                                  <div class="commonSelectWrap">
                                                      <em class="tit">����ʱ�䣺</em>
                                                      <div class="searchInput bordernone" style="border:1px solid #ccc;border-radius:3px;background-color:#fff;">
                                                         <input id="EndTime" name="EndTime" data-options="width:198,height:32"  />
														 <span class='date'><b></b></span>
                                                      </div>
                                                  </div>
                           
                              
                              </div>


                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">��Ա��ѯ</h2>-->

                </div>
                <div class="tableWrap" id="tableWrap">
                  <div class="optionBtn" id="opt">
                    <div class="commonBtn r sales" data-flag="add" id="sales"> <img src="images/add.png"  >��������</div>


                  </div>
                <div class="cursorDef">
                   <div  id="gridTable" class="gridLoading">
                         <div  class="loading">
                                  <span>
                                <img src="../static/images/loading.gif"></span>
                           </div>
                   </div>
                   </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >û�з��������Ĳ�ѯ��¼</div>
                        <div id="kkpager" >
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				ָ����ģ���������
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >ȷ��</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >ȡ��</a>
      			</div>
      		</div>

      	</div>
      </div>
      <!--��Ʒ������Ϣ�༭-->
      <div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
      <div id="goodsBasic_exit"  class="jui-dialog jui-dialog-addGoogs" style="display:none;">
        
          <div class="jui-dialog-tit">
            <h2>��ӿ���</h2>
              <a href="javascript:;" class="jui-dialog-close hintClose"></a>
          </div>
          <div class="optionclass">
             
            <div class="title">�����:</div>
            <div class="borderNone" >
             <input id="campaignName" data-options="width:260,height:34,min:1,precision:0,max:10000" name="IssuedQty" style="width:260px" placeholder="��ѡ������" />
            </div>
          </div>
          <div class="optionclass">
            <div class="title">��ʼʱ��:</div>
            <div class="searchInput bordernone">
              <input id="campaignBegin"  name="order_date_begin" style="width:260px" placeholder="��ѡ����ʼʱ��" />
            </div>
            
          </div>
          <div class="optionclass">
            <div class="title">����ʱ��:</div>
            <div class="searchInput bordernone">
              <input id="campaignEnd" name="order_date_end" style="width:260px" placeholder="��ѡ������ʱ��" />
            </div>
            
          </div>
          <div class="btnWrap">
              <a href="javascript:;" id="saveCampaign" class="commonBtn saveBtn">�ύ</a>
              <a href="javascript:;" class="commonBtn cancelBtn hintClose">ȡ��</a>
          </div>
        
        
      </div>

</asp:Content>