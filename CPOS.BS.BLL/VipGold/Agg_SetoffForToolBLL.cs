/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/20 9:22:52
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
    public partial class Agg_SetoffForToolBLL
    {
        /// <summary>
        /// ��ȡ���������Ϣ ����ʱ�䡢�̻���� ��ȡ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="DateCode">ͳ������</param>
        /// <param name="SetoffToolTypeId">���͹������ͣ�CTW������ֿ⡢Coupon���Ż�ȯ��SetoffPoster�����ͱ���Goods����Ʒ��</param>
        /// <returns></returns>
        public DataSet GetSetofToolListByCustomerId(string CustomerId, string DateCode, string SetoffRoleId, string begintime, string endtime)
        {
            return _currentDAO.GetSetofToolListByCustomerId(CustomerId, SetoffRoleId, begintime, endtime, DateCode);
        }


        /// <summary>
        /// ��ҳ��ȡ �������ݷ��� �б�
        /// </summary>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="PageSize">ÿҳ�ֶ�����</param>
        /// <param name="PageIndex">��ǰҳ</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForToolEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            return _currentDAO.FindAllByPage(pWhereConditions, pOrderBys, PageSize, PageIndex);
        }
        /// <summary>
        /// ��CTW Coupon Goods ת��Ϊ ������ CTW������ֿ�   Coupon���Ż�ȯ   SetoffPoster�����ͱ�   Goods����Ʒ   ����
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetSetoffToolTypeNameBySetoffToolType(string type)
        {
            if (type == "CTW")
            {
                return "����ֿ�";
            }
            else if (type == "Coupon")
            {
                return "�Ż�ȯ";
            }
            else if (type == "SetoffPoster")
            {
                return "���ͺ���";
            }
            else if (type == "Goods")
            {
                return "��Ʒ";
            }
            else
            {
                return "";
            }
        }
    }
}