/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:03
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
    /// ���ݷ��ʣ� ��Ϊ�����˫���� 
    /// ��SysRetailRewardRule�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysRetailRewardRuleDAO : Base.BaseCPOSDAO, ICRUDable<SysRetailRewardRuleEntity>, IQueryable<SysRetailRewardRuleEntity>
    {
          public void UpdateSysRetailRewardRule(int IsTemplate, string CooperateType, string RetailTraderID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@EndTime", DateTime.Now));

            string sql = @"update SysRetailRewardRule set isdelete=1,status='0',endTime=@EndTime where customerid=@CustomerId";
            ls.Add(new SqlParameter("@IsTemplate", IsTemplate));
            sql += " and IsTemplate=@IsTemplate";

            if (IsTemplate == 1)//�����ģ��
            {
                ls.Add(new SqlParameter("@CooperateType", CooperateType));
                sql += " and CooperateType=@CooperateType";
            }
            else {
                ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));  //����Ƿ����̣�������֮ǰ�õ�ʲô�������򶼸�ɾ����
                sql += " and RetailTraderID=@RetailTraderID";
            }

      
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());    //����������
           
        }
        /// <summary>
          /// ��Ʒ�б������۸�
        /// </summary>
        /// <param name="strCustomerID"></param>
        /// <param name="intPageSize"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
          public DataSet GetItemListWithSharePrice(string strCustomerID,string strRetailTraderID, int intPageIndex, int intPageSize)
          {

              
              var strSql = new StringBuilder();
              strSql.Append("SELECT * FROM (");
              strSql.Append("  SELECT	i.item_id ItemId,i.item_name ItemName,i.SalesPrice,i.salesQty");
              strSql.Append("  ,CAST(i.SalesPrice * ( CASE WHEN s.ItemSalsePriceRate IS NULL THEN 1 WHEN s.ItemSalsePriceRate = 0 THEN 1 ELSE s.ItemSalsePriceRate*0.01 END ) AS DECIMAL(18,2)) SharePrice ");
              strSql.Append("  ,CASE WHEN r.ItemId IS NULL THEN 0 ELSE 1 END  IsCheck,");
              strSql.Append("  ,ROW_NUMBER() OVER(ORDER BY i.CreateTime DESC) num");
              strSql.Append("   FROM dbo.[vw_item_detail] i ");
              strSql.AppendFormat("   LEFT JOIN dbo.SysRetailRewardRule s ON i.CustomerId=s.CustomerId AND s.CooperateType='Sales'  AND s.RewardTypeCode='Sales' AND s.IsDelete=0 AND s.RetailTraderID='{0}'", strRetailTraderID);
              strSql.Append(" LEFT JOIN RetailTraderItemMapping r ON i.item_id=r.ItemId AND i.CustomerId=r.CustomerID");
              strSql.AppendFormat(" WHERE  i.CustomerId ='{0}' and  s.CustomerId='{1}'AND i.IsDelete=0", strCustomerID, strCustomerID);
              strSql.AppendFormat(") A WHERE A.num >={0} and A.num<={1}", ((intPageIndex - 1) * intPageSize) + 1, intPageIndex * intPageSize);
              return SQLHelper.ExecuteDataset(strSql.ToString());
          }
    }
}
