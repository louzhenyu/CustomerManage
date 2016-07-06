/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/6 13:39:19
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MarketSignInBLL : BaseService
    {
        /// <summary>
        /// 市场活动签到
        /// </summary>
        /// <param name="openID">用户微信ID</param>
        /// <param name="eventID">活动ID</param>
        public void SignIn(string openID, string eventID)
        {
            IWhereCondition[] whereCondition = new IWhereCondition[] {
                    new EqualsCondition() { FieldName = "OpenID", Value = openID },
                    new EqualsCondition() { FieldName = "EventID", Value = eventID }
                };

            var vips = this.Query(whereCondition, null);

            //用户没有签到，添加签到信息
            if (vips.Length == 0)
            {
                MarketSignInEntity entity = new MarketSignInEntity()
                {
                    SignInID = this.NewGuid(),
                    OpenID = openID,
                    EventID = eventID
                };

                this.Create(entity);
            }
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity WebGetList(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.WebGetListCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.WebGetList(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity WebGetListAdd(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                //int iCount = _currentDAO.WebGetListCount(vipSearchInfo);
                int iCount = _currentDAO.WebGetListCountAdd(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.WebGetListAdd(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var vipObj = new VipEntity();
                        vipObj.VIPID = dr["VIPID"] != DBNull.Value ? dr["VIPID"].ToString() : "";
                        vipObj.VipCode = dr["VipCode"] != DBNull.Value ? dr["VipCode"].ToString() : "";
                        vipObj.VipLevel = dr["VipLevel"] != DBNull.Value ? Convert.ToInt32(dr["VipLevel"].ToString()) : 0;
                        vipObj.VipName = dr["VipName"] != DBNull.Value ? dr["VipName"].ToString() : "";
                        vipObj.Phone = dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : "";
                        vipObj.WeiXin = dr["WeiXin"] != DBNull.Value ? dr["WeiXin"].ToString() : "";
                        vipObj.Integration = dr["Integration"] != DBNull.Value ? Convert.ToInt32(dr["Integration"].ToString()) : 0;
                        vipObj.LastUpdateTime = dr["LastUpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["LastUpdateTime"].ToString()) : DateTime.Now;
                        vipObj.PurchaseAmount = dr["PurchaseAmount"] != DBNull.Value ? Convert.ToDecimal(dr["PurchaseAmount"].ToString()) : 0;
                        vipObj.PurchaseCount = dr["PurchaseCount"] != DBNull.Value ? Convert.ToInt32(dr["PurchaseCount"].ToString()) : 0;

                        vipInfoList.Add(vipObj);
                    }
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity MarketWebGetList(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.WebGetListCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.WebGetList(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}