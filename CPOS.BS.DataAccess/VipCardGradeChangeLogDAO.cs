/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:41:32
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
    /// 表VipCardGradeChangeLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardGradeChangeLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardGradeChangeLogEntity>, IQueryable<VipCardGradeChangeLogEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(VipCardGradeChangeLogEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(VipCardGradeChangeLogEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";

            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetListSql(VipCardGradeChangeLogEntity entity)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.VipCardGradeName ChangeBeforeGradeName, e.VipCardGradeName NowGradeName, c.Unit_Name UnitName, d.user_name OperationUserName ";
            sql += " ,(case when OperationType='1' then '自动' when OperationType='2' then '手动' else '' end) OperationTypeName ";
            sql += " into #tmp ";
            sql += " from VipCardGradeChangeLog a ";
            sql += " left join SysVipCardGrade b on (a.ChangeBeforeGradeID=b.VipCardGradeID and b.isDelete='0') ";
            sql += " left join T_Unit c on (a.UnitID=c.Unit_ID and c.status='1') ";
            sql += " left join t_user d on (d.user_id=a.OperationUserID and d.user_status='1') ";
            sql += " left join SysVipCardGrade e on (a.NowGradeID=e.VipCardGradeID and e.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            if (entity.VipCardID != null && entity.VipCardID.Trim().Length > 0)
            {
                sql += " and a.vipCardId='" + entity.VipCardID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region 设置卡等级 变更
        public bool SetVipCardStatusChange(VipCardEntity vipCardInfo, VipCardGradeChangeLogEntity vipCardGCInfo, out string strError)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VipCardDAO vipCardDAO = new VipCardDAO(this.CurrentUserInfo);
                    vipCardDAO.Update(vipCardInfo, tran);
                    Create(vipCardGCInfo, tran);
                    //TO-DO:实现自己的业务
                    tran.Commit();
                    strError = "卡等级变更成功.";
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
