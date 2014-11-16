/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 17:25:36
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
    /// ��vwItemPEventDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class vwItemPEventDetailDAO : Base.BaseCPOSDAO, ICRUDable<vwItemPEventDetailEntity>, IQueryable<vwItemPEventDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public vwItemPEventDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(vwItemPEventDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(vwItemPEventDetailEntity pEntity, IDbTransaction pTran)
        {
            throw new Exception("��ͼʵ���޷�ִ����������");
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public vwItemPEventDetailEntity GetByID(object pID)
        {
            throw new Exception("��ͼʵ�岻�ṩ������ѯ");
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public vwItemPEventDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwItemPEventDetail] where 1=1 ");
            //��ȡ����
            List<vwItemPEventDetailEntity> list = new List<vwItemPEventDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    vwItemPEventDetailEntity m;
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
        public void Update(vwItemPEventDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(vwItemPEventDetailEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            throw new Exception("��ͼʵ���޷�ִ�и��²���");
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(vwItemPEventDetailEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(vwItemPEventDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(vwItemPEventDetailEntity pEntity, IDbTransaction pTran)
        {
            throw new Exception("��ͼʵ���޷�ִ��ɾ������");          
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            throw new Exception("��ͼʵ���޷�ִ��ɾ������");
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(vwItemPEventDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.ItemID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.ItemID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(vwItemPEventDetailEntity[] pEntities)
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
            throw new Exception("��ͼʵ���޷�ִ��ɾ������");
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public vwItemPEventDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwItemPEventDetail] where 1=1  ");
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
            List<vwItemPEventDetailEntity> list = new List<vwItemPEventDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    vwItemPEventDetailEntity m;
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
        public PagedQueryResult<vwItemPEventDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ItemID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [vwItemPEventDetail] where 1=1  ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [vwItemPEventDetail] where 1=1  ");
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
            PagedQueryResult<vwItemPEventDetailEntity> result = new PagedQueryResult<vwItemPEventDetailEntity>();
            List<vwItemPEventDetailEntity> list = new List<vwItemPEventDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    vwItemPEventDetailEntity m;
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
        public vwItemPEventDetailEntity[] QueryByEntity(vwItemPEventDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<vwItemPEventDetailEntity> PagedQueryByEntity(vwItemPEventDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(vwItemPEventDetailEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.ItemCategoryID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemCategoryID", Value = pQueryEntity.ItemCategoryID });
            if (pQueryEntity.ItemCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemCode", Value = pQueryEntity.ItemCode });
            if (pQueryEntity.ItemName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemName", Value = pQueryEntity.ItemName });
            if (pQueryEntity.ItemNameEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemNameEn", Value = pQueryEntity.ItemNameEn });
            if (pQueryEntity.ItemNameShort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemNameShort", Value = pQueryEntity.ItemNameShort });
            if (pQueryEntity.Pyzjm!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Pyzjm", Value = pQueryEntity.Pyzjm });
            if (pQueryEntity.ItemRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemRemark", Value = pQueryEntity.ItemRemark });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.Tel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Tel", Value = pQueryEntity.Tel });
            if (pQueryEntity.ItemUnit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemUnit", Value = pQueryEntity.ItemUnit });
            if (pQueryEntity.EventId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventId", Value = pQueryEntity.EventId });
            if (pQueryEntity.EventTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EventTypeID", Value = pQueryEntity.EventTypeID });
            if (pQueryEntity.Qty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Qty", Value = pQueryEntity.Qty });
            if (pQueryEntity.RemainingQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemainingQty", Value = pQueryEntity.RemainingQty });
            if (pQueryEntity.BeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginTime", Value = pQueryEntity.BeginTime });
            if (pQueryEntity.EndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndTime", Value = pQueryEntity.EndTime });
            if (pQueryEntity.RemainingSec!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemainingSec", Value = pQueryEntity.RemainingSec });
            if (pQueryEntity.UseInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UseInfo", Value = pQueryEntity.UseInfo });
            if (pQueryEntity.BuyType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyType", Value = pQueryEntity.BuyType });
            if (pQueryEntity.OffersTips!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OffersTips", Value = pQueryEntity.OffersTips });
            if (pQueryEntity.IsOnline!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsOnline", Value = pQueryEntity.IsOnline });
            if (pQueryEntity.Price!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Price", Value = pQueryEntity.Price });
            if (pQueryEntity.SalesPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesPrice", Value = pQueryEntity.SalesPrice });
            if (pQueryEntity.DiscountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });
            if (pQueryEntity.IsFirst!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsFirst", Value = pQueryEntity.IsFirst });
            if (pQueryEntity.StopReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StopReason", Value = pQueryEntity.StopReason });
            if (pQueryEntity.SalesPersonCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesPersonCount", Value = pQueryEntity.SalesPersonCount });
            if (pQueryEntity.DownloadPersonCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DownloadPersonCount", Value = pQueryEntity.DownloadPersonCount });
            if (pQueryEntity.BrandID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BrandID", Value = pQueryEntity.BrandID });
            if (pQueryEntity.IsIAlumniItem!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsIAlumniItem", Value = pQueryEntity.IsIAlumniItem });
            if (pQueryEntity.IsExchange!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsExchange", Value = pQueryEntity.IsExchange });
            if (pQueryEntity.IntegralExchange!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IntegralExchange", Value = pQueryEntity.IntegralExchange });
            if (pQueryEntity.MonthSalesCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MonthSalesCount", Value = pQueryEntity.MonthSalesCount });
            if (pQueryEntity.ItemCategoryName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemCategoryName", Value = pQueryEntity.ItemCategoryName });
            if (pQueryEntity.SkuId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SkuId", Value = pQueryEntity.SkuId });
            if (pQueryEntity.Prop1Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Prop1Name", Value = pQueryEntity.Prop1Name });
            if (pQueryEntity.Prop2Name!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Prop2Name", Value = pQueryEntity.Prop2Name });
            if (pQueryEntity.ItemDisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemDisplayIndex", Value = pQueryEntity.ItemDisplayIndex });
            if (pQueryEntity.ItemTypeDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemTypeDesc", Value = pQueryEntity.ItemTypeDesc });
            if (pQueryEntity.ItemSortDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemSortDesc", Value = pQueryEntity.ItemSortDesc });
            if (pQueryEntity.SalesQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesQty", Value = pQueryEntity.SalesQty });
            if (pQueryEntity.Forpoints!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Forpoints", Value = pQueryEntity.Forpoints });
            if (pQueryEntity.ItemIntroduce!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemIntroduce", Value = pQueryEntity.ItemIntroduce });
            if (pQueryEntity.ItemParaIntroduce!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemParaIntroduce", Value = pQueryEntity.ItemParaIntroduce });
            if (pQueryEntity.ScanCodeIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ScanCodeIntegral", Value = pQueryEntity.ScanCodeIntegral });
            if (pQueryEntity.EdProp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EdProp", Value = pQueryEntity.EdProp });
            if (pQueryEntity.FactoryName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FactoryName", Value = pQueryEntity.FactoryName });
            if (pQueryEntity.GG!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GG", Value = pQueryEntity.GG });
            if (pQueryEntity.Degree!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Degree", Value = pQueryEntity.Degree });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        public void Load(IDataReader pReader, out vwItemPEventDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new vwItemPEventDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["ItemCategoryID"] != DBNull.Value)
			{
				pInstance.ItemCategoryID =  Convert.ToString(pReader["ItemCategoryID"]);
			}
			if (pReader["ItemCode"] != DBNull.Value)
			{
				pInstance.ItemCode =  Convert.ToString(pReader["ItemCode"]);
			}
			if (pReader["ItemName"] != DBNull.Value)
			{
				pInstance.ItemName =  Convert.ToString(pReader["ItemName"]);
			}
			if (pReader["ItemNameEn"] != DBNull.Value)
			{
				pInstance.ItemNameEn =  Convert.ToString(pReader["ItemNameEn"]);
			}
			if (pReader["ItemNameShort"] != DBNull.Value)
			{
				pInstance.ItemNameShort =  Convert.ToString(pReader["ItemNameShort"]);
			}
			if (pReader["Pyzjm"] != DBNull.Value)
			{
				pInstance.Pyzjm =  Convert.ToString(pReader["Pyzjm"]);
			}
			if (pReader["ItemRemark"] != DBNull.Value)
			{
				pInstance.ItemRemark =  Convert.ToString(pReader["ItemRemark"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToString(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToString(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["Tel"] != DBNull.Value)
			{
				pInstance.Tel =  Convert.ToString(pReader["Tel"]);
			}
			if (pReader["ItemUnit"] != DBNull.Value)
			{
				pInstance.ItemUnit =  Convert.ToString(pReader["ItemUnit"]);
			}
			if (pReader["EventId"] != DBNull.Value)
			{
				pInstance.EventId =  (Guid)pReader["EventId"];
			}
			if (pReader["EventTypeID"] != DBNull.Value)
			{
				pInstance.EventTypeID =   Convert.ToInt32(pReader["EventTypeID"]);
			}
			if (pReader["Qty"] != DBNull.Value)
			{
				pInstance.Qty =   Convert.ToInt32(pReader["Qty"]);
			}
			if (pReader["RemainingQty"] != DBNull.Value)
			{
				pInstance.RemainingQty =   Convert.ToInt32(pReader["RemainingQty"]);
			}
			if (pReader["BeginTime"] != DBNull.Value)
			{
				pInstance.BeginTime =  Convert.ToDateTime(pReader["BeginTime"]);
			}
			if (pReader["EndTime"] != DBNull.Value)
			{
				pInstance.EndTime =  Convert.ToDateTime(pReader["EndTime"]);
            }
            if (pReader["AddedTime"] != DBNull.Value)
            {
                pInstance.AddTime = Convert.ToDateTime(pReader["AddedTime"]);
            }
            if (pReader["Status"] != DBNull.Value)
            {
                pInstance.Status = Convert.ToInt32(pReader["Status"]);
            }
			if (pReader["RemainingSec"] != DBNull.Value)
			{
				pInstance.RemainingSec =   Convert.ToInt32(pReader["RemainingSec"]);
			}
			if (pReader["UseInfo"] != DBNull.Value)
			{
				pInstance.UseInfo =  Convert.ToString(pReader["UseInfo"]);
			}
			if (pReader["BuyType"] != DBNull.Value)
			{
				pInstance.BuyType =  Convert.ToString(pReader["BuyType"]);
			}
			if (pReader["OffersTips"] != DBNull.Value)
			{
				pInstance.OffersTips =  Convert.ToString(pReader["OffersTips"]);
			}
			if (pReader["IsOnline"] != DBNull.Value)
			{
				pInstance.IsOnline =  Convert.ToString(pReader["IsOnline"]);
			}
			if (pReader["Price"] != DBNull.Value)
			{
				pInstance.Price =  Convert.ToDecimal(pReader["Price"]);
			}
			if (pReader["SalesPrice"] != DBNull.Value)
			{
				pInstance.SalesPrice =  Convert.ToDecimal(pReader["SalesPrice"]);
			}
			if (pReader["DiscountRate"] != DBNull.Value)
			{
				pInstance.DiscountRate =  Convert.ToDecimal(pReader["DiscountRate"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}
			if (pReader["IsFirst"] != DBNull.Value)
			{
				pInstance.IsFirst =   Convert.ToInt32(pReader["IsFirst"]);
			}
			if (pReader["StopReason"] != DBNull.Value)
			{
				pInstance.StopReason =  Convert.ToString(pReader["StopReason"]);
			}
			if (pReader["SalesPersonCount"] != DBNull.Value)
			{
				pInstance.SalesPersonCount =   Convert.ToInt32(pReader["SalesPersonCount"]);
			}
			if (pReader["DownloadPersonCount"] != DBNull.Value)
			{
				pInstance.DownloadPersonCount =   Convert.ToInt32(pReader["DownloadPersonCount"]);
			}
			if (pReader["BrandID"] != DBNull.Value)
			{
				pInstance.BrandID =  Convert.ToString(pReader["BrandID"]);
			}
			if (pReader["IsIAlumniItem"] != DBNull.Value)
			{
				pInstance.IsIAlumniItem =  Convert.ToString(pReader["IsIAlumniItem"]);
			}
			if (pReader["IsExchange"] != DBNull.Value)
			{
				pInstance.IsExchange =  Convert.ToString(pReader["IsExchange"]);
			}
			if (pReader["IntegralExchange"] != DBNull.Value)
			{
				pInstance.IntegralExchange =  Convert.ToString(pReader["IntegralExchange"]);
			}
			if (pReader["MonthSalesCount"] != DBNull.Value)
			{
				pInstance.MonthSalesCount =  Convert.ToDecimal(pReader["MonthSalesCount"]);
			}
			if (pReader["ItemCategoryName"] != DBNull.Value)
			{
				pInstance.ItemCategoryName =  Convert.ToString(pReader["ItemCategoryName"]);
			}
			if (pReader["SkuId"] != DBNull.Value)
			{
				pInstance.SkuId =  Convert.ToString(pReader["SkuId"]);
			}
			if (pReader["Prop1Name"] != DBNull.Value)
			{
				pInstance.Prop1Name =  Convert.ToString(pReader["Prop1Name"]);
			}
			if (pReader["Prop2Name"] != DBNull.Value)
			{
				pInstance.Prop2Name =  Convert.ToString(pReader["Prop2Name"]);
			}
			if (pReader["ItemDisplayIndex"] != DBNull.Value)
			{
				pInstance.ItemDisplayIndex =  Convert.ToString(pReader["ItemDisplayIndex"]);
			}
			if (pReader["ItemTypeDesc"] != DBNull.Value)
			{
				pInstance.ItemTypeDesc =  Convert.ToString(pReader["ItemTypeDesc"]);
			}
			if (pReader["ItemSortDesc"] != DBNull.Value)
			{
				pInstance.ItemSortDesc =  Convert.ToString(pReader["ItemSortDesc"]);
			}
			if (pReader["SalesQty"] != DBNull.Value)
			{
				pInstance.SalesQty =   Convert.ToInt32(pReader["SalesQty"]);
			}
			if (pReader["Forpoints"] != DBNull.Value)
			{
				pInstance.Forpoints =  Convert.ToString(pReader["Forpoints"]);
			}
			if (pReader["ItemIntroduce"] != DBNull.Value)
			{
				pInstance.ItemIntroduce =  Convert.ToString(pReader["ItemIntroduce"]);
			}
			if (pReader["ItemParaIntroduce"] != DBNull.Value)
			{
				pInstance.ItemParaIntroduce =  Convert.ToString(pReader["ItemParaIntroduce"]);
			}
			if (pReader["ScanCodeIntegral"] != DBNull.Value)
			{
				pInstance.ScanCodeIntegral =  Convert.ToString(pReader["ScanCodeIntegral"]);
			}
			if (pReader["EdProp"] != DBNull.Value)
			{
				pInstance.EdProp =  Convert.ToString(pReader["EdProp"]);
			}
			if (pReader["FactoryName"] != DBNull.Value)
			{
				pInstance.FactoryName =  Convert.ToString(pReader["FactoryName"]);
			}
			if (pReader["GG"] != DBNull.Value)
			{
				pInstance.GG =  Convert.ToString(pReader["GG"]);
			}
			if (pReader["Degree"] != DBNull.Value)
			{
				pInstance.Degree =  Convert.ToString(pReader["Degree"]);
			}

        }
        #endregion
    }
}
