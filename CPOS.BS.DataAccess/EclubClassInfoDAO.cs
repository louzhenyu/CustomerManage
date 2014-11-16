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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表EclubClassInfo的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubClassInfoDAO : Base.BaseCPOSDAO, ICRUDable<EclubClassInfoEntity>, IQueryable<EclubClassInfoEntity>
    {

        #region 根据课程ID查询班级列表
        /// <summary>
        /// 根据课程ID查询班级列表
        /// </summary>
        /// <param name="CourseInfoID">课程ID</param>
        /// <returns></returns>
        public DataSet GetClassInfoListByCourseInfoID(string CourseInfoID)
        {


            string sql = string.Format(@"    
select ClassInfoID,ClassInfoName from EclubClassInfo 
  where IsDelete=0 and CustomerId='{0}' and  CourseInfoID='{1}' Order by Sequence ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, CourseInfoID);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据课程ID查询班级列表
        /// </summary>
        /// <param name="CourseInfoID">课程ID</param>
        /// <returns></returns>
        public DataSet GetClassInfoListByCourseInfoID_V1(string CourseInfoID)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select CONVERT(nvarchar(50),ClassInfoID) as ID,CourseInfoName+'-'+ClassInfoName as [Text] from EclubClassInfo Cl ");
            sbSQL.Append("inner join EclubCourseInfo Co on Co.IsDelete=0 and Co.CustomerId=Cl.CustomerId and Co.CourseInfoID=Cl.CourseInfoID ");
            sbSQL.AppendFormat("where Cl.IsDelete=0 and Cl.CustomerId='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            if (!string.IsNullOrEmpty(CourseInfoID))
            {
                sbSQL.AppendFormat("and Cl.CourseInfoID='{0}' ", CourseInfoID);
            }
            sbSQL.Append("Order by Cl.Sequence ;");

            //Execute SQL
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion

    }
}
