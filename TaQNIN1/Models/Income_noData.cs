using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaQNIN1.Models
{
    public class Income_noData
    {
        public int id { get; set; }
        public int ordersCount { get; set; }
        public int insideOrdersCount { get; set; }
        public int outsideOrdersCount { get; set; }
        public string uploaddate { get; set; }
        public string geographicperson { get; set; }
        public string PoineerApproval { get; set; }
        public string income_no { get; set; }
    }
}