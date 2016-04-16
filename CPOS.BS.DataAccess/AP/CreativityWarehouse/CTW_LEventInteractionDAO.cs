/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 18:04:05
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表T_CTW_LEventInteraction的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CTW_LEventInteractionDAO : Base.BaseCPOSDAO, ICRUDable<CTW_LEventInteractionEntity>, IQueryable<CTW_LEventInteractionEntity>
    {
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;

        public CTW_LEventInteractionDAO(LoggingSessionInfo pUserInfo, string connectionString)
            : base(pUserInfo)
        {
            this.StaticConnectionString = connectionString;
            this.SQLHelper = StaticSqlHelper;
        }
        protected ISQLHelper StaticSqlHelper
        {
            get
            {
                if (null == staticSqlHelper)
                    staticSqlHelper = new DefaultSQLHelper(StaticConnectionString);
                return staticSqlHelper;
            }
        }
        /// <summary>
        /// 根据模版id获取互动方式
        /// </summary>
        /// <param name="strTemplateId"></param>
        /// <returns></returns>
        public DataSet GetEventInteractionByTemplateId(string strTemplateId)
        {
            DataSet ds = null;
            string strSql = @"SELECT 
                                b.InteractionType
                                ,CASE  
                                        WHEN b.InteractionType=1 THEN '吸粉' 
                                        WHEN b.InteractionType=2 THEN '促销' 
                                END InteractionTypeName
                                ,a.TemplateId
                                ,a.ThemeId
                                ,a.ThemeName
                                ,a.H5Url
                                ,a.H5TemplateId
                                ,b.LeventId
                                ,c.DrawMethodCode
                                ,d.ActivityGroupId ,
                                d.ImageURL
                                FROM T_CTW_LEventTheme a
                                    LEFT JOIN T_CTW_LEventInteraction b on a.ThemeId=b.ThemeId
                                    LEFT JOIN T_CTW_LEventDrawMethod c  on b.DrawMethodId=c.DrawMethodId
                                    LEFT JOIN ( SELECT  ActivityGroupId ,
                                                TemplateId ,
                                                o.ImageURL
                                        FROM    T_CTW_LEventTemplate a
                                                LEFT JOIN dbo.ObjectImages o ON a.ImageId = o.ImageId
                                      ) d ON a.TemplateId = d.TemplateId
                                where a.TemplateId=@strTemplateId  and a.IsDelete=0
                                order by a.createtime asc
                        ";
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@strTemplateId", Value = strTemplateId });
            ds=staticSqlHelper.ExecuteDataset(CommandType.Text, strSql, paras.ToArray());
            return ds;
        }
    }
}
