/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:29
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
    /// ��VipCardStatusChangeLog�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardStatusChangeLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardStatusChangeLogEntity>, IQueryable<VipCardStatusChangeLogEntity>
    {

        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(VipCardStatusChangeLogEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(VipCardStatusChangeLogEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";

            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetListSql(VipCardStatusChangeLogEntity entity)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.VipCardStatusName VipCardStatusName, e.VipCardStatusName OldStatusName, c.Unit_Name UnitName, d.user_name OperationUserName ";
            sql += " into #tmp ";
            sql += " from VipCardStatusChangeLog a ";
            sql += " left join SysVipCardStatus b on (a.VipCardStatusID=b.VipCardStatusId and b.isDelete='0') ";
            sql += " left join T_Unit c on (a.UnitID=c.Unit_ID and c.status='1') ";
            sql += " left join t_user d on (d.user_id=a.CreateBy and d.user_status='1') ";
            sql += " left join SysVipCardStatus e on (a.OldStatusID=e.VipCardStatusId and e.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            if (entity.VipCardID != null && entity.VipCardID.Trim().Length > 0)
            {
                sql += " and a.vipCardId='" + entity.VipCardID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region ����״̬���
        public bool SetVipCardStatusChange(VipCardEntity vipCardInfo, VipCardStatusChangeLogEntity statusChangeInfo, out string strError)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VipCardDAO vipCardDAO = new VipCardDAO(this.CurrentUserInfo);
                    vipCardDAO.Update(vipCardInfo, false,tran);
                    Create(statusChangeInfo,tran);
                    //TO-DO:ʵ���Լ���ҵ��
                    tran.Commit();
                    strError = "״̬����ɹ�.";
                    return true;
                }
                catch
                {
                    //�ع�&ת���쳣
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

    }
}
