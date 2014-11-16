/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ� �ݷò���ѡ��ֵ 
    /// ��VisitingParameterOptions�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VisitingParameterOptionsDAO
    {
        #region GetOptionNameList
        public PagedQueryResult<VisitingParameterOptionsViewEntity> GetOptionNameList(IWhereCondition[] pWhereConditions, string ClientID, string ClientDistributorID, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            if (ClientID == null || ClientID == "")
            {
                ClientID = CurrentUserInfo.ClientID;
            }
            if (ClientDistributorID == null || ClientDistributorID == "")
            {
                ClientDistributorID = CurrentUserInfo.ClientDistributorID;
            }
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }
            //options
            StringBuilder sql = new StringBuilder();
            sql.Append(
            @"select OptionName,0 as IsDelete,max(clientid) as ClientID,max(CreateTime) as CreateTime,COUNT(*) as OptionCount from VisitingParameterOptions where isdelete=0 "
            + sqlWhere.ToString()
            + " and ClientID='" + ClientID+"'"
            + " and ClientDistributorID=" + ClientDistributorID
            + " group by optionName");
            //ͨ�÷�ҳ��ѯ
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + sql.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageWhere = sqlWhere.ToString();
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //����ֵ
            PagedQueryResult<VisitingParameterOptionsViewEntity> pEntity = new PagedQueryResult<VisitingParameterOptionsViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingParameterOptionsViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region QueryByEntityWithOutIsDelete
        /// <summary>
        /// ����ʵ��������ѯʵ��(�������ݲ鿴)
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VisitingParameterOptionsEntity[] QueryByEntityWithOutIsDelete(VisitingParameterOptionsEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return QueryWithOutIsDelete(queryWhereCondition, pOrderBys);
        }
        #endregion

        #region QueryWithOutIsDelete
        /// <summary>
        /// ִ�в�ѯ(�������ݲ鿴)
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public VisitingParameterOptionsEntity[] QueryWithOutIsDelete(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingParameterOptions] where 1=1 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //ִ��SQL
            List<VisitingParameterOptionsEntity> list = new List<VisitingParameterOptionsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingParameterOptionsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        #endregion
    }
}
