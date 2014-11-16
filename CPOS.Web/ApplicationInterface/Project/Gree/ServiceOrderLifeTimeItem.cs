using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Project.Gree.ActionHandler;

namespace JIT.CPOS.Web.ApplicationInterface.Project.Gree
{
    public class ServiceOrderLifeTimeItem
    {
        private readonly SubscribeOrderViewModel _order;
        private readonly List<string> _applyList;
        private readonly DateTime _lifeTime;

        public string OrderWinner { get; set; }

        public bool IsActive
        {
            get
            {
                return DateTime.Now < _lifeTime;
            }
        }

        public SubscribeOrderViewModel ServiceOrder
        {
            get
            {
                return _order;
            }
        }

        public List<string> ApplyList
        {
            get
            {
                return _applyList;
            }
        }

        public DateTime LifeTime
        {
            get
            {
                return _lifeTime;
            }
        }

        public ServiceOrderLifeTimeItem(SubscribeOrderViewModel order, DateTime lifeTime)
        {
            _order = order;
            _applyList = new List<string>();
            _lifeTime = lifeTime;
        }

        public void Apply(string servicePersonId)
        {
            _applyList.Add(servicePersonId);
        }
    }
}