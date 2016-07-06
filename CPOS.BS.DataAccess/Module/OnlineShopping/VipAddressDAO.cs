/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/11 11:45:55
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表VipAddress的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipAddressDAO
    {
        #region GetVIPAddressList
        public VipAddressEntity[] GetVIPAddressList(string pVipID)
        {
            VipAddressEntity[] pEntity = null;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT 
                a.*
                ,b.city1_name Province
                ,b.city2_name CityName
                ,b.city3_name DistrictName
            FROM vipaddress  a
            left join T_City b on a.CityID=b.city_id
            WHERE vipid='{0}' AND a.isdelete=0", pVipID);

            DataSet ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            if (ds != null
                && ds.Tables != null
                && ds.Tables.Count > 0)
            {
                pEntity = DataLoader.LoadFrom<VipAddressEntity>(ds.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region CreateAddress
        public void CreateAddress(VipAddressEntity pEntity)
        {
            if (pEntity.IsDefault == 1)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"
UPDATE vipaddress SET isdefault=0,lastupdateby={1},lastupdatetime=GETDATE()
FROM vipaddress WHERE isdelete=0 AND isdefault=1 AND vipid='{0}'
 ", pEntity.VIPID, CurrentUserInfo.UserID);
                this.SQLHelper.ExecuteNonQuery(sql.ToString());
            }
            this.Create(pEntity);
        }
        #endregion

        #region UpdateAddress
        public void UpdateAddress(VipAddressEntity pEntity)
        {
            if (pEntity.IsDefault == 1)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"
UPDATE vipaddress SET isdefault=0,lastupdateby={2},lastupdatetime=GETDATE()
FROM vipaddress WHERE isdelete=0 AND isdefault=1 AND vipid='{0}' AND vipaddressid<>'{1}'
 ", pEntity.VIPID, pEntity.VipAddressID, CurrentUserInfo.UserID);
                this.SQLHelper.ExecuteNonQuery(sql.ToString());
            }
            this.Update(pEntity);
        }
        #endregion
    }
}
