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
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Module.CardProduct.MakeVipCard.Request;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.UnitAndType
{
    /// <summary>
    /// UnitTypeGateway 的摘要说明
    /// </summary>
    public class UnitTypeGateway : BaseGateway
    {

        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;

        const int ERROR_VIPCARD_EXISTS = 340;   //会员已办卡
        #endregion


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {

                case "GetTypeList":  //更改促销分组
                    rst = GetTypeList(pRequest);
                    break;
                case "SaveTypeList":  //更改促销分组
                    rst = SaveTypeList(pRequest);
                    break;
                case "GetUnitStructList":  //更改促销分组
                    rst = GetUnitStructList(pRequest);
                    break;
                case "GetUnitStructByID":  //更改促销分组
                    rst = GetUnitStructByID(pRequest);
                    break;
                case "SaveUnitStruct":  //更改促销分组
                    rst = SaveUnitStruct(pRequest);
                    break;


                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }

        #region  获取组织层级数据
        public string GetTypeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetTypeListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new GetTypeListRD();//返回值

            var t_TypeBLL = new T_TypeBLL(loggingSessionInfo);
            T_TypeEntity en = new T_TypeEntity();
            en.customer_id = loggingSessionInfo.ClientID;
            var typeList = t_TypeBLL.QueryByEntity(en, null).Where(p => p.type_code != "OnlineShopping").OrderBy(p => p.type_Level).ToList();
            rd.HasSave = 1;//默认已经保存
            if (typeList != null && typeList.Count != 0)
            {
                int type_Level = (int)typeList[typeList.Count - 1].type_Level;
                //   var userDefineTypeList = typeList.Where(p => p.type_system_flag != 1).ToList();
                if (type_Level == 99)
                // if(userDefineTypeList.Count==0)
                {
                    rd.HasSave = 0;//默认已经保存
                }
            }
            rd.TypeList = typeList;
            rd.LevelCount = typeList.Count;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion


        #region  保存组织层级
        public string SaveTypeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveTypeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;


            var service = new T_TypeBLL(loggingSessionInfo);
            var LevelCount = rp.Parameters.LevelCount;
            if (LevelCount == 0)
            {
                throw new APIException("缺少参数【LevelCount】或参数值为空") { ErrorCode = 135 };
            }

            foreach (T_TypeEntity en in rp.Parameters.typeList)
            {
                if (!string.IsNullOrEmpty(en.type_name))
                {
                    en.type_id = Utils.NewGuid();
                    en.type_code = en.type_name;
                    en.type_name = en.type_name;
                    en.type_name_en = en.type_name;
                    en.type_domain = "UnitType";
                    en.type_system_flag = 0;//是否系统标识
                    en.status = 1;
                    en.type_Level = en.type_Level;
                    en.customer_id = loggingSessionInfo.ClientID;
                    service.Create(en);
                }
            }
            //更新门店type的等级和角色类型的等级
            service.UpdateShop(LevelCount, loggingSessionInfo.ClientID);

            var rd = new EmptyRD();//返回值    

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion


        #region  获取组织架构列表
        public string GetUnitStructList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitStructListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;


            var t_TypeBLL = new T_TypeBLL(loggingSessionInfo);
            var UnitStructTable = t_TypeBLL.GetUnitStructList(loggingSessionInfo.ClientID, loggingSessionInfo.UserID, rp.Parameters.hasShop);

            var rd = new GetUnitStructListRD();//返回值  

            if (UnitStructTable != null && UnitStructTable.Tables.Count != 0)
            {
                rd.UnitStructList = DataTableToObject.ConvertToList<UnitStructInfo>(UnitStructTable.Tables[0]);//直接根据所需要的字段反序列化
            }


            var rsp = new SuccessResponse<IAPIResponseData>(rd);



            return rsp.ToJSON();
        }
        #endregion
        #region  根据标识获取组织架构
        public string GetUnitStructByID(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetUnitStructByIDRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;


            var t_TypeBLL = new T_TypeBLL(loggingSessionInfo);
            var UnitStructTable = t_TypeBLL.GetUnitStructByID(loggingSessionInfo.ClientID, rp.Parameters.Unit_ID);

            var rd = new GetUnitStructByIDRD();//返回值  

            if (UnitStructTable != null && UnitStructTable.Tables.Count != 0 && UnitStructTable.Tables[0].Rows.Count != 0)
            {
                rd.unitStructInfo = DataTableToObject.ConvertToObject<UnitStructInfo>(UnitStructTable.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
            }


            var rsp = new SuccessResponse<IAPIResponseData>(rd);



            return rsp.ToJSON();
        }
        #endregion

        #region  保存组织层级
        public string SaveUnitStruct(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveUnitStructRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            if (string.IsNullOrEmpty(rp.Parameters.Unit_Name))
            {
                throw new APIException("缺少参数【Unit_Name】或参数值为空") { ErrorCode = 135 };
            }
            if (string.IsNullOrEmpty(rp.Parameters.Type_ID))
            {
                throw new APIException("缺少参数【Type_ID】或参数值为空") { ErrorCode = 135 };
            }
            //if (string.IsNullOrEmpty(rp.Parameters.parentUnit_ID))
            //{
            //    throw new APIException("缺少参数【parentUnit_ID】或参数值为空") { ErrorCode = 135 };
            //}
            //t_unitBLL t_unitBLL = new t_unitBLL(loggingSessionInfo);
            //t_unitEntity en = new t_unitEntity();
            //en.type_id = rp.Parameters.Type_ID;
            //en.unit_name = rp.Parameters.Unit_Name;
            //en.unit_code = rp.Parameters.Unit_Name;
            //en.unit_name_en = rp.Parameters.Unit_Name;
            //en.unit_name_short = rp.Parameters.Unit_Name;
            //en.Status = "1";//状态为有效
            //en.status_desc = "1";
            //en.customer_id =loggingSessionInfo.ClientName ;

            //en.modify_time = DateTime.Now.ToString();
            //en.modify_user_id = loggingSessionInfo.CurrentUser.User_Name;
            //en.unit_flag = "U";
            //en.CUSTOMER_LEVEL = 1;
            var unitService = new UnitService(loggingSessionInfo);

            UnitInfo en = new UnitInfo();
            if (string.IsNullOrEmpty(rp.Parameters.parentUnit_ID))
            {
                en.Parent_Unit_Id = "-99";
            }
            else
            {
                en.Parent_Unit_Id = rp.Parameters.parentUnit_ID;
            }
            en.TypeId = rp.Parameters.Type_ID;
            en.Code = rp.Parameters.Unit_Name;
            en.Name = rp.Parameters.Unit_Name;
            en.ShortName = rp.Parameters.Unit_Name;
            //  en.unit_ = rp.Parameters.Unit_Name;
            en.Status = "1";//状态为有效
            en.Status_Desc = "1";
            en.customer_id = loggingSessionInfo.ClientName;

            en.Modify_Time = DateTime.Now.ToString();
            en.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Name;
            en.Flag = "U";
            en.CustomerLevel = 1;
            if (string.IsNullOrEmpty(rp.Parameters.Unit_ID))//创建
            {
                en.Create_Time = DateTime.Now.ToString();
                en.Create_User_Id = loggingSessionInfo.CurrentUser.User_Name;
                en.Id = Utils.NewGuid();
                // t_unitBLL.Create(en);
            }
            else {
                en.Id = rp.Parameters.Unit_ID;//不是用unit_id,而是用Id
            }

           string errorMsg= unitService.SetUnitInfo(loggingSessionInfo, en);
           if (!string.IsNullOrEmpty(errorMsg) && errorMsg!="成功")
           {
               throw new APIException(errorMsg) { ErrorCode = 135 };
           }
            ////创建门店上下级关系
            //if (string.IsNullOrEmpty(rp.Parameters.Unit_ID))//新建时才需要创建上下级关系
            //{ 
            //}
           T_TypeBLL t_TypeBLL = new T_TypeBLL(loggingSessionInfo);
           var UnitStructTable = t_TypeBLL.GetUnitStructByID(loggingSessionInfo.ClientID, en.Id);//用en.id兼容创建和修改
           var rd = new GetUnitStructByIDRD();//返回值  

           if (UnitStructTable != null && UnitStructTable.Tables.Count != 0 && UnitStructTable.Tables[0].Rows.Count != 0)
           {
               rd.unitStructInfo = DataTableToObject.ConvertToObject<UnitStructInfo>(UnitStructTable.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
           }




            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
    }

    public class GetTypeListRD : IAPIResponseData
    {
        public IList<T_TypeEntity> TypeList { get; set; }
        public int HasSave { get; set; }
        public int LevelCount { get; set; }

    }
    public class GetTypeListRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }

    public class SaveTypeRP : IAPIRequestParameter
    {
        //促销分组
        public IList<T_TypeEntity> typeList { get; set; }//子节点
        public int LevelCount { get; set; }
        public void Validate()
        {
        }
    }
    public class SaveTypeRD : IAPIResponseData
    {

    }
    public class GetUnitStructListRD : IAPIResponseData
    {
        public IList<UnitStructInfo> UnitStructList { get; set; }
    }
    public class GetUnitStructListRP : IAPIRequestParameter
    {
        public int hasShop { get; set; }//是否包含子门店
        public void Validate()
        {
        }
    }

    public class GetUnitStructByIDRD : IAPIResponseData
    {
        public UnitStructInfo unitStructInfo { get; set; }
    }
    public class GetUnitStructByIDRP : IAPIRequestParameter
    {
        public string Unit_ID { get; set; }
        public void Validate()
        {
        }
    }

    public class UnitStructInfo
    {
        public String unit_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String type_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String unit_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String unit_name { get; set; }

        public String parent_id { get; set; }
        public String parentUnit_Name { get; set; }
        
        public String TYPE_NAME { get; set; }
        public int type_level { get; set; }
        public String next_type_id { get; set; }
        public String next_type_name { get; set; }

        public int canAddChild { get; set; }
        public int childCount { get; set; }
        public int userRoleCount { get; set; }
        public string Status { get; set; }



    }


    public class SaveUnitStructRD : IAPIResponseData
    {
        public UnitStructInfo unitStructInfo { get; set; }
    }
    public class SaveUnitStructRP : IAPIRequestParameter
    {
        public string Unit_ID { get; set; }
        public string Unit_Name { get; set; }
        public string Type_ID { get; set; }
        public string parentUnit_ID { get; set; }

        public void Validate()
        {
        }
    }


}