/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/18 16:43:12
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
    public partial class LNumberTypeMappingBLL
    {
        /// <summary>
        /// ��ȡ���ݼ���
        /// </summary>
        /// <param name="mapEn">����ʵ��</param>
        /// <returns></returns>
        public string GetNumberTypeMapping(LNumberTypeMappingEntity mapEn)
        {
            OrderBy[] order = new OrderBy[]{
                new OrderBy(){ FieldName="NumberId", Direction=OrderByDirections.Asc}
            };
            LNumberTypeMappingEntity[] mapEnLst = _currentDAO.QueryByEntity(mapEn, order);
            if (mapEnLst == null)
            {
                return string.Empty;
            }
            return mapEnLst.Select(r => r.TypeId).ToArray().ToJoinString(',');
        }
        /// <summary>
        /// �������������������б�
        /// </summary>
        /// <param name="numberId">����ID</param>
        /// <param name="typeIds">����ID�б�</param>
        public void SetNumberTypeMapping(string numberId, string typeIds)
        {
            //ִ����Ϣ��������
            _currentDAO.SetNumberTypeMapping(numberId, typeIds.Split(','));
        }
        /// <summary>
        /// ��ȡ������������Ҫ��Ϣ
        /// </summary>
        /// <param name="numberId">�ڿ�Id</param>
        /// <returns></returns>
        public DataTable GetNumberTypeSum(string numberId)
        {
            return _currentDAO.GetNumberTypeSum(numberId).Tables[0] ?? null;
        }
    }
}