/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:49
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 拜访数据表 
    /// 表VisitingTaskData的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingTaskDataDAO : BaseCPOSDAO, ICRUDable<VisitingTaskDataEntity>, IQueryable<VisitingTaskDataEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDataDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo, true)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VisitingTaskDataEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VisitingTaskDataEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VisitingTaskData](");
            strSql.Append("[ClientUserID],[POPID],[VisitingTaskID],[InTime],[InPic],[InCoordinate],[InGPSType],[OutTime],[OutPic],[OutCoordinate],[OutGPSType],[Remark],[ClientID],[ClientDistributorID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VisitingTaskDataID])");
            strSql.Append(" values (");
            strSql.Append("@ClientUserID,@POPID,@VisitingTaskID,@InTime,@InPic,@InCoordinate,@InGPSType,@OutTime,@OutPic,@OutCoordinate,@OutGPSType,@Remark,@ClientID,@ClientDistributorID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VisitingTaskDataID)");            

			Guid? pkGuid;
			if (pEntity.VisitingTaskDataID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.VisitingTaskDataID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientUserID",SqlDbType.VarChar,36),
					new SqlParameter("@POPID",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskID",SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@InTime",SqlDbType.DateTime),
					new SqlParameter("@InPic",SqlDbType.NVarChar,400),
					new SqlParameter("@InCoordinate",SqlDbType.NVarChar,100),
					new SqlParameter("@InGPSType",SqlDbType.Int),
					new SqlParameter("@OutTime",SqlDbType.DateTime),
					new SqlParameter("@OutPic",SqlDbType.NVarChar,400),
					new SqlParameter("@OutCoordinate",SqlDbType.NVarChar,100),
					new SqlParameter("@OutGPSType",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar,400),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VisitingTaskDataID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.ClientUserID;
			parameters[1].Value = pEntity.POPID;
			parameters[2].Value = pEntity.VisitingTaskID;
			parameters[3].Value = pEntity.InTime;
			parameters[4].Value = pEntity.InPic;
			parameters[5].Value = pEntity.InCoordinate;
			parameters[6].Value = pEntity.InGPSType;
			parameters[7].Value = pEntity.OutTime;
			parameters[8].Value = pEntity.OutPic;
			parameters[9].Value = pEntity.OutCoordinate;
			parameters[10].Value = pEntity.OutGPSType;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.ClientID;
			parameters[13].Value = pEntity.ClientDistributorID;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pkGuid;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VisitingTaskDataID = pkGuid;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VisitingTaskDataEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTaskData] where VisitingTaskDataID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VisitingTaskDataEntity m = null;
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
        public VisitingTaskDataEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTaskData] where isdelete=0");
            //读取数据
            List<VisitingTaskDataEntity> list = new List<VisitingTaskDataEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskDataEntity m;
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
        public void Update(VisitingTaskDataEntity pEntity , IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingTaskDataID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VisitingTaskData] set ");
            strSql.Append("[ClientUserID]=@ClientUserID,[POPID]=@POPID,[VisitingTaskID]=@VisitingTaskID,[InTime]=@InTime,[InPic]=@InPic,[InCoordinate]=@InCoordinate,[InGPSType]=@InGPSType,[OutTime]=@OutTime,[OutPic]=@OutPic,[OutCoordinate]=@OutCoordinate,[OutGPSType]=@OutGPSType,[Remark]=@Remark,[ClientID]=@ClientID,[ClientDistributorID]=@ClientDistributorID,[LastUpdateBy]=@LastUpdateBy,[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where VisitingTaskDataID=@VisitingTaskDataID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ClientUserID",SqlDbType.VarChar,36),
					new SqlParameter("@POPID",SqlDbType.NVarChar,100),
					new SqlParameter("@VisitingTaskID",SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@InTime",SqlDbType.DateTime),
					new SqlParameter("@InPic",SqlDbType.NVarChar,400),
					new SqlParameter("@InCoordinate",SqlDbType.NVarChar,100),
					new SqlParameter("@InGPSType",SqlDbType.Int),
					new SqlParameter("@OutTime",SqlDbType.DateTime),
					new SqlParameter("@OutPic",SqlDbType.NVarChar,400),
					new SqlParameter("@OutCoordinate",SqlDbType.NVarChar,100),
					new SqlParameter("@OutGPSType",SqlDbType.Int),
					new SqlParameter("@Remark",SqlDbType.NVarChar,400),
					new SqlParameter("@ClientID",SqlDbType.VarChar,36),
					new SqlParameter("@ClientDistributorID",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar,36),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VisitingTaskDataID",SqlDbType.UniqueIdentifier,16)
            };
			parameters[0].Value = pEntity.ClientUserID;
			parameters[1].Value = pEntity.POPID;
			parameters[2].Value = pEntity.VisitingTaskID;
			parameters[3].Value = pEntity.InTime;
			parameters[4].Value = pEntity.InPic;
			parameters[5].Value = pEntity.InCoordinate;
			parameters[6].Value = pEntity.InGPSType;
			parameters[7].Value = pEntity.OutTime;
			parameters[8].Value = pEntity.OutPic;
			parameters[9].Value = pEntity.OutCoordinate;
			parameters[10].Value = pEntity.OutGPSType;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.ClientID;
			parameters[13].Value = pEntity.ClientDistributorID;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.VisitingTaskDataID;

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
        public void Update(VisitingTaskDataEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VisitingTaskDataEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VisitingTaskDataEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.VisitingTaskDataID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VisitingTaskDataID.Value, pTran);           
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
            sql.AppendLine("update [VisitingTaskData] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VisitingTaskDataID=@VisitingTaskDataID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=CurrentUserInfo.UserID},
                new SqlParameter{ParameterName="@VisitingTaskDataID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(VisitingTaskDataEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (!item.VisitingTaskDataID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.VisitingTaskDataID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VisitingTaskDataEntity[] pEntities)
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
            sql.AppendLine("update [VisitingTaskData] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where VisitingTaskDataID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VisitingTaskDataEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VisitingTaskData] where isdelete=0 ");
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
            List<VisitingTaskDataEntity> list = new List<VisitingTaskDataEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskDataEntity m;
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
        public PagedQueryResult<VisitingTaskDataEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VisitingTaskDataID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VisitingTaskData] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VisitingTaskData] where isdelete=0 ");
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
            PagedQueryResult<VisitingTaskDataEntity> result = new PagedQueryResult<VisitingTaskDataEntity>();
            List<VisitingTaskDataEntity> list = new List<VisitingTaskDataEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VisitingTaskDataEntity m;
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
        public VisitingTaskDataEntity[] QueryByEntity(VisitingTaskDataEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VisitingTaskDataEntity> PagedQueryByEntity(VisitingTaskDataEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VisitingTaskDataEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VisitingTaskDataID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskDataID", Value = pQueryEntity.VisitingTaskDataID });
            if (pQueryEntity.ClientUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientUserID", Value = pQueryEntity.ClientUserID });
            if (pQueryEntity.POPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "POPID", Value = pQueryEntity.POPID });
            if (pQueryEntity.VisitingTaskID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VisitingTaskID", Value = pQueryEntity.VisitingTaskID });
            if (pQueryEntity.InTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InTime", Value = pQueryEntity.InTime });
            if (pQueryEntity.InPic!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InPic", Value = pQueryEntity.InPic });
            if (pQueryEntity.InCoordinate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InCoordinate", Value = pQueryEntity.InCoordinate });
            if (pQueryEntity.InGPSType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InGPSType", Value = pQueryEntity.InGPSType });
            if (pQueryEntity.OutTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutTime", Value = pQueryEntity.OutTime });
            if (pQueryEntity.OutPic!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutPic", Value = pQueryEntity.OutPic });
            if (pQueryEntity.OutCoordinate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutCoordinate", Value = pQueryEntity.OutCoordinate });
            if (pQueryEntity.OutGPSType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutGPSType", Value = pQueryEntity.OutGPSType });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.ClientID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientID", Value = pQueryEntity.ClientID });
            if (pQueryEntity.ClientDistributorID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = pQueryEntity.ClientDistributorID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out VisitingTaskDataEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VisitingTaskDataEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VisitingTaskDataID"] != DBNull.Value)
			{
				pInstance.VisitingTaskDataID =  (Guid)pReader["VisitingTaskDataID"];
			}
			if (pReader["ClientUserID"] != DBNull.Value)
			{
				pInstance.ClientUserID =   Convert.ToInt32(pReader["ClientUserID"]);
			}
			if (pReader["POPID"] != DBNull.Value)
			{
				pInstance.POPID =  Convert.ToString(pReader["POPID"]);
			}
			if (pReader["VisitingTaskID"] != DBNull.Value)
			{
				pInstance.VisitingTaskID =  (Guid)pReader["VisitingTaskID"];
			}
			if (pReader["InTime"] != DBNull.Value)
			{
				pInstance.InTime =  Convert.ToDateTime(pReader["InTime"]);
			}
			if (pReader["InPic"] != DBNull.Value)
			{
				pInstance.InPic =  Convert.ToString(pReader["InPic"]);
			}
			if (pReader["InCoordinate"] != DBNull.Value)
			{
				pInstance.InCoordinate =  Convert.ToString(pReader["InCoordinate"]);
			}
			if (pReader["InGPSType"] != DBNull.Value)
			{
				pInstance.InGPSType =   Convert.ToInt32(pReader["InGPSType"]);
			}
			if (pReader["OutTime"] != DBNull.Value)
			{
				pInstance.OutTime =  Convert.ToDateTime(pReader["OutTime"]);
			}
			if (pReader["OutPic"] != DBNull.Value)
			{
				pInstance.OutPic =  Convert.ToString(pReader["OutPic"]);
			}
			if (pReader["OutCoordinate"] != DBNull.Value)
			{
				pInstance.OutCoordinate =  Convert.ToString(pReader["OutCoordinate"]);
			}
			if (pReader["OutGPSType"] != DBNull.Value)
			{
				pInstance.OutGPSType =   Convert.ToInt32(pReader["OutGPSType"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["ClientID"] != DBNull.Value)
			{
				pInstance.ClientID =   pReader["ClientID"].ToString();
			}
			if (pReader["ClientDistributorID"] != DBNull.Value)
			{
				pInstance.ClientDistributorID =   Convert.ToInt32(pReader["ClientDistributorID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =   pReader["CreateBy"].ToString();
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =   pReader["LastUpdateBy"].ToString();
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
