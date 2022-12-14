using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaQNIN1.Viewmodel
{
    public class CaptianData
    {
        public int Taqninid { get; set; }
        public string id_no { get; set; }
        public string name { get; set; }

        public string activity { get; set; }
        public string governate { get; set; }

        public string income_no { get; set; }
        public string status { get; set; }
        public int id { get; set; }
        public string uploaddate { get; set; }
        public string person_upload { get; set; }
        public string ChangesCenterDescion { get; set; }
        public string geographic_person_response { get; set; }
        public string LegalFullfied { get; set; }
        public string tazalom { get; set; }
        public string OrderStatus { get; set; }
        public string responsestatus { get; set; }
        public string studentUser { get; set; }
        public string ResponseApproval { get; set; }

        public string captianUser { get; set; }
        public string CaptianApproval { get; set; }
        public int BackToCaptian { get; set; }

        public string ReviewerApproval { get; set; }
        public bool SuspendedOrder { get; set; }
       public string Reviewer{get;set;}

       public string responsedate{get;set;}
    }
}