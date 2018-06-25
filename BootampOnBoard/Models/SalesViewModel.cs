using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootampOnBoard.Models
{
    public class SalesViewModel
    {
        public int Ps_Id { get; set; }
        public int Ps_CustomerId { get; set; }
        public int Ps_ProductId { get; set; }
        public int Ps_StoreId { get; set; }
        public System.DateTime DateSold { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string StoreName { get; set; }
    }
}