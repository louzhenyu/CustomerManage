/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/5 18:00:39
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
    /// 数据访问： 交流记录 
    /// 表CSConversation的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CSConversationDAO : Base.BaseCPOSDAO, ICRUDable<CSConversationEntity>, IQueryable<CSConversationEntity>
    {

        #region 获取已发送客服信息的会员
        public DataSet GetMessageVipInfo(string personID,string customerId)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add( new SqlParameter( "@personID",personID));
            ls.Add(new SqlParameter("@customerId", customerId));

   //              ,(select top 1 HeadImageUrl from CSConversation x where IsCS=0 and CSMessageID=tt.CSMessageID order by x.CreateTime desc ) as VipHeadImage
   //,(select top 1 person from CSConversation x where IsCS=0 and CSMessageID=tt.CSMessageID order by x.CreateTime desc ) as VipName
            string sql = @"select *
	          ,(select top 1 HeadImgUrl from vip where vipid=tt.VipID ) as VipHeadImage
      ,(select top 1 VipName from vip where vipid=tt.VipID ) as VipName

              from (select Row_Number() OVER (partition by MemberID ORDER BY a.createtime desc) rn,a.* ,b.MemberID as VipID
	                from CSConversation a
	                inner join CSMessage b on a.CSMessageID=b.CSMessageID
	                 where 1=1 
	                 and  ((CurrentCSID=@personID) or (CurrentCSID IS NULL )
			                  OR (CurrentCSID<>@personID and datediff(minute,ConnectionTime,getdate())>60)) 
	                  and b.ClientID = @customerId)
	                  tt
	                  where rn=1
	                 order by CreateTime desc 	              ";

        
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text,sql.ToString(),ls.ToArray());
            return ds;
        }
        #endregion

    }
}
