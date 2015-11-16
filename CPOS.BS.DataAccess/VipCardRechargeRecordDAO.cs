/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:28
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
    /// 表VipCardRechargeRecord的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardRechargeRecordDAO : Base.BaseCPOSDAO, ICRUDable<VipCardRechargeRecordEntity>, IQueryable<VipCardRechargeRecordEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(VipCardRechargeRecordEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(VipCardRechargeRecordEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";

            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetListSql(VipCardRechargeRecordEntity entity)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.Payment_Type_Name PaymentTypeName, c.Unit_Name UnitName, d.user_name RechargeUserName ";
            sql += " into #tmp ";
            sql += " from VipCardRechargeRecord a ";
            sql += " left join T_Payment_Type b on (a.PaymentTypeID=b.Payment_Type_ID and b.status='1') ";
            sql += " left join T_Unit c on (a.UnitID=c.Unit_ID and c.status='1') ";
            sql += " left join t_user d on (d.user_id=a.RechargeUserID and d.user_status='1') ";
            sql += " where a.IsDelete='0' ";
            if (entity.VipCardID != null && entity.VipCardID.Trim().Length > 0)
            {
                sql += " and a.vipCardId='" + entity.VipCardID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region SetVipCardRecjargeRpecord
        public bool SetVipCardRecjargeRpecord(VipCardEntity vipCardInfo,VipCardRechargeRecordEntity vipCardRRInfo,out string Error)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VipCardDAO vipCardDAO = new VipCardDAO(this.CurrentUserInfo);
                    vipCardDAO.Update(vipCardInfo,tran);
                    Create(vipCardRRInfo,tran);
                    //TO-DO:实现自己的业务
                    tran.Commit();
                    Error = "充值成功.";
                    return true;
                }
                catch
                {
                    //回滚&转抛异常
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

    }
}
