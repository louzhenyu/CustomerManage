/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/1 15:59:30
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
    /// 表VipExpandSinaWb的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipExpandSinaWbDAO : Base.BaseCPOSDAO, ICRUDable<VipExpandSinaWbEntity>, IQueryable<VipExpandSinaWbEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipExpandSinaWbDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipExpandSinaWbEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipExpandSinaWbEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipExpandSinaWb](");
            strSql.Append("[AccessToken],[UID],[Appkey],[Scope],[CreateAt],[ExpireIn],[ScreenName],[LabelName],[Province],[City],[Location],[Description],[Url],[ProfileImageUrl],[ProfileUrl],[Gender],[Weihao],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VipId])");
            strSql.Append(" values (");
            strSql.Append("@AccessToken,@UID,@Appkey,@Scope,@CreateAt,@ExpireIn,@ScreenName,@LabelName,@Province,@City,@Location,@Description,@Url,@ProfileImageUrl,@ProfileUrl,@Gender,@Weihao,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VipId)");            

			string pkString = pEntity.VipId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AccessToken",SqlDbType.NVarChar),
					new SqlParameter("@UID",SqlDbType.NVarChar),
					new SqlParameter("@Appkey",SqlDbType.NVarChar),
					new SqlParameter("@Scope",SqlDbType.NVarChar),
					new SqlParameter("@CreateAt",SqlDbType.NVarChar),
					new SqlParameter("@ExpireIn",SqlDbType.NVarChar),
					new SqlParameter("@ScreenName",SqlDbType.NVarChar),
					new SqlParameter("@LabelName",SqlDbType.NVarChar),
					new SqlParameter("@Province",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@Location",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@ProfileImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ProfileUrl",SqlDbType.NVarChar),
					new SqlParameter("@Gender",SqlDbType.NVarChar),
					new SqlParameter("@Weihao",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AccessToken;
			parameters[1].Value = pEntity.UID;
			parameters[2].Value = pEntity.Appkey;
			parameters[3].Value = pEntity.Scope;
			parameters[4].Value = pEntity.CreateAt;
			parameters[5].Value = pEntity.ExpireIn;
			parameters[6].Value = pEntity.ScreenName;
			parameters[7].Value = pEntity.LabelName;
			parameters[8].Value = pEntity.Province;
			parameters[9].Value = pEntity.City;
			parameters[10].Value = pEntity.Location;
			parameters[11].Value = pEntity.Description;
			parameters[12].Value = pEntity.Url;
			parameters[13].Value = pEntity.ProfileImageUrl;
			parameters[14].Value = pEntity.ProfileUrl;
			parameters[15].Value = pEntity.Gender;
			parameters[16].Value = pEntity.Weihao;
			parameters[17].Value = pEntity.CreateTime;
			parameters[18].Value = pEntity.CreateBy;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.IsDelete;
			parameters[22].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipExpandSinaWbEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipExpandSinaWb] where VipId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VipExpandSinaWbEntity m = null;
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
        public VipExpandSinaWbEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipExpandSinaWb] where isdelete=0");
            //读取数据
            List<VipExpandSinaWbEntity> list = new List<VipExpandSinaWbEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipExpandSinaWbEntity m;
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
        public void Update(VipExpandSinaWbEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VipExpandSinaWbEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipExpandSinaWb] set ");
            if (pIsUpdateNullField || pEntity.AccessToken!=null)
                strSql.Append( "[AccessToken]=@AccessToken,");
            if (pIsUpdateNullField || pEntity.UID!=null)
                strSql.Append( "[UID]=@UID,");
            if (pIsUpdateNullField || pEntity.Appkey!=null)
                strSql.Append( "[Appkey]=@Appkey,");
            if (pIsUpdateNullField || pEntity.Scope!=null)
                strSql.Append( "[Scope]=@Scope,");
            if (pIsUpdateNullField || pEntity.CreateAt!=null)
                strSql.Append( "[CreateAt]=@CreateAt,");
            if (pIsUpdateNullField || pEntity.ExpireIn!=null)
                strSql.Append( "[ExpireIn]=@ExpireIn,");
            if (pIsUpdateNullField || pEntity.ScreenName!=null)
                strSql.Append( "[ScreenName]=@ScreenName,");
            if (pIsUpdateNullField || pEntity.LabelName!=null)
                strSql.Append( "[LabelName]=@LabelName,");
            if (pIsUpdateNullField || pEntity.Province!=null)
                strSql.Append( "[Province]=@Province,");
            if (pIsUpdateNullField || pEntity.City!=null)
                strSql.Append( "[City]=@City,");
            if (pIsUpdateNullField || pEntity.Location!=null)
                strSql.Append( "[Location]=@Location,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.Url!=null)
                strSql.Append( "[Url]=@Url,");
            if (pIsUpdateNullField || pEntity.ProfileImageUrl!=null)
                strSql.Append( "[ProfileImageUrl]=@ProfileImageUrl,");
            if (pIsUpdateNullField || pEntity.ProfileUrl!=null)
                strSql.Append( "[ProfileUrl]=@ProfileUrl,");
            if (pIsUpdateNullField || pEntity.Gender!=null)
                strSql.Append( "[Gender]=@Gender,");
            if (pIsUpdateNullField || pEntity.Weihao!=null)
                strSql.Append( "[Weihao]=@Weihao,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipId=@VipId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AccessToken",SqlDbType.NVarChar),
					new SqlParameter("@UID",SqlDbType.NVarChar),
					new SqlParameter("@Appkey",SqlDbType.NVarChar),
					new SqlParameter("@Scope",SqlDbType.NVarChar),
					new SqlParameter("@CreateAt",SqlDbType.NVarChar),
					new SqlParameter("@ExpireIn",SqlDbType.NVarChar),
					new SqlParameter("@ScreenName",SqlDbType.NVarChar),
					new SqlParameter("@LabelName",SqlDbType.NVarChar),
					new SqlParameter("@Province",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@Location",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@ProfileImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ProfileUrl",SqlDbType.NVarChar),
					new SqlParameter("@Gender",SqlDbType.NVarChar),
					new SqlParameter("@Weihao",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VipId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AccessToken;
			parameters[1].Value = pEntity.UID;
			parameters[2].Value = pEntity.Appkey;
			parameters[3].Value = pEntity.Scope;
			parameters[4].Value = pEntity.CreateAt;
			parameters[5].Value = pEntity.ExpireIn;
			parameters[6].Value = pEntity.ScreenName;
			parameters[7].Value = pEntity.LabelName;
			parameters[8].Value = pEntity.Province;
			parameters[9].Value = pEntity.City;
			parameters[10].Value = pEntity.Location;
			parameters[11].Value = pEntity.Description;
			parameters[12].Value = pEntity.Url;
			parameters[13].Value = pEntity.ProfileImageUrl;
			parameters[14].Value = pEntity.ProfileUrl;
			parameters[15].Value = pEntity.Gender;
			parameters[16].Value = pEntity.Weihao;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.VipId;

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
        public void Update(VipExpandSinaWbEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VipExpandSinaWbEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipExpandSinaWbEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipExpandSinaWbEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.VipId, pTran);           
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
            sql.AppendLine("update [VipExpandSinaWb] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VipId=@VipId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@VipId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipExpandSinaWbEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.VipId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.VipId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipExpandSinaWbEntity[] pEntities)
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
            sql.AppendLine("update [VipExpandSinaWb] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VipId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipExpandSinaWbEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipExpandSinaWb] where isdelete=0 ");
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
            List<VipExpandSinaWbEntity> list = new List<VipExpandSinaWbEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipExpandSinaWbEntity m;
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
        public PagedQueryResult<VipExpandSinaWbEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipExpandSinaWb] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipExpandSinaWb] where isdelete=0 ");
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
            PagedQueryResult<VipExpandSinaWbEntity> result = new PagedQueryResult<VipExpandSinaWbEntity>();
            List<VipExpandSinaWbEntity> list = new List<VipExpandSinaWbEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipExpandSinaWbEntity m;
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
        public VipExpandSinaWbEntity[] QueryByEntity(VipExpandSinaWbEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipExpandSinaWbEntity> PagedQueryByEntity(VipExpandSinaWbEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipExpandSinaWbEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.AccessToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AccessToken", Value = pQueryEntity.AccessToken });
            if (pQueryEntity.UID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UID", Value = pQueryEntity.UID });
            if (pQueryEntity.Appkey!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Appkey", Value = pQueryEntity.Appkey });
            if (pQueryEntity.Scope!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Scope", Value = pQueryEntity.Scope });
            if (pQueryEntity.CreateAt!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateAt", Value = pQueryEntity.CreateAt });
            if (pQueryEntity.ExpireIn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExpireIn", Value = pQueryEntity.ExpireIn });
            if (pQueryEntity.ScreenName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ScreenName", Value = pQueryEntity.ScreenName });
            if (pQueryEntity.LabelName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LabelName", Value = pQueryEntity.LabelName });
            if (pQueryEntity.Province!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Province", Value = pQueryEntity.Province });
            if (pQueryEntity.City!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "City", Value = pQueryEntity.City });
            if (pQueryEntity.Location!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Location", Value = pQueryEntity.Location });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.Url!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Url", Value = pQueryEntity.Url });
            if (pQueryEntity.ProfileImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfileImageUrl", Value = pQueryEntity.ProfileImageUrl });
            if (pQueryEntity.ProfileUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfileUrl", Value = pQueryEntity.ProfileUrl });
            if (pQueryEntity.Gender!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Gender", Value = pQueryEntity.Gender });
            if (pQueryEntity.Weihao!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Weihao", Value = pQueryEntity.Weihao });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
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
        protected void Load(SqlDataReader pReader, out VipExpandSinaWbEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipExpandSinaWbEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["AccessToken"] != DBNull.Value)
			{
				pInstance.AccessToken =  Convert.ToString(pReader["AccessToken"]);
			}
			if (pReader["UID"] != DBNull.Value)
			{
				pInstance.UID =  Convert.ToString(pReader["UID"]);
			}
			if (pReader["Appkey"] != DBNull.Value)
			{
				pInstance.Appkey =  Convert.ToString(pReader["Appkey"]);
			}
			if (pReader["Scope"] != DBNull.Value)
			{
				pInstance.Scope =  Convert.ToString(pReader["Scope"]);
			}
			if (pReader["CreateAt"] != DBNull.Value)
			{
				pInstance.CreateAt =  Convert.ToString(pReader["CreateAt"]);
			}
			if (pReader["ExpireIn"] != DBNull.Value)
			{
				pInstance.ExpireIn =  Convert.ToString(pReader["ExpireIn"]);
			}
			if (pReader["ScreenName"] != DBNull.Value)
			{
				pInstance.ScreenName =  Convert.ToString(pReader["ScreenName"]);
			}
			if (pReader["LabelName"] != DBNull.Value)
			{
				pInstance.LabelName =  Convert.ToString(pReader["LabelName"]);
			}
			if (pReader["Province"] != DBNull.Value)
			{
				pInstance.Province =  Convert.ToString(pReader["Province"]);
			}
			if (pReader["City"] != DBNull.Value)
			{
				pInstance.City =  Convert.ToString(pReader["City"]);
			}
			if (pReader["Location"] != DBNull.Value)
			{
				pInstance.Location =  Convert.ToString(pReader["Location"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["Url"] != DBNull.Value)
			{
				pInstance.Url =  Convert.ToString(pReader["Url"]);
			}
			if (pReader["ProfileImageUrl"] != DBNull.Value)
			{
				pInstance.ProfileImageUrl =  Convert.ToString(pReader["ProfileImageUrl"]);
			}
			if (pReader["ProfileUrl"] != DBNull.Value)
			{
				pInstance.ProfileUrl =  Convert.ToString(pReader["ProfileUrl"]);
			}
			if (pReader["Gender"] != DBNull.Value)
			{
				pInstance.Gender =  Convert.ToString(pReader["Gender"]);
			}
			if (pReader["Weihao"] != DBNull.Value)
			{
				pInstance.Weihao =  Convert.ToString(pReader["Weihao"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
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

        }
        #endregion
    }
}
