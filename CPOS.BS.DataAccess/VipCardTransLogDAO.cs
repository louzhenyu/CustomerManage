/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    /// 数据访问：  
    /// 表VipCardTransLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardTransLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardTransLogEntity>, IQueryable<VipCardTransLogEntity>
    {
        /// <summary>
        /// 获取会员卡交易记录
        /// </summary>
        /// <param name="VipCardCode"></param>
        /// <returns></returns>
        public DataSet GetVipCardTransLogList(string VipCardCode)
        {
            string sql = string.Format(@"
                    	--创建消费记录临时表
	                    CREATE TABLE #tempTable
		                    (
		                      TransTime DATETIME ,
		                      UnitCode NVARCHAR(50) ,
		                      VipCardCode NVARCHAR(50) ,
                              OldVipCardCode NVARCHAR(50),
		                      BillNo NVARCHAR(50) ,
		                      Cash DECIMAL(18, 2) ,
		                      Bonus DECIMAL(18, 2) ,
		                      Points DECIMAL(18, 2)
		                    )
	                    DECLARE @iniPoint DECIMAL(18, 2)=0	--期初积分
	                    DECLARE @iniCash DECIMAL(18, 2)=0	--期初余额
	                    DECLARE @iniBonus DECIMAL(18, 2)=0	--期初奖励

	                    --获取期初积分、余额、额外奖励
	                    SELECT TOP 1 @iniPoint =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='P' ORDER BY TransTime ASC
	                    SELECT TOP 1 @iniCash =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='C' ORDER BY TransTime ASC
	                    SELECT TOP 1 @iniBonus =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='B' ORDER BY TransTime ASC

	                    --期初数据插入临时表
	                    INSERT	INTO #tempTable  
	                    SELECT NULL,'','','','',@iniCash,@iniBonus,@iniPoint

	                    --获取明细数据插入临时表
	                    INSERT INTO #tempTable
	                    SELECT CAST(TransTime AS DATE) TransTime,UnitCode,VipCardCode,OldVipCardCode,BillNo,
			                    SUM(CASE TransType WHEN 'C' THEN TransAmount ELSE 0 END) AS 'Cash',
			                    SUM(CASE TransType WHEN 'B' THEN TransAmount ELSE 0 END) AS 'Bonus',
			                    SUM(CASE TransType WHEN 'P' THEN TransAmount ELSE 0 END) AS 'Points' 
	                    FROM dbo.VipCardTransLog
	                    WHERE VipCardCode='{0}' 
	                    GROUP BY VipCardCode ,BillNo,CAST(TransTime AS DATE),UnitCode,OldVipCardCode 
	                    order BY TransTime

	                    --查询临时表，联合汇总数据
	                    SELECT * FROM #tempTable WHERE  Cash<>0 OR Bonus<>0 OR Points<>0
	                    UNION ALL
	                    SELECT  NULL,'','','','总计',SUM(Cash)Cash,SUM(Bonus)Bonus,SUM(Points)Points FROM #tempTable
	                    --删除临时表
	                    DROP TABLE #tempTable
                ", VipCardCode);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 修改交易记录
        /// </summary>
        /// <param name="p_StrSql"></param>
        public void UpdateVipCardTransLog(string p_StrSql)
        {
            this.SQLHelper.ExecuteNonQuery(p_StrSql);
        }
    }
}
