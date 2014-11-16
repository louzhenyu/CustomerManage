/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/29 10:34:16
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
    /// 表LEventsWX的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventsWXDAO : Base.BaseCPOSDAO, ICRUDable<LEventsWXEntity>, IQueryable<LEventsWXEntity>
    {
        public string GetEventsWXCode(string EventId)
        {
            string sql = "select MAX(wxcode) WeiXinUnitCode From LEventsWX a "
                    + " inner join LEvents b on (a.EventId = b.EventId) "
                    + " where a.IsDelete = '0' "
                    + " and b.CustomerId = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' "
                    + " and a.WXCode is not null "
                    + " and a.WXCode <> '' "
                    + " group by b.CustomerId";
            return Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 添加活动和问卷的关系
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <param name="pMobileModuleID">问卷ID</param>
        /// <returns></returns>
        public int CreateMobileModuleObjectMapping(string pEventID, string pMobileModuleID)
        {
            string sql = @"                       
                         update MobileModuleObjectMapping set IsDelete=1,LastUpdateBy='{1}',LastUpdateTime=GETDATE()
                         where ObjectID='{2}' and IsDelete=0 and CustomerID='{0}';";
            if (!string.IsNullOrEmpty(pMobileModuleID))
            {
                sql = @"
                         if not  exists (select * from MobileModuleObjectMapping  where  ObjectID='{2}' and IsDelete=0 and CustomerID='{0}' and MobileModuleID='{3}')
                         begin 
                         update MobileModuleObjectMapping set IsDelete=1,LastUpdateBy='{1}',LastUpdateTime=GETDATE()
                         where ObjectID='{2}' and IsDelete=0 and CustomerID='{0}';
                         insert MobileModuleObjectMapping(ObjectType,ObjectID,MobileModuleID,CustomerID,IsDelete)
                         values(1,'{2}','{3}','{0}',0);
                        end";
            }
            sql = string.Format(sql, this.CurrentUserInfo.ClientID, this.CurrentUserInfo.UserID, pEventID, pMobileModuleID);
            return this.SQLHelper.ExecuteNonQuery(sql);
        }
        
        public int CreateObjectImages(string pEventID, string pImageURL)
        {

            string sql = @"if not exists (select  * from ObjectImages where CustomerId='{0}' and IsDelete=0 and ObjectId='{1}' 
                                and ImageURL='{2}' )
                                begin 
                                update ObjectImages set IsDelete=1,LastUpdateBy='{3}',LastUpdateTime=GETDATE() where 
                                CustomerId='{0}' and IsDelete=0 and ObjectId='{1}' 
                                insert ObjectImages(ImageId,ObjectId,ImageURL,DisplayIndex,IsDelete,CustomerId)
                                values(newid(),'{1}','{2}','1','0','{0}')
                                end ";
            sql = string.Format(sql, this.CurrentUserInfo.ClientID, pEventID, pImageURL, this.CurrentUserInfo.UserID);
            return this.SQLHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询MobileModuleID
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <returns>MobileModuleID</returns>
        public string GetMobileModuleID(string pEventID)
        {
            string sql = @"select MobileModuleID from MobileModuleObjectMapping where ObjectID='{0}' and IsDelete=0 and CustomerID='{1}' ";
            sql = string.Format(sql, pEventID, this.CurrentUserInfo.ClientID);
            return Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
        }
    }
}
