using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;
using System.Data;

namespace JIT.CPOS.BS.Web.Module.Vip.VipSearch.Handler
{
    /// <summary>
    /// VipHandler
    /// </summary>
    public class VipHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "search_vip":
                    content = GetVipListData();
                    break;
                case "get_vip_by_id":
                    content = GetVipInfoById();
                    break;
                case "GetShowCount"://获取数据
                    content = GetShowCount();
                    break;
                case "GetVipIntegralDetail":
                    content = GetVipIntegralDetailData();
                    break;
                case "GetPosOrderList":
                    content = GetPosOrderListData();
                    break;
                case "GetNextLevelUserList":
                    content = GetNextLevelUserListData();
                    break;
                case "GetCollectionPropertyList":
                    content = GetCollectionPropertyList();
                    break;
                case "GetVipTags":
                    content = GetVipTagsData();
                    break;
                case "tags_delete":
                    content = TagsDeleteData();
                    break;
                case "tags_save":
                    content = SaveTags();
                    break;
                case "changeIntegral_save":
                    content = SaveChangeIntegral();
                    break;
                case "search_IntegralRule":
                    content = GetIntegralRuleListData();
                    break;
                case "get_IntegralRule_by_id":
                    content = GetIntegralRuleInfoById();
                    break;
                case "IntegralRule_save":
                    content = SaveIntegralRule();
                    break;
                case "IntegralRule_delete":
                    content = IntegralRuleDeleteData();
                    break;
                case "search_VipReward":
                    content = GetVipRewardData();
                    break;
                case "search_SalesReward":
                    content = GetSalesRewardData();
                    break;
                case "search_UnitReward":
                    content = GetUnitRewardData();
                    break;
                case "GetIntegralByVip":  //获取积分明细
                    content = GetIntegralByVip();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        #region GetCollectionPropertyList
        private string GetCollectionPropertyList()
        {
            var service = new VIPCollectionDataBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }

