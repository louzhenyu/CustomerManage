using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.Web.ApplicationInterface.Project.PG
{
    public partial class StructureData : System.Web.UI.Page
    {
        //public static string m_customerID = "e703dbedadd943abacf864531decdac1";

        public static string m_customerID = "0ed1a737a178491c86278b001a059a15";
        public static string m_userID = "02AA1B9C39E941F498B2D406D4EB32F7";

        protected void Page_Load(object sender, EventArgs e)
        {
            AddCityButton.Click += AddCityButton_Click;
            DefaultTopicButton.Click += DefaultTopicButton_Click;

            SearchButton.Click += SearchButton_Click;
        }

        void SearchButton_Click(object sender, EventArgs e)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);
            PowerHourBLL bll = new PowerHourBLL(loggingSessionInfo);

            
        }

        /// <summary>
        /// 注册默认Topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DefaultTopicButton_Click(object sender, EventArgs e)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);
            DefaultTopicBLL bll = new DefaultTopicBLL(loggingSessionInfo);
            DefaultTopicEntity entity = new DefaultTopicEntity();
            entity.DefaultTopicID = Guid.NewGuid();
            entity.Index = Convert.ToInt32(IndexTextBox.Text);
            entity.Topic = TopicText.Text;
            entity.CustomerID = m_customerID;
            entity.CreateTime = DateTime.Now;
            bll.Create(entity);

        }

        /// <summary>
        /// 增加城市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddCityButton_Click(object sender, EventArgs e)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession(m_customerID, m_userID);
            CityBLL cityBll = new CityBLL(loggingSessionInfo);
            CityEntity city = new CityEntity();
            city.CityID = Guid.NewGuid();
            city.CityName = CityName.Text;
            city.LocalStaffCount = Convert.ToInt32(LocalStaffCount.Text);
            city.CustomerID = m_customerID;
            city.CreateTime = DateTime.Now;
            cityBll.Create(city);

        }
    }
}