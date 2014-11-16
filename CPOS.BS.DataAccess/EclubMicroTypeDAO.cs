/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
    /// ��EclubMicroType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubMicroTypeDAO : Base.BaseCPOSDAO, ICRUDable<EclubMicroTypeEntity>, IQueryable<EclubMicroTypeEntity>
    {
        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <param name="typeEn">ʵ��</param>
        /// <returns></returns>
        public DataSet GetMicroTypes(EclubMicroTypeEntity typeEn)
        {
            //Builde SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MicroTypeID,MicroTypeName,ParentID,TypeLevel from EclubMicroType ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(typeEn.ParentID))
            {
                sbSQL.AppendFormat("ParentID = '{0}' ", typeEn.ParentID);
            }
            sbSQL.Append("Order By Sequence ;");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <returns></returns>
        public DataSet GetMicroTypeList(int sortOrder, string sortField, int pageIndex, int pageSize)
        {
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            //Builde SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MicroTypeID,MicroTypeName,ParentID,TypeLevel,IconPath,Intro,Description,Style,ParentName,CreateTime from( ");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by C.{0} {1}) rowNum,C.MicroTypeID,C.MicroTypeName,C.ParentID,C.TypeLevel,C.IconPath,C.Intro,C.Description,C.Style,P.MicroTypeName as ParentName,C.CreateTime from EclubMicroType C ", sortField, sort);
            sbSQL.Append("left join EclubMicroType P on P.IsDelete=0 and P.CustomerId=C.CustomerId and P.MicroTypeID=C.ParentID ");
            sbSQL.AppendFormat("where C.IsDelete = 0 and C.CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.Append(")as res ");
            sbSQL.AppendFormat("where rowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.Append("select COUNT(MicroTypeID) from EclubMicroType ");
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{0}' ", CurrentUserInfo.ClientID);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// ��ȡ�����ϸ������������Ϣ
        /// </summary>
        /// <param name="typeEn">���ʵ����Ϣ</param>
        /// <returns></returns>
        public DataSet GetMicroTypeDtail(EclubMicroTypeEntity typeEn)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select C.MicroTypeID, C.MicroTypeName, C.ParentID, C.TypeLevel, C.IconPath, C.Intro, C.Description, C.Sequence, C.Style, C.CustomerId, C.CreateBy, C.CreateTime, C.LastUpdateBy, C.LastUpdateTime, C.IsDelete,P.MicroTypeName ParentTypeName from EclubMicroType C ");
            sbSQL.Append("left join EclubMicroType P on P.IsDelete=0 and P.CustomerId=C.CustomerId and P.MicroTypeID=C.ParentID ");
            sbSQL.AppendFormat("where C.IsDelete=0 and C.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and C.MicroTypeID='{0}' ;", typeEn.MicroTypeID);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// ����������������ȡ�ѹ����İ���б�
        /// by yehua
        /// </summary>
        public DataTable MicroIssueTypeGet(string numberId, string parentId, string typeLevel)
        {
            //Builde SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT MicroTypeID,MicroTypeName,ParentID,IconPath,TM.Style,Intro FROM LNumberTypeMapping TM ");
            sbSQL.AppendFormat("INNER JOIN EclubMicroType T ON TM.TypeId = T.MicroTypeID WHERE TM.NumberId = '{0}' AND TM.CustomerId = '{1}' ", numberId, CurrentUserInfo.ClientID);
            if (!string.IsNullOrEmpty(parentId))
            {
                sbSQL.AppendFormat("AND ParentID = '{0}' ", parentId);
            }
            if (!string.IsNullOrEmpty(typeLevel))
            {
                sbSQL.AppendFormat("AND TypeLevel = '{0}' ", typeLevel);
            }
            sbSQL.Append("Order By TM.Sequence;");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString()).Tables[0];
        }
    }
}
