using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Extension;
using System.Data;

namespace JIT.CPOS.BS.Web.Module.WMaterialText.Handler
{
    /// <summary>
    /// WMaterialTextHandler 的摘要说明
    /// </summary>
    public class WMaterialTextHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetList": //根据活动ID获取定义信息
                    res = GetList(pContext);
                    break;
                case "GetMaterialTextType":
                    res = GetMaterialTextType();
                    break;
                case "Delete":
                    res = Delete(pContext);
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetList
        public string GetList(HttpContext pContext)
        {
            try
            {
                WMaterialTextEntity entity = pContext.Request["form"].DeserializeJSONTo<WMaterialTextEntity>();
                int pageSize = pContext.Request["limit"].ToInt();
                int pageIndex = pContext.Request["page"].ToInt();
                DataSet ds = new WMaterialTextBLL(CurrentUserInfo).GetWMaterialTextPage(entity.Title, entity.ModelId, pageSize, pageIndex - 1);
                //return string.Format("{{\"totalCount\":'{0}',\"topics\":{1}}}",
                //          ds.Tables[1].Rows[0][0], ds.Tables[0].ToJSON());
                return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}", ds.Tables[0].ToJSON(), ds.Tables[1].Rows[0][0]);
            }
            catch (Exception)
            {

                return string.Format("{{\"totalCount\":'',\"topics\":''}}");
            }

        }
        #endregion

        #region GetMaterialTextType
        /// <summary>
        /// 获取所有图文素材类别
        /// </summary>
        /// <returns></returns>
        public string GetMaterialTextType()
        {
            try
            {
                DataSet ds = new WMaterialTextBLL(CurrentUserInfo).GetWmType();
                return ds.Tables[0].ToJSON();
            }
            catch (Exception)
            {

                return "";
            }

        }
        #endregion

        #region Delete
        public string Delete(HttpContext pContext)
        {
            try
            {
                WMaterialTextBLL bll = new WMaterialTextBLL(CurrentUserInfo);
                string textID = pContext.Request["textID"];
                var server = new WMenuMTextMappingBLL(CurrentUserInfo);
                var entity = server.QueryByEntity(new WMenuMTextMappingEntity { TextId = textID ,IsDelete=0},                              null).FirstOrDefault();
                if (entity != null)
                {
                    return string.Format("{{\"success\":'false',\"msg\":\"图文信息已被引用,不能删除\"}}");

                }

                object[] obi = new object[] { textID };
                bll.Delete(obi);
                return string.Format("{{\"success\":'true'}}");
            }
            catch (Exception)
            {
                return string.Format("{{\"success\":'false',\"msg\":\"操作异常，删除失败\"}}");
                throw;
            }

        }
        #endregion
    }
}