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
    /// ���ݷ��ʣ�  
    /// ��VwUnitProperty�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VwUnitPropertyDAO : Base.BaseCPOSDAO, ICRUDable<VwUnitPropertyEntity>, IQueryable<VwUnitPropertyEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VwUnitPropertyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VwUnitPropertyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VwUnitPropertyEntity pEntity, IDbTransaction pTran)
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

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.UnitId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VwUnitPropertyEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VwUnitProperty] where UnitId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            VwUnitPropertyEntity m = null;
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
        public VwUnitPropertyEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VwUnitProperty] where isdelete=0");
            //��ȡ����
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
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VwUnitPropertyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VwUnitPropertyEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
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
        public void Update(VwUnitPropertyEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VwUnitPropertyEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VwUnitPropertyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VwUnitPropertyEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UnitId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.UnitId, pTran);           
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
            sql.AppendLine("update [VwUnitProperty] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where UnitId=@UnitId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@UnitId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VwUnitPropertyEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.UnitId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.UnitId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VwUnitPropertyEntity[] pEntities)
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
            sql.AppendLine("update [VwUnitProperty] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where UnitId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VwUnitPropertyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
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
            //ִ��SQL
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
        public PagedQueryResult<VwUnitPropertyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [UnitId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwUnitProperty] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VwUnitProperty] where isdelete=0 ");
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
        public VwUnitPropertyEntity[] QueryByEntity(VwUnitPropertyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VwUnitPropertyEntity> PagedQueryByEntity(VwUnitPropertyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VwUnitPropertyEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
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
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out VwUnitPropertyEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
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
