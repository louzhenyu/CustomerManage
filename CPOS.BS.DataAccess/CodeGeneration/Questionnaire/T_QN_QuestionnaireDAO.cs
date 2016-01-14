/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/23 15:42:50
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
    /// ���ݷ��ʣ�  
    /// ��T_QN_Questionnaire�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_QuestionnaireDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireEntity>, IQueryable<T_QN_QuestionnaireEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_QN_QuestionnaireDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_QN_QuestionnaireEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_QN_QuestionnaireEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_QN_Questionnaire](");
            strSql.Append("[QuestionnaireName],[ModelType],[QuestionnaireType],[QRegular],[IsShowQRegular],[ButtonName],[BGImageSrc],[StartPageBtnBGColor],[StartPageBtnTextColor],[QResultTitle],[QResultBGImg],[QResultImg],[QResultBGColor],[QResultBtnTextColor],[Sort],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[QuestionnaireID])");
            strSql.Append(" values (");
            strSql.Append("@QuestionnaireName,@ModelType,@QuestionnaireType,@QRegular,@IsShowQRegular,@ButtonName,@BGImageSrc,@StartPageBtnBGColor,@StartPageBtnTextColor,@QResultTitle,@QResultBGImg,@QResultImg,@QResultBGColor,@QResultBtnTextColor,@Sort,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@QuestionnaireID)");            

			Guid? pkGuid;
			if (pEntity.QuestionnaireID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.QuestionnaireID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@QuestionnaireName",SqlDbType.NVarChar),
					new SqlParameter("@ModelType",SqlDbType.Int),
					new SqlParameter("@QuestionnaireType",SqlDbType.Int),
					new SqlParameter("@QRegular",SqlDbType.NVarChar),
					new SqlParameter("@IsShowQRegular",SqlDbType.Int),
					new SqlParameter("@ButtonName",SqlDbType.NVarChar),
					new SqlParameter("@BGImageSrc",SqlDbType.NVarChar),
					new SqlParameter("@StartPageBtnBGColor",SqlDbType.NVarChar),
					new SqlParameter("@StartPageBtnTextColor",SqlDbType.NVarChar),
					new SqlParameter("@QResultTitle",SqlDbType.NVarChar),
					new SqlParameter("@QResultBGImg",SqlDbType.NVarChar),
					new SqlParameter("@QResultImg",SqlDbType.NVarChar),
					new SqlParameter("@QResultBGColor",SqlDbType.NVarChar),
					new SqlParameter("@QResultBtnTextColor",SqlDbType.NVarChar),
					new SqlParameter("@Sort",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@QuestionnaireID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.QuestionnaireName;
			parameters[1].Value = pEntity.ModelType;
			parameters[2].Value = pEntity.QuestionnaireType;
			parameters[3].Value = pEntity.QRegular;
			parameters[4].Value = pEntity.IsShowQRegular;
			parameters[5].Value = pEntity.ButtonName;
			parameters[6].Value = pEntity.BGImageSrc;
			parameters[7].Value = pEntity.StartPageBtnBGColor;
			parameters[8].Value = pEntity.StartPageBtnTextColor;
			parameters[9].Value = pEntity.QResultTitle;
			parameters[10].Value = pEntity.QResultBGImg;
			parameters[11].Value = pEntity.QResultImg;
			parameters[12].Value = pEntity.QResultBGColor;
			parameters[13].Value = pEntity.QResultBtnTextColor;
			parameters[14].Value = pEntity.Sort;
			parameters[15].Value = pEntity.Status;
			parameters[16].Value = pEntity.CustomerID;
			parameters[17].Value = pEntity.CreateTime;
			parameters[18].Value = pEntity.CreateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.IsDelete;
			parameters[22].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.QuestionnaireID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_QN_QuestionnaireEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Questionnaire] where QuestionnaireID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_QN_QuestionnaireEntity m = null;
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
        public T_QN_QuestionnaireEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Questionnaire] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_QN_QuestionnaireEntity> list = new List<T_QN_QuestionnaireEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireEntity m;
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
        public void Update(T_QN_QuestionnaireEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_QN_QuestionnaireEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionnaireID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_QN_Questionnaire] set ");
                        if (pIsUpdateNullField || pEntity.QuestionnaireName!=null)
                strSql.Append( "[QuestionnaireName]=@QuestionnaireName,");
            if (pIsUpdateNullField || pEntity.ModelType!=null)
                strSql.Append( "[ModelType]=@ModelType,");
            if (pIsUpdateNullField || pEntity.QuestionnaireType!=null)
                strSql.Append( "[QuestionnaireType]=@QuestionnaireType,");
            if (pIsUpdateNullField || pEntity.QRegular!=null)
                strSql.Append( "[QRegular]=@QRegular,");
            if (pIsUpdateNullField || pEntity.IsShowQRegular!=null)
                strSql.Append( "[IsShowQRegular]=@IsShowQRegular,");
            if (pIsUpdateNullField || pEntity.ButtonName!=null)
                strSql.Append( "[ButtonName]=@ButtonName,");
            if (pIsUpdateNullField || pEntity.BGImageSrc!=null)
                strSql.Append( "[BGImageSrc]=@BGImageSrc,");
            if (pIsUpdateNullField || pEntity.StartPageBtnBGColor!=null)
                strSql.Append( "[StartPageBtnBGColor]=@StartPageBtnBGColor,");
            if (pIsUpdateNullField || pEntity.StartPageBtnTextColor!=null)
                strSql.Append( "[StartPageBtnTextColor]=@StartPageBtnTextColor,");
            if (pIsUpdateNullField || pEntity.QResultTitle!=null)
                strSql.Append( "[QResultTitle]=@QResultTitle,");
            if (pIsUpdateNullField || pEntity.QResultBGImg!=null)
                strSql.Append( "[QResultBGImg]=@QResultBGImg,");
            if (pIsUpdateNullField || pEntity.QResultImg!=null)
                strSql.Append( "[QResultImg]=@QResultImg,");
            if (pIsUpdateNullField || pEntity.QResultBGColor!=null)
                strSql.Append( "[QResultBGColor]=@QResultBGColor,");
            if (pIsUpdateNullField || pEntity.QResultBtnTextColor!=null)
                strSql.Append( "[QResultBtnTextColor]=@QResultBtnTextColor,");
            if (pIsUpdateNullField || pEntity.Sort!=null)
                strSql.Append( "[Sort]=@Sort,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            strSql.Append(" where QuestionnaireID=@QuestionnaireID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@QuestionnaireName",SqlDbType.NVarChar),
					new SqlParameter("@ModelType",SqlDbType.Int),
					new SqlParameter("@QuestionnaireType",SqlDbType.Int),
					new SqlParameter("@QRegular",SqlDbType.NVarChar),
					new SqlParameter("@IsShowQRegular",SqlDbType.Int),
					new SqlParameter("@ButtonName",SqlDbType.NVarChar),
					new SqlParameter("@BGImageSrc",SqlDbType.NVarChar),
					new SqlParameter("@StartPageBtnBGColor",SqlDbType.NVarChar),
					new SqlParameter("@StartPageBtnTextColor",SqlDbType.NVarChar),
					new SqlParameter("@QResultTitle",SqlDbType.NVarChar),
					new SqlParameter("@QResultBGImg",SqlDbType.NVarChar),
					new SqlParameter("@QResultImg",SqlDbType.NVarChar),
					new SqlParameter("@QResultBGColor",SqlDbType.NVarChar),
					new SqlParameter("@QResultBtnTextColor",SqlDbType.NVarChar),
					new SqlParameter("@Sort",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@QuestionnaireID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.QuestionnaireName;
			parameters[1].Value = pEntity.ModelType;
			parameters[2].Value = pEntity.QuestionnaireType;
			parameters[3].Value = pEntity.QRegular;
			parameters[4].Value = pEntity.IsShowQRegular;
			parameters[5].Value = pEntity.ButtonName;
			parameters[6].Value = pEntity.BGImageSrc;
			parameters[7].Value = pEntity.StartPageBtnBGColor;
			parameters[8].Value = pEntity.StartPageBtnTextColor;
			parameters[9].Value = pEntity.QResultTitle;
			parameters[10].Value = pEntity.QResultBGImg;
			parameters[11].Value = pEntity.QResultImg;
			parameters[12].Value = pEntity.QResultBGColor;
			parameters[13].Value = pEntity.QResultBtnTextColor;
			parameters[14].Value = pEntity.Sort;
			parameters[15].Value = pEntity.Status;
			parameters[16].Value = pEntity.CustomerID;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.QuestionnaireID;

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
        public void Update(T_QN_QuestionnaireEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_QN_QuestionnaireEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_QN_QuestionnaireEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.QuestionnaireID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.QuestionnaireID.Value, pTran);           
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
            sql.AppendLine("update [T_QN_Questionnaire] set  isdelete=1 where QuestionnaireID=@QuestionnaireID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@QuestionnaireID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(T_QN_QuestionnaireEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.QuestionnaireID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.QuestionnaireID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_QN_QuestionnaireEntity[] pEntities)
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
            sql.AppendLine("update [T_QN_Questionnaire] set  isdelete=1 where QuestionnaireID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_QN_QuestionnaireEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_Questionnaire] where 1=1  and isdelete=0 ");
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
            List<T_QN_QuestionnaireEntity> list = new List<T_QN_QuestionnaireEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireEntity m;
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
        public PagedQueryResult<T_QN_QuestionnaireEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [QuestionnaireID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_QN_Questionnaire] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_QN_Questionnaire] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_QN_QuestionnaireEntity> result = new PagedQueryResult<T_QN_QuestionnaireEntity>();
            List<T_QN_QuestionnaireEntity> list = new List<T_QN_QuestionnaireEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireEntity m;
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
        public T_QN_QuestionnaireEntity[] QueryByEntity(T_QN_QuestionnaireEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_QN_QuestionnaireEntity> PagedQueryByEntity(T_QN_QuestionnaireEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_QN_QuestionnaireEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.QuestionnaireID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireID", Value = pQueryEntity.QuestionnaireID });
            if (pQueryEntity.QuestionnaireName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireName", Value = pQueryEntity.QuestionnaireName });
            if (pQueryEntity.ModelType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ModelType", Value = pQueryEntity.ModelType });
            if (pQueryEntity.QuestionnaireType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QuestionnaireType", Value = pQueryEntity.QuestionnaireType });
            if (pQueryEntity.QRegular!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QRegular", Value = pQueryEntity.QRegular });
            if (pQueryEntity.IsShowQRegular!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsShowQRegular", Value = pQueryEntity.IsShowQRegular });
            if (pQueryEntity.ButtonName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ButtonName", Value = pQueryEntity.ButtonName });
            if (pQueryEntity.BGImageSrc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BGImageSrc", Value = pQueryEntity.BGImageSrc });
            if (pQueryEntity.StartPageBtnBGColor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartPageBtnBGColor", Value = pQueryEntity.StartPageBtnBGColor });
            if (pQueryEntity.StartPageBtnTextColor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StartPageBtnTextColor", Value = pQueryEntity.StartPageBtnTextColor });
            if (pQueryEntity.QResultTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QResultTitle", Value = pQueryEntity.QResultTitle });
            if (pQueryEntity.QResultBGImg!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QResultBGImg", Value = pQueryEntity.QResultBGImg });
            if (pQueryEntity.QResultImg!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QResultImg", Value = pQueryEntity.QResultImg });
            if (pQueryEntity.QResultBGColor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QResultBGColor", Value = pQueryEntity.QResultBGColor });
            if (pQueryEntity.QResultBtnTextColor!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "QResultBtnTextColor", Value = pQueryEntity.QResultBtnTextColor });
            if (pQueryEntity.Sort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sort", Value = pQueryEntity.Sort });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_QN_QuestionnaireEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_QN_QuestionnaireEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["QuestionnaireID"] != DBNull.Value)
			{
				pInstance.QuestionnaireID =  (Guid)pReader["QuestionnaireID"];
			}
			if (pReader["QuestionnaireName"] != DBNull.Value)
			{
				pInstance.QuestionnaireName =  Convert.ToString(pReader["QuestionnaireName"]);
			}
			if (pReader["ModelType"] != DBNull.Value)
			{
				pInstance.ModelType =   Convert.ToInt32(pReader["ModelType"]);
			}
			if (pReader["QuestionnaireType"] != DBNull.Value)
			{
				pInstance.QuestionnaireType =   Convert.ToInt32(pReader["QuestionnaireType"]);
			}
			if (pReader["QRegular"] != DBNull.Value)
			{
				pInstance.QRegular =  Convert.ToString(pReader["QRegular"]);
			}
			if (pReader["IsShowQRegular"] != DBNull.Value)
			{
				pInstance.IsShowQRegular =   Convert.ToInt32(pReader["IsShowQRegular"]);
			}
			if (pReader["ButtonName"] != DBNull.Value)
			{
				pInstance.ButtonName =  Convert.ToString(pReader["ButtonName"]);
			}
			if (pReader["BGImageSrc"] != DBNull.Value)
			{
				pInstance.BGImageSrc =  Convert.ToString(pReader["BGImageSrc"]);
			}
			if (pReader["StartPageBtnBGColor"] != DBNull.Value)
			{
				pInstance.StartPageBtnBGColor =  Convert.ToString(pReader["StartPageBtnBGColor"]);
			}
			if (pReader["StartPageBtnTextColor"] != DBNull.Value)
			{
				pInstance.StartPageBtnTextColor =  Convert.ToString(pReader["StartPageBtnTextColor"]);
			}
			if (pReader["QResultTitle"] != DBNull.Value)
			{
				pInstance.QResultTitle =  Convert.ToString(pReader["QResultTitle"]);
			}
			if (pReader["QResultBGImg"] != DBNull.Value)
			{
				pInstance.QResultBGImg =  Convert.ToString(pReader["QResultBGImg"]);
			}
			if (pReader["QResultImg"] != DBNull.Value)
			{
				pInstance.QResultImg =  Convert.ToString(pReader["QResultImg"]);
			}
			if (pReader["QResultBGColor"] != DBNull.Value)
			{
				pInstance.QResultBGColor =  Convert.ToString(pReader["QResultBGColor"]);
			}
			if (pReader["QResultBtnTextColor"] != DBNull.Value)
			{
				pInstance.QResultBtnTextColor =  Convert.ToString(pReader["QResultBtnTextColor"]);
			}
			if (pReader["Sort"] != DBNull.Value)
			{
				pInstance.Sort =   Convert.ToInt32(pReader["Sort"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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

        }
        #endregion
    }
}
