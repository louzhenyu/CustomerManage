/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/29 11:20:31
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
    /// 表StoreItemDailyStatus的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class StoreItemDailyStatusDAO : Base.BaseCPOSDAO, ICRUDable<StoreItemDailyStatusEntity>, IQueryable<StoreItemDailyStatusEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public StoreItemDailyStatusDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(StoreItemDailyStatusEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(StoreItemDailyStatusEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //初始化固定字段
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [StoreItemDailyStatus](");
            strSql.Append("[StoreID],[ChannelID],[SkuID],[StatusDate],[CanReserveBeginTime],[CanReserveEndTime],[StockAmount],[UsedAmount],[StorePrice],[LowestPrice],[ReservationRemark],[NeedCreditCard],[NeedPrepay],[CancellationRemark],[Remark],[Price1],[Price2],[Price3],[Price4],[Price5],[Price6],[Price7],[Price8],[Price9],[Price10],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[SourcePrice],[StoreItemDailyStatusID])");
            strSql.Append(" values (");
            strSql.Append("@StoreID,@ChannelID,@SkuID,@StatusDate,@CanReserveBeginTime,@CanReserveEndTime,@StockAmount,@UsedAmount,@StorePrice,@LowestPrice,@ReservationRemark,@NeedCreditCard,@NeedPrepay,@CancellationRemark,@Remark,@Price1,@Price2,@Price3,@Price4,@Price5,@Price6,@Price7,@Price8,@Price9,@Price10,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@SourcePrice,@StoreItemDailyStatusID)");

            Guid? pkGuid;
            if (pEntity.StoreItemDailyStatusID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.StoreItemDailyStatusID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@StoreID",SqlDbType.NVarChar),
					new SqlParameter("@ChannelID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@StatusDate",SqlDbType.Date),
					new SqlParameter("@CanReserveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@CanReserveEndTime",SqlDbType.DateTime),
					new SqlParameter("@StockAmount",SqlDbType.Int),
					new SqlParameter("@UsedAmount",SqlDbType.Int),
					new SqlParameter("@StorePrice",SqlDbType.Decimal),
					new SqlParameter("@LowestPrice",SqlDbType.Decimal),
					new SqlParameter("@ReservationRemark",SqlDbType.NVarChar),
					new SqlParameter("@NeedCreditCard",SqlDbType.Int),
					new SqlParameter("@NeedPrepay",SqlDbType.Int),
					new SqlParameter("@CancellationRemark",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Price1",SqlDbType.Decimal),
					new SqlParameter("@Price2",SqlDbType.Decimal),
					new SqlParameter("@Price3",SqlDbType.Decimal),
					new SqlParameter("@Price4",SqlDbType.Decimal),
					new SqlParameter("@Price5",SqlDbType.Decimal),
					new SqlParameter("@Price6",SqlDbType.Decimal),
					new SqlParameter("@Price7",SqlDbType.Decimal),
					new SqlParameter("@Price8",SqlDbType.Decimal),
					new SqlParameter("@Price9",SqlDbType.Decimal),
					new SqlParameter("@Price10",SqlDbType.Decimal),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@SourcePrice",SqlDbType.Decimal),
					new SqlParameter("@StoreItemDailyStatusID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.StoreID;
            parameters[1].Value = pEntity.ChannelID;
            parameters[2].Value = pEntity.SkuID;
            parameters[3].Value = pEntity.StatusDate;
            parameters[4].Value = pEntity.CanReserveBeginTime;
            parameters[5].Value = pEntity.CanReserveEndTime;
            parameters[6].Value = pEntity.StockAmount;
            parameters[7].Value = pEntity.UsedAmount;
            parameters[8].Value = pEntity.StorePrice;
            parameters[9].Value = pEntity.LowestPrice;
            parameters[10].Value = pEntity.ReservationRemark;
            parameters[11].Value = pEntity.NeedCreditCard;
            parameters[12].Value = pEntity.NeedPrepay;
            parameters[13].Value = pEntity.CancellationRemark;
            parameters[14].Value = pEntity.Remark;
            parameters[15].Value = pEntity.Price1;
            parameters[16].Value = pEntity.Price2;
            parameters[17].Value = pEntity.Price3;
            parameters[18].Value = pEntity.Price4;
            parameters[19].Value = pEntity.Price5;
            parameters[20].Value = pEntity.Price6;
            parameters[21].Value = pEntity.Price7;
            parameters[22].Value = pEntity.Price8;
            parameters[23].Value = pEntity.Price9;
            parameters[24].Value = pEntity.Price10;
            parameters[25].Value = pEntity.Col1;
            parameters[26].Value = pEntity.Col2;
            parameters[27].Value = pEntity.Col3;
            parameters[28].Value = pEntity.Col4;
            parameters[29].Value = pEntity.Col5;
            parameters[30].Value = pEntity.Col6;
            parameters[31].Value = pEntity.Col7;
            parameters[32].Value = pEntity.Col8;
            parameters[33].Value = pEntity.Col9;
            parameters[34].Value = pEntity.Col10;
            parameters[35].Value = pEntity.ClientID;
            parameters[36].Value = pEntity.CreateBy;
            parameters[37].Value = pEntity.CreateTime;
            parameters[38].Value = pEntity.LastUpdateBy;
            parameters[39].Value = pEntity.LastUpdateTime;
            parameters[40].Value = pEntity.IsDelete;
            parameters[41].Value = pEntity.SourcePrice;
            parameters[42].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.StoreItemDailyStatusID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public StoreItemDailyStatusEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [StoreItemDailyStatus] where StoreItemDailyStatusID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            StoreItemDailyStatusEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public StoreItemDailyStatusEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [StoreItemDailyStatus] where 1=1  and isdelete=0");
            //读取数据
            List<StoreItemDailyStatusEntity> list = new List<StoreItemDailyStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    StoreItemDailyStatusEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list.ToArray();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(StoreItemDailyStatusEntity pEntity, IDbTransaction pTran)
        {
            Update(pEntity, pTran, true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(StoreItemDailyStatusEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.StoreItemDailyStatusID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
            //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [StoreItemDailyStatus] set ");
            if (pIsUpdateNullField || pEntity.StoreID != null)
                strSql.Append("[StoreID]=@StoreID,");
            if (pIsUpdateNullField || pEntity.ChannelID != null)
                strSql.Append("[ChannelID]=@ChannelID,");
            if (pIsUpdateNullField || pEntity.SkuID != null)
                strSql.Append("[SkuID]=@SkuID,");
            if (pIsUpdateNullField || pEntity.StatusDate != null)
                strSql.Append("[StatusDate]=@StatusDate,");
            if (pIsUpdateNullField || pEntity.CanReserveBeginTime != null)
                strSql.Append("[CanReserveBeginTime]=@CanReserveBeginTime,");
            if (pIsUpdateNullField || pEntity.CanReserveEndTime != null)
                strSql.Append("[CanReserveEndTime]=@CanReserveEndTime,");
            if (pIsUpdateNullField || pEntity.StockAmount != null)
                strSql.Append("[StockAmount]=@StockAmount,");
            if (pIsUpdateNullField || pEntity.UsedAmount != null)
                strSql.Append("[UsedAmount]=@UsedAmount,");
            if (pIsUpdateNullField || pEntity.StorePrice != null)
                strSql.Append("[StorePrice]=@StorePrice,");
            if (pIsUpdateNullField || pEntity.LowestPrice != null)
                strSql.Append("[LowestPrice]=@LowestPrice,");
            if (pIsUpdateNullField || pEntity.ReservationRemark != null)
                strSql.Append("[ReservationRemark]=@ReservationRemark,");
            if (pIsUpdateNullField || pEntity.NeedCreditCard != null)
                strSql.Append("[NeedCreditCard]=@NeedCreditCard,");
            if (pIsUpdateNullField || pEntity.NeedPrepay != null)
                strSql.Append("[NeedPrepay]=@NeedPrepay,");
            if (pIsUpdateNullField || pEntity.CancellationRemark != null)
                strSql.Append("[CancellationRemark]=@CancellationRemark,");
            if (pIsUpdateNullField || pEntity.Remark != null)
                strSql.Append("[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.Price1 != null)
                strSql.Append("[Price1]=@Price1,");
            if (pIsUpdateNullField || pEntity.Price2 != null)
                strSql.Append("[Price2]=@Price2,");
            if (pIsUpdateNullField || pEntity.Price3 != null)
                strSql.Append("[Price3]=@Price3,");
            if (pIsUpdateNullField || pEntity.Price4 != null)
                strSql.Append("[Price4]=@Price4,");
            if (pIsUpdateNullField || pEntity.Price5 != null)
                strSql.Append("[Price5]=@Price5,");
            if (pIsUpdateNullField || pEntity.Price6 != null)
                strSql.Append("[Price6]=@Price6,");
            if (pIsUpdateNullField || pEntity.Price7 != null)
                strSql.Append("[Price7]=@Price7,");
            if (pIsUpdateNullField || pEntity.Price8 != null)
                strSql.Append("[Price8]=@Price8,");
            if (pIsUpdateNullField || pEntity.Price9 != null)
                strSql.Append("[Price9]=@Price9,");
            if (pIsUpdateNullField || pEntity.Price10 != null)
                strSql.Append("[Price10]=@Price10,");
            if (pIsUpdateNullField || pEntity.Col1 != null)
                strSql.Append("[Col1]=@Col1,");
            if (pIsUpdateNullField || pEntity.Col2 != null)
                strSql.Append("[Col2]=@Col2,");
            if (pIsUpdateNullField || pEntity.Col3 != null)
                strSql.Append("[Col3]=@Col3,");
            if (pIsUpdateNullField || pEntity.Col4 != null)
                strSql.Append("[Col4]=@Col4,");
            if (pIsUpdateNullField || pEntity.Col5 != null)
                strSql.Append("[Col5]=@Col5,");
            if (pIsUpdateNullField || pEntity.Col6 != null)
                strSql.Append("[Col6]=@Col6,");
            if (pIsUpdateNullField || pEntity.Col7 != null)
                strSql.Append("[Col7]=@Col7,");
            if (pIsUpdateNullField || pEntity.Col8 != null)
                strSql.Append("[Col8]=@Col8,");
            if (pIsUpdateNullField || pEntity.Col9 != null)
                strSql.Append("[Col9]=@Col9,");
            if (pIsUpdateNullField || pEntity.Col10 != null)
                strSql.Append("[Col10]=@Col10,");
            if (pIsUpdateNullField || pEntity.ClientID != null)
                strSql.Append("[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.SourcePrice != null)
                strSql.Append("[SourcePrice]=@SourcePrice");
            strSql.Append(" where StoreItemDailyStatusID=@StoreItemDailyStatusID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@StoreID",SqlDbType.NVarChar),
					new SqlParameter("@ChannelID",SqlDbType.NVarChar),
					new SqlParameter("@SkuID",SqlDbType.NVarChar),
					new SqlParameter("@StatusDate",SqlDbType.Date),
					new SqlParameter("@CanReserveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@CanReserveEndTime",SqlDbType.DateTime),
					new SqlParameter("@StockAmount",SqlDbType.Int),
					new SqlParameter("@UsedAmount",SqlDbType.Int),
					new SqlParameter("@StorePrice",SqlDbType.Decimal),
					new SqlParameter("@LowestPrice",SqlDbType.Decimal),
					new SqlParameter("@ReservationRemark",SqlDbType.NVarChar),
					new SqlParameter("@NeedCreditCard",SqlDbType.Int),
					new SqlParameter("@NeedPrepay",SqlDbType.Int),
					new SqlParameter("@CancellationRemark",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@Price1",SqlDbType.Decimal),
					new SqlParameter("@Price2",SqlDbType.Decimal),
					new SqlParameter("@Price3",SqlDbType.Decimal),
					new SqlParameter("@Price4",SqlDbType.Decimal),
					new SqlParameter("@Price5",SqlDbType.Decimal),
					new SqlParameter("@Price6",SqlDbType.Decimal),
					new SqlParameter("@Price7",SqlDbType.Decimal),
					new SqlParameter("@Price8",SqlDbType.Decimal),
					new SqlParameter("@Price9",SqlDbType.Decimal),
					new SqlParameter("@Price10",SqlDbType.Decimal),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@SourcePrice",SqlDbType.Decimal),
					new SqlParameter("@StoreItemDailyStatusID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.StoreID;
            parameters[1].Value = pEntity.ChannelID;
            parameters[2].Value = pEntity.SkuID;
            parameters[3].Value = pEntity.StatusDate;
            parameters[4].Value = pEntity.CanReserveBeginTime;
            parameters[5].Value = pEntity.CanReserveEndTime;
            parameters[6].Value = pEntity.StockAmount;
            parameters[7].Value = pEntity.UsedAmount;
            parameters[8].Value = pEntity.StorePrice;
            parameters[9].Value = pEntity.LowestPrice;
            parameters[10].Value = pEntity.ReservationRemark;
            parameters[11].Value = pEntity.NeedCreditCard;
            parameters[12].Value = pEntity.NeedPrepay;
            parameters[13].Value = pEntity.CancellationRemark;
            parameters[14].Value = pEntity.Remark;
            parameters[15].Value = pEntity.Price1;
            parameters[16].Value = pEntity.Price2;
            parameters[17].Value = pEntity.Price3;
            parameters[18].Value = pEntity.Price4;
            parameters[19].Value = pEntity.Price5;
            parameters[20].Value = pEntity.Price6;
            parameters[21].Value = pEntity.Price7;
            parameters[22].Value = pEntity.Price8;
            parameters[23].Value = pEntity.Price9;
            parameters[24].Value = pEntity.Price10;
            parameters[25].Value = pEntity.Col1;
            parameters[26].Value = pEntity.Col2;
            parameters[27].Value = pEntity.Col3;
            parameters[28].Value = pEntity.Col4;
            parameters[29].Value = pEntity.Col5;
            parameters[30].Value = pEntity.Col6;
            parameters[31].Value = pEntity.Col7;
            parameters[32].Value = pEntity.Col8;
            parameters[33].Value = pEntity.Col9;
            parameters[34].Value = pEntity.Col10;
            parameters[35].Value = pEntity.ClientID;
            parameters[36].Value = pEntity.LastUpdateBy;
            parameters[37].Value = pEntity.LastUpdateTime;
            parameters[38].Value = pEntity.SourcePrice;
            parameters[39].Value = pEntity.StoreItemDailyStatusID;

            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(StoreItemDailyStatusEntity pEntity)
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(StoreItemDailyStatusEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(StoreItemDailyStatusEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.StoreItemDailyStatusID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.StoreItemDailyStatusID.Value, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return;
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [StoreItemDailyStatus] set  isdelete=1 where StoreItemDailyStatusID=@StoreItemDailyStatusID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@StoreItemDailyStatusID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(StoreItemDailyStatusEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.StoreItemDailyStatusID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.StoreItemDailyStatusID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(StoreItemDailyStatusEntity[] pEntities)
        {
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            if (pIDs == null || pIDs.Length == 0)
                return;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',", item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [StoreItemDailyStatus] set  isdelete=1 where StoreItemDailyStatusID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public StoreItemDailyStatusEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [StoreItemDailyStatus] where 1=1  and isdelete=0 ");
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
            //执行SQL
            List<StoreItemDailyStatusEntity> list = new List<StoreItemDailyStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    StoreItemDailyStatusEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<StoreItemDailyStatusEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [StoreItemDailyStatusID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [StoreItemDailyStatus] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [StoreItemDailyStatus] where 1=1  and isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<StoreItemDailyStatusEntity> result = new PagedQueryResult<StoreItemDailyStatusEntity>();
            List<StoreItemDailyStatusEntity> list = new List<StoreItemDailyStatusEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    StoreItemDailyStatusEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public StoreItemDailyStatusEntity[] QueryByEntity(StoreItemDailyStatusEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<StoreItemDailyStatusEntity> PagedQueryByEntity(StoreItemDailyStatusEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(StoreItemDailyStatusEntity pQueryEntity)
        {
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.StoreItemDailyStatusID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreItemDailyStatusID", Value = pQueryEntity.StoreItemDailyStatusID });
            if (pQueryEntity.StoreID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreID", Value = pQueryEntity.StoreID });
            if (pQueryEntity.ChannelID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelID", Value = pQueryEntity.ChannelID });
            if (pQueryEntity.SkuID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuID", Value = pQueryEntity.SkuID });
            if (pQueryEntity.StatusDate != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDate", Value = pQueryEntity.StatusDate });
            if (pQueryEntity.CanReserveBeginTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CanReserveBeginTime", Value = pQueryEntity.CanReserveBeginTime });
            if (pQueryEntity.CanReserveEndTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CanReserveEndTime", Value = pQueryEntity.CanReserveEndTime });
            if (pQueryEntity.StockAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StockAmount", Value = pQueryEntity.StockAmount });
            if (pQueryEntity.UsedAmount != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UsedAmount", Value = pQueryEntity.UsedAmount });
            if (pQueryEntity.StorePrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StorePrice", Value = pQueryEntity.StorePrice });
            if (pQueryEntity.LowestPrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LowestPrice", Value = pQueryEntity.LowestPrice });
            if (pQueryEntity.ReservationRemark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationRemark", Value = pQueryEntity.ReservationRemark });
            if (pQueryEntity.NeedCreditCard != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NeedCreditCard", Value = pQueryEntity.NeedCreditCard });
            if (pQueryEntity.NeedPrepay != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NeedPrepay", Value = pQueryEntity.NeedPrepay });
            if (pQueryEntity.CancellationRemark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CancellationRemark", Value = pQueryEntity.CancellationRemark });
            if (pQueryEntity.Remark != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.Price1 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price1", Value = pQueryEntity.Price1 });
            if (pQueryEntity.Price2 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price2", Value = pQueryEntity.Price2 });
            if (pQueryEntity.Price3 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price3", Value = pQueryEntity.Price3 });
            if (pQueryEntity.Price4 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price4", Value = pQueryEntity.Price4 });
            if (pQueryEntity.Price5 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price5", Value = pQueryEntity.Price5 });
            if (pQueryEntity.Price6 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price6", Value = pQueryEntity.Price6 });
            if (pQueryEntity.Price7 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price7", Value = pQueryEntity.Price7 });
            if (pQueryEntity.Price8 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price8", Value = pQueryEntity.Price8 });
            if (pQueryEntity.Price9 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price9", Value = pQueryEntity.Price9 });
            if (pQueryEntity.Price10 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price10", Value = pQueryEntity.Price10 });
            if (pQueryEntity.Col1 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col1", Value = pQueryEntity.Col1 });
            if (pQueryEntity.Col2 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col2", Value = pQueryEntity.Col2 });
            if (pQueryEntity.Col3 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col3", Value = pQueryEntity.Col3 });
            if (pQueryEntity.Col4 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col4", Value = pQueryEntity.Col4 });
            if (pQueryEntity.Col5 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col5", Value = pQueryEntity.Col5 });
            if (pQueryEntity.Col6 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col6", Value = pQueryEntity.Col6 });
            if (pQueryEntity.Col7 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col7", Value = pQueryEntity.Col7 });
            if (pQueryEntity.Col8 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col8", Value = pQueryEntity.Col8 });
            if (pQueryEntity.Col9 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col9", Value = pQueryEntity.Col9 });
            if (pQueryEntity.Col10 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col10", Value = pQueryEntity.Col10 });
            if (pQueryEntity.ClientID != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.CreateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.SourcePrice != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SourcePrice", Value = pQueryEntity.SourcePrice });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out StoreItemDailyStatusEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new StoreItemDailyStatusEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["StoreItemDailyStatusID"] != DBNull.Value)
            {
                pInstance.StoreItemDailyStatusID = (Guid)pReader["StoreItemDailyStatusID"];
            }
            if (pReader["StoreID"] != DBNull.Value)
            {
                pInstance.StoreID = Convert.ToString(pReader["StoreID"]);
            }
            if (pReader["ChannelID"] != DBNull.Value)
            {
                pInstance.ChannelID = Convert.ToString(pReader["ChannelID"]);
            }
            if (pReader["SkuID"] != DBNull.Value)
            {
                pInstance.SkuID = Convert.ToString(pReader["SkuID"]);
            }
            if (pReader["StatusDate"] != DBNull.Value)
            {
               // pInstance.StatusDate = pReader["StatusDate"];
                pInstance.StatusDate = Convert.ToDateTime(pReader["StatusDate"]);
            }
            if (pReader["CanReserveBeginTime"] != DBNull.Value)
            {
                pInstance.CanReserveBeginTime = Convert.ToDateTime(pReader["CanReserveBeginTime"]);
            }
            if (pReader["CanReserveEndTime"] != DBNull.Value)
            {
                pInstance.CanReserveEndTime = Convert.ToDateTime(pReader["CanReserveEndTime"]);
            }
            if (pReader["StockAmount"] != DBNull.Value)
            {
                pInstance.StockAmount = Convert.ToInt32(pReader["StockAmount"]);
            }
            if (pReader["UsedAmount"] != DBNull.Value)
            {
                pInstance.UsedAmount = Convert.ToInt32(pReader["UsedAmount"]);
            }
            if (pReader["StorePrice"] != DBNull.Value)
            {
                pInstance.StorePrice = Convert.ToDecimal(pReader["StorePrice"]);
            }
            if (pReader["LowestPrice"] != DBNull.Value)
            {
                pInstance.LowestPrice = Convert.ToDecimal(pReader["LowestPrice"]);
            }
            if (pReader["ReservationRemark"] != DBNull.Value)
            {
                pInstance.ReservationRemark = Convert.ToString(pReader["ReservationRemark"]);
            }
            if (pReader["NeedCreditCard"] != DBNull.Value)
            {
                pInstance.NeedCreditCard = Convert.ToInt32(pReader["NeedCreditCard"]);
            }
            if (pReader["NeedPrepay"] != DBNull.Value)
            {
                pInstance.NeedPrepay = Convert.ToInt32(pReader["NeedPrepay"]);
            }
            if (pReader["CancellationRemark"] != DBNull.Value)
            {
                pInstance.CancellationRemark = Convert.ToString(pReader["CancellationRemark"]);
            }
            if (pReader["Remark"] != DBNull.Value)
            {
                pInstance.Remark = Convert.ToString(pReader["Remark"]);
            }
            if (pReader["Price1"] != DBNull.Value)
            {
                pInstance.Price1 = Convert.ToDecimal(pReader["Price1"]);
            }
            if (pReader["Price2"] != DBNull.Value)
            {
                pInstance.Price2 = Convert.ToDecimal(pReader["Price2"]);
            }
            if (pReader["Price3"] != DBNull.Value)
            {
                pInstance.Price3 = Convert.ToDecimal(pReader["Price3"]);
            }
            if (pReader["Price4"] != DBNull.Value)
            {
                pInstance.Price4 = Convert.ToDecimal(pReader["Price4"]);
            }
            if (pReader["Price5"] != DBNull.Value)
            {
                pInstance.Price5 = Convert.ToDecimal(pReader["Price5"]);
            }
            if (pReader["Price6"] != DBNull.Value)
            {
                pInstance.Price6 = Convert.ToDecimal(pReader["Price6"]);
            }
            if (pReader["Price7"] != DBNull.Value)
            {
                pInstance.Price7 = Convert.ToDecimal(pReader["Price7"]);
            }
            if (pReader["Price8"] != DBNull.Value)
            {
                pInstance.Price8 = Convert.ToDecimal(pReader["Price8"]);
            }
            if (pReader["Price9"] != DBNull.Value)
            {
                pInstance.Price9 = Convert.ToDecimal(pReader["Price9"]);
            }
            if (pReader["Price10"] != DBNull.Value)
            {
                pInstance.Price10 = Convert.ToDecimal(pReader["Price10"]);
            }
            if (pReader["Col1"] != DBNull.Value)
            {
                pInstance.Col1 = Convert.ToString(pReader["Col1"]);
            }
            if (pReader["Col2"] != DBNull.Value)
            {
                pInstance.Col2 = Convert.ToString(pReader["Col2"]);
            }
            if (pReader["Col3"] != DBNull.Value)
            {
                pInstance.Col3 = Convert.ToString(pReader["Col3"]);
            }
            if (pReader["Col4"] != DBNull.Value)
            {
                pInstance.Col4 = Convert.ToString(pReader["Col4"]);
            }
            if (pReader["Col5"] != DBNull.Value)
            {
                pInstance.Col5 = Convert.ToString(pReader["Col5"]);
            }
            if (pReader["Col6"] != DBNull.Value)
            {
                pInstance.Col6 = Convert.ToString(pReader["Col6"]);
            }
            if (pReader["Col7"] != DBNull.Value)
            {
                pInstance.Col7 = Convert.ToString(pReader["Col7"]);
            }
            if (pReader["Col8"] != DBNull.Value)
            {
                pInstance.Col8 = Convert.ToString(pReader["Col8"]);
            }
            if (pReader["Col9"] != DBNull.Value)
            {
                pInstance.Col9 = Convert.ToString(pReader["Col9"]);
            }
            if (pReader["Col10"] != DBNull.Value)
            {
                pInstance.Col10 = Convert.ToString(pReader["Col10"]);
            }
            if (pReader["ClientID"] != DBNull.Value)
            {
                pInstance.ClientID = Convert.ToString(pReader["ClientID"]);
            }
            if (pReader["CreateBy"] != DBNull.Value)
            {
                pInstance.CreateBy = Convert.ToString(pReader["CreateBy"]);
            }
            if (pReader["CreateTime"] != DBNull.Value)
            {
                pInstance.CreateTime = Convert.ToDateTime(pReader["CreateTime"]);
            }
            if (pReader["LastUpdateBy"] != DBNull.Value)
            {
                pInstance.LastUpdateBy = Convert.ToString(pReader["LastUpdateBy"]);
            }
            if (pReader["LastUpdateTime"] != DBNull.Value)
            {
                pInstance.LastUpdateTime = Convert.ToDateTime(pReader["LastUpdateTime"]);
            }
            if (pReader["IsDelete"] != DBNull.Value)
            {
                pInstance.IsDelete = Convert.ToInt32(pReader["IsDelete"]);
            }
            if (pReader["SourcePrice"] != DBNull.Value)
            {
                pInstance.SourcePrice = Convert.ToDecimal(pReader["SourcePrice"]);
            }

        }
        #endregion
    }
}
