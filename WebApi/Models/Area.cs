using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Area
    {
        public string areaCode { get; set; }
        public int realtyid { get; set; }
    }



    public class RealtyImg
    {
        public string message { get; set; }
        public Datum data { get; set; }
    }

    public class Datum
    {
        public string realtyID { get; set; }
        public string fileURIId { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public string imageClass { get; set; }
        public int employeeID { get; set; }
        public string datetime { get; set; }
    }

}
