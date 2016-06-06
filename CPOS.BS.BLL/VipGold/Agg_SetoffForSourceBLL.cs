/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:25:10
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class Agg_SetoffForSourceBLL
    {

        /// <summary>
        /// ��ҳ��ȡ �����ж���Դ����
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="SetoffRoleId">��ɫ��Դ 1=Ա����2=�ͷ���3=��Ա</param>
        /// <param name="PageSize">ҳ��</param>
        /// <param name="PageIndex">��ǰҳ����</param>
        /// <returns></returns>
        public DataSet GetSetofSourcesListByCustomerId(string CustomerId, int? SetoffRoleId, string beginTime, string endTime)
        {
            return _currentDAO.GetSetofSourcesListByCustomerId(CustomerId, SetoffRoleId, beginTime, endTime);
        }

        /// <summary>
        /// ��ҳ��ȡ ������Դ���� �б�
        /// </summary>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="PageSize">ÿҳ�ֶ�����</param>
        /// <param name="PageIndex">��ǰҳ</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForSourceEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            return _currentDAO.FindAllByPage(pWhereConditions, pOrderBys, PageSize, PageIndex);

        }

        /// <summary>
        /// ͨ�����ͽ�ɫ��Դ����Id ��ȡ����
        /// </summary>
        /// <param name="type">���ͱ��</param>
        /// <returns>���ͽ�ɫ��Դ����</returns>
        public string GetSetoffRoleNameBySetoffRoleId(int type)
        {
            if (type == 1)
            {
                return "Ա��";
            }
            else if (type == 2)
            {
                return "�ͷ�";
            }
            else
            {
                return "��Ա";
            }
        }
    }
}