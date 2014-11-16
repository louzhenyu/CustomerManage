using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.VipCard.Register.Handler
{
    /// <summary>
    /// RegisterHandler
    /// </summary>
    public class RegisterHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "get_vip_list":      //根据条件获取会员信息
                    content = GetVipListData();
                    break;
                case "get_vipcard_by_vipid":  //根据会员ID获取会员卡信息
                    content = GetVipCardByVipID();
                    break;
                case "get_vipcard_by_id":   //根据会员卡ID获取会员卡信息
                    content = GetVipCardByVipCardID();
                    break;
                case "save_vipcard":        //保存会员卡信息
                    content = SaveVipCard();
                    break;
                case "delete_vipcard":      //删除会员卡信息
                    content = DeleteVipCard();
                    break;
                case "get_vipexpand_by_vipid":  //根据会员ID获取车信息
                    content = GetVipExpandByVipID();
                    break;
                case "get_vipexpand_by_id":   //根据车信息ID获取车信息
                    content = GetVipExpandByVipExpandID();
                    break;
                case "save_vipexpand":      //保存车信息
                    content = SaveVipExpand();
                    break;
                case "delete_vipexpand":    //删除车信息
                    content = DeleteVipExpand();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetVipListData 获取VIP列表
        /// <summary>
        /// 获取VIP列表
        /// </summary>
        public string GetVipListData()
        {
            var service = new VipCardBLL(CurrentUserInfo);
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            var queryEntity = new VipSearchEntity();
            queryEntity.Page = pageIndex;
            queryEntity.PageSize = PageSize;

            if (Request("vipName") != "")
            {
                queryEntity.VipInfo = FormatParamValue(Request("vipName"));
            }
            if (Request("phone") != "")
            {
                queryEntity.Phone = FormatParamValue(Request("phone"));
            }

            var data = service.GetVipList(queryEntity);

            var dataTotalCount = data.ICount;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.vipInfoList.ToJSON(),
                dataTotalCount);
            return content;
        }
        #endregion

        #region GetVipCardByVipID 根据会员ID获取会员卡信息
        /// <summary>
        /// 根据会员ID获取会员卡信息
        /// </summary>
        public string GetVipCardByVipID()
        {
            var service = new VipCardBLL(CurrentUserInfo);
            var searchInfo = new VipCardEntity();
            string content = string.Empty;

            if (Request("vipId") != "")
            {
                searchInfo.VipId = FormatParamValue(Request("vipId"));
            }

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));
            searchInfo.maxRowCount = maxRowCount;
            searchInfo.startRowIndex = startRowIndex;
            var data = service.SearchVipCard(searchInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.VipCardInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetVipCardByVipCardID 根据会员卡ID获取会员卡信息
        /// <summary>
        /// 根据会员卡ID获取会员卡信息
        /// </summary>
        public string GetVipCardByVipCardID()
        {
            var service = new VipCardBLL(CurrentUserInfo);
            var searchInfo = new VipCardEntity();
            string content = string.Empty;

            if (Request("VipCardID") != "")
            {
                searchInfo.VipCardID = FormatParamValue(Request("VipCardID"));
            }

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));
            searchInfo.maxRowCount = maxRowCount;
            searchInfo.startRowIndex = startRowIndex;
            var data = service.SearchVipCard(searchInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.VipCardInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region SaveVipCard 保存会员卡信息

        /// <summary>
        /// 保存会员卡信息
        /// </summary>
        public string SaveVipCard()
        {
            var vipCardService = new VipCardBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string vipCardID = string.Empty;
            var vipCards = Request("vipCards");

            if (FormatParamValue(vipCards) != null && FormatParamValue(vipCards) != string.Empty)
            {
                key = FormatParamValue(vipCards).ToString().Trim();
            }
            if (FormatParamValue(Request("VipCardID")) != null && FormatParamValue(Request("VipCardID")) != string.Empty)
            {
                vipCardID = FormatParamValue(Request("VipCardID")).ToString().Trim();
            }

            var vipCardEntity = key.DeserializeJSONTo<VipCardEntity>();

            if (vipCardEntity.VipCardTypeID == null || vipCardEntity.VipCardTypeID.ToString().Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "卡类型不能为空";
                return responseData.ToJSON();
            }
            if (vipCardEntity.VipCardStatusId == null || vipCardEntity.VipCardStatusId.ToString().Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "卡状态不能为空";
                return responseData.ToJSON();
            }
            if (vipCardEntity.VipCardGradeID == null || vipCardEntity.VipCardGradeID.ToString().Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "卡等级不能为空";
                return responseData.ToJSON();
            }
            if (vipCardEntity.UnitID == null || vipCardEntity.UnitID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "会籍店不能为空";
                return responseData.ToJSON();
            }

            var flag = vipCardService.SaveVipCardInfo(vipCardID, vipCardEntity);

            responseData.success = flag;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region DeleteVipCard 删除会员卡信息
        /// <summary>
        /// 删除会员卡信息
        /// </summary>
        public string DeleteVipCard()
        {
            var service = new VipCardBLL(CurrentUserInfo);

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
                responseData.msg = "会员卡ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                //删除会员卡信息
                service.DeleteVipCardInfo(id);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region GetVipExpandByVipID 根据会员ID获取车信息
        /// <summary>
        /// 根据会员ID获取车信息
        /// </summary>
        public string GetVipExpandByVipID()
        {
            var service = new VipExpandBLL(CurrentUserInfo);
            var searchInfo = new VipExpandEntity();
            string content = string.Empty;

            if (Request("vipId") != "")
            {
                searchInfo.VipID = FormatParamValue(Request("vipId"));
            }

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));
            searchInfo.maxRowCount = maxRowCount;
            searchInfo.startRowIndex = startRowIndex;
            var data = service.SearchVipExpand(searchInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.VipExpandInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetVipExpandByVipExpandID 根据车信息ID获取车信息
        /// <summary>
        /// 根据车信息ID获取车信息
        /// </summary>
        public string GetVipExpandByVipExpandID()
        {
            var service = new VipExpandBLL(CurrentUserInfo);
            var searchInfo = new VipExpandEntity();
            string content = string.Empty;

            if (Request("VipExpandID") != "")
            {
                searchInfo.VipExpandID = FormatParamValue(Request("VipExpandID"));
            }

            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("start")));
            searchInfo.maxRowCount = maxRowCount;
            searchInfo.startRowIndex = startRowIndex;
            var data = service.SearchVipExpand(searchInfo);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.VipExpandInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region SaveVipExpand 保存车信息

        /// <summary>
        /// 保存车信息
        /// </summary>
        public string SaveVipExpand()
        {
            var vipExpandService = new VipExpandBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string vipExpandID = string.Empty;
            var vipExpands = Request("vipExpands");

            if (FormatParamValue(vipExpands) != null && FormatParamValue(vipExpands) != string.Empty)
            {
                key = FormatParamValue(vipExpands).ToString().Trim();
            }
            if (FormatParamValue(Request("VipExpandID")) != null && FormatParamValue(Request("VipExpandID")) != string.Empty)
            {
                vipExpandID = FormatParamValue(Request("VipExpandID")).ToString().Trim();
            }

            var vipExpandEntity = key.DeserializeJSONTo<VipExpandEntity>();

            if (vipExpandEntity.LicensePlateNo == null || vipExpandEntity.LicensePlateNo.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "车牌号不能为空";
                return responseData.ToJSON();
            }
            if (vipExpandEntity.CarBrandID == null || vipExpandEntity.CarBrandID.ToString().Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "车品牌不能为空";
                return responseData.ToJSON();
            }
            if (vipExpandEntity.CarModelsID == null || vipExpandEntity.CarModelsID.ToString().Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "车型不能为空";
                return responseData.ToJSON();
            }

            if (vipExpandID.Trim().Length == 0)
            {
                //新增车信息
                vipExpandEntity.VipExpandID = Utils.NewGuid();
                vipExpandEntity.VipCardID = string.Empty;
                vipExpandService.Create(vipExpandEntity);
            }
            else
            {
                //修改车信息
                vipExpandEntity.VipExpandID = vipExpandID;
                vipExpandService.Update(vipExpandEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region DeleteVipExpand 删除车信息
        /// <summary>
        /// 删除车信息
        /// </summary>
        public string DeleteVipExpand()
        {
            var service = new VipExpandBLL(CurrentUserInfo);

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
                responseData.msg = "车信息ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                //删除车信息
                service.Delete(new VipExpandEntity() { VipExpandID = id });
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }
}