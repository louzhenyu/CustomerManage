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
    /// ��VipExpand�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipExpandDAO : Base.BaseCPOSDAO, ICRUDable<VipExpandEntity>, IQueryable<VipExpandEntity>
    {

        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(VipExpandEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(VipExpandEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;

            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";

            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetListSql(VipExpandEntity entity)
        {
            string sql = string.Empty;
            sql = " SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.CarBarndName, c.CarModelsName ";
            sql += " into #tmp ";
            sql += " from VipExpand a ";
            sql += " left join VipCardCarBrand b on (a.CarBrandID=b.CarBrandID and b.isDelete='0') ";
            sql += " left join VipCardModels c on (a.CarModelsID=c.CarModelsID and c.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            if (entity.VipCardID != null && entity.VipCardID.Trim().Length > 0)
            {
                sql += " and a.vipCardId='" + entity.VipCardID + "' ";
            }
            if (entity.VipID != null && entity.VipID.Trim().Length > 0)
            {
                sql += " and a.VipID='" + entity.VipID + "' ";
            }
            sql += " order by a.CreateTime desc ";
            return sql;
        }
        #endregion

        #region ����Ϣ�б��ѯ
        public int SearchVipExpandCount(VipExpandEntity searchInfo)
        {
            string sql = SearchVipExpandSql(searchInfo);
            sql += " select count(*) From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet SearchVipExpandList(VipExpandEntity searchInfo)
        {
            int beginSize = searchInfo.startRowIndex - 1;
            int endSize = searchInfo.startRowIndex * searchInfo.maxRowCount + searchInfo.maxRowCount;

            string sql = SearchVipExpandSql(searchInfo);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";

            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string SearchVipExpandSql(VipExpandEntity searchInfo)
        {
            string sql = " SELECT a.* "
                    + " ,(SELECT x.CarBarndName FROM VipCardCarBrand x WHERE x.CarBrandID = a.CarBrandID AND x.IsDelete=0) CarBrandName "
                    + " ,(SELECT x.CarModelsName FROM VipCardModels x WHERE x.CarModelsID = a.CarModelsID AND x.IsDelete=0 ) CarModelsName "
                    + " ,DisplayIndex = row_number() over(order by a.LicensePlateNo desc) "
                    + " into #tmp "
                    + " FROM dbo.VipExpand a "
                    + " WHERE 1 = 1 AND a.IsDelete = 0 ";

            if (searchInfo.VipID != null && !searchInfo.VipID.Equals(""))
            {
                sql += " and a.VipID = '" + searchInfo.VipID + "'";
            }
            if (searchInfo.VipExpandID != null && !searchInfo.VipExpandID.Equals(""))
            {
                sql += " and a.VipExpandID = '" + searchInfo.VipExpandID + "'";
            }

            return sql;
        }
        #endregion

    }
}
