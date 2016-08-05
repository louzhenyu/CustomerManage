/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    /// ��VipCardProfitRuleUnitMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardProfitRuleUnitMappingDAO : Base.BaseCPOSDAO, ICRUDable<VipCardProfitRuleUnitMappingEntity>, IQueryable<VipCardProfitRuleUnitMappingEntity>
    {
        /// <summary>
        /// ���ݿ����� �ж� �ŵ��Ƿ� �ظ�
        /// </summary>
        /// <param name="VipCardTypeId">�����ͱ��</param>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="UnitId">�ŵ���</param>
        /// <param name="ProfitOwner">����ӵ����</param>
        /// <returns></returns>
        public DataSet CheckUnitIsExists(int VipCardTypeId,string ProfitOwner,string CustomerId, string UnitId)
        {
            string sql = @" SELECT *
                                FROM   VipCardProfitRuleUnitMapping  AS vcprum
                                       LEFT JOIN VipCardProfitRule   AS vcpr
                                            ON  vcprum.CardBuyToProfitRuleId = vcpr.CardBuyToProfitRuleId
                                WHERE  vcpr.VipCardTypeID = @VipCardTypeId
                                       AND vcpr.IsDelete = 0
                                       AND vcprum.IsDelete = 0
                                       AND vcpr.CustomerID = @CustomerId
                                       AND vcprum.CustomerID = @CustomerId
                                       AND vcpr.ProfitOwner=@ProfitOwner
                                       AND  vcprum.UnitID=@UnitId";
            SqlParameter[] parameter = new SqlParameter[]{
                     new SqlParameter("@VipCardTypeId",VipCardTypeId),
                     new SqlParameter("@CustomerId",CustomerId),
                     new SqlParameter("@UnitId",UnitId),
                      new SqlParameter("@ProfitOwner",ProfitOwner)
                };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
        public DataSet GetAllUnitTypeList(string CustomerId, Guid? CardBuyToProfitRuleId)
        {

            string sql = @"  SELECT distinct T_Unit_Relation.src_unit_id,unit_name FROM VipCardProfitRuleUnitMapping
                              INNER JOIN T_Unit_Relation ON T_Unit_Relation.dst_unit_id=VipCardProfitRuleUnitMapping.UnitId
                              INNER JOIN T_UNIT ON T_UNIT.UNIT_ID=T_Unit_Relation.src_unit_id
                               WHERE CardBuyToProfitRuleId=@CardBuyToProfitRuleId AND CustomerId=@CustomerId and IsDelete=0";

            sql += @"   SELECT Id,CardBuyToProfitRuleId,UnitId,T_Unit_Relation.src_unit_id,unit_name FROM VipCardProfitRuleUnitMapping
                                      INNER JOIN T_UNIT ON VipCardProfitRuleUnitMapping.UnitID=T_UNIT.Unit_Id 
                                      INNER JOIN T_Unit_Relation ON T_Unit_Relation.dst_unit_id=T_UNIT.Unit_Id 
                                      WHERE CustomerId=@CustomerId AND CardBuyToProfitRuleId=@CardBuyToProfitRuleId AND IsDelete=0";

            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@CardBuyToProfitRuleId",CardBuyToProfitRuleId),
                 new SqlParameter("@CustomerId",CustomerId),
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// ���� ɾ�� �ŵ���Ϣ
        /// </summary>
        /// <param name="CardBuyToProfitRuleId"></param>
        public void UpdateUnitMapping(Guid? CardBuyToProfitRuleId, IDbTransaction tran)
        {
            string sql = "UPDATE VipCardProfitRuleUnitMapping SET IsDelete=1 WHERE CardBuyToProfitRuleId=@CardBuyToProfitRuleId";
            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@CardBuyToProfitRuleId",CardBuyToProfitRuleId)
            };
            this.SQLHelper.ExecuteNonQuery((SqlTransaction)tran, CommandType.Text, sql, parameter); ;
        }
    }
}
