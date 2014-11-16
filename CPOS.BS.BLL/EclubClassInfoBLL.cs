/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:55
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
    public partial class EclubClassInfoBLL
    {

        #region ���ݿγ�ID��ѯ�༶�б�
        /// <summary>
        /// ���ݿγ�ID��ѯ�༶�б�
        /// </summary>
        /// <param name="CourseInfoID">�γ�ID</param>
        /// <returns></returns>
        public DataSet GetClassInfoListByCourseInfoID(string CourseInfoID)
        {
            return _currentDAO.GetClassInfoListByCourseInfoID(CourseInfoID);
        }
         /// <summary>
        /// ���ݿγ�ID��ѯ�༶�б�
        /// </summary>
        /// <param name="CourseInfoID">�γ�ID</param>
        /// <returns></returns>
        public DataSet GetClassInfoListByCourseInfoID_V1(string CourseInfoID)
        {
            return _currentDAO.GetClassInfoListByCourseInfoID_V1(CourseInfoID);
        }
        #endregion

    }
}