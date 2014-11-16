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

namespace JIT.CPOS.BS.Web.Module.Basic.VIP.Handler
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
                case "vip_save":
                    content = SaveVipData();
                    break;
                case "vip_delete":
                    content = VipDeleteData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }


        #region GetVipListData
        /// <summary>
        /// 查询会员
        /// </summary>
        public string GetVipListData()
        {
            var form = Request("form").DeserializeJSONTo<VipQueryEntity>();

            var vipBLL = new VipBLL(CurrentUserInfo);
            VipEntity vipList;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            VipSearchEntity queryEntity = new VipSearchEntity();
            queryEntity.VipInfo = FormatParamValue(form.VipName);
            queryEntity.Phone = FormatParamValue(form.Phone);
            queryEntity.Page = pageIndex;
            queryEntity.PageSize = PageSize;

            vipList = vipBLL.SearchVipInfo(queryEntity);
            var dataTotalCount = vipList.ICount;

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = vipList.vipInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetVipInfoById
        /// <summary>
        /// 根据会员ID获取会员信息
        /// </summary>
        public string GetVipInfoById()
        {
            var service = new VipBLL(CurrentUserInfo);
            VipEntity vip = new VipEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("VipID") != null && Request("VipID") != string.Empty)
            {
                key = Request("VipID").ToString().Trim();
            }

            vip = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = vip == null ? "0" : "1";
            jsonData.data = vip;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveVipData
        /// <summary>
        /// 保存会员
        /// </summary>
        public string SaveVipData()
        {
            var service = new VipBLL(CurrentUserInfo);
            VipEntity vip = new VipEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string vip_id = string.Empty;
            if (Request("vip") != null && Request("vip") != string.Empty)
            {
                key = Request("vip").ToString().Trim();
            }
            if (Request("VipId") != null && Request("VipId") != string.Empty)
            {
                vip_id = Request("VipId").ToString().Trim();
            }

            vip = key.DeserializeJSONTo<VipEntity>();

            if (vip.VipName == null || vip.VipName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "会员名称不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (vip.VIPID.Trim().Length == 0)
            {
                vip.VIPID = Utils.NewGuid();
                vip.ClientID = this.CurrentUserInfo.CurrentUser.customer_id;
                vip.Status = 1;
                service.Create(vip);
            }
            else
            {
                vip.VIPID = vip_id;
                service.Update(vip, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region VipDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string VipDeleteData()
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
            new VipBLL(this.CurrentUserInfo).Delete(new VipEntity()
            {
                VIPID = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity
    public class VipQueryEntity
    {
        public string VipName;
        public string Phone;
        public string VipStatus;
    }
    #endregion

}