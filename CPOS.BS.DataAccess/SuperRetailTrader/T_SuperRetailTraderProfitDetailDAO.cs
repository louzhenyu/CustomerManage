/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/3 15:11:55
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
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表T_SuperRetailTraderProfitDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderProfitDetailDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderProfitDetailEntity>, IQueryable<T_SuperRetailTraderProfitDetailEntity>
    {
        /// <summary>
        /// 返回超级分销商分销订单信息
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderOrderInfo(string strSuperRetailTraderID, string strCustomerId,int intPage,int intSize)
        {
            string strSql = string.Format(@"
                                            DECLARE @page INT ={2}
                                            DECLARE @size INT ={3}
                                            SELECT v.SuperRetailTraderName Name, sr.OrderNo,sr.OrderDate,Profit Commission,sr.OrderActualAmount
                                            FROM dbo.T_SuperRetailTraderProfitDetail sr WITH(NOLOCK) 
                                            INNER JOIN T_SuperRetailTrader v WITH(NOLOCK)  ON sr.SuperRetailTraderID=v.SuperRetailTraderID  AND v.CustomerId='{1}'
                                            WHERE sr.SuperRetailTraderID='{0}'
                                            AND Level=1
                                            ORDER BY sr.OrderDate DESC  OFFSET (@page -1) * @size ROWS
                                            FETCH NEXT @size ROWS ONLY;", strSuperRetailTraderID, strCustomerId, intPage, intSize);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
        /// <summary>
        /// 超级分销商直线下级的情况(贡献)
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderUnderlingInfo(string strSuperRetailTraderID, string strCustomerId, int intPage, int intSize,string strDomin)
        {
            
            string strSql = string.Format(@"
                                            DECLARE @page INT ={2}
                                            DECLARE @size INT ={3}
                                            SELECT  
                                                    SuperRetailTraderName Name,
		                                            b.SuperRetailTraderPhone Phone,
                                                    a.Profit Bonus,
		                                            (
			                                            SELECT COUNT(1)
			                                            FROM dbo.T_SuperRetailTrader s WITH ( NOLOCK )
			                                            WHERE a.SalesId=s.HigheSuperRetailTraderID
                                                        AND CustomerId='{1}'
			                                            AND IsDelete=0
                                                        AND status=10
		                                            ) UnderlingCount,
		                                            CONVERT(NVARCHAR(10),b.CreateTime ,120) JoinDate,
                                            		CASE WHEN CHARINDEX('http', h.HeadImgUrl, 0) = 1 THEN h.HeadImgUrl
                                                         WHEN CHARINDEX('http', h.HeadImgUrl, 0) = 0
                                                         THEN '｛4｝' + h.HeadImgUrl
                                                    END HeadImgUrl
                                            FROM    dbo.T_SuperRetailTraderProfitDetail a  WITH ( NOLOCK )
                                                    INNER JOIN T_SuperRetailTrader b ON a.SalesId = b.SuperRetailTraderID
                                                    LEFT JOIN ( SELECT  VIPID ,
                                                                                HeadImgUrl
                                                                        FROM    dbo.Vip
                                                                        WHERE   ClientID = '{1}'
                                                                        UNION
                                                                        SELECT  u.user_id ,
                                                                                ImageURL
                                                                        FROM    dbo.T_User u
                                                                                INNER JOIN ObjectImages o ON u.user_id = o.ObjectId
                                                                        WHERE   customer_id = '{1}'
                                                                                AND user_status = 1
                                                                      ) h ON b.SuperRetailTraderFromId = h.VIPID
                                            WHERE   a.SuperRetailTraderID = '{0}'
                                                    AND Level = 2 
                                            ORDER BY a.CreateTime	DESC OFFSET (@page -1) * @size ROWS
                                            FETCH NEXT @size ROWS ONLY;", strSuperRetailTraderID, strCustomerId, intPage, intSize,strDomin);
            return this.SQLHelper.ExecuteDataset(strSql);

                
        }
        /// <summary>
        /// 超级分销商分销收入和下线汇总信息
        /// </summary>
        /// <param name="strSuperRetailTraderID"></param>
        /// <returns></returns>
        public DataSet GetSuperRetailTraderIncomeAndUnderlingInfo(string strSuperRetailTraderID,string strDate)
        {
            string strSql = string.Format(@"
                                            DECLARE @DayBeg DATETIME
                                            DECLARE @DayEnd DATETIME
                                            DECLARE @MouthBeg DATETIME
                                            DECLARE @MouthEnd DATETIME
                                            DECLARE @dt DATETIME= '{0}'
                                            SET @DayBeg = @dt + ' 00:00:00'
                                            SET @DayEnd = @dt + ' 23:59:59'

                                            SET @MouthBeg = DATEADD(d, -DAY(@dt) + 1, @dt) + ' 00:00:00'
                                            SET @MouthEnd = DATEADD(d, -DAY(@dt), DATEADD(m, 1, @dt)) + ' 23:59:59'

                                            SELECT  ISNULL(COUNT(1),0) UnderlingCount
                                            FROM    dbo.T_SuperRetailTrader
                                            WHERE   HigheSuperRetailTraderID = '{1}'
                                                    AND CreateTime BETWEEN @DayBeg AND @DayEnd
                                                    AND IsDelete = 0
                                                    AND status=10

                                            SELECT  ISNULL(COUNT(1),0) UnderlingCount
                                            FROM    dbo.T_SuperRetailTrader
                                            WHERE   HigheSuperRetailTraderID = '{1}'
                                                    AND CreateTime BETWEEN @MouthBeg AND @MouthEnd
                                                    AND IsDelete = 0
                                                    AND status=10

                                            SELECT  ISNULL(COUNT(1),0)UnderlingCount
                                            FROM    dbo.T_SuperRetailTrader
                                            WHERE   HigheSuperRetailTraderID = '{1}'
                                                    AND IsDelete = 0
                                                    AND status=10

                                            SELECT  ISNULL(COUNT(DISTINCT SalesId),0) ContributeUnderling
                                            FROM    dbo.T_SuperRetailTraderProfitDetail a WITH ( NOLOCK )
                                            WHERE   a.SuperRetailTraderID = '{1}'
                                                    AND Level = 2



                                            SELECT  ISNULL(SUM(Profit),0) EarningMoney
                                            FROM    dbo.T_SuperRetailTraderProfitDetail
                                            WHERE   SuperRetailTraderID = '{1}'
                                                    AND OrderDate BETWEEN @DayBeg AND @DayEnd
                                                    AND IsDelete = 0

                                            SELECT  ISNULL(SUM(Profit),0) EarningMoney
                                            FROM    dbo.T_SuperRetailTraderProfitDetail
                                            WHERE   SuperRetailTraderID = '{1}'
                                                    AND OrderDate BETWEEN @MouthBeg AND @MouthEnd
                                                    AND IsDelete = 0

                                            SELECT  ISNULL(SUM(Profit),0) EarningMoney
                                            FROM    dbo.T_SuperRetailTraderProfitDetail
                                            WHERE   SuperRetailTraderID = '{1}'
                                                    AND IsDelete = 0
                                ", strDate,strSuperRetailTraderID);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
    }
}
