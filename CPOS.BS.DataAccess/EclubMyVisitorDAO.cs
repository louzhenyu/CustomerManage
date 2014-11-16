/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/5 14:43:17
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
    /// 表EclubMyVisitor的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EclubMyVisitorDAO : Base.BaseCPOSDAO, ICRUDable<EclubMyVisitorEntity>, IQueryable<EclubMyVisitorEntity>
    {
        /// <summary>
        /// 获取我的访客信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public DataSet GetMyVisitorByUserID(string userID, string page, string pageSize)
        {
            //Build SQL Script
            StringBuilder sbVisitorInfo = new StringBuilder();
            StringBuilder sbIsShow = new StringBuilder();

            DataSet dt = null;
            using (var trans = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //Get Visitor Info
                    sbVisitorInfo.Append("select VipID,HeadImgUrl,VipName,VisitsCount,IsShow from( ");
                    sbVisitorInfo.Append("select Row_Number() over(order by ObjectID) as Row,EMV.VipID,HeadImgUrl,VipName,VisitsCount,IsShow from Vip V ");
                    sbVisitorInfo.Append("inner join EclubMyVisitor EMV on V.VIPID=EMV.ObjectID and V.ClientID=EMv.CustomerId and EMV.IsDelete=0 ");
                    sbVisitorInfo.AppendFormat("where EMV.VipID='{0}' and V.ClientID='{1}' and V.IsDelete=0 ) as Res ", userID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbVisitorInfo.AppendFormat("where Row between {0}*{1}+1 and {0}*({1}+1) ;", pageSize, page);
                    dt = this.SQLHelper.ExecuteDataset(trans, CommandType.Text, sbVisitorInfo.ToString());

                    //Viewed
                    sbIsShow.AppendFormat("update EclubMyVisitor set IsShow=0,LastUpdateBy='{0}',LastUpdateTime=GETDATE() ", CurrentUserInfo.CurrentLoggingManager.User_Name);
                    sbIsShow.AppendFormat("where CustomerId='{0}' and IsDelete=0 and Exists( ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbIsShow.Append("select ObjectID from (");
                    sbIsShow.Append("select Row_Number() over(order by ObjectID) as Row,ObjectID ");
                    sbIsShow.Append("from EclubMyVisitor ");
                    sbIsShow.AppendFormat("where VipID='{0}' and CustomerId='{1}' and IsDelete=0 and ObjectID=ObjectID and VipID=VipID) as Res ", userID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                    sbIsShow.AppendFormat("where Row between {0}*{1}+1 and {0}*({1}+1) ", pageSize, page);
                    sbIsShow.Append(");");
                    this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbIsShow.ToString());

                    //Commit Transaction
                    trans.Commit();
                }
                catch
                {
                    //Roll Back Transaction
                    trans.Rollback();
                    throw;
                }
            }
            return dt;
        }
    }
}
