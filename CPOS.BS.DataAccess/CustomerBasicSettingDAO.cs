/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 16:10:52
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
    /// 表CustomerBasicSetting的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerBasicSettingDAO : Base.BaseCPOSDAO, ICRUDable<CustomerBasicSettingEntity>, IQueryable<CustomerBasicSettingEntity>
    {
        public CustomerBasicSettingEntity[] GetBasicSettings(string pCustomerID, string pCode)
        {
            List<CustomerBasicSettingEntity> list = new List<CustomerBasicSettingEntity> { };
            List<SqlParameter> paras = new List<SqlParameter> {
                new SqlParameter(){ ParameterName="@SettingCode", SqlDbType= SqlDbType.NVarChar, Value=pCode},
                new SqlParameter(){ ParameterName="@CustomerID", SqlDbType= SqlDbType.NVarChar, Value=pCustomerID}
            };
            string sql = "select * from CustomerBasicSetting where isdelete=0 and SettingCode=@SettingCode and CustomerID=@CustomerID";
            using (var rd = this.SQLHelper.ExecuteReader(CommandType.Text, sql, paras.ToArray()))
            {
                while (rd.Read())
                {
                    CustomerBasicSettingEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        #region GetCustomerInfo
        /// <summary>
        /// 根据客户ID获取客户信息
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public DataSet GetCustomerInfo(string customer_id)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"select  customer_id,customer_code,customer_name from [cpos_ap]..[t_customer] where 
               customer_id='{0}'", customer_id);
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }

        #endregion

        #region GetCustomerBasicSettingByKey
        /// <summary>
        /// 根据客户ID获取信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet GetCustomerBasicSettingByKey(string customerID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"select SettingCode,SettingDesc,SettingValue  from CustomerBasicSetting where CustomerID='{0}' and IsDelete='0'", customerID);
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region SaveustomerBasicrInfo
        public int SaveustomerBasicrInfo(List<CustomerBasicSettingEntity> list)
        {
            var tran = this.SQLHelper.CreateTransaction();
            int i = 0;
            using (tran.Connection)
            {
                try
                {
                    StringBuilder strb = new StringBuilder();
                    foreach (CustomerBasicSettingEntity item in list)
                    {
                        strb.AppendFormat(@"
                        if (select count(1) from CustomerBasicSetting where CustomerID='{0}' and IsDelete='0' and SettingCode='{1}')>0
                        begin
                          update CustomerBasicSetting set SettingValue='{2}' where  CustomerID='{0}' and IsDelete='0' and SettingCode='{1}'
                        end
                        else
                           begin
                         insert into CustomerBasicSetting(SettingID,CustomerID,SettingCode,SettingValue,IsDelete)
                         values( Newid(),'{0}','{1}','{2}','0')
                        end", this.CurrentUserInfo.ClientID, item.SettingCode, item.SettingValue);
                    }
                    i = this.SQLHelper.ExecuteNonQuery(strb.ToString());
                    tran.Commit();
                }
                catch
                {

                    tran.Rollback();
                    throw;
                }
            }
            return 1;

        }
        #endregion

        #region MyRegion
        public DataSet GetCousCustomerType()
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"  select UnitSortId,SortName  from TUnitSort where IsDelete='0'");
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion


        /// <summary>
        /// 1.获取客户基本信息(数据库表中配置。关于我们。品牌故事，品牌相关)。2获取图片集合
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerBaiscSettingInfo(string customerId)
        {

            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId } };
            StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append("select SettingCode,SettingValue from customerBasicsetting where isdelete = 0 and customerId = @pCustomerId and SettingCode in ('BrandRelated','AboutUs','BrandStory','IntegralAmountPer','SMSSign','ForwardingMessageLogo','ForwardingMessageTitle','ForwardingMessageSummary','WhatCommonPoints','GetPoints','SetSalesPoints','ImageList');");
            sbSQL.Append("select b.* From t_unit	 a inner join ObjectImages b on (a.unit_id = b.ObjectId) where a.type_id = '2F35F85CF7FF4DF087188A7FB05DED1D' and a.status = '1' and b.IsDelete = '0' and customer_id =@pCustomerId order by DisplayIndex");
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sbSQL.ToString(), paras.ToArray());
        }
        /// <summary>
        /// Redis中没有数据时 执行该方法
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerBaiscSettingInfoRedisBackUp(string customerId)
        {

            var paras = new List<SqlParameter> { new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId } };
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select SettingCode,SettingValue from customerBasicsetting where isdelete = 0 and customerId = @pCustomerId and SettingCode in ('BrandRelated','AboutUs','BrandStory','IntegralAmountPer','SMSSign','ForwardingMessageLogo','ForwardingMessageTitle','ForwardingMessageSummary','WhatCommonPoints','GetPoints','SetSalesPoints','ImageList');");
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sbSQL.ToString(), paras.ToArray());
        }
        #region IsAld
        public string GetIsAld()
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@" select isALD from cpos_ap.dbo.t_customer tc 
                                 left join cpos_ap.dbo.TCustomerDataDeployMapping tm on tc.customer_id=tm.CustomerId

                                   where tm.IsDelete=0 and tc.customer_id='{0}'", this.CurrentUserInfo.ClientID);
            return this.SQLHelper.ExecuteScalar(strb.ToString()).ToString();

        }
        #endregion
    }
}
