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
    /// 数据访问：  
    /// 表T_CTW_LEventTemplate的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventTemplateDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventTemplateEntity>, IQueryable<T_CTW_LEventTemplateEntity>
    {
        public string StaticConnectionString { get; set; }
        private ISQLHelper staticSqlHelper;

        public T_CTW_LEventTemplateDAO(LoggingSessionInfo pUserInfo, string connectionString)
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
        /// 获取创意仓库主题列表信息（包括主题列表，计划列表，Banner列表）
        /// </summary>
        /// <returns></returns>
        public DataSet GetTemplateList(string strActivityGroupCode)
        {
            DataSet ds = new DataSet();
            string strSql =string.Format(@"
                                SELECT  AdId ,
                                        a.ActivityGroupId,
                                        b.[ActivityGroupCode] ,
                                        b.Name,
                                        a.TemplateId ,
                                        c.ImageURL ,
                                        BannerUrl ,
                                        BannerName ,
                                        a.DisplayIndex ,
                                        Status,
										  ( SELECT TOP 1
                                                            RCodeUrl
                                                  FROM      T_CTW_LEventTheme theme
                                                  WHERE     theme.TemplateId = a.TemplateId
                                                            AND RCodeUrl IS NOT NULL
                                                  order by theme.createtime asc
                                                )QRCodeUrl
                                         ,
                                        (ISNULL(temp.Usecount,0)+ISNULL(temp.ClickCount,0)) as UserCount
                                FROM    T_CTW_Banner a
                                        INNER JOIN T_CTW_LEventTemplate temp ON a.TemplateId=temp.TemplateId and temp.IsDelete=0
                                        LEFT JOIN SysMarketingGroupType b ON a.ActivityGroupId = b.ActivityGroupId
                                        LEFT JOIN dbo.ObjectImages c ON a.BannerImageId = c.ImageId
                                WHERE   a.IsDelete = 0 and Status=30
                                ORDER BY a.DisplayIndex

                                SELECT  tem.TemplateId ,
                                        tem.TemplateName ,
                                        Img.ImageURL ,
                                        CASE WHEN tem.TemplateStatus = 10 THEN '待上架'
                                             WHEN tem.TemplateStatus = 20 THEN '待发布'
                                             WHEN tem.TemplateStatus = 30 THEN '已发布'
                                             WHEN tem.TemplateStatus = 40 THEN '已下架'
                                        END TemplateStatus ,
                                        b.ActivityGroupCode,
                                        b.ActivityGroupId,
                                        ( SELECT TOP 1
                                                            RCodeUrl
                                                  FROM      T_CTW_LEventTheme theme
                                                  WHERE     theme.TemplateId = Tem.TemplateId
                                                            AND RCodeUrl IS NOT NULL
                                                  order by theme.createtime asc
                                                )RCodeUrl,
                                        (ISNULL(tem.Usecount,0)+ISNULL(tem.ClickCount,0)) as UserCount
                                FROM    T_CTW_LEventTemplate Tem
                                        
                                        LEFT JOIN dbo.ObjectImages Img ON Tem.ImageId = Img.ImageId
                                        INNER JOIN SysMarketingGroupType b ON Tem.ActivityGroupId = b.ActivityGroupId and (b.ActivityGroupCode='{0}' or '{0}'='')
                                WHERE   Tem.IsDelete = 0 AND TemplateStatus=30 
                                ORDER BY Tem.DisplayIndex ASC

                                SELECT  
                                       CONVERT(NVARCHAR(10),  PlanDate,120) PlanDate ,
                                        PlanName ,
                                        DisplayIndex 
                                FROM    T_CTW_SeasonPlan
                                WHERE IsDelete =0
                                ORDER BY PlanDate ASC
                                 
                                SELECT O.ImageURL PlanImageUrl
                                FROM T_CTW_HomePageCommon H
                                INNER JOIN ObjectImages O ON H.ImageId=O.ImageId
                                WHERE h.IsDelete=0  AND SetType='SeasonPlan'

                                ", strActivityGroupCode);
            ds = staticSqlHelper.ExecuteDataset(strSql);
            return ds;
        }
        /// <summary>
        /// 更新创意仓库活动模板的参与人数数据
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <param name="pType">1=UseCount更新,2=ClickCount更新</param>
        /// <returns></returns>
        public int UpdateTemplateInfo(string pTemplateId, int pType)
        {
            string strSql = string.Empty;
            if (pType == 1)
            {
                strSql = @" update T_CTW_LEventTemplate  set LastUpdateBy=@UserID,LastUpdateTime=getdate(),UseCount=ISNULL(UseCount,0)+1 where TemplateId=@TemplateId ";
            }
            else
            {
                strSql = @" update T_CTW_LEventTemplate  set LastUpdateBy=@UserID,LastUpdateTime=getdate(),ClickCount=ISNULL(ClickCount,0)+1 where TemplateId=@TemplateId ";
            }
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@UserID",this.CurrentUserInfo.UserID),
                new SqlParameter("@TemplateId",pTemplateId)
            };
            int result = this.staticSqlHelper.ExecuteNonQuery(CommandType.Text, strSql, parameter);
            return result;
        }
    }
}
