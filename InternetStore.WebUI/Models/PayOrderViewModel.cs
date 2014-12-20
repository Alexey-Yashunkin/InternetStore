using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetStore.WebUI.Models
{
    public class PayOrderViewModel
    {
        [DataType(DataType.CreditCard)]
        public string NumberCard { get; set; }
        public string CVV2 { get; set; }
    }
}