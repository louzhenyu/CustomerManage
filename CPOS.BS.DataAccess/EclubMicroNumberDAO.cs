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
    /// ��EclubMicroNumber�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubMicroNumberDAO : Base.BaseCPOSDAO, ICRUDable<EclubMicroNumberEntity>, IQueryable<EclubMicroNumberEntity>
    {
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="microNumEn">����ʵ��</param>
        /// <returns></returns>
        public DataSet GetMicroNums(EclubMicroNumberEntity microNumEn)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MicroNumberID,MicroNumber from EclubMicroNumber ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' ", CurrentUserInfo.ClientID);
            sbSQL.Append("Order by MicroNumberNo DESC ;");

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="number">�ڿ���</param>
        /// <param name="keyword">�ؼ���</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns>���ݼ�</returns>
        public DataSet GetNumberList(string number, string keyword, string sortField, int sortOrder, int pageIndex, int pageSize)
        {
            //Instance Obj
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbCond = new StringBuilder();

            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (!string.IsNullOrEmpty(number))
            {
                sbCond.AppendFormat("and MicroNumberID = '{0}' ", number);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sbCond.AppendFormat("and MicroNumberName like '%{0}%' ", keyword);
            }
            //Build SQL Text
            sbSQL.Append("select NumberId, NumberName, MicroNumberNo, Number, CoverPath, Intro, Description, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete,'http://www.ckgsb.edu.cn/emba/pages/index/133' NumberUrl from(");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by {0} {1}) rowNum, MicroNumberID NumberId, MicroNumberName NumberName, MicroNumberNo, MicroNumber Number, CoverPath, Intro, Description, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete from dbo.EclubMicroNumber  ", sortField, sort);
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' {1} ", CurrentUserInfo.ClientID, sbCond.ToString());
            sbSQL.Append(") as Res ");
            sbSQL.AppendFormat("where rowNum between {0} and {1}  ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.AppendFormat("select COUNT(MicroNumberID) from EclubMicroNumber ");
            sbSQL.AppendFormat("where IsDelete = 0 and CustomerId = '{0}' {1} ", CurrentUserInfo.ClientID, sbCond.ToString());

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

    }
}
