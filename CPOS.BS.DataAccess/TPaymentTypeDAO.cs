/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表T_Payment_Type的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TPaymentTypeDAO : Base.BaseCPOSDAO, ICRUDable<TPaymentTypeEntity>, IQueryable<TPaymentTypeEntity>
    {
        /// <summary>
        /// 为了兼容原来的代码,
        /// 其中数据来源比较乱,所以定义了参数Source来源
        /// 其中 android,iphone是从Plat中取到的,
        /// 微信端是从dataFromId中取到的,如果这个值是3那么pSource="IsJSPay"
        /// </summary>
        /// <param name="pCustomerID">客户号</param>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public TPaymentTypeEntity[] GetByCustomerBySource(string pCustomerID,string pSource)
        {
            List<TPaymentTypeEntity> list = new List<TPaymentTypeEntity> { };
            string sql = string.Format(@"select a.* from T_Payment_Type a
                                            join TPaymentTypeCustomerMapping  b on a.Payment_Type_Id=b.paymenttypeid
                                         where a.IsDelete=0 and b.isdelete=0 and b.customerid='{0}'", pCustomerID);

            //var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
            switch (pSource.ToLower())
            {
                case "android"://从
                case "iphone":
                    sql += " a.IsNativePay =1";
                    break;

                case "IsJSPay":
                    sql += " a.IsJSPay =1";
                    break;
            }

            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    TPaymentTypeEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获得支付方式列表
        /// </summary>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pAPPChannelID">通道ID</param>
        /// <returns>支付方式TPaymentTypeEntity列表</returns>
        public TPaymentTypeEntity[] GetByCustomer(string pCustomerID,string pAPPChannelID)
        {
            List<TPaymentTypeEntity> list = new List<TPaymentTypeEntity> { };
            string sql = string.Format(@"select a.* from T_Payment_Type a
                                            join TPaymentTypeCustomerMapping  b on a.Payment_Type_Id=b.paymenttypeid
                                            join SysASCAndPaymentTypeMapping  c on a.Payment_Type_Id=c.PaymentTypeId
                                            where a.IsDelete=0 and b.isdelete=0 and c.IsDelete=0
                                            and b.customerid='{0}' and c.APPChannelID='{1}'", pCustomerID,pAPPChannelID);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    TPaymentTypeEntity m;
                    this.Load(rd, out m);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }

        #region 根据客户获取对应的支付方式集合

        /// <summary>
        /// 根据客户获取对应的支付方式集合
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public DataSet GetPaymentListByCustomerId(string customerId,string pAPPChannelID)
        {
            if(string.IsNullOrEmpty(pAPPChannelID))
            {
                pAPPChannelID = "5";
            }

            string sql =string.Format(@"
            SELECT paymentTypeId = a.Payment_Type_Id 
            , paymentTypeName  = a.Payment_Type_Name 
            , displayIndex = ROW_NUMBER() OVER(ORDER BY a.Payment_Type_Name) 
            ,paymentTypeCode = Payment_Type_Code,LogoURL 
            FROM dbo.T_Payment_Type a 
            INNER JOIN dbo.TPaymentTypeCustomerMapping b ON a.Payment_Type_Id = b.PaymentTypeID 
            join SysASCAndPaymentTypeMapping  c on a.Payment_Type_Id=c.PaymentTypeId
            WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND c.IsDelete = 0 
            AND b.CustomerId = '{0}' AND c.APPChannelID={1}
",customerId,pAPPChannelID);

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 根据客户ID和支付类型ID获取对应的支付方式

        /// <summary>
        /// 根据客户ID和支付类型ID获取对应的支付方式
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="paymentTypeId">支付类型ID</param>
        /// <returns></returns>
        public DataSet GetPaymentByCustomerIdAndPaymentID(string customerId, string paymentTypeId)
        {
            string sql = string.Empty;
            sql += " SELECT a.*,b.Payment_Type_Code FROM T_Payment_Type b join TPaymentTypeCustomerMapping a on b.Payment_Type_Id = a.PaymentTypeID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0";
            sql += " AND a.PaymentTypeID = '" + paymentTypeId + "' ";
            sql += " AND a.CustomerId = '" + customerId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取收款记录集合

        public int GetCollectionRecordCount(string customerId)
        {
            string sql = GetCollectionRecordSql(customerId);
            sql += " select count(*) From #tmp ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetCollectionRecord(string customerId, int page, int pageSize)
        {
            int beginSize = (page - 1) * pageSize + 1;
            int endSize = page * pageSize;
            DataSet ds = new DataSet();
            string sql = GetCollectionRecordSql(customerId);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetCollectionRecordSql(string customerId)
        {
            string sql = string.Empty;
            sql += " SELECT totalAmount = actual_amount, ";
            sql += " orderId = b.order_id, ";
            sql += " paymentDate = CONVERT(VARCHAR(16), a.CreateTime, 120), ";
            sql += " paymentTypeName = c.Payment_Type_Name, ";
            sql += " vipId = b.vip_no, ";
            sql += " vipName = d.VipName, ";
            sql += " displayIndex = row_number() over(order BY a.CreateTime DESC) ";
            sql += " into #tmp ";
            sql += " FROM dbo.TInoutStatus a ";
            sql += " INNER JOIN dbo.T_Inout b ON a.OrderID = b.order_id ";
            sql += " INNER JOIN dbo.T_Payment_Type c ON b.Field11 = c.Payment_Type_Id ";
            sql += " INNER JOIN dbo.Vip d ON b.vip_no = d.VIPID ";
            sql += " WHERE a.IsDelete = 0 ";
            sql += " AND a.OrderStatus = '10000' ";
            sql += " AND CustomerID = '" + customerId + "' ";

            return sql;
        }
        #endregion

        #region GetPaymentTypePage
        public PagedQueryResult<TPaymentTypeEntity> GetPaymentTypePage(IWhereCondition[] pWhereConditions, int pageIndex,
            int pageSize)
        {
            StringBuilder strbWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    strbWhere.AppendFormat(" AND {0}", item.GetExpression());
                }
            }
            UtilityEntity model = new UtilityEntity();
            string sql = @"select Payment_Type_Id PaymentTypeID, Payment_Type_Code PaymentTypeCode, Payment_Type_Name PaymentTypeName,Status
                         ,PaymentCompany,PaymentItemType,LogoURL,PaymentDesc,t.CreateTime,t.LastUpdateTime
                         ,t.CreateBy,t.LastUpdateBy,t.IsDelete
                         ,(case when m.PayDeplyType=0 then 'true' else 'false' end) IsDefault
                         ,(case when m.MappingId IS NULL then 'false' else 'true' end )IsOpen 
                         ,(case when m.PayDeplyType=1 then 'true' else 'false' end) IsCustom
                         ,m.ChannelId
                         from T_Payment_Type as t
                         left join TPaymentTypeCustomerMapping as m on t.Payment_Type_Id=m.PaymentTypeID 
                         and m.CustomerId='" + this.CurrentUserInfo.ClientID + @"'
                         and m.IsDelete='0'
                         where t.IsDelete='0' ";
            model.TableName = "(" + sql + strbWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = " CreateTime desc";
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            PagedQueryResult<TPaymentTypeEntity> pEntity = new PagedQueryResult<TPaymentTypeEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<TPaymentTypeEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region 停用支付通道
        /// <summary>
        /// 停用支付通道
        /// </summary>
        /// <param name="PaymentTypeID"></param>
        public void DisablePayment(string PaymentTypeID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"
                              update TPaymentTypeCustomerMapping set IsDelete=1
                              where IsDelete=0
                                and exists(select * from TPaymentTypeCustomerMapping m
                                inner join T_Payment_Type t on t.Payment_Type_Id=m.PaymentTypeID 
                                and  m.CustomerId='{0}' 
                                and m.IsDelete=0 and t.IsDelete=0
                                where Payment_Type_Id='{1}' 
                                and m.MappingId=TPaymentTypeCustomerMapping.MappingId)", this.CurrentUserInfo.ClientID, PaymentTypeID);
            this.SQLHelper.ExecuteNonQuery(strb.ToString());
        }
        #endregion
    }
}
