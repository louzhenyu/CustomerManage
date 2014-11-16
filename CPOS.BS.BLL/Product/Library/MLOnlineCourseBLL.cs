/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:35
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
    /// ҵ���� ��Ŀ�� 
    /// </summary>
    public partial class MLOnlineCourseBLL
    {
        #region ��ȡ�γ�
        /// <summary>
        /// ��ȡ�γ�
        /// </summary>
        /// <param name="pCourseType"></param>
        /// <param name="pSortKey"></param>
        /// <param name="pSortOrientation"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetOnlineCourse(string pCourseType, string pSortKey, string pSortOrientation, int pPageIndex, int pPageSize)
        {
            return _currentDAO.GetOnlineCourse(pCourseType, pSortKey, pSortOrientation, pPageIndex, pPageSize);
        }
        #endregion

        #region ģ����ѯ�γ�
        /// <summary>
        /// ģ����ѯ�γ�
        /// </summary>
        /// <param name="pKeyword"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable SearchOnlineCourse(string pKeyword, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.SearchOnlineCourse(pKeyword, pPageIndex, pPageSize);
        }
        #endregion


        #region ��ȡ�γ�����
        /// <summary>
        /// ��ȡ�γ�����
        /// </summary>
        /// <param name="pOnlineCourseId"></param>
        /// <returns></returns>
        public DataTable SearchOnlineCourseDetail(string pOnlineCourseId)
        {
            return this._currentDAO.SearchOnlineCourseDetail(pOnlineCourseId);
        }
        #endregion

        #region ��ȡ�ղؿγ�
        /// <summary>
        /// ��ȡ�ղؿγ�
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns></returns>
        public DataTable GetFavoriteCourse(string pUserID, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetFavoriteCourse(pUserID, pPageIndex, pPageSize);
        }
        #endregion
    }
}