            var queryEntity = new VIPCollectionDatEntity();
            queryEntity.VIPID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetCollectionPropertyList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetCollectionPropertyListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetVipListData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetVipListData()
        {
            var form = Request("form").DeserializeJSONTo<VipQueryEntity>();

            var service = new VipBLL(CurrentUserInfo);
            VipEntity listObj = null;
            string content = string.Empty;

            VipSearchEntity queryEntity = new VipSearchEntity();
            queryEntity.Page = Utils.GetIntVal(Request("page"));
            queryEntity.PageSize = PageSize;
            queryEntity.VipInfo = FormatParamValue(form.VipInfo);
            queryEntity.Phone = FormatParamValue(form.Phone);
            queryEntity.UnitId = FormatParamValue(Request("UnitId"));
            queryEntity.MembershipShopId = FormatParamValue(Request("MembershipShopId"));

            queryEntity.VipSourceId = FormatParamValue(form.VipSourceId);
            if (form.VipSourceId == null)
            {
                queryEntity.VipSourceId = FormatParamValue(Request("VipSourceId"));
            }

            queryEntity.Status = Utils.GetIntVal(FormatParamValue(form.Status));
            queryEntity.VipLevel = Utils.GetIntVal(FormatParamValue(form.VipLevel));
            queryEntity.RegistrationDateBegin = FormatParamValue(form.RegistrationDateBegin);
            queryEntity.RegistrationDateEnd = FormatParamValue(form.RegistrationDateEnd);
            queryEntity.RecentlySalesDateBegin = FormatParamValue(form.RecentlySalesDateBegin);
            queryEntity.RecentlySalesDateEnd = FormatParamValue(form.RecentlySalesDateEnd);
            queryEntity.IntegrationBegin = Utils.GetIntVal(FormatParamValue(form.IntegrationBegin));
            queryEntity.IntegrationEnd = Utils.GetIntVal(FormatParamValue(form.IntegrationEnd));
            queryEntity.UserId = CurrentUserInfo.CurrentUser.User_Id;
            queryEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;

            //Jermyn20130801
            if (Request("tags") != "") // 标签及组合关系
            {
                queryEntity.Tags = FormatParamValue(Request("tags"));
            }

            listObj = service.SearchVipInfo(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = listObj.ICount.ToString();
            jsonData.data = listObj.vipInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetVipInfoById
        /// <summary>
        /// 获取Vip信息
        /// </summary>
        public string GetVipInfoById()
        {
            var service = new VipBLL(CurrentUserInfo);
            VipEntity obj = new VipEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }

            obj = service.GetVipDetailByVipID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region GetShowCount
        public string GetShowCount()
        {
            string Timestamp = Request("Timestamp").ToString().Trim();
            if (Timestamp == null || Timestamp.Equals(""))
            {
                Timestamp = "0";
            }
            string content = string.Empty;
            VipBLL vipService = new VipBLL(CurrentUserInfo);
            var respData = new GetShowCount2RespData();
            try
            {
                respData.Code = "200";
                respData.Description = "操作成功";
                //respData.count = vipService.GetShowCount(Convert.ToInt64(Timestamp), out respData.NewTimestamp);
                var info = vipService.GetShowCount2(CurrentUserInfo.UserID,CurrentUserInfo.ClientID);
                if (info != null && info.Tables.Count>0)
                {
                    respData.Content = info.Tables[0];
                }
            }
            catch (Exception ex)
            {
                respData.Code = "201";
                respData.Description = "操作失败";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }


        public class GetShowCount2RespData
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public string Exception { get; set; }
            public DataTable Content { get; set; }
        }
        public class GetShowCountRespData
        {
            public string Code;
            public string Description;
            public string Exception = null;
            public JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity Content;
            public int count;
            public long NewTimestamp;
        }

        #endregion

        #region GetVipIntegralDetailData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipIntegralDetailData()
        {
            var service = new VipIntegralDetailBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }

            var queryEntity = new VipIntegralDetailEntity();
            queryEntity.VIPID = key;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetVipIntegralDetailList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetVipIntegralDetailListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetPosOrderListData
        /// <summary>
        /// 
        /// </summary>
        public string GetPosOrderListData()
        {
            var service = new PosService(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var inoutService = new InoutService(CurrentUserInfo);
            InoutDetailInfo data;

            int maxRowCount = PageSize;
            //int startRowIndex = Utils.GetIntVal(Request("start"));
            int startRowIndex = pageIndex * PageSize + 1;

            OrderSearchInfo queryInfo = new OrderSearchInfo();
            queryInfo.vip_no = key;
            queryInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
            queryInfo.order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
            queryInfo.red_flag = "1";
            queryInfo.StartRow = startRowIndex;
            queryInfo.EndRow = startRowIndex + maxRowCount;
            data = inoutService.SearchInoutDetailInfo(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.InoutDetailList.ToJSON(),
               data.ICount);

            return content;
        }
        #endregion

        #region GetNextLevelUserListData
        /// <summary>
        /// 
        /// </summary>
        public string GetNextLevelUserListData()
        {
            var service = new VipBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            var queryEntity = new VipSearchEntity();
            queryEntity.HigherVipId = key;
            queryEntity.Page = pageIndex;
            queryEntity.PageSize = PageSize;

            //var data = service.SearchVipInfo(queryEntity);

            //content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
            //    data.vipInfoList.ToJSON(),
            //    data.ICount);
            return content;
        }
        #endregion

        #region GetNextLevelUserListData
        /// <summary>
        /// 
        /// </summary>
        public string GetIntegralByVip()
        {
            var service = new VipBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            var queryEntity = new VipSearchEntity();
            queryEntity.HigherVipId = key;
            queryEntity.Page = pageIndex;
            queryEntity.PageSize = PageSize;

            var data = service.GetIntegralByVip(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.vipInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetVipTagsData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipTagsData()
        {
            var service = new VipBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("vip_id") != null && Request("vip_id") != string.Empty)
            {
                key = Request("vip_id").ToString().Trim();
            }

            //var queryEntity = new TagsEntity();
            //queryEntity.VIPID = key;
            //int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetVipMappingTags(key);
            var dataTotalCount = data.Count;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region TagsDeleteData

        /// <summary>
        /// 删除
        /// </summary>
        public string TagsDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new VipTagsMappingBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveTags

        /// <summary>
        /// 
        /// </summary>
        public string SaveTags()
        {
            var service = new VipTagsMappingBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string MappingId = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("MappingId")) != null && FormatParamValue(Request("MappingId")) != string.Empty)
            {
                MappingId = FormatParamValue(Request("MappingId")).ToString().Trim();
            }

            var tagsEntity = key.DeserializeJSONTo<VipTagsMappingEntity>();

            if (tagsEntity.TagsId == null || tagsEntity.TagsId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "TagsId不能为空";
                return responseData.ToJSON();
            }
            if (tagsEntity.VipId == null || tagsEntity.VipId.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "VipId不能为空";
                return responseData.ToJSON();
            }

            if (MappingId == null || MappingId.Trim().Length == 0)
            {
                tagsEntity.MappingId = Utils.NewGuid();
                tagsEntity.CreateBy = CurrentUserInfo.CurrentUser.User_Id;
                tagsEntity.CreateTime = DateTime.Now;
                tagsEntity.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
                tagsEntity.LastUpdateTime = DateTime.Now;
                service.Create(tagsEntity);
            }
            else
            {
                tagsEntity.MappingId = MappingId;
                tagsEntity.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
                tagsEntity.LastUpdateTime = DateTime.Now;
                service.Update(tagsEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveChangeIntegral

        /// <summary>
        /// 
        /// </summary>
        public string SaveChangeIntegral()
        {
            var vipIntegralDetailBLL = new VipIntegralDetailBLL(this.CurrentUserInfo);
            var vipIntegralBLL = new VipIntegralBLL(this.CurrentUserInfo);
            var vipBLL = new VipBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }

            var vipIntegralDetailEntity = key.DeserializeJSONTo<VipIntegralDetailEntity>();

            if (vipIntegralDetailEntity.Integral == null || vipIntegralDetailEntity.Integral == 0)
            {
                responseData.success = false;
                responseData.msg = "积分不能为空或0";
                return responseData.ToJSON();
            }
            if (vipIntegralDetailEntity.Remark == null || vipIntegralDetailEntity.Remark.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "变动原因不能为空";
                return responseData.ToJSON();
            }

            //[IntegralSourceID]: 11, 人工调整
            vipIntegralBLL.ProcessPoint(11, this.CurrentUserInfo.ClientID, vipIntegralDetailEntity.VIPID, this.CurrentUserInfo.UserID, null, null, vipIntegralDetailEntity.Integral.Value, vipIntegralDetailEntity.Remark, this.CurrentUserInfo.UserID);

            //vipIntegralDetailEntity.VipIntegralDetailID = Utils.NewGuid();
            //vipIntegralDetailEntity.EffectiveDate = DateTime.Now;
            //vipIntegralDetailEntity.CreateBy = CurrentUserInfo.CurrentUser.User_Id;
            //vipIntegralDetailEntity.CreateTime = DateTime.Now;
            //vipIntegralDetailEntity.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
            //vipIntegralDetailEntity.LastUpdateTime = DateTime.Now;
            //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);


            //var vipList = vipBLL.QueryByEntity(new VipEntity()
            //{
            //    VIPID = vipIntegralDetailEntity.VIPID
            //}, null);
            //VipEntity vipObj = vipList[0];

            //var vipIntegralList = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() {
            //    VipID = vipIntegralDetailEntity.VIPID
            //}, null);
            //VipIntegralEntity vipIntegralObj = null;
            //if (vipIntegralList != null && vipIntegralList.Length > 0)
            //{
            //    vipIntegralObj = vipIntegralList[0];
            //    if (vipIntegralDetailEntity.Integral > 0)
            //    {
            //        vipIntegralObj.InIntegral = vipIntegralDetailEntity.Integral;
            //        vipIntegralObj.ValidIntegral += vipIntegralDetailEntity.Integral;
            //        vipIntegralObj.EndIntegral = vipIntegralObj.ValidIntegral;
            //    }
            //    else if (vipIntegralDetailEntity.Integral < 0)
            //    {
            //        vipIntegralObj.OutIntegral = vipIntegralDetailEntity.Integral;
            //        vipIntegralObj.ValidIntegral += vipIntegralDetailEntity.Integral;
            //        vipIntegralObj.EndIntegral = vipIntegralObj.ValidIntegral;
            //    }
            //    vipIntegralBLL.Update(vipIntegralObj, false);
            //}
            //else
            //{
            //    vipIntegralBLL.Create(new VipIntegralEntity()
            //    {
            //        VipID = vipIntegralDetailEntity.VIPID,
            //        BeginIntegral = 0,
            //        InIntegral = vipIntegralDetailEntity.Integral > 0 ? vipIntegralDetailEntity.Integral : 0,
            //        OutIntegral = vipIntegralDetailEntity.Integral < 0 ? vipIntegralDetailEntity.Integral : 0,
            //        EndIntegral = vipIntegralDetailEntity.Integral,
            //        InvalidIntegral = 0,
            //        ValidIntegral = vipIntegralDetailEntity.Integral
            //    });
            //}

            //vipObj.Integration = vipIntegralObj.EndIntegral;
            //vipBLL.Update(vipObj, false);


            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetIntegralRuleListData
        /// <summary>
        /// 获取波段列表
        /// </summary>
        public string GetIntegralRuleListData()
        {
            var form = Request("form").DeserializeJSONTo<IntegralRuleEntity>();
            var service = new IntegralRuleBLL(CurrentUserInfo);
            string content = string.Empty;

            //string key = string.Empty;
            //if (Request("IntegralSourceID") != null && Request("IntegralSourceID") != string.Empty)
            //{
            //    key = Request("IntegralSourceID").ToString().Trim();
            //}

            var queryEntity = new IntegralRuleEntity();
            queryEntity.IntegralSourceID = form.IntegralSourceID;
            queryEntity.Integral = form.Integral;
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var data = service.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = service.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetIntegralRuleInfoById
        /// <summary>
        /// 获取信息
        /// </summary>
        public string GetIntegralRuleInfoById()
        {
            var service = new IntegralRuleBLL(CurrentUserInfo);
            IntegralRuleEntity obj = new IntegralRuleEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("IntegralRuleID") != null && Request("IntegralRuleID") != string.Empty)
            {
                key = Request("IntegralRuleID").ToString().Trim();
            }

            obj = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveIntegralRule

        /// <summary>
        /// 
        /// </summary>
        public string SaveIntegralRule()
        {
            var integralRuleBLL = new IntegralRuleBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }

            var integralRuleEntity = key.DeserializeJSONTo<IntegralRuleEntity>();
            if (integralRuleEntity.BeginDateStr != null && integralRuleEntity.BeginDateStr.Length > 0)
                integralRuleEntity.BeginDate = Convert.ToDateTime(integralRuleEntity.BeginDateStr);
            if (integralRuleEntity.EndDateStr != null && integralRuleEntity.EndDateStr.Length > 0)
                integralRuleEntity.EndDate = Convert.ToDateTime(integralRuleEntity.EndDateStr);

            if (integralRuleEntity.Integral == null || integralRuleEntity.Integral.Length == 0)
            {
                responseData.success = false;
                responseData.msg = "积分公式不能为空";
                return responseData.ToJSON();
            }

            if (integralRuleEntity.IntegralRuleID == null || integralRuleEntity.IntegralRuleID.Trim().Length == 0)
            {
                integralRuleEntity.IntegralRuleID = Utils.NewGuid();
                integralRuleEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                integralRuleBLL.Create(integralRuleEntity);
            }
            else
            {
                integralRuleBLL.Update(integralRuleEntity, null);
            }


            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region IntegralRuleDeleteData

        /// <summary>
        /// 删除
        /// </summary>
        public string IntegralRuleDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new IntegralRuleBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetVipRewardData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetVipRewardData()
        {
            string error = string.Empty;
            var service = new VipBLL(CurrentUserInfo);
            VipEntity listObj = null;
            string content = string.Empty;

            VipEntity queryEntity = new VipEntity();
            queryEntity.Page = Utils.GetIntVal(Request("page"));
            queryEntity.PageSize = PageSize;
            queryEntity.VipName = FormatParamValue(Request("VipInfo"));
            queryEntity.UnitId = FormatParamValue(Request("UnitId"));
            queryEntity.IntegralSourceIds = FormatParamValue(Request("IntegralSourceIds"));
            queryEntity.BeginDate = FormatParamValue(Request("BeginDate"));
            queryEntity.EndDate = FormatParamValue(Request("EndDate"));

            if (queryEntity.IntegralSourceIds.StartsWith(","))
                queryEntity.IntegralSourceIds = queryEntity.IntegralSourceIds.Substring(1);

            listObj = service.GetVipIntegral(queryEntity, out error);

            var jsonData = new JsonData();
            jsonData.totalCount = listObj.ICount.ToString();
            jsonData.data = listObj.vipInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetSalesRewardData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetSalesRewardData()
        {
            string error = string.Empty;
            var service = new VipBLL(CurrentUserInfo);
            VipEntity listObj = null;
            string content = string.Empty;

            VipEntity queryEntity = new VipEntity();
            queryEntity.Page = Utils.GetIntVal(Request("page"));
            queryEntity.PageSize = PageSize;
            queryEntity.UserName = FormatParamValue(Request("VipInfo"));
            queryEntity.UnitId = FormatParamValue(Request("UnitId"));
            queryEntity.IntegralSourceIds = FormatParamValue(Request("IntegralSourceIds"));
            queryEntity.BeginDate = FormatParamValue(Request("BeginDate"));
            queryEntity.EndDate = FormatParamValue(Request("EndDate"));

            if (queryEntity.IntegralSourceIds.StartsWith(","))
                queryEntity.IntegralSourceIds = queryEntity.IntegralSourceIds.Substring(1);

            listObj = service.GetPurchasingGuideIntegral(queryEntity, out error);

            var jsonData = new JsonData();
            jsonData.totalCount = listObj.ICount.ToString();
            jsonData.data = listObj.vipInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetUnitRewardData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetUnitRewardData()
        {
            string error = string.Empty;
            var service = new VipBLL(CurrentUserInfo);
            VipEntity listObj = null;
            string content = string.Empty;

            VipEntity queryEntity = new VipEntity();
            queryEntity.Page = Utils.GetIntVal(Request("page"));
            queryEntity.PageSize = PageSize;
            //queryEntity.VipInfo = FormatParamValue(form.VipInfo);
            queryEntity.UnitId = FormatParamValue(Request("UnitId"));
            queryEntity.IntegralSourceIds = FormatParamValue(Request("IntegralSourceIds"));
            queryEntity.BeginDate = FormatParamValue(Request("BeginDate"));
            queryEntity.EndDate = FormatParamValue(Request("EndDate"));

            if (queryEntity.IntegralSourceIds.StartsWith(","))
                queryEntity.IntegralSourceIds = queryEntity.IntegralSourceIds.Substring(1);

            listObj = service.GetUnitIntegral(queryEntity, out error);

            var jsonData = new JsonData();
            jsonData.totalCount = listObj.ICount.ToString();
            jsonData.data = listObj.vipInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class VipQueryEntity
    {
        public string VipInfo;
        public string Phone;
        public string UnitId;
        public string VipSourceId;
        public string Status;
        public string VipLevel;
        public string RegistrationDateBegin;
        public string RegistrationDateEnd;
        public string RecentlySalesDateBegin;
        public string RecentlySalesDateEnd;
        public string IntegrationBegin;
        public string IntegrationEnd;
        public string UserId;
        public string CustomerId;
    }
    #endregion

    #region
    public class RespData
    {
        public string Code;
        public string Description;
        public string Exception = null;
        public string Data;
        public int count;
        public long NewTimestamp;
    }
    #endregion

}