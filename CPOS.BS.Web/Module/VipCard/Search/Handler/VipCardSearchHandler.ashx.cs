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

namespace JIT.CPOS.BS.Web.Module.VipCard.Search.Handler
{
    /// <summary>
    /// VipCardSearchHandler
    /// </summary>
    public class VipCardSearchHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                    content = GetVipData();
                    break;
                case "GetVipCardSales":
                    content = GetVipCardSalesListData();
                    break;
                case "GetVipCardRechargeRecord":
                    content = GetVipCardRechargeRecordListData();
                    break;
                case "GetVipCardGradeChangeLog":
                    content = GetVipCardGradeChangeLogListData();
                    break;
                case "GetVipCardStatusChangeLog":
                    content = GetVipCardStatusChangeLogListData();
                    break;
                case "GetVipExpand":
                    content = GetVipExpandListData();
                    break;
                case "GetVipCardInfo":
                    content = GetVipCardInfoData();
                    break;
                case "SaveVipCardDisable":
                    content = SaveVipCardDisableData();
                    break;
                case "SaveVipCardFozen":
                    content = SaveVipCardFozenData();
                    break;
                case "SaveVipCardRecharge":
                    content = SaveVipCardRechargeData();
                    break;
                case "SaveVipCardExtension":
                    content = SaveVipCardExtensionData();
                    break;
                case "SaveVipCardChangeLevel":
                    content = SaveVipCardChangeLevelData();
                    break;
                case "SaveVipCardActive":
                    content = SaveVipCardActiveData();
                    break;
                case "SaveVipCardSleep":
                    content = SaveVipCardSleepData();
                    break;
                case "SaveVipCardReportLoss":
                    content = SaveVipCardReportLossData();
                    break;
                case "LockVipCard":
                    content = LockVipCardInfoData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }


        #region GetVipData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetVipData()
        {
            var vipBLL = new VipBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var respData = new VipRespData();

            var VipCardCode = FormatParamValue(Request("VipCardNumber"));
            var VipName = FormatParamValue(Request("VipName"));
            var CarCode = FormatParamValue(Request("CarNumber"));

            string vipId = "";
            string vipCardId = "";

            var vipCardEntity = vipCardBLL.SearchTopVipCard(new VipCardEntity()
            {
                VipCardCode = VipCardCode,
                VipName = VipName,
                CarCode = CarCode
            });
            vipId = vipCardEntity.VipId;
            vipCardId = vipCardEntity.VipCardID;

            respData.VipData = vipBLL.GetByID(vipId);
            respData.VipCardData = vipCardEntity;

            content = respData.ToJSON();
            return content;
        }
        #endregion

        #region GetVipCardSalesListData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipCardSalesListData()
        {
            var service = new VipCardSalesBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            IList<VipCardSalesEntity> data;

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            VipCardSalesEntity queryInfo = new VipCardSalesEntity();
            queryInfo.VipCardID = key;
            data = service.GetList(queryInfo, pageIndex, PageSize);
            int totalCount = service.GetListCount(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.ToJSON(),
               totalCount);

            return content;
        }
        #endregion

        #region GetVipCardRechargeRecordListData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipCardRechargeRecordListData()
        {
            var service = new VipCardRechargeRecordBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            IList<VipCardRechargeRecordEntity> data;

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            VipCardRechargeRecordEntity queryInfo = new VipCardRechargeRecordEntity();
            queryInfo.VipCardID = key;
            data = service.GetList(queryInfo, pageIndex, PageSize);
            int totalCount = service.GetListCount(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.ToJSON(),
               totalCount);

            return content;
        }
        #endregion

        #region GetVipCardGradeChangeLogListData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipCardGradeChangeLogListData()
        {
            var service = new VipCardGradeChangeLogBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            IList<VipCardGradeChangeLogEntity> data;

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            VipCardGradeChangeLogEntity queryInfo = new VipCardGradeChangeLogEntity();
            queryInfo.VipCardID = key;
            data = service.GetList(queryInfo, pageIndex, PageSize);
            int totalCount = service.GetListCount(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.ToJSON(),
               totalCount);

            return content;
        }
        #endregion

        #region GetVipCardStatusChangeLogListData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipCardStatusChangeLogListData()
        {
            var service = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            IList<VipCardStatusChangeLogEntity> data;

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            VipCardStatusChangeLogEntity queryInfo = new VipCardStatusChangeLogEntity();
            queryInfo.VipCardID = key;
            data = service.GetList(queryInfo, pageIndex, PageSize);
            int totalCount = service.GetListCount(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.ToJSON(),
               totalCount);

            return content;
        }
        #endregion

        #region GetVipExpandListData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipExpandListData()
        {
            var service = new VipExpandBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipID") != null && Request("VipID") != string.Empty)
            {
                key = Request("VipID").ToString().Trim();
            }

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            IList<VipExpandEntity> data;

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            VipExpandEntity queryInfo = new VipExpandEntity();
            queryInfo.VipID = key;
            data = service.GetList(queryInfo, pageIndex, PageSize);
            int totalCount = service.GetListCount(queryInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               data.ToJSON(),
               totalCount);

            return content;
        }
        #endregion

        #region GetVipCardInfoData
        /// <summary>
        /// 
        /// </summary>
        public string GetVipCardInfoData()
        {
            var service = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            VipCardEntity data = new VipCardEntity();

            data = service.SearchTopVipCard(new VipCardEntity() { VipCardID = key });

            string Lock = string.Empty;
            if (Request("Lock") != null && Request("Lock") != string.Empty)
            {
                Lock = Request("Lock").ToString().Trim();
                if (Lock == "1") LockVipCardInfoData();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region LockVipCardInfoData
        /// <summary>
        /// 
        /// </summary>
        public string LockVipCardInfoData()
        {
            var service = new VipCardBLL(CurrentUserInfo);
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipCardID") != null && Request("VipCardID") != string.Empty)
            {
                key = Request("VipCardID").ToString().Trim();
            }

            string Lock = string.Empty;
            if (Request("Lock") != null && Request("Lock") != string.Empty)
            {
                Lock = Request("Lock").ToString().Trim();
            }
            string UnitID = string.Empty;
            if (Request("UnitID") != null && Request("UnitID") != string.Empty)
            {
                UnitID = Request("UnitID").ToString().Trim();
            }
            string VipCardStatusId = string.Empty;
            if (Request("VipCardStatusId") != null && Request("VipCardStatusId") != string.Empty)
            {
                VipCardStatusId = Request("VipCardStatusId").ToString().Trim();
            }

            VipCardEntity data = service.GetByID(key);
            int? vipCardStatusId = data.VipCardStatusId;
            if (Lock == "1")
            {
                vipCardStatusId = 5;
                if (VipCardStatusId.Length > 0) vipCardStatusId = int.Parse(VipCardStatusId);
            }

            service.Update(new VipCardEntity() { VipCardID = key, VipCardStatusId = vipCardStatusId }, false);

            vipCardStatusChangeLogBLL.Create(new VipCardStatusChangeLogEntity()
            { 
                LogID = Utils.NewGuid(),
                VipCardID = key,
                VipCardStatusID = vipCardStatusId,
                OldStatusID = data.VipCardStatusId,
                UnitID = UnitID
            });

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardDisableData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardDisableData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardFozenData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardFozenData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardRechargeData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardRechargeData()
        {
            var vipCardRechargeRecordBLL = new VipCardRechargeRecordBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardRechargeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardRechargeRecordBLL.SetVipCardRecjargeRpecord(
                entity.VipCardID, entity.RechargeAmount, entity.OrderNo,
                entity.PaymentTypeID, entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            string Lock = string.Empty;
            if (Request("Lock") != null && Request("Lock") != string.Empty)
            {
                Lock = Request("Lock").ToString().Trim();
                if (Lock == "1") LockVipCardInfoData();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardExtensionData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardExtensionData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardChangeLevelData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardChangeLevelData()
        {
            var vipCardGradeChangeLogBLL = new VipCardGradeChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardChangeLevelQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }
            if (entity.NowGradeID == null || entity.NowGradeID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "等级不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardGradeChangeLogBLL.SetVipCardGradeChange(
                entity.VipCardID, int.Parse(entity.ChangeBeforeGradeID), int.Parse(entity.NowGradeID),
                entity.ChangeReason, entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardActiveData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardActiveData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardSleepData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardSleepData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipCardReportLossData
        /// <summary>
        /// 卡注销
        /// </summary>
        public string SaveVipCardReportLossData()
        {
            var vipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;
            var data = new VipRespData();

            string key = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }

            var entity = key.DeserializeJSONTo<VipCardStatusChangeQueryEntity>();
            if (entity.VipCardID == null || entity.VipCardID.Trim().Length == 0)
            {
                data.success = false;
                data.Description = "卡标识不能为空";
                return data.ToJSON();
            }

            string error = string.Empty;
            var result = vipCardStatusChangeLogBLL.SetVipCardStatusChange(
                entity.VipCardID, int.Parse(entity.StatusIDNow), int.Parse(entity.StatusIDNext),
                entity.UnitID, out error);

            if (!result)
            {
                data.success = false;
                data.Description = "错误：" + error;
                return data.ToJSON();
            }

            content = data.ToJSON();
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class VipQueryEntity
    {
        public string VipCardNumber;
        public string VipName;
        public string CarNumber;
    }
    public class VipRespData
    {
        public bool success = true;
        public string Code = "200";
        public string Description = "操作成功";
        public string Exception = null;
        public VipEntity VipData;
        public VipCardEntity VipCardData;
    }
    public class VipCardDisableQueryEntity
    {
        public string VipCardNumber;
        public string VipName;
        public string CarNumber;
    }
    public class VipCardRechargeQueryEntity
    {
        public string VipCardID;
        public string VipID;
        public string UnitID;
        public decimal RechargeAmount;
        public string OrderNo;
        public string PaymentTypeID;
        public string Remark;
    }
    public class VipCardChangeLevelQueryEntity
    {
        public string VipCardID;
        public string VipID;
        public string UnitID;
        public string ChangeBeforeGradeID;
        public string NowGradeID;
        public string ChangeReason;
    }
    public class VipCardStatusChangeQueryEntity
    {
        public string VipCardID;
        public string VipID;
        public string UnitID;
        public string StatusIDNow;
        public string StatusIDNext;
    }
    #endregion

}