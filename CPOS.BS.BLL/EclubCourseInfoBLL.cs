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
    /// 业务处理：  
    /// </summary>
    public partial class EclubCourseInfoBLL
    {

        #region 查询课程列表
        /// <summary>
        /// 查询课程列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList()
        {
            return _currentDAO.GetCourseInfoList();
        }
        /// <summary>
        /// 查询课程列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList_V1()
        {
            return _currentDAO.GetCourseInfoList_V1();
        }
        #endregion

        #region ZO课程班级人员信息统计
        /// <summary>
        /// 查询班级及人员统计信息
        /// </summary>
        /// <param name="gradeVal">年级</param>
        /// <param name="courseInfoID">班级ID</param>
        /// <param name="classInfoID">班级ID</param>
        /// <returns>数据集</returns>
        public DataTable GetCourseDetailInfo(string gradeVal, string courseInfoID, string classInfoID)
        {
            //所有参数为空
            if (string.IsNullOrEmpty(gradeVal) && string.IsNullOrEmpty(courseInfoID) && string.IsNullOrEmpty(classInfoID))
            {
                return null;
            }
            return _currentDAO.GetCourseDetailInfo(gradeVal, courseInfoID, classInfoID).Tables[0];
        }
        #endregion

        #region ZO人员信息收集统计
        /// <summary>
        /// 查询班级及人员统计信息
        /// </summary>
        /// <param name="gradeVal">年级</param>
        /// <param name="courseInfoID">班级ID</param>
        /// <param name="classInfoID">班级ID</param>
        /// <returns>数据集</returns>
        public DataTable GetInfoCollect(string gradeVal, string courseInfoID, string classInfoID)
        {
            return _currentDAO.GetInfoCollect(gradeVal, courseInfoID, classInfoID).Tables[0];
        }
        #endregion
    }
}