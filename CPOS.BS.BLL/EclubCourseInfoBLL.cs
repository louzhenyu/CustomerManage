/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    public partial class EclubCourseInfoBLL
    {

        #region ��ѯ�γ��б�
        /// <summary>
        /// ��ѯ�γ��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList()
        {
            return _currentDAO.GetCourseInfoList();
        }
        /// <summary>
        /// ��ѯ�γ��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList_V1()
        {
            return _currentDAO.GetCourseInfoList_V1();
        }
        #endregion

        #region ZO�γ̰༶��Ա��Ϣͳ��
        /// <summary>
        /// ��ѯ�༶����Աͳ����Ϣ
        /// </summary>
        /// <param name="gradeVal">�꼶</param>
        /// <param name="courseInfoID">�༶ID</param>
        /// <param name="classInfoID">�༶ID</param>
        /// <returns>���ݼ�</returns>
        public DataTable GetCourseDetailInfo(string gradeVal, string courseInfoID, string classInfoID)
        {
            //���в���Ϊ��
            if (string.IsNullOrEmpty(gradeVal) && string.IsNullOrEmpty(courseInfoID) && string.IsNullOrEmpty(classInfoID))
            {
                return null;
            }
            return _currentDAO.GetCourseDetailInfo(gradeVal, courseInfoID, classInfoID).Tables[0];
        }
        #endregion

        #region ZO��Ա��Ϣ�ռ�ͳ��
        /// <summary>
        /// ��ѯ�༶����Աͳ����Ϣ
        /// </summary>
        /// <param name="gradeVal">�꼶</param>
        /// <param name="courseInfoID">�༶ID</param>
        /// <param name="classInfoID">�༶ID</param>
        /// <returns>���ݼ�</returns>
        public DataTable GetInfoCollect(string gradeVal, string courseInfoID, string classInfoID)
        {
            return _currentDAO.GetInfoCollect(gradeVal, courseInfoID, classInfoID).Tables[0];
        }
        #endregion
    }
}