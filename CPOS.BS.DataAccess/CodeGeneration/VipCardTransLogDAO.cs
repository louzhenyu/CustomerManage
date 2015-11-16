/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/22 19:32:10
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
    /// 表VipCardTransLog的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardTransLogDAO : Base.BaseCPOSDAO, ICRUDable<VipCardTransLogEntity>, IQueryable<VipCardTransLogEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipCardTransLogDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(VipCardTransLogEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(VipCardTransLogEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardTransLog](");
            strSql.Append("[VipCode],[VipName],[VipCardCode],[VipCardTypeCode],[UnitCode],[UnitName],[OrderNo],[BillNo],[TransAction],[TableNum],[Person],[LastValue],[NewValue],[Reason],[TransType],[TransActionType],[TransTerminalType],[TransTerminalCode],[TransAddress],[TransContent],[TransTime],[TransAmount],[TransBalance],[RequestJSON],[CheckCode],[Operator],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[OldVipCardCode])");
            strSql.Append(" values (");
            strSql.Append("@VipCode,@VipName,@VipCardCode,@VipCardTypeCode,@UnitCode,@UnitName,@OrderNo,@BillNo,@TransAction,@TableNum,@Person,@LastValue,@NewValue,@Reason,@TransType,@TransActionType,@TransTerminalType,@TransTerminalCode,@TransAddress,@TransContent,@TransTime,@TransAmount,@TransBalance,@RequestJSON,@CheckCode,@Operator,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@OldVipCardCode)");            
			strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCode",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@TransAction",SqlDbType.NVarChar),
					new SqlParameter("@TableNum",SqlDbType.NVarChar),
					new SqlParameter("@Person",SqlDbType.Int),
					new SqlParameter("@LastValue",SqlDbType.Int),
					new SqlParameter("@NewValue",SqlDbType.Int),
					new SqlParameter("@Reason",SqlDbType.VarChar),
					new SqlParameter("@TransType",SqlDbType.Char),
					new SqlParameter("@TransActionType",SqlDbType.VarChar),
					new SqlParameter("@TransTerminalType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalCode",SqlDbType.NVarChar),
					new SqlParameter("@TransAddress",SqlDbType.NVarChar),
					new SqlParameter("@TransContent",SqlDbType.Char),
					new SqlParameter("@TransTime",SqlDbType.DateTime),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@TransBalance",SqlDbType.Decimal),
					new SqlParameter("@RequestJSON",SqlDbType.NVarChar),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@Operator",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@OldVipCardCode",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.VipCode;
			parameters[1].Value = pEntity.VipName;
			parameters[2].Value = pEntity.VipCardCode;
			parameters[3].Value = pEntity.VipCardTypeCode;
			parameters[4].Value = pEntity.UnitCode;
			parameters[5].Value = pEntity.UnitName;
			parameters[6].Value = pEntity.OrderNo;
			parameters[7].Value = pEntity.BillNo;
			parameters[8].Value = pEntity.TransAction;
			parameters[9].Value = pEntity.TableNum;
			parameters[10].Value = pEntity.Person;
			parameters[11].Value = pEntity.LastValue;
			parameters[12].Value = pEntity.NewValue;
			parameters[13].Value = pEntity.Reason;
			parameters[14].Value = pEntity.TransType;
			parameters[15].Value = pEntity.TransActionType;
			parameters[16].Value = pEntity.TransTerminalType;
			parameters[17].Value = pEntity.TransTerminalCode;
			parameters[18].Value = pEntity.TransAddress;
			parameters[19].Value = pEntity.TransContent;
			parameters[20].Value = pEntity.TransTime;
			parameters[21].Value = pEntity.TransAmount;
			parameters[22].Value = pEntity.TransBalance;
			parameters[23].Value = pEntity.RequestJSON;
			parameters[24].Value = pEntity.CheckCode;
			parameters[25].Value = pEntity.Operator;
			parameters[26].Value = pEntity.CreateTime;
			parameters[27].Value = pEntity.CreateBy;
			parameters[28].Value = pEntity.LastUpdateTime;
			parameters[29].Value = pEntity.LastUpdateBy;
			parameters[30].Value = pEntity.IsDelete;
			parameters[31].Value = pEntity.CustomerID;
			parameters[32].Value = pEntity.OldVipCardCode;

            //执行并将结果回写
            object result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TransID = Convert.ToInt32(result);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipCardTransLogEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLog] where TransID='{0}'  and isdelete=0 ", id.ToString());
            //读取数据
            VipCardTransLogEntity m = null;
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
        public VipCardTransLogEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLog] where 1=1  and isdelete=0");
            //读取数据
            List<VipCardTransLogEntity> list = new List<VipCardTransLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogEntity m;
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
        public void Update(VipCardTransLogEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(VipCardTransLogEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TransID.HasValue)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardTransLog] set ");
                        if (pIsUpdateNullField || pEntity.VipCode!=null)
                strSql.Append( "[VipCode]=@VipCode,");
            if (pIsUpdateNullField || pEntity.VipName!=null)
                strSql.Append( "[VipName]=@VipName,");
            if (pIsUpdateNullField || pEntity.VipCardCode!=null)
                strSql.Append( "[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.VipCardTypeCode!=null)
                strSql.Append( "[VipCardTypeCode]=@VipCardTypeCode,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.BillNo!=null)
                strSql.Append( "[BillNo]=@BillNo,");
            if (pIsUpdateNullField || pEntity.TransAction!=null)
                strSql.Append( "[TransAction]=@TransAction,");
            if (pIsUpdateNullField || pEntity.TableNum!=null)
                strSql.Append( "[TableNum]=@TableNum,");
            if (pIsUpdateNullField || pEntity.Person!=null)
                strSql.Append( "[Person]=@Person,");
            if (pIsUpdateNullField || pEntity.LastValue!=null)
                strSql.Append( "[LastValue]=@LastValue,");
            if (pIsUpdateNullField || pEntity.NewValue!=null)
                strSql.Append( "[NewValue]=@NewValue,");
            if (pIsUpdateNullField || pEntity.Reason!=null)
                strSql.Append( "[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.TransType!=null)
                strSql.Append( "[TransType]=@TransType,");
            if (pIsUpdateNullField || pEntity.TransActionType!=null)
                strSql.Append( "[TransActionType]=@TransActionType,");
            if (pIsUpdateNullField || pEntity.TransTerminalType!=null)
                strSql.Append( "[TransTerminalType]=@TransTerminalType,");
            if (pIsUpdateNullField || pEntity.TransTerminalCode!=null)
                strSql.Append( "[TransTerminalCode]=@TransTerminalCode,");
            if (pIsUpdateNullField || pEntity.TransAddress!=null)
                strSql.Append( "[TransAddress]=@TransAddress,");
            if (pIsUpdateNullField || pEntity.TransContent!=null)
                strSql.Append( "[TransContent]=@TransContent,");
            if (pIsUpdateNullField || pEntity.TransTime!=null)
                strSql.Append( "[TransTime]=@TransTime,");
            if (pIsUpdateNullField || pEntity.TransAmount!=null)
                strSql.Append( "[TransAmount]=@TransAmount,");
            if (pIsUpdateNullField || pEntity.TransBalance!=null)
                strSql.Append( "[TransBalance]=@TransBalance,");
            if (pIsUpdateNullField || pEntity.RequestJSON!=null)
                strSql.Append( "[RequestJSON]=@RequestJSON,");
            if (pIsUpdateNullField || pEntity.CheckCode!=null)
                strSql.Append( "[CheckCode]=@CheckCode,");
            if (pIsUpdateNullField || pEntity.Operator!=null)
                strSql.Append( "[Operator]=@Operator,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.OldVipCardCode!=null)
                strSql.Append( "[OldVipCardCode]=@OldVipCardCode");
            strSql.Append(" where TransID=@TransID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCode",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@TransAction",SqlDbType.NVarChar),
					new SqlParameter("@TableNum",SqlDbType.NVarChar),
					new SqlParameter("@Person",SqlDbType.Int),
					new SqlParameter("@LastValue",SqlDbType.Int),
					new SqlParameter("@NewValue",SqlDbType.Int),
					new SqlParameter("@Reason",SqlDbType.VarChar),
					new SqlParameter("@TransType",SqlDbType.Char),
					new SqlParameter("@TransActionType",SqlDbType.VarChar),
					new SqlParameter("@TransTerminalType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalCode",SqlDbType.NVarChar),
					new SqlParameter("@TransAddress",SqlDbType.NVarChar),
					new SqlParameter("@TransContent",SqlDbType.Char),
					new SqlParameter("@TransTime",SqlDbType.DateTime),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@TransBalance",SqlDbType.Decimal),
					new SqlParameter("@RequestJSON",SqlDbType.NVarChar),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@Operator",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@OldVipCardCode",SqlDbType.Int),
					new SqlParameter("@TransID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.VipCode;
			parameters[1].Value = pEntity.VipName;
			parameters[2].Value = pEntity.VipCardCode;
			parameters[3].Value = pEntity.VipCardTypeCode;
			parameters[4].Value = pEntity.UnitCode;
			parameters[5].Value = pEntity.UnitName;
			parameters[6].Value = pEntity.OrderNo;
			parameters[7].Value = pEntity.BillNo;
			parameters[8].Value = pEntity.TransAction;
			parameters[9].Value = pEntity.TableNum;
			parameters[10].Value = pEntity.Person;
			parameters[11].Value = pEntity.LastValue;
			parameters[12].Value = pEntity.NewValue;
			parameters[13].Value = pEntity.Reason;
			parameters[14].Value = pEntity.TransType;
			parameters[15].Value = pEntity.TransActionType;
			parameters[16].Value = pEntity.TransTerminalType;
			parameters[17].Value = pEntity.TransTerminalCode;
			parameters[18].Value = pEntity.TransAddress;
			parameters[19].Value = pEntity.TransContent;
			parameters[20].Value = pEntity.TransTime;
			parameters[21].Value = pEntity.TransAmount;
			parameters[22].Value = pEntity.TransBalance;
			parameters[23].Value = pEntity.RequestJSON;
			parameters[24].Value = pEntity.CheckCode;
			parameters[25].Value = pEntity.Operator;
			parameters[26].Value = pEntity.LastUpdateTime;
			parameters[27].Value = pEntity.LastUpdateBy;
			parameters[28].Value = pEntity.CustomerID;
			parameters[29].Value = pEntity.OldVipCardCode;
			parameters[30].Value = pEntity.TransID;

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
        public void Update(VipCardTransLogEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardTransLogEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(VipCardTransLogEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TransID.HasValue)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.TransID.Value, pTran);           
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
            sql.AppendLine("update [VipCardTransLog] set  isdelete=1 where TransID=@TransID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@TransID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipCardTransLogEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //参数校验
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.TransID.HasValue)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = pEntity.TransID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(VipCardTransLogEntity[] pEntities)
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
            sql.AppendLine("update [VipCardTransLog] set  isdelete=1 where TransID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardTransLogEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLog] where 1=1  and isdelete=0 ");
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
            List<VipCardTransLogEntity> list = new List<VipCardTransLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogEntity m;
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
        public PagedQueryResult<VipCardTransLogEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TransID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardTransLog] where 1=1  and isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardTransLog] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardTransLogEntity> result = new PagedQueryResult<VipCardTransLogEntity>();
            List<VipCardTransLogEntity> list = new List<VipCardTransLogEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogEntity m;
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
        public VipCardTransLogEntity[] QueryByEntity(VipCardTransLogEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardTransLogEntity> PagedQueryByEntity(VipCardTransLogEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardTransLogEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TransID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransID", Value = pQueryEntity.TransID });
            if (pQueryEntity.VipCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCode", Value = pQueryEntity.VipCode });
            if (pQueryEntity.VipName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipName", Value = pQueryEntity.VipName });
            if (pQueryEntity.VipCardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.VipCardTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeCode", Value = pQueryEntity.VipCardTypeCode });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.BillNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BillNo", Value = pQueryEntity.BillNo });
            if (pQueryEntity.TransAction!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAction", Value = pQueryEntity.TransAction });
            if (pQueryEntity.TableNum!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TableNum", Value = pQueryEntity.TableNum });
            if (pQueryEntity.Person!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Person", Value = pQueryEntity.Person });
            if (pQueryEntity.LastValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastValue", Value = pQueryEntity.LastValue });
            if (pQueryEntity.NewValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewValue", Value = pQueryEntity.NewValue });
            if (pQueryEntity.Reason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.TransType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransType", Value = pQueryEntity.TransType });
            if (pQueryEntity.TransActionType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransActionType", Value = pQueryEntity.TransActionType });
            if (pQueryEntity.TransTerminalType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTerminalType", Value = pQueryEntity.TransTerminalType });
            if (pQueryEntity.TransTerminalCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTerminalCode", Value = pQueryEntity.TransTerminalCode });
            if (pQueryEntity.TransAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAddress", Value = pQueryEntity.TransAddress });
            if (pQueryEntity.TransContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransContent", Value = pQueryEntity.TransContent });
            if (pQueryEntity.TransTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTime", Value = pQueryEntity.TransTime });
            if (pQueryEntity.TransAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAmount", Value = pQueryEntity.TransAmount });
            if (pQueryEntity.TransBalance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransBalance", Value = pQueryEntity.TransBalance });
            if (pQueryEntity.RequestJSON!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestJSON", Value = pQueryEntity.RequestJSON });
            if (pQueryEntity.CheckCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckCode", Value = pQueryEntity.CheckCode });
            if (pQueryEntity.Operator!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Operator", Value = pQueryEntity.Operator });
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
            if (pQueryEntity.OldVipCardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OldVipCardCode", Value = pQueryEntity.OldVipCardCode });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(IDataReader pReader, out VipCardTransLogEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new VipCardTransLogEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TransID"] != DBNull.Value)
			{
				pInstance.TransID =   Convert.ToInt32(pReader["TransID"]);
			}
			if (pReader["VipCode"] != DBNull.Value)
			{
				pInstance.VipCode =  Convert.ToString(pReader["VipCode"]);
			}
			if (pReader["VipName"] != DBNull.Value)
			{
				pInstance.VipName =  Convert.ToString(pReader["VipName"]);
			}
			if (pReader["VipCardCode"] != DBNull.Value)
			{
				pInstance.VipCardCode =  Convert.ToString(pReader["VipCardCode"]);
			}
			if (pReader["VipCardTypeCode"] != DBNull.Value)
			{
				pInstance.VipCardTypeCode =  Convert.ToString(pReader["VipCardTypeCode"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["BillNo"] != DBNull.Value)
			{
				pInstance.BillNo =  Convert.ToString(pReader["BillNo"]);
			}
			if (pReader["TransAction"] != DBNull.Value)
			{
				pInstance.TransAction =  Convert.ToString(pReader["TransAction"]);
			}
			if (pReader["TableNum"] != DBNull.Value)
			{
				pInstance.TableNum =  Convert.ToString(pReader["TableNum"]);
			}
			if (pReader["Person"] != DBNull.Value)
			{
				pInstance.Person =   Convert.ToInt32(pReader["Person"]);
			}
			if (pReader["LastValue"] != DBNull.Value)
			{
				pInstance.LastValue =   Convert.ToInt32(pReader["LastValue"]);
			}
			if (pReader["NewValue"] != DBNull.Value)
			{
				pInstance.NewValue =   Convert.ToInt32(pReader["NewValue"]);
			}
			if (pReader["Reason"] != DBNull.Value)
			{
				pInstance.Reason =  Convert.ToString(pReader["Reason"]);
			}
			if (pReader["TransType"] != DBNull.Value)
			{
                pInstance.TransType = Convert.ToString(pReader["TransType"]);
			}
			if (pReader["TransActionType"] != DBNull.Value)
			{
				pInstance.TransActionType =  Convert.ToString(pReader["TransActionType"]);
			}
			if (pReader["TransTerminalType"] != DBNull.Value)
			{
				pInstance.TransTerminalType =  Convert.ToString(pReader["TransTerminalType"]);
			}
			if (pReader["TransTerminalCode"] != DBNull.Value)
			{
				pInstance.TransTerminalCode =  Convert.ToString(pReader["TransTerminalCode"]);
			}
			if (pReader["TransAddress"] != DBNull.Value)
			{
				pInstance.TransAddress =  Convert.ToString(pReader["TransAddress"]);
			}
			if (pReader["TransContent"] != DBNull.Value)
			{
				pInstance.TransContent = Convert.ToString(pReader["TransContent"]);
			}
			if (pReader["TransTime"] != DBNull.Value)
			{
				pInstance.TransTime =  Convert.ToDateTime(pReader["TransTime"]);
			}
			if (pReader["TransAmount"] != DBNull.Value)
			{
				pInstance.TransAmount =  Convert.ToDecimal(pReader["TransAmount"]);
			}
			if (pReader["TransBalance"] != DBNull.Value)
			{
				pInstance.TransBalance =  Convert.ToDecimal(pReader["TransBalance"]);
			}
			if (pReader["RequestJSON"] != DBNull.Value)
			{
				pInstance.RequestJSON =  Convert.ToString(pReader["RequestJSON"]);
			}
			if (pReader["CheckCode"] != DBNull.Value)
			{
				pInstance.CheckCode =  Convert.ToString(pReader["CheckCode"]);
			}
			if (pReader["Operator"] != DBNull.Value)
			{
				pInstance.Operator =  Convert.ToString(pReader["Operator"]);
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
			if (pReader["OldVipCardCode"] != DBNull.Value)
			{
				pInstance.OldVipCardCode =   Convert.ToInt32(pReader["OldVipCardCode"]);
			}

        }
        #endregion
    }
}
