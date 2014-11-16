/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    /// 表WiFiUserVisit的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WiFiUserVisitDAO : Base.BaseCPOSDAO, ICRUDable<WiFiUserVisitEntity>, IQueryable<WiFiUserVisitEntity>
    {

        #region  查询对应时间内的人数
        /// <summary>
        /// 查询对应时间内的人数
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="minMinute">最小分钟</param>
        /// <param name="maxMinute">最大分钟</param>
        /// <returns></returns>
        public int GetVipNum(string unitID, int minMinute, int maxMinute)
        {
            string sql = string.Format(@"
  select count(*) from WiFiUserVisit where UnitID='{0}' 
  and CustomerID='{1}' 
  and IsDelete=0 
  and convert(varchar(10),CurrentDate,120)=convert(varchar(10),getdate(),120) 
  and datediff(mi,BeginTime,ISNULL(EndTime,getdate()))>={2} ", unitID, CurrentUserInfo.CurrentLoggingManager.Customer_Id, minMinute);

            if (maxMinute != -1)
            {
                sql += string.Format(" and datediff(mi,BeginTime,ISNULL(EndTime,getdate()))<{0} ", maxMinute);
            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region  查询全部或当前人数
        /// <summary>
        /// 查询全部或当前人数
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="isNowCount">是否是计算当前人数</param>
        /// <returns></returns>
        public int GetVipNumAllOrNow(string unitID, bool isNowCount)
        {
            string sql = string.Format(@"
  select count(*) from WiFiUserVisit where UnitID='{0}' 
  and CustomerID='{1}' 
  and IsDelete=0 
  and convert(varchar(10),CurrentDate,120)=convert(varchar(10),getdate(),120) ", unitID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            if (isNowCount)
            {
                sql += " and EndTime is null ";
            }

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region  分页查询当前人数信息
        /// <summary>
        /// 分页查询当前人数信息
        /// </summary>
        /// <param name="unitID">门店ID</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetVipListByPage(string unitID, int PageIndex, int PageSize)
        {

            StringBuilder sbSQL = new StringBuilder();
            string strSql = string.Format(@"
  select P.HeadImgUrl as VipPhoto,
		 P.VipName,
		 D.LocationDesc as VipArea,
		 cast(datediff(mi,V.BeginTime,getdate()) as varchar(20)) as VipTime 
		 from WiFiUserVisit V 
  left join WiFiDevice D 
  on V.CurrentDeviceID=D.DeviceID and V.IsDelete=D.IsDelete 
  left join Vip P 
  on V.VIPID=P.VIPID and V.IsDelete=P.IsDelete  
  where V.UnitID='{0}' 
  and V.CustomerID='{1}' 
  and V.IsDelete=0 
  and convert(varchar(10),V.CurrentDate,120)=convert(varchar(10),getdate(),120) 
  and V.EndTime is null", unitID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);


            sbSQL.AppendLine(string.Format(@"select count(*) from ({0}) T", strSql));
            sbSQL.AppendLine(string.Format("select * from (select row_number()over(order by VipTime asc) _row,* from ({2}) ABC) t where t._row>{0}*{1} and t._row<=({0}+1)*{1}",
                 (Convert.ToInt32(PageIndex) - 1) < 0 ? 0 : (Convert.ToInt32(PageIndex)), PageSize, strSql));
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion

        #region  返回门店某个会员详细
        /// <summary>
        /// 返回门店某个会员详细
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetVipDetailList(string strWhere)
        {


            string sql = string.Format(@"    
  select C.customer_name as CustomerName,
		 U.unit_name as UnitName,
		 D.LocationDesc,
         P.HeadImgUrl as VipPhoto,
		 P.VipName,
		 P.WeiXin 
		 from WiFiUserVisit V 
  left join WiFiDevice D 
  on V.CurrentDeviceID=D.DeviceID and V.IsDelete=D.IsDelete  
  left join Vip P 
  on V.VIPID=P.VIPID and V.IsDelete=P.IsDelete  
  left join t_unit U 
  on V.UnitID=U.unit_id 
  left join cpos_ap..t_customer C 
  on V.CustomerID=C.customer_id 
  where V.IsDelete=0 
  and convert(varchar(10),V.CurrentDate,120)=convert(varchar(10),getdate(),120) 
  and V.EndTime is null 
  and V.CustomerID='{0}' {1} ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, strWhere);

            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region 判断条件是否存在数据
        /// <summary>
        /// 判断条件是否存在数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExists(string strWhere)
        {

            string sql = string.Format(@"
  select count(*) from WiFiUserVisit where CustomerID='{0}' 
  and IsDelete=0 
  and convert(varchar(10),CurrentDate,120)=convert(varchar(10),getdate(),120) {1} ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, strWhere);
                return Convert.ToInt32(SQLHelper.ExecuteScalar(sql)) > 0 ? true : false;

        }
        #endregion

        #region  根据条件获取实例
        /// <summary>
        /// 根据条件获取实例
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public WiFiUserVisitEntity GetByWhere(string strWhere)
        {
            //参数检查
            if (string.IsNullOrEmpty(strWhere))
                return null;
            //组织SQL
            string sql = string.Format(@"
  select * from WiFiUserVisit where CustomerID='{0}' 
  and IsDelete=0 
  and convert(varchar(10),CurrentDate,120)=convert(varchar(10),getdate(),120) {1} ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, strWhere);

            //读取数据
            WiFiUserVisitEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
        #endregion

    }
}
