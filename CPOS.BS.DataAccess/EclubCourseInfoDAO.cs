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
    /// ���ݷ��ʣ�  
    /// ��EclubCourseInfo�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubCourseInfoDAO : Base.BaseCPOSDAO, ICRUDable<EclubCourseInfoEntity>, IQueryable<EclubCourseInfoEntity>
    {

        #region ��ѯ�γ��б�
        /// <summary>
        /// ��ѯ�γ��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList()
        {


            string sql = string.Format(@"    
select CourseInfoID,CourseInfoName from EclubCourseInfo 
  where IsDelete=0 and CustomerId='{0}' Order by Sequence ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// ��ѯ�γ��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCourseInfoList_V1()
        {


            string sql = string.Format(@"    
select CONVERT(nvarchar(50),CourseInfoID) as ID,CourseInfoName as Text from EclubCourseInfo 
  where IsDelete=0 and CustomerId='{0}' Order by Sequence ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            return this.SQLHelper.ExecuteDataset(sql);
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
        public DataSet GetCourseDetailInfo(string gradeVal, string courseInfoID, string classInfoID)
        {
            //Instance Append Object
            StringBuilder sbSQL = new StringBuilder();

            //Append SQL Script
            sbSQL.Append("select Cl.ClassInfoID as ClID,CourseInfoName as CoName,ClassInfoName as ClName,COUNT(distinct V.VIPID) as TotalCo ,COUNT(V.Col37) as UploadCo from Vip V ");
            sbSQL.Append("inner join EclubVipClassMapping VM on VM.VipID=V.VipID and VM.IsDelete=0 and VM.CustomerId=V.ClientID ");
            sbSQL.Append("inner join EclubClassInfo Cl on Cl.IsDelete=0 and Cl.CustomerId=VM.CustomerId and Cl.ClassInfoID=VM.ClassInfoID ");
            sbSQL.Append("inner join EclubCourseInfo Co on Co.IsDelete=0 and Co.CustomerId=Cl.CustomerId and Co.CourseInfoID=Cl.CourseInfoID ");
            sbSQL.AppendFormat("where V.IsDelete=0 and Cl.CustomerId='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            //Judge Condition
            if (!string.IsNullOrEmpty(courseInfoID))
            {
                sbSQL.AppendFormat("and Co.CourseInfoID='{0}' ", courseInfoID);
            }
            if (!string.IsNullOrEmpty(classInfoID))
            {
                sbSQL.AppendFormat("and Cl.ClassInfoID='{0}' and V.Col37 is not null ", classInfoID);
            }
            if (!string.IsNullOrEmpty(gradeVal))
            {
                sbSQL.AppendFormat("and Cl.ClassInfoName like '%{0}%' ", gradeVal);
            }
            sbSQL.Append("group by CourseInfoName,ClassInfoName,Cl.ClassInfoID ");
            sbSQL.Append("order by CourseInfoName,ClassInfoName; ");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
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
        public DataSet GetInfoCollect(string gradeVal, string courseInfoID, string classInfoID)
        {
            //Instance Append Object
            StringBuilder sbSQL = new StringBuilder();

            //Append SQL Script
            sbSQL.Append("select Cl.ClassInfoID,CourseInfoName,ClassInfoName,Cl.Sequence,COUNT(distinct InfoCollectID) as TotalCount,COUNT(Substance) as UploadCount from EclubInfoCollect IC ");
            sbSQL.Append("right join EclubClassInfo Cl on Cl.IsDelete=0 and Cl.CustomerId=IC.CustomerId and Cl.ClassInfoID=IC.ClassInfoID ");
            sbSQL.Append("inner join EclubCourseInfo Co on Co.IsDelete=0 and Co.CustomerId=Cl.CustomerId and Co.CourseInfoID=Cl.CourseInfoID ");
            sbSQL.AppendFormat("where (IC.IsDelete=0 or IC.IsDelete is null) and Cl.CustomerId='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            //Judge Condition
            if (!string.IsNullOrEmpty(courseInfoID))
            {
                sbSQL.AppendFormat("and Co.CourseInfoID='{0}' ", courseInfoID);
            }
            if (!string.IsNullOrEmpty(classInfoID))
            {
                sbSQL.AppendFormat("and Cl.ClassInfoID='{0}' ", classInfoID);
            }
            if (!string.IsNullOrEmpty(gradeVal))
            {
                sbSQL.AppendFormat("and Cl.Grade = '{0}' ", gradeVal);
            }
            sbSQL.Append("group by CourseInfoName,ClassInfoName,Cl.ClassInfoID,Cl.Sequence ");
            sbSQL.Append("order by Cl.Sequence,CourseInfoName,ClassInfoName; ");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion
    }
}
