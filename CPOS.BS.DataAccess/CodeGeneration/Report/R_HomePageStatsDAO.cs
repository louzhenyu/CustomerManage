/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-2-25 13:54:09
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
    /// ��R_HomePageStats�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_HomePageStatsDAO : Base.BaseCPOSDAO, ICRUDable<R_HomePageStatsEntity>, IQueryable<R_HomePageStatsEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public R_HomePageStatsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(R_HomePageStatsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(R_HomePageStatsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [R_HomePageStats](");
            strSql.Append("[StatsDateCode],[CurrentDay],[ViewUnitID],[ViewUnitName],[ViewUnitCode],[CustomerId],[UnitCount],[UnitCurrentDayOrderAmount],[UnitLastDayOrderAmount],[UnitCurrentDayOrderAmountDToD],[UnitMangerCount],[UnitCurrentDayAvgOrderAmount],[UnitLastDayAvgOrderAmount],[UnitCurrentDayAvgOrderAmountDToD],[UnitUserCount],[UserCurrentDayAvgOrderAmount],[UserLastDayAvgOrderAmount],[UserCurrentDayAvgOrderAmountDToD],[RetailTraderCount],[CurrentDayRetailTraderOrderAmount],[LastDayRetailTraderOrderAmount],[CurrentDayRetailTraderOrderAmountDToD],[VipCount],[NewVipCount],[LastDayNewVipCount],[NewVipDToD],[EventsCount],[EventJoinCount],[CurrentMonthSingleUnitAvgTranCount],[CurrentMonthUnitAvgCustPrice],[CurrentMonthSingleUnitAvgTranAmount],[CurrentMonthTranAmount],[TranAmount],[VipTranAmount],[VipContributePect],[MonthArchivePect],[PreAuditOrder],[PreSendOrder],[PreTakeOrder],[PreRefund],[PreReturnCash],[CreateTime],[LastUpdateTime],[ID])");
            strSql.Append(" values (");
            strSql.Append("@StatsDateCode,@CurrentDay,@ViewUnitID,@ViewUnitName,@ViewUnitCode,@CustomerId,@UnitCount,@UnitCurrentDayOrderAmount,@UnitLastDayOrderAmount,@UnitCurrentDayOrderAmountDToD,@UnitMangerCount,@UnitCurrentDayAvgOrderAmount,@UnitLastDayAvgOrderAmount,@UnitCurrentDayAvgOrderAmountDToD,@UnitUserCount,@UserCurrentDayAvgOrderAmount,@UserLastDayAvgOrderAmount,@UserCurrentDayAvgOrderAmountDToD,@RetailTraderCount,@CurrentDayRetailTraderOrderAmount,@LastDayRetailTraderOrderAmount,@CurrentDayRetailTraderOrderAmountDToD,@VipCount,@NewVipCount,@LastDayNewVipCount,@NewVipDToD,@EventsCount,@EventJoinCount,@CurrentMonthSingleUnitAvgTranCount,@CurrentMonthUnitAvgCustPrice,@CurrentMonthSingleUnitAvgTranAmount,@CurrentMonthTranAmount,@TranAmount,@VipTranAmount,@VipContributePect,@MonthArchivePect,@PreAuditOrder,@PreSendOrder,@PreTakeOrder,@PreRefund,@PreReturnCash,@CreateTime,@LastUpdateTime,@ID)");            

			Guid? pkGuid;
			if (pEntity.ID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@StatsDateCode",SqlDbType.Date),
					new SqlParameter("@CurrentDay",SqlDbType.Date),
					new SqlParameter("@ViewUnitID",SqlDbType.NVarChar),
					new SqlParameter("@ViewUnitName",SqlDbType.NVarChar),
					new SqlParameter("@ViewUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCount",SqlDbType.Int),
					new SqlParameter("@UnitCurrentDayOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitLastDayOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitCurrentDayOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@UnitMangerCount",SqlDbType.Int),
					new SqlParameter("@UnitCurrentDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitLastDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitCurrentDayAvgOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@UnitUserCount",SqlDbType.Int),
					new SqlParameter("@UserCurrentDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UserLastDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UserCurrentDayAvgOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@RetailTraderCount",SqlDbType.Int),
					new SqlParameter("@CurrentDayRetailTraderOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@LastDayRetailTraderOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@CurrentDayRetailTraderOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@NewVipCount",SqlDbType.Int),
					new SqlParameter("@LastDayNewVipCount",SqlDbType.Int),
					new SqlParameter("@NewVipDToD",SqlDbType.Decimal),
					new SqlParameter("@EventsCount",SqlDbType.Int),
					new SqlParameter("@EventJoinCount",SqlDbType.Int),
					new SqlParameter("@CurrentMonthSingleUnitAvgTranCount",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthUnitAvgCustPrice",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthSingleUnitAvgTranAmount",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthTranAmount",SqlDbType.Decimal),
					new SqlParameter("@TranAmount",SqlDbType.Decimal),
					new SqlParameter("@VipTranAmount",SqlDbType.Decimal),
					new SqlParameter("@VipContributePect",SqlDbType.Decimal),
					new SqlParameter("@MonthArchivePect",SqlDbType.Decimal),
					new SqlParameter("@PreAuditOrder",SqlDbType.Int),
					new SqlParameter("@PreSendOrder",SqlDbType.Int),
					new SqlParameter("@PreTakeOrder",SqlDbType.Int),
					new SqlParameter("@PreRefund",SqlDbType.Int),
					new SqlParameter("@PreReturnCash",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.StatsDateCode;
			parameters[1].Value = pEntity.CurrentDay;
			parameters[2].Value = pEntity.ViewUnitID;
			parameters[3].Value = pEntity.ViewUnitName;
			parameters[4].Value = pEntity.ViewUnitCode;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.UnitCount;
			parameters[7].Value = pEntity.UnitCurrentDayOrderAmount;
			parameters[8].Value = pEntity.UnitLastDayOrderAmount;
			parameters[9].Value = pEntity.UnitCurrentDayOrderAmountDToD;
			parameters[10].Value = pEntity.UnitMangerCount;
			parameters[11].Value = pEntity.UnitCurrentDayAvgOrderAmount;
			parameters[12].Value = pEntity.UnitLastDayAvgOrderAmount;
			parameters[13].Value = pEntity.UnitCurrentDayAvgOrderAmountDToD;
			parameters[14].Value = pEntity.UnitUserCount;
			parameters[15].Value = pEntity.UserCurrentDayAvgOrderAmount;
			parameters[16].Value = pEntity.UserLastDayAvgOrderAmount;
			parameters[17].Value = pEntity.UserCurrentDayAvgOrderAmountDToD;
			parameters[18].Value = pEntity.RetailTraderCount;
			parameters[19].Value = pEntity.CurrentDayRetailTraderOrderAmount;
			parameters[20].Value = pEntity.LastDayRetailTraderOrderAmount;
			parameters[21].Value = pEntity.CurrentDayRetailTraderOrderAmountDToD;
			parameters[22].Value = pEntity.VipCount;
			parameters[23].Value = pEntity.NewVipCount;
			parameters[24].Value = pEntity.LastDayNewVipCount;
			parameters[25].Value = pEntity.NewVipDToD;
			parameters[26].Value = pEntity.EventsCount;
			parameters[27].Value = pEntity.EventJoinCount;
			parameters[28].Value = pEntity.CurrentMonthSingleUnitAvgTranCount;
			parameters[29].Value = pEntity.CurrentMonthUnitAvgCustPrice;
			parameters[30].Value = pEntity.CurrentMonthSingleUnitAvgTranAmount;
			parameters[31].Value = pEntity.CurrentMonthTranAmount;
			parameters[32].Value = pEntity.TranAmount;
			parameters[33].Value = pEntity.VipTranAmount;
			parameters[34].Value = pEntity.VipContributePect;
			parameters[35].Value = pEntity.MonthArchivePect;
			parameters[36].Value = pEntity.PreAuditOrder;
			parameters[37].Value = pEntity.PreSendOrder;
			parameters[38].Value = pEntity.PreTakeOrder;
			parameters[39].Value = pEntity.PreRefund;
			parameters[40].Value = pEntity.PreReturnCash;
			parameters[41].Value = pEntity.CreateTime;
			parameters[42].Value = pEntity.LastUpdateTime;
			parameters[43].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public R_HomePageStatsEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_HomePageStats] where ID='{0}'  ", id.ToString());
            //��ȡ����
            R_HomePageStatsEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public R_HomePageStatsEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_HomePageStats] where 1=1 ");
            //��ȡ����
            List<R_HomePageStatsEntity> list = new List<R_HomePageStatsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_HomePageStatsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_HomePageStatsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(R_HomePageStatsEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [R_HomePageStats] set ");
                        if (pIsUpdateNullField || pEntity.StatsDateCode!=null)
                strSql.Append( "[StatsDateCode]=@StatsDateCode,");
            if (pIsUpdateNullField || pEntity.CurrentDay!=null)
                strSql.Append( "[CurrentDay]=@CurrentDay,");
            if (pIsUpdateNullField || pEntity.ViewUnitID!=null)
                strSql.Append( "[ViewUnitID]=@ViewUnitID,");
            if (pIsUpdateNullField || pEntity.ViewUnitName!=null)
                strSql.Append( "[ViewUnitName]=@ViewUnitName,");
            if (pIsUpdateNullField || pEntity.ViewUnitCode!=null)
                strSql.Append( "[ViewUnitCode]=@ViewUnitCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.UnitCount!=null)
                strSql.Append( "[UnitCount]=@UnitCount,");
            if (pIsUpdateNullField || pEntity.UnitCurrentDayOrderAmount!=null)
                strSql.Append( "[UnitCurrentDayOrderAmount]=@UnitCurrentDayOrderAmount,");
            if (pIsUpdateNullField || pEntity.UnitLastDayOrderAmount!=null)
                strSql.Append( "[UnitLastDayOrderAmount]=@UnitLastDayOrderAmount,");
            if (pIsUpdateNullField || pEntity.UnitCurrentDayOrderAmountDToD!=null)
                strSql.Append( "[UnitCurrentDayOrderAmountDToD]=@UnitCurrentDayOrderAmountDToD,");
            if (pIsUpdateNullField || pEntity.UnitMangerCount!=null)
                strSql.Append( "[UnitMangerCount]=@UnitMangerCount,");
            if (pIsUpdateNullField || pEntity.UnitCurrentDayAvgOrderAmount!=null)
                strSql.Append( "[UnitCurrentDayAvgOrderAmount]=@UnitCurrentDayAvgOrderAmount,");
            if (pIsUpdateNullField || pEntity.UnitLastDayAvgOrderAmount!=null)
                strSql.Append( "[UnitLastDayAvgOrderAmount]=@UnitLastDayAvgOrderAmount,");
            if (pIsUpdateNullField || pEntity.UnitCurrentDayAvgOrderAmountDToD!=null)
                strSql.Append( "[UnitCurrentDayAvgOrderAmountDToD]=@UnitCurrentDayAvgOrderAmountDToD,");
            if (pIsUpdateNullField || pEntity.UnitUserCount!=null)
                strSql.Append( "[UnitUserCount]=@UnitUserCount,");
            if (pIsUpdateNullField || pEntity.UserCurrentDayAvgOrderAmount!=null)
                strSql.Append( "[UserCurrentDayAvgOrderAmount]=@UserCurrentDayAvgOrderAmount,");
            if (pIsUpdateNullField || pEntity.UserLastDayAvgOrderAmount!=null)
                strSql.Append( "[UserLastDayAvgOrderAmount]=@UserLastDayAvgOrderAmount,");
            if (pIsUpdateNullField || pEntity.UserCurrentDayAvgOrderAmountDToD!=null)
                strSql.Append( "[UserCurrentDayAvgOrderAmountDToD]=@UserCurrentDayAvgOrderAmountDToD,");
            if (pIsUpdateNullField || pEntity.RetailTraderCount!=null)
                strSql.Append( "[RetailTraderCount]=@RetailTraderCount,");
            if (pIsUpdateNullField || pEntity.CurrentDayRetailTraderOrderAmount!=null)
                strSql.Append( "[CurrentDayRetailTraderOrderAmount]=@CurrentDayRetailTraderOrderAmount,");
            if (pIsUpdateNullField || pEntity.LastDayRetailTraderOrderAmount!=null)
                strSql.Append( "[LastDayRetailTraderOrderAmount]=@LastDayRetailTraderOrderAmount,");
            if (pIsUpdateNullField || pEntity.CurrentDayRetailTraderOrderAmountDToD!=null)
                strSql.Append( "[CurrentDayRetailTraderOrderAmountDToD]=@CurrentDayRetailTraderOrderAmountDToD,");
            if (pIsUpdateNullField || pEntity.VipCount!=null)
                strSql.Append( "[VipCount]=@VipCount,");
            if (pIsUpdateNullField || pEntity.NewVipCount!=null)
                strSql.Append( "[NewVipCount]=@NewVipCount,");
            if (pIsUpdateNullField || pEntity.LastDayNewVipCount!=null)
                strSql.Append( "[LastDayNewVipCount]=@LastDayNewVipCount,");
            if (pIsUpdateNullField || pEntity.NewVipDToD!=null)
                strSql.Append( "[NewVipDToD]=@NewVipDToD,");
            if (pIsUpdateNullField || pEntity.EventsCount!=null)
                strSql.Append( "[EventsCount]=@EventsCount,");
            if (pIsUpdateNullField || pEntity.EventJoinCount!=null)
                strSql.Append( "[EventJoinCount]=@EventJoinCount,");
            if (pIsUpdateNullField || pEntity.CurrentMonthSingleUnitAvgTranCount!=null)
                strSql.Append( "[CurrentMonthSingleUnitAvgTranCount]=@CurrentMonthSingleUnitAvgTranCount,");
            if (pIsUpdateNullField || pEntity.CurrentMonthUnitAvgCustPrice!=null)
                strSql.Append( "[CurrentMonthUnitAvgCustPrice]=@CurrentMonthUnitAvgCustPrice,");
            if (pIsUpdateNullField || pEntity.CurrentMonthSingleUnitAvgTranAmount!=null)
                strSql.Append( "[CurrentMonthSingleUnitAvgTranAmount]=@CurrentMonthSingleUnitAvgTranAmount,");
            if (pIsUpdateNullField || pEntity.CurrentMonthTranAmount!=null)
                strSql.Append( "[CurrentMonthTranAmount]=@CurrentMonthTranAmount,");
            if (pIsUpdateNullField || pEntity.TranAmount!=null)
                strSql.Append( "[TranAmount]=@TranAmount,");
            if (pIsUpdateNullField || pEntity.VipTranAmount!=null)
                strSql.Append( "[VipTranAmount]=@VipTranAmount,");
            if (pIsUpdateNullField || pEntity.VipContributePect!=null)
                strSql.Append( "[VipContributePect]=@VipContributePect,");
            if (pIsUpdateNullField || pEntity.MonthArchivePect!=null)
                strSql.Append( "[MonthArchivePect]=@MonthArchivePect,");
            if (pIsUpdateNullField || pEntity.PreAuditOrder!=null)
                strSql.Append( "[PreAuditOrder]=@PreAuditOrder,");
            if (pIsUpdateNullField || pEntity.PreSendOrder!=null)
                strSql.Append( "[PreSendOrder]=@PreSendOrder,");
            if (pIsUpdateNullField || pEntity.PreTakeOrder!=null)
                strSql.Append( "[PreTakeOrder]=@PreTakeOrder,");
            if (pIsUpdateNullField || pEntity.PreRefund!=null)
                strSql.Append( "[PreRefund]=@PreRefund,");
            if (pIsUpdateNullField || pEntity.PreReturnCash!=null)
                strSql.Append( "[PreReturnCash]=@PreReturnCash,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@StatsDateCode",SqlDbType.Date),
					new SqlParameter("@CurrentDay",SqlDbType.Date),
					new SqlParameter("@ViewUnitID",SqlDbType.NVarChar),
					new SqlParameter("@ViewUnitName",SqlDbType.NVarChar),
					new SqlParameter("@ViewUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UnitCount",SqlDbType.Int),
					new SqlParameter("@UnitCurrentDayOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitLastDayOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitCurrentDayOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@UnitMangerCount",SqlDbType.Int),
					new SqlParameter("@UnitCurrentDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitLastDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UnitCurrentDayAvgOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@UnitUserCount",SqlDbType.Int),
					new SqlParameter("@UserCurrentDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UserLastDayAvgOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@UserCurrentDayAvgOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@RetailTraderCount",SqlDbType.Int),
					new SqlParameter("@CurrentDayRetailTraderOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@LastDayRetailTraderOrderAmount",SqlDbType.Decimal),
					new SqlParameter("@CurrentDayRetailTraderOrderAmountDToD",SqlDbType.Decimal),
					new SqlParameter("@VipCount",SqlDbType.Int),
					new SqlParameter("@NewVipCount",SqlDbType.Int),
					new SqlParameter("@LastDayNewVipCount",SqlDbType.Int),
					new SqlParameter("@NewVipDToD",SqlDbType.Decimal),
					new SqlParameter("@EventsCount",SqlDbType.Int),
					new SqlParameter("@EventJoinCount",SqlDbType.Int),
					new SqlParameter("@CurrentMonthSingleUnitAvgTranCount",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthUnitAvgCustPrice",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthSingleUnitAvgTranAmount",SqlDbType.Decimal),
					new SqlParameter("@CurrentMonthTranAmount",SqlDbType.Decimal),
					new SqlParameter("@TranAmount",SqlDbType.Decimal),
					new SqlParameter("@VipTranAmount",SqlDbType.Decimal),
					new SqlParameter("@VipContributePect",SqlDbType.Decimal),
					new SqlParameter("@MonthArchivePect",SqlDbType.Decimal),
					new SqlParameter("@PreAuditOrder",SqlDbType.Int),
					new SqlParameter("@PreSendOrder",SqlDbType.Int),
					new SqlParameter("@PreTakeOrder",SqlDbType.Int),
					new SqlParameter("@PreRefund",SqlDbType.Int),
					new SqlParameter("@PreReturnCash",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.StatsDateCode;
			parameters[1].Value = pEntity.CurrentDay;
			parameters[2].Value = pEntity.ViewUnitID;
			parameters[3].Value = pEntity.ViewUnitName;
			parameters[4].Value = pEntity.ViewUnitCode;
			parameters[5].Value = pEntity.CustomerId;
			parameters[6].Value = pEntity.UnitCount;
			parameters[7].Value = pEntity.UnitCurrentDayOrderAmount;
			parameters[8].Value = pEntity.UnitLastDayOrderAmount;
			parameters[9].Value = pEntity.UnitCurrentDayOrderAmountDToD;
			parameters[10].Value = pEntity.UnitMangerCount;
			parameters[11].Value = pEntity.UnitCurrentDayAvgOrderAmount;
			parameters[12].Value = pEntity.UnitLastDayAvgOrderAmount;
			parameters[13].Value = pEntity.UnitCurrentDayAvgOrderAmountDToD;
			parameters[14].Value = pEntity.UnitUserCount;
			parameters[15].Value = pEntity.UserCurrentDayAvgOrderAmount;
			parameters[16].Value = pEntity.UserLastDayAvgOrderAmount;
			parameters[17].Value = pEntity.UserCurrentDayAvgOrderAmountDToD;
			parameters[18].Value = pEntity.RetailTraderCount;
			parameters[19].Value = pEntity.CurrentDayRetailTraderOrderAmount;
			parameters[20].Value = pEntity.LastDayRetailTraderOrderAmount;
			parameters[21].Value = pEntity.CurrentDayRetailTraderOrderAmountDToD;
			parameters[22].Value = pEntity.VipCount;
			parameters[23].Value = pEntity.NewVipCount;
			parameters[24].Value = pEntity.LastDayNewVipCount;
			parameters[25].Value = pEntity.NewVipDToD;
			parameters[26].Value = pEntity.EventsCount;
			parameters[27].Value = pEntity.EventJoinCount;
			parameters[28].Value = pEntity.CurrentMonthSingleUnitAvgTranCount;
			parameters[29].Value = pEntity.CurrentMonthUnitAvgCustPrice;
			parameters[30].Value = pEntity.CurrentMonthSingleUnitAvgTranAmount;
			parameters[31].Value = pEntity.CurrentMonthTranAmount;
			parameters[32].Value = pEntity.TranAmount;
			parameters[33].Value = pEntity.VipTranAmount;
			parameters[34].Value = pEntity.VipContributePect;
			parameters[35].Value = pEntity.MonthArchivePect;
			parameters[36].Value = pEntity.PreAuditOrder;
			parameters[37].Value = pEntity.PreSendOrder;
			parameters[38].Value = pEntity.PreTakeOrder;
			parameters[39].Value = pEntity.PreRefund;
			parameters[40].Value = pEntity.PreReturnCash;
			parameters[41].Value = pEntity.LastUpdateTime;
			parameters[42].Value = pEntity.ID;

            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(R_HomePageStatsEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(R_HomePageStatsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_HomePageStatsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.ID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ID.Value, pTran);           
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [R_HomePageStats] set  where ID=@ID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@ID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(R_HomePageStatsEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.ID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.ID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(R_HomePageStatsEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [R_HomePageStats] set  where ID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public R_HomePageStatsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [R_HomePageStats] where 1=1  ");
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
            List<R_HomePageStatsEntity> list = new List<R_HomePageStatsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    R_HomePageStatsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<R_HomePageStatsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [R_HomePageStats] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [R_HomePageStats] where 1=1  ");
            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<R_HomePageStatsEntity> result = new PagedQueryResult<R_HomePageStatsEntity>();
            List<R_HomePageStatsEntity> list = new List<R_HomePageStatsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    R_HomePageStatsEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public R_HomePageStatsEntity[] QueryByEntity(R_HomePageStatsEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<R_HomePageStatsEntity> PagedQueryByEntity(R_HomePageStatsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(R_HomePageStatsEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ID", Value = pQueryEntity.ID });
            if (pQueryEntity.StatsDateCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatsDateCode", Value = pQueryEntity.StatsDateCode });
            if (pQueryEntity.CurrentDay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentDay", Value = pQueryEntity.CurrentDay });
            if (pQueryEntity.ViewUnitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ViewUnitID", Value = pQueryEntity.ViewUnitID });
            if (pQueryEntity.ViewUnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ViewUnitName", Value = pQueryEntity.ViewUnitName });
            if (pQueryEntity.ViewUnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ViewUnitCode", Value = pQueryEntity.ViewUnitCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.UnitCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCount", Value = pQueryEntity.UnitCount });
            if (pQueryEntity.UnitCurrentDayOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCurrentDayOrderAmount", Value = pQueryEntity.UnitCurrentDayOrderAmount });
            if (pQueryEntity.UnitLastDayOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitLastDayOrderAmount", Value = pQueryEntity.UnitLastDayOrderAmount });
            if (pQueryEntity.UnitCurrentDayOrderAmountDToD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCurrentDayOrderAmountDToD", Value = pQueryEntity.UnitCurrentDayOrderAmountDToD });
            if (pQueryEntity.UnitMangerCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitMangerCount", Value = pQueryEntity.UnitMangerCount });
            if (pQueryEntity.UnitCurrentDayAvgOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCurrentDayAvgOrderAmount", Value = pQueryEntity.UnitCurrentDayAvgOrderAmount });
            if (pQueryEntity.UnitLastDayAvgOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitLastDayAvgOrderAmount", Value = pQueryEntity.UnitLastDayAvgOrderAmount });
            if (pQueryEntity.UnitCurrentDayAvgOrderAmountDToD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCurrentDayAvgOrderAmountDToD", Value = pQueryEntity.UnitCurrentDayAvgOrderAmountDToD });
            if (pQueryEntity.UnitUserCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitUserCount", Value = pQueryEntity.UnitUserCount });
            if (pQueryEntity.UserCurrentDayAvgOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserCurrentDayAvgOrderAmount", Value = pQueryEntity.UserCurrentDayAvgOrderAmount });
            if (pQueryEntity.UserLastDayAvgOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserLastDayAvgOrderAmount", Value = pQueryEntity.UserLastDayAvgOrderAmount });
            if (pQueryEntity.UserCurrentDayAvgOrderAmountDToD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserCurrentDayAvgOrderAmountDToD", Value = pQueryEntity.UserCurrentDayAvgOrderAmountDToD });
            if (pQueryEntity.RetailTraderCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RetailTraderCount", Value = pQueryEntity.RetailTraderCount });
            if (pQueryEntity.CurrentDayRetailTraderOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentDayRetailTraderOrderAmount", Value = pQueryEntity.CurrentDayRetailTraderOrderAmount });
            if (pQueryEntity.LastDayRetailTraderOrderAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDayRetailTraderOrderAmount", Value = pQueryEntity.LastDayRetailTraderOrderAmount });
            if (pQueryEntity.CurrentDayRetailTraderOrderAmountDToD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentDayRetailTraderOrderAmountDToD", Value = pQueryEntity.CurrentDayRetailTraderOrderAmountDToD });
            if (pQueryEntity.VipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCount", Value = pQueryEntity.VipCount });
            if (pQueryEntity.NewVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewVipCount", Value = pQueryEntity.NewVipCount });
            if (pQueryEntity.LastDayNewVipCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastDayNewVipCount", Value = pQueryEntity.LastDayNewVipCount });
            if (pQueryEntity.NewVipDToD!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewVipDToD", Value = pQueryEntity.NewVipDToD });
            if (pQueryEntity.EventsCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventsCount", Value = pQueryEntity.EventsCount });
            if (pQueryEntity.EventJoinCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventJoinCount", Value = pQueryEntity.EventJoinCount });
            if (pQueryEntity.CurrentMonthSingleUnitAvgTranCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentMonthSingleUnitAvgTranCount", Value = pQueryEntity.CurrentMonthSingleUnitAvgTranCount });
            if (pQueryEntity.CurrentMonthUnitAvgCustPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentMonthUnitAvgCustPrice", Value = pQueryEntity.CurrentMonthUnitAvgCustPrice });
            if (pQueryEntity.CurrentMonthSingleUnitAvgTranAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentMonthSingleUnitAvgTranAmount", Value = pQueryEntity.CurrentMonthSingleUnitAvgTranAmount });
            if (pQueryEntity.CurrentMonthTranAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrentMonthTranAmount", Value = pQueryEntity.CurrentMonthTranAmount });
            if (pQueryEntity.TranAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TranAmount", Value = pQueryEntity.TranAmount });
            if (pQueryEntity.VipTranAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipTranAmount", Value = pQueryEntity.VipTranAmount });
            if (pQueryEntity.VipContributePect!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipContributePect", Value = pQueryEntity.VipContributePect });
            if (pQueryEntity.MonthArchivePect!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MonthArchivePect", Value = pQueryEntity.MonthArchivePect });
            if (pQueryEntity.PreAuditOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreAuditOrder", Value = pQueryEntity.PreAuditOrder });
            if (pQueryEntity.PreSendOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreSendOrder", Value = pQueryEntity.PreSendOrder });
            if (pQueryEntity.PreTakeOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreTakeOrder", Value = pQueryEntity.PreTakeOrder });
            if (pQueryEntity.PreRefund!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreRefund", Value = pQueryEntity.PreRefund });
            if (pQueryEntity.PreReturnCash!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreReturnCash", Value = pQueryEntity.PreReturnCash });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out R_HomePageStatsEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new R_HomePageStatsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ID"] != DBNull.Value)
			{
				pInstance.ID =  (Guid)pReader["ID"];
			}
			if (pReader["StatsDateCode"] != DBNull.Value)
			{
				pInstance.StatsDateCode =Convert.ToDateTime(pReader["StatsDateCode"]);
			}
			if (pReader["CurrentDay"] != DBNull.Value)
			{
				pInstance.CurrentDay =Convert.ToDateTime(pReader["CurrentDay"]);
			}
			if (pReader["ViewUnitID"] != DBNull.Value)
			{
				pInstance.ViewUnitID =  Convert.ToString(pReader["ViewUnitID"]);
			}
			if (pReader["ViewUnitName"] != DBNull.Value)
			{
				pInstance.ViewUnitName =  Convert.ToString(pReader["ViewUnitName"]);
			}
			if (pReader["ViewUnitCode"] != DBNull.Value)
			{
				pInstance.ViewUnitCode =  Convert.ToString(pReader["ViewUnitCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["UnitCount"] != DBNull.Value)
			{
				pInstance.UnitCount =   Convert.ToInt32(pReader["UnitCount"]);
			}
			if (pReader["UnitCurrentDayOrderAmount"] != DBNull.Value)
			{
				pInstance.UnitCurrentDayOrderAmount =  Convert.ToDecimal(pReader["UnitCurrentDayOrderAmount"]);
			}
			if (pReader["UnitLastDayOrderAmount"] != DBNull.Value)
			{
				pInstance.UnitLastDayOrderAmount =  Convert.ToDecimal(pReader["UnitLastDayOrderAmount"]);
			}
			if (pReader["UnitCurrentDayOrderAmountDToD"] != DBNull.Value)
			{
				pInstance.UnitCurrentDayOrderAmountDToD =  Convert.ToDecimal(pReader["UnitCurrentDayOrderAmountDToD"]);
			}
			if (pReader["UnitMangerCount"] != DBNull.Value)
			{
				pInstance.UnitMangerCount =   Convert.ToInt32(pReader["UnitMangerCount"]);
			}
			if (pReader["UnitCurrentDayAvgOrderAmount"] != DBNull.Value)
			{
				pInstance.UnitCurrentDayAvgOrderAmount =  Convert.ToDecimal(pReader["UnitCurrentDayAvgOrderAmount"]);
			}
			if (pReader["UnitLastDayAvgOrderAmount"] != DBNull.Value)
			{
				pInstance.UnitLastDayAvgOrderAmount =  Convert.ToDecimal(pReader["UnitLastDayAvgOrderAmount"]);
			}
			if (pReader["UnitCurrentDayAvgOrderAmountDToD"] != DBNull.Value)
			{
				pInstance.UnitCurrentDayAvgOrderAmountDToD =  Convert.ToDecimal(pReader["UnitCurrentDayAvgOrderAmountDToD"]);
			}
			if (pReader["UnitUserCount"] != DBNull.Value)
			{
				pInstance.UnitUserCount =   Convert.ToInt32(pReader["UnitUserCount"]);
			}
			if (pReader["UserCurrentDayAvgOrderAmount"] != DBNull.Value)
			{
				pInstance.UserCurrentDayAvgOrderAmount =  Convert.ToDecimal(pReader["UserCurrentDayAvgOrderAmount"]);
			}
			if (pReader["UserLastDayAvgOrderAmount"] != DBNull.Value)
			{
				pInstance.UserLastDayAvgOrderAmount =  Convert.ToDecimal(pReader["UserLastDayAvgOrderAmount"]);
			}
			if (pReader["UserCurrentDayAvgOrderAmountDToD"] != DBNull.Value)
			{
				pInstance.UserCurrentDayAvgOrderAmountDToD =  Convert.ToDecimal(pReader["UserCurrentDayAvgOrderAmountDToD"]);
			}
			if (pReader["RetailTraderCount"] != DBNull.Value)
			{
				pInstance.RetailTraderCount =   Convert.ToInt32(pReader["RetailTraderCount"]);
			}
			if (pReader["CurrentDayRetailTraderOrderAmount"] != DBNull.Value)
			{
				pInstance.CurrentDayRetailTraderOrderAmount =  Convert.ToDecimal(pReader["CurrentDayRetailTraderOrderAmount"]);
			}
			if (pReader["LastDayRetailTraderOrderAmount"] != DBNull.Value)
			{
				pInstance.LastDayRetailTraderOrderAmount =  Convert.ToDecimal(pReader["LastDayRetailTraderOrderAmount"]);
			}
			if (pReader["CurrentDayRetailTraderOrderAmountDToD"] != DBNull.Value)
			{
				pInstance.CurrentDayRetailTraderOrderAmountDToD =  Convert.ToDecimal(pReader["CurrentDayRetailTraderOrderAmountDToD"]);
			}
			if (pReader["VipCount"] != DBNull.Value)
			{
				pInstance.VipCount =   Convert.ToInt32(pReader["VipCount"]);
			}
			if (pReader["NewVipCount"] != DBNull.Value)
			{
				pInstance.NewVipCount =   Convert.ToInt32(pReader["NewVipCount"]);
			}
			if (pReader["LastDayNewVipCount"] != DBNull.Value)
			{
				pInstance.LastDayNewVipCount =   Convert.ToInt32(pReader["LastDayNewVipCount"]);
			}
			if (pReader["NewVipDToD"] != DBNull.Value)
			{
				pInstance.NewVipDToD =  Convert.ToDecimal(pReader["NewVipDToD"]);
			}
			if (pReader["EventsCount"] != DBNull.Value)
			{
				pInstance.EventsCount =   Convert.ToInt32(pReader["EventsCount"]);
			}
			if (pReader["EventJoinCount"] != DBNull.Value)
			{
				pInstance.EventJoinCount =   Convert.ToInt32(pReader["EventJoinCount"]);
			}
			if (pReader["CurrentMonthSingleUnitAvgTranCount"] != DBNull.Value)
			{
				pInstance.CurrentMonthSingleUnitAvgTranCount =  Convert.ToDecimal(pReader["CurrentMonthSingleUnitAvgTranCount"]);
			}
			if (pReader["CurrentMonthUnitAvgCustPrice"] != DBNull.Value)
			{
				pInstance.CurrentMonthUnitAvgCustPrice =  Convert.ToDecimal(pReader["CurrentMonthUnitAvgCustPrice"]);
			}
			if (pReader["CurrentMonthSingleUnitAvgTranAmount"] != DBNull.Value)
			{
				pInstance.CurrentMonthSingleUnitAvgTranAmount =  Convert.ToDecimal(pReader["CurrentMonthSingleUnitAvgTranAmount"]);
			}
			if (pReader["CurrentMonthTranAmount"] != DBNull.Value)
			{
				pInstance.CurrentMonthTranAmount =  Convert.ToDecimal(pReader["CurrentMonthTranAmount"]);
			}
			if (pReader["TranAmount"] != DBNull.Value)
			{
				pInstance.TranAmount =  Convert.ToDecimal(pReader["TranAmount"]);
			}
			if (pReader["VipTranAmount"] != DBNull.Value)
			{
				pInstance.VipTranAmount =  Convert.ToDecimal(pReader["VipTranAmount"]);
			}
			if (pReader["VipContributePect"] != DBNull.Value)
			{
				pInstance.VipContributePect =  Convert.ToDecimal(pReader["VipContributePect"]);
			}
			if (pReader["MonthArchivePect"] != DBNull.Value)
			{
				pInstance.MonthArchivePect =  Convert.ToDecimal(pReader["MonthArchivePect"]);
			}
			if (pReader["PreAuditOrder"] != DBNull.Value)
			{
				pInstance.PreAuditOrder =   Convert.ToInt32(pReader["PreAuditOrder"]);
			}
			if (pReader["PreSendOrder"] != DBNull.Value)
			{
				pInstance.PreSendOrder =   Convert.ToInt32(pReader["PreSendOrder"]);
			}
			if (pReader["PreTakeOrder"] != DBNull.Value)
			{
				pInstance.PreTakeOrder =   Convert.ToInt32(pReader["PreTakeOrder"]);
			}
			if (pReader["PreRefund"] != DBNull.Value)
			{
				pInstance.PreRefund =   Convert.ToInt32(pReader["PreRefund"]);
			}
			if (pReader["PreReturnCash"] != DBNull.Value)
			{
				pInstance.PreReturnCash =   Convert.ToInt32(pReader["PreReturnCash"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}

        }
        #endregion
    }
}
