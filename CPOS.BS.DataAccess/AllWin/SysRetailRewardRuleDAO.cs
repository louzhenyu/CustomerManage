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
        public DataSet GetSysRetailRewardRule(SysRetailRewardRuleEntity en)
        {
            DataSet dataSet = new DataSet();
            List<SqlParameter> spl = new List<SqlParameter>();
            spl.Add(new SqlParameter("@RetailTraderId", en.RetailTraderID));
            spl.Add(new SqlParameter("@CustomerId", en.CustomerId));
            dataSet = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "[Proc_GetRetailRewardRule]", spl.ToArray());

            return dataSet;

        }
          public void UpdateSysRetailRewardRule(int IsTemplate,string CooperateType,string RewardTypeCode, string RetailTraderID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@EndTime", DateTime.Now));

            string sql = @"update SysRetailRewardRule set isdelete=1,status='0',endTime=@EndTime where customerid=@CustomerId";
            sql += " and IsTemplate=@IsTemplate";
            ls.Add(new SqlParameter("@IsTemplate", IsTemplate));
            if (IsTemplate == 0)//�������ģ��
            {
               
                ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));  //����Ƿ����̣�������֮ǰ�õ�ʲô�������򶼸�ɾ����
                sql += " and RetailTraderID=@RetailTraderID";
            }
            else
            {
                sql += " and CooperateType=@CooperateType and RewardTypeCode=@RewardTypeCode ";

                ls.Add(new SqlParameter("@CooperateType", CooperateType));
                ls.Add(new SqlParameter("@RewardTypeCode", RewardTypeCode));
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
          public DataSet GetItemListWithSharePrice(string strCustomerID,string strRetailTraderID, int intPageIndex, int intPageSize,string strSort,string strSortName)
          {

              
              var strSql = new StringBuilder();
              strSql.Append(" DECLARE @ItemSalesPriceRate DECIMAL(18,2)=1");
              strSql.Append(" DECLARE @RetailTraderReward DECIMAL(18,2)=1");
              strSql.AppendFormat(" IF EXISTS(SELECT 1 FROM SysRetailRewardRule s WHERE RetailTraderID='{0}' AND s.CooperateType='Sales'  AND s.RewardTypeCode='Sales' AND s.IsDelete=0 AND Status=1) ", strRetailTraderID);
              strSql.AppendFormat("  BEGIN   SELECT @ItemSalesPriceRate=ItemSalesPriceRate,@RetailTraderReward=RetailTraderReward FROM SysRetailRewardRule s WHERE RetailTraderID='{0}' AND s.CooperateType='Sales'  AND s.RewardTypeCode='Sales' AND s.IsDelete=0 AND Status=1  ", strRetailTraderID);
              strSql.Append("  END     ");
              strSql.Append("  ELSE BEGIN   SELECT @ItemSalesPriceRate=ItemSalesPriceRate,@RetailTraderReward=RetailTraderReward FROM SysRetailRewardRule s WHERE IsTemplate=1  AND s.CooperateType='Sales'  AND s.RewardTypeCode='Sales' AND s.IsDelete=0 AND Status=1  ");
              strSql.Append("  END     ");
              strSql.Append("  SELECT * FROM (");
              strSql.AppendFormat("  SELECT *,ROW_NUMBER() OVER(ORDER BY  {0}  {1}) num  FROM (", strSortName == "" ? "Commission" : strSortName, strSort == "" ? "Desc" : strSort);
              strSql.Append("  SELECT	i.item_id ItemId,i.item_name ItemName,i.SalesPrice,i.salesQty,i.imageUrl");
              strSql.Append("  ,CAST(i.SalesPrice * @ItemSalesPriceRate AS DECIMAL(18,2)) SharePrice ");
              strSql.Append("  ,CASE WHEN r.ItemId IS NULL THEN 0 ELSE 1 END  IsCheck");
              strSql.Append("  ,(i.SalesPrice* (@ItemSalesPriceRate*0.01)*(@RetailTraderReward*0.01))  Commission");
              //strSql.Append("  ,ROW_NUMBER() OVER(ORDER BY i.CreateTime DESC) num");
              //strSql.Append("  ,ROW_NUMBER() OVER(ORDER BY i.CreateTime DESC) num");
              strSql.Append("   FROM dbo.[vw_item_detail] i ");
              strSql.AppendFormat(" LEFT JOIN RetailTraderItemMapping r ON i.item_id=r.ItemId AND i.CustomerId=r.CustomerID and r.[IsDelete]=0   AND r.RetailTraderId= '{0}'",strRetailTraderID);
              strSql.AppendFormat(" WHERE  i.CustomerId ='{0}' AND i.IsDelete=0", strCustomerID, strCustomerID);
              strSql.Append(" ) A");
              strSql.AppendFormat(" ) Main WHERE Main.num >={0} and Main.num<={1}", ((intPageIndex - 1) * intPageSize) + 1, intPageIndex * intPageSize);
              //strSql.AppendFormat(" ORDER BY  {0}  {1}", strSortName == "" ? "Commission" : strSortName, strSort == "" ? "Desc" : strSort);
              return SQLHelper.ExecuteDataset(strSql.ToString());
          }
        /// <summary>
          /// ���������N���ڼ�������
        /// </summary>
        /// <param name="strRetailTraderID">������id</param>
        /// <param name="intDays">����</param>
        /// <returns></returns>
          public DataSet GetRetailTraderVipCountByDays(string strRetailTraderID, int intDays)
          {
              DataSet dataSet = new DataSet();
              List<SqlParameter> spl = new List<SqlParameter>();
              spl.Add(new SqlParameter("@Days", intDays));
              spl.Add(new SqlParameter("@RetailTraderID", strRetailTraderID));
              dataSet = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "[Proc_GetRetailTraderVipCountByDays]", spl.ToArray());

              return dataSet;

          }
          public DataSet GetRetailTraderEarnings(string strRetailTraderID, string strType)
          {
              DataSet dataSet = new DataSet();
              DateTime dt = DateTime.Now.Date;
              List<SqlParameter> spl = new List<SqlParameter>();
              spl.Add(new SqlParameter("@dt", dt));
              spl.Add(new SqlParameter("@RetailTraderID", strRetailTraderID));
              spl.Add(new SqlParameter("@Type", strType));
              dataSet = SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "[Proc_RetailTraderEarnings]", spl.ToArray());

              return dataSet;

          }
        
    }
}
