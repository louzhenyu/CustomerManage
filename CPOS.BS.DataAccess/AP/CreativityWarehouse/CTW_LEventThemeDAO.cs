/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    /// ���ݷ��ʣ�  
    /// ��T_CTW_LEventTheme�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CTW_LEventThemeDAO : Base.BaseCPOSDAO, ICRUDable<CTW_LEventThemeEntity>, IQueryable<CTW_LEventThemeEntity>
    {
          public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;

        public CTW_LEventThemeDAO(LoggingSessionInfo pUserInfo, string connectionString)
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
        public DataSet GetThemeInfo(string strTemplateId)
        {
            string strSql = string.Format(@"SELECT *,b.ImageURL ,C.ActivityGroupId,[TemplateName]
                                                FROM [dbo].[T_CTW_LEventTheme] a 
                                                LEFT JOIN dbo.ObjectImages b ON a.ImageId=b.ImageId 
                                                LEFT JOIN [dbo].[T_CTW_LEventTemplate] C ON A.[TemplateId]=C.[TemplateId]
                                                WHERE a.IsDelete=0 and A.TemplateId='{0}' Order by  a.createtime asc", strTemplateId);
            return staticSqlHelper.ExecuteDataset(strSql);
        }
    }
}
