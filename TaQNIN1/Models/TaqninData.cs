using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using DotSpatial.Topology;
using System.Data.Entity.Spatial;
namespace TaQNIN1.Models
{
    public class TaqninData
    {
        [Key]
        public int Taqninid { get; set; }
        public string id_no { get; set; }
        public string name { get; set; }
     
        public string activity { get; set; }
        public string governate { get; set; }
        public string unit { get; set; }
        public double area { get; set; }
        public string w_man { get; set; }
        public string tazalom { get; set; }
        public string study_note { get; set; }
        public string income_no { get; set; }
        public string status { get; set; }
        public double shapelength { get; set; }
        public double shapearea { get; set; }
        public double actualarea { get; set; }
        public string Commission { get; set; }
        public string Place { get; set; }
        public string Sheikhah { get; set; }
        public string center { get; set; }
        public string Anational { get; set; }
        public DbGeography PolygonData { get; set; }
         
        //public   MyProperty { get; set; }
        public string CreatedBY { get; set; }

        public string CreatedTime { get; set; }
        public string CreatedDevice { get; set; }
        public string Updated { get; set; }
        public string UpdatedTime { get; set; }
        public string UpdatedDevice { get; set; }

    }
}