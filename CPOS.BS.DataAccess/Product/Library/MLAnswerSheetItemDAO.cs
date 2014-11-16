/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:34
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
    /// 表MLAnswerSheetItem的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MLAnswerSheetItemDAO : Base.BaseCPOSDAO, ICRUDable<MLAnswerSheetItemEntity>, IQueryable<MLAnswerSheetItemEntity>
    {
        #region 获取答题列表
        /// <summary>
        /// 获取答题列表
        /// </summary>
        /// <param name="pAnswerSheetId"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataTable GetAnswerSheetItem(string pAnswerSheetId, string pUserID)
        {
            string sql = "SELECT AnswerSheetItemId,UserId,mlas.AnswerSheetId,quq.QuestionId,quq.QuestionType,DisplayIndexNo,Answer,IsCorrect,mlas.Score";
            sql += " FROM MLAnswerSheet AS mlas INNER JOIN MLAnswerSheetItem AS mlasi ON mlas.AnswerSheetId=mlasi.AnswerSheetId";
            sql += " INNER JOIN QuesQuestions AS quq on quq.QuestionID=mlasi.QuestionId";
            sql += " WHERE mlas.AnswerSheetId=@AnswerSheetId AND mlas.UserId=@UserID AND mlas.IsDelete=0 AND mlasi.IsDelete=0";
            sql += " ORDER BY DisplayIndexNo";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@AnswerSheetId", pAnswerSheetId));
            param.Add(new SqlParameter("@UserID", pUserID));
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
