/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/6 14:23:25
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
    /// 表VwUnitProperty的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VwUnitPropertyDAO : Base.BaseCPOSDAO, ICRUDable<VwUnitPropertyEntity>, IQueryable<VwUnitPropertyEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VwUnitPropertyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VwUnitPropertyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VwUnitPropertyEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VwUnitProperty](");
            strSql.Append("[unitName],[IsWeixinPush],[IsSMSPush],[IsAPPPush],[IsAPP],[FirstPageImage],[LoginImage],[ProductsBackgroundImage],[FansAwards],[TransactionAwards],[WeiXinUnitCode],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerId],[StockCount],[Distance],[ADDRESS],[Tel],[IsCallSMSPush],[IsCallEmailPush],[UnitId])");
            strSql.Append(" values (");
            strSql.Append("@UnitName,@IsWeixinPush,@IsSMSPush,@IsAPPPush,@IsAPP,@FirstPageImage,@LoginImage,@ProductsBackgroundImage,@FansAwards,@TransactionAwards,@WeiXinUnitCode,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerId,@StockCount,@Distance,@ADDRESS,@Tel,@IsCallSMSPush,@IsCallEmailPush,@UnitId)");            

			string pkString = pEntity.UnitId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@IsWeixinPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsAPPPush",SqlDbType.Int),
					new SqlParameter("@IsAPP",SqlDbType.Int),
					new SqlParameter("@FirstPageImage",SqlDbType.NVarChar),
					new SqlParameter("@LoginImage",SqlDbType.NVarChar),
					new SqlParameter("@ProductsBackgroundImage",SqlDbType.NVarChar),
					new SqlParameter("@FansAwards",SqlDbType.NVarChar),
					new SqlParameter("@TransactionAwards",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@StockCount",SqlDbType.Int),
					new SqlParameter("@Distance",SqlDbType.Int),
					new SqlParameter("@ADDRESS",SqlDbType.NVarChar),
					new SqlParameter("@Tel",SqlDbType.NVarChar),
					new SqlParameter("@IsCallSMSPush",SqlDbType.Int),
					new SqlParameter("@IsCallEmailPush",SqlDbType.Int),
					new SqlParameter("@UnitId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UnitName;
			parameters[1].Value = pEntity.IsWeixinPush;
			parameters[2].Value = pEntity.IsSMSPush;
			parameters[3].Value = pEntity.IsAPPPush;
			parameters[4].Value = pEntity.IsAPP;
			parameters[5].Value = pEntity.FirstPageImage;
			parameters[6].Value = pEntity.LoginImage;
			parameters[7].Value = pEntity.ProductsBackgroundImage;
			parameters[8].Value = pEntity.FansAwards;
			parameters[9].Value = pEntity.TransactionAwards;
			parameters[10].Value = pEntity.WeiXinUnitCode;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.StockCount;
			parameters[18].Value = pEntity.Distance;
			parameters[19].Value = pEntity.ADDRESS;
			parameters[20].Value = pEntity.Tel;
			parameters[21].Value = pEntity.IsCallSMSPush;
			parameters[22].Value = pEntity.IsCallEmailPush;
			parameters[23].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.UnitId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VwUnitPropertyEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VwUnitProperty] where UnitId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            VwUnitPropertyEntity m = null;
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
        public VwUnitPropertyEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VwUnitProperty] where isdelete=0");
            //读取数据
            List<VwUnitPropertyEntity> list = new List<VwUnitPropertyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwUnitPropertyEntity m;
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
        public void Update(VwUnitPropertyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VwUnitPropertyEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VwUnitProperty] set ");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[unitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.IsWeixinPush!=null)
                strSql.Append( "[IsWeixinPush]=@IsWeixinPush,");
            if (pIsUpdateNullField || pEntity.IsSMSPush!=null)
                strSql.Append( "[IsSMSPush]=@IsSMSPush,");
            if (pIsUpdateNullField || pEntity.IsAPPPush!=null)
                strSql.Append( "[IsAPPPush]=@IsAPPPush,");
            if (pIsUpdateNullField || pEntity.IsAPP!=null)
                strSql.Append( "[IsAPP]=@IsAPP,");
            if (pIsUpdateNullField || pEntity.FirstPageImage!=null)
                strSql.Append( "[FirstPageImage]=@FirstPageImage,");
            if (pIsUpdateNullField || pEntity.LoginImage!=null)
                strSql.Append( "[LoginImage]=@LoginImage,");
            if (pIsUpdateNullField || pEntity.ProductsBackgroundImage!=null)
                strSql.Append( "[ProductsBackgroundImage]=@ProductsBackgroundImage,");
            if (pIsUpdateNullField || pEntity.FansAwards!=null)
                strSql.Append( "[FansAwards]=@FansAwards,");
            if (pIsUpdateNullField || pEntity.TransactionAwards!=null)
                strSql.Append( "[TransactionAwards]=@TransactionAwards,");
            if (pIsUpdateNullField || pEntity.WeiXinUnitCode!=null)
                strSql.Append( "[WeiXinUnitCode]=@WeiXinUnitCode,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.StockCount!=null)
                strSql.Append( "[StockCount]=@StockCount,");
            if (pIsUpdateNullField || pEntity.Distance!=null)
                strSql.Append( "[Distance]=@Distance,");
            if (pIsUpdateNullField || pEntity.ADDRESS!=null)
                strSql.Append( "[ADDRESS]=@ADDRESS,");
            if (pIsUpdateNullField || pEntity.Tel!=null)
                strSql.Append( "[Tel]=@Tel,");
            if (pIsUpdateNullField || pEntity.IsCallSMSPush!=null)
                strSql.Append( "[IsCallSMSPush]=@IsCallSMSPush,");
            if (pIsUpdateNullField || pEntity.IsCallEmailPush!=null)
                strSql.Append( "[IsCallEmailPush]=@IsCallEmailPush");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where UnitId=@UnitId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@IsWeixinPush",SqlDbType.Int),
					new SqlParameter("@IsSMSPush",SqlDbType.Int),
					new SqlParameter("@IsAPPPush",SqlDbType.Int),
					new SqlParameter("@IsAPP",SqlDbType.Int),
					new SqlParameter("@FirstPageImage",SqlDbType.NVarChar),
					new SqlParameter("@LoginImage",SqlDbType.NVarChar),
					new SqlParameter("@ProductsBackgroundImage",SqlDbType.NVarChar),
					new SqlParameter("@FansAwards",SqlDbType.NVarChar),
					new SqlParameter("@TransactionAwards",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinUnitCode",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@StockCount",SqlDbType.Int),
					new SqlParameter("@Distance",SqlDbType.Int),
					new SqlParameter("@ADDRESS",SqlDbType.NVarChar),
					new SqlParameter("@Tel",SqlDbType.NVarChar),
					new SqlParameter("@IsCallSMSPush",SqlDbType.Int),
					new SqlParameter("@IsCallEmailPush",SqlDbType.Int),
					new SqlParameter("@UnitId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UnitName;
			parameters[1].Value = pEntity.IsWeixinPush;
			parameters[2].Value = pEntity.IsSMSPush;
			parameters[3].Value = pEntity.IsAPPPush;
			parameters[4].Value = pEntity.IsAPP;
			parameters[5].Value = pEntity.FirstPageImage;
			parameters[6].Value = pEntity.LoginImage;
			parameters[7].Value = pEntity.ProductsBackgroundImage;
			parameters[8].Value = pEntity.FansAwards;
			parameters[9].Value = pEntity.TransactionAwards;
			parameters[10].Value = pEntity.WeiXinUnitCode;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.StockCount;
			parameters[15].Value = pEntity.Distance;
			parameters[16].Value = pEntity.ADDRESS;
			parameters[17].Value = pEntity.Tel;
			parameters[18].Value = pEntity.IsCallSMSPush;
			parameters[19].Value = pEntity.IsCallEmailPush;
			parameters[20].Value = pEntity.UnitId;

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
        public void Update(VwUnitPropertyEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VwUnitPropertyEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VwUnitPropertyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VwUnitPropertyEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.UnitId, pTran);           
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
            sql.AppendLine("update [VwUnitProperty] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where UnitId=@UnitId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@UnitId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VwUnitPropertyEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.UnitId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.UnitId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VwUnitPropertyEntity[] pEntities)
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
            sql.AppendLine("update [VwUnitProperty] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where UnitId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VwUnitPropertyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VwUnitProperty] where isdelete=0 ");
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
            List<VwUnitPropertyEntity> list = new List<VwUnitPropertyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwUnitPropertyEntity m;
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
        public PagedQueryResult<VwUnitPropertyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [UnitId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwUnitProperty] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VwUnitProperty] where isdelete=0 ");
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
            PagedQueryResult<VwUnitPropertyEntity> result = new PagedQueryResult<VwUnitPropertyEntity>();
            List<VwUnitPropertyEntity> list = new List<VwUnitPropertyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VwUnitPropertyEntity m;
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
        public VwUnitPropertyEntity[] QueryByEntity(VwUnitPropertyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VwUnitPropertyEntity> PagedQueryByEntity(VwUnitPropertyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VwUnitPropertyEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.IsWeixinPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsWeixinPush", Value = pQueryEntity.IsWeixinPush });
            if (pQueryEntity.IsSMSPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSMSPush", Value = pQueryEntity.IsSMSPush });
            if (pQueryEntity.IsAPPPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAPPPush", Value = pQueryEntity.IsAPPPush });
            if (pQueryEntity.IsAPP!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAPP", Value = pQueryEntity.IsAPP });
            if (pQueryEntity.FirstPageImage!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstPageImage", Value = pQueryEntity.FirstPageImage });
            if (pQueryEntity.LoginImage!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LoginImage", Value = pQueryEntity.LoginImage });
            if (pQueryEntity.ProductsBackgroundImage!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProductsBackgroundImage", Value = pQueryEntity.ProductsBackgroundImage });
            if (pQueryEntity.FansAwards!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FansAwards", Value = pQueryEntity.FansAwards });
            if (pQueryEntity.TransactionAwards!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransactionAwards", Value = pQueryEntity.TransactionAwards });
            if (pQueryEntity.WeiXinUnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinUnitCode", Value = pQueryEntity.WeiXinUnitCode });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.StockCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StockCount", Value = pQueryEntity.StockCount });
            if (pQueryEntity.Distance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Distance", Value = pQueryEntity.Distance });
            if (pQueryEntity.ADDRESS!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ADDRESS", Value = pQueryEntity.ADDRESS });
            if (pQueryEntity.Tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Tel", Value = pQueryEntity.Tel });
            if (pQueryEntity.IsCallSMSPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCallSMSPush", Value = pQueryEntity.IsCallSMSPush });
            if (pQueryEntity.IsCallEmailPush!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCallEmailPush", Value = pQueryEntity.IsCallEmailPush });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out VwUnitPropertyEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VwUnitPropertyEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["unitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["unitName"]);
			}
			if (pReader["IsWeixinPush"] != DBNull.Value)
			{
				pInstance.IsWeixinPush =   Convert.ToInt32(pReader["IsWeixinPush"]);
			}
			if (pReader["IsSMSPush"] != DBNull.Value)
			{
				pInstance.IsSMSPush =   Convert.ToInt32(pReader["IsSMSPush"]);
			}
			if (pReader["IsAPPPush"] != DBNull.Value)
			{
				pInstance.IsAPPPush =   Convert.ToInt32(pReader["IsAPPPush"]);
			}
			if (pReader["IsAPP"] != DBNull.Value)
			{
				pInstance.IsAPP =   Convert.ToInt32(pReader["IsAPP"]);
			}
			if (pReader["FirstPageImage"] != DBNull.Value)
			{
				pInstance.FirstPageImage =  Convert.ToString(pReader["FirstPageImage"]);
			}
			if (pReader["LoginImage"] != DBNull.Value)
			{
				pInstance.LoginImage =  Convert.ToString(pReader["LoginImage"]);
			}
			if (pReader["ProductsBackgroundImage"] != DBNull.Value)
			{
				pInstance.ProductsBackgroundImage =  Convert.ToString(pReader["ProductsBackgroundImage"]);
			}
			if (pReader["FansAwards"] != DBNull.Value)
			{
				pInstance.FansAwards =  Convert.ToString(pReader["FansAwards"]);
			}
			if (pReader["TransactionAwards"] != DBNull.Value)
			{
				pInstance.TransactionAwards =  Convert.ToString(pReader["TransactionAwards"]);
			}
			if (pReader["WeiXinUnitCode"] != DBNull.Value)
			{
				pInstance.WeiXinUnitCode =  Convert.ToString(pReader["WeiXinUnitCode"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["StockCount"] != DBNull.Value)
			{
				pInstance.StockCount =   Convert.ToInt32(pReader["StockCount"]);
			}
			if (pReader["Distance"] != DBNull.Value)
			{
				pInstance.Distance =   Convert.ToInt32(pReader["Distance"]);
			}
			if (pReader["ADDRESS"] != DBNull.Value)
			{
				pInstance.ADDRESS =  Convert.ToString(pReader["ADDRESS"]);
			}
			if (pReader["Tel"] != DBNull.Value)
			{
				pInstance.Tel =  Convert.ToString(pReader["Tel"]);
			}
			if (pReader["IsCallSMSPush"] != DBNull.Value)
			{
				pInstance.IsCallSMSPush =   Convert.ToInt32(pReader["IsCallSMSPush"]);
			}
			if (pReader["IsCallEmailPush"] != DBNull.Value)
			{
				pInstance.IsCallEmailPush =   Convert.ToInt32(pReader["IsCallEmailPush"]);
			}

        }
        #endregion
    }
}
