/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表PowerHourInvite的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PowerHourInviteDAO : Base.BaseCPOSDAO, ICRUDable<PowerHourInviteEntity>, IQueryable<PowerHourInviteEntity>
    {
        /// <summary>
        /// 检查当前用户是否参加了培训。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <returns></returns>
        public int GetPowerHourAttendence(string customerID, string powerHouerID, string staffUserID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select ISNULL(Attendence,0) as Attendence from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}'", powerHouerID, staffUserID, customerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            int num = 0;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                int.TryParse(ds.Tables[0].Rows[0]["Attendence"].ToString(), out num);
                return num;
            }

            //返回
            return 0;
        }

        /// <summary>
        /// 检查当前用户是否收到了讲座邀请。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <param name="staffUserID"></param>
        /// <returns></returns>
        public PowerHourInviteEntity GetBeforeUserInvite(string customerID, string powerHouerID, string staffUserID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}' ", powerHouerID, staffUserID, customerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
            PowerHourInviteEntity entity = null;
            //读取数据
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out entity);
                    break;
                }
            }
            //返回
            return entity;
        }

        /// <summary>
        /// 验证Power Hour用户是否收到邀请，收到返回对象
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <param name="staffUserID"></param>
        /// <returns></returns>
        public PowerHourInviteEntity VerifyPowerHourInvite(string pCustomerID, string pPowerHouerID, string pStaffUserID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}' ", pPowerHouerID, pStaffUserID, pCustomerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
            PowerHourInviteEntity entity = null;
            //读取数据
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out entity);
                    break;
                }
            }
            //返回
            return entity;
        }
    }
}
