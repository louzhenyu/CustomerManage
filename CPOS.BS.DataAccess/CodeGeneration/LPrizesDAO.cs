/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/14 10:33:32
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
    /// ��LPrizes�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LPrizesDAO : Base.BaseCPOSDAO, ICRUDable<LPrizesEntity>, IQueryable<LPrizesEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LPrizesDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(LPrizesEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(LPrizesEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LPrizes](");
            strSql.Append("[PrizeName],[PrizeShortDesc],[PrizeDesc],[LogoURL],[ImageUrl],[ContentText],[ContentUrl],[Price],[DisplayIndex],[CountTotal],[CountLeft],[EventId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[PrizeTypeId],[Point],[IsAutoPrizes],[PrizesID])");
            strSql.Append(" values (");
            strSql.Append("@PrizeName,@PrizeShortDesc,@PrizeDesc,@LogoURL,@ImageUrl,@ContentText,@ContentUrl,@Price,@DisplayIndex,@CountTotal,@CountLeft,@EventId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@PrizeTypeId,@Point,@IsAutoPrizes,@PrizesID)");            

			string pkString = pEntity.PrizesID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrizeName",SqlDbType.NVarChar),
					new SqlParameter("@PrizeShortDesc",SqlDbType.NVarChar),
					new SqlParameter("@PrizeDesc",SqlDbType.NVarChar),
					new SqlParameter("@LogoURL",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ContentText",SqlDbType.NVarChar),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CountTotal",SqlDbType.Int),
					new SqlParameter("@CountLeft",SqlDbType.Int),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@PrizeTypeId",SqlDbType.NVarChar),
					new SqlParameter("@Point",SqlDbType.Int),
					new SqlParameter("@IsAutoPrizes",SqlDbType.Int),
					new SqlParameter("@PrizesID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.PrizeName;
			parameters[1].Value = pEntity.PrizeShortDesc;
			parameters[2].Value = pEntity.PrizeDesc;
			parameters[3].Value = pEntity.LogoURL;
			parameters[4].Value = pEntity.ImageUrl;
			parameters[5].Value = pEntity.ContentText;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.Price;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.CountTotal;
			parameters[10].Value = pEntity.CountLeft;
			parameters[11].Value = pEntity.EventId;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.CreateBy;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pEntity.PrizeTypeId;
			parameters[18].Value = pEntity.Point;
			parameters[19].Value = pEntity.IsAutoPrizes;
            parameters[20].Value = pkString;


            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PrizesID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public LPrizesEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizes] where PrizesID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            LPrizesEntity m = null;
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
        public LPrizesEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizes] where isdelete=0");
            //��ȡ����
            List<LPrizesEntity> list = new List<LPrizesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizesEntity m;
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
        public void Update(LPrizesEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LPrizesEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrizesID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LPrizes] set ");
            if (pIsUpdateNullField || pEntity.PrizeName!=null)
                strSql.Append( "[PrizeName]=@PrizeName,");
            if (pIsUpdateNullField || pEntity.PrizeShortDesc!=null)
                strSql.Append( "[PrizeShortDesc]=@PrizeShortDesc,");
            if (pIsUpdateNullField || pEntity.PrizeDesc!=null)
                strSql.Append( "[PrizeDesc]=@PrizeDesc,");
            if (pIsUpdateNullField || pEntity.LogoURL!=null)
                strSql.Append( "[LogoURL]=@LogoURL,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ContentText!=null)
                strSql.Append( "[ContentText]=@ContentText,");
            if (pIsUpdateNullField || pEntity.ContentUrl!=null)
                strSql.Append( "[ContentUrl]=@ContentUrl,");
            if (pIsUpdateNullField || pEntity.Price!=null)
                strSql.Append( "[Price]=@Price,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex,");
            if (pIsUpdateNullField || pEntity.CountTotal!=null)
                strSql.Append( "[CountTotal]=@CountTotal,");
            if (pIsUpdateNullField || pEntity.CountLeft!=null)
                strSql.Append( "[CountLeft]=@CountLeft,");
            if (pIsUpdateNullField || pEntity.EventId!=null)
                strSql.Append( "[EventId]=@EventId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.PrizeTypeId!=null)
                strSql.Append( "[PrizeTypeId]=@PrizeTypeId,");
            if (pIsUpdateNullField || pEntity.Point!=null)
                strSql.Append( "[Point]=@Point,");
            if (pIsUpdateNullField || pEntity.IsAutoPrizes!=null)
                strSql.Append( "[IsAutoPrizes]=@IsAutoPrizes");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where PrizesID=@PrizesID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrizeName",SqlDbType.NVarChar),
					new SqlParameter("@PrizeShortDesc",SqlDbType.NVarChar),
					new SqlParameter("@PrizeDesc",SqlDbType.NVarChar),
					new SqlParameter("@LogoURL",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ContentText",SqlDbType.NVarChar),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@Price",SqlDbType.Decimal),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@CountTotal",SqlDbType.Int),
					new SqlParameter("@CountLeft",SqlDbType.Int),
					new SqlParameter("@EventId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@PrizeTypeId",SqlDbType.Int),
					new SqlParameter("@Point",SqlDbType.Int),
					new SqlParameter("@IsAutoPrizes",SqlDbType.Int),
					new SqlParameter("@PrizesID",SqlDbType.NVarChar),
            };
			parameters[0].Value = pEntity.PrizeName;
			parameters[1].Value = pEntity.PrizeShortDesc;
			parameters[2].Value = pEntity.PrizeDesc;
			parameters[3].Value = pEntity.LogoURL;
			parameters[4].Value = pEntity.ImageUrl;
			parameters[5].Value = pEntity.ContentText;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.Price;
			parameters[8].Value = pEntity.DisplayIndex;
			parameters[9].Value = pEntity.CountTotal;
			parameters[10].Value = pEntity.CountLeft;
			parameters[11].Value = pEntity.EventId;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.PrizeTypeId;
			parameters[15].Value = pEntity.Point;
            parameters[16].Value = pEntity.IsAutoPrizes;
			parameters[17].Value = pEntity.PrizesID;

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
        public void Update(LPrizesEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LPrizesEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LPrizesEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LPrizesEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrizesID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.PrizesID, pTran);           
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
            sql.AppendLine("update [LPrizes] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where PrizesID=@PrizesID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@PrizesID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LPrizesEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.PrizesID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.PrizesID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(LPrizesEntity[] pEntities)
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
            sql.AppendLine("update [LPrizes] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where PrizesID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LPrizesEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LPrizes] where isdelete=0 ");
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
            List<LPrizesEntity> list = new List<LPrizesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizesEntity m;
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
        public PagedQueryResult<LPrizesEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PrizesID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [LPrizes] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [LPrizes] where isdelete=0 ");
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
            PagedQueryResult<LPrizesEntity> result = new PagedQueryResult<LPrizesEntity>();
            List<LPrizesEntity> list = new List<LPrizesEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LPrizesEntity m;
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
        public LPrizesEntity[] QueryByEntity(LPrizesEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LPrizesEntity> PagedQueryByEntity(LPrizesEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LPrizesEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PrizesID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizesID", Value = pQueryEntity.PrizesID });
            if (pQueryEntity.PrizeName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeName", Value = pQueryEntity.PrizeName });
            if (pQueryEntity.PrizeShortDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeShortDesc", Value = pQueryEntity.PrizeShortDesc });
            if (pQueryEntity.PrizeDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeDesc", Value = pQueryEntity.PrizeDesc });
            if (pQueryEntity.LogoURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogoURL", Value = pQueryEntity.LogoURL });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ContentText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentText", Value = pQueryEntity.ContentText });
            if (pQueryEntity.ContentUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentUrl", Value = pQueryEntity.ContentUrl });
            if (pQueryEntity.Price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.CountTotal!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CountTotal", Value = pQueryEntity.CountTotal });
            if (pQueryEntity.CountLeft!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CountLeft", Value = pQueryEntity.CountLeft });
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
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
            if (pQueryEntity.PrizeTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrizeTypeId", Value = pQueryEntity.PrizeTypeId });
            if (pQueryEntity.Point!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Point", Value = pQueryEntity.Point });
            if (pQueryEntity.IsAutoPrizes!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAutoPrizes", Value = pQueryEntity.IsAutoPrizes });
            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out LPrizesEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new LPrizesEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PrizesID"] != DBNull.Value)
			{
				pInstance.PrizesID =  Convert.ToString(pReader["PrizesID"]);
			}
			if (pReader["PrizeName"] != DBNull.Value)
			{
				pInstance.PrizeName =  Convert.ToString(pReader["PrizeName"]);
			}
			if (pReader["PrizeShortDesc"] != DBNull.Value)
			{
				pInstance.PrizeShortDesc =  Convert.ToString(pReader["PrizeShortDesc"]);
			}
			if (pReader["PrizeDesc"] != DBNull.Value)
			{
				pInstance.PrizeDesc =  Convert.ToString(pReader["PrizeDesc"]);
			}
			if (pReader["LogoURL"] != DBNull.Value)
			{
				pInstance.LogoURL =  Convert.ToString(pReader["LogoURL"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ContentText"] != DBNull.Value)
			{
				pInstance.ContentText =  Convert.ToString(pReader["ContentText"]);
			}
			if (pReader["ContentUrl"] != DBNull.Value)
			{
				pInstance.ContentUrl =  Convert.ToString(pReader["ContentUrl"]);
			}
			if (pReader["Price"] != DBNull.Value)
			{
				pInstance.Price =  Convert.ToDecimal(pReader["Price"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["CountTotal"] != DBNull.Value)
			{
				pInstance.CountTotal =   Convert.ToInt32(pReader["CountTotal"]);
			}
			if (pReader["CountLeft"] != DBNull.Value)
			{
				pInstance.CountLeft =   Convert.ToInt32(pReader["CountLeft"]);
			}
			if (pReader["EventId"] != DBNull.Value)
			{
				pInstance.EventId =  Convert.ToString(pReader["EventId"]);
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
			if (pReader["PrizeTypeId"] != DBNull.Value)
			{
                pInstance.PrizeTypeId = Convert.ToString(pReader["PrizeTypeId"]);
			}
			if (pReader["Point"] != DBNull.Value)
			{
				pInstance.Point =   Convert.ToInt32(pReader["Point"]);
			}
			if (pReader["IsAutoPrizes"] != DBNull.Value)
			{
				pInstance.IsAutoPrizes =   Convert.ToInt32(pReader["IsAutoPrizes"]);
			}
        }
        #endregion
    }
}
