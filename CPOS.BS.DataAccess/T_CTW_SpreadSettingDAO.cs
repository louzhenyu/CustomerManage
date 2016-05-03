/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/18 14:58:43
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
    /// ���ݷ��ʣ� Share ����   Focus ��ע   Reg ע�� 
    /// ��T_CTW_SpreadSetting�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_CTW_SpreadSettingDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_SpreadSettingEntity>, IQueryable<T_CTW_SpreadSettingEntity>
    {
        public void DeleteByCTWEventID(string strCTWEventId)
        {
            string strSql = string.Format("DELETE [dbo].[T_CTW_SpreadSetting] WHERE CTWEventId='{0}'", strCTWEventId);
            this.SQLHelper.ExecuteNonQuery(strSql);
        }
        public DataSet GetSpreadSettingByCTWEventId(string strCTWEventId)
        {
            string strSql = string.Format(@"    SELECT a.*,b.ImageURL BGImageUrl ,c.ImageURL LeadPageQRCodeImageUrl
                                                FROM [dbo].[T_CTW_SpreadSetting] a 
                                                LEFT JOIN dbo.ObjectImages b ON a.ImageId=b.ImageId
                                                LEFT JOIN dbo.WQRCodeManager c ON a.LeadPageQRCodeImageId=CAST(c.QRCodeId AS NVARCHAR(50))
                                                WHERE CTWEventId='{0}'", strCTWEventId);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
        public DataSet GetSpreadSettingQRImageByCTWEventId(string strCTWEventId, string strSpreadType)
        {
            string strSql = string.Format(@"    SELECT a.*,b.ImageURL BGImageUrl ,c.ImageURL LeadPageQRCodeImageUrl,d.OnLineRedirectUrl
                                                FROM [dbo].[T_CTW_SpreadSetting] a 
                                                LEFT JOIN dbo.ObjectImages b ON a.ImageId=b.ImageId
                                                LEFT JOIN dbo.WQRCodeManager c ON a.LeadPageQRCodeImageId=CAST(c.QRCodeId AS NVARCHAR(50))
                                                INNER JOIN dbo.T_CTW_LEvent d ON a.CTWEventId=d.CTWEventId
                                                
                                                WHERE SpreadType ='{1}' and a.CTWEventId='{0}'", strCTWEventId, strSpreadType);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
    }
}
