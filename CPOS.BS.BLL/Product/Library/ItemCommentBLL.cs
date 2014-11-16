/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:33
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
    public partial class ItemCommentBLL
    {
        #region ��ȡ�û�����
        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVipId"></param>
        /// <returns></returns>
        public ItemCommentEntity GetItemCommentEntityByUser(string pItemId, string pVipId)
        {
            return this._currentDAO.GetItemCommentEntityByUser(pItemId, pVipId);
        }
        #endregion

        #region ��ȡ�û�����
        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        /// <param name="pItemId"></param>
        /// <param name="pVidId"></param>
        /// <returns></returns>
        public DataTable GetItemCommentByUser(string pItemId, string pVidId)
        {
            return this._currentDAO.GetItemCommentByUser(pItemId, pVidId);
        }
        #endregion

        #region ��ȡ�γ������б�
        /// <summary>
        /// ��ȡ�γ������б�
        /// </summary>
        /// <param name="pItemId"></param>
        /// <returns></returns>
        public DataTable GetCourseComment(string pItemId, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetCourseComment(pItemId, pPageIndex, pPageSize);
        }
        #endregion

        #region ��ȡ�γ�������ƽ����
        /// <summary>
        /// ��ȡ�γ�������ƽ����
        /// </summary>
        /// <param name="pItemId"></param>
        /// <returns></returns>
        public int GetCourseAvgStar(string pItemId)
        {
            return this._currentDAO.GetCourseAvgStar(pItemId);
        }
        #endregion
    }
}