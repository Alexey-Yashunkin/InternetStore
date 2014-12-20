using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetStore.WebUI.Models
{
    [Serializable]
    public class OrderPayment
    {
        public string Payment { get; set; }
        public string[] GetPeyment()
        {
            return new[] { "PayCard", "PayMoney" };
        }
    }
}