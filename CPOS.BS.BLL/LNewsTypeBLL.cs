/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/3 10:59:10
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class LNewsTypeBLL
    {

        #region GetLNewsTypeList
        /// <summary>
        ///��ȡ��Ϣ
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<LNewsTypeEntity> GetLNewsTypeList(IWhereCondition[] pWhereConditions, int pageIndex, int pageSize)
        {
            PagedQueryResult<LNewsTypeEntity> pEntity = this._currentDAO.GetLNewsTypeList(pWhereConditions, pageIndex, pageSize);
            return pEntity;
        }
        #endregion

        #region GetPartentNewsType
        /// <summary>
        /// ��ѯ���е�ǰ���
        /// </summary>
        /// <returns></returns>
        public DataSet GetPartentNewsType()
        {
            return this._currentDAO.GetPartentNewsType();
        }
        #endregion

        #region DelLNewsTypeByID
        /// <summary>
        ///ɾ����Ѷ���
        /// </summary>
        /// <param name="lNewsTypeID"></param>
        /// <param name="msg"></param>
        public int DelLNewsTypeByID(string lNewsTypeID)
        {
            return this._currentDAO.DelLNewsTypeByID(lNewsTypeID);
        }

        #endregion
        #region IsSameName
        /// <summary>
        /// �ж��Ƿ�ͬ��
        /// </summary>
        /// <param name="lNewsTypeID"></param>
        /// <param name="newsTypeName"></param>
        /// <returns></returns>
        public bool IsSameName(string lNewsTypeID, string newsTypeName)
        {
            return this._currentDAO.IsSameName(lNewsTypeID, newsTypeName);
        }
        #endregion
        public void Update(LNewsTypeEntity pEntity, bool pIsUpdateNullField)
        {
            _currentDAO.Update(pEntity, pIsUpdateNullField);
        }



        public DataSet GetNewsTypeList(string customerId, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetNewsTypeList(customerId, pageIndex, pageSize);
        }

        /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||���°���Ѷ����Alan |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
        /// <summary>
        /// ��ѯ���е�ǰ���
        /// </summary>
        /// <param name="parentID">���ุID</param>
        /// <returns>DataTable���ݼ�</returns>
        public DataTable GetNewsTypes(string parentID)
        {
            return _currentDAO.GetNewsTypes(parentID).Tables[0];
        }

        /// <summary>
        /// ��ȡ�����б���ҳ
        /// </summary>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns>���ݼ�</returns>
        public DataTable GetTypeListPage(string sortField, int sortOrder, int pageIndex, int pageSize,ref int pageCount,ref int rowCount)
        {
            DataSet ds = _currentDAO.GetTypeListPage(sortField, sortOrder, pageIndex, pageSize);
            if (ds == null && ds.Tables.Count <= 0)
            {
                return null;
            }
            rowCount = 0;
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }
    }
}