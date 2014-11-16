using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using System.Data;
using JIT.CPOS.BS.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    public class AlumniDetailBLL
    {
        private AlumniDetailDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前登录用户信息</param>
        public AlumniDetailBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new AlumniDetailDAO(pUserInfo);
        }
        #endregion

        #region 获取校友详细
        /// <summary>
        /// 校友详细信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniID">校友ID</param>
        /// <param name="mobileModuleID">问卷ID</param>
        /// <returns></returns>
        public AlumniDetailEntity GetAlumniDetailInfo(string userID, string alumniID, string mobileModuleID)
        {
            //校友实体
            AlumniDetailEntity alumniDetail = new AlumniDetailEntity();

            //原始信息
            DataSet ds = GetAlumniUntreatedInfo_V1(userID, alumniID, mobileModuleID);
            if (ds != null && ds.Tables != null)
            {
                #region 数据集分装
                //数据集分装
                DataTable dtPage = null;
                DataTable dtControl = null;
                DataTable dtOption = null;
                DataTable dtVip = null;
                DataTable dtCourse = null;
                DataTable dtCity = null;
                DataTable dtLive = null;
                DataTable dtIndustry = null;
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtPage = ds.Tables[0];
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    dtControl = ds.Tables[1];
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    dtOption = ds.Tables[2];
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    dtVip = ds.Tables[3];
                }
                if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    dtCourse = ds.Tables[4];
                }
                if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                {
                    dtCity = ds.Tables[5];
                }
                if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                {
                    dtLive = ds.Tables[6];
                }
                if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                {
                    dtIndustry = ds.Tables[7];
                }
                #endregion

                //Get Users Info                                            
                DataRow[] drUserInfo = dtVip != null ? dtVip.Select(string.Format("VipId='{0}'", userID)) : null;
                DataRow[] drAlumniInfo = dtVip != null ? dtVip.Select(string.Format("VipId='{0}'", alumniID)) : null;

                //Get User Course Info
                DataRow[] drUserCourse = dtCourse != null ? dtCourse.Select(string.Format("VipId='{0}'", userID)) : null;
                DataRow[] drAlumniCourse = dtCourse != null ? dtCourse.Select(string.Format("VipId='{0}'", alumniID)) : null;

                #region User And Alumni Relation

                //User And Alumni Relation         
                Hashtable htSameInfo = new Hashtable();

                if (dtCourse != null && dtCourse.Rows.Count > 0)
                {
                    //Judge (User、Alumni) Both Relation
                    JudgeRelationMethod(htSameInfo, drUserCourse, drAlumniCourse);
                }
                #endregion

                if (drAlumniInfo != null && drAlumniInfo.Length > 0)
                {
                    alumniDetail.IsBookMark = drAlumniInfo[0]["BookMarkType"].ToString();
                }
                //数据整理
                if (dtPage != null)
                {
                    //Page
                    DataRow[] drPageRow = dtPage.Select("Type=1", "Sort asc") ?? null;
                    if (drPageRow != null && drPageRow.Length > 0)
                    {
                        List<AlumniDetailEntity.Page> lstPage = new List<AlumniDetailEntity.Page>();
                        foreach (var pageRow in drPageRow)
                        {
                            AlumniDetailEntity.Page pageInfo = new AlumniDetailEntity.Page();
                            pageInfo.PageName = pageRow["title"].ToString();

                            //Block
                            DataRow[] drBlockRow = dtPage.Select(string.Format("Type=2 and ParentID='{0}'", pageRow["MobilePageBlockID"]), "Sort asc") ?? null;
                            if (drBlockRow != null && drBlockRow.Length > 0)
                            {
                                List<AlumniDetailEntity.Block> lstBlock = new List<AlumniDetailEntity.Block>();
                                foreach (var blockRow in drBlockRow)
                                {
                                    AlumniDetailEntity.Block blockInfo = new AlumniDetailEntity.Block();
                                    blockInfo.BlockName = blockRow["title"].ToString();

                                    //Control
                                    DataRow[] drControl = dtControl != null ? dtControl.Select(string.Format("MobilePageBlockID='{0}'", blockRow["MobilePageBlockID"]), "EditOrder asc") : null;
                                    if (drControl != null && dtControl.Rows.Count > 0)
                                    {
                                        List<AlumniDetailEntity.Control> lstControl = new List<AlumniDetailEntity.Control>();
                                        foreach (var controlRow in drControl)
                                        {
                                            AlumniDetailEntity.Control controlInfo = new AlumniDetailEntity.Control();

                                            #region 权限控制
                                            //Is Privacy
                                            if (!drUserInfo[0]["VipLevel"].Equals(1) && controlRow["IsPrivacy"].Equals(1))
                                            {
                                                bool noPrivacy = true;
                                                if (controlRow["OperationStatus"] != null)
                                                {
                                                    //存放的权限值如：3,4 
                                                    string[] statusStr = controlRow["OperationStatus"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                                    for (int i = 0; i < statusStr.Length; i++)
                                                    {
                                                        if (htSameInfo.ContainsKey(100))//Classmate
                                                        {
                                                            noPrivacy = false;
                                                            break;
                                                        }
                                                        else if (statusStr[i] == "1")//Only self
                                                        {
                                                            noPrivacy = true;
                                                            break;
                                                        }
                                                        else if (statusStr[i] == "2")//All User
                                                        {
                                                            noPrivacy = false;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //No Contain
                                                            if (i < statusStr.Length - 1 && !htSameInfo.ContainsKey(statusStr[i]))
                                                            {
                                                                noPrivacy = true;
                                                                continue;
                                                            }
                                                            else if (i == statusStr.Length - 1 && !htSameInfo.ContainsKey(statusStr[i]))
                                                            {
                                                                noPrivacy = true;
                                                                break;
                                                            }
                                                            else//Contail
                                                            {
                                                                noPrivacy = false;
                                                                break;
                                                            }
                                                        }
                                                    }//End for cycle
                                                }
                                                if (noPrivacy)
                                                {
                                                    //No privacy 
                                                    continue;
                                                }
                                            }
                                            #endregion

                                            controlInfo.ControlID = controlRow["MobileBussinessDefinedID"].ToString();
                                            controlInfo.ColumnDesc = controlRow["ColumnDesc"].ToString();
                                            controlInfo.ColumnName = controlRow["ColumnName"].ToString();
                                            int controlType = 0;
                                            int.TryParse((controlRow["ControlType"] ?? "").ToString(), out controlType);
                                            controlInfo.ControlType = controlType;

                                            //Value
                                            List<AlumniDetailEntity.Value> lstValue = new List<AlumniDetailEntity.Value>();
                                            AlumniDetailEntity.Value valueInfo = null;

                                            #region 为控件赋值
                                            if (drAlumniInfo != null && drAlumniInfo.Length > 0)
                                            {
                                                switch (controlType)
                                                {
                                                    case 1://文本
                                                    case 2://数字
                                                    case 3://小数型
                                                    case 4://日期
                                                    case 5://时间类型
                                                    case 10://密码框
                                                        {
                                                            valueInfo = new AlumniDetailEntity.Value();
                                                            valueInfo.ID = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            valueInfo.Text = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            lstValue.Add(valueInfo);
                                                        }
                                                        break;
                                                    case 6://下拉框
                                                    case 7://单选框
                                                    case 8://多选框
                                                        {
                                                            //Access Options Table
                                                            if (!string.IsNullOrEmpty(controlRow["CorrelationValue"].ToString().Trim()))
                                                            {
                                                                string[] values = drAlumniInfo[0][controlInfo.ColumnName].ToString().Split(',');
                                                                DataRow[] drOption = dtOption != null ? dtOption.Select(string.Format("OptionName='{0}'", controlRow["CorrelationValue"]), "Sequence asc") : null;
                                                                if (drOption != null && drOption.Length > 0)
                                                                {
                                                                    for (int l = 0; l < drOption.Length; l++)
                                                                    {
                                                                        if (values.Contains(drOption[l]["OptionValue"].ToString()))
                                                                        {
                                                                            valueInfo = new AlumniDetailEntity.Value();
                                                                            valueInfo.ID = drOption[l]["OptionValue"].ToString();
                                                                            valueInfo.Text = drOption[l]["OptionText"].ToString();
                                                                            lstValue.Add(valueInfo);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case 9://超文本
                                                        {
                                                            valueInfo = new AlumniDetailEntity.Value();
                                                            valueInfo.ID = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            valueInfo.Text = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            lstValue.Add(valueInfo);
                                                        }
                                                        break;
                                                    case 27://省
                                                        {
                                                            //Access * Table 
                                                            ShengOrShiOrXianInfo(dtCity, drAlumniInfo, controlRow, lstValue, valueInfo, "city1_name");
                                                        }
                                                        break;
                                                    case 28://市
                                                        {
                                                            //Access * Table 
                                                            ShengOrShiOrXianInfo(dtCity, drAlumniInfo, controlRow, lstValue, valueInfo, "city2_name");
                                                            //DataRow drShi = dtCity.Select(string.Format("city_code like '{0}%'", drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString())).First() ?? null;
                                                            //if (drShi != null)
                                                            //{
                                                            //    valueInfo = new AlumniDetailEntity.Value();
                                                            //    valueInfo.ID = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            //    valueInfo.Text = drShi["city2_name"].ToString();
                                                            //    lstValue.Add(valueInfo);
                                                            //}
                                                        }
                                                        break;
                                                    case 29://县
                                                        {
                                                            //Access * Table
                                                            ShengOrShiOrXianInfo(dtCity, drAlumniInfo, controlRow, lstValue, valueInfo, "city3_name");
                                                            //DataRow drXian = dtCity.Select(string.Format("city_code like '{0}%'", drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString())).First() ?? null;
                                                            //if (drXian != null)
                                                            //{
                                                            //    valueInfo = new AlumniDetailEntity.Value();
                                                            //    valueInfo.ID = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                                                            //    valueInfo.Text = drXian["city3_name"].ToString();
                                                            //    lstValue.Add(valueInfo);
                                                            //}
                                                        }
                                                        break;
                                                    case 102://课程
                                                        {
                                                            if (dtCourse != null)
                                                            {
                                                                //Access * Table
                                                                DataRow[] drCourses = dtCourse.Select(string.Format("VipId='{0}'", alumniID)) ?? null;
                                                                if (drCourses != null)
                                                                {
                                                                    foreach (var item in drCourses)
                                                                    {
                                                                        valueInfo = new AlumniDetailEntity.Value();
                                                                        valueInfo.ID = item["CourseInfoID"].ToString();
                                                                        valueInfo.Text = item["CourseInfoName"].ToString();
                                                                        lstValue.Add(valueInfo);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case 103://班级（需要修改为关联）
                                                        {
                                                            if (dtCourse != null)
                                                            {
                                                                //Access * Table
                                                                DataRow[] drClass = dtCourse.Select(string.Format("VipId='{0}'", alumniID, "ClassInfoID asc")) ?? null;
                                                                if (drClass != null)
                                                                {
                                                                    for (int i = 0; i < drClass.Length; i++)
                                                                    {
                                                                        var temp = drClass[0];
                                                                        if (drClass.Length > 1 && temp == drClass[i])// Repeat
                                                                        {
                                                                            continue;
                                                                        }
                                                                        else
                                                                        {
                                                                            temp = drClass[i];
                                                                        }
                                                                        valueInfo = new AlumniDetailEntity.Value();
                                                                        valueInfo.ID = temp["ClassInfoID"].ToString();
                                                                        valueInfo.Text = temp["ClassInfoName"].ToString();
                                                                        lstValue.Add(valueInfo);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case 104://籍贯城市类型，1为籍贯，2为常住，3为常来往为1和2的时候，一般为一个城市 ，常来往城市为多个
                                                        {
                                                            //Access * Table
                                                            VipCityInfoCollec(dtLive, lstValue, valueInfo, 1);
                                                        }
                                                        break;
                                                    case 105://常驻
                                                        {
                                                            //Access * Table
                                                            VipCityInfoCollec(dtLive, lstValue, valueInfo, 2);
                                                        }
                                                        break;
                                                    case 106://常来往
                                                        {
                                                            //Access * Table
                                                            VipCityInfoCollec(dtLive, lstValue, valueInfo, 3);
                                                        }
                                                        break;
                                                    case 107://行业
                                                        {
                                                            if (dtIndustry != null)
                                                            {
                                                                //Access * Table
                                                                DataRow[] drIndus = dtIndustry.Select(string.Format("IndustryType2 = 1 ")) ?? null;
                                                                IndustryInfoCollec(dtIndustry, lstValue, valueInfo, drIndus);
                                                            }
                                                        }
                                                        break;
                                                    case 108://关注的行业
                                                        {
                                                            if (dtIndustry != null)
                                                            {
                                                                //Access * Table
                                                                DataRow[] drIndusBookMark = dtIndustry.Select(string.Format("IndustryType2 = 2 ")) ?? null;
                                                                IndustryInfoCollec(dtIndustry, lstValue, valueInfo, drIndusBookMark);
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            if (lstValue.Count == 0)
                                            {
                                                valueInfo = new AlumniDetailEntity.Value();
                                                valueInfo.ID = "";
                                                valueInfo.Text = "";
                                                lstValue.Add(valueInfo);
                                            }

                                            #endregion

                                            //Set Values To Control
                                            controlInfo.Values = lstValue.ToArray();

                                            //Add Control Info To Control List
                                            lstControl.Add(controlInfo);
                                        }
                                        //Set Controls To Block
                                        blockInfo.Controls = lstControl.ToArray();
                                    }
                                    //Add Block Info To Block List
                                    lstBlock.Add(blockInfo);
                                }
                                //Set Blocks To Page
                                pageInfo.Blocks = lstBlock.ToArray();
                            }
                            //Add Page Info To Page List
                            lstPage.Add(pageInfo);
                        }
                        //Set Pages To Alumni Detail Info
                        alumniDetail.Pages = lstPage.ToArray();
                    }
                }
            }
            return alumniDetail;
        }

        /// <summary>
        /// 获取省、市、县信息
        /// </summary>
        /// <param name="dtCity"></param>
        /// <param name="drAlumniInfo"></param>
        /// <param name="controlRow"></param>
        /// <param name="lstValue"></param>
        /// <param name="valueInfo"></param>
        /// <param name="cityName">省：city1_name、市city2_name、县city3_name</param>
        private static void ShengOrShiOrXianInfo(DataTable dtCity, DataRow[] drAlumniInfo, DataRow controlRow, List<AlumniDetailEntity.Value> lstValue, AlumniDetailEntity.Value valueInfo, string cityName)
        {
            if (dtCity == null)
            {
                return;
            }
            DataRow drCity = dtCity.Select(string.Format("city_code like '{0}%'", drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString())).First() ?? null;
            if (drCity != null)
            {
                valueInfo = new AlumniDetailEntity.Value();
                valueInfo.ID = drAlumniInfo[0][controlRow["ColumnName"].ToString()].ToString();
                valueInfo.Text = drCity[cityName].ToString();
                lstValue.Add(valueInfo);
            }
        }
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="dtLive"></param>
        /// <param name="lstValue"></param>
        /// <param name="valueInfo"></param>
        /// <param name="cityType">城市类型，1为籍贯，2为常住，3为常来往为1和2的时候，一般为一个城市 ，常来往城市为多个</param>
        private static void VipCityInfoCollec(DataTable dtLive, List<AlumniDetailEntity.Value> lstValue, AlumniDetailEntity.Value valueInfo, int cityType)
        {
            if (dtLive == null)
            {
                return;
            }
            DataRow[] drJG = dtLive.Select(string.Format("CityType = {0}", cityType)) ?? null;
            if (drJG != null)
            {
                foreach (var jg in drJG)
                {
                    valueInfo = new AlumniDetailEntity.Value();
                    valueInfo.ID = jg["CityCode"].ToString();
                    valueInfo.Text = jg["CityName"].ToString();
                    lstValue.Add(valueInfo);
                }
            }
        }

        /// <summary>
        /// 行业信息集合
        /// </summary>
        /// <param name="dtIndustry"></param>
        /// <param name="lstValue">行业值集合对象</param>
        /// <param name="valueInfo">单条行业信息</param>
        /// <param name="drIndus">待处理行业信息集合</param>
        private static void IndustryInfoCollec(DataTable dtIndustry, List<AlumniDetailEntity.Value> lstValue, AlumniDetailEntity.Value valueInfo, DataRow[] drIndus)
        {
            if (drIndus != null)
            {
                foreach (var indus in drIndus)
                {
                    //Parent
                    valueInfo = new AlumniDetailEntity.Value();
                    valueInfo.ID = indus["IndustryID"].ToString();
                    valueInfo.Text = indus["IndustryName"].ToString();
                    lstValue.Add(valueInfo);
                    if (dtIndustry == null)
                    {
                        continue;
                    }
                    //Children
                    DataRow[] drIndusChil = dtIndustry.Select(string.Format("ParentID='{0}' and IndustryType = 2", indus["IndustryID"])) ?? null;
                    if (drIndusChil != null)
                    {
                        foreach (var indusChil in drIndusChil)
                        {
                            valueInfo = new AlumniDetailEntity.Value();
                            valueInfo.ID = indusChil["IndustryID"].ToString();
                            valueInfo.Text = indusChil["IndustryName"].ToString();
                            lstValue.Add(valueInfo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断两者之间的关系
        /// </summary>
        /// <param name="htSameInfo">记录关系</param>
        /// <param name="drUserCourse">用户信息</param>
        /// <param name="drAlumniCourse">校友信息</param>
        private static void JudgeRelationMethod(Hashtable htSameInfo, DataRow[] drUserCourse, DataRow[] drAlumniCourse)
        {
            //1、用户班级、课程信息
            Hashtable htAlumniClassInfo = new Hashtable();
            Hashtable htAlumniCourseInfo = new Hashtable();
            if (drUserCourse.Length > 0 && drAlumniCourse.Length > 0)
            {
                foreach (DataRow row in drUserCourse)
                {
                    htAlumniClassInfo.Add(row["ClassInfoID"], row["ClassInfoName"]);
                    htAlumniCourseInfo.Add(row["CourseInfoID"], row["CourseInfoName"]);
                }

                /*2、当前用户的班级、课程信息
                 *   操作状态：1.仅自己、2.所有校友可见、3.本项目课程可见、4.我加入的俱乐部会员可见、
                 *             5.我加入的地方校友会员可见、6.我加入的行业协会可见
                 */
                foreach (DataRow row in drAlumniCourse)
                {
                    if (htAlumniClassInfo.ContainsKey(row["ClassInfoID"]))
                    {
                        if (!htSameInfo.ContainsKey(100))
                        {
                            htSameInfo.Add(100, "100.同班同学");
                        }
                    }
                    if (htAlumniCourseInfo.ContainsKey(row["CourseInfoID"]))
                    {
                        if (!htSameInfo.ContainsKey("3"))
                        {
                            htSameInfo.Add("3", "3.本项目课程可见");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取校友原始信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniID">校友ID</param>
        /// <param name="mobileModuleID">问卷ID</param>
        /// <returns></returns>
        public DataSet GetAlumniUntreatedInfo_V1(string userID, string alumniID, string mobileModuleID)
        {
            return _currentDAO.GetAlumniUntreatedInfo_V1(userID, alumniID, mobileModuleID);
        }
        #endregion

        #region 创建用户
        /// <summary>
        /// 根据班级ID创建用户
        /// </summary>
        /// <param name="classInfoID">班级ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>Return UserID</returns>
        public string CreateUserOper(string classInfoID, string userID)
        {
            return _currentDAO.CreateUserOper(classInfoID, userID);
        }
        #endregion

        #region 用户审核
        /// <summary>
        /// 审核该校友是否班级正确
        /// </summary>
        /// <param name="alumniID"></param>
        /// <param name="classInfoID"></param>
        /// <param name="isAudit"></param>
        /// <returns></returns>
        public int AuditUserOper(string alumniID, string classInfoID, bool isAudit)
        {
            return _currentDAO.AuditUserOper(alumniID, classInfoID, isAudit);
        }
        #endregion
    }
}
