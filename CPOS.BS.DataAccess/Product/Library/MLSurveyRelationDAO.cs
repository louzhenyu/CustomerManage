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
    /// ��MLSurveyRelation�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MLSurveyRelationDAO : Base.BaseCPOSDAO, ICRUDable<MLSurveyRelationEntity>, IQueryable<MLSurveyRelationEntity>
    {
        #region ��ȡ�γ̹����Ŀ���
        /// <summary>
        /// ��ȡ�γ̹����Ŀ���
        /// </summary>
        /// <param name="pOnlineCourseId"></param>
        /// <returns></returns>
        public DataTable GetSurveyTestInfo(string pOnlineCourseId)
        {
            string sql = "SELECT QuestionnaireID AS SurveyTestId,QuestionnaireDesc AS SurveyTestDesc,Remark AS SurveyTestRemark,SurfaceImage";
            sql += " ,PassScore,QuestionnaireName,ReleaseState,mloc.ReleaseTime,StartTime,EndTime,Redoable FROM MLSurveyRelation AS mlsr ";
            sql += " INNER JOIN MLOnlineCourse AS mloc ON mloc.OnlineCourseId=mlsr.OnlineCourseId";
            sql += " INNER JOIN Questionnaire AS q ON q.QuestionnaireID=mlsr.SurveyTestId";
            sql += " WHERE mloc.OnlineCourseId=@OnlineCourseId AND mloc.CustomerID=@CustomerID AND mloc.IsDelete=0 AND q.ReleaseState=1";
            sql += " AND mloc.IsDelete=0 AND mlsr.IsDelete=0 AND q.IsDelete=0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@OnlineCourseId", pOnlineCourseId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
