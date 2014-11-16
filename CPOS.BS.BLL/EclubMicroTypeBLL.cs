/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
    public partial class EclubMicroTypeBLL
    {
        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public EclubMicroTypeEntity[] MicroIssueTypeGet(EclubMicroTypeEntity pQueryEntity)
        {
            OrderBy[] pOrderBys = new OrderBy[] { 
                new OrderBy { FieldName = "Sequence", Direction = OrderByDirections.Asc } 
            };
            if (string.IsNullOrEmpty(pQueryEntity.ParentID))
            {
                pQueryEntity.ParentID = null;
                return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys).Where(r => string.IsNullOrEmpty(r.ParentID)).ToArray();
            }
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }
        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <param name="typeEn">ʵ��</param>
        /// <returns></returns>
        public DataTable GetMicroTypes(EclubMicroTypeEntity typeEn)
        {
            return _currentDAO.GetMicroTypes(typeEn).Tables[0];
        }

        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <returns></returns>
        public DataTable GetMicroTypeList(int sortOrder, string sortField, int pageIndex, int pageSize, ref int pageCount, ref int rowCount)
        {
            DataSet ds = _currentDAO.GetMicroTypeList(sortOrder, sortField, pageIndex, pageSize);
            if (ds == null || ds.Tables.Count == 0)
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

        /// <summary>
        /// ��ȡ�����ϸ������������Ϣ
        /// </summary>
        /// <param name="typeEn">���ʵ����Ϣ</param>
        /// <returns></returns>
        public DataTable GetMicroTypeDtail(EclubMicroTypeEntity typeEn)
        {
            return _currentDAO.GetMicroTypeDtail(typeEn).Tables[0] ?? null;
        }

        /// <summary>
        /// ����������������ȡ�ѹ����İ���б�
        /// by yehua
        /// </summary>
        public DataTable MicroIssueTypeGet(string numberId, string parentId, string typeLevel)
        {
            return _currentDAO.MicroIssueTypeGet(numberId, parentId, typeLevel);
        }
    }
}