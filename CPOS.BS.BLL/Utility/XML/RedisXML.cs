using CPOS.Common.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace JIT.CPOS.BS.BLL.Utility
{
    public class RedisXML
    {
        //static string XmlDocPath = "~/Base/XML/RedisReadDBCount.xml";
        //static XmlManager _XmlManager = new XmlManager(XmlDocPath);
        //webconfig里增加开关，关闭时，不记录从redis的读取次数。  但一直记录从数据库读取的次数
        public string RedisDBOpen {
            get {
                string _RedisDBOpen = ConfigurationManager.AppSettings["RedisDBOpen"];
                if (string.IsNullOrEmpty(_RedisDBOpen))
                {
                    _RedisDBOpen = "1";
                }
                return _RedisDBOpen;
            }
        }
        static Mutex m_Mutex = new Mutex();//使用了多线程的机制
        /// <summary>
        /// 向xml文件里写数据，如果文件里没有这个节点，就写新建一个这样的节点。
        /// </summary>
        /// <param name="modulekey"></param>
        /// <param name="modulename"></param>
        /// <param name="redisOrB">1：redis，2:DB</param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        public bool RedisReadDBCount(string modulekey, string modulename, int redisOrB)
        {
            if (RedisDBOpen == "0")
            {
                return true;
            }

        

m_Mutex.WaitOne();   

              string XmlDocPath = "~/Base/XML/RedisReadDBCount.xml";
         XmlManager _XmlManager = new XmlManager(XmlDocPath);

            //根据modulekey去取数据
            string xmlNodePath = "/Root/RedisOperation[modulekey/text()='" + modulekey + "']";
            XmlNode _node = _XmlManager.SelectSingleNode(xmlNodePath);

            if (_node != null)//修改原有数据
            {
                if (redisOrB == 1) //读取redis
                {
                    if (RedisDBOpen == "1")//开启记录redis读取次数
                    {
                        string RedisCountTemp = _XmlManager.GetNodeText(xmlNodePath + "/" + "RedisCount");
                        int RedisCount = RedisCountTemp == "" ? 0 : Convert.ToInt32(RedisCountTemp);
                        _XmlManager.UpdateNode(xmlNodePath, new string[] { "RedisCount", "LastRedisTime" }, new string[] { (RedisCount + 1).ToString(), DateTime.Now.ToString() });
                    }
                }
                else
                { //读取数据库//一直记录
                    string CountTemp = _XmlManager.GetNodeText(xmlNodePath + "/" + "DBCount");
                    int Count = CountTemp == "" ? 0 : Convert.ToInt32(CountTemp);
                    _XmlManager.UpdateNode(xmlNodePath, new string[] { "DBCount", "LastDBTime" }, new string[] { (Count + 1).ToString(), DateTime.Now.ToString() });
                }
            }
            else
            {
                int redisCount = 0;
                int DBCount = 0;
                if (redisOrB == 1) //读取redis
                {
                    redisCount = 1;
                }
                else
                {
                    DBCount = 1;
                }

                //插入一个新节点
                _XmlManager.InsertNode("/Root", "RedisOperation", new string[] { "modulename", "modulekey", "RedisCount", "LastRedisTime", "DBCount", "LastDBTime" }, new string[] { modulename, modulekey, redisCount.ToString(), DateTime.Now.ToString(), DBCount.ToString(), DateTime.Now.ToString() });

            }
            _XmlManager.Save(XmlDocPath);

            ///读文档
            m_Mutex.ReleaseMutex();
            return true;
        }
    }
}
