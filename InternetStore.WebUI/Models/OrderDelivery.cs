using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetStore.WebUI.Models
{
    [Serializable]
    public class OrderDelivery
    {
        public string Delivery { get; set; }
        public string[] GetDelivery()
        {
            return new[] { "Oneself", "InHome" };
        }
    }
}
