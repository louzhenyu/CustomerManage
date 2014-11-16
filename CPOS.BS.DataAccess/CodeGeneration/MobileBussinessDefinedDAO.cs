/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/8 11:16:03
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
    /// 表MobileBussinessDefined的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MobileBussinessDefinedDAO : Base.BaseCPOSDAO, ICRUDable<MobileBussinessDefinedEntity>, IQueryable<MobileBussinessDefinedEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileBussinessDefinedDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(MobileBussinessDefinedEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(MobileBussinessDefinedEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MobileBussinessDefined](");
            strSql.Append("[TableName],[MobilePageBlockID],[ColumnDesc],[ColumnDescEn],[ColumnName],[LinkageItem],[CorrelationValue],[ExampleValue],[ControlType],[AuthType],[MinLength],[MaxLength],[MinSelected],[MaxSelected],[IsMustDo],[EditOrder],[ViewOrder],[TypeID],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AttributeTypeID],[IsTemplate],[MobileBussinessDefinedID])");
            strSql.Append(" values (");
            strSql.Append("@TableName,@MobilePageBlockID,@ColumnDesc,@ColumnDescEn,@ColumnName,@LinkageItem,@CorrelationValue,@ExampleValue,@ControlType,@AuthType,@MinLength,@MaxLength,@MinSelected,@MaxSelected,@IsMustDo,@EditOrder,@ViewOrder,@TypeID,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AttributeTypeID,@IsTemplate,@MobileBussinessDefinedID)");            

			Guid? pkGuid;
			if (pEntity.MobileBussinessDefinedID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.MobileBussinessDefinedID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@MobilePageBlockID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@LinkageItem",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@ExampleValue",SqlDbType.NVarChar),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@AuthType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@MinSelected",SqlDbType.Int),
					new SqlParameter("@MaxSelected",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ViewOrder",SqlDbType.Int),
					new SqlParameter("@TypeID",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@MobileBussinessDefinedID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.MobilePageBlockID;
			parameters[2].Value = pEntity.ColumnDesc;
			parameters[3].Value = pEntity.ColumnDescEn;
			parameters[4].Value = pEntity.ColumnName;
			parameters[5].Value = pEntity.LinkageItem;
			parameters[6].Value = pEntity.CorrelationValue;
			parameters[7].Value = pEntity.ExampleValue;
			parameters[8].Value = pEntity.ControlType;
			parameters[9].Value = pEntity.AuthType;
			parameters[10].Value = pEntity.MinLength;
			parameters[11].Value = pEntity.MaxLength;
			parameters[12].Value = pEntity.MinSelected;
			parameters[13].Value = pEntity.MaxSelected;
			parameters[14].Value = pEntity.IsMustDo;
			parameters[15].Value = pEntity.EditOrder;
			parameters[16].Value = pEntity.ViewOrder;
			parameters[17].Value = pEntity.TypeID;
			parameters[18].Value = pEntity.CustomerID;
			parameters[19].Value = pEntity.CreateBy;
			parameters[20].Value = pEntity.CreateTime;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.IsDelete;
			parameters[24].Value = pEntity.AttributeTypeID;
			parameters[25].Value = pEntity.IsTemplate;
			parameters[26].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.MobileBussinessDefinedID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public MobileBussinessDefinedEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where MobileBussinessDefinedID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            MobileBussinessDefinedEntity m = null;
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
        public MobileBussinessDefinedEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where isdelete=0");
            //读取数据
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public void Update(MobileBussinessDefinedEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MobileBussinessDefinedEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MobileBussinessDefinedID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MobileBussinessDefined] set ");
            if (pIsUpdateNullField || pEntity.TableName!=null)
                strSql.Append( "[TableName]=@TableName,");
            if (pIsUpdateNullField || pEntity.MobilePageBlockID!=null)
                strSql.Append( "[MobilePageBlockID]=@MobilePageBlockID,");
            if (pIsUpdateNullField || pEntity.ColumnDesc!=null)
                strSql.Append( "[ColumnDesc]=@ColumnDesc,");
            if (pIsUpdateNullField || pEntity.ColumnDescEn!=null)
                strSql.Append( "[ColumnDescEn]=@ColumnDescEn,");
            if (pIsUpdateNullField || pEntity.ColumnName!=null)
                strSql.Append( "[ColumnName]=@ColumnName,");
            if (pIsUpdateNullField || pEntity.LinkageItem!=null)
                strSql.Append( "[LinkageItem]=@LinkageItem,");
            if (pIsUpdateNullField || pEntity.CorrelationValue!=null)
                strSql.Append( "[CorrelationValue]=@CorrelationValue,");
            if (pIsUpdateNullField || pEntity.ExampleValue!=null)
                strSql.Append( "[ExampleValue]=@ExampleValue,");
            if (pIsUpdateNullField || pEntity.ControlType!=null)
                strSql.Append( "[ControlType]=@ControlType,");
            if (pIsUpdateNullField || pEntity.AuthType!=null)
                strSql.Append( "[AuthType]=@AuthType,");
            if (pIsUpdateNullField || pEntity.MinLength!=null)
                strSql.Append( "[MinLength]=@MinLength,");
            if (pIsUpdateNullField || pEntity.MaxLength!=null)
                strSql.Append( "[MaxLength]=@MaxLength,");
            if (pIsUpdateNullField || pEntity.MinSelected!=null)
                strSql.Append( "[MinSelected]=@MinSelected,");
            if (pIsUpdateNullField || pEntity.MaxSelected!=null)
                strSql.Append( "[MaxSelected]=@MaxSelected,");
            if (pIsUpdateNullField || pEntity.IsMustDo!=null)
                strSql.Append( "[IsMustDo]=@IsMustDo,");
            if (pIsUpdateNullField || pEntity.EditOrder!=null)
                strSql.Append( "[EditOrder]=@EditOrder,");
            if (pIsUpdateNullField || pEntity.ViewOrder!=null)
                strSql.Append( "[ViewOrder]=@ViewOrder,");
            if (pIsUpdateNullField || pEntity.TypeID!=null)
                strSql.Append( "[TypeID]=@TypeID,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.AttributeTypeID!=null)
                strSql.Append( "[AttributeTypeID]=@AttributeTypeID,");
            if (pIsUpdateNullField || pEntity.IsTemplate!=null)
                strSql.Append( "[IsTemplate]=@IsTemplate");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MobileBussinessDefinedID=@MobileBussinessDefinedID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@MobilePageBlockID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@LinkageItem",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@ExampleValue",SqlDbType.NVarChar),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@AuthType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@MinSelected",SqlDbType.Int),
					new SqlParameter("@MaxSelected",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ViewOrder",SqlDbType.Int),
					new SqlParameter("@TypeID",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@MobileBussinessDefinedID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.MobilePageBlockID;
			parameters[2].Value = pEntity.ColumnDesc;
			parameters[3].Value = pEntity.ColumnDescEn;
			parameters[4].Value = pEntity.ColumnName;
			parameters[5].Value = pEntity.LinkageItem;
			parameters[6].Value = pEntity.CorrelationValue;
			parameters[7].Value = pEntity.ExampleValue;
			parameters[8].Value = pEntity.ControlType;
			parameters[9].Value = pEntity.AuthType;
			parameters[10].Value = pEntity.MinLength;
			parameters[11].Value = pEntity.MaxLength;
			parameters[12].Value = pEntity.MinSelected;
			parameters[13].Value = pEntity.MaxSelected;
			parameters[14].Value = pEntity.IsMustDo;
			parameters[15].Value = pEntity.EditOrder;
			parameters[16].Value = pEntity.ViewOrder;
			parameters[17].Value = pEntity.TypeID;
			parameters[18].Value = pEntity.CustomerID;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.AttributeTypeID;
			parameters[22].Value = pEntity.IsTemplate;
			parameters[23].Value = pEntity.MobileBussinessDefinedID;

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
        public void Update(MobileBussinessDefinedEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MobileBussinessDefinedEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MobileBussinessDefinedEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(MobileBussinessDefinedEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MobileBussinessDefinedID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.MobileBussinessDefinedID, pTran);           
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
            sql.AppendLine("update [MobileBussinessDefined] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MobileBussinessDefinedID=@MobileBussinessDefinedID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MobileBussinessDefinedID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(MobileBussinessDefinedEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MobileBussinessDefinedID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.MobileBussinessDefinedID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(MobileBussinessDefinedEntity[] pEntities)
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
            sql.AppendLine("update [MobileBussinessDefined] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where MobileBussinessDefinedID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MobileBussinessDefinedEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where isdelete=0 ");
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
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public PagedQueryResult<MobileBussinessDefinedEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MobileBussinessDefinedID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [MobileBussinessDefined] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [MobileBussinessDefined] where isdelete=0 ");
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
            PagedQueryResult<MobileBussinessDefinedEntity> result = new PagedQueryResult<MobileBussinessDefinedEntity>();
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public MobileBussinessDefinedEntity[] QueryByEntity(MobileBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MobileBussinessDefinedEntity> PagedQueryByEntity(MobileBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MobileBussinessDefinedEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MobileBussinessDefinedID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobileBussinessDefinedID", Value = pQueryEntity.MobileBussinessDefinedID });
            if (pQueryEntity.TableName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TableName", Value = pQueryEntity.TableName });
            if (pQueryEntity.MobilePageBlockID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobilePageBlockID", Value = pQueryEntity.MobilePageBlockID });
            if (pQueryEntity.ColumnDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDesc", Value = pQueryEntity.ColumnDesc });
            if (pQueryEntity.ColumnDescEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDescEn", Value = pQueryEntity.ColumnDescEn });
            if (pQueryEntity.ColumnName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnName", Value = pQueryEntity.ColumnName });
            if (pQueryEntity.LinkageItem!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LinkageItem", Value = pQueryEntity.LinkageItem });
            if (pQueryEntity.CorrelationValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CorrelationValue", Value = pQueryEntity.CorrelationValue });
            if (pQueryEntity.ExampleValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExampleValue", Value = pQueryEntity.ExampleValue });
            if (pQueryEntity.ControlType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ControlType", Value = pQueryEntity.ControlType });
            if (pQueryEntity.AuthType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AuthType", Value = pQueryEntity.AuthType });
            if (pQueryEntity.MinLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinLength", Value = pQueryEntity.MinLength });
            if (pQueryEntity.MaxLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxLength", Value = pQueryEntity.MaxLength });
            if (pQueryEntity.MinSelected!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinSelected", Value = pQueryEntity.MinSelected });
            if (pQueryEntity.MaxSelected!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxSelected", Value = pQueryEntity.MaxSelected });
            if (pQueryEntity.IsMustDo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDo", Value = pQueryEntity.IsMustDo });
            if (pQueryEntity.EditOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EditOrder", Value = pQueryEntity.EditOrder });
            if (pQueryEntity.ViewOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ViewOrder", Value = pQueryEntity.ViewOrder });
            if (pQueryEntity.TypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TypeID", Value = pQueryEntity.TypeID });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.AttributeTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AttributeTypeID", Value = pQueryEntity.AttributeTypeID });
            if (pQueryEntity.IsTemplate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTemplate", Value = pQueryEntity.IsTemplate });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out MobileBussinessDefinedEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new MobileBussinessDefinedEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["MobileBussinessDefinedID"] != DBNull.Value)
			{
				pInstance.MobileBussinessDefinedID =  (Guid)pReader["MobileBussinessDefinedID"];
			}
			if (pReader["TableName"] != DBNull.Value)
			{
				pInstance.TableName =  Convert.ToString(pReader["TableName"]);
			}
			if (pReader["MobilePageBlockID"] != DBNull.Value)
			{
				pInstance.MobilePageBlockID =  (Guid)pReader["MobilePageBlockID"];
			}
			if (pReader["ColumnDesc"] != DBNull.Value)
			{
				pInstance.ColumnDesc =  Convert.ToString(pReader["ColumnDesc"]);
			}
			if (pReader["ColumnDescEn"] != DBNull.Value)
			{
				pInstance.ColumnDescEn =  Convert.ToString(pReader["ColumnDescEn"]);
			}
			if (pReader["ColumnName"] != DBNull.Value)
			{
				pInstance.ColumnName =  Convert.ToString(pReader["ColumnName"]);
			}
			if (pReader["LinkageItem"] != DBNull.Value)
			{
				pInstance.LinkageItem =  Convert.ToString(pReader["LinkageItem"]);
			}
			if (pReader["CorrelationValue"] != DBNull.Value)
			{
				pInstance.CorrelationValue =  Convert.ToString(pReader["CorrelationValue"]);
			}
			if (pReader["ExampleValue"] != DBNull.Value)
			{
				pInstance.ExampleValue =  Convert.ToString(pReader["ExampleValue"]);
			}
			if (pReader["ControlType"] != DBNull.Value)
			{
				pInstance.ControlType =   Convert.ToInt32(pReader["ControlType"]);
			}
			if (pReader["AuthType"] != DBNull.Value)
			{
				pInstance.AuthType =   Convert.ToInt32(pReader["AuthType"]);
			}
			if (pReader["MinLength"] != DBNull.Value)
			{
				pInstance.MinLength =   Convert.ToInt32(pReader["MinLength"]);
			}
			if (pReader["MaxLength"] != DBNull.Value)
			{
				pInstance.MaxLength =   Convert.ToInt32(pReader["MaxLength"]);
			}
			if (pReader["MinSelected"] != DBNull.Value)
			{
				pInstance.MinSelected =   Convert.ToInt32(pReader["MinSelected"]);
			}
			if (pReader["MaxSelected"] != DBNull.Value)
			{
				pInstance.MaxSelected =   Convert.ToInt32(pReader["MaxSelected"]);
			}
			if (pReader["IsMustDo"] != DBNull.Value)
			{
				pInstance.IsMustDo =   Convert.ToInt32(pReader["IsMustDo"]);
			}
			if (pReader["EditOrder"] != DBNull.Value)
			{
				pInstance.EditOrder =   Convert.ToInt32(pReader["EditOrder"]);
			}
			if (pReader["ViewOrder"] != DBNull.Value)
			{
				pInstance.ViewOrder =   Convert.ToInt32(pReader["ViewOrder"]);
			}
			if (pReader["TypeID"] != DBNull.Value)
			{
				pInstance.TypeID =   Convert.ToInt32(pReader["TypeID"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["AttributeTypeID"] != DBNull.Value)
			{
				pInstance.AttributeTypeID =   Convert.ToInt32(pReader["AttributeTypeID"]);
			}
			if (pReader["IsTemplate"] != DBNull.Value)
			{
				pInstance.IsTemplate =   Convert.ToInt32(pReader["IsTemplate"]);
			}

        }
        #endregion
    }
}
