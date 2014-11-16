using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.BS.Entity.User
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    [XmlRootAttribute("data")]
    public class UserInfo
    {

        /// <summary>
        /// 用户标识
        /// </summary>
        [XmlElement("user_id")]
        public string User_Id { get; set; }

        /// <summary>
        /// 用户帐号（工号）
        /// </summary>
        [XmlElement("user_account")]
        public string User_Code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [XmlElement("user_name")]
        public string User_Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [XmlElement("user_name_en")]
        public string User_Name_En { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        [XmlElement("user_gender")]
        public string User_Gender { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        [XmlElement("user_birthday")]
        public string User_Birthday { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [XmlElement("user_pwd")]
        public string User_Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [XmlElement("user_email")]
        public string User_Email { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [XmlElement("user_identity")]
        public string User_Identity { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [XmlElement("user_telephone")]
        public string User_Telephone { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement("user_cellphone")]
        public string User_Cellphone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement("user_address")]
        public string User_Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement("user_postcode")]
        public string User_Postcode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("user_remark")]
        public string User_Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [XmlElement("user_status")]
        public string User_Status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [XmlElement("user_status_desc")]
        public string User_Status_Desc { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        [XmlElement("qq")]
        public string QQ { get; set; }


        /// <summary>
        /// msn
        /// </summary>
        [XmlElement("msn")]
        public string MSN { get; set; }

        /// <summary>
        /// blog
        /// </summary>
        [XmlElement("blog")]
        public string Blog { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [XmlElement("create_user_id")]
        public string Create_User_Id { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [XmlIgnore()]
        public string Create_User_Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("create_time")]
        public string Create_Time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [XmlElement("modify_user_id")]
        public string Modify_User_Id { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        [XmlIgnore()]
        public string Modify_User_Name { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modify_time")]
        public string Modify_Time { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        [XmlElement("row_no")]
        public int Row_No { get; set; }

        /// <summary>
        /// 总行号
        /// </summary>
        [XmlElement("icount")]
        public int ICount { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [XmlElement("user_expired_date")]
        public string Fail_Date { get; set; }




        /// <summary>
        /// 用户,角色,组织关系集合
        /// </summary>
        [XmlIgnore()]
        public IList<UserRoleInfo> userRoleInfoList { get; set; }
        /// <summary>
        /// 连接的客户信息
        /// </summary>
        [XmlIgnore()]
        public LoggingManager LoggingManagerInfo { get; set; }

        /// <summary>
        /// 该用户的所有门店信息
        /// </summary>
        [XmlIgnore()]
        public IList<UnitInfo> UnitList { get; set; }
        /// <summary>
        /// 用户集合
        /// </summary>
        [XmlIgnore()]
        public IList<UserInfo> UserInfoList { get; set; }

        /// <summary>
        /// 客户标识
        /// </summary>
        [XmlIgnore()]
        public string customer_id { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [XmlIgnore()]
        public string imageUrl { get; set; }

        /// <summary>
        /// UnitName
        /// </summary>
        [XmlIgnore()]
        public string UnitName { get; set; }

        /// <summary>
        /// DisplayIndex
        /// </summary>
        [XmlIgnore()]
        public Int64 DisplayIndex { get; set; }

        [XmlIgnore()]
        public string strDo { get; set; }

        [XmlIgnore()]
        public bool ModifyPassword { get; set; }


    }
}
