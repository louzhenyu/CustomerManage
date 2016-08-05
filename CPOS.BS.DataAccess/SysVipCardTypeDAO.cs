/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
    /// ���ݷ��ʣ�  
    /// ��SysVipCardType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysVipCardTypeDAO : Base.BaseCPOSDAO, ICRUDable<SysVipCardTypeEntity>, IQueryable<SysVipCardTypeEntity>
    {
        /// <summary>
        /// ��ȡ��Ա���б�
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public SysVipCardTypeEntity[] GetVipCardList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" select vct.*,vcr.CardDiscount,vcr.RuleID,vcr.PointsMultiple,vcr.ChargeFull,vcr.ChargeGive from [SysVipCardType] vct ");
            sql.AppendFormat(" left join [VipCardRule] vcr on vcr.VipCardTypeID=vct.VipCardTypeID ");

            sql.AppendFormat(" where 1=1  and vct.isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //ִ��SQL
            List<SysVipCardTypeEntity> list = new List<SysVipCardTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardTypeEntity m;
                    this.Load(rdr, out m);

                    if (rdr["RuleID"] != DBNull.Value)
                    {
                        m.RuleID = Convert.ToInt32(rdr["VipCardTypeID"]);
                    }
                    if (rdr["CardDiscount"] != DBNull.Value)
                    {
                        m.CardDiscount = Convert.ToDecimal(rdr["CardDiscount"]);
                    }
                    if (rdr["PointsMultiple"] != DBNull.Value)
                    {
                        m.PointsMultiple = Convert.ToInt32(rdr["PointsMultiple"]);
                    }
                    if (rdr["ChargeFull"] != DBNull.Value)
                    {
                        m.ChargeFull = Convert.ToDecimal(rdr["ChargeFull"]);
                    }
                    if (rdr["ChargeGive"] != DBNull.Value)
                    {
                        m.ChargeGive = Convert.ToDecimal(rdr["ChargeGive"]);
                    }

                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ��ȡ��Ա����ϵ��Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeSystemList(string CustomerId)
        {
            string sql = @"select A.VipCardTypeID,A.VipCardLevel,A.VipCardTypeName,
                            IsNull(A.PicUrl,'') AS PicUrl,ISNULL(A.IsPrepaid,0) AS IsPrepaid,ISNULL(A.IsOnlineSales,0) AS IsOnlineSales,
                            B.VipCardUpgradeRuleId,ISNULL(B.IsPurchaseUpgrade,0) AS IsPurchaseUpgrade,
                            ISNULL(A.IsExtraMoney,0) AS IsExtraMoney,ISNULL(A.Prices,0.00)AS Prices,ISNULL(A.ExchangeIntegral,0) AS ExchangeIntegral,
                            ISNULL(B.IsRecharge,0) AS IsRecharge ,ISNULL(B.OnceRechargeAmount,0.00) AS OnceRechargeAmount,ISNULL(B.IsBuyUpgrade,0) AS IsBuyUpgrade,
                            ISNULL(B.BuyAmount,0.00) AS BuyAmount,ISNULL(B.OnceBuyAmount,0) AS OnceBuyAmount, 
                            ISNULL(C.RuleID,0) AS RuleID,ISNULL(C.CardDiscount/10,10) AS CardDiscount,ISNULL(C.PaidGivePercetPoints,0.00) AS PaidGivePercetPoints,ISNULL(C.PaidGivePoints,0) AS PaidGivePoints
                            from SysVipCardType A 
                            LEFT JOIN  VipCardUpgradeRule B ON A.VipCardTypeID=B.VipCardTypeID
                            LEFT JOIN VipCardRule C ON A.VipCardTypeID=C.VipCardTypeID
                            WHERE A.CustomerID=@CustomerId AND A.IsDelete=0 AND A.Category=0 ORDER BY VipCardLevel ASC ";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// ���ݻ�Ա���ȼ���Ϣ��ȡ�������������Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetCardUpgradeRewardList(string CustomerId)
        {
            string strsql = @"select D.VipCardTypeID,
                            D.CardUpgradeRewardId,D.CouponTypeId,D.CouponNum,E.CouponTypeName AS CouponName,E.BeginTime,E.ServiceLife,E.EndTime,E.CouponTypeDesc AS CouponDesc,E.ParValue
                            from SysVipCardType A 
                            INNER JOIN VipCardUpgradeReward D ON A.VipCardTypeID=D.VipCardTypeID
                            INNER JOIN CouponType E ON D.CouponTypeId=E.CouponTypeID
                            WHERE A.CustomerID=@CustomerId AND A.IsDelete=0 
                            AND D.IsDelete=0 AND E.IsDelete=0 AND A.Category=0 ORDER BY VipCardLevel ASC";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strsql, parameter);
        }
        /// <summary>
        /// ��ȡ��ǰ��Ա��صĿ���ʵ�忨��Ϣ
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="Phone"></param>
        /// <param name="VipID"></param>
        /// <returns></returns>
        public DataSet GetBindVipCardTypeInfo(string CustomerID, string Phone, string VipID,int? CurLevel)
        {
            string strsql = @" select V.VipID as BindVipID, SVC.VipCardTypeID,SVC.VipCardTypeName,SVC.VipCardLevel,VCR.CardDiscount/10 AS CardDiscount,VI.ValidIntegral AS Integration,VA.EndAmount AS TotalAmount from Vip  V
                            left join VipCardVipMapping VCM ON V.VIPID=VCM.VIPID
                            left join [VipCard] VC ON VCM.VipCardID=VC.VipCardID
                            left join SysVipCardType SVC ON SVC.VipCardTypeID=VC.VipCardTypeID
                            left join VipCardRule VCR ON VCR.VipCardTypeID=SVC.VipCardTypeID
                            left join VipAmount VA ON VA.VipId=V.VIPID
                            left join VipIntegral VI ON VI.VipID=V.VIPID 
                            where V.Phone=@Phone and V.VipID!=@VipID and V.ClientID=@CustomerID and SVC.VipCardLevel>@CurLevel ORDER BY SVC.VipCardLevel ASC ";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@Phone",Phone),
                new SqlParameter("@VipID",VipID),
                new SqlParameter("@CurLevel",CurLevel),
                new SqlParameter("@CustomerId",CustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strsql, parameter);
        }

        /// <summary>
        /// ���ݿ��͵�ǰ�ȼ���ȡ�����ۿ����б� 1=΢�ŵ��ۿ�չʾ 2=APP���ۿ�չʾ
        /// </summary>
        /// <param name="CustomerId">�̻�ID</param>
        /// <param name="CurLevel">��ǰ��Ա�ȼ�</param>
        /// <param name="pType">��������(1=΢�ŵ��ۿ�չʾ 2=APP���ۿ�չʾ)</param>
        /// <returns></returns>
        public DataSet GetVipCardTypeVirtualItemList(string CustomerId, int? CurLevel, string pType, string pVipID)
        {
            string sql = @"select DISTINCT A.VipCardTypeID,A.VipCardLevel,A.VipCardTypeName,
                            IsNull(A.PicUrl,'') AS PicUrl,ISNULL(A.IsPrepaid,0) AS IsPrepaid,ISNULL(A.IsOnlineSales,0) AS IsOnlineSales,
                            B.VipCardUpgradeRuleId,ISNULL(B.IsPurchaseUpgrade,0) AS IsPurchaseUpgrade,
                            ISNULL(A.IsExtraMoney,0) AS IsExtraMoney,ISNULL(A.Prices,0.00)AS Prices,ISNULL(A.ExchangeIntegral,0) AS ExchangeIntegral,
                            ISNULL(B.IsRecharge,0) AS IsRecharge ,ISNULL(B.OnceRechargeAmount,0.00) AS OnceRechargeAmount,ISNULL(B.IsBuyUpgrade,0) AS IsBuyUpgrade,
                            ISNULL(B.BuyAmount,0.00) AS BuyAmount,ISNULL(B.OnceBuyAmount,0) AS OnceBuyAmount, 
                            ISNULL(C.RuleID,0) AS RuleID,ISNULL(C.CardDiscount/10,100) AS CardDiscount,ISNULL(C.PaidGivePercetPoints,0.00) AS PaidGivePercetPoints,
                            ISNULL(C.PaidGivePoints,0) AS PaidGivePoints,D.ItemId,D.SkuId
                            from SysVipCardType A 
                            Left JOIN  VipCardUpgradeRule B ON A.VipCardTypeID=B.VipCardTypeID AND A.IsOnlineSales=1
                            Left JOIN VipCardRule C ON A.VipCardTypeID=C.VipCardTypeID
                            INNER JOIN T_VirtualItemTypeSetting D ON CAST(A.VipCardTypeID as varchar)=D.ObjecetTypeId
                            WHERE A.CustomerID=@CustomerId AND A.IsDelete=0 AND A.Category=0 ";
            if (CurLevel != 0 && pType != "2")
            {
                sql += " AND  ((A.VipCardLevel >=@CurLevel and  A.IsOnlineSales=1) OR (A.VipCardLevel=@CurLevel AND A.IsOnlineSales=0)) ";
            }
            //���Ϊ1��ʾ΢���ۿ�û����ʾ���ƣ����Ϊ2��ʾAPP���ۿ�չʾ����������չʾ
            if (pType == "2")
            {
                sql += " AND B.IsBuyUpgrade=0 And (A.Prices!=0 or B.IsRecharge=1) ";
            }
            sql += " ORDER BY VipCardLevel ASC ";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@pVipID",pVipID),
                new SqlParameter("@CurLevel",CurLevel),
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
        /// <summary>
        /// ���ݵ�ǰ���ȼ���Ϣ��ȡ����������Ϣ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CurLevel"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeUpGradeInfo(string CustomerId, int? CurLevel)
        {
            string sql = @" select A.VipCardTypeID,A.VipCardLevel,A.VipCardTypeName,
                            A.PicUrl,A.IsPrepaid,ISNULL(A.IsOnlineSales,0) AS IsOnlineSales,
                            B.VipCardUpgradeRuleId,ISNULL(B.IsPurchaseUpgrade,0) AS IsPurchaseUpgrade,
                            ISNULL(A.IsExtraMoney,0) AS IsExtraMoney,ISNULL(A.Prices,0.00)AS Prices,ISNULL(A.ExchangeIntegral,0) AS ExchangeIntegral,
                            ISNULL(B.IsRecharge,0) AS IsRecharge ,ISNULL(B.OnceRechargeAmount,0.00) AS OnceRechargeAmount,ISNULL(B.IsBuyUpgrade,0) AS IsBuyUpgrade,
                            ISNULL(B.BuyAmount,0.00) AS BuyAmount,ISNULL(B.OnceBuyAmount,0) AS OnceBuyAmount
                            from SysVipCardType A 
                            INNER JOIN  VipCardUpgradeRule B ON A.VipCardTypeID=B.VipCardTypeID 
                            WHERE A.CustomerID=@CustomerId AND A.IsDelete=0 AND A.Category=0  ";
            if (CurLevel != 0)
            {
                sql += " AND A.VipCardLevel>=@CurLevel AND IsBuyUpgrade=1 AND BuyAmount!=0 ";
            }
            sql += " ORDER BY VipCardLevel ASC ";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId),
                new SqlParameter("@CurLevel",CurLevel)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// ���ݻ�Ա���ȼ���Ϣ��ȡ�������������Ϣ���� ����
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetVipCardTypeByIsprepaid(string CustomerId, int IsOnLineSale)
        {
            string sql = string.Empty;

            if (IsOnLineSale != -1)
            {
                sql = @" SELECT *
                           FROM SysVipCardType AS svct
                        LEFT JOIN VipCardUpgradeRule AS vcud ON svct.VipCardTypeID=vcud.VipCardTypeID
                        WHERE svct.CustomerID=@CustomerId AND svct.IsOnlineSales=1 AND Category=0 AND svct.IsDelete=0  ORDER BY VipCardLevel ASC";
            }
            else
            {
                sql = @" SELECT *
                           FROM SysVipCardType AS svct
                        LEFT JOIN VipCardUpgradeRule AS vcud ON svct.VipCardTypeID=vcud.VipCardTypeID
                        WHERE svct.CustomerID=@CustomerId AND Category=0  AND svct.IsDelete=0 ORDER BY VipCardLevel ASC";
            }
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
    }
}
