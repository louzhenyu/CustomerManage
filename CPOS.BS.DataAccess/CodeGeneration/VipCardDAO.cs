/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/28 11:20:43
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
    /// 表VipCard的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardDAO : Base.BaseCPOSDAO, ICRUDable<VipCardEntity>, IQueryable<VipCardEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipCardEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipCardEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [VipCard](");
            strSql.Append("[VipCardTypeID],[VipCardGradeID],[VipCardCode],[VipCardISN],[VipCardName],[BatchNo],[VipCardStatusId],[MembershipTime],[MembershipUnit],[BeginDate],[EndDate],[TotalAmount],[BalanceAmount],[BalancePoints],[BalanceBonus],[CumulativeBonus],[PurchaseTotalAmount],[PurchaseTotalCount],[CheckCode],[SingleTransLimit],[IsOverrunValid],[RechargeTotalAmount],[LastSalesTime],[IsGift],[SalesAmount],[SalesUserId],[SalesUserName],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[Operator],[VipCardID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardTypeID,@VipCardGradeID,@VipCardCode,@VipCardISN,@VipCardName,@BatchNo,@VipCardStatusId,@MembershipTime,@MembershipUnit,@BeginDate,@EndDate,@TotalAmount,@BalanceAmount,@BalancePoints,@BalanceBonus,@CumulativeBonus,@PurchaseTotalAmount,@PurchaseTotalCount,@CheckCode,@SingleTransLimit,@IsOverrunValid,@RechargeTotalAmount,@LastSalesTime,@IsGift,@SalesAmount,@SalesUserId,@SalesUserName,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@Operator,@VipCardID)");            

			string pkString = pEntity.VipCardID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@VipCardGradeID",SqlDbType.Int),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardISN",SqlDbType.NVarChar),
					new SqlParameter("@VipCardName",SqlDbType.NVarChar),
					new SqlParameter("@BatchNo",SqlDbType.NVarChar),
					new SqlParameter("@VipCardStatusId",SqlDbType.Int),
					new SqlParameter("@MembershipTime",SqlDbType.DateTime),
					new SqlParameter("@MembershipUnit",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.NVarChar),
					new SqlParameter("@EndDate",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceAmount",SqlDbType.Decimal),
					new SqlParameter("@BalancePoints",SqlDbType.Decimal),
					new SqlParameter("@BalanceBonus",SqlDbType.Decimal),
					new SqlParameter("@CumulativeBonus",SqlDbType.Decimal),
					new SqlParameter("@PurchaseTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PurchaseTotalCount",SqlDbType.Int),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@SingleTransLimit",SqlDbType.Decimal),
					new SqlParameter("@IsOverrunValid",SqlDbType.Int),
					new SqlParameter("@RechargeTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@LastSalesTime",SqlDbType.DateTime),
					new SqlParameter("@IsGift",SqlDbType.Int),
					new SqlParameter("@SalesAmount",SqlDbType.NVarChar),
					new SqlParameter("@SalesUserId",SqlDbType.NVarChar),
					new SqlParameter("@SalesUserName",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Operator",SqlDbType.NVarChar),
					new SqlParameter("@VipCardID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.VipCardGradeID;
			parameters[2].Value = pEntity.VipCardCode;
			parameters[3].Value = pEntity.VipCardISN;
			parameters[4].Value = pEntity.VipCardName;
			parameters[5].Value = pEntity.BatchNo;
			parameters[6].Value = pEntity.VipCardStatusId;
			parameters[7].Value = pEntity.MembershipTime;
			parameters[8].Value = pEntity.MembershipUnit;
			parameters[9].Value = pEntity.BeginDate;
			parameters[10].Value = pEntity.EndDate;
			parameters[11].Value = pEntity.TotalAmount;
			parameters[12].Value = pEntity.BalanceAmount;
			parameters[13].Value = pEntity.BalancePoints;
			parameters[14].Value = pEntity.BalanceBonus;
			parameters[15].Value = pEntity.CumulativeBonus;
			parameters[16].Value = pEntity.PurchaseTotalAmount;
			parameters[17].Value = pEntity.PurchaseTotalCount;
			parameters[18].Value = pEntity.CheckCode;
			parameters[19].Value = pEntity.SingleTransLimit;
			parameters[20].Value = pEntity.IsOverrunValid;
			parameters[21].Value = pEntity.RechargeTotalAmount;
			parameters[22].Value = pEntity.LastSalesTime;
			parameters[23].Value = pEntity.IsGift;
			parameters[24].Value = pEntity.SalesAmount;
			parameters[25].Value = pEntity.SalesUserId;
			parameters[26].Value = pEntity.SalesUserName;
			parameters[27].Value = pEntity.CreateTime;
			parameters[28].Value = pEntity.CreateBy;
			parameters[29].Value = pEntity.LastUpdateTime;
			parameters[30].Value = pEntity.LastUpdateBy;
			parameters[31].Value = pEntity.IsDelete;
			parameters[32].Value = pEntity.CustomerID;
			parameters[33].Value = pEntity.Operator;
			parameters[34].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipCardID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCard] where VipCardID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardEntity m = null;
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
        public VipCardEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCard] where 1=1  and isdelete=0");
            //读取数据
            List<VipCardEntity> list = new List<VipCardEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardEntity m;
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
        public void Update(VipCardEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipCardEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipCardID == null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCard] set ");
                        if (pIsUpdateNullField || pEntity.VipCardTypeID!=null)
                strSql.Append( "[VipCardTypeID]=@VipCardTypeID,");
            if (pIsUpdateNullField || pEntity.VipCardGradeID!=null)
                strSql.Append( "[VipCardGradeID]=@VipCardGradeID,");
            if (pIsUpdateNullField || pEntity.VipCardCode!=null)
                strSql.Append( "[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.VipCardISN!=null)
                strSql.Append( "[VipCardISN]=@VipCardISN,");
            if (pIsUpdateNullField || pEntity.VipCardName!=null)
                strSql.Append( "[VipCardName]=@VipCardName,");
            if (pIsUpdateNullField || pEntity.BatchNo!=null)
                strSql.Append( "[BatchNo]=@BatchNo,");
            if (pIsUpdateNullField || pEntity.VipCardStatusId!=null)
                strSql.Append( "[VipCardStatusId]=@VipCardStatusId,");
            if (pIsUpdateNullField || pEntity.MembershipTime!=null)
                strSql.Append( "[MembershipTime]=@MembershipTime,");
            if (pIsUpdateNullField || pEntity.MembershipUnit!=null)
                strSql.Append( "[MembershipUnit]=@MembershipUnit,");
            if (pIsUpdateNullField || pEntity.BeginDate!=null)
                strSql.Append( "[BeginDate]=@BeginDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.BalanceAmount!=null)
                strSql.Append( "[BalanceAmount]=@BalanceAmount,");
            if (pIsUpdateNullField || pEntity.BalancePoints!=null)
                strSql.Append( "[BalancePoints]=@BalancePoints,");
            if (pIsUpdateNullField || pEntity.BalanceBonus!=null)
                strSql.Append( "[BalanceBonus]=@BalanceBonus,");
            if (pIsUpdateNullField || pEntity.CumulativeBonus!=null)
                strSql.Append( "[CumulativeBonus]=@CumulativeBonus,");
            if (pIsUpdateNullField || pEntity.PurchaseTotalAmount!=null)
                strSql.Append( "[PurchaseTotalAmount]=@PurchaseTotalAmount,");
            if (pIsUpdateNullField || pEntity.PurchaseTotalCount!=null)
                strSql.Append( "[PurchaseTotalCount]=@PurchaseTotalCount,");
            if (pIsUpdateNullField || pEntity.CheckCode!=null)
                strSql.Append( "[CheckCode]=@CheckCode,");
            if (pIsUpdateNullField || pEntity.SingleTransLimit!=null)
                strSql.Append( "[SingleTransLimit]=@SingleTransLimit,");
            if (pIsUpdateNullField || pEntity.IsOverrunValid!=null)
                strSql.Append( "[IsOverrunValid]=@IsOverrunValid,");
            if (pIsUpdateNullField || pEntity.RechargeTotalAmount!=null)
                strSql.Append( "[RechargeTotalAmount]=@RechargeTotalAmount,");
            if (pIsUpdateNullField || pEntity.LastSalesTime!=null)
                strSql.Append( "[LastSalesTime]=@LastSalesTime,");
            if (pIsUpdateNullField || pEntity.IsGift!=null)
                strSql.Append( "[IsGift]=@IsGift,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.SalesUserId!=null)
                strSql.Append( "[SalesUserId]=@SalesUserId,");
            if (pIsUpdateNullField || pEntity.SalesUserName!=null)
                strSql.Append( "[SalesUserName]=@SalesUserName,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.Operator!=null)
                strSql.Append( "[Operator]=@Operator");
            
            strSql.Append(" where VipCardID=@VipCardID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardTypeID",SqlDbType.Int),
					new SqlParameter("@VipCardGradeID",SqlDbType.Int),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardISN",SqlDbType.NVarChar),
					new SqlParameter("@VipCardName",SqlDbType.NVarChar),
					new SqlParameter("@BatchNo",SqlDbType.NVarChar),
					new SqlParameter("@VipCardStatusId",SqlDbType.Int),
					new SqlParameter("@MembershipTime",SqlDbType.DateTime),
					new SqlParameter("@MembershipUnit",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.NVarChar),
					new SqlParameter("@EndDate",SqlDbType.NVarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@BalanceAmount",SqlDbType.Decimal),
					new SqlParameter("@BalancePoints",SqlDbType.Decimal),
					new SqlParameter("@BalanceBonus",SqlDbType.Decimal),
					new SqlParameter("@CumulativeBonus",SqlDbType.Decimal),
					new SqlParameter("@PurchaseTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@PurchaseTotalCount",SqlDbType.Int),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@SingleTransLimit",SqlDbType.Decimal),
					new SqlParameter("@IsOverrunValid",SqlDbType.Int),
					new SqlParameter("@RechargeTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@LastSalesTime",SqlDbType.DateTime),
					new SqlParameter("@IsGift",SqlDbType.Int),
					new SqlParameter("@SalesAmount",SqlDbType.NVarChar),
					new SqlParameter("@SalesUserId",SqlDbType.NVarChar),
					new SqlParameter("@SalesUserName",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@Operator",SqlDbType.NVarChar),
					new SqlParameter("@VipCardID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipCardTypeID;
			parameters[1].Value = pEntity.VipCardGradeID;
			parameters[2].Value = pEntity.VipCardCode;
			parameters[3].Value = pEntity.VipCardISN;
			parameters[4].Value = pEntity.VipCardName;
			parameters[5].Value = pEntity.BatchNo;
			parameters[6].Value = pEntity.VipCardStatusId;
			parameters[7].Value = pEntity.MembershipTime;
			parameters[8].Value = pEntity.MembershipUnit;
			parameters[9].Value = pEntity.BeginDate;
			parameters[10].Value = pEntity.EndDate;
			parameters[11].Value = pEntity.TotalAmount;
			parameters[12].Value = pEntity.BalanceAmount;
			parameters[13].Value = pEntity.BalancePoints;
			parameters[14].Value = pEntity.BalanceBonus;
			parameters[15].Value = pEntity.CumulativeBonus;
			parameters[16].Value = pEntity.PurchaseTotalAmount;
			parameters[17].Value = pEntity.PurchaseTotalCount;
			parameters[18].Value = pEntity.CheckCode;
			parameters[19].Value = pEntity.SingleTransLimit;
			parameters[20].Value = pEntity.IsOverrunValid;
			parameters[21].Value = pEntity.RechargeTotalAmount;
			parameters[22].Value = pEntity.LastSalesTime;
			parameters[23].Value = pEntity.IsGift;
			parameters[24].Value = pEntity.SalesAmount;
			parameters[25].Value = pEntity.SalesUserId;
			parameters[26].Value = pEntity.SalesUserName;
			parameters[27].Value = pEntity.LastUpdateTime;
			parameters[28].Value = pEntity.LastUpdateBy;
			parameters[29].Value = pEntity.CustomerID;
			parameters[30].Value = pEntity.Operator;
			parameters[31].Value = pEntity.VipCardID;

            //执行语句
            string sql = strSql.ToString().Replace(", where", " where");

            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql, parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(VipCardEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipCardID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipCardID, pTran);           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //组织参数化SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [VipCard] set  isdelete=1 where VipCardID=@VipCardID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipCardID",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //执行语句
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.VipCardID == null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.VipCardID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipCardEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //组织参数化SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [VipCard] set  isdelete=1 where VipCardID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //执行语句
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public VipCardEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCard] where 1=1  and isdelete=0 ");
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
            List<VipCardEntity> list = new List<VipCardEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardEntity m;
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
        public PagedQueryResult<VipCardEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [VipCardID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCard] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipCard] where 1=1  and isdelete=0 ");
            //过滤条件
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
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipCardEntity> result = new PagedQueryResult<VipCardEntity>();
            List<VipCardEntity> list = new List<VipCardEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardEntity m;
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
        public VipCardEntity[] QueryByEntity(VipCardEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<VipCardEntity> PagedQueryByEntity(VipCardEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 工具方法
        /// <summary>
        /// 根据实体非Null属性生成查询条件。
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipCardID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardID", Value = pQueryEntity.VipCardID });
            if (pQueryEntity.VipCardTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.VipCardGradeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardGradeID", Value = pQueryEntity.VipCardGradeID });
            if (pQueryEntity.VipCardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.VipCardISN!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardISN", Value = pQueryEntity.VipCardISN });
            if (pQueryEntity.VipCardName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardName", Value = pQueryEntity.VipCardName });
            if (pQueryEntity.BatchNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BatchNo", Value = pQueryEntity.BatchNo });
            if (pQueryEntity.VipCardStatusId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardStatusId", Value = pQueryEntity.VipCardStatusId });
            if (pQueryEntity.MembershipTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MembershipTime", Value = pQueryEntity.MembershipTime });
            if (pQueryEntity.MembershipUnit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MembershipUnit", Value = pQueryEntity.MembershipUnit });
            if (pQueryEntity.BeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginDate", Value = pQueryEntity.BeginDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.BalanceAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BalanceAmount", Value = pQueryEntity.BalanceAmount });
            if (pQueryEntity.BalancePoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BalancePoints", Value = pQueryEntity.BalancePoints });
            if (pQueryEntity.BalanceBonus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BalanceBonus", Value = pQueryEntity.BalanceBonus });
            if (pQueryEntity.CumulativeBonus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CumulativeBonus", Value = pQueryEntity.CumulativeBonus });
            if (pQueryEntity.PurchaseTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseTotalAmount", Value = pQueryEntity.PurchaseTotalAmount });
            if (pQueryEntity.PurchaseTotalCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PurchaseTotalCount", Value = pQueryEntity.PurchaseTotalCount });
            if (pQueryEntity.CheckCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckCode", Value = pQueryEntity.CheckCode });
            if (pQueryEntity.SingleTransLimit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SingleTransLimit", Value = pQueryEntity.SingleTransLimit });
            if (pQueryEntity.IsOverrunValid!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsOverrunValid", Value = pQueryEntity.IsOverrunValid });
            if (pQueryEntity.RechargeTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RechargeTotalAmount", Value = pQueryEntity.RechargeTotalAmount });
            if (pQueryEntity.LastSalesTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastSalesTime", Value = pQueryEntity.LastSalesTime });
            if (pQueryEntity.IsGift!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsGift", Value = pQueryEntity.IsGift });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.SalesUserId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUserId", Value = pQueryEntity.SalesUserId });
            if (pQueryEntity.SalesUserName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesUserName", Value = pQueryEntity.SalesUserName });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Operator!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Operator", Value = pQueryEntity.Operator });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out VipCardEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipCardEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipCardID"] != DBNull.Value)
			{
				pInstance.VipCardID =  Convert.ToString(pReader["VipCardID"]);
			}
			if (pReader["VipCardTypeID"] != DBNull.Value)
			{
				pInstance.VipCardTypeID =   Convert.ToInt32(pReader["VipCardTypeID"]);
			}
			if (pReader["VipCardGradeID"] != DBNull.Value)
			{
				pInstance.VipCardGradeID =   Convert.ToInt32(pReader["VipCardGradeID"]);
			}
			if (pReader["VipCardCode"] != DBNull.Value)
			{
				pInstance.VipCardCode =  Convert.ToString(pReader["VipCardCode"]);
			}
			if (pReader["VipCardISN"] != DBNull.Value)
			{
				pInstance.VipCardISN =  Convert.ToString(pReader["VipCardISN"]);
			}
			if (pReader["VipCardName"] != DBNull.Value)
			{
				pInstance.VipCardName =  Convert.ToString(pReader["VipCardName"]);
			}
			if (pReader["BatchNo"] != DBNull.Value)
			{
				pInstance.BatchNo =  Convert.ToString(pReader["BatchNo"]);
			}
			if (pReader["VipCardStatusId"] != DBNull.Value)
			{
				pInstance.VipCardStatusId =   Convert.ToInt32(pReader["VipCardStatusId"]);
			}
			if (pReader["MembershipTime"] != DBNull.Value)
			{
				pInstance.MembershipTime =  Convert.ToDateTime(pReader["MembershipTime"]);
			}
			if (pReader["MembershipUnit"] != DBNull.Value)
			{
				pInstance.MembershipUnit =  Convert.ToString(pReader["MembershipUnit"]);
			}
			if (pReader["BeginDate"] != DBNull.Value)
			{
				pInstance.BeginDate =  Convert.ToString(pReader["BeginDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToString(pReader["EndDate"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["BalanceAmount"] != DBNull.Value)
			{
				pInstance.BalanceAmount =  Convert.ToDecimal(pReader["BalanceAmount"]);
			}
			if (pReader["BalancePoints"] != DBNull.Value)
			{
				pInstance.BalancePoints =  Convert.ToDecimal(pReader["BalancePoints"]);
			}
			if (pReader["BalanceBonus"] != DBNull.Value)
			{
				pInstance.BalanceBonus =  Convert.ToDecimal(pReader["BalanceBonus"]);
			}
			if (pReader["CumulativeBonus"] != DBNull.Value)
			{
				pInstance.CumulativeBonus =  Convert.ToDecimal(pReader["CumulativeBonus"]);
			}
			if (pReader["PurchaseTotalAmount"] != DBNull.Value)
			{
				pInstance.PurchaseTotalAmount =  Convert.ToDecimal(pReader["PurchaseTotalAmount"]);
			}
			if (pReader["PurchaseTotalCount"] != DBNull.Value)
			{
				pInstance.PurchaseTotalCount =   Convert.ToInt32(pReader["PurchaseTotalCount"]);
			}
			if (pReader["CheckCode"] != DBNull.Value)
			{
				pInstance.CheckCode =  Convert.ToString(pReader["CheckCode"]);
			}
			if (pReader["SingleTransLimit"] != DBNull.Value)
			{
				pInstance.SingleTransLimit =  Convert.ToDecimal(pReader["SingleTransLimit"]);
			}
			if (pReader["IsOverrunValid"] != DBNull.Value)
			{
				pInstance.IsOverrunValid =   Convert.ToInt32(pReader["IsOverrunValid"]);
			}
			if (pReader["RechargeTotalAmount"] != DBNull.Value)
			{
				pInstance.RechargeTotalAmount =  Convert.ToDecimal(pReader["RechargeTotalAmount"]);
			}
			if (pReader["LastSalesTime"] != DBNull.Value)
			{
				pInstance.LastSalesTime =  Convert.ToDateTime(pReader["LastSalesTime"]);
			}
			if (pReader["IsGift"] != DBNull.Value)
			{
				pInstance.IsGift =   Convert.ToInt32(pReader["IsGift"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToString(pReader["SalesAmount"]);
			}
			if (pReader["SalesUserId"] != DBNull.Value)
			{
				pInstance.SalesUserId =  Convert.ToString(pReader["SalesUserId"]);
			}
			if (pReader["SalesUserName"] != DBNull.Value)
			{
				pInstance.SalesUserName =  Convert.ToString(pReader["SalesUserName"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["Operator"] != DBNull.Value)
			{
				pInstance.Operator =  Convert.ToString(pReader["Operator"]);
			}

        }
        #endregion
    }
}
