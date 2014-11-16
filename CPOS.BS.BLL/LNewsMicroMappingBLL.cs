/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/15 13:37:22
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
    public partial class LNewsMicroMappingBLL
    {
        #region ��ȡ΢����Ѷ�����б�
        /// <summary>
        /// ��ȡ΢����Ѷ�����б�
        /// </summary>
        /// <param name="microNumberId">����ID</param>
        /// <param name="microTypeId">���ID</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns></returns>
        public DataTable GetNewsMappList(string microNumberId, string microTypeId, string sortField, int sortOrder, int pageIndex, int pageSize, ref int rowCount, ref int pageCount)
        {
            DataSet ds = _currentDAO.GetNewsMappList(microNumberId, microTypeId, sortField, sortOrder, pageIndex, pageSize);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }
        
        #endregion

        #region ������Ѷ΢�������б�
        /// <summary>
        /// ������Ѷ΢�������б�
        /// </summary>
        /// <param name="numberId">����ID</param>
        /// <param name="typeId">���ID</param>
        /// <param name="newsIds">��ѶID</param>
        /// <returns>��Ӱ�������</returns>
        public int SetNewsMap(string numberId, string typeId, string newsIds)
        {
            if (string.IsNullOrEmpty(newsIds))
            {
                return 0;
            }
            return _currentDAO.SetNewsMap(numberId, typeId, newsIds.Split(','));
        } 
        #endregion

        #region ���š����ͳ�ƣ���ϵӳ���
        /// <summary>
        /// ���š����ͳ��
        /// �÷����ѷ��������ڵ�TypeCount���ݹ�����ͳ������ by yehua
        /// </summary>
        /// <param name="numberId">����Id</param>
        /// <param name="typeId">���Id</param>
        /// <returns>������Ӱ�������</returns>
        public int MicroNumberTypeCollect(string numberId, string typeId)
        {
            return _currentDAO.MicroNumberTypeCollect(numberId, typeId);
        } 
        #endregion

    }
}