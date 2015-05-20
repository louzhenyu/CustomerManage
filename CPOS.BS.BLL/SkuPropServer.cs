using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// sku属性
    /// </summary>
    public class SkuPropServer : BaseService
    {

        JIT.CPOS.BS.DataAccess.SkuPropServer skuPropService = null;
        #region 构造函数
        public SkuPropServer(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            skuPropService = new DataAccess.SkuPropServer(loggingSessionInfo);
        }
        #endregion

        /// <summary>
        /// 获取sku属性集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<SkuPropInfo> GetSkuPropList()
        {
            IList<SkuPropInfo> skuPropInfoList = new List<SkuPropInfo>();
            DataSet ds = new DataSet();
            ds = skuPropService.GetSkuPropList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                skuPropInfoList = DataTableToObject.ConvertToList<SkuPropInfo>(ds.Tables[0]);
            }
            return skuPropInfoList;
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<SkuPropInfo>("SkuProp.Select", "");
        }

        #region DeleteSkuProp
        public bool DeleteSkuProp(SkuPropInfo skuPropInfo)
        {
            skuPropInfo.modify_time = Utils.GetNow();
            skuPropInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            return skuPropService.DeleteSkuProp(skuPropInfo);
        }
        #endregion

        #region SaveSkuProp
        /// <summary>
        /// AddSkuProp
        /// </summary>
        public bool AddSkuProp(SkuPropInfo skuPropInfo)
        {
            skuPropInfo.create_time = Utils.GetNow();
            skuPropInfo.modify_time = Utils.GetNow();
            skuPropInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
            skuPropInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            return skuPropService.AddSkuProp(skuPropInfo);
        }
        /// <summary>
        /// CheckSkuProp
        /// </summary>
        public bool CheckSkuProp(string propId)
        {
            return skuPropService.CheckSkuProp(propId);
        }

        public bool CheckSkuPropByDisplayindex(string customerID, int Displayindex)
        {
            return skuPropService.CheckSkuPropByDisplayindex(customerID, Displayindex);
        }
        /// <summary>
        /// ISCheckSkuProp
        /// </summary>
        public bool ISCheckSkuProp(string propId)
        {
            return skuPropService.ISCheckSkuProp(propId);
        }
        public bool ISCheckSkuProp2(string propId)
        {
            return skuPropService.ISCheckSkuProp2(propId);
        }
        #endregion

    }
}
