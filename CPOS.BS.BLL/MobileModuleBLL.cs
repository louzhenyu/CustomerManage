/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/18 14:05:11
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
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MobileModuleBLL
    {
        /// <summary>
        /// 获取客户订单的表单列表
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public DataTable GetFormsTable(string clientID, int type, int page, int pageSize, out int totalRow)
        {
            return _currentDAO.GetFormsTable(clientID, type, page, pageSize, out totalRow);
        }

        /// <summary>
        /// 获取客户(VIP注册)属性列表
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public DataTable GetClientBunessDefined(string clientID, int page, int pageSize, out int totalRow)
        {
            return _currentDAO.GetClientBunessDefined(clientID, page, pageSize, out totalRow);
        }

        public void CreateWithMobilePageBlock(MobileModuleEntity entity)
        {
            _currentDAO.CreateWithMobilePageBlock(entity);
        }

        /// <summary>
        /// 获取活动(SignUp活动)属性列表
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public DataTable GetLeventSignUpAttri(string clientID, int page, int pageSize, out int totalRow)
        {
            return _currentDAO.GetLeventSignUpAttri(clientID, page, pageSize, out totalRow);
        }

        public string CreateAndReturnID(string name)
        {
            string result = "";

            MobileModuleEntity mobileModuleEntity = new MobileModuleEntity();
            mobileModuleEntity.ModuleName = name;
            mobileModuleEntity.CustomerID = CurrentUserInfo.ClientID;
            _currentDAO.Create(mobileModuleEntity);

            var entity = _currentDAO.QueryByEntity(mobileModuleEntity, new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } });

            if (entity.Length > 0)
                result = entity[0].MobileModuleID.ToString();

            return result;
        }

        public DynamicVipFormLoadRD DynamicVipFormLoad(DynamicVipFormIDRP dynamicVipFormLoadRP)
        {
            DynamicVipFormLoadRD dynamicVipFormLoadRD = new DynamicVipFormLoadRD();
            
            DataSet formDetail = _currentDAO.DynamicVipFormLoad(dynamicVipFormLoadRP.FormID);
            if (Utils.IsDataSetValid(formDetail))
            {
                dynamicVipFormLoadRD.FormName = formDetail.Tables[0].Rows[0][0].ToString();
                dynamicVipFormLoadRD.UsedFieldList = (from f in formDetail.Tables[1].AsEnumerable()
                                                        where f["IsUsed"].ToString() == "1"
                                                        select new Field()
                                                        {
                                                            PublicControlID = f["ClientBussinessDefinedID"].ToString(),
                                                            ColumnDesc = f["ColumnDesc"].ToString(),
                                                            ControlType = int.Parse(f["ControlType"].ToString()),
                                                            DisplayType = int.Parse(f["DisplayType"].ToString()),
                                                            EditOrder = int.Parse(f["EditOrder"].ToString()),
                                                            IsMustDo = int.Parse(f["IsMustDo"].ToString()),
                                                            FormControlID = f["MobileBussinessDefinedID"].ToString()
                                                        }).ToArray();

                dynamicVipFormLoadRD.AllFieldList = (from f in formDetail.Tables[1].AsEnumerable()
                                                      select new Field()
                                                      {
                                                          PublicControlID = f["ClientBussinessDefinedID"].ToString(),
                                                          ColumnDesc = f["ColumnDesc"].ToString(),
                                                          ControlType = int.Parse(f["ControlType"].ToString()),
                                                          DisplayType = int.Parse(f["DisplayType"].ToString()),
                                                          EditOrder = int.Parse(f["EditOrder"].ToString()),
                                                          IsMustDo = int.Parse(f["IsMustDo"].ToString()),
                                                          IsUsed = int.Parse(f["IsUsed"].ToString()),
                                                          FormControlID = f["MobileBussinessDefinedID"].ToString()
                                                      }).ToArray();

                dynamicVipFormLoadRD.SceneList = (from f in formDetail.Tables[2].AsEnumerable()
                                                  select new Scene()
                                                  {
                                                      SceneName = f["SceneName"].ToString(),
                                                      SceneValue = f["SceneValue"].ToString(),
                                                      IsSelected = int.Parse(f["IsSelected"].ToString())
                                                  }).ToArray();
            }

            return dynamicVipFormLoadRD;
        }

        public DataSet DynamicFormLoad(string formID, string tableName)
        {
            DataSet formDetail = new DataSet();

            formDetail = _currentDAO.DynamicFormLoad(formID, tableName);

            return formDetail;
        }

        public int DynamicVipFormSave(DynamicVipFormSaveRP dynamicVipFormSaveRP)
        {
            int result = 0;

            DataTable dataTable = new DataTable();
            dataTable = Utils.ToDataTable(dynamicVipFormSaveRP.FieldList);

            result = int.Parse(_currentDAO.DynamicVipFormSave(dynamicVipFormSaveRP.FormID, dataTable));

            return result;
        }

        public int DynamicFormSave(Field[] fieldList, string formID, string tableName, SqlTransaction tran = null)
        {
            int result = 0;

            DataTable dataTable = new DataTable();
            dataTable = Utils.ToDataTable(fieldList);

            result = int.Parse(_currentDAO.DynamicFormSave(formID, tableName, dataTable, tran));

            return result;
        }

        public EmptyRD DynamicVipFormRename(DynamicVipFormRenameRP rp)
        {
            var rd = new EmptyRD();

            MobileModuleEntity mme = new MobileModuleEntity();
            mme = GetByID(rp.FormID);

            if (mme != null)
            {
                mme.ModuleName = rp.Name;
                Update(mme);
            }

            return rd;
        }
        
        public EmptyRD DynamicVipFormDelete(DynamicVipFormIDRP dynamicVipFormIDRP)
        {
            var rd = new EmptyRD();

            Guid g = new Guid();
            Guid.TryParse(dynamicVipFormIDRP.FormID, out g);

            Delete(g, null);

            return rd;
        }

        public EmptyRD DynamicVipFormSceneSave(DynamicVipFormSceneSaveRP dynamicVipFormSceneSaveRP)
        {
            var rd = new EmptyRD();

            if (dynamicVipFormSceneSaveRP.SceneList.Length > 0)
            {
                MobileModuleObjectMappingBLL mmomBLL = new MobileModuleObjectMappingBLL(CurrentUserInfo);

                mmomBLL.DynamicVipFormSceneSave(dynamicVipFormSceneSaveRP);
            }

            return rd;
        }

        public DynamicVipPropertyListRD DynamicVipPropertyList(PaginationRP paginationRP)
        {
            DynamicVipPropertyListRD dynamicVipPropertyListRD = new DynamicVipPropertyListRD();

            DataSet propertyList = _currentDAO.DynamicVipFormLoad("");
            propertyList.Tables.RemoveAt(0);

            if (Utils.IsDataSetValid(propertyList))
            {
                dynamicVipPropertyListRD.PropertyList = (from f in propertyList.Tables[0].AsEnumerable()
                                                         select new Property()
                                                         {
                                                             PropertyID = f["ClientBussinessDefinedID"].ToString(),
                                                             PropertyName = f["ColumnDesc"].ToString(),
                                                             ControlType = f["ControlType"].ToString(),
                                                             DisplayType = f["DisplayType"].ToString(),
                                                             IsDefaultProp = f["IsDefaultProp"].ToString()
                                                         }).ToArray();

                dynamicVipPropertyListRD.TotalCount = propertyList.Tables[0].Rows.Count.ToString();
                dynamicVipPropertyListRD.TotalPage = Math.Round(double.Parse(((propertyList.Tables[0].Rows.Count / 50) + 1).ToString())).ToString();
                dynamicVipPropertyListRD.AvailableSlot = propertyList.Tables[2].Rows[0][0].ToString();
            }

            return dynamicVipPropertyListRD;
        }

        #region ResponseData

        public class FormListRD : IAPIResponseData
        {
            public Form[] FormList { get; set; }
            public int TotalCount { get; set; }
            public int TotalPage { get; set; }
        }

        public class Form
        {
            public string FormID { get; set; }
            public string FormName { get; set; }
            public string CreatedDate { get; set; }
        }

        public class DynamicVipFormCreateRD : IAPIResponseData
        {
            public string FormID { get; set; }
        }

        public class DynamicVipFormLoadRD : IAPIResponseData
        {
            public string FormName { get; set; }
            public Field[] UsedFieldList { get; set; }
            public Field[] AllFieldList { get; set; }
            public Scene[] SceneList { get; set; }
        }

        public class Field
        {
            public string PublicControlID { get; set; }
            public string FormControlID { get; set; }
            public string ColumnDesc { get; set; }
            public int ControlType { get; set; }
            public int DisplayType { get; set; }
            public int IsMustDo { get; set; }
            public int EditOrder { get; set; }
            public int IsUsed { get; set; }
            public string Hierarchy { get; set; }
        }

        public class Scene
        {
            public string SceneValue { get; set; }
            public string SceneName { get; set; }
            public int IsSelected { get; set; }
        }

        public class DynamicVipPropertyListRD : IAPIResponseData
        {
            public Property[] PropertyList { get; set; }
            public string TotalCount { get; set; }
            public string TotalPage { get; set; }
            public string AvailableSlot { get; set; }
        }

        public class Property
        {
            public string PropertyID { get; set; }
            public string PropertyName { get; set; }
            public string ControlType { get; set; }
            public string DisplayType { get; set; }
            public string IsDefaultProp { get; set; }
            public string HierarchyID { get; set; }
        }

        #endregion

        #region RequestParameter
        public class PaginationRP : IAPIRequestParameter
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(ChannelID))
                //    throw new APIException(201, "渠道不能为空！");
            }
        }

        public class DynamicVipFormCreateRP : IAPIRequestParameter
        {
            public string Name { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(Name))
                    throw new APIException(201, "表单名不能为空！");
                else if(Name.Length > 50)
                    throw new APIException(202, "表单名不能超过50个字！");
            }
        }

        public class DynamicVipFormIDRP : IAPIRequestParameter
        {
            public string FormID { get; set; }

            public void Validate()
            {
                Guid g = new Guid();
                if (string.IsNullOrEmpty(FormID) && Guid.TryParse(FormID, out g))
                    throw new APIException(201, "表单ID不能为空或非GUID字符！");
            }
        }

        public class DynamicVipFormSaveRP : IAPIRequestParameter
        {
            public string FormID { get; set; }
            public Field[] FieldList { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(FormID))
                    throw new APIException(201, "表单ID不能为空！");
            }
        }

        public class DynamicVipFormRenameRP : IAPIRequestParameter
        {
            public string FormID { get; set; }
            public string Name { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(FormID))
                    throw new APIException(201, "表单ID不能为空！");

                if (string.IsNullOrEmpty(Name))
                    throw new APIException(201, "表单名不能为空！");
            }
        }

        public class DynamicVipFormSceneSaveRP : IAPIRequestParameter
        {
            public string FormID { get; set; }
            public Scene[] SceneList { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(FormID))
                    throw new APIException(201, "表单ID不能为空！");
            }
        }
        
        #endregion
    }
}