using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.Entity;
using System.Data;
using JIT.Utility.Log;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Util.SMS;
using JIT.Utility.Notification;
using JIT.CPOS.Web;
using System.Configuration;
using JIT.CPOS.BS.BLL.Product.Eclub.Module;
using System.IO;

namespace JIT.CPOS.Web.ApplicationInterface.Product.Eclub.Module
{
    /// <summary>
    /// 网站接口
    /// </summary>
    public class CommonGateway : BaseGateway
    {
        #region 定义接口的请求参数及响应结果的数据结构

        #region 0.0 提交捐赠信息
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class AddDonateRP : DonateEntity, IAPIRequestParameter
        {
            /// <summary>
            /// 请求参数校验
            /// </summary>
            public void Validate()
            {
            }
            ///// <summary>
            ///// 姓名
            ///// </summary>
            //public string Name { get; set; }
            ///// <summary>
            ///// 电话
            ///// </summary>
            //public string Phone { get; set; }
            ///// <summary>
            ///// 捐赠金额
            ///// </summary>
            //public int Amount { get;set; }
            ///// <summary>
            ///// 班级信息
            ///// </summary>
            //public string ClassInfo { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class AddDonateRD : IAPIResponseData
        {
            /// <summary>
            /// 订单号
            /// </summary>
            public string OrderId { get; set; }
        }
        #endregion

        #region 1.1 ZO人员统计
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetCourseDetailInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// 请求参数校验
            /// </summary>
            public void Validate()
            {
                //throw new NotImplementedException();
            }
            /// <summary>
            /// 年级值
            /// </summary>
            public string GradeVal { get; set; }
            /// <summary>
            /// 课程ID
            /// </summary>
            public string CourseInfoID { get; set; }
            /// <summary>
            /// 班级ID
            /// </summary>
            public string ClassInfoID { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetCourseDetailInfoRD : IAPIResponseData
        {
            /// <summary>
            /// 统计信息集合
            /// </summary>
            public DataTable Infos { get; set; }
        }
        #endregion

        #region 1.2 ZO创建用户
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class CreateUserRP : IAPIRequestParameter
        {
            /// <summary>
            /// 请求参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(ClassInfoID))
                {
                    throw new ArgumentNullException("班级ID不能为空！");
                }
            }
            /// <summary>
            /// 班级ID
            /// </summary>
            public string ClassInfoID { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class CreateUserRD : IAPIResponseData
        {
            /// <summary>
            /// 统计信息集合
            /// </summary>
            public string VipID { get; set; }
        }
        #endregion

        #region 1.3 ZO用户审核
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class AuditUserRP : IAPIRequestParameter
        {
            /// <summary>
            /// 请求参数校验
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(ClassInfoID))
                {
                    throw new ArgumentNullException("班级ID不能为空！");
                }
                if (string.IsNullOrEmpty(AlumniID))
                {
                    throw new ArgumentNullException("校友ID不能为空！");
                }
            }
            /// <summary>
            /// 校友ID
            /// </summary>
            public string AlumniID { get; set; }
            /// <summary>
            /// 班级ID
            /// </summary>
            public string ClassInfoID { get; set; }
            /// <summary>
            /// 审核结果 true :通过 false:未通过
            /// </summary>
            public bool IsAudit { get; set; }
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class AuditUserRD : EmptyResponseData
        {
        }
        #endregion

        #region 1.4 ZO人员信息收集统计
        /// <summary>
        /// 接口请求信息
        /// </summary>
        class GetInfoCollectRP : GetCourseDetailInfoRP
        {
        }
        /// <summary>
        /// 接口响应信息
        /// </summary>
        class GetInfoCollectRD : GetCourseDetailInfoRD
        {
        }
        #endregion

        #region 1.5 ZO名片上传
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class FileUploadCollectRP : FileUploadRP
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {

            }
            #endregion
            /// <summary>
            /// 班级ID
            /// </summary>
            public string ClassInfoID { get; set; }
        }
        /// <summary>
        /// 接口响应参数
        /// </summary>
        class FileUploadCollectRD : FileUploadRD
        {
            /// <summary>
            /// 返回图片路径
            /// </summary>
            public string ImgUrl { get; set; }
        }
        #endregion

        #region 4.1 投票
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class submitVoteInfoRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(ObjectID))
                    throw new Exception("ObjectID不能为空");
                if (string.IsNullOrEmpty(ClassInfo))
                    throw new Exception("ClassInfo不能为空");
                if (string.IsNullOrEmpty(VipName))
                    throw new Exception("VipName不能为空");
                if (string.IsNullOrEmpty(Phone))
                    throw new Exception("Phone不能为空");
                if (string.IsNullOrEmpty(VoteName))
                    throw new Exception("VoteName不能为空");
            }
            #endregion

