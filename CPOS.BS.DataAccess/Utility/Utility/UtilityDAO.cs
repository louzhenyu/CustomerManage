/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2012/12/5 13:56:28
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
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess.Utility
{

    /// <summary>
    /// ��OperationLog�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class UtilityDAO : BaseCPOSDAO
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public UtilityDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        //#region �޸ģ�ɾ������
        ///// <summary>
        ///// �޸�״̬��ɾ������ʹ��
        ///// </summary>
        ///// <param name="pEntity">ʵ��ʵ��</param>   
        //public void Update(UtilityEntity pEntity)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update " + pEntity.TableName + " set ");
        //    strSql.Append(pEntity.UptField + "=" + pEntity.UptValue);
        //    strSql.Append(" where " + pEntity.UptWhereField + " in (" + pEntity.UptWhereValue + ")");
        //    int result = 0;
        //    result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        //    pEntity.ResultNum = result;
        //}
        //#endregion

        //#region ��չ���
        ///// <summary>
        ///// �������ʹ��
        ///// </summary>
        ///// <param name="pEntity">ʵ��ʵ��</param>  
        //public void Delete(UtilityEntity pEntity)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" delete " + pEntity.TableName);
        //    strSql.Append(" where " + pEntity.DeleteField + " in (" + pEntity.DeleteValue + ")");
        //    int result = 0;
        //    result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        //    pEntity.ResultNum = result;
        //}
        //#endregion

        #region ��ҳ��ѯ
        public void PagedQuery(UtilityEntity pEntity)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
            pagedSql.AppendFormat("select ");
            pagedSql.AppendFormat(" * from ( select row_number()over( order by ");
            if (pEntity.PageSort != null && pEntity.PageSort != "")
            {
                pagedSql.AppendFormat(" " + pEntity.PageSort + " )as ID,");
            }
            else
            {
                pagedSql.AppendFormat(" isdelete asc ) as ID,");
            }
            pagedSql.AppendFormat(" * from " + pEntity.TableName + "   where isdelete=0  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from " + pEntity.TableName + "  where isdelete=0 ");
            //��������
            if (pEntity.PageWhere != null && pEntity.PageWhere != "")
            {
                pagedSql.AppendFormat(pEntity.PageWhere);
                totalCountSql.AppendFormat(pEntity.PageWhere);
            }
            //ȡָ��ҳ������

            pagedSql.AppendFormat(" ) as ABCD where ID>{0} and ID<={1}", pEntity.PageSize * (pEntity.PageIndex - 1), pEntity.PageSize * (pEntity.PageIndex));

            pEntity.ResultError = pagedSql.ToString();
            //ִ����䲢���ؽ��    
            using (DataSet ds = this.SQLHelper.ExecuteDataset(pagedSql.ToString()))
            {
                int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
                pEntity.PageTotal = totalCount;
                int remainder = 0;
                pEntity.PageCount = Math.DivRem(totalCount, pEntity.PageSize, out remainder);
                if (remainder > 0)
                {
                    pEntity.PageCount++;
                }
                pEntity.PageDataSet = ds;
            }
        }
        #endregion

        #region �Զ���SQL��ѯ
        public void GetAll(UtilityEntity pEntity)
        {
            //ִ����䲢���ؽ��    
            using (DataSet ds = this.SQLHelper.ExecuteDataset(pEntity.CustomSql.ToString()))
            {
                pEntity.PageDataSet = ds;
            }
        }

        /// <summary>
        /// ����Ӱ������
        /// </summary>
        /// <param name="pEntity"></param>
        public void Query(UtilityEntity pEntity, IDbTransaction pTran)
        {
            int result = 0;
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            pEntity.OpResultID = result;
        }

        public void GetScalar(UtilityEntity pEntity, IDbTransaction pTran)
        {
            int result = 0;
            object obj = null;
            if (pTran != null)
            {
                obj = this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            else
            {
                obj = this.SQLHelper.ExecuteScalar(CommandType.Text, pEntity.CustomSql.ToString(), null);
            }
            if (obj != null)
            {
                result = int.Parse(obj.ToString());
            }
            pEntity.OpResultID = result;
        }
        #endregion

        #region ͨ�÷�ҳ��ѯ
        public PagedQueryResult<T> GetList<T>(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string sql, int pageIndex, int pageSize) where T : class,IEntity, new()
        {
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

            //ͨ�÷�ҳ��ѯ
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + string.Format(sql, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID) + sqlWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //����ֵ
            PagedQueryResult<T> pEntity = new PagedQueryResult<T>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<T>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion
    }
}
