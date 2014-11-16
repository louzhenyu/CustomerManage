/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 14:57:03
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
    /// 表ClientBussinessDefined的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ClientBussinessDefinedDAO : Base.BaseCPOSDAO, ICRUDable<ClientBussinessDefinedEntity>, IQueryable<ClientBussinessDefinedEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        //public ClientBussinessDefinedDAO(LoggingSessionInfo pUserInfo)
        //    : base(pUserInfo)
        //{
        //}
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ClientBussinessDefinedEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ClientBussinessDefinedEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ClientBussinessDefined](");
            strSql.Append("[TableName],[ColumnName],[ColumnType],[ControlType],[MinLength],[MaxLength],[ColumnDesc],[ColumnDescEn],[HierarchyID],[CorrelationValue],[IsRead],[IsMustDo],[IsUse],[IsRepeat],[EditOrder],[ListOrder],[ConditionOrder],[GridWidth],[SqlDesc],[Remark],[ClientID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AttributeTypeID],[IsTemplate],[DisplayType],[IsDefaultProp],[ClientBussinessDefinedID])");
            strSql.Append(" values (");
            strSql.Append("@TableName,@ColumnName,@ColumnType,@ControlType,@MinLength,@MaxLength,@ColumnDesc,@ColumnDescEn,@HierarchyID,@CorrelationValue,@IsRead,@IsMustDo,@IsUse,@IsRepeat,@EditOrder,@ListOrder,@ConditionOrder,@GridWidth,@SqlDesc,@Remark,@ClientID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AttributeTypeID,@IsTemplate,@DisplayType,@IsDefaultProp,@ClientBussinessDefinedID)");            

			string pkString = pEntity.ClientBussinessDefinedID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@ColumnType",SqlDbType.Int),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@HierarchyID",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@IsRead",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@IsUse",SqlDbType.Int),
					new SqlParameter("@IsRepeat",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ListOrder",SqlDbType.Int),
					new SqlParameter("@ConditionOrder",SqlDbType.Int),
					new SqlParameter("@GridWidth",SqlDbType.Decimal),
					new SqlParameter("@SqlDesc",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@DisplayType",SqlDbType.Int),
					new SqlParameter("@IsDefaultProp",SqlDbType.Int),
					new SqlParameter("@ClientBussinessDefinedID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.ColumnName;
			parameters[2].Value = pEntity.ColumnType;
			parameters[3].Value = pEntity.ControlType;
			parameters[4].Value = pEntity.MinLength;
			parameters[5].Value = pEntity.MaxLength;
			parameters[6].Value = pEntity.ColumnDesc;
			parameters[7].Value = pEntity.ColumnDescEn;
			parameters[8].Value = pEntity.HierarchyID;
			parameters[9].Value = pEntity.CorrelationValue;
			parameters[10].Value = pEntity.IsRead;
			parameters[11].Value = pEntity.IsMustDo;
			parameters[12].Value = pEntity.IsUse;
			parameters[13].Value = pEntity.IsRepeat;
			parameters[14].Value = pEntity.EditOrder;
			parameters[15].Value = pEntity.ListOrder;
			parameters[16].Value = pEntity.ConditionOrder;
			parameters[17].Value = pEntity.GridWidth;
			parameters[18].Value = pEntity.SqlDesc;
			parameters[19].Value = pEntity.Remark;
			parameters[20].Value = pEntity.ClientID;
			parameters[21].Value = pEntity.CreateBy;
			parameters[22].Value = pEntity.CreateTime;
			parameters[23].Value = pEntity.LastUpdateBy;
			parameters[24].Value = pEntity.LastUpdateTime;
			parameters[25].Value = pEntity.IsDelete;
			parameters[26].Value = pEntity.AttributeTypeID;
			parameters[27].Value = pEntity.IsTemplate;
			parameters[28].Value = pEntity.DisplayType;
			parameters[29].Value = pEntity.IsDefaultProp;
			parameters[30].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ClientBussinessDefinedID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ClientBussinessDefinedEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientBussinessDefined] where ClientBussinessDefinedID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            ClientBussinessDefinedEntity m = null;
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
        public ClientBussinessDefinedEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientBussinessDefined] where isdelete=0");
            //读取数据
            List<ClientBussinessDefinedEntity> list = new List<ClientBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientBussinessDefinedEntity m;
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
        public void Update(ClientBussinessDefinedEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ClientBussinessDefinedEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ClientBussinessDefinedID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ClientBussinessDefined] set ");
            if (pIsUpdateNullField || pEntity.TableName!=null)
                strSql.Append( "[TableName]=@TableName,");
            if (pIsUpdateNullField || pEntity.ColumnName!=null)
                strSql.Append( "[ColumnName]=@ColumnName,");
            if (pIsUpdateNullField || pEntity.ColumnType!=null)
                strSql.Append( "[ColumnType]=@ColumnType,");
            if (pIsUpdateNullField || pEntity.ControlType!=null)
                strSql.Append( "[ControlType]=@ControlType,");
            if (pIsUpdateNullField || pEntity.MinLength!=null)
                strSql.Append( "[MinLength]=@MinLength,");
            if (pIsUpdateNullField || pEntity.MaxLength!=null)
                strSql.Append( "[MaxLength]=@MaxLength,");
            if (pIsUpdateNullField || pEntity.ColumnDesc!=null)
                strSql.Append( "[ColumnDesc]=@ColumnDesc,");
            if (pIsUpdateNullField || pEntity.ColumnDescEn!=null)
                strSql.Append( "[ColumnDescEn]=@ColumnDescEn,");
            if (pIsUpdateNullField || pEntity.HierarchyID!=null)
                strSql.Append( "[HierarchyID]=@HierarchyID,");
            if (pIsUpdateNullField || pEntity.CorrelationValue!=null)
                strSql.Append( "[CorrelationValue]=@CorrelationValue,");
            if (pIsUpdateNullField || pEntity.IsRead!=null)
                strSql.Append( "[IsRead]=@IsRead,");
            if (pIsUpdateNullField || pEntity.IsMustDo!=null)
                strSql.Append( "[IsMustDo]=@IsMustDo,");
            if (pIsUpdateNullField || pEntity.IsUse!=null)
                strSql.Append( "[IsUse]=@IsUse,");
            if (pIsUpdateNullField || pEntity.IsRepeat!=null)
                strSql.Append( "[IsRepeat]=@IsRepeat,");
            if (pIsUpdateNullField || pEntity.EditOrder!=null)
                strSql.Append( "[EditOrder]=@EditOrder,");
            if (pIsUpdateNullField || pEntity.ListOrder!=null)
                strSql.Append( "[ListOrder]=@ListOrder,");
            if (pIsUpdateNullField || pEntity.ConditionOrder!=null)
                strSql.Append( "[ConditionOrder]=@ConditionOrder,");
            if (pIsUpdateNullField || pEntity.GridWidth!=null)
                strSql.Append( "[GridWidth]=@GridWidth,");
            if (pIsUpdateNullField || pEntity.SqlDesc!=null)
                strSql.Append( "[SqlDesc]=@SqlDesc,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.ClientID!=null)
                strSql.Append( "[ClientID]=@ClientID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.AttributeTypeID!=null)
                strSql.Append( "[AttributeTypeID]=@AttributeTypeID,");
            if (pIsUpdateNullField || pEntity.IsTemplate!=null)
                strSql.Append( "[IsTemplate]=@IsTemplate,");
            if (pIsUpdateNullField || pEntity.DisplayType!=null)
                strSql.Append( "[DisplayType]=@DisplayType,");
            if (pIsUpdateNullField || pEntity.IsDefaultProp!=null)
                strSql.Append( "[IsDefaultProp]=@IsDefaultProp");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ClientBussinessDefinedID=@ClientBussinessDefinedID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@ColumnType",SqlDbType.Int),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@HierarchyID",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@IsRead",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@IsUse",SqlDbType.Int),
					new SqlParameter("@IsRepeat",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ListOrder",SqlDbType.Int),
					new SqlParameter("@ConditionOrder",SqlDbType.Int),
					new SqlParameter("@GridWidth",SqlDbType.Decimal),
					new SqlParameter("@SqlDesc",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@ClientID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@DisplayType",SqlDbType.Int),
					new SqlParameter("@IsDefaultProp",SqlDbType.Int),
					new SqlParameter("@ClientBussinessDefinedID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.ColumnName;
			parameters[2].Value = pEntity.ColumnType;
			parameters[3].Value = pEntity.ControlType;
			parameters[4].Value = pEntity.MinLength;
			parameters[5].Value = pEntity.MaxLength;
			parameters[6].Value = pEntity.ColumnDesc;
			parameters[7].Value = pEntity.ColumnDescEn;
			parameters[8].Value = pEntity.HierarchyID;
			parameters[9].Value = pEntity.CorrelationValue;
			parameters[10].Value = pEntity.IsRead;
			parameters[11].Value = pEntity.IsMustDo;
			parameters[12].Value = pEntity.IsUse;
			parameters[13].Value = pEntity.IsRepeat;
			parameters[14].Value = pEntity.EditOrder;
			parameters[15].Value = pEntity.ListOrder;
			parameters[16].Value = pEntity.ConditionOrder;
			parameters[17].Value = pEntity.GridWidth;
			parameters[18].Value = pEntity.SqlDesc;
			parameters[19].Value = pEntity.Remark;
			parameters[20].Value = pEntity.ClientID;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.AttributeTypeID;
			parameters[24].Value = pEntity.IsTemplate;
			parameters[25].Value = pEntity.DisplayType;
			parameters[26].Value = pEntity.IsDefaultProp;
			parameters[27].Value = pEntity.ClientBussinessDefinedID;

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
        public void Update(ClientBussinessDefinedEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ClientBussinessDefinedEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ClientBussinessDefinedEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ClientBussinessDefinedEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ClientBussinessDefinedID==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ClientBussinessDefinedID, pTran);           
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
            sql.AppendLine("update [ClientBussinessDefined] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ClientBussinessDefinedID=@ClientBussinessDefinedID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ClientBussinessDefinedID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ClientBussinessDefinedEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ClientBussinessDefinedID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ClientBussinessDefinedID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ClientBussinessDefinedEntity[] pEntities)
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
            sql.AppendLine("update [ClientBussinessDefined] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ClientBussinessDefinedID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ClientBussinessDefinedEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ClientBussinessDefined] where isdelete=0 ");
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
            List<ClientBussinessDefinedEntity> list = new List<ClientBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientBussinessDefinedEntity m;
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
        public PagedQueryResult<ClientBussinessDefinedEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ClientBussinessDefinedID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ClientBussinessDefined] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ClientBussinessDefined] where isdelete=0 ");
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
            PagedQueryResult<ClientBussinessDefinedEntity> result = new PagedQueryResult<ClientBussinessDefinedEntity>();
            List<ClientBussinessDefinedEntity> list = new List<ClientBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ClientBussinessDefinedEntity m;
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
        public ClientBussinessDefinedEntity[] QueryByEntity(ClientBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ClientBussinessDefinedEntity> PagedQueryByEntity(ClientBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ClientBussinessDefinedEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ClientBussinessDefinedID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientBussinessDefinedID", Value = pQueryEntity.ClientBussinessDefinedID });
            if (pQueryEntity.TableName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TableName", Value = pQueryEntity.TableName });
            if (pQueryEntity.ColumnName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnName", Value = pQueryEntity.ColumnName });
            if (pQueryEntity.ColumnType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnType", Value = pQueryEntity.ColumnType });
            if (pQueryEntity.ControlType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ControlType", Value = pQueryEntity.ControlType });
            if (pQueryEntity.MinLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinLength", Value = pQueryEntity.MinLength });
            if (pQueryEntity.MaxLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxLength", Value = pQueryEntity.MaxLength });
            if (pQueryEntity.ColumnDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDesc", Value = pQueryEntity.ColumnDesc });
            if (pQueryEntity.ColumnDescEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDescEn", Value = pQueryEntity.ColumnDescEn });
            if (pQueryEntity.HierarchyID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HierarchyID", Value = pQueryEntity.HierarchyID });
            if (pQueryEntity.CorrelationValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CorrelationValue", Value = pQueryEntity.CorrelationValue });
            if (pQueryEntity.IsRead!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRead", Value = pQueryEntity.IsRead });
            if (pQueryEntity.IsMustDo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDo", Value = pQueryEntity.IsMustDo });
            if (pQueryEntity.IsUse!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsUse", Value = pQueryEntity.IsUse });
            if (pQueryEntity.IsRepeat!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRepeat", Value = pQueryEntity.IsRepeat });
            if (pQueryEntity.EditOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EditOrder", Value = pQueryEntity.EditOrder });
            if (pQueryEntity.ListOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ListOrder", Value = pQueryEntity.ListOrder });
            if (pQueryEntity.ConditionOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConditionOrder", Value = pQueryEntity.ConditionOrder });
            if (pQueryEntity.GridWidth!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GridWidth", Value = pQueryEntity.GridWidth });
            if (pQueryEntity.SqlDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SqlDesc", Value = pQueryEntity.SqlDesc });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
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
            if (pQueryEntity.DisplayType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayType", Value = pQueryEntity.DisplayType });
            if (pQueryEntity.IsDefaultProp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDefaultProp", Value = pQueryEntity.IsDefaultProp });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ClientBussinessDefinedEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ClientBussinessDefinedEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ClientBussinessDefinedID"] != DBNull.Value)
			{
				pInstance.ClientBussinessDefinedID =  Convert.ToString(pReader["ClientBussinessDefinedID"]);
			}
			if (pReader["TableName"] != DBNull.Value)
			{
				pInstance.TableName =  Convert.ToString(pReader["TableName"]);
			}
			if (pReader["ColumnName"] != DBNull.Value)
			{
				pInstance.ColumnName =  Convert.ToString(pReader["ColumnName"]);
			}
			if (pReader["ColumnType"] != DBNull.Value)
			{
				pInstance.ColumnType =   Convert.ToInt32(pReader["ColumnType"]);
			}
			if (pReader["ControlType"] != DBNull.Value)
			{
				pInstance.ControlType =   Convert.ToInt32(pReader["ControlType"]);
			}
			if (pReader["MinLength"] != DBNull.Value)
			{
				pInstance.MinLength =   Convert.ToInt32(pReader["MinLength"]);
			}
			if (pReader["MaxLength"] != DBNull.Value)
			{
				pInstance.MaxLength =   Convert.ToInt32(pReader["MaxLength"]);
			}
			if (pReader["ColumnDesc"] != DBNull.Value)
			{
				pInstance.ColumnDesc =  Convert.ToString(pReader["ColumnDesc"]);
			}
			if (pReader["ColumnDescEn"] != DBNull.Value)
			{
				pInstance.ColumnDescEn =  Convert.ToString(pReader["ColumnDescEn"]);
			}
			if (pReader["HierarchyID"] != DBNull.Value)
			{
				pInstance.HierarchyID =  Convert.ToString(pReader["HierarchyID"]);
			}
			if (pReader["CorrelationValue"] != DBNull.Value)
			{
				pInstance.CorrelationValue =  Convert.ToString(pReader["CorrelationValue"]);
			}
			if (pReader["IsRead"] != DBNull.Value)
			{
				pInstance.IsRead =   Convert.ToInt32(pReader["IsRead"]);
			}
			if (pReader["IsMustDo"] != DBNull.Value)
			{
				pInstance.IsMustDo =   Convert.ToInt32(pReader["IsMustDo"]);
			}
			if (pReader["IsUse"] != DBNull.Value)
			{
				pInstance.IsUse =   Convert.ToInt32(pReader["IsUse"]);
			}
			if (pReader["IsRepeat"] != DBNull.Value)
			{
				pInstance.IsRepeat =   Convert.ToInt32(pReader["IsRepeat"]);
			}
			if (pReader["EditOrder"] != DBNull.Value)
			{
				pInstance.EditOrder =   Convert.ToInt32(pReader["EditOrder"]);
			}
			if (pReader["ListOrder"] != DBNull.Value)
			{
				pInstance.ListOrder =   Convert.ToInt32(pReader["ListOrder"]);
			}
			if (pReader["ConditionOrder"] != DBNull.Value)
			{
				pInstance.ConditionOrder =   Convert.ToInt32(pReader["ConditionOrder"]);
			}
			if (pReader["GridWidth"] != DBNull.Value)
			{
				pInstance.GridWidth =  Convert.ToDecimal(pReader["GridWidth"]);
			}
			if (pReader["SqlDesc"] != DBNull.Value)
			{
				pInstance.SqlDesc =  Convert.ToString(pReader["SqlDesc"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =  Convert.ToString(pReader["ClientID"]);
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
			if (pReader["DisplayType"] != DBNull.Value)
			{
				pInstance.DisplayType =   Convert.ToInt32(pReader["DisplayType"]);
			}
			if (pReader["IsDefaultProp"] != DBNull.Value)
			{
				pInstance.IsDefaultProp =   Convert.ToInt32(pReader["IsDefaultProp"]);
			}

        }
        #endregion
    }
}
