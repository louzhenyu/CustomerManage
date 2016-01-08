/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
    /// 表Coupon的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CouponDAO : Base.BaseCPOSDAO, ICRUDable<CouponEntity>, IQueryable<CouponEntity>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CouponDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(CouponEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(CouponEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [Coupon](");
            strSql.Append("[CouponCode],[CouponDesc],[BeginDate],[EndDate],[CouponUrl],[ImageUrl],[Status],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CouponTypeID],[CouponID],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col14],[Col15],[Col16],[Col17],[Col18],[Col19],[Col20],[Col21],[Col22],[Col23],[Col24],[Col25],[Col26],[Col27],[Col28],[Col29],[Col30],[Col31],[Col32],[Col33],[Col34],[Col35],[Col36],[Col37],[Col38],[Col39],[Col40],[Col41],[Col42],[Col43],[Col44],[Col45],[Col46],[Col47],[Col48],[Col49],[Col50])");
            strSql.Append(" values (");
            strSql.Append("@CouponCode,@CouponDesc,@BeginDate,@EndDate,@CouponUrl,@ImageUrl,@Status,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CouponTypeID,@CouponID,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15,@Col16,@Col17,@Col18,@Col19,@Col20,@Col21,@Col22,@Col23,@Col24,@Col25,@Col26,@Col27,@Col28,@Col29,@Col30,@Col31,@Col32,@Col33,@Col34,@Col35,@Col36,@Col37,@Col38,@Col39,@Col40,@Col41,@Col42,@Col43,@Col44,@Col45,@Col46,@Col47,@Col48,@Col49,@Col50)");            

			string pkString = pEntity.CouponID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouponCode",SqlDbType.NVarChar),
					new SqlParameter("@CouponDesc",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@CouponUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CouponTypeID",SqlDbType.NVarChar),
					new SqlParameter("@CouponID",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Col11",SqlDbType.NVarChar),
					new SqlParameter("@Col12",SqlDbType.NVarChar),
					new SqlParameter("@Col13",SqlDbType.NVarChar),
					new SqlParameter("@Col14",SqlDbType.NVarChar),
					new SqlParameter("@Col15",SqlDbType.NVarChar),
					new SqlParameter("@Col16",SqlDbType.NVarChar),
					new SqlParameter("@Col17",SqlDbType.NVarChar),
					new SqlParameter("@Col18",SqlDbType.NVarChar),
					new SqlParameter("@Col19",SqlDbType.NVarChar),
					new SqlParameter("@Col20",SqlDbType.NVarChar),
					new SqlParameter("@Col21",SqlDbType.NVarChar),
					new SqlParameter("@Col22",SqlDbType.NVarChar),
					new SqlParameter("@Col23",SqlDbType.NVarChar),
					new SqlParameter("@Col24",SqlDbType.NVarChar),
					new SqlParameter("@Col25",SqlDbType.NVarChar),
					new SqlParameter("@Col26",SqlDbType.NVarChar),
					new SqlParameter("@Col27",SqlDbType.NVarChar),
					new SqlParameter("@Col28",SqlDbType.NVarChar),
					new SqlParameter("@Col29",SqlDbType.NVarChar),
					new SqlParameter("@Col30",SqlDbType.NVarChar),
					new SqlParameter("@Col31",SqlDbType.NVarChar),
					new SqlParameter("@Col32",SqlDbType.NVarChar),
					new SqlParameter("@Col33",SqlDbType.NVarChar),
					new SqlParameter("@Col34",SqlDbType.NVarChar),
					new SqlParameter("@Col35",SqlDbType.NVarChar),
					new SqlParameter("@Col36",SqlDbType.NVarChar),
					new SqlParameter("@Col37",SqlDbType.NVarChar),
					new SqlParameter("@Col38",SqlDbType.NVarChar),
					new SqlParameter("@Col39",SqlDbType.NVarChar),
					new SqlParameter("@Col40",SqlDbType.NVarChar),
					new SqlParameter("@Col41",SqlDbType.NVarChar),
					new SqlParameter("@Col42",SqlDbType.NVarChar),
					new SqlParameter("@Col43",SqlDbType.NVarChar),
					new SqlParameter("@Col44",SqlDbType.NVarChar),
					new SqlParameter("@Col45",SqlDbType.NVarChar),
					new SqlParameter("@Col46",SqlDbType.NVarChar),
					new SqlParameter("@Col47",SqlDbType.NVarChar),
					new SqlParameter("@Col48",SqlDbType.NVarChar),
					new SqlParameter("@Col49",SqlDbType.NVarChar),
					new SqlParameter("@Col50",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.CouponCode;
			parameters[1].Value = pEntity.CouponDesc;
			parameters[2].Value = pEntity.BeginDate;
			parameters[3].Value = pEntity.EndDate;
			parameters[4].Value = pEntity.CouponUrl;
			parameters[5].Value = pEntity.ImageUrl;
			parameters[6].Value = pEntity.Status;
			parameters[7].Value = pEntity.CreateTime;
			parameters[8].Value = pEntity.CreateBy;
			parameters[9].Value = pEntity.LastUpdateTime;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.IsDelete;
			parameters[12].Value = pEntity.CouponTypeID;
			parameters[13].Value = pkString;
            parameters[14].Value = pEntity.Col1;
            parameters[15].Value = pEntity.Col2;
            parameters[16].Value = pEntity.Col3;
            parameters[17].Value = pEntity.Col4;
            parameters[18].Value = pEntity.Col5;
            parameters[19].Value = pEntity.Col6;
            parameters[20].Value = pEntity.Col7;
            parameters[21].Value = pEntity.Col8;
            parameters[22].Value = pEntity.Col9;
            parameters[23].Value = pEntity.Col10;
            parameters[24].Value = pEntity.Col11;
            parameters[25].Value = pEntity.Col12;
            parameters[26].Value = pEntity.Col13;
            parameters[27].Value = pEntity.Col14;
            parameters[28].Value = pEntity.Col15;
            parameters[29].Value = pEntity.Col16;
            parameters[30].Value = pEntity.Col17;
            parameters[31].Value = pEntity.Col18;
            parameters[32].Value = pEntity.Col19;
            parameters[33].Value = pEntity.Col20;
            parameters[34].Value = pEntity.Col21;
            parameters[35].Value = pEntity.Col22;
            parameters[36].Value = pEntity.Col23;
            parameters[37].Value = pEntity.Col24;
            parameters[38].Value = pEntity.Col25;
            parameters[39].Value = pEntity.Col26;
            parameters[40].Value = pEntity.Col27;
            parameters[41].Value = pEntity.Col28;
            parameters[42].Value = pEntity.Col29;
            parameters[43].Value = pEntity.Col30;
            parameters[44].Value = pEntity.Col31;
            parameters[45].Value = pEntity.Col32;
            parameters[46].Value = pEntity.Col33;
            parameters[47].Value = pEntity.Col34;
            parameters[48].Value = pEntity.Col35;
            parameters[49].Value = pEntity.Col36;
            parameters[50].Value = pEntity.Col37;
            parameters[51].Value = pEntity.Col38;
            parameters[52].Value = pEntity.Col39;
            parameters[53].Value = pEntity.Col40;
            parameters[54].Value = pEntity.Col41;
            parameters[55].Value = pEntity.Col42;
            parameters[56].Value = pEntity.Col43;
            parameters[57].Value = pEntity.Col44;
            parameters[58].Value = pEntity.Col45;
            parameters[59].Value = pEntity.Col46;
            parameters[60].Value = pEntity.Col47;
            parameters[61].Value = pEntity.Col48;
            parameters[62].Value = pEntity.Col49;
            parameters[63].Value = pEntity.Col50;

            //执行并将结果回写
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.CouponID = pkString;
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public CouponEntity GetByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Coupon] where CouponID='{0}' and IsDelete=0 ", id.ToString());
            //读取数据
            CouponEntity m = null;
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
        public CouponEntity[] GetAll()
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Coupon] where isdelete=0");
            //读取数据
            List<CouponEntity> list = new List<CouponEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponEntity m;
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
        public void Update(CouponEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(CouponEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CouponID==null)
            {
                throw new ArgumentException("执行更新时,实体的主键属性值不能为null.");
            }
             //初始化固定字段
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //组织参数化SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [Coupon] set ");
            if (pIsUpdateNullField || pEntity.CouponCode!=null)
                strSql.Append( "[CouponCode]=@CouponCode,");
            if (pIsUpdateNullField || pEntity.CouponDesc!=null)
                strSql.Append( "[CouponDesc]=@CouponDesc,");
            if (pIsUpdateNullField || pEntity.BeginDate!=null)
                strSql.Append( "[BeginDate]=@BeginDate,");
            if (pIsUpdateNullField || pEntity.EndDate!=null)
                strSql.Append( "[EndDate]=@EndDate,");
            if (pIsUpdateNullField || pEntity.CouponUrl!=null)
                strSql.Append( "[CouponUrl]=@CouponUrl,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CouponTypeID!=null)
                strSql.Append( "[CouponTypeID]=@CouponTypeID,");
            /**
            if (pIsUpdateNullField || pEntity.Col1 != null)
                strSql.Append("[Col1]=@Col1,");
            if (pIsUpdateNullField || pEntity.Col2 != null)
                strSql.Append("[Col2]=@Col2,");
            if (pIsUpdateNullField || pEntity.Col3 != null)
                strSql.Append("[Col3]=@Col3,");
            if (pIsUpdateNullField || pEntity.Col4 != null)
                strSql.Append("[Col4]=@Col4,");
            if (pIsUpdateNullField || pEntity.Col5 != null)
                strSql.Append("[Col5]=@Col5,");
            if (pIsUpdateNullField || pEntity.Col6 != null)
                strSql.Append("[Col6]=@Col6,");
            if (pIsUpdateNullField || pEntity.Col7 != null)
                strSql.Append("[Col7]=@Col7,");
            if (pIsUpdateNullField || pEntity.Col8 != null)
                strSql.Append("[Col8]=@Col8,");
            if (pIsUpdateNullField || pEntity.Col9 != null)
                strSql.Append("[Col9]=@Col9,");
            if (pIsUpdateNullField || pEntity.Col10 != null)
                strSql.Append("[Col10]=@Col10,");
            if (pIsUpdateNullField || pEntity.Col11 != null)
                strSql.Append("[Col11]=@Col11,");
            if (pIsUpdateNullField || pEntity.Col12 != null)
                strSql.Append("[Col12]=@Col12,");
            if (pIsUpdateNullField || pEntity.Col13 != null)
                strSql.Append("[Col13]=@Col13,");
            if (pIsUpdateNullField || pEntity.Col14 != null)
                strSql.Append("[Col14]=@Col14,");
            if (pIsUpdateNullField || pEntity.Col15 != null)
                strSql.Append("[Col15]=@Col15,");
            if (pIsUpdateNullField || pEntity.Col16 != null)
                strSql.Append("[Col16]=@Col16,");
            if (pIsUpdateNullField || pEntity.Col17 != null)
                strSql.Append("[Col17]=@Col17,");
            if (pIsUpdateNullField || pEntity.Col18 != null)
                strSql.Append("[Col18]=@Col18,");
            if (pIsUpdateNullField || pEntity.Col19 != null)
                strSql.Append("[Col19]=@Col19,");
            if (pIsUpdateNullField || pEntity.Col20 != null)
                strSql.Append("[Col20]=@Col20,");
            if (pIsUpdateNullField || pEntity.Col21 != null)
                strSql.Append("[Col21]=@Col21,");
            if (pIsUpdateNullField || pEntity.Col22 != null)
                strSql.Append("[Col22]=@Col22,");
            if (pIsUpdateNullField || pEntity.Col23 != null)
                strSql.Append("[Col23]=@Col23,");
            if (pIsUpdateNullField || pEntity.Col24 != null)
                strSql.Append("[Col24]=@Col24,");
            if (pIsUpdateNullField || pEntity.Col25 != null)
                strSql.Append("[Col25]=@Col25,");
            if (pIsUpdateNullField || pEntity.Col26 != null)
                strSql.Append("[Col26]=@Col26,");
            if (pIsUpdateNullField || pEntity.Col27 != null)
                strSql.Append("[Col27]=@Col27,");
            if (pIsUpdateNullField || pEntity.Col28 != null)
                strSql.Append("[Col28]=@Col28,");
            if (pIsUpdateNullField || pEntity.Col29 != null)
                strSql.Append("[Col29]=@Col29,");
            if (pIsUpdateNullField || pEntity.Col30 != null)
                strSql.Append("[Col30]=@Col30,");
            if (pIsUpdateNullField || pEntity.Col31 != null)
                strSql.Append("[Col31]=@Col31,");
            if (pIsUpdateNullField || pEntity.Col32 != null)
                strSql.Append("[Col32]=@Col32,");
            if (pIsUpdateNullField || pEntity.Col33 != null)
                strSql.Append("[Col33]=@Col33,");
            if (pIsUpdateNullField || pEntity.Col34 != null)
                strSql.Append("[Col34]=@Col34,");
            if (pIsUpdateNullField || pEntity.Col35 != null)
                strSql.Append("[Col35]=@Col35,");
            if (pIsUpdateNullField || pEntity.Col36 != null)
                strSql.Append("[Col36]=@Col36,");
            if (pIsUpdateNullField || pEntity.Col37 != null)
                strSql.Append("[Col37]=@Col37,");
            if (pIsUpdateNullField || pEntity.Col38 != null)
                strSql.Append("[Col38]=@Col38,");
            if (pIsUpdateNullField || pEntity.Col39 != null)
                strSql.Append("[Col39]=@Col39,");
            if (pIsUpdateNullField || pEntity.Col40 != null)
                strSql.Append("[Col40]=@Col40,");
            if (pIsUpdateNullField || pEntity.Col41 != null)
                strSql.Append("[Col41]=@Col41,");
            if (pIsUpdateNullField || pEntity.Col42 != null)
                strSql.Append("[Col42]=@Col42,");
            if (pIsUpdateNullField || pEntity.Col43 != null)
                strSql.Append("[Col43]=@Col43,");
            if (pIsUpdateNullField || pEntity.Col44 != null)
                strSql.Append("[Col44]=@Col44,");
            if (pIsUpdateNullField || pEntity.Col45 != null)
                strSql.Append("[Col45]=@Col45,");
            if (pIsUpdateNullField || pEntity.Col46 != null)
                strSql.Append("[Col46]=@Col46,");
            if (pIsUpdateNullField || pEntity.Col47 != null)
                strSql.Append("[Col47]=@Col47,");
            if (pIsUpdateNullField || pEntity.Col48 != null)
                strSql.Append("[Col48]=@Col48,");
            if (pIsUpdateNullField || pEntity.Col49 != null)
                strSql.Append("[Col49]=@Col49,");
            if (pIsUpdateNullField || pEntity.Col50 != null)
                strSql.Append("[Col50]=@Col50");
             ***/
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where CouponID=@CouponID ; ");
            
            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouponCode",SqlDbType.NVarChar),
					new SqlParameter("@CouponDesc",SqlDbType.NVarChar),
					new SqlParameter("@BeginDate",SqlDbType.DateTime),
					new SqlParameter("@EndDate",SqlDbType.DateTime),
					new SqlParameter("@CouponUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CouponTypeID",SqlDbType.NVarChar),
					new SqlParameter("@CouponID",SqlDbType.NVarChar),
                    /**
                    new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Col11",SqlDbType.NVarChar),
					new SqlParameter("@Col12",SqlDbType.NVarChar),
					new SqlParameter("@Col13",SqlDbType.NVarChar),
					new SqlParameter("@Col14",SqlDbType.NVarChar),
					new SqlParameter("@Col15",SqlDbType.NVarChar),
					new SqlParameter("@Col16",SqlDbType.NVarChar),
					new SqlParameter("@Col17",SqlDbType.NVarChar),
					new SqlParameter("@Col18",SqlDbType.NVarChar),
					new SqlParameter("@Col19",SqlDbType.NVarChar),
					new SqlParameter("@Col20",SqlDbType.NVarChar),
					new SqlParameter("@Col21",SqlDbType.NVarChar),
					new SqlParameter("@Col22",SqlDbType.NVarChar),
					new SqlParameter("@Col23",SqlDbType.NVarChar),
					new SqlParameter("@Col24",SqlDbType.NVarChar),
					new SqlParameter("@Col25",SqlDbType.NVarChar),
					new SqlParameter("@Col26",SqlDbType.NVarChar),
					new SqlParameter("@Col27",SqlDbType.NVarChar),
					new SqlParameter("@Col28",SqlDbType.NVarChar),
					new SqlParameter("@Col29",SqlDbType.NVarChar),
					new SqlParameter("@Col30",SqlDbType.NVarChar),
					new SqlParameter("@Col31",SqlDbType.NVarChar),
					new SqlParameter("@Col32",SqlDbType.NVarChar),
					new SqlParameter("@Col33",SqlDbType.NVarChar),
					new SqlParameter("@Col34",SqlDbType.NVarChar),
					new SqlParameter("@Col35",SqlDbType.NVarChar),
					new SqlParameter("@Col36",SqlDbType.NVarChar),
					new SqlParameter("@Col37",SqlDbType.NVarChar),
					new SqlParameter("@Col38",SqlDbType.NVarChar),
					new SqlParameter("@Col39",SqlDbType.NVarChar),
					new SqlParameter("@Col40",SqlDbType.NVarChar),
					new SqlParameter("@Col41",SqlDbType.NVarChar),
					new SqlParameter("@Col42",SqlDbType.NVarChar),
					new SqlParameter("@Col43",SqlDbType.NVarChar),
					new SqlParameter("@Col44",SqlDbType.NVarChar),
					new SqlParameter("@Col45",SqlDbType.NVarChar),
					new SqlParameter("@Col46",SqlDbType.NVarChar),
					new SqlParameter("@Col47",SqlDbType.NVarChar),
					new SqlParameter("@Col48",SqlDbType.NVarChar),
					new SqlParameter("@Col49",SqlDbType.NVarChar),
					new SqlParameter("@Col50",SqlDbType.NVarChar)
                     * **/

            };
			parameters[0].Value = pEntity.CouponCode;
			parameters[1].Value = pEntity.CouponDesc;
			parameters[2].Value = pEntity.BeginDate;
			parameters[3].Value = pEntity.EndDate;
			parameters[4].Value = pEntity.CouponUrl;
			parameters[5].Value = pEntity.ImageUrl;
			parameters[6].Value = pEntity.Status;
			parameters[7].Value = pEntity.LastUpdateTime;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.CouponTypeID;
			parameters[10].Value = pEntity.CouponID;
            /**
            parameters[11].Value = pEntity.Col1;
            parameters[12].Value = pEntity.Col2;
            parameters[13].Value = pEntity.Col3;
            parameters[14].Value = pEntity.Col4;
            parameters[15].Value = pEntity.Col5;
            parameters[16].Value = pEntity.Col6;
            parameters[17].Value = pEntity.Col7;
            parameters[18].Value = pEntity.Col8;
            parameters[19].Value = pEntity.Col9;
            parameters[20].Value = pEntity.Col10;
            parameters[21].Value = pEntity.Col11;
            parameters[22].Value = pEntity.Col12;
            parameters[23].Value = pEntity.Col13;
            parameters[24].Value = pEntity.Col14;
            parameters[25].Value = pEntity.Col15;
            parameters[26].Value = pEntity.Col16;
            parameters[27].Value = pEntity.Col17;
            parameters[28].Value = pEntity.Col18;
            parameters[29].Value = pEntity.Col19;
            parameters[30].Value = pEntity.Col20;
            parameters[31].Value = pEntity.Col21;
            parameters[32].Value = pEntity.Col22;
            parameters[33].Value = pEntity.Col23;
            parameters[34].Value = pEntity.Col24;
            parameters[35].Value = pEntity.Col25;
            parameters[36].Value = pEntity.Col26;
            parameters[37].Value = pEntity.Col27;
            parameters[38].Value = pEntity.Col28;
            parameters[39].Value = pEntity.Col29;
            parameters[40].Value = pEntity.Col30;
            parameters[41].Value = pEntity.Col31;
            parameters[42].Value = pEntity.Col32;
            parameters[43].Value = pEntity.Col33;
            parameters[44].Value = pEntity.Col34;
            parameters[45].Value = pEntity.Col35;
            parameters[46].Value = pEntity.Col36;
            parameters[47].Value = pEntity.Col37;
            parameters[48].Value = pEntity.Col38;
            parameters[49].Value = pEntity.Col39;
            parameters[50].Value = pEntity.Col40;
            parameters[51].Value = pEntity.Col41;
            parameters[52].Value = pEntity.Col42;
            parameters[53].Value = pEntity.Col43;
            parameters[54].Value = pEntity.Col44;
            parameters[55].Value = pEntity.Col45;
            parameters[56].Value = pEntity.Col46;
            parameters[57].Value = pEntity.Col47;
            parameters[58].Value = pEntity.Col48;
            parameters[59].Value = pEntity.Col49;
            parameters[60].Value = pEntity.Col50;
            **/
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
        public void Update(CouponEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(CouponEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        #region 新增核销后插入数据方法 2014-10-8

        public void UpdateCouponUse(string CouponID, string Comment, string VipID, string CreateBy, string UnitID, string CustomerID)
        {
            string sql = string.Format(@"
                    insert into dbo.CouponUse 
                    (CouponUseID,CouponID,VipID,UnitID,Comment,CreateBy,CustomerID,CreateTime,LastUpdateBy,LastUpdateTime,IsDelete)
                    VALUES
                    (newid(),'{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}',getdate(),0)", CouponID, VipID, UnitID, Comment, CreateBy, CustomerID, CreateBy);
            SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(CouponEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(CouponEntity pEntity, IDbTransaction pTran)
        {
            //参数校验
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.CouponID == null)
            {
                throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
            }
            //执行 
            this.Delete(pEntity.CouponID, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>        /// 
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [Coupon] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=1 where CouponID = '" + pID + "';");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString()); 
        }
        #region 是否置为废卡 2014-9-26

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>        /// 
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        /// <param name="isDelete">是否删除</param>
        public void DeleteNew(object pID, IDbTransaction pTran, int isDel)
        {
            if (pID == null)
                return;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [Coupon] set LastUpdateTime='" + DateTime.Now.ToString() + "',LastUpdateBy='" + CurrentUserInfo.UserID + "',IsDelete=" + isDel + " where CouponID = '" + pID + "';");
            //执行语句
            int result = 0;
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString());
        }

        #endregion

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(CouponEntity[] pEntities, IDbTransaction pTran)
        {
            //整理主键值
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //参数校验
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.CouponID==null)
                {
                    throw new ArgumentException("执行删除时,实体的主键属性值不能为null.");
                }
                entityIDs[i] = item.CouponID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(CouponEntity[] pEntities)
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
            sql.AppendLine("update [Coupon] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy="+CurrentUserInfo.UserID+",IsDelete=1 where CouponID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public CouponEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [Coupon] WITH(NOLOCK) where isdelete=0 ");
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
            List<CouponEntity> list = new List<CouponEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponEntity m;
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
        public PagedQueryResult<CouponEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [CouponID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(") as ___rn,* from [Coupon] where isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1) from [Coupon] where isdelete=0 ");
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
            PagedQueryResult<CouponEntity> result = new PagedQueryResult<CouponEntity>();
            List<CouponEntity> list = new List<CouponEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    CouponEntity m;
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
        public CouponEntity[] QueryByEntity(CouponEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<CouponEntity> PagedQueryByEntity(CouponEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(CouponEntity pQueryEntity)
        { 
            //获取非空属性数量
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.CouponID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponID", Value = pQueryEntity.CouponID });
            if (pQueryEntity.CouponCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponCode", Value = pQueryEntity.CouponCode });
            if (pQueryEntity.CouponDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponDesc", Value = pQueryEntity.CouponDesc });
            if (pQueryEntity.BeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginDate", Value = pQueryEntity.BeginDate });
            if (pQueryEntity.EndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndDate", Value = pQueryEntity.EndDate });
            if (pQueryEntity.CouponUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponUrl", Value = pQueryEntity.CouponUrl });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
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
            if (pQueryEntity.CouponTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CouponTypeID", Value = pQueryEntity.CouponTypeID });
            if (pQueryEntity.Col1 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col1", Value = pQueryEntity.Col1 });
            if (pQueryEntity.Col2 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col2", Value = pQueryEntity.Col2 });
            if (pQueryEntity.Col3 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col3", Value = pQueryEntity.Col3 });
            if (pQueryEntity.Col4 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col4", Value = pQueryEntity.Col4 });
            if (pQueryEntity.Col5 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col5", Value = pQueryEntity.Col5 });
            if (pQueryEntity.Col6 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col6", Value = pQueryEntity.Col6 });
            if (pQueryEntity.Col7 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col7", Value = pQueryEntity.Col7 });
            if (pQueryEntity.Col8 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col8", Value = pQueryEntity.Col8 });
            if (pQueryEntity.Col9 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col9", Value = pQueryEntity.Col9 });
            if (pQueryEntity.Col10 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col10", Value = pQueryEntity.Col10 });
            if (pQueryEntity.Col11 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col11", Value = pQueryEntity.Col11 });
            if (pQueryEntity.Col12 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col12", Value = pQueryEntity.Col12 });
            if (pQueryEntity.Col13 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col13", Value = pQueryEntity.Col13 });
            if (pQueryEntity.Col14 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col14", Value = pQueryEntity.Col14 });
            if (pQueryEntity.Col15 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col15", Value = pQueryEntity.Col15 });
            if (pQueryEntity.Col16 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col16", Value = pQueryEntity.Col16 });
            if (pQueryEntity.Col17 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col17", Value = pQueryEntity.Col17 });
            if (pQueryEntity.Col18 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col18", Value = pQueryEntity.Col18 });
            if (pQueryEntity.Col19 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col19", Value = pQueryEntity.Col19 });
            if (pQueryEntity.Col20 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col20", Value = pQueryEntity.Col20 });
            if (pQueryEntity.Col21 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col21", Value = pQueryEntity.Col21 });
            if (pQueryEntity.Col22 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col22", Value = pQueryEntity.Col22 });
            if (pQueryEntity.Col23 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col23", Value = pQueryEntity.Col23 });
            if (pQueryEntity.Col24 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col24", Value = pQueryEntity.Col24 });
            if (pQueryEntity.Col25 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col25", Value = pQueryEntity.Col25 });
            if (pQueryEntity.Col26 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col26", Value = pQueryEntity.Col26 });
            if (pQueryEntity.Col27 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col27", Value = pQueryEntity.Col27 });
            if (pQueryEntity.Col28 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col28", Value = pQueryEntity.Col28 });
            if (pQueryEntity.Col29 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col29", Value = pQueryEntity.Col29 });
            if (pQueryEntity.Col30 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col30", Value = pQueryEntity.Col30 });
            if (pQueryEntity.Col31 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col31", Value = pQueryEntity.Col31 });
            if (pQueryEntity.Col32 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col32", Value = pQueryEntity.Col32 });
            if (pQueryEntity.Col33 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col33", Value = pQueryEntity.Col33 });
            if (pQueryEntity.Col34 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col34", Value = pQueryEntity.Col34 });
            if (pQueryEntity.Col35 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col35", Value = pQueryEntity.Col35 });
            if (pQueryEntity.Col36 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col36", Value = pQueryEntity.Col36 });
            if (pQueryEntity.Col37 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col37", Value = pQueryEntity.Col37 });
            if (pQueryEntity.Col38 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col38", Value = pQueryEntity.Col38 });
            if (pQueryEntity.Col39 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col39", Value = pQueryEntity.Col39 });
            if (pQueryEntity.Col40 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col40", Value = pQueryEntity.Col40 });
            if (pQueryEntity.Col41 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col41", Value = pQueryEntity.Col41 });
            if (pQueryEntity.Col42 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col42", Value = pQueryEntity.Col42 });
            if (pQueryEntity.Col43 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col43", Value = pQueryEntity.Col43 });
            if (pQueryEntity.Col44 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col44", Value = pQueryEntity.Col44 });
            if (pQueryEntity.Col45 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col45", Value = pQueryEntity.Col45 });
            if (pQueryEntity.Col46 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col46", Value = pQueryEntity.Col46 });
            if (pQueryEntity.Col47 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col47", Value = pQueryEntity.Col47 });
            if (pQueryEntity.Col48 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col48", Value = pQueryEntity.Col48 });
            if (pQueryEntity.Col49 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col49", Value = pQueryEntity.Col49 });
            if (pQueryEntity.Col50 != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col50", Value = pQueryEntity.Col50 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// 装载实体
        /// </summary>
        /// <param name="pReader">向前只读器</param>
        /// <param name="pInstance">实体实例</param>
        protected void Load(SqlDataReader pReader, out CouponEntity pInstance)
        {
            //将所有的数据从SqlDataReader中读取到Entity中
            pInstance = new CouponEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["CouponID"] != DBNull.Value)
			{
				pInstance.CouponID =  Convert.ToString(pReader["CouponID"]);
			}
			if (pReader["CouponCode"] != DBNull.Value)
			{
				pInstance.CouponCode =  Convert.ToString(pReader["CouponCode"]);
			}
			if (pReader["CouponDesc"] != DBNull.Value)
			{
				pInstance.CouponDesc =  Convert.ToString(pReader["CouponDesc"]);
			}
			if (pReader["BeginDate"] != DBNull.Value)
			{
				pInstance.BeginDate =  Convert.ToDateTime(pReader["BeginDate"]);
			}
			if (pReader["EndDate"] != DBNull.Value)
			{
				pInstance.EndDate =  Convert.ToDateTime(pReader["EndDate"]);
			}
			if (pReader["CouponUrl"] != DBNull.Value)
			{
				pInstance.CouponUrl =  Convert.ToString(pReader["CouponUrl"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
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
			if (pReader["CouponTypeID"] != DBNull.Value)
			{
				pInstance.CouponTypeID =  Convert.ToString(pReader["CouponTypeID"]);
			}
            /**
            if (pReader["Col1"] != DBNull.Value)
            {
                pInstance.Col1 = Convert.ToString(pReader["Col1"]);
            }
            if (pReader["Col2"] != DBNull.Value)
            {
                pInstance.Col2 = Convert.ToString(pReader["Col2"]);
            }
            if (pReader["Col3"] != DBNull.Value)
            {
                pInstance.Col3 = Convert.ToString(pReader["Col3"]);
            }
            if (pReader["Col4"] != DBNull.Value)
            {
                pInstance.Col4 = Convert.ToString(pReader["Col4"]);
            }
            if (pReader["Col5"] != DBNull.Value)
            {
                pInstance.Col5 = Convert.ToString(pReader["Col5"]);
            }
            if (pReader["Col6"] != DBNull.Value)
            {
                pInstance.Col6 = Convert.ToString(pReader["Col6"]);
            }
            if (pReader["Col7"] != DBNull.Value)
            {
                pInstance.Col7 = Convert.ToString(pReader["Col7"]);
            }
            if (pReader["Col8"] != DBNull.Value)
            {
                pInstance.Col8 = Convert.ToString(pReader["Col8"]);
            }
            if (pReader["Col9"] != DBNull.Value)
            {
                pInstance.Col9 = Convert.ToString(pReader["Col9"]);
            }
            if (pReader["Col10"] != DBNull.Value)
            {
                pInstance.Col10 = Convert.ToString(pReader["Col10"]);
            }
            if (pReader["Col11"] != DBNull.Value)
            {
                pInstance.Col11 = Convert.ToString(pReader["Col11"]);
            }
            if (pReader["Col12"] != DBNull.Value)
            {
                pInstance.Col12 = Convert.ToString(pReader["Col12"]);
            }
            if (pReader["Col13"] != DBNull.Value)
            {
                pInstance.Col13 = Convert.ToString(pReader["Col13"]);
            }
            if (pReader["Col14"] != DBNull.Value)
            {
                pInstance.Col14 = Convert.ToString(pReader["Col14"]);
            }
            if (pReader["Col15"] != DBNull.Value)
            {
                pInstance.Col15 = Convert.ToString(pReader["Col15"]);
            }
            if (pReader["Col16"] != DBNull.Value)
            {
                pInstance.Col16 = Convert.ToString(pReader["Col16"]);
            }
            if (pReader["Col17"] != DBNull.Value)
            {
                pInstance.Col17 = Convert.ToString(pReader["Col17"]);
            }
            if (pReader["Col18"] != DBNull.Value)
            {
                pInstance.Col18 = Convert.ToString(pReader["Col18"]);
            }
            if (pReader["Col19"] != DBNull.Value)
            {
                pInstance.Col19 = Convert.ToString(pReader["Col19"]);
            }
            if (pReader["Col20"] != DBNull.Value)
            {
                pInstance.Col20 = Convert.ToString(pReader["Col20"]);
            }
            if (pReader["Col21"] != DBNull.Value)
            {
                pInstance.Col21 = Convert.ToString(pReader["Col21"]);
            }
            if (pReader["Col22"] != DBNull.Value)
            {
                pInstance.Col22 = Convert.ToString(pReader["Col22"]);
            }
            if (pReader["Col23"] != DBNull.Value)
            {
                pInstance.Col23 = Convert.ToString(pReader["Col23"]);
            }
            if (pReader["Col24"] != DBNull.Value)
            {
                pInstance.Col24 = Convert.ToString(pReader["Col24"]);
            }
            if (pReader["Col25"] != DBNull.Value)
            {
                pInstance.Col25 = Convert.ToString(pReader["Col25"]);
            }
            if (pReader["Col26"] != DBNull.Value)
            {
                pInstance.Col26 = Convert.ToString(pReader["Col26"]);
            }
            if (pReader["Col27"] != DBNull.Value)
            {
                pInstance.Col27 = Convert.ToString(pReader["Col27"]);
            }
            if (pReader["Col28"] != DBNull.Value)
            {
                pInstance.Col28 = Convert.ToString(pReader["Col28"]);
            }
            if (pReader["Col29"] != DBNull.Value)
            {
                pInstance.Col29 = Convert.ToString(pReader["Col29"]);
            }
            if (pReader["Col30"] != DBNull.Value)
            {
                pInstance.Col30 = Convert.ToString(pReader["Col30"]);
            }
            if (pReader["Col31"] != DBNull.Value)
            {
                pInstance.Col31 = Convert.ToString(pReader["Col31"]);
            }
            if (pReader["Col32"] != DBNull.Value)
            {
                pInstance.Col32 = Convert.ToString(pReader["Col32"]);
            }
            if (pReader["Col33"] != DBNull.Value)
            {
                pInstance.Col33 = Convert.ToString(pReader["Col33"]);
            }
            if (pReader["Col34"] != DBNull.Value)
            {
                pInstance.Col34 = Convert.ToString(pReader["Col34"]);
            }
            if (pReader["Col35"] != DBNull.Value)
            {
                pInstance.Col35 = Convert.ToString(pReader["Col35"]);
            }
            if (pReader["Col36"] != DBNull.Value)
            {
                pInstance.Col36 = Convert.ToString(pReader["Col36"]);
            }
            if (pReader["Col37"] != DBNull.Value)
            {
                pInstance.Col37 = Convert.ToString(pReader["Col37"]);
            }
            if (pReader["Col38"] != DBNull.Value)
            {
                pInstance.Col38 = Convert.ToString(pReader["Col38"]);
            }
            if (pReader["Col39"] != DBNull.Value)
            {
                pInstance.Col39 = Convert.ToString(pReader["Col39"]);
            }
            if (pReader["Col40"] != DBNull.Value)
            {
                pInstance.Col40 = Convert.ToString(pReader["Col40"]);
            }
            if (pReader["Col41"] != DBNull.Value)
            {
                pInstance.Col41 = Convert.ToString(pReader["Col41"]);
            }
            if (pReader["Col42"] != DBNull.Value)
            {
                pInstance.Col42 = Convert.ToString(pReader["Col42"]);
            }
            if (pReader["Col43"] != DBNull.Value)
            {
                pInstance.Col43 = Convert.ToString(pReader["Col43"]);
            }
            if (pReader["Col44"] != DBNull.Value)
            {
                pInstance.Col44 = Convert.ToString(pReader["Col44"]);
            }
            if (pReader["Col45"] != DBNull.Value)
            {
                pInstance.Col45 = Convert.ToString(pReader["Col45"]);
            }
            if (pReader["Col46"] != DBNull.Value)
            {
                pInstance.Col46 = Convert.ToString(pReader["Col46"]);
            }
            if (pReader["Col47"] != DBNull.Value)
            {
                pInstance.Col47 = Convert.ToString(pReader["Col47"]);
            }
            if (pReader["Col48"] != DBNull.Value)
            {
                pInstance.Col48 = Convert.ToString(pReader["Col48"]);
            }
            if (pReader["Col49"] != DBNull.Value)
            {
                pInstance.Col49 = Convert.ToString(pReader["Col49"]);
            }
            if (pReader["Col50"] != DBNull.Value)
            {
                pInstance.Col50 = Convert.ToString(pReader["Col50"]);
            }
             * **/

        }
        #endregion
    }
}
