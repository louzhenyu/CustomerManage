/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/4 15:08:10
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
    /// 表EclubPageInfo的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubPageInfoDAO : Base.BaseCPOSDAO, ICRUDable<EclubPageInfoEntity>, IQueryable<EclubPageInfoEntity>
    {
        /// <summary>
        /// 浏览历史
        /// </summary>
        /// <param name="userID">当前登录用户ID</param>
        /// <param name="alumniID">校友ID</param>
        /// <param name="PageCode">页编码</param>
        /// <param name="footType">0，其他，1.资讯,  2.视频 ,3.活动, 4.课程, 5.校友</param>
        /// <param name="operationType">1.查询，2.修改，3.新增,4.删除,5登陆，6收藏</param>
        public void BrowsingHistoryInfo(string userID, string alumniID, string PageCode, int footType, int operationType)
        {
            //Temp Variant
            string customerId = CurrentUserInfo.CurrentLoggingManager.Customer_Id;
            string userName = CurrentUserInfo.CurrentLoggingManager.User_Name;

            //Create Info
            StringBuilder sbVisitor = new StringBuilder();
            StringBuilder sbFootPrint = new StringBuilder();
            StringBuilder sbCustomPage = new StringBuilder();
            StringBuilder sbPageInfo = new StringBuilder();

            //获取PageInfoID
            sbPageInfo.Append("select PageInfoID from EclubPageInfo ");
            sbPageInfo.AppendFormat("where PageCode='{0}' and CustomerId='{1}' and IsDelete =0 ;", PageCode, customerId);

            var pageInfoID = this.SQLHelper.ExecuteScalar(sbPageInfo.ToString()) ?? null;
            if (pageInfoID == null)
            {
                return;
            }

            //Create Transaction
            using (var trans = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //Visitor
                    sbVisitor.Append("if not Exists(select top 1 1 from dbo.EclubMyVisitor ");
                    sbVisitor.AppendFormat("where VipID='{0}' and ObjectID='{1}' and IsDelete =0 and CustomerId='{2}') ", alumniID, userID, customerId);
                    sbVisitor.Append("begin ");
                    sbVisitor.Append("insert into dbo.EclubMyVisitor(VipID, ObjectID, VisitsCount,IsShow, CustomerId, CreateBy,LastUpdateBy) ");
                    sbVisitor.AppendFormat("values('{0}', '{1}', 1,1, '{2}', '{3}','{3}' ) ", alumniID, userID, customerId, userName);
                    sbVisitor.Append("end ");
                    sbVisitor.Append("else ");
                    sbVisitor.Append("begin ");
                    sbVisitor.Append("update dbo.EclubMyVisitor set VisitsCount=VisitsCount+1,IsShow = 1,LastUpdateTime=GETDATE(),LastUpdateBy='' ");
                    sbVisitor.AppendFormat("where VipID='{0}' and ObjectID='{1}' and IsDelete =0 and CustomerId='{2}' ", alumniID, userID, customerId);
                    sbVisitor.Append("end ");

                    //Foot Print                   
                    sbFootPrint.Append("insert into dbo.EclubMyFootPrint(PageInfoID, VipID, PageDate, ObjectID, CustomerId, CreateBy,LastUpdateBy,FootType,OperationType) ");
                    sbFootPrint.AppendFormat("values('{0}', '{1}', GETDATE(), '{2}', '{3}', '{4}','{4}',{5},{6}) ", pageInfoID, userID, alumniID, customerId, userName, footType, operationType);

                    //Customer
                    sbCustomPage.Append("update dbo.EclubPageInfo set BrowseCount=BrowseCount + 1 ");
                    sbCustomPage.AppendFormat("where PageCode='{0}' and IsDelete =0 and CustomerId='{1}' ;", PageCode, customerId);

                    //Execute SQL Script
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbVisitor.ToString());
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbFootPrint.ToString());
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbCustomPage.ToString());

                    //Commit
                    trans.Commit();
                }
                catch
                {
                    //Rollback
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
