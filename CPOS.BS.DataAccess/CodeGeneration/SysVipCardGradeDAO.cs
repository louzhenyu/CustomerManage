/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/11 9:12:03
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
    /// 数据访问：  
    /// 表SysVipCardGrade的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SysVipCardGradeDAO : Base.BaseCPOSDAO, ICRUDable<SysVipCardGradeEntity>, IQueryable<SysVipCardGradeEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SysVipCardGradeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(SysVipCardGradeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(SysVipCardGradeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //初始化固定字段
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SysVipCardGrade](");
            strSql.Append("[VipCardGradeName],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[AddUpAmount],[IsExpandVip],[PreferentialAmount],[SalesPreferentiaAmount],[IntegralMultiples],[BeVip],[Remark],[BeginIntegral],[EndIntegral],[VipCardGradeID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardGradeName,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@AddUpAmount,@IsExpandVip,@PreferentialAmount,@SalesPreferentiaAmount,@IntegralMultiples,@BeVip,@Remark,@BeginIntegral,@EndIntegral,@VipCardGradeID)");            

			string pkString = Convert.ToString( pEntity.VipCardGradeID);

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardGradeName",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@AddUpAmount",SqlDbType.Decimal),
					new SqlParameter("@IsExpandVip",SqlDbType.Int),
					new SqlParameter("@PreferentialAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesPreferentiaAmount",SqlDbType.Decimal),
					new SqlParameter("@IntegralMultiples",SqlDbType.Decimal),
					new SqlParameter("@BeVip",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@BeginIntegral",SqlDbType.Int),
					new SqlParameter("@EndIntegral",SqlDbType.Int),
					new SqlParameter("@VipCardGradeID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.VipCardGradeName;
			parameters[1].Value = pEntity.CreateTime;
			parameters[2].Value = pEntity.CreateBy;
			parameters[3].Value = pEntity.LastUpdateTime;
			parameters[4].Value = pEntity.LastUpdateBy;
			parameters[5].Value = pEntity.IsDelete;
			parameters[6].Value = pEntity.CustomerID;
			parameters[7].Value = pEntity.AddUpAmount;
			parameters[8].Value = pEntity.IsExpandVip;
			parameters[9].Value = pEntity.PreferentialAmount;
			parameters[10].Value = pEntity.SalesPreferentiaAmount;
			parameters[11].Value = pEntity.IntegralMultiples;
			parameters[12].Value = pEntity.BeVip;
			parameters[13].Value = pEntity.Remark;
			parameters[14].Value = pEntity.BeginIntegral;
			parameters[15].Value = pEntity.EndIntegral;
			parameters[16].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipCardGradeID = Convert.ToInt32(pkString);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public SysVipCardGradeEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardGrade] where VipCardGradeID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            SysVipCardGradeEntity m = null;
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
        public SysVipCardGradeEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardGrade] where isdelete=0");
            //读取数据
            List<SysVipCardGradeEntity> list = new List<SysVipCardGradeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardGradeEntity m;
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
        public void Update(SysVipCardGradeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(SysVipCardGradeEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipCardGradeID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysVipCardGrade] set ");
            if (pIsUpdateNullField || pEntity.VipCardGradeName!=null)
                strSql.Append( "[VipCardGradeName]=@VipCardGradeName,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
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
            if (pIsUpdateNullField || pEntity.BeVip!=null)
                strSql.Append( "[BeVip]=@BeVip,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.BeginIntegral!=null)
                strSql.Append( "[BeginIntegral]=@BeginIntegral,");
            if (pIsUpdateNullField || pEntity.EndIntegral!=null)
                strSql.Append( "[EndIntegral]=@EndIntegral");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipCardGradeID=@VipCardGradeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardGradeName",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@AddUpAmount",SqlDbType.Decimal),
					new SqlParameter("@IsExpandVip",SqlDbType.Int),
					new SqlParameter("@PreferentialAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesPreferentiaAmount",SqlDbType.Decimal),
					new SqlParameter("@IntegralMultiples",SqlDbType.Decimal),
					new SqlParameter("@BeVip",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@BeginIntegral",SqlDbType.Int),
					new SqlParameter("@EndIntegral",SqlDbType.Int),
					new SqlParameter("@VipCardGradeID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.VipCardGradeName;
			parameters[1].Value = pEntity.LastUpdateTime;
			parameters[2].Value = pEntity.LastUpdateBy;
			parameters[3].Value = pEntity.CustomerID;
			parameters[4].Value = pEntity.AddUpAmount;
			parameters[5].Value = pEntity.IsExpandVip;
			parameters[6].Value = pEntity.PreferentialAmount;
			parameters[7].Value = pEntity.SalesPreferentiaAmount;
			parameters[8].Value = pEntity.IntegralMultiples;
			parameters[9].Value = pEntity.BeVip;
			parameters[10].Value = pEntity.Remark;
			parameters[11].Value = pEntity.BeginIntegral;
			parameters[12].Value = pEntity.EndIntegral;
			parameters[13].Value = pEntity.VipCardGradeID;

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
        public void Update(SysVipCardGradeEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(SysVipCardGradeEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysVipCardGradeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(SysVipCardGradeEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipCardGradeID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipCardGradeID, pTran);           
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
            sql.AppendLine("update [SysVipCardGrade] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VipCardGradeID=@VipCardGradeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@VipCardGradeID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(SysVipCardGradeEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.VipCardGradeID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.VipCardGradeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(SysVipCardGradeEntity[] pEntities)
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
            sql.AppendLine("update [SysVipCardGrade] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VipCardGradeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysVipCardGradeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVipCardGrade] where isdelete=0 ");
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
            List<SysVipCardGradeEntity> list = new List<SysVipCardGradeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardGradeEntity m;
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
        public PagedQueryResult<SysVipCardGradeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipCardGradeID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysVipCardGrade] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [SysVipCardGrade] where isdelete=0 ");
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
            PagedQueryResult<SysVipCardGradeEntity> result = new PagedQueryResult<SysVipCardGradeEntity>();
            List<SysVipCardGradeEntity> list = new List<SysVipCardGradeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardGradeEntity m;
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
        public SysVipCardGradeEntity[] QueryByEntity(SysVipCardGradeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysVipCardGradeEntity> PagedQueryByEntity(SysVipCardGradeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysVipCardGradeEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipCardGradeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardGradeID", Value = pQueryEntity.VipCardGradeID });
            if (pQueryEntity.VipCardGradeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardGradeName", Value = pQueryEntity.VipCardGradeName });
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
            if (pQueryEntity.BeVip!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeVip", Value = pQueryEntity.BeVip });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.BeginIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginIntegral", Value = pQueryEntity.BeginIntegral });
            if (pQueryEntity.EndIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndIntegral", Value = pQueryEntity.EndIntegral });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out SysVipCardGradeEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new SysVipCardGradeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipCardGradeID"] != DBNull.Value)
			{
				pInstance.VipCardGradeID =   Convert.ToInt32(pReader["VipCardGradeID"]);
			}
			if (pReader["VipCardGradeName"] != DBNull.Value)
			{
				pInstance.VipCardGradeName =  Convert.ToString(pReader["VipCardGradeName"]);
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
			if (pReader["AddUpAmount"] != DBNull.Value)
			{
				pInstance.AddUpAmount =  Convert.ToDecimal(pReader["AddUpAmount"]);
			}
			if (pReader["IsExpandVip"] != DBNull.Value)
			{
				pInstance.IsExpandVip =   Convert.ToInt32(pReader["IsExpandVip"]);
			}
			if (pReader["PreferentialAmount"] != DBNull.Value)
			{
				pInstance.PreferentialAmount =  Convert.ToDecimal(pReader["PreferentialAmount"]);
			}
			if (pReader["SalesPreferentiaAmount"] != DBNull.Value)
			{
				pInstance.SalesPreferentiaAmount =  Convert.ToDecimal(pReader["SalesPreferentiaAmount"]);
			}
			if (pReader["IntegralMultiples"] != DBNull.Value)
			{
				pInstance.IntegralMultiples =  Convert.ToDecimal(pReader["IntegralMultiples"]);
			}
			if (pReader["BeVip"] != DBNull.Value)
			{
				pInstance.BeVip =  Convert.ToString(pReader["BeVip"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["BeginIntegral"] != DBNull.Value)
			{
				pInstance.BeginIntegral =   Convert.ToInt32(pReader["BeginIntegral"]);
			}
			if (pReader["EndIntegral"] != DBNull.Value)
			{
				pInstance.EndIntegral =   Convert.ToInt32(pReader["EndIntegral"]);
			}

        }
        #endregion
    }
}
