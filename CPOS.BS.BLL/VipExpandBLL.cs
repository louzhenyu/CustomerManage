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
    public partial class VipExpandBLL
    {
        #region �б��ȡ
        /// <summary>
        /// �б��ȡ
        /// </summary>
        /// <param name="Page">��ҳҳ�롣��0��ʼ</param>
        /// <param name="PageSize">ÿҳ��������δָ��ʱĬ��Ϊ15</param>
        /// <returns></returns>
        public List<VipExpandEntity> GetList(VipExpandEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            List<VipExpandEntity> list = new List<VipExpandEntity>();

            DataSet ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipExpandEntity>(ds.Tables[0]);
            }

            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetListCount(VipExpandEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region ����Ϣ�б��ѯ
        /// <summary>
        /// ����Ϣ�б��ѯ
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public VipExpandEntity SearchVipExpand(VipExpandEntity searchInfo)
        {
            VipExpandEntity vipExpandInfo = new VipExpandEntity();
            IList<VipExpandEntity> vipExpandInfoList = new List<VipExpandEntity>();

            vipExpandInfo.ICount = _currentDAO.SearchVipExpandCount(searchInfo);
            DataSet ds = _currentDAO.SearchVipExpandList(searchInfo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipExpandInfoList = DataTableToObject.ConvertToList<VipExpandEntity>(ds.Tables[0]);
            }
            vipExpandInfo.VipExpandInfoList = vipExpandInfoList;
            return vipExpandInfo;
        }
        #endregion

    }
}