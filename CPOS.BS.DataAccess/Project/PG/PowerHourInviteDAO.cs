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
    /// ���ݷ��ʣ�  
    /// ��PowerHourInvite�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PowerHourInviteDAO : Base.BaseCPOSDAO, ICRUDable<PowerHourInviteEntity>, IQueryable<PowerHourInviteEntity>
    {
        /// <summary>
        /// ��鵱ǰ�û��Ƿ�μ�����ѵ��
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <returns></returns>
        public int GetPowerHourAttendence(string customerID, string powerHouerID, string staffUserID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select ISNULL(Attendence,0) as Attendence from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}'", powerHouerID, staffUserID, customerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            int num = 0;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                int.TryParse(ds.Tables[0].Rows[0]["Attendence"].ToString(), out num);
                return num;
            }

            //����
            return 0;
        }

        /// <summary>
        /// ��鵱ǰ�û��Ƿ��յ��˽������롣
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <param name="staffUserID"></param>
        /// <returns></returns>
        public PowerHourInviteEntity GetBeforeUserInvite(string customerID, string powerHouerID, string staffUserID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}' ", powerHouerID, staffUserID, customerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
            PowerHourInviteEntity entity = null;
            //��ȡ����
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out entity);
                    break;
                }
            }
            //����
            return entity;
        }

        /// <summary>
        /// ��֤Power Hour�û��Ƿ��յ����룬�յ����ض���
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="powerHouerID"></param>
        /// <param name="staffUserID"></param>
        /// <returns></returns>
        public PowerHourInviteEntity VerifyPowerHourInvite(string pCustomerID, string pPowerHouerID, string pStaffUserID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from PowerHourInvite where PowerHourID='{0}' and StaffUserID='{1}' and IsDelete=0 and CustomerID='{2}' ", pPowerHouerID, pStaffUserID, pCustomerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());
            PowerHourInviteEntity entity = null;
            //��ȡ����
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out entity);
                    break;
                }
            }
            //����
            return entity;
        }
    }
}
