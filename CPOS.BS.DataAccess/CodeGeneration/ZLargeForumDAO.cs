/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/16 11:05:55
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
    /// 表ZLargeForum的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ZLargeForumDAO : Base.BaseCPOSDAO, ICRUDable<ZLargeForumEntity>, IQueryable<ZLargeForumEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ZLargeForumDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(ZLargeForumEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(ZLargeForumEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ZLargeForum](");
            strSql.Append("[Title],[ForumTypeId],[Desc],[Organizer],[Schedule],[Speakers],[Food],[BeginTime],[EndTime],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[Email],[EmailTitle],[Sponsor],[Roundtable],[PreviousForum],[ContactUs],[City],[IsDesc],[IsOrganizer],[IsSchedule],[IsSpeakers],[IsRoundtable],[IsSponsor],[IsFood],[IsPreviousForum],[IsContactUs],[IsSignUp],[Register],[IsRegister],[ForumId])");
            strSql.Append(" values (");
            strSql.Append("@Title,@ForumTypeId,@Desc,@Organizer,@Schedule,@Speakers,@Food,@BeginTime,@EndTime,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@Email,@EmailTitle,@Sponsor,@Roundtable,@PreviousForum,@ContactUs,@City,@IsDesc,@IsOrganizer,@IsSchedule,@IsSpeakers,@IsRoundtable,@IsSponsor,@IsFood,@IsPreviousForum,@IsContactUs,@IsSignUp,@Register,@IsRegister,@ForumId)");            

			string pkString = pEntity.ForumId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ForumTypeId",SqlDbType.Int),
					new SqlParameter("@Desc",SqlDbType.NVarChar),
					new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@Schedule",SqlDbType.NVarChar),
					new SqlParameter("@Speakers",SqlDbType.NVarChar),
					new SqlParameter("@Food",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@Sponsor",SqlDbType.NVarChar),
					new SqlParameter("@Roundtable",SqlDbType.NVarChar),
					new SqlParameter("@PreviousForum",SqlDbType.NVarChar),
					new SqlParameter("@ContactUs",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@IsDesc",SqlDbType.Int),
					new SqlParameter("@IsOrganizer",SqlDbType.Int),
					new SqlParameter("@IsSchedule",SqlDbType.Int),
					new SqlParameter("@IsSpeakers",SqlDbType.Int),
					new SqlParameter("@IsRoundtable",SqlDbType.Int),
					new SqlParameter("@IsSponsor",SqlDbType.Int),
					new SqlParameter("@IsFood",SqlDbType.Int),
					new SqlParameter("@IsPreviousForum",SqlDbType.Int),
					new SqlParameter("@IsContactUs",SqlDbType.Int),
					new SqlParameter("@IsSignUp",SqlDbType.Int),
					new SqlParameter("@Register",SqlDbType.NVarChar),
					new SqlParameter("@IsRegister",SqlDbType.Int),
					new SqlParameter("@ForumId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.ForumTypeId;
			parameters[2].Value = pEntity.Desc;
			parameters[3].Value = pEntity.Organizer;
			parameters[4].Value = pEntity.Schedule;
			parameters[5].Value = pEntity.Speakers;
			parameters[6].Value = pEntity.Food;
			parameters[7].Value = pEntity.BeginTime;
			parameters[8].Value = pEntity.EndTime;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.Email;
			parameters[15].Value = pEntity.EmailTitle;
			parameters[16].Value = pEntity.Sponsor;
			parameters[17].Value = pEntity.Roundtable;
			parameters[18].Value = pEntity.PreviousForum;
			parameters[19].Value = pEntity.ContactUs;
			parameters[20].Value = pEntity.City;
			parameters[21].Value = pEntity.IsDesc;
			parameters[22].Value = pEntity.IsOrganizer;
			parameters[23].Value = pEntity.IsSchedule;
			parameters[24].Value = pEntity.IsSpeakers;
			parameters[25].Value = pEntity.IsRoundtable;
			parameters[26].Value = pEntity.IsSponsor;
			parameters[27].Value = pEntity.IsFood;
			parameters[28].Value = pEntity.IsPreviousForum;
			parameters[29].Value = pEntity.IsContactUs;
			parameters[30].Value = pEntity.IsSignUp;
			parameters[31].Value = pEntity.Register;
			parameters[32].Value = pEntity.IsRegister;
			parameters[33].Value = pkString;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ForumId = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public ZLargeForumEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZLargeForum] where ForumId='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            ZLargeForumEntity m = null;
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
        public ZLargeForumEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZLargeForum] where isdelete=0");
            //读取数据
            List<ZLargeForumEntity> list = new List<ZLargeForumEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZLargeForumEntity m;
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
        public void Update(ZLargeForumEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ZLargeForumEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ForumId==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ZLargeForum] set ");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.ForumTypeId!=null)
                strSql.Append( "[ForumTypeId]=@ForumTypeId,");
            if (pIsUpdateNullField || pEntity.Desc!=null)
                strSql.Append( "[Desc]=@Desc,");
            if (pIsUpdateNullField || pEntity.Organizer!=null)
                strSql.Append( "[Organizer]=@Organizer,");
            if (pIsUpdateNullField || pEntity.Schedule!=null)
                strSql.Append( "[Schedule]=@Schedule,");
            if (pIsUpdateNullField || pEntity.Speakers!=null)
                strSql.Append( "[Speakers]=@Speakers,");
            if (pIsUpdateNullField || pEntity.Food!=null)
                strSql.Append( "[Food]=@Food,");
            if (pIsUpdateNullField || pEntity.BeginTime!=null)
                strSql.Append( "[BeginTime]=@BeginTime,");
            if (pIsUpdateNullField || pEntity.EndTime!=null)
                strSql.Append( "[EndTime]=@EndTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.EmailTitle!=null)
                strSql.Append( "[EmailTitle]=@EmailTitle,");
            if (pIsUpdateNullField || pEntity.Sponsor!=null)
                strSql.Append( "[Sponsor]=@Sponsor,");
            if (pIsUpdateNullField || pEntity.Roundtable!=null)
                strSql.Append( "[Roundtable]=@Roundtable,");
            if (pIsUpdateNullField || pEntity.PreviousForum!=null)
                strSql.Append( "[PreviousForum]=@PreviousForum,");
            if (pIsUpdateNullField || pEntity.ContactUs!=null)
                strSql.Append( "[ContactUs]=@ContactUs,");
            if (pIsUpdateNullField || pEntity.City!=null)
                strSql.Append( "[City]=@City,");
            if (pIsUpdateNullField || pEntity.IsDesc!=null)
                strSql.Append( "[IsDesc]=@IsDesc,");
            if (pIsUpdateNullField || pEntity.IsOrganizer!=null)
                strSql.Append( "[IsOrganizer]=@IsOrganizer,");
            if (pIsUpdateNullField || pEntity.IsSchedule!=null)
                strSql.Append( "[IsSchedule]=@IsSchedule,");
            if (pIsUpdateNullField || pEntity.IsSpeakers!=null)
                strSql.Append( "[IsSpeakers]=@IsSpeakers,");
            if (pIsUpdateNullField || pEntity.IsRoundtable!=null)
                strSql.Append( "[IsRoundtable]=@IsRoundtable,");
            if (pIsUpdateNullField || pEntity.IsSponsor!=null)
                strSql.Append( "[IsSponsor]=@IsSponsor,");
            if (pIsUpdateNullField || pEntity.IsFood!=null)
                strSql.Append( "[IsFood]=@IsFood,");
            if (pIsUpdateNullField || pEntity.IsPreviousForum!=null)
                strSql.Append( "[IsPreviousForum]=@IsPreviousForum,");
            if (pIsUpdateNullField || pEntity.IsContactUs!=null)
                strSql.Append( "[IsContactUs]=@IsContactUs,");
            if (pIsUpdateNullField || pEntity.IsSignUp!=null)
                strSql.Append( "[IsSignUp]=@IsSignUp,");
            if (pIsUpdateNullField || pEntity.Register!=null)
                strSql.Append( "[Register]=@Register,");
            if (pIsUpdateNullField || pEntity.IsRegister!=null)
                strSql.Append( "[IsRegister]=@IsRegister");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ForumId=@ForumId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@ForumTypeId",SqlDbType.Int),
					new SqlParameter("@Desc",SqlDbType.NVarChar),
					new SqlParameter("@Organizer",SqlDbType.NVarChar),
					new SqlParameter("@Schedule",SqlDbType.NVarChar),
					new SqlParameter("@Speakers",SqlDbType.NVarChar),
					new SqlParameter("@Food",SqlDbType.NVarChar),
					new SqlParameter("@BeginTime",SqlDbType.NVarChar),
					new SqlParameter("@EndTime",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@EmailTitle",SqlDbType.NVarChar),
					new SqlParameter("@Sponsor",SqlDbType.NVarChar),
					new SqlParameter("@Roundtable",SqlDbType.NVarChar),
					new SqlParameter("@PreviousForum",SqlDbType.NVarChar),
					new SqlParameter("@ContactUs",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@IsDesc",SqlDbType.Int),
					new SqlParameter("@IsOrganizer",SqlDbType.Int),
					new SqlParameter("@IsSchedule",SqlDbType.Int),
					new SqlParameter("@IsSpeakers",SqlDbType.Int),
					new SqlParameter("@IsRoundtable",SqlDbType.Int),
					new SqlParameter("@IsSponsor",SqlDbType.Int),
					new SqlParameter("@IsFood",SqlDbType.Int),
					new SqlParameter("@IsPreviousForum",SqlDbType.Int),
					new SqlParameter("@IsContactUs",SqlDbType.Int),
					new SqlParameter("@IsSignUp",SqlDbType.Int),
					new SqlParameter("@Register",SqlDbType.NVarChar),
					new SqlParameter("@IsRegister",SqlDbType.Int),
					new SqlParameter("@ForumId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Title;
			parameters[1].Value = pEntity.ForumTypeId;
			parameters[2].Value = pEntity.Desc;
			parameters[3].Value = pEntity.Organizer;
			parameters[4].Value = pEntity.Schedule;
			parameters[5].Value = pEntity.Speakers;
			parameters[6].Value = pEntity.Food;
			parameters[7].Value = pEntity.BeginTime;
			parameters[8].Value = pEntity.EndTime;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.Email;
			parameters[12].Value = pEntity.EmailTitle;
			parameters[13].Value = pEntity.Sponsor;
			parameters[14].Value = pEntity.Roundtable;
			parameters[15].Value = pEntity.PreviousForum;
			parameters[16].Value = pEntity.ContactUs;
			parameters[17].Value = pEntity.City;
			parameters[18].Value = pEntity.IsDesc;
			parameters[19].Value = pEntity.IsOrganizer;
			parameters[20].Value = pEntity.IsSchedule;
			parameters[21].Value = pEntity.IsSpeakers;
			parameters[22].Value = pEntity.IsRoundtable;
			parameters[23].Value = pEntity.IsSponsor;
			parameters[24].Value = pEntity.IsFood;
			parameters[25].Value = pEntity.IsPreviousForum;
			parameters[26].Value = pEntity.IsContactUs;
			parameters[27].Value = pEntity.IsSignUp;
			parameters[28].Value = pEntity.Register;
			parameters[29].Value = pEntity.IsRegister;
			parameters[30].Value = pEntity.ForumId;

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
        public void Update(ZLargeForumEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ZLargeForumEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ZLargeForumEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(ZLargeForumEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ForumId==null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.ForumId, pTran);           
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
            sql.AppendLine("update [ZLargeForum] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ForumId=@ForumId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ForumId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ZLargeForumEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ForumId==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.ForumId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(ZLargeForumEntity[] pEntities)
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
            sql.AppendLine("update [ZLargeForum] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ForumId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ZLargeForumEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ZLargeForum] where isdelete=0 ");
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
            List<ZLargeForumEntity> list = new List<ZLargeForumEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ZLargeForumEntity m;
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
        public PagedQueryResult<ZLargeForumEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ForumId] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [ZLargeForum] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [ZLargeForum] where isdelete=0 ");
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
            PagedQueryResult<ZLargeForumEntity> result = new PagedQueryResult<ZLargeForumEntity>();
            List<ZLargeForumEntity> list = new List<ZLargeForumEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ZLargeForumEntity m;
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
        public ZLargeForumEntity[] QueryByEntity(ZLargeForumEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ZLargeForumEntity> PagedQueryByEntity(ZLargeForumEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ZLargeForumEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ForumId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ForumId", Value = pQueryEntity.ForumId });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.ForumTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ForumTypeId", Value = pQueryEntity.ForumTypeId });
            if (pQueryEntity.Desc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Desc", Value = pQueryEntity.Desc });
            if (pQueryEntity.Organizer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Organizer", Value = pQueryEntity.Organizer });
            if (pQueryEntity.Schedule!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Schedule", Value = pQueryEntity.Schedule });
            if (pQueryEntity.Speakers!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Speakers", Value = pQueryEntity.Speakers });
            if (pQueryEntity.Food!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Food", Value = pQueryEntity.Food });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
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
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.EmailTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EmailTitle", Value = pQueryEntity.EmailTitle });
            if (pQueryEntity.Sponsor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sponsor", Value = pQueryEntity.Sponsor });
            if (pQueryEntity.Roundtable!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Roundtable", Value = pQueryEntity.Roundtable });
            if (pQueryEntity.PreviousForum!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreviousForum", Value = pQueryEntity.PreviousForum });
            if (pQueryEntity.ContactUs!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContactUs", Value = pQueryEntity.ContactUs });
            if (pQueryEntity.City!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "City", Value = pQueryEntity.City });
            if (pQueryEntity.IsDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDesc", Value = pQueryEntity.IsDesc });
            if (pQueryEntity.IsOrganizer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsOrganizer", Value = pQueryEntity.IsOrganizer });
            if (pQueryEntity.IsSchedule!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSchedule", Value = pQueryEntity.IsSchedule });
            if (pQueryEntity.IsSpeakers!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSpeakers", Value = pQueryEntity.IsSpeakers });
            if (pQueryEntity.IsRoundtable!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRoundtable", Value = pQueryEntity.IsRoundtable });
            if (pQueryEntity.IsSponsor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSponsor", Value = pQueryEntity.IsSponsor });
            if (pQueryEntity.IsFood!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsFood", Value = pQueryEntity.IsFood });
            if (pQueryEntity.IsPreviousForum!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPreviousForum", Value = pQueryEntity.IsPreviousForum });
            if (pQueryEntity.IsContactUs!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsContactUs", Value = pQueryEntity.IsContactUs });
            if (pQueryEntity.IsSignUp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsSignUp", Value = pQueryEntity.IsSignUp });
            if (pQueryEntity.Register!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Register", Value = pQueryEntity.Register });
            if (pQueryEntity.IsRegister!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsRegister", Value = pQueryEntity.IsRegister });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out ZLargeForumEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new ZLargeForumEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ForumId"] != DBNull.Value)
			{
				pInstance.ForumId =  Convert.ToString(pReader["ForumId"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["ForumTypeId"] != DBNull.Value)
			{
				pInstance.ForumTypeId =   Convert.ToInt32(pReader["ForumTypeId"]);
			}
			if (pReader["Desc"] != DBNull.Value)
			{
				pInstance.Desc =  Convert.ToString(pReader["Desc"]);
			}
			if (pReader["Organizer"] != DBNull.Value)
			{
				pInstance.Organizer =  Convert.ToString(pReader["Organizer"]);
			}
			if (pReader["Schedule"] != DBNull.Value)
			{
				pInstance.Schedule =  Convert.ToString(pReader["Schedule"]);
			}
			if (pReader["Speakers"] != DBNull.Value)
			{
				pInstance.Speakers =  Convert.ToString(pReader["Speakers"]);
			}
			if (pReader["Food"] != DBNull.Value)
			{
				pInstance.Food =  Convert.ToString(pReader["Food"]);
			}
			if (pReader["BeginTime"] != DBNull.Value)
			{
				pInstance.BeginTime =  Convert.ToString(pReader["BeginTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToString(pReader["EndTime"]);
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
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["EmailTitle"] != DBNull.Value)
			{
				pInstance.EmailTitle =  Convert.ToString(pReader["EmailTitle"]);
			}
			if (pReader["Sponsor"] != DBNull.Value)
			{
				pInstance.Sponsor =  Convert.ToString(pReader["Sponsor"]);
			}
			if (pReader["Roundtable"] != DBNull.Value)
			{
				pInstance.Roundtable =  Convert.ToString(pReader["Roundtable"]);
			}
			if (pReader["PreviousForum"] != DBNull.Value)
			{
				pInstance.PreviousForum =  Convert.ToString(pReader["PreviousForum"]);
			}
			if (pReader["ContactUs"] != DBNull.Value)
			{
				pInstance.ContactUs =  Convert.ToString(pReader["ContactUs"]);
			}
			if (pReader["City"] != DBNull.Value)
			{
				pInstance.City =  Convert.ToString(pReader["City"]);
			}
			if (pReader["IsDesc"] != DBNull.Value)
			{
				pInstance.IsDesc =   Convert.ToInt32(pReader["IsDesc"]);
			}
			if (pReader["IsOrganizer"] != DBNull.Value)
			{
				pInstance.IsOrganizer =   Convert.ToInt32(pReader["IsOrganizer"]);
			}
			if (pReader["IsSchedule"] != DBNull.Value)
			{
				pInstance.IsSchedule =   Convert.ToInt32(pReader["IsSchedule"]);
			}
			if (pReader["IsSpeakers"] != DBNull.Value)
			{
				pInstance.IsSpeakers =   Convert.ToInt32(pReader["IsSpeakers"]);
			}
			if (pReader["IsRoundtable"] != DBNull.Value)
			{
				pInstance.IsRoundtable =   Convert.ToInt32(pReader["IsRoundtable"]);
			}
			if (pReader["IsSponsor"] != DBNull.Value)
			{
				pInstance.IsSponsor =   Convert.ToInt32(pReader["IsSponsor"]);
			}
			if (pReader["IsFood"] != DBNull.Value)
			{
				pInstance.IsFood =   Convert.ToInt32(pReader["IsFood"]);
			}
			if (pReader["IsPreviousForum"] != DBNull.Value)
			{
				pInstance.IsPreviousForum =   Convert.ToInt32(pReader["IsPreviousForum"]);
			}
			if (pReader["IsContactUs"] != DBNull.Value)
			{
				pInstance.IsContactUs =   Convert.ToInt32(pReader["IsContactUs"]);
			}
			if (pReader["IsSignUp"] != DBNull.Value)
			{
				pInstance.IsSignUp =   Convert.ToInt32(pReader["IsSignUp"]);
			}
			if (pReader["Register"] != DBNull.Value)
			{
				pInstance.Register =  Convert.ToString(pReader["Register"]);
			}
			if (pReader["IsRegister"] != DBNull.Value)
			{
				pInstance.IsRegister =   Convert.ToInt32(pReader["IsRegister"]);
			}

        }
        #endregion
    }
}