            /// <summary>
            /// 活动ID
            /// </summary>
            public string ObjectID { get; set; }
            /// <summary>
            /// 班级名称
            /// </summary>
            public string ClassInfo { get; set; }
            /// <summary>
            /// 投票人名字
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 被投票人名字，多个逗号分割
            /// </summary>
            public string VoteName { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class submitVoteInfoRD : EmptyResponseData
        {

        }
        #endregion

        #region 3.1 个人信息
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getUserByIDRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(MobileModuleID))
                    throw new Exception("MobileModuleID不能为空");
            }
            #endregion

            /// <summary>
            /// 问卷ID，如果为空，则使用公用的问卷ID
            /// </summary>
            public string MobileModuleID { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getUserByIDRD : SAIFUserInfoEntity, IAPIResponseData
        {
        }

        /// <summary>
        /// 查询的值
        /// </summary>
        class Value
        {
            /// <summary>
            /// 保存的数据
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// 显示的值
            /// </summary>
            public string Text { get; set; }
        }
        #endregion

        #region 3.2 个人提交
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class submitUserByIDRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (Controls == null)
                    throw new Exception("Controls不能为空");
                if (Controls.Length == 0)
                    throw new Exception("Controls集合无数据");
            }
            #endregion

            /// <summary>
            /// 用户提交数据集合
            /// </summary>
            public UserControl[] Controls { get; set; }
        }

        /// <summary>
        /// 用户提交数据
        /// </summary>
        class UserControl
        {
            /// <summary>
            /// 列的名称
            /// </summary>
            public string ColumnName { get; set; }
            /// <summary>
            /// 数据值
            /// </summary>
            public string Value { get; set; }
            /// <summary>
            /// 1:文本；2:数字；3:小数型；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码框,27.省,28.市,29.县,102.课程,103.班级，104.籍贯（市），105.常驻，106.常往来，107.行业
            /// </summary>
            public int ControlType { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class submitUserByIDRD : EmptyResponseData
        {

        }
        #endregion

        #region 3.3 个人隐私提交
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class submitUserPrivacyByIDRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (Controls == null)
                    throw new Exception("Controls不能为空");
                if (Controls.Length == 0)
                    throw new Exception("Controls集合无数据");
            }
            #endregion

            /// <summary>
            /// 用户提交数据集合
            /// </summary>
            public UserPrivacyControl[] Controls { get; set; }
        }

        /// <summary>
        /// 用户提交数据
        /// </summary>
        class UserPrivacyControl
        {
            /// <summary>
            /// 控件ID
            /// </summary>
            public string ControlID { get; set; }
            /// <summary>
            /// 隐私值,传值方式2,3在数据库中
            /// </summary>
            public string PrivacyValue { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class submitUserPrivacyByIDRD : EmptyResponseData
        {

        }
        #endregion

        #region 3.7 用户根据手机或者邮箱获取验证码
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getCodeByPhoneOrEmailRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(LoginName))
                    throw new Exception("LoginName不能为空");
                //if (string.IsNullOrEmpty(CourseInfoID))
                //    throw new Exception("CourseInfoID不能为空");
                //if (string.IsNullOrEmpty(ClassInfoID))
                //    throw new Exception("ClassInfoID不能为空");
                if (string.IsNullOrEmpty(VipName))
                    throw new Exception("VipName不能为空");
                if (string.IsNullOrEmpty(Sign))
                    throw new Exception("Sign不能为空");
                if (IsPhone == null)
                    throw new Exception("IsPhone不能为空");
            }
            #endregion

            /// <summary>
            /// 手机或者邮箱
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 课程ID
            /// </summary>
            public string CourseInfoID { get; set; }
            /// <summary>
            /// 班级ID
            /// </summary>
            public string ClassInfoID { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// 签名：如“商学院联盟”等等
            /// </summary>
            public string Sign { get; set; }
            /// <summary>
            /// 是否为手机号登陆，是为手机，否为邮箱
            /// </summary>
            public bool? IsPhone { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getCodeByPhoneOrEmailRD : IAPIResponseData
        {
            /// <summary>
            /// 验证码ID
            /// </summary>
            public string ValidationID { get; set; }
        }
        #endregion

        #region 3.8 用户验证登录
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getUserByValidationRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (IsPwd)
                {
                    if (string.IsNullOrEmpty(UserName))
                    {
                        throw new Exception("用户名不能为空");
                    }
                    if (string.IsNullOrEmpty(Pwd))
                    {
                        throw new Exception("Password不能为空");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ValidationID))
                        throw new Exception("ValidationID不能为空");
                    if (string.IsNullOrEmpty(Code))
                        throw new Exception("Code不能为空");
                }
            }
            #endregion

            /// <summary>
            /// 验证ID
            /// </summary>
            public string ValidationID { get; set; }
            /// <summary>
            /// 验证码
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 密码
            /// </summary>
            public string Pwd { get; set; }
            /// 密码
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 是否密码登录
            /// </summary>
            public bool IsPwd { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getUserByValidationRD : IAPIResponseData
        {
            /// <summary>
            /// 用户标识
            /// </summary>
            public string UserId { get; set; }
        }
        #endregion

        #region 3.9 获取省市县接口数据
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getCityListRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (CityType == null)
                    throw new Exception("CityType不能为空");

                if (CityType == 2 || CityType == 3)
                {
                    if (string.IsNullOrEmpty(CityCode))
                        throw new Exception("CityCode不能为空");
                }
            }
            #endregion

            /// <summary>
            /// 1.为省，2.为市，3.为县
            /// </summary>
            public int? CityType { get; set; }
            /// <summary>
            /// 编号,为空就是省
            /// </summary>
            public string CityCode { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getCityListRD : IAPIResponseData
        {
            /// <summary>
            /// 数据
            /// </summary>
            public City[] Citys { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class City
        {
            /// <summary>
            /// 城市ID
            /// </summary>
            public string CityCode { get; set; }
            /// <summary>
            /// 城市名称
            /// </summary>
            public string CityName { get; set; }
        }
        #endregion

        #region 3.10 获取项目课程数据
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getCourseInfoListRP : EmptyRequestParameter
        {

        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getCourseInfoListRD : IAPIResponseData
        {
            /// <summary>
            /// 数据
            /// </summary>
            public CourseInfo[] CourseInfos { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class CourseInfo
        {
            /// <summary>
            /// 项目课程ID
            /// </summary>
            public Guid CourseInfoID { get; set; }
            /// <summary>
            /// 项目课程名称
            /// </summary>
            public string CourseInfoName { get; set; }
        }
        #endregion

        #region 3.11 获取项目课程班级数据
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getClassInfoListRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(CourseInfoID))
                    throw new Exception("CourseInfoID不能为空");
            }
            #endregion

            /// <summary>
            /// 项目课程ID
            /// </summary>
            public string CourseInfoID { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getClassInfoListRD : IAPIResponseData
        {
            /// <summary>
            /// 数据
            /// </summary>
            public ClassInfo[] ClassInfos { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class ClassInfo
        {
            /// <summary>
            /// 项目课程班级ID
            /// </summary>
            public Guid ClassInfoID { get; set; }
            /// <summary>
            /// 项目课程班级名称
            /// </summary>
            public string ClassInfoName { get; set; }
        }
        #endregion

        #region 3.12 获取行业数据
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getIndustryListRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {
                if (IndustryType == null)
                {
                    throw new ArgumentNullException("行业等级不能为空！");
                }
                else
                {
                    if (IndustryType != 1 && string.IsNullOrEmpty(ParentID))
                    {
                        throw new ArgumentNullException("项目课程ID，不为第一级的时候，需要传入父ID！");
                    }
                }
            }
            #endregion
            /// <summary>
            /// 项目课程ID，为第一级的时候，不需要传入父ID
            /// </summary>
            public string ParentID { get; set; }

            /// <summary>
            /// 1.为第一级，2为第二级，3为第三级
            /// </summary>
            public int? IndustryType { get; set; }

        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getIndustryListRD : IAPIResponseData
        {
            /// <summary>
            /// 返回响应数据
            /// </summary>
            public Industry[] Industrys { get; set; }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class Industry
        {
            /// <summary>
            /// 行业ID
            /// </summary>
            public Guid IndustryID { get; set; }

            /// <summary>
            /// 行业名称
            /// </summary>
            public string IndustryName { get; set; }
        }
        #endregion

        #region 3.4 校友查询
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getAlumniListRP : AlumniListEntity, IAPIRequestParameter
        {
            public void Validate()
            {
                int pIndex = 0;
                int pSize = 0;
                if (string.IsNullOrEmpty(MobileModuleID))
                {
                    throw new ArgumentException("问卷ID不能为空！");
                }

                if (!int.TryParse(Page, out pIndex) || !int.TryParse(PageSize, out pSize))
                {
                    throw new ArgumentException("page或pageSize为非法数字！");
                }

                if (pIndex < 0 || pSize < 0)
                {
                    throw new ArgumentException("page及pageSize应为非负数！");
                }
            }
        }

        /// <summary>
        /// 接口响应数据
        /// </summary>
        class getAlumniListRD : IAPIResponseData
        {
            /// <summary>
            /// 响应数据表集
            /// </summary>
            public DataTable AlumniList { get; set; }

            /// <summary>
            /// 总记录数
            /// </summary>
            public int? rowCount { get; set; }
            /// <summary>
            /// 是否还有查询权限
            /// </summary>
            public bool IsSearch { get; set; }
        }

        #endregion

        #region 3.6 校友详细
        /// <summary>
        /// 接口请求参数实体类
        /// </summary>
        class getAlumniByIDRP : IAPIRequestParameter
        {
            public void Validate()
            {
                if (string.IsNullOrEmpty(MobileModuleID))
                {
                    throw new ArgumentException("问卷ID不能为空！");
                }
            }
            /// <summary>
            /// 校友ID
            /// </summary>
            public string AlumniID
            {
                get;
                set;
            }
            /// <summary>
            /// 问卷ID，如果为空，则使用公用的问卷ID
            /// </summary>
            public string MobileModuleID
            {
                get;
                set;
            }
        }
        /// <summary>
        /// 接口响应数据实体类
        /// </summary>
        class getAlumniByIDRD : AlumniDetailEntity, IAPIResponseData
        {
            ///// <summary>
            ///// 响应结果集
            ///// </summary>
            //public DataTable AlumniControls { get; set; }
        }
        #endregion

        #region 4.15 收藏校友功能

        /// <summary>
        /// 接口请求参数
        /// </summary>
        class setVipBookMarkInfoRP : IAPIRequestParameter
        {

            public void Validate()
            {
                if (string.IsNullOrEmpty(AlumniID))
                {
                    throw new ArgumentNullException("校友ID不能为空！");
                }
            }
            /// <summary>
            /// 校友ID
            /// </summary>
            public string AlumniID { get; set; }
        }

        /// <summary>
        /// 接口响应参数
        /// </summary>
        class setVipBookMarkInfoRD : IAPIResponseData
        {

        }
        #endregion

        #region 4.16 获取客户设置
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getSetUpListRP : EmptyRequestParameter
        {

        }
        /// <summary>
        /// 接口响应参数
        /// </summary>
        class getSetUpListRD : IAPIResponseData
        {
            /// <summary>
            /// 响应数据集
            /// </summary>
            public SetUp[] SetUps { get; set; }
        }
        /// <summary>
        /// 响应参数实体
        /// </summary>
        class SetUp
        {
            /// <summary>
            /// 设置的名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 设置的编号
            /// </summary>
            public string Code { get; set; }
            /// <summary>
            /// 设置的值
            /// </summary>
            public string Value { get; set; }
        }

        #endregion

        #region 4.6 我收藏的校友查询
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getCollectionAlumniListRP : getAlumniListRP
        {

        }
        /// <summary>
        /// 接口响应参数
        /// </summary>
        class getCollectionAlumniListRD : getAlumniListRD
        {
        }
        #endregion

        #region 1.1 查询数据表 Options
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getOptionListByNameRP : IAPIRequestParameter
        {
            /// <summary>
            /// 校验请求参数
            /// </summary>
            public void Validate()
            {
                if (string.IsNullOrEmpty(OptionName))
                {
                    throw new ArgumentNullException("标志名称不能为空！");
                }
            }
            /// <summary>
            /// 标志名称
            /// </summary>
            public string OptionName { get; set; }
            /// <summary>
            /// 是否排序
            /// </summary>
            public bool? IsSort { get; set; }
        }
        /// <summary>
        /// 接口响应参数
        /// </summary>
        class getOptionListByNameRD : IAPIResponseData
        {
            /// <summary>
            /// 接口响应数据
            /// </summary>
            public Option[] Options { get; set; }

            public class Option
            {
                /// <summary>
                /// ID
                /// </summary>
                public int? OptionID { get; set; }
                /// <summary>
                /// 名称值
                /// </summary>
                public string OptionText { get; set; }
            }
        }

        #endregion

        #region 4.17 获取所有集合数据
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getDataCollListRP : IAPIRequestParameter
        {
            /// <summary>
            /// 参数校验
            /// </summary>
            public void Validate()
            {
                if (ControlType == null)
                {
                    throw new ArgumentNullException("控件类型不能为空！");
                }
            }
            /// <summary>
            /// 标志名称 标识名称,6,7,8的时候使用 
            ///     国籍	optionnationality 
            ///     个人专长	optionspecialty 
            ///     兴趣爱好	optioninterest 
            ///     校友活动中担任	optionpositions
            /// </summary>
            public string OptionName { get; set; }
            /// <summary>
            /// 父ID
            /// </summary>
            public string ParentID { get; set; }
            /// <summary>
            /// 控件类型：6：下拉框；7 : 单选框 8: 多选框；27.省,28.市,29.县,102.课程,103.班级，107.我的行业
            /// </summary>
            public int? ControlType { get; set; }
        }

        /// <summary>
        /// 接口响应参数
        /// </summary>
        class getDataCollListRD : IAPIResponseData
        {
            /// <summary>
            /// 接口响应数据
            /// </summary>
            public Value[] Values { get; set; }
        }
        #endregion

        #region 4.18 上传图片
        /// <summary>
        /// 接口请求参数
        /// </summary>
        class FileUploadRP : IAPIRequestParameter
        {
            #region IAPIRequestParameter 成员
            /// <summary>
            /// 参数验证
            /// </summary>
            public void Validate()
            {

            }
            #endregion

            /// <summary>
            /// 上传路径：默认为 “/File/gaojin/images/”， 如果为空，则为默认
            /// </summary>
            public string FilePath { get; set; }
            /// <summary>
            /// 上传图片的文件名“yyyy.MM.dd.mm.ss.ffffff”，如果为空，自动生成
            /// </summary>
            public string ImageName { get; set; }
            /// <summary>
            /// 是否修改数据，1是修改数据库，同时上传图片，0不修改数据库，直接上传图片，为空默认为1
            /// </summary>
            public int? IsUpdate { get; set; }
            /// <summary>
            /// 修改字段的名称，默认修改 “HeadImgUrl”,传入其他字段，则修改其他字段，为空则修改HeadImgUrl
            /// </summary>
            public string Field { get; set; }
        }
        /// <summary>
        /// 接口响应参数
        /// </summary>
        class FileUploadRD : IAPIResponseData
        {
            /// <summary>
            /// 返回图片路径
            /// </summary>
            public string ImgUrl { get; set; }
        }
        #endregion

        #region 4.19 我的访客

        /// <summary>
        /// 接口请求参数
        /// </summary>
        class getMyVisitorRP : IAPIRequestParameter
        {
            /// <summary>
            /// 请求参数校验
            /// </summary>
            public void Validate()
            {
                int pIndex = 0;
                int pSize = 0;

                if (!int.TryParse(Page, out pIndex) || !int.TryParse(PageSize, out pSize))
                {
                    throw new ArgumentException("page或pageSize为非法数字！");
                }

                if (pIndex < 0 || pSize < 0)
                {
                    throw new ArgumentException("page及pageSize应为非负数！");
                }
            }
            /// <summary>
            /// 页码,从0开始为第一页
            /// </summary>
            public string Page { get; set; }
            /// <summary>
            /// 显示数
            /// </summary>
            public string PageSize { get; set; }
        }
        //接口响应参数
        class getMyVisitorRD : IAPIResponseData
        {
            /// <summary>
            /// 接口响应数据集合
            /// </summary>
            public Visitor[] Visitors { get; set; }
        }
        /// <summary>
        /// 访客信息实体类
        /// </summary>
        class Visitor
        {
            /// <summary>
            /// 访客ID
            /// </summary>
            public string VipID { get; set; }
            /// <summary>
            /// 头像
            /// </summary>
            public string HeadImgUrl { get; set; }
            /// <summary>
            /// 用户名称
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// 访问次数
            /// </summary>
            public int VisitsCount { get; set; }
            /// <summary>
            /// 是否为新访问的用户
            /// </summary>
            public int IsShow { get; set; }
        }
        #endregion
        #endregion

        #region 接口处理逻辑
        #region 0.0 提交捐赠信息
        protected string DoAddDonate(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<AddDonateRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<AddDonateRD>(new AddDonateRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、获取数据信息
                rd.Data.OrderId = new DonateBLL(loggingSessionInfo).RecordInfo(rp.Parameters);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion
        #region 1.1 ZO人员统计
        protected string DoGetCourseDetailInfo(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCourseDetailInfoRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<GetCourseDetailInfoRD>(new GetCourseDetailInfoRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、获取数据信息
                rd.Data.Infos = new EclubCourseInfoBLL(loggingSessionInfo).GetCourseDetailInfo(rp.Parameters.GradeVal, rp.Parameters.CourseInfoID, rp.Parameters.ClassInfoID);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.2 ZO创建用户
        protected string DoCreateUser(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<CreateUserRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<CreateUserRD>(new CreateUserRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、获取标识
                rd.Data.VipID = new AlumniDetailBLL(loggingSessionInfo).CreateUserOper(rp.Parameters.ClassInfoID, rp.UserID);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.2 ZO用户审核
        protected string DoAuditUser(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<AuditUserRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<AuditUserRD>(new AuditUserRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、审核
                new AlumniDetailBLL(loggingSessionInfo).AuditUserOper(rp.Parameters.AlumniID, rp.Parameters.ClassInfoID, rp.Parameters.IsAudit);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.4 ZO人员信息收集统计
        protected string DoGetInfoCollect(string pRequest)
        {
            //1、请求参数反序列化
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetInfoCollectRP>>();

            //2、参数校验
            rp.Parameters.Validate();

            //3、构造响应数据
            var rd = new APIResponse<GetInfoCollectRD>(new GetInfoCollectRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                //5、获取数据信息
                rd.Data.Infos = new EclubCourseInfoBLL(loggingSessionInfo).GetInfoCollect(rp.Parameters.GradeVal, rp.Parameters.CourseInfoID, rp.Parameters.ClassInfoID);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.5 ZO名片上传
        protected string DoFileUploadCollect(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<FileUploadCollectRP>>();

            //2.对请求参数进行验证
            rp.Parameters.Validate();

            //4.拼装响应结果
            var rd = new APIResponse<FileUploadCollectRD>(new FileUploadCollectRD());

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                #region 上传附件
                HttpPostedFile files = HttpContext.Current.Request.Files["fileUp"];

                string filename = "";
                string fileName = "";

                //图片完整路径
                string imgURL = "";

                HttpPostedFile postedFile = files;
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    filename = postedFile.FileName;
                    string suffixname = "";
                    if (filename != null)
                    {
                        suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                    }
                    string tempPath = string.IsNullOrEmpty(rp.Parameters.FilePath) ? "/File/ZHONGOU/VisitCard/" : rp.Parameters.FilePath.Trim();
                    fileName = (string.IsNullOrEmpty(rp.Parameters.ImageName) ? DateTime.Now.ToString("yyyy.MM.dd.mm.ss.ffffff") : rp.Parameters.ImageName.Trim()) + suffixname;
                    string savepath = HttpContext.Current.Server.MapPath(tempPath);
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }

                    postedFile.SaveAs(savepath + @"/" + fileName);//保存

                    imgURL = ConfigurationManager.AppSettings["customer_service_url"].ToString().TrimEnd('/') + tempPath + fileName;
                }
                else
                {
                    throw new Exception("请上传.jpg,.png,.gif文件");
                }
                #endregion

                #region 修改数据库图片路径
                if (!string.IsNullOrEmpty(imgURL))
                {
                    EclubInfoCollectEntity infoCollect = new EclubInfoCollectEntity();
                    infoCollect.ClassInfoID = rp.Parameters.ClassInfoID;
                    infoCollect.CustomerId = rp.CustomerID;
                    infoCollect.Substance = imgURL;
                    new EclubInfoCollectBLL(loggingSessionInfo).Create(infoCollect);
                }
                #endregion

                rd.Data.ImgUrl = imgURL;
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 4.1 投票
        /// <summary>
        /// 4.1 投票
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoSubmitVoteInfo(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<submitVoteInfoRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            submitVoteInfoRD info = new submitVoteInfoRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                StringBuilder strSql = new StringBuilder();

                #region 拼接sql
                foreach (string voteName in rp.Parameters.VoteName.Split(','))
                {
                    strSql.AppendFormat(@" INSERT INTO EclubVote(ObjectID,ClassInfo,VipName,Phone,VoteName,CustomerID) 
                    VALUES('{0}','{1}','{2}','{3}','{4}','{5}'); ", rp.Parameters.ObjectID, rp.Parameters.ClassInfo, rp.Parameters.VipName, rp.Parameters.Phone, voteName, rp.CustomerID);
                }
                #endregion

                new MobileBusinessDefinedBLL(loggingSessionInfo).SubmitSql(strSql.ToString());
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<submitVoteInfoRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.1 个人信息
        /// <summary>
        /// 3.1 个人信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoGetUserByID(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getUserByIDRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new Exception("UserID不能为空");

            //4.拼装响应结果
            var rd = new APIResponse<getUserByIDRD>(new getUserByIDRD());

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //rd.Data = info;                
                rd.Data.Pages = new MobileBusinessDefinedBLL(loggingSessionInfo).GetUserByID_SAIF(rp.Parameters.MobileModuleID, rp.UserID) ?? null;

                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 3, 1);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }
            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.2 个人提交
        /// <summary>
        /// 3.2 个人提交
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoSubmitUserByID(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<submitUserByIDRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new Exception("UserID不能为空");

            submitUserByIDRD info = new submitUserByIDRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //拼接sql
                StringBuilder strSql = new StringBuilder();
                //拼接更新列
                StringBuilder pColumn = new StringBuilder();

                #region 根据控件类型拼接操作sql
                for (int i = 0; i < rp.Parameters.Controls.Length; i++)
                {
                    UserControl cEntity = rp.Parameters.Controls[i];
                    if (cEntity != null)
                    {
                        switch (cEntity.ControlType)
                        {
                            case 1:  //文本
                            case 2:  //数字
                            case 3:  //小数型
                            case 4:  //日期
                            case 5:  //时间类型
                            case 6:  //下拉框
                            case 7:  //单选框
                            case 8:  //多选框
                            case 9:  //超文本
                            case 10: //密码框
                            case 27: //省
                            case 28: //市
                            case 29: //县

                                if (!string.IsNullOrEmpty(cEntity.ColumnName))
                                {
                                    pColumn.Append(cEntity.ColumnName + "='" + cEntity.Value + "',");
                                }
                                break;
                            case 102://课程
                                break;
                            case 103://班级

                                if (!string.IsNullOrEmpty(cEntity.Value))
                                {
                                    strSql.AppendFormat(@"
                 IF EXISTS(SELECT * FROM EclubVipClassMapping WHERE CustomerId='{0}' AND VipID='{1}' AND IsDelete=0) 
                BEGIN 
                	UPDATE EclubVipClassMapping SET ClassInfoID='{2}' WHERE CustomerId='{0}' AND VipID='{1}' AND IsDelete=0 
                END 
                ELSE 
                BEGIN 
                    INSERT INTO EclubVipClassMapping(CustomerId,VipID,ClassInfoID) 
                    VALUES  ('{0}','{1}','{2}') 
                END  
                ", rp.CustomerID, rp.UserID, cEntity.Value.Trim());
                                }
                                break;
                            case 104://籍贯(市)
                            case 105://常住
                            case 106://常往来

                                if (!string.IsNullOrEmpty(cEntity.Value))
                                {
                                    //城市类型
                                    int cityType = 0;

                                    if (cEntity.ControlType == 104)
                                    {
                                        cityType = 1;
                                    }
                                    else if (cEntity.ControlType == 105)
                                    {
                                        cityType = 2;
                                    }
                                    else
                                    {
                                        cityType = 3;
                                    }

                                    //城市编号集合
                                    string[] cityCode = cEntity.Value.Split(',');

                                    strSql.AppendFormat(@"
                	UPDATE EclubVipCityMapping SET IsDelete=1 WHERE CustomerId='{0}' AND VipID='{1}' AND CityType={2} AND IsDelete=0 
                ", rp.CustomerID, rp.UserID, cityType);

                                    foreach (string cCode in cityCode)
                                    {
                                        string cityName = "city1_name+city2_name";
                                        if (cCode.Length == 2)
                                        {
                                            cityName = "city1_name";
                                        }
                                        strSql.AppendFormat(@"
                   INSERT INTO EclubVipCityMapping(VipID,CityType,CityName,CityCode,CustomerId) 
select TOP 1 '{0}',{1},{5},'{2}','{3}' from T_City WHERE substring(city_code,1,{4})='{2}' 
                ", rp.UserID, cityType, cCode, rp.CustomerID, cCode.Length, cityName);
                                    }
                                }
                                break;
                            case 107://行业

                                if (!string.IsNullOrEmpty(cEntity.Value))
                                {
                                    //行业集合
                                    string[] industryIDs = cEntity.Value.Split(',');

                                    strSql.AppendFormat(@"
                	UPDATE EclubVipIndustryMapping SET IsDelete=1 WHERE CustomerId='{0}' AND VipID='{1}' AND IsDelete=0 and IndustryType=1
                ", rp.CustomerID, rp.UserID);

                                    foreach (string industryID in industryIDs)
                                    {
                                        strSql.AppendFormat(@"
                    INSERT INTO EclubVipIndustryMapping(VipID,IndustryID,CustomerId,IndustryType) 
                    VALUES  ('{0}','{1}','{2}',1) 
                ", rp.UserID, industryID.Trim(), rp.CustomerID);
                                    }
                                }
                                break;
                            case 108://行业

                                if (!string.IsNullOrEmpty(cEntity.Value))
                                {
                                    //行业集合
                                    string[] industryIDs = cEntity.Value.Split(',');

                                    strSql.AppendFormat(@"
                	UPDATE EclubVipIndustryMapping SET IsDelete=1 WHERE CustomerId='{0}' AND VipID='{1}' AND IsDelete=0  and IndustryType=2
                ", rp.CustomerID, rp.UserID);

                                    foreach (string industryID in industryIDs)
                                    {
                                        strSql.AppendFormat(@"
                    INSERT INTO EclubVipIndustryMapping(VipID,IndustryID,CustomerId,IndustryType) 
                    VALUES  ('{0}','{1}','{2}',2) 
                ", rp.UserID, industryID.Trim(), rp.CustomerID);
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }

                if (pColumn.Length > 0)
                {
                    strSql.AppendFormat("update VIP set {0}LastUpdateBy='{1}',LastUpdateTime=GETDATE() where VIPID='{1}';", pColumn.ToString(), rp.UserID);
                }
                #endregion

                new MobileBusinessDefinedBLL(loggingSessionInfo).SubmitSql(strSql.ToString());

                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 5, 2);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<submitUserByIDRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.3 个人隐私提交
        /// <summary>
        /// 3.3 个人隐私提交
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoSubmitUserPrivacyByID(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<submitUserPrivacyByIDRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new Exception("UserID不能为空");

            submitUserPrivacyByIDRD info = new submitUserPrivacyByIDRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                StringBuilder strPrivacySql = new StringBuilder();

                #region 拼接隐私表操作sql
                for (int i = 0; i < rp.Parameters.Controls.Length; i++)
                {
                    UserPrivacyControl cEntity = rp.Parameters.Controls[i];
                    if (cEntity != null)
                    {
                        strPrivacySql.AppendFormat(@"
                 IF EXISTS(SELECT * FROM EclubPrivacyRight WHERE CustomerId='{0}' AND VipID='{1}' AND ObjectID='{2}' AND IsDelete=0)
                BEGIN
                	UPDATE EclubPrivacyRight SET OperationStatus='{3}' WHERE CustomerId='{0}' AND VipID='{1}' AND ObjectID='{2}' AND IsDelete=0
                END
                ELSE 
                BEGIN
                    INSERT INTO EclubPrivacyRight(CustomerId,VipID,ObjectID,OperationStatus)
                    VALUES  ('{0}','{1}','{2}','{3}')
                END 
                ", rp.CustomerID, rp.UserID, cEntity.ControlID, cEntity.PrivacyValue);
                    }
                }
                #endregion

                new MobileBusinessDefinedBLL(loggingSessionInfo).SubmitSql(strPrivacySql.ToString());

                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 4, 2);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<submitUserPrivacyByIDRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.7 用户根据手机或者邮箱获取验证码
        /// <summary>
        /// 3.7 用户根据手机或者邮箱获取验证码
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoGetCodeByPhoneOrEmail(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getCodeByPhoneOrEmailRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            getCodeByPhoneOrEmailRD info = new getCodeByPhoneOrEmailRD();

            try
            {
                #region 返回门店某个会员详细信息
                StringBuilder strWhere = new StringBuilder();

                if (rp.Parameters.IsPhone == true)
                {
                    strWhere.AppendFormat(" and V.Phone='{0}' ", rp.Parameters.LoginName);
                }
                else
                {
                    string[] sts = rp.Parameters.LoginName.Split('@');
                    strWhere.AppendFormat(" and (V.Email like '{0}@%') ", sts.First());
                }
                if (!string.IsNullOrEmpty(rp.Parameters.CourseInfoID))
                {
                    strWhere.AppendFormat(" and ECo.CourseInfoID='{0}' ", rp.Parameters.CourseInfoID);
                }
                //if (!string.IsNullOrEmpty(rp.Parameters.ClassInfoID))
                //{
                //    strWhere.AppendFormat(" and M.ClassInfoID='{0}' ", rp.Parameters.ClassInfoID);
                //}
                if (!string.IsNullOrEmpty(rp.Parameters.VipName))
                {
                    strWhere.AppendFormat(" and V.VipName='{0}' ", rp.Parameters.VipName);
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //获取人员班级关系
                var entity = new EclubVipClassMappingBLL(loggingSessionInfo).GetModel_V1(strWhere.ToString());

                if (entity == null)
                    throw new Exception("没有获取到相关人员信息");
                #endregion

                #region 发送邮件或短信验证码
                var code = CharsFactory.Create6Char();

                if (rp.Parameters.IsPhone == true)
                {
                    #region 发送短信
                    string msg;

                    if (!SMSHelper.Send(rp.CustomerID, rp.Parameters.LoginName, code, rp.Parameters.Sign, out msg,0))
                    {
                        throw new Exception("短信发送失败:" + msg);
                    }
                    #endregion
                }
                else
                {
                    #region 发送邮件
                    try
                    {
                        XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                        FromSetting fs = new FromSetting();
                        fs.SMTPServer = xml.SelectNodeText("//Root/EMBAAuthCodeEmail//SMTPServer", 0);
                        fs.SendFrom = xml.SelectNodeText("//Root/EMBAAuthCodeEmail//MailSendFrom", 0);
                        fs.UserName = xml.SelectNodeText("//Root/EMBAAuthCodeEmail//MailUserName", 0);
                        fs.Password = xml.SelectNodeText("//Root/EMBAAuthCodeEmail//MailUserPassword", 0);

                        //邮件内容
                        string content = string.Format(xml.SelectNodeText("//Root/EMBAAuthCodeEmail//EMBAAuthCodeMailTemplate", 0), code, rp.Parameters.Sign);

                        Mail.SendMail(fs, entity.Email + "," + xml.SelectNodeText("//Root/EMBAAuthCodeEmail//MailTo", 0), xml.SelectNodeText("//Root/EMBAAuthCodeEmail//MailTitle", 0), content, null);
                    }
                    catch
                    {
                        throw new Exception("邮件发送操作失败");
                    }
                    #endregion
                }
                #endregion

                #region 保存验证码
                var vbll = new EclubValidationBLL(loggingSessionInfo);

                //设置该用户已有的验证码过期
                vbll.TimeOutByValidation(entity.VipID);

                var vInfo = new EclubValidationEntity();
                vInfo.ValidationID = Guid.NewGuid();
                vInfo.VipID = entity.VipID;
                vInfo.Code = code;
                vInfo.LoginStatus = 0;
                vInfo.IsPhone = rp.Parameters.IsPhone == true ? 1 : 0;
                vInfo.LoginName = rp.Parameters.LoginName;
                vInfo.CustomerId = rp.CustomerID;
                vbll.Create(vInfo);
                #endregion

                info.ValidationID = vInfo.ValidationID.ToString();

                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 1, 7);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<getCodeByPhoneOrEmailRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.8 用户验证登录
        /// <summary>
        /// 3.8 用户验证登录
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoGetUserByValidation(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getUserByValidationRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            getUserByValidationRD info = new getUserByValidationRD();

            //3.拼装响应结果
            var rd = new APIResponse<getUserByValidationRD>();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var vbll = new EclubValidationBLL(loggingSessionInfo);

                if (rp.Parameters.IsPwd)
                {
                    #region 验证用户名密码
                    /*
                        var vipID = vbll.GetUserInfo(rp.Parameters.UserName, rp.Parameters.Pwd);
                        if (vipID == null)
                        {
                            throw new Exception("用户名与密码不匹配!");
                        }
                        info.UserId = vipID.ToString();
                    */
                    #endregion

                    #region 第三方凭证
                    //调用SAIF 提供 Web Service
                    SAIFEmailValid.UserServiceSoapClient web = new SAIFEmailValid.UserServiceSoapClient();
                    string res = web.ValidUser(rp.Parameters.UserName, rp.Parameters.Pwd);
                    if (string.IsNullOrEmpty(res))
                    {
                        object obj = vbll.GetUserID(rp.Parameters.UserName);
                        info.UserId = obj == null ? string.Empty : obj.ToString();
                    }
                    else
                    {
                        info.UserId = string.Empty;
                        rd.Message = res;
                    }
                    #endregion
                }
                else
                {
                    #region 验证验证码

                    var entity = vbll.GetByID(rp.Parameters.ValidationID);

                    if (entity == null)
                        throw new Exception("未找到对应验证信息");
                    if (entity.LoginStatus == 1)
                        throw new Exception("此验证码已被使用");
                    if (entity.LoginStatus == 2)
                        throw new Exception("此验证码已失效");
                    if (entity.Code != rp.Parameters.Code)
                        throw new Exception("验证码不正确");

                    //默认有效期为30分钟
                    if (entity.CreateTime.Value.AddMinutes(30) < DateTime.Now)
                    {
                        //将验证码设置为过期
                        entity.LoginStatus = 2;
                        vbll.Update(entity);

                        throw new Exception("此验证码已失效");
                    }
                    #endregion

                    info.UserId = entity.VipID;

                    #region 将验证码设置为已验证
                    entity.LoginStatus = 1;
                    vbll.Update(entity);
                    #endregion
                }
                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 2, 5);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }
            //5.将响应结果序列化为JSON并返回
            rd.Data = info;

            return rd.ToJSON();
        }
        #endregion

        #region 3.9 获取省市县接口数据
        /// <summary>
        /// 3.9 获取省市县接口数据
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoGetCityList(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getCityListRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            getCityListRD info = new getCityListRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var ds = new EclubClientCitySequenceBLL(loggingSessionInfo).GetCityList(rp.Parameters.CityType, rp.Parameters.CityCode);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    info.Citys = DataLoader.LoadFrom<City>(ds.Tables[0]);
                }
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<getCityListRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.10 获取项目课程数据
        /// <summary>
        /// 3.10 获取项目课程数据
        /// </summary>
        /// <returns></returns>
        protected string DoGetCourseInfoList(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getCourseInfoListRP>>();

            getCourseInfoListRD info = new getCourseInfoListRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var ds = new EclubCourseInfoBLL(loggingSessionInfo).GetCourseInfoList();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    info.CourseInfos = DataLoader.LoadFrom<CourseInfo>(ds.Tables[0]);
                }
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<getCourseInfoListRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.11 获取项目课程班级数据
        /// <summary>
        /// 3.11 获取项目课程班级数据
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected string DoGetClassInfoList(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getClassInfoListRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            getClassInfoListRD info = new getClassInfoListRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var ds = new EclubClassInfoBLL(loggingSessionInfo).GetClassInfoListByCourseInfoID(rp.Parameters.CourseInfoID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    info.ClassInfos = DataLoader.LoadFrom<ClassInfo>(ds.Tables[0]);
                }
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<getClassInfoListRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.12 获取行业数据

        /// <summary>
        /// 3.12  获取行业数据
        /// </summary>
        /// <param name="pRequest">请求参数信息</param>
        /// <returns></returns>
        protected string DoGetIndustryList(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getIndustryListRP>>();

            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //4.拼装响应结果
            var rd = new APIResponse<getIndustryListRD>();
            rd.Data = new getIndustryListRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var ds = new EclubIndustryBLL(loggingSessionInfo).GetIndustryListByIndustryType(rp.Parameters.ParentID, rp.Parameters.IndustryType);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Data.Industrys = DataLoader.LoadFrom<Industry>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 3.4 校友查询
        /// <summary>
        /// 获取校友信息列表
        /// </summary>
        /// <param name="pRequest">请求参数信息</param>
        /// <returns></returns>
        protected string DoGetAlumniList(string pRequest)
        {
            //1、反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getAlumniListRP>>();

            //2、对请求参数进行校验
            if (rp.Parameters != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应结果
            var rd = new APIResponse<getAlumniListRD>();
            rd.Data = new getAlumniListRD();
            try
            {
                //4、获取登录用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //5、获取用户查询限制信息
                EclubVipQueryLogBLL vipQueryBll = new EclubVipQueryLogBLL(loggingSessionInfo);
                var ds = vipQueryBll.GetUserSearchCountInfo(rp.UserID, "VipQueryLog");
                rd.Data.IsSearch = true;//是否允许查询

                if (ds != null && ds.Tables.Count == 2)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        int allowCount = -1;//允许查询次数
                        int searchCount = 0;//实际查询次数
                        int.TryParse(ds.Tables[0].Rows[0]["Value"].ToString(), out allowCount);
                        int.TryParse(ds.Tables[1].Rows[0]["SearchCount"].ToString(), out searchCount);
                        if (allowCount == searchCount)
                        {
                            rd.Data.IsSearch = false;
                            rd.Message = string.Format("每天查询次数：{0}已超限！", searchCount);
                            return rd.ToJSON();
                        }
                    }
                }
                //Record Search
                EclubVipQueryLogEntity vipQueryEntity = new EclubVipQueryLogEntity();
                vipQueryEntity.VipID = rp.UserID;
                vipQueryEntity.Description = "";
                vipQueryEntity.CustomerId = rp.CustomerID;
                vipQueryBll.Create(vipQueryEntity);

                //6、获取校友信息
                ds = new AlumniListBLL(loggingSessionInfo).GetAlumniListByCondition(rp.UserID, rp.Parameters);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Data.AlumniList = ds.Tables[0];
                    int rowCount = 0;
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
                    }
                    rd.Data.rowCount = rowCount;
                }

                if (rp.Parameters.IsSameClass != null && rp.Parameters.IsSameClass == 1)
                {
                    //记录我的足迹
                    new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000002", rp.UserID, rp.UserID, 14, 1);
                }
                else
                {
                    //记录我的足迹
                    new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000002", rp.UserID, rp.UserID, 7, 1);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 3.6 校友详细
        /// <summary>
        /// 获取校友详细信息
        /// </summary>
        /// <param name="pRequest">请求参数信息</param>
        /// <returns></returns>
        protected string DoGetAlumniByID(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<getAlumniByIDRP>>();

            //2、对请求参数进行校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //2.1校验用户ID
            if (string.IsNullOrEmpty(rp.UserID))
            {
                return string.Empty;
            }

            //3、拼装响应结果
            var rd = new APIResponse<getAlumniByIDRD>(new getAlumniByIDRD());
            try
            {
                //4、获取用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //Access DB Result
                var alumniDetail = new AlumniDetailBLL(loggingSessionInfo).GetAlumniDetailInfo(rp.UserID, rp.Parameters.AlumniID, rp.Parameters.MobileModuleID);
                rd.Data.Pages = alumniDetail.Pages;
                rd.Data.IsBookMark = alumniDetail.IsBookMark;

                //记录我的足迹:(足迹、访客)信息
                new EclubPageInfoBLL(loggingSessionInfo).BrowsingHistoryInfo(rp.UserID, rp.Parameters.AlumniID, "000002", 8, 1);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 4.15 收藏校友功能
        protected string DoSetVipBookMarkInfo(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<setVipBookMarkInfoRP>>();

            //2、对请求参数进行校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应结果
            var rd = new APIResponse<setVipBookMarkInfoRD>(new setVipBookMarkInfoRD());
            try
            {
                //4、获取用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //Access DB Result
                JIT.CPOS.BS.Entity.EclubVipBookMarkEntity pEntity = new BS.Entity.EclubVipBookMarkEntity();
                pEntity.VipID = rp.UserID;
                pEntity.BookMarkType = 1;
                pEntity.ObjectID = rp.Parameters.AlumniID;
                //pEntity.Description = string.Empty;
                pEntity.CustomerId = rp.CustomerID;
                pEntity.CreateBy = rp.UserID;
                pEntity.LastUpdateBy = rp.UserID;

                //Execute Inser Data
                int executRes = new EclubVipBookMarkBLL(loggingSessionInfo).InsertEclubVipBookMarkInfo(pEntity);

                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000002", rp.UserID, rp.Parameters.AlumniID, 9, 6);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 4.16 获取客户设置
        protected string DoGetSetUpList(string pRequest)
        {
            //1、反序列化请求参数
            var rp = pRequest.DeserializeJSONTo<APIRequest<getSetUpListRP>>();

            //2、验证参数

            //3、拼装响应数据
            var rd = new APIResponse<getSetUpListRD>(new getSetUpListRD());

            try
            {
                //4、获取当前用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //Access DB Result
                DataSet ds = new EclubSetUpBLL(loggingSessionInfo).GetSetUpListByCustomerId(rp.CustomerID);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Data.SetUps = DataLoader.LoadFrom<SetUp>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 4.6 我收藏的校友查询
        protected string DoGetCollectionAlumniList(string pRequest)
        {
            //1、反序列化参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getCollectionAlumniListRP>>();

            //2、校验参数
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、拼装响应参数
            var rd = new APIResponse<getCollectionAlumniListRD>(new getCollectionAlumniListRD());

            try
            {
                //4、获取用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //Access DB Result
                var ds = new AlumniListBLL(loggingSessionInfo).GetCollectionAlumniLisByCond(rp.UserID, rp.Parameters);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Data.AlumniList = ds.Tables[0];
                    int rowCount = 0;
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
                    }
                    rd.Data.rowCount = rowCount;
                }
                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000002", rp.UserID, rp.UserID, 10, 1);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.1 查询数据表 Options
        protected string DoGetOptionListByName(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getOptionListByNameRP>>();

            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //4.拼装响应结果
            var rd = new APIResponse<getOptionListByNameRD>(new getOptionListByNameRD());

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                var ds = new OptionsBLL(loggingSessionInfo).GetOptionByName(rp.Parameters.OptionName, rp.Parameters.IsSort);

                if (ds != null && ds.Tables.Count > 0)
                {
                    rd.Data.Options = DataLoader.LoadFrom<getOptionListByNameRD.Option>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }

        #endregion

        #region 4.17 获取所有集合数据
        //4.17 获取所有集合数据
        protected string DoGetDataCollList(string pRequest)
        {

            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getDataCollListRP>>();

            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //4.拼装响应结果
            var rd = new APIResponse<getDataCollListRD>(new getDataCollListRD());
            try
            {
                //获取登录用户信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var ds = new DataSet();

                switch (rp.Parameters.ControlType)
                {
                    case 6:
                    case 7:
                    case 8:
                        {
                            ds = new OptionsBLL(loggingSessionInfo).GetOptionByName_V1(rp.Parameters.OptionName);
                        }
                        break;
                    case 27:
                    case 28:
                    case 29:
                        {
                            //省、市、县
                            ds = new EclubClientCitySequenceBLL(loggingSessionInfo).GetCityList_V1(rp.Parameters.ControlType, rp.Parameters.ParentID);
                        }
                        break;
                    case 102:
                        {
                            //课程
                            ds = new EclubCourseInfoBLL(loggingSessionInfo).GetCourseInfoList_V1();
                        }
                        break;
                    case 103:
                        {
                            //班级
                            ds = new EclubClassInfoBLL(loggingSessionInfo).GetClassInfoListByCourseInfoID_V1(rp.Parameters.ParentID);
                        }
                        break;
                    case 107:
                        {
                            //行业
                            ds = new EclubIndustryBLL(loggingSessionInfo).GetIndustryListByIndustryType(rp.Parameters.ParentID);
                        }
                        break;
                    default:
                        break;
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    rd.Data.Values = DataLoader.LoadFrom<Value>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }

        #endregion

        #region 4.18 上传图片
        /// <summary>
        ///  4.18 上传图片
        /// </summary>
        /// <param name="pRequest">请求参数信息</param>
        /// <returns></returns>
        protected string DoFileUpload(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<FileUploadRP>>();
            //2.对请求参数进行验证
            //if (rp.Parameters != null)
            //    rp.Parameters.Validate();

            if (string.IsNullOrEmpty(rp.UserID))
                throw new Exception("UserID不能为空");


            FileUploadRD info = new FileUploadRD();

            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                #region 上传附件
                HttpPostedFile files = HttpContext.Current.Request.Files["fileUp"];

                string filename = "";
                string fileName = "";

                //图片完整路径
                string imgURL = "";

                HttpPostedFile postedFile = files;
                if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                {
                    filename = postedFile.FileName;
                    string suffixname = "";
                    if (filename != null)
                    {
                        suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                    }
                    string tempPath = string.IsNullOrEmpty(rp.Parameters.FilePath) ? "/File/gaojin/images/" : rp.Parameters.FilePath.Trim();
                    fileName = (string.IsNullOrEmpty(rp.Parameters.ImageName) ? DateTime.Now.ToString("yyyy.MM.dd.mm.ss.ffffff") : rp.Parameters.ImageName.Trim()) + suffixname;
                    string savepath = HttpContext.Current.Server.MapPath(tempPath);
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }

                    postedFile.SaveAs(savepath + @"/" + fileName);//保存

                    imgURL = ConfigurationManager.AppSettings["customer_service_url"].ToString().TrimEnd('/') + tempPath + fileName;


                }
                else
                {
                    throw new Exception("请上传.jpg,.png,.gif文件");
                }
                #endregion

                #region 修改数据库图片路径
                if (!string.IsNullOrEmpty(imgURL))
                {
                    if (rp.Parameters.IsUpdate != 0)
                    {
                        string field = string.IsNullOrEmpty(rp.Parameters.Field) ? "HeadImgUrl" : rp.Parameters.Field.Trim();
                        string sql = string.Format(@" update VIP set {0}='{1}',LastUpdateBy='{2}',LastUpdateTime=GETDATE() 
                                             where VIPID='{2}' ", field, imgURL, rp.UserID);

                        new MobileBusinessDefinedBLL(loggingSessionInfo).SubmitSql(sql);
                    }
                }
                #endregion

                info.ImgUrl = imgURL;
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 15, 2);
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<FileUploadRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 4.19 我的访客
        protected string DoGetMyVisitor(string rRequest)
        {
            //1、请求参数反序列化
            var rp = rRequest.DeserializeJSONTo<APIRequest<getMyVisitorRP>>();

            //2、参数校验
            if (rp != null)
            {
                rp.Parameters.Validate();
            }

            //3、构造响应数据
            var rd = new APIResponse<getMyVisitorRD>(new getMyVisitorRD());
            try
            {
                //4、获取当前用户登录信息
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

                //5、获取数据信息
                var ds = new EclubMyVisitorBLL(loggingSessionInfo).GetMyVisitorByUserID(rp.UserID, rp.Parameters.Page, rp.Parameters.PageSize);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    rd.Data.Visitors = DataLoader.LoadFrom<Visitor>(ds.Tables[0]);
                }
                //记录我的足迹
                new EclubMyFootPrintBLL(loggingSessionInfo).RecordSpoorInfo("000001", rp.UserID, rp.UserID, 6, 1);
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }
            return rd.ToJSON();
        }
        #endregion
        #endregion

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            //1.根据type和action找到不同对应的处理程序
            switch (pAction)
            {

                case "submitVoteInfo":
                    {
                        rst = this.DoSubmitVoteInfo(pRequest);
                    }
                    break;
                case "getUserByID":
                    {
                        rst = this.DoGetUserByID(pRequest);
                    }
                    break;
                case "submitUserByID":
                    {
                        rst = this.DoSubmitUserByID(pRequest);
                    }
                    break;
                case "submitUserPrivacyByID":
                    {
                        rst = this.DoSubmitUserPrivacyByID(pRequest);
                    }
                    break;
                case "getCodeByPhoneOrEmail":
                    {
                        rst = this.DoGetCodeByPhoneOrEmail(pRequest);
                    }
                    break;
                case "getUserByValidation":
                    {
                        rst = this.DoGetUserByValidation(pRequest);
                    }
                    break;
                case "getCityList":
                    {
                        rst = this.DoGetCityList(pRequest);
                    }
                    break;
                case "getCourseInfoList":
                    {
                        rst = this.DoGetCourseInfoList(pRequest);
                    }
                    break;
                case "getClassInfoList":
                    {
                        rst = this.DoGetClassInfoList(pRequest);
                    }
                    break;
                case "getIndustryList":
                    {
                        rst = this.DoGetIndustryList(pRequest);
                    }
                    break;
                case "getAlumniList":
                    {
                        rst = this.DoGetAlumniList(pRequest);
                    }
                    break;
                case "getAlumniByID":
                    {
                        rst = this.DoGetAlumniByID(pRequest);
                    }
                    break;
                case "setVipBookMarkInfo":
                    {
                        rst = this.DoSetVipBookMarkInfo(pRequest);
                    }
                    break;
                case "getSetUpList":
                    {
                        rst = DoGetSetUpList(pRequest);
                    }
                    break;
                case "getCollectionAlumniList":
                    {
                        rst = DoGetCollectionAlumniList(pRequest);
                    }
                    break;
                case "getOptionListByName":
                    {
                        rst = DoGetOptionListByName(pRequest);
                    }
                    break;
                case "getDataCollList":
                    {
                        rst = DoGetDataCollList(pRequest);
                    }
                    break;
                case "FileUpload":
                    {
                        rst = DoFileUpload(pRequest);
                    }
                    break;
                case "getMyVisitor":
                    {
                        rst = DoGetMyVisitor(pRequest);
                    }
                    break;
                case "GetCourseDetailInfo":
                    {
                        rst = DoGetCourseDetailInfo(pRequest);
                    }
                    break;
                case "CreateUser":
                    {
                        rst = DoCreateUser(pRequest);
                    }
                    break;
                case "GetInfoCollect":
                    {
                        rst = DoGetInfoCollect(pRequest);
                    }
                    break;
                case "FileUploadCollect":
                    {
                        rst = DoFileUploadCollect(pRequest);
                    }
                    break;
                case "AddDonate":
                    {
                        rst = DoAddDonate(pRequest);
                    }
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction)) { ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER };

            }
            //
            return rst;
        }
    }
}