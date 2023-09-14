using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getdata
{
    internal class jwtToken
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
        public bool hasPopupAnnouncements { get; set; }
    }
}
