using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITT_sys.Models
{
     public class TruckerModel
    {
        public string TruckId { get; set; }
        public string TruckType { get; set; }
        public string TruckSize { get; set; }
        public string Status { get; set; }
        public string registerDate { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
