/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016-3-6 11:18:16
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
    /// 表SysVipCardType的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysVipCardTypeDAO : Base.BaseCPOSDAO, ICRUDable<SysVipCardTypeEntity>, IQueryable<SysVipCardTypeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysVipCardTypeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(SysVipCardTypeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(SysVipCardTypeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [SysVipCardType](");
            strSql.Append("[Category],[VipCardTypeCode],[VipCardTypeName],[VipCardLevel],[IsPassword],[AddUpAmount],[IsExpandVip],[PreferentialAmount],[SalesPreferentiaAmount],[IntegralMultiples],[Isprepaid],[IsPoints],[IsDiscount],[IsOnlineRecharge],[IsRegName],[IsUseCoupon],[IsBindECard],[PicUrl],[UpgradeAmount],[UpgradeOnceAmount],[UpgradePoint],[ExchangeIntegral],[Prices],[IsExtraMoney],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[IsOnlineSales])");
            strSql.Append(" values (");
            strSql.Append("@Category,@VipCardTypeCode,@VipCardTypeName,@VipCardLevel,@IsPassword,@AddUpAmount,@IsExpandVip,@PreferentialAmount,@SalesPreferentiaAmount,@IntegralMultiples,@Isprepaid,@IsPoints,@IsDiscount,@IsOnlineRecharge,@IsRegName,@IsUseCoupon,@IsBindECard,@PicUrl,@UpgradeAmount,@UpgradeOnceAmount,@UpgradePoint,@ExchangeIntegral,@Prices,@IsExtraMoney,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@IsOnlineSales)");            
			strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@Category",SqlDbType.Int),
					new SqlParameter("@VipCardTypeCode",SqlDbType.VarChar),
					new SqlParameter("@VipCardTypeName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardLevel",SqlDbType.Int),
					new SqlParameter("@IsPassword",SqlDbType.Int),
					new SqlParameter("@AddUpAmount",SqlDbType.Decimal),
					new SqlParameter("@IsExpandVip",SqlDbType.Int),
					new SqlParameter("@PreferentialAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesPreferentiaAmount",SqlDbType.Decimal),
					new SqlParameter("@IntegralMultiples",SqlDbType.Int),
					new SqlParameter("@Isprepaid",SqlDbType.Int),
					new SqlParameter("@IsPoints",SqlDbType.Int),
					new SqlParameter("@IsDiscount",SqlDbType.Int),
					new SqlParameter("@IsOnlineRecharge",SqlDbType.Int),
					new SqlParameter("@IsRegName",SqlDbType.Int),
					new SqlParameter("@IsUseCoupon",SqlDbType.Int),
					new SqlParameter("@IsBindECard",SqlDbType.Int),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@UpgradeAmount",SqlDbType.Decimal),
					new SqlParameter("@UpgradeOnceAmount",SqlDbType.Decimal),
					new SqlParameter("@UpgradePoint",SqlDbType.Int),
					new SqlParameter("@ExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@Prices",SqlDbType.Decimal),
					new SqlParameter("@IsExtraMoney",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@IsOnlineSales",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.Category;
			parameters[1].Value = pEntity.VipCardTypeCode;
			parameters[2].Value = pEntity.VipCardTypeName;
			parameters[3].Value = pEntity.VipCardLevel;
			parameters[4].Value = pEntity.IsPassword;
			parameters[5].Value = pEntity.AddUpAmount;
			parameters[6].Value = pEntity.IsExpandVip;
			parameters[7].Value = pEntity.PreferentialAmount;
			parameters[8].Value = pEntity.SalesPreferentiaAmount;
			parameters[9].Value = pEntity.IntegralMultiples;
			parameters[10].Value = pEntity.Isprepaid;
			parameters[11].Value = pEntity.IsPoints;
			parameters[12].Value = pEntity.IsDiscount;
			parameters[13].Value = pEntity.IsOnlineRecharge;
			parameters[14].Value = pEntity.IsRegName;
			parameters[15].Value = pEntity.IsUseCoupon;
			parameters[16].Value = pEntity.IsBindECard;
			parameters[17].Value = pEntity.PicUrl;
			parameters[18].Value = pEntity.UpgradeAmount;
			parameters[19].Value = pEntity.UpgradeOnceAmount;
			parameters[20].Value = pEntity.UpgradePoint;
			parameters[21].Value = pEntity.ExchangeIntegral;
			parameters[22].Value = pEntity.Prices;
			parameters[23].Value = pEntity.IsExtraMoney;
			parameters[24].Value = pEntity.CustomerID;
			parameters[25].Value = pEntity.CreateTime;
			parameters[26].Value = pEntity.CreateBy;
			parameters[27].Value = pEntity.LastUpdateTime;
			parameters[28].Value = pEntity.LastUpdateBy;
			parameters[29].Value = pEntity.IsDelete;
			parameters[30].Value = pEntity.IsOnlineSales;

            //执行并将结果回写
            object result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipCardTypeID = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public SysVipCardTypeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardType] where VipCardTypeID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            SysVipCardTypeEntity m = null;
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
        public SysVipCardTypeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardType] where 1=1  and isdelete=0");
            //读取数据
            List<SysVipCardTypeEntity> list = new List<SysVipCardTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardTypeEntity m;
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
        public void Update(SysVipCardTypeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(SysVipCardTypeEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardTypeID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysVipCardType] set ");
                        if (pIsUpdateNullField || pEntity.Category!=null)
                strSql.Append( "[Category]=@Category,");
            if (pIsUpdateNullField || pEntity.VipCardTypeCode!=null)
                strSql.Append( "[VipCardTypeCode]=@VipCardTypeCode,");
            if (pIsUpdateNullField || pEntity.VipCardTypeName!=null)
                strSql.Append( "[VipCardTypeName]=@VipCardTypeName,");
            if (pIsUpdateNullField || pEntity.VipCardLevel!=null)
                strSql.Append( "[VipCardLevel]=@VipCardLevel,");
            if (pIsUpdateNullField || pEntity.IsPassword!=null)
                strSql.Append( "[IsPassword]=@IsPassword,");
            if (pIsUpdateNullField || pEntity.AddUpAmount!=null)
                strSql.Append( "[AddUpAmount]=@AddUpAmount,");
            if (pIsUpdateNullField || pEntity.IsExpandVip!=null)
                strSql.Append( "[IsExpandVip]=@IsExpandVip,");
            if (pIsUpdateNullField || pEntity.PreferentialAmount!=null)
                strSql.Append( "[PreferentialAmount]=@PreferentialAmount,");
            if (pIsUpdateNullField || pEntity.SalesPreferentiaAmount!=null)
                strSql.Append( "[SalesPreferentiaAmount]=@SalesPreferentiaAmount,");
            if (pIsUpdateNullField || pEntity.IntegralMultiples!=null)
                strSql.Append( "[IntegralMultiples]=@IntegralMultiples,");
            if (pIsUpdateNullField || pEntity.Isprepaid!=null)
                strSql.Append( "[Isprepaid]=@Isprepaid,");
            if (pIsUpdateNullField || pEntity.IsPoints!=null)
                strSql.Append( "[IsPoints]=@IsPoints,");
            if (pIsUpdateNullField || pEntity.IsDiscount!=null)
                strSql.Append( "[IsDiscount]=@IsDiscount,");
            if (pIsUpdateNullField || pEntity.IsOnlineRecharge!=null)
                strSql.Append( "[IsOnlineRecharge]=@IsOnlineRecharge,");
            if (pIsUpdateNullField || pEntity.IsRegName!=null)
                strSql.Append( "[IsRegName]=@IsRegName,");
            if (pIsUpdateNullField || pEntity.IsUseCoupon!=null)
                strSql.Append( "[IsUseCoupon]=@IsUseCoupon,");
            if (pIsUpdateNullField || pEntity.IsBindECard!=null)
                strSql.Append( "[IsBindECard]=@IsBindECard,");
            if (pIsUpdateNullField || pEntity.PicUrl!=null)
                strSql.Append( "[PicUrl]=@PicUrl,");
            if (pIsUpdateNullField || pEntity.UpgradeAmount!=null)
                strSql.Append( "[UpgradeAmount]=@UpgradeAmount,");
            if (pIsUpdateNullField || pEntity.UpgradeOnceAmount!=null)
                strSql.Append( "[UpgradeOnceAmount]=@UpgradeOnceAmount,");
            if (pIsUpdateNullField || pEntity.UpgradePoint!=null)
                strSql.Append( "[UpgradePoint]=@UpgradePoint,");
            if (pIsUpdateNullField || pEntity.ExchangeIntegral!=null)
                strSql.Append( "[ExchangeIntegral]=@ExchangeIntegral,");
            if (pIsUpdateNullField || pEntity.Prices!=null)
                strSql.Append( "[Prices]=@Prices,");
            if (pIsUpdateNullField || pEntity.IsExtraMoney!=null)
                strSql.Append( "[IsExtraMoney]=@IsExtraMoney,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.IsOnlineSales!=null)
                strSql.Append( "[IsOnlineSales]=@IsOnlineSales");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipCardTypeID=@VipCardTypeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Category",SqlDbType.Int),
					new SqlParameter("@VipCardTypeCode",SqlDbType.VarChar),
					new SqlParameter("@VipCardTypeName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardLevel",SqlDbType.Int),
					new SqlParameter("@IsPassword",SqlDbType.Int),
					new SqlParameter("@AddUpAmount",SqlDbType.Decimal),
					new SqlParameter("@IsExpandVip",SqlDbType.Int),
					new SqlParameter("@PreferentialAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesPreferentiaAmount",SqlDbType.Decimal),
					new SqlParameter("@IntegralMultiples",SqlDbType.Int),
					new SqlParameter("@Isprepaid",SqlDbType.Int),
					new SqlParameter("@IsPoints",SqlDbType.Int),
					new SqlParameter("@IsDiscount",SqlDbType.Int),
					new SqlParameter("@IsOnlineRecharge",SqlDbType.Int),
					new SqlParameter("@IsRegName",SqlDbType.Int),
					new SqlParameter("@IsUseCoupon",SqlDbType.Int),
					new SqlParameter("@IsBindECard",SqlDbType.Int),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@UpgradeAmount",SqlDbType.Decimal),
					new SqlParameter("@UpgradeOnceAmount",SqlDbType.Decimal),
					new SqlParameter("@UpgradePoint",SqlDbType.Int),
					new SqlParameter("@ExchangeIntegral",SqlDbType.Int),
					new SqlParameter("@Prices",SqlDbType.Decimal),
					new SqlParameter("@IsExtraMoney",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsOnlineSales",SqlDbType.Int),
					new SqlParameter("@VipCardTypeID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.Category;
			parameters[1].Value = pEntity.VipCardTypeCode;
			parameters[2].Value = pEntity.VipCardTypeName;
			parameters[3].Value = pEntity.VipCardLevel;
			parameters[4].Value = pEntity.IsPassword;
			parameters[5].Value = pEntity.AddUpAmount;
			parameters[6].Value = pEntity.IsExpandVip;
			parameters[7].Value = pEntity.PreferentialAmount;
			parameters[8].Value = pEntity.SalesPreferentiaAmount;
			parameters[9].Value = pEntity.IntegralMultiples;
			parameters[10].Value = pEntity.Isprepaid;
			parameters[11].Value = pEntity.IsPoints;
			parameters[12].Value = pEntity.IsDiscount;
			parameters[13].Value = pEntity.IsOnlineRecharge;
			parameters[14].Value = pEntity.IsRegName;
			parameters[15].Value = pEntity.IsUseCoupon;
			parameters[16].Value = pEntity.IsBindECard;
			parameters[17].Value = pEntity.PicUrl;
			parameters[18].Value = pEntity.UpgradeAmount;
			parameters[19].Value = pEntity.UpgradeOnceAmount;
			parameters[20].Value = pEntity.UpgradePoint;
			parameters[21].Value = pEntity.ExchangeIntegral;
			parameters[22].Value = pEntity.Prices;
			parameters[23].Value = pEntity.IsExtraMoney;
			parameters[24].Value = pEntity.CustomerID;
			parameters[25].Value = pEntity.LastUpdateTime;
			parameters[26].Value = pEntity.LastUpdateBy;
			parameters[27].Value = pEntity.IsOnlineSales;
			parameters[28].Value = pEntity.VipCardTypeID;

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
        public void Update(SysVipCardTypeEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysVipCardTypeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(SysVipCardTypeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VipCardTypeID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipCardTypeID.Value, pTran);           
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
            sql.AppendLine("update [SysVipCardType] set  isdelete=1 where VipCardTypeID=@VipCardTypeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipCardTypeID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(SysVipCardTypeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.VipCardTypeID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.VipCardTypeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(SysVipCardTypeEntity[] pEntities)
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
                primaryKeys.AppendFormat("{0},",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [SysVipCardType] set  isdelete=1 where VipCardTypeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysVipCardTypeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardType] where 1=1  and isdelete=0 ");
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
            List<SysVipCardTypeEntity> list = new List<SysVipCardTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardTypeEntity m;
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
        public PagedQueryResult<SysVipCardTypeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipCardTypeID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysVipCardType] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [SysVipCardType] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<SysVipCardTypeEntity> result = new PagedQueryResult<SysVipCardTypeEntity>();
            List<SysVipCardTypeEntity> list = new List<SysVipCardTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardTypeEntity m;
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
        public SysVipCardTypeEntity[] QueryByEntity(SysVipCardTypeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysVipCardTypeEntity> PagedQueryByEntity(SysVipCardTypeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysVipCardTypeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipCardTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeID", Value = pQueryEntity.VipCardTypeID });
            if (pQueryEntity.Category!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Category", Value = pQueryEntity.Category });
            if (pQueryEntity.VipCardTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeCode", Value = pQueryEntity.VipCardTypeCode });
            if (pQueryEntity.VipCardTypeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeName", Value = pQueryEntity.VipCardTypeName });
            if (pQueryEntity.VipCardLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardLevel", Value = pQueryEntity.VipCardLevel });
            if (pQueryEntity.IsPassword!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPassword", Value = pQueryEntity.IsPassword });
            if (pQueryEntity.AddUpAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AddUpAmount", Value = pQueryEntity.AddUpAmount });
            if (pQueryEntity.IsExpandVip!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsExpandVip", Value = pQueryEntity.IsExpandVip });
            if (pQueryEntity.PreferentialAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreferentialAmount", Value = pQueryEntity.PreferentialAmount });
            if (pQueryEntity.SalesPreferentiaAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesPreferentiaAmount", Value = pQueryEntity.SalesPreferentiaAmount });
            if (pQueryEntity.IntegralMultiples!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IntegralMultiples", Value = pQueryEntity.IntegralMultiples });
            if (pQueryEntity.Isprepaid!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Isprepaid", Value = pQueryEntity.Isprepaid });
            if (pQueryEntity.IsPoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPoints", Value = pQueryEntity.IsPoints });
            if (pQueryEntity.IsDiscount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDiscount", Value = pQueryEntity.IsDiscount });
            if (pQueryEntity.IsOnlineRecharge!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsOnlineRecharge", Value = pQueryEntity.IsOnlineRecharge });
            if (pQueryEntity.IsRegName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRegName", Value = pQueryEntity.IsRegName });
            if (pQueryEntity.IsUseCoupon!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsUseCoupon", Value = pQueryEntity.IsUseCoupon });
            if (pQueryEntity.IsBindECard!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsBindECard", Value = pQueryEntity.IsBindECard });
            if (pQueryEntity.PicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PicUrl", Value = pQueryEntity.PicUrl });
            if (pQueryEntity.UpgradeAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UpgradeAmount", Value = pQueryEntity.UpgradeAmount });
            if (pQueryEntity.UpgradeOnceAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UpgradeOnceAmount", Value = pQueryEntity.UpgradeOnceAmount });
            if (pQueryEntity.UpgradePoint!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UpgradePoint", Value = pQueryEntity.UpgradePoint });
            if (pQueryEntity.ExchangeIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExchangeIntegral", Value = pQueryEntity.ExchangeIntegral });
            if (pQueryEntity.Prices!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Prices", Value = pQueryEntity.Prices });
            if (pQueryEntity.IsExtraMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsExtraMoney", Value = pQueryEntity.IsExtraMoney });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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
            if (pQueryEntity.IsOnlineSales!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsOnlineSales", Value = pQueryEntity.IsOnlineSales });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out SysVipCardTypeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new SysVipCardTypeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

            if (pReader["VipCardTypeID"] != DBNull.Value)
            {
                pInstance.VipCardTypeID = Convert.ToInt32(pReader["VipCardTypeID"]);
            }
            if (pReader["Category"] != DBNull.Value)
            {
                pInstance.Category = Convert.ToInt32(pReader["Category"]);
            }
            if (pReader["VipCardTypeCode"] != DBNull.Value)
            {
                pInstance.VipCardTypeCode = Convert.ToString(pReader["VipCardTypeCode"]);
            }
            if (pReader["VipCardTypeName"] != DBNull.Value)
            {
                pInstance.VipCardTypeName = Convert.ToString(pReader["VipCardTypeName"]);
            }
            if (pReader["VipCardLevel"] != DBNull.Value)
            {
                pInstance.VipCardLevel = Convert.ToInt32(pReader["VipCardLevel"]);
            }
            else { pInstance.VipCardLevel = 0; }
            if (pReader["IsPassword"] != DBNull.Value)
            {
                pInstance.IsPassword = Convert.ToInt32(pReader["IsPassword"]);
            }
            else { pInstance.IsPassword = 0; }
            if (pReader["AddUpAmount"] != DBNull.Value)
            {
                pInstance.AddUpAmount = Convert.ToDecimal(pReader["AddUpAmount"]);
            }
            else { pInstance.AddUpAmount = 0; }
            if (pReader["IsExpandVip"] != DBNull.Value)
            {
                pInstance.IsExpandVip = Convert.ToInt32(pReader["IsExpandVip"]);
            }
            if (pReader["PreferentialAmount"] != DBNull.Value)
            {
                pInstance.PreferentialAmount = Convert.ToDecimal(pReader["PreferentialAmount"]);
            }
            else { pInstance.PreferentialAmount = 0; }
            if (pReader["SalesPreferentiaAmount"] != DBNull.Value)
            {
                pInstance.SalesPreferentiaAmount = Convert.ToDecimal(pReader["SalesPreferentiaAmount"]);
            }
            else { pInstance.SalesPreferentiaAmount = 0; }
            if (pReader["IntegralMultiples"] != DBNull.Value)
            {
                pInstance.IntegralMultiples = Convert.ToInt32(pReader["IntegralMultiples"]);
            }
            else { pInstance.IntegralMultiples = 0; }
            if (pReader["Isprepaid"] != DBNull.Value)
            {
                pInstance.Isprepaid = Convert.ToInt32(pReader["Isprepaid"]);
            }
            else { pInstance.Isprepaid = 0; }
            if (pReader["IsPoints"] != DBNull.Value)
            {
                pInstance.IsPoints = Convert.ToInt32(pReader["IsPoints"]);
            }
            else { pInstance.IsPoints = 0; }
            if (pReader["IsDiscount"] != DBNull.Value)
            {
                pInstance.IsDiscount = Convert.ToInt32(pReader["IsDiscount"]);
            }
            else { pInstance.IsDiscount = 0; }
            if (pReader["IsOnlineRecharge"] != DBNull.Value)
            {
                pInstance.IsOnlineRecharge = Convert.ToInt32(pReader["IsOnlineRecharge"]);
            }
            else { pInstance.IsOnlineRecharge = 0; }
            if (pReader["IsRegName"] != DBNull.Value)
            {
                pInstance.IsRegName = Convert.ToInt32(pReader["IsRegName"]);
            }
            else { pInstance.IsRegName = 0; }
            if (pReader["IsUseCoupon"] != DBNull.Value)
            {
                pInstance.IsUseCoupon = Convert.ToInt32(pReader["IsUseCoupon"]);
            }
            else { pInstance.IsUseCoupon = 0; }
            if (pReader["IsBindECard"] != DBNull.Value)
            {
                pInstance.IsBindECard = Convert.ToInt32(pReader["IsBindECard"]);
            }
            else { pInstance.IsBindECard =0; }
            if (pReader["PicUrl"] != DBNull.Value)
            {
                pInstance.PicUrl = Convert.ToString(pReader["PicUrl"]);
            }
            if (pReader["UpgradeAmount"] != DBNull.Value)
            {
                pInstance.UpgradeAmount = Convert.ToDecimal(pReader["UpgradeAmount"]);
            }
            else { pInstance.UpgradeAmount = 0; }
            if (pReader["UpgradeOnceAmount"] != DBNull.Value)
            {
                pInstance.UpgradeOnceAmount = Convert.ToDecimal(pReader["UpgradeOnceAmount"]);
            }
            else { pInstance.UpgradeOnceAmount = 0; }
            if (pReader["UpgradePoint"] != DBNull.Value)
            {
                pInstance.UpgradePoint = Convert.ToInt32(pReader["UpgradePoint"]);
            }
            else { pInstance.UpgradePoint = 0; }
            if (pReader["ExchangeIntegral"] != DBNull.Value)
            {
                pInstance.ExchangeIntegral = Convert.ToInt32(pReader["ExchangeIntegral"]);
            }
            else { pInstance.ExchangeIntegral = 0; }
            if (pReader["Prices"] != DBNull.Value)
            {
                pInstance.Prices = Convert.ToDecimal(pReader["Prices"]);
            }
			if (pReader["IsExtraMoney"] != DBNull.Value)
			{
				pInstance.IsExtraMoney =   Convert.ToInt32(pReader["IsExtraMoney"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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
			if (pReader["IsOnlineSales"] != DBNull.Value)
			{
				pInstance.IsOnlineSales =   Convert.ToInt32(pReader["IsOnlineSales"]);
			}

        }
        #endregion
    }
}
