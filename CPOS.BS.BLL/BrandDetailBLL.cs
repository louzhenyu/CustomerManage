/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/29 16:01:06
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class BrandDetailBLL
    {
        #region ��ȡͬ������Ʒ��

        /// <summary>
        /// ��ȡͬ������Ʒ��
        /// </summary>
        /// <param name="latestTime">���ͬ��ʱ��</param>
        /// <returns></returns>
        public DataSet GetSynWelfareBrandList(string latestTime)
        {
            return this._currentDAO.GetSynWelfareBrandList(latestTime);
        }
        #endregion

        #region Ʒ�Ʊ�ͬ��
        /// <summary>
        /// Ʒ����Ϣͬ��
        /// </summary>
        /// <param name="BrandId">Ʒ��Ψһ��</param>
        /// <param name="BrandName">Ʒ������</param>
        /// <param name="DataFrom">��Դ1=brandDetail��2=prop</param>
        /// <param name="IsDelete">�Ƿ�ɾ��1=�ǣ�0=��</param>
        /// <returns></returns>
        public bool SetBrandAndPropSyn(string BrandId, string BrandName, int DataFrom, int IsDelete,out string strError)
        {
            bool bReturn = true;
            try
            {
                #region
                if (BrandId == null || BrandId.Trim().Equals(""))
                {
                    strError = "Ʒ��Ψһ��Ϊ��";
                    return false;
                }
                if (BrandName == null || BrandName.Trim().Equals(""))
                {
                    strError = "Ʒ������Ϊ��";
                    return false;
                }
                #endregion
                #region �޸�����
                if (DataFrom == 1)
                {
                    PropService propService = new PropService(this.CurrentUserInfo);
                    PropInfo propInfo = new PropInfo();
                    propInfo = propService.GetPropInfoById(BrandId);
                    if (propInfo == null || propInfo.Prop_Id == null || propInfo.Prop_Id.Equals(""))
                    {
                        propInfo.Prop_Id = BrandId;
                        propInfo.Prop_Code = BrandName;
                        propInfo.Prop_Name = BrandName;
                        propInfo.Prop_Eng_Name = BrandName;
                        propInfo.Prop_Type = "3";
                        propInfo.Parent_Prop_id = "F8823C2EBACF4965BA134D3B10BD0B9F";
                        propInfo.Prop_Level = 3;
                        propInfo.Prop_Domain = "ITEM";
                        propInfo.Prop_Status = 1;
                        propInfo.Display_Index = 10;
                        propInfo.Create_Time = BaseService.NewGuidPub();
                        propInfo.Create_User_Id = this.CurrentUserInfo.CurrentUser.User_Id;
                        propInfo.Modify_Time = BaseService.NewGuidPub();
                        propInfo.Modify_User_Id = this.CurrentUserInfo.CurrentUser.User_Id;
                        strError = "�½�����Ʒ����Ϣ����";
                        bReturn = propService.SaveProp(propInfo,ref strError);
                    }
                    else {
                        propInfo.Prop_Code = BrandName;
                        propInfo.Prop_Name = BrandName;
                        propInfo.Prop_Eng_Name = BrandName;
                        propInfo.Modify_Time = BaseService.NewGuidPub();
                        propInfo.Modify_User_Id = this.CurrentUserInfo.CurrentUser.User_Id;
                        propInfo.Prop_Status = 1;
                        bReturn = propService.UpdateProp(propInfo);
                    }
                }
                #endregion

                #region �޸�Ʒ��
                if (DataFrom == 2)
                {
                    BrandDetailEntity brandInfo = new BrandDetailEntity();
                    brandInfo = GetByID(BrandId);
                    if (brandInfo == null || brandInfo.BrandId == null || brandInfo.BrandId.Trim().Equals(""))
                    {
                        BrandDetailEntity brandInfo1 = new BrandDetailEntity();
                        brandInfo1.BrandId = BrandId;
                        brandInfo1.BrandCode = BrandName;
                        brandInfo1.BrandName = BrandName;
                        brandInfo1.BrandEngName = BrandName;
                        brandInfo1.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                        Create(brandInfo1);
                    }
                    else {
                        brandInfo.BrandCode = BrandName;
                        brandInfo.BrandName = BrandName;
                        brandInfo.BrandEngName = BrandName;
                        brandInfo.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                        Update(brandInfo, false);
                    }
                }
                #endregion

                strError = "�ɹ�";
                return bReturn;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region Web�б��ȡ
        /// <summary>
        /// Web�б��ȡ
        /// </summary>
        public IList<BrandDetailEntity> GetWebBrandDetail(BrandDetailEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<BrandDetailEntity> list = new List<BrandDetailEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebBrandDetail(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<BrandDetailEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetWebBrandDetailCount(BrandDetailEntity entity)
        {
            return _currentDAO.GetWebBrandDetailCount(entity);
        }
        #endregion

    }
}