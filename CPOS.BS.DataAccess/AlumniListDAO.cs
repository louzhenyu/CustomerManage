using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using System.Data;

namespace JIT.CPOS.BS.DataAccess
{
    public class AlumniListDAO : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AlumniListDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region 获取校友信息列表
        /// <summary>
        /// 校友信息列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniList">查询条件实体</param>
        /// <param name="IsBookMark">查询收藏情况 False:所有校友 True:仅收藏校友</param>
        /// <returns></returns>
        public DataSet GetAlumniListByCondition(string userID, AlumniListEntity alumniList, bool IsBookMark)
        {
            //Create SQL Text            
            StringBuilder getColumnNamesSB = new StringBuilder();
            StringBuilder getAlumniListSB = new StringBuilder();
            StringBuilder getAlumnRowCount = new StringBuilder();
            StringBuilder getConditionSB = new StringBuilder();

            //Is Book Mark
            string bookMarkCondi = string.Empty;
            if (IsBookMark)
            {
                bookMarkCondi = string.Format("inner join EclubVipBookMark bMark on bMark.ObjectID=R.VIPID and bMark.VipID='{0}' and bMark.CustomerId=R.ClientID and bMark.IsDelete=0 ", userID);
            }
            else
            {
                bookMarkCondi = "";
            }

            //Get Column Names
            getColumnNamesSB.Append("select Buss.MobileBussinessDefinedID,Buss.TableName,Buss.ColumnDesc, Buss.ColumnDescEn,Buss.ColumnName from MobileBussinessDefined as Buss ");
            getColumnNamesSB.Append("inner join MobilePageBlock as Page on Page.MobilePageBlockID=Buss.MobilePageBlockID and Page.CustomerID=Buss.CustomerID and Page.IsDelete = 0 ");
            getColumnNamesSB.Append("inner join MobileModule as Module on Module.MobileModuleID=Page.MobileModuleID and Module.CustomerID=Page.CustomerID and Module.IsDelete = 0 ");
            getColumnNamesSB.Append("where Buss.IsDelete = 0 ");
            getColumnNamesSB.Append("and Buss.ListShowOrder>0 ");
            getColumnNamesSB.AppendFormat("and Module.MobileModuleID='{0}' and Module.CustomerID='{1}';", alumniList.MobileModuleID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            //获取问卷信息
            DataSet columnNamesDT = this.SQLHelper.ExecuteDataset(getColumnNamesSB.ToString());

            //创建拼接展示列对象
            StringBuilder columnNamesSB = new StringBuilder();
            if (columnNamesDT.Tables.Count > 0 && columnNamesDT.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in columnNamesDT.Tables[0].Rows)
                {
                    columnNamesSB.AppendFormat(",{0}", row["ColumnName"]);
                }
            }

            //Get Alumni Info List
            getAlumniListSB.AppendFormat("select VIPID{0},CourseInfoName from(", columnNamesSB.ToString());
            getAlumniListSB.AppendFormat("select ROW_NUMBER()over(order by VIPID) as Row ,VipID{0},CourseInfoName from( ", columnNamesSB.ToString());
            getAlumniListSB.AppendFormat("select distinct R.VipID{0},Z.CourseInfoName from Vip R ", columnNamesSB.ToString());
            getAlumniListSB.Append("inner join EclubVipClassMapping X on X.IsDelete=0 and X.CustomerId=R.ClientID and X.VipID=R.VIPID ");
            getAlumniListSB.Append("inner join EclubClassInfo as Y on Y.IsDelete=0 and Y.CustomerID=X.CustomerId and Y.ClassInfoID=X.ClassInfoID ");
            getAlumniListSB.Append("inner join EclubCourseInfo as Z on Z.IsDelete=0 and Z.CustomerID=Y.CustomerId and Z.CourseInfoID=Y.CourseInfoID ");
            getAlumniListSB.Append(bookMarkCondi);
            //是否为本班级
            StringBuilder sameClassSB = new StringBuilder();

            if (alumniList.IsSameClass != null && alumniList.IsSameClass == 1)
            {
                sameClassSB.AppendFormat("inner join EclubVipClassMapping as A on A.IsDelete=0 and A.CustomerID=R.ClientID and A.VipID='{0}' ", userID);
                sameClassSB.Append("inner join EclubVipClassMapping as B on B.IsDelete=0 and B.CustomerID=R.ClientID and B.ClassInfoID=A.ClassInfoID and B.VipID=R.VIPID ");
                if (IsBookMark)
                {
                    sameClassSB.AppendFormat("where R.IsDelete=0 and R.ClientID='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                }
                else
                {
                    //sameClassSB.AppendFormat("where R.IsDelete=0 and R.VIPID != '{0}' and R.ClientID='{1}' ", userID, CurrentUserInfo.CurrentLoggingManager.Customer_Id);//剔除自己
                    sameClassSB.AppendFormat("where R.IsDelete=0 and R.ClientID='{0}' ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);
                }
            }

            //查询校友信息条件
            StringBuilder columnConditionSB = new StringBuilder();

            //课程、班级、行业、（省、市、县）、（籍贯、常驻、常往来）
            StringBuilder sqlRelateSB = new StringBuilder();

            //SQL条件 字典集合
            Dictionary<string, string> dicSQL = new Dictionary<string, string>();

            //根据条件返回关联SQL
            ColumnCondition(alumniList, columnConditionSB, dicSQL, CurrentUserInfo.CurrentLoggingManager.Customer_Id, userID, IsBookMark);

            foreach (var item in dicSQL)
            {
                sqlRelateSB.Append(item.Value);
            }
            //根据请求参数追加SQL Script
            getAlumniListSB.Append(sqlRelateSB.ToString());
            getAlumniListSB.Append(sameClassSB.ToString());
            getAlumniListSB.Append(columnConditionSB.ToString());

            getAlumniListSB.Append(") as Result ");
            getAlumniListSB.Append(") as Res ");
            getAlumniListSB.AppendFormat("where Res.Row between {0}*{1} + 1 and {0}*({1}+1) ;", alumniList.PageSize, alumniList.Page);

            //获取校友信息总数
            getAlumniListSB.Append("select Count(distinct R.VIPID) as RCount from Vip R ");
            getAlumniListSB.Append(bookMarkCondi);
            getAlumniListSB.Append(sqlRelateSB.ToString());
            getAlumniListSB.Append(sameClassSB.ToString());
            getAlumniListSB.Append(columnConditionSB.ToString());

            //Return Result
            return this.SQLHelper.ExecuteDataset(getAlumniListSB.ToString());
        }

        /// <summary>
        /// Append EclubVip Class Mapping Parameter
        /// </summary>
        /// <param name="alumniList">参数实体对象</param>
        /// <param name="columnConditionSB">StringBuilder拼接对象</param>
        /// <param name="dicSQL">查询SQL拼接字符串</param>
        /// <param name="customerID">客户标识</param>
        /// <param name="userID">用户ID</param>
        /// <param name="IsBookMark">是否收藏条件</param>
        private static void ColumnCondition(AlumniListEntity alumniList, StringBuilder columnConditionSB, Dictionary<string, string> dicSQL, string customerID, string userID, bool IsBookMark)
        {
            //关联表
            StringBuilder classSB = new StringBuilder();
            StringBuilder courseSB = new StringBuilder();
            StringBuilder citySB = new StringBuilder();
            StringBuilder industrySB = new StringBuilder();
            if (alumniList.IsSameClass == null || alumniList.IsSameClass == 0)
            {
                if (IsBookMark)
                {
                    columnConditionSB.AppendFormat("where R.IsDelete=0 and R.ClientID='{0}' ", customerID);
                }
                else
                {

                    //columnConditionSB.AppendFormat("where R.IsDelete=0 and R.ClientID='{0}' and R.VIPID != '{1}' ", customerID, userID);//剔除自己
                    columnConditionSB.AppendFormat("where R.IsDelete=0 and R.ClientID='{0}' ", customerID);
                }
            }
            if (alumniList.Control.Length > 0)
            {
                foreach (Control control in alumniList.Control)
                {
                    if (string.IsNullOrEmpty(control.Value))
                    {
                        continue;
                    }
                    switch (control.ControlType)
                    {
                        case 1://文本
                        case 9://超文本
                            columnConditionSB.AppendFormat("and R.{0} like '%{1}%' ", control.ColumnName, control.Value);
                            break;
                        case 2://数字
                        case 3://小数型
                        case 6://下拉框
                        case 7://单选框
                        case 27://省
                        case 28://市
                        case 29://县
                            columnConditionSB.AppendFormat("and R.{0}='{1}'", control.ColumnName, control.Value);
                            break;
                        case 4://日期；
                            columnConditionSB.AppendFormat("and DATEDIFF(DD,R.{0},'{1}') = 0 ", control.ColumnName, control.Value);
                            break;
                        case 5://时间类型
                            columnConditionSB.AppendFormat("and DATEDIFF(HH,R.{0},'{1}') = 0 ", control.ColumnName, control.Value);
                            break;
                        case 8://多选框
                            {
                                //columnConditionSB.AppendFormat("and R.{0} in({1})", control.ColumnName, TreatQueryPara(control));
                                if (control != null)
                                {
                                    StringBuilder sbCon = new StringBuilder();
                                    sbCon.Append("and ( ");
                                    string[] values = control.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < values.Length; i++)
                                    {
                                        if (i == values.Length - 1)
                                        {
                                            sbCon.AppendFormat("','+R.{0}+',' like '%,{1},%' ) ", control.ColumnName, values[i]);
                                            continue;
                                        }
                                        sbCon.AppendFormat("','+R.{0}+',' like '%,{1},%' or ", control.ColumnName, values[i]);
                                    }
                                    columnConditionSB.Append(sbCon.ToString());
                                }
                            }
                            break;
                        case 10://密码框
                            break;
                        case 102://课程
                            {
                                courseSB.Clear();
                                courseSB.Append("inner join EclubVipClassMapping VipMapClass on VipMapClass.VipID=R.VIPID and VipMapClass.IsDelete=0 and VipMapClass.CustomerID=R.ClientID ");
                                courseSB.Append("inner join EclubClassInfo Class on Class.ClassInfoID=VipMapClass.ClassInfoID and Class.IsDelete=0 and Class.CustomerID=VipMapClass.CustomerID ");
                                columnConditionSB.AppendFormat("and Class.CourseInfoID='{0}' ", control.Value);
                            }
                            break;
                        case 103://班级
                            {
                                classSB.Clear();
                                classSB.Append("inner join EclubVipClassMapping VipMapCourse on VipMapCourse.VipID=R.VIPID and VipMapCourse.IsDelete=0 and VipMapCourse.CustomerID=R.ClientID ");
                                columnConditionSB.AppendFormat("and VipMapCourse.ClassInfoID in({0}) ", TreatQueryPara(control));
                            }
                            break;
                        case 104://籍贯（市）
                            citySB.Clear();
                            citySB.Append("inner join EclubVipCityMapping VipMapCity on VipMapCity.VipID=R.VIPID and VipMapCity.IsDelete=0 and VipMapCity.CustomerID=R.ClientID ");
                            columnConditionSB.AppendFormat("and VipMapCity.CityCode='{0}' and VipMapCity.CityType=1  ", control.Value);
                            break;
                        case 105://常驻
                            citySB.Clear();
                            citySB.Append("inner join EclubVipCityMapping VipMapCity on VipMapCity.VipID=R.VIPID and VipMapCity.IsDelete=0 and VipMapCity.CustomerID=R.ClientID ");
                            columnConditionSB.AppendFormat("and VipMapCity.CityCode in({0}) and VipMapCity.CityType=2  ", TreatQueryPara(control));
                            break;
                        case 106://常往来
                            citySB.Clear();
                            citySB.Append("inner join EclubVipCityMapping VipMapCity on VipMapCity.VipID=R.VIPID and VipMapCity.IsDelete=0 and VipMapCity.CustomerID=R.ClientID ");
                            columnConditionSB.AppendFormat("and VipMapCity.CityCode='{0}' and VipMapCity.CityType=3  ", control.Value);
                            break;
                        case 107://行业
                            industrySB.Clear();
                            industrySB.Append("inner join EclubVipIndustryMapping VipMapIndust on VipMapIndust.VipID=R.VIPID and VipMapIndust.IsDelete=0 and VipMapIndust.CustomerID=R.ClientID ");
                            industrySB.Append("inner join EclubIndustry Industry on Industry.IndustryID=VipMapIndust.IndustryID and VipMapIndust.CustomerID=Industry.CustomerID  and Industry.IsDelete=0 ");
                            columnConditionSB.AppendFormat("and Industry.IndustryID in({0}) ", TreatQueryPara(control));
                            break;
                        default:
                            break;
                    }
                }
                //Set Key Value
                dicSQL.Add("City", citySB.ToString());
                dicSQL.Add("Course", courseSB.ToString());
                dicSQL.Add("Class", classSB.ToString());
                dicSQL.Add("Industry", industrySB.ToString());
            }
        }
        /// <summary>
        /// 处理选项多选
        /// </summary>
        /// <param name="control">控件</param>
        /// <returns></returns>
        private static string TreatQueryPara(Control control)
        {
            string valusDeal = string.Empty;
            if (control.Value != null)
            {
                StringBuilder sbValuesDeal = new StringBuilder();
                string[] values = control.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < values.Length; i++)
                {
                    if (i == values.Length - 1)
                    {
                        sbValuesDeal.AppendFormat("'{0}'", values[i]);
                        continue;
                    }
                    sbValuesDeal.AppendFormat("'{0}',", values[i]);
                }
                valusDeal = sbValuesDeal.ToString();
            }
            return valusDeal;
        }
        #endregion

    }
}
