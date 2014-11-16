using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS.Web.Base.Excel;
using Aspose.Cells;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.ApplicationInterface.DynamicVipForm
{
    /// <summary>
    /// DynamicVipForm 的摘要说明
    /// </summary>
    public class DynamicVipFormEntry : BaseGateway
    {
        private JIT.CPOS.BS.Entity.LoggingSessionInfo PrivateLoggingSessionInfo { get { return new SessionManager().CurrentUserLoginInfo;} }
        private MobileModuleBLL PrivatePublicMobileModuleBLL { get { return new MobileModuleBLL(PrivateLoggingSessionInfo); } }
        private OptionsBLL PrivateOptionsBLL { get { return new OptionsBLL(PrivateLoggingSessionInfo); } }

        #region 表单管理

        public string DynamicVipFormList(string pRequest)
        {            
            var rd = new JIT.CPOS.BS.BLL.MobileModuleBLL.FormListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.PaginationRP>>();

            var ds = PrivatePublicMobileModuleBLL.PagedQuery(
                new Utility.DataAccess.Query.IWhereCondition[] { 
                    new Utility.DataAccess.Query.EqualsCondition() { 
                        FieldName = "CustomerId", Value = PrivateLoggingSessionInfo.ClientID
                    },
                    new Utility.DataAccess.Query.EqualsCondition() { 
                        FieldName = "IsDelete", Value = 0
                    }
                }
                , new Utility.DataAccess.Query.OrderBy[] { 
                    new Utility.DataAccess.Query.OrderBy() { 
                        Direction = Utility.DataAccess.Query.OrderByDirections.Desc, FieldName="CreateTime" 
                    }   
                }
                 , rp.Parameters.PageSize, rp.Parameters.PageIndex + 1);

            if (ds != null)
            {
                var formList = from d in ds.Entities
                                  select new JIT.CPOS.BS.BLL.MobileModuleBLL.Form()
                                  {
                                      FormID = d.MobileModuleID.ToString(),
                                      FormName = d.ModuleName,
                                      CreatedDate = d.CreateTime.ToString()
                                  };

                rd.TotalPage = ds.PageCount;
                rd.TotalCount = ds.RowCount;
                rd.FormList = formList.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipFormCreate(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormCreateRP>>();
            rp.Parameters.Validate();

            var rd = new JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormCreateRD();

            string result = "";
            result = PrivatePublicMobileModuleBLL.CreateAndReturnID(rp.Parameters.Name);
            rd.FormID = result;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            if (string.IsNullOrEmpty(result))
            {
                rsp.ResultCode = 204;
                rsp.Message = "生成失败!";
            }

            return rsp.ToJSON();
        }

        public string DynamicVipFormLoad(string pRequest)
        {
            var rd = new JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormLoadRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormIDRP>>();
            rp.Parameters.Validate();

            rd = PrivatePublicMobileModuleBLL.DynamicVipFormLoad(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipFormSave(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormSaveRP>>();
            rp.Parameters.Validate();

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            int result = this.PrivatePublicMobileModuleBLL.DynamicVipFormSave(rp.Parameters);

            if (result >= 0)
            {
                rsp.ResultCode = 0;
                rsp.Message = result.ToString();
            }
            else
            {
                rsp.ResultCode = 202;
                rsp.Message = "更新失败!";
            }
            return rsp.ToJSON();
        }

        public string DynamicVipFormRename(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipBLL = new VipBLL(loggingSessionInfo);

            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormRenameRP>>();
            rp.Parameters.Validate();

            rd = this.PrivatePublicMobileModuleBLL.DynamicVipFormRename(rp.Parameters);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipFormDelete(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipBLL = new VipBLL(loggingSessionInfo);

            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormIDRP>>();
            rp.Parameters.Validate();

            rd = this.PrivatePublicMobileModuleBLL.DynamicVipFormDelete(rp.Parameters);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipFormSceneSave(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipBLL = new VipBLL(loggingSessionInfo);

            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipFormSceneSaveRP>>();
            rp.Parameters.Validate();

            rd = this.PrivatePublicMobileModuleBLL.DynamicVipFormSceneSave(rp.Parameters);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        #region 属性管理

        public string DynamicVipDisplayTypeList(string pRequest)
        {
            var rd = new JIT.CPOS.BS.BLL.OptionsBLL.DisplayTypeListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.PaginationRP>>();

            var options = PrivateOptionsBLL.Query(
                new Utility.DataAccess.Query.IWhereCondition[] { 
                    new Utility.DataAccess.Query.EqualsCondition() { 
                        FieldName = "OptionName", Value = "ClientBussinessDefined DisplayType"
                    },
                    new Utility.DataAccess.Query.ComplexCondition() { 
                        Left = new Utility.DataAccess.Query.IsNullCondition() {
                            FieldName = "CustomerID", IsNull = true
                        }
                        , Operator = Utility.DataAccess.Query.LogicalOperators.Or 
                        , Right = new Utility.DataAccess.Query.EqualsCondition() { 
                            FieldName = "CustomerId", Value = PrivateLoggingSessionInfo.ClientID
                        }
                    },
                    new Utility.DataAccess.Query.EqualsCondition() { 
                        FieldName = "IsDelete", Value = 0
                    }
                }
                , new Utility.DataAccess.Query.OrderBy[] { 
                    new Utility.DataAccess.Query.OrderBy() { 
                        Direction = Utility.DataAccess.Query.OrderByDirections.Asc, FieldName="OptionsID" 
                    }   
                });

            if (options.Length > 0)
            {
                var displayTypeList = from d in options.AsEnumerable()
                               select new JIT.CPOS.BS.BLL.OptionsBLL.DisplayTypeEntity()
                               {
                                   DisplayType = d.OptionValue.ToString(),
                                   DisplayName = d.OptionText
                               };

                rd.DisplayTypeList = displayTypeList.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipPropertyList(string pRequest)
        {
            var rd = new JIT.CPOS.BS.BLL.MobileModuleBLL.DynamicVipPropertyListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.MobileModuleBLL.PaginationRP>>();
            rp.Parameters.Validate();

            if (rp.Parameters.PageSize == 0)
                rp.Parameters.PageSize = 50;

            rd = PrivatePublicMobileModuleBLL.DynamicVipPropertyList(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string DynamicVipPropertyOptionList(string pRequest)
        {
            var rd = new JIT.CPOS.BS.BLL.OptionsBLL.DynamicVipPropertyOptionListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<JIT.CPOS.BS.BLL.OptionsBLL.DynamicVipPropertyOptionListRP>>();
            rp.Parameters.Validate();

            rd = this.PrivateOptionsBLL.DynamicVipPropertyOptionList(rp.Parameters);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "DynamicVipFormList":
                    rst = this.DynamicVipFormList(pRequest);
                    break;
                case "DynamicVipFormCreate":
                    rst = this.DynamicVipFormCreate(pRequest);
                    break;
                case "DynamicVipFormLoad":
                    rst = this.DynamicVipFormLoad(pRequest);
                    break;
                case "DynamicVipFormSave":
                    rst = this.DynamicVipFormSave(pRequest);
                    break;
                case "DynamicVipFormRename":
                    rst = this.DynamicVipFormRename(pRequest);
                    break;
                case "DynamicVipFormDelete":
                    rst = this.DynamicVipFormDelete(pRequest);
                    break;
                case "DynamicVipFormSceneSave":
                    rst = this.DynamicVipFormSceneSave(pRequest);
                    break;
                case "DynamicVipDisplayTypeList":
                    rst = this.DynamicVipDisplayTypeList(pRequest);
                    break;
                case "DynamicVipPropertyList":
                    rst = this.DynamicVipPropertyList(pRequest);
                    break;
                case "DynamicVipPropertyOptionList":
                    rst = this.DynamicVipPropertyOptionList(pRequest);
                    break;
                //case "DynamicVipPropertyOptionList":
                //    rst = this.DynamicVipPropertyOptionList(pRequest);
                //    break;
                //case "DynamicVipPropertyOptionList":
                //    rst = this.DynamicVipPropertyOptionList(pRequest);
                //    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }
}