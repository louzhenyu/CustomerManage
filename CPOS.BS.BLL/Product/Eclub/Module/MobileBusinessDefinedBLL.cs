using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.DataAccess.Product.Eclub.Module;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Product.Eclub.Module;

namespace JIT.CPOS.BS.BLL.Product.Eclub.Module
{
    public class MobileBusinessDefinedBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private MobileBusinessDefinedDAO _currentDAO;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileBusinessDefinedBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new MobileBusinessDefinedDAO(pUserInfo);
        }
        #endregion

        #region GetUserByID
        //public List<JIT.CPOS.BS.Entity.Product.Eclub.Module.PageEntity> getUserByID(string pModuleCode,string userID)
        //{
        //    DataSet ds = this._currentDAO.getUserByID(pModuleCode, userID);
        //    List<JIT.CPOS.BS.Entity.Product.Eclub.Module.PageEntity> lPageEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.PageEntity>();
        //    List<JIT.CPOS.BS.Entity.Product.Eclub.Module.BlockEntity> lBlockEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.BlockEntity>();
        //    List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlEntity> lControlEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlEntity>();
        //    List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ConOptionsEntity> lOptionEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ConOptionsEntity>();
        //    if (ds != null && ds.Tables != null)
        //    {
        //        DataTable dtPage = null;
        //        DataTable dtControl = null;
        //        DataTable dtOption = null;
        //        DataTable dtVip = null;
        //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtPage = ds.Tables[0];
        //        }
        //        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        //        {
        //            dtControl = ds.Tables[1];
        //        }
        //        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
        //        {
        //            dtOption = ds.Tables[2];
        //        }
        //        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
        //        {
        //            dtVip = ds.Tables[3];
        //        }
        //        if (dtPage != null)
        //        {
        //            DataRow[] dtPageRow = dtPage.Select(" Type=1", " Sort asc");
        //            if (dtPageRow != null && dtPageRow.Length > 0)
        //            {
        //                for (int i = 0; i < dtPageRow.Length; i++)
        //                {
        //                    JIT.CPOS.BS.Entity.Product.Eclub.Module.PageEntity pageEntity = new JIT.CPOS.BS.Entity.Product.Eclub.Module.PageEntity();
        //                    pageEntity.PageNum = (i + 1);
        //                    pageEntity.PageName = dtPageRow[i]["title"].ToString();
        //                    DataRow[] dtBlockRow = dtPage.Select(" Type=2 and ParentID='" + dtPageRow[i]["MobilePageBlockID"] + "'", " Sort asc");
        //                    if (dtBlockRow != null && dtBlockRow.Length > 0)
        //                    {
        //                        lBlockEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.BlockEntity>();
        //                        for (int j = 0; j < dtBlockRow.Length; j++)
        //                        {
        //                            JIT.CPOS.BS.Entity.Product.Eclub.Module.BlockEntity blockEntity = new JIT.CPOS.BS.Entity.Product.Eclub.Module.BlockEntity();
        //                            blockEntity.BlockName = dtBlockRow[j]["title"].ToString();
        //                            blockEntity.BlockSort = Convert.ToInt32(dtBlockRow[j]["Sort"]);
        //                            DataRow[] drControl = dtControl.Select("MobilePageBlockID='" + dtBlockRow[j]["MobilePageBlockID"] + "'", " EditOrder asc");
        //                            if (drControl != null && drControl.Length > 0)
        //                            {
        //                                lControlEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlEntity>();
        //                                for (int k = 0; k < drControl.Length; k++)
        //                                {
        //                                    JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlEntity controlEntity = new JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlEntity();
        //                                    controlEntity.AuthType = drControl[k]["AuthType"].ToString();
        //                                    controlEntity.ColumnDesc = drControl[k]["ColumnDesc"].ToString();
        //                                    controlEntity.ColumnName = drControl[k]["ColumnName"].ToString();
        //                                    controlEntity.ColumnDescEN = drControl[k]["ColumnDescEN"].ToString();
        //                                    controlEntity.ControlID = drControl[k]["MobileBussinessDefinedID"].ToString();
        //                                    controlEntity.ControlType = drControl[k]["ControlType"].ToString();
        //                                    controlEntity.IsMustDo = drControl[k]["IsMustDo"].ToString() == "1" ? true : false;
        //                                    controlEntity.LinkageItem = drControl[k]["LinkageItem"].ToString();
        //                                    controlEntity.ExampleValue = drControl[k]["ExampleValue"].ToString();
        //                                    controlEntity.MaxLength = Convert.ToInt32(drControl[k]["MaxLength"]);
        //                                    controlEntity.MaxSelected = Convert.ToInt32(drControl[k]["MaxSelected"]);
        //                                    controlEntity.MinLength = Convert.ToInt32(drControl[k]["MinLength"]);
        //                                    controlEntity.MinSelected = Convert.ToInt32(drControl[k]["MinSelected"]);

        //                                    controlEntity.CorrelationValue = drControl[k]["CorrelationValue"].ToString();
        //                                    controlEntity.OperationStatus = drControl[k]["OperationStatus"].ToString();
        //                                    if (dtVip != null && dtVip.Rows.Count > 0)
        //                                    {
        //                                        if (dtVip.Rows[0]["" + controlEntity.ColumnName + ""] != null)
        //                                        {
        //                                            controlEntity.Value = dtVip.Rows[0]["" + controlEntity.ColumnName + ""].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            controlEntity.Value = "";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        controlEntity.Value = "";
        //                                    }
        //                                    if (!string.IsNullOrEmpty(controlEntity.CorrelationValue.Trim()))
        //                                    {
        //                                        DataRow[] drOption = dtOption.Select("OptionName='" + controlEntity.CorrelationValue + "'", " Sequence asc");
        //                                        if (drOption != null && drOption.Length > 0)
        //                                        {
        //                                            lOptionEntity = new List<JIT.CPOS.BS.Entity.Product.Eclub.Module.ConOptionsEntity>();
        //                                            for (int l = 0; l < drOption.Length; l++)
        //                                            {
        //                                                JIT.CPOS.BS.Entity.Product.Eclub.Module.ConOptionsEntity optionsEntity = new JIT.CPOS.BS.Entity.Product.Eclub.Module.ConOptionsEntity();
        //                                                optionsEntity.IsSelected = false;
        //                                                optionsEntity.OptionID = drOption[l]["OptionValue"].ToString();
        //                                                optionsEntity.OptionText = drOption[l]["OptionText"].ToString();
        //                                                lOptionEntity.Add(optionsEntity);
        //                                            }
        //                                            controlEntity.Options = lOptionEntity;
        //                                        }
        //                                    }
        //                                    lControlEntity.Add(controlEntity);
        //                                }
        //                                blockEntity.Control = lControlEntity;
        //                            }
        //                            lBlockEntity.Add(blockEntity);
        //                        }
        //                        pageEntity.Block = lBlockEntity;
        //                    }
        //                    lPageEntity.Add(pageEntity);
        //                }
        //            }
        //        }
        //    }
        //    return lPageEntity;
        //}
        #endregion

        #region SubmitUserByID
        //        public int SubmitUserByID(RequestSubmitUserByIDData pEntity)
        //        {
        //            string strSql = "";
        //            StringBuilder strPrivacySql = new StringBuilder();

        //            string pUserID = string.IsNullOrEmpty(pEntity.UserId) ? Guid.NewGuid().ToString() : pEntity.UserId;
        //            if (pEntity != null && pEntity.Control != null && pEntity.Control.Count > 0)
        //            {
        //                StringBuilder pColumn = new StringBuilder();
        //                StringBuilder pValues = new StringBuilder();
        //                for (int i = 0; i < pEntity.Control.Count; i++)
        //                {
        //                    JIT.CPOS.BS.Entity.Product.Eclub.Module.ControlUpdateEntity cEntity = pEntity.Control[i];
        //                    if (cEntity != null)
        //                    {
        //                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
        //                        {
        //                            if (string.IsNullOrEmpty(pEntity.UserId))//新增
        //                            {
        //                                pColumn.Append(cEntity.ColumnName + ",");
        //                                pValues.Append("'" + cEntity.Value + "',");
        //                            }
        //                            else//修改
        //                            {
        //                                pColumn.Append(cEntity.ColumnName + "='" + cEntity.Value + "',");
        //                            }

        //                            if (cEntity.IsPrivacy == 1)
        //                            {
        //                                strPrivacySql.AppendFormat(@"
        //IF EXISTS(SELECT * FROM EclubPrivacyRight WHERE CustomerId='{0}' AND VipID='{1}' AND ObjectID='{2}' AND IsDelete=0)
        //BEGIN
        //	UPDATE EclubPrivacyRight SET OperationStatus='{3}' WHERE CustomerId='{0}' AND VipID='{1}' AND ObjectID='{2}' AND IsDelete=0
        //END
        //ELSE 
        //BEGIN
        //    INSERT INTO EclubPrivacyRight(CustomerId,VipID,ObjectID,OperationStatus)
        //    VALUES  ('{0}','{1}','{2}','{3}')
        //END
        //", pEntity.CustomerId, pUserID, cEntity.ControlID, cEntity.OperationStatus);
        //                            }
        //                        }
        //                    }
        //                }

        //                if (string.IsNullOrEmpty(pEntity.UserId))
        //                {
        //                    strSql = string.Format(@"
        //insert vip ({0}VipID,CreateBy,CreateTime,ClientID)values({1}'{2}','1',GETDATE(),'{3}');", pColumn.ToString(), pValues.ToString(), pUserID, pEntity.CustomerId);
        //                }
        //                else
        //                {
        //                    strSql = string.Format("update VIP set {0}LastUpdateBy='{1}',LastUpdateTime=GETDATE() where VIPID='{1}';", pColumn.ToString(), pEntity.UserId);
        //                }
        //            }
        //            return this._currentDAO.SubmitSql(strSql + strPrivacySql);
        //        }
        #endregion

        #region 获取个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="moduleCode">问卷编号</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public DataSet getUserByID(string mobileModuleID, string userID)
        {
            return this._currentDAO.getUserByID(mobileModuleID, userID);
        }
        /// <summary>
        /// 获取个人信息：Add By Alan
        /// </summary>
        /// <param name="moduleCode">问卷编号</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public SAIFUserInfoEntity.Page[] GetUserByID_SAIF(string mobileModuleID, string userID)
        {
            //PageInfos List
            List<SAIFUserInfoEntity.Page> lPage = new List<SAIFUserInfoEntity.Page>();

            //Get Data Source
            DataSet ds = this._currentDAO.getUserByID(mobileModuleID, userID);

            //个人信息处理
            if (ds != null && ds.Tables != null)
            {
                #region 数据源分类
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

                if (dtPage != null)
                {
                    DataRow[] dtPageRow = dtPage.Select(" Type=1", " Sort asc");
                    if (dtPageRow != null && dtPageRow.Length > 0)
                    {
                        for (int i = 0; i < dtPageRow.Length; i++)
                        {
                            //Page
                            SAIFUserInfoEntity.Page pageInfo = new SAIFUserInfoEntity.Page();
                            pageInfo.PageName = dtPageRow[i]["title"].ToString();
                            DataRow[] dtBlockRow = dtPage.Select(string.Format(" Type=2 and ParentID='{0}'", dtPageRow[i]["MobilePageBlockID"]), " Sort asc");
                            if (dtBlockRow != null && dtBlockRow.Length > 0)
                            {
                                List<SAIFUserInfoEntity.Block> lBlock = new List<SAIFUserInfoEntity.Block>();
                                for (int j = 0; j < dtBlockRow.Length; j++)
                                {
                                    //Block
                                    SAIFUserInfoEntity.Block blockInfo = new SAIFUserInfoEntity.Block();
                                    blockInfo.BlockName = dtBlockRow[j]["title"].ToString();
                                    DataRow[] drControl = (dtControl != null) ? dtControl.Select(string.Format("MobilePageBlockID='{0}'", dtBlockRow[j]["MobilePageBlockID"]), " EditOrder asc") : null;
                                    if (drControl != null && drControl.Length > 0)
                                    {
                                        List<SAIFUserInfoEntity.Control> lControl = new List<SAIFUserInfoEntity.Control>();
                                        for (int k = 0; k < drControl.Length; k++)
                                        {
                                            //Control
                                            SAIFUserInfoEntity.Control controlInfo = new SAIFUserInfoEntity.Control();
                                            controlInfo.ControlID = drControl[k]["MobileBussinessDefinedID"].ToString();
                                            controlInfo.ColumnDesc = drControl[k]["ColumnDesc"].ToString();
                                            controlInfo.ColumnName = drControl[k]["ColumnName"].ToString();
                                            controlInfo.PrivacyValue = drControl[k]["OperationStatus"].ToString();

                                            #region 控件赋值
                                            if (dtVip != null && dtVip.Rows.Count > 0)
                                            {
                                                List<SAIFUserInfoEntity.Value> lValue = new List<SAIFUserInfoEntity.Value>();
                                                if (dtVip.Rows[0]["" + controlInfo.ColumnName + ""] != null)
                                                {
                                                    //Value                 
                                                    SAIFUserInfoEntity.Value valueInfo = null;
                                                    int controlType = 0;
                                                    int.TryParse((drControl[k]["ControlType"] ?? "").ToString(), out controlType);
                                                    switch (controlType)
                                                    {

                                                        case 1://文本
                                                        case 2://数字
                                                        case 3://小数型
                                                        case 4://日期
                                                        case 5://时间类型
                                                        case 9://超文本
                                                        case 10://密码框
                                                            {
                                                                valueInfo = new SAIFUserInfoEntity.Value();
                                                                valueInfo.ID = dtVip.Rows[0][controlInfo.ColumnName].ToString();
                                                                valueInfo.Text = dtVip.Rows[0][controlInfo.ColumnName].ToString();
                                                                lValue.Add(valueInfo);
                                                            }
                                                            break;
                                                        case 6://下拉框
                                                        case 7://单选框
                                                        case 8://多选框
                                                            {
                                                                if (!string.IsNullOrEmpty(dtVip.Rows[0][controlInfo.ColumnName].ToString().Trim()))
                                                                {
                                                                    string[] values = dtVip.Rows[0][controlInfo.ColumnName].ToString().Split(',');
                                                                    if (dtOption != null)
                                                                    {
                                                                        DataRow[] drOption = dtOption.Select(string.Format("OptionName='{0}'", drControl[k]["CorrelationValue"].ToString()), "Sequence asc") ?? null;
                                                                        if (drOption != null && drOption.Length > 0)
                                                                        {
                                                                            for (int l = 0; l < drOption.Length; l++)
                                                                            {
                                                                                if (values.Contains(drOption[l]["OptionValue"].ToString()))
                                                                                {
                                                                                    valueInfo = new SAIFUserInfoEntity.Value();
                                                                                    valueInfo.ID = drOption[l]["OptionValue"].ToString();
                                                                                    valueInfo.Text = drOption[l]["OptionText"].ToString();
                                                                                    lValue.Add(valueInfo);
                                                                                }
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case 27://省
                                                            {
                                                                CityInfoHandle(dtVip, dtCity, controlInfo, lValue, valueInfo, "city1_name");
                                                            }
                                                            break;
                                                        case 28://市
                                                            {
                                                                CityInfoHandle(dtVip, dtCity, controlInfo, lValue, valueInfo, "city2_name");
                                                            }
                                                            break;
                                                        case 29://县
                                                            {
                                                                CityInfoHandle(dtVip, dtCity, controlInfo, lValue, valueInfo, "city3_name");
                                                            }
                                                            break;
                                                        case 102://课程
                                                            {
                                                                if (dtCourse != null)
                                                                {
                                                                    DataRow[] drCourses = dtCourse.Select() ?? null;
                                                                    if (drCourses != null)
                                                                    {
                                                                        foreach (var item in drCourses)
                                                                        {
                                                                            valueInfo = new SAIFUserInfoEntity.Value();
                                                                            valueInfo.ID = item["CourseInfoID"].ToString();
                                                                            valueInfo.Text = item["CourseInfoName"].ToString();
                                                                            lValue.Add(valueInfo);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case 103://班级
                                                            {
                                                                if (dtCourse != null)
                                                                {
                                                                    DataRow[] drClass = dtCourse.Select("1 = 1", "ClassInfoID asc") ?? null;
                                                                    if (drClass != null)
                                                                    {
                                                                        for (int z = 0; z < drClass.Length; z++)
                                                                        {
                                                                            var temp = drClass[0];
                                                                            if (drClass.Length > 1 && temp == drClass[z])// Repeat
                                                                            {
                                                                                continue;
                                                                            }
                                                                            else
                                                                            {
                                                                                temp = drClass[z];
                                                                            }
                                                                            valueInfo = new SAIFUserInfoEntity.Value();
                                                                            valueInfo.ID = temp["ClassInfoID"].ToString();
                                                                            valueInfo.Text = temp["ClassInfoName"].ToString();
                                                                            lValue.Add(valueInfo);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        case 104://籍贯城市类型，1为籍贯，2为常住，3为常来往为1和2的时候，一般为一个城市 ，常来往城市为多个
                                                            {
                                                                LiveInfoHandle(dtLive, lValue, valueInfo, 1);
                                                            }
                                                            break;
                                                        case 105://常驻
                                                            {
                                                                LiveInfoHandle(dtLive, lValue, valueInfo, 2);
                                                            }
                                                            break;
                                                        case 106://常来往
                                                            {
                                                                LiveInfoHandle(dtLive, lValue, valueInfo, 3);
                                                            }
                                                            break;
                                                        case 107://行业
                                                            {
                                                                IndustryInfoHandle(dtVip, dtIndustry, controlInfo, lValue, valueInfo, 1);
                                                            }
                                                            break;
                                                        case 108://关注的行业
                                                            {
                                                                IndustryInfoHandle(dtVip, dtIndustry, controlInfo, lValue, valueInfo, 2);
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }


                                                }

                                                if (lValue == null || lValue.Count == 0)
                                                {
                                                    SAIFUserInfoEntity.Value vInfo = new SAIFUserInfoEntity.Value();
                                                    vInfo.ID = "";
                                                    vInfo.Text = "";
                                                    lValue.Add(vInfo);
                                                }
                                                controlInfo.Values = lValue.ToArray();
                                            }
                                            #endregion

                                            if (drControl[k]["ControlType"] != DBNull.Value)
                                                controlInfo.ControlType = int.Parse(drControl[k]["ControlType"].ToString());
                                            if (drControl[k]["AuthType"] != DBNull.Value)
                                                controlInfo.AuthType = int.Parse(drControl[k]["AuthType"].ToString());
                                            if (drControl[k]["MaxLength"] != DBNull.Value)
                                                controlInfo.MaxLength = int.Parse(drControl[k]["MaxLength"].ToString());
                                            if (drControl[k]["MaxSelected"] != DBNull.Value)
                                                controlInfo.MaxSelected = int.Parse(drControl[k]["MaxSelected"].ToString());
                                            if (drControl[k]["MinLength"] != DBNull.Value)
                                                controlInfo.MinLength = int.Parse(drControl[k]["MinLength"].ToString());
                                            if (drControl[k]["MinSelected"] != DBNull.Value)
                                                controlInfo.MinSelected = int.Parse(drControl[k]["MinSelected"].ToString());
                                            if (drControl[k]["IsMustDo"] != DBNull.Value)
                                                controlInfo.IsMustDo = int.Parse(drControl[k]["IsMustDo"].ToString());
                                            if (drControl[k]["IsEdit"] != DBNull.Value)
                                                controlInfo.IsEdit = int.Parse(drControl[k]["IsEdit"].ToString());
                                            if (drControl[k]["IsPrivacy"] != DBNull.Value)
                                                controlInfo.IsPrivacy = int.Parse(drControl[k]["IsPrivacy"].ToString());
                                            if (drControl[k]["OperationStatus"] != DBNull.Value)
                                                controlInfo.PrivacyValue = drControl[k]["OperationStatus"].ToString();


                                            if (!string.IsNullOrEmpty(drControl[k]["CorrelationValue"].ToString().Trim()))
                                            {
                                                DataRow[] drOption = (dtOption != null) ? dtOption.Select("OptionName='" + drControl[k]["CorrelationValue"] + "'", " Sequence asc") : null;
                                                if (drOption != null && drOption.Length > 0)
                                                {
                                                    List<SAIFUserInfoEntity.Option> lOption = new List<SAIFUserInfoEntity.Option>();
                                                    for (int l = 0; l < drOption.Length; l++)
                                                    {
                                                        //Option
                                                        SAIFUserInfoEntity.Option optionInfo = new SAIFUserInfoEntity.Option();
                                                        optionInfo.IsSelected = 0;
                                                        optionInfo.OptionID = drOption[l]["OptionValue"].ToString();
                                                        optionInfo.OptionText = drOption[l]["OptionText"].ToString();
                                                        lOption.Add(optionInfo);
                                                    }
                                                    controlInfo.Options = lOption.ToArray();
                                                }
                                            }
                                            lControl.Add(controlInfo);
                                        }
                                        blockInfo.Controls = lControl.ToArray();
                                    }
                                    lBlock.Add(blockInfo);
                                }
                                pageInfo.Blocks = lBlock.ToArray();
                            }
                            lPage.Add(pageInfo);
                        }
                    }
                }
            }

            //Return
            return lPage.ToArray();
        }
        /// <summary>
        /// 行业类别信息
        /// </summary>
        /// <param name="dtVip">用户信息</param>
        /// <param name="dtIndustry">行业信息</param>
        /// <param name="controlInfo">控件信息</param>
        /// <param name="lValue">信息集合</param>
        /// <param name="valueInfo">信息对象</param>
        /// <param name="type">我的类别</param>
        private static void IndustryInfoHandle(DataTable dtVip, DataTable dtIndustry, SAIFUserInfoEntity.Control controlInfo, List<SAIFUserInfoEntity.Value> lValue, SAIFUserInfoEntity.Value valueInfo, int type)
        {
            if (dtIndustry != null)
            {
                #region Industry
                DataRow[] drIndus = dtIndustry.Select(string.Format(string.Format(" IndustryType2 = {0} ", type), dtVip.Rows[0][controlInfo.ColumnName].ToString())) ?? null;
                if (drIndus != null)
                {
                    foreach (var indus in drIndus)
                    {
                        //Parent
                        valueInfo = new SAIFUserInfoEntity.Value();
                        valueInfo.ID = indus["IndustryID"].ToString();
                        valueInfo.Text = indus["IndustryName"].ToString();
                        lValue.Add(valueInfo);
                        //Children
                        DataRow[] drIndusChil = dtIndustry.Select(string.Format("ParentID='{0}' and IndustryType= 2 ", indus["IndustryID"])) ?? null;
                        if (drIndusChil != null)
                        {
                            foreach (var indusChil in drIndusChil)
                            {
                                valueInfo = new SAIFUserInfoEntity.Value();
                                valueInfo.ID = indus["IndustryID"].ToString();
                                valueInfo.Text = indus["IndustryName"].ToString();
                                lValue.Add(valueInfo);
                            }
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 获取居住类别信息
        /// </summary>
        /// <param name="dtLive">居住信息</param>
        /// <param name="lValue">信息集合</param>
        /// <param name="valueInfo">具体居住地对象</param>
        /// <param name="type">居住类别</param>
        private static void LiveInfoHandle(DataTable dtLive, List<SAIFUserInfoEntity.Value> lValue, SAIFUserInfoEntity.Value valueInfo, int type)
        {
            if (dtLive != null)
            {
                //Access * Table
                DataRow[] drObj = dtLive.Select(string.Format("CityType = '{0}'", type)) ?? null;
                if (drObj != null)
                {
                    foreach (var jg in drObj)
                    {
                        valueInfo = new SAIFUserInfoEntity.Value();
                        valueInfo.ID = jg["CityCode"].ToString();
                        valueInfo.Text = jg["CityName"].ToString();
                        lValue.Add(valueInfo);
                    }
                }
            }
        }
        /// <summary>
        /// 获取具体地域信息
        /// </summary>
        /// <param name="dtVip">用户信息</param>
        /// <param name="dtCity">地域信息</param>
        /// <param name="controlInfo">控件信息</param>
        /// <param name="lValue">信息集合</param>
        /// <param name="valueInfo">具体信息对象</param>
        /// <param name="cityName">地域类别</param>
        private static void CityInfoHandle(DataTable dtVip, DataTable dtCity, SAIFUserInfoEntity.Control controlInfo, List<SAIFUserInfoEntity.Value> lValue, SAIFUserInfoEntity.Value valueInfo, string cityName)
        {
            if (dtCity != null)
            {
                DataRow drSheng = dtCity.Select(string.Format("city_code like '{0}%'", dtVip.Rows[0][controlInfo.ColumnName].ToString())).First() ?? null;
                if (drSheng != null)
                {
                    valueInfo = new SAIFUserInfoEntity.Value();
                    valueInfo.ID = dtVip.Rows[0][controlInfo.ColumnName].ToString();
                    valueInfo.Text = drSheng[cityName].ToString();
                    lValue.Add(valueInfo);
                }
            }
        }

        #endregion

        #region 执行提交数据
        /// <summary>
        /// 执行提交数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int SubmitSql(string pSql)
        {
            return this._currentDAO.SubmitSql(pSql);
        }
        #endregion

    }
}
