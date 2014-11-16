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
    /// 数据访问：  
    /// 表QuesOption的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class QuesOptionDAO : Base.BaseCPOSDAO, ICRUDable<QuesOptionEntity>, IQueryable<QuesOptionEntity>
    {
        #region 获取考卷考题选项
        /// <summary>
        /// 获取考卷考题选项
        /// </summary>
        /// <param name="pSurveyTestId"></param>
        /// <returns></returns>
        public DataTable GetQuesOptions(string pQuestionID)
        {
            string sql = "SELECT QuestionId,DisplayIndex,OptionIndex,OptionsText,IsAnswer,OptionMedia,MediaType";
            sql += " FROM QuesOption WHERE QuestionID=@QuestionID AND IsDelete=0 ORDER BY DisplayIndex ";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@QuestionID", pQuestionID));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion
    }
}
