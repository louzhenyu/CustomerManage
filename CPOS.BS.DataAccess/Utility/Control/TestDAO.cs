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
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DAL
{
    /// <summary>
    /// ��OperationLog�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TestDAO : BaseCPOSDAO
    {
        protected BasicTenantUserInfo CurrentUserInfo;  
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TestDAO(BasicTenantUserInfo pUserInfo)
            : base(pUserInfo, true)
        {
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion

        #region GetList
        /// <summary>
        /// ���õ�GetList����
        /// </summary>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">ҳ����ʾ��</param>
        /// <param name="strWhere">��ѯ����</param>
        /// <param name="strOrder">��������</param>
        /// <param name="tableName">�������ѯ��sql���</param>
        /// <param name="RowCount">out ���ص�������</param>
        /// <returns>DataSet</returns>
        public DataSet GetList(int pageIndex, int pageSize, string strWhere, string strOrder, string tableName, out int RowCount)
        {
            UtilityEntity model = new UtilityEntity();
            if (!string.IsNullOrEmpty(tableName) && tableName.Length < 30 && tableName.Trim().IndexOf(' ') < 0)
            {
                model.TableName = tableName;
            }
            else
            {
                model.TableName = "(" + tableName + ") as A";
            }

            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageWhere = strWhere;
            model.PageSort = strOrder;
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);
            RowCount = 0;
            DataSet ds = null;
            if (model != null)
            {
                RowCount = model.PageTotal;
                ds = model.PageDataSet;
            }
            return ds;
        }
        #endregion

        #region NonQuery
        /// <summary>
        /// ����SQl��䷵����Ӱ������
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="Params">����(����)</param>
        /// <returns>int</returns>
        public int? NonQuery(string sql, IDbTransaction pTran)
        {
            UtilityEntity model = new UtilityEntity();
            model.CustomSql = sql;
            new UtilityDAO(this.CurrentUserInfo).Query(model, null);
            return model.OpResultID;
        }
        #endregion

    }
}
