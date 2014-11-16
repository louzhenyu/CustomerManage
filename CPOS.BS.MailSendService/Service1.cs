using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Configuration;
using System.Threading;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web;
using Aspose.Cells;
using System.IO;
using JIT.Utility.Notification;


namespace CPOS.BS.MailSendService
{
    public partial class Service1 : ServiceBase
    {
        private TimeSpan _timeSpan;
        private BackgroundWorker _worker;
        LoggingSessionInfo loggingSessionInfo;

        public Service1()
        {
            _worker = new BackgroundWorker();
            var time = ConfigurationManager.AppSettings["Timing"];        
            TimeSpan.TryParse(time, out _timeSpan);
            _worker.DoWork += (s, e) => DoWork();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _worker.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            _worker.Dispose();
        }

        private void DoWork()
        {
            while (true)
            {

                try
                {

                    ConfigurationManager.RefreshSection("appSettings");
                    var time = ConfigurationManager.AppSettings["Timing"];

                    TimeSpan.TryParse(time, out _timeSpan);
                    if (DateTime.Now.TimeOfDay.Hours == _timeSpan.Hours)
                    {
                        //支持配置多客户
                        string[] customerIDs = ConfigurationManager.AppSettings["CustomerIDs"].Split(',');

                        foreach (string customerID in customerIDs)
                        {

                            loggingSessionInfo = Default.GetBSLoggingSession(customerID, "1");
                            var eventsService = new LEventsBLL(loggingSessionInfo);

                            LEventsEntity queryEntity = new LEventsEntity();
                            queryEntity.EventType = 0;
                            var data = eventsService.WEventGetWebEvents(queryEntity, 0, 100000000);

                            foreach (var item in data)
                            {

                                XieHuiBaoBLL b = new XieHuiBaoBLL(loggingSessionInfo, "vip");

                                List<DefindControlEntity> l = new List<DefindControlEntity>();
                                PageResultEntity pageEntity = null;
                                try
                                {
                                    pageEntity = b.GetPageDataByEventID(l, 100000000, 0, item.EventID);
                                }
                                catch (Exception e)
                                {
                                    Loggers.Debug(new DebugLogInfo
                                    {
                                        Message = string.Format("{0} {1} (EventID:{3})报名人员信息通知邮件发送{2}", DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay, "失败", item.EventID)
                                    });
                                    Loggers.Exception(new BasicUserInfo(), e);
                                    continue;
                                }

                                if (pageEntity.GridData != null)
                                {
                                    if (pageEntity.GridData.Rows.Count == 0)
                                    {
                                        continue;
                                    }
                                }

                                GridInitEntity g = GetInitGridDataByEventID(item.EventID);
                                if (pageEntity != null && pageEntity.GridData != null)
                                {
                                    #region 替换标题信息
                                    if (g != null && g.GridColumnDefinds != null)
                                    {
                                        if (pageEntity.GridData.Columns.Contains("ROW_NUMBER"))
                                        {
                                            pageEntity.GridData.Columns.Remove("ROW_NUMBER");
                                        }
                                        if (pageEntity.GridData.Columns.Contains("SignUpID"))
                                        {
                                            pageEntity.GridData.Columns.Remove("SignUpID");
                                        }

                                        if (pageEntity.GridData.Columns.Count == 0)
                                        {
                                            continue;
                                        }


                                        for (int i = 0; i < pageEntity.GridData.Columns.Count; i++)
                                        {
                                            for (int j = 0; j < g.GridColumnDefinds.Count; j++)
                                            {
                                                if (pageEntity.GridData.Columns[i].ColumnName.ToLower() == g.GridColumnDefinds[j].DataIndex.ToLower())
                                                {
                                                    pageEntity.GridData.Columns[i].ColumnName = g.GridColumnDefinds[j].ColumnText;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                //数据获取        
                                Workbook wb = DataTableExporter.WriteXLS(pageEntity.GridData, 0);
                                wb.Worksheets[0].Name = "参加活动人员信息";

                                string savePath = ConfigurationManager.AppSettings["FileURL"];
                                if (!Directory.Exists(savePath))
                                {
                                    Directory.CreateDirectory(savePath);
                                }
                                savePath = savePath + "/" + item.Title + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                                wb.Save(savePath);//保存Excel文件

                                #region 邮件发送
                                FromSetting fs = new FromSetting();
                                fs.SMTPServer = ConfigurationManager.AppSettings["SmtpServer"];
                                fs.SendFrom = ConfigurationManager.AppSettings["MailSendFrom"];
                                fs.UserName = ConfigurationManager.AppSettings["MailUserName"];
                                fs.Password = ConfigurationManager.AppSettings["MailUserPassword"];

                                string mailTo = ConfigurationManager.AppSettings["MailTo"];
                                string isSendMail = ConfigurationManager.AppSettings["IsSendMailToCreator"];

                                //是否发送成功
                                bool result = false;

                                if (!string.IsNullOrEmpty(mailTo))
                                {
                                    result = Mail.SendMail(fs, mailTo, ConfigurationManager.AppSettings["MailTitle"], ConfigurationManager.AppSettings["Subject"], savePath.Split(','));
                                }

                                if (isSendMail == "1")
                                {

                                    if (!string.IsNullOrEmpty(item.Email))
                                    {
                                        if (DateTime.Now < DateTime.Parse(item.EndTime))
                                        {
                                            //等于0表示不发送邮件
                                            if (item.MailSendInterval == null || item.MailSendInterval == 0)
                                            {
                                                continue;
                                            }

                                            //日期差与间隔时间取余判断是否符合发送邮件日期
                                            int dateDiff = ((TimeSpan)(DateTime.Now.Date - item.LastUpdateTime.Value.Date)).Days;
                                            if (dateDiff % item.MailSendInterval != 0)
                                            {
                                                continue;
                                            }


                                            result = Mail.SendMail(fs, item.Email, ConfigurationManager.AppSettings["MailTitle"], ConfigurationManager.AppSettings["Subject"], savePath.Split(','));

                                            Loggers.Debug(new DebugLogInfo
                                            {
                                                Message = string.Format("{0} {1} (EventID:{3})报名人员信息通知邮件发送{2}", DateTime.Now.ToShortDateString(), DateTime.Now.TimeOfDay, result ? "成功" : "失败", item.EventID)
                                            });

                                        }
                                    }

                                }
                                #endregion
                            }

                        }


                    }
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new BasicUserInfo(), ex);
                }
                Thread.Sleep(new TimeSpan(0, 1, 0, 0));

            }
        }

        public class GridInitEntity
        {
            #region 属性集
            /// <summary>
            /// 表格数据定义
            /// </summary>
            public List<GridColumnModelEntity> GridDataDefinds { get; set; }

            /// <summary>
            /// 表格表头定义
            /// </summary>
            public List<GridColumnEntity> GridColumnDefinds { get; set; }

            /// <summary>
            /// 表格数据
            /// </summary>
            public PageResultEntity GridDatas { get; set; }
            #endregion
        }

        #region GetGridDataModelsByEventID 获取列的模型
        /// <summary>
        /// 获取列的模型
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModelsByEventID(string pEventID)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(loggingSessionInfo, "vip");
            return bll.GetGridDataModelsByEventID(pEventID);
        }
        #endregion   

        #region  GetGridColumnsByEventID 获取配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="pEventID"></param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumnsByEventID(string pEventID)
        {
            XieHuiBaoBLL bll = new XieHuiBaoBLL(loggingSessionInfo, "vip");
            return bll.GetGridColumnsByEventID(pEventID);
        }
        #endregion

        #region GetInitGridDataByEventID 获取列的模型和列
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        private GridInitEntity GetInitGridDataByEventID(string pEventID)
        {
            GridInitEntity g = new GridInitEntity();
            if (!string.IsNullOrEmpty(pEventID))
            {
                g.GridDataDefinds = GetGridDataModelsByEventID(pEventID);
                g.GridColumnDefinds = GetGridColumnsByEventID(pEventID);
            }
            return g;
        }
        #endregion


    }
}
