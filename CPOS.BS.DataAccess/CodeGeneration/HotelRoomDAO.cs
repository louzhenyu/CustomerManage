/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 11:41:01
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
    /// ���ݷ��ʣ�  
    /// ��HotelRoom�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HotelRoomDAO : Base.BaseCPOSDAO, ICRUDable<HotelRoomEntity>, IQueryable<HotelRoomEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HotelRoomDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(HotelRoomEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(HotelRoomEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [HotelRoom](");
            strSql.Append("[UnitId],[RoomName],[StandardPrice],[MaxIntegralToCurrency],[DonateIntegral],[ReturnCurrency],[MinBookPeriod],[MaxBookPeriod],[CommitPrice],[CommitType],[ValidBeginDate],[ValidEndDate],[RoomQty],[IsDisplay],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[RoomId])");
            strSql.Append(" values (");
            strSql.Append("@UnitId,@RoomName,@StandardPrice,@MaxIntegralToCurrency,@DonateIntegral,@ReturnCurrency,@MinBookPeriod,@MaxBookPeriod,@CommitPrice,@CommitType,@ValidBeginDate,@ValidEndDate,@RoomQty,@IsDisplay,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@RoomId)");            

			string pkString = pEntity.RoomId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@RoomName",SqlDbType.NVarChar),
					new SqlParameter("@StandardPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@MinBookPeriod",SqlDbType.Int),
					new SqlParameter("@MaxBookPeriod",SqlDbType.Int),
					new SqlParameter("@CommitPrice",SqlDbType.Decimal),
					new SqlParameter("@CommitType",SqlDbType.Int),
					new SqlParameter("@ValidBeginDate",SqlDbType.DateTime),
					new SqlParameter("@ValidEndDate",SqlDbType.DateTime),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@IsDisplay",SqlDbType.Int),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@RoomId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UnitId;
			parameters[1].Value = pEntity.RoomName;
			parameters[2].Value = pEntity.StandardPrice;
			parameters[3].Value = pEntity.MaxIntegralToCurrency;
			parameters[4].Value = pEntity.DonateIntegral;
			parameters[5].Value = pEntity.ReturnCurrency;
			parameters[6].Value = pEntity.MinBookPeriod;
			parameters[7].Value = pEntity.MaxBookPeriod;
			parameters[8].Value = pEntity.CommitPrice;
			parameters[9].Value = pEntity.CommitType;
			parameters[10].Value = pEntity.ValidBeginDate;
			parameters[11].Value = pEntity.ValidEndDate;
			parameters[12].Value = pEntity.RoomQty;
			parameters[13].Value = pEntity.IsDisplay;
			parameters[14].Value = pEntity.CreateBy;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.CustomerId;
			parameters[20].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RoomId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public HotelRoomEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelRoom] where RoomId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            HotelRoomEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public HotelRoomEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelRoom] where isdelete=0");
            //��ȡ����
            List<HotelRoomEntity> list = new List<HotelRoomEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelRoomEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(HotelRoomEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(HotelRoomEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RoomId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [HotelRoom] set ");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.RoomName!=null)
                strSql.Append( "[RoomName]=@RoomName,");
            if (pIsUpdateNullField || pEntity.StandardPrice!=null)
                strSql.Append( "[StandardPrice]=@StandardPrice,");
            if (pIsUpdateNullField || pEntity.MaxIntegralToCurrency!=null)
                strSql.Append( "[MaxIntegralToCurrency]=@MaxIntegralToCurrency,");
            if (pIsUpdateNullField || pEntity.DonateIntegral!=null)
                strSql.Append( "[DonateIntegral]=@DonateIntegral,");
            if (pIsUpdateNullField || pEntity.ReturnCurrency!=null)
                strSql.Append( "[ReturnCurrency]=@ReturnCurrency,");
            if (pIsUpdateNullField || pEntity.MinBookPeriod!=null)
                strSql.Append( "[MinBookPeriod]=@MinBookPeriod,");
            if (pIsUpdateNullField || pEntity.MaxBookPeriod!=null)
                strSql.Append( "[MaxBookPeriod]=@MaxBookPeriod,");
            if (pIsUpdateNullField || pEntity.CommitPrice!=null)
                strSql.Append( "[CommitPrice]=@CommitPrice,");
            if (pIsUpdateNullField || pEntity.CommitType!=null)
                strSql.Append( "[CommitType]=@CommitType,");
            if (pIsUpdateNullField || pEntity.ValidBeginDate!=null)
                strSql.Append( "[ValidBeginDate]=@ValidBeginDate,");
            if (pIsUpdateNullField || pEntity.ValidEndDate!=null)
                strSql.Append( "[ValidEndDate]=@ValidEndDate,");
            if (pIsUpdateNullField || pEntity.RoomQty!=null)
                strSql.Append( "[RoomQty]=@RoomQty,");
            if (pIsUpdateNullField || pEntity.IsDisplay!=null)
                strSql.Append( "[IsDisplay]=@IsDisplay,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where RoomId=@RoomId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@RoomName",SqlDbType.NVarChar),
					new SqlParameter("@StandardPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@MinBookPeriod",SqlDbType.Int),
					new SqlParameter("@MaxBookPeriod",SqlDbType.Int),
					new SqlParameter("@CommitPrice",SqlDbType.Decimal),
					new SqlParameter("@CommitType",SqlDbType.Int),
					new SqlParameter("@ValidBeginDate",SqlDbType.DateTime),
					new SqlParameter("@ValidEndDate",SqlDbType.DateTime),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@IsDisplay",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@RoomId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UnitId;
			parameters[1].Value = pEntity.RoomName;
			parameters[2].Value = pEntity.StandardPrice;
			parameters[3].Value = pEntity.MaxIntegralToCurrency;
			parameters[4].Value = pEntity.DonateIntegral;
			parameters[5].Value = pEntity.ReturnCurrency;
			parameters[6].Value = pEntity.MinBookPeriod;
			parameters[7].Value = pEntity.MaxBookPeriod;
			parameters[8].Value = pEntity.CommitPrice;
			parameters[9].Value = pEntity.CommitType;
			parameters[10].Value = pEntity.ValidBeginDate;
			parameters[11].Value = pEntity.ValidEndDate;
			parameters[12].Value = pEntity.RoomQty;
			parameters[13].Value = pEntity.IsDisplay;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pEntity.RoomId;

            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(HotelRoomEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(HotelRoomEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HotelRoomEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HotelRoomEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RoomId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RoomId, pTran);           
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [HotelRoom] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where RoomId=@RoomId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@RoomId",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HotelRoomEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.RoomId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.RoomId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(HotelRoomEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [HotelRoom] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where RoomId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public HotelRoomEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelRoom] where isdelete=0 ");
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
            //ִ��SQL
            List<HotelRoomEntity> list = new List<HotelRoomEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelRoomEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<HotelRoomEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
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
                pagedSql.AppendFormat(" [RoomId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [HotelRoom] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [HotelRoom] where isdelete=0 ");
            //��������
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
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<HotelRoomEntity> result = new PagedQueryResult<HotelRoomEntity>();
            List<HotelRoomEntity> list = new List<HotelRoomEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelRoomEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public HotelRoomEntity[] QueryByEntity(HotelRoomEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<HotelRoomEntity> PagedQueryByEntity(HotelRoomEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(HotelRoomEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RoomId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomId", Value = pQueryEntity.RoomId });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.RoomName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomName", Value = pQueryEntity.RoomName });
            if (pQueryEntity.StandardPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StandardPrice", Value = pQueryEntity.StandardPrice });
            if (pQueryEntity.MaxIntegralToCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxIntegralToCurrency", Value = pQueryEntity.MaxIntegralToCurrency });
            if (pQueryEntity.DonateIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DonateIntegral", Value = pQueryEntity.DonateIntegral });
            if (pQueryEntity.ReturnCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnCurrency", Value = pQueryEntity.ReturnCurrency });
            if (pQueryEntity.MinBookPeriod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinBookPeriod", Value = pQueryEntity.MinBookPeriod });
            if (pQueryEntity.MaxBookPeriod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxBookPeriod", Value = pQueryEntity.MaxBookPeriod });
            if (pQueryEntity.CommitPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommitPrice", Value = pQueryEntity.CommitPrice });
            if (pQueryEntity.CommitType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommitType", Value = pQueryEntity.CommitType });
            if (pQueryEntity.ValidBeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidBeginDate", Value = pQueryEntity.ValidBeginDate });
            if (pQueryEntity.ValidEndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidEndDate", Value = pQueryEntity.ValidEndDate });
            if (pQueryEntity.RoomQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomQty", Value = pQueryEntity.RoomQty });
            if (pQueryEntity.IsDisplay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDisplay", Value = pQueryEntity.IsDisplay });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out HotelRoomEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new HotelRoomEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RoomId"] != DBNull.Value)
			{
				pInstance.RoomId =  Convert.ToString(pReader["RoomId"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["RoomName"] != DBNull.Value)
			{
				pInstance.RoomName =  Convert.ToString(pReader["RoomName"]);
			}
			if (pReader["StandardPrice"] != DBNull.Value)
			{
				pInstance.StandardPrice =  Convert.ToDecimal(pReader["StandardPrice"]);
			}
			if (pReader["MaxIntegralToCurrency"] != DBNull.Value)
			{
				pInstance.MaxIntegralToCurrency =   Convert.ToInt32(pReader["MaxIntegralToCurrency"]);
			}
			if (pReader["DonateIntegral"] != DBNull.Value)
			{
				pInstance.DonateIntegral =   Convert.ToInt32(pReader["DonateIntegral"]);
			}
			if (pReader["ReturnCurrency"] != DBNull.Value)
			{
				pInstance.ReturnCurrency =  Convert.ToDecimal(pReader["ReturnCurrency"]);
			}
			if (pReader["MinBookPeriod"] != DBNull.Value)
			{
				pInstance.MinBookPeriod =   Convert.ToInt32(pReader["MinBookPeriod"]);
			}
			if (pReader["MaxBookPeriod"] != DBNull.Value)
			{
				pInstance.MaxBookPeriod =   Convert.ToInt32(pReader["MaxBookPeriod"]);
			}
			if (pReader["CommitPrice"] != DBNull.Value)
			{
				pInstance.CommitPrice =  Convert.ToDecimal(pReader["CommitPrice"]);
			}
			if (pReader["CommitType"] != DBNull.Value)
			{
				pInstance.CommitType =   Convert.ToInt32(pReader["CommitType"]);
			}
			if (pReader["ValidBeginDate"] != DBNull.Value)
			{
				pInstance.ValidBeginDate =  Convert.ToDateTime(pReader["ValidBeginDate"]);
			}
			if (pReader["ValidEndDate"] != DBNull.Value)
			{
				pInstance.ValidEndDate =  Convert.ToDateTime(pReader["ValidEndDate"]);
			}
			if (pReader["RoomQty"] != DBNull.Value)
			{
				pInstance.RoomQty =   Convert.ToInt32(pReader["RoomQty"]);
			}
			if (pReader["IsDisplay"] != DBNull.Value)
			{
				pInstance.IsDisplay =   Convert.ToInt32(pReader["IsDisplay"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
