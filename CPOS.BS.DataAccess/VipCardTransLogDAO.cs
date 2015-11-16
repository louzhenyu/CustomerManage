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
    /// ���ݷ��ʣ�  
    /// ��VipCardTransLog�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardTransLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardTransLogEntity>, IQueryable<VipCardTransLogEntity>
    {
        /// <summary>
        /// ��ȡ��Ա�����׼�¼
        /// </summary>
        /// <param name="VipCardCode"></param>
        /// <returns></returns>
        public DataSet GetVipCardTransLogList(string VipCardCode)
        {
            string sql = string.Format(@"
                    	--�������Ѽ�¼��ʱ��
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
	                    DECLARE @iniPoint DECIMAL(18, 2)=0	--�ڳ�����
	                    DECLARE @iniCash DECIMAL(18, 2)=0	--�ڳ����
	                    DECLARE @iniBonus DECIMAL(18, 2)=0	--�ڳ�����

	                    --��ȡ�ڳ����֡������⽱��
	                    SELECT TOP 1 @iniPoint =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='P' ORDER BY TransTime ASC
	                    SELECT TOP 1 @iniCash =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='C' ORDER BY TransTime ASC
	                    SELECT TOP 1 @iniBonus =LastValue FROM dbo.VipCardTransLog WHERE VipCardCode='{0}' and TransType='B' ORDER BY TransTime ASC

	                    --�ڳ����ݲ�����ʱ��
	                    INSERT	INTO #tempTable  
	                    SELECT NULL,'','','','',@iniCash,@iniBonus,@iniPoint

	                    --��ȡ��ϸ���ݲ�����ʱ��
	                    INSERT INTO #tempTable
	                    SELECT CAST(TransTime AS DATE) TransTime,UnitCode,VipCardCode,OldVipCardCode,BillNo,
			                    SUM(CASE TransType WHEN 'C' THEN TransAmount ELSE 0 END) AS 'Cash',
			                    SUM(CASE TransType WHEN 'B' THEN TransAmount ELSE 0 END) AS 'Bonus',
			                    SUM(CASE TransType WHEN 'P' THEN TransAmount ELSE 0 END) AS 'Points' 
	                    FROM dbo.VipCardTransLog
	                    WHERE VipCardCode='{0}' 
	                    GROUP BY VipCardCode ,BillNo,CAST(TransTime AS DATE),UnitCode,OldVipCardCode 
	                    order BY TransTime

	                    --��ѯ��ʱ�����ϻ�������
	                    SELECT * FROM #tempTable WHERE  Cash<>0 OR Bonus<>0 OR Points<>0
	                    UNION ALL
	                    SELECT  NULL,'','','','�ܼ�',SUM(Cash)Cash,SUM(Bonus)Bonus,SUM(Points)Points FROM #tempTable
	                    --ɾ����ʱ��
	                    DROP TABLE #tempTable
                ", VipCardCode);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// �޸Ľ��׼�¼
        /// </summary>
        /// <param name="p_StrSql"></param>
        public void UpdateVipCardTransLog(string p_StrSql)
        {
            this.SQLHelper.ExecuteNonQuery(p_StrSql);
        }
    }
}
