using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace InternetStore.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public string OrderStatus { get; set; }
        public string PayStatus { get; set; }
        public string Payment { get; set; }
        public string Delivery { get; set; }
        public decimal TotalPrice { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public string Cart { get; set; }

        public Order()
        {
            this.StartDate = DateTime.Now;
            this.StopDate = this.StartDate.AddDays(1);
            this.PayStatus = "NotPaid";
            this.OrderStatus = "WillBeCreated";
        }

    }
}
