using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using JIT.CPOS.BS.BLL;
using System.Data;
using JIT.CPOS.BS.Entity;

using System.Text.RegularExpressions;
using System.Configuration;


namespace JIT.CPOS.Web.ApplicationInterface.Module.WX.SysPage
{
    public class CreateCustomerConfigAH : BaseActionHandler<CreateCustomerConfigRP, CreateCustomerConfigRD>
    {
        protected override CreateCustomerConfigRD ProcessRequest(APIRequest<CreateCustomerConfigRP> pRequest)
        {
            CreateCustomerConfigRD rdRes = new CreateCustomerConfigRD();
            #region 1.写入文件。单个客户
            if (!string.IsNullOrWhiteSpace(CurrentUserInfo.ClientID.ToString()))//如果CustomerID不为空
            {
                try
                {
                    SysPageBLL bll = new SysPageBLL(this.CurrentUserInfo);
                    string strConfig = bll.GetCreateCustomerConfig(CurrentUserInfo.ClientID); //获取要生成的Config内容
                    strConfig = Regex.Replace(strConfig, @"\s", "");
                    //写入Config文件
                    string FileName = CurrentUserInfo.ClientID.ToString() + ".js";
                    string strpath = ConfigurationManager.AppSettings["CposWebConfigPath"];
                    if (!Directory.Exists(strpath))
                    {
                        Directory.CreateDirectory(strpath);
                    }
                    strpath = strpath + FileName; //存储文件路径
                    FileStream fs = new FileStream(strpath, FileMode.Create); //新建一个文件
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(strConfig);  //开始写入
                    sw.Flush();//清除缓冲区
                    sw.Close();//关闭流
                    // 写入varsion文件
                    string strVersion = bll.GetCreateCustomerVersion(CurrentUserInfo.ClientID,CurrentUserInfo.CurrentLoggingManager.Customer_Name); //获取要生成的Version内容
                    strVersion = Regex.Replace(strVersion, @"\s", "");
                    string FileNameVersion = CurrentUserInfo.ClientID.ToString() + ".js";
                    // string strpathVersion  = HttpContext.Current.Server.MapPath(@"../" +  "/HtmlApps/version/Test/").Replace("CPOS.BS.Web", "CPOS.Web");
                    string strpathVersion = ConfigurationManager.AppSettings["CposWebVersionPath"];
                    if (!Directory.Exists(strpathVersion))
                    {
                        Directory.CreateDirectory(strpathVersion);
                    }
                    strpathVersion = strpathVersion + FileNameVersion;//存储文件路径
                    FileStream fsVersion = new FileStream(strpathVersion, FileMode.Create); //新建一个文件
                    StreamWriter swVersion = new StreamWriter(fsVersion);
                    swVersion.Write(strVersion);  //开始写入
                    swVersion.Flush();//清除缓冲区
                    swVersion.Close();//关闭流
                }
                catch (Exception ex)
                {
                    JIT.Utility.Log.Loggers.Exception(new Utility.Log.ExceptionLogInfo(ex));
                    throw ex;
                }
            }
            #endregion
            #region 2.写入文件。全部客户
            else
            {
                var userInfo = new LoggingSessionInfo() { CurrentLoggingManager = new LoggingManager() };
                userInfo.CurrentLoggingManager.Connection_String = System.Configuration.ConfigurationManager.AppSettings["Conn_ap"];
                SysPageBLL BLL1 = new SysPageBLL(userInfo);
                DataTable dt = BLL1.GetCustomerInfo();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            string CustomerId = dt.Rows[i]["customer_id"].ToString();
                            var currentInfo = Default.GetBSLoggingSession(CustomerId, "");
                            SysPageBLL bll = new SysPageBLL(currentInfo);
                            string AllConfig = bll.GetCreateCustomerConfig(CustomerId); //获取要生成的Config内容
                            AllConfig = Regex.Replace(AllConfig, @"\s", "");

                            //写入Config文件
                            string FileName = CustomerId + ".js";

                            string strpath = ConfigurationManager.AppSettings["CposWebConfigPath"];
                            if (!Directory.Exists(strpath))
                            {
                                Directory.CreateDirectory(strpath);
                            }
                            strpath = strpath + FileName; //存储文件路径
                            // string outpath = HttpContext.Current.Server.MapPath(@"../" + strpath).Replace("CPOS.BS.Web", "CPOS.Web");
                            FileStream fs = new FileStream(strpath, FileMode.Create); //新建一个文件
                            StreamWriter sw = new StreamWriter(fs);
                            sw.Write(AllConfig);  //开始写入
                            sw.Flush();//清除缓冲区
                            sw.Close();//关闭流
                            // 写入varsion文件
                            string strVersion = bll.GetCreateCustomerVersion(CustomerId,CurrentUserInfo.CurrentLoggingManager.Customer_Name); //获取要生成的Version内容
                            strVersion = Regex.Replace(strVersion, @"\s", "");
                            string FileNameVersion = CustomerId + ".js";

                            //string strpathVersion = HttpContext.Current.Server.MapPath(@"../" + "/HtmlApps/version/Test/").Replace("CPOS.BS.Web", "CPOS.Web"); 
                            string strpathVersion = ConfigurationManager.AppSettings["CposWebVersionPath"];
                            if (!Directory.Exists(strpathVersion))
                            {
                                Directory.CreateDirectory(strpathVersion);
                            }
                            strpathVersion = strpathVersion + FileNameVersion;//存储文件路径

                            FileStream fsVersion = new FileStream(strpathVersion, FileMode.Create); //新建一个文件
                            StreamWriter swVersion = new StreamWriter(fsVersion);
                            swVersion.Write(strVersion);  //开始写入
                            swVersion.Flush();//清除缓冲区
                            swVersion.Close();//关闭流
                        }
                        catch (Exception ex)
                        {
                            JIT.Utility.Log.Loggers.Exception(new Utility.Log.ExceptionLogInfo(ex));
                            throw ex;
                        }
                    }
                }
            }
            #endregion
            return rdRes;
        }
    }
}